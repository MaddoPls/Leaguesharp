namespace Anouncersharp.Handlers
{
    using LeagueSharp;

    public static class ChatCommands
    {
        public static void Game_OnInput(GameInputEventArgs args)
        {
            if (args.Input.StartsWith(".")) args.Process = false;

            //.SPECIFIER TEAM(0 == enemy, 1 == ally) TIER(1,2,3,4,5)
            switch (args.Input)
            {
                case ".fb 0":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.EnemyFirstblood);
                    break;
                case ".kill 0":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.EnemyKill);
                    break;
                case ".double 0":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.EnemyDouble);
                    break;
                case ".triple 0":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.EnemyTriple);
                    break;
                case ".quadra 0":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.EnemyQuadra);
                    break;
                case ".penta 0":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.EnemyPenta);
                    break;
                case ".fb 1":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.AllyFirstblood);
                    break;
                case ".kill 1":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.AllyKill);
                    break;
                case ".double 1":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.AllyDouble);
                    break;
                case ".triple 1":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.AllyTriple);
                    break;
                case ".quadra 1":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.AllyQuadra);
                    break;
                case ".penta 1":
                    PlayAbstractingLayer.Kill(Enumerations.Events.Kills.AllyPenta);
                    break;
                case ".welcome":
                    PlayAbstractingLayer.Notify(Enumerations.Events.Notify.Welcome);
                    break;
                case ".minions":
                    PlayAbstractingLayer.Notify(Enumerations.Events.Notify.MinionSpawn);
                    break;
                case ".win":
                    PlayAbstractingLayer.Notify(Enumerations.Events.Notify.Win);
                    break;
                case ".loss":
                    PlayAbstractingLayer.Notify(Enumerations.Events.Notify.Defeat);
                    break;
                case ".tower 0 top 1":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyTop_T1);
                    break;
                case ".tower 0 top 2":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyTop_T2);
                    break;
                case ".tower 0 top 3":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyTop_T3);
                    break;
                case ".tower 0 top 4":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyTop_InHib);
                    break;
                case ".tower 0 mid 1":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyMid_T1);
                    break;
                case ".tower 0 mid 2":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyMid_T2);
                    break;
                case ".tower 0 mid 3":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyMid_T3);
                    break;
                case ".tower 0 mid 4":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyMid_InHib);
                    break;
                case ".tower 0 bot 1":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyBot_T1);
                    break;
                case ".tower 0 bot 2":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyBot_T2);
                    break;
                case ".tower 0 bot 3":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyBot_T3);
                    break;
                case ".tower 0 bot 4":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyBot_InHib);
                    break;
                case ".tower 1 top 1":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyTop_T1);
                    break;
                case ".tower 1 top 2":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyTop_T2);
                    break;
                case ".tower 1 top 3":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyTop_T3);
                    break;
                case ".tower 1 top 4":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyTop_InHib);
                    break;
                case ".tower 1 mid 1":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyMid_T1);
                    break;
                case ".tower 1 mid 2":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyMid_T2);
                    break;
                case ".tower 1 mid 3":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyMid_T3);
                    break;
                case ".tower 1 mid 4":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyMid_InHib);
                    break;
                case ".tower 1 bot 1":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyBot_T1);
                    break;
                case ".tower 1 bot 2":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyBot_T2);
                    break;
                case ".tower 1 bot 3":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyBot_T3);
                    break;
                case ".tower 1 bot 4":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyBot_InHib);
                    break;
                case ".tower 0 5":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.EnemyNexus_Turrets);
                    break;
                case ".tower 1 5":
                    PlayAbstractingLayer.Structure(Enumerations.Events.Structures.AllyNexus_Turrets);
                    break;
            }
        }
    }
}
