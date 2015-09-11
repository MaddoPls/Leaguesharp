// ReSharper disable InconsistentNaming

namespace Anouncersharp.Packs
{
	public static class StanleyParable
	{
		public static void Kills(Enumerations.Events.Kills enumCase)
		{
			switch (enumCase)
			{
				case Enumerations.Events.Kills.EnemyFirstblood:
					AbstractionLayer._Kills.EnemyFirstblood();
					break;
				case Enumerations.Events.Kills.EnemyKill:
					AbstractionLayer._Kills.EnemyKill();
					break;
				case Enumerations.Events.Kills.EnemyDouble:
					AbstractionLayer._Kills.EnemyDouble();
					break;
				case Enumerations.Events.Kills.EnemyTriple:
					AbstractionLayer._Kills.EnemyTriple();
					break;
				case Enumerations.Events.Kills.EnemyQuadra:
					AbstractionLayer._Kills.EnemyQuadra();
					break;
				case Enumerations.Events.Kills.EnemyPenta:
					AbstractionLayer._Kills.EnemyPenta();
					break;
				case Enumerations.Events.Kills.AllyFirstblood:
					AbstractionLayer._Kills.AllyFirstblood();
					break;
				case Enumerations.Events.Kills.AllyKill:
					AbstractionLayer._Kills.AllyKill();
					break;
				case Enumerations.Events.Kills.AllyDouble:
					AbstractionLayer._Kills.AllyDouble();
					break;
				case Enumerations.Events.Kills.AllyTriple:
					AbstractionLayer._Kills.AllyTriple();
					break;
				case Enumerations.Events.Kills.AllyQuadra:
					AbstractionLayer._Kills.AllyQuadra();
					break;
				case Enumerations.Events.Kills.AllyPenta:
					AbstractionLayer._Kills.AllyPenta();
					break;
			}
		}

		public static void Notify(Enumerations.Events.Notify enumCase)
		{
			switch (enumCase)
			{
				case Enumerations.Events.Notify.Defeat:
					AbstractionLayer._Notify.Defeat();
					break;
				case Enumerations.Events.Notify.Win:
					AbstractionLayer._Notify.Win();
					break;
				case Enumerations.Events.Notify.MinionSpawn:
					AbstractionLayer._Notify.MinionSpawn();
					break;
				case Enumerations.Events.Notify.Welcome:
					AbstractionLayer._Notify.Welcome();
					break;
			}
		}

