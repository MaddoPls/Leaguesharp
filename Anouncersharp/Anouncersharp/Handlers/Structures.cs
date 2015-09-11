using Anouncersharp.Packs;
using LeagueSharp;
using LeagueSharp.SDK.Core.Enumerations;
using LeagueSharp.SDK.Core.Extensions;
using SharpDX;

namespace Anouncersharp.Handlers
{
	public static class Structures
	{
		public static void Handle(GameNotifyEventArgs args)
		{
			if (!Manage.Menu.get_bool("announcer.structures") || !Manage.Menu.get_bool("announcer.enabled"))
				return;
			Obj_AI_Turret turret = ObjectManager.GetUnitByNetworkId<Obj_AI_Turret>(args.NetworkId);
			if (turret.IsAlly)
			{
				switch (turret.GetTurretType())
				{
					case TurretType.TierOne:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyTop_T1);
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
					case TurretType.TierTwo:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
					case TurretType.TierThree:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
					case TurretType.TierFour:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
				}
			}
			else
			{
				switch (turret.GetTurretType())
				{
					case TurretType.TierOne:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyTop_T1);
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
					case TurretType.TierTwo:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
					case TurretType.TierThree:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
					case TurretType.TierFour:
						switch (GetLane(turret))
						{
							case Lanes.Top:
								break;
							case Lanes.Mid:
								break;
							case Lanes.Bot:
								break;
						}
						break;
				}
			}
		}

		private static Lanes GetLane(Obj_AI_Turret turret)
		{
			if ((turret.Distance(Point1) <= 1500 || turret.Distance(Point2) <= 1500 || turret.Distance(Point3) <= 1500 ||
			     turret.Distance(Point4) <= 1500 || turret.Distance(Point5) <= 1500 || turret.Distance(Point6) <= 1500 ||
			     turret.Distance(Point7) <= 1500 || turret.Distance(Point8) <= 1500 || turret.Distance(Point9) <= 1500 ||
			     turret.Distance(Point10) <= 1500 || turret.Distance(Point11) <= 1500 || turret.Distance(Point12) <= 1500 ||
			     turret.Distance(Point13) <= 1500 || turret.Distance(Point14) <= 1500 || turret.Distance(Point15) <= 1500 ||
			     turret.Distance(Point16) <= 1500 || turret.Distance(Point17) <= 1500 || turret.Distance(Point18) <= 1500 ||
			     turret.Distance(Point19) <= 1500 || turret.Distance(Point20) <= 1500 || turret.Distance(Point21) <= 1500 ||
			     turret.Distance(Point22) <= 1500 || turret.Distance(Point23) <= 1500 || turret.Distance(Point24) <= 1500 ||
			     turret.Distance(Point25) <= 1500))
			{
				return Lanes.Bot;
			}

			if ((turret.Distance(Pointtop1) <= 1500 || turret.Distance(Pointtop2) <= 1500 || turret.Distance(Pointtop3) <= 1500 ||
			     turret.Distance(Pointtop4) <= 1500 || turret.Distance(Pointtop5) <= 1500 || turret.Distance(Pointtop6) <= 1500 ||
			     turret.Distance(Pointtop7) <= 1500 || turret.Distance(Pointtop8) <= 1500 || turret.Distance(Pointtop9) <= 1500 ||
			     turret.Distance(Pointtop10) <= 1500 || turret.Distance(Pointtop11) <= 1500))
			{
				return Lanes.Top;
			}

			if (turret.Distance(Pointmid1) <= 800 || turret.Distance(Pointmid2) <= 800 || turret.Distance(Pointmid3) <= 800 ||
			    turret.Distance(Pointmid4) <= 800 || turret.Distance(Pointmid5) <= 800 || turret.Distance(Pointmid6) <= 800 ||
			    turret.Distance(Pointmid7) <= 800 || turret.Distance(Pointmid8) <= 800 || turret.Distance(Pointmid9) <= 800 ||
			    turret.Distance(Pointmid10) <= 800)
			{
				return Lanes.Mid;
			}
			return Lanes.Unknown;
		}

		//Ty HOES for letting me steal your vectors (mm)
		// botlane
		private static readonly Vector2 Point1 = new Vector2(5758, 1230);
		private static readonly Vector2 Point2 = new Vector2(6258, 1230);
		private static readonly Vector2 Point3 = new Vector2(6758, 1230);
		private static readonly Vector2 Point4 = new Vector2(7258, 1230);
		private static readonly Vector2 Point5 = new Vector2(7758, 1230);
		private static readonly Vector2 Point6 = new Vector2(8258, 1230);
		private static readonly Vector2 Point7 = new Vector2(8758, 1230);
		private static readonly Vector2 Point8 = new Vector2(9258, 1230);
		private static readonly Vector2 Point9 = new Vector2(9758, 1230);
		private static readonly Vector2 Point10 = new Vector2(10258, 1230);
		private static readonly Vector2 Point11 = new Vector2(10758, 1230);
		private static readonly Vector2 Point12 = new Vector2(11858, 1230);
		private static readonly Vector2 Point13 = new Vector2(12616, 1754);
		private static readonly Vector2 Point14 = new Vector2(13416, 1754);
		private static readonly Vector2 Point15 = new Vector2(14216, 6958);
		private static readonly Vector2 Point16 = new Vector2(15016, 6958);
		private static readonly Vector2 Point17 = new Vector2(15816, 6958);
		private static readonly Vector2 Point18 = new Vector2(16616, 6958);
		private static readonly Vector2 Point19 = new Vector2(17416, 6958);
		private static readonly Vector2 Point20 = new Vector2(18216, 6958);
		private static readonly Vector2 Point21 = new Vector2(19016, 6958);
		private static readonly Vector2 Point22 = new Vector2(19816, 6958);
		private static readonly Vector2 Point23 = new Vector2(20616, 6958);
		private static readonly Vector2 Point24 = new Vector2(13416, 5400);
		private static readonly Vector2 Point25 = new Vector2(13482, 3586);

		// toplane
		private static readonly Vector2 Pointtop1 = new Vector2(1212, 5866);
		private static readonly Vector2 Pointtop2 = new Vector2(1212, 6666);
		private static readonly Vector2 Pointtop3 = new Vector2(1212, 7466);
		private static readonly Vector2 Pointtop4 = new Vector2(1212, 8266);
		private static readonly Vector2 Pointtop5 = new Vector2(1212, 9066);
		private static readonly Vector2 Pointtop6 = new Vector2(1212, 9866);
		private static readonly Vector2 Pointtop7 = new Vector2(1212, 10666);
		private static readonly Vector2 Pointtop8 = new Vector2(1212, 11466);
		private static readonly Vector2 Pointtop9 = new Vector2(1212, 12266);
		private static readonly Vector2 Pointtop10 = new Vector2(1212, 13066);
		private static readonly Vector2 Pointtop11 = new Vector2(1212, 13866);

		// midelane
		private static readonly Vector2 Pointmid1 = new Vector2(4000, 4000);
		private static readonly Vector2 Pointmid2 = new Vector2(4800, 4800);
		private static readonly Vector2 Pointmid3 = new Vector2(5600, 5600);
		private static readonly Vector2 Pointmid4 = new Vector2(6400, 6400);
		private static readonly Vector2 Pointmid5 = new Vector2(7200, 7200);
		private static readonly Vector2 Pointmid6 = new Vector2(8000, 8000);
		private static readonly Vector2 Pointmid7 = new Vector2(8800, 8800);
		private static readonly Vector2 Pointmid8 = new Vector2(9600, 9600);
		private static readonly Vector2 Pointmid9 = new Vector2(10400, 10400);
		private static readonly Vector2 Pointmid10 = new Vector2(1212, 5866);

		private enum Lanes
		{
			Top,
			Mid,
			Bot,
			Unknown
		}
	}
}