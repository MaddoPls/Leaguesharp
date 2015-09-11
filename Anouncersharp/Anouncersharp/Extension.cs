using System.IO;
using System.Media;
using LeagueSharp;
using LeagueSharp.Common;

namespace Anouncersharp
{
	public static class Extension
	{
		/// <returns>Base respawn wait time in s</returns>
		public static int BRW(this Obj_AI_Hero champ)
		{
			return (int)((champ.Level * 2.5) + 7.5);
		}

		public static int DeathTime(this Obj_AI_Hero champ)
		{
			if (Game.ClockTime >= 0xb04)
			{
				return (int)(champ.BRW() + ((champ.BRW() / 25) * 12.5D));
			}

			if (Game.ClockTime >= 0x834)
			{
				return (int)((champ.BRW()) + ((champ.BRW() / 25) * (Game.ClockTime / 60 - 35)));
			}

			return champ.BRW();
		}

		public static void DisposePlayer(this SoundPlayer player, UnmanagedMemoryStream stream)
		{
			Utility.DelayAction.Add((int)(stream.WavPlayLength() * 2000), () => Func(player));
		}

		private static void Func(SoundPlayer player)
		{
			player.Dispose();
		}

		/// <returns>Strean play length in seconds, error marge with file header etc but w/e</returns>
		private static double WavPlayLength(this UnmanagedMemoryStream stream)
		{
			double playLength = (stream.Length * 8) / 705.6; //Bits divided by bitrate ofc lel
			return playLength;
		}

		public static bool get_bool(this Menu menu, string name)
		{
			return menu.Item(name).GetValue<bool>();
		}
	}
}
