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
            StaticInfoBox.LoadContent(Content);
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
            EngineLoader.Update(gameTime, IsActive);
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
            IsMouseVisible = true;
            graphics.SynchronizeWithVerticalRetrace = false;


            for (int i = 0; i < CameraManager.MainCameras.Count; i++)
            {
                cam = CameraManager.MainCameras[i];

                cam.ClearRenderTargets();
                {
                    //cam.UpdateRenderTarget((int)GraphicsManager.VirtualResolution.X, (int)GraphicsManager.VirtualResolution.Y);


                    GraphicsDevice.SetRenderTarget(cam.RenderTarget);
                    GraphicsDevice.Clear(Color.Black);

                    cam.SetRenderTarget(RenderMode.Regular);

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Transform);

                    if (DebugOptions.DebugActive)
                        DebugOptions.Draw(spriteBatch);
                    
                    spriteBatch.End();

                    SkeletonQuad S1 = new SkeletonQuad(new Vector2(-200, -200), 100, Color.White, "Core", "Marker");
                    SkeletonQuad S2 = new SkeletonQuad(new Vector2(0, 0), 100, Color.White, "InfoBox", "ColorBlack");
                    SkeletonQuad S3 = new SkeletonQuad(new Vector2(200, 200), 100, Color.White, "InfoBox", "ColorPallette");

                    //S1.AddSoloCamera("MainCamera2");
                    //S1.AddSoloCamera("MainCamera3");

                    for (int n = 0; n < 1; n++)
                    {
                        QuadManager.AddQuad(S1);
                        QuadManager.AddQuad(S2);
                        QuadManager.AddQuad(S3);
                    }

                    QuadManager.Draw(FillMode.Solid, cam);


                    //DrawLighting(cam);

                    //FinalDraw(cam);

                    CameraManager.DrawMainCameras();
                }
            }




            #region Render Mouse
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GraphicsManager.ScreenMatrix);
               
            //Color CursorColor = Color.White;
            //spriteBatch.Draw(StaticTests.TestCursor, InputManager.MousePosition, CursorColor);

            //spriteBatch.End();
            #endregion



            base.Draw(gameTime);
        }


        private void DrawLighting(Camera cam)
        {
            LightManager.AmbientBrightness = 0.1f;
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

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Transform);

                if (DebugOptions.DebugActive)
                    DebugOptions.Draw(spriteBatch);

                spriteBatch.End();
            }

            List<Light> Lights = new List<Light>();

            //for (int i = 0; i < Lights.Count; i++)
            //{
            //    Vector3.Transform(Lights[i].Position, cam.Transform);
            //}


            #region Apply Normals


            cam.ApplyNormals(Lights);

            //Lights.RemoveRange(6, Lights.Count - 6);
            Lights.Clear();
            LightManager.AmbientBrightness = 0.5f;


            //cam.ApplyNormalsUI(spriteBatch, GraphicsDevice, Lights);



            #endregion
        }


        //private void FinalDraw(Camera cam)
        //{
        //    cam.DrawFinalRender();

        //    GraphicsDevice.SetRenderTarget(null);
        //    GraphicsDevice.Clear(Color.Purple);

        //    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GraphicsManager.ScreenMatrix);
        //    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.Identity);

        //    Vector2 Position = Vector2.Zero;
        //    float Scale = 0.5f;

        //    //Vector2 RenderSize = new Vector2(cam.FinalRender.Width, cam.FinalRender.Height);

        //    //Scale = RenderSize / 


        //    spriteBatch.Draw(cam.FinalRender, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        //    spriteBatch.Draw(cam.FinalRender, Position + new Vector2(cam.FinalRender.Width / 2f, 0), null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

        //    //spriteBatch.Draw(cam.RenderTargetBloom, Vector2.Zero, Color.White);
        //    //spriteBatch.Draw(cam.BloomSceneTarget, new Vector2(384f * 0f, 0f), null, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 0f);
        //    //spriteBatch.Draw(cam.BloomTarget1, new Vector2(384f * 1f, 0f), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        //    //spriteBatch.Draw(cam.BloomTarget2, new Vector2(384f * 2f, 0f), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

        //    spriteBatch.End();

        //}

    }
}
