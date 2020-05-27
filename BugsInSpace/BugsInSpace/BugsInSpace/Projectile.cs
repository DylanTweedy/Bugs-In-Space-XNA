using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BugsInSpace
{
    class Projectile
    {
        #region Variables

        public MobileSprite projectile;
        Texture2D projectileTexture;
        public Vector2 position;
        public bool active;
        public int damage;
        float moveSpeedVertical;
        float moveSpeedHorizontal;
        int projectileType;
        int fireStyle;
        int shooter;
        Random random;
        bool Spin;
        public string hitSound;
        string fireSound;
        bool Unfired;
        public ParticleEngine particleEngine;
        public int projectileLevel;
        public int stunChance;
        public float stunTime;
        int ExtraDamage;
        float ExtraVerticalSpeed;
        float ExtraHorizontalSpeed;
        int ProjectileLength;
        public bool Piercing;
        public int ElementType;
        public int BurnChance;
        public float BurnTime;
        public int BurnDamage;
        Vector2 velocity;
        public float PoisonTime;
        public int PoisonDamage;
        public int radius;
        public int SlowChance;
        public float SlowTime;
        public bool homing;
        public bool Healing;
        public bool Energy;
        public Rectangle RectangleRadius;
        public bool Explosion;
        public ParticleEngine ExplosionParticle;
        public bool ExplodeDamage;
        TimeSpan HomingTime;
        TimeSpan PreviousHomingTime;
        TimeSpan MovementTime;
        TimeSpan PreviousMovementTime;
        bool Left;
        bool Right;
        bool Up;
        bool Down;
        bool InitializeHoming;
        bool Unexploded;
        public int ProjectileCount;
        public bool HitEnemy = false;
        public int MoneyValue;
        bool AddParticleVelocity;
        float BonusSpeed;
        bool IndividualSpeed;
        public bool Locked;
        int PassCounter;
        public bool InitializeProjectile;
        int SpeedPercentageX;
        int SpeedPercentageY;

        #endregion

        #region Properties

        public int ScreenWidth
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Width; }
        }
        public int ScreenHeight
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Height; }
        }
        int PositionX
        {
            get { return (int)projectile.Sprite.Position.X; }
        }

        int PositionY
        {
            get { return (int)projectile.Sprite.Position.Y; }
        }

        #endregion

        public void Initialize(int ProjectileType, Vector2 Position, int Damage, float VerticalSpeed, float HorizontalSpeed, int FireStyle, int Shooter, Vector2 Velocity)
        {
            projectileType = ProjectileType;
            position = Position;
            active = true;
            fireStyle = FireStyle;
            shooter = Shooter;
            random = new Random();
            Unfired = true;
            projectileLevel = 10;
            stunChance = 0;
            stunTime = 0;
            ExtraDamage = Damage;
            ExtraHorizontalSpeed = HorizontalSpeed;
            ExtraVerticalSpeed = VerticalSpeed;
            ProjectileLength = 0;
            Piercing = false;
            BurnChance = 0;
            BurnTime = 0;
            BurnDamage = 0;
            velocity = Velocity;
            PoisonDamage = 0;
            PoisonTime = 0;
            radius = 0;
            SlowChance = 0;
            SlowTime = 0;
            homing = false;
            Healing = false;
            Energy = false;
            RectangleRadius = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            Explosion = false;
            PreviousHomingTime = TimeSpan.Zero;
            HomingTime = TimeSpan.FromSeconds(5f / (VerticalSpeed + 0.01f));
            PreviousMovementTime = TimeSpan.Zero;
            MovementTime = TimeSpan.FromSeconds(0.3f);
            Up = false;
            Down = false;
            Left = false;
            Right = false;
            InitializeHoming = true;
            Unexploded = true;
            ProjectileCount = 0;
            MoneyValue = 0;
            AddParticleVelocity = false;
            BonusSpeed = 0;
            IndividualSpeed = false;
            PassCounter = 0;
            InitializeProjectile = true;

            if (fireStyle == 0)
                position = position + (velocity * 5);
        }

        private void InitializeProjectiles(int damageModifier, float verticalSpeed, float horizontalSpeed)
        {
            #region Projectile 0

            if (projectileType == 0)
            {
                damage = 0;
                BonusSpeed = 0.2f;
                ElementType = 1;
                Spin = true;
                fireSound = "CrystalFire";
                hitSound = "CrystalHit";
                particleEngine = new ParticleEngine(7, Color.White, position);
                AddParticleVelocity = true;
            }

            #endregion

            #region Projectile 1

            if (projectileType == 1)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 5;
                    BonusSpeed = 1f;
                    stunChance = 5;
                    stunTime = 1f;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 10;
                    BonusSpeed = 1.2f;
                    stunChance = 9;
                    stunTime = 1.1f;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 20;
                    BonusSpeed = 1.4f;
                    stunChance = 14;
                    stunTime = 1.2f;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 40;
                    BonusSpeed = 1.6f;
                    stunChance = 17;
                    stunTime = 1.3f;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 100;
                    BonusSpeed = 1.8f;
                    stunChance = 20;
                    stunTime = 1.4f;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 150;
                    BonusSpeed = 2f;
                    stunChance = 23;
                    stunTime = 1.5f;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 200;
                    BonusSpeed = 2.2f;
                    stunChance = 26;
                    stunTime = 1.6f;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 300;
                    BonusSpeed = 2.4f;
                    stunChance = 29;
                    stunTime = 1.7f;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 400;
                    BonusSpeed = 2.7f;
                    stunChance = 31;
                    stunTime = 1.8f;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 500;
                    BonusSpeed = 3f;
                    stunChance = 33;
                    stunTime = 1.9f;
                }

                ElementType = 4;
                Spin = false;
                fireSound = "ElectricProjectile";
                hitSound = "ElectricProjectileHit";
                particleEngine = new ParticleEngine(4, Color.Yellow, position);
            }

            #endregion

            #region Projectile 2

            if (projectileType == 2)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 3;
                    BonusSpeed = 3f;
                    ProjectileLength = 10;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 6;
                    BonusSpeed = 5f;
                    ProjectileLength = 15;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 9;
                    BonusSpeed = 7f;
                    ProjectileLength = 20;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 15;
                    BonusSpeed = 9f;
                    ProjectileLength = 25;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 30;
                    BonusSpeed = 11f;
                    ProjectileLength = 30;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 45;
                    BonusSpeed = 13f;
                    ProjectileLength = 35;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 75;
                    BonusSpeed = 15f;
                    ProjectileLength = 40;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 100;
                    BonusSpeed = 17f;
                    ProjectileLength = 43;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 125;
                    BonusSpeed = 20f;
                    ProjectileLength = 46;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 150;
                    BonusSpeed = 25f;
                    ProjectileLength = 50;
                }

                ElementType = 2;
                Piercing = true;
                Spin = false;
                fireSound = "LaserProjectile";
                hitSound = "LaserProjectileHit";
                particleEngine = new ParticleEngine(0, new Color(255,200,200), position);
            }

            #endregion

            #region Projectile 3

            if (projectileType == 3)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 3;
                    BurnChance = 5;
                    BurnTime = 1;
                    BurnDamage = 1;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 6;
                    BurnChance = 9;
                    BurnTime = 2;
                    BurnDamage = 3;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 10;
                    BurnChance = 14;
                    BurnTime = 3;
                    BurnDamage = 5;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 20;
                    BurnChance = 17;
                    BurnTime = 4;
                    BurnDamage = 10;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 50;
                    BurnChance = 20;
                    BurnTime = 5;
                    BurnDamage = 20;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 100;
                    BurnChance = 23;
                    BurnTime = 6;
                    BurnDamage = 30;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 150;
                    BurnChance = 26;
                    BurnTime = 7;
                    BurnDamage = 50;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 200;
                    BurnChance = 29;
                    BurnTime = 8;
                    BurnDamage = 75;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 300;
                    BurnChance = 31;
                    BurnTime = 9;
                    BurnDamage = 100;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 400;
                    BurnChance = 33;
                    BurnTime = 10;
                    BurnDamage = 125;
                }
                BonusSpeed = 2f;
                ElementType = 6;
                Spin = true;
                fireSound = "FireProjectile";
                hitSound = "FireProjectileHit";
                particleEngine = new ParticleEngine(5, Color.Orange, position);
            }

            #endregion

            #region Projectile 4

            if (projectileType == 4)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 2;
                    PoisonTime = 2f;
                    PoisonDamage = 1;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 6;
                    PoisonTime = 4f;
                    PoisonDamage = 3;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 10;
                    PoisonTime = 6f;
                    PoisonDamage = 5;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 15;
                    PoisonTime = 8f;
                    PoisonDamage = 10;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 30;
                    PoisonTime = 10f;
                    PoisonDamage = 20;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 60;
                    PoisonTime = 12f;
                    PoisonDamage = 30;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 100;
                    PoisonTime = 14f;
                    PoisonDamage = 50;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 150;
                    PoisonTime = 16f;
                    PoisonDamage = 75;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 200;
                    PoisonTime = 18f;
                    PoisonDamage = 100;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 300;
                    PoisonTime = 20f;
                    PoisonDamage = 150;
                }

                BonusSpeed = 2f;
                ElementType = 7;
                Spin = true;
                fireSound = "PoisonProjectile";
                hitSound = "PoisonProjectileHit";
                particleEngine = new ParticleEngine(3, Color.Green, position);
                AddParticleVelocity = true;
            }

            #endregion

            #region Projectile 5

            if (projectileType == 5)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 10;
                    BonusSpeed = 1f;
                    radius = 30;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 25;
                    BonusSpeed = 1.2f;
                    radius = 35;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 50;
                    BonusSpeed = 1.5f;
                    radius = 40;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 75;
                    BonusSpeed = 1.7f;
                    radius = 45;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 100;
                    BonusSpeed = 2f;
                    radius = 50;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 150;
                    BonusSpeed = 2.2f;
                    radius = 55;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 200;
                    BonusSpeed = 2.5f;
                    radius = 60;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 350;
                    BonusSpeed = 2.7f;
                    radius = 65;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 500;
                    BonusSpeed = 3f;
                    radius = 70;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 750;
                    BonusSpeed = 3.2f;
                    radius = 75;
                }

                ElementType = 9;
                Spin = true;
                fireSound = "ExplosiveProjectile";
                hitSound = "ExplosiveProjectileHit";
                particleEngine = new ParticleEngine(0, Color.White, position);
            }

            #endregion

            #region Projectile 6

            if (projectileType == 6)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 5;
                    BonusSpeed = 1f;
                    SlowChance = 10;
                    SlowTime = 2f;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 10;
                    BonusSpeed = 1.2f;
                    SlowChance = 18;
                    SlowTime = 2.2f;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 25;
                    BonusSpeed = 1.4f;
                    SlowChance = 28;
                    SlowTime = 2.4f;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 50;
                    BonusSpeed = 1.6f;
                    SlowChance = 34;
                    SlowTime = 2.6f;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 100;
                    BonusSpeed = 1.8f;
                    SlowChance = 40;
                    SlowTime = 2.8f;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 150;
                    BonusSpeed = 2f;
                    SlowChance = 46;
                    SlowTime = 3f;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 200;
                    BonusSpeed = 2.2f;
                    SlowChance = 52;
                    SlowTime = 3.2f;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 300;
                    BonusSpeed = 2.4f;
                    SlowChance = 56;
                    SlowTime = 3.4f;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 400;
                    BonusSpeed = 2.6f;
                    SlowChance = 62;
                    SlowTime = 3.6f;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 500;
                    BonusSpeed = 2.8f;
                    SlowChance = 66;
                    SlowTime = 3.8f;
                }

                ElementType = 10;
                Spin = false;
                fireSound = "SlowProjectile";
                hitSound = "SlowProjectileHit";
                particleEngine = new ParticleEngine(3, Color.LightBlue, position);
                particleEngine.scale = 0.5f;
            }

            #endregion

            #region Projectile 7

            if (projectileType == 7)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 6;
                    BonusSpeed = 2f;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 12;
                    BonusSpeed = 4f;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 18;
                    BonusSpeed = 6f;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 30;
                    BonusSpeed = 8f;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 60;
                    BonusSpeed = 10f;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 90;
                    BonusSpeed = 12f;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 150;
                    BonusSpeed = 14f;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 200;
                    BonusSpeed = 16f;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 250;
                    BonusSpeed = 18f;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 300;
                    BonusSpeed = 20f;
                }

                Healing = true;
                ElementType = 8;
                Spin = true;
                fireSound = "HealthProjectile";
                hitSound = "HealthProjectileHit";
                particleEngine = new ParticleEngine(1, Color.Red, position);
            }

            #endregion

            #region Projectile 8

            if (projectileType == 8)
            {
                if (projectileLevel == 1)
                {
                    damage = damageModifier + 6;
                    BonusSpeed = 2f;
                }
                else if (projectileLevel == 2)
                {
                    damage = damageModifier + 12;
                    BonusSpeed = 4f;
                }
                else if (projectileLevel == 3)
                {
                    damage = damageModifier + 18;
                    BonusSpeed = 6f;
                }
                else if (projectileLevel == 4)
                {
                    damage = damageModifier + 30;
                    BonusSpeed = 8f;
                }
                else if (projectileLevel == 5)
                {
                    damage = damageModifier + 60;
                    BonusSpeed = 10f;
                }
                else if (projectileLevel == 6)
                {
                    damage = damageModifier + 90;
                    BonusSpeed = 12f;
                }
                else if (projectileLevel == 7)
                {
                    damage = damageModifier + 150;
                    BonusSpeed = 14f;
                }
                else if (projectileLevel == 8)
                {
                    damage = damageModifier + 200;
                    BonusSpeed = 16f;
                }
                else if (projectileLevel == 9)
                {
                    damage = damageModifier + 250;
                    BonusSpeed = 18f;
                }
                else if (projectileLevel == 10)
                {
                    damage = damageModifier + 300;
                    BonusSpeed = 20f;
                }

                Energy = true;
                ElementType = 1;
                Spin = true;
                fireSound = "EnergyProjectile";
                hitSound = "EnergyProjectileHit";
                particleEngine = new ParticleEngine(1, Color.Blue, position);
            }

            #endregion

            #region Projectile 9

            if (projectileType == 9)
            {
                damage = damageModifier + 1;
                BonusSpeed = 0.1f;

                ElementType = 1;
                Spin = false;
                fireSound = "Projectile001";
                hitSound = "Projectile001Hit";
                particleEngine = new ParticleEngine(0, Color.Red, position);
            }

            #endregion

            #region Projectile 10

            if (projectileType == 10)
            {
                damage = damageModifier + 1;
                BonusSpeed = 0.2f;
                ElementType = 1;
                Spin = true;
                fireSound = "Projectile002";
                hitSound = "Projectile002Hit";
                particleEngine = new ParticleEngine(1, Color.Blue, position);
            }

            #endregion

            #region Projectile 11

            if (projectileType == 11)
            {
                damage = damageModifier + 3;
                BonusSpeed = 5f;

                homing = true;
                IndividualSpeed = true;
                ElementType = 3;
                Spin = true;
                fireSound = "Projectile003";
                hitSound = "Projectile003Hit";
                particleEngine = new ParticleEngine(1, Color.DarkSlateGray, position);
                                AddParticleVelocity = true;
            }

            #endregion

            #region Projectile 18

            if (projectileType == 18)
            {
                damage = damageModifier + 7;
                BonusSpeed = 7f;

                homing = true;
                IndividualSpeed = true;
                radius = 7;
                ElementType = 8;
                Spin = true;
                fireSound = "Projectile010";
                hitSound = "Projectile010Hit";
                particleEngine = new ParticleEngine(5, Color.Orange, position);
            }

            #endregion
        }

        public void LoadContent()
        {
            bool ChangeHorizontalSpeed = false;
            bool ChangeVerticalSpeed = false;

            if (ExtraVerticalSpeed < 0)
            {
                ChangeVerticalSpeed = true;
                ExtraVerticalSpeed = -ExtraVerticalSpeed;
            }
            if (ExtraHorizontalSpeed < 0)
            {
                ChangeHorizontalSpeed = true;
                ExtraHorizontalSpeed = -ExtraHorizontalSpeed;
            }

            InitializeProjectiles(ExtraDamage, ExtraVerticalSpeed, ExtraHorizontalSpeed);

            if (ExtraHorizontalSpeed > ExtraVerticalSpeed)
            {
                SpeedPercentageX = (int)((double)ExtraHorizontalSpeed / (double)ExtraHorizontalSpeed);
                SpeedPercentageY = (int)((double)ExtraVerticalSpeed / (double)ExtraHorizontalSpeed);
            }
            else
            {
                SpeedPercentageX = (int)((double)ExtraHorizontalSpeed / (double)ExtraVerticalSpeed);
                SpeedPercentageY = (int)((double)ExtraVerticalSpeed / (double)ExtraVerticalSpeed);
            }

            if (Locked)
                moveSpeedHorizontal = ExtraHorizontalSpeed;
            else if (ExtraHorizontalSpeed != 0)
            {
                if (IndividualSpeed)
                    moveSpeedHorizontal = BonusSpeed * SpeedPercentageX;
                else
                    moveSpeedHorizontal = ExtraHorizontalSpeed + (BonusSpeed * SpeedPercentageX);
            }
            else
                moveSpeedHorizontal = 0;

            if (ExtraVerticalSpeed != 0)
            {
                if (IndividualSpeed)
                    moveSpeedVertical = BonusSpeed * SpeedPercentageY;
                else
                    moveSpeedVertical = ExtraVerticalSpeed + (BonusSpeed * SpeedPercentageY);
            }
            else
                moveSpeedVertical = 0;

            particleEngine.LoadContent();

            ExplosionParticle = new ParticleEngine(6, Color.DarkGoldenrod, position);
            ExplosionParticle.LoadContent();
            ExplosionParticle.AddParticles = false;
            ExplosionParticle.scale = 2f;

            if (projectileType == 0)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//Crystal");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 14, 14, 1, 0.05f);
            }

            if (projectileType == 1)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//ElectricProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 7, 15, 2, 0.1f);
            }

            if (projectileType == 2)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//LaserProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 3, ProjectileLength, 3, 0.01f);
            }

            if (projectileType == 3)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//FireProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 24, 24, 1, 10f);
            }

            if (projectileType == 4)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//PoisonProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 24, 24, 1, 10f);
            }

            if (projectileType == 5)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//ExplosiveProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 12, 13, 1, 10f);
            }

            if (projectileType == 6)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//SlowProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 7, 12, 1, 10f);
            }

            if (projectileType == 7)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//HealthProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 10, 10, 1, 10f);
            }

            if (projectileType == 8)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//EnergyProjectile");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 10, 10, 1, 10f);
            }

            if (projectileType == 9)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//Projectile001");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 6, 8, 1, 10f);
            }

            if (projectileType == 10)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//Projectile002");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 6, 8, 1, 10f);
            }

            if (projectileType == 11)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//Projectile003");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 12, 12, 2, 0.1f);
                projectile.Sprite.AutoRotate = true;
            }

            if (projectileType == 18)
            {
                projectileTexture = Game1.Instance.Content.Load<Texture2D>("Images//Projectiles//Projectile010");
                projectile = new MobileSprite(projectileTexture);
                projectile.Sprite.AddAnimation("default", 0, 0, 10, 16, 1, 0.1f);
                projectile.Sprite.AutoRotate = true;
                particleEngine.scale = 0.2f;
            }

            if (projectileType != 0)
                projectile.Sprite.Tint = Color.White;
            else
                projectile.Sprite.Tint = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            projectile.Sprite.CurrentAnimation = "default";
            projectile.Position = position;
            projectile.IsCollidable = true;
            projectile.IsMoving = false;

            if (moveSpeedVertical != 0)
                projectile.Sprite.Physics(moveSpeedVertical, 1);
            else
                projectile.Sprite.Physics(moveSpeedHorizontal, 1);

            if (ChangeHorizontalSpeed)
                moveSpeedHorizontal = -moveSpeedHorizontal;

            if (ChangeVerticalSpeed)
                moveSpeedVertical = -moveSpeedVertical;
        }

        public void Update(GameTime gameTime, Vector2 Destination)
        {
            if (InitializeHoming)
            {
                PreviousHomingTime = gameTime.TotalGameTime;
                InitializeHoming = false;
            }

            if (active)
            {
                particleEngine.EmitterLocationUpdate(projectile.Position.X, projectile.Position.X + projectile.Sprite.Width, projectile.Position.Y, projectile.Position.Y + projectile.Sprite.Height);

                if (AddParticleVelocity)
                    particleEngine.Velocity = projectile.Sprite.velocity;

                #region fireStyle 0

                if (fireStyle == 0)
                {
                    if (Spin == false)
                        projectile.Sprite.AutoRotate = true;

                    projectile.Sprite.velocity = velocity * 5;
                    projectile.Sprite.Scale(0.7f);
                    particleEngine.scale = 0.5f;

                    if (projectile.Position.X > ScreenWidth || projectile.Position.X < 0 || projectile.Position.Y < 0 || projectile.Position.Y > ScreenHeight)
                        active = false;

                    if (Unfired)
                    {
                        Game1.Instance.AudioPlay(fireSound, 3);
                        Unfired = false;
                    }
                }

                #endregion

                else if (shooter == 1 && homing == false)
                    PlayerUpdate();
                else if (shooter == 2 && homing == false)
                    EnemyUpdate();

                #region Homing

                else if (homing)
                {
                    if (gameTime.TotalGameTime - PreviousHomingTime < HomingTime || Destination == Vector2.Zero)
                    {
                        if (shooter == 1)
                            PlayerUpdate();
                        else if (shooter == 2)
                            EnemyUpdate();
                    }
                    else
                    {
                        if (gameTime.TotalGameTime - PreviousMovementTime > MovementTime)
                        {
                            if ((Destination.X - projectile.Position.X) > 3 || (Destination.X - projectile.Position.X) < -3)
                            {
                                if (Destination.X > projectile.Position.X)
                                {
                                    if (Left)
                                    {
                                        projectile.Sprite.velocity.X = projectile.Sprite.velocity.X / 10;
                                    }
                                    Right = true;
                                    Left = false;
                                    projectile.Sprite.MoveX(moveSpeedVertical);
                                }
                                else if (Destination.X < projectile.Position.X)
                                {
                                    if (Right)
                                    {
                                        projectile.Sprite.velocity.X = projectile.Sprite.velocity.X / 10;
                                    }
                                    Left = true;
                                    Right = false;
                                    projectile.Sprite.MoveX(-moveSpeedVertical);
                                }
                            }

                            if ((Destination.Y - projectile.Position.Y) > 3 || (Destination.Y - projectile.Position.Y) < -3)
                            {
                                if (Destination.Y > projectile.Position.Y)
                                {
                                    if (Up)
                                    {
                                        projectile.Sprite.velocity.Y = projectile.Sprite.velocity.Y / 1.5f;
                                    }
                                    Down = true;
                                    Up = false;
                                    projectile.Sprite.MoveY(moveSpeedVertical);
                                }
                                else if (Destination.Y < projectile.Position.Y)
                                {
                                    if (Down)
                                    {
                                        projectile.Sprite.velocity.Y = projectile.Sprite.velocity.Y / 1.5f;
                                    }
                                    Down = false;
                                    Up = true;
                                    projectile.Sprite.MoveY(-moveSpeedVertical);
                                }
                            }

                            PreviousMovementTime = gameTime.TotalGameTime;
                        }

                        if (projectile.Position.Y > ScreenHeight || projectile.Position.Y < 0 - projectile.Sprite.Height)
                            active = false;
                    }
                }

                #endregion

                #region Explosion Radius

                RectangleRadius.X = ((int)projectile.Position.X + (projectile.Sprite.Width / 2)) - radius;
                RectangleRadius.Y = ((int)projectile.Position.Y + (projectile.Sprite.Width / 2)) - radius;
                RectangleRadius.Width = radius * 2;
                RectangleRadius.Height = radius * 2;
                ExplodeDamage = true;

                ExplosionParticle.Update();

                #endregion

                #region Boundries

                if (projectile.Position.Y > ScreenHeight)
                {
                    radius = 0;
                    Explosion = false;
                    active = false;
                }
                if (projectile.Position.Y < 0 - projectile.Sprite.Height)
                {
                    radius = 0;
                    Explosion = false;
                    active = false;
                }

                if (projectile.Position.X > ScreenWidth)
                {
                    projectile.Position = new Vector2(-49, projectile.Position.Y);
                    PassCounter += 1;
                }

                if (projectile.Position.X < 0 - 50)
                {
                    projectile.Position = new Vector2(ScreenWidth + 49, projectile.Position.Y);
                    PassCounter += 1;
                }

                if (PassCounter == 3)
                    active = false;

                if (moveSpeedVertical == 0 && moveSpeedHorizontal == 0)
                    active = false;

                #endregion

                if (Spin)
                    projectile.Sprite.Rotation += 0.34f;
                else
                    projectile.Sprite.AutoRotate = true;

                projectile.Update(gameTime);
            }

            if (particleEngine.ParticleType != 0)
                particleEngine.Update();

            #region Explode

            if (active == false)
            {
                particleEngine.AddParticles = false;

                if (Explosion)
                {
                    if (Unexploded)
                    {
                        Game1.Instance.AudioPlay("Explosion2", 4);
                        damage = damage / 2;
                        Unexploded = false;
                    }

                    RectangleRadius.X = RectangleRadius.X + 5;
                    RectangleRadius.Y = RectangleRadius.Y + 5;
                    RectangleRadius.Width = radius * 2;
                    RectangleRadius.Height = radius * 2;

                    ExplodeDamage = false;                        

                    if (radius > 0)
                    {
                        ExplosionParticle.AddParticles = true;

                        ExplosionParticle.EmitterLocationUpdate(RectangleRadius.X, RectangleRadius.X + (radius * 2), RectangleRadius.Y, RectangleRadius.Y + (radius * 2));
                        ExplosionParticle.Update();
                    }

                    ExplosionParticle.Update();

                    radius -= 5;
                }

            #endregion

                ExplosionParticle.AddParticles = false;
            }
        }

        public void PlayerUpdate()
        {
            if (fireStyle == 1)
            {
                if (Unfired)
                {
                    if (ProjectileCount < 150)
                        Game1.Instance.AudioPlay(fireSound, 3);
                    Unfired = false;
                }

                //if ((projectile.Sprite.velocity.X > moveSpeedHorizontal && moveSpeedHorizontal > 0) || (projectile.Sprite.velocity.X < moveSpeedHorizontal && moveSpeedHorizontal < 0))
                    projectile.Sprite.velocity.X = moveSpeedHorizontal;
                //else
                //    projectile.Sprite.MoveX(moveSpeedHorizontal * 10);

                //if ((projectile.Sprite.velocity.Y > moveSpeedVertical && moveSpeedVertical > 0) || (projectile.Sprite.velocity.Y < moveSpeedVertical && moveSpeedVertical < 0))
                    projectile.Sprite.velocity.Y = moveSpeedVertical;
                //else
                //    projectile.Sprite.MoveY(moveSpeedVertical * 10);
            }
            if (fireStyle == 2)
            {
                if (Unfired)
                {
                    if (ProjectileCount < 25)
                        Game1.Instance.AudioPlay(fireSound, 3);
                    Unfired = false;
                }
                projectile.Sprite.MoveY(moveSpeedVertical * 10);
                projectile.Sprite.velocity.X = (-((((int)20 / 2) / 2) / 2) + ((int)moveSpeedHorizontal / 5));
            }
            if (fireStyle == 3)
            {
                if (Unfired)
                {
                    if (ProjectileCount < 25)
                        Game1.Instance.AudioPlay(fireSound, 3);
                    Unfired = false;
                }
                projectile.Sprite.MoveY(moveSpeedVertical * 10);
                projectile.Sprite.velocity.X = (-((((int)20) / 2) / 2) + ((int)moveSpeedHorizontal / 5));
            }
            if (fireStyle == 4)
            {
                if (Unfired)
                {
                    if (ProjectileCount < 25)
                        Game1.Instance.AudioPlay(fireSound, 3);
                    Unfired = false;
                }
                projectile.Sprite.MoveY(moveSpeedVertical * 10);
                projectile.Sprite.velocity.X = (((((int)20 / 2) / 2) / 2) + ((int)moveSpeedHorizontal / 5));
            }
            if (fireStyle == 5)
            {
                if (Unfired)
                {
                    if (ProjectileCount < 25)
                        Game1.Instance.AudioPlay(fireSound, 3);
                    Unfired = false;
                }
                projectile.Sprite.MoveY(moveSpeedVertical * 10);
                projectile.Sprite.velocity.X = (((((int)20) / 2) / 2) + ((int)moveSpeedHorizontal / 5));
            }
        }

        public void EnemyUpdate()
        {
            if (fireStyle == 1)
            {
                if (Unfired)
                {
                    Game1.Instance.AudioPlay(fireSound, 3);
                    Unfired = false;
                }
                if (Spin == false)
                    projectile.Sprite.AutoRotate = true;
                projectile.Sprite.MoveY(moveSpeedVertical * 3);

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (particleEngine.ParticleType != 0)
                particleEngine.Draw(spriteBatch);
            if (active)
                projectile.Draw(spriteBatch);
            if (active == false)
                ExplosionParticle.Draw(spriteBatch);
        }
    }
}
