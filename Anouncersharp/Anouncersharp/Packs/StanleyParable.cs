// ReSharper disable InconsistentNaming

namespace Anouncersharp.Packs
{
    using System;
    using System.Media;

    public static class StanleyParable
    {
        public static void Kills(Enumerations.Events.Kills enumCase)
        {
            switch (enumCase)
            {
                case Enumerations.Events.Kills.EnemyFirstblood:
                    AbstractionLayer._Kills.EnemyFirstblood();
                    break;
                case Enumerations.Events.Kills.EnemyKill:
                    AbstractionLayer._Kills.EnemyKill();
                    break;
                case Enumerations.Events.Kills.EnemyDouble:
                    AbstractionLayer._Kills.EnemyDouble();
                    break;
                case Enumerations.Events.Kills.EnemyTriple:
                    AbstractionLayer._Kills.EnemyTriple();
                    break;
                case Enumerations.Events.Kills.EnemyQuadra:
                    AbstractionLayer._Kills.EnemyQuadra();
                    break;
                case Enumerations.Events.Kills.EnemyPenta:
                    AbstractionLayer._Kills.EnemyPenta();
                    break;
                case Enumerations.Events.Kills.AllyFirstblood:
                    AbstractionLayer._Kills.AllyFirstblood();
                    break;
                case Enumerations.Events.Kills.AllyKill:
                    AbstractionLayer._Kills.AllyKill();
                    break;
                case Enumerations.Events.Kills.AllyDouble:
                    AbstractionLayer._Kills.AllyDouble();
                    break;
                case Enumerations.Events.Kills.AllyTriple:
                    AbstractionLayer._Kills.AllyTriple();
                    break;
                case Enumerations.Events.Kills.AllyQuadra:
                    AbstractionLayer._Kills.AllyQuadra();
                    break;
                case Enumerations.Events.Kills.AllyPenta:
                    AbstractionLayer._Kills.AllyPenta();
                    break;
            }
        }

        public static void Notify(Enumerations.Events.Notify enumCase)
        {
            switch (enumCase)
            {
                case Enumerations.Events.Notify.Defeat:
                    AbstractionLayer._Notify.Defeat();
                    break;
                case Enumerations.Events.Notify.Win:
                    AbstractionLayer._Notify.Win();
                    break;
                case Enumerations.Events.Notify.MinionSpawn:
                    AbstractionLayer._Notify.MinionSpawn();
                    break;
                case Enumerations.Events.Notify.Welcome:
                    AbstractionLayer._Notify.Welcome();
                    break;
            }
        }

