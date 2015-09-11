using System;
using LeagueSharp;
using LeagueSharp.Common;
// ReSharper disable InconsistentNaming

namespace Anouncersharp
{
	public static class Manage
	{
		public static bool WelcomePlayed = false;
		public static bool MinionsSpawnedPlayed = false;
		public static bool FirstBloodPlayed = false;

		public static bool IsSummonersRift { get { return Game.MapId == GameMapId.SummonersRift; } }
		public static float RealTime;
		public static Enumerations.SoundPacks CurrentAnnouncer = Enumerations.SoundPacks.Portal2;
		public static Menu Menu;
		public static Random Rand = new Random();

		/// <summary>
		/// The amount of different sounds/event
		/// </summary>
		public static class Sounds
		{
			public static class Portal2
			{
				public static class Kills
				{
					public static int EnemyFirstBlood =1;
					public static int EnemyKill = 2;
					public static int EnemyDouble = 1;
					public static int EnemyTriple = 1;
					public static int EnemyQuadra = 1;
					public static int EnemyPenta = 1;
					public static int AllyFirstBlood = 1;
					public static int AllyKill = 1;
					public static int AllyDouble = 1;
					public static int AllyTriple = 1;
					public static int AllyQuadra = 1;
					public static int AllyPenta = 1;


				}

				public static class Notify
				{
					public static int Welcome = 7;
					public static int Win = 1;
					public static int Defeat = 1;
					public static int MinionSpawn = 1;
				}

				public static class Structures
				{
					public static class Enemy
					{
						public static int Top_T1 = 1;
						public static int Mid_T1 = 1;
						public static int Bot_T1 = 1;
						public static int Top_T2 = 1;
						public static int Mid_T2 = 1;
						public static int Bot_T2 = 1;
						public static int Top_T3 = 1;
						public static int Mid_T3 = 1;
						public static int Bot_T3 = 1;
						public static int Top_Inhib = 1;
						public static int Mid_Inhib = 1;
						public static int Bot_Inhib = 1;
						public static int Nexus_Tower = 1;
					}

					public static class Ally
					{
						public static int Top_T1 = 1;
						public static int Mid_T1 = 1;
						public static int Bot_T1 = 1;
						public static int Top_T2 = 1;
						public static int Mid_T2 = 1;
						public static int Bot_T2 = 1;
						public static int Top_T3 = 1;
						public static int Mid_T3 = 1;
						public static int Bot_T3 = 1;
						public static int Top_Inhib = 1;
						public static int Mid_Inhib = 1;
						public static int Bot_Inhib = 1;
						public static int Nexus_Tower = 1;
					}
				}
			}

			public static class StanleyParable
			{
				public static class Kills
				{
					public static int EnemyFirstBlood = 1;//
					public static int EnemyKill = 1;
					public static int EnemyDouble = 1;
					public static int EnemyTriple = 1;
					public static int EnemyQuadra = 1;
					public static int EnemyPenta = 1;
					public static int AllyFirstBlood = 1;
					public static int AllyKill = 1;
					public static int AllyDouble = 1;
					public static int AllyTriple = 1;
					public static int AllyQuadra = 1;
					public static int AllyPenta = 1;
				}

				public static class Notify
				{
					public static int Welcome = 7;
					public static int Win = 1;
					public static int Defeat = 1;
					public static int MinionSpawn = 1;
				}

				public static class Structures
				{
					public static class Enemy
					{
						public static int Top_T1 = 3;
						public static int Mid_T1 = 3;
						public static int Bot_T1 = 3;
						public static int Top_T2 = 3;
						public static int Mid_T2 = 3;
						public static int Bot_T2 = 3;
						public static int Top_T3 = 3;
						public static int Mid_T3 = 3;
						public static int Bot_T3 = 3;
						public static int Top_Inhib = 1;
						public static int Mid_Inhib = 1;
						public static int Bot_Inhib = 1;
						public static int Nexus_Tower = 1;
					}

					public static class Ally
					{
						public static int Top_T1 = 3;
						public static int Mid_T1 = 3;
						public static int Bot_T1 = 3;
						public static int Top_T2 = 3;
						public static int Mid_T2 = 3;
						public static int Bot_T2 = 3;
						public static int Top_T3 = 3;
						public static int Mid_T3 = 3;
						public static int Bot_T3 = 3;
						public static int Top_Inhib = 1;
						public static int Mid_Inhib = 1;
						public static int Bot_Inhib = 1;
						public static int Nexus_Tower = 1;
					}
				}
			}
		}
	}
}
