using System;
using System.IO;
using LeagueSharp;

namespace GamEventsId
{
	public static class Program
	{

		private static StreamWriter Log;
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
			Log = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt") {AutoFlush = true};
			Game.OnNotify += Game_OnNotify;
		}

		private static void Game_OnNotify(GameNotifyEventArgs args)
		{
			if (args.EventId != GameEventId.OnSurrenderVote)
			{
				Log.WriteLine(Game.ClockTime + ": " + args.EventId);
			}
		}
	}
}