        public static void Structures(Enumerations.Events.Structures enumCase)
        {
            switch (enumCase)
            {
                case Enumerations.Events.Structures.EnemyTop_T1:
                    AbstractionLayer._Structures.Enemy.Top_T1();
                    break;
                case Enumerations.Events.Structures.EnemyMid_T1:
                    AbstractionLayer._Structures.Enemy.Mid_T1();
                    break;
                case Enumerations.Events.Structures.EnemyBot_T1:
                    AbstractionLayer._Structures.Enemy.Bot_T1();
                    break;
                case Enumerations.Events.Structures.EnemyTop_T2:
                    AbstractionLayer._Structures.Enemy.Top_T2();
                    break;
                case Enumerations.Events.Structures.EnemyMid_T2:
                    AbstractionLayer._Structures.Enemy.Mid_T2();
                    break;
                case Enumerations.Events.Structures.EnemyBot_T2:
                    AbstractionLayer._Structures.Enemy.Bot_T2();
                    break;
                case Enumerations.Events.Structures.EnemyTop_T3:
                    AbstractionLayer._Structures.Enemy.Top_T3();
                    break;
                case Enumerations.Events.Structures.EnemyMid_T3:
                    AbstractionLayer._Structures.Enemy.Mid_T3();
                    break;
                case Enumerations.Events.Structures.EnemyBot_T3:
                    AbstractionLayer._Structures.Enemy.Bot_T3();
                    break;
                case Enumerations.Events.Structures.EnemyTop_InHib:
                    AbstractionLayer._Structures.Enemy.Top_Inhib();
                    break;
                case Enumerations.Events.Structures.EnemyMid_InHib:
                    AbstractionLayer._Structures.Enemy.Mid_Inhib();
                    break;
                case Enumerations.Events.Structures.EnemyBot_InHib:
                    AbstractionLayer._Structures.Enemy.Bot_Inhib();
                    break;
                case Enumerations.Events.Structures.EnemyNexus_Turrets:
                    AbstractionLayer._Structures.Enemy.Nexus_Tower();
                    break;
                case Enumerations.Events.Structures.AllyTop_T1:
                    AbstractionLayer._Structures.Ally.Top_T1();
                    break;
                case Enumerations.Events.Structures.AllyMid_T1:
                    AbstractionLayer._Structures.Ally.Mid_T1();
                    break;
                case Enumerations.Events.Structures.AllyBot_T1:
                    AbstractionLayer._Structures.Ally.Bot_T1();
                    break;
                case Enumerations.Events.Structures.AllyTop_T2:
                    AbstractionLayer._Structures.Ally.Top_T2();
                    break;
                case Enumerations.Events.Structures.AllyMid_T2:
                    AbstractionLayer._Structures.Ally.Mid_T2();
                    break;
                case Enumerations.Events.Structures.AllyBot_T2:
                    AbstractionLayer._Structures.Ally.Bot_T2();
                    break;
                case Enumerations.Events.Structures.AllyTop_T3:
                    AbstractionLayer._Structures.Ally.Top_T3();
                    break;
                case Enumerations.Events.Structures.AllyMid_T3:
                    AbstractionLayer._Structures.Ally.Mid_T3();
                    break;
                case Enumerations.Events.Structures.AllyBot_T3:
                    AbstractionLayer._Structures.Ally.Bot_T3();
                    break;
                case Enumerations.Events.Structures.AllyTop_InHib:
                    AbstractionLayer._Structures.Ally.Top_Inhib();
                    break;
                case Enumerations.Events.Structures.AllyMid_InHib:
                    AbstractionLayer._Structures.Ally.Mid_Inhib();
                    break;
                case Enumerations.Events.Structures.AllyBot_InHib:
                    AbstractionLayer._Structures.Ally.Bot_Inhib();
                    break;
                case Enumerations.Events.Structures.AllyNexus_Turrets:
                    AbstractionLayer._Structures.Ally.Nexus_Tower();
                    break;
            }
        }

