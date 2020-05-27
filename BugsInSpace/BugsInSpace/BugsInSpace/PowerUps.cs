using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class PowerUps
    {
        #region Variables

        public MobileSprite powerUp;
        Texture2D powerUpTexture;
        public int powerUpNumber;
        float moveSpeedVertical;
        Random random;
        public bool active;
        int owner;
        int receivingNumber;
        public float Acceleration;
        public bool AutoFire;
        public float BulletSpeed;
        public int Damage;
        public float FireRate;
        public int MaxBullets;
        public int Lives;
        public bool Energy;
        public bool Health;

        #endregion

        #region Properties

        public int ScreenHeight
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Height; }
        }

        #endregion

        public void Initialize(int Owner, int ReceivingNumber)
        {
            random = new Random();
            active = true;
            powerUpNumber = random.Next(1, 15);
            //powerUpNumber = 6;
            owner = Owner;
            receivingNumber = ReceivingNumber;

            if (owner == 1)
                moveSpeedVertical = 10f;

            if (owner == 2)
                moveSpeedVertical = -10f;

            ReceivingPowers();
        }

        private void ReceivingPowers()
        {
            if (receivingNumber == 1)
            {
                Acceleration = 10f;
                Game1.Instance.AudioPlay("AccelerationPowerUp", 4);
            }

            if (receivingNumber == 2)
            {
                Acceleration = -10f;
                Game1.Instance.AudioPlay("AccelerationPowerDown", 4);
            }

            if (receivingNumber == 3)
            {
                AutoFire = true;
                Game1.Instance.AudioPlay("AutoFirePowerUp", 4);
            }

            if (receivingNumber == 4)
            {
                BulletSpeed = 2f;
                Game1.Instance.AudioPlay("BulletSpeedPowerUp", 4);
            }

            if (receivingNumber == 5)
            {
                BulletSpeed = -2f;
                Game1.Instance.AudioPlay("BulletSpeedPowerDown", 4);
            }

            if (receivingNumber == 6)
            {
                Damage = 1;
                Game1.Instance.AudioPlay("DamagePowerUp", 4);
            }

            if (receivingNumber == 7)
            {
                Damage = -1;
                Game1.Instance.AudioPlay("DamagePowerDown", 4);
            }

            if (receivingNumber == 8)
            {
                FireRate = 0.1f;
                Game1.Instance.AudioPlay("FireRatePowerUp", 4);
            }

            if (receivingNumber == 9)
            {
                FireRate = -0.1f;
                Game1.Instance.AudioPlay("FireRatePowerDown", 4);
            }

            if (receivingNumber == 10)
            {
                MaxBullets = 10;
                Game1.Instance.AudioPlay("MaxBulletsPowerUp", 4);
            }

            if (receivingNumber == 11)
            {
                MaxBullets = -10;
                Game1.Instance.AudioPlay("MaxBulletsPowerDown", 4);
            }

            if (receivingNumber == 12)
            {
                Lives = 1;
                Game1.Instance.AudioPlay("LivesUp", 4);
            }

            if (receivingNumber == 13)
            {
                Energy = true;
                Game1.Instance.AudioPlay("EnergyUp", 4);
            }

            if (receivingNumber == 14)
            {
                Health = true;
                Game1.Instance.AudioPlay("HealthUp", 4);
            }
        }

        public void ResetPowerUps()
        {
            Acceleration = 0;
            AutoFire = false;
            BulletSpeed = 0;
            Damage = 0;
            FireRate = 0;
            MaxBullets = 0;
            Lives = 0;
            Energy = false;
            Health = false;
        }

        public void LoadContent(Vector2 Position)
        {
            if (powerUpNumber == 1)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//AccelerationUp");
            }

            if (powerUpNumber == 2)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//AccelerationDown");
            }

            if (powerUpNumber == 3)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//AutoFire");
            }

            if (powerUpNumber == 4)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//BulletSpeedUp");
            }

            if (powerUpNumber == 5)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//BulletSpeedDown");
            }

            if (powerUpNumber == 6)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//DamageUp");
            }

            if (powerUpNumber == 7)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//DamageDown");
            }

            if (powerUpNumber == 8)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//FireRateUp");
            }

            if (powerUpNumber == 9)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//FireRateDown");
            }

            if (powerUpNumber == 10)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//MaxBulletsUp");
            }

            if (powerUpNumber == 11)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//MaxBulletsDown");
            }

            if (powerUpNumber == 12)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//LivesUp");
            }

            if (powerUpNumber == 13)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//EnergyUp");
            }

            if (powerUpNumber == 14)
            {
                powerUpTexture = Game1.Instance.Content.Load<Texture2D>("Images//PowerUps//HealthUp");
            }

            powerUp = new MobileSprite(powerUpTexture);
            powerUp.Sprite.AddAnimation("default", 0, 0, 32, 32, 3, 0.2f);
            powerUp.Sprite.Tint = Color.White;
            powerUp.Sprite.CurrentAnimation = "default";
            powerUp.IsActive = true;
            powerUp.IsMoving = false;
            powerUp.IsCollidable = true;
            powerUp.Sprite.Physics(random.Next(30, 100) / 10, 0f);
            powerUp.Position = Position;

            Game1.Instance.AudioPlay("PowerUpDrop", 4);
        }

        public void Update(GameTime gameTime)
        {
            if (active)
            {
                powerUp.Sprite.MoveY(moveSpeedVertical);

                if (powerUp.Sprite.Position.Y > ScreenHeight)
                {
                    active = false;
                }

                powerUp.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
                powerUp.Draw(spriteBatch);
        }
    }
}
