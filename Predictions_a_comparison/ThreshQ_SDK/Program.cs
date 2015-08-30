/*
	Leaguesharp.SDK prediction test
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
using LeagueSharp.SDK.Core;
using LeagueSharp.SDK.Core.Enumerations;
using LeagueSharp.SDK.Core.UI.IMenu.Values;
using LeagueSharp.SDK.Core.Wrappers;
using Menu = LeagueSharp.SDK.Core.UI.IMenu.Menu;
using System.Drawing;
using LeagueSharp.SDK.Core.Extensions;
using LeagueSharp.SDK.Core.Math.Prediction;

namespace ThreshQ_SDK
{
	public static class Program
	{
		private static Obj_AI_Hero Player {get { return ObjectManager.Player; }}
		private static Spell _q;
		private static Orbwalker _orbwalker;
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
			_q.SetSkillshot(true, SkillshotType.SkillshotLine);

			_config = new Menu("Prediction", "Prediction", true);
			_orbwalker = new Orbwalker();
			Menu settings = new Menu("Settings", "Settings");
			settings.Add(new MenuKeyBind("Write", "Write Last Cast information to log", System.Windows.Forms.Keys.L,
				KeyBindType.Press));
			settings.Add(new MenuSeparator("Info", "Log files are located in %appdata%"));
			_config.Add(settings);
			_config.Attach();

			Game.OnUpdate += Game_OnUpdate;
			Drawing.OnDraw += Drawing_OnDraw;
		}

		private static void Game_OnUpdate(EventArgs args)
		{
			if (Player.IsDead) return;
			if (_config["Settings"]["Write"].GetValue<MenuKeyBind>().Active) _logger.WriteLine(_lastCast);
			if (Orbwalker.ActiveMode == OrbwalkerMode.Orbwalk) Combo();
		}

		private static void Combo()
		{
			Obj_AI_Hero tar = _q.GetTarget();
			if (tar == null || !tar.IsValid) return;

			PredictionOutput pred = _q.GetPrediction(tar, false, _q.Range);
			try
			{
				_q.Cast(pred.CastPosition);
				float distance = Player.Distance(tar);
				float travelTime = _q.GetTravelTime(distance);
				_lastCast = "Distance(units): " + distance + " TravelTime(sec): " + travelTime + " Hitchance: " + pred.Hitchance;
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
				_q.DrawRange(Color.GreenYellow);
				return;
			}
			_q.DrawRange(Color.Red);
		}

		private static void DrawRange(this Spell spell, Color color)
		{
			Drawing.DrawCircle(Player.Position, spell.Range, color);
		}

		private static float GetTravelTime(this Spell spell, float distance)
		{
			float travelTime = distance / spell.Speed;
			return travelTime;
		}
	}
}