        private static class AbstractionLayer
        {
            public static class _Kills
            {
                public static void EnemyFirstblood()
                {
                    try
                    {
                        SoundPlayer player = new SoundPlayer(Resource.stanleyparable_notify_first_blood);
                        player.Play();
                        player.DisposePlayer(Resource.stanleyparable_notify_first_blood);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void EnemyKill()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void EnemyDouble()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void EnemyTriple()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void EnemyQuadra()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void EnemyPenta()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void AllyFirstblood()
                {
                    try
                    {
                        SoundPlayer player = new SoundPlayer(Resource.stanleyparable_notify_first_blood);
                        player.Play();
                        player.DisposePlayer(Resource.stanleyparable_notify_first_blood);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void AllyKill()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void AllyDouble()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void AllyTriple()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void AllyQuadra()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void AllyPenta()
                {
                    try
                    {

                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            public static class _Notify
            {
                public static void Defeat()
                {
                    try
                    {
                        SoundPlayer player = new SoundPlayer(Resource.stanleyparable_notify_defeat);
                        player.Play();
                        player.DisposePlayer(Resource.stanleyparable_notify_defeat);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void Win()
                {
                    try
                    {
                        SoundPlayer player = new SoundPlayer(Resource.stanleyparable_notify_victory);
                        player.Play();
                        player.DisposePlayer(Resource.stanleyparable_notify_victory);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void MinionSpawn()
                {
                    try
                    {
                        SoundPlayer player = new SoundPlayer(Resource.stanleyparable_notify_minion_spawn);
                        player.Play();
                        player.DisposePlayer(Resource.stanleyparable_notify_minion_spawn);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                public static void Welcome()
                {
                    try
                    {
                        int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Notify.Welcome);
                        switch (index)
                        {
                            case 1:
                                SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_welcome_1);
                                player1.Play();
                                player1.DisposePlayer(Resource.stanleyparable_welcome_1);
                                break;
                            case 2:
                                SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_welcome_2);
                                player2.Play();
                                player2.DisposePlayer(Resource.stanleyparable_welcome_2);
                                break;
                            case 3:
                                SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_welcome_3);
                                player3.Play();
                                player3.DisposePlayer(Resource.stanleyparable_welcome_3);
                                break;
                            case 4:
                                SoundPlayer player4 = new SoundPlayer(Resource.stanleyparable_welcome_4);
                                player4.Play();
                                player4.DisposePlayer(Resource.stanleyparable_welcome_4);
                                break;
                            case 5:
                                SoundPlayer player5 = new SoundPlayer(Resource.stanleyparable_welcome_5);
                                player5.Play();
                                player5.DisposePlayer(Resource.stanleyparable_welcome_5);
                                break;
                            case 6:
                                SoundPlayer player6 = new SoundPlayer(Resource.stanleyparable_welcome_6);
                                player6.Play();
                                player6.DisposePlayer(Resource.stanleyparable_welcome_6);
                                break;
                            case 7:
                                SoundPlayer player7 = new SoundPlayer(Resource.stanleyparable_welcome_7);
                                player7.Play();
                                player7.DisposePlayer(Resource.stanleyparable_welcome_7);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            public static class _Structures
            {
                public static class Enemy
                {
                    public static void Top_T1()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Top_T1);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_T1()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Mid_T1);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_T1()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Bot_T1);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Top_T2()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Top_T2);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_T2()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Mid_T2);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_T2()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Bot_T2);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Top_T3()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Top_T3);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_top_tower_enemy_2);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_T3()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Mid_T3);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_mid_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_mid_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_T3()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Enemy.Bot_T3);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_1);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_1);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_2);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_2);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_structures_bot_tower_enemy_3);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_structures_bot_tower_enemy_3);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Top_Inhib()
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(Resource.stanleyparable_structures_top_inhib_enemy);
                            player.Play();
                            player.DisposePlayer(Resource.stanleyparable_structures_top_inhib_enemy);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_Inhib()
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(Resource.stanleyparable_structures_mid_inhib_enemy);
                            player.Play();
                            player.DisposePlayer(Resource.stanleyparable_structures_mid_inhib_enemy);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_Inhib()
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(Resource.glados_structures_bot_inhib_enemy);
                            player.Play();
                            player.DisposePlayer(Resource.glados_structures_bot_inhib_enemy);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Nexus_Tower()
                    {
                        try
                        {

                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }

                public static class Ally
                {
                    public static void Top_T1()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Top_T1);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_T1()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Mid_T1);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_T1()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Bot_T1);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Top_T2()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Top_T2);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_T2()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Mid_T2);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_T2()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Bot_T2);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Top_T3()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Top_T3);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_T3()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Mid_T3);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_T3()
                    {
                        try
                        {
                            int index = Manage.Rand.Next(1, Manage.Sounds.StanleyParable.Structures.Ally.Bot_T3);
                            switch (index)
                            {
                                case 1:
                                    SoundPlayer player1 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player1.Play();
                                    player1.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 2:
                                    SoundPlayer player2 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player2.Play();
                                    player2.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                                case 3:
                                    SoundPlayer player3 = new SoundPlayer(Resource.stanleyparable_notify_victory);
                                    player3.Play();
                                    player3.DisposePlayer(Resource.stanleyparable_notify_victory);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Top_Inhib()
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(Resource.stanleyparable_structures_top_inhib);
                            player.Play();
                            player.DisposePlayer(Resource.stanleyparable_structures_top_inhib);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Mid_Inhib()
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(Resource.stanleyparable_structures_mid_inhib);
                            player.Play();
                            player.DisposePlayer(Resource.stanleyparable_structures_mid_inhib);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Bot_Inhib()
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(Resource.stanleyparable_structures_bot_inhib);
                            player.Play();
                            player.DisposePlayer(Resource.stanleyparable_structures_bot_inhib);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    public static void Nexus_Tower()
                    {
                        try
                        {

                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }
            }
        }
    }
}
