using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Linq;
using Terraria.GameContent.Events;


//Before anything, lets list some songs here.

//Biome Themes//
/*
   Overworld/Forest Themes: Glorious Morning 2, Glorious Morning



*/
//Boss Themes//
/*
    Eye of Cthulhu: Eye of Cthulhu


 */


namespace MusicMod
{
    public class MusicMod : Mod
    {
        string Nexterpack = "Sounds/Music/TheNexterPack/";
        double prev;
        int overy;

        public void Talk(string message)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                string text = Language.GetTextValue(message, "Bob", message);
                Main.NewText(text, 150, 250, 150);
            }
            else
            {
                NetworkText text = NetworkText.FromKey("Mods.ExampleMod.NPCTalk", "Bob", message);
                NetMessage.BroadcastChatMessage(text, new Color(150, 250, 150));
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
         {
            

            if (Main.musicVolume != 0)
             {

                if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
                 {

                    bool invasion = false;

                    //Boss Themes
                    if (Main.npc.Any(n => n.active && n.boss))
                    {
                        //Pre-Hardmode//
                        if (NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/Moonbase");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/EyeofCthulhu");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.BrainofCthulhu))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/TheDarkestLord");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.EaterofWorldsBody) || NPC.AnyNPCs(NPCID.EaterofWorldsHead) || NPC.AnyNPCs(NPCID.EaterofWorldsTail))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/TheDarkestLord");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.SkeletronHead))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/Megalovania");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.QueenBee))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/TheGiantEnemySpider");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.WallofFlesh))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/Boss2Beta");
                            priority = MusicPriority.BossHigh;
                        }
                        //Mechanical Bosses//
                        if (NPC.AnyNPCs(NPCID.TheDestroyer) || NPC.AnyNPCs(NPCID.TheDestroyerBody) || NPC.AnyNPCs(NPCID.TheDestroyerTail))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/HighFructoseBacon");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.SkeletronPrime))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/CamiMegalovania");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.Spazmatism) || NPC.AnyNPCs(NPCID.Retinazer))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/Boss2");
                            priority = MusicPriority.BossHigh;
                        }
                        //Post Mechanical Bosses//
                        if (NPC.AnyNPCs(NPCID.Plantera))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/Plantera");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.Golem))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/TheAttackOfTheDeadMen");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.DukeFishron))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/Yggdrasil");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.CultistBoss))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/TheAttackOfTheDeadMen");
                            priority = MusicPriority.BossHigh;
                        }
                        if (NPC.AnyNPCs(NPCID.MoonLordCore))
                        {
                            music = this.GetSoundSlot(SoundType.Music, Nexterpack + "BossThemes/BrainPower");
                            priority = MusicPriority.BossHigh;
                        }
                    }
                    
                    else
                    {
                        //Special Themes (Invasions and such)

                        if (Main.bloodMoon) // Blood Moon
                        {
                            invasion = true;
                            int x = Main.rand.Next(3); //Determines what song plays for Overworld.
                            priority = MusicPriority.Event;

                            if (prev != 1)
                            {
                                if (x == 1)
                                {
                                    prev = 1;
                                    overy = 1;
                                }
                                if (x == 2)
                                {
                                    prev = 1;
                                    overy = 2;
                                }
                            }

                            if (overy == 1)
                            {
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/8BitsAreScary");
                            }
                            if (overy == 2)
                            {
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/PartyDontStop");
                            } 
                        }

                        if (Main.eclipse) // Solar Eclipse - Best song ngl
                        {
                            invasion = true;
                            priority = MusicPriority.Event;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/SkyIsFalling");
                        }

                        if (NPC.AnyNPCs(NPCID.GoblinPeon) || NPC.AnyNPCs(NPCID.GoblinArcher) || NPC.AnyNPCs(NPCID.GoblinSorcerer) || NPC.AnyNPCs(NPCID.GoblinWarrior) || NPC.AnyNPCs(NPCID.GoblinThief) || NPC.AnyNPCs(NPCID.GoblinSummoner)) // Goblin Invasion
                        {
                            invasion = true;
                            priority = MusicPriority.Event;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/GreatWar");

                        }

                        if (NPC.AnyNPCs(NPCID.PirateCaptain) || NPC.AnyNPCs(NPCID.Parrot) || NPC.AnyNPCs(NPCID.PirateCorsair) || NPC.AnyNPCs(NPCID.PirateCrossbower) || NPC.AnyNPCs(NPCID.PirateShip) || NPC.AnyNPCs(NPCID.PirateDeadeye) || NPC.AnyNPCs(NPCID.PirateDeckhand))
                        {
                            invasion = true;
                            priority = MusicPriority.Event;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/Bismarck");
                        }

                        if (Main.LocalPlayer.ZoneOldOneArmy) // Old one's army
                        {
                            invasion = true;
                            priority = MusicPriority.Event;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/TheLastStand");
                        }

                        if (Main.LocalPlayer.ZoneTowerNebula || Main.LocalPlayer.ZoneTowerSolar || Main.LocalPlayer.ZoneTowerStardust || Main.LocalPlayer.ZoneTowerVortex) // Pillars
                        {
                            invasion = true;
                            priority = MusicPriority.BossHigh;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/FieldsOfVerdun");
                        }
                        
                        if (Main.pumpkinMoon) // Pumpkin Moon
                        {
                            invasion = true;
                            priority = MusicPriority.Environment;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/PimpedOutGetaway");
                        }

                        //Biome Themes
                        if (Main.LocalPlayer.ZoneOverworldHeight && !Main.bloodMoon && !invasion) // The Overworld
                        {

                            if (Main.dayTime)
                            {
                                int x = Main.rand.Next(3); //Determines what song plays for Overworld.
                                priority = MusicPriority.BiomeLow;

                                if (prev != 1)
                                {
                                    if (x == 1)
                                    {
                                        prev = 1;
                                        overy = 1;
                                    }
                                    if (x == 2)
                                    {
                                        prev = 1;
                                        overy = 2;
                                    }
                                }

                                if (overy == 1)
                                {
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/GloriousMorning2");
                                }
                                if (overy == 2)
                                {
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/GloriousMorning");
                                }
                            }
                            else if (!Main.dayTime)
                            {
                                priority = MusicPriority.BiomeLow;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/IAmEnd");
                            }

                        }

                        if (Main.LocalPlayer.ZoneRain && !invasion) // Rain
                        {

                            priority = MusicPriority.Environment;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/Todayisfornothing");

                        }

                        if (Main.LocalPlayer.ZoneDungeon)
                        {
                            priority = MusicPriority.BiomeHigh;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/LockeAndLoad");
                        }

                        if (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneDungeon)
                        {
                            priority = MusicPriority.BiomeHigh;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/BreakTheRules");
                        }

                        if (Main.LocalPlayer.ZoneJungle && !invasion) // Jungle
                        {
                            priority = MusicPriority.BiomeMedium;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/JungleJapes");
                        }

                        if (Main.LocalPlayer.ZoneSnow && !invasion) // Snow
                        {
                            priority = MusicPriority.BiomeMedium;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/BlackIce");
                        }

                        if (Main.LocalPlayer.ZoneDesert || Main.LocalPlayer.ZoneUndergroundDesert) // Desert
                        {
                            if (Sandstorm.Happening && !Main.LocalPlayer.ZoneDirtLayerHeight) // Sandstorm
                            {
                                priority = MusicPriority.Environment;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/SevenPillarsOfWisdom");
                            }
                            else
                            {
                                priority = MusicPriority.BiomeLow;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/LifeIsStillAwesome");
                            } 

                        }

                        if (Main.LocalPlayer.ZoneCrimson && !invasion) // Crimson
                        {
                            if (Main.LocalPlayer.ZoneDesert)
                            {
                                if (Sandstorm.Happening && !Main.LocalPlayer.ZoneDirtLayerHeight) // Sandstorm
                                {
                                    priority = MusicPriority.Environment;
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/SevenPillarsOfWisdom");
                                }
                            }
                            else
                            {
                                priority = MusicPriority.BiomeHigh;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "CorruptionBiomes/Showdown");
                            }
                            
                        }

                        if (Main.LocalPlayer.ZoneHoly && Main.dayTime && !invasion) // Hallow
                        {
                            if (Main.LocalPlayer.ZoneDesert)
                            {
                                if (Sandstorm.Happening && !Main.LocalPlayer.ZoneDirtLayerHeight) // Sandstorm
                                {
                                    priority = MusicPriority.Environment;
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/SevenPillarsOfWisdom");
                                }
                            }
                            else
                            {
                                priority = MusicPriority.BiomeLow;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "CorruptionBiomes/Karma");
                            }
                        }

                        if (Main.LocalPlayer.ZoneCorrupt && !invasion) // Corruption
                        {
                            if (Main.LocalPlayer.ZoneDesert)
                            {
                                if (Sandstorm.Happening && !Main.LocalPlayer.ZoneDirtLayerHeight) // Sandstorm
                                {
                                    priority = MusicPriority.Environment;
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/SevenPillarsOfWisdom");
                                }
                            }
                            else
                            {
                                priority = MusicPriority.BiomeHigh;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "CorruptionBiomes/ChaozFantasy");
                            }
                        }
                        if (Main.LocalPlayer.ZoneBeach && !invasion) // Ocean
                        {
                            priority = MusicPriority.BiomeLow;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/DanceOfTheViolins");
                        }
                        if (Main.LocalPlayer.ZoneMeteor && !invasion) // Meteor
                        {
                            priority = MusicPriority.BiomeMedium;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldEventThemes/PartyDontStop");
                        }
                        if (Main.LocalPlayer.ZoneGlowshroom && !invasion) // Kaspland
                        {
                            priority = MusicPriority.BiomeHigh;
                            music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/DameDaNe");
                        }

                        if (Main.LocalPlayer.ZoneDirtLayerHeight || Main.LocalPlayer.ZoneRockLayerHeight && !invasion) // Underground
                        {
                            if (Main.LocalPlayer.ZoneCrimson)
                            {
                                priority = MusicPriority.BiomeHigh;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "CorruptionBiomes/DISGRACETOTHEUNIFORM");
                            }
                            else if (Main.LocalPlayer.ZoneCorrupt)
                            {
                                priority = MusicPriority.BiomeHigh;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "CorruptionBiomes/TheFallingMysts");
                            }
                            else if (Main.LocalPlayer.ZoneHoly)
                            {
                                priority = MusicPriority.BiomeLow;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "CorruptionBiomes/Samsara");
                            }
                            else if (Main.LocalPlayer.ZoneSnow)
                            {
                                priority = MusicPriority.BiomeMedium;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/Sphere");
                            }
                            else if (Main.LocalPlayer.ZoneDesert)
                            {
                                priority = MusicPriority.BiomeLow;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/LifeIsStillAwesome");
                            }
                            else if (Main.LocalPlayer.ZoneJungle)
                            {
                                priority = MusicPriority.BiomeMedium;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/JungleJapes");
                            }
                            else if (Main.LocalPlayer.ZoneGlowshroom)
                            {
                                priority = MusicPriority.BiomeHigh;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/ShootingAllStar");
                            }

                            else
                            {
                                int x = Main.rand.Next(3); //Determines what song plays for Underground.
                                if (prev != 7)
                                {

                                    if (x == 1)
                                    {
                                        prev = 7;
                                        overy = 1;
                                    }
                                    if (x == 2)
                                    {
                                        prev = 7;
                                        overy = 2;
                                    }
                                }
                                if (overy == 1)
                                {
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/DiggyDiggyHole");
                                }
                                if (overy == 2)
                                {
                                    music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/normalcrabrave");
                                }
                            }
                        }

                        if (Main.LocalPlayer.ZoneUnderworldHeight) // Underworld Themes
                        {
                            int x = Main.rand.Next(3); //Determines what song plays for Underworld.
                            if (prev != 8)
                            {

                                if (x == 1)
                                {
                                    prev = 8;
                                    overy = 1;
                                }
                                if (x == 2)
                                {
                                    prev = 8;
                                    overy = 2;
                                }
                            }
                            if (overy == 1)
                            {
                                priority = MusicPriority.Environment;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "HellThemes/BFGDivision");
                            }
                            if (overy == 2)
                            {
                                priority = MusicPriority.Environment;
                                music = GetSoundSlot(SoundType.Music, Nexterpack + "HellThemes/ThroughTheFireAndFlames");
                            }
                        }
                    }
                    if (Main.LocalPlayer.ZoneSkyHeight) // Space
                    {
                        priority = MusicPriority.Environment;
                        music = GetSoundSlot(SoundType.Music, Nexterpack + "OverworldThemes/ExoPlanet");
                    }
                }
             }
         }

    }
}
