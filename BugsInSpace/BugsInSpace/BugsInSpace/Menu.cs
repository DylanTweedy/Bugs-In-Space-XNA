using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BugsInSpace
{
    class Menu
    {
        #region Variables

        List<Enemy> enemies;
        MobileSprite Title;
        Texture2D TitleTexture;
        public float Scale;
        public bool GameStart;
        bool StartMoving;
        bool ProfilesMoving;
        bool OptionsMoving;
        bool QuitGameMoving;
        MobileSprite Start;
        MobileSprite QuitGame;
        MobileSprite Profiles;
        MobileSprite Options;
        Texture2D StartTexture;
        Texture2D QuitGameTexture;
        Texture2D ProfilesTexture;
        Texture2D OptionsTexture;
        Texture2D White;
        int AlphaValue;
        bool StartFade;
        bool FinishFade;
        Random random;
        TimeSpan enemiesSpawnTime;
        TimeSpan enemiesPreviousSpawnTime;
        GamePadState currentGamepad1State;
        GamePadState previousGamepad1State;
        int controlSystem;
        int MainMenuLocation;
        TimeSpan MoveTime1;
        TimeSpan previousMoveTime1;
        TimeSpan MoveTime2;
        TimeSpan previousMoveTime2;
        public bool Select;
        public int GameState;
        int MenuState;
        bool loadOptions;
        int OptionMenuLocation;
        MobileSprite FullScreen;
        MobileSprite Windowed;
        MobileSprite Resolution;
        MobileSprite ApplyChanges;
        Texture2D FullScreenTexture;
        Texture2D WindowedTexture;
        Texture2D ResolutionTexture;
        Texture2D ApplyChangesTexture;
        bool fullScreen;
        int fullScreenValue;
        SpriteFont font;
        public int resolution;
        bool restartMenu;
        KeyboardUpdate keyboardUpdate;
        bool ProfileInitialize;
        Profile profile;
        bool MusicStart;
        TimeSpan PreviousMusicTime;
        TimeSpan MusicTime;
        Color ResolutionColor;
        Texture2D HealthBarTexture;
        MobileSprite HealthBar;
        Texture2D SoundTexture;
        MobileSprite Sound;
        Texture2D MusicTexture;
        MobileSprite Music;
        Texture2D VolumeControlTexture;
        Rectangle SoundRectangle;
        Rectangle MusicRectangle;
        public float SoundVolumeValue;
        public float MusicVolumeValue;
        Color HealthBarColor;

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

        public void Initialize(float soundVolumeValue, float musicVolumeValue)
        {
            Scale = 0.01f;
            GameStart = false;
            StartMoving = true;
            ProfilesMoving = true;
            OptionsMoving = true;
            QuitGameMoving = true;
            AlphaValue = 0;
            StartFade = false;
            FinishFade = false;
            random = new Random();
            enemiesSpawnTime = TimeSpan.FromSeconds(0.2f);
            enemiesPreviousSpawnTime = TimeSpan.Zero;
            MoveTime1 = TimeSpan.FromSeconds(0.2);
            previousMoveTime1 = TimeSpan.Zero;
            MoveTime2 = TimeSpan.FromSeconds(0.2);
            previousMoveTime2 = TimeSpan.Zero;
            MusicTime = TimeSpan.FromSeconds(0.3);
            PreviousMusicTime = TimeSpan.Zero;
            MusicStart = true;
            MainMenuLocation = 1;
            controlSystem = 1;
            GameState = 0;
            MenuState = 1;
            loadOptions = false;
            OptionMenuLocation = 1;
            fullScreen = Game1.Instance.FullScreen;
            restartMenu = false;
            keyboardUpdate = new KeyboardUpdate();
            keyboardUpdate.Initialize(controlSystem, 0);
            ProfileInitialize = true;
            ResolutionColor = Color.White;
            if (fullScreen)
                fullScreenValue = 1;
            else
                fullScreenValue = 0;
            MusicVolumeValue = musicVolumeValue;
            SoundVolumeValue = soundVolumeValue;
        }

        public void LoadContent()
        {
            font = Game1.Instance.Content.Load<SpriteFont>("Fonts//Font1");

            TitleTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Title");
            Title = new MobileSprite(TitleTexture);
            Title.Sprite.AddAnimation("default", 0, 0, 512, 192, 1, 10f);
            Title.Sprite.CurrentAnimation = "default";
            Title.Position = new Vector2((screenWidth / 2) - (Title.Sprite.Width / 2), -(Title.Sprite.Height));
            Title.IsMoving = false;

            StartTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Start");
            Start = new MobileSprite(StartTexture);
            Start.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Start.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Start.Sprite.CurrentAnimation = "default";
            Start.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), screenHeight);
            Start.IsMoving = false;

            ProfilesTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Profiles");
            Profiles = new MobileSprite(ProfilesTexture);
            Profiles.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Profiles.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Profiles.Sprite.CurrentAnimation = "default";
            Profiles.Position = new Vector2((screenWidth / 2) - (Profiles.Sprite.Width / 2), (float)(screenHeight * 1.4));
            Profiles.IsMoving = false;

            OptionsTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Options");
            Options = new MobileSprite(OptionsTexture);
            Options.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Options.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Options.Sprite.CurrentAnimation = "default";
            Options.Position = new Vector2((screenWidth / 2) - (Options.Sprite.Width / 2), (float)(screenHeight * 1.8));
            Options.IsMoving = false;

            QuitGameTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//QuitGame");
            QuitGame = new MobileSprite(QuitGameTexture);
            QuitGame.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            QuitGame.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            QuitGame.Sprite.CurrentAnimation = "default";
            QuitGame.Position = new Vector2((screenWidth / 2) - (QuitGame.Sprite.Width / 2), (float)(screenHeight * 2.2));
            QuitGame.IsMoving = false;

            White = Game1.Instance.Content.Load<Texture2D>("Images//Menu//White");

            enemies = new List<Enemy>();
        }

        private void LoadOptions()
        {
            ResolutionTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Resolution");
            Resolution = new MobileSprite(ResolutionTexture);
            Resolution.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Resolution.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Resolution.Sprite.CurrentAnimation = "default";
            Resolution.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (screenHeight / 4) + 20);
            Resolution.IsMoving = false;

            FullScreenTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//FullScreen");
            FullScreen = new MobileSprite(FullScreenTexture);
            FullScreen.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            FullScreen.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            FullScreen.Sprite.CurrentAnimation = "default";
            FullScreen.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (float)((screenHeight / 4) + (Profiles.Sprite.Height / 1.5) * 2) + 20);
            FullScreen.IsMoving = false;

            WindowedTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Windowed");
            Windowed = new MobileSprite(WindowedTexture);
            Windowed.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Windowed.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Windowed.Sprite.CurrentAnimation = "default";
            Windowed.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (float)((screenHeight / 4) + (Profiles.Sprite.Height / 1.5) * 2) + 20);
            Windowed.IsMoving = false;

            HealthBarTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//HealthBar");
            HealthBar = new MobileSprite(HealthBarTexture);
            HealthBar.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            HealthBar.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            HealthBar.Sprite.CurrentAnimation = "default";
            HealthBar.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (float)((screenHeight / 4) + (Options.Sprite.Height / 1.5) * 4) + 20);
            HealthBar.IsMoving = false;

            SoundTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Sound");
            Sound = new MobileSprite(SoundTexture);
            Sound.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Sound.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Sound.Sprite.CurrentAnimation = "default";
            Sound.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (float)((screenHeight / 4) + (Options.Sprite.Height / 1.5) * 6) + 20);
            Sound.IsMoving = false;

            MusicTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//Music");
            Music = new MobileSprite(MusicTexture);
            Music.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            Music.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            Music.Sprite.CurrentAnimation = "default";
            Music.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (float)((screenHeight / 4) + (Options.Sprite.Height / 1.5) * 8) + 20);
            Music.IsMoving = false;

            ApplyChangesTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//ApplyChanges");
            ApplyChanges = new MobileSprite(ApplyChangesTexture);
            ApplyChanges.Sprite.AddAnimation("selected", 0, 64, 391, 64, 1, 10f);
            ApplyChanges.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            ApplyChanges.Sprite.CurrentAnimation = "default";
            ApplyChanges.Position = new Vector2((screenWidth / 2) - (Start.Sprite.Width / 2), (float)((screenHeight / 4) + (Options.Sprite.Height / 1.5) * 10) + 20);
            ApplyChanges.IsMoving = false;

            VolumeControlTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//VolumeControl");

            SoundRectangle = new Rectangle(0, 0, VolumeControlTexture.Width, VolumeControlTexture.Height);
            MusicRectangle = new Rectangle(0, 0, VolumeControlTexture.Width, VolumeControlTexture.Height);

            fullScreen = Game1.Instance.FullScreen;
            if (fullScreen)
                fullScreenValue = 1;
            else
                fullScreenValue = 0;

            for (int d = 0; d < Game1.Instance.DisplaysWidth.Count; d++)
            {
                if ((Game1.Instance.DisplaysWidth[d] == Game1.Instance.currentWidth) && (Game1.Instance.DisplaysHeight[d] == Game1.Instance.currentHeight))
                {
                    resolution = d;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (MenuState != 3)
            {
                #region MenuState 1

                if (MenuState == 1)
                {
                    if (restartMenu)
                    {
                        Title.Sprite.SetPosY(screenHeight / 100);
                        Start.Sprite.SetPosY((screenHeight / 3) + 20);
                        Profiles.Sprite.SetPosY((int)((screenHeight / 3) + (Profiles.Sprite.Height / 1.5) * 2) + 20);
                        Options.Sprite.SetPosY((int)((screenHeight / 3) + (Options.Sprite.Height / 1.5) * 4) + 20);
                        QuitGame.Sprite.SetPosY((int)((screenHeight / 3) + (QuitGame.Sprite.Height / 1.5) * 6) + 20);
                        Title.Sprite.SetPosX((screenWidth / 2) - (Title.Sprite.Width / 2));
                        Start.Sprite.SetPosX((screenWidth / 2) - (Start.Sprite.Width / 2));
                        Profiles.Sprite.SetPosX((screenWidth / 2) - (Profiles.Sprite.Width / 2));
                        Options.Sprite.SetPosX((screenWidth / 2) - (Options.Sprite.Width / 2));
                        QuitGame.Sprite.SetPosX((screenWidth / 2) - (QuitGame.Sprite.Width / 2));
                        ProfileInitialize = true;
                        restartMenu = false;
                    }
                    if (GameStart == false)
                    {
                        Startup();
                    }

                    if (FinishFade)
                    {
                        AlphaValue = 0;
                        FinishFade = false;
                        PreviousMusicTime = gameTime.TotalGameTime;
                    }

                    Title.Update(gameTime);
                    Start.Update(gameTime);
                    Profiles.Update(gameTime);
                    Options.Update(gameTime);
                    QuitGame.Update(gameTime);
                }

                #endregion

                if (MusicStart && GameStart && gameTime.TotalGameTime - PreviousMusicTime > MusicTime)
                {
                    Game1.Instance.AudioPlay("Intro", 2);
                    MusicStart = false;
                }

                ControlsUpdate(gameTime);
                if (GameStart)
                    Navigation(gameTime);

                if (fullScreenValue == 1)
                    fullScreen = true;
                if (fullScreenValue == 0)
                    fullScreen = false;
            }

            if (MenuState == 3)
            {
                if (ProfileInitialize)
                {
                    ProfileInitialize = false;
                    profile = new Profile();
                    profile.Initialize(controlSystem);
                    profile.LoadContent();
                }
                profile.Update(gameTime);
                MenuState = profile.MainMenuState;
            }

            #region Enemies

            if (gameTime.TotalGameTime - enemiesPreviousSpawnTime > enemiesSpawnTime && enemies.Count < 25)
            {
                enemiesPreviousSpawnTime = gameTime.TotalGameTime;
                AddEnemy();
            }

            for (int e = 0; e < enemies.Count; e++)
            {
                enemies[e].Update(gameTime);

                for (int p = 0; p < enemies[e].projectiles.Count; p++)
                {
                    for (int e2 = 0; e2 < enemies.Count; e2++)
                    {
                        enemies[e2].EnemyBody.VerticalCollisionBuffer = 16;
                        enemies[e2].EnemyBody.HorizontalCollisionBuffer = 16;

                        if (enemies[e].projectiles[p].projectile.CollisionBox.Intersects(enemies[e2].EnemyBody.CollisionBox) && enemies[e].projectiles[p].active && e != e2)
                        {
                            Game1.Instance.AudioPlay(enemies[e].projectiles[p].hitSound, 3);
                            enemies[e].projectiles[p].active = false;
                            enemies[e2].Health = 0;
                        }
                    }
                }

                if (enemies[e].Active == false && enemies[e].deathParticles.particles.Count == 0 && enemies[e].projectiles.Count == 0)
                {
                    enemies.RemoveAt(e);
                }
            }

            #endregion
        }

        public void Startup()
        {
            if (Scale < 0.9f)
            {
                Title.Sprite.MoveY(3f);
                Scale += 0.01f;
                Title.Sprite.Scale(Scale);
            }

            if (Title.Position.Y >= (screenHeight / 100))
            {
                Title.Sprite.MoveY(0);
                Title.Sprite.velocity = new Vector2(0, 0);
                Title.Sprite.SetPosY(screenHeight / 100);
            }

            if (Start.Position.Y > (screenHeight / 3) + 20 && StartMoving)
            {
                Start.Sprite.MoveY(-7f);
            }

            if (Profiles.Position.Y > (float)((screenHeight / 3) + (Profiles.Sprite.Height / 1.5) * 2) + 20 && ProfilesMoving)
            {
                Profiles.Sprite.MoveY(-7f);
            }

            if (Options.Position.Y > (float)((screenHeight / 3) + (Options.Sprite.Height / 1.5) * 4) + 20 && OptionsMoving)
            {
                Options.Sprite.MoveY(-7f);
            }

            if (QuitGame.Position.Y > (float)((screenHeight / 3) + (QuitGame.Sprite.Height / 1.5) * 6) + 20 && QuitGameMoving)
            {
                QuitGame.Sprite.MoveY(-7f);
            }

            if (Start.Position.Y < (screenHeight / 3) + 20)
            {
                Game1.Instance.AudioPlay("ButtonMove", 1);
                Start.Sprite.MoveY(0);
                Start.Sprite.velocity = new Vector2(0, 0);
                Start.Sprite.SetPosY((screenHeight / 3) + 20);
                StartMoving = false;
            }

            if (Profiles.Position.Y < (float)((screenHeight / 3) + (Profiles.Sprite.Height / 1.5) * 2) + 20 && ProfilesMoving)
            {
                Game1.Instance.AudioPlay("ButtonMove", 1);
                Profiles.Sprite.MoveY(0);
                Profiles.Sprite.velocity = new Vector2(0, 0);
                Profiles.Sprite.SetPosY((int)((screenHeight / 3) + (Profiles.Sprite.Height / 1.5) * 2) + 20);
                ProfilesMoving = false;
            }

            if (Options.Position.Y < (float)((screenHeight / 3) + (Options.Sprite.Height / 1.5) * 4) + 20 && OptionsMoving)
            {
                Game1.Instance.AudioPlay("ButtonMove", 1);
                Options.Sprite.MoveY(0);
                Options.Sprite.velocity = new Vector2(0, 0);
                Options.Sprite.SetPosY((int)((screenHeight / 3) + (Options.Sprite.Height / 1.5) * 4) + 20);
                OptionsMoving = false;
            }

            if (QuitGame.Position.Y < (float)((screenHeight / 3) + (QuitGame.Sprite.Height / 1.5) * 6) + 20 && QuitGameMoving)
            {
                Game1.Instance.AudioPlay("ButtonMove", 1);
                Game1.Instance.AudioPlay("Fade", 1);
                QuitGame.Sprite.MoveY(0);
                QuitGame.Sprite.velocity = new Vector2(0, 0);
                QuitGame.Sprite.SetPosY((int)((screenHeight / 3) + (QuitGame.Sprite.Height / 1.5) * 6) + 20);
                QuitGameMoving = false;
                StartFade = true;
            }

            if (StartFade)
            {
                AlphaValue += 5;
                if (AlphaValue >= 255)
                {
                    GameStart = true;
                    StartFade = false;
                    FinishFade = true;
                }
            }
        }

        public void ControlsUpdate(GameTime gameTime)
        {
            previousGamepad1State = currentGamepad1State;
            currentGamepad1State = GamePad.GetState(PlayerIndex.One);

            if (currentGamepad1State.IsConnected)
            {
                controlSystem = 2;
                keyboardUpdate.controlSystem = 2;
            }
            else
            {
                keyboardUpdate.controlSystem = 1;
                controlSystem = 1;
            }

            keyboardUpdate.Update(gameTime);

            if (GameStart)
            {
                if (keyboardUpdate.SelectLeft)
                {
                    if (MenuState == 2)
                    {
                        if (FullScreen.Sprite.CurrentAnimation == "selected" || Windowed.Sprite.CurrentAnimation == "selected")
                        {
                            fullScreenValue -= 1;
                            Game1.Instance.AudioPlay("Select", 1);
                        }

                        if (Resolution.Sprite.CurrentAnimation == "selected")
                        {
                            resolution -= 1;
                            Game1.Instance.AudioPlay("Select", 1);
                        }
                    }
                }

                if (keyboardUpdate.SelectRight)
                {
                    if (MenuState == 2)
                    {
                        if (FullScreen.Sprite.CurrentAnimation == "selected" || Windowed.Sprite.CurrentAnimation == "selected")
                        {
                            fullScreenValue += 1;
                            Game1.Instance.AudioPlay("Select", 1);
                        }

                        if (Resolution.Sprite.CurrentAnimation == "selected")
                        {
                            resolution += 1;
                            Game1.Instance.AudioPlay("Select", 1);
                        }
                    }
                }

                if (keyboardUpdate.SelectUp)
                {
                    MainMenuLocation -= 1;
                    OptionMenuLocation -= 1;
                    Game1.Instance.AudioPlay("Select", 1);
                }

                if (keyboardUpdate.SelectDown)
                {
                    MainMenuLocation += 1;
                    OptionMenuLocation += 1;
                    Game1.Instance.AudioPlay("Select", 1);
                }

                if (keyboardUpdate.Back)
                {
                    if (MenuState == 2)
                    {
                        MenuState = 1;
                        MainMenuLocation = 1;
                        Game1.Instance.AudioPlay("Select", 1);
                    }

                    GameState = 0;
                }

                Select = keyboardUpdate.Select;
            }
        }

        public void Navigation(GameTime gameTime)
        {
            if (MenuState == 1)
            {
                if (MainMenuLocation > 4)
                    MainMenuLocation = 1;
                if (MainMenuLocation < 1)
                    MainMenuLocation = 4;
            }
            else
                MainMenuLocation = 0;

            if (MenuState == 2)
            {
                if (OptionMenuLocation > 6)
                    OptionMenuLocation = 1;
                if (OptionMenuLocation < 1)
                    OptionMenuLocation = 6;
            }
            else
                OptionMenuLocation = 0;

            if (fullScreenValue > 1)
                fullScreenValue = 0;
            if (fullScreenValue < 0)
                fullScreenValue = 1;

            if (resolution < 0)
                resolution = Game1.Instance.DisplaysWidth.Count - 1;
            if (resolution > Game1.Instance.DisplaysWidth.Count - 1)
                resolution = 0;

            if (MenuState == 1)
            {

                #region Start

                if (MainMenuLocation == 1)
                {
                    Start.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    Start.Sprite.CurrentAnimation = "default";
                }

                if (Start.Sprite.CurrentAnimation == "selected" && Select)
                {
                    Select = false;
                    MainMenuLocation = 1;
                    restartMenu = true;
                    GameState = 1;
                    Game1.Instance.AudioPlay("Selected", 1);
                }

                #endregion

                #region Profiles

                if (MainMenuLocation == 2)
                {
                    Profiles.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    Profiles.Sprite.CurrentAnimation = "default";
                }

                if (Profiles.Sprite.CurrentAnimation == "selected" && Select)
                {
                    Select = false;
                    MainMenuLocation = 1;
                    MenuState = 3;
                    restartMenu = true;
                    Game1.Instance.AudioPlay("Selected", 1);
                    Game1.Instance.controlSystem = controlSystem;
                }

                #endregion

                #region Options

                if (MainMenuLocation == 3)
                {
                    Options.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    Options.Sprite.CurrentAnimation = "default";
                }

                if (Options.Sprite.CurrentAnimation == "selected" && Select)
                {
                    MenuState = 2;
                    loadOptions = true;
                    OptionMenuLocation = 1;
                    Game1.Instance.AudioPlay("Selected", 1);
                }

                #endregion

                #region Quit Game

                if (MainMenuLocation == 4)
                {
                    QuitGame.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    QuitGame.Sprite.CurrentAnimation = "default";
                }

                if (QuitGame.Sprite.CurrentAnimation == "selected" && Select)
                {
                    Game1.Instance.AudioPlay("Back", 1);
                    Game1.Instance.Exit();
                }

                #endregion

            }

            if (MenuState == 2)

            {
                if (loadOptions)
                {
                    LoadOptions();
                    loadOptions = false;
                }

                if (OptionMenuLocation == 1)
                {
                    Resolution.Sprite.CurrentAnimation = "selected";
                    ResolutionColor = Color.Red;
                }
                else
                {
                    Resolution.Sprite.CurrentAnimation = "default";
                    ResolutionColor = Color.White;
                }

                if (OptionMenuLocation == 2)
                {
                    FullScreen.Sprite.CurrentAnimation = "selected";
                    Windowed.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    FullScreen.Sprite.CurrentAnimation = "default";
                    Windowed.Sprite.CurrentAnimation = "default";
                }
                if ((FullScreen.Sprite.CurrentAnimation == "selected" || Windowed.Sprite.CurrentAnimation == "selected") && Select)
                {
                    Select = false;
                    fullScreenValue += 1;
                }

                if (OptionMenuLocation == 3)
                {
                    HealthBar.Sprite.CurrentAnimation = "selected";
                    HealthBarColor = Color.Red;
                }
                else
                {
                    HealthBar.Sprite.CurrentAnimation = "default";
                    HealthBarColor = Color.White;
                }
                if (HealthBar.Sprite.CurrentAnimation == "selected" && (Select || keyboardUpdate.SelectLeft || keyboardUpdate.SelectRight))
                {
                    Select = false;

                    Game1.Instance.AudioPlay("Selected", 1);

                    if (Game1.Instance.ShowHealthBars)
                        Game1.Instance.ShowHealthBars = false;
                    else
                        Game1.Instance.ShowHealthBars = true;
                }

                if (OptionMenuLocation == 4)
                {
                    Sound.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    Sound.Sprite.CurrentAnimation = "default";
                }
                if (Sound.Sprite.CurrentAnimation == "selected")
                {
                    if (keyboardUpdate.SelectRight && SoundVolumeValue != 1f)
                    {
                        if (SoundVolumeValue == 0f)
                            SoundVolumeValue = 0.1f;
                        else if (SoundVolumeValue == 0.1f)
                            SoundVolumeValue = 0.2f;
                        else if (SoundVolumeValue == 0.2f)
                            SoundVolumeValue = 0.3f;
                        else if (SoundVolumeValue == 0.3f)
                            SoundVolumeValue = 0.4f;
                        else if (SoundVolumeValue == 0.4f)
                            SoundVolumeValue = 0.5f;
                        else if (SoundVolumeValue == 0.5f)
                            SoundVolumeValue = 0.6f;
                        else if (SoundVolumeValue == 0.6f)
                            SoundVolumeValue = 0.7f;
                        else if (SoundVolumeValue == 0.7f)
                            SoundVolumeValue = 0.8f;
                        else if (SoundVolumeValue == 0.8f)
                            SoundVolumeValue = 0.9f;
                        else if (SoundVolumeValue == 0.9f)
                            SoundVolumeValue = 1f;

                        Game1.Instance.AudioPlay("Selected", 1);
                    }

                    if (keyboardUpdate.SelectLeft && SoundVolumeValue != 0f)
                    {
                        if (SoundVolumeValue == 1f)
                            SoundVolumeValue = 0.9f;
                        else if (SoundVolumeValue == 0.9f)
                            SoundVolumeValue = 0.8f;
                        else if (SoundVolumeValue == 0.8f)
                            SoundVolumeValue = 0.7f;
                        else if (SoundVolumeValue == 0.7f)
                            SoundVolumeValue = 0.6f;
                        else if (SoundVolumeValue == 0.6f)
                            SoundVolumeValue = 0.5f;
                        else if (SoundVolumeValue == 0.5f)
                            SoundVolumeValue = 0.4f;
                        else if (SoundVolumeValue == 0.4f)
                            SoundVolumeValue = 0.3f;
                        else if (SoundVolumeValue == 0.3f)
                            SoundVolumeValue = 0.2f;
                        else if (SoundVolumeValue == 0.2f)
                            SoundVolumeValue = 0.1f;
                        else if (SoundVolumeValue == 0.1f)
                            SoundVolumeValue = 0f;

                        Game1.Instance.AudioPlay("Selected", 1);
                    }

                    if (SoundVolumeValue > 1f)
                        SoundVolumeValue = 1f;
                    if (SoundVolumeValue < 0f)
                        SoundVolumeValue = 0f;
                }

                SoundRectangle.Width = (int)((VolumeControlTexture.Width / 10) * (SoundVolumeValue * 10));

                if (OptionMenuLocation == 5)
                {
                    Music.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    Music.Sprite.CurrentAnimation = "default";
                }
                if (Music.Sprite.CurrentAnimation == "selected")
                {
                    if (keyboardUpdate.SelectRight && MusicVolumeValue != 1f)
                    {
                        if (MusicVolumeValue == 0f)
                            MusicVolumeValue = 0.1f;
                        else if (MusicVolumeValue == 0.1f)
                            MusicVolumeValue = 0.2f;
                        else if (MusicVolumeValue == 0.2f)
                            MusicVolumeValue = 0.3f;
                        else if (MusicVolumeValue == 0.3f)
                            MusicVolumeValue = 0.4f;
                        else if (MusicVolumeValue == 0.4f)
                            MusicVolumeValue = 0.5f;
                        else if (MusicVolumeValue == 0.5f)
                            MusicVolumeValue = 0.6f;
                        else if (MusicVolumeValue == 0.6f)
                            MusicVolumeValue = 0.7f;
                        else if (MusicVolumeValue == 0.7f)
                            MusicVolumeValue = 0.8f;
                        else if (MusicVolumeValue == 0.8f)
                            MusicVolumeValue = 0.9f;
                        else if (MusicVolumeValue == 0.9f)
                            MusicVolumeValue = 1f;

                        Game1.Instance.AudioPlay("Selected", 1);
                    }

                    if (keyboardUpdate.SelectLeft && MusicVolumeValue != 0f)
                    {
                        if (MusicVolumeValue == 1f)
                            MusicVolumeValue = 0.9f;
                        else if (MusicVolumeValue == 0.9f)
                            MusicVolumeValue = 0.8f;
                        else if (MusicVolumeValue == 0.8f)
                            MusicVolumeValue = 0.7f;
                        else if (MusicVolumeValue == 0.7f)
                            MusicVolumeValue = 0.6f;
                        else if (MusicVolumeValue == 0.6f)
                            MusicVolumeValue = 0.5f;
                        else if (MusicVolumeValue == 0.5f)
                            MusicVolumeValue = 0.4f;
                        else if (MusicVolumeValue == 0.4f)
                            MusicVolumeValue = 0.3f;
                        else if (MusicVolumeValue == 0.3f)
                            MusicVolumeValue = 0.2f;
                        else if (MusicVolumeValue == 0.2f)
                            MusicVolumeValue = 0.1f;
                        else if (MusicVolumeValue == 0.1f)
                            MusicVolumeValue = 0f;

                        Game1.Instance.AudioPlay("Selected", 1);
                    }

                    if (MusicVolumeValue > 1f)
                        MusicVolumeValue = 1f;
                    if (MusicVolumeValue < 0f)
                        MusicVolumeValue = 0f;
                }

                Game1.Instance.SoundVolume = SoundVolumeValue;
                Game1.Instance.MusicVolume = MusicVolumeValue;

                MusicRectangle.Width = (int)((VolumeControlTexture.Width / 10) * (MusicVolumeValue * 10));

                if (OptionMenuLocation == 6)
                {
                    ApplyChanges.Sprite.CurrentAnimation = "selected";
                }
                else
                {
                    ApplyChanges.Sprite.CurrentAnimation = "default";
                }

                if (ApplyChanges.Sprite.CurrentAnimation == "selected" && Select)
                {
                    MenuState = 1;
                    
                    MainMenuLocation = 1;
                    Game1.Instance.AudioPlay("Selected", 1);
                    Game1.Instance.currentWidth = Game1.Instance.DisplaysWidth[resolution];
                    Game1.Instance.currentHeight = Game1.Instance.DisplaysHeight[resolution];
                    Game1.Instance.FullScreen = fullScreen;
                    Game1.Instance.savingPreferences = true;
                    restartMenu = true;
                }
            }
        }

        public void AddEnemy()
        {
            Enemy enemy = new Enemy();
            enemy.Initialize(random.Next(1, enemy.EnemyTypes + 1), 0, 0, 1, true, 15, 50);
            enemy.scale = (float)((float)random.Next(5, 7) / 10);
            enemy.LoadContent(new Vector2(random.Next(0, screenWidth), 0));
            enemy.EnemyBody.Sprite.SetPosY(-enemy.EnemyBody.Sprite.Height);
            enemy.customMovement = true;
            enemy.EnemyProjectile = random.Next(1, 12);
            enemy.EnemyFireStyle = 0;
            enemies.Add(enemy);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameStart)
            {
                for (int e = 0; e < enemies.Count; e++)
                {
                    enemies[e].Draw(spriteBatch);
                }
            }

            if (MenuState != 3)
            {
                if (MenuState == 1)
                {
                    Title.Draw(spriteBatch);
                    Start.Draw(spriteBatch);
                    Options.Draw(spriteBatch);
                    Profiles.Draw(spriteBatch);
                    QuitGame.Draw(spriteBatch);
                    spriteBatch.Draw(White, new Vector2((screenWidth / 2) - (White.Width / 2)), new Color(AlphaValue, AlphaValue, AlphaValue, AlphaValue));
                }

                if (MenuState == 2)
                {
                    Title.Draw(spriteBatch);
                    spriteBatch.DrawString(font, Game1.Instance.DisplaysWidth[resolution] + " x " + Game1.Instance.DisplaysHeight[resolution], new Vector2((Resolution.Position.X + (Resolution.Sprite.Width / 2)) - (font.MeasureString(Game1.Instance.DisplaysWidth[resolution] + " x " + Game1.Instance.DisplaysHeight[resolution]).X / 2), (Resolution.Position.Y + Resolution.Sprite.Height) + 2), ResolutionColor);
                    Resolution.Draw(spriteBatch);
                    if (fullScreenValue == 1)
                        FullScreen.Draw(spriteBatch);
                    if (fullScreenValue == 0)
                        Windowed.Draw(spriteBatch);
                    HealthBar.Draw(spriteBatch);
                    if (Game1.Instance.ShowHealthBars)
                        spriteBatch.DrawString(font, "On", new Vector2((HealthBar.Position.X + (HealthBar.Sprite.Width / 2)) - (font.MeasureString("On").X / 2), (HealthBar.Position.Y + HealthBar.Sprite.Height) + 2), HealthBarColor);
                    else
                        spriteBatch.DrawString(font, "Off", new Vector2((HealthBar.Position.X + (HealthBar.Sprite.Width / 2)) - (font.MeasureString("Off").X / 2), (HealthBar.Position.Y + HealthBar.Sprite.Height) + 2), HealthBarColor);
                    Sound.Draw(spriteBatch);
                    spriteBatch.Draw(VolumeControlTexture, new Vector2(Sound.Position.X, (Sound.Position.Y + Sound.Sprite.Height) + 2), SoundRectangle, Color.White); 
                    Music.Draw(spriteBatch);
                    spriteBatch.Draw(VolumeControlTexture, new Vector2(Music.Position.X, (Music.Position.Y + Music.Sprite.Height) + 2), MusicRectangle, Color.White); 
                    ApplyChanges.Draw(spriteBatch);
                }
            }

            if (MenuState == 3)
            {
                profile.Draw(spriteBatch);
            }
        }
    }
}
