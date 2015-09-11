//using System.Media;
//// ReSharper disable InconsistentNaming

//namespace Anouncersharp
//{
//    /*
//        Will be used instead of always disposing of the soundsplayers
//    */
//	public static class Soundplayers
//	{
//		public static class Portal2
//		{
//			public static SoundPlayer EnemyFirstblood = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyKill = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyDouble = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyTriple = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyQuadra = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyPenta = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyFirstblood = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyKill = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyDouble = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyTriple = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyQuadra = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyPenta = new SoundPlayer(Resource.glados_notify_first_blood);
//			//====
//			public static SoundPlayer Defeat = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer Win = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer MinionSpawn = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer Welcome = new SoundPlayer(Resource.glados_notify_first_blood);

//			public static class Structures
//			{
//				public static class Ally
//				{
//					public static SoundPlayer Top_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Nexus_Tower = new SoundPlayer(Resource.glados_notify_first_blood);
//				}

//				public static class Enemy
//				{
//					public static SoundPlayer Top_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Nexus_Tower = new SoundPlayer(Resource.glados_notify_first_blood);
//				}
//			}
//		}

//		public static class StanleyParable
//		{
//			public static SoundPlayer EnemyFirstblood = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyKill = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyDouble = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyTriple = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyQuadra = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer EnemyPenta = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyFirstblood = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyKill = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyDouble = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyTriple = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyQuadra = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer AllyPenta = new SoundPlayer(Resource.glados_notify_first_blood);
//			//====
//			public static SoundPlayer Defeat = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer Win = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer MinionSpawn = new SoundPlayer(Resource.glados_notify_first_blood);
//			public static SoundPlayer Welcome = new SoundPlayer(Resource.glados_notify_first_blood);

//			public static class Structures
//			{
//				public static class Ally
//				{
//					public static SoundPlayer Top_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Nexus_Tower = new SoundPlayer(Resource.glados_notify_first_blood);
//				}

//				public static class Enemy
//				{
//					public static SoundPlayer Top_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T1 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T2 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_T3 = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Top_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Mid_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Bot_Inhib = new SoundPlayer(Resource.glados_notify_first_blood);
//					public static SoundPlayer Nexus_Tower = new SoundPlayer(Resource.glados_notify_first_blood);
//				}
//			}
//		}
//	}
//}
