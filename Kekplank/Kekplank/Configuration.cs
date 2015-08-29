using LeagueSharp.Common;

namespace Kekplank
{
	public static class Configuration
	{
		private static Menu _config;

		public static Menu get_menu()
		{
			_config = new Menu("Kekplank", "Kekplank", true);

			Menu orbMenu = _config.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
			Program.Orbwalker = new Orbwalking.Orbwalker(orbMenu);

			Menu targetSelectorMenu = _config.AddSubMenu(new Menu("TargetSelector", "Target Selector"));
			TargetSelector.AddToMenu(targetSelectorMenu);

			Menu combo = new Menu("Combo", "Combo");
			combo.add_bool("Use Q");
			combo.add_bool("Use E");
			combo.add_bool("Bust E with Q");

			Menu harass = new Menu("Harass", "Harass");
			harass.add_bool("Use Q");
			harass.add_slider("Manalimiter Q", 55);

			Menu clear = new Menu("Clear", "Clear");
			clear.add_bool("Use Q Minions");
			clear.add_bool("Use Q Harass");
			clear.add_bool("Prioritize Minions");
			clear.add_slider("Mana limiter Q", 35);

			Menu settings = new Menu("Settings", "Settings");
			settings.add_bool("Auto W");
			settings.add_bool("Auto W on slow");
			settings.add_slider("Auto W manalimiter", 0);
			settings.add_bool("Auto R");
			settings.add_bool("Auto Ignite");
			settings.AddItem(new MenuItem("DisableE", "Disable E from casting")).SetValue(new KeyBind(0x41, KeyBindType.Press));

			Menu drawings = new Menu("Drawings", "Drawings");
			drawings.add_bool("Draw Q");
			drawings.add_bool("Draw E");
			drawings.add_bool("Draw focused barrel");


			_config.AddSubMenu(combo);
			_config.AddSubMenu(harass);
			_config.AddSubMenu(clear);
			_config.AddSubMenu(settings);
			_config.AddSubMenu(drawings);

			return _config;
		}

		private static void add_bool(this Menu menu, string dispName)
		{
			menu.AddItem(new MenuItem(dispName.Replace(" ", ""), dispName).SetValue(true));
		}

		public static bool get_bool(this Menu menu, string dispName, string subMenuName)
		{
			return menu.SubMenu(subMenuName).Item(dispName.Replace(" ", "")).GetValue<bool>();
		}

		private static void add_slider(this Menu menu, string dispName, int startValue)
		{
			menu.AddItem(new MenuItem(dispName.Replace(" ", ""), dispName).SetValue(new Slider(startValue)));
		}

		public static bool ManaLimitCheck(this Menu menu, string dispName, string subMenuName)
		{
			return Program.Player.ManaPercent >= menu.SubMenu(subMenuName).Item(dispName.Replace(" ", "")).GetValue<Slider>().Value;
		}
	}
}
