using System;
using System.Media;
using Events = Anouncersharp.Enumerations.Events;

// ReSharper disable InconsistentNaming

namespace Anouncersharp.Packs
{
	public static class Portal2
	{
		public static void Kills(Events.Kills enumCase)
		{
			switch (enumCase)
			{
				case Events.Kills.EnemyFirstblood:
					AbstractionLayer._Kills.EnemyFirstblood();
					break;
				case Events.Kills.EnemyKill:
					AbstractionLayer._Kills.EnemyKill();
					break;
				case Events.Kills.EnemyDouble:
					AbstractionLayer._Kills.EnemyDouble();
					break;
				case Events.Kills.EnemyTriple:
					AbstractionLayer._Kills.EnemyTriple();
					break;
				case Events.Kills.EnemyQuadra:
					AbstractionLayer._Kills.EnemyQuadra();
					break;
				case Events.Kills.EnemyPenta:
					AbstractionLayer._Kills.EnemyPenta();
					break;
				case Events.Kills.AllyFirstblood:
					AbstractionLayer._Kills.AllyFirstblood();
					break;
				case Events.Kills.AllyKill:
					AbstractionLayer._Kills.AllyKill();
					break;
				case Events.Kills.AllyDouble:
					AbstractionLayer._Kills.AllyDouble();
					break;
				case Events.Kills.AllyTriple:
					AbstractionLayer._Kills.AllyTriple();
					break;
				case Events.Kills.AllyQuadra:
					AbstractionLayer._Kills.AllyQuadra();
					break;
				case Events.Kills.AllyPenta:
					AbstractionLayer._Kills.AllyPenta();
					break;
			}
		}

		public static void Notify(Events.Notify enumCase)
		{
			switch (enumCase)
			{
				case Events.Notify.Defeat:
					AbstractionLayer._Notify.Defeat();
					break;
				case Events.Notify.Win:
					AbstractionLayer._Notify.Win();
					break;
				case Events.Notify.MinionSpawn:
					AbstractionLayer._Notify.MinionSpawn();
					break;
				case Events.Notify.Welcome:
					AbstractionLayer._Notify.Welcome();
					break;
			}
		}

