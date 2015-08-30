/*
	Leaguesharp Shine's SPrediction prediction test
	Copyright (C) 2015 HyunMi

	This program is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using SPrediction;
using Color = System.Drawing.Color;

namespace ThreshQ_SPrediction
{
	public static class Program
	{
		private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
		private static Spell _q;
		private static Orbwalking.Orbwalker _orbwalker;
		private static Menu _config;
		private static StreamWriter _logger;
		private static string _lastCast;

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
			if (Player.ChampionName != "Thresh") return;

			_logger = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\log.txt") { AutoFlush = true };

			_q = new Spell(SpellSlot.Q, 1100);
			_q.SetSkillshot(500, 70, 1900, true, SkillshotType.SkillshotLine);

			_config = new Menu("Prediction", "Prediction", true);
			Menu orb = new Menu("Orbwalker", "Orbwalker");
			_orbwalker = new Orbwalking.Orbwalker(orb);
			_config.AddSubMenu(orb);
			Menu settings = new Menu("Settings", "Settings");
			settings.AddItem(
				new MenuItem("Write", "Write Last Cast information to log").SetValue(new KeyBind(0x4C, KeyBindType.Press)));
			settings.AddItem(new MenuItem("Info", "Log files are located in %appdata%"));
			_config.AddSubMenu(settings);
			SPrediction.Prediction.Initialize(_config);
			_config.AddToMainMenu();

			Game.OnUpdate += Game_OnUpdate;
			Drawing.OnDraw += Drawing_OnDraw;
		}

		private static void Game_OnUpdate(EventArgs args)
		{
			if (Player.IsDead) return;
			if (_config.Item("Write").IsActive()) _logger.WriteLine(_lastCast);
			if (_orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo) Combo();
		}

		private static void Combo()
		{
			Obj_AI_Hero tar = _q.GetTarget();
			if (tar == null) return;

			HitChance hc;
			Vector2 pred = SPrediction.Prediction.GetPrediction(tar, _q, tar.GetWaypoints(), tar.AvgMovChangeTime(), tar.LastMovChangeTime(), tar.AvgPathLenght(), out hc , Player.Position);
			try
			{
				_q.SPredictionCast(tar, hc);
				float distance = Player.Distance(tar);
				float travelTime = _q.GetTravelTime(distance);
				_lastCast = "Distance(units): " + distance + " TravelTime(sec): " + travelTime + " Hitchance: " + hc;
			}
			catch (Exception)
			{
				// ignored
			}
		}

		private static void Drawing_OnDraw(EventArgs args)
		{
			if (_q.Level <= 0) return;

			if (_q.IsReady())
			{
				_q.DrawRange(!Player.GetEnemiesInRange(_q.Range).Count.Equals(0) ? Color.GreenYellow : Color.Aqua);
				return;
			}
			_q.DrawRange(Color.Red);
		}

		private static void DrawRange(this Spell spell, Color color)
		{
			Render.Circle.DrawCircle(Player.Position, spell.Range, color);
		}

		private static float GetTravelTime(this Spell spell, float distance)
		{
			float travelTime = distance / spell.Speed;
			return travelTime;
		}
	}
}
