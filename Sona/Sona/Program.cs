using System;
using LeagueSharp;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp.Common;
using SharpDX;
using SPrediction;
using Color = System.Drawing.Color;

namespace Sona
{
    //fix r only getting to number 2, improve e

    public static class Program
    {
        //Thnx asuna for being my helpdesk
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

        private static Menu config;
        private static Orbwalking.Orbwalker orbwalker;
        private static Spell q = new Spell(SpellSlot.Q, 850, TargetSelector.DamageType.Magical);
        private static Spell w = new Spell(SpellSlot.W, 1000, TargetSelector.DamageType.Magical);
        private static Spell e = new Spell(SpellSlot.E, 350, TargetSelector.DamageType.Magical);
        private static Spell r = new Spell(SpellSlot.R, 1000, TargetSelector.DamageType.Magical);
        private static Spell ignite = new Spell(Player.GetSpellSlot("summonerdot"), 600, TargetSelector.DamageType.True);
        private static string prediction;

        static void Main(string[] args)
        {
            if (Game.Mode == GameMode.Running)
            {
                Game_OnGameStart(new EventArgs());
            }

            Game.OnStart += Game_OnGameStart;
        }

        private static void Game_OnGameStart(EventArgs eventArgs)
        {
            if (Player.ChampionName != "Sona") return;
            Game.PrintChat("Disable AA on minions broken atm will be fixed soon™");
            r.SetSkillshot(250, 140, 2400, false, SkillshotType.SkillshotLine);
            LoadMenu();
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            AntiGapcloser.OnEnemyGapcloser += AntiGapcloser_OnEnemyGapcloser;
            Obj_AI_Hero.OnIssueOrder += Obj_AI_Hero_OnIssueOrder;
            Interrupter2.OnInterruptableTarget += Interrupter2_OnInterruptableTarget;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsDead) return;
            prediction = config.Item("main.settings.pred").GetValue<StringList>().SelectedValue;
            if (GetBool("main.settings.w.enabled") && !Player.IsRecalling()) Casts.W();
            if (GetBool("main.settings.ignite")) Casts.Ignite();

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
            if (GetBool("main.combo.q")) Casts.Q();

            if (GetBool("main.combo.e")) Casts.E();

            if (GetBool("main.combo.r")) Casts.R();
        }

        private static void Harass()
        {
            if (GetBool("main.harass.q") && ManalimiterCheck("main.harass.qm")) Casts.Q();
        }

        private static void Clear()
        {
            if (GetBool("main.clear.q") && ManalimiterCheck("main.clear.qm")) Casts.QMinion();
        }


        internal static class Casts
        {
            internal static void Q()
            {
                if (!q.IsReady()) return;
                Obj_AI_Hero tar = TargetSelector.GetTarget(Player, q.Range, q.DamageType, false);
                if (tar != null)
                {
                    if (GetBool("main.harass.q2"))
                    {
                        q.CastIfWillHit(tar, 2);
                    }
                    else
                    {
                        q.Cast();
                    }
                }
            }

            internal static void QMinion()
            {
                if (!q.IsReady()) return;
                Obj_AI_Base minion = MinionManager.GetMinions(Player.ServerPosition, q.Range).FirstOrDefault();
                if (minion != null && minion.Health < q.GetDamage(minion)) q.Cast();
            }

            internal static void W()
            {
                if (!w.IsReady()) return;
                if (
                    Player.GetAlliesInRange(w.Range)
                        .Where(ally => ally.HealthPercent < GetSliderValue("main.settings.w.hp"))
                        .ToList()
                        .Count != 0) w.Cast();
            }

            internal static void E()
            {
                //TODO: think of something decent
                if (!e.IsReady()) return;
                if (Player.GetAlliesInRange(e.Range).Where(ally => ally.HealthPercent < 8).ToList().Count != 0)
                {
                    e.Cast();
                }
            }

            internal static void R()
            {
                if (!r.IsReady()) return;
                Obj_AI_Hero target = TargetSelector.GetTarget(Player, r.Range, r.DamageType);
                if (target == null) return;
                switch (prediction)
                {
                    case "Common":
                        int minHit = config.Item("main.combo.rminhit").GetValue<Slider>().Value;
                        switch (minHit)
                        {
                            case 1:
                                r.CastIfHitchanceEquals(target, HitChance.High);
                                break;
                            default:
                                r.CastIfWillHit(target, minHit);
                                break;
                        }
                        break;
                    case "SPrediction":
                        HitChance hitChance;
                        Vector2 spred = SPrediction.Prediction.GetPrediction(
                            target,
                            r,
                            target.GetWaypoints(),
                            target.AvgMovChangeTime(),
                            target.LastMovChangeTime(),
                            target.AvgPathLenght(), out hitChance,
                            Player.ServerPosition);
                        r.Cast(spred);
                        break;
                }
            }

