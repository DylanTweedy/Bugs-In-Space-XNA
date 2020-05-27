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

namespace GameRebuild
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Temp BG Colour
        Color BG = new Color(5, 5, 5);

        public Game1()
        {     
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = false;

            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 720;

            //graphics.PreferredBackBufferWidth = 1600;
            //graphics.PreferredBackBufferHeight = 900;
            
            //graphics.PreferredBackBufferWidth = 1920 * 3;
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1180;

            graphics.PreferredBackBufferWidth = 3840;
            graphics.PreferredBackBufferHeight = 2160;
        }


        protected override void Initialize()
        {
            ColorManager.Initialize();
            WorldVariables.Initialize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            LifeManager.Initialize();
            InputManager.Initialize();
            CameraManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            StaticTests.LoadTextures(Content);
            SM.Initialize(Content, GraphicsDevice);


            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //Console.Clear();
            InputManager.Update();
            WorldVariables.Update(gameTime);            
            



            float x = InputManager.GP1.ThumbSticks.Left.X * 5;
            float y = -InputManager.GP1.ThumbSticks.Left.Y * 5;

            float r = CameraManager.Cams[0].R;
            float cos = (float)Math.Cos(r);
            float sin = (float)Math.Sin(r);
            float z = CameraManager.Cams[0].Z;

            CameraManager.Cams[0].SetPosition(new Vector2((x * cos) - (-y * sin), (y * cos) - (x * sin)), 10);

            if (InputManager.GP1.IsButtonDown(Buttons.LeftShoulder))
                CameraManager.Cams[0].SetRotation(0.1f, 3f);
            if (InputManager.GP1.IsButtonDown(Buttons.RightShoulder))
                CameraManager.Cams[0].SetRotation(-0.1f, 3f);

            CameraManager.Cams[0].SetZoom(InputManager.GP1.Triggers.Right * 0.01f, 10);
            CameraManager.Cams[0].SetZoom(-InputManager.GP1.Triggers.Left * 0.01f, 10);
            
            if (InputManager.KB.IsKeyDown(Keys.Up))
                CameraManager.Cams[0].SetRotation(0f);
            if (InputManager.KB.IsKeyDown(Keys.Down))
                CameraManager.Cams[0].SetRotation((float)Math.PI);
            if (InputManager.KB.IsKeyDown(Keys.Left))
                CameraManager.Cams[0].SetRotation(((float)Math.PI / 2f) * 3f);
            if (InputManager.KB.IsKeyDown(Keys.Right))
                CameraManager.Cams[0].SetRotation((float)Math.PI / 2f);

            Vector2 p = new Vector2(InputManager.pM.X - InputManager.M.X, InputManager.pM.Y - InputManager.M.Y);

            x = p.X;
            y = p.Y;

            Vector2 Butts;
            Butts.X = (x * cos) - (-y * sin);
            Butts.Y = (y * cos) - (x * sin);

            
            if (InputManager.M.LeftButton == ButtonState.Pressed)
                CameraManager.Cams[0].SetPosition(Butts / CameraManager.Cams[0].Z, 10);
            
            List<string> Info = CameraManager.Cams[0].Write();

            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (CameraManager.Cams[0].Z < 0.1f)
                CameraManager.Cams[0].SetZoom(0.1f);
            
            BG = Color.Black;

            GraphicsDevice.Clear(BG);
            CameraManager.Update();

            for (byte i = 0; i < CameraManager.Cams.Count; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.Cams[i].Transform);
                CameraManager.DrawBack(spriteBatch);
                spriteBatch.End();    
            }

            for (byte i = 0; i < CameraManager.Cams.Count; i++)
            {    
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.Cams[i].Transform);
                LifeManager.Draw(spriteBatch, i);
                spriteBatch.End();    
            }
            
            

            spriteBatch.Begin();
            spriteBatch.Draw(StaticTests.TestCursor, new Vector2(InputManager.M.X, InputManager.M.Y), Color.White);

            int ID = 3;

            spriteBatch.Draw(
                SM.StarsBack.S(ID),
                Vector2.Zero, Color.White);
            spriteBatch.Draw(SM.StarsFront.Sheets[0], new Vector2(0, 32), Color.White);
            
            spriteBatch.End();

            for (byte i = 0; i < CameraManager.Cams.Count; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.Cams[i].Transform);
                CameraManager.DrawFront(spriteBatch);
                spriteBatch.End();  
            }

            base.Draw(gameTime);
        }
    }
}
