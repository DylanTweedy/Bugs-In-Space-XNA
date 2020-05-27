using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class LoadPlayer
    {
        #region Variables

        SaveManager saveManager;
        public bool Return;
        public Vector2 Position1;
        public Vector2 Position2;
        int SaveFile;
        public string saveFileOutput;
        public bool CreateNew;
        public bool saveSelect;
        bool saveManagerInitialize;
        bool selected;
        TimeSpan MoveTime1;
        TimeSpan previousMoveTime1;
        TimeSpan MoveTime2;
        TimeSpan previousMoveTime2;
        TimeSpan MoveTime3;
        TimeSpan previousMoveTime3;
        TimeSpan SaveTime;
        TimeSpan previousSaveTime;
        bool CreatePlayer;
        SpriteFont font;
        TimeSpan Time;
        TimeSpan previousTime;
        KeyboardUpdate keyboardUpdate;
        public string playerName;
        public int controlSystem;
        public bool Exit;
        public bool Quit;
        public bool inGame;
        public string titleText;
        public int playerNumber;
        KeyboardUpdate Controls;
        bool NameAvailable;
        public int MainMenuState;
        public List<string> activePlayers;

        #endregion

        #region Player Variables

        public float playerAcceleration;
        public float playerBulletSpeed;
        public int playerShip;
        public int playerMaxBullets;
        public int playerDamage;
        public int playerRedValue;
        public int playerBlueValue;
        public int playerGreenValue;
        public float playerFireRate;
        public int playerCredits;
        public int playerLives;
        public int playerMaxHealth;
        public int playerMaxEnergy;

        public int iplayerAmmo;
        public int iplayerBulletSpeed;
        public int iplayerDamage;
        public int iplayerElectricProjectile;
        public int iplayerEnergy;
        public int iplayerEnergyProjectile;
        public int iplayerExplosiveProjectile;
        public int iplayerFireProjectile;
        public int iplayerFireRate;
        public int iplayerHealingSpecial;
        public int iplayerHealth;
        public int iplayerHealthProjectile;
        public int iplayerLaserProjectile;
        public int iplayerLaserSpecial;
        public int iplayerMoneySpecial;
        public int iplayerMovementSpeed;
        public int iplayerPoisonProjectile;
        public int iplayerShieldSpecial;
        public int iplayerSlowProjectile;
        public int iplayerTimeStopSpecial;

        public bool bplayerAutoFire;
        public bool bplayerTripleShot;
        public bool bplayerExtraLife1;
        public bool bplayerExtraLife2;
        public bool bplayerExtraLife3;
        public bool bplayerExtraLife4;
        public bool bplayerQuadShot;
        public bool bplayerQuintupleShot;
        public bool bplayerDoubleShot;

        public int playerLevel;
        public int playerDeathCount;
        public int playerTimePlayedHours;
        public int playerTimePlayedMinutes;
        public int playerTimePlayedSeconds;
        public int playerCreditsCollected;
        public int playerCreditsSpent;
        public int playerWeaponsCollected;
        public int playerPercentageComplete;
        public int playerBulletsFired;
        public int playerAccuracy;
        public int playerEnemiesKilled;
        public int playerEnemiesHit;
        public int playerMiniGamesPassed;
        public int playerUpgradesPurchased;
        public int playerPowerUpsCollected;
        public int playerLevelsCompleted;

        public bool playerAchievement1;
        public bool playerAchievement2;
        public bool playerAchievement3;
        public bool playerAchievement4;
        public bool playerAchievement5;
        public bool playerAchievement6;
        public bool playerAchievement7;
        public bool playerAchievement8;
        public bool playerAchievement9;
        public bool playerAchievement10;
        public bool playerAchievement11;
        public bool playerAchievement12;
        public bool playerAchievement13;
        public bool playerAchievement14;
        public bool playerAchievement15;
        public bool playerAchievement16;
        public bool playerAchievement17;
        public bool playerAchievement18;
        public bool playerAchievement19;
        public bool playerAchievement20;
        public bool playerAchievement21;
        public bool playerAchievement22;
        public bool playerAchievement23;
        public bool playerAchievement24;
        public bool playerAchievement25;
        public bool playerAchievement26;
        public bool playerAchievement27;
        public bool playerAchievement28;
        public bool playerAchievement29;
        public bool playerAchievement30;
        public bool playerAchievement31;
        public bool playerAchievement32;
        public bool playerAchievement33;
        public bool playerAchievement34;
        public bool playerAchievement35;
        public bool playerAchievement36;
        public bool playerAchievement37;
        public bool playerAchievement38;
        public bool playerAchievement39;
        public bool playerAchievement40;
        public bool playerAchievement41;
        public bool playerAchievement42;
        public bool playerAchievement43;
        public bool playerAchievement44;
        public bool playerAchievement45;
        public bool playerAchievement46;
        public bool playerAchievement47;
        public bool playerAchievement48;
        public bool playerAchievement49;
        public bool playerAchievement50;
        public int playerAchievementCount;
        public int playerSelectedWeapon1;
        public int playerSelectedWeapon2;
        public int playerSelectedWeapon3;
        public int playerSelectedWeapon4;
        public int playerSelectedWeapon5;
        public int playerSelectedSpecial;
        public int playerShipsUnlocked;
        public int playerXP;
        public int playerLevelNumber;

        #endregion

        public void Initialize(int ControlSystem, int PlayerNumber)
        {
            Return = false;
            SaveFile = 0;
            saveManagerInitialize = false;
            SaveTime = TimeSpan.FromSeconds(1f);
            previousSaveTime = TimeSpan.Zero;
            previousMoveTime1 = TimeSpan.Zero;
            MoveTime1 = TimeSpan.FromSeconds(1f);
            previousMoveTime2 = TimeSpan.Zero;
            MoveTime2 = TimeSpan.FromSeconds(1f);
            previousMoveTime3 = TimeSpan.Zero;
            MoveTime3 = TimeSpan.FromSeconds(1f);
            saveSelect = true;
            saveManagerInitialize = true;
            saveManager = new SaveManager();
            Time = TimeSpan.FromSeconds(0.5f);
            previousTime = TimeSpan.Zero;
            keyboardUpdate = new KeyboardUpdate();
            controlSystem = ControlSystem;
            Exit = false;
            Quit = false;
            inGame = true;
            saveFileOutput = "";
            playerName = "";
            titleText = "Create Profile";
            playerNumber = PlayerNumber;
            Controls = new KeyboardUpdate();
            Controls.Initialize(ControlSystem, 0);
            NameAvailable = true;
            MainMenuState = 3;
            activePlayers = new List<string>();
        }

        public void LoadContent()
        {
            font = Game1.Instance.Content.Load<SpriteFont>("Fonts//Font1");
        }

        public void Update(GameTime gameTime, Vector2 position1, Vector2 position2)
        {
            Position1 = position1;
            Position2 = position2;

            Controls.Update(gameTime);

            #region Controls

            if (saveSelect)
            {
                if (Controls.SelectLeft)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    SaveFile -= 1;
                }

                if (Controls.SelectRight)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    SaveFile += 1;
                }
            }

            if (Controls.Select)
            {
                selected = true;
            }
            else
                selected = false;


            #endregion

            if (inGame)
            {
                if (Controls.Back)
                {
                    Game1.Instance.AudioPlay("Back", 1);
                    Return = true;
                    Exit = true;
                    Quit = true;
                    Game1.Instance.GameState = 1;
                    CreateNew = false;
                }
            }

            if (inGame == false)
            {
                if (Controls.Back && saveSelect)
                {
                    Game1.Instance.AudioPlay("Back", 1);
                    MainMenuState = 1;
                }

                if (Controls.Back && CreateNew)
                {
                    Game1.Instance.AudioPlay("Back", 1);
                    saveSelect = true;
                    CreateNew = false;
                }
            }

            #region Save Select

            if (saveSelect)
            {
                titleText = "Select Profile";

                if (saveManagerInitialize)
                {
                    selected = false;
                    saveManager.Initialize();
                    saveManagerInitialize = false;
                }

                if (gameTime.TotalGameTime - previousSaveTime > SaveTime)
                {
                    previousSaveTime = gameTime.TotalGameTime;
                    saveManager.InitiateLoad();
                    for (int p = 0; p < activePlayers.Count; p++)
                    {
                        for (int s = 0; s < saveManager.SaveFiles.Count; s++)
                        {
                            if (saveManager.SaveFiles[s] == activePlayers[p])
                            {
                                saveManager.SaveFiles.RemoveAt(s);
                            }
                        }
                    }
                }

                if (SaveFile < 0)
                    SaveFile = saveManager.SaveFiles.Count;
                if (SaveFile > saveManager.SaveFiles.Count)
                    SaveFile = 0;

                if (SaveFile == 0)
                {
                    saveFileOutput = "Create New";

                    if (selected)
                    {
                        Game1.Instance.AudioPlay("Selected", 1);
                        CreateNew = true;
                        saveSelect = false;
                        CreatePlayer = true;
                        selected = false;
                        previousTime = gameTime.TotalGameTime;
                    }
                }

                if (SaveFile != 0)
                {
                    saveFileOutput = saveManager.SaveFiles[SaveFile - 1];

                    if (selected)
                    {
                        Game1.Instance.AudioPlay("Selected", 1);
                        saveManager.LoadSave = true;
                        selected = false;
                        saveSelect = false;
                        saveManager.playerName = saveFileOutput;
                        saveManager.InitiateLoad();
                        playerName = saveManager.playerName;

                        #region Save Variables

                        playerAcceleration = saveManager.playerAcceleration;
                        playerBulletSpeed = saveManager.playerBulletSpeed;
                        playerShip = saveManager.playerShip;
                        playerMaxBullets = saveManager.playerMaxBullets;
                        playerDamage = saveManager.playerDamage;
                        playerRedValue = saveManager.playerRedValue;
                        playerBlueValue = saveManager.playerBlueValue;
                        playerGreenValue = saveManager.playerGreenValue;
                        playerFireRate = saveManager.playerFireRate;
                        playerCredits = saveManager.playerCredits;
                        playerLives = saveManager.playerLives;
                        playerMaxHealth = saveManager.playerMaxHealth;
                        playerMaxEnergy = saveManager.playerMaxEnergy;

                        iplayerAmmo = saveManager.iplayerAmmo;
                        bplayerAutoFire = saveManager.bplayerAutoFire;
                        iplayerBulletSpeed = saveManager.iplayerBulletSpeed;
                        iplayerDamage = saveManager.iplayerDamage;
                        iplayerElectricProjectile = saveManager.iplayerElectricProjectile;
                        iplayerEnergy = saveManager.iplayerEnergy;
                        iplayerEnergyProjectile = saveManager.iplayerEnergyProjectile;
                        iplayerExplosiveProjectile = saveManager.iplayerExplosiveProjectile;
                        iplayerFireProjectile = saveManager.iplayerFireProjectile;
                        iplayerFireRate = saveManager.iplayerFireRate;
                        iplayerHealingSpecial = saveManager.iplayerHealingSpecial;
                        iplayerHealth = saveManager.iplayerHealth;
                        iplayerHealthProjectile = saveManager.iplayerHealthProjectile;
                        iplayerLaserProjectile = saveManager.iplayerLaserProjectile;
                        iplayerLaserSpecial = saveManager.iplayerLaserSpecial;
                        iplayerMoneySpecial = saveManager.iplayerMoneySpecial;
                        iplayerMovementSpeed = saveManager.iplayerMovementSpeed;
                        iplayerPoisonProjectile = saveManager.iplayerPoisonProjectile;
                        iplayerShieldSpecial = saveManager.iplayerShieldSpecial;
                        iplayerSlowProjectile = saveManager.iplayerSlowProjectile;
                        iplayerTimeStopSpecial = saveManager.iplayerTimeStopSpecial;

                        bplayerQuadShot = saveManager.bplayerQuadShot;
                        bplayerQuintupleShot = saveManager.bplayerQuintupleShot;
                        bplayerTripleShot = saveManager.bplayerTripleShot;
                        bplayerDoubleShot = saveManager.bplayerDoubleShot;
                        bplayerExtraLife1 = saveManager.bplayerExtraLife1;
                        bplayerExtraLife2 = saveManager.bplayerExtraLife2;
                        bplayerExtraLife3 = saveManager.bplayerExtraLife3;
                        bplayerExtraLife4 = saveManager.bplayerExtraLife4;

                        playerLevel = saveManager.playerLevel;
                        playerDeathCount = saveManager.playerDeathCount;
                        playerTimePlayedHours = saveManager.playerTimePlayedHours;
                        playerTimePlayedMinutes = saveManager.playerTimePlayedMinutes;
                        playerTimePlayedSeconds = saveManager.playerTimePlayedSeconds;
                        playerCreditsCollected = saveManager.playerCreditsCollected;
                        playerCreditsSpent = saveManager.playerCreditsSpent;
                        playerWeaponsCollected = saveManager.playerWeaponsCollected;
                        playerPercentageComplete = saveManager.playerPercentageComplete;
                        playerBulletsFired = saveManager.playerBulletsFired;
                        playerAccuracy = saveManager.playerAccuracy;
                        playerEnemiesKilled = saveManager.playerEnemiesKilled;
                        playerEnemiesHit = saveManager.playerEnemiesHit;
                        playerMiniGamesPassed = saveManager.playerMiniGamesPassed;
                        playerUpgradesPurchased = saveManager.playerUpgradesPurchased;
                        playerPowerUpsCollected = saveManager.playerPowerUpsCollected;
                        playerLevelsCompleted = saveManager.playerLevelsCompleted;

                        playerAchievement1 = saveManager.playerAchievement1;
                        playerAchievement2 = saveManager.playerAchievement2;
                        playerAchievement3 = saveManager.playerAchievement3;
                        playerAchievement4 = saveManager.playerAchievement4;
                        playerAchievement5 = saveManager.playerAchievement5;
                        playerAchievement6 = saveManager.playerAchievement6;
                        playerAchievement7 = saveManager.playerAchievement7;
                        playerAchievement8 = saveManager.playerAchievement8;
                        playerAchievement9 = saveManager.playerAchievement9;
                        playerAchievement10 = saveManager.playerAchievement10;
                        playerAchievement11 = saveManager.playerAchievement11;
                        playerAchievement12 = saveManager.playerAchievement12;
                        playerAchievement13 = saveManager.playerAchievement13;
                        playerAchievement14 = saveManager.playerAchievement14;
                        playerAchievement15 = saveManager.playerAchievement15;
                        playerAchievement16 = saveManager.playerAchievement16;
                        playerAchievement17 = saveManager.playerAchievement17;
                        playerAchievement18 = saveManager.playerAchievement18;
                        playerAchievement19 = saveManager.playerAchievement19;
                        playerAchievement20 = saveManager.playerAchievement20;
                        playerAchievement21 = saveManager.playerAchievement21;
                        playerAchievement22 = saveManager.playerAchievement22;
                        playerAchievement23 = saveManager.playerAchievement23;
                        playerAchievement24 = saveManager.playerAchievement24;
                        playerAchievement25 = saveManager.playerAchievement25;
                        playerAchievement26 = saveManager.playerAchievement26;
                        playerAchievement27 = saveManager.playerAchievement27;
                        playerAchievement28 = saveManager.playerAchievement28;
                        playerAchievement29 = saveManager.playerAchievement29;
                        playerAchievement30 = saveManager.playerAchievement30;
                        playerAchievement31 = saveManager.playerAchievement31;
                        playerAchievement32 = saveManager.playerAchievement32;
                        playerAchievement33 = saveManager.playerAchievement33;
                        playerAchievement34 = saveManager.playerAchievement34;
                        playerAchievement35 = saveManager.playerAchievement35;
                        playerAchievement36 = saveManager.playerAchievement36;
                        playerAchievement37 = saveManager.playerAchievement37;
                        playerAchievement38 = saveManager.playerAchievement38;
                        playerAchievement39 = saveManager.playerAchievement39;
                        playerAchievement40 = saveManager.playerAchievement40;
                        playerAchievement41 = saveManager.playerAchievement41;
                        playerAchievement42 = saveManager.playerAchievement42;
                        playerAchievement43 = saveManager.playerAchievement43;
                        playerAchievement44 = saveManager.playerAchievement44;
                        playerAchievement45 = saveManager.playerAchievement45;
                        playerAchievement46 = saveManager.playerAchievement46;
                        playerAchievement47 = saveManager.playerAchievement47;
                        playerAchievement48 = saveManager.playerAchievement48;
                        playerAchievement49 = saveManager.playerAchievement49;
                        playerAchievement50 = saveManager.playerAchievement50;
                        playerAchievementCount = saveManager.playerAchievementCount;
                        playerSelectedWeapon1 = saveManager.playerSelectedWeapon1;
                        playerSelectedWeapon2 = saveManager.playerSelectedWeapon2;
                        playerSelectedWeapon3 = saveManager.playerSelectedWeapon3;
                        playerSelectedWeapon4 = saveManager.playerSelectedWeapon4;
                        playerSelectedWeapon5 = saveManager.playerSelectedWeapon5;
                        playerSelectedSpecial = saveManager.playerSelectedSpecial;
                        playerShipsUnlocked = saveManager.playerShipsUnlocked;
                        playerXP = saveManager.playerXP;
                        playerLevelNumber = saveManager.playerLevelNumber;

                        #endregion

                        Return = true;
                    }
                }
            }

            #endregion

            #region Create New

            if (CreateNew)
            {
                titleText = "Profile Name";

                if (inGame)
                {
                    Game1.Instance.GameState = 2;
                }

                if (gameTime.TotalGameTime - previousTime > Time)
                {
                    if (CreatePlayer)
                    {
                        keyboardUpdate.Initialize(1, 1);
                        CreatePlayer = false;
                        if (inGame)
                        {
                            Game1.Instance.GameState = 1;
                        }
                    }

                    previousTime = TimeSpan.Zero;
                    keyboardUpdate.Update(gameTime);
                    playerName = keyboardUpdate.Input;
                    saveFileOutput = keyboardUpdate.Input;

                    if (Controls.Select)
                    {
                        NameAvailable = true;

                        for (int s = 0; s < saveManager.SaveFiles.Count; s++)
                        {
                            if (playerName == saveManager.SaveFiles[s])
                            {
                                Game1.Instance.AudioPlay("Unavailable", 1);
                                NameAvailable = false;
                                keyboardUpdate.state = 1;
                            }                                
                        }

                        if (playerName == "")
                        {
                            Game1.Instance.AudioPlay("Unavailable", 1);
                            NameAvailable = false;
                            keyboardUpdate.state = 1;
                        }

                        if (selected && NameAvailable)
                        {
                            if (inGame)
                            {
                                Game1.Instance.GameState = 1;
                            }
                            CreateNew = false;
                            saveManager.playerName = playerName;

                            #region Save Variables

                            saveManager.playerAcceleration = 20f;
                            saveManager.playerBulletSpeed = 5f;
                            saveManager.playerShip = 1;
                            saveManager.playerMaxBullets = 3;
                            saveManager.playerDamage = 1;
                            saveManager.playerRedValue = 255;
                            saveManager.playerBlueValue = 255;
                            saveManager.playerGreenValue = 255;
                            saveManager.playerFireRate = 1.01f;
                            saveManager.playerCredits = 0;
                            saveManager.playerLives = 0;
                            saveManager.playerMaxHealth = 10;
                            saveManager.playerMaxEnergy = 10;

                            saveManager.iplayerAmmo = 0;
                            saveManager.iplayerBulletSpeed = 0;
                            saveManager.iplayerDamage = 0;
                            saveManager.iplayerElectricProjectile = 0;
                            saveManager.iplayerEnergy = 0;
                            saveManager.iplayerEnergyProjectile = 0;
                            saveManager.iplayerExplosiveProjectile = 0;
                            saveManager.iplayerFireProjectile = 0;
                            saveManager.iplayerFireRate = 0;
                            saveManager.iplayerHealingSpecial = 0;
                            saveManager.iplayerHealth = 0;
                            saveManager.iplayerHealthProjectile = 0;
                            saveManager.iplayerLaserProjectile = 0;
                            saveManager.iplayerLaserSpecial = 0;
                            saveManager.iplayerMoneySpecial = 0;
                            saveManager.iplayerMovementSpeed = 0;
                            saveManager.iplayerPoisonProjectile = 0;
                            saveManager.iplayerShieldSpecial = 0;
                            saveManager.iplayerSlowProjectile = 0;
                            saveManager.iplayerTimeStopSpecial = 0;

                            saveManager.bplayerAutoFire = false;
                            saveManager.bplayerQuadShot = false;
                            saveManager.bplayerQuintupleShot = false;
                            saveManager.bplayerTripleShot = false;
                            saveManager.bplayerDoubleShot = false;
                            saveManager.bplayerExtraLife1 = false;
                            saveManager.bplayerExtraLife2 = false;
                            saveManager.bplayerExtraLife3 = false;
                            saveManager.bplayerExtraLife4 = false;

                            saveManager.playerLevel = 1;
                            saveManager.playerDeathCount = 0;
                            saveManager.playerTimePlayedHours = 0;
                            saveManager.playerTimePlayedMinutes = 0;
                            saveManager.playerTimePlayedSeconds = 0;
                            saveManager.playerCreditsCollected = 0;
                            saveManager.playerCreditsSpent = 0;
                            saveManager.playerWeaponsCollected = 1;
                            saveManager.playerPercentageComplete = 0;
                            saveManager.playerBulletsFired = 0;
                            saveManager.playerAccuracy = 0;
                            saveManager.playerEnemiesKilled = 0;
                            saveManager.playerEnemiesHit = 0;
                            saveManager.playerMiniGamesPassed = 0;
                            saveManager.playerUpgradesPurchased = 0;
                            saveManager.playerPowerUpsCollected = 0;
                            saveManager.playerLevelsCompleted =  0;

                            saveManager.playerAchievement1 = false;
                            saveManager.playerAchievement2 = false;
                            saveManager.playerAchievement3 = false;
                            saveManager.playerAchievement4 = false;
                            saveManager.playerAchievement5 = false;
                            saveManager.playerAchievement6 = false;
                            saveManager.playerAchievement7 = false;
                            saveManager.playerAchievement8 = false;
                            saveManager.playerAchievement9 = false;
                            saveManager.playerAchievement10 = false;
                            saveManager.playerAchievement11 = false;
                            saveManager.playerAchievement12 = false;
                            saveManager.playerAchievement13 = false;
                            saveManager.playerAchievement14 = false;
                            saveManager.playerAchievement15 = false;
                            saveManager.playerAchievement16 = false;
                            saveManager.playerAchievement17 = false;
                            saveManager.playerAchievement18 = false;
                            saveManager.playerAchievement19 = false;
                            saveManager.playerAchievement20 = false;
                            saveManager.playerAchievement21 = false;
                            saveManager.playerAchievement22 = false;
                            saveManager.playerAchievement23 = false;
                            saveManager.playerAchievement24 = false;
                            saveManager.playerAchievement25 = false;
                            saveManager.playerAchievement26 = false;
                            saveManager.playerAchievement27 = false;
                            saveManager.playerAchievement28 = false;
                            saveManager.playerAchievement29 = false;
                            saveManager.playerAchievement30 = false;
                            saveManager.playerAchievement31 = false;
                            saveManager.playerAchievement32 = false;
                            saveManager.playerAchievement33 = false;
                            saveManager.playerAchievement34 = false;
                            saveManager.playerAchievement35 = false;
                            saveManager.playerAchievement36 = false;
                            saveManager.playerAchievement37 = false;
                            saveManager.playerAchievement38 = false;
                            saveManager.playerAchievement39 = false;
                            saveManager.playerAchievement40 = false;
                            saveManager.playerAchievement41 = false;
                            saveManager.playerAchievement42 = false;
                            saveManager.playerAchievement43 = false;
                            saveManager.playerAchievement44 = false;
                            saveManager.playerAchievement45 = false;
                            saveManager.playerAchievement46 = false;
                            saveManager.playerAchievement47 = false;
                            saveManager.playerAchievement48 = false;
                            saveManager.playerAchievement49 = false;
                            saveManager.playerAchievement50 = false;
                            saveManager.playerAchievementCount = 0;
                            saveManager.playerSelectedWeapon1 = 9;
                            saveManager.playerSelectedWeapon2 = 0;
                            saveManager.playerSelectedWeapon3 = 0;
                            saveManager.playerSelectedWeapon4 = 0;
                            saveManager.playerSelectedWeapon5 = 0;
                            saveManager.playerSelectedSpecial = 0;
                            saveManager.playerShipsUnlocked = 1;
                            saveManager.playerXP = 0;
                            saveManager.playerLevelNumber = 0;

                            playerAcceleration = saveManager.playerAcceleration;
                            playerBulletSpeed = saveManager.playerBulletSpeed;
                            playerShip = saveManager.playerShip;
                            playerMaxBullets = saveManager.playerMaxBullets;
                            playerDamage = saveManager.playerDamage;
                            playerRedValue = saveManager.playerRedValue;
                            playerBlueValue = saveManager.playerBlueValue;
                            playerGreenValue = saveManager.playerGreenValue;
                            playerFireRate = saveManager.playerFireRate;
                            playerCredits = saveManager.playerCredits;
                            playerLives = saveManager.playerLives;
                            playerMaxHealth = saveManager.playerMaxHealth;
                            playerMaxEnergy = saveManager.playerMaxEnergy;

                            iplayerAmmo = saveManager.iplayerAmmo;
                            bplayerAutoFire = saveManager.bplayerAutoFire;
                            iplayerBulletSpeed = saveManager.iplayerBulletSpeed;
                            iplayerDamage = saveManager.iplayerDamage;
                            iplayerElectricProjectile = saveManager.iplayerElectricProjectile;
                            iplayerEnergy = saveManager.iplayerEnergy;
                            iplayerEnergyProjectile = saveManager.iplayerEnergyProjectile;
                            iplayerExplosiveProjectile = saveManager.iplayerExplosiveProjectile;
                            iplayerFireProjectile = saveManager.iplayerFireProjectile;
                            iplayerFireRate = saveManager.iplayerFireRate;
                            iplayerHealingSpecial = saveManager.iplayerHealingSpecial;
                            iplayerHealth = saveManager.iplayerHealth;
                            iplayerHealthProjectile = saveManager.iplayerHealthProjectile;
                            iplayerLaserProjectile = saveManager.iplayerLaserProjectile;
                            iplayerLaserSpecial = saveManager.iplayerLaserSpecial;
                            iplayerMoneySpecial = saveManager.iplayerMoneySpecial;
                            iplayerMovementSpeed = saveManager.iplayerMovementSpeed;
                            iplayerPoisonProjectile = saveManager.iplayerPoisonProjectile;
                            iplayerShieldSpecial = saveManager.iplayerShieldSpecial;
                            iplayerSlowProjectile = saveManager.iplayerSlowProjectile;
                            iplayerTimeStopSpecial = saveManager.iplayerTimeStopSpecial;

                            bplayerQuadShot = saveManager.bplayerQuadShot;
                            bplayerQuintupleShot = saveManager.bplayerQuintupleShot;
                            bplayerTripleShot = saveManager.bplayerTripleShot;
                            bplayerDoubleShot = saveManager.bplayerDoubleShot;
                            bplayerExtraLife1 = saveManager.bplayerExtraLife1;
                            bplayerExtraLife2 = saveManager.bplayerExtraLife2;
                            bplayerExtraLife3 = saveManager.bplayerExtraLife3;
                            bplayerExtraLife4 = saveManager.bplayerExtraLife4;

                            playerLevel = saveManager.playerLevel;
                            playerDeathCount = saveManager.playerDeathCount;
                            playerTimePlayedHours = saveManager.playerTimePlayedHours;
                            playerTimePlayedMinutes = saveManager.playerTimePlayedMinutes;
                            playerTimePlayedSeconds = saveManager.playerTimePlayedSeconds;
                            playerCreditsCollected = saveManager.playerCreditsCollected;
                            playerCreditsSpent = saveManager.playerCreditsSpent;
                            playerWeaponsCollected = saveManager.playerWeaponsCollected;
                            playerPercentageComplete = saveManager.playerPercentageComplete;
                            playerBulletsFired = saveManager.playerBulletsFired;
                            playerAccuracy = saveManager.playerAccuracy;
                            playerEnemiesKilled = saveManager.playerEnemiesKilled;
                            playerEnemiesHit = saveManager.playerEnemiesHit;
                            playerMiniGamesPassed = saveManager.playerMiniGamesPassed;
                            playerUpgradesPurchased = saveManager.playerUpgradesPurchased;
                            playerPowerUpsCollected = saveManager.playerPowerUpsCollected;
                            playerLevelsCompleted = saveManager.playerLevelsCompleted;

                            playerAchievement1 = saveManager.playerAchievement1;
                            playerAchievement2 = saveManager.playerAchievement2;
                            playerAchievement3 = saveManager.playerAchievement3;
                            playerAchievement4 = saveManager.playerAchievement4;
                            playerAchievement5 = saveManager.playerAchievement5;
                            playerAchievement6 = saveManager.playerAchievement6;
                            playerAchievement7 = saveManager.playerAchievement7;
                            playerAchievement8 = saveManager.playerAchievement8;
                            playerAchievement9 = saveManager.playerAchievement9;
                            playerAchievement10 = saveManager.playerAchievement10;
                            playerAchievement11 = saveManager.playerAchievement11;
                            playerAchievement12 = saveManager.playerAchievement12;
                            playerAchievement13 = saveManager.playerAchievement13;
                            playerAchievement14 = saveManager.playerAchievement14;
                            playerAchievement15 = saveManager.playerAchievement15;
                            playerAchievement16 = saveManager.playerAchievement16;
                            playerAchievement17 = saveManager.playerAchievement17;
                            playerAchievement18 = saveManager.playerAchievement18;
                            playerAchievement19 = saveManager.playerAchievement19;
                            playerAchievement20 = saveManager.playerAchievement20;
                            playerAchievement21 = saveManager.playerAchievement21;
                            playerAchievement22 = saveManager.playerAchievement22;
                            playerAchievement23 = saveManager.playerAchievement23;
                            playerAchievement24 = saveManager.playerAchievement24;
                            playerAchievement25 = saveManager.playerAchievement25;
                            playerAchievement26 = saveManager.playerAchievement26;
                            playerAchievement27 = saveManager.playerAchievement27;
                            playerAchievement28 = saveManager.playerAchievement28;
                            playerAchievement29 = saveManager.playerAchievement29;
                            playerAchievement30 = saveManager.playerAchievement30;
                            playerAchievement31 = saveManager.playerAchievement31;
                            playerAchievement32 = saveManager.playerAchievement32;
                            playerAchievement33 = saveManager.playerAchievement33;
                            playerAchievement34 = saveManager.playerAchievement34;
                            playerAchievement35 = saveManager.playerAchievement35;
                            playerAchievement36 = saveManager.playerAchievement36;
                            playerAchievement37 = saveManager.playerAchievement37;
                            playerAchievement38 = saveManager.playerAchievement38;
                            playerAchievement39 = saveManager.playerAchievement39;
                            playerAchievement40 = saveManager.playerAchievement40;
                            playerAchievement41 = saveManager.playerAchievement41;
                            playerAchievement42 = saveManager.playerAchievement42;
                            playerAchievement43 = saveManager.playerAchievement43;
                            playerAchievement44 = saveManager.playerAchievement44;
                            playerAchievement45 = saveManager.playerAchievement45;
                            playerAchievement46 = saveManager.playerAchievement46;
                            playerAchievement47 = saveManager.playerAchievement47;
                            playerAchievement48 = saveManager.playerAchievement48;
                            playerAchievement49 = saveManager.playerAchievement49;
                            playerAchievement50 = saveManager.playerAchievement50;
                            playerAchievementCount = saveManager.playerAchievementCount;
                            playerSelectedWeapon1 = saveManager.playerSelectedWeapon1;
                            playerSelectedWeapon2 = saveManager.playerSelectedWeapon2;
                            playerSelectedWeapon3 = saveManager.playerSelectedWeapon3;
                            playerSelectedWeapon4 = saveManager.playerSelectedWeapon4;
                            playerSelectedWeapon5 = saveManager.playerSelectedWeapon5;
                            playerSelectedSpecial = saveManager.playerSelectedSpecial;
                            playerShipsUnlocked = saveManager.playerShipsUnlocked;
                            playerXP = saveManager.playerXP;
                            playerLevelNumber = saveManager.playerLevelNumber;

                            #endregion

                            saveManager.InitiateSave();
                            Return = true;
                        }
                    }
                }
            }

            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (saveSelect)
            {
                spriteBatch.DrawString(font, "Select Profile", Position1, Color.Red);
                spriteBatch.DrawString(font, "" + saveFileOutput, Position2, Color.White);
            }

            if (CreateNew)
            {
                spriteBatch.DrawString(font, "Profile Name", Position1, Color.Red);
                spriteBatch.DrawString(font, "" + playerName, Position2, Color.White);
            }
        }
    }
}
