using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace Template
{
    public static class Program
    {

        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

        private static Menu config;
        private static Orbwalking.Orbwalker orbwalker;
        private static Spell q = new Spell(SpellSlot.Q);
        private static Spell w = new Spell(SpellSlot.W);
        private static Spell e = new Spell(SpellSlot.E);
        private static Spell r = new Spell(SpellSlot.R);
        private static Spell ignite = new Spell(Player.GetSpellSlot("summonerdot"), 600, TargetSelector.DamageType.True);

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
            if (Player.ChampionName != "") return;

            LoadMenu();

            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsDead) return;

            switch (orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.LastHit:
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    break;
                case Orbwalking.OrbwalkingMode.Combo:
                    break;
                case Orbwalking.OrbwalkingMode.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void Combo()
        {
        }

        private static void Harass()
        {
        }

        private static void Clear()
        {
        }

        internal static class Casts
        {
            internal static void Q()
            {

            }

            internal static void W()
            {

            }

            internal static void E()
            {

            }

            internal static void R()
            {

            }

            internal static void Ignite()
            {
                int dmg = (Player.Level * 20) + 50;
                foreach (
                    Obj_AI_Hero tar in
                        Player.GetEnemiesInRange(ignite.Range).Where(tar => dmg >= tar.Health && ignite.IsReady())) ignite.Cast(tar);
            }
        }

        private static void LoadMenu()
        {
            config = new Menu("", "main", true);

            Menu orb = config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
            orbwalker = new Orbwalking.Orbwalker(orb);

            Menu targetSelector = config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
            TargetSelector.AddToMenu(targetSelector);

            Menu combo = new Menu("Combo", "main.combo");

            Menu harass = new Menu("Harass", "main.harass");

            Menu clear = new Menu("Clear", "main.clear");

            Menu settings = new Menu("Settings", "main.settings");

            Menu drawings = new Menu("Drawings", "main.drawings");

            config.AddSubMenu(combo);
            config.AddSubMenu(harass);
            config.AddSubMenu(clear);
            config.AddSubMenu(settings);
            config.AddSubMenu(drawings);
            config.AddToMainMenu();
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
        }

        private static void AddBool(this Menu menu, string displayName, string name, bool value)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(value));
        }

        private static void AddSlider(this Menu menu, string displayName, string name, int startVal)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(new Slider(startVal)));
        }

        private static bool GetBool(string name)
        {
            return config.Item(name).GetValue<bool>();
        }

        private static bool ManalimiterCheck(string name)
        {
            return Player.ManaPercent > config.Item(name).GetValue<Slider>().Value;
        }

        private static int GetSliderValue(string name)
        {
            return config.Item(name).GetValue<Slider>().Value;
        }
    }
}
