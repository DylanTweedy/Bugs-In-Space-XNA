using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BugsInSpace
{
    class PlayerManager
    {
        #region Variables

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        GamePadState currentGamepad1State;
        GamePadState previousGamepad1State;
        GamePadState currentGamepad2State;
        GamePadState previousGamepad2State;
        GamePadState currentGamepad3State;
        GamePadState previousGamepad3State;
        GamePadState currentGamepad4State;
        GamePadState previousGamepad4State;
        public bool keyboardAvailable;
        public bool gamepad1Available;
        public bool gamepad2Available;
        public bool gamepad3Available;
        public bool gamepad4Available;
        public int activePlayers;
        public List<Player> players;
        List<GUI> guis;
        List<LoadPlayer> loadPlayer;
        SpriteFont font;
        float AccelerationPool;
        public float FireRatePool;
        public int MaxBulletsPool;
        public int HealthPool;
        public int EnergyPool;
        public int DamagePool;
        public int LivesPool;
        int AvailablePads;
        bool CheckPad1;
        bool CheckPad2;
        bool CheckPad3;
        bool CheckPad4;
        bool CheckPadOff1;
        bool CheckPadOff2;
        bool CheckPadOff3;
        bool CheckPadOff4;
        TimeSpan blinkTime;
        TimeSpan previousBlinkTime;
        bool Blink;
        int BlinkInt;
        bool LoadPlayerRemove;
        public int GameState;
        bool Collide;
        Random random;
        Vector2 Closest;
        TimeSpan MovementTime;
        TimeSpan PreviousMovementTime;
        bool Control1JustQuit;
        bool Control2JustQuit;
        bool Control3JustQuit;
        bool Control4JustQuit;
        bool Control5JustQuit;
        int HealthIncrease;
        bool CureStatus;
        bool GetExtraLife;
        int RandomStatus;
        TimeSpan StatusTime;
        TimeSpan PreviousStatusTime;
        bool InitializeTimeStop;
        float OriginalGameSpeed;
        ParticleEngine TimeStopParticles;
        Texture2D WhiteTexture;
        float FlashAlpha;
        bool FlashStart;
        bool PreviousTimeStop;

        #endregion Varaibles

        #region Level Manager Variables

        public Levels level;
        Vector2 ButtonPosition;
        bool startLevel;
        bool levelInitialize;
        bool levelStart;
        string levelName;
        public bool ButtonPaused;
        public bool isHit;
        TimeSpan completeTime;
        TimeSpan previousCompleteTime;
        bool time;
        public bool levelActive;
        public MobileSprite LevelButton;
        Texture2D ButtonTexture;
        int CurrentLevel;

        #endregion Variables

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
            players = new List<Player>();
            guis = new List<GUI>();
            loadPlayer = new List<LoadPlayer>();
            keyboardAvailable = true;
            gamepad1Available = true;
            gamepad2Available = true;
            gamepad3Available = true;
            gamepad4Available = true;
            activePlayers = 0;
            CheckPad1 = true;
            CheckPad2 = true;
            CheckPad3 = true;
            CheckPad4 = true;
            previousBlinkTime = TimeSpan.Zero;
            blinkTime = TimeSpan.FromSeconds(0.3f);
            BlinkInt = 1;
            LoadPlayerRemove = false;
            GameState = 1;
            Collide = false;
            Control1JustQuit = true;
            Control2JustQuit = true;
            Control3JustQuit = true;
            Control4JustQuit = true;
            Control5JustQuit = true;
            random = new Random();
            InitializeLevelManager();
            Closest = Vector2.Zero;
            PreviousMovementTime = TimeSpan.Zero;
            MovementTime = TimeSpan.FromSeconds(0.5f);
            PreviousStatusTime = TimeSpan.Zero;
            StatusTime = TimeSpan.FromSeconds(1f);
            HealthIncrease = 0;
            InitializeTimeStop = true;
            OriginalGameSpeed = Game1.Instance.speed;
            FlashAlpha = 0;
            FlashStart = false;
            PreviousTimeStop = false;
        }

        private void InitializeLevelManager()
        {
            level = new Levels();
            levelInitialize = true;

            startLevel = true;
            levelStart = false;
            isHit = false;
            ButtonPaused = true;
            completeTime = TimeSpan.FromSeconds(3.0f);
            previousCompleteTime = TimeSpan.Zero;
            time = true;
            levelActive = false;
            levelName = "";
            CurrentLevel = 1;
        }

        public void LoadContent()
        {
            font = Game1.Instance.Content.Load<SpriteFont>("Fonts//Font1");

            WhiteTexture = Game1.Instance.Content.Load<Texture2D>("Images//Menu//White");

            ButtonTexture = Game1.Instance.Content.Load<Texture2D>("Images//GUI//LevelButton");
            LevelButton = new MobileSprite(ButtonTexture);
            LevelButton.Sprite.AddAnimation("default", 0, 0, 391, 64, 1, 10f);
            LevelButton.Sprite.CurrentAnimation = "default";
            LevelButton.Sprite.Tint = Color.White;
            LevelButton.Position = new Vector2(-LevelButton.Sprite.Width, (screenHeight / 2) - (LevelButton.Sprite.Height / 2));
            LevelButton.IsMoving = false;
            LevelButton.IsActive = true;
            LevelButton.IsCollidable = true;

            ButtonPosition = new Vector2(-LevelButton.Sprite.Width, (screenHeight / 2) - (LevelButton.Sprite.Height / 2));

            LoadParticles();
        }

        private void LoadParticles()
        {
            TimeStopParticles = new ParticleEngine(1, Color.White, Vector2.Zero);
            TimeStopParticles.LoadContent();
            TimeStopParticles.AddParticles = false;
            TimeStopParticles.total = 10;
        }

        public void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousGamepad1State = currentGamepad1State;
            currentGamepad1State = GamePad.GetState(PlayerIndex.One);
            previousGamepad2State = currentGamepad2State;
            currentGamepad2State = GamePad.GetState(PlayerIndex.Two);
            previousGamepad3State = currentGamepad3State;
            currentGamepad3State = GamePad.GetState(PlayerIndex.Three);
            previousGamepad4State = currentGamepad4State;
            currentGamepad4State = GamePad.GetState(PlayerIndex.Four);

            LevelButton.Update(gameTime);

            UpdateLevelManager(gameTime);

            if (Game1.Instance.GameState == 1)
            {
                #region Drop-In

                if (currentKeyboardState.IsKeyDown(Keys.Space) && keyboardAvailable && Control1JustQuit == false)
                {
                    keyboardAvailable = false;
                    activePlayers += 1;
                    AddLoad(gameTime, 1);
                }

                if (currentGamepad1State.IsButtonDown(Buttons.A) && gamepad1Available && Control2JustQuit == false)
                {
                    gamepad1Available = false;
                    activePlayers += 1;
                    AddLoad(gameTime, 2);
                }

                if (currentGamepad2State.IsButtonDown(Buttons.A) && gamepad2Available && Control3JustQuit == false)
                {
                    gamepad2Available = false;
                    activePlayers += 1;
                    AddLoad(gameTime, 3);
                }

                if (currentGamepad3State.IsButtonDown(Buttons.A) && gamepad3Available && Control4JustQuit == false)
                {
                    gamepad3Available = false;
                    activePlayers += 1;
                    AddLoad(gameTime, 4);
                }

                if (currentGamepad4State.IsButtonDown(Buttons.A) && gamepad4Available && Control5JustQuit == false)
                {
                    gamepad4Available = false;
                    activePlayers += 1;
                    AddLoad(gameTime, 5);
                }

                if (currentKeyboardState.IsKeyUp(Keys.Space) && keyboardAvailable)
                    Control1JustQuit = false;
                if (currentGamepad1State.IsButtonUp(Buttons.A) && gamepad1Available)
                    Control2JustQuit = false;
                if (currentGamepad2State.IsButtonUp(Buttons.A) && gamepad2Available)
                    Control3JustQuit = false;
                if (currentGamepad3State.IsButtonUp(Buttons.A) && gamepad3Available)
                    Control4JustQuit = false;
                if (currentGamepad4State.IsButtonUp(Buttons.A) && gamepad4Available)
                    Control5JustQuit = false;

                if (previousGamepad1State.IsConnected == false && currentGamepad1State.IsConnected)
                    Control2JustQuit = false;
                if (previousGamepad2State.IsConnected == false && currentGamepad1State.IsConnected)
                    Control3JustQuit = false;
                if (previousGamepad3State.IsConnected == false && currentGamepad1State.IsConnected)
                    Control4JustQuit = false;
                if (previousGamepad4State.IsConnected == false && currentGamepad1State.IsConnected)
                    Control5JustQuit = false;
                
                #endregion Drop-In

                #region Update Gui

                for (int pl = 0; pl < players.Count; pl++)
                {
                    if (players[pl].LivesPool > 0 && players[pl].GameOver)
                    {
                        LivesPool += players[pl].LivesPool;
                        players[pl].LivesPool = 0;
                    }
                }

                for (int p = 0; p < players.Count; p++)
                {
                    players[p].Update(gameTime, new Vector2(guis[p].Gui.Position.X + (guis[p].Gui.Sprite.Width / 2) - 32,
                                                            guis[p].Gui.Position.Y + (guis[p].Gui.Sprite.Height / 2) - 32));

                    guis[p].Update(gameTime);

                    guis[p].Active = players[p].Active;
                    guis[p].PlayerCredits = players[p].GameCredits;
                    guis[p].PlayerHealth = players[p].PlayerHealth;
                    guis[p].PlayerEnergy = players[p].PlayerEnergy;
                    guis[p].PlayerLives = players[p].FinalLives;
                    guis[p].PlayerMaxHealth = players[p].PlayerMaxHealth;
                    guis[p].PlayerMaxEnergy = players[p].PlayerMaxEnergy;
                    guis[p].PlayerExperience = players[p].PlayerXP - players[p].ExperienceSubtract;
                    guis[p].PlayerExperienceToNextLevel = players[p].ExperienceToLevel - players[p].ExperienceSubtract;
                    guis[p].PlayerLevel = players[p].PlayerLevel;


                    guis[p].PlayerName = players[p].PlayerName;
                    guis[p].PlayerColor = players[p].PlayerColor;

                    #region Pools

                    if (players[p].AccelerationPool > 0)
                    {
                        AccelerationPool += players[p].AccelerationPool;
                        players[p].AccelerationPool = 0;
                    }

                    if (players[p].DamagePool > 0)
                    {
                        DamagePool += players[p].DamagePool;
                        players[p].DamagePool = 0;
                    }

                    if (players[p].EnergyPool > 0)
                    {
                        EnergyPool += players[p].EnergyPool;
                        players[p].EnergyPool = 0;
                    }

                    if (players[p].FireRatePool > 0)
                    {
                        FireRatePool += players[p].FireRatePool;
                        players[p].FireRatePool = 0;
                    }

                    if (players[p].HealthPool > 0)
                    {
                        HealthPool += players[p].HealthPool;
                        players[p].HealthPool = 0;
                    }

                    if (players[p].LivesPool > 0)
                    {
                        LivesPool += players[p].LivesPool;
                        players[p].LivesPool = 0;
                    }

                    if (players[p].MaxBulletsPool > 0)
                    {
                        MaxBulletsPool += players[p].MaxBulletsPool;
                        players[p].MaxBulletsPool = 0;
                    }

                    if (AccelerationPool > 0 && players[p].FinalAcceleration < 150)
                    {
                        players[p].BonusAcceleration += AccelerationPool / players.Count;
                        AccelerationPool -= AccelerationPool / players.Count;
                    }

                    if (DamagePool > 0 && players[p].FinalDamage < 1000)
                    {
                        players[p].BonusDamage += DamagePool / players.Count;
                        DamagePool -= DamagePool / players.Count;
                    }

                    if (HealthPool > 0 && players[p].PlayerHealth < players[p].PlayerMaxHealth)
                    {
                        players[p].PlayerHealth += HealthPool / players.Count;
                        HealthPool -= HealthPool / players.Count;
                    }

                    if (EnergyPool > 0 && players[p].PlayerEnergy < players[p].PlayerMaxEnergy)
                    {
                        players[p].PlayerEnergy += EnergyPool / players.Count;
                        EnergyPool -= EnergyPool / players.Count;
                    }

                    if (FireRatePool > 0 && (players[p].PlayerFireRate - players[p].BonusFireRate) > 0.01f)
                    {
                        players[p].BonusFireRate += FireRatePool / players.Count;
                        FireRatePool -= FireRatePool / players.Count;
                    }

                    if (LivesPool > 0 && players[p].FinalLives < 4)
                    {
                        players[p].BonusLives += LivesPool;
                        LivesPool -= LivesPool;
                    }

                    if (MaxBulletsPool > 0 && players[p].FinalMaxBullets < 250)
                    {
                        players[p].BonusMaxBullets += MaxBulletsPool / players.Count;
                        MaxBulletsPool -= MaxBulletsPool / players.Count;
                    }

                    #endregion

                    if (p == 0)
                    {
                        guis[p].XPosition = 0;
                    }
                    if (p == 1)
                    {
                        guis[p].XPosition = ((int)(screenWidth / 3.35) - (guis[p].Gui.Sprite.Width / 2));
                    }
                    if (p == 2)
                    {
                        guis[p].XPosition = (screenWidth / 2) - (guis[p].Gui.Sprite.Width / 2);
                    }
                    if (p == 3)
                    {
                        guis[p].XPosition = (((int)(screenWidth) - (int)(screenWidth / 3.35)) - (guis[p].Gui.Sprite.Width / 2));
                    }
                    if (p == 4)
                    {
                        guis[p].XPosition = screenWidth - guis[p].Gui.Sprite.Width;
                    }


                    if (players[p].controlSystem == 2 && currentGamepad1State.IsConnected == false)
                        players[p].Active = false;
                    if (players[p].controlSystem == 3 && currentGamepad2State.IsConnected == false)
                        players[p].Active = false;
                    if (players[p].controlSystem == 4 && currentGamepad3State.IsConnected == false)
                        players[p].Active = false;
                    if (players[p].controlSystem == 5 && currentGamepad4State.IsConnected == false)
                        players[p].Active = false;


                    if (players[p].Active == false)
                    {
                        if (players[p].controlSystem == 1)
                            keyboardAvailable = true;
                        if (players[p].controlSystem == 2)
                            gamepad1Available = true;
                        if (players[p].controlSystem == 3)
                            gamepad2Available = true;
                        if (players[p].controlSystem == 4)
                            gamepad3Available = true;
                        if (players[p].controlSystem == 5)
                            gamepad4Available = true;
                        activePlayers -= 1;

                        if (players[p].controlSystem == 1)
                            Control1JustQuit = true;
                        if (players[p].controlSystem == 2)
                            Control2JustQuit = true;
                        if (players[p].controlSystem == 3)
                            Control3JustQuit = true;
                        if (players[p].controlSystem == 4)
                            Control4JustQuit = true;
                        if (players[p].controlSystem == 5)
                            Control5JustQuit = true;

                        players.RemoveAt(p);
                        guis.RemoveAt(p);

                        for (int i = 0; i < loadPlayer.Count; i++)
                        {
                            loadPlayer[i].playerNumber -= 1;
                        }
                    }
                }

                #endregion

                #region Check Gamepads

                if (currentGamepad1State.IsConnected && CheckPad1)
                {
                    AvailablePads += 1;
                    CheckPad1 = false;
                    CheckPadOff1 = true;
                }
                if (currentGamepad1State.IsConnected == false && CheckPadOff1)
                {
                    AvailablePads -= 1;
                    CheckPad1 = true;
                    CheckPadOff1 = false;
                }

                if (currentGamepad2State.IsConnected && CheckPad2)
                {
                    AvailablePads += 1;
                    CheckPad2 = false;
                    CheckPadOff2 = true;
                }
                if (currentGamepad2State.IsConnected == false && CheckPadOff2)
                {
                    AvailablePads -= 1;
                    CheckPad2 = true;
                    CheckPadOff2 = false;
                }

                if (currentGamepad3State.IsConnected && CheckPad3)
                {
                    AvailablePads += 1;
                    CheckPad3 = false;
                    CheckPadOff3 = true;
                }
                if (currentGamepad3State.IsConnected == false && CheckPadOff3)
                {
                    AvailablePads -= 1;
                    CheckPad3 = true;
                    CheckPadOff3 = false;
                }

                if (currentGamepad4State.IsConnected && CheckPad4)
                {
                    AvailablePads += 1;
                    CheckPad4 = false;
                    CheckPadOff4 = true;
                }
                if (currentGamepad4State.IsConnected == false && CheckPadOff4)
                {
                    AvailablePads -= 1;
                    CheckPad4 = true;
                    CheckPadOff4 = false;
                }

                #endregion

                #region Blink

                if (gameTime.TotalGameTime - previousBlinkTime > blinkTime)
                {
                    if (BlinkInt <= 2)
                    {
                        Blink = true;
                    }

                    BlinkInt += 1;
                }

                if (gameTime.TotalGameTime - previousBlinkTime > blinkTime)
                {
                    if (BlinkInt > 2)
                    {
                        Blink = false;
                    }

                    if (BlinkInt == 4)
                        BlinkInt = 0;

                    previousBlinkTime = gameTime.TotalGameTime;
                }

                #endregion

                UpdateCollision(gameTime);
                UpdateClosest(gameTime);
                UpdateSpecials(gameTime);
                UpdateParticles(gameTime);

                #region Draining

                for (int pl = 0; pl < players.Count; pl++)
                {
                    if (levelStart)
                        players[pl].Draining = true;
                    else
                        players[pl].Draining = false;
                }

                #endregion

                #region Quit

                if (currentKeyboardState.IsKeyDown(Keys.Escape) && activePlayers == 0)
                {
                    GameState = 0;
                }

                if ((currentGamepad1State.IsButtonDown(Buttons.B) || currentGamepad1State.IsButtonDown(Buttons.Back)) && activePlayers == 0)
                {
                    GameState = 0;
                }

                #endregion
            }

            #region Loadplayer

            for (int l = 0; l < loadPlayer.Count; l++)
            {
                if (loadPlayer[l].playerNumber == 1)
                {
                    if (loadPlayer[l].saveSelect)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(20 + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(20 + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                    }
                    if (loadPlayer[l].CreateNew)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(20 + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(20 + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                    }
                }

                if (loadPlayer[l].playerNumber == 2)
                {
                    if (loadPlayer[l].saveSelect)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(((int)(screenWidth / 3.35) - (200 / 2)) + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(((int)(screenWidth / 3.35) - (200 / 2)) + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                    }
                    if (loadPlayer[l].CreateNew)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(((int)(screenWidth / 3.35) - (200 / 2)) + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(((int)(screenWidth / 3.35) - (200 / 2)) + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                    }
                }

                if (loadPlayer[l].playerNumber == 3)
                {
                    if (loadPlayer[l].saveSelect)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((screenWidth / 2) - 100 + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((screenWidth / 2) - 100 + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                    }
                    if (loadPlayer[l].CreateNew)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((screenWidth / 2) - 100 + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((screenWidth / 2) - 100 + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                    }
                }

                if (loadPlayer[l].playerNumber == 4)
                {
                    if (loadPlayer[l].saveSelect)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((((int)(screenWidth) - (int)(screenWidth / 3.35)) - (200 / 2)) + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((((int)(screenWidth) - (int)(screenWidth / 3.35)) - (200 / 2)) + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                    }
                    if (loadPlayer[l].CreateNew)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((((int)(screenWidth) - (int)(screenWidth / 3.35)) - (200 / 2)) + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((((int)(screenWidth) - (int)(screenWidth / 3.35)) - (200 / 2)) + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                    }
                }

                if (loadPlayer[l].playerNumber == 5)
                {
                    if (loadPlayer[l].saveSelect)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((screenWidth - 200) + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2((screenWidth - 200) + (font.MeasureString("Select Profile").X / 2) - (font.MeasureString(loadPlayer[l].saveFileOutput).X / 2), 40));
                    }
                    if (loadPlayer[l].CreateNew)
                    {
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(screenWidth - 200 + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                        loadPlayer[l].Update(gameTime, new Vector2(20, 20), new Vector2(screenWidth - 200 + (font.MeasureString("Profile Name").X / 2) - (font.MeasureString(loadPlayer[l].playerName).X / 2), 40));
                    }
                }

                if (loadPlayer[l].Return && loadPlayer[l].Exit == false)
                {
                    playerAcceleration = loadPlayer[l].playerAcceleration;
                    playerBulletSpeed = loadPlayer[l].playerBulletSpeed;
                    playerShip = loadPlayer[l].playerShip;
                    playerMaxBullets = loadPlayer[l].playerMaxBullets;
                    playerDamage = loadPlayer[l].playerDamage;
                    playerFireRate = loadPlayer[l].playerFireRate;
                    playerRedValue = loadPlayer[l].playerRedValue;
                    playerBlueValue = loadPlayer[l].playerBlueValue;
                    playerGreenValue = loadPlayer[l].playerGreenValue;

                    playerCredits = loadPlayer[l].playerCredits;
                    playerLives = loadPlayer[l].playerLives;
                    playerMaxHealth = loadPlayer[l].playerMaxHealth;
                    playerMaxEnergy = loadPlayer[l].playerMaxEnergy;

                    iplayerAmmo = loadPlayer[l].iplayerAmmo;
                    bplayerAutoFire = loadPlayer[l].bplayerAutoFire;
                    iplayerBulletSpeed = loadPlayer[l].iplayerBulletSpeed;
                    iplayerDamage = loadPlayer[l].iplayerDamage;
                    iplayerElectricProjectile = loadPlayer[l].iplayerElectricProjectile;
                    iplayerEnergy = loadPlayer[l].iplayerEnergy;
                    iplayerEnergyProjectile = loadPlayer[l].iplayerEnergyProjectile;
                    iplayerExplosiveProjectile = loadPlayer[l].iplayerExplosiveProjectile;
                    iplayerFireProjectile = loadPlayer[l].iplayerFireProjectile;
                    iplayerFireRate = loadPlayer[l].iplayerFireRate;
                    iplayerHealingSpecial = loadPlayer[l].iplayerHealingSpecial;
                    iplayerHealth = loadPlayer[l].iplayerHealth;
                    iplayerHealthProjectile = loadPlayer[l].iplayerHealthProjectile;
                    iplayerLaserProjectile = loadPlayer[l].iplayerLaserProjectile;
                    iplayerLaserSpecial = loadPlayer[l].iplayerLaserSpecial;
                    iplayerMoneySpecial = loadPlayer[l].iplayerMoneySpecial;
                    iplayerMovementSpeed = loadPlayer[l].iplayerMovementSpeed;
                    iplayerPoisonProjectile = loadPlayer[l].iplayerPoisonProjectile;
                    iplayerShieldSpecial = loadPlayer[l].iplayerShieldSpecial;
                    iplayerSlowProjectile = loadPlayer[l].iplayerSlowProjectile;
                    iplayerTimeStopSpecial = loadPlayer[l].iplayerTimeStopSpecial;

                    bplayerQuadShot = loadPlayer[l].bplayerQuadShot;
                    bplayerQuintupleShot = loadPlayer[l].bplayerQuintupleShot;
                    bplayerTripleShot = loadPlayer[l].bplayerTripleShot;
                    bplayerDoubleShot = loadPlayer[l].bplayerDoubleShot;
                    bplayerExtraLife1 = loadPlayer[l].bplayerExtraLife1;
                    bplayerExtraLife2 = loadPlayer[l].bplayerExtraLife2;
                    bplayerExtraLife3 = loadPlayer[l].bplayerExtraLife3;
                    bplayerExtraLife4 = loadPlayer[l].bplayerExtraLife4;

                    playerLevel = loadPlayer[l].playerLevel;
                    playerDeathCount = loadPlayer[l].playerDeathCount;
                    playerTimePlayedHours = loadPlayer[l].playerTimePlayedHours;
                    playerTimePlayedMinutes = loadPlayer[l].playerTimePlayedMinutes;
                    playerTimePlayedSeconds = loadPlayer[l].playerTimePlayedSeconds;
                    playerCreditsCollected = loadPlayer[l].playerCreditsCollected;
                    playerCreditsSpent = loadPlayer[l].playerCreditsSpent;
                    playerWeaponsCollected = loadPlayer[l].playerWeaponsCollected;
                    playerPercentageComplete = loadPlayer[l].playerPercentageComplete;
                    playerBulletsFired = loadPlayer[l].playerBulletsFired;
                    playerAccuracy = loadPlayer[l].playerAccuracy;
                    playerEnemiesKilled = loadPlayer[l].playerEnemiesKilled;
                    playerEnemiesHit = loadPlayer[l].playerEnemiesHit;
                    playerMiniGamesPassed = loadPlayer[l].playerMiniGamesPassed;
                    playerUpgradesPurchased = loadPlayer[l].playerUpgradesPurchased;
                    playerPowerUpsCollected = loadPlayer[l].playerPowerUpsCollected;
                    playerLevelsCompleted = loadPlayer[l].playerLevelsCompleted;

                    playerAchievement1 = loadPlayer[l].playerAchievement1;
                    playerAchievement2 = loadPlayer[l].playerAchievement2;
                    playerAchievement3 = loadPlayer[l].playerAchievement3;
                    playerAchievement4 = loadPlayer[l].playerAchievement4;
                    playerAchievement5 = loadPlayer[l].playerAchievement5;
                    playerAchievement6 = loadPlayer[l].playerAchievement6;
                    playerAchievement7 = loadPlayer[l].playerAchievement7;
                    playerAchievement8 = loadPlayer[l].playerAchievement8;
                    playerAchievement9 = loadPlayer[l].playerAchievement9;
                    playerAchievement10 = loadPlayer[l].playerAchievement10;
                    playerAchievement11 = loadPlayer[l].playerAchievement11;
                    playerAchievement12 = loadPlayer[l].playerAchievement12;
                    playerAchievement13 = loadPlayer[l].playerAchievement13;
                    playerAchievement14 = loadPlayer[l].playerAchievement14;
                    playerAchievement15 = loadPlayer[l].playerAchievement15;
                    playerAchievement16 = loadPlayer[l].playerAchievement16;
                    playerAchievement17 = loadPlayer[l].playerAchievement17;
                    playerAchievement18 = loadPlayer[l].playerAchievement18;
                    playerAchievement19 = loadPlayer[l].playerAchievement19;
                    playerAchievement20 = loadPlayer[l].playerAchievement20;
                    playerAchievement21 = loadPlayer[l].playerAchievement21;
                    playerAchievement22 = loadPlayer[l].playerAchievement22;
                    playerAchievement23 = loadPlayer[l].playerAchievement23;
                    playerAchievement24 = loadPlayer[l].playerAchievement24;
                    playerAchievement25 = loadPlayer[l].playerAchievement25;
                    playerAchievement26 = loadPlayer[l].playerAchievement26;
                    playerAchievement27 = loadPlayer[l].playerAchievement27;
                    playerAchievement28 = loadPlayer[l].playerAchievement28;
                    playerAchievement29 = loadPlayer[l].playerAchievement29;
                    playerAchievement30 = loadPlayer[l].playerAchievement30;
                    playerAchievement31 = loadPlayer[l].playerAchievement31;
                    playerAchievement32 = loadPlayer[l].playerAchievement32;
                    playerAchievement33 = loadPlayer[l].playerAchievement33;
                    playerAchievement34 = loadPlayer[l].playerAchievement34;
                    playerAchievement35 = loadPlayer[l].playerAchievement35;
                    playerAchievement36 = loadPlayer[l].playerAchievement36;
                    playerAchievement37 = loadPlayer[l].playerAchievement37;
                    playerAchievement38 = loadPlayer[l].playerAchievement38;
                    playerAchievement39 = loadPlayer[l].playerAchievement39;
                    playerAchievement40 = loadPlayer[l].playerAchievement40;
                    playerAchievement41 = loadPlayer[l].playerAchievement41;
                    playerAchievement42 = loadPlayer[l].playerAchievement42;
                    playerAchievement43 = loadPlayer[l].playerAchievement43;
                    playerAchievement44 = loadPlayer[l].playerAchievement44;
                    playerAchievement45 = loadPlayer[l].playerAchievement45;
                    playerAchievement46 = loadPlayer[l].playerAchievement46;
                    playerAchievement47 = loadPlayer[l].playerAchievement47;
                    playerAchievement48 = loadPlayer[l].playerAchievement48;
                    playerAchievement49 = loadPlayer[l].playerAchievement49;
                    playerAchievement50 = loadPlayer[l].playerAchievement50;
                    playerAchievementCount = loadPlayer[l].playerAchievementCount;
                    playerSelectedWeapon1 = loadPlayer[l].playerSelectedWeapon1;
                    playerSelectedWeapon2 = loadPlayer[l].playerSelectedWeapon2;
                    playerSelectedWeapon3 = loadPlayer[l].playerSelectedWeapon3;
                    playerSelectedWeapon4 = loadPlayer[l].playerSelectedWeapon4;
                    playerSelectedWeapon5 = loadPlayer[l].playerSelectedWeapon5;
                    playerSelectedSpecial = loadPlayer[l].playerSelectedSpecial;
                    playerShipsUnlocked = loadPlayer[l].playerShipsUnlocked;
                    playerXP = loadPlayer[l].playerXP;
                    playerLevelNumber = loadPlayer[l].playerLevelNumber;

                    AddPlayer(loadPlayer[l].controlSystem, loadPlayer[l].playerName);

                    loadPlayer.RemoveAt(l);

                    for (int i = 0; i < loadPlayer.Count; i++)
                    {
                        loadPlayer[i].playerNumber += 1;
                    }
                }
            }

            for (int l = 0; l < loadPlayer.Count; l++)
            {
                if (loadPlayer[l].Exit && loadPlayer[l].Quit)
                {
                    if (loadPlayer[l].controlSystem == 1)
                    {
                        activePlayers -= 1;
                        keyboardAvailable = true;
                    }
                    if (loadPlayer[l].controlSystem == 2)
                    {
                        activePlayers -= 1;
                        gamepad1Available = true;
                    }
                    if (loadPlayer[l].controlSystem == 3)
                    {
                        activePlayers -= 1;
                        gamepad2Available = true;
                    }
                    if (loadPlayer[l].controlSystem == 4)
                    {
                        activePlayers -= 1;
                        gamepad3Available = true;
                    }
                    if (loadPlayer[l].controlSystem == 5)
                    {
                        activePlayers -= 1;
                        gamepad4Available = true;
                    }

                    LoadPlayerRemove = true;
                }

                if (loadPlayer[l].controlSystem == 2 && currentGamepad1State.IsConnected == false)
                {
                    activePlayers -= 1;
                    gamepad1Available = true;
                    LoadPlayerRemove = true;
                }
                if (loadPlayer[l].controlSystem == 3 && currentGamepad2State.IsConnected == false)
                {
                    activePlayers -= 1;
                    gamepad2Available = true;
                    LoadPlayerRemove = true;
                }
                if (loadPlayer[l].controlSystem == 4 && currentGamepad3State.IsConnected == false)
                {
                    activePlayers -= 1;
                    gamepad3Available = true;
                    LoadPlayerRemove = true;
                }
                if (loadPlayer[l].controlSystem == 5 && currentGamepad4State.IsConnected == false)
                {
                    activePlayers -= 1;
                    gamepad4Available = true;
                    LoadPlayerRemove = true;
                }

                if (LoadPlayerRemove)
                {
                    for (int i = 0; i < loadPlayer.Count; i++)
                    {
                        loadPlayer[i].playerNumber -= 1;
                    }
                    loadPlayer.RemoveAt(l);
                    LoadPlayerRemove = false;
                }
            }

            #endregion
        }

        private void UpdateParticles(GameTime gameTime)
        {
            TimeStopParticles.EmitterLocationUpdate(0, screenWidth, 0, screenHeight);
            TimeStopParticles.Update();
            TimeStopParticles.EmitterLocationUpdate(0, screenWidth, 0, screenHeight);
            TimeStopParticles.Update();
        }

        static bool IntersectsPixel(Rectangle rect1, Color[] data1, Rectangle rect2, Color[] data2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            for (int y = top; y < bottom; y++)
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
                    Color color2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

                    if (color1.A != 0 && color2.A != 0)
                        return true;
                }

            return false;
        }

        private void UpdateCollision(GameTime gameTime)
        {
            for (int e = 0; e < level.enemies.Count; e++)
            {
                level.enemies[e].MoneyValueMultiplier = 0;
                level.enemies[e].Money = false;
                level.enemies[e].LargeCrystals = false;
                level.enemies[e].FirePowerUps = false;
                level.enemies[e].TimeStop = false;
                TimeStopParticles.AddParticles = false;
            }

            for (int pl = 0; pl < players.Count; pl++)
            {
                if (ButtonPaused)
                {
                    for (int p = 0; p < players[pl].projectiles.Count; p++)
                    {
                        if (players[pl].projectiles[p].projectile.CollisionBox.Intersects(LevelButton.CollisionBox))
                        {
                            isHit = true;
                            players[pl].projectiles[p].active = false;
                            players[pl].hit = true;
                        }
                    }
                }
                for (int e = 0; e < level.enemies.Count; e++)
                {
                    if (level.enemies[e].Health <= 0 && level.enemies[e].GivenCredits == false)
                    {
                        players[pl].PlayerCredits += level.enemies[e].Value;
                        players[pl].PlayerCreditsCollected += level.enemies[e].Value;
                        players[pl].PlayerEnemiesKilled += 1;

                        if (pl == players.Count - 1)
                            level.enemies[e].GivenCredits = true;
                    }

                    for (int i = 0; i < players[pl].LaserPositions.Length; i++)
                    {
                        #region Laser Special

                        if (players[pl].LaserRectanglesHitBox[i].Intersects(level.enemies[e].EnemyBody.CollisionBox) && players[pl].LaserFired)
                        {
                            level.enemies[e].Health -= players[pl].FinalLaserDamage;
                            level.enemies[e].beenHit = true;
                            players[pl].hit = true;

                            if (gameTime.TotalGameTime - PreviousStatusTime > StatusTime)
                            {
                                RandomStatus = random.Next(1, 5);

                                if (random.Next(1, 101) >= players[pl].RandomStatusNumber)
                                {
                                    if (RandomStatus == 1)
                                    {
                                        level.enemies[e].Stunned = true;
                                        level.enemies[e].StunTime = (float)random.Next(1, 1000) / 100f;
                                    }
                                    if (RandomStatus == 2)
                                    {
                                        level.enemies[e].Burned = true;
                                        level.enemies[e].BurnTime = (float)random.Next(1, 1000) / 100f;
                                        level.enemies[e].BurnDamage = players[pl].FinalLaserDamage;
                                    }
                                    if (RandomStatus == 3)
                                    {
                                        level.enemies[e].Slowed = true;
                                        level.enemies[e].SlowTime = (float)random.Next(1, 1000) / 100f;
                                    }
                                    if (RandomStatus == 4)
                                    {
                                        level.enemies[e].Poisoned = true;
                                        level.enemies[e].PoisonTime = (float)random.Next(1, 1000) / 100f;
                                        level.enemies[e].PoisonDamage = players[pl].FinalLaserDamage;
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    #region Money Special
                                        
                    if (players[pl].MoneyActivator)
                    {
                        level.enemies[e].Money = true;
                        level.enemies[e].MoneyValueMultiplier += players[pl].MoneyValueMultiplier;
                    
                        if (players[pl].LargeCrystals)
                            level.enemies[e].LargeCrystals = true;
                    
                        if (players[pl].FirePowerUps)
                            level.enemies[e].FirePowerUps = true;
                    }
                    
                    #endregion

                    #region Time Stop

                    if (players[pl].TimeStop)
                    {
                        level.enemies[e].TimeStop = true;

                        if (InitializeTimeStop)
                        {
                            FlashStart = true;
                            InitializeTimeStop = false;
                            OriginalGameSpeed = Game1.Instance.speed;
                            Game1.Instance.speed = 0f;
                        }

                        TimeStopParticles.AddParticles = true;
                    }

                    #endregion

                    if (e == level.enemies.Count - 1 && pl == players.Count - 1 && level.enemies[e].TimeStop == false)
                    {
                        InitializeTimeStop = true;
                        Game1.Instance.speed = OriginalGameSpeed;
                    }

                    if (e == level.enemies.Count - 1)
                    {
                        players[pl].FinalLaserDamage = 0;

                        if (gameTime.TotalGameTime - PreviousStatusTime > StatusTime)
                            PreviousStatusTime = gameTime.TotalGameTime;
                    }

                    if (InitializeTimeStop)
                    {
                        for (int p = 0; p < players[pl].projectiles.Count; p++)
                        {
                            #region Projectile Hit Enemy

                            if (players[pl].projectiles[p].projectile.CollisionBox.Intersects(level.enemies[e].EnemyBody.CollisionBox) && level.enemies[e].EnemyBody.Sprite.CurrentAnimation == "default" && level.enemies[e].Health > 0)
                            {

                                if (players[pl].projectiles[p].Piercing == false)
                                    players[pl].projectiles[p].active = false;

                                if (players[pl].projectiles[p].HitEnemy == false)
                                {
                                    players[pl].projectiles[p].HitEnemy = true;
                                    players[pl].PlayerEnemiesHit += 1;
                                }
                                level.enemies[e].beenHit = true;
                                players[pl].hit = true;
                                if (players[pl].projectiles[p].ProjectileCount > 25)
                                    Game1.Instance.AudioPlay(players[pl].projectiles[p].hitSound, 3);

                                if (players[pl].projectiles[p].stunChance >= random.Next(1, 101))
                                {
                                    level.enemies[e].Stunned = true;
                                    level.enemies[e].StunTime = players[pl].projectiles[p].stunTime;
                                }
                                if (players[pl].projectiles[p].BurnChance >= random.Next(1, 101))
                                {
                                    level.enemies[e].Burned = true;
                                    level.enemies[e].BurnTime = players[pl].projectiles[p].BurnTime;
                                    level.enemies[e].BurnDamage = players[pl].projectiles[p].BurnDamage;
                                }
                                if (players[pl].projectiles[p].ElementType == 7)
                                {
                                    level.enemies[e].Poisoned = true;
                                    level.enemies[e].PoisonTime = players[pl].projectiles[p].PoisonTime;
                                    level.enemies[e].PoisonDamage = players[pl].projectiles[p].PoisonDamage;
                                }
                                if (players[pl].projectiles[p].SlowChance >= random.Next(1, 101))
                                {
                                    level.enemies[e].Slowed = true;
                                    level.enemies[e].SlowTime = players[pl].projectiles[p].SlowTime;
                                }
                                if (players[pl].projectiles[p].Healing && players[pl].PlayerHealth < players[pl].PlayerMaxHealth && random.Next(1, 10) == 1)
                                {
                                    players[pl].PlayerHealth += (players[pl].projectiles[p].damage / 2);
                                }
                                if (players[pl].projectiles[p].Energy && players[pl].PlayerEnergy < players[pl].PlayerMaxEnergy && random.Next(1, 10) == 1)
                                {
                                    players[pl].PlayerEnergy += (players[pl].projectiles[p].damage / 2);
                                }

                                #region Strengths and Weaknesses

                                if (level.enemies[e].ElementType == 4 && players[pl].projectiles[p].ElementType == 5)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 5 && players[pl].projectiles[p].ElementType == 6)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 6 && players[pl].projectiles[p].ElementType == 7)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 7 && players[pl].projectiles[p].ElementType == 8)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 8 && players[pl].projectiles[p].ElementType == 9)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 9 && players[pl].projectiles[p].ElementType == 10)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 10 && players[pl].projectiles[p].ElementType == 4)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage / 2;
                                else if (level.enemies[e].ElementType == 4 && players[pl].projectiles[p].ElementType == 10)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 5 && players[pl].projectiles[p].ElementType == 4)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 6 && players[pl].projectiles[p].ElementType == 5)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 7 && players[pl].projectiles[p].ElementType == 6)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 8 && players[pl].projectiles[p].ElementType == 7)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 9 && players[pl].projectiles[p].ElementType == 8)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 10 && players[pl].projectiles[p].ElementType == 9)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 2 && players[pl].projectiles[p].ElementType == 3)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else if (level.enemies[e].ElementType == 3 && players[pl].projectiles[p].ElementType == 2)
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage * 2;
                                else
                                    level.enemies[e].Health -= players[pl].projectiles[p].damage;

                                #endregion
                            }

                            if (players[pl].projectiles[p].RectangleRadius.Intersects(level.enemies[e].EnemyBody.CollisionBox) && players[pl].projectiles[p].ExplodeDamage && players[pl].projectiles[p].active == false && level.enemies[e].beenHit == false)
                            {
                                players[pl].PlayerEnemiesHit += 1;
                                level.enemies[e].beenHit = true;
                                level.enemies[e].Health -= players[pl].projectiles[p].damage / 5;
                                players[pl].hit = true;
                            }

                            #endregion
                        }

                        for (int p = 0; p < level.enemies[e].projectiles.Count; p++)
                        {
                            #region Projectile Hit Player

                            players[pl].player.Sprite.SetTextureData();
                            level.enemies[e].projectiles[p].projectile.Sprite.SetTextureData();

                            if (IntersectsPixel(level.enemies[e].projectiles[p].projectile.CollisionBox, level.enemies[e].projectiles[p].projectile.Sprite.textureData, players[pl].player.CollisionBox, players[pl].player.Sprite.textureData))
                            {
                                Collide = true;
                            }

                            if (Collide)
                            {
                                if (players[pl].Alive)
                                    if (level.enemies[e].projectiles[p].projectile.CollisionBox.Intersects(players[pl].player.CollisionBox))
                                    {
                                        players[pl].PlayerHealth -= level.enemies[e].projectiles[p].damage;
                                        players[pl].beenHit = true;
                                        level.enemies[e].hit = true;
                                        if (level.enemies[e].projectiles[p].Piercing == false)
                                            level.enemies[e].projectiles[p].active = false;
                                        Game1.Instance.AudioPlay(level.enemies[e].projectiles[p].hitSound, 3);

                                        if (level.enemies[e].projectiles[p].stunChance >= random.Next(1, 101))
                                        {
                                            players[pl].Stunned = true;
                                            players[pl].StunTime = level.enemies[e].projectiles[p].stunTime;
                                        }

                                        if (level.enemies[e].projectiles[p].BurnChance >= random.Next(1, 101))
                                        {
                                            players[pl].Burned = true;
                                            players[pl].BurnTime = level.enemies[e].projectiles[p].BurnTime;
                                            players[pl].BurnDamage = level.enemies[e].projectiles[p].BurnDamage;
                                        }
                                        if (level.enemies[e].projectiles[p].ElementType == 7)
                                        {
                                            players[pl].Poisoned = true;
                                            players[pl].PoisonTime = level.enemies[e].projectiles[p].PoisonTime;
                                            players[pl].PoisonDamage = level.enemies[e].projectiles[p].PoisonDamage;
                                        }
                                        if (level.enemies[e].projectiles[p].SlowChance >= random.Next(1, 101))
                                        {
                                            players[pl].Slowed = true;
                                            players[pl].SlowTime = level.enemies[e].projectiles[p].SlowTime;
                                        }
                                        if (level.enemies[e].projectiles[p].Healing)
                                        {
                                            level.enemies[e].Health += level.enemies[e].projectiles[p].damage / 2;
                                        }
                                        if (level.enemies[e].projectiles[p].Energy)
                                        {
                                            level.enemies[e].Health += level.enemies[e].projectiles[p].damage / 2;
                                        }

                                        if (level.enemies[e].projectiles[p].MoneyValue > 0)
                                        {
                                            players[pl].PlayerCredits += level.enemies[e].projectiles[p].MoneyValue;
                                            level.enemies[e].projectiles[p].MoneyValue = 0;
                                        }
                                    }
                            }

                            for (int p2 = 0; p2 < players.Count; p2++)
                            {
                                if (level.enemies[e].projectiles[p].RectangleRadius.Intersects(players[p2].player.CollisionBox) && level.enemies[e].projectiles[p].active == false && level.enemies[e].projectiles[p].ExplodeDamage)
                                {
                                    players[p2].PlayerHealth -= level.enemies[e].projectiles[p].damage / 5;
                                    players[p2].beenHit = true;
                                    level.enemies[e].hit = true;
                                    level.enemies[e].projectiles[p].active = false;
                                }
                            }

                            #endregion
                        }

                        #region Player Collect PowerUp

                        if (players[pl].Alive)
                            if (level.enemies[e].PowerUpActive == 1)
                            {
                                if (players[pl].player.CollisionBox.Intersects(level.enemies[e].powerUps.powerUp.CollisionBox))
                                {

                                    players[pl].player.Sprite.SetTextureData();
                                    level.enemies[e].powerUps.powerUp.Sprite.SetTextureData();

                                    if (IntersectsPixel(level.enemies[e].powerUps.powerUp.CollisionBox, level.enemies[e].powerUps.powerUp.Sprite.textureData, players[pl].player.CollisionBox, players[pl].player.Sprite.textureData))
                                    {
                                        Collide = true;
                                    }

                                    if (Collide)
                                    {
                                        players[pl].PlayerPowerUpsCollected += 1;
                                        players[pl].powerUp = level.enemies[e].powerUps.powerUpNumber;
                                        players[pl].powerUpActivated = true;
                                        level.enemies[e].powerUps.active = false;
                                        level.enemies[e].PowerUpActive = 0;
                                        level.enemies[e].playerPowerUp = true;
                                    }
                                }
                            }

                        #endregion

                        #region Enemy Hit Player

                        if (players[pl].player.CollisionBox.Intersects(level.enemies[e].EnemyBody.CollisionBox) && players[pl].GameOver == false)
                        {
                            if (gameTime.TotalGameTime - level.enemies[e].previousTouchTime > level.enemies[e].touchTime)
                            {
                                level.enemies[e].EnemyBody.Sprite.velocity = new Vector2(-level.enemies[e].EnemyBody.Sprite.velocity.X, -level.enemies[e].EnemyBody.Sprite.velocity.Y);
                                if (players[pl].player.Sprite.velocity.X != 0)
                                    level.enemies[e].EnemyBody.Sprite.velocity = new Vector2((players[pl].player.Sprite.velocity.X * 2), level.enemies[e].EnemyBody.Sprite.velocity.Y);

                                if (players[pl].player.Sprite.velocity.Y != 0)
                                    level.enemies[e].EnemyBody.Sprite.velocity = new Vector2(level.enemies[e].EnemyBody.Sprite.velocity.X, (players[pl].player.Sprite.velocity.Y * 2));

                                level.enemies[e].previousTouchTime = gameTime.TotalGameTime;
                            }

                            players[pl].PlayerHealth -= level.enemies[e].Damage;
                            players[pl].beenHit = true;
                        }

                        #endregion
                    }

                    if (FlashStart)
                    {
                        FlashAlpha += 5;

                        if (FlashAlpha >= 255)
                            FlashStart = false;
                    }

                    if (FlashStart == false && FlashAlpha != 0)
                    {
                        FlashAlpha -= 5;

                        if (FlashAlpha <= 0)
                            FlashAlpha = 0;
                    }

                    PreviousTimeStop = InitializeTimeStop;
                }
            }
        }

        private void UpdateLevelManager(GameTime gameTime)
        {
            #region Start

            if (startLevel)
            {
                InitializeLevelManager();
                levelActive = true;
                startLevel = false;
            }

            #endregion

            #region Level Control

            if (levelActive)
            {
                if (levelInitialize)
                {
                    level.Initialize(gameTime);
                }
            }

            #endregion

            #region Level 1

            if (levelActive && CurrentLevel == 1)
            {
                if (levelInitialize)
                {
                    levelName = "Level 1 - Getting Started";
                }

                if (levelStart)
                {
                    level.level1 = true;
                }
            }

            #endregion

            #region Level Control

            if (levelActive)
            {
                if (levelInitialize)
                {
                    ButtonPosition = new Vector2(-LevelButton.Sprite.Width, (screenHeight / 2) - (LevelButton.Sprite.Height / 2));
                    ButtonPaused = false;
                    levelActive = true;
                    levelInitialize = false;
                }

                if (ButtonPaused == false)
                {
                    ButtonPosition.X += 5f;
                }

                if (ButtonPosition.X >= (screenWidth / 2) - (LevelButton.Sprite.Width / 2))
                {
                    ButtonPaused = true;
                }

                if (isHit)
                {
                    levelStart = true;
                    ButtonPosition.X = -LevelButton.Sprite.Width;
                    levelActive = true;
                }

                if (levelStart)
                {
                    level.Update(gameTime);
                }

                if (level.levelComplete && (gameTime.TotalGameTime - previousCompleteTime > completeTime) && time)
                {
                    previousCompleteTime = gameTime.TotalGameTime;
                    time = false;
                }

                if (level.levelComplete && (gameTime.TotalGameTime - previousCompleteTime > completeTime))
                {
                    levelActive = false;
                    levelStart = false;
                    for (int pl = 0; pl < players.Count; pl++)
                    {
                        players[pl].PlayerLevelsCompleted += 1;
                    }
                    Game1.Instance.Saving = true;
                    CurrentLevel += 1;
                }
            }

            #endregion

            LevelButton.Position = ButtonPosition;

            ///////////////////////////////////////////////////////////////////////

            if (levelActive == false)
            {
                startLevel = true;
            }

            ///////////////////////////////////////////////////////////////////////

            isHit = false;
        }

        private void UpdateSpecials(GameTime gameTime)
        {
            HealthIncrease = 0;
            GetExtraLife = false;
            CureStatus = false;

            for (int p = 0; p < players.Count; p++)
            {
                if (players[p].HealingActivator)
                {
                    HealthIncrease += players[p].HealthIncrease;

                    if (GetExtraLife == false && players[p].GetExtraLife)
                        GetExtraLife = true;

                    if (CureStatus == false && players[p].CureStatus)
                        CureStatus = true;
                }
            }

            if (HealthIncrease > 0)
            {
                for (int p = 0; p < players.Count; p++)
                {
                    players[p].Healing = true;
                    players[p].RecievedGetExtraLife = GetExtraLife;
                    players[p].RecievedCureStatus = CureStatus;
                    players[p].RecievedHealthIncrease = HealthIncrease;
                }
            }
            else
            {
                for (int p = 0; p < players.Count; p++)
                {
                    players[p].Healing = false;
                    players[p].RecievedGetExtraLife = false;
                    players[p].RecievedCureStatus = false;
                    players[p].RecievedHealthIncrease = 0;
                }
            }
        }

        private void UpdateClosest(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - PreviousMovementTime > MovementTime)
            {
                for (int pl = 0; pl < players.Count; pl++)
                {
                    for (int e = 0; e < level.enemies.Count; e++)
                    {
                        if (e == 0)
                            Closest = Vector2.Zero;

                        if (((players[pl].player.Position.X - level.enemies[e].EnemyBody.Position.X) < Closest.X &&
                            (players[pl].player.Position.Y - level.enemies[e].EnemyBody.Position.Y) < Closest.Y) ||
                            Closest == Vector2.Zero)
                        {
                            Closest = level.enemies[e].EnemyBody.Position + new Vector2(level.enemies[e].EnemyBody.Sprite.Width / 2, level.enemies[e].EnemyBody.Sprite.Height / 2);
                        }

                        if (e == level.enemies.Count - 1)
                        {
                            players[pl].ClosestEnemy = Closest;
                        }
                    }

                    if (level.enemies.Count == 0)
                        players[pl].ClosestEnemy = Vector2.Zero;
                }


                for (int e = 0; e < level.enemies.Count; e++)
                {
                    for (int pl = 0; pl < players.Count; pl++)
                    {
                        if (pl == 0)
                            Closest = Vector2.Zero;

                        if ((level.enemies[e].EnemyBody.Position.X - players[pl].player.Position.X) > 0)
                        {
                            if (((level.enemies[e].EnemyBody.Position.X - players[pl].player.Position.X) < Closest.X &&
                                (level.enemies[e].EnemyBody.Position.Y - players[pl].player.Position.Y) < Closest.Y) ||
                                Closest == Vector2.Zero)
                            {
                                Closest = players[pl].player.Position + new Vector2(players[pl].player.Sprite.Width / 2, players[pl].player.Sprite.Height / 2);
                            }
                        }
                        else
                            if ((-(level.enemies[e].EnemyBody.Position.X - players[pl].player.Position.X) < Closest.X &&
                                (level.enemies[e].EnemyBody.Position.Y - players[pl].player.Position.Y) < Closest.Y) ||
                                Closest == Vector2.Zero)
                            {
                                Closest = players[pl].player.Position + new Vector2(players[pl].player.Sprite.Width / 2, players[pl].player.Sprite.Height / 2);
                            }

                        if (pl == players.Count - 1)
                        {
                            level.enemies[e].ClosestPlayer = Closest;
                        }
                    }

                    if (players.Count == 0)
                        level.enemies[e].ClosestPlayer = Vector2.Zero;
                }

                PreviousMovementTime = gameTime.TotalGameTime;
            }
        }

        private void AddLoad(GameTime gameTime, int ControlSystem)
        {
            LoadPlayer loadPlayers = new LoadPlayer();
            loadPlayers.Initialize(ControlSystem, activePlayers);
            loadPlayers.inGame = true;
            loadPlayers.LoadContent();

            for (int p = 0; p < players.Count; p++)
            {
                loadPlayers.activePlayers.Add(players[p].PlayerName);
            }

            loadPlayer.Add(loadPlayers);
        }

        private void AddPlayer(int ControlSystem, string PlayerName)
        {
            Player player = new Player();
            player.InitializePlayer(PlayerName, playerAcceleration, playerBulletSpeed, playerFireRate, playerRedValue, playerBlueValue, playerGreenValue, playerDamage,
                                    playerShip, playerMaxBullets, playerCredits, playerLives, playerMaxHealth, playerMaxEnergy,
                                    iplayerAmmo, bplayerAutoFire, iplayerBulletSpeed, iplayerDamage, iplayerElectricProjectile, iplayerEnergy,
                                    iplayerEnergyProjectile, iplayerExplosiveProjectile, iplayerFireProjectile, iplayerFireRate, iplayerHealingSpecial,
                                    iplayerHealth, iplayerHealthProjectile, iplayerLaserProjectile, iplayerLaserSpecial, iplayerMoneySpecial,
                                    iplayerMovementSpeed, iplayerPoisonProjectile, iplayerShieldSpecial, iplayerSlowProjectile, iplayerTimeStopSpecial,
                                    bplayerQuadShot, bplayerQuintupleShot, bplayerTripleShot, bplayerDoubleShot, bplayerExtraLife1,
                                    bplayerExtraLife2, bplayerExtraLife3, bplayerExtraLife4, playerLevel, playerDeathCount, playerTimePlayedHours, playerTimePlayedMinutes,
                                    playerTimePlayedSeconds, playerCreditsCollected, playerCreditsSpent, playerWeaponsCollected, playerPercentageComplete, playerBulletsFired,
                                    playerAccuracy, playerEnemiesKilled, playerEnemiesHit, playerMiniGamesPassed, playerUpgradesPurchased, playerPowerUpsCollected,
                                    playerLevelsCompleted, playerAchievement1, playerAchievement2, playerAchievement3, playerAchievement4, playerAchievement5, playerAchievement6,
                                    playerAchievement7, playerAchievement8, playerAchievement9, playerAchievement10, playerAchievement11, playerAchievement12, playerAchievement13,
                                    playerAchievement14, playerAchievement15, playerAchievement16, playerAchievement17, playerAchievement18, playerAchievement19, playerAchievement20,
                                    playerAchievement21, playerAchievement22, playerAchievement23, playerAchievement24, playerAchievement25, playerAchievement26,
                                    playerAchievement27, playerAchievement28, playerAchievement29, playerAchievement30, playerAchievement31, playerAchievement32,
                                    playerAchievement33, playerAchievement34, playerAchievement35, playerAchievement36, playerAchievement37, playerAchievement38,
                                    playerAchievement39, playerAchievement40, playerAchievement41, playerAchievement42, playerAchievement43, playerAchievement44,
                                    playerAchievement45, playerAchievement46, playerAchievement47, playerAchievement48, playerAchievement49, playerAchievement50,
                                    playerAchievementCount, playerSelectedWeapon1, playerSelectedWeapon2, playerSelectedWeapon3, playerSelectedWeapon4, playerSelectedWeapon5,
                                    playerSelectedSpecial, playerShipsUnlocked, playerXP, playerLevelNumber);

            player.Initialize(ControlSystem, Vector2.Zero);
            player.LoadContent();
            player.Active = true;
            players.Add(player);

            GUI gui = new GUI();
            gui.Initialize();
            gui.LoadContent();
            gui.Active = true;
            guis.Add(gui);
        }
    
        public void PlayerDraw(SpriteBatch spriteBatch)
        {
            for (int p = 0; p < players.Count; p++)
            {
                players[p].Draw(spriteBatch);
            }

            for (int g = 0; g < guis.Count; g++)
            {
                if (players[g].GameOver == false && players[g].paused == false)
                    guis[g].Draw(spriteBatch);
                if (players[g].GameOver && Blink && players[g].paused == false)
                    spriteBatch.DrawString(font, "Game Over", new Vector2((guis[g].Gui.Position.X + (guis[g].Gui.Sprite.Width / 2)) - (font.MeasureString("Game Over").X / 2), guis[g].Gui.Position.Y + 20), Color.White);
            }

            for (int l = 0; l < loadPlayer.Count; l++)
            {
                loadPlayer[l].Draw(spriteBatch);
            }

            if (activePlayers == 0 && Blink)
            {
                spriteBatch.DrawString(font, "Press Fire", new Vector2(20, 20), Color.White);
                spriteBatch.DrawString(font, "To Join", new Vector2(20 + (font.MeasureString("Press Fire").X / 2) - (font.MeasureString("To Join").X / 2), 40), Color.White);
            }

            if (activePlayers <= 1 && AvailablePads >= 1 && Blink)
            {
                spriteBatch.DrawString(font, "Press Fire", new Vector2(((int)(screenWidth / 3.35) - (200 / 2)), 20), Color.White);
                spriteBatch.DrawString(font, "To Join", new Vector2(((int)(screenWidth / 3.35) - (200 / 2)) + (font.MeasureString("Press Fire").X / 2) - (font.MeasureString("To Join").X / 2), 40), Color.White);
            }

            if (activePlayers <= 2 && AvailablePads >= 2 && Blink)
            {
                spriteBatch.DrawString(font, "Press Fire", new Vector2((screenWidth / 2) - 100, 20), Color.White);
                spriteBatch.DrawString(font, "To Join", new Vector2((screenWidth / 2) - 100 + (font.MeasureString("Press Fire").X / 2) - (font.MeasureString("To Join").X / 2), 40), Color.White);
            }

            if (activePlayers <= 3 && AvailablePads >= 3 && Blink)
            {
                spriteBatch.DrawString(font, "Press Fire", new Vector2((((int)(screenWidth) - (int)(screenWidth / 3.35)) - (200 / 2)), 20), Color.White);
                spriteBatch.DrawString(font, "To Join", new Vector2((((int)(screenWidth) - (int)(screenWidth / 3.35)) - (200 / 2)) + (font.MeasureString("Press Fire").X / 2) - (font.MeasureString("To Join").X / 2), 40), Color.White);
            }

            if (activePlayers <= 4 && AvailablePads >= 4 && Blink)
            {
                spriteBatch.DrawString(font, "Press Fire", new Vector2(screenWidth - 200, 20), Color.White);
                spriteBatch.DrawString(font, "To Join", new Vector2(screenWidth - 200 + (font.MeasureString("Press Fire").X / 2) - (font.MeasureString("To Join").X / 2), 40), Color.White);
            }

            if (levelActive)
            {
                level.Draw(spriteBatch);
            }

            LevelButton.Draw(spriteBatch);
            spriteBatch.DrawString(font, levelName, new Vector2(LevelButton.Position.X + (LevelButton.Sprite.Width / 2) - (font.MeasureString(levelName).X / 2),
                LevelButton.Position.Y + (LevelButton.Sprite.Height / 2) - (font.MeasureString(levelName).Y / 2)), Color.Black, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            TimeStopParticles.Draw(spriteBatch);

            spriteBatch.Draw(WhiteTexture, Vector2.Zero, new Color(FlashAlpha, FlashAlpha, FlashAlpha, FlashAlpha));
        }
    }
}
