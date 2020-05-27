using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BugsInSpace
{
    class Profile
    {
        #region Variables

        SaveManager saveManager;
        LoadPlayer loadPlayer;
        GamePadState currentGamepad1State;
        GamePadState previousGamepad1State;
        TimeSpan MoveTime1;
        TimeSpan previousMoveTime1;
        TimeSpan MoveTime2;
        TimeSpan previousMoveTime2;
        TimeSpan SelectTime;
        TimeSpan previousSelectTime;
        int controlSystem;
        public int MenuLocationX;
        public int MenuLocationY;
        bool Select;
        int MenuState;
        public int MainMenuState;
        bool Loading;
        Vector2 TreePosition;
        Texture2D SelectedTexture;
        Texture2D TextBackgroundTexture;
        Vector2 SelectedPosition;
        bool Left;
        bool Right;
        bool Up;
        bool Down;
        Vector2 IconPositionX1;
        Vector2 IconPositionX2;
        Vector2 IconPositionX3;
        Vector2 IconPositionX4;
        SpriteFont font;
        Vector2 TextBoxPosition;
        string UpgradeCostString;
        int UpgradeCost;
        string UpgradeName;
        string Description1;
        string Description2;
        string Description3;
        KeyboardUpdate keyboardUpdate;
        bool MenuYChanged;
        MobileSprite AchievementsButton;
        Texture2D AchievementsButtonTexture;
        MobileSprite UpgradesButton;
        Texture2D UpgradesButtonTexture;
        MobileSprite StatsButton;
        Texture2D StatsButtonTexture;
        MobileSprite InventoryButton;
        Texture2D InventoryButtonTexture;
        bool FirstSelect;
        string playTime;
        TimeSpan Timer;
        TimeSpan previousTimer;
        bool StartTimer;
        string ZeroOne;
        string ZeroTwo;
        Texture2D StatsBackgroundTexture;
        bool Painting;
        Texture2D ColorPallet;
        Texture2D ColorSelectTexture;
        Vector2 ColorSelectPosition;
        Vector2 PalletPosition;
        Color ColorPicker;
        MobileSprite player;
        MobileSprite playerHull;
        Texture2D shipTexture;
        Texture2D hullTexture;
        bool PaintFirstTime;
        List<MobileSprite> ShipBackground;
        List<MobileSprite> ProjectileBackground;
        List<Projectile> Projectile;
        MobileSprite Ship1;
        Texture2D Ship1Texture;
        MobileSprite Hull1;
        Texture2D Hull1Texture;
        MobileSprite Ship2;
        Texture2D Ship2Texture;
        MobileSprite Hull2;
        Texture2D Hull2Texture;
        MobileSprite Ship3;
        Texture2D Ship3Texture;
        MobileSprite Hull3;
        Texture2D Hull3Texture;
        MobileSprite Ship4;
        Texture2D Ship4Texture;
        MobileSprite Hull4;
        Texture2D Hull4Texture;
        MobileSprite Ship5;
        Texture2D Ship5Texture;
        MobileSprite Hull5;
        Texture2D Hull5Texture;
        MobileSprite Ship6;
        Texture2D Ship6Texture;
        MobileSprite Hull6;
        Texture2D Hull6Texture;
        MobileSprite Ship7;
        Texture2D Ship7Texture;
        MobileSprite Hull7;
        Texture2D Hull7Texture;
        MobileSprite Ship8;
        Texture2D Ship8Texture;
        MobileSprite Hull8;
        Texture2D Hull8Texture;
        MobileSprite Ship9;
        Texture2D Ship9Texture;
        MobileSprite Hull9;
        Texture2D Hull9Texture;
        MobileSprite Ship10;
        Texture2D Ship10Texture;
        MobileSprite Hull10;
        Texture2D Hull10Texture;
        MobileSprite Ship11;
        Texture2D Ship11Texture;
        MobileSprite Hull11;
        Texture2D Hull11Texture;
        MobileSprite Ship12;
        Texture2D Ship12Texture;
        MobileSprite Hull12;
        Texture2D Hull12Texture;
        MobileSprite Ship13;
        Texture2D Ship13Texture;
        MobileSprite Hull13;
        Texture2D Hull13Texture;
        MobileSprite Ship14;
        Texture2D Ship14Texture;
        MobileSprite Hull14;
        Texture2D Hull14Texture;
        MobileSprite Ship15;
        Texture2D Ship15Texture;
        MobileSprite Hull15;
        Texture2D Hull15Texture;
        MobileSprite Ship16;
        Texture2D Ship16Texture;
        MobileSprite Hull16;
        Texture2D Hull16Texture;
        MobileSprite Ship17;
        Texture2D Ship17Texture;
        MobileSprite Hull17;
        Texture2D Hull17Texture;
        MobileSprite Ship18;
        Texture2D Ship18Texture;
        MobileSprite Hull18;
        Texture2D Hull18Texture;
        MobileSprite Ship19;
        Texture2D Ship19Texture;
        MobileSprite Hull19;
        Texture2D Hull19Texture;
        MobileSprite Ship20;
        Texture2D Ship20Texture;
        MobileSprite Hull20;
        Texture2D Hull20Texture;
        bool UpdateShipPosition;
        int ShipLocationIndex;
        Texture2D BlankButton;
        Texture2D MoneyTexture;
        int ShipSelector;
        int ProjectileSelector;
        int UpgradeUnlocks;
        int SpecialSelector;
        bool Loaded;
        int loading;
        int loadNumber;
        Texture2D LoadingTexture;
        Vector2 LoadingPosition;
        float LoadingRotation;

        #endregion

        #region Achievements

        bool InitialTracking;
        MobileSprite Achievement1;
        Texture2D Achievement1Texture;
        MobileSprite Achievement2;
        Texture2D Achievement2Texture;
        MobileSprite Achievement3;
        Texture2D Achievement3Texture;
        MobileSprite Achievement4;
        Texture2D Achievement4Texture;
        MobileSprite Achievement5;
        Texture2D Achievement5Texture;
        MobileSprite Achievement6;
        Texture2D Achievement6Texture;
        MobileSprite Achievement7;
        Texture2D Achievement7Texture;
        MobileSprite Achievement8;
        Texture2D Achievement8Texture;
        MobileSprite Achievement9;
        Texture2D Achievement9Texture;
        MobileSprite Achievement10;
        Texture2D Achievement10Texture;
        MobileSprite Achievement11;
        Texture2D Achievement11Texture;
        MobileSprite Achievement12;
        Texture2D Achievement12Texture;
        MobileSprite Achievement13;
        Texture2D Achievement13Texture;
        MobileSprite Achievement14;
        Texture2D Achievement14Texture;
        MobileSprite Achievement15;
        Texture2D Achievement15Texture;
        MobileSprite Achievement16;
        Texture2D Achievement16Texture;
        MobileSprite Achievement17;
        Texture2D Achievement17Texture;
        MobileSprite Achievement18;
        Texture2D Achievement18Texture;
        MobileSprite Achievement19;
        Texture2D Achievement19Texture;
        MobileSprite Achievement20;
        Texture2D Achievement20Texture;
        MobileSprite Achievement21;
        Texture2D Achievement21Texture;
        MobileSprite Achievement22;
        Texture2D Achievement22Texture;
        MobileSprite Achievement23;
        Texture2D Achievement23Texture;
        MobileSprite Achievement24;
        Texture2D Achievement24Texture;
        MobileSprite Achievement25;
        Texture2D Achievement25Texture;
        MobileSprite Achievement26;
        Texture2D Achievement26Texture;
        MobileSprite Achievement27;
        Texture2D Achievement27Texture;
        MobileSprite Achievement28;
        Texture2D Achievement28Texture;
        MobileSprite Achievement29;
        Texture2D Achievement29Texture;
        MobileSprite Achievement30;
        Texture2D Achievement30Texture;
        MobileSprite Achievement31;
        Texture2D Achievement31Texture;
        MobileSprite Achievement32;
        Texture2D Achievement32Texture;
        MobileSprite Achievement33;
        Texture2D Achievement33Texture;
        MobileSprite Achievement34;
        Texture2D Achievement34Texture;
        MobileSprite Achievement35;
        Texture2D Achievement35Texture;
        MobileSprite Achievement36;
        Texture2D Achievement36Texture;
        MobileSprite Achievement37;
        Texture2D Achievement37Texture;
        MobileSprite Achievement38;
        Texture2D Achievement38Texture;
        MobileSprite Achievement39;
        Texture2D Achievement39Texture;
        MobileSprite Achievement40;
        Texture2D Achievement40Texture;
        MobileSprite Achievement41;
        Texture2D Achievement41Texture;
        MobileSprite Achievement42;
        Texture2D Achievement42Texture;
        MobileSprite Achievement43;
        Texture2D Achievement43Texture;
        MobileSprite Achievement44;
        Texture2D Achievement44Texture;
        MobileSprite Achievement45;
        Texture2D Achievement45Texture;
        MobileSprite Achievement46;
        Texture2D Achievement46Texture;
        MobileSprite Achievement47;
        Texture2D Achievement47Texture;
        MobileSprite Achievement48;
        Texture2D Achievement48Texture;
        MobileSprite Achievement49;
        Texture2D Achievement49Texture;
        MobileSprite Achievement50;
        Texture2D Achievement50Texture;
        bool Achievement1Tracker;
        bool Achievement2Tracker;
        bool Achievement3Tracker;
        bool Achievement4Tracker;
        bool Achievement5Tracker;
        bool Achievement6Tracker;
        bool Achievement7Tracker;
        bool Achievement8Tracker;
        bool Achievement9Tracker;
        bool Achievement10Tracker;
        bool Achievement11Tracker;
        bool Achievement12Tracker;
        bool Achievement13Tracker;
        bool Achievement14Tracker;
        bool Achievement15Tracker;
        bool Achievement16Tracker;
        bool Achievement17Tracker;
        bool Achievement18Tracker;
        bool Achievement19Tracker;
        bool Achievement20Tracker;
        bool Achievement21Tracker;
        bool Achievement22Tracker;
        bool Achievement23Tracker;
        bool Achievement24Tracker;
        bool Achievement25Tracker;
        bool Achievement26Tracker;
        bool Achievement27Tracker;
        bool Achievement28Tracker;
        bool Achievement29Tracker;
        bool Achievement30Tracker;
        bool Achievement31Tracker;
        bool Achievement32Tracker;
        bool Achievement33Tracker;
        bool Achievement34Tracker;
        bool Achievement35Tracker;
        bool Achievement36Tracker;
        bool Achievement37Tracker;
        bool Achievement38Tracker;
        bool Achievement39Tracker;
        bool Achievement40Tracker;
        bool Achievement41Tracker;
        bool Achievement42Tracker;
        bool Achievement43Tracker;
        bool Achievement44Tracker;
        bool Achievement45Tracker;
        bool Achievement46Tracker;
        bool Achievement47Tracker;
        bool Achievement48Tracker;
        bool Achievement49Tracker;
        bool Achievement50Tracker;
        bool InitialLocationAchievement1;
        bool InitialLocationAchievement2;
        bool InitialLocationAchievement3;
        bool InitialLocationAchievement4;
        bool InitialLocationAchievement5;
        bool InitialLocationAchievement6;
        bool InitialLocationAchievement7;
        bool InitialLocationAchievement8;
        bool InitialLocationAchievement9;
        bool InitialLocationAchievement10;
        bool InitialLocationAchievement11;
        bool InitialLocationAchievement12;
        bool InitialLocationAchievement13;
        bool InitialLocationAchievement14;
        bool InitialLocationAchievement15;
        bool InitialLocationAchievement16;
        bool InitialLocationAchievement17;
        bool InitialLocationAchievement18;
        bool InitialLocationAchievement19;
        bool InitialLocationAchievement20;
        bool InitialLocationAchievement21;
        bool InitialLocationAchievement22;
        bool InitialLocationAchievement23;
        bool InitialLocationAchievement24;
        bool InitialLocationAchievement25;
        bool InitialLocationAchievement26;
        bool InitialLocationAchievement27;
        bool InitialLocationAchievement28;
        bool InitialLocationAchievement29;
        bool InitialLocationAchievement30;
        bool InitialLocationAchievement31;
        bool InitialLocationAchievement32;
        bool InitialLocationAchievement33;
        bool InitialLocationAchievement34;
        bool InitialLocationAchievement35;
        bool InitialLocationAchievement36;
        bool InitialLocationAchievement37;
        bool InitialLocationAchievement38;
        bool InitialLocationAchievement39;
        bool InitialLocationAchievement40;
        bool InitialLocationAchievement41;
        bool InitialLocationAchievement42;
        bool InitialLocationAchievement43;
        bool InitialLocationAchievement44;
        bool InitialLocationAchievement45;
        bool InitialLocationAchievement46;
        bool InitialLocationAchievement47;
        bool InitialLocationAchievement48;
        bool InitialLocationAchievement49;
        bool InitialLocationAchievement50;
        bool DrawAchievement1;
        bool DrawAchievement2;
        bool DrawAchievement3;
        bool DrawAchievement4;
        bool DrawAchievement5;
        bool DrawAchievement6;
        bool DrawAchievement7;
        bool DrawAchievement8;
        bool DrawAchievement9;
        bool DrawAchievement10;
        bool DrawAchievement11;
        bool DrawAchievement12;
        bool DrawAchievement13;
        bool DrawAchievement14;
        bool DrawAchievement15;
        bool DrawAchievement16;
        bool DrawAchievement17;
        bool DrawAchievement18;
        bool DrawAchievement19;
        bool DrawAchievement20;
        bool DrawAchievement21;
        bool DrawAchievement22;
        bool DrawAchievement23;
        bool DrawAchievement24;
        bool DrawAchievement25;
        bool DrawAchievement26;
        bool DrawAchievement27;
        bool DrawAchievement28;
        bool DrawAchievement29;
        bool DrawAchievement30;
        bool DrawAchievement31;
        bool DrawAchievement32;
        bool DrawAchievement33;
        bool DrawAchievement34;
        bool DrawAchievement35;
        bool DrawAchievement36;
        bool DrawAchievement37;
        bool DrawAchievement38;
        bool DrawAchievement39;
        bool DrawAchievement40;
        bool DrawAchievement41;
        bool DrawAchievement42;
        bool DrawAchievement43;
        bool DrawAchievement44;
        bool DrawAchievement45;
        bool DrawAchievement46;
        bool DrawAchievement47;
        bool DrawAchievement48;
        bool DrawAchievement49;
        bool DrawAchievement50;

        #endregion

        #region UpgradeIcons

        MobileSprite Ammo;
        Texture2D AmmoTexture;
        MobileSprite AutoFire;
        Texture2D AutoFireTexture;
        MobileSprite BulletSpeed;
        Texture2D BulletSpeedTexture;
        MobileSprite Damage;
        Texture2D DamageTexture;
        MobileSprite DoubleShot;
        Texture2D DoubleShotTexture;
        MobileSprite ElectricProjectile;
        Texture2D ElectricProjectileTexture;
        MobileSprite Energy;
        Texture2D EnergyTexture;
        MobileSprite EnergyProjectile;
        Texture2D EnergyProjectileTexture;
        MobileSprite ExplosiveProjectile;
        Texture2D ExplosiveProjectileTexture;
        MobileSprite ExtraLife1;
        Texture2D ExtraLifeTexture;
        MobileSprite ExtraLife2;
        MobileSprite ExtraLife3;
        MobileSprite ExtraLife4;
        MobileSprite FireProjectile;
        Texture2D FireProjectileTexture;
        MobileSprite FireRate;
        Texture2D FireRateTexture;
        MobileSprite HealingSpecial;
        Texture2D HealingSpecialTexture;
        MobileSprite Health;
        Texture2D HealthTexture;
        MobileSprite HealthProjectile;
        Texture2D HealthProjectileTexture;
        MobileSprite LaserProjectile;
        Texture2D LaserProjectileTexture;
        MobileSprite LaserSpecial;
        Texture2D LaserSpecialTexture;
        MobileSprite MoneySpecial;
        Texture2D MoneySpecialTexture;
        MobileSprite MovementSpeed;
        Texture2D MovementSpeedTexture;
        MobileSprite PoisonProjectile;
        Texture2D PoisonProjectileTexture;
        MobileSprite QuadShot;
        Texture2D QuadShotTexture;
        MobileSprite QuintupleShot;
        Texture2D QuintupleShotTexture;
        MobileSprite ShieldSpecial;
        Texture2D ShieldSpecialTexture;
        MobileSprite SlowProjectile;
        Texture2D SlowProjectileTexture;
        MobileSprite TimeStopSpecial;
        Texture2D TimeStopSpecialTexture;
        MobileSprite TripleShot;
        Texture2D TripleShotTexture;

        #endregion

        #region Tree

        Texture2D Tree1Texture;
        Texture2D Tree2Texture;
        Texture2D Tree3Texture;
        Texture2D Tree4Texture;
        Texture2D Tree5Texture;
        Texture2D Tree6Texture;
        Texture2D Tree7Texture;
        Texture2D Tree8Texture;
        Texture2D Tree9Texture;
        Texture2D Tree10Texture;
        Texture2D Tree11Texture;
        Texture2D Tree12Texture;
        Texture2D Tree13Texture;
        Texture2D Tree14Texture;
        Texture2D Tree15Texture;
        Texture2D Tree16Texture;
        Texture2D Tree17Texture;
        Texture2D Tree18Texture;
        Texture2D Tree19Texture;
        Texture2D Tree20Texture;
        Texture2D Tree21Texture;
        Texture2D Tree22Texture;

        Texture2D Tree1ActivatedTexture;
        Texture2D Tree2ActivatedTexture;
        Texture2D Tree3ActivatedTexture;
        Texture2D Tree4ActivatedTexture;
        Texture2D Tree5ActivatedTexture;
        Texture2D Tree6ActivatedTexture;
        Texture2D Tree7ActivatedTexture;
        Texture2D Tree8ActivatedTexture;
        Texture2D Tree9ActivatedTexture;
        Texture2D Tree10ActivatedTexture;
        Texture2D Tree11ActivatedTexture;
        Texture2D Tree12ActivatedTexture;
        Texture2D Tree13ActivatedTexture;
        Texture2D Tree14ActivatedTexture;
        Texture2D Tree15ActivatedTexture;
        Texture2D Tree16ActivatedTexture;
        Texture2D Tree17ActivatedTexture;
        Texture2D Tree18ActivatedTexture;
        Texture2D Tree19ActivatedTexture;
        Texture2D Tree20ActivatedTexture;
        Texture2D Tree21ActivatedTexture;
        Texture2D Tree22ActivatedTexture;

        bool Activated1;
        bool Activated2;
        bool Activated3;
        bool Activated4;
        bool Activated5;
        bool Activated6;
        bool Activated7;
        bool Activated8;
        bool Activated9;
        bool Activated10;
        bool Activated11;
        bool Activated12;
        bool Activated13;
        bool Activated14;
        bool Activated15;
        bool Activated16;
        bool Activated17;
        bool Activated18;
        bool Activated19;
        bool Activated20;
        bool Activated21;
        bool Activated22;

        Texture2D TreeBackgroundTexture;

        #endregion

        #region Player Variables

        public string playerName;
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

        #region Properties

        public int screenWidth
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Width; }
        }

        public int screenHeight
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Height; }
        }

        #endregion

        public void Initialize(int ControlSystem)
        {
            MenuState = 0;
            MenuLocationX = 1;
            MenuLocationY = 1;
            MainMenuState = 3;
            saveManager = new SaveManager();
            controlSystem = ControlSystem;
            MoveTime1 = TimeSpan.FromSeconds(0.2);
            previousMoveTime1 = TimeSpan.Zero;
            MoveTime2 = TimeSpan.FromSeconds(0.2);
            previousMoveTime2 = TimeSpan.Zero;
            SelectTime = TimeSpan.FromSeconds(0.2);
            previousSelectTime = TimeSpan.Zero;
            loadPlayer = new LoadPlayer();
            loadPlayer.Initialize(controlSystem, 1);
            loadPlayer.inGame = false;
            TreePosition = new Vector2((screenWidth / 2) - 512, 0);
            Loading = true;
            Left = false;
            Right = false;
            Up = false;
            Down = false;
            MenuYChanged = false;
            FirstSelect = true;
            PaintFirstTime = true;
            IconPositionX1.X = ((screenWidth / 2) - 410 - (205 / 2) - 32) + (205 * 1);
            IconPositionX2.X = ((screenWidth / 2) - 410 - (205 / 2) - 32) + (205 * 2);
            IconPositionX3.X = ((screenWidth / 2) - 410 - (205 / 2) - 32) + (205 * 3);
            IconPositionX4.X = ((screenWidth / 2) - 410 - (205 / 2) - 32) + (205 * 4);
            InitializeTree();
            keyboardUpdate = new KeyboardUpdate();
            Timer = TimeSpan.FromSeconds(1f);
            StartTimer = true;
            keyboardUpdate.Initialize(controlSystem, 0);
            UpdateShipPosition = true;
            ShipSelector = 1;
            ShipBackground = new List<MobileSprite>();
            ProjectileBackground = new List<MobileSprite>();
            Projectile = new List<Projectile>();
            UpgradeUnlocks = 0;
            SpecialSelector = 0;
            InitialTracking = true;
            InitialLocationAchievement1 = true;
            InitialLocationAchievement2 = true;
            InitialLocationAchievement3 = true;
            InitialLocationAchievement4 = true;
            InitialLocationAchievement5 = true;
            InitialLocationAchievement6 = true;
            InitialLocationAchievement7 = true;
            InitialLocationAchievement8 = true;
            InitialLocationAchievement9 = true;
            InitialLocationAchievement10 = true;
            InitialLocationAchievement11 = true;
            InitialLocationAchievement12 = true;
            InitialLocationAchievement13 = true;
            InitialLocationAchievement14 = true;
            InitialLocationAchievement15 = true;
            InitialLocationAchievement16 = true;
            InitialLocationAchievement17 = true;
            InitialLocationAchievement18 = true;
            InitialLocationAchievement19 = true;
            InitialLocationAchievement20 = true;
            InitialLocationAchievement21 = true;
            InitialLocationAchievement22 = true;
            InitialLocationAchievement23 = true;
            InitialLocationAchievement24 = true;
            InitialLocationAchievement25 = true;
            InitialLocationAchievement26 = true;
            InitialLocationAchievement27 = true;
            InitialLocationAchievement28 = true;
            InitialLocationAchievement29 = true;
            InitialLocationAchievement30 = true;
            InitialLocationAchievement31 = true;
            InitialLocationAchievement32 = true;
            InitialLocationAchievement33 = true;
            InitialLocationAchievement34 = true;
            InitialLocationAchievement35 = true;
            InitialLocationAchievement36 = true;
            InitialLocationAchievement37 = true;
            InitialLocationAchievement38 = true;
            InitialLocationAchievement39 = true;
            InitialLocationAchievement40 = true;
            InitialLocationAchievement41 = true;
            InitialLocationAchievement42 = true;
            InitialLocationAchievement43 = true;
            InitialLocationAchievement44 = true;
            InitialLocationAchievement45 = true;
            InitialLocationAchievement46 = true;
            InitialLocationAchievement47 = true;
            InitialLocationAchievement48 = true;
            InitialLocationAchievement49 = true;
            InitialLocationAchievement50 = true;
            loading = 1;
            loadNumber += 1;
            LoadingRotation = 0f;
            LoadingPosition = new Vector2(47, 47);
        }

        private void InitializeTree()
        {
            Activated1 = false;
            Activated2 = false;
            Activated3 = false;
            Activated4 = false;
            Activated5 = false;
            Activated6 = false;
            Activated7 = false;
            Activated8 = false;
            Activated9 = false;
            Activated10 = false;
            Activated11 = false;
            Activated12 = false;
            Activated13 = false;
            Activated14 = false;
            Activated15 = false;
            Activated16 = false;
            Activated17 = false;
            Activated18 = false;
            Activated19 = false;
            Activated20 = false;
            Activated21 = false;
            Activated22 = false;
        }

        public void LoadContent()
        {
            LoadTree();
            LoadUpgradeIcons();
            LoadButtons();
            LoadShips1();
            LoadHulls1();
            LoadAchievementIcons();

            if (loading == 1)
            {
                StatsBackgroundTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//StatsBackground");
                font = Game1.Instance.Content.Load<SpriteFont>("Fonts//Font1");
                loadPlayer.LoadContent();
                LoadingTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//Loading");
            }
            else if (loading == 2)
                ColorPallet = Game1.Instance.Content.Load<Texture2D>("Images//Menu//ColorPallet");
            else if (loading == 3)
                ColorSelectTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//PalletSelect");
            else if (loading == 4)
                BlankButton = Game1.Instance.Content.Load<Texture2D>("Images//Menu//BlankButton");
            else if (loading == 5)
                MoneyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Money");

            loadNumber += 1;

            if (loadNumber == 2)
            {
                loading += 1;
                loadNumber = 1;
            }

            if (loading > 156)
                Loaded = true;
        }

        private void LoadButtons()
        {
            if (loading == 6)
            {
                AchievementsButtonTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Achievements");
                AchievementsButton = new MobileSprite(AchievementsButtonTexture);
                AchievementsButton.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
                AchievementsButton.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
                AchievementsButton.Sprite.CurrentAnimation = "default";
                AchievementsButton.Position = new Vector2((screenWidth / 2) - (AchievementsButton.Sprite.Width / 2), 50);
                AchievementsButton.IsMoving = false;
            }
            else if (loading == 7)
            {
                UpgradesButtonTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades");
                UpgradesButton = new MobileSprite(UpgradesButtonTexture);
                UpgradesButton.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
                UpgradesButton.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
                UpgradesButton.Sprite.CurrentAnimation = "default";
                UpgradesButton.Position = new Vector2((screenWidth / 2) - (UpgradesButton.Sprite.Width / 2), 50);
                UpgradesButton.IsMoving = false;
            }
            else if (loading == 8)
            {
                InventoryButtonTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Inventory");
                InventoryButton = new MobileSprite(InventoryButtonTexture);
                InventoryButton.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
                InventoryButton.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
                InventoryButton.Sprite.CurrentAnimation = "default";
                InventoryButton.Position = new Vector2((screenWidth / 2) - (AchievementsButton.Sprite.Width / 2), 50);
                InventoryButton.IsMoving = false;
            }
            else if (loading == 9)
            {
                StatsButtonTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Stats");
                StatsButton = new MobileSprite(StatsButtonTexture);
                StatsButton.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
                StatsButton.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
                StatsButton.Sprite.CurrentAnimation = "default";
                StatsButton.Position = new Vector2((screenWidth / 2) - (StatsButton.Sprite.Width / 2), 50);
                StatsButton.IsMoving = false;
            }
        }

        private void LoadTree()
        {
            if (loading == 10)
                Tree1Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//1");
            else if (loading == 11)
                Tree2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//2");
            else if (loading == 12)
                Tree3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//3");
            else if (loading == 13)
                Tree4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//4");
            else if (loading == 14)
                Tree5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//5");
            else if (loading == 15)
                Tree6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//6");
            else if (loading == 16)
                Tree7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//7");
            else if (loading == 17)
                Tree8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//8");
            else if (loading == 18)
                Tree9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//9");
            else if (loading == 19)
                Tree10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//10");
            else if (loading == 20)
                Tree11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//11");
            else if (loading == 21)
                Tree12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//12");
            else if (loading == 22)
                Tree13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//13");
            else if (loading == 23)
                Tree14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//14");
            else if (loading == 24)
                Tree15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//15");
            else if (loading == 25)
                Tree16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//16");
            else if (loading == 26)
                Tree17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//17");
            else if (loading == 27)
                Tree18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//18");
            else if (loading == 28)
                Tree19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//19");
            else if (loading == 29)
                Tree20Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//20");
            else if (loading == 30)
                Tree21Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//21");
            else if (loading == 31)
                Tree22Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//22");
            else if (loading == 32)
                Tree1ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//1Activated");
            else if (loading == 33)
                Tree2ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//2Activated");
            else if (loading == 34)
                Tree3ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//3Activated");
            else if (loading == 35)
                Tree4ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//4Activated");
            else if (loading == 36)
                Tree5ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//5Activated");
            else if (loading == 37)
                Tree6ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//6Activated");
            else if (loading == 38)
                Tree7ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//7Activated");
            else if (loading == 39)
                Tree8ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//8Activated");
            else if (loading == 40)
                Tree9ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//9Activated");
            else if (loading == 41)
                Tree10ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//10Activated");
            else if (loading == 42)
                Tree11ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//11Activated");
            else if (loading == 43)
                Tree12ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//12Activated");
            else if (loading == 44)
                Tree13ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//13Activated");
            else if (loading == 45)
                Tree14ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//14Activated");
            else if (loading == 46)
                Tree15ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//15Activated");
            else if (loading == 47)
                Tree16ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//16Activated");
            else if (loading == 48)
                Tree17ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//17Activated");
            else if (loading == 49)
                Tree18ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//18Activated");
            else if (loading == 50)
                Tree19ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//19Activated");
            else if (loading == 51)
                Tree20ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//20Activated");
            else if (loading == 52)
                Tree21ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//21Activated");
            else if (loading == 53)
                Tree22ActivatedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//22Activated");
            else if (loading == 54)
                TextBackgroundTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//TextBackground");
            else if (loading == 55)
                TreeBackgroundTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//Background");
            else if (loading == 56)
                SelectedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Upgrades//Selected");
            else if (loading == 57)
                TextBoxPosition = new Vector2((screenWidth / 2) - (TextBackgroundTexture.Width / 2), screenHeight - TextBackgroundTexture.Height - 20);
        }

        private void LoadUpgradeIcons()
        {
            if (loading == 58)
            {
                AmmoTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Ammo");
                Ammo = new MobileSprite(AmmoTexture);
                Ammo.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                Ammo.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                Ammo.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                Ammo.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                Ammo.Sprite.CurrentAnimation = "default";
                Ammo.Position = new Vector2(IconPositionX2.X, 200 * 1);
                Ammo.IsMoving = false;
            }
            else if (loading == 59)
            {
                AutoFireTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//AutoFire");
                AutoFire = new MobileSprite(AutoFireTexture);
                AutoFire.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                AutoFire.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                AutoFire.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                AutoFire.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                AutoFire.Sprite.CurrentAnimation = "default";
                AutoFire.Position = new Vector2(IconPositionX1.X, 200 * 1);
                AutoFire.IsMoving = false;
            }
            else if (loading == 60)
            {
                BulletSpeedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//BulletSpeed");
                BulletSpeed = new MobileSprite(BulletSpeedTexture);
                BulletSpeed.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                BulletSpeed.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                BulletSpeed.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                BulletSpeed.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                BulletSpeed.Sprite.CurrentAnimation = "default";
                BulletSpeed.Position = new Vector2(IconPositionX4.X, 200 * 1);
                BulletSpeed.IsMoving = false;
            }
            else if (loading == 61)
            {
                DamageTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Damage");
                Damage = new MobileSprite(DamageTexture);
                Damage.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                Damage.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                Damage.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                Damage.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                Damage.Sprite.CurrentAnimation = "default";
                Damage.Position = new Vector2(IconPositionX3.X, 200 * 1);
                Damage.IsMoving = false;
            }
            else if (loading == 62)
            {
                DoubleShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//DoubleShot");
                DoubleShot = new MobileSprite(DoubleShotTexture);
                DoubleShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                DoubleShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                DoubleShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                DoubleShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                DoubleShot.Sprite.CurrentAnimation = "default";
                DoubleShot.Position = new Vector2(IconPositionX2.X, 200 * 1);
                DoubleShot.IsMoving = false;
            }
            else if (loading == 63)
            {
                ElectricProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ElectricProjectile");
                ElectricProjectile = new MobileSprite(ElectricProjectileTexture);
                ElectricProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ElectricProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ElectricProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ElectricProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ElectricProjectile.Sprite.CurrentAnimation = "default";
                ElectricProjectile.Position = new Vector2(IconPositionX1.X, 200 * 1);
                ElectricProjectile.IsMoving = false;
            }
            else if (loading == 64)
            {
                EnergyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Energy");
                Energy = new MobileSprite(EnergyTexture);
                Energy.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                Energy.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                Energy.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                Energy.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                Energy.Sprite.CurrentAnimation = "default";
                Energy.Position = new Vector2(IconPositionX1.X, 200 * 1);
                Energy.IsMoving = false;
            }
            else if (loading == 65)
            {
                EnergyProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//EnergyProjectile");
                EnergyProjectile = new MobileSprite(EnergyProjectileTexture);
                EnergyProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                EnergyProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                EnergyProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                EnergyProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                EnergyProjectile.Sprite.CurrentAnimation = "default";
                EnergyProjectile.Position = new Vector2(IconPositionX3.X, 200 * 1);
                EnergyProjectile.IsMoving = false;
            }
            else if (loading == 66)
            {
                ExplosiveProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ExplosiveProjectile");
                ExplosiveProjectile = new MobileSprite(ExplosiveProjectileTexture);
                ExplosiveProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ExplosiveProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ExplosiveProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ExplosiveProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ExplosiveProjectile.Sprite.CurrentAnimation = "default";
                ExplosiveProjectile.Position = new Vector2(IconPositionX3.X, 200 * 1);
                ExplosiveProjectile.IsMoving = false;
            }
            else if (loading == 67)
            {
                ExtraLifeTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ExtraLife");
                ExtraLife1 = new MobileSprite(ExtraLifeTexture);
                ExtraLife1.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ExtraLife1.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ExtraLife1.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ExtraLife1.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ExtraLife1.Sprite.CurrentAnimation = "default";
                ExtraLife1.Position = new Vector2(IconPositionX2.X, 200 * 1);
                ExtraLife1.IsMoving = false;
            }
            else if (loading == 68)
            {
                ExtraLife2 = new MobileSprite(ExtraLifeTexture);
                ExtraLife2.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ExtraLife2.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ExtraLife2.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ExtraLife2.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ExtraLife2.Sprite.CurrentAnimation = "default";
                ExtraLife2.Position = new Vector2(IconPositionX1.X, 200 * 1);
                ExtraLife2.IsMoving = false;
            }
            else if (loading == 69)
            {
                ExtraLife3 = new MobileSprite(ExtraLifeTexture);
                ExtraLife3.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ExtraLife3.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ExtraLife3.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ExtraLife3.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ExtraLife3.Sprite.CurrentAnimation = "default";
                ExtraLife3.Position = new Vector2(IconPositionX4.X, 200 * 1);
                ExtraLife3.IsMoving = false;
            }
            else if (loading == 70)
            {
                ExtraLife4 = new MobileSprite(ExtraLifeTexture);
                ExtraLife4.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ExtraLife4.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ExtraLife4.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ExtraLife4.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ExtraLife4.Sprite.CurrentAnimation = "default";
                ExtraLife4.Position = new Vector2(IconPositionX3.X, 200 * 1);
                ExtraLife4.IsMoving = false;
            }
            else if (loading == 71)
            {
                FireProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//FireProjectile");
                FireProjectile = new MobileSprite(FireProjectileTexture);
                FireProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                FireProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                FireProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                FireProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                FireProjectile.Sprite.CurrentAnimation = "default";
                FireProjectile.Position = new Vector2(IconPositionX3.X, 200 * 1);
                FireProjectile.IsMoving = false;
            }
            else if (loading == 72)
            {
                FireRateTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//FireRate");
                FireRate = new MobileSprite(FireRateTexture);
                FireRate.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                FireRate.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                FireRate.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                FireRate.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                FireRate.Sprite.CurrentAnimation = "default";
                FireRate.Position = new Vector2(IconPositionX3.X, 200 * 1);
                FireRate.IsMoving = false;
            }
            else if (loading == 73)
            {
                HealingSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//HealingSpecial");
                HealingSpecial = new MobileSprite(HealingSpecialTexture);
                HealingSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                HealingSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                HealingSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                HealingSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                HealingSpecial.Sprite.CurrentAnimation = "default";
                HealingSpecial.Position = new Vector2(IconPositionX3.X, 200 * 1);
                HealingSpecial.IsMoving = false;
            }
            else if (loading == 74)
            {
                HealthTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Health");
                Health = new MobileSprite(HealthTexture);
                Health.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                Health.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                Health.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                Health.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                Health.Sprite.CurrentAnimation = "default";
                Health.Position = new Vector2(IconPositionX2.X, 200 * 1);
                Health.IsMoving = false;
            }
            else if (loading == 75)
            {
                HealthProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//HealthProjectile");
                HealthProjectile = new MobileSprite(HealthProjectileTexture);
                HealthProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                HealthProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                HealthProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                HealthProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                HealthProjectile.Sprite.CurrentAnimation = "default";
                HealthProjectile.Position = new Vector2(IconPositionX1.X, 200 * 1);
                HealthProjectile.IsMoving = false;
            }
            else if (loading == 76)
            {
                LaserProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//LaserProjectile");
                LaserProjectile = new MobileSprite(LaserProjectileTexture);
                LaserProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                LaserProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                LaserProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                LaserProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                LaserProjectile.Sprite.CurrentAnimation = "default";
                LaserProjectile.Position = new Vector2(IconPositionX4.X, 200 * 1);
                LaserProjectile.IsMoving = false;
            }
            else if (loading == 77)
            {
                LaserSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//LaserSpecial");
                LaserSpecial = new MobileSprite(LaserSpecialTexture);
                LaserSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                LaserSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                LaserSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                LaserSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                LaserSpecial.Sprite.CurrentAnimation = "default";
                LaserSpecial.Position = new Vector2(IconPositionX1.X, 200 * 1);
                LaserSpecial.IsMoving = false;
            }
            else if (loading == 78)
            {
                MoneySpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//MoneySpecial");
                MoneySpecial = new MobileSprite(MoneySpecialTexture);
                MoneySpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                MoneySpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                MoneySpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                MoneySpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                MoneySpecial.Sprite.CurrentAnimation = "default";
                MoneySpecial.Position = new Vector2(IconPositionX1.X, 200 * 1);
                MoneySpecial.IsMoving = false;
            }
            else if (loading == 79)
            {
                MovementSpeedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//MovementSpeed");
                MovementSpeed = new MobileSprite(MovementSpeedTexture);
                MovementSpeed.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                MovementSpeed.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                MovementSpeed.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                MovementSpeed.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                MovementSpeed.Sprite.CurrentAnimation = "default";
                MovementSpeed.Position = new Vector2(IconPositionX1.X, 200 * 1);
                MovementSpeed.IsMoving = false;
            }
            else if (loading == 80)
            {
                PoisonProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//PoisonProjectile");
                PoisonProjectile = new MobileSprite(PoisonProjectileTexture);
                PoisonProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                PoisonProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                PoisonProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                PoisonProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                PoisonProjectile.Sprite.CurrentAnimation = "default";
                PoisonProjectile.Position = new Vector2(IconPositionX4.X, 200 * 1);
                PoisonProjectile.IsMoving = false;
            }
            else if (loading == 81)
            {
                QuadShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//QuadShot");
                QuadShot = new MobileSprite(QuadShotTexture);
                QuadShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                QuadShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                QuadShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                QuadShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                QuadShot.Sprite.CurrentAnimation = "default";
                QuadShot.Position = new Vector2(IconPositionX2.X, 200 * 1);
                QuadShot.IsMoving = false;
            }
            else if (loading == 82)
            {
                QuintupleShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//QuintupleShot");
                QuintupleShot = new MobileSprite(QuintupleShotTexture);
                QuintupleShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                QuintupleShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                QuintupleShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                QuintupleShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                QuintupleShot.Sprite.CurrentAnimation = "default";
                QuintupleShot.Position = new Vector2(IconPositionX2.X, 200 * 1);
                QuintupleShot.IsMoving = false;
            }
            else if (loading == 83)
            {
                ShieldSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ShieldSpecial");
                ShieldSpecial = new MobileSprite(ShieldSpecialTexture);
                ShieldSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                ShieldSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                ShieldSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                ShieldSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                ShieldSpecial.Sprite.CurrentAnimation = "default";
                ShieldSpecial.Position = new Vector2(IconPositionX3.X, 200 * 1);
                ShieldSpecial.IsMoving = false;
            }
            else if (loading == 84)
            {
                SlowProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//SlowProjectile");
                SlowProjectile = new MobileSprite(SlowProjectileTexture);
                SlowProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                SlowProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                SlowProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                SlowProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                SlowProjectile.Sprite.CurrentAnimation = "default";
                SlowProjectile.Position = new Vector2(IconPositionX4.X, 200 * 1);
                SlowProjectile.IsMoving = false;
            }
            else if (loading == 85)
            {
                TimeStopSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//TimeStopSpecial");
                TimeStopSpecial = new MobileSprite(TimeStopSpecialTexture);
                TimeStopSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                TimeStopSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                TimeStopSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                TimeStopSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                TimeStopSpecial.Sprite.CurrentAnimation = "default";
                TimeStopSpecial.Position = new Vector2(IconPositionX2.X, 200 * 1);
                TimeStopSpecial.IsMoving = false;
            }
            else if (loading == 86)
            {
                TripleShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//TripleShot");
                TripleShot = new MobileSprite(TripleShotTexture);
                TripleShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
                TripleShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
                TripleShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
                TripleShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
                TripleShot.Sprite.CurrentAnimation = "default";
                TripleShot.Position = new Vector2(IconPositionX2.X, 200 * 1);
                TripleShot.IsMoving = false;
            }
        }

        private void LoadShips1()
        {
            if (loading == 87)
            {
                Ship1Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship1");
                Ship1 = new MobileSprite(Ship1Texture);
                Ship1.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship1.Sprite.Tint = Color.White;
                Ship1.Sprite.CurrentAnimation = "default";
                Ship1.Position = new Vector2((screenWidth / 2) - (Ship1.Sprite.Width / 2), 350);
                Ship1.Sprite.Scale(0.75f);
                Ship1.IsMoving = false;
                Ship1.IsActive = true;
            }
            else if (loading == 88)
            {
                Ship2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship2");
                Ship2 = new MobileSprite(Ship2Texture);
                Ship2.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship2.Sprite.Tint = Color.White;
                Ship2.Sprite.CurrentAnimation = "default";
                Ship2.Position = Vector2.Zero;
                Ship2.Sprite.Scale(0.75f);
                Ship2.IsMoving = false;
                Ship2.IsActive = true;
            }
            else if (loading == 89)
            {
                Ship3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship3");
                Ship3 = new MobileSprite(Ship3Texture);
                Ship3.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship3.Sprite.Tint = Color.White;
                Ship3.Sprite.CurrentAnimation = "default";
                Ship3.Position = Vector2.Zero;
                Ship3.Sprite.Scale(0.75f);
                Ship3.IsMoving = false;
                Ship3.IsActive = true;
            }
            else if (loading == 90)
            {
                Ship4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship4");
                Ship4 = new MobileSprite(Ship4Texture);
                Ship4.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship4.Sprite.Tint = Color.White;
                Ship4.Sprite.CurrentAnimation = "default";
                Ship4.Position = Vector2.Zero;
                Ship4.Sprite.Scale(0.75f);
                Ship4.IsMoving = false;
                Ship4.IsActive = true;
            }
            else if (loading == 91)
            {
                Ship5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship5");
                Ship5 = new MobileSprite(Ship5Texture);
                Ship5.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship5.Sprite.Tint = Color.White;
                Ship5.Sprite.CurrentAnimation = "default";
                Ship5.Position = Vector2.Zero;
                Ship5.Sprite.Scale(0.75f);
                Ship5.IsMoving = false;
                Ship5.IsActive = true;
            }
            else if (loading == 92)
            {
                Ship6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship6");
                Ship6 = new MobileSprite(Ship6Texture);
                Ship6.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship6.Sprite.Tint = Color.White;
                Ship6.Sprite.CurrentAnimation = "default";
                Ship6.Position = Vector2.Zero;
                Ship6.Sprite.Scale(0.75f);
                Ship6.IsMoving = false;
                Ship6.IsActive = true;
            }
            else if (loading == 93)
            {
                Ship7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship7");
                Ship7 = new MobileSprite(Ship7Texture);
                Ship7.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship7.Sprite.Tint = Color.White;
                Ship7.Sprite.CurrentAnimation = "default";
                Ship7.Position = Vector2.Zero;
                Ship7.Sprite.Scale(0.75f);
                Ship7.IsMoving = false;
                Ship7.IsActive = true;
            }
            else if (loading == 94)
            {
                Ship8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship8");
                Ship8 = new MobileSprite(Ship8Texture);
                Ship8.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship8.Sprite.Tint = Color.White;
                Ship8.Sprite.CurrentAnimation = "default";
                Ship8.Position = Vector2.Zero;
                Ship8.Sprite.Scale(0.75f);
                Ship8.IsMoving = false;
                Ship8.IsActive = true;
            }
            else if (loading == 95)
            {
                Ship9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship9");
                Ship9 = new MobileSprite(Ship9Texture);
                Ship9.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship9.Sprite.Tint = Color.White;
                Ship9.Sprite.CurrentAnimation = "default";
                Ship9.Position = Vector2.Zero;
                Ship9.Sprite.Scale(0.75f);
                Ship9.IsMoving = false;
                Ship9.IsActive = true;
            }
            else if (loading == 96)
            {
                Ship10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship10");
                Ship10 = new MobileSprite(Ship10Texture);
                Ship10.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship10.Sprite.Tint = Color.White;
                Ship10.Sprite.CurrentAnimation = "default";
                Ship10.Position = Vector2.Zero;
                Ship10.Sprite.Scale(0.75f);
                Ship10.IsMoving = false;
                Ship10.IsActive = true;
            }
            else if (loading == 97)
            {
                Ship11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship11");
                Ship11 = new MobileSprite(Ship11Texture);
                Ship11.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship11.Sprite.Tint = Color.White;
                Ship11.Sprite.CurrentAnimation = "default";
                Ship11.Position = Vector2.Zero;
                Ship11.Sprite.Scale(0.75f);
                Ship11.IsMoving = false;
                Ship11.IsActive = true;
            }
            else if (loading == 98)
            {
                Ship12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship12");
                Ship12 = new MobileSprite(Ship12Texture);
                Ship12.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship12.Sprite.Tint = Color.White;
                Ship12.Sprite.CurrentAnimation = "default";
                Ship12.Position = Vector2.Zero;
                Ship12.Sprite.Scale(0.75f);
                Ship12.IsMoving = false;
                Ship12.IsActive = true;
            }
            else if (loading == 99)
            {
                Ship13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship13");
                Ship13 = new MobileSprite(Ship13Texture);
                Ship13.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship13.Sprite.Tint = Color.White;
                Ship13.Sprite.CurrentAnimation = "default";
                Ship13.Position = Vector2.Zero;
                Ship13.Sprite.Scale(0.75f);
                Ship13.IsMoving = false;
                Ship13.IsActive = true;
            }
            else if (loading == 100)
            {
                Ship14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship14");
                Ship14 = new MobileSprite(Ship14Texture);
                Ship14.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship14.Sprite.Tint = Color.White;
                Ship14.Sprite.CurrentAnimation = "default";
                Ship14.Position = Vector2.Zero;
                Ship14.Sprite.Scale(0.75f);
                Ship14.IsMoving = false;
                Ship14.IsActive = true;
            }
            else if (loading == 101)
            {
                Ship15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship15");
                Ship15 = new MobileSprite(Ship15Texture);
                Ship15.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship15.Sprite.Tint = Color.White;
                Ship15.Sprite.CurrentAnimation = "default";
                Ship15.Position = Vector2.Zero;
                Ship15.Sprite.Scale(0.75f);
                Ship15.IsMoving = false;
                Ship15.IsActive = true;
            }
            else if (loading == 102)
            {
                Ship16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship16");
                Ship16 = new MobileSprite(Ship16Texture);
                Ship16.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship16.Sprite.Tint = Color.White;
                Ship16.Sprite.CurrentAnimation = "default";
                Ship16.Position = Vector2.Zero;
                Ship16.Sprite.Scale(0.75f);
                Ship16.IsMoving = false;
                Ship16.IsActive = true;
            }
            else if (loading == 103)
            {
                Ship17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship17");
                Ship17 = new MobileSprite(Ship17Texture);
                Ship17.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship17.Sprite.Tint = Color.White;
                Ship17.Sprite.CurrentAnimation = "default";
                Ship17.Position = Vector2.Zero;
                Ship17.Sprite.Scale(0.75f);
                Ship17.IsMoving = false;
                Ship17.IsActive = true;
            }
            else if (loading == 104)
            {
                Ship18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship18");
                Ship18 = new MobileSprite(Ship18Texture);
                Ship18.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship18.Sprite.Tint = Color.White;
                Ship18.Sprite.CurrentAnimation = "default";
                Ship18.Position = Vector2.Zero;
                Ship18.Sprite.Scale(0.75f);
                Ship18.IsMoving = false;
                Ship18.IsActive = true;
            }
            else if (loading == 105)
            {
                Ship19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship19");
                Ship19 = new MobileSprite(Ship19Texture);
                Ship19.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship19.Sprite.Tint = Color.White;
                Ship19.Sprite.CurrentAnimation = "default";
                Ship19.Position = Vector2.Zero;
                Ship19.Sprite.Scale(0.75f);
                Ship19.IsMoving = false;
                Ship19.IsActive = true;
            }
            else if (loading == 106)
            {
                Ship20Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship20");
                Ship20 = new MobileSprite(Ship20Texture);
                Ship20.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Ship20.Sprite.Tint = Color.White;
                Ship20.Sprite.CurrentAnimation = "default";
                Ship20.Position = Vector2.Zero;
                Ship20.Sprite.Scale(0.75f);
                Ship20.IsMoving = false;
                Ship20.IsActive = true;
            }
        }

        private void LoadHulls1()
        {
            if (loading == 107)
            {
                Hull1Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull1");
                Hull1 = new MobileSprite(Hull1Texture);
                Hull1.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, 0.05f);
                Hull1.Sprite.Tint = Color.White;
                Hull1.Sprite.CurrentAnimation = "default";
                Hull1.Position = Ship1.Position;
                Hull1.Sprite.Scale(0.75f);
                Hull1.IsMoving = false;
                Hull1.IsActive = true;
            }
            else if (loading == 108)
            {
                Hull2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull2");
                Hull2 = new MobileSprite(Hull2Texture);
                Hull2.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull2.Sprite.Tint = Color.White;
                Hull2.Sprite.CurrentAnimation = "default";
                Hull2.Position = Ship2.Position;
                Hull2.Sprite.Scale(0.75f);
                Hull2.IsMoving = false;
                Hull2.IsActive = true;
            }
            else if (loading == 109)
            {
                Hull3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull3");
                Hull3 = new MobileSprite(Hull3Texture);
                Hull3.Sprite.AddAnimation("default", 0, 0, 64, 64, 3, 0.05f);
                Hull3.Sprite.Tint = Color.White;
                Hull3.Sprite.CurrentAnimation = "default";
                Hull3.Position = Ship3.Position;
                Hull3.Sprite.Scale(0.75f);
                Hull3.IsMoving = false;
                Hull3.IsActive = true;
            }
            else if (loading == 110)
            {
                Hull4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull4");
                Hull4 = new MobileSprite(Hull4Texture);
                Hull4.Sprite.AddAnimation("default", 0, 0, 64, 64, 12, 0.05f);
                Hull4.Sprite.Tint = Color.White;
                Hull4.Sprite.CurrentAnimation = "default";
                Hull4.Position = Ship4.Position;
                Hull4.Sprite.Scale(0.75f);
                Hull4.IsMoving = false;
                Hull4.IsActive = true;
            }
            else if (loading == 111)
            {
                Hull5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull5");
                Hull5 = new MobileSprite(Hull5Texture);
                Hull5.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull5.Sprite.Tint = Color.White;
                Hull5.Sprite.CurrentAnimation = "default";
                Hull5.Position = Ship5.Position;
                Hull5.Sprite.Scale(0.75f);
                Hull5.IsMoving = false;
                Hull5.IsActive = true;
            }
            else if (loading == 112)
            {
                Hull6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull6");
                Hull6 = new MobileSprite(Hull6Texture);
                Hull6.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull6.Sprite.Tint = Color.White;
                Hull6.Sprite.CurrentAnimation = "default";
                Hull6.Position = Ship6.Position;
                Hull6.Sprite.Scale(0.75f);
                Hull6.IsMoving = false;
                Hull6.IsActive = true;
            }
            else if (loading == 113)
            {
                Hull7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull7");
                Hull7 = new MobileSprite(Hull7Texture);
                Hull7.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull7.Sprite.Tint = Color.White;
                Hull7.Sprite.CurrentAnimation = "default";
                Hull7.Position = Ship7.Position;
                Hull7.Sprite.Scale(0.75f);
                Hull7.IsMoving = false;
                Hull7.IsActive = true;
            }
            else if (loading == 114)
            {
                Hull8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull8");
                Hull8 = new MobileSprite(Hull8Texture);
                Hull8.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull8.Sprite.Tint = Color.White;
                Hull8.Sprite.CurrentAnimation = "default";
                Hull8.Position = Ship8.Position;
                Hull8.Sprite.Scale(0.75f);
                Hull8.IsMoving = false;
                Hull8.IsActive = true;
            }
            else if (loading == 115)
            {
                Hull9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull9");
                Hull9 = new MobileSprite(Hull9Texture);
                Hull9.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull9.Sprite.Tint = Color.White;
                Hull9.Sprite.CurrentAnimation = "default";
                Hull9.Position = Ship9.Position;
                Hull9.Sprite.Scale(0.75f);
                Hull9.IsMoving = false;
                Hull9.IsActive = true;
            }
            else if (loading == 116)
            {
                Hull10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull10");
                Hull10 = new MobileSprite(Hull10Texture);
                Hull10.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull10.Sprite.Tint = Color.White;
                Hull10.Sprite.CurrentAnimation = "default";
                Hull10.Position = Ship10.Position;
                Hull10.Sprite.Scale(0.75f);
                Hull10.IsMoving = false;
                Hull10.IsActive = true;
            }
            else if (loading == 117)
            {
                Hull11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull11");
                Hull11 = new MobileSprite(Hull11Texture);
                Hull11.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull11.Sprite.Tint = Color.White;
                Hull11.Sprite.CurrentAnimation = "default";
                Hull11.Position = Ship11.Position;
                Hull11.Sprite.Scale(0.75f);
                Hull11.IsMoving = false;
                Hull11.IsActive = true;
            }
            else if (loading == 118)
            {
                Hull12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull12");
                Hull12 = new MobileSprite(Hull12Texture);
                Hull12.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull12.Sprite.Tint = Color.White;
                Hull12.Sprite.CurrentAnimation = "default";
                Hull12.Position = Ship12.Position;
                Hull12.Sprite.Scale(0.75f);
                Hull12.IsMoving = false;
                Hull12.IsActive = true;
            }
            else if (loading == 119)
            {
                Hull13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull13");
                Hull13 = new MobileSprite(Hull13Texture);
                Hull13.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull13.Sprite.Tint = Color.White;
                Hull13.Sprite.CurrentAnimation = "default";
                Hull13.Position = Ship13.Position;
                Hull13.Sprite.Scale(0.75f);
                Hull13.IsMoving = false;
                Hull13.IsActive = true;
            }
            else if (loading == 120)
            {
                Hull14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull14");
                Hull14 = new MobileSprite(Hull14Texture);
                Hull14.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull14.Sprite.Tint = Color.White;
                Hull14.Sprite.CurrentAnimation = "default";
                Hull14.Position = Ship14.Position;
                Hull14.Sprite.Scale(0.75f);
                Hull14.IsMoving = false;
                Hull14.IsActive = true;
            }
            else if (loading == 121)
            {
                Hull15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull15");
                Hull15 = new MobileSprite(Hull15Texture);
                Hull15.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull15.Sprite.Tint = Color.White;
                Hull15.Sprite.CurrentAnimation = "default";
                Hull15.Position = Ship15.Position;
                Hull15.Sprite.Scale(0.75f);
                Hull15.IsMoving = false;
                Hull15.IsActive = true;
            }
            else if (loading == 122)
            {
                Hull16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull16");
                Hull16 = new MobileSprite(Hull16Texture);
                Hull16.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull16.Sprite.Tint = Color.White;
                Hull16.Sprite.CurrentAnimation = "default";
                Hull16.Position = Ship16.Position;
                Hull16.Sprite.Scale(0.75f);
                Hull16.IsMoving = false;
                Hull16.IsActive = true;
            }
            else if (loading == 123)
            {
                Hull17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull17");
                Hull17 = new MobileSprite(Hull17Texture);
                Hull17.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull17.Sprite.Tint = Color.White;
                Hull17.Sprite.CurrentAnimation = "default";
                Hull17.Position = Ship17.Position;
                Hull17.Sprite.Scale(0.75f);
                Hull17.IsMoving = false;
                Hull17.IsActive = true;
            }
            else if (loading == 124)
            {
                Hull18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull18");
                Hull18 = new MobileSprite(Hull18Texture);
                Hull18.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull18.Sprite.Tint = Color.White;
                Hull18.Sprite.CurrentAnimation = "default";
                Hull18.Position = Ship18.Position;
                Hull18.Sprite.Scale(0.75f);
                Hull18.IsMoving = false;
                Hull18.IsActive = true;
            }
            else if (loading == 125)
            {
                Hull19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull19");
                Hull19 = new MobileSprite(Hull19Texture);
                Hull19.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull19.Sprite.Tint = Color.White;
                Hull19.Sprite.CurrentAnimation = "default";
                Hull19.Position = Ship19.Position;
                Hull19.Sprite.Scale(0.75f);
                Hull19.IsMoving = false;
                Hull19.IsActive = true;
            }
            else if (loading == 126)
            {
                Hull20Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull20");
                Hull20 = new MobileSprite(Hull20Texture);
                Hull20.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                Hull20.Sprite.Tint = Color.White;
                Hull20.Sprite.CurrentAnimation = "default";
                Hull20.Position = Ship20.Position;
                Hull20.Sprite.Scale(0.75f);
                Hull20.IsMoving = false;
                Hull20.IsActive = true;
            }
        }

        private void LoadShips()
        {
            if (playerShip == 1)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship1");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 2)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship2");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 3)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship3");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 4)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship4");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 5)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship5");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 6)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship6");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 7)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship7");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 8)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship8");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 9)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship9");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 10)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship10");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 11)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship11");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 12)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship12");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 13)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship13");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 14)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship14");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 15)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship15");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 16)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship16");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 17)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship17");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 18)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship18");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 19)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship19");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 20)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship20");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                player.Sprite.Tint = Color.White;
                player.Sprite.CurrentAnimation = "default";
            }

            player.Position = new Vector2((screenWidth / 2) - player.Sprite.Width, 0 - player.Sprite.Height);
            player.IsMoving = false;
            player.locked = true;
            player.IsActive = true;
            player.IsCollidable = true;
        }

        private void LoadHulls()
        {
            if (playerShip == 1)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull1");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 2)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull2");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 3)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull3");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 3, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 4)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull4");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 12, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 5)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull5");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 6)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull6");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 7)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull7");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 8)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull8");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 9)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull9");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 10)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull10");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 11)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull11");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 12)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull12");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 13)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull13");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 14)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull14");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 15)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull15");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 16)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull16");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 17)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull17");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 18)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull18");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 19)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull19");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            if (playerShip == 20)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull20");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
                playerHull.Sprite.Tint = Color.White;
                playerHull.Sprite.CurrentAnimation = "default";
            }

            playerHull.Position = player.Position;
            playerHull.IsMoving = false;
            playerHull.locked = false;
            playerHull.IsActive = true;
        }

        private void LoadAchievementIcons()
        {
            if (loading == 107)
            {
                Achievement1Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement01");
                Achievement1 = new MobileSprite(Achievement1Texture);
                Achievement1.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement1.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement1.Sprite.CurrentAnimation = "default";
                Achievement1.Position = Vector2.Zero;
                Achievement1.IsMoving = false;
            }
            else if (loading == 108)
            {
                Achievement2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement02");
                Achievement2 = new MobileSprite(Achievement2Texture);
                Achievement2.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement2.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement2.Sprite.CurrentAnimation = "default";
                Achievement2.Position = Vector2.Zero;
                Achievement2.IsMoving = false;
            }
            else if (loading == 109)
            {
                Achievement3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement03");
                Achievement3 = new MobileSprite(Achievement3Texture);
                Achievement3.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement3.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement3.Sprite.CurrentAnimation = "default";
                Achievement3.Position = Vector2.Zero;
                Achievement3.IsMoving = false;
            }
            else if (loading == 110)
            {
                Achievement4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement04");
                Achievement4 = new MobileSprite(Achievement4Texture);
                Achievement4.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement4.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement4.Sprite.CurrentAnimation = "default";
                Achievement4.Position = Vector2.Zero;
                Achievement4.IsMoving = false;
            }
            else if (loading == 111)
            {
                Achievement5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement05");
                Achievement5 = new MobileSprite(Achievement5Texture);
                Achievement5.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement5.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement5.Sprite.CurrentAnimation = "default";
                Achievement5.Position = Vector2.Zero;
                Achievement5.IsMoving = false;
            }
            else if (loading == 112)
            {
                Achievement6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement06");
                Achievement6 = new MobileSprite(Achievement6Texture);
                Achievement6.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement6.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement6.Sprite.CurrentAnimation = "default";
                Achievement6.Position = Vector2.Zero;
                Achievement6.IsMoving = false;
            }
            else if (loading == 113)
            {
                Achievement7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement07");
                Achievement7 = new MobileSprite(Achievement7Texture);
                Achievement7.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement7.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement7.Sprite.CurrentAnimation = "default";
                Achievement7.Position = Vector2.Zero;
                Achievement7.IsMoving = false;
            }
            else if (loading == 114)
            {
                Achievement8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement08");
                Achievement8 = new MobileSprite(Achievement8Texture);
                Achievement8.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement8.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement8.Sprite.CurrentAnimation = "default";
                Achievement8.Position = Vector2.Zero;
                Achievement8.IsMoving = false;
            }
            else if (loading == 115)
            {
                Achievement9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement09");
                Achievement9 = new MobileSprite(Achievement1Texture);
                Achievement9.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement9.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement9.Sprite.CurrentAnimation = "default";
                Achievement9.Position = Vector2.Zero;
                Achievement9.IsMoving = false;
            }
            else if (loading == 116)
            {
                Achievement10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement10");
                Achievement10 = new MobileSprite(Achievement10Texture);
                Achievement10.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement10.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement10.Sprite.CurrentAnimation = "default";
                Achievement10.Position = Vector2.Zero;
                Achievement10.IsMoving = false;
            }
            else if (loading == 117)
            {
                Achievement11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement11");
                Achievement11 = new MobileSprite(Achievement11Texture);
                Achievement11.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement11.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement11.Sprite.CurrentAnimation = "default";
                Achievement11.Position = Vector2.Zero;
                Achievement11.IsMoving = false;
            }
            else if (loading == 118)
            {
                Achievement12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement12");
                Achievement12 = new MobileSprite(Achievement12Texture);
                Achievement12.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement12.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement12.Sprite.CurrentAnimation = "default";
                Achievement12.Position = Vector2.Zero;
                Achievement12.IsMoving = false;
            }
            else if (loading == 119)
            {
                Achievement13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement13");
                Achievement13 = new MobileSprite(Achievement13Texture);
                Achievement13.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement13.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement13.Sprite.CurrentAnimation = "default";
                Achievement13.Position = Vector2.Zero;
                Achievement13.IsMoving = false;
            }
            else if (loading == 120)
            {
                Achievement14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement14");
                Achievement14 = new MobileSprite(Achievement14Texture);
                Achievement14.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement14.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement14.Sprite.CurrentAnimation = "default";
                Achievement14.Position = Vector2.Zero;
                Achievement14.IsMoving = false;
            }
            else if (loading == 121)
            {
                Achievement15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement15");
                Achievement15 = new MobileSprite(Achievement15Texture);
                Achievement15.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement15.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement15.Sprite.CurrentAnimation = "default";
                Achievement15.Position = Vector2.Zero;
                Achievement15.IsMoving = false;
            }
            else if (loading == 122)
            {
                Achievement16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement16");
                Achievement16 = new MobileSprite(Achievement16Texture);
                Achievement16.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement16.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement16.Sprite.CurrentAnimation = "default";
                Achievement16.Position = Vector2.Zero;
                Achievement16.IsMoving = false;
            }
            else if (loading == 123)
            {
                Achievement17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement17");
                Achievement17 = new MobileSprite(Achievement17Texture);
                Achievement17.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement17.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement17.Sprite.CurrentAnimation = "default";
                Achievement17.Position = Vector2.Zero;
                Achievement17.IsMoving = false;
            }
            else if (loading == 124)
            {
                Achievement18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement18");
                Achievement18 = new MobileSprite(Achievement18Texture);
                Achievement18.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement18.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement18.Sprite.CurrentAnimation = "default";
                Achievement18.Position = Vector2.Zero;
                Achievement18.IsMoving = false;
            }
            else if (loading == 125)
            {
                Achievement19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement19");
                Achievement19 = new MobileSprite(Achievement11Texture);
                Achievement19.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement19.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement19.Sprite.CurrentAnimation = "default";
                Achievement19.Position = Vector2.Zero;
                Achievement19.IsMoving = false;
            }
            else if (loading == 126)
            {
                Achievement20Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement20");
                Achievement20 = new MobileSprite(Achievement20Texture);
                Achievement20.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement20.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement20.Sprite.CurrentAnimation = "default";
                Achievement20.Position = Vector2.Zero;
                Achievement20.IsMoving = false;
            }
            else if (loading == 127)
            {
                Achievement21Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement21");
                Achievement21 = new MobileSprite(Achievement21Texture);
                Achievement21.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement21.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement21.Sprite.CurrentAnimation = "default";
                Achievement21.Position = Vector2.Zero;
                Achievement21.IsMoving = false;
            }
            else if (loading == 128)
            {
                Achievement22Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement22");
                Achievement22 = new MobileSprite(Achievement22Texture);
                Achievement22.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement22.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement22.Sprite.CurrentAnimation = "default";
                Achievement22.Position = Vector2.Zero;
                Achievement22.IsMoving = false;
            }
            else if (loading == 129)
            {
                Achievement23Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement23");
                Achievement23 = new MobileSprite(Achievement23Texture);
                Achievement23.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement23.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement23.Sprite.CurrentAnimation = "default";
                Achievement23.Position = Vector2.Zero;
                Achievement23.IsMoving = false;
            }
            else if (loading == 130)
            {
                Achievement24Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement24");
                Achievement24 = new MobileSprite(Achievement24Texture);
                Achievement24.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement24.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement24.Sprite.CurrentAnimation = "default";
                Achievement24.Position = Vector2.Zero;
                Achievement24.IsMoving = false;
            }
            else if (loading == 131)
            {
                Achievement25Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement25");
                Achievement25 = new MobileSprite(Achievement25Texture);
                Achievement25.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement25.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement25.Sprite.CurrentAnimation = "default";
                Achievement25.Position = Vector2.Zero;
                Achievement25.IsMoving = false;
            }
            else if (loading == 132)
            {
                Achievement26Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement26");
                Achievement26 = new MobileSprite(Achievement26Texture);
                Achievement26.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement26.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement26.Sprite.CurrentAnimation = "default";
                Achievement26.Position = Vector2.Zero;
                Achievement26.IsMoving = false;
            }
            else if (loading == 133)
            {
                Achievement27Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement27");
                Achievement27 = new MobileSprite(Achievement27Texture);
                Achievement27.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement27.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement27.Sprite.CurrentAnimation = "default";
                Achievement27.Position = Vector2.Zero;
                Achievement27.IsMoving = false;
            }
            else if (loading == 134)
            {
                Achievement28Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement28");
                Achievement28 = new MobileSprite(Achievement28Texture);
                Achievement28.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement28.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement28.Sprite.CurrentAnimation = "default";
                Achievement28.Position = Vector2.Zero;
                Achievement28.IsMoving = false;
            }
            else if (loading == 135)
            {
                Achievement29Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement29");
                Achievement29 = new MobileSprite(Achievement21Texture);
                Achievement29.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement29.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement29.Sprite.CurrentAnimation = "default";
                Achievement29.Position = Vector2.Zero;
                Achievement29.IsMoving = false;
            }
            else if (loading == 136)
            {
                Achievement30Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement30");
                Achievement30 = new MobileSprite(Achievement30Texture);
                Achievement30.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement30.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement30.Sprite.CurrentAnimation = "default";
                Achievement30.Position = Vector2.Zero;
                Achievement30.IsMoving = false;
            }
            else if (loading == 137)
            {
                Achievement31Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement31");
                Achievement31 = new MobileSprite(Achievement31Texture);
                Achievement31.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement31.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement31.Sprite.CurrentAnimation = "default";
                Achievement31.Position = Vector2.Zero;
                Achievement31.IsMoving = false;
            }
            else if (loading == 138)
            {
                Achievement32Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement32");
                Achievement32 = new MobileSprite(Achievement32Texture);
                Achievement32.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement32.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement32.Sprite.CurrentAnimation = "default";
                Achievement32.Position = Vector2.Zero;
                Achievement32.IsMoving = false;
            }
            else if (loading == 139)
            {
                Achievement33Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement33");
                Achievement33 = new MobileSprite(Achievement33Texture);
                Achievement33.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement33.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement33.Sprite.CurrentAnimation = "default";
                Achievement33.Position = Vector2.Zero;
                Achievement33.IsMoving = false;
            }
            else if (loading == 140)
            {
                Achievement34Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement34");
                Achievement34 = new MobileSprite(Achievement34Texture);
                Achievement34.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement34.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement34.Sprite.CurrentAnimation = "default";
                Achievement34.Position = Vector2.Zero;
                Achievement34.IsMoving = false;
            }
            else if (loading == 141)
            {
                Achievement35Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement35");
                Achievement35 = new MobileSprite(Achievement35Texture);
                Achievement35.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement35.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement35.Sprite.CurrentAnimation = "default";
                Achievement35.Position = Vector2.Zero;
                Achievement35.IsMoving = false;
            }
            else if (loading == 142)
            {
                Achievement36Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement36");
                Achievement36 = new MobileSprite(Achievement36Texture);
                Achievement36.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement36.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement36.Sprite.CurrentAnimation = "default";
                Achievement36.Position = Vector2.Zero;
                Achievement36.IsMoving = false;
            }
            else if (loading == 143)
            {
                Achievement37Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement37");
                Achievement37 = new MobileSprite(Achievement37Texture);
                Achievement37.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement37.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement37.Sprite.CurrentAnimation = "default";
                Achievement37.Position = Vector2.Zero;
                Achievement37.IsMoving = false;
            }
            else if (loading == 144)
            {
                Achievement38Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement38");
                Achievement38 = new MobileSprite(Achievement38Texture);
                Achievement38.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement38.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement38.Sprite.CurrentAnimation = "default";
                Achievement38.Position = Vector2.Zero;
                Achievement38.IsMoving = false;
            }
            else if (loading == 145)
            {
                Achievement39Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement39");
                Achievement39 = new MobileSprite(Achievement31Texture);
                Achievement39.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement39.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement39.Sprite.CurrentAnimation = "default";
                Achievement39.Position = Vector2.Zero;
                Achievement39.IsMoving = false;
            }
            else if (loading == 146)
            {
                Achievement40Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement40");
                Achievement40 = new MobileSprite(Achievement40Texture);
                Achievement40.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement40.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement40.Sprite.CurrentAnimation = "default";
                Achievement40.Position = Vector2.Zero;
                Achievement40.IsMoving = false;
            }
            else if (loading == 147)
            {
                Achievement41Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement41");
                Achievement41 = new MobileSprite(Achievement41Texture);
                Achievement41.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement41.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement41.Sprite.CurrentAnimation = "default";
                Achievement41.Position = Vector2.Zero;
                Achievement41.IsMoving = false;
            }
            else if (loading == 148)
            {
                Achievement42Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement42");
                Achievement42 = new MobileSprite(Achievement42Texture);
                Achievement42.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement42.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement42.Sprite.CurrentAnimation = "default";
                Achievement42.Position = Vector2.Zero;
                Achievement42.IsMoving = false;
            }
            else if (loading == 149)
            {
                Achievement43Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement43");
                Achievement43 = new MobileSprite(Achievement43Texture);
                Achievement43.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement43.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement43.Sprite.CurrentAnimation = "default";
                Achievement43.Position = Vector2.Zero;
                Achievement43.IsMoving = false;
            }
            else if (loading == 150)
            {
                Achievement44Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement44");
                Achievement44 = new MobileSprite(Achievement44Texture);
                Achievement44.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement44.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement44.Sprite.CurrentAnimation = "default";
                Achievement44.Position = Vector2.Zero;
                Achievement44.IsMoving = false;
            }
            else if (loading == 151)
            {
                Achievement45Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement45");
                Achievement45 = new MobileSprite(Achievement45Texture);
                Achievement45.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement45.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement45.Sprite.CurrentAnimation = "default";
                Achievement45.Position = Vector2.Zero;
                Achievement45.IsMoving = false;
            }
            else if (loading == 152)
            {
                Achievement46Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement46");
                Achievement46 = new MobileSprite(Achievement46Texture);
                Achievement46.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement46.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement46.Sprite.CurrentAnimation = "default";
                Achievement46.Position = Vector2.Zero;
                Achievement46.IsMoving = false;
            }
            else if (loading == 153)
            {
                Achievement47Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement47");
                Achievement47 = new MobileSprite(Achievement47Texture);
                Achievement47.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement47.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement47.Sprite.CurrentAnimation = "default";
                Achievement47.Position = Vector2.Zero;
                Achievement47.IsMoving = false;
            }
            else if (loading == 154)
            {
                Achievement48Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement48");
                Achievement48 = new MobileSprite(Achievement48Texture);
                Achievement48.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement48.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement48.Sprite.CurrentAnimation = "default";
                Achievement48.Position = Vector2.Zero;
                Achievement48.IsMoving = false;
            }
            else if (loading == 155)
            {
                Achievement49Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement49");
                Achievement49 = new MobileSprite(Achievement41Texture);
                Achievement49.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement49.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement49.Sprite.CurrentAnimation = "default";
                Achievement49.Position = Vector2.Zero;
                Achievement49.IsMoving = false;
            }
            else if (loading == 156)
            {
                Achievement50Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement50");
                Achievement50 = new MobileSprite(Achievement50Texture);
                Achievement50.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
                Achievement50.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                Achievement50.Sprite.CurrentAnimation = "default";
                Achievement50.Position = Vector2.Zero;
                Achievement50.IsMoving = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            #region Load Player

            if (Loading)
            {
                loadPlayer.Update(gameTime, new Vector2((screenWidth / 2) - (font.MeasureString(loadPlayer.titleText).X / 2), 40), new Vector2((screenWidth / 2) - (font.MeasureString(loadPlayer.saveFileOutput).X / 2), 60));
                loadPlayer.Update(gameTime, new Vector2((screenWidth / 2) - (font.MeasureString(loadPlayer.titleText).X / 2), 40), new Vector2((screenWidth / 2) - (font.MeasureString(loadPlayer.saveFileOutput).X / 2), 60));
                MainMenuState = loadPlayer.MainMenuState;
                LoadingRotation += 0.1f;
            }

            if (Loaded == false)
            {
                LoadContent();
            }
            else
            {
                if (loadPlayer.Return && loadPlayer.Exit == false && Loading)
                {
                    #region SaveData

                    playerName = loadPlayer.playerName;
                    playerAcceleration = loadPlayer.playerAcceleration;
                    playerBulletSpeed = loadPlayer.playerBulletSpeed;
                    playerShip = loadPlayer.playerShip;
                    playerMaxBullets = loadPlayer.playerMaxBullets;
                    playerDamage = loadPlayer.playerDamage;
                    playerRedValue = loadPlayer.playerRedValue;
                    playerBlueValue = loadPlayer.playerBlueValue;
                    playerGreenValue = loadPlayer.playerGreenValue;
                    playerFireRate = loadPlayer.playerFireRate;
                    playerCredits = loadPlayer.playerCredits;
                    playerLives = loadPlayer.playerLives;
                    playerMaxHealth = loadPlayer.playerMaxHealth;
                    playerMaxEnergy = loadPlayer.playerMaxEnergy;

                    iplayerAmmo = loadPlayer.iplayerAmmo;
                    bplayerAutoFire = loadPlayer.bplayerAutoFire;
                    iplayerBulletSpeed = loadPlayer.iplayerBulletSpeed;
                    iplayerDamage = loadPlayer.iplayerDamage;
                    iplayerElectricProjectile = loadPlayer.iplayerElectricProjectile;
                    iplayerEnergy = loadPlayer.iplayerEnergy;
                    iplayerEnergyProjectile = loadPlayer.iplayerEnergyProjectile;
                    iplayerExplosiveProjectile = loadPlayer.iplayerExplosiveProjectile;
                    iplayerFireProjectile = loadPlayer.iplayerFireProjectile;
                    iplayerFireRate = loadPlayer.iplayerFireRate;
                    iplayerHealingSpecial = loadPlayer.iplayerHealingSpecial;
                    iplayerHealth = loadPlayer.iplayerHealth;
                    iplayerHealthProjectile = loadPlayer.iplayerHealthProjectile;
                    iplayerLaserProjectile = loadPlayer.iplayerLaserProjectile;
                    iplayerLaserSpecial = loadPlayer.iplayerLaserSpecial;
                    iplayerMoneySpecial = loadPlayer.iplayerMoneySpecial;
                    iplayerMovementSpeed = loadPlayer.iplayerMovementSpeed;
                    iplayerPoisonProjectile = loadPlayer.iplayerPoisonProjectile;
                    iplayerShieldSpecial = loadPlayer.iplayerShieldSpecial;
                    iplayerSlowProjectile = loadPlayer.iplayerSlowProjectile;
                    iplayerTimeStopSpecial = loadPlayer.iplayerTimeStopSpecial;

                    bplayerQuadShot = loadPlayer.bplayerQuadShot;
                    bplayerQuintupleShot = loadPlayer.bplayerQuintupleShot;
                    bplayerTripleShot = loadPlayer.bplayerTripleShot;
                    bplayerDoubleShot = loadPlayer.bplayerDoubleShot;
                    bplayerExtraLife1 = loadPlayer.bplayerExtraLife1;
                    bplayerExtraLife2 = loadPlayer.bplayerExtraLife2;
                    bplayerExtraLife3 = loadPlayer.bplayerExtraLife3;
                    bplayerExtraLife4 = loadPlayer.bplayerExtraLife4;

                    playerLevel = loadPlayer.playerLevel;
                    playerDeathCount = loadPlayer.playerDeathCount;
                    playerTimePlayedHours = loadPlayer.playerTimePlayedHours;
                    playerTimePlayedMinutes = loadPlayer.playerTimePlayedMinutes;
                    playerTimePlayedSeconds = loadPlayer.playerTimePlayedSeconds;
                    playerCreditsCollected = loadPlayer.playerCreditsCollected;
                    playerCreditsSpent = loadPlayer.playerCreditsSpent;
                    playerWeaponsCollected = loadPlayer.playerWeaponsCollected;
                    playerPercentageComplete = loadPlayer.playerPercentageComplete;
                    playerBulletsFired = loadPlayer.playerBulletsFired;
                    playerAccuracy = loadPlayer.playerAccuracy;
                    playerEnemiesKilled = loadPlayer.playerEnemiesKilled;
                    playerEnemiesHit = loadPlayer.playerEnemiesHit;
                    playerMiniGamesPassed = loadPlayer.playerMiniGamesPassed;
                    playerUpgradesPurchased = loadPlayer.playerUpgradesPurchased;
                    playerPowerUpsCollected = loadPlayer.playerPowerUpsCollected;
                    playerLevelsCompleted = loadPlayer.playerLevelsCompleted;

                    playerAchievement1 = loadPlayer.playerAchievement1;
                    playerAchievement2 = loadPlayer.playerAchievement2;
                    playerAchievement3 = loadPlayer.playerAchievement3;
                    playerAchievement4 = loadPlayer.playerAchievement4;
                    playerAchievement5 = loadPlayer.playerAchievement5;
                    playerAchievement6 = loadPlayer.playerAchievement6;
                    playerAchievement7 = loadPlayer.playerAchievement7;
                    playerAchievement8 = loadPlayer.playerAchievement8;
                    playerAchievement9 = loadPlayer.playerAchievement9;
                    playerAchievement10 = loadPlayer.playerAchievement10;
                    playerAchievement11 = loadPlayer.playerAchievement11;
                    playerAchievement12 = loadPlayer.playerAchievement12;
                    playerAchievement13 = loadPlayer.playerAchievement13;
                    playerAchievement14 = loadPlayer.playerAchievement14;
                    playerAchievement15 = loadPlayer.playerAchievement15;
                    playerAchievement16 = loadPlayer.playerAchievement16;
                    playerAchievement17 = loadPlayer.playerAchievement17;
                    playerAchievement18 = loadPlayer.playerAchievement18;
                    playerAchievement19 = loadPlayer.playerAchievement19;
                    playerAchievement20 = loadPlayer.playerAchievement20;
                    playerAchievement21 = loadPlayer.playerAchievement21;
                    playerAchievement22 = loadPlayer.playerAchievement22;
                    playerAchievement23 = loadPlayer.playerAchievement23;
                    playerAchievement24 = loadPlayer.playerAchievement24;
                    playerAchievement25 = loadPlayer.playerAchievement25;
                    playerAchievement26 = loadPlayer.playerAchievement26;
                    playerAchievement27 = loadPlayer.playerAchievement27;
                    playerAchievement28 = loadPlayer.playerAchievement28;
                    playerAchievement29 = loadPlayer.playerAchievement29;
                    playerAchievement30 = loadPlayer.playerAchievement30;
                    playerAchievement31 = loadPlayer.playerAchievement31;
                    playerAchievement32 = loadPlayer.playerAchievement32;
                    playerAchievement33 = loadPlayer.playerAchievement33;
                    playerAchievement34 = loadPlayer.playerAchievement34;
                    playerAchievement35 = loadPlayer.playerAchievement35;
                    playerAchievement36 = loadPlayer.playerAchievement36;
                    playerAchievement37 = loadPlayer.playerAchievement37;
                    playerAchievement38 = loadPlayer.playerAchievement38;
                    playerAchievement39 = loadPlayer.playerAchievement39;
                    playerAchievement40 = loadPlayer.playerAchievement40;
                    playerAchievement41 = loadPlayer.playerAchievement41;
                    playerAchievement42 = loadPlayer.playerAchievement42;
                    playerAchievement43 = loadPlayer.playerAchievement43;
                    playerAchievement44 = loadPlayer.playerAchievement44;
                    playerAchievement45 = loadPlayer.playerAchievement45;
                    playerAchievement46 = loadPlayer.playerAchievement46;
                    playerAchievement47 = loadPlayer.playerAchievement47;
                    playerAchievement48 = loadPlayer.playerAchievement48;
                    playerAchievement49 = loadPlayer.playerAchievement49;
                    playerAchievement50 = loadPlayer.playerAchievement50;
                    playerAchievementCount = loadPlayer.playerAchievementCount;
                    playerSelectedWeapon1 = loadPlayer.playerSelectedWeapon1;
                    playerSelectedWeapon2 = loadPlayer.playerSelectedWeapon2;
                    playerSelectedWeapon3 = loadPlayer.playerSelectedWeapon3;
                    playerSelectedWeapon4 = loadPlayer.playerSelectedWeapon4;
                    playerSelectedWeapon5 = loadPlayer.playerSelectedWeapon5;
                    playerSelectedSpecial = loadPlayer.playerSelectedSpecial;
                    playerShipsUnlocked = loadPlayer.playerShipsUnlocked;
                    playerXP = loadPlayer.playerXP;
                    playerLevelNumber = loadPlayer.playerLevelNumber;

                    #endregion

                    LoadShips();
                    LoadHulls();
                    Loading = false;
                    previousSelectTime = gameTime.TotalGameTime;
                    MenuState = 1;
                    MenuLocationX = 1;
                    MenuLocationY = 0;
                    ShipSelector = playerShip;
                    ProjectileSelector = playerSelectedWeapon1;
                    SpecialSelector = playerSelectedSpecial;
                    if (iplayerElectricProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerLaserProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerFireProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerPoisonProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerExplosiveProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerSlowProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerHealthProjectile > 0)
                        UpgradeUnlocks += 1;
                    if (iplayerEnergyProjectile > 0)
                        UpgradeUnlocks += 1;

                    AddProjectile(1);
                    AddProjectile(2);
                    AddProjectile(3);
                    AddProjectile(4);
                    AddProjectile(5);
                    AddProjectile(6);
                    AddProjectile(7);
                    AddProjectile(8);

                    for (int i = 0; i < playerShipsUnlocked; i++)
                    {
                        AddImageBackground();
                    }

                    for (int i = 0; i < (playerWeaponsCollected + 8); i++)
                    {
                        AddProjectileBackground();
                    }

                    for (int i = 0; i < playerWeaponsCollected; i++)
                    {
                        AddProjectile(i + 9);
                    }
                }

            #endregion


                if (MenuState == 1)
                {
                    if (MenuLocationY == 0)
                        UpgradesButton.Sprite.CurrentAnimation = "selected";
                    else
                        UpgradesButton.Sprite.CurrentAnimation = "default";


                    UpgradesButton.Update(gameTime);
                    ControlsUpdate(gameTime);
                    SelectionUpdate();
                    UpgradeIconsUpdate(gameTime);
                    UpdateUpgrades();
                }

                if (MenuState == 2)
                {
                    if (MenuLocationY == 0)
                        InventoryButton.Sprite.CurrentAnimation = "selected";
                    else
                        InventoryButton.Sprite.CurrentAnimation = "default";

                    InventoryButton.Update(gameTime);
                    ControlsUpdate(gameTime);
                    InventoryUpdate(gameTime);
                }

                if (MenuState == 3)
                {
                    if (MenuLocationY == 0)
                        StatsButton.Sprite.CurrentAnimation = "selected";
                    else
                        StatsButton.Sprite.CurrentAnimation = "default";

                    StatsButton.Update(gameTime);
                    ControlsUpdate(gameTime);
                    StatsUpdate();
                }

                if (MenuState == 4)
                {
                    if (MenuLocationY == 0)
                        AchievementsButton.Sprite.CurrentAnimation = "selected";
                    else
                        AchievementsButton.Sprite.CurrentAnimation = "default";

                    AchievementsButton.Update(gameTime);
                    ControlsUpdate(gameTime);
                    AchievementsUpdate(gameTime);
                }

                if (MenuState != 0 && MenuState != 4)
                {
                    AchievementTracker(gameTime);
                }
            }

            #region Timer

            if (MenuState != 0)
            {
                if (StartTimer)
                {
                    previousTimer = gameTime.TotalGameTime;
                    StartTimer = false;
                }

                if (gameTime.TotalGameTime - previousTimer > Timer)
                {
                    previousTimer = gameTime.TotalGameTime;
                    playerTimePlayedSeconds += 1;

                    if (playerTimePlayedSeconds > 59)
                    {
                        playerTimePlayedSeconds = 0;
                        playerTimePlayedMinutes += 1;
                        if (MenuState != 0)
                            Save();
                        if (playerTimePlayedMinutes > 59)
                        {
                            playerTimePlayedMinutes = 0;
                            playerTimePlayedHours += 1;
                        }
                    }

                    if (playerTimePlayedMinutes < 10)
                        ZeroOne = "0";
                    else
                        ZeroOne = "";

                    if (playerTimePlayedSeconds < 10)
                        ZeroTwo = "0";
                    else
                        ZeroTwo = "";
                }
            }

            #endregion
        }

        private void AchievementTracker(GameTime gameTime)
        {
            #region Initialize

            if (InitialTracking)
            {
                Achievement1Tracker = playerAchievement1;
                Achievement2Tracker = playerAchievement2;
                Achievement3Tracker = playerAchievement3;
                Achievement4Tracker = playerAchievement4;
                Achievement5Tracker = playerAchievement5;
                Achievement6Tracker = playerAchievement6;
                Achievement7Tracker = playerAchievement7;
                Achievement8Tracker = playerAchievement8;
                Achievement9Tracker = playerAchievement9;
                Achievement10Tracker = playerAchievement10;
                Achievement11Tracker = playerAchievement11;
                Achievement12Tracker = playerAchievement12;
                Achievement13Tracker = playerAchievement13;
                Achievement14Tracker = playerAchievement14;
                Achievement15Tracker = playerAchievement15;
                Achievement16Tracker = playerAchievement16;
                Achievement17Tracker = playerAchievement17;
                Achievement18Tracker = playerAchievement18;
                Achievement19Tracker = playerAchievement19;
                Achievement20Tracker = playerAchievement20;
                Achievement21Tracker = playerAchievement21;
                Achievement22Tracker = playerAchievement22;
                Achievement23Tracker = playerAchievement23;
                Achievement24Tracker = playerAchievement24;
                Achievement25Tracker = playerAchievement25;
                Achievement26Tracker = playerAchievement26;
                Achievement27Tracker = playerAchievement27;
                Achievement28Tracker = playerAchievement28;
                Achievement29Tracker = playerAchievement29;
                Achievement30Tracker = playerAchievement30;
                Achievement31Tracker = playerAchievement31;
                Achievement32Tracker = playerAchievement32;
                Achievement33Tracker = playerAchievement33;
                Achievement34Tracker = playerAchievement34;
                Achievement35Tracker = playerAchievement35;
                Achievement36Tracker = playerAchievement36;
                Achievement37Tracker = playerAchievement37;
                Achievement38Tracker = playerAchievement38;
                Achievement39Tracker = playerAchievement39;
                Achievement40Tracker = playerAchievement40;
                Achievement41Tracker = playerAchievement41;
                Achievement42Tracker = playerAchievement42;
                Achievement43Tracker = playerAchievement43;
                Achievement44Tracker = playerAchievement44;
                Achievement45Tracker = playerAchievement45;
                Achievement46Tracker = playerAchievement46;
                Achievement47Tracker = playerAchievement47;
                Achievement48Tracker = playerAchievement48;
                Achievement49Tracker = playerAchievement49;
                Achievement50Tracker = playerAchievement50;

                InitialTracking = false;
            }

            #endregion

            #region Animation

            if (Achievement1Tracker == false && playerAchievement1)
            {                
                if (InitialLocationAchievement1)
                {
                    Achievement1.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement1.Sprite.CurrentAnimation = "default";
                    DrawAchievement1 = true;
                    InitialLocationAchievement1 = false;
                }

                Achievement1.Update(gameTime);

                if (Achievement1.Position.Y > (screenHeight - Achievement1.Sprite.Height))
                {
                    Achievement1.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement1.Sprite.CurrentAnimation = "complete";
                    Achievement1.Sprite.velocity.Y = 0;
                    Achievement1.Sprite.acceleration.Y = 0;
                    Achievement1.Sprite.MoveX(5f);

                    if (Achievement1.Position.X > screenWidth)
                    {
                        Achievement1Tracker = true;
                        InitialLocationAchievement1 = true;
                        DrawAchievement1 = false;
                    }
                }
            }

            if (Achievement2Tracker == false && playerAchievement2)
            {
                if (InitialLocationAchievement2)
                {
                    Achievement2.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement2.Sprite.CurrentAnimation = "default";
                    DrawAchievement2 = true;
                    InitialLocationAchievement2 = false;
                }

                Achievement2.Update(gameTime);

                if (Achievement2.Position.Y > (screenHeight - Achievement2.Sprite.Height))
                {
                    Achievement2.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement2.Sprite.CurrentAnimation = "complete";
                    Achievement2.Sprite.velocity.Y = 0;
                    Achievement2.Sprite.acceleration.Y = 0;
                    Achievement2.Sprite.MoveX(5f);

                    if (Achievement2.Position.X > screenWidth)
                    {
                        Achievement2Tracker = true;
                        InitialLocationAchievement2 = true;
                        DrawAchievement2 = false;
                    }
                }
            }

            if (Achievement3Tracker == false && playerAchievement3)
            {
                if (InitialLocationAchievement3)
                {
                    Achievement3.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement3.Sprite.CurrentAnimation = "default";
                    DrawAchievement3 = true;
                    InitialLocationAchievement3 = false;
                }

                Achievement3.Update(gameTime);

                if (Achievement3.Position.Y > (screenHeight - Achievement3.Sprite.Height))
                {
                    Achievement3.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement3.Sprite.CurrentAnimation = "complete";
                    Achievement3.Sprite.velocity.Y = 0;
                    Achievement3.Sprite.acceleration.Y = 0;
                    Achievement3.Sprite.MoveX(5f);

                    if (Achievement3.Position.X > screenWidth)
                    {
                        Achievement3Tracker = true;
                        InitialLocationAchievement3 = true;
                        DrawAchievement3 = false;
                    }
                }
            }

            if (Achievement4Tracker == false && playerAchievement4)
            {
                if (InitialLocationAchievement4)
                {
                    Achievement4.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement4.Sprite.CurrentAnimation = "default";
                    DrawAchievement4 = true;
                    InitialLocationAchievement4 = false;
                }

                Achievement4.Update(gameTime);

                if (Achievement4.Position.Y > (screenHeight - Achievement4.Sprite.Height))
                {
                    Achievement4.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement4.Sprite.CurrentAnimation = "complete";
                    Achievement4.Sprite.velocity.Y = 0;
                    Achievement4.Sprite.acceleration.Y = 0;
                    Achievement4.Sprite.MoveX(5f);

                    if (Achievement4.Position.X > screenWidth)
                    {
                        Achievement4Tracker = true;
                        InitialLocationAchievement4 = true;
                        DrawAchievement4 = false;
                    }
                }
            }

            if (Achievement5Tracker == false && playerAchievement5)
            {
                if (InitialLocationAchievement5)
                {
                    Achievement5.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement5.Sprite.CurrentAnimation = "default";
                    DrawAchievement5 = true;
                    InitialLocationAchievement5 = false;
                }

                Achievement5.Update(gameTime);

                if (Achievement5.Position.Y > (screenHeight - Achievement5.Sprite.Height))
                {
                    Achievement5.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement5.Sprite.CurrentAnimation = "complete";
                    Achievement5.Sprite.velocity.Y = 0;
                    Achievement5.Sprite.acceleration.Y = 0;
                    Achievement5.Sprite.MoveX(5f);

                    if (Achievement5.Position.X > screenWidth)
                    {
                        Achievement5Tracker = true;
                        InitialLocationAchievement5 = true;
                        DrawAchievement5 = false;
                    }
                }
            }


            if (Achievement6Tracker == false && playerAchievement6)
            {
                if (InitialLocationAchievement6)
                {
                    Achievement6.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement6.Sprite.CurrentAnimation = "default";
                    DrawAchievement6 = true;
                    InitialLocationAchievement6 = false;
                }

                Achievement6.Update(gameTime);

                if (Achievement6.Position.Y > (screenHeight - Achievement6.Sprite.Height))
                {
                    Achievement6.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement6.Sprite.CurrentAnimation = "complete";
                    Achievement6.Sprite.velocity.Y = 0;
                    Achievement6.Sprite.acceleration.Y = 0;
                    Achievement6.Sprite.MoveX(5f);

                    if (Achievement6.Position.X > screenWidth)
                    {
                        Achievement6Tracker = true;
                        InitialLocationAchievement6 = true;
                        DrawAchievement6 = false;
                    }
                }
            }


            if (Achievement7Tracker == false && playerAchievement7)
            {
                if (InitialLocationAchievement7)
                {
                    Achievement7.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement7.Sprite.CurrentAnimation = "default";
                    DrawAchievement7 = true;
                    InitialLocationAchievement7 = false;
                }

                Achievement7.Update(gameTime);

                if (Achievement7.Position.Y > (screenHeight - Achievement7.Sprite.Height))
                {
                    Achievement7.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement7.Sprite.CurrentAnimation = "complete";
                    Achievement7.Sprite.velocity.Y = 0;
                    Achievement7.Sprite.acceleration.Y = 0;
                    Achievement7.Sprite.MoveX(5f);

                    if (Achievement7.Position.X > screenWidth)
                    {
                        Achievement7Tracker = true;
                        InitialLocationAchievement7 = true;
                        DrawAchievement7 = false;
                    }
                }
            }


            if (Achievement8Tracker == false && playerAchievement8)
            {
                if (InitialLocationAchievement8)
                {
                    Achievement8.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement8.Sprite.CurrentAnimation = "default";
                    DrawAchievement8 = true;
                    InitialLocationAchievement8 = false;
                }

                Achievement8.Update(gameTime);

                if (Achievement8.Position.Y > (screenHeight - Achievement8.Sprite.Height))
                {
                    Achievement8.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement8.Sprite.CurrentAnimation = "complete";
                    Achievement8.Sprite.velocity.Y = 0;
                    Achievement8.Sprite.acceleration.Y = 0;
                    Achievement8.Sprite.MoveX(5f);

                    if (Achievement8.Position.X > screenWidth)
                    {
                        Achievement8Tracker = true;
                        InitialLocationAchievement8 = true;
                        DrawAchievement8 = false;
                    }
                }
            }


            if (Achievement9Tracker == false && playerAchievement9)
            {
                if (InitialLocationAchievement9)
                {
                    Achievement9.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement9.Sprite.CurrentAnimation = "default";
                    DrawAchievement9 = true;
                    InitialLocationAchievement9 = false;
                }

                Achievement9.Update(gameTime);

                if (Achievement9.Position.Y > (screenHeight - Achievement9.Sprite.Height))
                {
                    Achievement9.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement9.Sprite.CurrentAnimation = "complete";
                    Achievement9.Sprite.velocity.Y = 0;
                    Achievement9.Sprite.acceleration.Y = 0;
                    Achievement9.Sprite.MoveX(5f);

                    if (Achievement9.Position.X > screenWidth)
                    {
                        Achievement9Tracker = true;
                        InitialLocationAchievement9 = true;
                        DrawAchievement9 = false;
                    }
                }
            }


            if (Achievement10Tracker == false && playerAchievement10)
            {
                if (InitialLocationAchievement10)
                {
                    Achievement10.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement10.Sprite.CurrentAnimation = "default";
                    DrawAchievement10 = true;
                    InitialLocationAchievement10 = false;
                }

                Achievement10.Update(gameTime);

                if (Achievement10.Position.Y > (screenHeight - Achievement10.Sprite.Height))
                {
                    Achievement10.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement10.Sprite.CurrentAnimation = "complete";
                    Achievement10.Sprite.velocity.Y = 0;
                    Achievement10.Sprite.acceleration.Y = 0;
                    Achievement10.Sprite.MoveX(5f);

                    if (Achievement10.Position.X > screenWidth)
                    {
                        Achievement10Tracker = true;
                        InitialLocationAchievement10 = true;
                        DrawAchievement10 = false;
                    }
                }
            }

            if (Achievement11Tracker == false && playerAchievement11)
            {
                if (InitialLocationAchievement11)
                {
                    Achievement11.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement11.Sprite.CurrentAnimation = "default";
                    DrawAchievement11 = true;
                    InitialLocationAchievement11 = false;
                }

                Achievement11.Update(gameTime);

                if (Achievement11.Position.Y > (screenHeight - Achievement11.Sprite.Height))
                {
                    Achievement11.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement11.Sprite.CurrentAnimation = "complete";
                    Achievement11.Sprite.velocity.Y = 0;
                    Achievement11.Sprite.acceleration.Y = 0;
                    Achievement11.Sprite.MoveX(5f);

                    if (Achievement11.Position.X > screenWidth)
                    {
                        Achievement11Tracker = true;
                        InitialLocationAchievement11 = true;
                        DrawAchievement11 = false;
                    }
                }
            }

            if (Achievement12Tracker == false && playerAchievement12)
            {
                if (InitialLocationAchievement12)
                {
                    Achievement12.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement12.Sprite.CurrentAnimation = "default";
                    DrawAchievement12 = true;
                    InitialLocationAchievement12 = false;
                }

                Achievement12.Update(gameTime);

                if (Achievement12.Position.Y > (screenHeight - Achievement12.Sprite.Height))
                {
                    Achievement12.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement12.Sprite.CurrentAnimation = "complete";
                    Achievement12.Sprite.velocity.Y = 0;
                    Achievement12.Sprite.acceleration.Y = 0;
                    Achievement12.Sprite.MoveX(5f);

                    if (Achievement12.Position.X > screenWidth)
                    {
                        Achievement12Tracker = true;
                        InitialLocationAchievement12 = true;
                        DrawAchievement12 = false;
                    }
                }
            }

            if (Achievement13Tracker == false && playerAchievement13)
            {
                if (InitialLocationAchievement13)
                {
                    Achievement13.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement13.Sprite.CurrentAnimation = "default";
                    DrawAchievement13 = true;
                    InitialLocationAchievement13 = false;
                }

                Achievement13.Update(gameTime);

                if (Achievement13.Position.Y > (screenHeight - Achievement13.Sprite.Height))
                {
                    Achievement13.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement13.Sprite.CurrentAnimation = "complete";
                    Achievement13.Sprite.velocity.Y = 0;
                    Achievement13.Sprite.acceleration.Y = 0;
                    Achievement13.Sprite.MoveX(5f);

                    if (Achievement13.Position.X > screenWidth)
                    {
                        Achievement13Tracker = true;
                        InitialLocationAchievement13 = true;
                        DrawAchievement13 = false;
                    }
                }
            }

            if (Achievement14Tracker == false && playerAchievement14)
            {
                if (InitialLocationAchievement14)
                {
                    Achievement14.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement14.Sprite.CurrentAnimation = "default";
                    DrawAchievement14 = true;
                    InitialLocationAchievement14 = false;
                }

                Achievement14.Update(gameTime);

                if (Achievement14.Position.Y > (screenHeight - Achievement14.Sprite.Height))
                {
                    Achievement14.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement14.Sprite.CurrentAnimation = "complete";
                    Achievement14.Sprite.velocity.Y = 0;
                    Achievement14.Sprite.acceleration.Y = 0;
                    Achievement14.Sprite.MoveX(5f);

                    if (Achievement14.Position.X > screenWidth)
                    {
                        Achievement14Tracker = true;
                        InitialLocationAchievement14 = true;
                        DrawAchievement14 = false;
                    }
                }
            }

            if (Achievement15Tracker == false && playerAchievement15)
            {
                if (InitialLocationAchievement15)
                {
                    Achievement15.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement15.Sprite.CurrentAnimation = "default";
                    DrawAchievement15 = true;
                    InitialLocationAchievement15 = false;
                }

                Achievement15.Update(gameTime);

                if (Achievement15.Position.Y > (screenHeight - Achievement15.Sprite.Height))
                {
                    Achievement15.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement15.Sprite.CurrentAnimation = "complete";
                    Achievement15.Sprite.velocity.Y = 0;
                    Achievement15.Sprite.acceleration.Y = 0;
                    Achievement15.Sprite.MoveX(5f);

                    if (Achievement15.Position.X > screenWidth)
                    {
                        Achievement15Tracker = true;
                        InitialLocationAchievement15 = true;
                        DrawAchievement15 = false;
                    }
                }
            }


            if (Achievement16Tracker == false && playerAchievement16)
            {
                if (InitialLocationAchievement16)
                {
                    Achievement16.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement16.Sprite.CurrentAnimation = "default";
                    DrawAchievement16 = true;
                    InitialLocationAchievement16 = false;
                }

                Achievement16.Update(gameTime);

                if (Achievement16.Position.Y > (screenHeight - Achievement16.Sprite.Height))
                {
                    Achievement16.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement16.Sprite.CurrentAnimation = "complete";
                    Achievement16.Sprite.velocity.Y = 0;
                    Achievement16.Sprite.acceleration.Y = 0;
                    Achievement16.Sprite.MoveX(5f);

                    if (Achievement16.Position.X > screenWidth)
                    {
                        Achievement16Tracker = true;
                        InitialLocationAchievement16 = true;
                        DrawAchievement16 = false;
                    }
                }
            }


            if (Achievement17Tracker == false && playerAchievement17)
            {
                if (InitialLocationAchievement17)
                {
                    Achievement17.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement17.Sprite.CurrentAnimation = "default";
                    DrawAchievement17 = true;
                    InitialLocationAchievement17 = false;
                }

                Achievement17.Update(gameTime);

                if (Achievement17.Position.Y > (screenHeight - Achievement17.Sprite.Height))
                {
                    Achievement17.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement17.Sprite.CurrentAnimation = "complete";
                    Achievement17.Sprite.velocity.Y = 0;
                    Achievement17.Sprite.acceleration.Y = 0;
                    Achievement17.Sprite.MoveX(5f);

                    if (Achievement17.Position.X > screenWidth)
                    {
                        Achievement17Tracker = true;
                        InitialLocationAchievement17 = true;
                        DrawAchievement17 = false;
                    }
                }
            }


            if (Achievement18Tracker == false && playerAchievement18)
            {
                if (InitialLocationAchievement18)
                {
                    Achievement18.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement18.Sprite.CurrentAnimation = "default";
                    DrawAchievement18 = true;
                    InitialLocationAchievement18 = false;
                }

                Achievement18.Update(gameTime);

                if (Achievement18.Position.Y > (screenHeight - Achievement18.Sprite.Height))
                {
                    Achievement18.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement18.Sprite.CurrentAnimation = "complete";
                    Achievement18.Sprite.velocity.Y = 0;
                    Achievement18.Sprite.acceleration.Y = 0;
                    Achievement18.Sprite.MoveX(5f);

                    if (Achievement18.Position.X > screenWidth)
                    {
                        Achievement18Tracker = true;
                        InitialLocationAchievement18 = true;
                        DrawAchievement18 = false;
                    }
                }
            }


            if (Achievement19Tracker == false && playerAchievement19)
            {
                if (InitialLocationAchievement19)
                {
                    Achievement19.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement19.Sprite.CurrentAnimation = "default";
                    DrawAchievement19 = true;
                    InitialLocationAchievement19 = false;
                }

                Achievement19.Update(gameTime);

                if (Achievement19.Position.Y > (screenHeight - Achievement19.Sprite.Height))
                {
                    Achievement19.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement19.Sprite.CurrentAnimation = "complete";
                    Achievement19.Sprite.velocity.Y = 0;
                    Achievement19.Sprite.acceleration.Y = 0;
                    Achievement19.Sprite.MoveX(5f);

                    if (Achievement19.Position.X > screenWidth)
                    {
                        Achievement19Tracker = true;
                        InitialLocationAchievement19 = true;
                        DrawAchievement19 = false;
                    }
                }
            }


            if (Achievement20Tracker == false && playerAchievement20)
            {
                if (InitialLocationAchievement20)
                {
                    Achievement20.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement20.Sprite.CurrentAnimation = "default";
                    DrawAchievement20 = true;
                    InitialLocationAchievement20 = false;
                }

                Achievement20.Update(gameTime);

                if (Achievement20.Position.Y > (screenHeight - Achievement20.Sprite.Height))
                {
                    Achievement20.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement20.Sprite.CurrentAnimation = "complete";
                    Achievement20.Sprite.velocity.Y = 0;
                    Achievement20.Sprite.acceleration.Y = 0;
                    Achievement20.Sprite.MoveX(5f);

                    if (Achievement20.Position.X > screenWidth)
                    {
                        Achievement20Tracker = true;
                        InitialLocationAchievement20 = true;
                        DrawAchievement20 = false;
                    }
                }
            }

            if (Achievement21Tracker == false && playerAchievement21)
            {
                if (InitialLocationAchievement21)
                {
                    Achievement21.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement21.Sprite.CurrentAnimation = "default";
                    DrawAchievement21 = true;
                    InitialLocationAchievement21 = false;
                }

                Achievement21.Update(gameTime);

                if (Achievement21.Position.Y > (screenHeight - Achievement21.Sprite.Height))
                {
                    Achievement21.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement21.Sprite.CurrentAnimation = "complete";
                    Achievement21.Sprite.velocity.Y = 0;
                    Achievement21.Sprite.acceleration.Y = 0;
                    Achievement21.Sprite.MoveX(5f);

                    if (Achievement21.Position.X > screenWidth)
                    {
                        Achievement21Tracker = true;
                        InitialLocationAchievement21 = true;
                        DrawAchievement21 = false;
                    }
                }
            }

            if (Achievement22Tracker == false && playerAchievement22)
            {
                if (InitialLocationAchievement22)
                {
                    Achievement22.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement22.Sprite.CurrentAnimation = "default";
                    DrawAchievement22 = true;
                    InitialLocationAchievement22 = false;
                }

                Achievement22.Update(gameTime);

                if (Achievement22.Position.Y > (screenHeight - Achievement22.Sprite.Height))
                {
                    Achievement22.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement22.Sprite.CurrentAnimation = "complete";
                    Achievement22.Sprite.velocity.Y = 0;
                    Achievement22.Sprite.acceleration.Y = 0;
                    Achievement22.Sprite.MoveX(5f);

                    if (Achievement22.Position.X > screenWidth)
                    {
                        Achievement22Tracker = true;
                        InitialLocationAchievement22 = true;
                        DrawAchievement22 = false;
                    }
                }
            }

            if (Achievement23Tracker == false && playerAchievement23)
            {
                if (InitialLocationAchievement23)
                {
                    Achievement23.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement23.Sprite.CurrentAnimation = "default";
                    DrawAchievement23 = true;
                    InitialLocationAchievement23 = false;
                }

                Achievement23.Update(gameTime);

                if (Achievement23.Position.Y > (screenHeight - Achievement23.Sprite.Height))
                {
                    Achievement23.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement23.Sprite.CurrentAnimation = "complete";
                    Achievement23.Sprite.velocity.Y = 0;
                    Achievement23.Sprite.acceleration.Y = 0;
                    Achievement23.Sprite.MoveX(5f);

                    if (Achievement23.Position.X > screenWidth)
                    {
                        Achievement23Tracker = true;
                        InitialLocationAchievement23 = true;
                        DrawAchievement23 = false;
                    }
                }
            }

            if (Achievement24Tracker == false && playerAchievement24)
            {
                if (InitialLocationAchievement24)
                {
                    Achievement24.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement24.Sprite.CurrentAnimation = "default";
                    DrawAchievement24 = true;
                    InitialLocationAchievement24 = false;
                }

                Achievement24.Update(gameTime);

                if (Achievement24.Position.Y > (screenHeight - Achievement24.Sprite.Height))
                {
                    Achievement24.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement24.Sprite.CurrentAnimation = "complete";
                    Achievement24.Sprite.velocity.Y = 0;
                    Achievement24.Sprite.acceleration.Y = 0;
                    Achievement24.Sprite.MoveX(5f);

                    if (Achievement24.Position.X > screenWidth)
                    {
                        Achievement24Tracker = true;
                        InitialLocationAchievement24 = true;
                        DrawAchievement24 = false;
                    }
                }
            }

            if (Achievement25Tracker == false && playerAchievement25)
            {
                if (InitialLocationAchievement25)
                {
                    Achievement25.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement25.Sprite.CurrentAnimation = "default";
                    DrawAchievement25 = true;
                    InitialLocationAchievement25 = false;
                }

                Achievement25.Update(gameTime);

                if (Achievement25.Position.Y > (screenHeight - Achievement25.Sprite.Height))
                {
                    Achievement25.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement25.Sprite.CurrentAnimation = "complete";
                    Achievement25.Sprite.velocity.Y = 0;
                    Achievement25.Sprite.acceleration.Y = 0;
                    Achievement25.Sprite.MoveX(5f);

                    if (Achievement25.Position.X > screenWidth)
                    {
                        Achievement25Tracker = true;
                        InitialLocationAchievement25 = true;
                        DrawAchievement25 = false;
                    }
                }
            }


            if (Achievement26Tracker == false && playerAchievement26)
            {
                if (InitialLocationAchievement26)
                {
                    Achievement26.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement26.Sprite.CurrentAnimation = "default";
                    DrawAchievement26 = true;
                    InitialLocationAchievement26 = false;
                }

                Achievement26.Update(gameTime);

                if (Achievement26.Position.Y > (screenHeight - Achievement26.Sprite.Height))
                {
                    Achievement26.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement26.Sprite.CurrentAnimation = "complete";
                    Achievement26.Sprite.velocity.Y = 0;
                    Achievement26.Sprite.acceleration.Y = 0;
                    Achievement26.Sprite.MoveX(5f);

                    if (Achievement26.Position.X > screenWidth)
                    {
                        Achievement26Tracker = true;
                        InitialLocationAchievement26 = true;
                        DrawAchievement26 = false;
                    }
                }
            }


            if (Achievement27Tracker == false && playerAchievement27)
            {
                if (InitialLocationAchievement27)
                {
                    Achievement27.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement27.Sprite.CurrentAnimation = "default";
                    DrawAchievement27 = true;
                    InitialLocationAchievement27 = false;
                }

                Achievement27.Update(gameTime);

                if (Achievement27.Position.Y > (screenHeight - Achievement27.Sprite.Height))
                {
                    Achievement27.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement27.Sprite.CurrentAnimation = "complete";
                    Achievement27.Sprite.velocity.Y = 0;
                    Achievement27.Sprite.acceleration.Y = 0;
                    Achievement27.Sprite.MoveX(5f);

                    if (Achievement27.Position.X > screenWidth)
                    {
                        Achievement27Tracker = true;
                        InitialLocationAchievement27 = true;
                        DrawAchievement27 = false;
                    }
                }
            }


            if (Achievement28Tracker == false && playerAchievement28)
            {
                if (InitialLocationAchievement28)
                {
                    Achievement28.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement28.Sprite.CurrentAnimation = "default";
                    DrawAchievement28 = true;
                    InitialLocationAchievement28 = false;
                }

                Achievement28.Update(gameTime);

                if (Achievement28.Position.Y > (screenHeight - Achievement28.Sprite.Height))
                {
                    Achievement28.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement28.Sprite.CurrentAnimation = "complete";
                    Achievement28.Sprite.velocity.Y = 0;
                    Achievement28.Sprite.acceleration.Y = 0;
                    Achievement28.Sprite.MoveX(5f);

                    if (Achievement28.Position.X > screenWidth)
                    {
                        Achievement28Tracker = true;
                        InitialLocationAchievement28 = true;
                        DrawAchievement28 = false;
                    }
                }
            }


            if (Achievement29Tracker == false && playerAchievement29)
            {
                if (InitialLocationAchievement29)
                {
                    Achievement29.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement29.Sprite.CurrentAnimation = "default";
                    DrawAchievement29 = true;
                    InitialLocationAchievement29 = false;
                }

                Achievement29.Update(gameTime);

                if (Achievement29.Position.Y > (screenHeight - Achievement29.Sprite.Height))
                {
                    Achievement29.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement29.Sprite.CurrentAnimation = "complete";
                    Achievement29.Sprite.velocity.Y = 0;
                    Achievement29.Sprite.acceleration.Y = 0;
                    Achievement29.Sprite.MoveX(5f);

                    if (Achievement29.Position.X > screenWidth)
                    {
                        Achievement29Tracker = true;
                        InitialLocationAchievement29 = true;
                        DrawAchievement29 = false;
                    }
                }
            }


            if (Achievement30Tracker == false && playerAchievement30)
            {
                if (InitialLocationAchievement30)
                {
                    Achievement30.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement30.Sprite.CurrentAnimation = "default";
                    DrawAchievement30 = true;
                    InitialLocationAchievement30 = false;
                }

                Achievement30.Update(gameTime);

                if (Achievement30.Position.Y > (screenHeight - Achievement30.Sprite.Height))
                {
                    Achievement30.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement30.Sprite.CurrentAnimation = "complete";
                    Achievement30.Sprite.velocity.Y = 0;
                    Achievement30.Sprite.acceleration.Y = 0;
                    Achievement30.Sprite.MoveX(5f);

                    if (Achievement30.Position.X > screenWidth)
                    {
                        Achievement30Tracker = true;
                        InitialLocationAchievement30 = true;
                        DrawAchievement30 = false;
                    }
                }
            }

            if (Achievement31Tracker == false && playerAchievement31)
            {
                if (InitialLocationAchievement31)
                {
                    Achievement31.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement31.Sprite.CurrentAnimation = "default";
                    DrawAchievement31 = true;
                    InitialLocationAchievement31 = false;
                }

                Achievement31.Update(gameTime);

                if (Achievement31.Position.Y > (screenHeight - Achievement31.Sprite.Height))
                {
                    Achievement31.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement31.Sprite.CurrentAnimation = "complete";
                    Achievement31.Sprite.velocity.Y = 0;
                    Achievement31.Sprite.acceleration.Y = 0;
                    Achievement31.Sprite.MoveX(5f);

                    if (Achievement31.Position.X > screenWidth)
                    {
                        Achievement31Tracker = true;
                        InitialLocationAchievement31 = true;
                        DrawAchievement31 = false;
                    }
                }
            }

            if (Achievement32Tracker == false && playerAchievement32)
            {
                if (InitialLocationAchievement32)
                {
                    Achievement32.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement32.Sprite.CurrentAnimation = "default";
                    DrawAchievement32 = true;
                    InitialLocationAchievement32 = false;
                }

                Achievement32.Update(gameTime);

                if (Achievement32.Position.Y > (screenHeight - Achievement32.Sprite.Height))
                {
                    Achievement32.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement32.Sprite.CurrentAnimation = "complete";
                    Achievement32.Sprite.velocity.Y = 0;
                    Achievement32.Sprite.acceleration.Y = 0;
                    Achievement32.Sprite.MoveX(5f);

                    if (Achievement32.Position.X > screenWidth)
                    {
                        Achievement32Tracker = true;
                        InitialLocationAchievement32 = true;
                        DrawAchievement32 = false;
                    }
                }
            }

            if (Achievement33Tracker == false && playerAchievement33)
            {
                if (InitialLocationAchievement33)
                {
                    Achievement33.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement33.Sprite.CurrentAnimation = "default";
                    DrawAchievement33 = true;
                    InitialLocationAchievement33 = false;
                }

                Achievement33.Update(gameTime);

                if (Achievement33.Position.Y > (screenHeight - Achievement33.Sprite.Height))
                {
                    Achievement33.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement33.Sprite.CurrentAnimation = "complete";
                    Achievement33.Sprite.velocity.Y = 0;
                    Achievement33.Sprite.acceleration.Y = 0;
                    Achievement33.Sprite.MoveX(5f);

                    if (Achievement33.Position.X > screenWidth)
                    {
                        Achievement33Tracker = true;
                        InitialLocationAchievement33 = true;
                        DrawAchievement33 = false;
                    }
                }
            }

            if (Achievement34Tracker == false && playerAchievement34)
            {
                if (InitialLocationAchievement34)
                {
                    Achievement34.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement34.Sprite.CurrentAnimation = "default";
                    DrawAchievement34 = true;
                    InitialLocationAchievement34 = false;
                }

                Achievement34.Update(gameTime);

                if (Achievement34.Position.Y > (screenHeight - Achievement34.Sprite.Height))
                {
                    Achievement34.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement34.Sprite.CurrentAnimation = "complete";
                    Achievement34.Sprite.velocity.Y = 0;
                    Achievement34.Sprite.acceleration.Y = 0;
                    Achievement34.Sprite.MoveX(5f);

                    if (Achievement34.Position.X > screenWidth)
                    {
                        Achievement34Tracker = true;
                        InitialLocationAchievement34 = true;
                        DrawAchievement34 = false;
                    }
                }
            }

            if (Achievement35Tracker == false && playerAchievement35)
            {
                if (InitialLocationAchievement35)
                {
                    Achievement35.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement35.Sprite.CurrentAnimation = "default";
                    DrawAchievement35 = true;
                    InitialLocationAchievement35 = false;
                }

                Achievement35.Update(gameTime);

                if (Achievement35.Position.Y > (screenHeight - Achievement35.Sprite.Height))
                {
                    Achievement35.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement35.Sprite.CurrentAnimation = "complete";
                    Achievement35.Sprite.velocity.Y = 0;
                    Achievement35.Sprite.acceleration.Y = 0;
                    Achievement35.Sprite.MoveX(5f);

                    if (Achievement35.Position.X > screenWidth)
                    {
                        Achievement35Tracker = true;
                        InitialLocationAchievement35 = true;
                        DrawAchievement35 = false;
                    }
                }
            }


            if (Achievement36Tracker == false && playerAchievement36)
            {
                if (InitialLocationAchievement36)
                {
                    Achievement36.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement36.Sprite.CurrentAnimation = "default";
                    DrawAchievement36 = true;
                    InitialLocationAchievement36 = false;
                }

                Achievement36.Update(gameTime);

                if (Achievement36.Position.Y > (screenHeight - Achievement36.Sprite.Height))
                {
                    Achievement36.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement36.Sprite.CurrentAnimation = "complete";
                    Achievement36.Sprite.velocity.Y = 0;
                    Achievement36.Sprite.acceleration.Y = 0;
                    Achievement36.Sprite.MoveX(5f);

                    if (Achievement36.Position.X > screenWidth)
                    {
                        Achievement36Tracker = true;
                        InitialLocationAchievement36 = true;
                        DrawAchievement36 = false;
                    }
                }
            }


            if (Achievement37Tracker == false && playerAchievement37)
            {
                if (InitialLocationAchievement37)
                {
                    Achievement37.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement37.Sprite.CurrentAnimation = "default";
                    DrawAchievement37 = true;
                    InitialLocationAchievement37 = false;
                }

                Achievement37.Update(gameTime);

                if (Achievement37.Position.Y > (screenHeight - Achievement37.Sprite.Height))
                {
                    Achievement37.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement37.Sprite.CurrentAnimation = "complete";
                    Achievement37.Sprite.velocity.Y = 0;
                    Achievement37.Sprite.acceleration.Y = 0;
                    Achievement37.Sprite.MoveX(5f);

                    if (Achievement37.Position.X > screenWidth)
                    {
                        Achievement37Tracker = true;
                        InitialLocationAchievement37 = true;
                        DrawAchievement37 = false;
                    }
                }
            }


            if (Achievement38Tracker == false && playerAchievement38)
            {
                if (InitialLocationAchievement38)
                {
                    Achievement38.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement38.Sprite.CurrentAnimation = "default";
                    DrawAchievement38 = true;
                    InitialLocationAchievement38 = false;
                }

                Achievement38.Update(gameTime);

                if (Achievement38.Position.Y > (screenHeight - Achievement38.Sprite.Height))
                {
                    Achievement38.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement38.Sprite.CurrentAnimation = "complete";
                    Achievement38.Sprite.velocity.Y = 0;
                    Achievement38.Sprite.acceleration.Y = 0;
                    Achievement38.Sprite.MoveX(5f);

                    if (Achievement38.Position.X > screenWidth)
                    {
                        Achievement38Tracker = true;
                        InitialLocationAchievement38 = true;
                        DrawAchievement38 = false;
                    }
                }
            }


            if (Achievement39Tracker == false && playerAchievement39)
            {
                if (InitialLocationAchievement39)
                {
                    Achievement39.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement39.Sprite.CurrentAnimation = "default";
                    DrawAchievement39 = true;
                    InitialLocationAchievement39 = false;
                }

                Achievement39.Update(gameTime);

                if (Achievement39.Position.Y > (screenHeight - Achievement39.Sprite.Height))
                {
                    Achievement39.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement39.Sprite.CurrentAnimation = "complete";
                    Achievement39.Sprite.velocity.Y = 0;
                    Achievement39.Sprite.acceleration.Y = 0;
                    Achievement39.Sprite.MoveX(5f);

                    if (Achievement39.Position.X > screenWidth)
                    {
                        Achievement39Tracker = true;
                        InitialLocationAchievement39 = true;
                        DrawAchievement39 = false;
                    }
                }
            }


            if (Achievement40Tracker == false && playerAchievement40)
            {
                if (InitialLocationAchievement40)
                {
                    Achievement40.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement40.Sprite.CurrentAnimation = "default";
                    DrawAchievement40 = true;
                    InitialLocationAchievement40 = false;
                }

                Achievement40.Update(gameTime);

                if (Achievement40.Position.Y > (screenHeight - Achievement40.Sprite.Height))
                {
                    Achievement40.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement40.Sprite.CurrentAnimation = "complete";
                    Achievement40.Sprite.velocity.Y = 0;
                    Achievement40.Sprite.acceleration.Y = 0;
                    Achievement40.Sprite.MoveX(5f);

                    if (Achievement40.Position.X > screenWidth)
                    {
                        Achievement40Tracker = true;
                        InitialLocationAchievement40 = true;
                        DrawAchievement40 = false;
                    }
                }
            }

            if (Achievement41Tracker == false && playerAchievement41)
            {
                if (InitialLocationAchievement41)
                {
                    Achievement41.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement41.Sprite.CurrentAnimation = "default";
                    DrawAchievement41 = true;
                    InitialLocationAchievement41 = false;
                }

                Achievement41.Update(gameTime);

                if (Achievement41.Position.Y > (screenHeight - Achievement41.Sprite.Height))
                {
                    Achievement41.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement41.Sprite.CurrentAnimation = "complete";
                    Achievement41.Sprite.velocity.Y = 0;
                    Achievement41.Sprite.acceleration.Y = 0;
                    Achievement41.Sprite.MoveX(5f);

                    if (Achievement41.Position.X > screenWidth)
                    {
                        Achievement41Tracker = true;
                        InitialLocationAchievement41 = true;
                        DrawAchievement41 = false;
                    }
                }
            }

            if (Achievement42Tracker == false && playerAchievement42)
            {
                if (InitialLocationAchievement42)
                {
                    Achievement42.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement42.Sprite.CurrentAnimation = "default";
                    DrawAchievement42 = true;
                    InitialLocationAchievement42 = false;
                }

                Achievement42.Update(gameTime);

                if (Achievement42.Position.Y > (screenHeight - Achievement42.Sprite.Height))
                {
                    Achievement42.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement42.Sprite.CurrentAnimation = "complete";
                    Achievement42.Sprite.velocity.Y = 0;
                    Achievement42.Sprite.acceleration.Y = 0;
                    Achievement42.Sprite.MoveX(5f);

                    if (Achievement42.Position.X > screenWidth)
                    {
                        Achievement42Tracker = true;
                        InitialLocationAchievement42 = true;
                        DrawAchievement42 = false;
                    }
                }
            }

            if (Achievement43Tracker == false && playerAchievement43)
            {
                if (InitialLocationAchievement43)
                {
                    Achievement43.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement43.Sprite.CurrentAnimation = "default";
                    DrawAchievement43 = true;
                    InitialLocationAchievement43 = false;
                }

                Achievement43.Update(gameTime);

                if (Achievement43.Position.Y > (screenHeight - Achievement43.Sprite.Height))
                {
                    Achievement43.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement43.Sprite.CurrentAnimation = "complete";
                    Achievement43.Sprite.velocity.Y = 0;
                    Achievement43.Sprite.acceleration.Y = 0;
                    Achievement43.Sprite.MoveX(5f);

                    if (Achievement43.Position.X > screenWidth)
                    {
                        Achievement43Tracker = true;
                        InitialLocationAchievement43 = true;
                        DrawAchievement43 = false;
                    }
                }
            }

            if (Achievement44Tracker == false && playerAchievement44)
            {
                if (InitialLocationAchievement44)
                {
                    Achievement44.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement44.Sprite.CurrentAnimation = "default";
                    DrawAchievement44 = true;
                    InitialLocationAchievement44 = false;
                }

                Achievement44.Update(gameTime);

                if (Achievement44.Position.Y > (screenHeight - Achievement44.Sprite.Height))
                {
                    Achievement44.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement44.Sprite.CurrentAnimation = "complete";
                    Achievement44.Sprite.velocity.Y = 0;
                    Achievement44.Sprite.acceleration.Y = 0;
                    Achievement44.Sprite.MoveX(5f);

                    if (Achievement44.Position.X > screenWidth)
                    {
                        Achievement44Tracker = true;
                        InitialLocationAchievement44 = true;
                        DrawAchievement44 = false;
                    }
                }
            }

            if (Achievement45Tracker == false && playerAchievement45)
            {
                if (InitialLocationAchievement45)
                {
                    Achievement45.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement45.Sprite.CurrentAnimation = "default";
                    DrawAchievement45 = true;
                    InitialLocationAchievement45 = false;
                }

                Achievement45.Update(gameTime);

                if (Achievement45.Position.Y > (screenHeight - Achievement45.Sprite.Height))
                {
                    Achievement45.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement45.Sprite.CurrentAnimation = "complete";
                    Achievement45.Sprite.velocity.Y = 0;
                    Achievement45.Sprite.acceleration.Y = 0;
                    Achievement45.Sprite.MoveX(5f);

                    if (Achievement45.Position.X > screenWidth)
                    {
                        Achievement45Tracker = true;
                        InitialLocationAchievement45 = true;
                        DrawAchievement45 = false;
                    }
                }
            }


            if (Achievement46Tracker == false && playerAchievement46)
            {
                if (InitialLocationAchievement46)
                {
                    Achievement46.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement46.Sprite.CurrentAnimation = "default";
                    DrawAchievement46 = true;
                    InitialLocationAchievement46 = false;
                }

                Achievement46.Update(gameTime);

                if (Achievement46.Position.Y > (screenHeight - Achievement46.Sprite.Height))
                {
                    Achievement46.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement46.Sprite.CurrentAnimation = "complete";
                    Achievement46.Sprite.velocity.Y = 0;
                    Achievement46.Sprite.acceleration.Y = 0;
                    Achievement46.Sprite.MoveX(5f);

                    if (Achievement46.Position.X > screenWidth)
                    {
                        Achievement46Tracker = true;
                        InitialLocationAchievement46 = true;
                        DrawAchievement46 = false;
                    }
                }
            }


            if (Achievement47Tracker == false && playerAchievement47)
            {
                if (InitialLocationAchievement47)
                {
                    Achievement47.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement47.Sprite.CurrentAnimation = "default";
                    DrawAchievement47 = true;
                    InitialLocationAchievement47 = false;
                }

                Achievement47.Update(gameTime);

                if (Achievement47.Position.Y > (screenHeight - Achievement47.Sprite.Height))
                {
                    Achievement47.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement47.Sprite.CurrentAnimation = "complete";
                    Achievement47.Sprite.velocity.Y = 0;
                    Achievement47.Sprite.acceleration.Y = 0;
                    Achievement47.Sprite.MoveX(5f);

                    if (Achievement47.Position.X > screenWidth)
                    {
                        Achievement47Tracker = true;
                        InitialLocationAchievement47 = true;
                        DrawAchievement47 = false;
                    }
                }
            }


            if (Achievement48Tracker == false && playerAchievement48)
            {
                if (InitialLocationAchievement48)
                {
                    Achievement48.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement48.Sprite.CurrentAnimation = "default";
                    DrawAchievement48 = true;
                    InitialLocationAchievement48 = false;
                }

                Achievement48.Update(gameTime);

                if (Achievement48.Position.Y > (screenHeight - Achievement48.Sprite.Height))
                {
                    Achievement48.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement48.Sprite.CurrentAnimation = "complete";
                    Achievement48.Sprite.velocity.Y = 0;
                    Achievement48.Sprite.acceleration.Y = 0;
                    Achievement48.Sprite.MoveX(5f);

                    if (Achievement48.Position.X > screenWidth)
                    {
                        Achievement48Tracker = true;
                        InitialLocationAchievement48 = true;
                        DrawAchievement48 = false;
                    }
                }
            }


            if (Achievement49Tracker == false && playerAchievement49)
            {
                if (InitialLocationAchievement49)
                {
                    Achievement49.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement49.Sprite.CurrentAnimation = "default";
                    DrawAchievement49 = true;
                    InitialLocationAchievement49 = false;
                }

                Achievement49.Update(gameTime);

                if (Achievement49.Position.Y > (screenHeight - Achievement49.Sprite.Height))
                {
                    Achievement49.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement49.Sprite.CurrentAnimation = "complete";
                    Achievement49.Sprite.velocity.Y = 0;
                    Achievement49.Sprite.acceleration.Y = 0;
                    Achievement49.Sprite.MoveX(5f);

                    if (Achievement49.Position.X > screenWidth)
                    {
                        Achievement49Tracker = true;
                        InitialLocationAchievement49 = true;
                        DrawAchievement49 = false;
                    }
                }
            }


            if (Achievement50Tracker == false && playerAchievement50)
            {
                if (InitialLocationAchievement50)
                {
                    Achievement50.Position = new Vector2(0, screenHeight);
                    Game1.Instance.AudioPlay("Achievement", 1);
                    Achievement50.Sprite.CurrentAnimation = "default";
                    DrawAchievement50 = true;
                    InitialLocationAchievement50 = false;
                }

                Achievement50.Update(gameTime);

                if (Achievement50.Position.Y > (screenHeight - Achievement50.Sprite.Height))
                {
                    Achievement50.Sprite.MoveY(-10f);
                }
                else
                {
                    Achievement50.Sprite.CurrentAnimation = "complete";
                    Achievement50.Sprite.velocity.Y = 0;
                    Achievement50.Sprite.acceleration.Y = 0;
                    Achievement50.Sprite.MoveX(5f);

                    if (Achievement50.Position.X > screenWidth)
                    {
                        Achievement50Tracker = true;
                        InitialLocationAchievement50 = true;
                        DrawAchievement50 = false;
                    }
                }
            }

            #endregion
        }

        private void InventoryUpdate(GameTime gameTime)
        {
            SelectedPosition.X = (screenWidth / 2) - (SelectedTexture.Width / 2);

            if (Painting == false)
            {
                PaintFirstTime = true;

                if (MenuLocationY > 3)
                    MenuLocationY = 0;
                if (MenuLocationY < 0)
                    MenuLocationY = 3;

                if (MenuLocationY == 1)
                {
                    SelectedPosition.Y = 150;

                    SpecialDescriptions();

                    #region Navigation

                    if (SpecialSelector != 0)
                    {
                        if (keyboardUpdate.SelectLeft)
                        {
                            SpecialSelector -= 1;
                            if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                                SpecialSelector -= 1;
                            if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                                SpecialSelector -= 1;
                            if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                                SpecialSelector -= 1;
                            if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                                SpecialSelector -= 1;
                            if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                                SpecialSelector -= 1;
                        }
                        if (keyboardUpdate.SelectRight)
                        {
                            SpecialSelector += 1;
                            if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                                SpecialSelector += 1;
                            if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                                SpecialSelector += 1;
                            if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                                SpecialSelector += 1;
                            if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                                SpecialSelector += 1;
                            if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                                SpecialSelector += 1;
                        }

                        if (SpecialSelector > 5)
                        {
                            SpecialSelector = 1;
                            if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                                SpecialSelector += 1;
                            if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                                SpecialSelector += 1;
                            if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                                SpecialSelector += 1;
                            if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                                SpecialSelector += 1;
                            if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                                SpecialSelector += 1;
                        }
                        if (SpecialSelector < 1)
                        {
                            SpecialSelector = 5;
                            if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                                SpecialSelector -= 1;
                            else if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                                SpecialSelector -= 1;
                            else if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                                SpecialSelector -= 1;
                            else if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                                SpecialSelector -= 1;
                            else if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                                SpecialSelector -= 1;
                        }

                        if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                            SpecialSelector += 1;
                        if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                            SpecialSelector += 1;
                        if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                            SpecialSelector += 1;
                        if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                            SpecialSelector += 1;
                        if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                            SpecialSelector += 1;

                        if (SpecialSelector > 5)
                        {
                            SpecialSelector = 1;
                            if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                                SpecialSelector += 1;
                            if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                                SpecialSelector += 1;
                            if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                                SpecialSelector += 1;
                            if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                                SpecialSelector += 1;
                            if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                                SpecialSelector += 1;
                        }
                        if (SpecialSelector < 1)
                        {
                            SpecialSelector = 5;
                            if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                                SpecialSelector -= 1;
                            else if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                                SpecialSelector -= 1;
                            else if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                                SpecialSelector -= 1;
                            else if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                                SpecialSelector -= 1;
                            else if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                                SpecialSelector -= 1;
                        }

                        if (iplayerHealingSpecial == 0 && SpecialSelector == 1)
                            SpecialSelector += 1;
                        if (iplayerLaserSpecial == 0 && SpecialSelector == 2)
                            SpecialSelector += 1;
                        if (iplayerMoneySpecial == 0 && SpecialSelector == 3)
                            SpecialSelector += 1;
                        if (iplayerShieldSpecial == 0 && SpecialSelector == 4)
                            SpecialSelector += 1;
                        if (iplayerTimeStopSpecial == 0 && SpecialSelector == 5)
                            SpecialSelector += 1;
                    }

                    #endregion

                    if (Select)
                    {
                        if (SpecialSelector != 0)
                        {
                            Game1.Instance.AudioPlay("Selected", 1);
                            playerSelectedSpecial = SpecialSelector;
                        }
                        else
                            Game1.Instance.AudioPlay("Unavailable", 1);
                    }
                }

                #region Special Update

                if (SpecialSelector == 0)
                {
                    HealingSpecial.Position = new Vector2((screenWidth / 2) - (HealingSpecial.Sprite.Width / 2) - (92 * 2), 150);
                    if (iplayerHealingSpecial > 0)
                        SpecialSelector = 1;
                    if (iplayerLaserSpecial > 0)
                        SpecialSelector = 2;
                    if (iplayerShieldSpecial > 0)
                        SpecialSelector = 4;
                    if (iplayerTimeStopSpecial > 0)
                        SpecialSelector = 5;
                    if (iplayerMoneySpecial > 0)
                        SpecialSelector = 3;
                }
                else
                    HealingSpecial.Position = new Vector2((screenWidth / 2) - (HealingSpecial.Sprite.Width / 2) - (92 * (SpecialSelector - 1)), 150);

                if (iplayerHealingSpecial > 0)
                    HealingSpecial.Sprite.CurrentAnimation = "purchased";
                if (iplayerLaserSpecial > 0)
                    LaserSpecial.Sprite.CurrentAnimation = "purchased";
                if (iplayerMoneySpecial > 0)
                    MoneySpecial.Sprite.CurrentAnimation = "purchased";
                if (iplayerShieldSpecial > 0)
                    ShieldSpecial.Sprite.CurrentAnimation = "purchased";
                if (iplayerTimeStopSpecial > 0)
                    TimeStopSpecial.Sprite.CurrentAnimation = "purchased";

                if (playerSelectedSpecial == 1)
                    HealingSpecial.Sprite.CurrentAnimation = "complete";
                else if (playerSelectedSpecial == 2)
                    LaserSpecial.Sprite.CurrentAnimation = "complete";
                else if (playerSelectedSpecial == 3)
                    MoneySpecial.Sprite.CurrentAnimation = "complete";
                else if (playerSelectedSpecial == 4)
                    ShieldSpecial.Sprite.CurrentAnimation = "complete";
                else if (playerSelectedSpecial == 5)
                    TimeStopSpecial.Sprite.CurrentAnimation = "complete";

                LaserSpecial.Position = new Vector2(HealingSpecial.Position.X + (92), HealingSpecial.Position.Y);
                MoneySpecial.Position = new Vector2(HealingSpecial.Position.X + (92 * 2), HealingSpecial.Position.Y);
                ShieldSpecial.Position = new Vector2(HealingSpecial.Position.X + (92 * 3), HealingSpecial.Position.Y);
                TimeStopSpecial.Position = new Vector2(HealingSpecial.Position.X + (92 * 4), HealingSpecial.Position.Y);

                HealingSpecial.Update(gameTime);
                LaserProjectile.Update(gameTime);
                MoneySpecial.Update(gameTime);
                ShieldSpecial.Update(gameTime);
                TimeStopSpecial.Update(gameTime);

                #endregion

                if (MenuLocationY == 2)
                {
                    SelectedPosition.Y = 250;

                    ProjectileDescriptions();

                    #region Navigation

                    if (keyboardUpdate.SelectLeft)
                    {
                        ProjectileSelector -= 1;
                        if (ProjectileSelector == 8 && iplayerEnergyProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 7 && iplayerHealthProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 6 && iplayerSlowProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 5 && iplayerExplosiveProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 4 && iplayerPoisonProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 3 && iplayerFireProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 2 && iplayerLaserProjectile == 0)
                            ProjectileSelector -= 1;
                        if (ProjectileSelector == 1 && iplayerElectricProjectile == 0)
                            ProjectileSelector -= 1;
                    }
                    if (keyboardUpdate.SelectRight)
                    {
                        ProjectileSelector += 1;
                        if (ProjectileSelector == 1 && iplayerElectricProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 2 && iplayerLaserProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 3 && iplayerFireProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 4 && iplayerPoisonProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 5 && iplayerExplosiveProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 6 && iplayerSlowProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 7 && iplayerHealthProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 8 && iplayerEnergyProjectile == 0)
                            ProjectileSelector += 1;
                    }

                    if (ProjectileSelector > playerWeaponsCollected + 8)
                    {
                        ProjectileSelector = 1;
                        if (ProjectileSelector == 1 && iplayerElectricProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 2 && iplayerLaserProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 3 && iplayerFireProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 4 && iplayerPoisonProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 5 && iplayerExplosiveProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 6 && iplayerSlowProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 7 && iplayerHealthProjectile == 0)
                            ProjectileSelector += 1;
                        if (ProjectileSelector == 8 && iplayerEnergyProjectile == 0)
                            ProjectileSelector += 1;
                    }
                    if (ProjectileSelector < 1)
                    {
                        ProjectileSelector = playerWeaponsCollected + 8;
                    }

                    #endregion

                    #region Weapon Select

                    if (Select)
                    {
                        Game1.Instance.AudioPlay("Selected", 1);

                        if (bplayerQuintupleShot)
                        {
                            playerSelectedWeapon5 = playerSelectedWeapon4;
                            playerSelectedWeapon4 = playerSelectedWeapon3;
                            playerSelectedWeapon3 = playerSelectedWeapon2;
                            playerSelectedWeapon2 = playerSelectedWeapon1;
                            playerSelectedWeapon1 = ProjectileSelector;
                        }
                        else
                            if (bplayerQuadShot)
                            {
                                playerSelectedWeapon4 = playerSelectedWeapon3;
                                playerSelectedWeapon3 = playerSelectedWeapon2;
                                playerSelectedWeapon2 = playerSelectedWeapon1;
                                playerSelectedWeapon1 = ProjectileSelector;
                            }
                            else
                                if (bplayerTripleShot)
                                {
                                    playerSelectedWeapon3 = playerSelectedWeapon2;
                                    playerSelectedWeapon2 = playerSelectedWeapon1;
                                    playerSelectedWeapon1 = ProjectileSelector;
                                }
                                else
                                    if (bplayerDoubleShot)
                                    {
                                        playerSelectedWeapon2 = playerSelectedWeapon1;
                                        playerSelectedWeapon1 = ProjectileSelector;
                                    }
                                    else
                                        playerSelectedWeapon1 = ProjectileSelector;

                    }

                    #endregion
                }

                #region Ship Selector

                if (MenuLocationY == 3)
                {
                    SelectedPosition.Y = 350;
                    if (keyboardUpdate.SelectLeft)
                    {
                        ShipSelector -= 1;
                    }
                    if (keyboardUpdate.SelectRight)
                    {
                        ShipSelector += 1;
                    }

                    if (ShipSelector > playerShipsUnlocked)
                    {
                        ShipSelector = 1;
                    }
                    if (ShipSelector < 1)
                    {
                        ShipSelector = playerShipsUnlocked;
                    }

                    if (Select)
                    {
                        Game1.Instance.AudioPlay("Selected", 1);
                        playerShip = ShipSelector;
                        MenuLocationX = 1;
                        MenuLocationY = 1;
                        LoadShips();
                        LoadHulls();
                        Painting = true;

                        #region Color Menu Location

                        if (playerRedValue == 127 && playerGreenValue == 127 && playerBlueValue == 127)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 255 && playerBlueValue == 255)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 191 && playerBlueValue == 255)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 191 && playerBlueValue == 255)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 191 && playerBlueValue == 191)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 255 && playerBlueValue == 191)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 255 && playerBlueValue == 191)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 1;
                        }
                        else if (playerRedValue == 223 && playerGreenValue == 223 && playerBlueValue == 223)
                        {
                            MenuLocationX = 1;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 111 && playerGreenValue == 111 && playerBlueValue == 111)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 127 && playerGreenValue == 255 && playerBlueValue == 255)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 127 && playerGreenValue == 127 && playerBlueValue == 255)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 127 && playerBlueValue == 255)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 127 && playerBlueValue == 127)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 255 && playerBlueValue == 127)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 127 && playerGreenValue == 255 && playerBlueValue == 127)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 2;
                        }
                        else if (playerRedValue == 207 && playerGreenValue == 207 && playerBlueValue == 207)
                        {
                            MenuLocationX = 1;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 95 && playerGreenValue == 95 && playerBlueValue == 95)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 255 && playerBlueValue == 255)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 63 && playerBlueValue == 255)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 63 && playerBlueValue == 255)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 63 && playerBlueValue == 63)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 255 && playerBlueValue == 63)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 255 && playerBlueValue == 63)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 3;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 191 && playerBlueValue == 191)
                        {
                            MenuLocationX = 1;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 79 && playerGreenValue == 79 && playerBlueValue == 79)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 255 && playerBlueValue == 255)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 0 && playerBlueValue == 255)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 252 && playerGreenValue == 0 && playerBlueValue == 252)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 0 && playerBlueValue == 0)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 255 && playerGreenValue == 255 && playerBlueValue == 0)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 255 && playerBlueValue == 0)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 4;
                        }
                        else if (playerRedValue == 175 && playerGreenValue == 175 && playerBlueValue == 175)
                        {
                            MenuLocationX = 1;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 63 && playerBlueValue == 63)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 191 && playerBlueValue == 191)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 0 && playerBlueValue == 191)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 0 && playerBlueValue == 191)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 0 && playerBlueValue == 0)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 191 && playerGreenValue == 191 && playerBlueValue == 0)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 191 && playerBlueValue == 0)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 5;
                        }
                        else if (playerRedValue == 159 && playerGreenValue == 159 && playerBlueValue == 159)
                        {
                            MenuLocationX = 1;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 47 && playerGreenValue == 47 && playerBlueValue == 47)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 127 && playerBlueValue == 127)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 0 && playerBlueValue == 127)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 127 && playerGreenValue == 0 && playerBlueValue == 127)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 127 && playerGreenValue == 0 && playerBlueValue == 0)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 127 && playerGreenValue == 127 && playerBlueValue == 0)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 127 && playerBlueValue == 0)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 6;
                        }
                        else if (playerRedValue == 143 && playerGreenValue == 143 && playerBlueValue == 143)
                        {
                            MenuLocationX = 1;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 9 && playerGreenValue == 9 && playerBlueValue == 9)
                        {
                            MenuLocationX = 2;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 63 && playerBlueValue == 63)
                        {
                            MenuLocationX = 3;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 0 && playerBlueValue == 63)
                        {
                            MenuLocationX = 4;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 0 && playerBlueValue == 63)
                        {
                            MenuLocationX = 5;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 0 && playerBlueValue == 0)
                        {
                            MenuLocationX = 6;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 63 && playerGreenValue == 63 && playerBlueValue == 0)
                        {
                            MenuLocationX = 7;
                            MenuLocationY = 7;
                        }
                        else if (playerRedValue == 0 && playerGreenValue == 63 && playerBlueValue == 0)
                        {
                            MenuLocationX = 8;
                            MenuLocationY = 7;
                        }

                        #endregion
                    }
                }

                #endregion
            }

            for (int i = 0; i < ProjectileBackground.Count; i++)
            {
                ProjectileBackground[i].Update(gameTime);

                if (i == 0)
                {
                    ProjectileBackground[i].Position = new Vector2(((screenWidth / 2) - (ProjectileBackground[i].Sprite.Width / 2) - (92 * (ProjectileSelector - 1))), 250);
                }
                else
                    ProjectileBackground[i].Position = new Vector2(ProjectileBackground[0].Position.X + (92 * i), 250);

                if (playerSelectedWeapon1 == i + 1 || playerSelectedWeapon2 == i + 1 || playerSelectedWeapon3 == i + 1 || playerSelectedWeapon4 == i + 1 || playerSelectedWeapon5 == i + 1)
                    ProjectileBackground[i].Sprite.CurrentAnimation = "selected";
                else
                    ProjectileBackground[i].Sprite.CurrentAnimation = "default";
            }

            for (int i = 0; i < Projectile.Count; i++)
            {
                Projectile[i].Update(gameTime, Vector2.Zero);
                Projectile[i].projectile.Position = new Vector2((ProjectileBackground[i].Position.X + (ProjectileBackground[i].Sprite.Width / 2)) - (Projectile[i].projectile.Sprite.Width / 2),
                                                                (ProjectileBackground[i].Position.Y + (ProjectileBackground[i].Sprite.Height / 2)) - (Projectile[i].projectile.Sprite.Height / 2));
            }

            #region Update Ship Position

            if (UpdateShipPosition)
            {
                if (ShipSelector == 1)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2));
                if (ShipSelector == 2)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92));
                if (ShipSelector == 3)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 2));
                if (ShipSelector == 4)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 3));
                if (ShipSelector == 5)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 4));
                if (ShipSelector == 6)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 5));
                if (ShipSelector == 7)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 6));
                if (ShipSelector == 8)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 7));
                if (ShipSelector == 9)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 8));
                if (ShipSelector == 10)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 9));
                if (ShipSelector == 11)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 10));
                if (ShipSelector == 12)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 11));
                if (ShipSelector == 13)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 12));
                if (ShipSelector == 14)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 13));
                if (ShipSelector == 15)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 14));
                if (ShipSelector == 16)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 15));
                if (ShipSelector == 17)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 16));
                if (ShipSelector == 18)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 17));
                if (ShipSelector == 19)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 18));
                if (ShipSelector == 20)
                    ShipLocationIndex = ((screenWidth / 2) - (Ship1.Sprite.Width / 2) - (92 * 19));

                Ship1.Sprite.SetPosX(ShipLocationIndex);

                Ship1.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship2.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship3.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship4.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship5.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship6.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship7.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship8.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship9.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship10.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship11.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship12.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship13.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship14.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship15.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship16.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship17.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship18.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship19.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                Ship20.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);

                Ship1.Update(gameTime);
                Hull1.Update(gameTime);
                Ship2.Update(gameTime);
                Hull2.Update(gameTime);
                Ship3.Update(gameTime);
                Hull3.Update(gameTime);
                Ship4.Update(gameTime);
                Hull4.Update(gameTime);
                Ship5.Update(gameTime);
                Hull5.Update(gameTime);
                Ship6.Update(gameTime);
                Hull6.Update(gameTime);
                Ship7.Update(gameTime);
                Hull7.Update(gameTime);
                Ship8.Update(gameTime);
                Hull8.Update(gameTime);
                Ship9.Update(gameTime);
                Hull9.Update(gameTime);
                Ship10.Update(gameTime);
                Hull10.Update(gameTime);
                Ship11.Update(gameTime);
                Hull11.Update(gameTime);
                Ship12.Update(gameTime);
                Hull12.Update(gameTime);
                Ship13.Update(gameTime);
                Hull13.Update(gameTime);
                Ship14.Update(gameTime);
                Hull14.Update(gameTime);
                Ship15.Update(gameTime);
                Hull15.Update(gameTime);
                Ship16.Update(gameTime);
                Hull16.Update(gameTime);
                Ship17.Update(gameTime);
                Hull17.Update(gameTime);
                Ship18.Update(gameTime);
                Hull18.Update(gameTime);
                Ship19.Update(gameTime);
                Hull19.Update(gameTime);
                Ship20.Update(gameTime);
                Hull20.Update(gameTime);

                Ship2.Position = new Vector2(Ship1.Position.X + (92), Ship1.Position.Y);
                Ship3.Position = new Vector2(Ship1.Position.X + (92 * 2), Ship1.Position.Y);
                Ship4.Position = new Vector2(Ship1.Position.X + (92 * 3), Ship1.Position.Y);
                Ship5.Position = new Vector2(Ship1.Position.X + (92 * 4), Ship1.Position.Y);
                Ship6.Position = new Vector2(Ship1.Position.X + (92 * 5), Ship1.Position.Y);
                Ship7.Position = new Vector2(Ship1.Position.X + (92 * 6), Ship1.Position.Y);
                Ship8.Position = new Vector2(Ship1.Position.X + (92 * 7), Ship1.Position.Y);
                Ship9.Position = new Vector2(Ship1.Position.X + (92 * 8), Ship1.Position.Y);
                Ship10.Position = new Vector2(Ship1.Position.X + (92 * 9), Ship1.Position.Y);
                Ship11.Position = new Vector2(Ship1.Position.X + (92 * 10), Ship1.Position.Y);
                Ship12.Position = new Vector2(Ship1.Position.X + (92 * 11), Ship1.Position.Y);
                Ship13.Position = new Vector2(Ship1.Position.X + (92 * 12), Ship1.Position.Y);
                Ship14.Position = new Vector2(Ship1.Position.X + (92 * 13), Ship1.Position.Y);
                Ship15.Position = new Vector2(Ship1.Position.X + (92 * 14), Ship1.Position.Y);
                Ship16.Position = new Vector2(Ship1.Position.X + (92 * 15), Ship1.Position.Y);
                Ship17.Position = new Vector2(Ship1.Position.X + (92 * 16), Ship1.Position.Y);
                Ship18.Position = new Vector2(Ship1.Position.X + (92 * 17), Ship1.Position.Y);
                Ship19.Position = new Vector2(Ship1.Position.X + (92 * 18), Ship1.Position.Y);
                Ship20.Position = new Vector2(Ship1.Position.X + (92 * 19), Ship1.Position.Y);

                for (int i = ShipBackground.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                        ShipBackground[i].Position = Ship1.Position;
                    else
                        ShipBackground[i].Position = new Vector2(Ship1.Position.X + (92 * i), Ship1.Position.Y);

                    if (i + 1 == playerShip)
                        ShipBackground[i].Sprite.CurrentAnimation = "selected";
                    else
                        ShipBackground[i].Sprite.CurrentAnimation = "default";
                }

                if (playerShipsUnlocked <= 19)
                    Ship20.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 18)
                    Ship19.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 17)
                    Ship18.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 16)
                    Ship17.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 15)
                    Ship16.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 14)
                    Ship15.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 13)
                    Ship14.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 12)
                    Ship13.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 11)
                    Ship12.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 10)
                    Ship11.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 9)
                    Ship10.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 8)
                    Ship9.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 7)
                    Ship8.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 6)
                    Ship7.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 5)
                    Ship6.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 4)
                    Ship5.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 3)
                    Ship4.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 2)
                    Ship3.Sprite.SetPosY(screenHeight * 2);
                if (playerShipsUnlocked <= 1)
                    Ship2.Sprite.SetPosY(screenHeight * 2);

                Hull1.Position = Ship1.Position;
                Hull2.Position = Ship2.Position;
                Hull3.Position = Ship3.Position;
                Hull4.Position = Ship4.Position;
                Hull5.Position = Ship5.Position;
                Hull6.Position = Ship6.Position;
                Hull7.Position = Ship7.Position;
                Hull8.Position = Ship8.Position;
                Hull9.Position = Ship9.Position;
                Hull10.Position = Ship10.Position;
                Hull11.Position = Ship11.Position;
                Hull12.Position = Ship12.Position;
                Hull13.Position = Ship13.Position;
                Hull14.Position = Ship14.Position;
                Hull15.Position = Ship15.Position;
                Hull16.Position = Ship16.Position;
                Hull17.Position = Ship17.Position;
                Hull18.Position = Ship18.Position;
                Hull19.Position = Ship19.Position;
                Hull20.Position = Ship20.Position;
            }
            else
                if (UpdateShipPosition == false)
                {
                    #region Update

                    if (ShipSelector == 1)
                    {
                        Ship1.Update(gameTime);
                        Hull1.Update(gameTime);
                        Ship2.Update(gameTime);
                        Hull2.Update(gameTime);
                        Ship3.Update(gameTime);
                        Hull3.Update(gameTime);
                    }
                    if (ShipSelector == 2)
                    {
                        Ship1.Update(gameTime);
                        Hull1.Update(gameTime);
                        Ship2.Update(gameTime);
                        Hull2.Update(gameTime);
                        Ship3.Update(gameTime);
                        Hull3.Update(gameTime);
                        Ship4.Update(gameTime);
                        Hull4.Update(gameTime);
                    }
                    if (ShipSelector == 3)
                    {
                        Ship1.Update(gameTime);
                        Hull1.Update(gameTime);
                        Ship2.Update(gameTime);
                        Hull2.Update(gameTime);
                        Ship3.Update(gameTime);
                        Hull3.Update(gameTime);
                        Ship4.Update(gameTime);
                        Hull4.Update(gameTime);
                        Ship5.Update(gameTime);
                        Hull5.Update(gameTime);
                    }
                    if (ShipSelector == 4)
                    {
                        Ship2.Update(gameTime);
                        Hull2.Update(gameTime);
                        Ship3.Update(gameTime);
                        Hull3.Update(gameTime);
                        Ship4.Update(gameTime);
                        Hull4.Update(gameTime);
                        Ship5.Update(gameTime);
                        Hull5.Update(gameTime);
                        Ship6.Update(gameTime);
                        Hull6.Update(gameTime);
                    }
                    if (ShipSelector == 5)
                    {
                        Ship3.Update(gameTime);
                        Hull3.Update(gameTime);
                        Ship4.Update(gameTime);
                        Hull4.Update(gameTime);
                        Ship5.Update(gameTime);
                        Hull5.Update(gameTime);
                        Ship6.Update(gameTime);
                        Hull6.Update(gameTime);
                        Ship7.Update(gameTime);
                        Hull7.Update(gameTime);
                    }
                    if (ShipSelector == 6)
                    {
                        Ship4.Update(gameTime);
                        Hull4.Update(gameTime);
                        Ship5.Update(gameTime);
                        Hull5.Update(gameTime);
                        Ship6.Update(gameTime);
                        Hull6.Update(gameTime);
                        Ship7.Update(gameTime);
                        Hull7.Update(gameTime);
                        Ship8.Update(gameTime);
                        Hull8.Update(gameTime);
                    }
                    if (ShipSelector == 7)
                    {
                        Ship5.Update(gameTime);
                        Hull5.Update(gameTime);
                        Ship6.Update(gameTime);
                        Hull6.Update(gameTime);
                        Ship7.Update(gameTime);
                        Hull7.Update(gameTime);
                        Ship8.Update(gameTime);
                        Hull8.Update(gameTime);
                        Ship9.Update(gameTime);
                        Hull9.Update(gameTime);
                    }
                    if (ShipSelector == 8)
                    {
                        Ship6.Update(gameTime);
                        Hull6.Update(gameTime);
                        Ship7.Update(gameTime);
                        Hull7.Update(gameTime);
                        Ship8.Update(gameTime);
                        Hull8.Update(gameTime);
                        Ship9.Update(gameTime);
                        Hull9.Update(gameTime);
                        Ship10.Update(gameTime);
                        Hull10.Update(gameTime);
                    }
                    if (ShipSelector == 9)
                    {
                        Ship7.Update(gameTime);
                        Hull7.Update(gameTime);
                        Ship8.Update(gameTime);
                        Hull8.Update(gameTime);
                        Ship9.Update(gameTime);
                        Hull9.Update(gameTime);
                        Ship10.Update(gameTime);
                        Hull10.Update(gameTime);
                        Ship11.Update(gameTime);
                        Hull11.Update(gameTime);
                    }
                    if (ShipSelector == 10)
                    {
                        Ship8.Update(gameTime);
                        Hull8.Update(gameTime);
                        Ship9.Update(gameTime);
                        Hull9.Update(gameTime);
                        Ship10.Update(gameTime);
                        Hull10.Update(gameTime);
                        Ship11.Update(gameTime);
                        Hull11.Update(gameTime);
                        Ship12.Update(gameTime);
                        Hull12.Update(gameTime);
                    }
                    if (ShipSelector == 11)
                    {
                        Ship9.Update(gameTime);
                        Hull9.Update(gameTime);
                        Ship10.Update(gameTime);
                        Hull10.Update(gameTime);
                        Ship11.Update(gameTime);
                        Hull11.Update(gameTime);
                        Ship12.Update(gameTime);
                        Hull12.Update(gameTime);
                        Ship13.Update(gameTime);
                        Hull13.Update(gameTime);
                    }
                    if (ShipSelector == 12)
                    {
                        Ship10.Update(gameTime);
                        Hull10.Update(gameTime);
                        Ship11.Update(gameTime);
                        Hull11.Update(gameTime);
                        Ship12.Update(gameTime);
                        Hull12.Update(gameTime);
                        Ship13.Update(gameTime);
                        Hull13.Update(gameTime);
                        Ship14.Update(gameTime);
                        Hull14.Update(gameTime);
                    }
                    if (ShipSelector == 13)
                    {
                        Ship11.Update(gameTime);
                        Hull11.Update(gameTime);
                        Ship12.Update(gameTime);
                        Hull12.Update(gameTime);
                        Ship13.Update(gameTime);
                        Hull13.Update(gameTime);
                        Ship14.Update(gameTime);
                        Hull14.Update(gameTime);
                        Ship15.Update(gameTime);
                        Hull15.Update(gameTime);
                    }
                    if (ShipSelector == 14)
                    {
                        Ship12.Update(gameTime);
                        Hull12.Update(gameTime);
                        Ship13.Update(gameTime);
                        Hull13.Update(gameTime);
                        Ship14.Update(gameTime);
                        Hull14.Update(gameTime);
                        Ship15.Update(gameTime);
                        Hull15.Update(gameTime);
                        Ship16.Update(gameTime);
                        Hull16.Update(gameTime);
                    }
                    if (ShipSelector == 15)
                    {
                        Ship13.Update(gameTime);
                        Hull13.Update(gameTime);
                        Ship14.Update(gameTime);
                        Hull14.Update(gameTime);
                        Ship15.Update(gameTime);
                        Hull15.Update(gameTime);
                        Ship16.Update(gameTime);
                        Hull16.Update(gameTime);
                        Ship17.Update(gameTime);
                        Hull17.Update(gameTime);
                    }
                    if (ShipSelector == 16)
                    {
                        Ship14.Update(gameTime);
                        Hull14.Update(gameTime);
                        Ship15.Update(gameTime);
                        Hull15.Update(gameTime);
                        Ship16.Update(gameTime);
                        Hull16.Update(gameTime);
                        Ship17.Update(gameTime);
                        Hull17.Update(gameTime);
                        Ship18.Update(gameTime);
                        Hull18.Update(gameTime);
                    }
                    if (ShipSelector == 17)
                    {
                        Ship15.Update(gameTime);
                        Hull15.Update(gameTime);
                        Ship16.Update(gameTime);
                        Hull16.Update(gameTime);
                        Ship17.Update(gameTime);
                        Hull17.Update(gameTime);
                        Ship18.Update(gameTime);
                        Hull18.Update(gameTime);
                        Ship19.Update(gameTime);
                        Hull19.Update(gameTime);
                    }
                    if (ShipSelector == 18)
                    {
                        Ship16.Update(gameTime);
                        Hull16.Update(gameTime);
                        Ship17.Update(gameTime);
                        Hull17.Update(gameTime);
                        Ship18.Update(gameTime);
                        Hull18.Update(gameTime);
                        Ship19.Update(gameTime);
                        Hull19.Update(gameTime);
                        Ship20.Update(gameTime);
                        Hull20.Update(gameTime);
                    }
                    if (ShipSelector == 19)
                    {
                        Ship17.Update(gameTime);
                        Hull17.Update(gameTime);
                        Ship18.Update(gameTime);
                        Hull18.Update(gameTime);
                        Ship19.Update(gameTime);
                        Hull19.Update(gameTime);
                        Ship20.Update(gameTime);
                        Hull20.Update(gameTime);
                    }
                    if (ShipSelector == 20)
                    {
                        Ship18.Update(gameTime);
                        Hull18.Update(gameTime);
                        Ship19.Update(gameTime);
                        Hull19.Update(gameTime);
                        Ship20.Update(gameTime);
                        Hull20.Update(gameTime);
                    }

                    #endregion
                }

            #endregion

            #region Painting

            if (Painting)
            {
                player.Update(gameTime);
                playerHull.Update(gameTime);

                player.Position = new Vector2((screenWidth / 2) - (player.Sprite.Width / 2), 50);
                playerHull.Position = player.Position;

                PalletPosition = new Vector2((screenWidth / 2) - (ColorPallet.Width / 2), 150);

                #region Navigation

                if (MenuLocationX > 8)
                    MenuLocationX = 1;
                if (MenuLocationX < 1)
                    MenuLocationX = 8;

                if (MenuLocationY > 7)
                    MenuLocationY = 1;
                if (MenuLocationY < 1)
                    MenuLocationY = 7;

                if (MenuLocationX == 1)
                    ColorSelectPosition.X = (PalletPosition.X + 3);
                if (MenuLocationX == 2)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + (ColorSelectTexture.Width + 2);
                if (MenuLocationX == 3)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + ((ColorSelectTexture.Width + 2) * 2);
                if (MenuLocationX == 4)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + ((ColorSelectTexture.Width + 2) * 3);
                if (MenuLocationX == 5)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + ((ColorSelectTexture.Width + 2) * 4);
                if (MenuLocationX == 6)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + ((ColorSelectTexture.Width + 2) * 5);
                if (MenuLocationX == 7)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + ((ColorSelectTexture.Width + 2) * 6);
                if (MenuLocationX == 8)
                    ColorSelectPosition.X = (PalletPosition.X + 3) + ((ColorSelectTexture.Width + 2) * 7);


                if (MenuLocationY == 1)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3);
                if (MenuLocationY == 2)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3) + (ColorSelectTexture.Height + 2);
                if (MenuLocationY == 3)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3) + ((ColorSelectTexture.Height + 2) * 2);
                if (MenuLocationY == 4)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3) + ((ColorSelectTexture.Height + 2) * 3);
                if (MenuLocationY == 5)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3) + ((ColorSelectTexture.Height + 2) * 4);
                if (MenuLocationY == 6)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3) + ((ColorSelectTexture.Height + 2) * 5);
                if (MenuLocationY == 7)
                    ColorSelectPosition.Y = (PalletPosition.Y + 3) + ((ColorSelectTexture.Height + 2) * 6);

                #endregion

                #region Color Picker

                if (MenuLocationX == 1 && MenuLocationY == 1)
                    ColorPicker = new Color(254, 254, 254);
                if (MenuLocationX == 2 && MenuLocationY == 1)
                    ColorPicker = new Color(127, 127, 127);
                if (MenuLocationX == 3 && MenuLocationY == 1)
                    ColorPicker = new Color(191, 255, 255);
                if (MenuLocationX == 4 && MenuLocationY == 1)
                    ColorPicker = new Color(191, 191, 255);
                if (MenuLocationX == 5 && MenuLocationY == 1)
                    ColorPicker = new Color(255, 191, 255);
                if (MenuLocationX == 6 && MenuLocationY == 1)
                    ColorPicker = new Color(255, 191, 191);
                if (MenuLocationX == 7 && MenuLocationY == 1)
                    ColorPicker = new Color(255, 255, 191);
                if (MenuLocationX == 8 && MenuLocationY == 1)
                    ColorPicker = new Color(191, 255, 191);

                if (MenuLocationX == 1 && MenuLocationY == 2)
                    ColorPicker = new Color(223, 223, 223);
                if (MenuLocationX == 2 && MenuLocationY == 2)
                    ColorPicker = new Color(111, 111, 111);
                if (MenuLocationX == 3 && MenuLocationY == 2)
                    ColorPicker = new Color(127, 255, 255);
                if (MenuLocationX == 4 && MenuLocationY == 2)
                    ColorPicker = new Color(127, 127, 255);
                if (MenuLocationX == 5 && MenuLocationY == 2)
                    ColorPicker = new Color(255, 127, 255);
                if (MenuLocationX == 6 && MenuLocationY == 2)
                    ColorPicker = new Color(255, 127, 127);
                if (MenuLocationX == 7 && MenuLocationY == 2)
                    ColorPicker = new Color(255, 255, 127);
                if (MenuLocationX == 8 && MenuLocationY == 2)
                    ColorPicker = new Color(127, 255, 127);

                if (MenuLocationX == 1 && MenuLocationY == 3)
                    ColorPicker = new Color(207, 207, 207);
                if (MenuLocationX == 2 && MenuLocationY == 3)
                    ColorPicker = new Color(95, 95, 95);
                if (MenuLocationX == 3 && MenuLocationY == 3)
                    ColorPicker = new Color(63, 255, 255);
                if (MenuLocationX == 4 && MenuLocationY == 3)
                    ColorPicker = new Color(63, 63, 255);
                if (MenuLocationX == 5 && MenuLocationY == 3)
                    ColorPicker = new Color(255, 63, 255);
                if (MenuLocationX == 6 && MenuLocationY == 3)
                    ColorPicker = new Color(255, 63, 63);
                if (MenuLocationX == 7 && MenuLocationY == 3)
                    ColorPicker = new Color(255, 255, 63);
                if (MenuLocationX == 8 && MenuLocationY == 3)
                    ColorPicker = new Color(63, 255, 63);

                if (MenuLocationX == 1 && MenuLocationY == 4)
                    ColorPicker = new Color(191, 191, 191);
                if (MenuLocationX == 2 && MenuLocationY == 4)
                    ColorPicker = new Color(79, 79, 79);
                if (MenuLocationX == 3 && MenuLocationY == 4)
                    ColorPicker = new Color(0, 255, 255);
                if (MenuLocationX == 4 && MenuLocationY == 4)
                    ColorPicker = new Color(0, 0, 255);
                if (MenuLocationX == 5 && MenuLocationY == 4)
                    ColorPicker = new Color(252, 0, 252);
                if (MenuLocationX == 6 && MenuLocationY == 4)
                    ColorPicker = new Color(255, 0, 0);
                if (MenuLocationX == 7 && MenuLocationY == 4)
                    ColorPicker = new Color(255, 255, 0);
                if (MenuLocationX == 8 && MenuLocationY == 4)
                    ColorPicker = new Color(0, 255, 0);

                if (MenuLocationX == 1 && MenuLocationY == 5)
                    ColorPicker = new Color(175, 175, 175);
                if (MenuLocationX == 2 && MenuLocationY == 5)
                    ColorPicker = new Color(63, 63, 63);
                if (MenuLocationX == 3 && MenuLocationY == 5)
                    ColorPicker = new Color(0, 191, 191);
                if (MenuLocationX == 4 && MenuLocationY == 5)
                    ColorPicker = new Color(0, 0, 191);
                if (MenuLocationX == 5 && MenuLocationY == 5)
                    ColorPicker = new Color(191, 0, 191);
                if (MenuLocationX == 6 && MenuLocationY == 5)
                    ColorPicker = new Color(191, 0, 0);
                if (MenuLocationX == 7 && MenuLocationY == 5)
                    ColorPicker = new Color(191, 191, 0);
                if (MenuLocationX == 8 && MenuLocationY == 5)
                    ColorPicker = new Color(0, 191, 0);

                if (MenuLocationX == 1 && MenuLocationY == 6)
                    ColorPicker = new Color(159, 159, 159);
                if (MenuLocationX == 2 && MenuLocationY == 6)
                    ColorPicker = new Color(47, 47, 47);
                if (MenuLocationX == 3 && MenuLocationY == 6)
                    ColorPicker = new Color(0, 127, 127);
                if (MenuLocationX == 4 && MenuLocationY == 6)
                    ColorPicker = new Color(0, 0, 127);
                if (MenuLocationX == 5 && MenuLocationY == 6)
                    ColorPicker = new Color(127, 0, 127);
                if (MenuLocationX == 6 && MenuLocationY == 6)
                    ColorPicker = new Color(127, 0, 0);
                if (MenuLocationX == 7 && MenuLocationY == 6)
                    ColorPicker = new Color(127, 127, 0);
                if (MenuLocationX == 8 && MenuLocationY == 6)
                    ColorPicker = new Color(0, 127, 0);

                if (MenuLocationX == 1 && MenuLocationY == 7)
                    ColorPicker = new Color(143, 143, 143);
                if (MenuLocationX == 2 && MenuLocationY == 7)
                    ColorPicker = new Color(9, 9, 9);
                if (MenuLocationX == 3 && MenuLocationY == 7)
                    ColorPicker = new Color(0, 63, 63);
                if (MenuLocationX == 4 && MenuLocationY == 7)
                    ColorPicker = new Color(0, 0, 63);
                if (MenuLocationX == 5 && MenuLocationY == 7)
                    ColorPicker = new Color(63, 0, 63);
                if (MenuLocationX == 6 && MenuLocationY == 7)
                    ColorPicker = new Color(63, 0, 0);
                if (MenuLocationX == 7 && MenuLocationY == 7)
                    ColorPicker = new Color(63, 63, 0);
                if (MenuLocationX == 8 && MenuLocationY == 7)
                    ColorPicker = new Color(0, 63, 0);

                #endregion

                player.Sprite.Tint = ColorPicker;

                if (PaintFirstTime)
                {
                    Select = false;
                    PaintFirstTime = false;
                }

                if (Select)
                {
                    Game1.Instance.AudioPlay("Selected", 1);
                    playerRedValue = ColorPicker.R;
                    playerBlueValue = ColorPicker.B;
                    playerGreenValue = ColorPicker.G;
                    Painting = false;
                    Save();

                    Ship1.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship2.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship3.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship4.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship5.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship6.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship7.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship8.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship9.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship10.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship11.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship12.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship13.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship14.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship15.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship16.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship17.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship18.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship19.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);
                    Ship20.Sprite.Tint = new Color(playerRedValue, playerGreenValue, playerBlueValue);

                    MenuLocationY = 3;
                }
            }

            #endregion
        }

        private void ProjectileDescriptions()
        {
            UpgradeName = "";

            Description1 = "";
            Description2 = "";
            Description3 = "";

            #region Electric

            if (ProjectileSelector == 1)
            {
                UpgradeName = "";

                if (iplayerElectricProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerElectricProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Laser

            if (ProjectileSelector == 2)
            {
                UpgradeName = "";

                if (iplayerLaserProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Fire

            if (ProjectileSelector == 3)
            {
                UpgradeName = "";

                if (iplayerFireProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerFireProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Poison

            if (ProjectileSelector == 4)
            {
                UpgradeName = "";

                if (iplayerPoisonProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerPoisonProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Explosive

            if (ProjectileSelector == 5)
            {
                UpgradeName = "";

                if (iplayerExplosiveProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerExplosiveProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Slow

            if (ProjectileSelector == 6)
            {
                UpgradeName = "";

                if (iplayerSlowProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerSlowProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Healing

            if (ProjectileSelector == 7)
            {
                UpgradeName = "";

                if (iplayerHealthProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealthProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            #region Energy

            if (ProjectileSelector == 8)
            {
                UpgradeName = "";

                if (iplayerEnergyProjectile == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerEnergyProjectile == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

            }

            #endregion

            if (ProjectileSelector == 9)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }

            if (ProjectileSelector == 10)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
        }

        private void SpecialDescriptions()
        {
            UpgradeName = "";

            Description1 = "";
            Description2 = "";
            Description3 = "";

            if (SpecialSelector == 0)
            {
                UpgradeName = "None";

                Description1 = "You have no specials unlocked";
                Description2 = "At this time, purchase one in";
                Description3 = "The Upgrades menu to activate.";
            }

            #region Healing

            if (SpecialSelector == 1)
            {         
                UpgradeName = "Medic Ship";

                if (iplayerHealingSpecial == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerHealingSpecial == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }
            }

            #endregion

            #region Laser

            if (SpecialSelector == 2)
            {
                UpgradeName = "ION Cannon";

                if (iplayerLaserSpecial == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerLaserSpecial == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }
            }

            #endregion

            #region Money

            if (SpecialSelector == 3)
            {
                UpgradeName = "Money Something?";

                if (iplayerMoneySpecial == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerMoneySpecial == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }
            }

            #endregion

            #region Shield

            if (SpecialSelector == 4)
            {
                UpgradeName = "Auto-Modulating Shields";

                if (iplayerShieldSpecial == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerShieldSpecial == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }
            }

            #endregion

            #region Freezing

            if (SpecialSelector == 5)
            {
                UpgradeName = "Time Break";

                if (iplayerTimeStopSpecial == 1)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 2)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 3)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 4)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 5)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 6)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 7)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 8)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 9)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }

                if (iplayerTimeStopSpecial == 10)
                {
                    Description1 = "";
                    Description2 = "";
                    Description3 = "";
                }
            }

            #endregion
        }

        private void AddImageBackground()
        {
            Texture2D ImageBackgroundTexture;
            MobileSprite ImageBackgroundMobileSprite;
            ImageBackgroundTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//ImageBackground");
            ImageBackgroundMobileSprite = new MobileSprite(ImageBackgroundTexture);
            ImageBackgroundMobileSprite.Sprite.AddAnimation("selected", 0, 64, 64, 64, 1, 0.05f);
            ImageBackgroundMobileSprite.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 0.05f);
            ImageBackgroundMobileSprite.Sprite.Tint = Color.White;
            ImageBackgroundMobileSprite.Sprite.CurrentAnimation = "default";
            ImageBackgroundMobileSprite.Position = Ship1.Position;
            ImageBackgroundMobileSprite.IsMoving = false;
            ImageBackgroundMobileSprite.IsActive = true;
            ShipBackground.Add(ImageBackgroundMobileSprite);
        }

        private void AddProjectileBackground()
        {
            Texture2D ImageBackgroundTexture;
            MobileSprite ImageBackgroundMobileSprite;
            ImageBackgroundTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//ImageBackground");
            ImageBackgroundMobileSprite = new MobileSprite(ImageBackgroundTexture);
            ImageBackgroundMobileSprite.Sprite.AddAnimation("selected", 0, 64, 64, 64, 1, 0.05f);
            ImageBackgroundMobileSprite.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 0.05f);
            ImageBackgroundMobileSprite.Sprite.Tint = Color.White;
            ImageBackgroundMobileSprite.Sprite.CurrentAnimation = "default";
            ImageBackgroundMobileSprite.Position = Vector2.Zero;
            ImageBackgroundMobileSprite.IsMoving = false;
            ImageBackgroundMobileSprite.IsActive = true;
            ProjectileBackground.Add(ImageBackgroundMobileSprite);
        }

        private void AddProjectile(int ProjectileType)
        {
            Projectile projectile;
            projectile = new Projectile();
            projectile.Initialize(ProjectileType, Vector2.Zero, 0, 0f, 0f, 0, 0, Vector2.Zero);
            projectile.LoadContent();
            Projectile.Add(projectile);
        }

        private void StatsUpdate()
        {
            playTime = playerTimePlayedHours + ":" + ZeroOne + playerTimePlayedMinutes + ":" + ZeroTwo + playerTimePlayedSeconds;
            playerAccuracy = (int)((double)((double)playerEnemiesHit / (double)playerBulletsFired) * 100);

            if (playerAccuracy > 100)
                playerAccuracy = 100;
            if (playerAccuracy < 0)
                playerAccuracy = 0;
        }

        private void AchievementsUpdate(GameTime gameTime)
        {
            #region Achievement Complete

            if (playerAchievement1)
                Achievement1.Sprite.CurrentAnimation = "complete";
            if (playerAchievement2)
                Achievement2.Sprite.CurrentAnimation = "complete";
            if (playerAchievement3)
                Achievement3.Sprite.CurrentAnimation = "complete";
            if (playerAchievement4)
                Achievement4.Sprite.CurrentAnimation = "complete";
            if (playerAchievement5)
                Achievement5.Sprite.CurrentAnimation = "complete";
            if (playerAchievement6)
                Achievement6.Sprite.CurrentAnimation = "complete";
            if (playerAchievement7)
                Achievement7.Sprite.CurrentAnimation = "complete";
            if (playerAchievement8)
                Achievement8.Sprite.CurrentAnimation = "complete";
            if (playerAchievement9)
                Achievement9.Sprite.CurrentAnimation = "complete";
            if (playerAchievement10)
                Achievement10.Sprite.CurrentAnimation = "complete";
            if (playerAchievement11)
                Achievement11.Sprite.CurrentAnimation = "complete";
            if (playerAchievement12)
                Achievement12.Sprite.CurrentAnimation = "complete";
            if (playerAchievement13)
                Achievement13.Sprite.CurrentAnimation = "complete";
            if (playerAchievement14)
                Achievement14.Sprite.CurrentAnimation = "complete";
            if (playerAchievement15)
                Achievement15.Sprite.CurrentAnimation = "complete";
            if (playerAchievement16)
                Achievement16.Sprite.CurrentAnimation = "complete";
            if (playerAchievement17)
                Achievement17.Sprite.CurrentAnimation = "complete";
            if (playerAchievement18)
                Achievement18.Sprite.CurrentAnimation = "complete";
            if (playerAchievement19)
                Achievement19.Sprite.CurrentAnimation = "complete";
            if (playerAchievement20)
                Achievement20.Sprite.CurrentAnimation = "complete";
            if (playerAchievement21)
                Achievement21.Sprite.CurrentAnimation = "complete";
            if (playerAchievement22)
                Achievement22.Sprite.CurrentAnimation = "complete";
            if (playerAchievement23)
                Achievement23.Sprite.CurrentAnimation = "complete";
            if (playerAchievement24)
                Achievement24.Sprite.CurrentAnimation = "complete";
            if (playerAchievement25)
                Achievement25.Sprite.CurrentAnimation = "complete";
            if (playerAchievement26)
                Achievement26.Sprite.CurrentAnimation = "complete";
            if (playerAchievement27)
                Achievement27.Sprite.CurrentAnimation = "complete";
            if (playerAchievement28)
                Achievement28.Sprite.CurrentAnimation = "complete";
            if (playerAchievement29)
                Achievement29.Sprite.CurrentAnimation = "complete";
            if (playerAchievement30)
                Achievement30.Sprite.CurrentAnimation = "complete";
            if (playerAchievement31)
                Achievement31.Sprite.CurrentAnimation = "complete";
            if (playerAchievement32)
                Achievement32.Sprite.CurrentAnimation = "complete";
            if (playerAchievement33)
                Achievement33.Sprite.CurrentAnimation = "complete";
            if (playerAchievement34)
                Achievement34.Sprite.CurrentAnimation = "complete";
            if (playerAchievement35)
                Achievement35.Sprite.CurrentAnimation = "complete";
            if (playerAchievement36)
                Achievement36.Sprite.CurrentAnimation = "complete";
            if (playerAchievement37)
                Achievement37.Sprite.CurrentAnimation = "complete";
            if (playerAchievement38)
                Achievement38.Sprite.CurrentAnimation = "complete";
            if (playerAchievement39)
                Achievement39.Sprite.CurrentAnimation = "complete";
            if (playerAchievement40)
                Achievement40.Sprite.CurrentAnimation = "complete";
            if (playerAchievement41)
                Achievement41.Sprite.CurrentAnimation = "complete";
            if (playerAchievement42)
                Achievement42.Sprite.CurrentAnimation = "complete";
            if (playerAchievement43)
                Achievement43.Sprite.CurrentAnimation = "complete";
            if (playerAchievement44)
                Achievement44.Sprite.CurrentAnimation = "complete";
            if (playerAchievement45)
                Achievement45.Sprite.CurrentAnimation = "complete";
            if (playerAchievement46)
                Achievement46.Sprite.CurrentAnimation = "complete";
            if (playerAchievement47)
                Achievement47.Sprite.CurrentAnimation = "complete";
            if (playerAchievement48)
                Achievement48.Sprite.CurrentAnimation = "complete";
            if (playerAchievement49)
                Achievement49.Sprite.CurrentAnimation = "complete";
            if (playerAchievement50)
                Achievement50.Sprite.CurrentAnimation = "complete";

            #endregion

            #region Position

            Achievement1.Position = new Vector2((screenWidth / 11) - (Achievement1.Sprite.Width / 2), 150);
            Achievement2.Position = new Vector2(((screenWidth / 11) * 2) - (Achievement1.Sprite.Width / 2), 150);
            Achievement3.Position = new Vector2(((screenWidth / 11) * 3) - (Achievement1.Sprite.Width / 2), 150);
            Achievement4.Position = new Vector2(((screenWidth / 11) * 4) - (Achievement1.Sprite.Width / 2), 150);
            Achievement5.Position = new Vector2(((screenWidth / 11) * 5) - (Achievement1.Sprite.Width / 2), 150);
            Achievement6.Position = new Vector2(((screenWidth / 11) * 6) - (Achievement1.Sprite.Width / 2), 150);
            Achievement7.Position = new Vector2(((screenWidth / 11) * 7) - (Achievement1.Sprite.Width / 2), 150);
            Achievement8.Position = new Vector2(((screenWidth / 11) * 8) - (Achievement1.Sprite.Width / 2), 150);
            Achievement9.Position = new Vector2(((screenWidth / 11) * 9) - (Achievement1.Sprite.Width / 2), 150);
            Achievement10.Position = new Vector2(((screenWidth / 11) * 10) - (Achievement1.Sprite.Width / 2), 150);
            Achievement11.Position = new Vector2((screenWidth / 11) - (Achievement1.Sprite.Width / 2), 225);
            Achievement12.Position = new Vector2(((screenWidth / 11) * 2) - (Achievement1.Sprite.Width / 2), 225);
            Achievement13.Position = new Vector2(((screenWidth / 11) * 3) - (Achievement1.Sprite.Width / 2), 225);
            Achievement14.Position = new Vector2(((screenWidth / 11) * 4) - (Achievement1.Sprite.Width / 2), 225);
            Achievement15.Position = new Vector2(((screenWidth / 11) * 5) - (Achievement1.Sprite.Width / 2), 225);
            Achievement16.Position = new Vector2(((screenWidth / 11) * 6) - (Achievement1.Sprite.Width / 2), 225);
            Achievement17.Position = new Vector2(((screenWidth / 11) * 7) - (Achievement1.Sprite.Width / 2), 225);
            Achievement18.Position = new Vector2(((screenWidth / 11) * 8) - (Achievement1.Sprite.Width / 2), 225);
            Achievement19.Position = new Vector2(((screenWidth / 11) * 9) - (Achievement1.Sprite.Width / 2), 225);
            Achievement20.Position = new Vector2(((screenWidth / 11) * 10) - (Achievement1.Sprite.Width / 2), 225);
            Achievement21.Position = new Vector2((screenWidth / 11) - (Achievement1.Sprite.Width / 2), 300);
            Achievement22.Position = new Vector2(((screenWidth / 11) * 2) - (Achievement1.Sprite.Width / 2), 300);
            Achievement23.Position = new Vector2(((screenWidth / 11) * 3) - (Achievement1.Sprite.Width / 2), 300);
            Achievement24.Position = new Vector2(((screenWidth / 11) * 4) - (Achievement1.Sprite.Width / 2), 300);
            Achievement25.Position = new Vector2(((screenWidth / 11) * 5) - (Achievement1.Sprite.Width / 2), 300);
            Achievement26.Position = new Vector2(((screenWidth / 11) * 6) - (Achievement1.Sprite.Width / 2), 300);
            Achievement27.Position = new Vector2(((screenWidth / 11) * 7) - (Achievement1.Sprite.Width / 2), 300);
            Achievement28.Position = new Vector2(((screenWidth / 11) * 8) - (Achievement1.Sprite.Width / 2), 300);
            Achievement29.Position = new Vector2(((screenWidth / 11) * 9) - (Achievement1.Sprite.Width / 2), 300);
            Achievement30.Position = new Vector2(((screenWidth / 11) * 10) - (Achievement1.Sprite.Width / 2), 300);
            Achievement31.Position = new Vector2((screenWidth / 11) - (Achievement1.Sprite.Width / 2), 375);
            Achievement32.Position = new Vector2(((screenWidth / 11) * 2) - (Achievement1.Sprite.Width / 2), 375);
            Achievement33.Position = new Vector2(((screenWidth / 11) * 3) - (Achievement1.Sprite.Width / 2), 375);
            Achievement34.Position = new Vector2(((screenWidth / 11) * 4) - (Achievement1.Sprite.Width / 2), 375);
            Achievement35.Position = new Vector2(((screenWidth / 11) * 5) - (Achievement1.Sprite.Width / 2), 375);
            Achievement36.Position = new Vector2(((screenWidth / 11) * 6) - (Achievement1.Sprite.Width / 2), 375);
            Achievement37.Position = new Vector2(((screenWidth / 11) * 7) - (Achievement1.Sprite.Width / 2), 375);
            Achievement38.Position = new Vector2(((screenWidth / 11) * 8) - (Achievement1.Sprite.Width / 2), 375);
            Achievement39.Position = new Vector2(((screenWidth / 11) * 9) - (Achievement1.Sprite.Width / 2), 375);
            Achievement40.Position = new Vector2(((screenWidth / 11) * 10) - (Achievement1.Sprite.Width / 2), 375);
            Achievement41.Position = new Vector2((screenWidth / 11) - (Achievement1.Sprite.Width / 2), 450);
            Achievement42.Position = new Vector2(((screenWidth / 11) * 2) - (Achievement1.Sprite.Width / 2), 450);
            Achievement43.Position = new Vector2(((screenWidth / 11) * 3) - (Achievement1.Sprite.Width / 2), 450);
            Achievement44.Position = new Vector2(((screenWidth / 11) * 4) - (Achievement1.Sprite.Width / 2), 450);
            Achievement45.Position = new Vector2(((screenWidth / 11) * 5) - (Achievement1.Sprite.Width / 2), 450);
            Achievement46.Position = new Vector2(((screenWidth / 11) * 6) - (Achievement1.Sprite.Width / 2), 450);
            Achievement47.Position = new Vector2(((screenWidth / 11) * 7) - (Achievement1.Sprite.Width / 2), 450);
            Achievement48.Position = new Vector2(((screenWidth / 11) * 8) - (Achievement1.Sprite.Width / 2), 450);
            Achievement49.Position = new Vector2(((screenWidth / 11) * 9) - (Achievement1.Sprite.Width / 2), 450);
            Achievement50.Position = new Vector2(((screenWidth / 11) * 10) - (Achievement1.Sprite.Width / 2), 450);

            #endregion

            #region Update

            Achievement1.Update(gameTime);
            Achievement2.Update(gameTime);
            Achievement3.Update(gameTime);
            Achievement4.Update(gameTime);
            Achievement5.Update(gameTime);
            Achievement6.Update(gameTime);
            Achievement7.Update(gameTime);
            Achievement8.Update(gameTime);
            Achievement9.Update(gameTime);
            Achievement10.Update(gameTime);
            Achievement11.Update(gameTime);
            Achievement12.Update(gameTime);
            Achievement13.Update(gameTime);
            Achievement14.Update(gameTime);
            Achievement15.Update(gameTime);
            Achievement16.Update(gameTime);
            Achievement17.Update(gameTime);
            Achievement18.Update(gameTime);
            Achievement19.Update(gameTime);
            Achievement20.Update(gameTime);
            Achievement21.Update(gameTime);
            Achievement22.Update(gameTime);
            Achievement23.Update(gameTime);
            Achievement24.Update(gameTime);
            Achievement25.Update(gameTime);
            Achievement26.Update(gameTime);
            Achievement27.Update(gameTime);
            Achievement28.Update(gameTime);
            Achievement29.Update(gameTime);
            Achievement30.Update(gameTime);
            Achievement31.Update(gameTime);
            Achievement32.Update(gameTime);
            Achievement33.Update(gameTime);
            Achievement34.Update(gameTime);
            Achievement35.Update(gameTime);
            Achievement36.Update(gameTime);
            Achievement37.Update(gameTime);
            Achievement38.Update(gameTime);
            Achievement39.Update(gameTime);
            Achievement40.Update(gameTime);
            Achievement41.Update(gameTime);
            Achievement42.Update(gameTime);
            Achievement43.Update(gameTime);
            Achievement44.Update(gameTime);
            Achievement45.Update(gameTime);
            Achievement46.Update(gameTime);
            Achievement47.Update(gameTime);
            Achievement48.Update(gameTime);
            Achievement49.Update(gameTime);
            Achievement50.Update(gameTime);

            #endregion

            #region Navigation

            if (MenuLocationX > 10)
                MenuLocationX = 1;
            if (MenuLocationX < 1)
                MenuLocationX = 10;
            if (MenuLocationY > 5)
                MenuLocationY = 0;
            if (MenuLocationY < 0)
                MenuLocationY = 5;

            if (MenuLocationX == 1)
                SelectedPosition.X = Achievement1.Position.X;
            if (MenuLocationX == 2)
                SelectedPosition.X = Achievement2.Position.X;
            if (MenuLocationX == 3)
                SelectedPosition.X = Achievement3.Position.X;
            if (MenuLocationX == 4)
                SelectedPosition.X = Achievement4.Position.X;
            if (MenuLocationX == 5)
                SelectedPosition.X = Achievement5.Position.X;
            if (MenuLocationX == 6)
                SelectedPosition.X = Achievement6.Position.X;
            if (MenuLocationX == 7)
                SelectedPosition.X = Achievement7.Position.X;
            if (MenuLocationX == 8)
                SelectedPosition.X = Achievement8.Position.X;
            if (MenuLocationX == 9)
                SelectedPosition.X = Achievement9.Position.X;
            if (MenuLocationX == 10)
                SelectedPosition.X = Achievement10.Position.X;

            if (MenuLocationY == 1)
                SelectedPosition.Y = Achievement1.Position.Y;
            if (MenuLocationY == 2)
                SelectedPosition.Y = Achievement11.Position.Y;
            if (MenuLocationY == 3)
                SelectedPosition.Y = Achievement21.Position.Y;
            if (MenuLocationY == 4)
                SelectedPosition.Y = Achievement31.Position.Y;
            if (MenuLocationY == 5)
                SelectedPosition.Y = Achievement41.Position.Y;

            #endregion

            UpgradeName = "";

            Description1 = "";
            Description2 = "";
            Description3 = "";

            #region Text

            if (MenuLocationX == 1 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 2 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 3 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 4 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 5 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 6 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 7 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 8 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 9 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 10 && MenuLocationY == 1)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 1 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 2 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 3 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 4 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 5 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 6 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 7 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 8 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 9 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 10 && MenuLocationY == 2)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 1 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 2 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 3 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 4 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 5 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 6 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 7 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 8 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 9 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 10 && MenuLocationY == 3)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 1 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 2 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 3 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 4 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 5 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 6 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 7 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 8 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 9 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 10 && MenuLocationY == 4)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 1 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 2 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 3 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 4 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 5 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 6 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 7 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 8 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 9 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }
            else if (MenuLocationX == 10 && MenuLocationY == 5)
            {
                UpgradeName = "";

                Description1 = "";
                Description2 = "";
                Description3 = "";
            }

            #endregion
        }

        private void UpdateUpgrades()
        {
            UpgradeName = "";
            UpgradeCostString = "";
            Description1 = "";
            Description2 = "";
            Description3 = "";

            #region Tree Update

            #region Movement Speed

            if (playerLevel >= 1 && iplayerMovementSpeed == 0)
            {
                MovementSpeed.Sprite.CurrentAnimation = "available";
            }

            if (iplayerMovementSpeed > 0)
            {
                MovementSpeed.Sprite.CurrentAnimation = "purchased";
                Activated1 = true;
                Activated3 = true;
                TimeStopSpecial.Sprite.CurrentAnimation = "available";
                if (playerLevel >= 2)
                    MoneySpecial.Sprite.CurrentAnimation = "available";
            }
            if (iplayerMovementSpeed == 10)
            {
                MovementSpeed.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Time Stop Special

            if (iplayerTimeStopSpecial > 0)
            {
                TimeStopSpecial.Sprite.CurrentAnimation = "purchased";
            }

            if (iplayerTimeStopSpecial == 10)
            {
                TimeStopSpecial.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Fire Rate

            if (playerLevel >= 1 && iplayerFireRate == 0)
            {
                FireRate.Sprite.CurrentAnimation = "available";
            }

            if (iplayerFireRate > 0)
            {
                FireRate.Sprite.CurrentAnimation = "purchased";
                Activated2 = true;
                if (playerLevel >= 2)
                {
                    Ammo.Sprite.CurrentAnimation = "available";
                    BulletSpeed.Sprite.CurrentAnimation = "available";
                }
            }
            if (iplayerFireRate == 10)
            {
                FireRate.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Money Special

            if (iplayerMoneySpecial > 0)
            {
                MoneySpecial.Sprite.CurrentAnimation = "purchased";
                Activated4 = true;
                if (playerLevel >= 3)
                    ElectricProjectile.Sprite.CurrentAnimation = "available";
            }
            if (iplayerMoneySpecial == 10)
            {
                MoneySpecial.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Ammo

            if (iplayerAmmo > 0)
            {
                Ammo.Sprite.CurrentAnimation = "purchased";
                Activated5 = true;
                if (playerLevel >= 3)
                {
                    Health.Sprite.CurrentAnimation = "available";
                    Damage.Sprite.CurrentAnimation = "available";
                }
            }
            if (iplayerAmmo == 10)
            {
                Ammo.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Bullet Speed

            if (iplayerBulletSpeed > 0)
            {
                BulletSpeed.Sprite.CurrentAnimation = "purchased";
                Activated6 = true;
                if (playerLevel >= 3)
                {
                    LaserProjectile.Sprite.CurrentAnimation = "available";
                }
            }
            if (iplayerBulletSpeed == 10)
            {
                BulletSpeed.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Electric Projectile

            if (iplayerElectricProjectile > 0)
            {
                ElectricProjectile.Sprite.CurrentAnimation = "purchased";
                Activated7 = true;
                Activated8 = true;
                Health.Sprite.CurrentAnimation = "available";
                if (playerLevel >= 4)
                    Energy.Sprite.CurrentAnimation = "available";
                if (playerLevel >= 5 && iplayerHealth < 1)
                    Health.Sprite.CurrentAnimation = "available";
            }
            if (iplayerElectricProjectile == 10)
            {
                ElectricProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Health

            if (iplayerHealth > 0)
            {
                Health.Sprite.CurrentAnimation = "purchased";
                Activated11 = true;
                Activated7 = true;
                if (playerLevel >= 5 && Activated11 && Activated13 && Activated15)
                    ExtraLife1.Sprite.CurrentAnimation = "available";
                if (playerLevel >= 5 && iplayerElectricProjectile < 1)
                    ElectricProjectile.Sprite.CurrentAnimation = "available";
            }
            if (iplayerHealth == 10)
            {
                Health.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Damage

            if (iplayerDamage > 0)
            {
                Damage.Sprite.CurrentAnimation = "purchased";
                Activated9 = true;
                if (playerLevel >= 4)
                    HealingSpecial.Sprite.CurrentAnimation = "available";
            }
            if (iplayerDamage == 10)
            {
                Damage.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Laser Projectile

            if (iplayerLaserProjectile > 0)
            {
                LaserProjectile.Sprite.CurrentAnimation = "purchased";
                Activated10 = true;
                if (playerLevel >= 4)
                    HealingSpecial.Sprite.CurrentAnimation = "available";
            }
            if (iplayerLaserProjectile == 10)
            {
                LaserProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Energy

            if (iplayerEnergy > 0)
            {
                Energy.Sprite.CurrentAnimation = "purchased";
                Activated15 = true;
                if (playerLevel >= 5 && Activated11 && Activated13 && Activated15)
                    ExtraLife1.Sprite.CurrentAnimation = "available";
                if (playerLevel >= 6)
                    LaserSpecial.Sprite.CurrentAnimation = "available";
                if (playerLevel >= 5 && iplayerEnergy < 1)
                    Energy.Sprite.CurrentAnimation = "available";
            }
            if (iplayerEnergy == 10)
            {
                Energy.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Healing Special

            if (iplayerHealingSpecial > 0)
            {
                HealingSpecial.Sprite.CurrentAnimation = "purchased";
                Activated12 = true;
                if (playerLevel >= 5)
                {
                    FireProjectile.Sprite.CurrentAnimation = "available";
                    PoisonProjectile.Sprite.CurrentAnimation = "available";
                }
            }
            if (iplayerHealingSpecial == 10)
            {
                HealingSpecial.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Laser Special

            if (iplayerLaserSpecial > 0)
            {
                LaserSpecial.Sprite.CurrentAnimation = "purchased";
            }
            if (iplayerLaserSpecial == 10)
            {
                LaserSpecial.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Fire Projectile

            if (iplayerFireProjectile > 0)
            {
                FireProjectile.Sprite.CurrentAnimation = "purchased";
                Activated13 = true;
                if (playerLevel >= 5 && Activated11 && Activated13 && Activated15)
                    ExtraLife1.Sprite.CurrentAnimation = "available";
            }
            if (iplayerFireProjectile == 10)
            {
                FireProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Poison Projectile

            if (iplayerPoisonProjectile > 0)
            {
                PoisonProjectile.Sprite.CurrentAnimation = "purchased";
                Activated18 = true;
                if (playerLevel >= 8)
                    SlowProjectile.Sprite.CurrentAnimation = "available";
            }
            if (iplayerPoisonProjectile == 10)
            {
                PoisonProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Extra Life 1

            if (bplayerExtraLife1)
            {
                ExtraLife1.Sprite.CurrentAnimation = "complete";
                Activated14 = true;
                if (playerLevel >= 6)
                {
                    DoubleShot.Sprite.CurrentAnimation = "available";
                }
            }

            #endregion

            #region Double Shot

            if (bplayerDoubleShot)
            {
                DoubleShot.Sprite.CurrentAnimation = "complete";
                Activated17 = true;
                if (playerLevel >= 7)
                {
                    TripleShot.Sprite.CurrentAnimation = "available";
                    ShieldSpecial.Sprite.CurrentAnimation = "available";
                }
            }

            #endregion

            #region Triple Shot

            if (bplayerTripleShot)
            {
                TripleShot.Sprite.CurrentAnimation = "complete";
                Activated16 = true;

                ExtraLife2.Sprite.CurrentAnimation = "available";

                if (bplayerTripleShot == true && iplayerExplosiveProjectile > 0)
                {
                    Activated21 = true;
                    if (playerLevel >= 9)
                    {
                        HealthProjectile.Sprite.CurrentAnimation = "available";
                        EnergyProjectile.Sprite.CurrentAnimation = "available";
                        QuadShot.Sprite.CurrentAnimation = "available";
                    }
                }
            }

            #endregion

            #region Extra Life 2

            if (bplayerExtraLife2)
            {
                ExtraLife2.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Shield Special

            if (iplayerShieldSpecial > 0)
            {
                ShieldSpecial.Sprite.CurrentAnimation = "purchased";
            }
            if (iplayerShieldSpecial == 10)
            {
                ShieldSpecial.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Slow Projectile

            if (iplayerSlowProjectile > 0)
            {
                SlowProjectile.Sprite.CurrentAnimation = "purchased";
                Activated19 = true;
                Activated20 = true;
                if (playerLevel >= 9)
                {
                    ExplosiveProjectile.Sprite.CurrentAnimation = "available";
                    ExtraLife3.Sprite.CurrentAnimation = "available";
                }
            }
            if (iplayerSlowProjectile == 10)
            {
                SlowProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Explosive Projectile

            if (iplayerExplosiveProjectile > 0)
            {
                ExplosiveProjectile.Sprite.CurrentAnimation = "purchased";
                if (bplayerTripleShot == true && iplayerExplosiveProjectile > 0)
                {
                    Activated21 = true;
                    if (playerLevel >= 9)
                    {
                        HealthProjectile.Sprite.CurrentAnimation = "available";
                        EnergyProjectile.Sprite.CurrentAnimation = "available";
                        QuadShot.Sprite.CurrentAnimation = "available";
                    }
                }
            }
            if (iplayerExplosiveProjectile == 10)
            {
                ExplosiveProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Health Projectile

            if (iplayerHealthProjectile > 0)
            {
                HealthProjectile.Sprite.CurrentAnimation = "purchased";
            }
            if (iplayerHealthProjectile == 10)
            {
                HealthProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Energy Projectile

            if (iplayerEnergyProjectile > 0)
            {
                EnergyProjectile.Sprite.CurrentAnimation = "purchased";
            }
            if (iplayerEnergyProjectile == 10)
            {
                EnergyProjectile.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Quad Shot

            if (bplayerQuadShot)
            {
                QuadShot.Sprite.CurrentAnimation = "complete";
                if (bplayerExtraLife3 == true && bplayerQuadShot == true)
                {
                    Activated22 = true;
                    if (playerLevel >= 10)
                    {
                        AutoFire.Sprite.CurrentAnimation = "available";
                        ExtraLife4.Sprite.CurrentAnimation = "available";
                        QuintupleShot.Sprite.CurrentAnimation = "available";
                    }
                }
            }

            #endregion

            #region Extra Life 3

            if (bplayerExtraLife3)
            {
                ExtraLife3.Sprite.CurrentAnimation = "complete";
                if (bplayerExtraLife3 == true && bplayerQuadShot == true)
                {
                    Activated22 = true;
                    if (playerLevel >= 10)
                    {
                        AutoFire.Sprite.CurrentAnimation = "available";
                        ExtraLife4.Sprite.CurrentAnimation = "available";
                        QuintupleShot.Sprite.CurrentAnimation = "available";
                    }
                }
            }

            #endregion

            #region Extra Life 4

            if (bplayerExtraLife4)
            {
                ExtraLife4.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Quintuple Shot

            if (bplayerQuintupleShot)
            {
                QuintupleShot.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #region Autofire

            if (bplayerAutoFire)
            {
                AutoFire.Sprite.CurrentAnimation = "complete";
            }

            #endregion

            #endregion

            #region Movement Speed

            if (MenuLocationX == 1 && MenuLocationY == 1)
            {
                UpgradeName = "Improve Engines";

                Description1 = "Increases your ship speed";
                Description2 = "";
                Description3 = "";

                if (MovementSpeed.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerMovementSpeed == 0)
                        UpgradeCost = 100;
                    if (iplayerMovementSpeed == 1)
                        UpgradeCost = 200;
                    if (iplayerMovementSpeed == 2)
                        UpgradeCost = 300;
                    if (iplayerMovementSpeed == 3)
                        UpgradeCost = 400;
                    if (iplayerMovementSpeed == 4)
                        UpgradeCost = 500;
                    if (iplayerMovementSpeed == 5)
                        UpgradeCost = 1000;
                    if (iplayerMovementSpeed == 6)
                        UpgradeCost = 2500;
                    if (iplayerMovementSpeed == 7)
                        UpgradeCost = 5000;
                    if (iplayerMovementSpeed == 8)
                        UpgradeCost = 10000;
                    if (iplayerMovementSpeed == 9)
                        UpgradeCost = 25000;

                    if (MovementSpeed.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerMovementSpeed != 10)
                    {
                        iplayerMovementSpeed += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerAcceleration += 10;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Time Stop Special

            if (MenuLocationX == 2 && MenuLocationY == 1)
            {
                UpgradeName = "Quantum Break";

                Description1 = "Stops time at the cost of Energy";
                Description2 = "";
                Description3 = "";

                if (TimeStopSpecial.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerMovementSpeed == 0)
                        UpgradeCost = 500;
                    if (iplayerMovementSpeed == 1)
                        UpgradeCost = 1000;
                    if (iplayerMovementSpeed == 2)
                        UpgradeCost = 2000;
                    if (iplayerMovementSpeed == 3)
                        UpgradeCost = 3000;
                    if (iplayerMovementSpeed == 4)
                        UpgradeCost = 4000;
                    if (iplayerMovementSpeed == 5)
                        UpgradeCost = 5000;
                    if (iplayerMovementSpeed == 6)
                        UpgradeCost = 10000;
                    if (iplayerMovementSpeed == 7)
                        UpgradeCost = 25000;
                    if (iplayerMovementSpeed == 8)
                        UpgradeCost = 50000;
                    if (iplayerMovementSpeed == 9)
                        UpgradeCost = 100000;

                    if (TimeStopSpecial.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerTimeStopSpecial != 10)
                    {
                        iplayerTimeStopSpecial += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Fire Rate

            if (MenuLocationX == 3 && MenuLocationY == 1)
            {
                UpgradeName = "Fire Rate";

                Description1 = "Increases your rate of fire";
                Description2 = "";
                Description3 = "";

                if (FireRate.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerFireRate == 0)
                        UpgradeCost = 100;
                    if (iplayerFireRate == 1)
                        UpgradeCost = 200;
                    if (iplayerFireRate == 2)
                        UpgradeCost = 300;
                    if (iplayerFireRate == 3)
                        UpgradeCost = 400;
                    if (iplayerFireRate == 4)
                        UpgradeCost = 500;
                    if (iplayerFireRate == 5)
                        UpgradeCost = 1000;
                    if (iplayerFireRate == 6)
                        UpgradeCost = 2500;
                    if (iplayerFireRate == 7)
                        UpgradeCost = 5000;
                    if (iplayerFireRate == 8)
                        UpgradeCost = 10000;
                    if (iplayerFireRate == 9)
                        UpgradeCost = 25000;

                    if (FireRate.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerFireRate != 10)
                    {
                        iplayerFireRate += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerFireRate -= 0.1f;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Money Special

            if (MenuLocationX == 1 && MenuLocationY == 2)
            {
                UpgradeName = "Money Maker";

                Description1 = "Enemies drop money over time";
                Description2 = "at the cost of energy";
                Description3 = "";

                if (MoneySpecial.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerMoneySpecial == 0)
                        UpgradeCost = 500;
                    if (iplayerMoneySpecial == 1)
                        UpgradeCost = 1000;
                    if (iplayerMoneySpecial == 2)
                        UpgradeCost = 2000;
                    if (iplayerMoneySpecial == 3)
                        UpgradeCost = 3000;
                    if (iplayerMoneySpecial == 4)
                        UpgradeCost = 4000;
                    if (iplayerMoneySpecial == 5)
                        UpgradeCost = 5000;
                    if (iplayerMoneySpecial == 6)
                        UpgradeCost = 10000;
                    if (iplayerMoneySpecial == 7)
                        UpgradeCost = 25000;
                    if (iplayerMoneySpecial == 8)
                        UpgradeCost = 50000;
                    if (iplayerMoneySpecial == 9)
                        UpgradeCost = 100000;

                    if (MoneySpecial.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerMoneySpecial != 10)
                    {
                        iplayerMoneySpecial += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Ammo

            if (MenuLocationX == 2 && MenuLocationY == 2)
            {
                UpgradeName = "More Ammo";

                Description1 = "Increases the maximum bullets your";
                Description2 = "ship can have on screen at once";
                Description3 = "";

                if (Ammo.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerAmmo == 0)
                    {
                        UpgradeCost = 200;
                        Description3 = "+2 More Bullets";
                    }
                    if (iplayerAmmo == 1)
                    {
                        UpgradeCost = 400;
                        Description3 = "+3 More Bullets";
                    }
                    if (iplayerAmmo == 2)
                    {
                        UpgradeCost = 600;
                        Description3 = "+4 More Bullets";
                    }
                    if (iplayerAmmo == 3)
                    {
                        UpgradeCost = 800;
                        Description3 = "+5 More Bullets";
                    }
                    if (iplayerAmmo == 4)
                    {
                        UpgradeCost = 1000;
                        Description3 = "+10 More Bullets";
                    }
                    if (iplayerAmmo == 5)
                    {
                        UpgradeCost = 2500;
                        Description3 = "+15 More Bullets";
                    }
                    if (iplayerAmmo == 6)
                    {
                        UpgradeCost = 5000;
                        Description3 = "+20 More Bullets";
                    }
                    if (iplayerAmmo == 7)
                    {
                        UpgradeCost = 10000;
                        Description3 = "+25 More Bullets";
                    }
                    if (iplayerAmmo == 8)
                    {
                        UpgradeCost = 25000;
                        Description3 = "+50 More Bullets";
                    }
                    if (iplayerAmmo == 9)
                    {
                        UpgradeCost = 50000;
                        Description3 = "+100 More Bullets";
                    }

                    if (Ammo.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerAmmo != 10)
                    {
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;

                        if (iplayerAmmo == 0)
                            playerMaxBullets += 2;
                        if (iplayerAmmo == 1)
                            playerMaxBullets += 3;
                        if (iplayerAmmo == 2)
                            playerMaxBullets += 4;
                        if (iplayerAmmo == 3)
                            playerMaxBullets += 5;
                        if (iplayerAmmo == 4)
                            playerMaxBullets += 10;
                        if (iplayerAmmo == 5)
                            playerMaxBullets += 15;
                        if (iplayerAmmo == 6)
                            playerMaxBullets += 20;
                        if (iplayerAmmo == 7)
                            playerMaxBullets += 25;
                        if (iplayerAmmo == 8)
                            playerMaxBullets += 50;
                        if (iplayerAmmo == 9)
                            playerMaxBullets += 100;
                        iplayerAmmo += 1;

                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Bullet Speed

            if (MenuLocationX == 4 && MenuLocationY == 2)
            {
                UpgradeName = "Bullet Speed";

                Description1 = "Faster bullets";
                Description2 = "";
                Description3 = "";

                if (BulletSpeed.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerBulletSpeed == 0)
                        UpgradeCost = 200;
                    if (iplayerBulletSpeed == 1)
                        UpgradeCost = 400;
                    if (iplayerBulletSpeed == 2)
                        UpgradeCost = 600;
                    if (iplayerBulletSpeed == 3)
                        UpgradeCost = 800;
                    if (iplayerBulletSpeed == 4)
                        UpgradeCost = 1000;
                    if (iplayerBulletSpeed == 5)
                        UpgradeCost = 2500;
                    if (iplayerBulletSpeed == 6)
                        UpgradeCost = 5000;
                    if (iplayerBulletSpeed == 7)
                        UpgradeCost = 10000;
                    if (iplayerBulletSpeed == 8)
                        UpgradeCost = 25000;
                    if (iplayerBulletSpeed == 9)
                        UpgradeCost = 50000;

                    if (BulletSpeed.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerBulletSpeed != 10)
                    {
                        iplayerBulletSpeed += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerBulletSpeed += 2f;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Electric Projectile

            if (MenuLocationX == 1 && MenuLocationY == 3)
            {
                UpgradeName = "Electrical Shot";

                Description1 = "Electrical weapon";
                Description2 = "capable of stunning";
                Description3 = "";

                if (ElectricProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerElectricProjectile == 0)
                        UpgradeCost = 500;
                    if (iplayerElectricProjectile == 1)
                        UpgradeCost = 1000;
                    if (iplayerElectricProjectile == 2)
                        UpgradeCost = 2000;
                    if (iplayerElectricProjectile == 3)
                        UpgradeCost = 3000;
                    if (iplayerElectricProjectile == 4)
                        UpgradeCost = 4000;
                    if (iplayerElectricProjectile == 5)
                        UpgradeCost = 5000;
                    if (iplayerElectricProjectile == 6)
                        UpgradeCost = 10000;
                    if (iplayerElectricProjectile == 7)
                        UpgradeCost = 25000;
                    if (iplayerElectricProjectile == 8)
                        UpgradeCost = 50000;
                    if (iplayerElectricProjectile == 9)
                        UpgradeCost = 100000;

                    if (ElectricProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerElectricProjectile != 10)
                    {
                        iplayerElectricProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Health

            if (MenuLocationX == 2 && MenuLocationY == 3)
            {
                UpgradeName = "Increased Hull Protection";

                Description1 = "Increases your maximum health";
                Description2 = "";
                Description3 = "";

                if (Health.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerHealth == 0)
                    {
                        UpgradeCost = 200;
                        Description3 = "+10 Health";
                    }
                    if (iplayerHealth == 1)
                    {
                        UpgradeCost = 400;
                        Description3 = "+20 Health";
                    }
                    if (iplayerHealth == 2)
                    {
                        UpgradeCost = 600;
                        Description3 = "+60 Health";
                    }
                    if (iplayerHealth == 3)
                    {
                        UpgradeCost = 800;
                        Description3 = "+100 Health";
                    }
                    if (iplayerHealth == 4)
                    {
                        UpgradeCost = 1000;
                        Description3 = "+200 Health";
                    }
                    if (iplayerHealth == 5)
                    {
                        UpgradeCost = 2500;
                        Description3 = "+600 Health";
                    }
                    if (iplayerHealth == 6)
                    {
                        UpgradeCost = 5000;
                        Description3 = "+1000 Health";
                    }
                    if (iplayerHealth == 7)
                    {
                        UpgradeCost = 10000;
                        Description3 = "+2000 Health";
                    }
                    if (iplayerHealth == 8)
                    {
                        UpgradeCost = 25000;
                        Description3 = "+2500 Health";
                    }
                    if (iplayerHealth == 9)
                    {
                        UpgradeCost = 50000;
                        Description3 = "+3500 Health";
                    }

                    if (Health.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerHealth != 10)
                    {
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;

                        if (iplayerHealth == 0)
                            playerMaxHealth += 10;
                        if (iplayerHealth == 1)
                            playerMaxHealth += 20;
                        if (iplayerHealth == 2)
                            playerMaxHealth += 60;
                        if (iplayerHealth == 3)
                            playerMaxHealth += 100;
                        if (iplayerHealth == 4)
                            playerMaxHealth += 200;
                        if (iplayerHealth == 5)
                            playerMaxHealth += 600;
                        if (iplayerHealth == 6)
                            playerMaxHealth += 1000;
                        if (iplayerHealth == 7)
                            playerMaxHealth += 2000;
                        if (iplayerHealth == 8)
                            playerMaxHealth += 2500;
                        if (iplayerHealth == 9)
                            playerMaxHealth += 3500;

                        iplayerHealth += 1;

                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Damage

            if (MenuLocationX == 3 && MenuLocationY == 3)
            {
                UpgradeName = "Increased Damage";

                Description1 = "Increases the damage";
                Description2 = "of your shots";
                Description3 = "";

                if (Damage.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerDamage == 0)
                    {
                        UpgradeCost = 200;
                        Description3 = "+1 Damage";
                    }
                    if (iplayerDamage == 1)
                    {
                        UpgradeCost = 400;
                        Description3 = "+2 Damage";
                    }
                    if (iplayerDamage == 2)
                    {
                        UpgradeCost = 600;
                        Description3 = "+6 Damage";
                    }
                    if (iplayerDamage == 3)
                    {
                        UpgradeCost = 800;
                        Description3 = "+10 Damage";
                    }
                    if (iplayerDamage == 4)
                    {
                        UpgradeCost = 1000;
                        Description3 = "+20 Damage";
                    }
                    if (iplayerDamage == 5)
                    {
                        UpgradeCost = 2500;
                        Description3 = "+60 Damage";
                    }
                    if (iplayerDamage == 6)
                    {
                        UpgradeCost = 5000;
                        Description3 = "+100 Damage";
                    }
                    if (iplayerDamage == 7)
                    {
                        UpgradeCost = 10000;
                        Description3 = "+200 Damage";
                    }
                    if (iplayerDamage == 8)
                    {
                        UpgradeCost = 25000;
                        Description3 = "+250 Damage";
                    }
                    if (iplayerDamage == 9)
                    {
                        UpgradeCost = 50000;
                        Description3 = "+350 Damage";
                    }

                    if (Damage.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerDamage != 10)
                    {
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;

                        if (iplayerDamage == 0)
                            playerDamage += 1;
                        if (iplayerDamage == 1)
                            playerDamage += 2;
                        if (iplayerDamage == 2)
                            playerDamage += 6;
                        if (iplayerDamage == 3)
                            playerDamage += 10;
                        if (iplayerDamage == 4)
                            playerDamage += 20;
                        if (iplayerDamage == 5)
                            playerDamage += 60;
                        if (iplayerDamage == 6)
                            playerDamage += 100;
                        if (iplayerDamage == 7)
                            playerDamage += 200;
                        if (iplayerDamage == 8)
                            playerDamage += 250;
                        if (iplayerDamage == 9)
                            playerDamage += 350;

                        iplayerDamage += 1;

                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Laser Projectile

            if (MenuLocationX == 4 && MenuLocationY == 3)
            {
                UpgradeName = "Laser";

                Description1 = "An incredibly fast";
                Description2 = "but weak weapon";
                Description3 = "";

                if (LaserProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerLaserProjectile == 0)
                        UpgradeCost = 500;
                    if (iplayerLaserProjectile == 1)
                        UpgradeCost = 1000;
                    if (iplayerLaserProjectile == 2)
                        UpgradeCost = 2000;
                    if (iplayerLaserProjectile == 3)
                        UpgradeCost = 3000;
                    if (iplayerLaserProjectile == 4)
                        UpgradeCost = 4000;
                    if (iplayerLaserProjectile == 5)
                        UpgradeCost = 5000;
                    if (iplayerLaserProjectile == 6)
                        UpgradeCost = 10000;
                    if (iplayerLaserProjectile == 7)
                        UpgradeCost = 25000;
                    if (iplayerLaserProjectile == 8)
                        UpgradeCost = 50000;
                    if (iplayerLaserProjectile == 9)
                        UpgradeCost = 100000;

                    if (LaserProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerLaserProjectile != 10)
                    {
                        iplayerLaserProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Energy

            if (MenuLocationX == 1 && MenuLocationY == 4)
            {
                UpgradeName = "Increased Energy Capacity";

                Description1 = "Increases your maximum energy";
                Description2 = "";
                Description3 = "";

                if (Energy.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerEnergy == 0)
                    {
                        UpgradeCost = 200;
                        Description3 = "+10 Energy";
                    }
                    if (iplayerEnergy == 1)
                    {
                        UpgradeCost = 400;
                        Description3 = "+20 Energy";
                    }
                    if (iplayerEnergy == 2)
                    {
                        UpgradeCost = 600;
                        Description3 = "+60 Energy";
                    }
                    if (iplayerEnergy == 3)
                    {
                        UpgradeCost = 800;
                        Description3 = "+100 Energy";
                    }
                    if (iplayerEnergy == 4)
                    {
                        UpgradeCost = 1000;
                        Description3 = "+200 Energy";
                    }
                    if (iplayerEnergy == 5)
                    {
                        UpgradeCost = 2500;
                        Description3 = "+600 Energy";
                    }
                    if (iplayerEnergy == 6)
                    {
                        UpgradeCost = 5000;
                        Description3 = "+1000 Energy";
                    }
                    if (iplayerEnergy == 7)
                    {
                        UpgradeCost = 10000;
                        Description3 = "+2000 Energy";
                    }
                    if (iplayerEnergy == 8)
                    {
                        UpgradeCost = 25000;
                        Description3 = "+2500 Energy";
                    }
                    if (iplayerEnergy == 9)
                    {
                        UpgradeCost = 50000;
                        Description3 = "+3500 Energy";
                    }

                    if (Energy.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerEnergy != 10)
                    {
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;

                        if (iplayerEnergy == 0)
                            playerMaxEnergy += 10;
                        if (iplayerEnergy == 1)
                            playerMaxEnergy += 20;
                        if (iplayerEnergy == 2)
                            playerMaxEnergy += 60;
                        if (iplayerEnergy == 3)
                            playerMaxEnergy += 100;
                        if (iplayerEnergy == 4)
                            playerMaxEnergy += 200;
                        if (iplayerEnergy == 5)
                            playerMaxEnergy += 600;
                        if (iplayerEnergy == 6)
                            playerMaxEnergy += 1000;
                        if (iplayerEnergy == 7)
                            playerMaxEnergy += 2000;
                        if (iplayerEnergy == 8)
                            playerMaxEnergy += 2500;
                        if (iplayerEnergy == 9)
                            playerMaxEnergy += 3500;

                        iplayerEnergy += 1;

                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Healing Special

            if (MenuLocationX == 3 && MenuLocationY == 4)
            {
                UpgradeName = "Medic Ship";

                Description1 = "Heals you and nearby allies";
                Description2 = "at the cost of energy";
                Description3 = "";

                if (HealingSpecial.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerHealingSpecial == 0)
                        UpgradeCost = 500;
                    if (iplayerHealingSpecial == 1)
                        UpgradeCost = 1000;
                    if (iplayerHealingSpecial == 2)
                        UpgradeCost = 2000;
                    if (iplayerHealingSpecial == 3)
                        UpgradeCost = 3000;
                    if (iplayerHealingSpecial == 4)
                        UpgradeCost = 4000;
                    if (iplayerHealingSpecial == 5)
                        UpgradeCost = 5000;
                    if (iplayerHealingSpecial == 6)
                        UpgradeCost = 10000;
                    if (iplayerHealingSpecial == 7)
                        UpgradeCost = 25000;
                    if (iplayerHealingSpecial == 8)
                        UpgradeCost = 50000;
                    if (iplayerHealingSpecial == 9)
                        UpgradeCost = 100000;

                    if (HealingSpecial.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerHealingSpecial != 10)
                    {
                        iplayerHealingSpecial += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Laser Special

            if (MenuLocationX == 1 && MenuLocationY == 6)
            {
                UpgradeName = "Ion Cannon";

                Description1 = "Fire a powerful beam";
                Description2 = "at the cost of energy";
                Description3 = "";

                if (LaserSpecial.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerLaserSpecial == 0)
                        UpgradeCost = 500;
                    if (iplayerLaserSpecial == 1)
                        UpgradeCost = 1000;
                    if (iplayerLaserSpecial == 2)
                        UpgradeCost = 2000;
                    if (iplayerLaserSpecial == 3)
                        UpgradeCost = 3000;
                    if (iplayerLaserSpecial == 4)
                        UpgradeCost = 4000;
                    if (iplayerLaserSpecial == 5)
                        UpgradeCost = 5000;
                    if (iplayerLaserSpecial == 6)
                        UpgradeCost = 10000;
                    if (iplayerLaserSpecial == 7)
                        UpgradeCost = 25000;
                    if (iplayerLaserSpecial == 8)
                        UpgradeCost = 50000;
                    if (iplayerLaserSpecial == 9)
                        UpgradeCost = 100000;

                    if (LaserSpecial.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerLaserSpecial != 10)
                    {                        
                        iplayerLaserSpecial += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Fire Projectile

            if (MenuLocationX == 3 && MenuLocationY == 5)
            {
                UpgradeName = "Fireballs";

                Description1 = "A fairly powerful weapon";
                Description2 = "with a chance to burn";
                Description3 = "";

                if (FireProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerFireProjectile == 0)
                        UpgradeCost = 1000;
                    if (iplayerFireProjectile == 1)
                        UpgradeCost = 2000;
                    if (iplayerFireProjectile == 2)
                        UpgradeCost = 4000;
                    if (iplayerFireProjectile == 3)
                        UpgradeCost = 6000;
                    if (iplayerFireProjectile == 4)
                        UpgradeCost = 8000;
                    if (iplayerFireProjectile == 5)
                        UpgradeCost = 10000;
                    if (iplayerFireProjectile == 6)
                        UpgradeCost = 25000;
                    if (iplayerFireProjectile == 7)
                        UpgradeCost = 50000;
                    if (iplayerFireProjectile == 8)
                        UpgradeCost = 100000;
                    if (iplayerFireProjectile == 9)
                        UpgradeCost = 200000;

                    if (FireProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerFireProjectile != 10)
                    {                        
                        iplayerFireProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Poison Projectile

            if (MenuLocationX == 4 && MenuLocationY == 5)
            {
                UpgradeName = "Poison Darts";

                Description1 = "A fairly weak weapon";
                Description2 = "which drains enemies health";
                Description3 = "";

                if (PoisonProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerPoisonProjectile == 0)
                        UpgradeCost = 1000;
                    if (iplayerPoisonProjectile == 1)
                        UpgradeCost = 2000;
                    if (iplayerPoisonProjectile == 2)
                        UpgradeCost = 4000;
                    if (iplayerPoisonProjectile == 3)
                        UpgradeCost = 6000;
                    if (iplayerPoisonProjectile == 4)
                        UpgradeCost = 8000;
                    if (iplayerPoisonProjectile == 5)
                        UpgradeCost = 10000;
                    if (iplayerPoisonProjectile == 6)
                        UpgradeCost = 25000;
                    if (iplayerPoisonProjectile == 7)
                        UpgradeCost = 50000;
                    if (iplayerPoisonProjectile == 8)
                        UpgradeCost = 100000;
                    if (iplayerPoisonProjectile == 9)
                        UpgradeCost = 200000;

                    if (PoisonProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerPoisonProjectile != 10)
                    {                        
                        iplayerPoisonProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Extra Life 1

            if (MenuLocationX == 2 && MenuLocationY == 5)
            {
                UpgradeName = "Extra Life";

                Description1 = "start with an extra life";
                Description2 = "";
                Description3 = "";

                if (ExtraLife1.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 10000;

                    if (ExtraLife1.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerExtraLife1 == false)
                    {                        
                        bplayerExtraLife1 = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerLives += 1;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Double Shot

            if (MenuLocationX == 2 && MenuLocationY == 6)
            {
                UpgradeName = "Double Shot";

                Description1 = "Another Canon allows you to";
                Description2 = "shoot two bullets at once";
                Description3 = "";

                if (DoubleShot.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 100000;

                    if (DoubleShot.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerDoubleShot == false)
                    {                        
                        bplayerDoubleShot = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Extra Life 2

            if (MenuLocationX == 1 && MenuLocationY == 7)
            {
                UpgradeName = "Extra Life";

                Description1 = "start with another extra life";
                Description2 = "";
                Description3 = "";

                if (ExtraLife2.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 10000;

                    if (ExtraLife2.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerExtraLife2 == false)
                    {                        
                        bplayerExtraLife2 = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerLives += 1;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Triple Shot

            if (MenuLocationX == 2 && MenuLocationY == 7)
            {
                UpgradeName = "Triple Shot";

                Description1 = "Another Canon allows you to";
                Description2 = "shoot three bullets at once";
                Description3 = "";

                if (TripleShot.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 250000;

                    if (TripleShot.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerTripleShot == false)
                    {                        
                        bplayerTripleShot = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Shield Special

            if (MenuLocationX == 3 && MenuLocationY == 7)
            {
                UpgradeName = "Auto-modulating shields";

                Description1 = "Makes your ship invulnerable";
                Description2 = "at the cost of energy";
                Description3 = "";

                if (ShieldSpecial.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerShieldSpecial == 0)
                        UpgradeCost = 500;
                    if (iplayerShieldSpecial == 1)
                        UpgradeCost = 1000;
                    if (iplayerShieldSpecial == 2)
                        UpgradeCost = 2000;
                    if (iplayerShieldSpecial == 3)
                        UpgradeCost = 3000;
                    if (iplayerShieldSpecial == 4)
                        UpgradeCost = 4000;
                    if (iplayerShieldSpecial == 5)
                        UpgradeCost = 5000;
                    if (iplayerShieldSpecial == 6)
                        UpgradeCost = 10000;
                    if (iplayerShieldSpecial == 7)
                        UpgradeCost = 25000;
                    if (iplayerShieldSpecial == 8)
                        UpgradeCost = 50000;
                    if (iplayerShieldSpecial == 9)
                        UpgradeCost = 100000;

                    if (ShieldSpecial.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerShieldSpecial != 10)
                    {                        
                        iplayerShieldSpecial += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Explosive Projectile

            if (MenuLocationX == 3 && MenuLocationY == 8)
            {
                UpgradeName = "Explosive Rounds";

                Description1 = "An exploding weapon that";
                Description2 = "causes damage to nearby enemies";
                Description3 = "";

                if (ExplosiveProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerExplosiveProjectile == 0)
                        UpgradeCost = 2000;
                    if (iplayerExplosiveProjectile == 1)
                        UpgradeCost = 4000;
                    if (iplayerExplosiveProjectile == 2)
                        UpgradeCost = 8000;
                    if (iplayerExplosiveProjectile == 3)
                        UpgradeCost = 12000;
                    if (iplayerExplosiveProjectile == 4)
                        UpgradeCost = 16000;
                    if (iplayerExplosiveProjectile == 5)
                        UpgradeCost = 20000;
                    if (iplayerExplosiveProjectile == 6)
                        UpgradeCost = 50000;
                    if (iplayerExplosiveProjectile == 7)
                        UpgradeCost = 100000;
                    if (iplayerExplosiveProjectile == 8)
                        UpgradeCost = 200000;
                    if (iplayerExplosiveProjectile == 9)
                        UpgradeCost = 400000;

                    if (ExplosiveProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerExplosiveProjectile != 10)
                    {                        
                        iplayerExplosiveProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Slow Projectile

            if (MenuLocationX == 4 && MenuLocationY == 8)
            {
                UpgradeName = "Frozen Rounds";

                Description1 = "An exteremly cold weapon";
                Description2 = "that slows enemies";
                Description3 = "";

                if (SlowProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerSlowProjectile == 0)
                        UpgradeCost = 2000;
                    if (iplayerSlowProjectile == 1)
                        UpgradeCost = 4000;
                    if (iplayerSlowProjectile == 2)
                        UpgradeCost = 8000;
                    if (iplayerSlowProjectile == 3)
                        UpgradeCost = 12000;
                    if (iplayerSlowProjectile == 4)
                        UpgradeCost = 16000;
                    if (iplayerSlowProjectile == 5)
                        UpgradeCost = 20000;
                    if (iplayerSlowProjectile == 6)
                        UpgradeCost = 50000;
                    if (iplayerSlowProjectile == 7)
                        UpgradeCost = 100000;
                    if (iplayerSlowProjectile == 8)
                        UpgradeCost = 200000;
                    if (iplayerSlowProjectile == 9)
                        UpgradeCost = 400000;

                    if (SlowProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerSlowProjectile != 10)
                    {                        
                        iplayerSlowProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Health Projectile

            if (MenuLocationX == 1 && MenuLocationY == 9)
            {
                UpgradeName = "Life Leech";

                Description1 = "An attack that steals";
                Description2 = "enemies health";
                Description3 = "";

                if (HealthProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerHealthProjectile == 0)
                        UpgradeCost = 4000;
                    if (iplayerHealthProjectile == 1)
                        UpgradeCost = 6000;
                    if (iplayerHealthProjectile == 2)
                        UpgradeCost = 16000;
                    if (iplayerHealthProjectile == 3)
                        UpgradeCost = 24000;
                    if (iplayerHealthProjectile == 4)
                        UpgradeCost = 32000;
                    if (iplayerHealthProjectile == 5)
                        UpgradeCost = 40000;
                    if (iplayerHealthProjectile == 6)
                        UpgradeCost = 100000;
                    if (iplayerHealthProjectile == 7)
                        UpgradeCost = 200000;
                    if (iplayerHealthProjectile == 8)
                        UpgradeCost = 400000;
                    if (iplayerHealthProjectile == 9)
                        UpgradeCost = 800000;

                    if (HealthProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerHealthProjectile != 10)
                    {                        
                        iplayerHealthProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Energy Projectile

            if (MenuLocationX == 3 && MenuLocationY == 9)
            {
                UpgradeName = "Energy Leech";

                Description1 = "An attack that steals";
                Description2 = "enemies energy";
                Description3 = "";

                if (EnergyProjectile.Sprite.CurrentAnimation != "default")
                {
                    if (iplayerEnergyProjectile == 0)
                        UpgradeCost = 4000;
                    if (iplayerEnergyProjectile == 1)
                        UpgradeCost = 6000;
                    if (iplayerEnergyProjectile == 2)
                        UpgradeCost = 16000;
                    if (iplayerEnergyProjectile == 3)
                        UpgradeCost = 24000;
                    if (iplayerEnergyProjectile == 4)
                        UpgradeCost = 32000;
                    if (iplayerEnergyProjectile == 5)
                        UpgradeCost = 40000;
                    if (iplayerEnergyProjectile == 6)
                        UpgradeCost = 100000;
                    if (iplayerEnergyProjectile == 7)
                        UpgradeCost = 200000;
                    if (iplayerEnergyProjectile == 8)
                        UpgradeCost = 400000;
                    if (iplayerEnergyProjectile == 9)
                        UpgradeCost = 800000;

                    if (EnergyProjectile.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && iplayerEnergyProjectile != 10)
                    {                        
                        iplayerEnergyProjectile += 1;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Quad Shot

            if (MenuLocationX == 2 && MenuLocationY == 9)
            {
                UpgradeName = "Quad Shot";

                Description1 = "Another Canon allows you to";
                Description2 = "shoot four bullets at once";
                Description3 = "";

                if (QuadShot.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 500000;

                    if (QuadShot.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerQuadShot == false)
                    {                        
                        bplayerQuadShot = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Extra Life 3

            if (MenuLocationX == 4 && MenuLocationY == 9)
            {
                UpgradeName = "Extra Life";

                Description1 = "start with an extra life";
                Description2 = "";
                Description3 = "";

                if (ExtraLife3.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 10000;

                    if (ExtraLife3.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerExtraLife3 == false)
                    {                        
                        bplayerExtraLife3 = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerLives += 1;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Extra Life 4

            if (MenuLocationX == 3 && MenuLocationY == 10)
            {
                UpgradeName = "Extra Life";

                Description1 = "start with another extra life";
                Description2 = "";
                Description3 = "";

                if (ExtraLife4.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 10000;

                    if (ExtraLife4.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerExtraLife4 == false)
                    {                        
                        bplayerExtraLife4 = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        playerLives += 1;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Quintuple Shot

            if (MenuLocationX == 2 && MenuLocationY == 10)
            {
                UpgradeName = "Quintuple Shot";

                Description1 = "Another Canon allows you to";
                Description2 = "shoot five bullets at once";
                Description3 = "";

                if (QuintupleShot.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 1000000;

                    if (QuintupleShot.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerQuintupleShot == false)
                    {                        
                        bplayerQuintupleShot = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion

            #region Autofire

            if (MenuLocationX == 1 && MenuLocationY == 10)
            {
                UpgradeName = "Autofire";

                Description1 = "Control over firing becomes";
                Description2 = "semi-automatic";
                Description3 = "";

                if (AutoFire.Sprite.CurrentAnimation != "default")
                {
                    UpgradeCost = 1000000;

                    if (AutoFire.Sprite.CurrentAnimation != "complete")
                    {
                        UpgradeCostString = "Price: " + UpgradeCost + "/" + playerCredits;
                    }
                    else
                    {
                        Description1 = "This upgrade has reached";
                        Description2 = "maximum efficiency";
                        Description3 = "";                        
                    }

                    if (playerCredits >= UpgradeCost && Select && bplayerAutoFire == false)
                    {                        
                        bplayerAutoFire = true;
                        playerCreditsSpent += UpgradeCost;
                        playerUpgradesPurchased += 1;
                        playerCredits -= UpgradeCost;
                        Save();
                    }
                    else if (Select)
                        Game1.Instance.AudioPlay("Unavailable", 1);
                }
            }

            #endregion
        }

        private void UpgradeIconsUpdate(GameTime gameTime)
        {
            if (MenuLocationY == 0)
            {
                MenuLocationY = 1;
                MenuYChanged = true;
            }

            HealingSpecial.Sprite.SetPosX((int)IconPositionX3.X);
            LaserSpecial.Sprite.SetPosX((int)IconPositionX1.X);
            MoneySpecial.Sprite.SetPosX((int)IconPositionX1.X);
            ShieldSpecial.Sprite.SetPosX((int)IconPositionX3.X);
            TimeStopSpecial.Sprite.SetPosX((int)IconPositionX2.X);

            Ammo.Sprite.SetPosY(200 + (200 * 2) - (200 * MenuLocationY));
            Ammo.Update(gameTime);

            AutoFire.Sprite.SetPosY(200 + (200 * 10) - (200 * MenuLocationY));
            AutoFire.Update(gameTime);

            BulletSpeed.Sprite.SetPosY(200 + (200 * 2) - (200 * MenuLocationY));
            BulletSpeed.Update(gameTime);

            Damage.Sprite.SetPosY(200 + (200 * 3) - (200 * MenuLocationY));
            Damage.Update(gameTime);

            DoubleShot.Sprite.SetPosY(200 + (200 * 6) - (200 * MenuLocationY));
            DoubleShot.Update(gameTime);

            ElectricProjectile.Sprite.SetPosY(200 + (200 * 3) - (200 * MenuLocationY));
            ElectricProjectile.Update(gameTime);

            Energy.Sprite.SetPosY(200 + (200 * 4) - (200 * MenuLocationY));
            Energy.Update(gameTime);

            EnergyProjectile.Sprite.SetPosY(200 + (200 * 9) - (200 * MenuLocationY));
            EnergyProjectile.Update(gameTime);

            ExplosiveProjectile.Sprite.SetPosY(200 + (200 * 8) - (200 * MenuLocationY));
            ExplosiveProjectile.Update(gameTime);

            ExtraLife1.Sprite.SetPosY(200 + (200 * 5) - (200 * MenuLocationY));
            ExtraLife1.Update(gameTime);
            ExtraLife2.Sprite.SetPosY(200 + (200 * 7) - (200 * MenuLocationY));
            ExtraLife2.Update(gameTime);
            ExtraLife3.Sprite.SetPosY(200 + (200 * 9) - (200 * MenuLocationY));
            ExtraLife3.Update(gameTime);
            ExtraLife4.Sprite.SetPosY(200 + (200 * 10) - (200 * MenuLocationY));
            ExtraLife4.Update(gameTime);

            FireProjectile.Sprite.SetPosY(200 + (200 * 5) - (200 * MenuLocationY));
            FireProjectile.Update(gameTime);

            FireRate.Sprite.SetPosY(200 + (200 * 1) - (200 * MenuLocationY));
            FireRate.Update(gameTime);

            HealingSpecial.Sprite.SetPosY(200 + (200 * 4) - (200 * MenuLocationY));
            HealingSpecial.Update(gameTime);

            Health.Sprite.SetPosY(200 + (200 * 3) - (200 * MenuLocationY));
            Health.Update(gameTime);

            HealthProjectile.Sprite.SetPosY(200 + (200 * 9) - (200 * MenuLocationY));
            HealthProjectile.Update(gameTime);

            LaserProjectile.Sprite.SetPosY(200 + (200 * 3) - (200 * MenuLocationY));
            LaserProjectile.Update(gameTime);

            LaserSpecial.Sprite.SetPosY(200 + (200 * 6) - (200 * MenuLocationY));
            LaserSpecial.Update(gameTime);

            MoneySpecial.Sprite.SetPosY(200 + (200 * 2) - (200 * MenuLocationY));
            MoneySpecial.Update(gameTime);

            MovementSpeed.Sprite.SetPosY(200 + (200 * 1) - (200 * MenuLocationY));
            MovementSpeed.Update(gameTime);

            PoisonProjectile.Sprite.SetPosY(200 + (200 * 5) - (200 * MenuLocationY));
            PoisonProjectile.Update(gameTime);

            QuadShot.Sprite.SetPosY(200 + (200 * 9) - (200 * MenuLocationY));
            QuadShot.Update(gameTime);

            QuintupleShot.Sprite.SetPosY(200 + (200 * 10) - (200 * MenuLocationY));
            QuintupleShot.Update(gameTime);

            ShieldSpecial.Sprite.SetPosY(200 + (200 * 7) - (200 * MenuLocationY));
            ShieldSpecial.Update(gameTime);

            SlowProjectile.Sprite.SetPosY(200 + (200 * 8) - (200 * MenuLocationY));
            SlowProjectile.Update(gameTime);

            TimeStopSpecial.Sprite.SetPosY(200 + (200 * 1) - (200 * MenuLocationY));
            TimeStopSpecial.Update(gameTime);

            TripleShot.Sprite.SetPosY(200 + (200 * 7) - (200 * MenuLocationY));
            TripleShot.Update(gameTime);

            if (MenuYChanged)
            {
                MenuLocationY = 0;
                MenuYChanged = false;
            }
        }

        private void SelectionUpdate()
        {

            if (MenuLocationX > 4)
                MenuLocationX = 1;
            if (MenuLocationX < 1)
                MenuLocationX = 4;
            if (MenuLocationY > 10)
                MenuLocationY = 0;
            if (MenuLocationY < 0)
                MenuLocationY = 10;

            #region MenuSkip

            if (MenuLocationX == 4 && MenuLocationY == 1 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Right)
                MenuLocationX += 1;

            if (MenuLocationX == 4 && MenuLocationY == 1 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Left)
                MenuLocationX -= 1;

            if (MenuLocationX == 4 && MenuLocationY == 1 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Up)
                MenuLocationX -= 1;

            if (MenuLocationX == 4 && MenuLocationY == 1 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Down)
                MenuLocationX += 1;

            #endregion
            #region MenuSkip

            if (MenuLocationX == 4 && MenuLocationY == 1 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Right)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Right)
                MenuLocationX += 1;

            if (MenuLocationX == 4 && MenuLocationY == 1 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Left)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Left)
                MenuLocationX -= 1;

            if (MenuLocationX == 4 && MenuLocationY == 1 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Up)
                MenuLocationX -= 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Up)
                MenuLocationX -= 1;

            if (MenuLocationX == 4 && MenuLocationY == 1 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 2 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 4 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 4 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 5 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 3 && MenuLocationY == 6 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 6 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 7 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 1 && MenuLocationY == 8 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 2 && MenuLocationY == 8 && Down)
                MenuLocationX += 1;
            if (MenuLocationX == 4 && MenuLocationY == 10 && Down)
                MenuLocationX += 1;

            #endregion

            if (MenuLocationX > 4)
                MenuLocationX = 1;
            if (MenuLocationX < 1)
                MenuLocationX = 4;
            if (MenuLocationY > 10)
                MenuLocationY = 0;
            if (MenuLocationY < 0)
                MenuLocationY = 10;

            SelectedPosition.X = ((screenWidth / 2) - 410 - (205 / 2) - 32) + (205 * MenuLocationX);
            SelectedPosition.Y = 200;
            if (MenuLocationY != 0)
                TreePosition.Y = (332) - (200 * MenuLocationY);
            else
                TreePosition.Y = (332) - (200);
        }

        private void ControlsUpdate(GameTime gameTime)
        {
            previousGamepad1State = currentGamepad1State;
            currentGamepad1State = GamePad.GetState(PlayerIndex.One);

            if (currentGamepad1State.IsConnected)
            {
                keyboardUpdate.controlSystem = 2;
                controlSystem = 2;
            }
            else
            {
                keyboardUpdate.controlSystem = 1;
                controlSystem = 1;
            }

            keyboardUpdate.Update(gameTime);

            if (keyboardUpdate.SelectLeft)
            {
                if (MenuLocationY != 0)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    MenuLocationX -= 1;
                    Left = true;
                }

                if (MenuLocationY == 0)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    if (MenuState == 1)
                        MenuState = 4;
                    else
                        MenuState -= 1;
                }
            }
            else
                Left = false;

            if (keyboardUpdate.SelectRight)
            {
                if (MenuLocationY != 0)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    MenuLocationX += 1;
                    Right = true;
                }

                if (MenuLocationY == 0)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    if (MenuState == 4)
                        MenuState = 1;
                    else
                        MenuState += 1;
                }
            }
            else
                Right = false;

            if (keyboardUpdate.SelectUp)
            {
                if (MenuState != 3)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    MenuLocationY -= 1;
                    Up = true;
                }
            }
            else
                Up = false;

            if (keyboardUpdate.SelectDown)
            {
                if (MenuState != 3)
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    MenuLocationY += 1;
                    Down = true;
                }
            }
            else
                Down = false;

            if (keyboardUpdate.Back)
            {
                Game1.Instance.AudioPlay("Back", 1);
                if (Painting == false)
                {
                    if (MenuState == 0)
                        MainMenuState = 1;
                    if (MenuState != 0)
                    {
                        Save();
                        MainMenuState = 1;
                    }
                }
                else
                {
                    MenuLocationY = 3;
                    Painting = false;
                }
            }

            Select = keyboardUpdate.Select;

            if (MenuLocationY == 0 && Select)
            {
                if (FirstSelect)
                    FirstSelect = false;
                else
                {
                    Game1.Instance.AudioPlay("Select", 1);
                    if (MenuState == 4)
                        MenuState = 1;
                    else
                        MenuState += 1;
                }
            }
        }

        private void Save()
        {
            saveManager.playerName = playerName;
            saveManager.playerAcceleration = playerAcceleration;
            saveManager.playerBulletSpeed = playerBulletSpeed;
            saveManager.playerShip = playerShip;
            saveManager.playerMaxBullets = playerMaxBullets;
            saveManager.playerDamage = playerDamage;
            saveManager.playerRedValue = playerRedValue;
            saveManager.playerBlueValue = playerBlueValue;
            saveManager.playerGreenValue = playerGreenValue;
            saveManager.playerFireRate = playerFireRate;
            saveManager.playerCredits = playerCredits;
            saveManager.playerLives = playerLives;
            saveManager.playerMaxHealth = playerMaxHealth;
            saveManager.playerMaxEnergy = playerMaxEnergy;

            saveManager.iplayerAmmo = iplayerAmmo;
            saveManager.bplayerAutoFire = bplayerAutoFire;
            saveManager.iplayerBulletSpeed = iplayerBulletSpeed;
            saveManager.iplayerDamage = iplayerDamage;
            saveManager.iplayerElectricProjectile = iplayerElectricProjectile;
            saveManager.iplayerEnergy = iplayerEnergy;
            saveManager.iplayerEnergyProjectile = iplayerEnergyProjectile;
            saveManager.iplayerExplosiveProjectile = iplayerExplosiveProjectile;
            saveManager.iplayerFireProjectile = iplayerFireProjectile;
            saveManager.iplayerFireRate = iplayerFireRate;
            saveManager.iplayerHealingSpecial = iplayerHealingSpecial;
            saveManager.iplayerHealth = iplayerHealth;
            saveManager.iplayerHealthProjectile = iplayerHealthProjectile;
            saveManager.iplayerLaserProjectile = iplayerLaserProjectile;
            saveManager.iplayerLaserSpecial = iplayerLaserSpecial;
            saveManager.iplayerMoneySpecial = iplayerMoneySpecial;
            saveManager.iplayerMovementSpeed = iplayerMovementSpeed;
            saveManager.iplayerPoisonProjectile = iplayerPoisonProjectile;
            saveManager.iplayerShieldSpecial = iplayerShieldSpecial;
            saveManager.iplayerSlowProjectile = iplayerSlowProjectile;
            saveManager.iplayerTimeStopSpecial = iplayerTimeStopSpecial;

            saveManager.bplayerQuadShot = bplayerQuadShot;
            saveManager.bplayerQuintupleShot = bplayerQuintupleShot;
            saveManager.bplayerTripleShot = bplayerTripleShot;
            saveManager.bplayerDoubleShot = bplayerDoubleShot;
            saveManager.bplayerExtraLife1 = bplayerExtraLife1;
            saveManager.bplayerExtraLife2 = bplayerExtraLife2;
            saveManager.bplayerExtraLife3 = bplayerExtraLife3;
            saveManager.bplayerExtraLife4 = bplayerExtraLife4;

            saveManager.playerLevel = playerLevel;
            saveManager.playerDeathCount = playerDeathCount;
            saveManager.playerTimePlayedHours = playerTimePlayedHours;
            saveManager.playerTimePlayedMinutes = playerTimePlayedMinutes;
            saveManager.playerTimePlayedSeconds = playerTimePlayedSeconds;
            saveManager.playerCreditsCollected = playerCreditsCollected;
            saveManager.playerCreditsSpent = playerCreditsSpent;
            saveManager.playerWeaponsCollected = playerWeaponsCollected;
            saveManager.playerPercentageComplete = playerPercentageComplete;
            saveManager.playerBulletsFired = playerBulletsFired;
            saveManager.playerAccuracy = playerAccuracy;
            saveManager.playerEnemiesKilled = playerEnemiesKilled;
            saveManager.playerEnemiesHit = playerEnemiesHit;
            saveManager.playerMiniGamesPassed = playerMiniGamesPassed;
            saveManager.playerUpgradesPurchased = playerUpgradesPurchased;
            saveManager.playerPowerUpsCollected = playerPowerUpsCollected;
            saveManager.playerLevelsCompleted = playerLevelsCompleted;

            saveManager.playerAchievement1 = playerAchievement1;
            saveManager.playerAchievement2 = playerAchievement2;
            saveManager.playerAchievement3 = playerAchievement3;
            saveManager.playerAchievement4 = playerAchievement4;
            saveManager.playerAchievement5 = playerAchievement5;
            saveManager.playerAchievement6 = playerAchievement6;
            saveManager.playerAchievement7 = playerAchievement7;
            saveManager.playerAchievement8 = playerAchievement8;
            saveManager.playerAchievement9 = playerAchievement9;
            saveManager.playerAchievement10 = playerAchievement10;
            saveManager.playerAchievement11 = playerAchievement11;
            saveManager.playerAchievement12 = playerAchievement12;
            saveManager.playerAchievement13 = playerAchievement13;
            saveManager.playerAchievement14 = playerAchievement14;
            saveManager.playerAchievement15 = playerAchievement15;
            saveManager.playerAchievement16 = playerAchievement16;
            saveManager.playerAchievement17 = playerAchievement17;
            saveManager.playerAchievement18 = playerAchievement18;
            saveManager.playerAchievement19 = playerAchievement19;
            saveManager.playerAchievement20 = playerAchievement20;
            saveManager.playerAchievement21 = playerAchievement21;
            saveManager.playerAchievement22 = playerAchievement22;
            saveManager.playerAchievement23 = playerAchievement23;
            saveManager.playerAchievement24 = playerAchievement24;
            saveManager.playerAchievement25 = playerAchievement25;
            saveManager.playerAchievement26 = playerAchievement26;
            saveManager.playerAchievement27 = playerAchievement27;
            saveManager.playerAchievement28 = playerAchievement28;
            saveManager.playerAchievement29 = playerAchievement29;
            saveManager.playerAchievement30 = playerAchievement30;
            saveManager.playerAchievement31 = playerAchievement31;
            saveManager.playerAchievement32 = playerAchievement32;
            saveManager.playerAchievement33 = playerAchievement33;
            saveManager.playerAchievement34 = playerAchievement34;
            saveManager.playerAchievement35 = playerAchievement35;
            saveManager.playerAchievement36 = playerAchievement36;
            saveManager.playerAchievement37 = playerAchievement37;
            saveManager.playerAchievement38 = playerAchievement38;
            saveManager.playerAchievement39 = playerAchievement39;
            saveManager.playerAchievement40 = playerAchievement40;
            saveManager.playerAchievement41 = playerAchievement41;
            saveManager.playerAchievement42 = playerAchievement42;
            saveManager.playerAchievement43 = playerAchievement43;
            saveManager.playerAchievement44 = playerAchievement44;
            saveManager.playerAchievement45 = playerAchievement45;
            saveManager.playerAchievement46 = playerAchievement46;
            saveManager.playerAchievement47 = playerAchievement47;
            saveManager.playerAchievement48 = playerAchievement48;
            saveManager.playerAchievement49 = playerAchievement49;
            saveManager.playerAchievement50 = playerAchievement50;
            saveManager.playerAchievementCount = playerAchievementCount;
            saveManager.playerSelectedWeapon1 = playerSelectedWeapon1;
            saveManager.playerSelectedWeapon2 = playerSelectedWeapon2;
            saveManager.playerSelectedWeapon3 = playerSelectedWeapon3;
            saveManager.playerSelectedWeapon4 = playerSelectedWeapon4;
            saveManager.playerSelectedWeapon5 = playerSelectedWeapon5;
            saveManager.playerSelectedSpecial = playerSelectedSpecial;
            saveManager.playerShipsUnlocked = playerShipsUnlocked;
            saveManager.playerXP = playerXP;
            saveManager.playerLevelNumber = playerLevelNumber;

            saveManager.InitiateSave();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            loadPlayer.Draw(spriteBatch);

            if (Loaded == false)
            {
                spriteBatch.Draw(LoadingTexture, LoadingPosition, null, Color.White, LoadingRotation, new Vector2(LoadingTexture.Width / 2, LoadingTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            }

            if (Loaded)
            {
                #region Upgrades

                if (MenuState == 1)
                {
                    UpgradesButton.Draw(spriteBatch);

                    DrawTree(spriteBatch);

                    #region Upgrade Icon Draw

                    Ammo.Draw(spriteBatch);
                    AutoFire.Draw(spriteBatch);
                    BulletSpeed.Draw(spriteBatch);
                    Damage.Draw(spriteBatch);
                    DoubleShot.Draw(spriteBatch);
                    ElectricProjectile.Draw(spriteBatch);
                    Energy.Draw(spriteBatch);
                    EnergyProjectile.Draw(spriteBatch);
                    ExplosiveProjectile.Draw(spriteBatch);
                    ExtraLife1.Draw(spriteBatch);
                    ExtraLife2.Draw(spriteBatch);
                    ExtraLife3.Draw(spriteBatch);
                    ExtraLife4.Draw(spriteBatch);
                    FireProjectile.Draw(spriteBatch);
                    FireRate.Draw(spriteBatch);
                    HealingSpecial.Draw(spriteBatch);
                    Health.Draw(spriteBatch);
                    HealthProjectile.Draw(spriteBatch);
                    LaserProjectile.Draw(spriteBatch);
                    LaserSpecial.Draw(spriteBatch);
                    MoneySpecial.Draw(spriteBatch);
                    MovementSpeed.Draw(spriteBatch);
                    PoisonProjectile.Draw(spriteBatch);
                    QuadShot.Draw(spriteBatch);
                    QuintupleShot.Draw(spriteBatch);
                    ShieldSpecial.Draw(spriteBatch);
                    SlowProjectile.Draw(spriteBatch);
                    TimeStopSpecial.Draw(spriteBatch);
                    TripleShot.Draw(spriteBatch);

                    #endregion

                    DrawUpgradeNumbers(spriteBatch);

                    if (MenuLocationY != 0)
                    {
                        spriteBatch.Draw(SelectedTexture, SelectedPosition, Color.White);
                        spriteBatch.Draw(TextBackgroundTexture, TextBoxPosition, Color.White);
                    }

                    spriteBatch.DrawString(font, UpgradeName, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(UpgradeName).X / 2) + 10, TextBoxPosition.Y + 10), Color.White);

                    spriteBatch.DrawString(font, Description1, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description1).X / 2) + 10, TextBoxPosition.Y + 45), Color.White);
                    spriteBatch.DrawString(font, Description2, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description2).X / 2) + 10, TextBoxPosition.Y + 65), Color.White);
                    spriteBatch.DrawString(font, Description3, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description3).X / 2) + 10, TextBoxPosition.Y + 85), Color.White);

                    spriteBatch.DrawString(font, UpgradeCostString, new Vector2(TextBoxPosition.X + (TextBackgroundTexture.Width / 2) - (font.MeasureString(UpgradeCostString).X / 2), TextBoxPosition.Y + 122), Color.White);
                }

                #endregion

                #region Inventory

                if (MenuState == 2)
                {
                    if (Painting == false)
                    {
                        if (MenuLocationY == 1 || MenuLocationY == 2)
                        {
                            spriteBatch.Draw(TextBackgroundTexture, TextBoxPosition, Color.White);

                            spriteBatch.DrawString(font, UpgradeName, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(UpgradeName).X / 2) + 10, TextBoxPosition.Y + 10), Color.White);

                            spriteBatch.DrawString(font, Description1, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description1).X / 2) + 10, TextBoxPosition.Y + 45), Color.White);
                            spriteBatch.DrawString(font, Description2, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description2).X / 2) + 10, TextBoxPosition.Y + 65), Color.White);
                            spriteBatch.DrawString(font, Description3, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description3).X / 2) + 10, TextBoxPosition.Y + 85), Color.White);

                        }

                        if (SpecialSelector <= 3 || SpecialSelector == 0)
                            HealingSpecial.Draw(spriteBatch);
                        if (SpecialSelector <= 4 || SpecialSelector == 0)
                            LaserSpecial.Draw(spriteBatch);
                        MoneySpecial.Draw(spriteBatch);
                        if (SpecialSelector >= 2 || SpecialSelector == 0)
                            ShieldSpecial.Draw(spriteBatch);
                        if (SpecialSelector >= 3 || SpecialSelector == 0)
                            TimeStopSpecial.Draw(spriteBatch);

                        #region Draw Projectiles

                        if (ProjectileSelector != 1 && ProjectileSelector != 2)
                        {
                            ProjectileBackground[ProjectileSelector - 3].Draw(spriteBatch);
                            Projectile[ProjectileSelector - 3].Draw(spriteBatch);
                        }
                        if (ProjectileSelector != 1)
                        {
                            ProjectileBackground[ProjectileSelector - 2].Draw(spriteBatch);
                            Projectile[ProjectileSelector - 2].Draw(spriteBatch);
                        }
                        ProjectileBackground[ProjectileSelector - 1].Draw(spriteBatch);
                        Projectile[ProjectileSelector - 1].Draw(spriteBatch);

                        if (ProjectileSelector != playerWeaponsCollected + 8)
                        {
                            ProjectileBackground[ProjectileSelector].Draw(spriteBatch);
                            Projectile[ProjectileSelector].Draw(spriteBatch);
                        }
                        if (ProjectileSelector != playerWeaponsCollected + 8 && ProjectileSelector != playerWeaponsCollected + 7)
                        {
                            ProjectileBackground[ProjectileSelector + 1].Draw(spriteBatch);
                            Projectile[ProjectileSelector + 1].Draw(spriteBatch);
                        }

                        #endregion

                        #region Draw Ships

                        if (ShipSelector != 1 && ShipSelector != 2)
                            ShipBackground[ShipSelector - 3].Draw(spriteBatch);
                        if (ShipSelector != 1)
                            ShipBackground[ShipSelector - 2].Draw(spriteBatch);
                        ShipBackground[ShipSelector - 1].Draw(spriteBatch);
                        if (ShipSelector != playerShipsUnlocked)
                            ShipBackground[ShipSelector].Draw(spriteBatch);
                        if (ShipSelector != playerShipsUnlocked && ShipSelector != playerShipsUnlocked - 1)
                            ShipBackground[ShipSelector + 1].Draw(spriteBatch);

                        if (ShipSelector == 1)
                        {
                            Ship1.Draw(spriteBatch);
                            Hull1.Draw(spriteBatch);
                            Ship2.Draw(spriteBatch);
                            Hull2.Draw(spriteBatch);
                            Ship3.Draw(spriteBatch);
                            Hull3.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 2)
                        {
                            Ship1.Draw(spriteBatch);
                            Hull1.Draw(spriteBatch);
                            Ship2.Draw(spriteBatch);
                            Hull2.Draw(spriteBatch);
                            Ship3.Draw(spriteBatch);
                            Hull3.Draw(spriteBatch);
                            Ship4.Draw(spriteBatch);
                            Hull4.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 3)
                        {
                            Ship1.Draw(spriteBatch);
                            Hull1.Draw(spriteBatch);
                            Ship2.Draw(spriteBatch);
                            Hull2.Draw(spriteBatch);
                            Ship3.Draw(spriteBatch);
                            Hull3.Draw(spriteBatch);
                            Ship4.Draw(spriteBatch);
                            Hull4.Draw(spriteBatch);
                            Ship5.Draw(spriteBatch);
                            Hull5.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 4)
                        {
                            Ship2.Draw(spriteBatch);
                            Hull2.Draw(spriteBatch);
                            Ship3.Draw(spriteBatch);
                            Hull3.Draw(spriteBatch);
                            Ship4.Draw(spriteBatch);
                            Hull4.Draw(spriteBatch);
                            Ship5.Draw(spriteBatch);
                            Hull5.Draw(spriteBatch);
                            Ship6.Draw(spriteBatch);
                            Hull6.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 5)
                        {
                            Ship3.Draw(spriteBatch);
                            Hull3.Draw(spriteBatch);
                            Ship4.Draw(spriteBatch);
                            Hull4.Draw(spriteBatch);
                            Ship5.Draw(spriteBatch);
                            Hull5.Draw(spriteBatch);
                            Ship6.Draw(spriteBatch);
                            Hull6.Draw(spriteBatch);
                            Ship7.Draw(spriteBatch);
                            Hull7.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 6)
                        {
                            Ship4.Draw(spriteBatch);
                            Hull4.Draw(spriteBatch);
                            Ship5.Draw(spriteBatch);
                            Hull5.Draw(spriteBatch);
                            Ship6.Draw(spriteBatch);
                            Hull6.Draw(spriteBatch);
                            Ship7.Draw(spriteBatch);
                            Hull7.Draw(spriteBatch);
                            Ship8.Draw(spriteBatch);
                            Hull8.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 7)
                        {
                            Ship5.Draw(spriteBatch);
                            Hull5.Draw(spriteBatch);
                            Ship6.Draw(spriteBatch);
                            Hull6.Draw(spriteBatch);
                            Ship7.Draw(spriteBatch);
                            Hull7.Draw(spriteBatch);
                            Ship8.Draw(spriteBatch);
                            Hull8.Draw(spriteBatch);
                            Ship9.Draw(spriteBatch);
                            Hull9.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 8)
                        {
                            Ship6.Draw(spriteBatch);
                            Hull6.Draw(spriteBatch);
                            Ship7.Draw(spriteBatch);
                            Hull7.Draw(spriteBatch);
                            Ship8.Draw(spriteBatch);
                            Hull8.Draw(spriteBatch);
                            Ship9.Draw(spriteBatch);
                            Hull9.Draw(spriteBatch);
                            Ship10.Draw(spriteBatch);
                            Hull10.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 9)
                        {
                            Ship7.Draw(spriteBatch);
                            Hull7.Draw(spriteBatch);
                            Ship8.Draw(spriteBatch);
                            Hull8.Draw(spriteBatch);
                            Ship9.Draw(spriteBatch);
                            Hull9.Draw(spriteBatch);
                            Ship10.Draw(spriteBatch);
                            Hull10.Draw(spriteBatch);
                            Ship11.Draw(spriteBatch);
                            Hull11.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 10)
                        {
                            Ship8.Draw(spriteBatch);
                            Hull8.Draw(spriteBatch);
                            Ship9.Draw(spriteBatch);
                            Hull9.Draw(spriteBatch);
                            Ship10.Draw(spriteBatch);
                            Hull10.Draw(spriteBatch);
                            Ship11.Draw(spriteBatch);
                            Hull11.Draw(spriteBatch);
                            Ship12.Draw(spriteBatch);
                            Hull12.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 11)
                        {
                            Ship9.Draw(spriteBatch);
                            Hull9.Draw(spriteBatch);
                            Ship10.Draw(spriteBatch);
                            Hull10.Draw(spriteBatch);
                            Ship11.Draw(spriteBatch);
                            Hull11.Draw(spriteBatch);
                            Ship12.Draw(spriteBatch);
                            Hull12.Draw(spriteBatch);
                            Ship13.Draw(spriteBatch);
                            Hull13.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 12)
                        {
                            Ship10.Draw(spriteBatch);
                            Hull10.Draw(spriteBatch);
                            Ship11.Draw(spriteBatch);
                            Hull11.Draw(spriteBatch);
                            Ship12.Draw(spriteBatch);
                            Hull12.Draw(spriteBatch);
                            Ship13.Draw(spriteBatch);
                            Hull13.Draw(spriteBatch);
                            Ship14.Draw(spriteBatch);
                            Hull14.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 13)
                        {
                            Ship11.Draw(spriteBatch);
                            Hull11.Draw(spriteBatch);
                            Ship12.Draw(spriteBatch);
                            Hull12.Draw(spriteBatch);
                            Ship13.Draw(spriteBatch);
                            Hull13.Draw(spriteBatch);
                            Ship14.Draw(spriteBatch);
                            Hull14.Draw(spriteBatch);
                            Ship15.Draw(spriteBatch);
                            Hull15.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 14)
                        {
                            Ship12.Draw(spriteBatch);
                            Hull12.Draw(spriteBatch);
                            Ship13.Draw(spriteBatch);
                            Hull13.Draw(spriteBatch);
                            Ship14.Draw(spriteBatch);
                            Hull14.Draw(spriteBatch);
                            Ship15.Draw(spriteBatch);
                            Hull15.Draw(spriteBatch);
                            Ship16.Draw(spriteBatch);
                            Hull16.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 15)
                        {
                            Ship13.Draw(spriteBatch);
                            Hull13.Draw(spriteBatch);
                            Ship14.Draw(spriteBatch);
                            Hull14.Draw(spriteBatch);
                            Ship15.Draw(spriteBatch);
                            Hull15.Draw(spriteBatch);
                            Ship16.Draw(spriteBatch);
                            Hull16.Draw(spriteBatch);
                            Ship17.Draw(spriteBatch);
                            Hull17.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 16)
                        {
                            Ship14.Draw(spriteBatch);
                            Hull14.Draw(spriteBatch);
                            Ship15.Draw(spriteBatch);
                            Hull15.Draw(spriteBatch);
                            Ship16.Draw(spriteBatch);
                            Hull16.Draw(spriteBatch);
                            Ship17.Draw(spriteBatch);
                            Hull17.Draw(spriteBatch);
                            Ship18.Draw(spriteBatch);
                            Hull18.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 17)
                        {
                            Ship15.Draw(spriteBatch);
                            Hull15.Draw(spriteBatch);
                            Ship16.Draw(spriteBatch);
                            Hull16.Draw(spriteBatch);
                            Ship17.Draw(spriteBatch);
                            Hull17.Draw(spriteBatch);
                            Ship18.Draw(spriteBatch);
                            Hull18.Draw(spriteBatch);
                            Ship19.Draw(spriteBatch);
                            Hull19.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 18)
                        {
                            Ship16.Draw(spriteBatch);
                            Hull16.Draw(spriteBatch);
                            Ship17.Draw(spriteBatch);
                            Hull17.Draw(spriteBatch);
                            Ship18.Draw(spriteBatch);
                            Hull18.Draw(spriteBatch);
                            Ship19.Draw(spriteBatch);
                            Hull19.Draw(spriteBatch);
                            Ship20.Draw(spriteBatch);
                            Hull20.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 19)
                        {
                            Ship17.Draw(spriteBatch);
                            Hull17.Draw(spriteBatch);
                            Ship18.Draw(spriteBatch);
                            Hull18.Draw(spriteBatch);
                            Ship19.Draw(spriteBatch);
                            Hull19.Draw(spriteBatch);
                            Ship20.Draw(spriteBatch);
                            Hull20.Draw(spriteBatch);
                        }
                        else if (ShipSelector == 20)
                        {
                            Ship18.Draw(spriteBatch);
                            Hull18.Draw(spriteBatch);
                            Ship19.Draw(spriteBatch);
                            Hull19.Draw(spriteBatch);
                            Ship20.Draw(spriteBatch);
                            Hull20.Draw(spriteBatch);
                        }

                        #endregion

                        if (MenuLocationY != 0 && MenuLocationY != 4)
                            spriteBatch.Draw(SelectedTexture, SelectedPosition, Color.White);

                        InventoryButton.Draw(spriteBatch);
                    }
                    else
                    {
                        player.Draw(spriteBatch);
                        playerHull.Draw(spriteBatch);
                        spriteBatch.Draw(ColorPallet, PalletPosition, Color.White);
                        spriteBatch.Draw(ColorSelectTexture, ColorSelectPosition, Color.White);
                    }
                }

                #endregion

                #region Stats

                if (MenuState == 3)
                {
                    spriteBatch.Draw(StatsBackgroundTexture, new Vector2((screenWidth / 2) - (StatsBackgroundTexture.Width / 2), 175), Color.White);

                    spriteBatch.DrawString(font, "Percent Complete", new Vector2((screenWidth / 2) - 300, 200), Color.White);
                    spriteBatch.DrawString(font, "Accuracy", new Vector2((screenWidth / 2) - 300, 220), Color.White);
                    spriteBatch.DrawString(font, "Bullets Fired", new Vector2((screenWidth / 2) - 300, 240), Color.White);
                    spriteBatch.DrawString(font, "Enemies Killed", new Vector2((screenWidth / 2) - 300, 260), Color.White);
                    spriteBatch.DrawString(font, "Times Died", new Vector2((screenWidth / 2) - 300, 280), Color.White);
                    spriteBatch.DrawString(font, "Time Played", new Vector2((screenWidth / 2) - 300, 300), Color.White);
                    spriteBatch.DrawString(font, "Credits Collected", new Vector2((screenWidth / 2) - 300, 320), Color.White);
                    spriteBatch.DrawString(font, "Credits Spent", new Vector2((screenWidth / 2) - 300, 340), Color.White);
                    spriteBatch.DrawString(font, "Levels Passed", new Vector2((screenWidth / 2) - 300, 360), Color.White);
                    spriteBatch.DrawString(font, "Highest Level", new Vector2((screenWidth / 2) - 300, 380), Color.White);
                    spriteBatch.DrawString(font, "Minigames Passed", new Vector2((screenWidth / 2) - 300, 400), Color.White);
                    spriteBatch.DrawString(font, "PowerUps Collected", new Vector2((screenWidth / 2) - 300, 420), Color.White);
                    spriteBatch.DrawString(font, "Ships Unlocked", new Vector2((screenWidth / 2) - 300, 440), Color.White);
                    spriteBatch.DrawString(font, "Achievements Unlocked", new Vector2((screenWidth / 2) - 300, 460), Color.White);
                    spriteBatch.DrawString(font, "Weapons Unlocked", new Vector2((screenWidth / 2) - 300, 480), Color.White);
                    spriteBatch.DrawString(font, "Upgrades Purchased", new Vector2((screenWidth / 2) - 300, 500), Color.White);
                    spriteBatch.DrawString(font, "Player Level", new Vector2((screenWidth / 2) - 300, 540), Color.White);

                    spriteBatch.DrawString(font, "" + playerPercentageComplete + "%", new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerPercentageComplete + "%").X, 200), Color.White);
                    spriteBatch.DrawString(font, "" + playerAccuracy + "%", new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerAccuracy + "%").X, 220), Color.White);
                    spriteBatch.DrawString(font, "" + playerBulletsFired, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerBulletsFired).X, 240), Color.White);
                    spriteBatch.DrawString(font, "" + playerEnemiesKilled, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerEnemiesKilled).X, 260), Color.White);
                    spriteBatch.DrawString(font, "" + playerDeathCount, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerDeathCount).X, 280), Color.White);
                    spriteBatch.DrawString(font, "" + playTime, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playTime).X, 300), Color.White);
                    spriteBatch.DrawString(font, "" + playerCreditsCollected, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerCreditsCollected).X, 320), Color.White);
                    spriteBatch.DrawString(font, "" + playerCreditsSpent, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerCreditsSpent).X, 340), Color.White);
                    spriteBatch.DrawString(font, "" + playerLevelsCompleted, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerLevelsCompleted).X, 360), Color.White);
                    spriteBatch.DrawString(font, "" + playerLevelNumber, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerLevelNumber).X, 380), Color.White);
                    spriteBatch.DrawString(font, "" + playerMiniGamesPassed, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerMiniGamesPassed).X, 400), Color.White);
                    spriteBatch.DrawString(font, "" + playerPowerUpsCollected, new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerPowerUpsCollected).X, 420), Color.White);
                    spriteBatch.DrawString(font, "" + playerShipsUnlocked + "/20", new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + playerShipsUnlocked + "/20").X, 440), Color.White);
                    spriteBatch.DrawString(font, playerAchievementCount + "/50", new Vector2((screenWidth / 2) + 300 - font.MeasureString(playerAchievementCount + "/50").X, 460), Color.White);
                    spriteBatch.DrawString(font, "" + (playerWeaponsCollected + UpgradeUnlocks) + "/0", new Vector2((screenWidth / 2) + 300 - font.MeasureString("" + (playerWeaponsCollected + UpgradeUnlocks) + "/0").X, 480), Color.White);
                    spriteBatch.DrawString(font, playerUpgradesPurchased + "/209", new Vector2((screenWidth / 2) + 300 - font.MeasureString(playerUpgradesPurchased + "/209").X, 500), Color.White);
                    spriteBatch.DrawString(font, playerLevel + "", new Vector2((screenWidth / 2) + 300 - font.MeasureString(playerLevel + "").X, 540), Color.White);

                    StatsButton.Draw(spriteBatch);
                }

                #endregion

                #region Achievements

                if (MenuState == 4)
                {
                    Achievement1.Draw(spriteBatch);
                    Achievement2.Draw(spriteBatch);
                    Achievement3.Draw(spriteBatch);
                    Achievement4.Draw(spriteBatch);
                    Achievement5.Draw(spriteBatch);
                    Achievement6.Draw(spriteBatch);
                    Achievement7.Draw(spriteBatch);
                    Achievement8.Draw(spriteBatch);
                    Achievement9.Draw(spriteBatch);
                    Achievement10.Draw(spriteBatch);
                    Achievement11.Draw(spriteBatch);
                    Achievement12.Draw(spriteBatch);
                    Achievement13.Draw(spriteBatch);
                    Achievement14.Draw(spriteBatch);
                    Achievement15.Draw(spriteBatch);
                    Achievement16.Draw(spriteBatch);
                    Achievement17.Draw(spriteBatch);
                    Achievement18.Draw(spriteBatch);
                    Achievement19.Draw(spriteBatch);
                    Achievement20.Draw(spriteBatch);
                    Achievement21.Draw(spriteBatch);
                    Achievement22.Draw(spriteBatch);
                    Achievement23.Draw(spriteBatch);
                    Achievement24.Draw(spriteBatch);
                    Achievement25.Draw(spriteBatch);
                    Achievement26.Draw(spriteBatch);
                    Achievement27.Draw(spriteBatch);
                    Achievement28.Draw(spriteBatch);
                    Achievement29.Draw(spriteBatch);
                    Achievement30.Draw(spriteBatch);
                    Achievement31.Draw(spriteBatch);
                    Achievement32.Draw(spriteBatch);
                    Achievement33.Draw(spriteBatch);
                    Achievement34.Draw(spriteBatch);
                    Achievement35.Draw(spriteBatch);
                    Achievement36.Draw(spriteBatch);
                    Achievement37.Draw(spriteBatch);
                    Achievement38.Draw(spriteBatch);
                    Achievement39.Draw(spriteBatch);
                    Achievement40.Draw(spriteBatch);
                    Achievement41.Draw(spriteBatch);
                    Achievement42.Draw(spriteBatch);
                    Achievement43.Draw(spriteBatch);
                    Achievement44.Draw(spriteBatch);
                    Achievement45.Draw(spriteBatch);
                    Achievement46.Draw(spriteBatch);
                    Achievement47.Draw(spriteBatch);
                    Achievement48.Draw(spriteBatch);
                    Achievement49.Draw(spriteBatch);
                    Achievement50.Draw(spriteBatch);

                    if (MenuLocationY != 0)
                        spriteBatch.Draw(SelectedTexture, SelectedPosition, Color.White);

                    if (MenuLocationY != 0)
                    {
                        spriteBatch.Draw(TextBackgroundTexture, TextBoxPosition, Color.White);

                        spriteBatch.DrawString(font, UpgradeName, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(UpgradeName).X / 2) + 10, TextBoxPosition.Y + 10), Color.White);

                        spriteBatch.DrawString(font, Description1, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description1).X / 2) + 10, TextBoxPosition.Y + 45), Color.White);
                        spriteBatch.DrawString(font, Description2, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description2).X / 2) + 10, TextBoxPosition.Y + 65), Color.White);
                        spriteBatch.DrawString(font, Description3, new Vector2((TextBoxPosition.X + (TextBackgroundTexture.Width / 2)) - (font.MeasureString(Description3).X / 2) + 10, TextBoxPosition.Y + 85), Color.White);
                    }

                    AchievementsButton.Draw(spriteBatch);
                }

                #endregion

                #region Achievement Tracker

                if (MenuState != 0 && MenuState != 4)
                {
                    if (DrawAchievement1)
                        Achievement1.Draw(spriteBatch);
                    if (DrawAchievement2)
                        Achievement2.Draw(spriteBatch);
                    if (DrawAchievement3)
                        Achievement3.Draw(spriteBatch);
                    if (DrawAchievement4)
                        Achievement4.Draw(spriteBatch);
                    if (DrawAchievement5)
                        Achievement5.Draw(spriteBatch);
                    if (DrawAchievement6)
                        Achievement6.Draw(spriteBatch);
                    if (DrawAchievement7)
                        Achievement7.Draw(spriteBatch);
                    if (DrawAchievement8)
                        Achievement8.Draw(spriteBatch);
                    if (DrawAchievement9)
                        Achievement9.Draw(spriteBatch);
                    if (DrawAchievement10)
                        Achievement10.Draw(spriteBatch);
                    if (DrawAchievement11)
                        Achievement11.Draw(spriteBatch);
                    if (DrawAchievement12)
                        Achievement12.Draw(spriteBatch);
                    if (DrawAchievement13)
                        Achievement13.Draw(spriteBatch);
                    if (DrawAchievement14)
                        Achievement14.Draw(spriteBatch);
                    if (DrawAchievement15)
                        Achievement15.Draw(spriteBatch);
                    if (DrawAchievement16)
                        Achievement16.Draw(spriteBatch);
                    if (DrawAchievement17)
                        Achievement17.Draw(spriteBatch);
                    if (DrawAchievement18)
                        Achievement18.Draw(spriteBatch);
                    if (DrawAchievement19)
                        Achievement19.Draw(spriteBatch);
                    if (DrawAchievement20)
                        Achievement20.Draw(spriteBatch);
                    if (DrawAchievement21)
                        Achievement21.Draw(spriteBatch);
                    if (DrawAchievement22)
                        Achievement22.Draw(spriteBatch);
                    if (DrawAchievement23)
                        Achievement23.Draw(spriteBatch);
                    if (DrawAchievement24)
                        Achievement24.Draw(spriteBatch);
                    if (DrawAchievement25)
                        Achievement25.Draw(spriteBatch);
                    if (DrawAchievement26)
                        Achievement26.Draw(spriteBatch);
                    if (DrawAchievement27)
                        Achievement27.Draw(spriteBatch);
                    if (DrawAchievement28)
                        Achievement28.Draw(spriteBatch);
                    if (DrawAchievement29)
                        Achievement29.Draw(spriteBatch);
                    if (DrawAchievement30)
                        Achievement30.Draw(spriteBatch);
                    if (DrawAchievement31)
                        Achievement31.Draw(spriteBatch);
                    if (DrawAchievement32)
                        Achievement32.Draw(spriteBatch);
                    if (DrawAchievement33)
                        Achievement33.Draw(spriteBatch);
                    if (DrawAchievement34)
                        Achievement34.Draw(spriteBatch);
                    if (DrawAchievement35)
                        Achievement35.Draw(spriteBatch);
                    if (DrawAchievement36)
                        Achievement36.Draw(spriteBatch);
                    if (DrawAchievement37)
                        Achievement37.Draw(spriteBatch);
                    if (DrawAchievement38)
                        Achievement38.Draw(spriteBatch);
                    if (DrawAchievement39)
                        Achievement39.Draw(spriteBatch);
                    if (DrawAchievement40)
                        Achievement40.Draw(spriteBatch);
                    if (DrawAchievement41)
                        Achievement41.Draw(spriteBatch);
                    if (DrawAchievement42)
                        Achievement42.Draw(spriteBatch);
                    if (DrawAchievement43)
                        Achievement43.Draw(spriteBatch);
                    if (DrawAchievement44)
                        Achievement44.Draw(spriteBatch);
                    if (DrawAchievement45)
                        Achievement45.Draw(spriteBatch);
                    if (DrawAchievement46)
                        Achievement46.Draw(spriteBatch);
                    if (DrawAchievement47)
                        Achievement47.Draw(spriteBatch);
                    if (DrawAchievement48)
                        Achievement48.Draw(spriteBatch);
                    if (DrawAchievement49)
                        Achievement49.Draw(spriteBatch);
                    if (DrawAchievement50)
                        Achievement50.Draw(spriteBatch);
                }

                #endregion

                if (MenuState != 0)
                {
                    spriteBatch.Draw(BlankButton, new Vector2(-100, -20), Color.White);
                    spriteBatch.Draw(BlankButton, new Vector2((screenWidth - BlankButton.Width) + 100, -20), Color.White);

                    spriteBatch.Draw(MoneyTexture, new Vector2((screenWidth - 20), 18), null, Color.White, 0f, new Vector2(MoneyTexture.Width / 2, MoneyTexture.Height / 2), 0.6f, SpriteEffects.None, 1);
                    spriteBatch.DrawString(font, "" + playerName, new Vector2(12, 10), Color.Black);
                    spriteBatch.DrawString(font, "" + playerCredits, new Vector2((screenWidth - 40) - font.MeasureString("" + playerCredits).X, 10), Color.Black);
                }
            }
        }

        private void DrawTree(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TreeBackgroundTexture, TreePosition, Color.White);
            if (Activated1)
                spriteBatch.Draw(Tree1ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree1Texture, TreePosition, Color.White);

            if (Activated2)
                spriteBatch.Draw(Tree2ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree2Texture, TreePosition, Color.White);

            if (Activated3)
                spriteBatch.Draw(Tree3ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree3Texture, TreePosition, Color.White);

            if (Activated4)
                spriteBatch.Draw(Tree4ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree4Texture, TreePosition, Color.White);

            if (Activated5)
                spriteBatch.Draw(Tree5ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree5Texture, TreePosition, Color.White);

            if (Activated6)
                spriteBatch.Draw(Tree6ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree6Texture, TreePosition, Color.White);

            if (Activated7)
                spriteBatch.Draw(Tree7ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree7Texture, TreePosition, Color.White);

            if (Activated8)
                spriteBatch.Draw(Tree8ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree8Texture, TreePosition, Color.White);

            if (Activated9)
                spriteBatch.Draw(Tree9ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree9Texture, TreePosition, Color.White);

            if (Activated10)
                spriteBatch.Draw(Tree10ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree10Texture, TreePosition, Color.White);

            if (Activated11)
                spriteBatch.Draw(Tree11ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree11Texture, TreePosition, Color.White);

            if (Activated12)
                spriteBatch.Draw(Tree12ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree12Texture, TreePosition, Color.White);

            if (Activated13)
                spriteBatch.Draw(Tree13ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree13Texture, TreePosition, Color.White);

            if (Activated14)
                spriteBatch.Draw(Tree14ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree14Texture, TreePosition, Color.White);

            if (Activated15)
                spriteBatch.Draw(Tree15ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree15Texture, TreePosition, Color.White);

            if (Activated16)
                spriteBatch.Draw(Tree16ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree16Texture, TreePosition, Color.White);

            if (Activated17)
                spriteBatch.Draw(Tree17ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree17Texture, TreePosition, Color.White);

            if (Activated18)
                spriteBatch.Draw(Tree18ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree18Texture, TreePosition, Color.White);

            if (Activated19)
                spriteBatch.Draw(Tree19ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree19Texture, TreePosition, Color.White);

            if (Activated20)
                spriteBatch.Draw(Tree20ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree20Texture, TreePosition, Color.White);

            if (Activated21)
                spriteBatch.Draw(Tree21ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree21Texture, TreePosition, Color.White);

            if (Activated22)
                spriteBatch.Draw(Tree22ActivatedTexture, TreePosition, Color.White);
            else
                spriteBatch.Draw(Tree22Texture, TreePosition, Color.White);
        }

        private void DrawUpgradeNumbers(SpriteBatch spriteBatch)
        {
            if (iplayerAmmo != 0 && iplayerAmmo != 10)
                spriteBatch.DrawString(font, "" + iplayerAmmo, new Vector2(Ammo.Position.X + 5, Ammo.Position.Y + 5), Color.Black);

            if (iplayerBulletSpeed != 0 && iplayerBulletSpeed != 10)
                spriteBatch.DrawString(font, "" + iplayerBulletSpeed, new Vector2(BulletSpeed.Position.X + 5, BulletSpeed.Position.Y + 5), Color.Black);

            if (iplayerDamage != 0 && iplayerDamage != 10)
                spriteBatch.DrawString(font, "" + iplayerDamage, new Vector2(Damage.Position.X + 5, Damage.Position.Y + 5), Color.Black);

            if (iplayerElectricProjectile != 0 && iplayerElectricProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerElectricProjectile, new Vector2(ElectricProjectile.Position.X + 5, ElectricProjectile.Position.Y + 5), Color.Black);

            if (iplayerEnergy != 0 && iplayerEnergy != 10)
                spriteBatch.DrawString(font, "" + iplayerEnergy, new Vector2(Energy.Position.X + 5, Energy.Position.Y + 5), Color.Black);

            if (iplayerEnergyProjectile != 0 && iplayerEnergyProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerEnergyProjectile, new Vector2(EnergyProjectile.Position.X + 5, EnergyProjectile.Position.Y + 5), Color.Black);

            if (iplayerExplosiveProjectile != 0 && iplayerExplosiveProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerExplosiveProjectile, new Vector2(ExplosiveProjectile.Position.X + 5, ExplosiveProjectile.Position.Y + 5), Color.Black);

            if (iplayerFireProjectile != 0 && iplayerFireProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerFireProjectile, new Vector2(FireProjectile.Position.X + 5, FireProjectile.Position.Y + 5), Color.Black);

            if (iplayerFireRate != 0 && iplayerFireRate != 10)
                spriteBatch.DrawString(font, "" + iplayerFireRate, new Vector2(FireRate.Position.X + 5, FireRate.Position.Y + 5), Color.Black);

            if (iplayerHealingSpecial != 0 && iplayerHealingSpecial != 10)
                spriteBatch.DrawString(font, "" + iplayerHealingSpecial, new Vector2(HealingSpecial.Position.X + 5, HealingSpecial.Position.Y + 5), Color.Black);

            if (iplayerHealth != 0 && iplayerHealth != 10)
                spriteBatch.DrawString(font, "" + iplayerHealth, new Vector2(Health.Position.X + 5, Health.Position.Y + 5), Color.Black);

            if (iplayerHealthProjectile != 0 && iplayerHealthProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerHealthProjectile, new Vector2(HealthProjectile.Position.X + 5, HealthProjectile.Position.Y + 5), Color.Black);

            if (iplayerLaserProjectile != 0 && iplayerLaserProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerLaserProjectile, new Vector2(LaserProjectile.Position.X + 5, LaserProjectile.Position.Y + 5), Color.Black);

            if (iplayerLaserSpecial != 0 && iplayerLaserSpecial != 10)
                spriteBatch.DrawString(font, "" + iplayerLaserSpecial, new Vector2(LaserSpecial.Position.X + 5, LaserSpecial.Position.Y + 5), Color.Black);

            if (iplayerMoneySpecial != 0 && iplayerMoneySpecial != 10)
                spriteBatch.DrawString(font, "" + iplayerMoneySpecial, new Vector2(MoneySpecial.Position.X + 5, MoneySpecial.Position.Y + 5), Color.Black);

            if (iplayerMovementSpeed != 0 && iplayerMovementSpeed != 10)
                spriteBatch.DrawString(font, "" + iplayerMovementSpeed, new Vector2(MovementSpeed.Position.X + 5, MovementSpeed.Position.Y + 5), Color.Black);

            if (iplayerPoisonProjectile != 0 && iplayerPoisonProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerPoisonProjectile, new Vector2(PoisonProjectile.Position.X + 5, PoisonProjectile.Position.Y + 5), Color.Black);

            if (iplayerShieldSpecial != 0 && iplayerShieldSpecial != 10)
                spriteBatch.DrawString(font, "" + iplayerShieldSpecial, new Vector2(ShieldSpecial.Position.X + 5, ShieldSpecial.Position.Y + 5), Color.Black);

            if (iplayerSlowProjectile != 0 && iplayerSlowProjectile != 10)
                spriteBatch.DrawString(font, "" + iplayerSlowProjectile, new Vector2(SlowProjectile.Position.X + 5, SlowProjectile.Position.Y + 5), Color.Black);

            if (iplayerTimeStopSpecial != 0 && iplayerTimeStopSpecial != 10)
                spriteBatch.DrawString(font, "" + iplayerTimeStopSpecial, new Vector2(TimeStopSpecial.Position.X + 5, TimeStopSpecial.Position.Y + 5), Color.Black);
        }
    }
}
