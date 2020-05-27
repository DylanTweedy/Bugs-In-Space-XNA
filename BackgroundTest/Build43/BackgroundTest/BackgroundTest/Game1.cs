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
using Spine;

namespace BackgroundTest
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Add world time speed.

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Texture2D Marker;
        SpriteFont spriteFont;
        BackgroundManager background;

        List<Test> Hugo;
        Random rand;

        SpineTest spineTest;

        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
            };

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1200;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;



            //graphics.SynchronizeWithVerticalRetrace = false;
            //this.IsFixedTimeStep = false;
            //graphics.ApplyChanges();            
        }

        protected override void Initialize()
        {
            WorldVariables.Initialize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            CameraManager.Initialize(GraphicsDevice.Viewport);
            TileData.Initialize(Content);
            //LevelLoader.Initialize();
            WorldManager.Initialize();
            
            Hugo = new List<Test>();
            rand = new Random();
            //Hugo.Initialize(6.5f, 6f, 0.047f, 0.21875f);

            for (int i = 0; i < 1; i++)
            {
                Hugo.Add(new Test());
                //Hugo[i].Initialize(rand.Next(90, 100) / 10f, rand.Next(50, 100) / 10f, 0.21875f);
                Hugo[i].Initialize(10.5f, 40f, 0.21875f);
                //Hugo[i].Initialize(rand.Next(900, 1000) / 100f, WorldVariables.RandomNumber.Next(1000, 7000) / 100f, 0.21875f);
                //Hugo[i].Initialize(10.5f, 64 * 16, 0.21875f);
            }


            //spineTest = new SpineTest();
            //spineTest.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            FrameRateCounter.LoadContent(Content);

            //LevelLoader.LoadContent(Content);
            SpineLoader.LoadContent(GraphicsDevice);            
            TileData.LoadContent(Content, GraphicsDevice);
            WorldManager.LoadContent(graphics, Content, GraphicsDevice);
            LoadWorld(Content);

            Marker = Content.Load<Texture2D>("Images//Marker");

            spriteFont = Content.Load<SpriteFont>("Fonts//TestFont");

            for (int i = 0; i < Hugo.Count; i++)
            {
                Hugo[i].LoadContent(Content);
            }

            

            //spineTest.LoadContent(GraphicsDevice);
            


        }

        private void LoadWorld(ContentManager Content)
        {
            background = new BackgroundManager();
            background.Initialize();

            BackgroundTextureManager.LoadContent(Content);
            background.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            background.UpdateParallax(gameTime, 0, Content);

            CameraManager.ZoomSpeeds[0] = 100f;

            Console.Clear();

            Random t = new Random(10);

            Console.WriteLine(t.Next(0,10));

            #region ControlsCommented

            //float boost = 5f / CameraManager.CamerasRead[0].Zoom;

            //float LeftX1 = currentGamepad1State.ThumbSticks.Left.X * boost;
            //float LeftY1 = -currentGamepad1State.ThumbSticks.Left.Y * boost;
            //float RightX1 = currentGamepad1State.ThumbSticks.Right.X * boost;
            //float RightY1 = -currentGamepad1State.ThumbSticks.Right.Y * boost;

            //float Deadzone = 0f;

            //if (RightX1 > Deadzone || RightX1 < -Deadzone || RightY1 > Deadzone || RightY1 < -Deadzone)
            //    player1.MoveBy(RightX1 * 500, RightY1 * 500);
            //if (LeftX1 > Deadzone || LeftX1 < -Deadzone || LeftY1 > Deadzone || LeftY1 < -Deadzone)
            //    player1.MoveBy(LeftX1, LeftY1);

            //if (LeftX2 > Deadzone || LeftX2 < -Deadzone || LeftY2 > Deadzone || LeftY2 < -Deadzone)
            //    player2.MoveBy(LeftX2, LeftY2);

            //if (currentGamepad1State.IsButtonDown(Buttons.LeftShoulder))
            //    CameraManager.RotationValues[0] -= 0.01f;
            //if (currentGamepad1State.IsButtonDown(Buttons.RightShoulder))
            //    CameraManager.RotationValues[0] += 0.01f;
                    
            //if (currentGamepad2State.IsConnected)
            //    CameraManager.FocusPoints = 2;
            //else
            //    CameraManager.FocusPoints = 1;
            
            //if (CameraManager.ZoomValues.Count > 0)
            //{
            //    CameraManager.ZoomValues[0] -= (currentGamepad1State.Triggers.Left / 50) * CameraManager.CamerasRead[0].Zoom;
            //    CameraManager.ZoomValues[0] += (currentGamepad1State.Triggers.Right / 50) * CameraManager.CamerasRead[0].Zoom;

            //    if (currentGamepad1State.IsButtonDown(Buttons.Y))
            //        CameraManager.ZoomValues[0] = 0.00075f;
            //    else if (currentGamepad1State.IsButtonDown(Buttons.X))
            //        CameraManager.ZoomValues[0] = 0.00055f;
            //    else if (currentGamepad1State.IsButtonDown(Buttons.B))
            //        CameraManager.ZoomValues[0] = 0.00035f;
            //    else if (currentGamepad1State.IsButtonDown(Buttons.A))
            //        CameraManager.ZoomValues[0] = 0.00015f;

            //    if (currentGamepad1State.IsButtonDown(Buttons.LeftStick))
            //        background.UpdateGalaxyOrbit(1f, 1f);

            //    if (currentGamepad1State.IsButtonDown(Buttons.RightStick))
            //        background.ResetGalaxyOrbits();

            //    if (CameraManager.ZoomValues[0] < 0.00003f)
            //        CameraManager.ZoomValues[0] = 0.00003f;
            //}

                        
            //if (currentKeyboardState.IsKeyDown(Keys.W))
            //    player1.MoveBy(0, -boost);
            //if (currentKeyboardState.IsKeyDown(Keys.S))
            //    player1.MoveBy(0, boost);
            //if (currentKeyboardState.IsKeyDown(Keys.A))
            //    player1.MoveBy(-boost, 0);
            //if (currentKeyboardState.IsKeyDown(Keys.D))
            //    player1.MoveBy(boost, 0);

            //for (int i = 0; i < CameraManager.FocusValues.Count; i++)
            //{
            //    if (i == 0)
            //        CameraManager.FocusValues[0] = player1.Position;

            //    if (i == 1)
            //        CameraManager.FocusValues[1] = player2.Position;
            //}

            //GC.Collect();
            //SuppressDraw();

            #endregion

            CameraManager.FocusPoints = 1;
            //CameraManager.FocusValues[0] = new Vector2(1024, 512);
            //CameraManager.FocusValues[0] = Vector2.Zero;
            CameraManager.FocusValues[0] = Hugo[0].Position;
            //CameraManager.RotationValues[0] = -Hugo[0].Rotation;
            //CameraManager.ZoomValues[0] = 1f - (Math.Abs(Hugo[0].physics.GroundSpeed) / 100);
            CameraManager.ZoomValues[0] = 1f;

            for (int i = 0; i < Hugo.Count; i++)
            {
                Hugo[i].Update(gameTime);                                
            }
            
            CameraManager.Update(gameTime);
            FrameRateCounter.Update(gameTime);
            WorldVariables.Update(gameTime);
            WorldManager.Update(gameTime);

            

            //spineTest.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdateWorld(GameTime gameTime)
        {
            background.Update(gameTime);

            for (int i = 0; i < CameraManager.CameraCount; i++)
            {
                background.UpdateParallax(gameTime, i, Content);
            }
        }

        private void PreDraw()
        {
            for (int i = 0; i < CameraManager.CameraCount; i++)
            {
                WorldManager.PreDraw(spriteBatch, i, GraphicsDevice);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            PreDraw();
            GraphicsDevice.Clear(Color.Black);


            //GraphicsDevice.SetRenderTarget(null);
            //GraphicsDevice.Clear(Color.Black);            

            for (int i = 0; i < CameraManager.CameraCount; i++)
            {
                GraphicsDevice.Viewport = CameraManager.ViewportsRead[i];                
                DrawParallax(i);                
                DrawSprites(i);                
                DrawDebug();
            }

            

            base.Draw(gameTime);
        }
        
        private void DrawParallax(int cameraNumber)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, CameraManager.CamerasRead[cameraNumber].TransformRotateOnly);
            
            background.DrawParallax(spriteBatch, cameraNumber);
            
            spriteBatch.End();
        }

        private void DrawSprites(int cameraNumber)
        {            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.CamerasRead[cameraNumber].Transform);
            WorldManager.Draw(spriteBatch, cameraNumber);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.CamerasRead[cameraNumber].Transform);

            SpineLoader.RenderStart(cameraNumber);
            //background.Draw(spriteBatch, cameraNumber);


            //LevelLoader.Draw(spriteBatch, cameraNumber);

            for (int i = 0; i < Hugo.Count; i++)
            {
                Hugo[i].Draw(spriteBatch);
            }
                        
            //spineTest.Draw(cameraNumber);
                     
            SpineLoader.RenderEnd();
            spriteBatch.End();
        }

        private void DrawDebug()
        {
            spriteBatch.Begin();

            FrameRateCounter.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "" + WorldVariables.FrameTime, new Vector2(33, 50), Color.White);


            spriteBatch.End();
        }
    }
}
