using System;
using System.Drawing;
using Anouncersharp.Handlers;
using Anouncersharp.Misc;
using LeagueSharp;
using LeagueSharp.Common;

namespace Anouncersharp
{
	public static class Program
	{
		public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

		static void Main(string[] args)
		{
			try
			{
				if (Game.Mode == GameMode.Running)
				{
					Game_OnStart(new EventArgs());
				}

				Game.OnStart += Game_OnStart;
			}
			catch (Exception)
			{
				// ignored
			}
		}

		private static void Game_OnStart(EventArgs args)
		{
			if (!Manage.IsSummonersRift) return;

			if (Player.Gold >= 0) Manage.RealTime = Game.ClockTime;

			ShowNotification("Announcer# by blacky & HyunMi - Loaded", Color.DeepSkyBlue, 10000);

			MenuGen.Load();
			Game.OnEnd += Notify.Game_OnEnd;
			Game.OnUpdate += Game_OnUpdate;
			Game.OnNotify += Kills.Game_OnNotify;
            Game.OnInput += ChatCommands.Game_OnInput;
		}



        private static void Game_OnUpdate(EventArgs args)
		{
			if (!Manage.Menu.get_bool("announcer.enabled")) return;

			switch (Manage.Menu.Item("announcer.pack").GetValue<StringList>().SelectedIndex)
			{
				case 0:
					Manage.CurrentAnnouncer = Enumerations.SoundPacks.Portal2;
					break;
				case 1:
					Manage.CurrentAnnouncer = Enumerations.SoundPacks.StanlyParable;
					break;
			}

			Notify.Handle();
		}

		//Notifications Credits to Beaving.
		public static Notification ShowNotification(string message, Color color, int duration = -1, bool dispose = true)
		{
			Notification notif = new Notification(message).SetTextColor(color);
			Notifications.AddNotification(notif);

			if (dispose)
			{
				Utility.DelayAction.Add(duration, () => notif.Dispose());
			}

			return notif;
		}
	}
}