		public static void Structures(Enumerations.Events.Structures enumCase)
		{
			switch (enumCase)
			{
				case Enumerations.Events.Structures.EnemyTop_T1:
					AbstractionLayer._Structures.Enemy.Top_T1();
					break;
				case Enumerations.Events.Structures.EnemyMid_T1:
					AbstractionLayer._Structures.Enemy.Mid_T1();
					break;
				case Enumerations.Events.Structures.EnemyBot_T1:
					AbstractionLayer._Structures.Enemy.Bot_T1();
					break;
				case Enumerations.Events.Structures.EnemyTop_T2:
					AbstractionLayer._Structures.Enemy.Top_T2();
					break;
				case Enumerations.Events.Structures.EnemyMid_T2:
					AbstractionLayer._Structures.Enemy.Mid_T2();
					break;
				case Enumerations.Events.Structures.EnemyBot_T2:
					AbstractionLayer._Structures.Enemy.Bot_T2();
					break;
				case Enumerations.Events.Structures.EnemyTop_T3:
					AbstractionLayer._Structures.Enemy.Top_T3();
					break;
				case Enumerations.Events.Structures.EnemyMid_T3:
					AbstractionLayer._Structures.Enemy.Mid_T3();
					break;
				case Enumerations.Events.Structures.EnemyBot_T3:
					AbstractionLayer._Structures.Enemy.Bot_T3();
					break;
				case Enumerations.Events.Structures.EnemyTop_InHib:
					AbstractionLayer._Structures.Enemy.Top_Inhib();
					break;
				case Enumerations.Events.Structures.EnemyMid_InHib:
					AbstractionLayer._Structures.Enemy.Mid_Inhib();
					break;
				case Enumerations.Events.Structures.EnemyBot_InHib:
					AbstractionLayer._Structures.Enemy.Bot_Inhib();
					break;
				case Enumerations.Events.Structures.EnemyNexus_Turrets:
					AbstractionLayer._Structures.Enemy.Nexus_Tower();
					break;
				case Enumerations.Events.Structures.AllyTop_T1:
					AbstractionLayer._Structures.Ally.Top_T1();
					break;
				case Enumerations.Events.Structures.AllyMid_T1:
					AbstractionLayer._Structures.Ally.Mid_T1();
					break;
				case Enumerations.Events.Structures.AllyBot_T1:
					AbstractionLayer._Structures.Ally.Bot_T1();
					break;
				case Enumerations.Events.Structures.AllyTop_T2:
					AbstractionLayer._Structures.Ally.Top_T2();
					break;
				case Enumerations.Events.Structures.AllyMid_T2:
					AbstractionLayer._Structures.Ally.Mid_T2();
					break;
				case Enumerations.Events.Structures.AllyBot_T2:
					AbstractionLayer._Structures.Ally.Bot_T2();
					break;
				case Enumerations.Events.Structures.AllyTop_T3:
					AbstractionLayer._Structures.Ally.Top_T3();
					break;
				case Enumerations.Events.Structures.AllyMid_T3:
					AbstractionLayer._Structures.Ally.Mid_T3();
					break;
				case Enumerations.Events.Structures.AllyBot_T3:
					AbstractionLayer._Structures.Ally.Bot_T3();
					break;
				case Enumerations.Events.Structures.AllyTop_InHib:
					AbstractionLayer._Structures.Ally.Top_Inhib();
					break;
				case Enumerations.Events.Structures.AllyMid_InHib:
					AbstractionLayer._Structures.Ally.Mid_Inhib();
					break;
				case Enumerations.Events.Structures.AllyBot_InHib:
					AbstractionLayer._Structures.Ally.Bot_Inhib();
					break;
				case Enumerations.Events.Structures.AllyNexus_Turrets:
					AbstractionLayer._Structures.Ally.Nexus_Tower();
					break;
			}
		}

		private static class AbstractionLayer
		{
			public static class _Kills
			{
				public static void EnemyFirstblood()
				{
				}

				public static void EnemyKill()
				{
				}

				public static void EnemyDouble()
				{
				}

				public static void EnemyTriple()
				{
				}

				public static void EnemyQuadra()
				{
				}

				public static void EnemyPenta()
				{
				}

				public static void AllyFirstblood()
				{
				}

				public static void AllyKill()
				{
				}

				public static void AllyDouble()
				{
				}

				public static void AllyTriple()
				{
				}

				public static void AllyQuadra()
				{
				}

				public static void AllyPenta()
				{
				}
			}

			public static class _Notify
			{
				public static void Defeat()
				{
				}

				public static void Win()
				{
				}

				public static void MinionSpawn()
				{
				}

				public static void Welcome()
				{
				}
			}

			public static class _Structures
			{
				public static class Enemy
				{
					public static void Top_T1()
					{
					}

					public static void Mid_T1()
					{
					}

					public static void Bot_T1()
					{
					}

					public static void Top_T2()
					{
					}

					public static void Mid_T2()
					{
					}

					public static void Bot_T2()
					{
					}

					public static void Top_T3()
					{
					}

					public static void Mid_T3()
					{
					}

					public static void Bot_T3()
					{
					}

					public static void Top_Inhib()
					{
					}

					public static void Mid_Inhib()
					{
					}

					public static void Bot_Inhib()
					{
					}

					public static void Nexus_Tower()
					{
					}
				}

				public static class Ally
				{
					public static void Top_T1()
					{
					}

					public static void Mid_T1()
					{
					}

					public static void Bot_T1()
					{
					}

					public static void Top_T2()
					{
					}

					public static void Mid_T2()
					{
					}

					public static void Bot_T2()
					{
					}

					public static void Top_T3()
					{
					}

					public static void Mid_T3()
					{
					}

					public static void Bot_T3()
					{
					}

					public static void Top_Inhib()
					{
					}

					public static void Mid_Inhib()
					{
					}

					public static void Bot_Inhib()
					{
					}

					public static void Nexus_Tower()
					{
					}
				}
			}
		}
	}
}
