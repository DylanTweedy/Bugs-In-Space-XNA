using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class PauseMenu
    {
        KeyboardUpdate keyboardUpdate;
        int MenuXLocation;
        int MenuYLocation;
        int controlSystem;
        SpriteFont font;
        int MenuState;
        Vector2 ResumeLocation;
        Vector2 InventoryLocation;
        Vector2 UpgradeLocation;
        Vector2 DropOutLocation;
        Color ResumeColor;
        Color InventoryColor;
        Color UpgradeColor;
        Color DropOutColor;
        public bool DropOut;
        public bool Resume;
        Vector2 IconPosition;
        bool InitializeSystem;

        #region Upgrade Icons

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

        #region Ships

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

        #endregion

        #region Player Variables

        //public float playerAcceleration;
        //public float playerBulletSpeed;
        //public int playerShip;
        //public int playerMaxBullets;
        //public int playerDamage;
        //public int playerRedValue;
        //public int playerBlueValue;
        //public int playerGreenValue;
        //public float playerFireRate;
        //public int playerCredits;
        //public int playerLives;
        //public int playerMaxHealth;
        //public int playerMaxEnergy;

        //public int iplayerAmmo;
        //public int iplayerBulletSpeed;
        //public int iplayerDamage;
        //public int iplayerElectricProjectile;
        //public int iplayerEnergy;
        //public int iplayerEnergyProjectile;
        //public int iplayerExplosiveProjectile;
        //public int iplayerFireProjectile;
        //public int iplayerFireRate;
        //public int iplayerHealingSpecial;
        //public int iplayerHealth;
        //public int iplayerHealthProjectile;
        //public int iplayerLaserProjectile;
        //public int iplayerLaserSpecial;
        //public int iplayerMoneySpecial;
        //public int iplayerMovementSpeed;
        //public int iplayerPoisonProjectile;
        //public int iplayerShieldSpecial;
        //public int iplayerSlowProjectile;
        //public int iplayerTimeStopSpecial;

        //public bool bplayerTripleShot;
        //public bool bplayerQuadShot;
        //public bool bplayerQuintupleShot;
        //public bool bplayerDoubleShot;
        //public int playerWeaponsCollected;
        //public int playerSelectedWeapon1;
        //public int playerSelectedWeapon2;
        //public int playerSelectedWeapon3;
        //public int playerSelectedWeapon4;
        //public int playerSelectedWeapon5;
        //public int playerSelectedSpecial;
        //public int playerShipsUnlocked;

        #endregion

        public void Initialize(int ControlSystem, Vector2 iconPosition)
        {
            keyboardUpdate = new KeyboardUpdate();
            keyboardUpdate.Initialize(ControlSystem, 0);
            MenuXLocation = 1;
            MenuYLocation = 1;
            controlSystem = ControlSystem;
            MenuState = 0;
            Resume = false;
            IconPosition = iconPosition;
            InitializeSystem = true;
        }

        public void RecievingVariables()
        {
        }

        public void SendingVariables()
        {
        }

        public void LoadContent()
        {
            font = Game1.Instance.Content.Load<SpriteFont>("Fonts//Font1");
        }
        
        private void LoadUpgradeIcons()
        {
            AmmoTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Ammo");
            Ammo = new MobileSprite(AmmoTexture);
            Ammo.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            Ammo.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            Ammo.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            Ammo.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            Ammo.Sprite.CurrentAnimation = "default";
            Ammo.Position = IconPosition;
            Ammo.IsMoving = false;

            AutoFireTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//AutoFire");
            AutoFire = new MobileSprite(AutoFireTexture);
            AutoFire.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            AutoFire.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            AutoFire.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            AutoFire.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            AutoFire.Sprite.CurrentAnimation = "default";
            AutoFire.Position = IconPosition;
            AutoFire.IsMoving = false;

            BulletSpeedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//BulletSpeed");
            BulletSpeed = new MobileSprite(BulletSpeedTexture);
            BulletSpeed.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            BulletSpeed.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            BulletSpeed.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            BulletSpeed.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            BulletSpeed.Sprite.CurrentAnimation = "default";
            BulletSpeed.Position = IconPosition;
            BulletSpeed.IsMoving = false;

            DamageTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Damage");
            Damage = new MobileSprite(DamageTexture);
            Damage.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            Damage.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            Damage.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            Damage.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            Damage.Sprite.CurrentAnimation = "default";
            Damage.Position = IconPosition;
            Damage.IsMoving = false;

            DoubleShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//DoubleShot");
            DoubleShot = new MobileSprite(DoubleShotTexture);
            DoubleShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            DoubleShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            DoubleShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            DoubleShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            DoubleShot.Sprite.CurrentAnimation = "default";
            DoubleShot.Position = IconPosition;
            DoubleShot.IsMoving = false;

            ElectricProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ElectricProjectile");
            ElectricProjectile = new MobileSprite(ElectricProjectileTexture);
            ElectricProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ElectricProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ElectricProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ElectricProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ElectricProjectile.Sprite.CurrentAnimation = "default";
            ElectricProjectile.Position = IconPosition;
            ElectricProjectile.IsMoving = false;

            EnergyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Energy");
            Energy = new MobileSprite(EnergyTexture);
            Energy.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            Energy.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            Energy.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            Energy.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            Energy.Sprite.CurrentAnimation = "default";
            Energy.Position = IconPosition;
            Energy.IsMoving = false;

            EnergyProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//EnergyProjectile");
            EnergyProjectile = new MobileSprite(EnergyProjectileTexture);
            EnergyProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            EnergyProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            EnergyProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            EnergyProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            EnergyProjectile.Sprite.CurrentAnimation = "default";
            EnergyProjectile.Position = IconPosition;
            EnergyProjectile.IsMoving = false;

            ExplosiveProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ExplosiveProjectile");
            ExplosiveProjectile = new MobileSprite(ExplosiveProjectileTexture);
            ExplosiveProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ExplosiveProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ExplosiveProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ExplosiveProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ExplosiveProjectile.Sprite.CurrentAnimation = "default";
            ExplosiveProjectile.Position = IconPosition;
            ExplosiveProjectile.IsMoving = false;

            ExtraLifeTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ExtraLife");
            ExtraLife1 = new MobileSprite(ExtraLifeTexture);
            ExtraLife1.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ExtraLife1.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ExtraLife1.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ExtraLife1.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ExtraLife1.Sprite.CurrentAnimation = "default";
            ExtraLife1.Position = IconPosition;
            ExtraLife1.IsMoving = false;
            ExtraLife2 = new MobileSprite(ExtraLifeTexture);
            ExtraLife2.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ExtraLife2.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ExtraLife2.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ExtraLife2.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ExtraLife2.Sprite.CurrentAnimation = "default";
            ExtraLife2.Position = IconPosition;
            ExtraLife2.IsMoving = false;
            ExtraLife3 = new MobileSprite(ExtraLifeTexture);
            ExtraLife3.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ExtraLife3.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ExtraLife3.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ExtraLife3.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ExtraLife3.Sprite.CurrentAnimation = "default";
            ExtraLife3.Position = IconPosition;
            ExtraLife3.IsMoving = false;
            ExtraLife4 = new MobileSprite(ExtraLifeTexture);
            ExtraLife4.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ExtraLife4.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ExtraLife4.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ExtraLife4.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ExtraLife4.Sprite.CurrentAnimation = "default";
            ExtraLife4.Position = IconPosition;
            ExtraLife4.IsMoving = false;

            FireProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//FireProjectile");
            FireProjectile = new MobileSprite(FireProjectileTexture);
            FireProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            FireProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            FireProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            FireProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            FireProjectile.Sprite.CurrentAnimation = "default";
            FireProjectile.Position = IconPosition;
            FireProjectile.IsMoving = false;

            FireRateTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//FireRate");
            FireRate = new MobileSprite(FireRateTexture);
            FireRate.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            FireRate.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            FireRate.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            FireRate.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            FireRate.Sprite.CurrentAnimation = "default";
            FireRate.Position = IconPosition;
            FireRate.IsMoving = false;

            HealingSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//HealingSpecial");
            HealingSpecial = new MobileSprite(HealingSpecialTexture);
            HealingSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            HealingSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            HealingSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            HealingSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            HealingSpecial.Sprite.CurrentAnimation = "default";
            HealingSpecial.Position = IconPosition;
            HealingSpecial.IsMoving = false;

            HealthTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//Health");
            Health = new MobileSprite(HealthTexture);
            Health.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            Health.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            Health.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            Health.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            Health.Sprite.CurrentAnimation = "default";
            Health.Position = IconPosition;
            Health.IsMoving = false;

            HealthProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//HealthProjectile");
            HealthProjectile = new MobileSprite(HealthProjectileTexture);
            HealthProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            HealthProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            HealthProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            HealthProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            HealthProjectile.Sprite.CurrentAnimation = "default";
            HealthProjectile.Position = IconPosition;
            HealthProjectile.IsMoving = false;

            LaserProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//LaserProjectile");
            LaserProjectile = new MobileSprite(LaserProjectileTexture);
            LaserProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            LaserProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            LaserProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            LaserProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            LaserProjectile.Sprite.CurrentAnimation = "default";
            LaserProjectile.Position = IconPosition;
            LaserProjectile.IsMoving = false;

            LaserSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//LaserSpecial");
            LaserSpecial = new MobileSprite(LaserSpecialTexture);
            LaserSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            LaserSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            LaserSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            LaserSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            LaserSpecial.Sprite.CurrentAnimation = "default";
            LaserSpecial.Position = IconPosition;
            LaserSpecial.IsMoving = false;

            MoneySpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//MoneySpecial");
            MoneySpecial = new MobileSprite(MoneySpecialTexture);
            MoneySpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            MoneySpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            MoneySpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            MoneySpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            MoneySpecial.Sprite.CurrentAnimation = "default";
            MoneySpecial.Position = IconPosition;
            MoneySpecial.IsMoving = false;

            MovementSpeedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//MovementSpeed");
            MovementSpeed = new MobileSprite(MovementSpeedTexture);
            MovementSpeed.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            MovementSpeed.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            MovementSpeed.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            MovementSpeed.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            MovementSpeed.Sprite.CurrentAnimation = "default";
            MovementSpeed.Position = IconPosition;
            MovementSpeed.IsMoving = false;

            PoisonProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//PoisonProjectile");
            PoisonProjectile = new MobileSprite(PoisonProjectileTexture);
            PoisonProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            PoisonProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            PoisonProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            PoisonProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            PoisonProjectile.Sprite.CurrentAnimation = "default";
            PoisonProjectile.Position = IconPosition;
            PoisonProjectile.IsMoving = false;

            QuadShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//QuadShot");
            QuadShot = new MobileSprite(QuadShotTexture);
            QuadShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            QuadShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            QuadShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            QuadShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            QuadShot.Sprite.CurrentAnimation = "default";
            QuadShot.Position = IconPosition;
            QuadShot.IsMoving = false;

            QuintupleShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//QuintupleShot");
            QuintupleShot = new MobileSprite(QuintupleShotTexture);
            QuintupleShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            QuintupleShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            QuintupleShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            QuintupleShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            QuintupleShot.Sprite.CurrentAnimation = "default";
            QuintupleShot.Position = IconPosition;
            QuintupleShot.IsMoving = false;

            ShieldSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//ShieldSpecial");
            ShieldSpecial = new MobileSprite(ShieldSpecialTexture);
            ShieldSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            ShieldSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            ShieldSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            ShieldSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            ShieldSpecial.Sprite.CurrentAnimation = "default";
            ShieldSpecial.Position = IconPosition;
            ShieldSpecial.IsMoving = false;

            SlowProjectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//SlowProjectile");
            SlowProjectile = new MobileSprite(SlowProjectileTexture);
            SlowProjectile.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            SlowProjectile.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            SlowProjectile.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            SlowProjectile.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            SlowProjectile.Sprite.CurrentAnimation = "default";
            SlowProjectile.Position = IconPosition;
            SlowProjectile.IsMoving = false;

            TimeStopSpecialTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//TimeStopSpecial");
            TimeStopSpecial = new MobileSprite(TimeStopSpecialTexture);
            TimeStopSpecial.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            TimeStopSpecial.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            TimeStopSpecial.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            TimeStopSpecial.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            TimeStopSpecial.Sprite.CurrentAnimation = "default";
            TimeStopSpecial.Position = IconPosition;
            TimeStopSpecial.IsMoving = false;

            TripleShotTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//UpgradeIcons//TripleShot");
            TripleShot = new MobileSprite(TripleShotTexture);
            TripleShot.Sprite.AddAnimation("complete", 0, 0, 64, 64, 1, 10f);
            TripleShot.Sprite.AddAnimation("purchased", 0, 64, 64, 64, 1, 10f);
            TripleShot.Sprite.AddAnimation("available", 0, 128, 64, 64, 1, 10f);
            TripleShot.Sprite.AddAnimation("default", 0, 192, 64, 64, 1, 10f);
            TripleShot.Sprite.CurrentAnimation = "default";
            TripleShot.Position = IconPosition;
            TripleShot.IsMoving = false;
        }

        private void LoadShips()
        {
            Ship1Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship1");
            Ship1 = new MobileSprite(Ship1Texture);
            Ship1.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship1.Sprite.Tint = Color.White;
            Ship1.Sprite.CurrentAnimation = "default";
            Ship1.Position = IconPosition;
            Ship1.Sprite.Scale(0.75f);
            Ship1.IsMoving = false;
            Ship1.IsActive = true;

            Ship2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship2");
            Ship2 = new MobileSprite(Ship2Texture);
            Ship2.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship2.Sprite.Tint = Color.White;
            Ship2.Sprite.CurrentAnimation = "default";
            Ship2.Position = IconPosition;
            Ship2.Sprite.Scale(0.75f);
            Ship2.IsMoving = false;
            Ship2.IsActive = true;

            Ship3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship3");
            Ship3 = new MobileSprite(Ship3Texture);
            Ship3.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship3.Sprite.Tint = Color.White;
            Ship3.Sprite.CurrentAnimation = "default";
            Ship3.Position = IconPosition;
            Ship3.Sprite.Scale(0.75f);
            Ship3.IsMoving = false;
            Ship3.IsActive = true;

            Ship4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship4");
            Ship4 = new MobileSprite(Ship4Texture);
            Ship4.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship4.Sprite.Tint = Color.White;
            Ship4.Sprite.CurrentAnimation = "default";
            Ship4.Position = IconPosition;
            Ship4.Sprite.Scale(0.75f);
            Ship4.IsMoving = false;
            Ship4.IsActive = true;

            Ship5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship5");
            Ship5 = new MobileSprite(Ship5Texture);
            Ship5.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship5.Sprite.Tint = Color.White;
            Ship5.Sprite.CurrentAnimation = "default";
            Ship5.Position = IconPosition;
            Ship5.Sprite.Scale(0.75f);
            Ship5.IsMoving = false;
            Ship5.IsActive = true;

            Ship6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship6");
            Ship6 = new MobileSprite(Ship6Texture);
            Ship6.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship6.Sprite.Tint = Color.White;
            Ship6.Sprite.CurrentAnimation = "default";
            Ship6.Position = IconPosition;
            Ship6.Sprite.Scale(0.75f);
            Ship6.IsMoving = false;
            Ship6.IsActive = true;

            Ship7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship7");
            Ship7 = new MobileSprite(Ship7Texture);
            Ship7.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship7.Sprite.Tint = Color.White;
            Ship7.Sprite.CurrentAnimation = "default";
            Ship7.Position = IconPosition;
            Ship7.Sprite.Scale(0.75f);
            Ship7.IsMoving = false;
            Ship7.IsActive = true;

            Ship8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship8");
            Ship8 = new MobileSprite(Ship8Texture);
            Ship8.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship8.Sprite.Tint = Color.White;
            Ship8.Sprite.CurrentAnimation = "default";
            Ship8.Position = IconPosition;
            Ship8.Sprite.Scale(0.75f);
            Ship8.IsMoving = false;
            Ship8.IsActive = true;

            Ship9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship9");
            Ship9 = new MobileSprite(Ship9Texture);
            Ship9.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship9.Sprite.Tint = Color.White;
            Ship9.Sprite.CurrentAnimation = "default";
            Ship9.Position = IconPosition;
            Ship9.Sprite.Scale(0.75f);
            Ship9.IsMoving = false;
            Ship9.IsActive = true;

            Ship10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship10");
            Ship10 = new MobileSprite(Ship10Texture);
            Ship10.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship10.Sprite.Tint = Color.White;
            Ship10.Sprite.CurrentAnimation = "default";
            Ship10.Position = IconPosition;
            Ship10.Sprite.Scale(0.75f);
            Ship10.IsMoving = false;
            Ship10.IsActive = true;

            Ship11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship11");
            Ship11 = new MobileSprite(Ship11Texture);
            Ship11.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship11.Sprite.Tint = Color.White;
            Ship11.Sprite.CurrentAnimation = "default";
            Ship11.Position = IconPosition;
            Ship11.Sprite.Scale(0.75f);
            Ship11.IsMoving = false;
            Ship11.IsActive = true;

            Ship12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship12");
            Ship12 = new MobileSprite(Ship12Texture);
            Ship12.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship12.Sprite.Tint = Color.White;
            Ship12.Sprite.CurrentAnimation = "default";
            Ship12.Position = IconPosition;
            Ship12.Sprite.Scale(0.75f);
            Ship12.IsMoving = false;
            Ship12.IsActive = true;

            Ship13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship13");
            Ship13 = new MobileSprite(Ship13Texture);
            Ship13.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship13.Sprite.Tint = Color.White;
            Ship13.Sprite.CurrentAnimation = "default";
            Ship13.Position = IconPosition;
            Ship13.Sprite.Scale(0.75f);
            Ship13.IsMoving = false;
            Ship13.IsActive = true;

            Ship14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship14");
            Ship14 = new MobileSprite(Ship14Texture);
            Ship14.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship14.Sprite.Tint = Color.White;
            Ship14.Sprite.CurrentAnimation = "default";
            Ship14.Position = IconPosition;
            Ship14.Sprite.Scale(0.75f);
            Ship14.IsMoving = false;
            Ship14.IsActive = true;

            Ship15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship15");
            Ship15 = new MobileSprite(Ship15Texture);
            Ship15.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship15.Sprite.Tint = Color.White;
            Ship15.Sprite.CurrentAnimation = "default";
            Ship15.Position = IconPosition;
            Ship15.Sprite.Scale(0.75f);
            Ship15.IsMoving = false;
            Ship15.IsActive = true;

            Ship16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship16");
            Ship16 = new MobileSprite(Ship16Texture);
            Ship16.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship16.Sprite.Tint = Color.White;
            Ship16.Sprite.CurrentAnimation = "default";
            Ship16.Position = IconPosition;
            Ship16.Sprite.Scale(0.75f);
            Ship16.IsMoving = false;
            Ship16.IsActive = true;

            Ship17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship17");
            Ship17 = new MobileSprite(Ship17Texture);
            Ship17.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship17.Sprite.Tint = Color.White;
            Ship17.Sprite.CurrentAnimation = "default";
            Ship17.Position = IconPosition;
            Ship17.Sprite.Scale(0.75f);
            Ship17.IsMoving = false;
            Ship17.IsActive = true;

            Ship18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship18");
            Ship18 = new MobileSprite(Ship18Texture);
            Ship18.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship18.Sprite.Tint = Color.White;
            Ship18.Sprite.CurrentAnimation = "default";
            Ship18.Position = IconPosition;
            Ship18.Sprite.Scale(0.75f);
            Ship18.IsMoving = false;
            Ship18.IsActive = true;

            Ship19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship19");
            Ship19 = new MobileSprite(Ship19Texture);
            Ship19.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship19.Sprite.Tint = Color.White;
            Ship19.Sprite.CurrentAnimation = "default";
            Ship19.Position = IconPosition;
            Ship19.Sprite.Scale(0.75f);
            Ship19.IsMoving = false;
            Ship19.IsActive = true;

            Ship20Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship20");
            Ship20 = new MobileSprite(Ship20Texture);
            Ship20.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
            Ship20.Sprite.Tint = Color.White;
            Ship20.Sprite.CurrentAnimation = "default";
            Ship20.Position = IconPosition;
            Ship20.Sprite.Scale(0.75f);
            Ship20.IsMoving = false;
            Ship20.IsActive = true;
        }

        private void LoadHulls()
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

            Hull2Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull2");
            Hull2 = new MobileSprite(Hull2Texture);
            Hull2.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull2.Sprite.Tint = Color.White;
            Hull2.Sprite.CurrentAnimation = "default";
            Hull2.Position = Ship2.Position;
            Hull2.Sprite.Scale(0.75f);
            Hull2.IsMoving = false;
            Hull2.IsActive = true;

            Hull3Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull3");
            Hull3 = new MobileSprite(Hull3Texture);
            Hull3.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull3.Sprite.Tint = Color.White;
            Hull3.Sprite.CurrentAnimation = "default";
            Hull3.Position = Ship3.Position;
            Hull3.Sprite.Scale(0.75f);
            Hull3.IsMoving = false;
            Hull3.IsActive = true;

            Hull4Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull4");
            Hull4 = new MobileSprite(Hull4Texture);
            Hull4.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull4.Sprite.Tint = Color.White;
            Hull4.Sprite.CurrentAnimation = "default";
            Hull4.Position = Ship4.Position;
            Hull4.Sprite.Scale(0.75f);
            Hull4.IsMoving = false;
            Hull4.IsActive = true;

            Hull5Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull5");
            Hull5 = new MobileSprite(Hull5Texture);
            Hull5.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull5.Sprite.Tint = Color.White;
            Hull5.Sprite.CurrentAnimation = "default";
            Hull5.Position = Ship5.Position;
            Hull5.Sprite.Scale(0.75f);
            Hull5.IsMoving = false;
            Hull5.IsActive = true;

            Hull6Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull6");
            Hull6 = new MobileSprite(Hull6Texture);
            Hull6.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull6.Sprite.Tint = Color.White;
            Hull6.Sprite.CurrentAnimation = "default";
            Hull6.Position = Ship6.Position;
            Hull6.Sprite.Scale(0.75f);
            Hull6.IsMoving = false;
            Hull6.IsActive = true;

            Hull7Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull7");
            Hull7 = new MobileSprite(Hull7Texture);
            Hull7.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull7.Sprite.Tint = Color.White;
            Hull7.Sprite.CurrentAnimation = "default";
            Hull7.Position = Ship7.Position;
            Hull7.Sprite.Scale(0.75f);
            Hull7.IsMoving = false;
            Hull7.IsActive = true;

            Hull8Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull8");
            Hull8 = new MobileSprite(Hull8Texture);
            Hull8.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull8.Sprite.Tint = Color.White;
            Hull8.Sprite.CurrentAnimation = "default";
            Hull8.Position = Ship8.Position;
            Hull8.Sprite.Scale(0.75f);
            Hull8.IsMoving = false;
            Hull8.IsActive = true;

            Hull9Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull9");
            Hull9 = new MobileSprite(Hull9Texture);
            Hull9.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull9.Sprite.Tint = Color.White;
            Hull9.Sprite.CurrentAnimation = "default";
            Hull9.Position = Ship9.Position;
            Hull9.Sprite.Scale(0.75f);
            Hull9.IsMoving = false;
            Hull9.IsActive = true;

            Hull10Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull10");
            Hull10 = new MobileSprite(Hull10Texture);
            Hull10.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull10.Sprite.Tint = Color.White;
            Hull10.Sprite.CurrentAnimation = "default";
            Hull10.Position = Ship10.Position;
            Hull10.Sprite.Scale(0.75f);
            Hull10.IsMoving = false;
            Hull10.IsActive = true;

            Hull11Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull11");
            Hull11 = new MobileSprite(Hull11Texture);
            Hull11.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull11.Sprite.Tint = Color.White;
            Hull11.Sprite.CurrentAnimation = "default";
            Hull11.Position = Ship11.Position;
            Hull11.Sprite.Scale(0.75f);
            Hull11.IsMoving = false;
            Hull11.IsActive = true;

            Hull12Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull12");
            Hull12 = new MobileSprite(Hull12Texture);
            Hull12.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull12.Sprite.Tint = Color.White;
            Hull12.Sprite.CurrentAnimation = "default";
            Hull12.Position = Ship12.Position;
            Hull12.Sprite.Scale(0.75f);
            Hull12.IsMoving = false;
            Hull12.IsActive = true;

            Hull13Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull13");
            Hull13 = new MobileSprite(Hull13Texture);
            Hull13.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull13.Sprite.Tint = Color.White;
            Hull13.Sprite.CurrentAnimation = "default";
            Hull13.Position = Ship13.Position;
            Hull13.Sprite.Scale(0.75f);
            Hull13.IsMoving = false;
            Hull13.IsActive = true;

            Hull14Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull14");
            Hull14 = new MobileSprite(Hull14Texture);
            Hull14.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull14.Sprite.Tint = Color.White;
            Hull14.Sprite.CurrentAnimation = "default";
            Hull14.Position = Ship14.Position;
            Hull14.Sprite.Scale(0.75f);
            Hull14.IsMoving = false;
            Hull14.IsActive = true;

            Hull15Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull15");
            Hull15 = new MobileSprite(Hull15Texture);
            Hull15.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull15.Sprite.Tint = Color.White;
            Hull15.Sprite.CurrentAnimation = "default";
            Hull15.Position = Ship15.Position;
            Hull15.Sprite.Scale(0.75f);
            Hull15.IsMoving = false;
            Hull15.IsActive = true;

            Hull16Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull16");
            Hull16 = new MobileSprite(Hull16Texture);
            Hull16.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull16.Sprite.Tint = Color.White;
            Hull16.Sprite.CurrentAnimation = "default";
            Hull16.Position = Ship16.Position;
            Hull16.Sprite.Scale(0.75f);
            Hull16.IsMoving = false;
            Hull16.IsActive = true;

            Hull17Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull17");
            Hull17 = new MobileSprite(Hull17Texture);
            Hull17.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull17.Sprite.Tint = Color.White;
            Hull17.Sprite.CurrentAnimation = "default";
            Hull17.Position = Ship17.Position;
            Hull17.Sprite.Scale(0.75f);
            Hull17.IsMoving = false;
            Hull17.IsActive = true;

            Hull18Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull18");
            Hull18 = new MobileSprite(Hull18Texture);
            Hull18.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull18.Sprite.Tint = Color.White;
            Hull18.Sprite.CurrentAnimation = "default";
            Hull18.Position = Ship18.Position;
            Hull18.Sprite.Scale(0.75f);
            Hull18.IsMoving = false;
            Hull18.IsActive = true;

            Hull19Texture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull19");
            Hull19 = new MobileSprite(Hull19Texture);
            Hull19.Sprite.AddAnimation("default", 0, 0, 64, 64, 13, 0.05f);
            Hull19.Sprite.Tint = Color.White;
            Hull19.Sprite.CurrentAnimation = "default";
            Hull19.Position = Ship19.Position;
            Hull19.Sprite.Scale(0.75f);
            Hull19.IsMoving = false;
            Hull19.IsActive = true;

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

        public void Update(GameTime gameTime, Vector2 iconPosition)
        {
            IconPosition = iconPosition;

            keyboardUpdate.Update(gameTime);

            Resume = false;

            if (keyboardUpdate.SelectLeft)
                MenuXLocation -= 1;
            if (keyboardUpdate.SelectRight)
                MenuXLocation += 1;
            if (keyboardUpdate.SelectUp)
                MenuYLocation -= 1;
            if (keyboardUpdate.SelectDown)
                MenuYLocation += 1;

            if (InitializeSystem && keyboardUpdate.Back == false)
            {
                InitializeSystem = false;
            }

            #region Menu 0

            if (MenuState == 0 && InitializeSystem == false)
            {
                ResumeLocation = new Vector2((IconPosition.X + 32) - (font.MeasureString("Resume").X / 2), IconPosition.Y);
                InventoryLocation = new Vector2((IconPosition.X + 32) - (font.MeasureString("Inventory").X / 2), IconPosition.Y + 20);
                UpgradeLocation = new Vector2((IconPosition.X + 32) - (font.MeasureString("Upgrades").X / 2), IconPosition.Y + 40);
                DropOutLocation = new Vector2((IconPosition.X + 32) - (font.MeasureString("Drop Out").X / 2), IconPosition.Y + 60);

                if (keyboardUpdate.Back)
                {
                    Resume = true;
                    InitializeSystem = true;
                }

                if (MenuYLocation == 1)
                {
                    if (keyboardUpdate.Select)
                    {
                        Resume = true;
                        InitializeSystem = true;
                    }

                    ResumeColor = Color.Red;
                }
                else
                    ResumeColor = Color.White;

                if (MenuYLocation == 2)
                {
                    if (keyboardUpdate.Select)
                        MenuState = 1;

                    InventoryColor = Color.Red;
                }
                else
                    InventoryColor = Color.White;

                if (MenuYLocation == 3)
                {
                    if (keyboardUpdate.Select)
                        MenuState = 2;

                    UpgradeColor = Color.Red;
                }
                else
                    UpgradeColor = Color.White;

                if (MenuYLocation == 4)
                {
                    if (keyboardUpdate.Select)
                        DropOut = true;

                    DropOutColor = Color.Red;
                }
                else
                    DropOutColor = Color.White;

                if (MenuYLocation > 4)
                    MenuYLocation = 1;
                if (MenuYLocation < 1)
                    MenuYLocation = 4;
            }

            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (MenuState == 0)
            {
                spriteBatch.DrawString(font, "Resume", ResumeLocation, ResumeColor);
                spriteBatch.DrawString(font, "Inventory", InventoryLocation, InventoryColor);
                spriteBatch.DrawString(font, "Upgrades", UpgradeLocation, UpgradeColor);
                spriteBatch.DrawString(font, "Drop Out", DropOutLocation, DropOutColor);
            }
        }
    }
}
