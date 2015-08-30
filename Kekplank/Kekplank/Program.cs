using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace Kekplank
{
	public static class Program
	{
		/*
			TODO LIST
			improve aa on e
			add damage drawings/imnprove drawings
		*/

		public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
		private const string Champion = "Gangplank";

		public static Menu Config;
		public static Orbwalking.Orbwalker Orbwalker;
		public static Spell Q, W, E, R, Ignite;

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
			if (Player.ChampionName != Champion) return;

			Q = new Spell(SpellSlot.Q, 625);
			W = new Spell(SpellSlot.W);
			E = new Spell(SpellSlot.E, 1150);
			R = new Spell(SpellSlot.R);
			R.SetSkillshot(0.7f, 200, float.MaxValue, false, SkillshotType.SkillshotCircle);
			Ignite = new Spell(Player.GetSpellSlot("summonerdot"), 600, TargetSelector.DamageType.True);

			Config = Configuration.get_menu();
			Config.AddToMainMenu();
			
			Game.OnUpdate += Game_OnUpdate;
			Drawing.OnDraw += Drawings.Drawing_OnDraw;
			GameObject.OnCreate += EWrapper.GameObject_OnCreate;
		}

		private static void Game_OnUpdate(EventArgs args)
		{
			Casts.PreOrbwalker();

			switch (Orbwalker.ActiveMode)
			{
				case Orbwalking.OrbwalkingMode.Combo:
					EWrapper.CastHelper.PopEWithAa();
					Combo();
					break;
				case Orbwalking.OrbwalkingMode.Mixed:
					EWrapper.CastHelper.PopEWithAa();
					Harass();
					break;
				case Orbwalking.OrbwalkingMode.LaneClear:
				case Orbwalking.OrbwalkingMode.LastHit:
					EWrapper.CastHelper.PopEWithAa();
					Clear();
					break;
			}
		}

		private static void Combo()
		{
			if (Config.get_bool("Bust E with Q", "Combo")) Casts.Combo.BustEWithQ();

			if (Config.get_bool("Use E", "Combo")) Casts.Combo.E();

			if (Config.get_bool("Use Q", "Combo")) Casts.Combo.QEnemy();
		}

		private static void Harass()
		{
			if (Config.get_bool("Use Q", "Harass") && Config.ManaLimitCheck("Manalimiter Q", "Harass")) Casts.Harass.QEnemy();
		}

		private static void Clear()
		{
			if (!Config.ManaLimitCheck("Mana limiter Q", "Clear")) return;

			if (Config.get_bool("Prioritize Minions", "Clear"))
			{
				if (Config.get_bool("Use Q Minions", "Clear")) Casts.Clear.QMinion();

				if (Config.get_bool("Use Q Harass", "Clear")) Casts.Clear.QEnemy();
			}
			else
			{
				if (Config.get_bool("Use Q Harass", "Clear")) Casts.Clear.QEnemy();

				if (Config.get_bool("Use Q Minions", "Clear")) Casts.Clear.QMinion();
			}
		}
	}
}
