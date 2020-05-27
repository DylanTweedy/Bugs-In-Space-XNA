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
            EngineLoader.InitializeEngine(Window.Handle, graphics, GraphicsDevice, Content);
            SheetManager.Initialize(GraphicsDevice);
            InputManager.Initialize();
            DrawRectangle3.Initialize(GraphicsDevice);
                             
            base.Initialize();
        }

        protected override void LoadContent()
        {
            StaticInfoBox.LoadContent(Content);
            StaticTests.LoadTextures(Content);
            FontManager.LoadContent(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);


            BloomEffect.LoadContent(GraphicsDevice, Content);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            SheetManager.Update(GraphicsDevice);

            DebugOptions.DebugDisplay.Clear();
            EngineLoader.Update(gameTime, Window.ClientBounds, IsActive);
            InputManager.Update();
            DebugOptions.Update();
            
            if (InputManager.KBButtonPressed(true, Keys.Home))
                SheetManager.Initialize(GraphicsDevice);



            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            EngineLoader.Draw(spriteBatch);
            CameraManager.Update();

            Camera cam;

            DebugOptions.OnlyDrawNormals = true;

            IsFixedTimeStep = false;
            IsMouseVisible = false;
            graphics.SynchronizeWithVerticalRetrace = false;


            for (int i = 0; i < CameraManager.Cams.Count; i++)
            {
                cam = CameraManager.Cams[i];

                cam.ClearRenderTargets(GraphicsDevice);
                {
                    cam.UpdateRenderTarget(new Rectangle(0, 0, (int)GraphicsManager.GameResolution.X, (int)GraphicsManager.GameResolution.Y), GraphicsDevice);
                    cam.subCameras.Clear();


                    GraphicsDevice.SetRenderTarget(cam.RenderTarget);
                    GraphicsDevice.Clear(Color.Black);



                    DrawLighting(cam);

                    FinalDraw(cam);

                }
            }


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GraphicsManager.ScreenMatrix);


            //pong.Draw(spriteBatch);



            Color CursorColor = Color.White;
            spriteBatch.Draw(StaticTests.TestCursor, InputManager.MousePosition, CursorColor);

            if (DebugOptions.DebugActive)
                DebugOptions.Draw(spriteBatch);
            
            spriteBatch.End();



            //test.Draw(spriteBatch, GraphicsDevice);

            base.Draw(gameTime);
        }

        private void DrawLighting(Camera cam)
        {
            LightManager.AmbientBrightness = 0.01f;
            LightManager.AmbientColor = Color.White;
            LightManager.SpecularStrength = 0.5f;

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    DebugOptions.DrawNormals = false;
                }
                else
                {
                    DebugOptions.DrawNormals = true;
                }

                //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Transform);


                //spriteBatch.End();
            }

            List<Light> Lights = new List<Light>();

            //for (int i = 0; i < Lights.Count; i++)
            //{
            //    Vector3.Transform(Lights[i].Position, cam.Transform);
            //}


            #region Apply Normals


            cam.ApplyNormals(spriteBatch, GraphicsDevice, Lights);

            //Lights.RemoveRange(6, Lights.Count - 6);
            Lights.Clear();
            LightManager.AmbientBrightness = 0.5f;


            //cam.ApplyNormalsUI(spriteBatch, GraphicsDevice, Lights);



            #endregion
        }


        private void FinalDraw(Camera cam)
        {
            cam.DrawFinalRender(spriteBatch, GraphicsDevice);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            spriteBatch.Draw(cam.FinalRender, Vector2.Zero, Color.White);

            //spriteBatch.Draw(cam.RenderTargetBloom, Vector2.Zero, Color.White);
            //spriteBatch.Draw(cam.BloomSceneTarget, new Vector2(384f * 0f, 0f), null, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(cam.BloomTarget1, new Vector2(384f * 1f, 0f), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(cam.BloomTarget2, new Vector2(384f * 2f, 0f), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            spriteBatch.End();

        }
    }
}