		public static void Structures(Events.Structures enumCase)
		{

			switch (enumCase)
			{
				case Events.Structures.EnemyTop_T1:
					AbstractionLayer._Structures.Enemy.Top_T1();
					break;
				case Events.Structures.EnemyMid_T1:
					AbstractionLayer._Structures.Enemy.Mid_T1();
					break;
				case Events.Structures.EnemyBot_T1:
					AbstractionLayer._Structures.Enemy.Bot_T1();
					break;
				case Events.Structures.EnemyTop_T2:
					AbstractionLayer._Structures.Enemy.Top_T2();
					break;
				case Events.Structures.EnemyMid_T2:
					AbstractionLayer._Structures.Enemy.Mid_T2();
					break;
				case Events.Structures.EnemyBot_T2:
					AbstractionLayer._Structures.Enemy.Bot_T2();
					break;
				case Events.Structures.EnemyTop_T3:
					AbstractionLayer._Structures.Enemy.Top_T3();
					break;
				case Events.Structures.EnemyMid_T3:
					AbstractionLayer._Structures.Enemy.Mid_T3();
					break;
				case Events.Structures.EnemyBot_T3:
					AbstractionLayer._Structures.Enemy.Bot_T3();
					break;
				case Events.Structures.EnemyTop_InHib:
					AbstractionLayer._Structures.Enemy.Top_Inhib();
					break;
				case Events.Structures.EnemyMid_InHib:
					AbstractionLayer._Structures.Enemy.Mid_Inhib();
					break;
				case Events.Structures.EnemyBot_InHib:
					AbstractionLayer._Structures.Enemy.Bot_Inhib();
					break;
				case Events.Structures.EnemyNexus_Turrets:
					AbstractionLayer._Structures.Enemy.Nexus_Tower();
					break;
				case Events.Structures.AllyTop_T1:
					AbstractionLayer._Structures.Ally.Top_T1();
					break;
				case Events.Structures.AllyMid_T1:
					AbstractionLayer._Structures.Ally.Mid_T1();
					break;
				case Events.Structures.AllyBot_T1:
					AbstractionLayer._Structures.Ally.Bot_T1();
					break;
				case Events.Structures.AllyTop_T2:
					AbstractionLayer._Structures.Ally.Top_T2();
					break;
				case Events.Structures.AllyMid_T2:
					AbstractionLayer._Structures.Ally.Mid_T2();
					break;
				case Events.Structures.AllyBot_T2:
					AbstractionLayer._Structures.Ally.Bot_T2();
					break;
				case Events.Structures.AllyTop_T3:
					AbstractionLayer._Structures.Ally.Top_T3();
					break;
				case Events.Structures.AllyMid_T3:
					AbstractionLayer._Structures.Ally.Mid_T3();
					break;
				case Events.Structures.AllyBot_T3:
					AbstractionLayer._Structures.Ally.Bot_T3();
					break;
				case Events.Structures.AllyTop_InHib:
					AbstractionLayer._Structures.Ally.Top_Inhib();
					break;
				case Events.Structures.AllyMid_InHib:
					AbstractionLayer._Structures.Ally.Mid_Inhib();
					break;
				case Events.Structures.AllyBot_InHib:
					AbstractionLayer._Structures.Ally.Bot_Inhib();
					break;
				case Events.Structures.AllyNexus_Turrets:
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
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_first_blood);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_first_blood);
					}
					catch (Exception)
					{
						// ignored
					}
				}

				public static void EnemyKill()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_death_1);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_death_1);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void EnemyDouble()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_double);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_double);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void EnemyTriple()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_triple);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_triple);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void EnemyQuadra()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_quadra);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_quadra);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void EnemyPenta()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_penta);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_penta);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void AllyFirstblood()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_first_blood);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_first_blood);
					}
					catch (Exception)
					{
						// ignored
					}
				}

				public static void AllyKill()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_enemy_death_1);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_enemy_death_1);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void AllyDouble()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_double);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_double);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void AllyTriple()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_triple);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_triple);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void AllyQuadra()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_quadra);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_quadra);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void AllyPenta()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_kills_penta);
						player.Play();
						player.DisposePlayer(Resource.glados_kills_penta);
					}
					catch (Exception)
					{
						// ignored
					}

				}
			}

			public static class _Notify
			{
				public static void Defeat()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_defeat);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_defeat);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void Win()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_victory);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_victory);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void MinionSpawn()
				{
					try
					{
						SoundPlayer player = new SoundPlayer(Resource.glados_notify_minion_spawn);
						player.Play();
						player.DisposePlayer(Resource.glados_notify_minion_spawn);
					}
					catch (Exception)
					{
						// ignored
					}

				}

				public static void Welcome()
				{
					try
					{
						int index = Manage.Rand.Next(1, Manage.Sounds.Portal2.Notify.Welcome);
						switch (index)
						{
							case 1:
								SoundPlayer player1 = new SoundPlayer(Resource.glados_welcome_1);
								player1.Play();
								player1.DisposePlayer(Resource.glados_welcome_1);
								break;
							case 2:
								SoundPlayer player2 = new SoundPlayer(Resource.glados_welcome_2);
								player2.Play();
								player2.DisposePlayer(Resource.glados_welcome_2);
								break;
							case 3:
								SoundPlayer player3 = new SoundPlayer(Resource.glados_welcome_3);
								player3.Play();
								player3.DisposePlayer(Resource.glados_welcome_3);
								break;
							case 4:
								SoundPlayer player4 = new SoundPlayer(Resource.glados_welcome_4);
								player4.Play();
								player4.DisposePlayer(Resource.glados_welcome_4);
								break;
							case 5:
								SoundPlayer player5 = new SoundPlayer(Resource.glados_welcome_5);
								player5.Play();
								player5.DisposePlayer(Resource.glados_welcome_5);
								break;
							case 6:
								SoundPlayer player6 = new SoundPlayer(Resource.glados_welcome_6);
								player6.Play();
								player6.DisposePlayer(Resource.glados_welcome_6);
								break;
							case 7:
								SoundPlayer player7 = new SoundPlayer(Resource.glados_welcome_7);
								player7.Play();
								player7.DisposePlayer(Resource.glados_welcome_7);
								break;
						}
					}
					catch (Exception)
					{
						// ignored
					}

				}
			}

			public static class _Structures
			{
				public static class Enemy
				{
					public static void Top_T1()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_T1()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_T1()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Top_T2()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_T2()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_T2()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Top_T3()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_T3()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_T3()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_tower_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_tower_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Top_Inhib()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_inhib_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_inhib_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_Inhib()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_inhib_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_inhib_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_Inhib()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_inhib_enemy);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_inhib_enemy);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Nexus_Tower()
					{
						try
						{

						}
						catch (Exception)
						{
							// ignored
						}
					}
				}

				public static class Ally
				{
					public static void Top_T1()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_T1()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_T1()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Top_T2()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_T2()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_T2()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Top_T3()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_T3()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Bot_T3()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_tower);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_tower);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Top_Inhib()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_top_inhib);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_top_inhib);
						}
						catch (Exception)
						{
							// ignored
						}

					}

					public static void Mid_Inhib()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_mid_inhib);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_mid_inhib);
						}
						catch (Exception)
						{
							// ignored
						}
					}

					public static void Bot_Inhib()
					{
						try
						{
							SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_inhib);
							player.Play();
							player.DisposePlayer(Resource.glados_structures_bot_inhib);
						}
						catch (Exception)
						{
							// ignored
						}
					}

					public static void Nexus_Tower()
					{
						try
						{

						}
						catch (Exception)
						{
							// ignored
						}
					}
				}
			}
		}
	}
}