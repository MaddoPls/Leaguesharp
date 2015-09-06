using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace Kekplank
{
	public static class Program
	{
		private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

		private static Menu _config;
		private static Orbwalking.Orbwalker _orbwalker;
		private static Spell _q = new Spell(SpellSlot.Q, 625);
		private static Spell _w = new Spell(SpellSlot.W);
		private static Spell _e = new Spell(SpellSlot.E, 1000);
		private static Spell _r = new Spell(SpellSlot.R);
		private static Spell _ignite = new Spell(Player.GetSpellSlot("summonerdot"), 600, TargetSelector.DamageType.True);
		private static List<Barrel> _livebarrels = new List<Barrel>();
		private static Barrel _targetedBarrelQ;
		private static float _connectionRange = 650;
		private static float _explosionRange = 400;

		static void Main(string[] args)
		{
			if (Game.Mode == GameMode.Running)
			{
				Game_OnGameStart(new EventArgs());
			}

			Game.OnStart += Game_OnGameStart;
		}

		private static void Game_OnGameStart(EventArgs args)
		{
			.
			//if (Player.ChampionName != "Gangplank") return;

			_r.SetSkillshot(0.7f, 200, float.MaxValue, false, SkillshotType.SkillshotCircle);

			LoadMenu();

			Game.OnUpdate += Game_OnUpdate;
			Drawing.OnDraw += Drawing_OnDraw;
			GameObject.OnCreate += GameObject_OnCreate;
			GameObject.OnDelete += GameObject_OnDelete;
		}

		private static void LoadMenu()
		{
			_config = new Menu("Kekplank", "main", true);

			Menu orb = _config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
			_orbwalker = new Orbwalking.Orbwalker(orb);

			Menu targetSelector = _config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
			TargetSelector.AddToMenu(targetSelector);

			Menu combo = new Menu("Combo", "main.combo");
			combo.Add_bool("Use Q", "main.combo.q", true);
			combo.Add_bool("Use E", "main.combo.e", true);
			combo.Add_bool("Bust E with Q", "main.combo.qe", true);

			Menu harass = new Menu("Harass", "main.harass");
			harass.Add_bool("Use Q", "main.harass.q", true);
			harass.AddItem(new MenuItem("main.harass.qm", "Manalimiter").SetValue(new Slider(55)));

			Menu clear = new Menu("Clear", "main.clear");
			clear.Add_bool("Q minions", "main.clear.q", true);
			clear.AddItem(new MenuItem("main.clear.qm", "Manalimiter").SetValue(new Slider(55)));

			Menu settings = new Menu("Settings", "main.settings");
			Menu wsettings = new Menu("W settings", "main.settings.w");
			wsettings.Add_bool("Slow", "main.settings.w.slow", false);
			wsettings.Add_bool("Blind", "main.settings.w.blind", true);
			wsettings.Add_bool("Polymorph", "main.settings.w.poly", true);
			wsettings.Add_bool("Stun", "main.settings.w.stun", true);
			wsettings.Add_bool("Taunt", "main.settings.w.taunt", true);
			wsettings.Add_bool("Fear", "main.settings.w.fear", true);
			wsettings.Add_bool("Charm", "main.settings.w.charm", true);
			wsettings.AddItem(new MenuItem("main.settings.w.wm", "Manalimiter").SetValue(new Slider(15)));
			wsettings.Add_bool("W enabled", "main.settings.w.enabled", true);
			settings.AddSubMenu(wsettings);
			settings.Add_bool("Auto R", "main.settings.r", true);
			settings.Add_bool("Auto ignite", "main.settings.ignite", true);
			settings.AddItem(
				new MenuItem("Disable E from casting", "main.settings.disablee").SetValue(new KeyBind(0x41, KeyBindType.Press)));

			Menu drawings = new Menu("Drawings", "main.drawings");
			drawings.Add_bool("Draw Q", "main.drawings.q", true);
			drawings.Add_bool("Draw E", "main.drawings.e", false);
			drawings.Add_bool("Draw focused barrel", "main.drawings.barrel", true);

			_config.AddSubMenu(combo);
			_config.AddSubMenu(harass);
			_config.AddSubMenu(clear);
			_config.AddSubMenu(settings);
			_config.AddSubMenu(drawings);
			_config.AddToMainMenu();
		}

		private static void Game_OnUpdate(EventArgs args)
		{
			if (Get_bool("main.settings.r")) Casts.R();
			if (Get_bool("main.settings.ignite")) Casts.Ignite();
			if (ManalimitCheck("main.settings.w.wm") && Get_bool("main.settings.w.enabled")) Casts.W();
			_targetedBarrelQ = null;
			_targetedBarrelQ = _livebarrels.FirstOrDefault(barrel => barrel.BarrelObj.IsValid && Player.Distance(barrel.BarrelObj) < _q.Range && barrel.IsReady);

			switch (_orbwalker.ActiveMode)
			{
				case Orbwalking.OrbwalkingMode.Combo:
					break;
				case Orbwalking.OrbwalkingMode.Mixed:
					break;
				case Orbwalking.OrbwalkingMode.LaneClear:
				case Orbwalking.OrbwalkingMode.LastHit:
					break;
			}
		}

		private static class Casts
		{
			internal static void R()
			{
				if (!_r.IsReady()) return;
				float dmg = 360 + (240*_r.Level) + ((Player.TotalMagicalDamage/10)*12);
				Obj_AI_Hero target = HeroManager.Enemies.FirstOrDefault(enemy => dmg > enemy.Health);
				if (target == null) return;
				_r.CastOnUnit(target);
			}

			internal static void W()
			{
				if (!_w.IsReady() || !Player.HasDebuffs()) return;
				_w.Cast();
			}

			internal static void Ignite()
			{
				int dmg = (Player.Level*20) + 50;
				foreach (Obj_AI_Hero tar in Player.GetEnemiesInRange(_ignite.Range).Where(tar => dmg >= tar.Health && _ignite.IsReady()))
				{
					_ignite.Cast(tar);
				}
			}

			internal static void QMinion(Obj_AI_Minion minion)
			{
				if (!_q.IsReady() || Player.Distance(minion.Position) > _q.Range) return;
				_q.CastOnUnit(minion);
			}

			internal static void QHero(Obj_AI_Hero hero)
			{
				if (!_q.IsReady() || Player.Distance(hero.Position) > _q.Range) return;
				_q.CastOnUnit(hero);
			}

			internal static void EMinion()
			{
				if (!_e.IsReady() || MinionManager.GetMinions(_e.Range).Count.Equals(0)) return;

				List<Vector2> minionPos = MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(_e.Range), _e.Delay, _e.Width, _e.Speed, Player.Position,
					_e.Range, false, _e.Type);
				MinionManager.FarmLocation castPos = MinionManager.GetBestCircularFarmLocation(minionPos, _e.Width, _e.Range);
				if (castPos.MinionsHit != 0) _e.Cast(castPos.Position);
			}

			internal static void EHero()
			{
				if (!_e.IsReady() ||
				    (Player.GetEnemiesInRange(_e.Range).Count == 0 && MinionManager.GetMinions(_e.Range).Count == 0)) return;

				if (_livebarrels.Count == 0)
				{
					List<Obj_AI_Hero> enemies = Player.GetEnemiesInRange(_e.Range);
					switch (enemies.Count)
					{
						case 0:
							return;
						case 1:
							_e.Cast(_e.GetPrediction(enemies[0], true).CastPosition);
							break;
						default:
							List<Vector2> positions = enemies.Select(enemy => enemy.Position.To2D()).ToList();
							int count = positions.Count;
							float x = 0, y = 0;
							foreach (Vector2 vector2 in positions)
							{
								x += vector2.X;
								y += vector2.Y;
							}
							x = x / count;
							y = y / count;
							Vector2 center = new Vector2(x, y);
							_e.Cast(center);
							break;
					}
				}
				else
				{
					List<Obj_AI_Hero> enemies = Player.GetEnemiesInRange(1200);
					switch (enemies.Count)
					{
						case 0:
							return;
						case 1:
							Obj_AI_Hero enemy = enemies[0];
							List<Barrel> avaibleBarrels = _livebarrels.Where(barrel => Player.Distance(enemy) > barrel.BarrelObj.Distance(enemy)).ToList();
							//if enemy is in explosion range of a barrel
							//	extend position of that barrel via the enemy to 90% of barrelConnectionRange*2 then shorten it back into the e cast range
							//		if the barrels connect cast it
							//		else shorten the barrel back into the connectionrange
							//else
							//	find closest barrel
							//		if enemy is in 90% of that barrels connectionrange*2 cast barrel on enemy
							//		else shorten the barrel back into the connectionrange
							//			if the barrel is in castrange cast it
							//			else shorten it back into the cast range

							break;
						default:
							break;
					}
				}
			}
		}

		internal static class Math
		{
			public static Barrel ClosestBarrelWhereEnemyInExplosionRange(Obj_AI_Hero hero)
			{
				Barrel[] inExplosionRange =
					_livebarrels.Where(barrel => barrel.BarrelObj.GetEnemiesInRange(_explosionRange).Count != 0).ToArray();
				if (inExplosionRange.Length == 0) return null;
				float[] distances = new float[inExplosionRange.Length];
				for (int i = 0; i < inExplosionRange.Length; i++)
				{
					distances[i] = Player.Distance(inExplosionRange[i].BarrelObj);
				}
				int index = distances.IndexOf(new[] {distances.Min()}).First();
				return inExplosionRange[index];
			}
		}

		private static void GameObject_OnDelete(GameObject sender, EventArgs args)
		{
			foreach (Barrel barrel in _livebarrels.Where(barrel => barrel.BarrelObj.NetworkId == sender.NetworkId)) _livebarrels.Remove(barrel);
		}

		private static void GameObject_OnCreate(GameObject sender, EventArgs args)
		{
			if (sender.Name == "Barrel") _livebarrels.Add(new Barrel(sender as Obj_AI_Minion));
		}

		private static void Drawing_OnDraw(EventArgs args)
		{
			if (Get_bool("main.drawings.q") && _q.Level > 0)
				Render.Circle.DrawCircle(Player.Position, _q.Range, _q.IsReady() ? Color.BlueViolet : Color.Red);

			if (Get_bool("main.drawings.e") && _e.Level > 0)
				Render.Circle.DrawCircle(Player.Position, _e.Range, _e.IsReady() ? Color.BlueViolet : Color.Red);

			if (Get_bool("main.drawings.barrel") && _q.Level > 0 && _targetedBarrelQ != null)
				Render.Circle.DrawCircle(_targetedBarrelQ.BarrelObj.Position, 100, _e.IsReady() ? Color.BlueViolet : Color.Red);
		}

		private static void Add_bool(this Menu menu, string displayName, string name, bool value)
		{
			menu.AddItem(new MenuItem(name, displayName).SetValue(value));
		}

		private static bool Get_bool(string name)
		{
			return _config.Item(name).GetValue<bool>();
		}

		private static bool ManalimitCheck(string name)
		{
			return Player.ManaPercent > _config.Item(name).GetValue<Slider>().Value;
		}

		private static bool HasDebuffs(this Obj_AI_Hero champ)
		{
			List<BuffType> debuffs = new List<BuffType>();
			if (Get_bool("main.settings.w.slow")) debuffs.Add(BuffType.Slow);
			if (Get_bool("main.settings.w.blind")) debuffs.Add(BuffType.Blind);
			if (Get_bool("main.settings.w.poly")) debuffs.Add(BuffType.Polymorph);
			if (Get_bool("main.settings.w.stun")) debuffs.Add(BuffType.Stun);
			if (Get_bool("main.settings.w.taunt")) debuffs.Add(BuffType.Taunt);
			if (Get_bool("main.settings.w.fear")) debuffs.Add(BuffType.Fear);
			if (Get_bool("main.settings.w.charm")) debuffs.Add(BuffType.Charm);
			return debuffs.Any(champ.HasBuffOfType);
		}

		internal class Barrel
		{
			public Obj_AI_Minion BarrelObj;
			public bool IsReady;

			public Barrel(Obj_AI_Minion barrelObj)
			{
				BarrelObj = barrelObj;
				DelayReady();
			}

			private void DelayReady()
			{
				int decayT = Player.Level > 6 ? (Player.Level > 12 ? 1000 : 2000) : 4000;
				Utility.DelayAction.Add(decayT, () => IsReady = true);
			}
		}
	}
}
