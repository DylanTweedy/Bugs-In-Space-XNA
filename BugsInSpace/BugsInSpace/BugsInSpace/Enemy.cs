using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class Enemy
    {
        #region Variables

        public MobileSprite EnemyBody;
        public MobileSprite EnemyWings;
        public PowerUps powerUps;
        TimeSpan MoveTime;
        TimeSpan previousMoveTime;
        TimeSpan MoveTime2;
        TimeSpan previousMoveTime2;
        Random random;
        Texture2D enemyBodyTexture;
        Texture2D enemyWingsTexture;
        public bool Active;
        public int Health;
        public int Damage;
        public int Value;
        int MaxX;
        int MinX;
        int MaxY;
        int MinY;
        int screenCenterX;
        int screenCenterY;
        public float enemyMoveSpeedVertical;
        public float enemyMoveSpeedHorizontal;
        int enemyIndex;
        public int movementPattern;
        int deathPattern;
        int screenPattern;
        bool move1;
        bool move2;
        bool dying;
        public List<Projectile> projectiles;
        TimeSpan fireTime;
        public TimeSpan previousFireTime;
        int projectileWidth;
        int projectileHeight;
        public int EnemyProjectile;
        int EnemyDamage;
        float EnemyBulletSpeedY;
        float EnemyBulletSpeedX;
        public bool isFiring;
        public int EnemyFireStyle;
        public bool hit;
        public bool beenHit;
        public bool playerPowerUp;
        public bool customMovement;
        public TimeSpan staticMoveTime;
        public TimeSpan previousStaticMoveTime;
        public float EnemyMaxSpeed;
        public float EnemyStoppingSpeed;
        public float EnemyAcceleration;
        public float scale;
        bool firing;
        float animationSpeed;
        public int PowerUpActive;
        bool PowerUpActivated;
        public int EnemyNumber;
        Vector2 enemyLocation;
        public int enemyLocationMove;
        public TimeSpan touchTime;
        public TimeSpan previousTouchTime;
        ParticleEngine particleEngine;
        public ParticleEngine deathParticles;
        TimeSpan DeathTime;
        TimeSpan previousDeathTime;
        public bool Stunned;
        public float StunTime;
        TimeSpan StunTimer;
        TimeSpan PreviousStunTimer;
        bool InitiateStun;
        ParticleEngine StunParticle;
        Color OriginalColor;
        bool Initializing;
        int minFireTime;
        int maxFireTime;
        ParticleEngine hitParticles;
        TimeSpan HitTime;
        TimeSpan previousHitTime;
        public int ElementType;
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
        public Vector2 ClosestPlayer;
        bool Up;
        bool Down;
        bool Left;
        bool Right;
        TimeSpan MovementTime;
        TimeSpan PreviousMovementTime;
        public ParticleEngine AuraParticles;
        public bool GivenCredits;
        public bool Money;
        public bool LargeCrystals;
        public bool FirePowerUps;
        public float MoneyValueMultiplier;
        public bool TimeStop;

        #endregion

        #region Properties

        public int Width
        {
            get { return EnemyBody.Sprite.Width; }
        }

        public int Height
        {
            get { return EnemyBody.Sprite.Height; }
        }

        public int screenWidth
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Width; }
        }

        public int screenHeight
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Height; }
        }

        #endregion

        public int enemyTypes = 23;

        public int EnemyTypes
        {
            get { return enemyTypes; }
            set { EnemyTypes = value; }
        }

        public void Initialize(int EnemyIndex, int MovementPattern, int DeathPattern, int ScreenPattern, bool Firing, int MinFireTime, int MaxFireTime)
        {
            firing = Firing;

            enemyIndex = EnemyIndex;
            movementPattern = MovementPattern;
            deathPattern = DeathPattern;
            screenPattern = ScreenPattern;

            Active = true;
            dying = false;
            random = new Random();
            screenCenterX = (screenWidth / 2);
            screenCenterY = (screenHeight / 2);
            EnemyBulletSpeedX = 0;
            ElementType = 0;

            MaxX = screenWidth;
            MinX = 0;
            MaxY = screenHeight;
            MinY = 0;

            hit = false;
            beenHit = false;
            playerPowerUp = false;
            customMovement = false;
            Initializing = true;
            Recovered = true;
            Up = false;
            Down = false;
            Left = false;
            Right = false;
            GivenCredits = false;

            Stunned = false;
            InitiateStun = true;
            StunTime = 0;
            Burned = false;
            InitiateBurn = true;
            BurnTime = 0;
            BurnDamage = 0;
            BurnCounter = 0;
            Poisoned = false;
            InitiatePoison = true;
            PoisonTime = 0;
            PoisonDamage = 0;
            PoisonCounter = 0;
            Slowed = false;
            InitiateSlow = true;
            SlowTime = 0;

            minFireTime = MinFireTime;
            maxFireTime = MaxFireTime;

            scale = (float)((random.Next(90, 110)) / 100f);
            animationSpeed = (float)((float)(random.Next(5, 30)) / 100f);

            PreviousStunTimer = TimeSpan.Zero;
            StunTimer = TimeSpan.FromSeconds(StunTime);
            PreviousBurnTimer = TimeSpan.Zero;
            BurnTimer = TimeSpan.FromSeconds(BurnTime);
            PreviousBurnAdd = TimeSpan.Zero;
            BurnAdd = TimeSpan.FromSeconds(0.1f);
            previousDeathTime = TimeSpan.Zero;
            DeathTime = TimeSpan.FromSeconds(0.2f);
            previousTouchTime = TimeSpan.Zero;
            touchTime = TimeSpan.FromSeconds(1.0f);
            previousHitTime = TimeSpan.Zero;
            HitTime = TimeSpan.FromSeconds(0.2f);
            PreviousPoisonTimer = TimeSpan.Zero;
            PoisonTimer = TimeSpan.FromSeconds(PoisonTime);
            PreviousPoisonAdd = TimeSpan.Zero;
            PoisonAdd = TimeSpan.FromSeconds(0.1f);
            PreviousSlowTimer = TimeSpan.Zero;
            SlowTimer = TimeSpan.FromSeconds(SlowTime);
            PreviousMovementTime = TimeSpan.Zero;
            MovementTime = TimeSpan.FromSeconds(0.3f);

            ClosestPlayer = Vector2.Zero;

            EnemyInitialize();
            ProjectilesInitialize();
            MovementInitialize();
        }

        public void EnemyInitialize()
        {
            #region Enemy 1

            if (enemyIndex == 1)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 2

            if (enemyIndex == 2)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 3

            if (enemyIndex == 3)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 4

            if (enemyIndex == 4)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 5

            if (enemyIndex == 5)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 6

            if (enemyIndex == 6)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 7

            if (enemyIndex == 7)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 8

            if (enemyIndex == 8)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 9

            if (enemyIndex == 9)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 10

            if (enemyIndex == 10)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 11

            if (enemyIndex == 11)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 12

            if (enemyIndex == 12)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 13

            if (enemyIndex == 13)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 14

            if (enemyIndex == 14)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 15

            if (enemyIndex == 15)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 16

            if (enemyIndex == 16)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 17

            if (enemyIndex == 17)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 18

            if (enemyIndex == 18)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 19

            if (enemyIndex == 19)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 20

            if (enemyIndex == 20)
            {
                EnemyProjectile = 2;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 21

            if (enemyIndex == 21)
            {
                EnemyProjectile = 9;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 50000;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 22

            if (enemyIndex == 22)
            {
                EnemyProjectile = 8;
                EnemyDamage = 1;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5f;
                Health = 5;
                Damage = 1;
                Value = 10;
                ElementType = 1;
            }

            #endregion

            #region Enemy 23

            if (enemyIndex == 23)
            {
                EnemyProjectile = 11;
                EnemyDamage = 3;
                EnemyBulletSpeedY = 5f;
                EnemyFireStyle = 1;
                EnemyMaxSpeed = 3.1f;
                EnemyStoppingSpeed = 20f;
                EnemyAcceleration = 5.2f;
                Health = 7;
                Damage = 3;
                Value = 13;
                ElementType = 3;
            }

            #endregion
        }

        public void ProjectilesInitialize()
        {
            if (firing)
            {
                projectiles = new List<Projectile>();
                fireTime = TimeSpan.FromSeconds(.15f);
                isFiring = true;
            }
        }

        public void MovementInitialize()
        {
            MoveTime = TimeSpan.FromSeconds(random.NextDouble() * 4);
            previousMoveTime = TimeSpan.Zero;
            MoveTime2 = TimeSpan.FromSeconds(1f);
            previousMoveTime2 = TimeSpan.Zero;
            staticMoveTime = TimeSpan.FromSeconds(random.Next(100, 500) / 10f);
            previousStaticMoveTime = TimeSpan.Zero;
            enemyLocationMove = 0;

            enemyMoveSpeedHorizontal = 0;
            enemyMoveSpeedVertical = 0;

            move1 = false;
            move2 = false;
        }

        public void LoadContent(Vector2 Spawn)
        {
            LoadBodies(Spawn);
            LoadWings(Spawn);

            EnemyBody.Sprite.Scale(scale);
            EnemyWings.Sprite.Scale(scale);
            EnemyBody.Sprite.Tint = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            LoadParticles();

            OriginalColor = EnemyBody.Sprite.Tint;
        }

        private void LoadParticles()
        {
            deathParticles = new ParticleEngine(3, EnemyBody.Sprite.Tint, Vector2.Zero);
            deathParticles.LoadContent();
            deathParticles.AddParticles = false;

            StunParticle = new ParticleEngine(4, Color.Yellow, Vector2.Zero);
            StunParticle.LoadContent();
            StunParticle.AddParticles = false;
            
            BurnParticle = new ParticleEngine(5, Color.Orange, Vector2.Zero);
            BurnParticle.LoadContent();
            BurnParticle.AddParticles = false;

            hitParticles = new ParticleEngine(1, Color.WhiteSmoke, Vector2.Zero);
            hitParticles.LoadContent();

            PoisonParticle = new ParticleEngine(3, Color.Green, Vector2.Zero);
            PoisonParticle.LoadContent();
            PoisonParticle.AddParticles = false;

            SlowParticle = new ParticleEngine(3, Color.LightBlue, Vector2.Zero);
            SlowParticle.LoadContent();
            SlowParticle.AddParticles = false;

            if (ElementType == 1)
            {
                AuraParticles = new ParticleEngine(0, Color.White, Vector2.Zero);
                AuraParticles.AddParticles = false;
            }
            else if (ElementType == 2)
            {
                AuraParticles = new ParticleEngine(7, Color.White, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 3)
            {
                AuraParticles = new ParticleEngine(7, Color.Black, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 4)
            {
                AuraParticles = new ParticleEngine(7, Color.Yellow, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 5)
            {
                AuraParticles = new ParticleEngine(7, Color.DarkBlue, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 6)
            {
                AuraParticles = new ParticleEngine(7, Color.Orange, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 7)
            {
                AuraParticles = new ParticleEngine(7, Color.DarkGreen, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 8)
            {
                AuraParticles = new ParticleEngine(7, Color.Red, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 9)
            {
                AuraParticles = new ParticleEngine(7, Color.Purple, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else if (ElementType == 10)
            {
                AuraParticles = new ParticleEngine(7, Color.LightBlue, Vector2.Zero);
                AuraParticles.AddParticles = true;
            }
            else
            {
                AuraParticles = new ParticleEngine(0, Color.White, Vector2.Zero);
                AuraParticles.AddParticles = false;
            }
            AuraParticles.LoadContent();
        }

        public void LoadBodies(Vector2 Spawn)
        {
            particleEngine = new ParticleEngine(0, Color.White, Vector2.Zero);

            #region Enemy 1

            if (enemyIndex == 1)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship1");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
                particleEngine = new ParticleEngine(2, new Color(93, 253, 253), Vector2.Zero);
            }

            #endregion

            #region Enemy 2

            if (enemyIndex == 2)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship2");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
                particleEngine = new ParticleEngine(2, new Color(218, 2, 86), Vector2.Zero);
            }

            #endregion

            #region Enemy 3

            if (enemyIndex == 3)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship3");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
                particleEngine = new ParticleEngine(2, new Color(218, 2, 86), Vector2.Zero);
            }

            #endregion

            #region Enemy 4

            if (enemyIndex == 4)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship4");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
                particleEngine = new ParticleEngine(2, Color.DarkBlue, Vector2.Zero);
            }

            #endregion

            #region Enemy 5

            if (enemyIndex == 5)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship5");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 6

            if (enemyIndex == 6)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship6");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 7

            if (enemyIndex == 7)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship7");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 8

            if (enemyIndex == 8)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship8");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 9

            if (enemyIndex == 9)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship9");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 10

            if (enemyIndex == 10)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship10");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 11

            if (enemyIndex == 11)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship11");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 12

            if (enemyIndex == 12)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship12");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 13

            if (enemyIndex == 13)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship13");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 14

            if (enemyIndex == 14)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship14");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 15

            if (enemyIndex == 15)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship15");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 16

            if (enemyIndex == 16)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship16");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 17

            if (enemyIndex == 17)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship17");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 18

            if (enemyIndex == 18)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship18");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 19

            if (enemyIndex == 19)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship19");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 20

            if (enemyIndex == 20)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Ship20");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 21

            if (enemyIndex == 21)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Enemies//Enemy001Body");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 8, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 22

            if (enemyIndex == 22)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Enemies//Enemy002Body");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 23

            if (enemyIndex == 23)
            {
                enemyBodyTexture = Game1.Instance.Content.Load<Texture2D>("Images//Enemies//Enemy003Body");
                EnemyBody = new MobileSprite(enemyBodyTexture);
                EnemyBody.Sprite.AddAnimation("default", 0, 0, 64, 64, 22, animationSpeed / 4);
                EnemyBody.Sprite.AutoRotate = false;
            }

            #endregion

            particleEngine.LoadContent();
            EnemyBody.Sprite.CurrentAnimation = "default";
            EnemyBody.Position = Spawn;
            EnemyBody.IsActive = true;
            EnemyBody.IsCollidable = true;
            EnemyBody.locked = false;
            EnemyBody.IsMoving = false;
            EnemyBody.Sprite.Physics(EnemyMaxSpeed, EnemyStoppingSpeed);
        }

        public void LoadWings(Vector2 Spawn)
        {
            #region Enemy 1

            if (enemyIndex == 1)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull1");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 2

            if (enemyIndex == 2)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull2");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 3

            if (enemyIndex == 3)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull3");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 3, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 4

            if (enemyIndex == 4)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull4");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 12, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 5

            if (enemyIndex == 5)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull5");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 6

            if (enemyIndex == 6)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull6");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 7

            if (enemyIndex == 7)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull7");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 8

            if (enemyIndex == 8)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull8");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 9

            if (enemyIndex == 9)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull9");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 10

            if (enemyIndex == 10)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull10");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 11

            if (enemyIndex == 11)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull11");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 12

            if (enemyIndex == 12)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull12");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 13

            if (enemyIndex == 13)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull13");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 14

            if (enemyIndex == 14)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull14");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 15

            if (enemyIndex == 15)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull15");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 16

            if (enemyIndex == 16)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull16");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 17

            if (enemyIndex == 17)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull17");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 18

            if (enemyIndex == 18)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull18");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 19

            if (enemyIndex == 19)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull19");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 20

            if (enemyIndex == 20)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Ships//Hull20");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = true;
            }

            #endregion

            #region Enemy 21

            if (enemyIndex == 21)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Enemies//Enemy001Wings");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 8, animationSpeed);
                EnemyWings.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 22

            if (enemyIndex == 22)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Enemies//Enemy002Wings");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 4, animationSpeed);
                EnemyWings.Sprite.AutoRotate = false;
            }

            #endregion

            #region Enemy 23

            if (enemyIndex == 23)
            {
                enemyWingsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Enemies//Enemy003Wings");
                EnemyWings = new MobileSprite(enemyWingsTexture);
                EnemyWings.Sprite.AddAnimation("default", 0, 0, 64, 64, 22, animationSpeed / 4);
                EnemyWings.Sprite.AutoRotate = false;
            }

            #endregion

            EnemyWings.Sprite.CurrentAnimation = EnemyBody.Sprite.CurrentAnimation;
            EnemyWings.Position = Spawn;
            EnemyWings.IsMoving = false;
            EnemyWings.locked = false;
            EnemyWings.IsActive = true;
            EnemyWings.IsCollidable = true;
        }

        public void Update(GameTime gameTime)
        {
            if (TimeStop == false)
            {
                if (Initializing)
                {
                    fireTime = TimeSpan.FromSeconds((float)random.Next(minFireTime, maxFireTime) / 10f);
                    previousFireTime = gameTime.TotalGameTime;
                    Initializing = false;
                }

                if (Health <= 0)
                    UpdateDeath(gameTime);
                //UpdateScreenPattern();
                UpdateMovement(gameTime);

                if (customMovement)
                    EnemyBody.Sprite.MoveBy(enemyMoveSpeedHorizontal, enemyMoveSpeedVertical);

                EnemyBody.Sprite.Rotation = EnemyWings.Sprite.Rotation;

                if (Stunned)
                    BeenStunned(gameTime);
                if (Burned)
                    BeenBurned(gameTime);
                if (Poisoned)
                    BeenPoisoned(gameTime);
                if (Slowed)
                    BeenSlowed(gameTime);

                EnemyBody.Update(gameTime);
                EnemyWings.Update(gameTime);
                UpdateProjectiles(gameTime);
                UpdateHit(gameTime);
                UpdatePlayerPowerUp();
                UpdateParticles(gameTime);

                if (PowerUpActive == 1)
                {
                    PowerUp(gameTime);
                }

                EnemyWings.Position = EnemyBody.Position;
            }
        }

        private void BeenStunned(GameTime gameTime)
        {
            if (InitiateStun)
            {
                PreviousStunTimer = gameTime.TotalGameTime;
                StunTimer = TimeSpan.FromSeconds(StunTime);
                InitiateStun = false;
            }

            StunParticle.AddParticles = true;
            EnemyBody.Sprite.Tint = Color.Yellow;
            EnemyWings.Sprite.Tint = Color.Yellow;

            previousFireTime = gameTime.TotalGameTime;
            EnemyBody.Sprite.acceleration = Vector2.Zero;
            EnemyBody.Sprite.velocity = Vector2.Zero;


            if (gameTime.TotalGameTime - PreviousStunTimer > StunTimer || Health <= 0)
            {
                EnemyBody.Sprite.Tint = OriginalColor;
                EnemyWings.Sprite.Tint = Color.White;
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

            EnemyBody.Sprite.Tint = Color.Orange;
            EnemyWings.Sprite.Tint = Color.Orange;

            if (gameTime.TotalGameTime - PreviousBurnAdd > BurnAdd)
            {
                PreviousBurnAdd = gameTime.TotalGameTime;
                Health -= BurnDamage / 10;
                if (BurnDamage / 10 < 1)
                {
                    BurnCounter += BurnDamage;
                    if (BurnCounter >= 10)
                    {
                        BurnCounter = 0;
                        Health -= 1;
                    }
                }
            }

            if (gameTime.TotalGameTime - PreviousBurnTimer > BurnTimer || Health <= 0)
            {
                BurnCounter = 0;
                EnemyBody.Sprite.Tint = OriginalColor;
                EnemyWings.Sprite.Tint = Color.White;
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

            EnemyBody.Sprite.Tint = Color.Green;
            EnemyWings.Sprite.Tint = Color.Green;

            if (gameTime.TotalGameTime - PreviousPoisonAdd > PoisonAdd)
            {
                PreviousPoisonAdd = gameTime.TotalGameTime;
                Health -= PoisonDamage / 10;
                if (PoisonDamage / 10 < 1)
                {
                    PoisonCounter += PoisonDamage;
                    if (PoisonCounter >= 10)
                    {
                        PoisonCounter = 0;
                        Health -= 1;
                    }
                }
            }

            if (gameTime.TotalGameTime - PreviousPoisonTimer > PoisonTimer || Health <= 0)
            {
                PoisonCounter = 0;
                EnemyBody.Sprite.Tint = OriginalColor;
                EnemyWings.Sprite.Tint = Color.White;
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
                EnemyBody.Sprite.maxSpeed = EnemyBody.Sprite.maxSpeed / 4;
                EnemyBulletSpeedY = EnemyBulletSpeedY / 4;
            }

            EnemyBody.Sprite.Tint = Color.LightBlue;
            EnemyWings.Sprite.Tint = Color.LightBlue;

            if (gameTime.TotalGameTime - PreviousSlowTimer > SlowTimer || Health <= 0)
            {
                EnemyBody.Sprite.maxSpeed = EnemyBody.Sprite.OriginalMaxSpeed;
                EnemyBulletSpeedY = EnemyBulletSpeedY * 4;
                EnemyBody.Sprite.Tint = OriginalColor;
                EnemyWings.Sprite.Tint = Color.White;
                SlowParticle.AddParticles = false;
                InitiateSlow = true;
                Slowed = false;
            }
        }

        private void UpdateParticles(GameTime gameTime)
        {
            if (Health < 0)
            {
                deathParticles.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
                deathParticles.Update();
            }

            StunParticle.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
            StunParticle.Update();

            BurnParticle.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
            BurnParticle.Update();

            PoisonParticle.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
            PoisonParticle.Update();

            SlowParticle.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
            SlowParticle.Update();

            particleEngine.EmitterLocationUpdate(new Vector2(EnemyBody.Position.X + (EnemyBody.Sprite.Width / 2), EnemyBody.Position.Y + (EnemyBody.Sprite.Height / 2)));
            particleEngine.Update();

            hitParticles.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
            hitParticles.Update();

            AuraParticles.EmitterLocationUpdate(EnemyBody.Position.X + 10, EnemyBody.Position.X + EnemyBody.Sprite.Width - 10, EnemyBody.Position.Y + 10, EnemyBody.Position.Y + EnemyBody.Sprite.Height - 10);
            AuraParticles.Update();
            
            if (gameTime.TotalGameTime - previousHitTime > HitTime)
            {
                hitParticles.AddParticles = false;
            }
        }

        public void UpdateMovement(GameTime gameTime)
        {
            if (movementPattern == 0)
            {
                if (gameTime.TotalGameTime - previousMoveTime > MoveTime)
                {
                    move1 = true;
                }

                if (gameTime.TotalGameTime - previousMoveTime > MoveTime2)
                {
                    move2 = true;
                }

                if (move1 && EnemyBody.Sprite.CurrentAnimation == "default")
                {
                    previousMoveTime = gameTime.TotalGameTime;
                    enemyMoveSpeedHorizontal = (float)(random.Next(-10, 10) / 10f);
                    move1 = false;
                }

                if (move2 && EnemyBody.Sprite.CurrentAnimation == "default")
                {
                    previousMoveTime2 = gameTime.TotalGameTime;
                    enemyMoveSpeedVertical = (float)random.Next(-200, 200) / 100f;
                    move2 = false;
                }
            }

            if (movementPattern == 1 && customMovement)
            {                
                if (gameTime.TotalGameTime - previousMoveTime > MoveTime)
                {
                    move1 = true;
                }

                if (gameTime.TotalGameTime - previousMoveTime > MoveTime2)
                {
                    move2 = true;
                }

                if (move1 && EnemyBody.Sprite.CurrentAnimation == "default")
                {
                    previousMoveTime = gameTime.TotalGameTime;
                    enemyMoveSpeedHorizontal = (float)random.Next(-5, 5) / 10f;
                    move1 = false;
                }

                if (move2 && EnemyBody.Sprite.CurrentAnimation == "default")
                {
                    previousMoveTime2 = gameTime.TotalGameTime;
                    enemyMoveSpeedVertical = (float)random.Next(-10, 20) / 10f;
                    move2 = false;
                }
            }

            if (movementPattern == 2)
            {
                #region Index Locations

                if (EnemyNumber == 1)
                    enemyLocation = new Vector2(((screenWidth / 11) * 5) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 2)
                    enemyLocation = new Vector2(((screenWidth / 11) * 4) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 3)
                    enemyLocation = new Vector2(((screenWidth / 11) * 6) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 4)
                    enemyLocation = new Vector2(((screenWidth / 11) * 3) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 5)
                    enemyLocation = new Vector2(((screenWidth / 11) * 7) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 6)
                    enemyLocation = new Vector2(((screenWidth / 11) * 2) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 7)
                    enemyLocation = new Vector2(((screenWidth / 11) * 8) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 8)
                    enemyLocation = new Vector2(((screenWidth / 11) * 1) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 9)
                    enemyLocation = new Vector2(((screenWidth / 11) * 9) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 10)
                    enemyLocation = new Vector2(((screenWidth / 11) * 10) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, (screenHeight / 11) + Game1.Instance.ViewportPositionOne.Y);

                if (EnemyNumber == 11)
                    enemyLocation = new Vector2(((screenWidth / 11) * 5) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 12)
                    enemyLocation = new Vector2(((screenWidth / 11) * 4) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 13)
                    enemyLocation = new Vector2(((screenWidth / 11) * 6) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 14)
                    enemyLocation = new Vector2(((screenWidth / 11) * 3) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 15)
                    enemyLocation = new Vector2(((screenWidth / 11) * 7) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 16)
                    enemyLocation = new Vector2(((screenWidth / 11) * 2) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 17)
                    enemyLocation = new Vector2(((screenWidth / 11) * 8) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 18)
                    enemyLocation = new Vector2(((screenWidth / 11) * 1) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 19)
                    enemyLocation = new Vector2(((screenWidth / 11) * 9) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 20)
                    enemyLocation = new Vector2(((screenWidth / 11) * 10) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 2) + Game1.Instance.ViewportPositionOne.Y);

                if (EnemyNumber == 21)
                    enemyLocation = new Vector2(((screenWidth / 11) * 5) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 22)
                    enemyLocation = new Vector2(((screenWidth / 11) * 4) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 23)
                    enemyLocation = new Vector2(((screenWidth / 11) * 6) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 24)
                    enemyLocation = new Vector2(((screenWidth / 11) * 3) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 25)
                    enemyLocation = new Vector2(((screenWidth / 11) * 7) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 26)
                    enemyLocation = new Vector2(((screenWidth / 11) * 2) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 27)
                    enemyLocation = new Vector2(((screenWidth / 11) * 8) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 28)
                    enemyLocation = new Vector2(((screenWidth / 11) * 1) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 29)
                    enemyLocation = new Vector2(((screenWidth / 11) * 9) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 30)
                    enemyLocation = new Vector2(((screenWidth / 11) * 10) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 3) + Game1.Instance.ViewportPositionOne.Y);


                if (EnemyNumber == 31)
                    enemyLocation = new Vector2(((screenWidth / 11) * 5) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 32)
                    enemyLocation = new Vector2(((screenWidth / 11) * 4) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 33)
                    enemyLocation = new Vector2(((screenWidth / 11) * 6) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 34)
                    enemyLocation = new Vector2(((screenWidth / 11) * 3) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 35)
                    enemyLocation = new Vector2(((screenWidth / 11) * 7) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 36)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 2) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 37)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 8) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 38)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 1) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 39)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 9) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 40)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 10) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 4) + Game1.Instance.ViewportPositionOne.Y);
                                                                                                                                                                  
                                                                                                                                                                  
                if (EnemyNumber == 41)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 5) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 42)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 4) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 43)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 6) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 44)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 3) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 45)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 7) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 46)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 2) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 47)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 8) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 48)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 1) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 49)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 9) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);
                if (EnemyNumber == 50)                                                                                                                            
                    enemyLocation = new Vector2(((screenWidth / 11) * 10) - (EnemyBody.Sprite.Width / 2) + enemyLocationMove + Game1.Instance.ViewportPositionOne.X, ((screenHeight / 11) * 5) + Game1.Instance.ViewportPositionOne.Y);


                #endregion

                if (gameTime.TotalGameTime - PreviousMovementTime > MovementTime)
                {
                    if ((enemyLocation.X - EnemyBody.Position.X) > 3 || (enemyLocation.X - EnemyBody.Position.X) < -3)
                    {
                        EnemyWings.Sprite.AutoRotate = true;
                        if (enemyLocation.X > EnemyBody.Position.X)
                        {
                            if (Left)
                            {
                                EnemyBody.Sprite.velocity.X = EnemyBody.Sprite.velocity.X / 5;
                            }
                            Right = true;
                            Left = false;
                            EnemyBody.Sprite.MoveX(EnemyAcceleration);
                        }
                        else if (enemyLocation.X < EnemyBody.Position.X)
                        {
                            if (Right)
                            {
                                EnemyBody.Sprite.velocity.X = EnemyBody.Sprite.velocity.X / 5;
                            }
                            Left = true;
                            Right = false;
                            EnemyBody.Sprite.MoveX(-EnemyAcceleration);
                        }
                    }

                    if ((enemyLocation.Y - EnemyBody.Position.Y) > 3 || (enemyLocation.Y - EnemyBody.Position.Y) < -3)
                    {
                        EnemyWings.Sprite.AutoRotate = true;
                        if (enemyLocation.Y > EnemyBody.Position.Y)
                        {
                            if (Up)
                            {
                                EnemyBody.Sprite.velocity.Y = EnemyBody.Sprite.velocity.Y / 5;
                            }
                            Down = true;
                            Up = false;
                            EnemyBody.Sprite.MoveY(EnemyAcceleration);
                        }
                        else if (enemyLocation.Y < EnemyBody.Position.Y)
                        {
                            if (Down)
                            {
                                EnemyBody.Sprite.velocity.Y = EnemyBody.Sprite.velocity.Y / 5;
                            }
                            Down = false;
                            Up = true;
                            EnemyBody.Sprite.MoveY(-EnemyAcceleration);
                        }
                    }

                    PreviousMovementTime = gameTime.TotalGameTime;
                }

                if (((enemyLocation.Y - EnemyBody.Position.Y) < 48 && (enemyLocation.Y - EnemyBody.Position.Y) > -48) &&
                    ((enemyLocation.X - EnemyBody.Position.X) < 48 && (enemyLocation.X - EnemyBody.Position.X) > -48))
                {
                    EnemyWings.Sprite.AutoRotate = false;
                    EnemyWings.Sprite.Rotation = 0f;

                    if (enemyLocation.Y > EnemyBody.Position.Y)
                        EnemyBody.Sprite.velocity.Y = 1;
                    else if (enemyLocation.Y < EnemyBody.Position.Y)
                        EnemyBody.Sprite.velocity.Y = -1;
                    else
                        EnemyBody.Sprite.velocity.Y = 0;

                    if (enemyLocation.X > EnemyBody.Position.X)
                        EnemyBody.Sprite.velocity.X = 1;
                    else if (enemyLocation.X < EnemyBody.Position.X)
                        EnemyBody.Sprite.velocity.X = -1;
                    else
                        EnemyBody.Sprite.velocity.X = 0;
                }
            }
        }

        public void UpdateDeath(GameTime gameTime)
        {
            if (deathPattern == 0)
            {
                if (Health <= 0 && EnemyBody.Sprite.CurrentAnimation == "default" && dying == false)
                {
                    EnemyBody.Sprite.CurrentAnimation = "dead";
                    EnemyWings.Sprite.CurrentAnimation = "dead";
                    enemyMoveSpeedHorizontal = 0;
                    enemyMoveSpeedVertical = 0;
                    dying = true;
                    deathParticles.AddParticles = true;
                    particleEngine.AddParticles = false;
                    AuraParticles.AddParticles = false;
                    previousDeathTime = gameTime.TotalGameTime;
                    Game1.Instance.AudioPlay("Explosion", 4);
                }

                EnemyBody.Sprite.acceleration = Vector2.Zero;
                EnemyBody.Sprite.velocity = Vector2.Zero;

                if (EnemyBody.Sprite.CurrentAnimation == "dead" && gameTime.TotalGameTime - previousDeathTime > DeathTime)
                {
                    deathParticles.AddParticles = false;

                    if (projectiles.Count == 0 && deathParticles.particles.Count == 0)
                    {
                        Active = false;
                    }
                }
            }

            if (deathPattern == 1)
            {
                if (Health <= 0 && EnemyBody.Sprite.CurrentAnimation == "default" && dying == false)
                {
                    PowerUpActive = random.Next(0, 5);
                    if (PowerUpActive == 1)
                    {
                        powerUps = new PowerUps();
                        powerUps.Initialize(1, 0);
                        PowerUpActivated = false;
                        powerUps.active = true;
                    }
                    EnemyBody.Sprite.CurrentAnimation = "dead";
                    EnemyWings.Sprite.CurrentAnimation = "dead";
                    enemyMoveSpeedHorizontal = 0;
                    enemyMoveSpeedVertical = 0;
                    dying = true;
                    deathParticles.AddParticles = true;
                    particleEngine.AddParticles = false;
                    AuraParticles.AddParticles = false;
                    previousDeathTime = gameTime.TotalGameTime;
                    Game1.Instance.AudioPlay("Explosion", 4);
                }

                EnemyBody.Sprite.acceleration = Vector2.Zero;
                EnemyBody.Sprite.velocity = Vector2.Zero;

                if (EnemyBody.Sprite.CurrentAnimation == "dead" && gameTime.TotalGameTime - previousDeathTime > DeathTime)
                {
                    deathParticles.AddParticles = false;

                    if (projectiles.Count == 0 && deathParticles.particles.Count == 0)
                    {
                        if (PowerUpActive == 1)
                        {
                            if (powerUps.active == false)
                            {
                                Active = false;
                            }
                        }
                        else
                            Active = false;
                    }
                }
            }
        }

        public void UpdateScreenPattern()
        {
            if (screenPattern == 0)
            {
                if (EnemyBody.Sprite.Position.X > (screenWidth + 10) - EnemyBody.Sprite.Width)
                {
                    EnemyBody.Sprite.SetPosX((screenWidth + 10) - EnemyBody.Sprite.Width);
                    EnemyBody.Sprite.velocity = new Vector2(-EnemyBody.Sprite.velocity.X, -1);
                    beenHit = true;
                }

                if (EnemyBody.Sprite.Position.X < MinX - 10)
                {
                    EnemyBody.Sprite.SetPosX(-10);
                    EnemyBody.Sprite.velocity = new Vector2(-EnemyBody.Sprite.velocity.X, -1);
                    beenHit = true;
                }

                if (EnemyBody.Sprite.Position.Y > MaxY + EnemyBody.Sprite.Height && projectiles.Count == 0)
                {
                    Active = false;
                }
            }

            if (screenPattern == 1)
            {
                if (EnemyBody.Sprite.Position.X > MaxX + EnemyBody.Sprite.Width)
                {
                    EnemyBody.Sprite.SetPosX(MinX - (EnemyBody.Sprite.Width - 1));
                }

                if (EnemyBody.Sprite.Position.X < MinX - EnemyBody.Sprite.Width)
                {
                    EnemyBody.Sprite.SetPosX(MaxX + (EnemyBody.Sprite.Width - 1));
                }

                if (EnemyBody.Sprite.Position.Y > MaxY + EnemyBody.Sprite.Height)
                {
                    EnemyBody.Sprite.SetPosY(MinY - (EnemyBody.Sprite.Height - 1));
                }

                if (EnemyBody.Sprite.Position.Y < MinY - EnemyBody.Sprite.Height)
                {
                    EnemyBody.Sprite.SetPosY(MaxY + (EnemyBody.Sprite.Height - 1));
                }
            }
        }

        private void UpdateProjectiles(GameTime gameTime)
        {
            if (firing)
            {
                for (int i = projectiles.Count - 1; i >= 0; i--)
                {
                    projectileWidth = projectiles[i].projectile.Sprite.Width;
                    projectileHeight = projectiles[i].projectile.Sprite.Height;
                    projectiles[i].Update(gameTime, ClosestPlayer);
                    
                    if (projectiles[i].active == false)
                        projectiles[i].projectile.Position = new Vector2(screenWidth * 2, screenHeight * 2);
                    if (projectiles[i].active == false && projectiles[i].radius > 0)
                        projectiles[i].Explosion = true;
                    if (projectiles[i].active == false && projectiles[i].particleEngine.particles.Count == 0 && projectiles[i].ExplosionParticle.particles.Count == 0 && projectiles[i].radius < 1)
                    {
                        projectiles.RemoveAt(i);
                    }
                }
            }

            if (dying == false)
            {
                if (gameTime.TotalGameTime - previousFireTime > fireTime && isFiring)
                {
                    if (EnemyBody.Position.Y > 0 && EnemyBody.Position.Y < screenHeight)
                    {
                        fireTime = TimeSpan.FromSeconds((float)random.Next(minFireTime, maxFireTime) / 10f);
                        previousFireTime = gameTime.TotalGameTime;
                        AddProjectile(EnemyBody.Sprite.Position + new Vector2(Width / 2 - (projectileWidth / 2), Height - (projectileHeight / 2)));
                    }
                }
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
                EnemyBody.Sprite.Tint = Color.Red;
                EnemyWings.Sprite.Tint = Color.Red;
                beenHit = false;
                Recovered = false;
            }

            if (gameTime.TotalGameTime - previousHitTime > TimeSpan.FromSeconds(0.03f) && Recovered == false)
            {
                EnemyBody.Sprite.Tint = OriginalColor;
                EnemyWings.Sprite.Tint = Color.White;
                Recovered = true;
            }
        }

        public void UpdatePlayerPowerUp()
        {
            if (playerPowerUp)
            {
                playerPowerUp = false;
            }
        }

        private void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile();
            int i = random.Next(0, 4);
            if (Money && i != 1)
                projectile.Initialize(0, position, 0, EnemyBulletSpeedY, EnemyBulletSpeedX, EnemyFireStyle, 2, EnemyBody.Sprite.velocity);
            else
                projectile.Initialize(EnemyProjectile, position, EnemyDamage, EnemyBulletSpeedY, EnemyBulletSpeedX, EnemyFireStyle, 2, EnemyBody.Sprite.velocity);


            projectile.LoadContent();
            if (Money && i != 1)
            {
                if (LargeCrystals && random.Next(1, 20) == 1)
                {
                    projectile.projectile.Sprite.Scale((float)random.Next(100, 225) / 100f);
                    projectile.particleEngine.scale = projectile.projectile.Sprite.scale - 0.5f;
                }

                projectile.MoneyValue = (int)(Value * MoneyValueMultiplier * projectile.projectile.Sprite.scale);
            }
            if (Money == false)
                projectiles.Add(projectile);
            else if (i == 1 && random.Next(0, 10) == 1)
            {
                powerUps = new PowerUps();
                powerUps.Initialize(1, 0);
                PowerUpActivated = false;
                PowerUpActive = 1;
                powerUps.active = true;
            }
            else
                projectiles.Add(projectile);

        }

        private void PowerUp(GameTime gameTime)
        {
            if (PowerUpActivated == false)
            {
                powerUps.LoadContent(EnemyBody.Position + new Vector2((EnemyBody.Sprite.Width / 2) - 16, (EnemyBody.Sprite.Height / 2) - 16));
                PowerUpActivated = true;
            }

            powerUps.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AuraParticles.Draw(spriteBatch);
            particleEngine.Draw(spriteBatch);

            if (firing)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }
            }

            if (PowerUpActive == 1)
                powerUps.Draw(spriteBatch);
            EnemyBody.Draw(spriteBatch);
            EnemyWings.Draw(spriteBatch);
            deathParticles.Draw(spriteBatch);
            StunParticle.Draw(spriteBatch);
            BurnParticle.Draw(spriteBatch);
            PoisonParticle.Draw(spriteBatch);
            SlowParticle.Draw(spriteBatch);
            hitParticles.Draw(spriteBatch);
        }
    }
}

