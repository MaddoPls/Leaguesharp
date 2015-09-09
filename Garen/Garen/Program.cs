namespace Garen
{
    using System;
    using System.Drawing;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.Common;

    public static class Program
    {
        //stop e when nobody is in decent range, cast q before e

        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

        private static Menu config;
        private static Orbwalking.Orbwalker orbwalker;
        private static Spell q = new Spell(SpellSlot.Q);
        private static Spell w = new Spell(SpellSlot.W);
        private static Spell e = new Spell(SpellSlot.E);
        private static Spell r = new Spell(SpellSlot.R, 400);
        private static Spell ignite = new Spell(Player.GetSpellSlot("summonerdot"), 600, TargetSelector.DamageType.True);
        private static bool DAMACIA = true;
        private static bool eallowed = true;

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
            if (Player.ChampionName != "Garen") return;

            LoadMenu();

            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }



        private static void Game_OnUpdate(EventArgs args)
        {
            if (GetBool("main.settings.ignite")) Casts.Ignite();
            if (GetBool("main.settings.ks")) Casts.R();

            switch (orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case Orbwalking.OrbwalkingMode.LastHit:
                case Orbwalking.OrbwalkingMode.LaneClear:
                    Clear();
                    break;
            }

            if (config.Item("main.settings.flee").GetValue<KeyBind>().Active)
            {
                Orbwalking.MoveTo(Game.CursorPos);
                if (q.IsReady()) q.Cast();
            }
        }

        private static void Combo()
        {
            if (GetBool("main.combo.q") && GetBool("main.combo.e"))
            {
                Casts.Q(GetBool("main.combo.w"));
                Utility.DelayAction.Add(20, () => Casts.E(false));
            }

            if (GetBool("main.combo.q")) Casts.Q(GetBool("main.combo.w"));

            if (GetBool("main.combo.e")) Casts.E(GetBool("main.combo.w"));

            if (GetBool("main.combo.r")) Casts.R();
        }

        private static void Harass()
        {
            if (GetBool("main.harass.q")) Casts.Q(GetBool("main.harass.w"));

            if (GetBool("main.harass.e")) Casts.E(GetBool("main.harass.w"));
        }

        private static void Clear()
        {
            if (GetBool("main.clear.e"))
            {
                if (!eallowed || !e.IsReady() || Player.UnderTurret(true)) return;
                int minionCount = MinionManager.GetMinions(Player.ServerPosition, Player.AttackRange).Count;
                int count = config.Item("main.clear.ecount").GetValue<Slider>().Value;
                if (minionCount >= count)
                {
                    e.Cast();
                    eallowed = false;
                    Utility.DelayAction.Add(7000, () => eallowed = true);
                    //player will stop moving if e will kill the minions, TODO: find bypass
                }
            }
        }

        internal static class Casts
        {
            internal static void Ignite()
            {
                int dmg = (Player.Level * 20) + 50;
                foreach (Obj_AI_Hero tar in
                    Player.GetEnemiesInRange(ignite.Range).Where(tar => dmg >= tar.Health && ignite.IsReady()))
                {
                    ignite.Cast(tar);
                }
            }

            internal static void Q(bool castW)
            {
                if (!q.IsReady()) return;

                Obj_AI_Hero target = Player.GetEnemiesInRange(Player.AttackRange + 200).FirstOrDefault();
                if (target == null) return;
                q.Cast(target);
                if (castW) W();
            }

            internal static void W()
            {
                if (w.IsReady()) w.Cast();
            }

            internal static void E(bool castW)
            {
                if (!e.IsReady() || !eallowed) return;
                Obj_AI_Hero target = Player.GetEnemiesInRange(Player.AttackRange).FirstOrDefault();
                if (target == null) return;
                e.Cast();
                if (castW) W();
            }

            internal static void R()
            {
                if (!r.IsReady()) return;

                Obj_AI_Hero target = r.GetTarget();
                if (target == null) return;

                if (target.Health < r.GetDamage(target)) r.CastOnUnit(target);
                if (GetBool("main.settings.damacia"))
                {
                    Utility.DelayAction.Add(3000, () => CheckDeath(target));
                }
            }

            private static void CheckDeath(Obj_AI_Hero target)
            {
                if (target.IsDead && DAMACIA)
                {
                    DAMACIA = false;
                    Game.Say(@"/all DEMACIA!!");
                    Utility.DelayAction.Add(10000, () => DAMACIA = true);
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (GetBool("main.drawings.r") && r.Level > 0) Render.Circle.DrawCircle(Player.Position, r.Range, r.IsReady() ? Color.BlueViolet : Color.Red);
        }

        private static void LoadMenu()
        {
            config = new Menu("Garen", "main", true);

            Menu orb = config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
            orbwalker = new Orbwalking.Orbwalker(orb);

            Menu targetSelector = config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
            TargetSelector.AddToMenu(targetSelector);

            Menu combo = new Menu("Combo", "main.combo");
            combo.AddBool("Use Q", "main.combo.q", true);
            combo.AddBool("Use W", "main.combo.w", true);
            combo.AddBool("Use E", "main.combo.e", true);
            combo.AddBool("Use R", "main.combo.r", true);

            Menu harass = new Menu("Harass", "main.harass");
            harass.AddBool("Use Q", "main.harass.q", true);
            harass.AddBool("Use W", "main.harass.w", true);
            harass.AddBool("Use E", "main.harass.e", true);

            Menu clear = new Menu("Clear", "main.clear");
            clear.AddBool("Use E", "main.clear.e", true);
            clear.AddItem(new MenuItem("main.clear.ecount", "When minions hit").SetValue(new Slider(3, 3, 6)));

            Menu settings = new Menu("Settings", "main.settings");
            settings.AddBool("Auto ignite", "main.settings.ignite", true);
            settings.AddBool("KS with R", "main.settings.ks", true);
            settings.AddBool("Shout DEMACIA (only on R)", "main.settings.damacia", false);
            settings.AddItem(
                new MenuItem("main.settings.flee", "Flee/Chase").SetValue(new KeyBind(0x41, KeyBindType.Press)));

            Menu drawings = new Menu("Drawings", "main.drawings");
            drawings.AddBool("Draw R", "main.drawings.r", true);

            config.AddSubMenu(combo);
            config.AddSubMenu(harass);
            config.AddSubMenu(clear);
            config.AddSubMenu(settings);
            config.AddSubMenu(drawings);
            config.AddToMainMenu();
        }

        private static void AddBool(this Menu menu, string displayName, string name, bool value)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(value));
        }

        private static bool GetBool(string name)
        {
            return config.Item(name).GetValue<bool>();
        }
    }
}
