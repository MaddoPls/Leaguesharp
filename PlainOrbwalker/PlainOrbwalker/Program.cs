using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace PlainOrbwalker
{
    public static class Program
    {
        private static Menu config;
        private static Orbwalking.Orbwalker orbwalker;

        static void Main(string[] args)
        {
            if (Game.Mode == GameMode.Running)
            {
                Game_OnGameStart(new EventArgs());
            }

            Game.OnStart += Game_OnGameStart;
        }

        private static void Game_OnGameStart(EventArgs args)
        {
            config = new Menu("Plain orbwalker", "main", true);
            Menu orb = config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
            orbwalker = new Orbwalking.Orbwalker(orb);
            Menu targetSelector = config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
            TargetSelector.AddToMenu(targetSelector);
            config.AddToMainMenu();
        }
    }
}
