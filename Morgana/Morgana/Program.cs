using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace Morgana
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
            if (Player.ChampionName != "Morgana") return;

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
                case Orbwalking.OrbwalkingMode.LaneClear:
                    Clear();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
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
                        Player.GetEnemiesInRange(ignite.Range).Where(tar => dmg >= tar.Health && ignite.IsReady()))
                    ignite.Cast(tar);
            }
        }

        private static void LoadMenu()
        {
            config = new Menu("Morgana", "main", true);

            Menu orb = config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
            orbwalker = new Orbwalking.Orbwalker(orb);

            Menu targetSelector = config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
            TargetSelector.AddToMenu(targetSelector);

            Menu combo = new Menu("Combo", "main.combo");
            combo.AddBool("Use Q", "main.combo.q", true);
            combo.AddBool("Use W", "main.combo.w", true);
            combo.AddBool("Use R", "main.combo.r", true);


            Menu harass = new Menu("Harass", "main.harass");
            harass.AddBool("Use Q", "main.harass.q", true);
            harass.AddBool("Use W", "main.harass.w", true);

            Menu clear = new Menu("Clear", "main.clear");
            clear.AddBool("Use W", "main.clear.w", true);
            clear.AddBool("Must kill minions", "main.clear.wk", true);
            clear.AddSlider("Min minion kill/hit", "main.clear.wmk", 3, 1, 6);

            Menu settings = new Menu("Settings", "main.settings");
            Menu events = new Menu("Events", "main.settings.events");
            events.AddBool("Antigapcloser", "main.settings.events.gapcloser", true);
            events.AddBool("Interrupter", "main.settings.events.interrupter", true);
            events.AddBool("OnDash", "main.settings.events.ondash", true);
            settings.AddSubMenu(events);

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

        private static void AddManalimiter(this Menu menu, string displayName, string name, int startVal)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(new Slider(startVal)));
        }

        private static void AddSlider(this Menu menu, string displayName, string name, int startVal, int minVal, int maxVal)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(new Slider(startVal, minVal, maxVal)));
        }

        internal static bool GetBool(string name)
        {
            return config.Item(name).GetValue<bool>();
        }

        internal static bool ManalimiterCheck(string name)
        {
            return Player.ManaPercent > config.Item(name).GetValue<Slider>().Value;
        }

        internal static int GetSliderValue(string name)
        {
            return config.Item(name).GetValue<Slider>().Value;
        }

        internal enum PredictionSet
        {
            Common,
            SPrediction
        }

        internal static class Settings
        {
            internal static PredictionSet Pred
            {
                get
                {
                    switch (config.Item("").GetValue<StringList>().SelectedIndex)
                    {
                        case 1:
                            return PredictionSet.Common;
                        case 2:
                            return PredictionSet.SPrediction;
                        default:
                            return PredictionSet.Common;
                    }
                }
            }

            internal static HitChance MinHitChanceQ
            {
                get
                {
                    switch (config.Item("").GetValue<StringList>().SelectedIndex)
                    {
                        case 0:
                            return HitChance.Medium;
                        case 1:
                            return HitChance.High;
                        case 2:
                            return HitChance.VeryHigh;
                        default:
                            return HitChance.Immobile;
                    }
                }
            }

            internal static bool ComboQ{ get { return GetBool("") && q.IsReady(); } }
            internal static bool ComboW { get { return GetBool("") && w.IsReady(); } }
            internal static bool ComboR { get { return GetBool("") && r.IsReady(); } }

            internal static bool HarassQ { get { return GetBool("") && q.IsReady(); } }
            internal static bool HarassW { get { return GetBool("") && w.IsReady(); } }

            internal static bool ClearW { get { return GetBool("") && w.IsReady(); } }

            internal static bool DrawQ { get { return GetBool("") && q.Level > 0; } }
        }
    }
}
