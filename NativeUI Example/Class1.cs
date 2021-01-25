using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;
using System.IO;
using System.Drawing.Printing;
using GTA.NaturalMotion;
using System.Media;

namespace NativeUI_Example
{
    public class Class1 : Script
    {
        //Add submenus here:
        MenuPool modMenuPool;
        UIMenu mainMenu;
        UIMenu playerMenu;
        UIMenu weaponsMenu;
        UIMenu vehicleMenu;
        UIMenu HealthMenu;
        UIMenu WantedMenu;
        UIMenu MoveMentMenu;
        UIMenu VehicleSpawnerMenu;
        UIMenu GiveWeaponsMenu;

        UIMenuItem resetWantedLevel;
        UIMenuItem PlayerHealth;

        public Class1()
        {
            Setup();

            Tick += OnTick;
            KeyDown += OnKeyDown;

            string PlayerName = GTA.Game.Player.Name;

            if (true)
            {
                GTA.UI.Notification.Show("~g~Welcome ~r~" + PlayerName + "~g~!"); //This adds a welcome notification when the game loads.
            }
            if (true)
            {
                GTA.UI.Notification.Show("~r~Example ~g~1.0"); //This adds the second notification when the game starts.
            }
        }

        void Setup()
        {
            modMenuPool = new MenuPool();
            mainMenu = new UIMenu("~r~Example", "~r~V. 1.0                  Made by: The_MZ"); /*You NEED to adjust the blank spaces to fit with your name!!!
                                                                                               * If it's 2 characters longer than remove 2 blank spaces!!!
                                                                                               * Else it wan't work!*/
            modMenuPool.Add(mainMenu);

            playerMenu = modMenuPool.AddSubMenu(mainMenu, "Self Menu ~r~>", "                             ~r~1/3");
            weaponsMenu = modMenuPool.AddSubMenu(mainMenu, "Weapons Menu ~r~>", "                             ~r~2/3");
            vehicleMenu = modMenuPool.AddSubMenu(mainMenu, "Vehicle Menu ~r~>", "                             ~r~3/3");

            SetupPlayerFunctions();
            SetupWeaponsFunctions();
            SetupVehicleFunctions();
        }

        void MoveMentOptions()
        {
            FastRun();
            NoRagdollOn();
        }

        void HealthOptions()
        {
            Invincible();
            HealPlayer();
        }

        void WantedOptions()
        {
            NeverWantedLevel();
            ResetWantedLevel();
            MaxWantedLevel();

        }

        void SetupPlayerFunctions()
        {
            HealthMenu = modMenuPool.AddSubMenu(playerMenu, "Health Options ~r~>", "                             ~r~1/5");
            WantedMenu = modMenuPool.AddSubMenu(playerMenu, "Wanted Options ~r~>", "                             ~r~2/5");
            MoveMentMenu = modMenuPool.AddSubMenu(playerMenu, "Movement Options ~r~>", "                             ~r~3/5");

            HealthOptions();
            WantedOptions();
            MoveMentOptions();
        }

        void SetupWeaponsFunctions()
        {
            WeaponSelectorMenu = modMenuPool.AddSubMenu(weaponsMenu, "Weapon selector ~r~>", "                             ~r~1/5");
            GiveWeaponsMenu = modMenuPool.AddSubMenu(weaponsMenu, "Weapon giver ~r~>", "                             ~r~2/5");
            WeaponSelectorSubMenu();
            WeaponsGiverSubMenu();
            AmmoClip();
            GetAllWeapons();
            RemoveAllWeapons();
        }

        void WeaponSelectorSubMenu()
        {
        MeleeSelectorMenu();
        HandgunSelectorMenu();
        HandgunMK2SelectorMenu();
        }

        void WeaponsGiverSubMenu()
        {
            GetAllMeleeWeapons();
            GetAllHandguns();
            GetAllMK2Handguns();
        }

        void SetupVehicleFunctions()
        {
            VehicleSpawnerMenu = modMenuPool.AddSubMenu(vehicleMenu, "Vehicle Spawner ~r~>", "                             ~r~1/1");
            SpawnVehicle();
        }

