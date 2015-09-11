using Anouncersharp.Packs;
using LeagueSharp;
using Events = Anouncersharp.Enumerations.Events;

namespace Anouncersharp.Handlers
{
	public static class Kills
	{
		//Thnx jodus for fixing this and thnx asuna for verifying and telling jodus that this was broken
		public static void Game_OnNotify(GameNotifyEventArgs args)
		{
			if (!Manage.Menu.get_bool("announcer.kill")) return;

			switch (args.EventId)
			{
				case GameEventId.OnChampionSingleKill:
					PlayAbstractingLayer.Kill(ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.NetworkId).IsAlly
						? Events.Kills.AllyKill
						: Events.Kills.EnemyKill);
					break;
				case GameEventId.OnChampionDoubleKill:
					PlayAbstractingLayer.Kill(ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.NetworkId).IsAlly
						? Events.Kills.AllyDouble
						: Events.Kills.EnemyDouble);
					break;
				case GameEventId.OnChampionTripleKill:
					PlayAbstractingLayer.Kill(ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.NetworkId).IsAlly
						? Events.Kills.AllyTriple
						: Events.Kills.EnemyTriple);
					break;
				case GameEventId.OnChampionQuadraKill:
					PlayAbstractingLayer.Kill(ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.NetworkId).IsAlly
						? Events.Kills.AllyQuadra
						: Events.Kills.EnemyQuadra);
					break;
				case GameEventId.OnChampionPentaKill:
					PlayAbstractingLayer.Kill(ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.NetworkId).IsAlly
						? Events.Kills.AllyPenta
						: Events.Kills.EnemyPenta);
					break;
				case GameEventId.OnFirstBlood:
					PlayAbstractingLayer.Kill(ObjectManager.GetUnitByNetworkId<Obj_AI_Hero>(args.NetworkId).IsAlly
						? Events.Kills.AllyFirstblood
						: Events.Kills.EnemyFirstblood);
					break;
				case GameEventId.OnAce:
					break;
				case GameEventId.OnTurretKill:
					Structures.Handle(args);
					break;
			}
		}
	}
}
