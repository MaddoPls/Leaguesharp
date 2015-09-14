using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace Kekplank
{
    public static class Program
    {
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

        private static Menu config;
        private static Orbwalking.Orbwalker orbwalker;
        private static Spell q = new Spell(SpellSlot.Q, 625);
        private static Spell w = new Spell(SpellSlot.W);
        private static Spell e = new Spell(SpellSlot.E, 1000);
        private static Spell r = new Spell(SpellSlot.R);
        private static Spell ignite = new Spell(Player.GetSpellSlot("summonerdot"), 600, TargetSelector.DamageType.True);
        private static List<Barrel> livebarrels = new List<Barrel>();
        private static List<Barrel> allBarrels = new List<Barrel>();
        private static Barrel targetedBarrelQ;
        private const float ConnectionRange = 650;
        private const float ExtendConnectionRange = 585;
        private const float ExplosionRange = 400;
        private const float SightRange = 1200;
        private const float Max = 845;
        private static bool eAllowed = true;

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
            if (Player.ChampionName != "Gangplank") return;

            r.SetSkillshot(0.7f, 200, float.MaxValue, false, SkillshotType.SkillshotCircle);

            LoadMenu();

            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        private static void LoadMenu()
        {
            config = new Menu("Kekplank", "main", true);

            Menu orb = config.AddSubMenu(new Menu("Orbwalker", "main.orbwalker"));
            orbwalker = new Orbwalking.Orbwalker(orb);

            Menu targetSelector = config.AddSubMenu(new Menu("Target selector", "main.targetselector"));
            TargetSelector.AddToMenu(targetSelector);

            Menu combo = new Menu("Combo", "main.combo");
            combo.AddBool("Use Q", "main.combo.q", true);
            combo.AddBool("Use E", "main.combo.e", true);
            combo.AddBool("Bust E with Q", "main.combo.qe", true);

            Menu harass = new Menu("Harass", "main.harass");
            harass.AddBool("Use Q", "main.harass.q", true);
            harass.AddItem(new MenuItem("main.harass.qm", "Manalimiter").SetValue(new Slider(55)));

            Menu lasthit = new Menu("Lasthit", "main.lasthit");
            lasthit.AddBool("Use Q", "main.lasthit.q", true);
            lasthit.AddItem(new MenuItem("main.lasthit.qm", "Manalimiter").SetValue(new Slider(55)));

            Menu clear = new Menu("Clear", "main.clear");
            clear.AddBool("Q minions", "main.clear.q", true);
            clear.AddItem(new MenuItem("main.clear.qm", "Manalimiter").SetValue(new Slider(55)));
            clear.AddBool("Attack barrels", "main.clear.barrel", false);

            Menu settings = new Menu("Settings", "main.settings");
            Menu wsettings = new Menu("W settings", "main.settings.w");
            wsettings.AddBool("Slow", "main.settings.w.slow", false);
            wsettings.AddBool("Blind", "main.settings.w.blind", true);
            wsettings.AddBool("Polymorph", "main.settings.w.poly", true);
            wsettings.AddBool("Stun", "main.settings.w.stun", true);
            wsettings.AddBool("Taunt", "main.settings.w.taunt", true);
            wsettings.AddBool("Fear", "main.settings.w.fear", true);
            wsettings.AddBool("Charm", "main.settings.w.charm", true);
            wsettings.AddItem(new MenuItem("main.settings.w.wm", "Manalimiter").SetValue(new Slider(15)));
            wsettings.AddBool("W enabled", "main.settings.w.enabled", true);
            settings.AddSubMenu(wsettings);
            settings.AddBool("Auto R", "main.settings.r", true);
            settings.AddItem(new MenuItem("main.settings.rminhit", "Min R targets hit").SetValue(new Slider(2, 1, 5)));
            settings.AddBool("Auto ignite", "main.settings.ignite", true);
            settings.AddItem(
                new MenuItem("main.settings.disablee", "Disable E from casting").SetValue(new KeyBind(0x41, KeyBindType.Press)));
            settings.AddBool("Auto pop barrel with Q", "main.settings.aqe", true);
            settings.AddItem(new MenuItem("main.settings.et", "E disable time").SetValue(new Slider(2000, 1000, 5000)));

            Menu drawings = new Menu("Drawings", "main.drawings");
            drawings.AddBool("Draw Q", "main.drawings.q", true);
            drawings.AddBool("Draw E", "main.drawings.e", false);
            drawings.AddBool("Draw focused barrel", "main.drawings.barrel", true);

            config.AddSubMenu(combo);
            config.AddSubMenu(harass);
            config.AddSubMenu(clear);
            config.AddSubMenu(settings);
            config.AddSubMenu(drawings);
            config.AddSubMenu(lasthit);
            config.AddToMainMenu();
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.IsDead) return;
            if (GetBool("main.settings.r")) Casts.R();
            if (GetBool("main.settings.ignite")) Casts.Ignite();
            if (ManalimiterCheck("main.settings.w.wm") && GetBool("main.settings.w.enabled")) Casts.W();
            targetedBarrelQ = null;
            targetedBarrelQ = Math.ClosestBarrelWherePosInExplosionRange(Player.ServerPosition.To2D());
            if (GetBool("main.settings.aqe")) Casts.AQE();

            switch (orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                case Orbwalking.OrbwalkingMode.LastHit:
                    Clear();
                    break;
            }
        }

        private static void Combo()
        {
            if (
                Math.ClosestBarrelWherePosInExplosionRange(
                    Player.GetEnemiesInRange(SightRange).First().ServerPosition.To2D()) != null) Casts.AttackBarrel();

            if (GetBool("main.combo.qe"))
            {
                if (targetedBarrelQ != null)
                {
                    Barrel bar =
                        Math.ClosestBarrelWherePosInExplosionRange(
                            Player.GetEnemiesInRange(SightRange).First().ServerPosition.To2D());

                    if (bar != null && bar.IsReady) Casts.QMinion(bar.BarrelObj);
                }
            }

            if (GetBool("main.combo.e") && config.Item("main.settings.disablee").GetValue<KeyBind>().Active.Equals(false))
            {
                Casts.EHero();
            }

            if (GetBool("main.combo.q"))
            {
                Obj_AI_Hero target = TargetSelector.GetTarget(q.Range, q.DamageType);
                if (target != null)
                {
                    Casts.QHero(target);
                }
            }
        }

        private static void Harass()
        {
            if (GetBool("main.harass.q") && ManalimiterCheck("main.harass.qm"))
            {
                Obj_AI_Hero target = TargetSelector.GetTarget(q.Range, q.DamageType);
                if (target != null)
                {
                    Casts.QHero(target);
                }

            }
        }

        private static void Clear()
        {
            if (GetBool("main.clear.barrel")) if (Math.ClosestBarrel(Player.GetEnemiesInRange(SightRange).First().ServerPosition.To2D()) != null) Casts.AttackBarrel();

            if (GetBool("main.clear.q") && ManalimiterCheck("main.clear.qm"))
            {
                Obj_AI_Minion targetMinion = MinionManager.GetMinions(q.Range).FirstOrDefault() as Obj_AI_Minion;
                if (targetMinion != null)
                {
                    Casts.QMinion(targetMinion);
                }
            }
        }

        private static void Lasthit()
        {
            if (GetBool("main.lasthit.q") && ManalimiterCheck("main.lasthit.qm"))
            {
                Obj_AI_Minion targetMinion = MinionManager.GetMinions(q.Range).FirstOrDefault() as Obj_AI_Minion;
                if (targetMinion != null)
                {
                    Casts.QMinion(targetMinion);

                }
            }
        }


        private static class Casts
        {
            internal static void R()
            {
                if (!r.IsReady()) return;
                float dmg = 360 + (240 * r.Level) + (Player.TotalMagicalDamage);
                Obj_AI_Hero target = r.GetTarget(20000);
                if (target == null) return;
                if (target.Health > dmg) return;
                int minhit = config.Item("main.settings.rminhit").GetValue<Slider>().Value;
                switch (minhit)
                {
                    case 1:
                        r.CastOnUnit(target);
                        break;
                    default:
                        r.CastIfWillHit(target, minhit);
                        break;
                }
            }

            internal static void W()
            {
                if (!w.IsReady() || !Player.HasDebuffs()) return;
                w.Cast();
            }

            internal static void Ignite()
            {
                int dmg = (Player.Level * 20) + 50;
                foreach (Obj_AI_Hero tar in Player.GetEnemiesInRange(ignite.Range).Where(tar => dmg >= tar.Health && ignite.IsReady()))
                {
                    ignite.Cast(tar);
                }
            }

            internal static void QMinion(Obj_AI_Minion minion)
            {
                if (!q.IsReady() || Player.Distance(minion.ServerPosition) > q.Range || q.GetDamage(minion) < minion.Health || Player.Distance(minion.ServerPosition) < Player.AttackRange) return;
                q.CastOnUnit(minion);
            }

            internal static void QHero(Obj_AI_Hero hero)
            {
                if (!q.IsReady() || Player.Distance(hero.ServerPosition) > q.Range) return;
                q.CastOnUnit(hero);
            }

            internal static void AQE()
            {
                if (!q.IsReady() || !Math.EnemyInBarrelExplosionRange()) return;
                Barrel closestBar = Math.ClosestReadyBarrel(Player.ServerPosition.To2D());

                if (Math.ClosestBarrel(closestBar.BarrelObj.Position.To2D()) != null || closestBar.BarrelObj.GetEnemiesInRange(ExplosionRange).Count != 0)
                {
                    q.Cast(closestBar.BarrelObj);
                }
            }

            internal static void EMinion()
            {
                if (!e.IsReady() || MinionManager.GetMinions(e.Range).Count.Equals(0)) return;

                List<Vector2> minionPos = MinionManager.GetMinionsPredictedPositions(MinionManager.GetMinions(e.Range), e.Delay, e.Width, e.Speed, Player.ServerPosition,
                    e.Range, false, e.Type);
                MinionManager.FarmLocation castPos = MinionManager.GetBestCircularFarmLocation(minionPos, e.Width, e.Range);
                if (castPos.MinionsHit != 0) e.Cast(castPos.Position);
            }

            internal static void EHero()
            {
                if (!e.IsReady() || eAllowed.Equals(false)) return;
                List<Obj_AI_Hero> enemies = Player.GetEnemiesInRange(SightRange);
                if (enemies.Count == 0) return;
                if (livebarrels.Count == 0 || livebarrels == null)
                {
                    switch (enemies.Count)
                    {
                        case 0:
                            return;
                        case 1:
                            Vector3 castPos = Player.ServerPosition.Extend(enemies[0].ServerPosition, e.Range / 2);
                            e.Cast(castPos);
                            break;
                        default:
                            Vector2 center = Math.GetCenter(enemies);
                            Vector3 castPos2 = Player.ServerPosition.Extend(center.To3D(), e.Range / 2);
                            e.Cast(castPos2);
                            break;
                    }
                }
                else
                {
                    switch (enemies.Count)
                    {
                        case 0:
                            return;
                        case 1:
                            Barrel closestBarrel = Math.ClosestBarrel(enemies[0].ServerPosition.To2D());
                            if (enemies[0].ServerPosition.Distance(closestBarrel.BarrelObj.Position) < 1000)
                            {
                                Vector3 vertex0 = closestBarrel.BarrelObj.Position.Extend(enemies[0].ServerPosition, ConnectionRange);
                                Vector3 vertex1 = closestBarrel.BarrelObj.Position.Extend(Player.ServerPosition, ConnectionRange);
                                Vector2 castPos = Math.Average(vertex0.To2D(), vertex1.To2D());
                                e.Cast(castPos);
                            }
                            break;
                        default:
                            var center2 = Math.GetCenter(enemies);
                            Barrel closestBarrel2 = Math.ClosestBarrel(center2);
                            if (center2.Distance(closestBarrel2.BarrelObj.Position) < 1000)
                            {
                                Vector3 vertex0 = closestBarrel2.BarrelObj.Position.Extend(center2.To3D(), ConnectionRange);
                                Vector3 vertex1 = closestBarrel2.BarrelObj.Position.Extend(Player.ServerPosition, ConnectionRange);
                                Vector2 castPos = Math.Average(vertex0.To2D(), vertex1.To2D());
                                e.Cast(castPos);
                            }
                            break;
                    }
                }
            }

            internal static void AttackBarrel()
            {
                if (livebarrels.Count == 0) return;
                Barrel bartar = Math.ClosestBarrel(Player.ServerPosition.To2D());
                if (bartar == null) return;
                if (Player.ServerPosition.Distance(bartar.BarrelObj.Position) < Player.AttackRange && bartar.BarrelObj.IsValidTarget() && bartar.BarrelObj.IsTargetable)
                {
                    Player.IssueOrder(GameObjectOrder.AttackUnit, bartar.BarrelObj);
                }
            }
        }

        internal static class Math
        {
            public static Vector2 GetCenter(List<Vector2> vertices)
            {
                float x = 0, y = 0;
                foreach (Vector2 vertex in vertices)
                {
                    x += vertex.X;
                    y += vertex.Y;
                }
                x = x / vertices.Count;
                y = y / vertices.Count;
                return new Vector2(x, y);
            }

            public static Vector2 GetCenter(List<Obj_AI_Hero> heroes)
            {
                List<Vector2> vertices = heroes.Select(h => h.ServerPosition.To2D()).ToList();
                float x = 0, y = 0;
                foreach (Vector2 vertex in vertices)
                {
                    x += vertex.X;
                    y += vertex.Y;
                }
                x = x / vertices.Count;
                y = y / vertices.Count;
                return new Vector2(x, y);
            }

            public static Barrel ClosestBarrel(Vector2 position)
            {
                if (livebarrels.Count == 0) return null;
                return livebarrels.OrderBy(b => b.BarrelObj.ServerPosition.Distance(position.To3D())).FirstOrDefault();
            }
            public static Barrel ClosestReadyBarrel(Vector2 position)
            {
                if (livebarrels.Count == 0) return null;
                return
                    livebarrels.Where(bar => bar.IsReady)
                        .OrderBy(b => b.BarrelObj.ServerPosition.Distance(position.To3D()))
                        .FirstOrDefault();
            }

            public static Barrel ClosestBarrelWherePosInExplosionRange(Vector2 position)
            {
                if (livebarrels.Count == 0) return null;
                return livebarrels.Where(b => b.BarrelObj.ServerPosition.Distance(position.To3D()) < ExplosionRange).ToList().OrderBy(b => b.BarrelObj.ServerPosition.Distance(position.To3D())).FirstOrDefault();
            }

            public static bool EnemyInBarrelExplosionRange()
            {
                IEnumerable<Barrel> barrels = livebarrels.Where(bar => bar.IsReady);
                return barrels.Any(barrel => barrel.BarrelObj.GetEnemiesInRange(ExplosionRange).Count != 0);
            }

            public static Vector2 Average(Vector2 vertex0, Vector2 vertex1)
            {
                return new Vector2((vertex0.X + vertex1.X) / 2, (vertex0.Y + vertex1.Y) / 2);
            }
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            foreach (Barrel barrel in livebarrels.Where(barrel => barrel.BarrelObj.NetworkId == sender.NetworkId)) livebarrels.Remove(barrel);
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name == "Barrel")
            {
                livebarrels.Add(new Barrel(sender as Obj_AI_Minion));
                eAllowed = false;
                int disableT = config.Item("main.settings.et").GetValue<Slider>().Value;
                Utility.DelayAction.Add(disableT, () => eAllowed = true);
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (GetBool("main.drawings.q") && q.Level > 0)
                Render.Circle.DrawCircle(Player.Position, q.Range, q.IsReady() ? Color.BlueViolet : Color.Red);

            if (GetBool("main.drawings.e") && e.Level > 0)
                Render.Circle.DrawCircle(Player.Position, e.Range, e.IsReady() ? Color.BlueViolet : Color.Red);

            if (GetBool("main.drawings.barrel") && q.Level > 0 && targetedBarrelQ != null)
                Render.Circle.DrawCircle(targetedBarrelQ.BarrelObj.Position, 100, q.IsReady() ? Color.BlueViolet : Color.Red);
        }

        private static void AddBool(this Menu menu, string displayName, string name, bool value)
        {
            menu.AddItem(new MenuItem(name, displayName).SetValue(value));
        }

        private static bool GetBool(string name)
        {
            return config.Item(name).GetValue<bool>();
        }

        private static bool ManalimiterCheck(string name)
        {
            return Player.ManaPercent > config.Item(name).GetValue<Slider>().Value;
        }

        private static bool HasDebuffs(this Obj_AI_Hero champ)
        {
            List<BuffType> debuffs = new List<BuffType>();
            if (GetBool("main.settings.w.slow")) debuffs.Add(BuffType.Slow);
            if (GetBool("main.settings.w.blind")) debuffs.Add(BuffType.Blind);
            if (GetBool("main.settings.w.poly")) debuffs.Add(BuffType.Polymorph);
            if (GetBool("main.settings.w.stun")) debuffs.Add(BuffType.Stun);
            if (GetBool("main.settings.w.taunt")) debuffs.Add(BuffType.Taunt);
            if (GetBool("main.settings.w.fear")) debuffs.Add(BuffType.Fear);
            if (GetBool("main.settings.w.charm")) debuffs.Add(BuffType.Charm);
            return debuffs.Any(champ.HasBuffOfType);
        }

        internal class Barrel
        {
            public Obj_AI_Minion BarrelObj;
            public bool IsReady;

            public Barrel(Obj_AI_Minion barrelObj)
            {
                this.BarrelObj = barrelObj;
                this.DelayReady();
            }

            private void DelayReady()
            {
                int decayT = Player.Level > 6 ? (Player.Level > 12 ? 1000 : 2000) : 4000;
                Utility.DelayAction.Add(decayT, () => this.IsReady = true);
            }
        }
    }
}
