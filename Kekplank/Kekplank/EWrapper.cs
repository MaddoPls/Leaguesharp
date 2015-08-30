using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace Kekplank
{
	public static class EWrapper
	{
		public static List<Barrel> LiveBarrels = new List<Barrel>(); 

		//GameObject.OnDelete gave weird behaviour

		public static void GameObject_OnCreate(GameObject sender, EventArgs args)
		{
			if (sender.Name == "Barrel" && sender.IsAlly)
			{
				Barrel barrel = new Barrel(sender);
			}
		}

		public static class CastHelper
		{
			private static bool _eAllowed = true;

			private static bool IsReadyE()
			{
				return _eAllowed && !Program.E.IsReady().Equals(false);
			}

			private static void DisableE()
			{
				_eAllowed = false;
				Utility.DelayAction.Add(2000, () => _eAllowed = true);
			}

			private static void CheckBarrel(Barrel barrel)
			{
				try
				{
					Vector3 tmp = barrel.GameObj.Position;
				}
				catch (Exception ex)
				{
					if (ex is NullReferenceException || ex is GameObjectNotFoundException)
					{
						barrel.Dispose();
					}
				}
			}

			public static bool EnemiesInBlastRange()
			{
				return LiveBarrels.Any(barrel => barrel.BarrelPos.CountEnemiesInRange(400) != 0);
			}

			public static void CastE()
			{
				if (Program.Config.SubMenu("Settings").Item("DisableE").GetValue<KeyBind>().Active) return;

				Obj_AI_Hero tar = Program.Player.GetEnemiesInRange(Program.E.Range).FirstOrDefault();
				if (tar == null || !Program.E.CanCast(tar) || !IsReadyE()) return;
				try
				{
					Program.E.Cast(tar.Position);
					DisableE();
				}
				catch (Exception)
				{
					// ignored
				}
			}

			public static void PopEWithQ()
			{
				if (!EnemiesInBlastRange()) return;

				Barrel tarBar = LiveBarrels.FirstOrDefault(barrel => barrel.IsReady && !(Program.Player.Distance(barrel.BarrelPos) > Program.Q.Range) && Program.Q.IsReady() && barrel.GameObj.IsValidTarget() && barrel.GameObj.IsTargetable && Program.Player.Distance(barrel.BarrelPos) > Program.Player.AttackRange);

				if (tarBar == null) return;
				Obj_AI_Base tar = new Obj_AI_Base((ushort) tarBar.GameObj.Index, (uint) tarBar.GameObj.NetworkId);
				if (tar.IsValidTarget() && tar.IsTargetable && Program.Q.CanCast(tar))
				{
					Program.Q.CastOnUnit(tar);
					Utility.DelayAction.Add(1000, () => CheckBarrel(tarBar));
				}

			}

			public static void PopEWithAa()
			{
				Barrel tarBar = LiveBarrels.FirstOrDefault(barrel => barrel.IsReady && !(Program.Player.Distance(barrel.BarrelPos) > Program.Q.Range) && Program.Q.IsReady() && barrel.GameObj.IsValidTarget() && barrel.GameObj.IsTargetable && Program.Player.Distance(barrel.BarrelPos) < Program.Player.AttackRange);

				if (tarBar == null) return;

				Program.Player.IssueOrder(GameObjectOrder.AttackUnit, tarBar.GameObj);
				Utility.DelayAction.Add(1000, () => CheckBarrel(tarBar));
			}
		}

		public class Barrel : IDisposable
		{
			private Vector3 _barrelPos;
			private int _decayT;
			private bool _ready;

			private bool _disposed;

			public AttackableUnit GameObj;

			public Vector3 BarrelPos
			{
				get { return _barrelPos; }
			}

			public bool IsReady
			{
				get { return _ready; }
			}

			public Barrel(GameObject sender)
			{
				LiveBarrels.Add(this);

				_ready = false;
				_disposed = false;
				_barrelPos = sender.Position;
				GameObj = sender as AttackableUnit;

				_decayT = Program.Player.Level > 6 ? (Program.Player.Level > 12 ? 1000 : 2000) : 4000;

				AwaitReady();
				EndLifeTime();
			}

			private void AwaitReady()
			{
				Utility.DelayAction.Add( _decayT, () => _ready = true);
			}

			private void EndLifeTime()
			{
				Utility.DelayAction.Add(60000, Dispose);
			}

			//Not that it does anything useful but hey XDDDD
			public void Dispose()
			{
				LiveBarrels.Remove(this);
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			private void Dispose(bool disposing)
			{
				if (_disposed) return;

				if (disposing)
				{
					//managed
				}
				//unmanaged
				_disposed = true;
			}
		}
	}
}
