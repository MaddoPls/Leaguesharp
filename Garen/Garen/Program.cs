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
        // ReSharper disable once InconsistentNaming
        private static bool DAMACIA = true;
        private static readonly Random rand = new Random();

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
            AttackableUnit.OnDamage += OnDamage;
            Orbwalking.AfterAttack += Orbwalking_AfterAttack;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.HasBuff("GarenE")) Orbwalking.MoveTo(Game.CursorPos);

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

        private static void Orbwalking_AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            if (!unit.IsMe) return;
            switch (orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.LastHit:
                case Orbwalking.OrbwalkingMode.LaneClear:
                    if (!target.IsDead && q.IsReady() && GetBool("main.clear.q")) q.CastOnUnit(ObjectManager.GetUnitByNetworkId<Obj_AI_Base>(target.NetworkId));
                    break;
                case Orbwalking.OrbwalkingMode.Combo:
                    if (!target.IsDead && q.IsReady() && GetBool("main.combo.q") && GetBool("main.combo.qaa") && target.Type == GameObjectType.obj_AI_Hero && target.IsEnemy && GetBool("main.combo.e") && GetBool("main.combo.eaa"))
                    {
                        q.Cast();
                        Utility.DelayAction.Add(1337, () => Casts.E());
                        return;
                    }
                    if (!target.IsDead && q.IsReady() && GetBool("main.combo.q") && GetBool("main.combo.qaa")
                        && target.Type == GameObjectType.obj_AI_Hero && target.IsEnemy) q.Cast();
                    if (!target.IsDead && e.IsReady() && GetBool("main.combo.e") && GetBool("main.combo.eaa")
                        && target.Type == GameObjectType.obj_AI_Hero && target.IsEnemy) e.Cast();
                    break;
            }
        }

        private static void OnDamage(AttackableUnit sender, AttackableUnitDamageEventArgs args)
        {
            if (!sender.IsEnemy || !config.Item("main.settings.w").GetValue<bool>() || sender.Type != GameObjectType.obj_AI_Hero) return;
            Obj_AI_Hero tar = ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.TargetNetworkId);
            if (((tar.Health - args.Damage) / tar.Health) > config.Item("main.settings.whp").GetValue<Slider>().Value)
            {
                Casts.W();
            }
        }

        private static void Combo()
        {
            if (GetBool("main.combo.q") && !GetBool("main.combo.qaa")) Casts.Q();

            if (GetBool("main.combo.e") && !GetBool("main.combo.eaa")) Casts.E();

            if (GetBool("main.combo.r")) Casts.R();
        }

        private static void Harass()
        {
            if (GetBool("main.harass.q")) Casts.Q();

            if (GetBool("main.harass.e")) Casts.E();
        }

        private static void Clear()
        {
            //fix
            if (GetBool("main.clear.q")) Casts.QMinion();

            if (GetBool("main.clear.e")) Casts.EMinion();
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

            internal static void Q()
            {
                if (!q.IsReady()) return;

                Obj_AI_Hero target = Player.GetEnemiesInRange(Player.AttackRange + r.Range).FirstOrDefault();
                if (target == null) return;
                q.Cast(target);
            }

            internal static void QMinion()
            {
                if (!q.IsReady()) return;

                Obj_AI_Minion targetMinion = MinionManager.GetMinions(Player.ServerPosition, Player.AttackRange).OrderBy(t => t.Health).FirstOrDefault() as Obj_AI_Minion;
                if (targetMinion == null) return;
                if (targetMinion.Health > q.GetDamage(targetMinion)) return;
                q.CastOnUnit(targetMinion);
            }

            internal static void W()
            {
                if (w.IsReady()) w.Cast();
            }

            internal static void E()
            {
                if (!e.IsReady()) return;
                Obj_AI_Hero target = Player.GetEnemiesInRange(Player.AttackRange).FirstOrDefault();
                if (target == null) return;
                e.Cast();
            }

            internal static void EMinion()
            {
                if (!e.IsReady() || Player.UnderTurret(true) || Player.HasBuff("GarenE")) return;
                int minionCount = MinionManager.GetMinions(Player.ServerPosition, Player.AttackRange).Count;
                int count = config.Item("main.clear.ecount").GetValue<Slider>().Value;
                if (minionCount >= count)
                {
                    e.Cast();
                }
            }

            internal static void R()
            {
                if (!r.IsReady()) return;

                Obj_AI_Hero target = r.GetTarget();
                if (target == null) return;
                if (GetBool("main.combo.s") && target.HasBuffOfType(BuffType.SpellShield)) return;

                if (target.Health < r.GetDamage(target))
                {
                    r.CastOnUnit(target);
                }
                if (GetBool("main.settings.demacia"))
                {
                    int randDelayT = rand.Next(5000, 15000);
                    Utility.DelayAction.Add(GetBool("main.settings.demaciar") ? randDelayT : 5000, () => CheckDeath(target));
                }
            }

            private static void CheckDeath(Obj_AI_Hero target)
            {
                if (target.IsDead && DAMACIA)
                {
                    DAMACIA = false;
                    Game.Say(@"/all DEMACIA!!");
                    Utility.DelayAction.Add(60000, () => DAMACIA = true);
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!GetBool("main.drawings.enabled")) return;
            if (r.Level > 0)
            {
                Render.Circle.DrawCircle(Player.Position, r.Range, r.IsReady() ? config.Item("main.drawings.r").GetValue<Color>() : Color.Red);
            }
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
            combo.AddBool("Use E", "main.combo.e", true);
            combo.AddBool("Use R", "main.combo.r", true);
            combo.AddBool("Spellshield check", "main.combo.s", true);
            combo.AddBool("Q after AA", "main.combo.qaa", true);
            combo.AddBool("E after AA", "main.combo.eaa", false);

            Menu harass = new Menu("Harass", "main.harass");
            harass.AddBool("Use Q", "main.harass.q", true);
            harass.AddBool("Use E", "main.harass.e", true);

            Menu clear = new Menu("Clear", "main.clear");
            clear.AddBool("Use E", "main.clear.e", true);
            clear.AddItem(new MenuItem("main.clear.ecount", "When minions hit").SetValue(new Slider(3, 3, 6)));
            clear.AddBool("Use Q", "main.clear.q", true);

            Menu settings = new Menu("Settings", "main.settings");
            settings.AddBool("Auto ignite", "main.settings.ignite", true);
            settings.AddBool("KS with R", "main.settings.ks", true);
            settings.AddBool("Shout DEMACIA (only on R)", "main.settings.demacia", false);
            settings.AddBool("Demacia shout time randomization", "main.settings.demaciar", true);
            settings.AddItem(new MenuItem("main.settings.flee", "Flee/Chase").SetValue(new KeyBind(0x41, KeyBindType.Press)));
            settings.AddBool("Use W on incoming dmg", "main.settings.w", true);
            settings.AddItem(new MenuItem("main.settings.whp", "Active on %hp will be dealt").SetValue(new Slider(2)));
            settings.AddItem(new MenuItem("unfo2", "OnDamage broken atm"));

            Menu drawings = new Menu("Drawings", "main.drawings");
            drawings.AddItem(new MenuItem("main.drawings.r", "Draw R")).SetValue(Color.BlueViolet);
            drawings.AddBool("Enable drawings", "main.drawings.enabled", true);
            //drawings.AddBool("Draw R", "main.drawings.r", true);

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
