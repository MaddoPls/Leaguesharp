using System;
using LeagueSharp;

namespace GamEventsId
{
	public static class Program
	{
		static void Main(string[] args)
		{
			if (Game.Mode == GameMode.Running)
			{
				Game_OnStart(new EventArgs());
			}

			Game.OnStart += Game_OnStart;
		}

		private static void Game_OnStart(EventArgs args)
		{
			Game.OnNotify += Game_OnNotify;
		}

		private static void Game_OnNotify(GameNotifyEventArgs args)
		{
		    if (args.EventId == GameEventId.OnTurretKill) Game.PrintChat("Turret killed");
		}
	}
}
