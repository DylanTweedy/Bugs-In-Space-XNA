using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BugsInSpace
{
    class Player
    {

        #region Variables

        Random random;
        public MobileSprite player;
        public MobileSprite playerHull;
        Texture2D shipTexture;
        Texture2D hullTexture;
        int MaxX;
        int MaxY;
        int MinX;
        int MinY;
        int YLock;
        int screenCenterX;
        int screenCenterY;
        public int controlSystem;
        public bool Active;
        public bool isFiring;
        public bool autoFire;
        public bool autoFireOff;
        public List<Projectile> projectiles;
        TimeSpan fireTime;
        TimeSpan previousFireTime;
        int projectileWidth;
        int projectileHeight;
        public int PlayerCredits;
        public bool hit;
        public bool beenHit;
        public string PlayerName;
        public int PlayerLives;
        public int PlayerMaxHealth;
        public int PlayerEnergy;
        public int PlayerMaxEnergy;
        TimeSpan spawnTime;
        TimeSpan previousSpawnTime;
        public bool Alive;
        bool Left;
        bool Right;
        TimeSpan energyDrainTime;
        TimeSpan previousEnergyDrainTime;
        public bool Draining;
        public bool GameOver;
        public KeyboardUpdate keyboardUpdate;
        PowerUps PowerUp;
        TimeSpan Timer;
        TimeSpan previousTimer;
        bool StartTimer;
        public Color PlayerColor;
        int bulletCounter;
        int fireStyle;
        int availableWeapons;
        public bool paused;
        PauseMenu pauseMenu;
        ParticleEngine particleEngine;
        float EmitterMinX;
        float EmitterMaxX;
        float EmitterMinY;
        float EmitterMaxY;
        ParticleEngine deathParticles;
        ParticleEngine hitParticles;
        TimeSpan HitTime;
        TimeSpan previousHitTime;
        public bool Stunned;
        public float StunTime;
        TimeSpan StunTimer;
        TimeSpan PreviousStunTimer;
        bool InitiateStun;
        ParticleEngine StunParticle;
        public bool Burned;
        public float BurnTime;
        TimeSpan BurnTimer;
        TimeSpan PreviousBurnTimer;
        bool InitiateBurn;
        ParticleEngine BurnParticle;
        TimeSpan BurnAdd;
        TimeSpan PreviousBurnAdd;
        public int BurnDamage;
        int BurnCounter;
        bool Recovered;
        public bool Poisoned;
        public float PoisonTime;
        TimeSpan PoisonTimer;
        TimeSpan PreviousPoisonTimer;
        bool InitiatePoison;
        ParticleEngine PoisonParticle;
        TimeSpan PoisonAdd;
        TimeSpan PreviousPoisonAdd;
        public int PoisonDamage;
        int PoisonCounter;
        public bool Slowed;
        public float SlowTime;
        TimeSpan SlowTimer;
        TimeSpan PreviousSlowTimer;
        bool InitiateSlow;
        ParticleEngine SlowParticle;
        float OriginalAcceleration;
        float OriginalBulletSpeed;
        float OriginalFireRate;
        public Vector2 ClosestEnemy;
        int HomingProjectiles;
        int MaxHomingProjectiles;
        TimeSpan PreviousExperienceBonus;
        TimeSpan ExperienceBonus;
        int PreviousEnemiesHit;
        int PreviousProjectilesFired;
        public int ExperienceToLevel;
        public int ExperienceSubtract;
        public int GameCredits;
        int PreviousCredits;
        int previousLevel;
        bool LevelUp;
        ParticleEngine LevelUpParticles;
        TimeSpan LevelUpTime;
        TimeSpan PreviousLevelUpTime;
        int RedSkull;
        int GreenSkull;
        int PinkSkull;
        int YellowSkull;
        int BlueSkull;
        public bool Healing;
        public bool Shield;
        public bool TimeStopActivator;
        public bool HealingActivator;
        public bool ShieldActivator;
        int EnergyCost;
        ParticleEngine HealingParticles;
        public int HealthIncrease;
        public bool CureStatus;
        public bool GetExtraLife;
        public int RecievedHealthIncrease;
        public bool RecievedCureStatus;
        public bool RecievedGetExtraLife;
        TimeSpan HealingTime;
        TimeSpan PreviousHealingTime;
        int HealingCounter;
        TimeSpan EnergyDrain;
        TimeSpan PreviousEnergyDrain;
        int EnergyDrainCounter;
        TimeSpan HealingSound;
        TimeSpan PreviousHealingSound;
        TimeSpan DeadTime;
        TimeSpan PreviousDeadTime;
        float LaserDamage;
        int LaserWidth;
        bool RandomStatusChance;
        public int RandomStatusNumber;
        bool FireRockets;
        bool Laser;
        TimeSpan LaserSound;
        TimeSpan PreviousLaserSound;
        TimeSpan LaserSound2;
        TimeSpan PreviousLaserSound2;
        TimeSpan LaserDamageTime;
        TimeSpan PreviousLaserDamageTime;
        bool LoadLaser;
        TimeSpan LaserFireTime;
        TimeSpan PreviousLaserFireTime;
        Texture2D LaserTexture;
        public Vector2[] LaserPositions;
        public Rectangle[] LaserRectangles;
        public Rectangle[] LaserRectanglesHitBox;
        bool LaserLoaded;
        Texture2D LaserGlow;
        Vector2 LaserGlowPosition;
        bool DrawGlow;
        int LaserCounter;
        bool LaserStarted;
        public bool LaserFired;
        TimeSpan RocketFireTime;
        TimeSpan PreviousRocketFireTime;
        int RocketFired;
        int BulletFiring;
        TimeSpan PreviousShotVolley;
        TimeSpan ShotVolley;
        bool VolleyOver;
        public int FinalLaserDamage;
        int LaserDamageCounter;
        public float MoneyValueMultiplier;
        public bool MoneyActivator;
        bool Money;
        public bool LargeCrystals;
        public bool FirePowerUps;
        ParticleEngine MoneyParticles;
        ParticleEngine LaserParticles;
        TimeSpan PreviousMoneySound;
        TimeSpan MoneySound;
        public bool TimeStop;
        int CurrentWeapon;


        #endregion variables

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

        #region Player Variables

        public float PlayerAcceleration;
        public float PlayerStoppingSpeed;
        public float PlayerMaxSpeed;
        public float PlayerBulletSpeed;
        public int PlayerShip;
        public int PlayerMaxBullets;
        public int PlayerHealth;
        public int PlayerDamage;
        public float PlayerFireRate;
        public bool PlayerAutoFire;
        public int PlayerFireStyle;
        public int PlayerRedValue;
        public int PlayerBlueValue;
        public int PlayerGreenValue;

        public int iPlayerAmmo;
        public int iPlayerBulletSpeed;
        public int iPlayerDamage;
        public int iPlayerElectricProjectile;
        public int iPlayerEnergy;
        public int iPlayerEnergyProjectile;
        public int iPlayerExplosiveProjectile;
        public int iPlayerFireProjectile;
        public int iPlayerFireRate;
        public int iPlayerHealingSpecial;
        public int iPlayerHealth;
        public int iPlayerHealthProjectile;
        public int iPlayerLaserProjectile;
        public int iPlayerLaserSpecial;
        public int iPlayerMoneySpecial;
        public int iPlayerMovementSpeed;
        public int iPlayerPoisonProjectile;
        public int iPlayerShieldSpecial;
        public int iPlayerSlowProjectile;
        public int iPlayerTimeStopSpecial;

        public bool bPlayerAutoFire;
        public bool bPlayerTripleShot;
        public bool bPlayerExtraLife1;
        public bool bPlayerExtraLife2;
        public bool bPlayerExtraLife3;
        public bool bPlayerExtraLife4;
        public bool bPlayerQuadShot;
        public bool bPlayerQuintupleShot;
        public bool bPlayerDoubleShot;

        public int PlayerLevel;
        public int PlayerDeathCount;
        public int PlayerTimePlayedHours;
        public int PlayerTimePlayedMinutes;
        public int PlayerTimePlayedSeconds;
        public int PlayerCreditsCollected;
        public int PlayerCreditsSpent;
        public int PlayerWeaponsCollected;
        public int PlayerPercentageComplete;
        public int PlayerBulletsFired;
        public int PlayerAccuracy;
        public int PlayerEnemiesKilled;
        public int PlayerEnemiesHit;
        public int PlayerMiniGamesPassed;
        public int PlayerUpgradesPurchased;
        public int PlayerPowerUpsCollected;
        public int PlayerLevelsCompleted;

        public bool PlayerAchievement1;
        public bool PlayerAchievement2;
        public bool PlayerAchievement3;
        public bool PlayerAchievement4;
        public bool PlayerAchievement5;
        public bool PlayerAchievement6;
        public bool PlayerAchievement7;
        public bool PlayerAchievement8;
        public bool PlayerAchievement9;
        public bool PlayerAchievement10;
        public bool PlayerAchievement11;
        public bool PlayerAchievement12;
        public bool PlayerAchievement13;
        public bool PlayerAchievement14;
        public bool PlayerAchievement15;
        public bool PlayerAchievement16;
        public bool PlayerAchievement17;
        public bool PlayerAchievement18;
        public bool PlayerAchievement19;
        public bool PlayerAchievement20;
        public bool PlayerAchievement21;
        public bool PlayerAchievement22;
        public bool PlayerAchievement23;
        public bool PlayerAchievement24;
        public bool PlayerAchievement25;
        public bool PlayerAchievement26;
        public bool PlayerAchievement27;
        public bool PlayerAchievement28;
        public bool PlayerAchievement29;
        public bool PlayerAchievement30;
        public bool PlayerAchievement31;
        public bool PlayerAchievement32;
        public bool PlayerAchievement33;
        public bool PlayerAchievement34;
        public bool PlayerAchievement35;
        public bool PlayerAchievement36;
        public bool PlayerAchievement37;
        public bool PlayerAchievement38;
        public bool PlayerAchievement39;
        public bool PlayerAchievement40;
        public bool PlayerAchievement41;
        public bool PlayerAchievement42;
        public bool PlayerAchievement43;
        public bool PlayerAchievement44;
        public bool PlayerAchievement45;
        public bool PlayerAchievement46;
        public bool PlayerAchievement47;
        public bool PlayerAchievement48;
        public bool PlayerAchievement49;
        public bool PlayerAchievement50;
        public int PlayerAchievementCount;
        public int PlayerSelectedWeapon1;
        public int PlayerSelectedWeapon2;
        public int PlayerSelectedWeapon3;
        public int PlayerSelectedWeapon4;
        public int PlayerSelectedWeapon5;
        public int PlayerSelectedSpecial;
        public int PlayerShipsUnlocked;
        public int PlayerXP;
        public int PlayerLevelNumber;

        #endregion

        #region Final Values

        public float FinalAcceleration;
        TimeSpan FinalFireRate;
        public int FinalMaxBullets;
        public float FinalBulletSpeedY;
        public float FinalBulletSpeedX;
        public int FinalDamage;
        public int FinalLives;

        #endregion

        #region Bonus Values

        public bool powerUpActivated;
        public int powerUp;

        public float BonusAcceleration;
        public float BonusFireRate;
        public int BonusMaxBullets;
        public float BonusBulletSpeed;
        public int BonusDamage;
        public bool BonusAutoFire;
        public int BonusLives;

        public float AccelerationPool;
        public float FireRatePool;
        public int MaxBulletsPool;
        public int HealthPool;
        public int EnergyPool;
        public int DamagePool;
        public int LivesPool;

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

        public int Height
        {
            get { return player.Sprite.Height; }
        }

        public int Width
        {
            get { return player.Sprite.Width; }
        }

        public Vector2 Position
        {
            get { return player.Position; }
        }

        public bool IsFiring
        {
            get { return isFiring; }
            set { isFiring = false; }
        }

        #endregion properties

        public void Initialize(int ControlSystem, Vector2 iconPosition)
        {
            random = new Random();
            controlSystem = ControlSystem;
            InitializePowerUps();
            MaxX = screenWidth;
            MaxY = screenHeight;
            MinX = 0;
            MinY = 0;
            YLock = 20;
            screenCenterX = (screenWidth / 2);
            screenCenterY = (screenHeight / 2);
            autoFire = PlayerAutoFire;
            autoFireOff = true;
            projectiles = new List<Projectile>();
            fireTime = TimeSpan.FromSeconds(PlayerFireRate);
            spawnTime = TimeSpan.FromSeconds(2.5f);
            energyDrainTime = TimeSpan.FromSeconds(1f);
            previousEnergyDrainTime = TimeSpan.Zero;
            Active = false;
            hit = false;
            beenHit = true;
            Alive = true;
            Draining = false;
            Timer = TimeSpan.FromSeconds(1f);
            StartTimer = true;
            keyboardUpdate = new KeyboardUpdate();
            keyboardUpdate.Initialize(ControlSystem, 0);
            PowerUp = new PowerUps();
            InitializeAchievements();
            fireStyle = 1;
            bulletCounter = 1;
            paused = false;
            pauseMenu = new PauseMenu();
            HitTime = TimeSpan.FromSeconds(0.2f);
            previousHitTime = TimeSpan.Zero;
            pauseMenu.Initialize(controlSystem, iconPosition);
            pauseMenu.RecievingVariables();
            Stunned = false;
            InitiateStun = true;
            StunTime = 0;
            PreviousStunTimer = TimeSpan.Zero;
            StunTimer = TimeSpan.FromSeconds(StunTime);
            Burned = false;
            InitiateBurn = true;
            BurnTime = 0;
            BurnDamage = 0;
            BurnCounter = 0;
            PreviousBurnTimer = TimeSpan.Zero;
            BurnTimer = TimeSpan.FromSeconds(BurnTime);
            PreviousBurnAdd = TimeSpan.Zero;
            BurnAdd = TimeSpan.FromSeconds(0.1f);
            Recovered = true;
            Poisoned = false;
            InitiatePoison = true;
            PoisonTime = 0;
            PoisonDamage = 0;
            PoisonCounter = 0;
            PreviousPoisonTimer = TimeSpan.Zero;
            PoisonTimer = TimeSpan.FromSeconds(PoisonTime);
            PreviousPoisonAdd = TimeSpan.Zero;
            PoisonAdd = TimeSpan.FromSeconds(0.1f);
            Slowed = false;
            InitiateSlow = true;
            SlowTime = 0;
            PreviousSlowTimer = TimeSpan.Zero;
            SlowTimer = TimeSpan.FromSeconds(SlowTime);
            OriginalAcceleration = PlayerAcceleration;
            OriginalBulletSpeed = PlayerBulletSpeed;
            OriginalFireRate = PlayerFireRate;
            ClosestEnemy = Vector2.Zero;
            HomingProjectiles = 0;
            MaxHomingProjectiles = 5000;
            PreviousExperienceBonus = TimeSpan.Zero;
            ExperienceBonus = TimeSpan.FromMinutes(1f);
            ExperienceToLevel = 9999999;
            ExperienceSubtract = 0;
            LevelUp = false;
            LevelUpTime = TimeSpan.FromSeconds(1.15f);
            PreviousLevelUpTime = TimeSpan.Zero;
            RedSkull = 0;
            BlueSkull = 0;
            PinkSkull = 0;
            GreenSkull = 0;
            YellowSkull = 0;
            Money = false;
            Shield = false;
            Laser = false;
            MoneyActivator = false;
            TimeStopActivator = false;
            ShieldActivator = false;
            HealingActivator = false;
            EnergyCost = 0;
            HealingTime = TimeSpan.FromSeconds(0.1f);
            PreviousHealingTime = TimeSpan.Zero;
            EnergyDrain = TimeSpan.FromSeconds(0.1f);
            PreviousEnergyDrain = TimeSpan.Zero;
            HealingSound = TimeSpan.FromSeconds(0.3f);
            PreviousHealingSound = TimeSpan.Zero;
            DeadTime = TimeSpan.FromSeconds(0.15f);
            PreviousDeadTime = TimeSpan.Zero;
            LaserSound = TimeSpan.FromSeconds(1.7f);
            PreviousLaserSound = TimeSpan.Zero;
            LaserDamageTime = TimeSpan.FromSeconds(0.1f);
            PreviousLaserDamageTime = TimeSpan.Zero;
            LaserFireTime = TimeSpan.FromSeconds(1.75f);
            PreviousLaserFireTime = TimeSpan.Zero;
            LaserSound2 = TimeSpan.FromSeconds(0.75f);
            PreviousLaserSound2 = TimeSpan.Zero;
            RocketFireTime = TimeSpan.FromSeconds(0.5f);
            PreviousRocketFireTime = TimeSpan.Zero;
            ShotVolley = TimeSpan.FromSeconds(0.05f);
            PreviousShotVolley = TimeSpan.Zero;
            MoneySound = TimeSpan.FromSeconds(0.7f);
            PreviousMoneySound = TimeSpan.Zero;
            HealingCounter = 0;
            EnergyDrainCounter = 0;
            LaserDamage = 0f;
            LaserWidth = 0;
            RandomStatusChance = false;
            FireRockets = false;
            Laser = false;
            LoadLaser = false;
            LaserPositions = new Vector2[0];
            LaserRectangles = new Rectangle[0];
            LaserLoaded = false;
            LaserGlowPosition = Vector2.Zero;
            DrawGlow = false;
            LaserCounter = 0;
            LaserStarted = false;
            LaserFired = false;
            LaserPositions = new Vector2[screenHeight / 64];
            LaserRectangles = new Rectangle[screenHeight / 64];
            LaserRectanglesHitBox = new Rectangle[screenHeight / 64];
            BulletFiring = 1;
            VolleyOver = true;
            RandomStatusNumber = 0;
            FinalLaserDamage = 0;
            LaserDamage = 0;
            TimeStop = true;
            CurrentWeapon = 0;

            for (int i = 0; i < LaserPositions.Length; i++)
            {
                LaserRectangles[i] = new Rectangle(0, 0, 64, 0);
            }

            if (PlayerSelectedWeapon1 > 0)
                availableWeapons += 1;
            if (PlayerSelectedWeapon2 > 0)
                availableWeapons += 1;
            if (PlayerSelectedWeapon3 > 0)
                availableWeapons += 1;
            if (PlayerSelectedWeapon4 > 0)
                availableWeapons += 1;
            if (PlayerSelectedWeapon5 > 0)
                availableWeapons += 1;
        }

        private void InitializeAchievements()
        {
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
        }

        #region Initialize Player

        public void InitializePlayer(string playerName, float playerAcceleration, float playerBulletSpeed,
            float playerFireRate, int playerRedValue, int playerBlueValue, int playerGreenValue, int playerDamage, int playerShip, int playerMaxBullets, int playerCredits, int playerLives, int playerMaxHealth, int playerMaxEnergy,
            int iplayerAmmo, bool iplayerAutoFire, int iplayerBulletSpeed, int iplayerDamage, int iplayerElectricProjectile, int iplayerEnergy,
            int iplayerEnergyProjectile, int iplayerExplosiveProjectile, int iplayerFireProjectile, int iplayerFireRate, int iplayerHealingSpecial,
            int iplayerHealth, int iplayerHealthProjectile, int iplayerLaserProjectile, int iplayerLaserSpecial, int iplayerMoneySpecial,
            int iplayerMovementSpeed, int iplayerPoisonProjectile, int iplayerShieldSpecial, int iplayerSlowProjectile, int iplayerTimeStopSpecial,
            bool bplayerQuadShot, bool bplayerQuintupleShot, bool bplayerTripleShot, bool bplayerDoubleShot, bool bplayerExtraLife1,
            bool bplayerExtraLife2, bool bplayerExtraLife3, bool bplayerExtraLife4, int playerLevel, int playerDeathCount, int playerTimePlayedHours,
            int playerTimePlayedMinutes, int playerTimePlayedSeconds, int playerCreditsCollected, int playerCreditsSpent, int playerWeaponsCollected,
            int playerPercentageComplete, int playerBulletsFired, int playerAccuracy, int playerEnemiesKilled, int playerEnemiesHit,
            int playerMiniGamesPassed, int playerUpgradesPurchased, int playerPowerUpsCollected, int playerLevelsCompleted, bool playerAchievement1,
            bool playerAchievement2, bool playerAchievement3, bool playerAchievement4, bool playerAchievement5, bool playerAchievement6, bool playerAchievement7,
            bool playerAchievement8, bool playerAchievement9, bool playerAchievement10, bool playerAchievement11, bool playerAchievement12, bool playerAchievement13,
            bool playerAchievement14, bool playerAchievement15, bool playerAchievement16, bool playerAchievement17, bool playerAchievement18,
            bool playerAchievement19, bool playerAchievement20, bool playerAchievement21, bool playerAchievement22, bool playerAchievement23,
            bool playerAchievement24, bool playerAchievement25, bool playerAchievement26, bool playerAchievement27, bool playerAchievement28,
            bool playerAchievement29, bool playerAchievement30, bool playerAchievement31, bool playerAchievement32, bool playerAchievement33,
            bool playerAchievement34, bool playerAchievement35, bool playerAchievement36, bool playerAchievement37, bool playerAchievement38,
            bool playerAchievement39, bool playerAchievement40, bool playerAchievement41, bool playerAchievement42, bool playerAchievement43,
            bool playerAchievement44, bool playerAchievement45, bool playerAchievement46, bool playerAchievement47, bool playerAchievement48,
            bool playerAchievement49, bool playerAchievement50, int playerAchievementCount, int playerSelectedWeapon1, int playerSelectedWeapon2,
            int playerSelectedWeapon3, int playerSelectedWeapon4, int playerSelectedWeapon5, int playerSelectedSpecial, int playerShipsUnlocked, int playerXP, int playerLevelNumber)
        {
            PlayerName = playerName;

            PlayerAcceleration = playerAcceleration;
            PlayerBulletSpeed = playerBulletSpeed;

            PlayerFireRate = playerFireRate;

            PlayerRedValue = playerRedValue;
            PlayerBlueValue = playerBlueValue;
            PlayerGreenValue = playerGreenValue;

            PlayerColor = new Color(PlayerRedValue, PlayerGreenValue, PlayerBlueValue);

            PlayerDamage = playerDamage;
            PlayerShip = playerShip;
            PlayerMaxBullets = playerMaxBullets;
            PlayerFireStyle = 1;
            PlayerCredits = playerCredits;
            PlayerLives = playerLives;
            PlayerMaxHealth = playerMaxHealth;
            PlayerMaxEnergy = playerMaxEnergy;

            PlayerEnergy = playerMaxEnergy;
            PlayerHealth = playerMaxHealth;

            iPlayerAmmo = iplayerAmmo;
            bPlayerAutoFire = iplayerAutoFire;
            iPlayerBulletSpeed = iplayerBulletSpeed;
            iPlayerDamage = iplayerDamage;
            iPlayerElectricProjectile = iplayerElectricProjectile;
            iPlayerEnergy = iplayerEnergy;
            iPlayerEnergyProjectile = iplayerEnergyProjectile;
            iPlayerExplosiveProjectile = iplayerExplosiveProjectile;
            iPlayerFireProjectile = iplayerFireProjectile;
            iPlayerFireRate = iplayerFireRate;
            iPlayerHealingSpecial = iplayerHealingSpecial;
            iPlayerHealth = iplayerHealth;
            iPlayerHealthProjectile = iplayerHealthProjectile;
            iPlayerLaserProjectile = iplayerLaserProjectile;
            iPlayerLaserSpecial = iplayerLaserSpecial;
            iPlayerMoneySpecial = iplayerMoneySpecial;
            iPlayerMovementSpeed = iplayerMovementSpeed;
            iPlayerPoisonProjectile = iplayerPoisonProjectile;
            iPlayerShieldSpecial = iplayerShieldSpecial;
            iPlayerSlowProjectile = iplayerSlowProjectile;
            iPlayerTimeStopSpecial = iplayerTimeStopSpecial;

            PlayerAutoFire = bPlayerAutoFire;
            if (bPlayerAutoFire == true)
                autoFireOff = false;
            else
                autoFireOff = true;

            bPlayerTripleShot = bplayerTripleShot;
            bPlayerExtraLife1 = bplayerExtraLife1;
            bPlayerExtraLife2 = bplayerExtraLife2;
            bPlayerExtraLife3 = bplayerExtraLife3;
            bPlayerExtraLife4 = bplayerExtraLife4;
            bPlayerQuadShot = bplayerQuadShot;
            bPlayerQuintupleShot = bplayerQuintupleShot;
            bPlayerDoubleShot = bplayerDoubleShot;

            PlayerLevel = playerLevel;
            PlayerDeathCount = playerDeathCount;
            PlayerTimePlayedHours = playerTimePlayedHours;
            PlayerTimePlayedMinutes = playerTimePlayedMinutes;
            PlayerTimePlayedSeconds = playerTimePlayedSeconds;
            PlayerCreditsCollected = playerCreditsCollected;
            PlayerCreditsSpent = playerCreditsSpent;
            PlayerWeaponsCollected = playerWeaponsCollected;
            PlayerPercentageComplete = playerPercentageComplete;
            PlayerBulletsFired = playerBulletsFired;
            PlayerAccuracy = playerAccuracy;
            PlayerEnemiesKilled = playerEnemiesKilled;
            PlayerEnemiesHit = playerEnemiesHit;
            PlayerMiniGamesPassed = playerMiniGamesPassed;
            PlayerUpgradesPurchased = playerUpgradesPurchased;
            PlayerPowerUpsCollected = playerPowerUpsCollected;
            PlayerLevelsCompleted = playerLevelsCompleted;

            PlayerAchievement1 = playerAchievement1;
            PlayerAchievement2 = playerAchievement2;
            PlayerAchievement3 = playerAchievement3;
            PlayerAchievement4 = playerAchievement4;
            PlayerAchievement5 = playerAchievement5;
            PlayerAchievement6 = playerAchievement6;
            PlayerAchievement7 = playerAchievement7;
            PlayerAchievement8 = playerAchievement8;
            PlayerAchievement9 = playerAchievement9;
            PlayerAchievement10 = playerAchievement10;
            PlayerAchievement11 = playerAchievement11;
            PlayerAchievement12 = playerAchievement12;
            PlayerAchievement13 = playerAchievement13;
            PlayerAchievement14 = playerAchievement14;
            PlayerAchievement15 = playerAchievement15;
            PlayerAchievement16 = playerAchievement16;
            PlayerAchievement17 = playerAchievement17;
            PlayerAchievement18 = playerAchievement18;
            PlayerAchievement19 = playerAchievement19;
            PlayerAchievement20 = playerAchievement20;
            PlayerAchievement21 = playerAchievement21;
            PlayerAchievement22 = playerAchievement22;
            PlayerAchievement23 = playerAchievement23;
            PlayerAchievement24 = playerAchievement24;
            PlayerAchievement25 = playerAchievement25;
            PlayerAchievement26 = playerAchievement26;
            PlayerAchievement27 = playerAchievement27;
            PlayerAchievement28 = playerAchievement28;
            PlayerAchievement29 = playerAchievement29;
            PlayerAchievement30 = playerAchievement30;
            PlayerAchievement31 = playerAchievement31;
            PlayerAchievement32 = playerAchievement32;
            PlayerAchievement33 = playerAchievement33;
            PlayerAchievement34 = playerAchievement34;
            PlayerAchievement35 = playerAchievement35;
            PlayerAchievement36 = playerAchievement36;
            PlayerAchievement37 = playerAchievement37;
            PlayerAchievement38 = playerAchievement38;
            PlayerAchievement39 = playerAchievement39;
            PlayerAchievement40 = playerAchievement40;
            PlayerAchievement41 = playerAchievement41;
            PlayerAchievement42 = playerAchievement42;
            PlayerAchievement43 = playerAchievement43;
            PlayerAchievement44 = playerAchievement44;
            PlayerAchievement45 = playerAchievement45;
            PlayerAchievement46 = playerAchievement46;
            PlayerAchievement47 = playerAchievement47;
            PlayerAchievement48 = playerAchievement48;
            PlayerAchievement49 = playerAchievement49;
            PlayerAchievement50 = playerAchievement50;
            PlayerAchievementCount = playerAchievementCount;
            PlayerSelectedWeapon1 = playerSelectedWeapon1;
            PlayerSelectedWeapon2 = playerSelectedWeapon2;
            PlayerSelectedWeapon3 = playerSelectedWeapon3;
            PlayerSelectedWeapon4 = playerSelectedWeapon4;
            PlayerSelectedWeapon5 = playerSelectedWeapon5;
            PlayerSelectedSpecial = playerSelectedSpecial;
            PlayerShipsUnlocked = playerShipsUnlocked;
            PlayerXP = playerXP;
            PlayerLevelNumber = playerLevelNumber;

            PreviousCredits = PlayerCredits;
            PreviousEnemiesHit = PlayerEnemiesHit;
            PreviousProjectilesFired = PlayerBulletsFired;
            GameCredits = 0;
            previousLevel = PlayerLevel;
        }

        #endregion

        public void InitializePowerUps()
        {
            powerUpActivated = false;
            powerUp = 0;
            BonusAcceleration = 0;
            BonusAutoFire = false;
            BonusDamage = 0;
            BonusFireRate = 0;
            BonusMaxBullets = 0;
            BonusBulletSpeed = 0;
        }

        public void LoadContent()
        {
            LoadShips();
            LoadHulls();
            LoadAchievementIcons();
            pauseMenu.LoadContent();
            LoadParticles();
            LoadLaserTexture();
        }

        private void LoadParticles()
        {
            deathParticles = new ParticleEngine(3, PlayerColor, Vector2.Zero);
            deathParticles.LoadContent();
            deathParticles.AddParticles = false;

            hitParticles = new ParticleEngine(1, Color.WhiteSmoke, Vector2.Zero);
            hitParticles.LoadContent();

            StunParticle = new ParticleEngine(4, Color.Yellow, Vector2.Zero);
            StunParticle.LoadContent();
            StunParticle.AddParticles = false;

            BurnParticle = new ParticleEngine(5, Color.Orange, Vector2.Zero);
            BurnParticle.LoadContent();
            BurnParticle.AddParticles = false;

            PoisonParticle = new ParticleEngine(3, Color.Green, Vector2.Zero);
            PoisonParticle.LoadContent();
            PoisonParticle.AddParticles = false;

            SlowParticle = new ParticleEngine(3, Color.LightBlue, Vector2.Zero);
            SlowParticle.LoadContent();
            SlowParticle.AddParticles = false;

            LevelUpParticles = new ParticleEngine(3, Color.Gold, Vector2.Zero);
            LevelUpParticles.LoadContent();
            LevelUpParticles.AddParticles = false;

            HealingParticles = new ParticleEngine(9, Color.White, Vector2.Zero);
            HealingParticles.LoadContent();
            HealingParticles.AddParticles = false;
            HealingParticles.total = 1;

            LaserParticles = new ParticleEngine(5, Color.OrangeRed, Vector2.Zero);
            LaserParticles.LoadContent();
            LaserParticles.AddParticles = false;

            MoneyParticles = new ParticleEngine(10, Color.White, Vector2.Zero);
            MoneyParticles.LoadContent();
            MoneyParticles.AddParticles = false;
        }

        public void LoadShips()
        {
            if (PlayerShip == 1)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship1");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(2, new Color(93, 253, 253), Vector2.Zero);
                EmitterMinX = 15;
                EmitterMaxX = 50;
                EmitterMinY = 52;
                EmitterMaxY = 54;
                particleEngine.LoadContent();
            }

            if (PlayerShip == 2)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship2");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(2, new Color(218, 2, 86), Vector2.Zero);
                EmitterMinX = 24;
                EmitterMaxX = 40;
                EmitterMinY = 56;
                EmitterMaxY = 58;
                particleEngine.LoadContent();
            }

            if (PlayerShip == 3)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship3");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine.LoadContent();
                particleEngine = new ParticleEngine(2, new Color(218, 2, 86), Vector2.Zero);
                EmitterMinX = 24;
                EmitterMaxX = 40;
                EmitterMinY = 46;
                EmitterMaxY = 48;
                particleEngine.LoadContent();
            }

            if (PlayerShip == 4)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship4");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
                particleEngine = new ParticleEngine(2, Color.DarkBlue, Vector2.Zero);
                EmitterMinX = 24;
                EmitterMaxX = 40;
                EmitterMinY = 52;
                EmitterMaxY = 54;
                particleEngine.LoadContent();
            }

            if (PlayerShip == 5)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship5");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 6)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship6");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 7)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship7");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 8)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship8");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 9)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship9");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 10)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship10");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 11)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship11");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 12)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship12");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 13)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship13");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 14)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship14");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 15)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship15");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 16)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship16");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 17)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship17");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 18)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship18");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 19)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship19");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            if (PlayerShip == 20)
            {
                shipTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship20");
                player = new MobileSprite(shipTexture);
                player.Sprite.AddAnimation("left", 0, 64, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("right", 0, 128, 64, 64, 1, 10f);
                player.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                player.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);
                particleEngine.LoadContent();
            }

            particleEngine.MoveUp = true;
            player.Sprite.CurrentAnimation = "default";
            player.Sprite.Tint = PlayerColor;
            player.Position = new Vector2(screenCenterX - player.Sprite.Width, 0 - player.Sprite.Height);
            player.IsMoving = false;
            player.locked = true;
            player.IsActive = true;
            player.IsCollidable = true;
        }

        public void LoadHulls()
        {
            if (PlayerShip == 1)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull1");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, 0.05f);
            }

            if (PlayerShip == 2)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull2");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 3)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull3");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 3, 0.05f);
            }

            if (PlayerShip == 4)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull4");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 12, 0.05f);
            }

            if (PlayerShip == 5)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull5");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 6)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull6");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 7)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull7");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 8)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull8");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 9)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull9");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 10)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull10");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 11)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull11");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 12)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull12");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 13)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull13");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 14)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull14");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 15)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull15");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 16)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull16");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 17)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull17");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 18)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull18");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 19)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull19");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            if (PlayerShip == 20)
            {
                hullTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull20");
                playerHull = new MobileSprite(hullTexture);
                playerHull.Sprite.AddAnimation("gone", 0, 0, 0, 0, 1, 0.05f);
                playerHull.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            }

            playerHull.Sprite.CurrentAnimation = "default";
            playerHull.Sprite.Tint = Color.White;
            playerHull.Position = player.Position;
            playerHull.IsMoving = false;
            playerHull.locked = false;
            playerHull.IsActive = true;
        }

        private void LoadAchievementIcons()
        {
            Achievement1Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement01");
            Achievement1 = new MobileSprite(Achievement1Texture);
            Achievement1.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement1.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement1.Sprite.CurrentAnimation = "default";
            Achievement1.Position = Vector2.Zero;
            Achievement1.IsMoving = false;

            Achievement2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement02");
            Achievement2 = new MobileSprite(Achievement2Texture);
            Achievement2.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement2.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement2.Sprite.CurrentAnimation = "default";
            Achievement2.Position = Vector2.Zero;
            Achievement2.IsMoving = false;

            Achievement3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement03");
            Achievement3 = new MobileSprite(Achievement3Texture);
            Achievement3.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement3.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement3.Sprite.CurrentAnimation = "default";
            Achievement3.Position = Vector2.Zero;
            Achievement3.IsMoving = false;

            Achievement4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement04");
            Achievement4 = new MobileSprite(Achievement4Texture);
            Achievement4.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement4.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement4.Sprite.CurrentAnimation = "default";
            Achievement4.Position = Vector2.Zero;
            Achievement4.IsMoving = false;

            Achievement5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement05");
            Achievement5 = new MobileSprite(Achievement5Texture);
            Achievement5.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement5.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement5.Sprite.CurrentAnimation = "default";
            Achievement5.Position = Vector2.Zero;
            Achievement5.IsMoving = false;

            Achievement6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement06");
            Achievement6 = new MobileSprite(Achievement6Texture);
            Achievement6.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement6.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement6.Sprite.CurrentAnimation = "default";
            Achievement6.Position = Vector2.Zero;
            Achievement6.IsMoving = false;

            Achievement7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement07");
            Achievement7 = new MobileSprite(Achievement7Texture);
            Achievement7.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement7.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement7.Sprite.CurrentAnimation = "default";
            Achievement7.Position = Vector2.Zero;
            Achievement7.IsMoving = false;

            Achievement8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement08");
            Achievement8 = new MobileSprite(Achievement8Texture);
            Achievement8.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement8.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement8.Sprite.CurrentAnimation = "default";
            Achievement8.Position = Vector2.Zero;
            Achievement8.IsMoving = false;

            Achievement9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement09");
            Achievement9 = new MobileSprite(Achievement1Texture);
            Achievement9.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement9.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement9.Sprite.CurrentAnimation = "default";
            Achievement9.Position = Vector2.Zero;
            Achievement9.IsMoving = false;

            Achievement10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement10");
            Achievement10 = new MobileSprite(Achievement10Texture);
            Achievement10.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement10.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement10.Sprite.CurrentAnimation = "default";
            Achievement10.Position = Vector2.Zero;
            Achievement10.IsMoving = false;

            Achievement11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement11");
            Achievement11 = new MobileSprite(Achievement11Texture);
            Achievement11.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement11.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement11.Sprite.CurrentAnimation = "default";
            Achievement11.Position = Vector2.Zero;
            Achievement11.IsMoving = false;

            Achievement12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement12");
            Achievement12 = new MobileSprite(Achievement12Texture);
            Achievement12.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement12.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement12.Sprite.CurrentAnimation = "default";
            Achievement12.Position = Vector2.Zero;
            Achievement12.IsMoving = false;

            Achievement13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement13");
            Achievement13 = new MobileSprite(Achievement13Texture);
            Achievement13.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement13.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement13.Sprite.CurrentAnimation = "default";
            Achievement13.Position = Vector2.Zero;
            Achievement13.IsMoving = false;

            Achievement14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement14");
            Achievement14 = new MobileSprite(Achievement14Texture);
            Achievement14.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement14.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement14.Sprite.CurrentAnimation = "default";
            Achievement14.Position = Vector2.Zero;
            Achievement14.IsMoving = false;

            Achievement15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement15");
            Achievement15 = new MobileSprite(Achievement15Texture);
            Achievement15.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement15.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement15.Sprite.CurrentAnimation = "default";
            Achievement15.Position = Vector2.Zero;
            Achievement15.IsMoving = false;

            Achievement16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement16");
            Achievement16 = new MobileSprite(Achievement16Texture);
            Achievement16.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement16.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement16.Sprite.CurrentAnimation = "default";
            Achievement16.Position = Vector2.Zero;
            Achievement16.IsMoving = false;

            Achievement17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement17");
            Achievement17 = new MobileSprite(Achievement17Texture);
            Achievement17.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement17.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement17.Sprite.CurrentAnimation = "default";
            Achievement17.Position = Vector2.Zero;
            Achievement17.IsMoving = false;

            Achievement18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement18");
            Achievement18 = new MobileSprite(Achievement18Texture);
            Achievement18.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement18.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement18.Sprite.CurrentAnimation = "default";
            Achievement18.Position = Vector2.Zero;
            Achievement18.IsMoving = false;

            Achievement19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement19");
            Achievement19 = new MobileSprite(Achievement11Texture);
            Achievement19.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement19.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement19.Sprite.CurrentAnimation = "default";
            Achievement19.Position = Vector2.Zero;
            Achievement19.IsMoving = false;

            Achievement20Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement20");
            Achievement20 = new MobileSprite(Achievement20Texture);
            Achievement20.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement20.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement20.Sprite.CurrentAnimation = "default";
            Achievement20.Position = Vector2.Zero;
            Achievement20.IsMoving = false;

            Achievement21Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement21");
            Achievement21 = new MobileSprite(Achievement21Texture);
            Achievement21.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement21.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement21.Sprite.CurrentAnimation = "default";
            Achievement21.Position = Vector2.Zero;
            Achievement21.IsMoving = false;

            Achievement22Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement22");
            Achievement22 = new MobileSprite(Achievement22Texture);
            Achievement22.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement22.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement22.Sprite.CurrentAnimation = "default";
            Achievement22.Position = Vector2.Zero;
            Achievement22.IsMoving = false;

            Achievement23Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement23");
            Achievement23 = new MobileSprite(Achievement23Texture);
            Achievement23.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement23.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement23.Sprite.CurrentAnimation = "default";
            Achievement23.Position = Vector2.Zero;
            Achievement23.IsMoving = false;

            Achievement24Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement24");
            Achievement24 = new MobileSprite(Achievement24Texture);
            Achievement24.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement24.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement24.Sprite.CurrentAnimation = "default";
            Achievement24.Position = Vector2.Zero;
            Achievement24.IsMoving = false;

            Achievement25Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement25");
            Achievement25 = new MobileSprite(Achievement25Texture);
            Achievement25.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement25.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement25.Sprite.CurrentAnimation = "default";
            Achievement25.Position = Vector2.Zero;
            Achievement25.IsMoving = false;

            Achievement26Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement26");
            Achievement26 = new MobileSprite(Achievement26Texture);
            Achievement26.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement26.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement26.Sprite.CurrentAnimation = "default";
            Achievement26.Position = Vector2.Zero;
            Achievement26.IsMoving = false;

            Achievement27Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement27");
            Achievement27 = new MobileSprite(Achievement27Texture);
            Achievement27.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement27.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement27.Sprite.CurrentAnimation = "default";
            Achievement27.Position = Vector2.Zero;
            Achievement27.IsMoving = false;

            Achievement28Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement28");
            Achievement28 = new MobileSprite(Achievement28Texture);
            Achievement28.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement28.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement28.Sprite.CurrentAnimation = "default";
            Achievement28.Position = Vector2.Zero;
            Achievement28.IsMoving = false;

            Achievement29Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement29");
            Achievement29 = new MobileSprite(Achievement21Texture);
            Achievement29.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement29.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement29.Sprite.CurrentAnimation = "default";
            Achievement29.Position = Vector2.Zero;
            Achievement29.IsMoving = false;

            Achievement30Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement30");
            Achievement30 = new MobileSprite(Achievement30Texture);
            Achievement30.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement30.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement30.Sprite.CurrentAnimation = "default";
            Achievement30.Position = Vector2.Zero;
            Achievement30.IsMoving = false;

            Achievement31Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement31");
            Achievement31 = new MobileSprite(Achievement31Texture);
            Achievement31.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement31.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement31.Sprite.CurrentAnimation = "default";
            Achievement31.Position = Vector2.Zero;
            Achievement31.IsMoving = false;

            Achievement32Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement32");
            Achievement32 = new MobileSprite(Achievement32Texture);
            Achievement32.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement32.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement32.Sprite.CurrentAnimation = "default";
            Achievement32.Position = Vector2.Zero;
            Achievement32.IsMoving = false;

            Achievement33Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement33");
            Achievement33 = new MobileSprite(Achievement33Texture);
            Achievement33.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement33.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement33.Sprite.CurrentAnimation = "default";
            Achievement33.Position = Vector2.Zero;
            Achievement33.IsMoving = false;

            Achievement34Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement34");
            Achievement34 = new MobileSprite(Achievement34Texture);
            Achievement34.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement34.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement34.Sprite.CurrentAnimation = "default";
            Achievement34.Position = Vector2.Zero;
            Achievement34.IsMoving = false;

            Achievement35Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement35");
            Achievement35 = new MobileSprite(Achievement35Texture);
            Achievement35.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement35.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement35.Sprite.CurrentAnimation = "default";
            Achievement35.Position = Vector2.Zero;
            Achievement35.IsMoving = false;

            Achievement36Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement36");
            Achievement36 = new MobileSprite(Achievement36Texture);
            Achievement36.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement36.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement36.Sprite.CurrentAnimation = "default";
            Achievement36.Position = Vector2.Zero;
            Achievement36.IsMoving = false;

            Achievement37Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement37");
            Achievement37 = new MobileSprite(Achievement37Texture);
            Achievement37.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement37.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement37.Sprite.CurrentAnimation = "default";
            Achievement37.Position = Vector2.Zero;
            Achievement37.IsMoving = false;

            Achievement38Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement38");
            Achievement38 = new MobileSprite(Achievement38Texture);
            Achievement38.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement38.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement38.Sprite.CurrentAnimation = "default";
            Achievement38.Position = Vector2.Zero;
            Achievement38.IsMoving = false;

            Achievement39Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement39");
            Achievement39 = new MobileSprite(Achievement31Texture);
            Achievement39.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement39.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement39.Sprite.CurrentAnimation = "default";
            Achievement39.Position = Vector2.Zero;
            Achievement39.IsMoving = false;

            Achievement40Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement40");
            Achievement40 = new MobileSprite(Achievement40Texture);
            Achievement40.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement40.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement40.Sprite.CurrentAnimation = "default";
            Achievement40.Position = Vector2.Zero;
            Achievement40.IsMoving = false;

            Achievement41Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement41");
            Achievement41 = new MobileSprite(Achievement41Texture);
            Achievement41.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement41.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement41.Sprite.CurrentAnimation = "default";
            Achievement41.Position = Vector2.Zero;
            Achievement41.IsMoving = false;

            Achievement42Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement42");
            Achievement42 = new MobileSprite(Achievement42Texture);
            Achievement42.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement42.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement42.Sprite.CurrentAnimation = "default";
            Achievement42.Position = Vector2.Zero;
            Achievement42.IsMoving = false;

            Achievement43Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement43");
            Achievement43 = new MobileSprite(Achievement43Texture);
            Achievement43.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement43.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement43.Sprite.CurrentAnimation = "default";
            Achievement43.Position = Vector2.Zero;
            Achievement43.IsMoving = false;

            Achievement44Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement44");
            Achievement44 = new MobileSprite(Achievement44Texture);
            Achievement44.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement44.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement44.Sprite.CurrentAnimation = "default";
            Achievement44.Position = Vector2.Zero;
            Achievement44.IsMoving = false;

            Achievement45Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement45");
            Achievement45 = new MobileSprite(Achievement45Texture);
            Achievement45.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement45.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement45.Sprite.CurrentAnimation = "default";
            Achievement45.Position = Vector2.Zero;
            Achievement45.IsMoving = false;

            Achievement46Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement46");
            Achievement46 = new MobileSprite(Achievement46Texture);
            Achievement46.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement46.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement46.Sprite.CurrentAnimation = "default";
            Achievement46.Position = Vector2.Zero;
            Achievement46.IsMoving = false;

            Achievement47Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement47");
            Achievement47 = new MobileSprite(Achievement47Texture);
            Achievement47.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement47.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement47.Sprite.CurrentAnimation = "default";
            Achievement47.Position = Vector2.Zero;
            Achievement47.IsMoving = false;

            Achievement48Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement48");
            Achievement48 = new MobileSprite(Achievement48Texture);
            Achievement48.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement48.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement48.Sprite.CurrentAnimation = "default";
            Achievement48.Position = Vector2.Zero;
            Achievement48.IsMoving = false;

            Achievement49Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement49");
            Achievement49 = new MobileSprite(Achievement41Texture);
            Achievement49.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement49.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement49.Sprite.CurrentAnimation = "default";
            Achievement49.Position = Vector2.Zero;
            Achievement49.IsMoving = false;

            Achievement50Texture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//AchievementIcons//Achievement50");
            Achievement50 = new MobileSprite(Achievement50Texture);
            Achievement50.Sprite.AddAnimation("complete", 0, 64, 64, 64, 1, 10f);
            Achievement50.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Achievement50.Sprite.CurrentAnimation = "default";
            Achievement50.Position = Vector2.Zero;
            Achievement50.IsMoving = false;
        }

        private void LoadLaserTexture()
        {
            LaserGlow = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserGlow");
            LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserTemplate");

            if (LaserWidth == 1)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth1");
            else if (LaserWidth == 2)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth2");
            else if (LaserWidth == 3)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth3");
            else if (LaserWidth == 4)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth4");
            else if (LaserWidth == 5)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth5");
            else if (LaserWidth == 6)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth6");
            else if (LaserWidth == 7)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth7");
            else if (LaserWidth == 8)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth8");
            else if (LaserWidth == 9)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth9");
            else if (LaserWidth == 10)
                LaserTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LaserWidth10");
        }

        public void Update(GameTime gameTime, Vector2 iconPosition)
        {
            if (Active && paused == false)
            {
                if (Alive)
                {
                    UpdateProjectiles(gameTime);
                    UpdatePowerUps();
                    UpdateHit(gameTime);

                    if (player.locked)
                    {
                        player.Sprite.AutoRotate = false;
                        player.Sprite.Rotation = playerHull.Sprite.Rotation;
                    }
                    else
                    {
                        player.Sprite.AutoRotate = true;
                        playerHull.Sprite.Rotation = player.Sprite.Rotation;
                    }
                }

                player.locked = false;

                UpdateParticles(gameTime);
                FinalValues();
                PlayerPools();
                keyboardUpdate.Update(gameTime);
                UpdateControls();
                //Boundries();
                UpdateExperience(gameTime);

                if (previousLevel != PlayerLevel && gameTime.TotalGameTime - PreviousLevelUpTime > LevelUpTime)
                {
                    LevelUp = true;
                    Game1.Instance.AudioPlay("LevelUp", 4);
                    PreviousLevelUpTime = gameTime.TotalGameTime;
                }

                if (LevelUp)
                    LevelUpUpdate(gameTime);

                if (Stunned)
                    BeenStunned(gameTime);
                if (Burned)
                    BeenBurned(gameTime);
                if (Poisoned)
                    BeenPoisoned(gameTime);
                if (Slowed)
                    BeenSlowed(gameTime);

                UpdatePlayers(gameTime);
                UpdateSpecials(gameTime);
                DeathDrain(gameTime);

                previousLevel = PlayerLevel;
            }

            if (paused)
            {
                pauseMenu.Update(gameTime, iconPosition);

                if (pauseMenu.Resume)
                    paused = false;

                if (pauseMenu.DropOut)
                    Active = false;
            }

            AchievementTracker(gameTime);

            #region Timer

            if (StartTimer)
            {
                previousTimer = gameTime.TotalGameTime;
                StartTimer = false;
            }

            if (gameTime.TotalGameTime - previousTimer > Timer)
            {
                previousTimer = gameTime.TotalGameTime;
                PlayerTimePlayedSeconds += 1;

                if (PlayerTimePlayedSeconds > 59)
                {
                    PlayerTimePlayedSeconds = 0;
                    PlayerTimePlayedMinutes += 1;
                    Game1.Instance.Saving = true;
                    if (PlayerTimePlayedMinutes > 59)
                    {
                        PlayerTimePlayedMinutes = 0;
                        PlayerTimePlayedHours += 1;
                    }
                }
            }

            #endregion
        }

        private void UpdateSpecials(GameTime gameTime)
        {
            if (keyboardUpdate.Special && PlayerEnergy > 0)
            {
                #region Healing

                if (PlayerSelectedSpecial == 1)
                {
                    if (iPlayerHealingSpecial == 1)
                        HealthIncrease = 1;
                    else if (iPlayerHealingSpecial == 2)
                        HealthIncrease = 1;
                    else if (iPlayerHealingSpecial == 3)
                        HealthIncrease = 1;
                    else if (iPlayerHealingSpecial == 4)
                        HealthIncrease = 2;
                    else if (iPlayerHealingSpecial == 5)
                        HealthIncrease = 4;
                    else if (iPlayerHealingSpecial == 6)
                        HealthIncrease = 6;
                    else if (iPlayerHealingSpecial == 7)
                        HealthIncrease = 8;
                    else if (iPlayerHealingSpecial == 8)
                        HealthIncrease = 10;
                    else if (iPlayerHealingSpecial == 9)
                        HealthIncrease = 12;
                    else if (iPlayerHealingSpecial == 10)
                        HealthIncrease = 15;

                    if (PlayerHealth + HealthIncrease >= PlayerMaxHealth && FinalLives == 4)
                    {
                        EnergyCost = 0;
                        CureStatus = false;
                        HealthIncrease = 0;
                        GetExtraLife = false;
                        HealingActivator = false;
                    }
                    else
                    {
                        EnergyCost = iPlayerHealingSpecial;
                        HealingActivator = true;

                        if (iPlayerHealingSpecial == 1)
                            HealthIncrease = 1;
                        else if (iPlayerHealingSpecial == 2)
                            HealthIncrease = 5;
                        else if (iPlayerHealingSpecial == 3)
                            HealthIncrease = 10;
                        else if (iPlayerHealingSpecial == 4)
                            HealthIncrease = 20;
                        else if (iPlayerHealingSpecial == 5)
                            HealthIncrease = 40;
                        else if (iPlayerHealingSpecial == 6)
                            HealthIncrease = 60;
                        else if (iPlayerHealingSpecial == 7)
                            HealthIncrease = 80;
                        else if (iPlayerHealingSpecial == 8)
                            HealthIncrease = 100;
                        else if (iPlayerHealingSpecial == 9)
                            HealthIncrease = 120;
                        else if (iPlayerHealingSpecial == 10)
                            HealthIncrease = 150;

                        if (iPlayerHealingSpecial >= 5)
                            CureStatus = true;
                        else
                            CureStatus = false;

                        if (iPlayerHealingSpecial == 10)
                            GetExtraLife = true;
                        else
                            GetExtraLife = false;
                    }
                }

                #endregion

                #region Laser

                else if (PlayerSelectedSpecial == 2)
                {
                    EnergyCost = iPlayerLaserSpecial;
                    Laser = true;
                    LaserParticles.AddParticles = true;

                    iPlayerLaserSpecial = 10;

                    if (iPlayerLaserSpecial == 1)
                    {
                        LaserDamage = (float)FinalDamage * 1f;
                        LaserWidth = 1;
                    }
                    else if (iPlayerLaserSpecial == 2)
                    {
                        LaserDamage = (float)FinalDamage * 1.2f;
                        LaserWidth = 2;
                    }
                    else if (iPlayerLaserSpecial == 3)
                    {
                        LaserDamage = (float)FinalDamage * 1.4f;
                        LaserWidth = 3;
                    }
                    else if (iPlayerLaserSpecial == 4)
                    {
                        LaserDamage = (float)FinalDamage * 1.6f;
                        LaserWidth = 4;
                    }
                    else if (iPlayerLaserSpecial == 5)
                    {
                        LaserDamage = (float)FinalDamage * 1.8f;
                        LaserWidth = 5;
                    }
                    else if (iPlayerLaserSpecial == 6)
                    {
                        LaserDamage = (float)FinalDamage * 2f;
                        LaserWidth = 6;
                    }
                    else if (iPlayerLaserSpecial == 7)
                    {
                        LaserDamage = (float)FinalDamage * 2.2f;
                        LaserWidth = 7;
                    }
                    else if (iPlayerLaserSpecial == 8)
                    {
                        LaserDamage = (float)FinalDamage * 2.5f;
                        LaserWidth = 8;
                    }
                    else if (iPlayerLaserSpecial == 9)
                    {
                        LaserDamage = (float)FinalDamage * 2.7f;
                        LaserWidth = 9;
                    }
                    else if (iPlayerLaserSpecial == 10)
                    {
                        LaserDamage = (float)FinalDamage * 3f;
                        LaserWidth = 10;
                        FireRockets = true;
                    }

                    if (LaserLoaded == false)
                    {
                        LoadLaser = true;
                        PreviousLaserFireTime = gameTime.TotalGameTime;
                    }

                    if (iPlayerLaserSpecial >= 5)
                        RandomStatusChance = true;
                }

                #endregion

                #region Money

                else if (PlayerSelectedSpecial == 3)
                {
                    EnergyCost = iPlayerMoneySpecial;

                    if (iPlayerMoneySpecial == 1)
                    {
                        MoneyValueMultiplier = 0.1f;
                    }
                    else if (iPlayerMoneySpecial == 2)
                    {
                        MoneyValueMultiplier = 0.2f;
                    }
                    else if (iPlayerMoneySpecial == 3)
                    {
                        MoneyValueMultiplier = 0.3f;
                    }
                    else if (iPlayerMoneySpecial == 4)
                    {
                        MoneyValueMultiplier = 0.4f;
                    }
                    else if (iPlayerMoneySpecial == 5)
                    {
                        MoneyValueMultiplier = 0.5f;
                    }
                    else if (iPlayerMoneySpecial == 6)
                    {
                        MoneyValueMultiplier = 0.6f;
                    }
                    else if (iPlayerMoneySpecial == 7)
                    {
                        MoneyValueMultiplier = 0.7f;
                    }
                    else if (iPlayerMoneySpecial == 8)
                    {
                        MoneyValueMultiplier = 0.8f;
                    }
                    else if (iPlayerMoneySpecial == 9)
                    {
                        MoneyValueMultiplier = 0.9f;
                    }
                    else if (iPlayerMoneySpecial == 10)
                    {
                        MoneyValueMultiplier = 1f;
                    }

                    if (iPlayerMoneySpecial >= 5)
                        LargeCrystals = true;
                    else
                        LargeCrystals = false;

                    if (iPlayerMoneySpecial == 10)
                        FirePowerUps = true;
                    else
                        FirePowerUps = false;

                    MoneyActivator = true;
                    Money = true;
                }

                #endregion

                //SHIELD
                else if (PlayerSelectedSpecial == 4)
                {
                    EnergyCost = iPlayerShieldSpecial;
                }

                //TIMESTOP
                else if (PlayerSelectedSpecial == 5)
                {
                    EnergyCost = iPlayerTimeStopSpecial;

                    TimeStop = true;
                }

            }
            else
            {
                EnergyCost = 0;
                CureStatus = false;
                HealthIncrease = 0;
                GetExtraLife = false;
                HealingActivator = false;

                LaserDamage = 0;
                LaserWidth = 0;
                RandomStatusChance = false;
                FireRockets = false;
                Laser = false;
                LaserLoaded = false;
                DrawGlow = false;
                LaserFired = false;

                for (int i = 0; i < LaserPositions.Length; i++)
                {
                    LaserPositions[i] = new Vector2(player.Position.X + (player.Sprite.Width / 2) - (LaserTexture.Width / 2), player.Position.Y - (player.Sprite.Height / 2) - (i * LaserTexture.Height));
                    LaserRectanglesHitBox[i] = new Rectangle((int)LaserPositions[i].X, (int)LaserPositions[i].Y, 0, 0); 
                }

                MoneyActivator = false;
                FirePowerUps = false;
                LargeCrystals = false;
                Money = false;
                MoneyValueMultiplier = 0f;

                LaserParticles.AddParticles = false;

                TimeStop = false;
            }

            #region Healing

            if (Healing && Alive)
            {
                if (gameTime.TotalGameTime - PreviousHealingTime > HealingTime)
                {
                    PreviousHealingTime = gameTime.TotalGameTime;

                    if ((float)RecievedHealthIncrease / 10f < 1f)
                    {
                        HealingCounter += RecievedHealthIncrease;

                        if (HealingCounter > 9)
                        {
                            HealingCounter -= 10;
                            PlayerHealth = 1;
                        }
                    }
                    else if ((float)PlayerHealth + ((float)RecievedHealthIncrease / 10f) > PlayerMaxHealth && FinalLives != 4)
                    {
                        if (RecievedGetExtraLife)
                        {
                            BonusLives += 1;
                            PlayerHealth = (int)((float)RecievedHealthIncrease / 10f);
                        }
                    }
                    else
                    {
                        PlayerHealth += (int)((float)RecievedHealthIncrease / 10f);
                    }
                }

                if (RecievedCureStatus)
                {
                    if (Burned)
                        BurnTime = 0f;
                    if (Poisoned)
                        PoisonTime = 0f;
                    if (Stunned)
                        StunTime = 0f;
                    if (Slowed)
                        SlowTime = 0f;
                }
                HealingParticles.AddParticles = true;
            }
            else
            {
                HealingParticles.AddParticles = false;
            }

            #endregion

            #region Laser

            if (Laser && Alive)
            {
                if (LoadLaser)
                {
                    LoadLaserTexture();

                    LaserCounter = 0;
                    LoadLaser = false;
                    LaserLoaded = true;
                    if (RandomStatusChance)
                        RandomStatusNumber = random.Next(50,101);
                    else
                        RandomStatusNumber = 0;

                    if (gameTime.TotalGameTime - PreviousLaserSound > LaserSound)
                    {
                        Game1.Instance.AudioPlay("PowerUpLaser", 4);
                        PreviousLaserSound = gameTime.TotalGameTime;
                    }
                }

                if (gameTime.TotalGameTime - PreviousLaserFireTime > LaserFireTime)
                {
                    for (int i = 0; i < LaserPositions.Length; i++)
                    {
                        LaserPositions[i] = new Vector2(player.Position.X + (player.Sprite.Width / 2) - (LaserTexture.Width / 2), player.Position.Y - (player.Sprite.Height / 2) - (i * LaserTexture.Height));
                        LaserRectangles[i].Width = LaserTexture.Width;
                        LaserRectanglesHitBox[i] = new Rectangle((int)LaserPositions[i].X, (int)LaserPositions[i].Y, LaserRectangles[i].Width, LaserRectangles[i].Height);
                    }

                    if (gameTime.TotalGameTime - PreviousLaserDamageTime > LaserDamageTime)
                    {
                        PreviousLaserDamageTime = gameTime.TotalGameTime;

                        if (LaserDamage / 10f < 1f)
                        {
                            LaserDamageCounter += (int)LaserDamage;

                            if (LaserDamageCounter > 9)
                            {
                                LaserDamageCounter -= 10;
                                FinalLaserDamage = 1;
                            }
                        }
                        else
                            FinalLaserDamage = (int)((float)LaserDamage / 10f);
                    }


                    if (LaserFired == false)
                    {
                        LaserFired = true;
                        PreviousLaserSound2 = gameTime.TotalGameTime;
                        Game1.Instance.AudioPlay("LaserFired", 4);
                    }

                    if (FireRockets)
                    {
                        if (gameTime.TotalGameTime - PreviousRocketFireTime > RocketFireTime)
                        {
                            Projectile projectile = new Projectile();
                            if (RocketFired == 0)
                            {
                                RocketFired = 1;
                                projectile.Initialize(18, Position + new Vector2(Width / 2 - (projectileWidth / 2), Height / 3), (int)((float)FinalDamage * LaserDamage), FinalBulletSpeedY, FinalBulletSpeedX, 3, 1, player.Sprite.velocity);
                                projectile.LoadContent();
                                projectiles.Add(projectile);
                            }
                            else if (RocketFired == 1)
                            {
                                RocketFired = 0;
                                projectile.Initialize(18, Position + new Vector2(Width / 2 - (projectileWidth / 2), Height / 3), (int)((float)FinalDamage * LaserDamage), FinalBulletSpeedY, FinalBulletSpeedX, 5, 1, player.Sprite.velocity);
                                projectile.LoadContent();
                                projectiles.Add(projectile);
                            }

                            PreviousRocketFireTime = gameTime.TotalGameTime;
                        }
                    }

                    if (gameTime.TotalGameTime - PreviousLaserSound2 > LaserSound2)
                    {
                        PreviousLaserSound2 = gameTime.TotalGameTime;
                        Game1.Instance.AudioPlay("LaserFiring", 4);
                    }

                    if (LaserRectangles[0].Height > 0)
                        DrawGlow = true;
                    LaserStarted = true;
                }

            }

            LaserGlowPosition = new Vector2(player.Position.X + (player.Sprite.Width / 2), player.Position.Y + (player.Sprite.Height / 2));

            if (LaserStarted)
            {
                if (LaserCounter < LaserPositions.Length)
                {
                    if (LaserRectangles[LaserCounter].Height >= LaserTexture.Height)
                    {
                        LaserRectangles[LaserCounter].Height = LaserTexture.Height;
                        LaserCounter += 1;
                    }

                    if (LaserCounter < LaserPositions.Length)
                    {
                        LaserRectangles[LaserCounter].Height += 64;
                    }
                }
                else
                {
                    LaserCounter = 0;
                    LaserStarted = false;
                }
            }
            else
            {
                if (LaserCounter < LaserPositions.Length)
                {
                    if (LaserRectangles[LaserCounter].Height <= LaserTexture.Height)
                    {
                        LaserRectangles[LaserCounter].Height = 0;
                        LaserCounter += 1;
                    }

                    if (LaserCounter < LaserPositions.Length)
                    {
                        LaserRectangles[LaserCounter].Height -= LaserTexture.Height;
                    }
                }

                if (LaserRectangles[1].Height > 0)
                    DrawGlow = true;
            }

            #endregion

            #region Money

            if (Money)
            {
                MoneyParticles.AddParticles = true;

                if (gameTime.TotalGameTime - PreviousMoneySound > MoneySound)
                {
                    PreviousMoneySound = gameTime.TotalGameTime;
                    Game1.Instance.AudioPlay("MoneyPower", 4);
                }
            }
            else
            {
                MoneyParticles.AddParticles = false;
            }

            #endregion

            if (Shield)
            {
            }

            if ((Healing || Laser || Money || TimeStop) && gameTime.TotalGameTime - PreviousEnergyDrain > EnergyDrain && PlayerEnergy >= EnergyCost)
            {
                if ((float)RecievedHealthIncrease / 10f < 1f)
                {
                    EnergyDrainCounter += EnergyCost;

                    if (EnergyDrainCounter > 9)
                    {
                        EnergyDrainCounter -= 10;
                        PlayerEnergy -= 1;
                    }
                }
                else
                {
                    PlayerEnergy -= (int)((float)EnergyCost / 10f);
                }
            }
        }

        private void UpdateExperience(GameTime gameTime)
        {
            PlayerXP += (PlayerCredits - PreviousCredits);
            GameCredits += (PlayerCredits - PreviousCredits);
            if (PlayerCredits > 1000000000)
                PlayerCredits = 1000000000;

            ExperienceSubtract = 0;

            if (PlayerLevel == 1)
                ExperienceToLevel = 1000;
            else if (PlayerLevel == 2)
            {
                ExperienceToLevel = 2500;
                ExperienceSubtract = 1000;
            }
            else if (PlayerLevel == 3)
            {
                ExperienceToLevel = 5000;
                ExperienceSubtract = 2500;
            }
            else if (PlayerLevel == 4)
            {
                ExperienceToLevel = 10000;
                ExperienceSubtract = 5000;
            }
            else if (PlayerLevel == 5)
            {
                ExperienceToLevel = 25000;
                ExperienceSubtract = 10000;
            }
            else if (PlayerLevel == 6)
            {
                ExperienceToLevel = 100000;
                ExperienceSubtract = 25000;
            }
            else if (PlayerLevel == 7)
            {
                ExperienceToLevel = 250000;
                ExperienceSubtract = 100000;
            }
            else if (PlayerLevel == 8)
            {
                ExperienceToLevel = 500000;
                ExperienceSubtract = 250000;
            }
            else if (PlayerLevel == 9)
            {
                ExperienceToLevel = 1000000;
                ExperienceSubtract = 500000;
            }
            else if (PlayerLevel == 10)
                ExperienceToLevel = 1;

            if (PlayerLevel != 10)
            {
                if (PlayerXP >= ExperienceToLevel)
                {
                    PlayerLevel += 1;
                }
            }
            
            if (gameTime.TotalGameTime - PreviousExperienceBonus > ExperienceBonus)
            {
                PlayerXP += (int)((float)(PreviousProjectilesFired - PlayerBulletsFired) / (float)(PreviousEnemiesHit - PlayerEnemiesHit) * 1000);

                PreviousExperienceBonus = gameTime.TotalGameTime;

                PreviousEnemiesHit = PlayerEnemiesHit;
                PreviousProjectilesFired = PlayerBulletsFired;
            }

            PreviousCredits = PlayerCredits;
        }

        private void LevelUpUpdate(GameTime gameTime)
        {
            LevelUpParticles.AddParticles = true;
            LevelUpParticles.EmitterLocationUpdate(new Vector2(random.Next((int)player.Position.X, (int)player.Position.X + player.Sprite.Width), random.Next((int)player.Position.Y, (int)player.Position.Y + player.Sprite.Height)));
            player.Sprite.Tint = Color.Gold;
            playerHull.Sprite.Tint = Color.Gold;

            if (gameTime.TotalGameTime - PreviousLevelUpTime > LevelUpTime || PlayerHealth <= 0)
            {
                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                LevelUp = false;
                LevelUpParticles.AddParticles = false;
            }
        }

        private void BeenStunned(GameTime gameTime)
        {
            if (InitiateStun)
            {
                PreviousStunTimer = gameTime.TotalGameTime;
                StunTimer = TimeSpan.FromSeconds(StunTime);
                StunParticle.AddParticles = true;
                InitiateStun = false;
            }

            player.Sprite.Tint = Color.Yellow;
            playerHull.Sprite.Tint = Color.Yellow;
            player.Sprite.acceleration = Vector2.Zero;
            player.Sprite.velocity = Vector2.Zero;


            if (gameTime.TotalGameTime - PreviousStunTimer > StunTimer || PlayerHealth <= 0)
            {
                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                StunParticle.AddParticles = false;
                InitiateStun = true;
                Stunned = false;
            }
        }

        private void BeenBurned(GameTime gameTime)
        {
            if (InitiateBurn)
            {
                PreviousBurnTimer = gameTime.TotalGameTime;
                BurnTimer = TimeSpan.FromSeconds(BurnTime);
                BurnParticle.AddParticles = true;
                InitiateBurn = false;
            }

            player.Sprite.Tint = Color.Orange;
            playerHull.Sprite.Tint = Color.Orange;

            if (gameTime.TotalGameTime - PreviousBurnAdd > BurnAdd || PlayerHealth <= 0)
            {
                PreviousBurnAdd = gameTime.TotalGameTime;
                PlayerHealth -= BurnDamage / 10;
                if (BurnDamage / 10 < 1)
                {
                    BurnCounter += BurnDamage;
                    if (BurnCounter >= 10)
                    {
                        BurnCounter = 0;
                        PlayerHealth -= 1;
                    }
                }
            }

            if (gameTime.TotalGameTime - PreviousBurnTimer > BurnTimer)
            {
                BurnCounter = 0;
                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                BurnParticle.AddParticles = false;
                InitiateBurn = true;
                Burned = false;
            }
        }

        private void BeenPoisoned(GameTime gameTime)
        {
            if (InitiatePoison)
            {
                PreviousPoisonTimer = gameTime.TotalGameTime;
                PoisonTimer = TimeSpan.FromSeconds(PoisonTime);
                PoisonParticle.AddParticles = true;
                InitiatePoison = false;
            }

            player.Sprite.Tint = Color.Green;
            playerHull.Sprite.Tint = Color.Green;

            if (gameTime.TotalGameTime - PreviousPoisonAdd > PoisonAdd)
            {
                PreviousPoisonAdd = gameTime.TotalGameTime;
                PlayerHealth -= PoisonDamage / 10;
                if (PoisonDamage / 10 < 1)
                {
                    PoisonCounter += PoisonDamage;
                    if (PoisonCounter >= 10)
                    {
                        PoisonCounter = 0;
                        PlayerHealth -= 1;
                    }
                }
            }

            if (gameTime.TotalGameTime - PreviousPoisonTimer > PoisonTimer || PlayerHealth <= 0)
            {
                PoisonCounter = 0;
                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                PoisonParticle.AddParticles = false;
                InitiatePoison = true;
                Poisoned = false;
            }
        }

        private void BeenSlowed(GameTime gameTime)
        {
            if (InitiateSlow)
            {
                PreviousSlowTimer = gameTime.TotalGameTime;
                SlowTimer = TimeSpan.FromSeconds(SlowTime);
                SlowParticle.AddParticles = true;
                InitiateSlow = false;
                PlayerAcceleration = PlayerAcceleration / 4;
                PlayerBulletSpeed = PlayerBulletSpeed / 4;
                PlayerFireRate = PlayerFireRate * 4;
            }

            player.Sprite.Tint = Color.LightBlue;
            playerHull.Sprite.Tint = Color.LightBlue;

            if (gameTime.TotalGameTime - PreviousSlowTimer > SlowTimer || PlayerHealth <= 0)
            {
                PlayerAcceleration = OriginalAcceleration;
                PlayerBulletSpeed = OriginalBulletSpeed;
                PlayerFireRate = OriginalFireRate;

                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                SlowParticle.AddParticles = false;
                InitiateSlow = true;
                Slowed = false;
            }
        }

        private void UpdateParticles(GameTime gameTime)
        {
            if (Alive)
            {
                if (player.locked)
                {
                    particleEngine.EmitterLocationUpdate(player.Position.X + EmitterMinX, player.Position.X + EmitterMaxX, player.Position.Y + EmitterMinY, player.Position.Y + EmitterMaxY);
                    particleEngine.Velocity = new Vector2(0, 4);
                    particleEngine.Velocity.X += player.Sprite.velocity.X / 3;
                    particleEngine.MoveUp = true;
                }
                else
                {
                    particleEngine.EmitterLocationUpdate(player.Position.X + player.Sprite.v2Center.X, player.Position.X + player.Sprite.v2Center.X, player.Position.Y + player.Sprite.v2Center.Y, player.Position.Y + player.Sprite.v2Center.Y);
                    
                    particleEngine.Velocity = -player.Sprite.rotationVector / 4;

                    if (player.Sprite.rotationVector.X > 0 && player.Sprite.rotationVector.X < 3)
                        particleEngine.Velocity.X = -3;
                    if (player.Sprite.rotationVector.X < 0 && player.Sprite.rotationVector.X < -3)
                        particleEngine.Velocity.X = 3;
                    if (player.Sprite.rotationVector.Y > 0 && player.Sprite.rotationVector.Y < 3)
                        particleEngine.Velocity.Y = -3;
                    if (player.Sprite.rotationVector.Y < 0 && player.Sprite.rotationVector.Y < -3)
                        particleEngine.Velocity.Y = 3;

                    particleEngine.MoveUp = false;
                }
                deathParticles.AddParticles = false;
            }
            else
                particleEngine.AddParticles = false;

            if (gameTime.TotalGameTime - previousHitTime > HitTime)
            {
                hitParticles.AddParticles = false;
            }

            deathParticles.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            hitParticles.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);

            StunParticle.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            StunParticle.Update();

            BurnParticle.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            BurnParticle.Update();

            PoisonParticle.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            PoisonParticle.Update();

            SlowParticle.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            SlowParticle.Update();

            LevelUpParticles.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            LevelUpParticles.Update();
            
            HealingParticles.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            HealingParticles.Update();

            LaserParticles.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            LaserParticles.Update();

            MoneyParticles.EmitterLocationUpdate(player.Position.X + 10, player.Position.X + player.Sprite.Width - 10, player.Position.Y + 10, player.Position.Y + player.Sprite.Height - 10);
            MoneyParticles.Update();

            if (HealingParticles.AddParticles && gameTime.TotalGameTime - PreviousHealingSound > HealingSound)
            {
                Game1.Instance.AudioPlay("Healing", 4);
                PreviousHealingSound = gameTime.TotalGameTime;
            }

            hitParticles.Velocity = player.Sprite.velocity / 3;
            hitParticles.Update();

            particleEngine.Update();
            deathParticles.Update();
        }

        private void AchievementTracker(GameTime gameTime)
        {
            #region Initialize

            if (InitialTracking)
            {
                Achievement1Tracker = PlayerAchievement1;
                Achievement2Tracker = PlayerAchievement2;
                Achievement3Tracker = PlayerAchievement3;
                Achievement4Tracker = PlayerAchievement4;
                Achievement5Tracker = PlayerAchievement5;
                Achievement6Tracker = PlayerAchievement6;
                Achievement7Tracker = PlayerAchievement7;
                Achievement8Tracker = PlayerAchievement8;
                Achievement9Tracker = PlayerAchievement9;
                Achievement10Tracker = PlayerAchievement10;
                Achievement11Tracker = PlayerAchievement11;
                Achievement12Tracker = PlayerAchievement12;
                Achievement13Tracker = PlayerAchievement13;
                Achievement14Tracker = PlayerAchievement14;
                Achievement15Tracker = PlayerAchievement15;
                Achievement16Tracker = PlayerAchievement16;
                Achievement17Tracker = PlayerAchievement17;
                Achievement18Tracker = PlayerAchievement18;
                Achievement19Tracker = PlayerAchievement19;
                Achievement20Tracker = PlayerAchievement20;
                Achievement21Tracker = PlayerAchievement21;
                Achievement22Tracker = PlayerAchievement22;
                Achievement23Tracker = PlayerAchievement23;
                Achievement24Tracker = PlayerAchievement24;
                Achievement25Tracker = PlayerAchievement25;
                Achievement26Tracker = PlayerAchievement26;
                Achievement27Tracker = PlayerAchievement27;
                Achievement28Tracker = PlayerAchievement28;
                Achievement29Tracker = PlayerAchievement29;
                Achievement30Tracker = PlayerAchievement30;
                Achievement31Tracker = PlayerAchievement31;
                Achievement32Tracker = PlayerAchievement32;
                Achievement33Tracker = PlayerAchievement33;
                Achievement34Tracker = PlayerAchievement34;
                Achievement35Tracker = PlayerAchievement35;
                Achievement36Tracker = PlayerAchievement36;
                Achievement37Tracker = PlayerAchievement37;
                Achievement38Tracker = PlayerAchievement38;
                Achievement39Tracker = PlayerAchievement39;
                Achievement40Tracker = PlayerAchievement40;
                Achievement41Tracker = PlayerAchievement41;
                Achievement42Tracker = PlayerAchievement42;
                Achievement43Tracker = PlayerAchievement43;
                Achievement44Tracker = PlayerAchievement44;
                Achievement45Tracker = PlayerAchievement45;
                Achievement46Tracker = PlayerAchievement46;
                Achievement47Tracker = PlayerAchievement47;
                Achievement48Tracker = PlayerAchievement48;
                Achievement49Tracker = PlayerAchievement49;
                Achievement50Tracker = PlayerAchievement50;

                InitialTracking = false;
            }

            #endregion

            #region Animation

            if (Achievement1Tracker == false && PlayerAchievement1)
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

            if (Achievement2Tracker == false && PlayerAchievement2)
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

            if (Achievement3Tracker == false && PlayerAchievement3)
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

            if (Achievement4Tracker == false && PlayerAchievement4)
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

            if (Achievement5Tracker == false && PlayerAchievement5)
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


            if (Achievement6Tracker == false && PlayerAchievement6)
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


            if (Achievement7Tracker == false && PlayerAchievement7)
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


            if (Achievement8Tracker == false && PlayerAchievement8)
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


            if (Achievement9Tracker == false && PlayerAchievement9)
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


            if (Achievement10Tracker == false && PlayerAchievement10)
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

            if (Achievement11Tracker == false && PlayerAchievement11)
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

            if (Achievement12Tracker == false && PlayerAchievement12)
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

            if (Achievement13Tracker == false && PlayerAchievement13)
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

            if (Achievement14Tracker == false && PlayerAchievement14)
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

            if (Achievement15Tracker == false && PlayerAchievement15)
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


            if (Achievement16Tracker == false && PlayerAchievement16)
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


            if (Achievement17Tracker == false && PlayerAchievement17)
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


            if (Achievement18Tracker == false && PlayerAchievement18)
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


            if (Achievement19Tracker == false && PlayerAchievement19)
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


            if (Achievement20Tracker == false && PlayerAchievement20)
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

            if (Achievement21Tracker == false && PlayerAchievement21)
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

            if (Achievement22Tracker == false && PlayerAchievement22)
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

            if (Achievement23Tracker == false && PlayerAchievement23)
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

            if (Achievement24Tracker == false && PlayerAchievement24)
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

            if (Achievement25Tracker == false && PlayerAchievement25)
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


            if (Achievement26Tracker == false && PlayerAchievement26)
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


            if (Achievement27Tracker == false && PlayerAchievement27)
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


            if (Achievement28Tracker == false && PlayerAchievement28)
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


            if (Achievement29Tracker == false && PlayerAchievement29)
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


            if (Achievement30Tracker == false && PlayerAchievement30)
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

            if (Achievement31Tracker == false && PlayerAchievement31)
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

            if (Achievement32Tracker == false && PlayerAchievement32)
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

            if (Achievement33Tracker == false && PlayerAchievement33)
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

            if (Achievement34Tracker == false && PlayerAchievement34)
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

            if (Achievement35Tracker == false && PlayerAchievement35)
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


            if (Achievement36Tracker == false && PlayerAchievement36)
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


            if (Achievement37Tracker == false && PlayerAchievement37)
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


            if (Achievement38Tracker == false && PlayerAchievement38)
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


            if (Achievement39Tracker == false && PlayerAchievement39)
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


            if (Achievement40Tracker == false && PlayerAchievement40)
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

            if (Achievement41Tracker == false && PlayerAchievement41)
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

            if (Achievement42Tracker == false && PlayerAchievement42)
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

            if (Achievement43Tracker == false && PlayerAchievement43)
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

            if (Achievement44Tracker == false && PlayerAchievement44)
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

            if (Achievement45Tracker == false && PlayerAchievement45)
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


            if (Achievement46Tracker == false && PlayerAchievement46)
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


            if (Achievement47Tracker == false && PlayerAchievement47)
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


            if (Achievement48Tracker == false && PlayerAchievement48)
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


            if (Achievement49Tracker == false && PlayerAchievement49)
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


            if (Achievement50Tracker == false && PlayerAchievement50)
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

        private void DeathDrain(GameTime gameTime)
        {
            if (PlayerHealth <= 0 && Alive)
            {
                deathParticles.AddParticles = true;
                deathParticles.color = PlayerColor;
                deathParticles.originalColor = PlayerColor;
                PlayerDeathCount += 1;
                player.Sprite.CurrentAnimation = "gone";
                playerHull.Sprite.CurrentAnimation = "gone";
                previousSpawnTime = gameTime.TotalGameTime;
                player.Sprite.acceleration = Vector2.Zero;
                player.Sprite.velocity = Vector2.Zero;
                BurnCounter = 0;
                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                BurnParticle.AddParticles = false;
                InitiateBurn = true;
                Burned = false;
                StunParticle.AddParticles = false;
                InitiateStun = true;
                Stunned = false;
                Alive = false;


                for (int i = projectiles.Count - 1; i >= 0; i--)
                {
                    projectiles.RemoveAt(i);
                }
            }

            if (Alive == false && gameTime.TotalGameTime - PreviousDeadTime > DeadTime && gameTime.TotalGameTime - previousSpawnTime < spawnTime)
            {
                Game1.Instance.AudioPlay("Explosion", 4);
                PreviousDeadTime = gameTime.TotalGameTime;
            }

            if (playerHull.Sprite.CurrentAnimation == "gone" && gameTime.TotalGameTime - previousSpawnTime > spawnTime && FinalLives > 0)
            {
                playerHull.Sprite.CurrentAnimation = "default";
                player.Sprite.CurrentAnimation = "default";
                player.Sprite.SetPosX((screenWidth / 2) - (player.Sprite.Width / 2));
                PlayerHealth = PlayerMaxHealth;
                PlayerEnergy = PlayerMaxEnergy;
                previousEnergyDrainTime = gameTime.TotalGameTime;
                BonusLives -= 1;
                beenHit = true;
                Alive = true;
            }

            if (playerHull.Sprite.CurrentAnimation == "gone" && gameTime.TotalGameTime - previousSpawnTime > spawnTime && FinalLives == 0)
                GameOver = true;
            else
                GameOver = false;

            if (Draining)
            {
                if (gameTime.TotalGameTime - previousEnergyDrainTime > energyDrainTime && PlayerEnergy > 0 && Alive)
                {
                    PlayerEnergy -= 1;
                    previousEnergyDrainTime = gameTime.TotalGameTime;
                }

                if (gameTime.TotalGameTime - previousEnergyDrainTime > energyDrainTime && PlayerEnergy <= 0 && PlayerHealth > 0)
                {
                    PlayerHealth -= 1;
                    previousEnergyDrainTime = gameTime.TotalGameTime;
                }
            }

            if (GameOver)
            {
                player.Sprite.Position = new Vector2(0 - (screenHeight * 2), 0 - (screenWidth * 2));
            }
        }

        private void PlayerPools()
        {
            if ((PlayerAcceleration + BonusAcceleration) > 150)
            {
                AccelerationPool = FinalAcceleration - 150;
                BonusAcceleration = BonusAcceleration - AccelerationPool;
            }

            if ((PlayerFireRate - BonusFireRate) < 0.01)
            {
                FireRatePool = 0.1f;
                BonusFireRate = BonusFireRate - FireRatePool;
            }

            if ((PlayerBulletSpeed + BonusBulletSpeed) > 30)
            {
                //BulletSpeedPool = FinalBulletSpeedY - 30;
                //BonusBulletSpeed = BonusBulletSpeed - BulletSpeedPool;
            }

            if ((PlayerMaxBullets + BonusMaxBullets) > 250)
            {
                MaxBulletsPool = FinalMaxBullets - 250;
                BonusMaxBullets = BonusMaxBullets - MaxBulletsPool;
            }

            if (PlayerHealth > PlayerMaxHealth)
            {
                HealthPool = PlayerHealth - PlayerMaxHealth;
                PlayerHealth = PlayerMaxHealth;
            }

            if (PlayerEnergy > PlayerMaxEnergy)
            {
                EnergyPool = PlayerEnergy - PlayerMaxEnergy;
                PlayerEnergy = PlayerMaxEnergy;
            }

            if ((PlayerDamage + BonusDamage) > 1000)
            {
                DamagePool = FinalDamage - 1000;
                BonusDamage = BonusDamage - DamagePool;
            }

            if ((PlayerLives + BonusLives) > 4)
            {
                LivesPool = (PlayerLives + BonusLives) - 4;
                BonusLives = BonusLives - LivesPool;
            }
        }

        public void FinalValues()
        {
            PlayerMaxSpeed = FinalAcceleration / 6;
            PlayerStoppingSpeed = (float)(FinalAcceleration / 1.25);
            player.Sprite.Physics(PlayerMaxSpeed, PlayerStoppingSpeed);

            if (PlayerFireRate < 0.01)
                PlayerFireRate = 0.01f;
            FinalFireRate = TimeSpan.FromSeconds((PlayerFireRate - BonusFireRate) + (float)((float)random.Next(0, 2) / 10));
            FinalAcceleration = PlayerAcceleration + BonusAcceleration;

            if (player.locked)
            {
                FinalBulletSpeedY = -(PlayerBulletSpeed + BonusBulletSpeed);
                FinalBulletSpeedX = player.Sprite.acceleration.X / 20;

                FinalBulletSpeedX = 0;
            }
            else
            {
                if (player.Sprite.rotationVector.Y > 0.1f)
                    FinalBulletSpeedY = ((PlayerBulletSpeed + BonusBulletSpeed) * player.Sprite.rotationVector.Y);
                else if (player.Sprite.rotationVector.Y < -0.1f)
                    FinalBulletSpeedY = ((PlayerBulletSpeed + BonusBulletSpeed) * player.Sprite.rotationVector.Y);
                else
                    FinalBulletSpeedY = 0;

                if (player.Sprite.rotationVector.X > 0.1f)
                    FinalBulletSpeedX = ((PlayerBulletSpeed + BonusBulletSpeed) * player.Sprite.rotationVector.X);
                else if (player.Sprite.rotationVector.X < -0.1f)
                    FinalBulletSpeedX = ((PlayerBulletSpeed + BonusBulletSpeed) * player.Sprite.rotationVector.X);
                else
                    FinalBulletSpeedX = 0;
            }
            FinalDamage = PlayerDamage + BonusDamage;
            FinalMaxBullets = PlayerMaxBullets + BonusMaxBullets;
            FinalLives = PlayerLives + BonusLives;

            if (FinalAcceleration < 10f)
                BonusAcceleration += 10f;
            if ((PlayerFireRate - BonusFireRate) > 1.11f)
                BonusFireRate -= 0.1f;
            if (FinalBulletSpeedY > -5f && player.locked)
                BonusBulletSpeed += 5f;
            if (FinalDamage < 1)
                BonusDamage += 1;
            if (FinalMaxBullets < 1)
                BonusMaxBullets += 1;

            FinalFireRate = TimeSpan.FromSeconds((PlayerFireRate - BonusFireRate) + (float)((float)random.Next(0, 2) / 10));
            FinalAcceleration = PlayerAcceleration + BonusAcceleration;

            FinalDamage = PlayerDamage + BonusDamage;
            FinalMaxBullets = PlayerMaxBullets + BonusMaxBullets;
        }

        private void UpdatePlayers(GameTime gameTime)
        {
            player.Update(gameTime);
            playerHull.Position = player.Position;
            playerHull.Update(gameTime);
        }

        private void UpdateProjectiles(GameTime gameTime)
        {
            if (PlayerBulletsFired > 1000000000)
                PlayerBulletsFired = 1000000000;

            if (Active)
            {
                if (gameTime.TotalGameTime - previousFireTime > FinalFireRate && IsFiring && projectiles.Count < FinalMaxBullets && Alive)
                {
                    previousFireTime = gameTime.TotalGameTime;
                    AddProjectile(Position + new Vector2(Width / 2 - (projectileWidth / 2), Height / 2));
                    isFiring = false;
                }
                else if (gameTime.TotalGameTime - PreviousShotVolley > ShotVolley && VolleyOver == false)
                {
                    PreviousShotVolley = gameTime.TotalGameTime;
                    AddProjectile(Position + new Vector2(Width / 2 - (projectileWidth / 2), Height / 2));
                }

                if (TimeStop == false)
                {
                    for (int i = projectiles.Count - 1; i >= 0; i--)
                    {
                        projectiles[i].ProjectileCount = projectiles.Count;
                        projectiles[i].particleEngine.total = FinalMaxBullets / projectiles.Count;
                        projectiles[i].Update(gameTime, ClosestEnemy);
                        if (projectiles[i].active == false)
                            projectiles[i].projectile.Position = new Vector2(screenWidth * 2, screenHeight * 2);
                        if (projectiles[i].active == false && projectiles[i].radius > 0)
                        {
                            projectiles[i].Explosion = true;
                        }
                        if ((projectiles[i].active == false && projectiles[i].particleEngine.particles.Count == 0 && projectiles[i].ExplosionParticle.particles.Count == 0 && projectiles[i].radius < 1) || Active == false)
                        {
                            if (projectiles[i].homing)
                                HomingProjectiles -= 1;

                            projectiles.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void UpdatePowerUps()
        {
            if (powerUpActivated)
            {
                PowerUp.Initialize(1, powerUp);

                BonusAcceleration += PowerUp.Acceleration;
                BonusBulletSpeed += PowerUp.BulletSpeed;
                if (powerUp == 6 || powerUp == 7)
                    BonusDamage += ((FinalDamage / 2) * PowerUp.Damage);
                BonusFireRate += PowerUp.FireRate;
                BonusMaxBullets += PowerUp.MaxBullets;
                BonusLives += PowerUp.Lives;
                if (PowerUp.AutoFire)
                    BonusAutoFire = true;
                if (PowerUp.Energy)
                    PlayerEnergy = PlayerMaxEnergy;
                if (PowerUp.Health)
                    PlayerHealth = PlayerMaxHealth;

                if (powerUp == 2)
                    RedSkull += 1;
                if (powerUp == 5)
                    GreenSkull += 1;
                if (powerUp == 7)
                    PinkSkull += 1;
                if (powerUp == 9)
                    YellowSkull += 1;
                if (powerUp == 11)
                    BlueSkull += 1;

                PowerUp.ResetPowerUps();

                powerUpActivated = false;
            }

            if (RedSkull > 0 && GreenSkull > 0 && PinkSkull > 0 && YellowSkull > 0 && BlueSkull > 0)
            {
                RedSkull -= 1;
                GreenSkull -= 1;
                PinkSkull -= 1;
                YellowSkull -= 1;
                BlueSkull -= 1;

                PlayerCredits += (PlayerCredits / 100);
                PlayerHealth = PlayerMaxHealth;
                PlayerEnergy = PlayerMaxEnergy;

                if (fireStyle < 5)
                    fireStyle += 1;
            }
        }

        public void UpdateHit(GameTime gameTime)
        {
            if (hit)
            {
                hit = false;
            }

            if (beenHit)
            {
                previousHitTime = gameTime.TotalGameTime;
                hitParticles.AddParticles = true;
                keyboardUpdate.Vibrate = true;
                player.Sprite.Tint = Color.Red;
                playerHull.Sprite.Tint = Color.Red;
                beenHit = false;
                Recovered = false;
            }

            if (gameTime.TotalGameTime - previousHitTime > TimeSpan.FromSeconds(0.03f) && Recovered == false)
            {
                player.Sprite.Tint = PlayerColor;
                playerHull.Sprite.Tint = Color.White;
                Recovered = true;
            }
        }

        private void UpdateControls()
        {
            if (Alive)
            {
                player.Sprite.MoveX(keyboardUpdate.LeftStickX * FinalAcceleration);
                player.Sprite.MoveY(-keyboardUpdate.LeftStickY * FinalAcceleration);

                if (keyboardUpdate.LeftStickX != 0)
                {
                    if (keyboardUpdate.LeftStickX > 0)
                    {
                        Right = true;
                        Left = false;
                    }
                    else if (keyboardUpdate.LeftStickX < 0)
                    {
                        Right = false;
                        Left = true;
                    }
                }
                else if (keyboardUpdate.Left)
                {
                    player.Sprite.MoveX(-FinalAcceleration);
                    Left = true;
                }
                else
                    if (keyboardUpdate.Right)
                    {
                        player.Sprite.MoveX(FinalAcceleration);
                        Left = false;
                        Right = true;
                    }
                    else
                    {
                        player.Sprite.MoveX(0);
                        Left = false;
                        Right = false;
                    }

                if (keyboardUpdate.LeftStickY != 0)
                {
                }
                else if (keyboardUpdate.Up)
                {
                    player.Sprite.MoveY(-FinalAcceleration);
                }
                else
                    if (keyboardUpdate.Down)
                    {
                        player.Sprite.MoveY(FinalAcceleration);
                    }
                    else
                    {
                        player.Sprite.MoveY(0);
                    }
            }

            if (keyboardUpdate.Quit)
            {
                paused = true;
            }

            //if (keyboardUpdate.Quit)
            //{
            //    Active = false;
            //}

            if (autoFire)
            {
                isFiring = keyboardUpdate.Fire;
            }
            if (autoFireOff)
            {
                isFiring = false;
                if (keyboardUpdate.Fire)
                {
                    isFiring = true;
                    autoFireOff = false;
                }
            }
            else if (keyboardUpdate.Fire == false)
            {
                autoFireOff = true;
            }
        }

        private void Boundries()
        {
            if (player.locked)
            {
                if (player.Position.X > MaxX + player.Sprite.Width)
                {
                    player.Sprite.SetPosX(MinX - player.Sprite.Width);
                }

                if (player.Position.X < MinX - player.Sprite.Width)
                {
                    player.Sprite.SetPosX(MaxX + player.Sprite.Width);
                }

                if (player.Position.Y > MaxY + player.Sprite.Height)
                {
                    player.Sprite.SetPosY(MinY - player.Sprite.Height);
                }

                if (player.Position.Y < MinY - player.Sprite.Height)
                {
                    player.Sprite.SetPosY(MaxY + player.Sprite.Height);
                }

                player.Sprite.SetPosY((screenHeight - player.Sprite.Height) - YLock);
                player.Sprite.MoveY(0);
            }

            if (playerHull.Sprite.CurrentAnimation == "default")
            {
                if (Left)
                {
                    player.Sprite.CurrentAnimation = "left";

                    if (player.Sprite.AutoRotate == false)
                    {
                        player.Sprite.Rotation = -0.1f;
                        playerHull.Sprite.Rotation = -0.1f;
                    }
                }
                else
                    if (Right)
                    {
                        player.Sprite.CurrentAnimation = "right";

                        if (player.Sprite.AutoRotate == false)
                        {
                            player.Sprite.Rotation = 0.1f;
                            playerHull.Sprite.Rotation = 0.1f;
                        }
                    }
                    else
                    {
                        player.Sprite.CurrentAnimation = "default";

                        if (player.Sprite.AutoRotate == false)
                        {
                            player.Sprite.Rotation = 0f;
                            playerHull.Sprite.Rotation = 0f;
                        }
                    }
            }
        }

        private void AddProjectile(Vector2 position)
        {
            if (bulletCounter > availableWeapons)
                bulletCounter = 1;

            #region Shot 1

            if ((fireStyle == 1 || fireStyle == 3 || fireStyle == 5) && BulletFiring == 1)
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();

                CurrentSelectedWeapon();

                projectile.Initialize(CurrentWeapon, position, FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;

                if (player.locked)
                    projectile.Locked = true;
                else
                    projectile.Locked = false;

                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                projectile.projectile.Sprite.SetPosY((int)projectile.projectile.Position.Y - (projectileHeight / 2));
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            #endregion

            #region Shot 1 For Two and Four Shot

            else if ((fireStyle == 2 || fireStyle == 4) && BulletFiring == 1)
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();
                if (bulletCounter == 1)
                    projectile.Initialize(PlayerSelectedWeapon1, new Vector2(position.X - 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 2)
                    projectile.Initialize(PlayerSelectedWeapon2, new Vector2(position.X - 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 3)
                    projectile.Initialize(PlayerSelectedWeapon3, new Vector2(position.X - 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 4)
                    projectile.Initialize(PlayerSelectedWeapon4, new Vector2(position.X - 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 5)
                    projectile.Initialize(PlayerSelectedWeapon5, new Vector2(position.X - 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;
                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                position.Y -= projectileHeight / 2;
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            if (bulletCounter > availableWeapons)
                bulletCounter = 1;

            #endregion

            #region Shot 2 for Two and Four Shot

            else if ((fireStyle == 2 || fireStyle == 4) && BulletFiring == 2)
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();
                if (bulletCounter == 1)
                    projectile.Initialize(PlayerSelectedWeapon1, new Vector2(position.X + 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 2)
                    projectile.Initialize(PlayerSelectedWeapon2, new Vector2(position.X + 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 3)
                    projectile.Initialize(PlayerSelectedWeapon3, new Vector2(position.X + 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 4)
                    projectile.Initialize(PlayerSelectedWeapon4, new Vector2(position.X + 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);
                if (bulletCounter == 5)
                    projectile.Initialize(PlayerSelectedWeapon5, new Vector2(position.X + 10, position.Y + 10), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 1, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;
                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                position.Y -= projectileHeight / 2;
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            #endregion

            #region Shot 2

            else if ((fireStyle == 3 && BulletFiring == 2) || (fireStyle == 4 && BulletFiring == 3) || (fireStyle == 5 && BulletFiring == 2))
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();
                if (bulletCounter == 1)
                    projectile.Initialize(PlayerSelectedWeapon1, new Vector2(position.X - 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 2, 1, player.Sprite.velocity);
                if (bulletCounter == 2)
                    projectile.Initialize(PlayerSelectedWeapon2, new Vector2(position.X - 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 2, 1, player.Sprite.velocity);
                if (bulletCounter == 3)
                    projectile.Initialize(PlayerSelectedWeapon3, new Vector2(position.X - 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 2, 1, player.Sprite.velocity);
                if (bulletCounter == 4)
                    projectile.Initialize(PlayerSelectedWeapon4, new Vector2(position.X - 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 2, 1, player.Sprite.velocity);
                if (bulletCounter == 5)
                    projectile.Initialize(PlayerSelectedWeapon5, new Vector2(position.X - 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 2, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;
                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                position.Y -= projectileHeight / 2;
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            #endregion

            #region Shot 3

            else if ((fireStyle == 3 && BulletFiring == 3) || (fireStyle == 4 && BulletFiring == 4) || (fireStyle == 5 && BulletFiring == 3))
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();
                if (bulletCounter == 1)
                    projectile.Initialize(PlayerSelectedWeapon1, new Vector2(position.X + 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 4, 1, player.Sprite.velocity);
                if (bulletCounter == 2)
                    projectile.Initialize(PlayerSelectedWeapon2, new Vector2(position.X + 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 4, 1, player.Sprite.velocity);
                if (bulletCounter == 3)
                    projectile.Initialize(PlayerSelectedWeapon3, new Vector2(position.X + 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 4, 1, player.Sprite.velocity);
                if (bulletCounter == 4)
                    projectile.Initialize(PlayerSelectedWeapon4, new Vector2(position.X + 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 4, 1, player.Sprite.velocity);
                if (bulletCounter == 5)
                    projectile.Initialize(PlayerSelectedWeapon5, new Vector2(position.X + 15, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 4, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;
                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                position.Y -= projectileHeight / 2;
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            #endregion

            #region Shot 4

            else if (fireStyle == 5 && BulletFiring == 4)
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();
                if (bulletCounter == 1)
                    projectile.Initialize(PlayerSelectedWeapon1, new Vector2(position.X - 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 3, 1, player.Sprite.velocity);
                if (bulletCounter == 2)
                    projectile.Initialize(PlayerSelectedWeapon2, new Vector2(position.X - 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 3, 1, player.Sprite.velocity);
                if (bulletCounter == 3)
                    projectile.Initialize(PlayerSelectedWeapon3, new Vector2(position.X - 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 3, 1, player.Sprite.velocity);
                if (bulletCounter == 4)
                    projectile.Initialize(PlayerSelectedWeapon4, new Vector2(position.X - 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 3, 1, player.Sprite.velocity);
                if (bulletCounter == 5)
                    projectile.Initialize(PlayerSelectedWeapon5, new Vector2(position.X - 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 3, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;
                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                position.Y -= projectileHeight / 2;
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            #endregion

            #region Shot 5

            else if (fireStyle == 5 && BulletFiring == 5)
            {
                BulletFiring += 1;

                Projectile projectile = new Projectile();
                if (bulletCounter == 1)
                    projectile.Initialize(PlayerSelectedWeapon1, new Vector2(position.X + 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 5, 1, player.Sprite.velocity);
                if (bulletCounter == 2)
                    projectile.Initialize(PlayerSelectedWeapon2, new Vector2(position.X + 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 5, 1, player.Sprite.velocity);
                if (bulletCounter == 3)
                    projectile.Initialize(PlayerSelectedWeapon3, new Vector2(position.X + 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 5, 1, player.Sprite.velocity);
                if (bulletCounter == 4)
                    projectile.Initialize(PlayerSelectedWeapon4, new Vector2(position.X + 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 5, 1, player.Sprite.velocity);
                if (bulletCounter == 5)
                    projectile.Initialize(PlayerSelectedWeapon5, new Vector2(position.X + 20, position.Y + 15), FinalDamage, FinalBulletSpeedY, FinalBulletSpeedX, 5, 1, player.Sprite.velocity);

                #region Projectile Level

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 1)
                    projectile.projectileLevel = iPlayerElectricProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 2)
                    projectile.projectileLevel = iPlayerLaserProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 3)
                    projectile.projectileLevel = iPlayerFireProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 4)
                    projectile.projectileLevel = iPlayerPoisonProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 5)
                    projectile.projectileLevel = iPlayerExplosiveProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 6)
                    projectile.projectileLevel = iPlayerSlowProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 7)
                    projectile.projectileLevel = iPlayerHealthProjectile;

                if (bulletCounter == 1 && PlayerSelectedWeapon1 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 2 && PlayerSelectedWeapon2 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 3 && PlayerSelectedWeapon3 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 4 && PlayerSelectedWeapon4 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;
                if (bulletCounter == 5 && PlayerSelectedWeapon5 == 8)
                    projectile.projectileLevel = iPlayerEnergyProjectile;

                #endregion

                bulletCounter += 1;
                projectile.LoadContent();
                projectileWidth = projectile.projectile.Sprite.Width;
                projectileHeight = projectile.projectile.Sprite.Height;
                position.Y -= projectileHeight / 2;
                if (projectile.homing)
                {
                    if (HomingProjectiles < MaxHomingProjectiles)
                    {
                        HomingProjectiles += 1;
                        projectiles.Add(projectile);
                        PlayerBulletsFired += 1;
                    }
                }
                else
                {
                    projectiles.Add(projectile);
                    PlayerBulletsFired += 1;
                }
            }

            #endregion

            if (fireStyle == 1 && BulletFiring > 1)
            {
                BulletFiring = 1;
                VolleyOver = true;
            }
            else if (fireStyle == 2 && BulletFiring > 2)
            {
                BulletFiring = 1;
                VolleyOver = true;
            }
            else if (fireStyle == 3 && BulletFiring > 3)
            {
                BulletFiring = 1;
                VolleyOver = true;
            }
            else if (fireStyle == 4 && BulletFiring > 4)
            {
                BulletFiring = 1;
                VolleyOver = true;
            }
            else if (fireStyle == 5 && BulletFiring > 5)
            {
                BulletFiring = 1;
                VolleyOver = true;
            }
            else
                VolleyOver = false;
        }

        private void CurrentSelectedWeapon()
        {
            if (bulletCounter == 1)
                CurrentWeapon = PlayerSelectedWeapon1;
            if (bulletCounter == 2)
                CurrentWeapon = PlayerSelectedWeapon2;
            if (bulletCounter == 3)
                CurrentWeapon = PlayerSelectedWeapon3;
            if (bulletCounter == 4)
                CurrentWeapon = PlayerSelectedWeapon4;
            if (bulletCounter == 5)
                CurrentWeapon = PlayerSelectedWeapon5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active && paused == false)
            {
                for (int i = 0; i < LaserPositions.Length; i++)
                {
                    spriteBatch.Draw(LaserTexture, LaserPositions[i], LaserRectangles[i], Color.White);
                }

                if (DrawGlow)
                    spriteBatch.Draw(LaserGlow, LaserGlowPosition, null, Color.White, 0f, new Vector2(LaserGlow.Width / 2, LaserGlow.Height / 2), 1f + ((float)iPlayerLaserSpecial / 30f), SpriteEffects.None, 0f);

                LaserParticles.Draw(spriteBatch);
                MoneyParticles.Draw(spriteBatch);

                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }


                particleEngine.Draw(spriteBatch);
                player.Draw(spriteBatch);
                playerHull.Draw(spriteBatch);
                deathParticles.Draw(spriteBatch);
                StunParticle.Draw(spriteBatch);
                BurnParticle.Draw(spriteBatch);
                PoisonParticle.Draw(spriteBatch);
                SlowParticle.Draw(spriteBatch);

                hitParticles.Draw(spriteBatch);
                HealingParticles.Draw(spriteBatch);
                LevelUpParticles.Draw(spriteBatch);
            }

            if (paused)
            {
                pauseMenu.Draw(spriteBatch);
            }

            #region Achievement Tracker

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
            
            #endregion
        }
    }
}
