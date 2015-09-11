namespace Anouncersharp
{
	public static class Enumerations
	{
		public static class Events
		{
			public enum Kills
			{
				EnemyFirstblood = 0,
				EnemyKill,
				EnemyDouble,
				EnemyTriple,
				EnemyQuadra,
				EnemyPenta,
				AllyFirstblood,
				AllyKill,
				AllyDouble,
				AllyTriple,
				AllyQuadra,
				AllyPenta
			}

			public enum Notify
			{
				Defeat = 0,
				Win,
				MinionSpawn,
				Welcome
			}

			public enum Structures
			{
				EnemyTop_T1 = 0,
				EnemyMid_T1,
				EnemyBot_T1,
				EnemyTop_T2,
				EnemyMid_T2,
				EnemyBot_T2,
				EnemyTop_T3,
				EnemyMid_T3,
				EnemyBot_T3,
				EnemyTop_InHib,
				EnemyMid_InHib,
				EnemyBot_InHib,
				EnemyNexus_Turrets,
				AllyTop_T1,
				AllyMid_T1,
				AllyBot_T1,
				AllyTop_T2,
				AllyMid_T2,
				AllyBot_T2,
				AllyTop_T3,
				AllyMid_T3,
				AllyBot_T3,
				AllyTop_InHib,
				AllyMid_InHib,
				AllyBot_InHib,
				AllyNexus_Turrets
			}
		}

		public enum SoundPacks
		{
			Portal2 = 0,
			StanlyParable
		}
	}
}