            internal static void Ignite()
            {
                int dmg = (Player.Level * 20) + 50;
                foreach (
                    Obj_AI_Hero tar in
                        Player.GetEnemiesInRange(ignite.Range).Where(tar => dmg >= tar.Health && ignite.IsReady())) ignite.Cast(tar);
            }
        }

        private static void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (!GetBool("main.settings.gapcloser.enabled")) return;

            if (GetBool("main.settings.gapcloser.allies"))
            {
                IEnumerable<Obj_AI_Hero> allies = Player.GetAlliesInRange(200).Where(ally => ally.HealthPercent <= GetSliderValue("main.settings.gapcloser.hp"));
                if (allies.ToList().Count != 0) r.Cast(gapcloser.End);
            }
            else
            {
                if (Player.HealthPercent < GetSliderValue("main.settings.gapcloser.hp")) r.Cast(gapcloser.End);
            }
        }

        private static void Interrupter2_OnInterruptableTarget(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs args)
        {
            if (!GetBool("main.settings.interrupter") || sender.IsAlly) return;
            r.Cast(sender.ServerPosition);
        }

        private static void Obj_AI_Hero_OnIssueOrder(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args)
        {
            if (!sender.IsMe) return;
            switch (orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.LastHit:
                case Orbwalking.OrbwalkingMode.LaneClear:
                    if (!GetBool("main.clear.minions")) if (args.Target is Obj_AI_Minion && args.Order == GameObjectOrder.AttackTo) args.Process = false;
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    if (!GetBool("main.harass.minions")) if (args.Target is Obj_AI_Minion && args.Order == GameObjectOrder.AttackTo) args.Process = false;
                    break;
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (GetBool("main.drawings.q") && q.Level > 0) Render.Circle.DrawCircle(Player.Position, q.Range, q.IsReady() ? Color.BlueViolet : Color.Red);
            if (GetBool("main.drawings.w") && w.Level > 0) Render.Circle.DrawCircle(Player.Position, w.Range, w.IsReady() ? Color.BlueViolet : Color.Red);
            if (GetBool("main.drawings.e") && e.Level > 0) Render.Circle.DrawCircle(Player.Position, e.Range, e.IsReady() ? Color.BlueViolet : Color.Red);
            if (GetBool("main.drawings.r") && r.Level > 0) Render.Circle.DrawCircle(Player.Position, r.Range, r.IsReady() ? Color.BlueViolet : Color.Red);
        }

        private static void LoadMenu()
        {
            config = new Menu("Sona", "main", true);

            Menu orb = config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
            orbwalker = new Orbwalking.Orbwalker(orb);

            Menu targetSelector = config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
            TargetSelector.AddToMenu(targetSelector);

            Menu combo = new Menu("Combo", "main.combo");
            combo.AddBool("Use Q", "main.combo.q", true);
            combo.AddBool("Use E", "main.combo.e", true);
            combo.AddBool("Use R", "main.combo.r", true);
            combo.AddItem(new MenuItem("main.combo.rminhit", "R min hit").SetValue(new Slider(2, 1, 5)));

            Menu harass = new Menu("Harass", "main.harass");
            harass.AddBool("Use Q", "main.harass.q", true);
            harass.AddSlider("Q manalimiter", "main.harass.qm", 15);
            harass.AddBool("Only Q when hits 2", "main.harass.q2", true);
            harass.AddBool("Attack minions", "main.harass.minions", false);

            Menu clear = new Menu("Clear", "main.clear");
            clear.AddBool("Use Q", "main.clear.q", false);
            clear.AddSlider("Q manalimiter", "main.clear.qm", 55);
            clear.AddBool("Attack minions", "main.clear.minions", false);

            Menu settings = new Menu("Settings", "main.settings");
            Menu wsettings = new Menu("Heal settings", "main.settings.w");
            wsettings.AddSlider("When HP below", "main.settings.w.hp", 25);
            wsettings.AddBool("Auto heal enabled", "main.settings.w.enabled", true);
            settings.AddSubMenu(wsettings);
            Menu gapcloser = new Menu("Gapcloser", "main.settings.gapcloser");
            gapcloser.AddSlider("Use when hp below", "main.settings.gapcloser.hp", 10);
            gapcloser.AddBool("Use to save allies", "main.settings.gapcloser.allies", true);
            gapcloser.AddBool("Enabled", "main.settings.gapcloser.enabled", true);
            settings.AddSubMenu(gapcloser);
            settings.AddBool("Interrupter enabled", "main.settings.interrupter", true);
            settings.AddItem(new MenuItem("main.settings.pred", "Prediction").SetValue(new StringList(new[] { "Common", "SPrediction" })));
            settings.AddBool("Use Ignite", "main.settings.ignite", true);

            Menu drawings = new Menu("Drawings", "main.drawings");
            drawings.AddBool("Draw Q", "main.drawings.q", true);
            drawings.AddBool("Draw W", "main.drawings.w", true);
            drawings.AddBool("Draw E", "main.drawings.e", false);
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
