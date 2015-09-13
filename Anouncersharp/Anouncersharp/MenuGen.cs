using LeagueSharp.Common;

namespace Anouncersharp.Misc
{
	public static class MenuGen
	{
		public static void Load()
		{

			Manage.Menu = new Menu("Announcer#", "announcer", true);

			Manage.Menu.AddItem(new MenuItem("announcer.enabled", "Enable Announcer#").SetValue(true));
			Manage.Menu.AddItem(new MenuItem("announcer.pack", "Announcer# Pack").SetValue(new StringList(new[] { "Portal 2", "Stanley Parable" })));

			Menu announcerOptions = Manage.Menu.AddSubMenu(new Menu("Announcer# - Options", "announcer.options"));
			{
				announcerOptions.AddItem(new MenuItem("announcer.welcome", "Enable Welcome Sound").SetValue(true));
				announcerOptions.AddItem(new MenuItem("announcer.minions", "Enable Minion Spawn Sound").SetValue(true));
				announcerOptions.AddItem(new MenuItem("announcer.structures", "Enable Towers / Inhibitor Sound").SetValue(true));
				announcerOptions.AddItem(new MenuItem("announcer.win.lose", "Enable Win / Lose Sound").SetValue(true));
				announcerOptions.AddItem(new MenuItem("announcer.kill", "Enable Kill Sound").SetValue(true));
			}

			Manage.Menu.AddItem(new MenuItem("seperator", ""));
			Manage.Menu.AddItem(new MenuItem("by.blacky", "by blacky & HyunMi"));

			Manage.Menu.AddToMainMenu();
		}
	}
}
