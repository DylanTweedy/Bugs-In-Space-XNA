using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class GUI
    {

        SpriteFont font;
        public bool Active;
        Texture2D GuiTexture;
        public MobileSprite Gui;
        Texture2D LifeTexture;
        Vector2 LifePosition;
        Rectangle LifeRectangle;
        Texture2D ExperienceTexture;
        Vector2 ExperiencePosition;
        Rectangle ExperienceRectangle;
        Texture2D GuiTextureBorder;
        MobileSprite GuiBorder;
        Texture2D GuiTextureHealth;
        Texture2D GuiTextureEnergy;
        public string PlayerName;
        public Color PlayerColor;
        public int PlayerCredits;
        public int PlayerHealth;
        public int PlayerEnergy;
        public int PlayerMaxEnergy;
        public int PlayerLives;
        public int PlayerMaxHealth;
        public int PlayerExperience;
        public int PlayerExperienceToNextLevel;
        public int PlayerLevel;
        public bool playerReset;
        Rectangle guiHealthRect;
        Rectangle guiEnergyRect;
        Vector2 GuiHealthPosition;
        Vector2 GuiEnergyPosition;
        public double PlayerHealthPercentage;
        public double PlayerEnergyPercentage;
        public int XPosition;
        int PlayerExperiencePercentage;
        int previousLevel;
        bool LevelUp;
        ParticleEngine LevelUpParticles;
        TimeSpan LevelUpTime;
        TimeSpan PreviousLevelUpTime;
        Random random;

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

        public void Initialize()
        {
            playerReset = true;
            LevelUp = false;
            LevelUpTime = TimeSpan.FromSeconds(1.15f);
            PreviousLevelUpTime = TimeSpan.Zero;
            random = new Random();
        }

        public void LoadContent()
        {
            GuiTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//GUI");
            Gui = new MobileSprite(GuiTexture);
            Gui.Sprite.AddAnimation("default", 0, 0, 200, 100, 1, 10f);
            Gui.Sprite.CurrentAnimation = "default";
            Gui.Position = new Vector2(-200, 0);
            GuiTextureBorder = Game1.Instance.Content.Load<Texture2D>("Images//GUI//GUIBorder");
            GuiBorder = new MobileSprite(GuiTextureBorder);
            GuiBorder.Sprite.AddAnimation("default", 0, 0, 200, 100, 1, 10f);
            GuiBorder.Sprite.CurrentAnimation = "default";
            GuiBorder.Position = new Vector2(0, -Gui.Sprite.Height);
            GuiTextureHealth = Game1.Instance.Content.Load<Texture2D>("Images//GUI//Healthbar");
            GuiTextureEnergy = Game1.Instance.Content.Load<Texture2D>("Images//GUI//EnergyBar");
            font = Game1.Instance.Content.Load<SpriteFont>("Fonts//Font1");
            guiHealthRect = new Rectangle(0, 0, GuiTextureHealth.Width - 7, GuiTextureHealth.Height);
            guiEnergyRect = new Rectangle(0, 0, GuiTextureEnergy.Width - 7, GuiTextureEnergy.Height);
            GuiHealthPosition = Gui.Position;
            GuiEnergyPosition = Gui.Position;
            LifeTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//Life");
            LifePosition = Gui.Position;
            LifeRectangle = new Rectangle(0, 0, LifeTexture.Width, LifeTexture.Height);
            ExperienceTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//ExperienceBar");
            ExperiencePosition = Gui.Position;
            ExperienceRectangle = new Rectangle(0, 0, ExperienceTexture.Width - 7, ExperienceTexture.Height);

            LevelUpParticles = new ParticleEngine(8, Color.Gold, Vector2.Zero);
            LevelUpParticles.LoadContent();
            LevelUpParticles.AddParticles = false;
        }

        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                if (playerReset)
                {
                    Gui.Position = new Vector2(0, -Gui.Sprite.Height);
                    playerReset = false;
                    PreviousLevelUpTime = gameTime.TotalGameTime;
                }

                if (Gui.Position.Y < 0)
                {
                    Gui.Sprite.SetPosY((int)Gui.Position.Y + 10);
                }

                if (previousLevel != PlayerLevel && gameTime.TotalGameTime - PreviousLevelUpTime > LevelUpTime)
                {
                    LevelUp = true;
                    PreviousLevelUpTime = gameTime.TotalGameTime;
                }

                if (LevelUp)
                    LevelUpUpdate(gameTime);

                Gui.Sprite.SetPosX(XPosition);
                GuiBorder.Position = Gui.Position;
                GuiHealthPosition = Gui.Position;
                GuiEnergyPosition = Gui.Position;
                LifePosition = Gui.Position;
                ExperiencePosition = Gui.Position;

                if (LevelUp == false)
                    Gui.Sprite.Tint = PlayerColor;
                else
                    Gui.Sprite.Tint = Color.Gold;


                PlayerHealthPercentage = ((double)PlayerHealth / (double)PlayerMaxHealth) * 100;
                guiHealthRect.Width = (int)((PlayerHealthPercentage * 200) / 100);
                PlayerEnergyPercentage = ((double)PlayerEnergy / (double)PlayerMaxEnergy) * 100;
                guiEnergyRect.Width = (int)((PlayerEnergyPercentage * 200) / 100);
                PlayerExperiencePercentage = (int)(((double)PlayerExperience / (double)PlayerExperienceToNextLevel) * 100);
                ExperienceRectangle.Width = (int)((PlayerExperiencePercentage * 200) / 100);

                if (PlayerExperiencePercentage > 100)
                    PlayerExperiencePercentage = 100;

                if (PlayerLives == 4)
                    LifeRectangle.Width = 200;
                if (PlayerLives == 3)
                    LifeRectangle.Width = 150;
                if (PlayerLives == 2)
                    LifeRectangle.Width = 100;
                if (PlayerLives == 1)
                    LifeRectangle.Width = 50;
                if (PlayerLives == 0)
                    LifeRectangle.Width = 0;

                LevelUpParticles.Update();

                previousLevel = PlayerLevel;
            }
        }

        private void LevelUpUpdate(GameTime gameTime)
        {
            LevelUpParticles.AddParticles = true;
            LevelUpParticles.EmitterLocationUpdate(new Vector2(random.Next((int)Gui.Position.X, (int)Gui.Position.X + Gui.Sprite.Width), random.Next((int)Gui.Position.Y, (int)Gui.Position.Y + Gui.Sprite.Height)));

            if (gameTime.TotalGameTime - PreviousLevelUpTime > LevelUpTime)
            {
                LevelUp = false;
                LevelUpParticles.AddParticles = false;
            }       
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Gui.Draw(spriteBatch);
                GuiBorder.Draw(spriteBatch);
                spriteBatch.Draw(GuiTextureHealth, GuiHealthPosition, guiHealthRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(GuiTextureEnergy, GuiEnergyPosition, guiEnergyRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(LifeTexture, LifePosition, LifeRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(ExperienceTexture, ExperiencePosition, ExperienceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "" + PlayerCredits, new Vector2(Gui.Position.X + (Gui.Sprite.Width / 2) - ((font.MeasureString("" + PlayerCredits).X / 2f) * 0.7f), Gui.Position.Y + 28), Color.White, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, "" + PlayerName, new Vector2(Gui.Position.X + (Gui.Sprite.Width / 2) - (font.MeasureString("" + PlayerName).X / 2), Gui.Position.Y + 4), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, "" + PlayerLevel, new Vector2(Gui.Position.X + (Gui.Sprite.Width / 2) - (font.MeasureString("" + PlayerLevel).X / 2), Gui.Position.Y + 46), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

                LevelUpParticles.Draw(spriteBatch);
            }
        }
    }
}
