using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Kekplank
{
	public static class Casts
	{
		public static readonly List<BuffType> Debuffs = new List<BuffType>()
		{
			BuffType.Slow,
			BuffType.Blind,
			BuffType.Polymorph,
			BuffType.Stun,
			BuffType.Taunt,
			BuffType.Fear,
			BuffType.Charm
		};

		public static readonly List<BuffType> DebuffsMinusSlow = new List<BuffType>()
		{
			BuffType.Blind,
			BuffType.Polymorph,
			BuffType.Stun,
			BuffType.Taunt,
			BuffType.Fear,
			BuffType.Charm
		};

		public static void PreOrbwalker()
		{
			EWrapper.CastHelper.PopEWithAa();
			if (Program.Config.get_bool("Auto R", "Settings")) AutoR();
			if (Program.Config.get_bool("Auto Ignite", "Settings")) Ignite();
			if (Program.Config.get_bool("Auto W", "Settings") && Program.Config.ManaLimitCheck("Auto W manalimiter", "Settings"))
			{
				AutoW(Program.Config.get_bool("Auto W on slow", "Settings"));
			}
		}

		private static void Ignite()
		{
			int dmg = (Program.Player.Level * 20) + 50;
			foreach (Obj_AI_Hero tar in Program.Player.GetEnemiesInRange(Program.Ignite.Range).Where(tar => dmg >= tar.Health && Program.Ignite.IsReady()))
			{
				Program.Ignite.Cast(tar);
			}
		}

		private static void AutoW(bool includeSlow)
		{
			if (Program.Player.HasDebuffs(includeSlow) && Program.W.IsReady())
			{
				Program.W.Cast();
			}
		}

		private static void AutoR()
		{
			float dmg = 360 + (240*Program.R.Level) + ((Program.Player.TotalMagicalDamage/10)*12);
			Obj_AI_Hero tar = Program.R.GetTarget(20000);
			if (tar == null) return;
			if (Program.Player.CalcDamage(tar, Damage.DamageType.Magical, dmg) > tar.Health)
			{
				Program.R.CastOnUnit(tar);
			}
		}

		private static bool HasDebuffs(this Obj_AI_Hero hero, bool includeSlow)
		{
			return includeSlow ? Debuffs.Any(hero.HasBuffOfType) : DebuffsMinusSlow.Any(hero.HasBuffOfType);
		}

		private static void Q()
		{
			Obj_AI_Hero tar = Program.Q.GetTarget();
			if (tar == null || !Program.Q.CanCast(tar)) return;
			Program.Q.CastOnUnit(tar);
		}

		public static class Combo
		{
			public static void QEnemy()
			{
				Q();
			}

			public static void BustEWithQ()
			{
				EWrapper.CastHelper.PopEWithQ();
			}

			public static void E()
			{
				EWrapper.CastHelper.CastE();
			}
		}

		public static class Harass
		{
			public static void QEnemy()
			{
				Q();
			}
		}

		public static class Clear
		{
			public static void QMinion()
			{
				List<Obj_AI_Base> minions = MinionManager.GetMinions(Program.Player.Position, Program.Q.Range);
				if (minions.Count != 0)
				{
					Obj_AI_Base targetMinion = minions.FirstOrDefault();
					Debug.Assert(targetMinion != null, "targetMinion != null");
					if (targetMinion.Health < Program.Q.GetDamage(targetMinion) && Program.Player.Distance(targetMinion) > Program.Player.AttackRange && Program.Q.CanCast(targetMinion))
					{
						Program.Q.CastOnUnit(targetMinion);
					}
				}
			}

			public static void QEnemy()
			{
				Q();
			}
		}
	}
}
