using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BugsInSpace
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {               
        #region Variables

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Game1 Instance;
        ParallaxingBackground background;
        Menu menu;
        PlayerManager players;
        SpriteFont font;
        GUI gui;
        public bool Player1Initialized;
        public bool Player2Initialized;
        public bool Player3Initialized;
        public bool Player4Initialized;
        public bool Player5Initialized;
        public int GameState;
        bool GameStart;
        bool GameInitialize;
        SaveManager saveManager;
        public bool Saving;
        SavePreferences savePreferences;
        public List<int> DisplaysWidth;
        public List<int> DisplaysHeight;
        public bool FullScreen;
        public bool savingPreferences;
        public int currentWidth;
        public int currentHeight;
        public int controlSystem;
        public float speed;
        public bool ShowHealthBars;
        public float SoundVolume;
        public float MusicVolume;
        public Vector2 ViewportPositionOne;
        public Vector2 ViewportPositionTwo;
        Texture2D Marker;
        CameraManager cameraManager;

        #endregion

        #region Audio Variables

        AudioEngine audioEngine;
        WaveBank MiscWave;
        SoundBank MiscSound;
        WaveBank MusicWave;
        SoundBank MusicSound;
        WaveBank ProjectileWave;
        SoundBank ProjectileSound;
        WaveBank EventWave;
        SoundBank EventSound;
        Cue Sound;
        AudioCategory SoundAudio;
        AudioCategory MusicAudio;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            Instance = this;

            //Components.Add(new GamerServicesComponent(this));

        }

        protected override void Initialize()
        {
            GameState = 0;

            saveManager = new SaveManager();
            Saving = false;
            savingPreferences = false;
            savePreferences = new SavePreferences();
            InitializePreferences();

            background = new ParallaxingBackground();
            background.Initialize();

            GameStart = false;

            menu = new Menu();
            menu.Initialize(SoundVolume, MusicVolume);

            DisplaysWidth = new List<int>();
            DisplaysHeight = new List<int>();
            currentWidth = graphics.PreferredBackBufferWidth;
            currentHeight = graphics.PreferredBackBufferHeight;

            speed = 0.25f;

            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (DisplaysWidth.Contains(mode.Width) && DisplaysHeight.Contains(mode.Height) || mode.Width < 1024)
                {
                }
                else
                {
                    DisplaysWidth.Add(mode.Width);
                    DisplaysHeight.Add(mode.Height);
                }
            }

            ViewportPositionOne = Vector2.Zero;
            ViewportPositionTwo = Vector2.Zero;

            cameraManager = new CameraManager();
            cameraManager.Initialize(GraphicsDevice.Viewport);
            
            base.Initialize();
        }

        private void InitializePreferences()
        {
            savePreferences.Initialize();
            savePreferences.InitiateLoad();

            if (savePreferences.Available == true)
            {
                graphics.PreferredBackBufferWidth = savePreferences.backBufferWidth;
                graphics.PreferredBackBufferHeight = savePreferences.backBufferHeight;
                graphics.IsFullScreen = savePreferences.fullScreen;
                ShowHealthBars = savePreferences.showHealthBars;
                SoundVolume = savePreferences.soundVolume;
                MusicVolume = savePreferences.musicVolume;
                graphics.ApplyChanges();
            }
            else
            {
                SoundVolume = 1f;
                MusicVolume = 1f;
                ShowHealthBars = false;
            }

            if (graphics.IsFullScreen == true)
                FullScreen = true;
            else
                FullScreen = false;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts//Font1");
            Marker = Content.Load<Texture2D>("Images//Marker");

            background.LoadContent();
            LoadAudio();

            if (GameState == 0)
            {
                menu.LoadContent();
            }
        }

        private void LoadAudio()
        {
            audioEngine = new AudioEngine("Content//Audio//Audio.xgs");
            MiscWave = new WaveBank(audioEngine, "Content//Audio//Misc Wave.xwb");
            MiscSound = new SoundBank(audioEngine, "Content//Audio//Misc Sound.xsb");
            MusicWave = new WaveBank(audioEngine, "Content//Audio//Music Wave.xwb");
            MusicSound = new SoundBank(audioEngine, "Content//Audio//Music Sound.xsb");
            ProjectileWave = new WaveBank(audioEngine, "Content//Audio//Projectile Wave.xwb");
            ProjectileSound = new SoundBank(audioEngine, "Content//Audio//Projectile Sound.xsb");
            EventWave = new WaveBank(audioEngine, "Content//Audio//Event Wave.xwb");
            EventSound = new SoundBank(audioEngine, "Content//Audio//Event Sound.xsb");

            MusicAudio = audioEngine.GetCategory("Music");
            SoundAudio = audioEngine.GetCategory("Default");

            Sound = MusicSound.GetCue("Background");
            Sound.Play();
        }

        protected override void UnloadContent()
        {
        }

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        protected override void Update(GameTime gameTime)
        {

            if (GameState != 2)
                background.Update(gameTime);

            background.speed = speed;

            MusicAudio.SetVolume(MusicVolume);
            SoundAudio.SetVolume(SoundVolume);

            if (GameState == 0)
            {
                menu.Update(gameTime);

                if (menu.GameStart == true)
                {
                    GameStart = true;
                    background.Initializing = false;
                }

                GameState = menu.GameState;
                GameInitialize = GameStart;
            }

            if (GameState == 1)
            {
                if (GameInitialize)
                {
                    GameInitialize = false;

                    players = new PlayerManager();
                    players.Initialize();
                    gui = new GUI();
                    players.LoadContent();
                }

                players.Update(gameTime);

                GameState = players.GameState;

                cameraManager.FocusPoints = players.players.Count;
            }


            cameraManager.Update(gameTime);

            if (GameState == 1)
            {
                previousKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();


                for (int i = 0; i < cameraManager.CameraCount; i++)
                {
                    if (currentKeyboardState.IsKeyDown(Keys.U))
                        cameraManager.ZoomValues[i] += 0.01f;
                    if (currentKeyboardState.IsKeyDown(Keys.J))
                        cameraManager.ZoomValues[i] -= 0.01f;

                    if (currentKeyboardState.IsKeyDown(Keys.H))
                        cameraManager.RotationValues[i] -= 0.01f;
                    if (currentKeyboardState.IsKeyDown(Keys.K))
                        cameraManager.RotationValues[i] += 0.01f;

                    if (currentKeyboardState.IsKeyDown(Keys.I))
                        cameraManager.ControlInstant(5f, i, Vector2.Zero, 0f, 0.5f);
                    if (currentKeyboardState.IsKeyDown(Keys.Y))
                        cameraManager.ControlSlide(5f, i, new Vector2(currentWidth, currentHeight), 0f, 1.3f);
                    if (currentKeyboardState.IsKeyDown(Keys.L))
                        cameraManager.ControlShake(5f, i, 20);


                    if (currentKeyboardState.IsKeyDown(Keys.NumPad6) && previousKeyboardState.IsKeyUp(Keys.NumPad6))
                        cameraManager.Spacing += 1;
                    if (currentKeyboardState.IsKeyDown(Keys.NumPad4) && previousKeyboardState.IsKeyUp(Keys.NumPad4))
                        cameraManager.Spacing -= 1;
                }

<<<<<<< HEAD
                Console.Clear();
                Console.WriteLine(cameraManager.Spacing);
=======
                //Console.Clear();
                //Console.WriteLine(cameraManager.Spacing);
>>>>>>> Camera experiment.

                if (currentKeyboardState.IsKeyDown(Keys.O) && previousKeyboardState.IsKeyUp(Keys.O) && cameraManager.CameraLayout == 0)
                {
                    cameraManager.CameraLayout = 1;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.O) && previousKeyboardState.IsKeyUp(Keys.O) && cameraManager.CameraLayout == 1)
                {
                    cameraManager.CameraLayout = 0;
                }

                for (int i = 0; i < players.players.Count; i++)
                {
                    //if (players.players[0].FinalAcceleration > 0)
                    //{
                    //    cameraManager.ControlShake(0f, 0, (int)players.players[0].FinalAcceleration);
                    //}

                    cameraManager.FocusValues[i] = new Vector2(players.players[i].Position.X + (players.players[i].Width / 2), players.players[i].Position.Y + (players.players[i].Height / 2));
                }
            }

            if (GameState == 2)
            {
                players.Update(gameTime);
                GameState = players.GameState;
            }

            if (Saving)
            {
                Save();
            }

            if (savingPreferences)
            {
                SavingPreferences();
            }

            base.Update(gameTime);
        }

        public void AudioPlay(string SoundName, int Function)
        {
            if (Function == 1)
                Sound = MiscSound.GetCue(SoundName);
            if (Function == 2)
                Sound = MusicSound.GetCue(SoundName);
            if (Function == 3)
                Sound = ProjectileSound.GetCue(SoundName);
            if (Function == 4)
                Sound = EventSound.GetCue(SoundName);
            Sound.Play();
        }

        private void SavingPreferences()
        {
            graphics.IsFullScreen = FullScreen;
            graphics.PreferredBackBufferWidth = currentWidth;
            graphics.PreferredBackBufferHeight = currentHeight;
            graphics.ApplyChanges();
            savePreferences.fullScreen = FullScreen;
            savePreferences.backBufferWidth = currentWidth;
            savePreferences.backBufferHeight = currentHeight;
            savePreferences.showHealthBars = ShowHealthBars;
            savePreferences.soundVolume = SoundVolume;
            savePreferences.musicVolume = MusicVolume;
            savePreferences.InitiateSave();
            savingPreferences = false;
            background.Initializing = true;
            background.InitializeOnce = true;
        }

        private void Save()
        {
            for (int p = 0; p < players.players.Count; p++)
            {
                saveManager.Initialize();

                saveManager.playerName = players.players[p].PlayerName;
                saveManager.playerAcceleration = players.players[p].PlayerAcceleration;
                saveManager.playerBulletSpeed = players.players[p].PlayerBulletSpeed;
                saveManager.playerShip = players.players[p].PlayerShip;
                saveManager.playerMaxBullets = players.players[p].PlayerMaxBullets;
                saveManager.playerDamage = players.players[p].PlayerDamage;
                saveManager.playerRedValue = players.players[p].PlayerRedValue;
                saveManager.playerBlueValue = players.players[p].PlayerBlueValue;
                saveManager.playerGreenValue = players.players[p].PlayerGreenValue;
                saveManager.playerFireRate = players.players[p].PlayerFireRate;
                saveManager.playerCredits = players.players[p].PlayerCredits;
                saveManager.playerLives = players.players[p].PlayerLives;
                saveManager.playerMaxHealth = players.players[p].PlayerMaxHealth;
                saveManager.playerMaxEnergy = players.players[p].PlayerMaxEnergy;

                saveManager.iplayerAmmo = players.players[p].iPlayerAmmo;
                saveManager.bplayerAutoFire = players.players[p].bPlayerAutoFire;
                saveManager.iplayerBulletSpeed = players.players[p].iPlayerBulletSpeed;
                saveManager.iplayerDamage = players.players[p].iPlayerDamage;
                saveManager.iplayerElectricProjectile = players.players[p].iPlayerElectricProjectile;
                saveManager.iplayerEnergy = players.players[p].iPlayerEnergy;
                saveManager.iplayerEnergyProjectile = players.players[p].iPlayerEnergyProjectile;
                saveManager.iplayerExplosiveProjectile = players.players[p].iPlayerExplosiveProjectile;
                saveManager.iplayerFireProjectile = players.players[p].iPlayerFireProjectile;
                saveManager.iplayerFireRate = players.players[p].iPlayerFireRate;
                saveManager.iplayerHealingSpecial = players.players[p].iPlayerHealingSpecial;
                saveManager.iplayerHealth = players.players[p].iPlayerHealth;
                saveManager.iplayerHealthProjectile = players.players[p].iPlayerHealthProjectile;
                saveManager.iplayerLaserProjectile = players.players[p].iPlayerLaserProjectile;
                saveManager.iplayerLaserSpecial = players.players[p].iPlayerLaserSpecial;
                saveManager.iplayerMoneySpecial = players.players[p].iPlayerMoneySpecial;
                saveManager.iplayerMovementSpeed = players.players[p].iPlayerMovementSpeed;
                saveManager.iplayerPoisonProjectile = players.players[p].iPlayerPoisonProjectile;
                saveManager.iplayerShieldSpecial = players.players[p].iPlayerShieldSpecial;
                saveManager.iplayerSlowProjectile = players.players[p].iPlayerSlowProjectile;
                saveManager.iplayerTimeStopSpecial = players.players[p].iPlayerTimeStopSpecial;

                saveManager.bplayerQuadShot = players.players[p].bPlayerQuadShot;
                saveManager.bplayerQuintupleShot = players.players[p].bPlayerQuintupleShot;
                saveManager.bplayerTripleShot = players.players[p].bPlayerTripleShot;
                saveManager.bplayerDoubleShot = players.players[p].bPlayerDoubleShot;
                saveManager.bplayerExtraLife1 = players.players[p].bPlayerExtraLife1;
                saveManager.bplayerExtraLife2 = players.players[p].bPlayerExtraLife2;
                saveManager.bplayerExtraLife3 = players.players[p].bPlayerExtraLife3;
                saveManager.bplayerExtraLife4 = players.players[p].bPlayerExtraLife4;

                saveManager.playerLevel = players.players[p].PlayerLevel;
                saveManager.playerDeathCount = players.players[p].PlayerDeathCount;
                saveManager.playerTimePlayedHours = players.players[p].PlayerTimePlayedHours;
                saveManager.playerTimePlayedMinutes = players.players[p].PlayerTimePlayedMinutes;
                saveManager.playerTimePlayedSeconds = players.players[p].PlayerTimePlayedSeconds;
                saveManager.playerCreditsCollected = players.players[p].PlayerCreditsCollected;
                saveManager.playerCreditsSpent = players.players[p].PlayerCreditsSpent;
                saveManager.playerWeaponsCollected = players.players[p].PlayerWeaponsCollected;
                saveManager.playerPercentageComplete = players.players[p].PlayerPercentageComplete;
                saveManager.playerBulletsFired = players.players[p].PlayerBulletsFired;
                saveManager.playerAccuracy = players.players[p].PlayerAccuracy;
                saveManager.playerEnemiesKilled = players.players[p].PlayerEnemiesKilled;
                saveManager.playerEnemiesHit = players.players[p].PlayerEnemiesHit;
                saveManager.playerMiniGamesPassed = players.players[p].PlayerMiniGamesPassed;
                saveManager.playerUpgradesPurchased = players.players[p].PlayerUpgradesPurchased;
                saveManager.playerPowerUpsCollected = players.players[p].PlayerPowerUpsCollected;
                saveManager.playerLevelsCompleted = players.players[p].PlayerLevelsCompleted;

                saveManager.playerAchievement1 = players.players[p].PlayerAchievement1;
                saveManager.playerAchievement2 = players.players[p].PlayerAchievement2;
                saveManager.playerAchievement3 = players.players[p].PlayerAchievement3;
                saveManager.playerAchievement4 = players.players[p].PlayerAchievement4;
                saveManager.playerAchievement5 = players.players[p].PlayerAchievement5;
                saveManager.playerAchievement6 = players.players[p].PlayerAchievement6;
                saveManager.playerAchievement7 = players.players[p].PlayerAchievement7;
                saveManager.playerAchievement8 = players.players[p].PlayerAchievement8;
                saveManager.playerAchievement9 = players.players[p].PlayerAchievement9;
                saveManager.playerAchievement10 = players.players[p].PlayerAchievement10;
                saveManager.playerAchievement11 = players.players[p].PlayerAchievement11;
                saveManager.playerAchievement12 = players.players[p].PlayerAchievement12;
                saveManager.playerAchievement13 = players.players[p].PlayerAchievement13;
                saveManager.playerAchievement14 = players.players[p].PlayerAchievement14;
                saveManager.playerAchievement15 = players.players[p].PlayerAchievement15;
                saveManager.playerAchievement16 = players.players[p].PlayerAchievement16;
                saveManager.playerAchievement17 = players.players[p].PlayerAchievement17;
                saveManager.playerAchievement18 = players.players[p].PlayerAchievement18;
                saveManager.playerAchievement19 = players.players[p].PlayerAchievement19;
                saveManager.playerAchievement20 = players.players[p].PlayerAchievement20;
                saveManager.playerAchievement21 = players.players[p].PlayerAchievement21;
                saveManager.playerAchievement22 = players.players[p].PlayerAchievement22;
                saveManager.playerAchievement23 = players.players[p].PlayerAchievement23;
                saveManager.playerAchievement24 = players.players[p].PlayerAchievement24;
                saveManager.playerAchievement25 = players.players[p].PlayerAchievement25;
                saveManager.playerAchievement26 = players.players[p].PlayerAchievement26;
                saveManager.playerAchievement27 = players.players[p].PlayerAchievement27;
                saveManager.playerAchievement28 = players.players[p].PlayerAchievement28;
                saveManager.playerAchievement29 = players.players[p].PlayerAchievement29;
                saveManager.playerAchievement30 = players.players[p].PlayerAchievement30;
                saveManager.playerAchievement31 = players.players[p].PlayerAchievement31;
                saveManager.playerAchievement32 = players.players[p].PlayerAchievement32;
                saveManager.playerAchievement33 = players.players[p].PlayerAchievement33;
                saveManager.playerAchievement34 = players.players[p].PlayerAchievement34;
                saveManager.playerAchievement35 = players.players[p].PlayerAchievement35;
                saveManager.playerAchievement36 = players.players[p].PlayerAchievement36;
                saveManager.playerAchievement37 = players.players[p].PlayerAchievement37;
                saveManager.playerAchievement38 = players.players[p].PlayerAchievement38;
                saveManager.playerAchievement39 = players.players[p].PlayerAchievement39;
                saveManager.playerAchievement40 = players.players[p].PlayerAchievement40;
                saveManager.playerAchievement41 = players.players[p].PlayerAchievement41;
                saveManager.playerAchievement42 = players.players[p].PlayerAchievement42;
                saveManager.playerAchievement43 = players.players[p].PlayerAchievement43;
                saveManager.playerAchievement44 = players.players[p].PlayerAchievement44;
                saveManager.playerAchievement45 = players.players[p].PlayerAchievement45;
                saveManager.playerAchievement46 = players.players[p].PlayerAchievement46;
                saveManager.playerAchievement47 = players.players[p].PlayerAchievement47;
                saveManager.playerAchievement48 = players.players[p].PlayerAchievement48;
                saveManager.playerAchievement49 = players.players[p].PlayerAchievement49;
                saveManager.playerAchievement50 = players.players[p].PlayerAchievement50;
                saveManager.playerAchievementCount = players.players[p].PlayerAchievementCount;
                saveManager.playerSelectedWeapon1 = players.players[p].PlayerSelectedWeapon1;
                saveManager.playerSelectedWeapon2 = players.players[p].PlayerSelectedWeapon2;
                saveManager.playerSelectedWeapon3 = players.players[p].PlayerSelectedWeapon3;
                saveManager.playerSelectedWeapon4 = players.players[p].PlayerSelectedWeapon4;
                saveManager.playerSelectedWeapon5 = players.players[p].PlayerSelectedWeapon5;
                saveManager.playerSelectedSpecial = players.players[p].PlayerSelectedSpecial;
                saveManager.playerShipsUnlocked = players.players[p].PlayerShipsUnlocked;
                saveManager.playerXP = players.players[p].PlayerXP;
                saveManager.playerLevelNumber = players.players[p].PlayerLevelNumber;

                saveManager.InitiateSave();
            }

            Saving = false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            for (int i = 0; i < cameraManager.CameraCount; i++)
            {
                GraphicsDevice.Viewport = cameraManager.ViewportsRead[i];
                DrawSprites(cameraManager.CamerasRead[i]);
            }

            base.Draw(gameTime);
        }

        void DrawSprites(Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            if (GameStart == true)
                background.Draw(spriteBatch);

            if (GameState == 0)
            {
                menu.Draw(spriteBatch);
            }

            if (GameState == 1 || GameState == 2)
            {
                players.PlayerDraw(spriteBatch);
            }

            if (GameState == 2)
            {
                spriteBatch.DrawString(font, "Paused", new Vector2(500, 500), Color.White);
            }

            //spriteBatch.Draw(Marker, cameraManager.TL, Color.Red);
            //spriteBatch.Draw(Marker, cameraManager.TR, Color.Blue);
            //spriteBatch.Draw(Marker, cameraManager.BL, Color.Green);
            //spriteBatch.Draw(Marker, cameraManager.BR, Color.Orange);

            //spriteBatch.Draw(Marker, cameraManager.CenterPoint, Color.Yellow);

            spriteBatch.End();
        }
    }
}
