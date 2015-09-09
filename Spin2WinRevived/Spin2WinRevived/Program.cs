using System;
using LeagueSharp;
using LeagueSharp.Common;


namespace Spin2WinRevived
{
    using SharpDX;

    public static class Program
    {
        private const int Degrees = 30;
        private const int DelayT = 30;

        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        private static Menu config;

        private static Vector2 lastVertex1 = new Vector2(0,0);
        private static bool enabled = true;

        static void Main(string[] args)
        {
            if (Game.Mode == GameMode.Running)
            {
                Game_OnStart(new EventArgs());
            }

            Game.OnStart += Game_OnStart;
        }

        private static void Game_OnStart(EventArgs eventArgs)
        {
            config = new Menu("Much spin such wow", "main", true);
            config.AddItem(new MenuItem("main.k", "Spin keybind").SetValue(new KeyBind(0x41, KeyBindType.Press)));
            config.AddToMainMenu();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (lastVertex1 == new Vector2(0, 0)) lastVertex1 = Game.CursorPos.To2D();
            if (config.Item("main.k").GetValue<KeyBind>().Active && enabled) Spin();
        }

        private static void Spin()
        {
            Vector2 rotated = Rotate(Player.ServerPosition.To2D(), lastVertex1);
            Orbwalking.MoveTo(rotated.To3D());
            lastVertex1 = rotated;
            enabled = false;
            Utility.DelayAction.Add(DelayT, () => enabled = true);
        }

        private static Vector2 Rotate(Vector2 vertexOrigin, Vector2 vertex)
        {
            Vector2 vertex1 = vertexOrigin;
            Vector2 vertex2 = vertex;
            double x = (Math.Cos(Degrees) * (vertex2.X - vertex1.X)) - (Math.Sin(Degrees) * (vertex2.Y - vertex1.Y)) + vertex1.X;
            double y = (Math.Sin(Degrees) * (vertex2.X - vertex1.X) - (Math.Cos(Degrees) * (vertex2.Y - vertex1.Y))) + vertex1.Y;
            Vector2 point = new Vector2((float)x, (float)y);
            return point;
        }
    }
}
