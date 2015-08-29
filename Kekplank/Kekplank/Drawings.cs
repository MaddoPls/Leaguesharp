using System;
using System.Linq;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace Kekplank
{
	public static class Drawings
	{
		public static void Drawing_OnDraw(EventArgs args)
		{
			if (Program.Config.get_bool("Draw Q", "Drawings") && Program.Q.Level > 0) Q();

			if (Program.Config.get_bool("Draw E", "Drawings") && Program.E.Level > 0) E();

			if (Program.Config.get_bool("Draw focused barrel", "Drawings")) TarBarrel();
		}

		private static void Q()
		{
			Render.Circle.DrawCircle(Program.Player.Position, Program.Q.Range, Color.BlueViolet);
		}

		private static void E()
		{
			Render.Circle.DrawCircle(Program.Player.Position, Program.E.Range, Color.BlueViolet);
		}

		private static void TarBarrel()
		{
			EWrapper.Barrel tarBar = EWrapper.LiveBarrels.FirstOrDefault(barrel => barrel.IsReady && !(Program.Player.Distance(barrel.BarrelPos) > Program.Q.Range) && Program.Q.IsReady() && barrel.GameObj.IsValidTarget() && barrel.GameObj.IsTargetable && Program.Player.Distance(barrel.BarrelPos) > Program.Player.AttackRange);
			if (tarBar == null) return;
			Render.Circle.DrawCircle(tarBar.BarrelPos, 50, Color.DarkRed);
		}
	}
}