        bool FastRunOn = false;
        void FastRun()
        {
            {
                UIMenuItem RunOn = new UIMenuItem("Fast run: ~r~OFF", "                             ~r~1/3");

                MoveMentMenu.AddItem(RunOn);

                MoveMentMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == RunOn)
                    {
                        FastRunOn = !FastRunOn;

                        if (FastRunOn)
                        {
                            RunOn.Text = "Fast run: ~g~ON";
                        }
                        else
                        {
                            RunOn.Text = "Fast run: ~r~OFF";
                        }
                    }
                };
            }
        }

        void SpawnVehicle()
        {
            SpawnBoats();
            SpawnCommercial();
        }
        void SpawnBoats()
        {
            UIMenu submenu = modMenuPool.AddSubMenu(VehicleSpawnerMenu, "Boats ~r~>", "                             ~r~1/10");

            UIMenuItem AvisaMenuItem = new UIMenuItem("Avisa", "                             ~r~1/25");
            submenu.AddItem(AvisaMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == AvisaMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Avisa = World.CreateVehicle(VehicleHash.Avisa, gamePed.Position, gamePed.Heading);
                    Avisa.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Avisa, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Avisa spawned");
                }
            };

            UIMenuItem DinghyMenuItem = new UIMenuItem("Dinghy", "                             ~r~2/25");
            submenu.AddItem(DinghyMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == DinghyMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Dinghy = World.CreateVehicle(VehicleHash.Dinghy, gamePed.Position, gamePed.Heading);
                    Dinghy.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Dinghy, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Dinghy spawned");
                }
            };
            UIMenuItem Dinghy2MenuItem = new UIMenuItem("Dinghy2", "                             ~r~3/25");
            submenu.AddItem(Dinghy2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Dinghy2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Dinghy2 = World.CreateVehicle(VehicleHash.Dinghy2, gamePed.Position, gamePed.Heading);
                    Dinghy2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Dinghy2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Dinghy2 spawned");
                }
            };
            UIMenuItem Dinghy3MenuItem = new UIMenuItem("Dinghy3", "                             ~r~4/25");
            submenu.AddItem(Dinghy3MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Dinghy3MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Dinghy3 = World.CreateVehicle(VehicleHash.Dinghy3, gamePed.Position, gamePed.Heading);
                    Dinghy3.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Dinghy3, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Dinghy3 spawned");
                }
            };
            UIMenuItem Dinghy4MenuItem = new UIMenuItem("Dinghy4", "                             ~r~5/25");
            submenu.AddItem(Dinghy4MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Dinghy4MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Dinghy = World.CreateVehicle(VehicleHash.Dinghy4, gamePed.Position, gamePed.Heading);
                    Dinghy.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Dinghy, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Dinghy4 spawned");
                }
            };
            UIMenuItem Dinghy5MenuItem = new UIMenuItem("Dinghy5", "                             ~r~6/25");
            submenu.AddItem(Dinghy5MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Dinghy5MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Dinghy5 = World.CreateVehicle(VehicleHash.Dinghy5, gamePed.Position, gamePed.Heading);
                    Dinghy5.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Dinghy5, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Dinghy5 spawned");
                }
            };
            UIMenuItem JetMaxMenuItem = new UIMenuItem("Jetmax", "                             ~r~7/25");
            submenu.AddItem(JetMaxMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == JetMaxMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Jetmax = World.CreateVehicle(VehicleHash.Jetmax, gamePed.Position, gamePed.Heading);
                    Jetmax.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Jetmax, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Jetmax spawned");
                }
            };
            UIMenuItem KosatkaMenuItem = new UIMenuItem("Kosatka", "                             ~r~8/25");
            submenu.AddItem(KosatkaMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == KosatkaMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Kosatka = World.CreateVehicle(VehicleHash.Kosatka, gamePed.Position, gamePed.Heading);
                    Kosatka.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Kosatka, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Kosatka spawned");
                }
            };
            UIMenuItem LongfinMenuItem = new UIMenuItem("Longfin", "                             ~r~9/25");
            submenu.AddItem(LongfinMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == LongfinMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Longfin = World.CreateVehicle(VehicleHash.Longfin, gamePed.Position, gamePed.Heading);
                    Longfin.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Longfin, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Longfin spawned");
                }
            };
            UIMenuItem MarquisMenuItem = new UIMenuItem("Marquis", "                             ~r~10/25");
            submenu.AddItem(MarquisMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == MarquisMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Marquis = World.CreateVehicle(VehicleHash.Marquis, gamePed.Position, gamePed.Heading);
                    Marquis.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Marquis, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Marquis spawned");
                }
            };
            UIMenuItem PatrolBoatMenuItem = new UIMenuItem("PatrolBoat", "                             ~r~11/25");
            submenu.AddItem(PatrolBoatMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == PatrolBoatMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle PatrolBoat = World.CreateVehicle(VehicleHash.PatrolBoat, gamePed.Position, gamePed.Heading);
                    PatrolBoat.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(PatrolBoat, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~PatrolBoat spawned");
                }
            };
            UIMenuItem SeasharkMenuItem = new UIMenuItem("Seashark", "                             ~r~12/25");
            submenu.AddItem(SeasharkMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == SeasharkMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Seashark = World.CreateVehicle(VehicleHash.Seashark, gamePed.Position, gamePed.Heading);
                    Seashark.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Seashark, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Seashark spawned");
                }
            };
            UIMenuItem Seashark2MenuItem = new UIMenuItem("Seashark2", "                             ~r~13/25");
            submenu.AddItem(Seashark2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Seashark2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Seashark2 = World.CreateVehicle(VehicleHash.Seashark2, gamePed.Position, gamePed.Heading);
                    Seashark2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Seashark2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Seashark2 spawned");
                }
            };
            UIMenuItem Seashark3MenuItem = new UIMenuItem("Seashark3", "                             ~r~14/25");
            submenu.AddItem(Seashark3MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Seashark3MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Seashark3 = World.CreateVehicle(VehicleHash.Seashark3, gamePed.Position, gamePed.Heading);
                    Seashark3.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Seashark3, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Seashark3 spawned");
                }
            };
            UIMenuItem SpeederMenuItem = new UIMenuItem("Speeder", "                             ~r~15/25");
            submenu.AddItem(SpeederMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == SpeederMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Speeder = World.CreateVehicle(VehicleHash.Speeder, gamePed.Position, gamePed.Heading);
                    Speeder.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Speeder, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Speeder spawned");
                }
            };
            UIMenuItem Speeder2MenuItem = new UIMenuItem("Speeder2", "                             ~r~16/25");
            submenu.AddItem(Speeder2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Speeder2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Speeder2 = World.CreateVehicle(VehicleHash.Speeder2, gamePed.Position, gamePed.Heading);
                    Speeder2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Speeder2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Speeder2 spawned");
                }
            };
            UIMenuItem SqualoMenuItem = new UIMenuItem("Squalo", "                             ~r~17/25");
            submenu.AddItem(SqualoMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == SqualoMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Squalo = World.CreateVehicle(VehicleHash.Squalo, gamePed.Position, gamePed.Heading);
                    Squalo.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Squalo, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Squalo spawned");
                }
            };
            UIMenuItem SubmersibleMenuItem = new UIMenuItem("Submersible", "                             ~r~18/25");
            submenu.AddItem(SubmersibleMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == SubmersibleMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Submersible = World.CreateVehicle(VehicleHash.Submersible, gamePed.Position, gamePed.Heading);
                    Submersible.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Submersible, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Submersible spawned");
                }
            };
            UIMenuItem Submersible2MenuItem = new UIMenuItem("Submersible2", "                             ~r~19/25");
            submenu.AddItem(Submersible2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Submersible2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Submersible2 = World.CreateVehicle(VehicleHash.Submersible2, gamePed.Position, gamePed.Heading);
                    Submersible2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Submersible2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Submersible2 spawned");
                }
            };
            UIMenuItem SuntrapMenuItem = new UIMenuItem("Suntrap", "                             ~r~20/25");
            submenu.AddItem(SuntrapMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == SuntrapMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Suntrap = World.CreateVehicle(VehicleHash.Suntrap, gamePed.Position, gamePed.Heading);
                    Suntrap.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Suntrap, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Suntrap spawned");
                }
            };
            UIMenuItem ToroMenuItem = new UIMenuItem("Toro", "                             ~r~21/25");
            submenu.AddItem(ToroMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == ToroMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Toro = World.CreateVehicle(VehicleHash.Toro, gamePed.Position, gamePed.Heading);
                    Toro.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Toro, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Toro spawned");
                }
            };
            UIMenuItem Toro2MenuItem = new UIMenuItem("Toro2", "                             ~r~22/25");
            submenu.AddItem(Toro2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Toro2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Toro2 = World.CreateVehicle(VehicleHash.Toro2, gamePed.Position, gamePed.Heading);
                    Toro2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Toro2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Toro2 spawned");
                }
            };
            UIMenuItem TropicMenuItem = new UIMenuItem("Tropic", "                             ~r~23/25");
            submenu.AddItem(TropicMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == TropicMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Tropic = World.CreateVehicle(VehicleHash.Tropic, gamePed.Position, gamePed.Heading);
                    Tropic.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Tropic, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Tropic spawned");
                }
            };
            UIMenuItem Tropic2MenuItem = new UIMenuItem("Tropic2", "                             ~r~24/25");
            submenu.AddItem(Tropic2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Tropic2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Tropic2 = World.CreateVehicle(VehicleHash.Tropic2, gamePed.Position, gamePed.Heading);
                    Tropic2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Tropic2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Tropic2 spawned");
                }
            };
            UIMenuItem TugMenuItem = new UIMenuItem("Tug", "                             ~r~25/25");
            submenu.AddItem(TugMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == TugMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Tug = World.CreateVehicle(VehicleHash.Tug, gamePed.Position, gamePed.Heading);
                    Tug.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Tug, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Tug spawned");
                }
            };
        }

        void SpawnCommercial()
        {
            UIMenu submenu = modMenuPool.AddSubMenu(VehicleSpawnerMenu, "Commercials ~r~>", "                             ~r~2/10");

            UIMenuItem BensonMenuItem = new UIMenuItem("Benson", "                             ~r~1/18");
            submenu.AddItem(BensonMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == BensonMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Benson = World.CreateVehicle(VehicleHash.Benson, gamePed.Position, gamePed.Heading);
                    Benson.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Benson, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Benson spawned");
                }
            };
            UIMenuItem BiffMenuItem = new UIMenuItem("Biff", "                             ~r~2/18");
            submenu.AddItem(BiffMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == BiffMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Biff = World.CreateVehicle(VehicleHash.Biff, gamePed.Position, gamePed.Heading);
                    Biff.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Biff, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Biff spawned");
                }
            };
            UIMenuItem CerberusMenuItem = new UIMenuItem("Cerberus", "                             ~r~3/18");
            submenu.AddItem(CerberusMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == CerberusMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Cerberus = World.CreateVehicle(VehicleHash.Cerberus, gamePed.Position, gamePed.Heading);
                    Cerberus.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Cerberus, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Cerberus spawned");
                }
            };
            UIMenuItem Cerberus2MenuItem = new UIMenuItem("Cerberus2", "                             ~r~4/18");
            submenu.AddItem(Cerberus2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Cerberus2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Cerberus2 = World.CreateVehicle(VehicleHash.Cerberus2, gamePed.Position, gamePed.Heading);
                    Cerberus2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Cerberus2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Cerberus2 spawned");
                }
            };
            UIMenuItem Cerberus3MenuItem = new UIMenuItem("Cerberus3", "                             ~r~5/18");
            submenu.AddItem(Cerberus3MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Cerberus3MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Cerberus3 = World.CreateVehicle(VehicleHash.Cerberus3, gamePed.Position, gamePed.Heading);
                    Cerberus3.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Cerberus3, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Cerberus3 spawned");
                }
            };
            UIMenuItem HaulerMenuItem = new UIMenuItem("Hauler", "                             ~r~6/18");
            submenu.AddItem(HaulerMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == HaulerMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Hauler = World.CreateVehicle(VehicleHash.Hauler, gamePed.Position, gamePed.Heading);
                    Hauler.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Hauler, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Hauler spawned");
                }
            };
            UIMenuItem Hauler2MenuItem = new UIMenuItem("Hauler2", "                             ~r~6/18");
            submenu.AddItem(Hauler2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Hauler2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Hauler2 = World.CreateVehicle(VehicleHash.Hauler2, gamePed.Position, gamePed.Heading);
                    Hauler2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Hauler2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Hauler2 spawned");
                }
            };
            UIMenuItem MuleMenuItem = new UIMenuItem("Mule", "                             ~r~7/18");
            submenu.AddItem(MuleMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == MuleMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Mule = World.CreateVehicle(VehicleHash.Mule, gamePed.Position, gamePed.Heading);
                    Mule.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Mule, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Mule spawned");
                }
            };
            UIMenuItem Mule2MenuItem = new UIMenuItem("Mule2", "                             ~r~8/18");
            submenu.AddItem(Mule2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Mule2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Mule2 = World.CreateVehicle(VehicleHash.Mule2, gamePed.Position, gamePed.Heading);
                    Mule2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Mule2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Mule2 spawned");
                }
            };
            UIMenuItem Mule3MenuItem = new UIMenuItem("Mule3", "                             ~r~9/18");
            submenu.AddItem(Mule3MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Mule3MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Mule3 = World.CreateVehicle(VehicleHash.Mule3, gamePed.Position, gamePed.Heading);
                    Mule3.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Mule3, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Mule3 spawned");
                }
            };
            UIMenuItem Mule4MenuItem = new UIMenuItem("Mule4", "                             ~r~10/18");
            submenu.AddItem(Mule4MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Mule4MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Mule4 = World.CreateVehicle(VehicleHash.Mule4, gamePed.Position, gamePed.Heading);
                    Mule4.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Mule4, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Mule4 spawned");
                }
            };
            UIMenuItem PhantomMenuItem = new UIMenuItem("Phantom", "                             ~r~11/18");
            submenu.AddItem(PhantomMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == PhantomMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Phantom = World.CreateVehicle(VehicleHash.Phantom, gamePed.Position, gamePed.Heading);
                    Phantom.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Phantom, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Phantom spawned");
                }
            };
            UIMenuItem Phantom2MenuItem = new UIMenuItem("Phantom2", "                             ~r~12/18");
            submenu.AddItem(Phantom2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Phantom2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Phantom2 = World.CreateVehicle(VehicleHash.Phantom2, gamePed.Position, gamePed.Heading);
                    Phantom2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Phantom2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Phantom2 spawned");
                }
            };
            UIMenuItem Phantom3MenuItem = new UIMenuItem("Phantom3", "                             ~r~13/18");
            submenu.AddItem(Phantom3MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Phantom3MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Phantom3 = World.CreateVehicle(VehicleHash.Phantom3, gamePed.Position, gamePed.Heading);
                    Phantom3.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Phantom3, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Phantom3 spawned");
                }
            };
            UIMenuItem PounderMenuItem = new UIMenuItem("Pounder", "                             ~r~14/18");
            submenu.AddItem(PounderMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == PounderMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Pounder = World.CreateVehicle(VehicleHash.Pounder, gamePed.Position, gamePed.Heading);
                    Pounder.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Pounder, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Pounder spawned");
                }
            };
            UIMenuItem Pounder2MenuItem = new UIMenuItem("Pounder2", "                             ~r~15/18");
            submenu.AddItem(Pounder2MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Pounder2MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Pounder2 = World.CreateVehicle(VehicleHash.Pounder2, gamePed.Position, gamePed.Heading);
                    Pounder2.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Pounder2, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Pounder2 spawned");
                }
            };
            UIMenuItem StockadeMenuItem = new UIMenuItem("Stockade", "                             ~r~16/18");
            submenu.AddItem(StockadeMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == StockadeMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Stockade = World.CreateVehicle(VehicleHash.Stockade, gamePed.Position, gamePed.Heading);
                    Stockade.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Stockade, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Stockade spawned");
                }
            };
            UIMenuItem Stockade3MenuItem = new UIMenuItem("Stockade3", "                             ~r~17/18");
            submenu.AddItem(Stockade3MenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Stockade3MenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Stockade3 = World.CreateVehicle(VehicleHash.Stockade3, gamePed.Position, gamePed.Heading);
                    Stockade3.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Stockade3, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Stockade3 spawned");
                }
            };
            UIMenuItem TerrorbyteMenuItem = new UIMenuItem("Terrorbyte", "                             ~r~18/18");
            submenu.AddItem(TerrorbyteMenuItem);

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == TerrorbyteMenuItem)
                {
                    Ped gamePed = Game.Player.Character;

                    Vehicle Terrorbyte = World.CreateVehicle(VehicleHash.Terrorbyte, gamePed.Position, gamePed.Heading);
                    Terrorbyte.PlaceOnGround();
                    gamePed.Task.WarpIntoVehicle(Terrorbyte, VehicleSeat.Driver);

                    GTA.UI.Screen.ShowSubtitle("~g~Terrorbyte spawned");
                }
            };
        }

        bool UnlimAmmoOn = false;
        void AmmoClip()
        {
            {
                UIMenuItem UnlimAmmo = new UIMenuItem("Unlimited Ammo: ~r~OFF", "                             ~r~3/5");

                weaponsMenu.AddItem(UnlimAmmo);

                weaponsMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == UnlimAmmo)
                    {
                        UnlimAmmoOn = !UnlimAmmoOn;

                        if (UnlimAmmoOn)
                        {
                            UnlimAmmo.Text = "Unlimited Ammo: ~g~ON";
                        }
                        else
                        {
                            UnlimAmmo.Text = "Unlimited Ammo: ~r~OFF";
                        }
                    }
                };
            }
        }

        void ResetWantedLevel()
        {
            resetWantedLevel = new UIMenuItem("Clear wanted level", "                             ~r~2/4");
            WantedMenu.AddItem(resetWantedLevel);

            WantedMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == resetWantedLevel)
                {
                    if (Game.Player.WantedLevel == 0)
                    {
                        GTA.UI.Screen.ShowSubtitle("~r~You don't have a wanted level");
                    }
                    Wait(1);
                    if (Game.Player.WantedLevel > 0)
                    {
                        Game.Player.WantedLevel = 0;
                        GTA.UI.Screen.ShowSubtitle("~g~Wanted level removed");
                    }
                }
            };
        }

        void MaxWantedLevel()
        {
            UIMenuItem maxWanted = new UIMenuItem("Get max wanted level", "                             ~r~3/8");
            WantedMenu.AddItem(maxWanted);

            WantedMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == maxWanted)
                {
                    if (Game.Player.WantedLevel == 5)
                    {
                        GTA.UI.Screen.ShowSubtitle("~r~You already have the max wanted level");
                    }
                    Wait(1);
                    if (Game.Player.WantedLevel < 5)
                    {
                        Game.Player.WantedLevel = 5;
                        GTA.UI.Screen.ShowSubtitle("~g~Max wanted level added");
                    }
                }
            };
        }

        void HealPlayer()
        {
            PlayerHealth = new UIMenuItem("Heal Player", "                             ~r~2/4");
            HealthMenu.AddItem(PlayerHealth);

            HealthMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == PlayerHealth)
                {

                    if (Game.Player.Character.Health == Game.Player.Character.MaxHealth)
                    {
                        GTA.UI.Screen.ShowSubtitle("~r~You already have max health");
                    }
                    Wait(1);
                    if (Game.Player.Character.Health != Game.Player.Character.MaxHealth)
                    {
                        Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                        GTA.UI.Screen.ShowSubtitle("~g~Max health added");
                    }
                }
            };
        }

        void GetAllMeleeWeapons()
        {
            UIMenuItem allMeleeWeapons = new UIMenuItem("Get all melee weapons", "                             ~r~1/16");
            GiveWeaponsMenu.AddItem(allMeleeWeapons);

            GiveWeaponsMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == allMeleeWeapons)
                {
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x92A27487, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x958A4A8F, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xF9E6AA4B, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x84BD7BFD, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xA2719263, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x8BB05FD7, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x440E4788, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x4E875F73, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xF9DCBF2D, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xD8DF3C3C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x99B507EA, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDD5DF8D9, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDFE37640, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x678B81B1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x19044EE0, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xCD274149, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x94117305, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x3813FC08, 9999, false, false);

                    GTA.UI.Screen.ShowSubtitle("~g~All melee weapons added");
                }
            };
        }

        void GetAllHandguns()
        {
            UIMenuItem allHandguns = new UIMenuItem("Get all handguns", "                             ~r~2/16");
            GiveWeaponsMenu.AddItem(allHandguns);

            GiveWeaponsMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == allHandguns)
                {
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x1B06D571, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x5EF9FEC4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x22D8FE39, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x3656C8C1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x99AEEB3B, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBFD21232, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xD205520E, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x83839C4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x47757124, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDC4DB296, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xC1B3C3D1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x97EA20B8, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xAF3696A1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x2B5EF5EC, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x917F6C8C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x57A4368C, 9999, false, false);

                    GTA.UI.Screen.ShowSubtitle("~g~All handguns added");
                }
            };
        }

        void GetAllMK2Handguns()
        {
            UIMenuItem allMK2Handguns = new UIMenuItem("Get all MKII handguns", "                             ~r~3/16");
            GiveWeaponsMenu.AddItem(allMK2Handguns);

            GiveWeaponsMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == allMK2Handguns)
                {
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBFE256D4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x88374054, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xCB96392F, 9999, false, false);

                    GTA.UI.Screen.ShowSubtitle("~g~All MKII handguns added");
                }
            };
        }

        void GetAllWeapons()
        {
            UIMenuItem allWeapons = new UIMenuItem("Get all weapons", "                             ~r~4/5");
            weaponsMenu.AddItem(allWeapons);

            weaponsMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == allWeapons)
                {
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x92A27487, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x958A4A8F, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xF9E6AA4B, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x84BD7BFD, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xA2719263, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x8BB05FD7, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x440E4788, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x4E875F73, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xF9DCBF2D, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xD8DF3C3C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x99B507EA, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDD5DF8D9, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDFE37640, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x678B81B1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x19044EE0, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xCD274149, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x94117305, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x3813FC08, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x1B06D571, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x5EF9FEC4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x22D8FE39, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x3656C8C1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x99AEEB3B, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBFD21232, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xD205520E, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x83839C4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x47757124, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDC4DB296, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xC1B3C3D1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x97EA20B8, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xAF3696A1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x2B5EF5EC, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x917F6C8C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x57A4368C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBFE256D4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x88374054, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xCB96392F, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x13532244, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x2BE6766B, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xEFE7E2DF, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x0A3D4D34, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDB1AA450, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBD248B55, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x476BF155, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x78A97CD0, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x1D073A89, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x7846A318, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xE284C527, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x9D61E50F, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xA89CB99E, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x3AABBBAA, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xEF951FBB, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x12E82D3D, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x5A96BA4, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x555AF99A, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBFEFFF6D, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x83BF0278, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xAF113F99, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xC0A3098D, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x7F229F94, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x624FE830, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x9D1F17E6, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x394F415C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xFAD1F1C9, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x969C3D67, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x84D6FAFD, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x9D07F764, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x7FD62962, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x61012683, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xDBBD7280, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x05FC3C11, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x0C472FE2, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xC734385A, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xA914799, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x6A6C02E0, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xB1CA77B1, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xA284510B, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x4DD2DC56, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x42BF8A85, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x7F7497E5, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x6D544C99, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x63AB0442, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x0781FE4A, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xB62D1F67, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x93E220BD, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xA0973D5E, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x24B17070, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x2C3731D9, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xAB564B93, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x787F0BB, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBA45E8B8, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x23C9F95C, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xFDBC8A50, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x497FACC3, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x34A67B97, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xFBAB5776, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0x060EC506, 9999, false, false);
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, 0xBA536372, 9999, false, false);

                    GTA.UI.Screen.ShowSubtitle("~g~All weapons added");
                }
            };
        }

        void RemoveAllWeapons()
        {
            UIMenuItem removeButton = new UIMenuItem("Remove all weapons", "                             ~r~5/5");

            weaponsMenu.AddItem(removeButton);
            weaponsMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == removeButton)
                {
                    Function.Call(Hash.REMOVE_ALL_PED_WEAPONS, Game.Player.Character);
                    GTA.UI.Screen.ShowSubtitle("~g~All weapons removed");
                }
            };
        }

        public enum MeleeHash : uint
        {
            Antique_Cavalry_Dagger = 0x92A27487,
            Baseball_Bat = 0x958A4A8F,
            Broken_Bottle = 0xF9E6AA4B,
            Crowbar = 0x84BD7BFD,
            Flashlight = 0x8BB05FD7,
            Golf_Club = 0x440E4788,
            Hammer = 0x4E875F73,
            Hatchet = 0xF9DCBF2D,
            Brass_Knuckles = 0xD8DF3C3C,
            Knife = 0x99B507EA,
            Machete = 0xDD5DF8D9,
            Switchblade = 0xDFE37640,
            Nightstick = 0x678B81B1,
            Pipe_Wrench = 0x19044EE0,
            Battle_Axe = 0xCD274149,
            Pool_Cue = 0x94117305,
            Stone_Hatchet = 0x3813FC08
        }
        public enum HandgunHash : uint
        {
            Pistol = 0x1B06D571,
            Combat_Pistol = 0x5EF9FEC4,
            AP_Pistol = 0x22D8FE39,
            Stun_Gun = 0x3656C8C1,
            Pistol_50 = 0x99AEEB3B,
            SNS_Pistol = 0xBFD21232,
            Heavy_Pistol = 0xD205520E,
            Vintage_Pistol = 0x83839C4,
            Flare_Gun = 0x47757124,
            Marksman_Pistol = 0xDC4DB296,
            Heavy_Revolver = 0xC1B3C3D1,
            Double_Action_Revolver = 0x97EA20B8,
            Up_n_Atomizer = 0xAF3696A1,
            Ceramic_Pistol = 0x2B5EF5EC,
            Navy_Revolver = 0x917F6C8C,
            Perico_Pistol = 0x57A4368C
        }
        public enum HandgunMK2Hash : uint
        {
            Pistol_MK_II = 0xBFE256D4,
            SNS_Pistol_MK_II = 0x88374054,
            Heavy_Revolver_MK_II = 0xCB96392F
        }

        void MeleeSelectorMenu()
        {
            List<dynamic> listOfWeapons = new List<dynamic>();
            MeleeHash[] allMeleeHashes = (MeleeHash[])Enum.GetValues(typeof(MeleeHash));
            for (int i = 0; i < allMeleeHashes.Length; i++)
            {
                listOfWeapons.Add(allMeleeHashes[i]);
            }

            UIMenuListItem list = new UIMenuListItem("Melee: ", listOfWeapons, 0);
            WeaponSelectorMenu.AddItem(list);

            UIMenuItem getWeapon = new UIMenuItem("Get melee weapon", "                             ~r~1/16");
            WeaponSelectorMenu.AddItem(getWeapon);

            WeaponSelectorMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == getWeapon)
                {
                    int listIndex = list.Index;
                    MeleeHash currentHash = allMeleeHashes[listIndex];
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, currentHash, 9999, false, false);
                    GTA.UI.Screen.ShowSubtitle("~g~" + currentHash + " added");
                }
            };
        }
        void HandgunSelectorMenu()
        {
            List<dynamic> listOfWeapons = new List<dynamic>();
            HandgunHash[] allHandgunHashes = (HandgunHash[])Enum.GetValues(typeof(HandgunHash));
            for (int i = 0; i < allHandgunHashes.Length; i++)
            {
                listOfWeapons.Add(allHandgunHashes[i]);
            }

            UIMenuListItem list = new UIMenuListItem("Handgun: ", listOfWeapons, 0);
            WeaponSelectorMenu.AddItem(list);

            UIMenuItem getWeapon = new UIMenuItem("Get handgun", "                             ~r~2/16");
            WeaponSelectorMenu.AddItem(getWeapon);

            WeaponSelectorMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == getWeapon)
                {
                    int listIndex = list.Index;
                    HandgunHash currentHash = allHandgunHashes[listIndex];
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, currentHash, 9999, false, false);
                    GTA.UI.Screen.ShowSubtitle("~g~" + currentHash +" added");
                }
            };
        }
        void HandgunMK2SelectorMenu()
        {
            List<dynamic> listOfWeapons = new List<dynamic>();
            HandgunMK2Hash[] allHandgunMK2Hashes = (HandgunMK2Hash[])Enum.GetValues(typeof(HandgunMK2Hash));
            for (int i = 0; i < allHandgunMK2Hashes.Length; i++)
            {
                listOfWeapons.Add(allHandgunMK2Hashes[i]);
            }

            UIMenuListItem list = new UIMenuListItem("MKII Handgun: ", listOfWeapons, 0);
            WeaponSelectorMenu.AddItem(list);

            UIMenuItem getWeapon = new UIMenuItem("Get MKII Handgun", "                             ~r~3/16");
            WeaponSelectorMenu.AddItem(getWeapon);

            WeaponSelectorMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == getWeapon)
                {
                    int listIndex = list.Index;
                    HandgunMK2Hash currentHash = allHandgunMK2Hashes[listIndex];
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, currentHash, 9999, false, false);
                    GTA.UI.Screen.ShowSubtitle("~g~" + currentHash + " added");
                }
            };
        }

        bool InvincibleOn = false;
        void Invincible()
        {
            UIMenuItem Invincibility = new UIMenuItem("Invincible: ~r~OFF", "                             ~r~1/4");

            HealthMenu.AddItem(Invincibility);

            HealthMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Invincibility)
                {
                    InvincibleOn = !InvincibleOn;

                    if (InvincibleOn)
                    {
                        Invincibility.Text = "Invincible: ~g~ON";
                    }
                    else
                    {
                        Invincibility.Text = "Invincible: ~r~OFF";
                    }
                }
            };
        }

        bool neverWantedLeveOn = false;
        void NeverWantedLevel()
        {
            UIMenuItem neverWanted = new UIMenuItem("Never wanted: ~r~OFF", "                             ~r~1/4");

            WantedMenu.AddItem(neverWanted);

            WantedMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == neverWanted)
                {
                    neverWantedLeveOn = !neverWantedLeveOn;

                    if (neverWantedLeveOn)
                        neverWanted.Text = "Never wanted: ~g~ON";
                    else
                        neverWanted.Text = "Never wanted: ~r~OFF";
                }
            };
        }

        bool noRagdollOn = false;
        void NoRagdollOn()
        {
            UIMenuItem Ragdoll = new UIMenuItem("No Ragdoll: ~r~OFF", "                             ~r~3/3");

            MoveMentMenu.AddItem(Ragdoll);

            MoveMentMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == Ragdoll)
                {
                    noRagdollOn = !noRagdollOn;

                    if (noRagdollOn)
                    {
                        Ragdoll.Text = "No Ragdoll: ~g~ON";
                    }
                    else
                    {
                        Ragdoll.Text = "No Ragdoll: ~r~OFF";
                    }
                }
            };
        }

        void OnTick(object sender, EventArgs e) //Add bool options here. Look at the examples and understand them.
        {
            if (modMenuPool != null)
                modMenuPool.ProcessMenus();

            if (neverWantedLeveOn)
            {
                Game.Player.WantedLevel = 0;
                Game.Player.IgnoredByPolice = true;
                Function.Call(Hash.SET_MAX_WANTED_LEVEL, 0);
            }
            else
            {
                Game.Player.IgnoredByPolice = false;
                Function.Call(Hash.SET_MAX_WANTED_LEVEL, 5);
            }

            if (InvincibleOn)
            {
                Game.Player.Character.Health = Game.Player.Character.MaxHealth;
                Game.Player.Character.Armor = 100;
                Game.Player.Character.IsBulletProof = true;
                Game.Player.Character.IsFireProof = true;
                Game.Player.Character.IsInvincible = true;
                Game.Player.Character.IsExplosionProof = true;
                Function.Call(Hash.SET_ENTITY_PROOFS, Game.Player.Character, 1, 1, 1, 1, 1, 1, 1, 1);
            }
            else
            {
                Game.Player.Character.IsBulletProof = false;
                Game.Player.Character.IsFireProof = false;
                Game.Player.Character.IsInvincible = false;
                Game.Player.Character.IsExplosionProof = false;
                Function.Call(Hash.SET_ENTITY_PROOFS, Game.Player.Character, 0, 0, 0, 0, 0, 0, 0, 0);
            }

            if (noRagdollOn)
            {
                Game.Player.Character.CanRagdoll = false;
            }
            else
            {
                Game.Player.Character.CanRagdoll = true;
            }
            if (UnlimAmmoOn)
            {
                Game.Player.Character.Weapons.Current.InfiniteAmmoClip = true;
                Game.Player.Character.Weapons.Current.InfiniteAmmo = true;
            }
            else
            {
                Game.Player.Character.Weapons.Current.InfiniteAmmoClip = false;
                Game.Player.Character.Weapons.Current.InfiniteAmmo = false;
            }
            if (InvisibleOn)
            {
                Game.Player.Character.IsVisible = false;
            }
            else
            {
                Game.Player.Character.IsVisible = true;
            }
            if (FastRunOn)
            {
                if (Game.Player.Character.IsSprinting && Game.Player.Character.IsRagdoll == false && Game.Player.Character.IsAiming == false && Game.Player.Character.IsRunning == false && Game.Player.Character.IsSwimming == false && Game.Player.Character.IsSwimmingUnderWater == false && Game.Player.Character.IsAimingFromCover == false && Game.Player.Character.IsInAir == false && Game.Player.Character.IsInCombat == false && Game.Player.Character.IsInCoverFacingLeft == false && Game.Player.Character.IsInStealthMode == false && Game.Player.Character.IsInWater == false && Game.Player.Character.IsJacking == false && Game.Player.Character.IsJumpingOutOfVehicle == false && Game.Player.Character.IsClimbing == false && Game.Player.Character.IsFalling == false && Game.Player.Character.IsGettingUp == false && Game.Player.Character.IsGoingIntoCover == false && Game.Player.Character.IsGettingIntoVehicle == false)
                {
                    Function.Call(Hash.SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER, Game.Player, 1.49);
                }
            }
            else
            {
                Function.Call(Hash.SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER, Game.Player, 1.00);
            }
        }

        void OnKeyDown(object sender, KeyEventArgs e) //This will open the menu if a certain key is pressed.
        {
            if (e.KeyCode == Keys.F5 /*This is key used for opening the menu, you can change it by any key you want.*/ && !modMenuPool.IsAnyMenuOpen())
            {
                mainMenu.Visible = !mainMenu.Visible; //If you don't know what this does than leave it alone!!! But it's obvious what it does...
            }
        }
    }
}