using System;
using LeagueSharp;

namespace Anouncersharp.Handlers
{
	public static class Notify
	{
		public static void Handle()
		{
			try
			{
				if (!Manage.WelcomePlayed) Welcome();

				if (!Manage.MinionsSpawnedPlayed) MinionSpawn();
			}
			catch (Exception)
			{
				// ignored
			}
		}

		public static void Welcome()
		{
			if (!Manage.Menu.get_bool("announcer.welcome")) return;

			if (Game.ClockTime - Manage.RealTime >= 15)
			{
				PlayAbstractingLayer.Notify(Enumerations.Events.Notify.Welcome);
				Manage.WelcomePlayed = true;
			}
		}

		public static void MinionSpawn()
		{
			if (!Manage.Menu.get_bool("announcer.minions")) return;

			if (Game.ClockTime - Manage.RealTime >= 60)
			{
				PlayAbstractingLayer.Notify(Enumerations.Events.Notify.MinionSpawn);
				Manage.MinionsSpawnedPlayed = true;
			}
		}

		public static void Game_OnEnd(GameEndEventArgs args)
		{
			if (!Manage.Menu.get_bool("announcer.win.lose")) return;

			PlayAbstractingLayer.Notify(args.WinningTeam == Program.Player.Team
				? Enumerations.Events.Notify.Win
				: Enumerations.Events.Notify.Defeat);
		}
	}
}
