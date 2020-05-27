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
using SkeletonEngine;

namespace SkinnerBox
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            InactiveSleepTime = new TimeSpan(0);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            EngineLoader.InitializeEngine(Window, graphics, GraphicsDevice, Content, spriteBatch);
            SheetManager.Initialize(GraphicsDevice);
            InputManager.Initialize();
            DrawRectangle3.Initialize(GraphicsDevice);
   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            StaticTests.LoadTextures(Content);
            FontManager.LoadContent(Content);


            BloomEffect.LoadContent(GraphicsDevice, Content);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {           
            SheetManager.Update(GraphicsDevice);
            DebugOptions.DebugTextLines.Clear();
            InputManager.Update();
            EngineLoader.Update(gameTime, IsActive);
            DebugOptions.Update();
            
            if (InputManager.KBPressed(true, Keys.Home))
                SheetManager.Initialize(GraphicsDevice);



            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            graphics.SynchronizeWithVerticalRetrace = false;
            
            CameraManager.Update();
            EngineLoader.Draw();

            base.Draw(gameTime);
        }



        private void DrawLighting(Camera camera)
        {
            LightManager.AmbientBrightness = 0.1f;
            LightManager.AmbientColor = Color.White;
            LightManager.SpecularStrength = 0.5f;

            for (int i = 0; i < 2; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

                if (DebugOptions.DebugActive)
                    DebugOptions.Draw(camera);

                spriteBatch.End();
            }

        }

    }
}
