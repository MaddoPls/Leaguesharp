using Anouncersharp.Packs;

namespace Anouncersharp
{
	public class PlayAbstractingLayer
	{
		public static void Notify(Enumerations.Events.Notify enumCase)
		{
			switch (Manage.CurrentAnnouncer)
			{
				case Enumerations.SoundPacks.Portal2:
					Portal2.Notify(enumCase);
					break;
				case Enumerations.SoundPacks.StanlyParable:
					StanleyParable.Notify(enumCase);
					break;
			}
		}

		public static void Kill(Enumerations.Events.Kills enumCase)
		{
			switch (Manage.CurrentAnnouncer)
			{
				case Enumerations.SoundPacks.Portal2:
					Portal2.Kills(enumCase);
					break;
				case Enumerations.SoundPacks.StanlyParable:
					StanleyParable.Kills(enumCase);
					break;
			}
		}

		public static void Structure(Enumerations.Events.Structures enumCase)
		{
			switch (Manage.CurrentAnnouncer)
			{
				case Enumerations.SoundPacks.Portal2:
					Portal2.Structures(enumCase);
					break;
				case Enumerations.SoundPacks.StanlyParable:
					StanleyParable.Structures(enumCase);
					break;
			}
		}
	}
}
