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
        Pong pong;

        
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
                 
            pong = new Pong();
            pong.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            StaticInfoBox.LoadContent(Content);
            StaticTests.LoadTextures(Content);
            FontManager.LoadContent(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);


            EffectTest.LoadContent(GraphicsDevice, Content);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            DebugOptions.DebugDisplay.Clear();
            EngineLoader.Update(gameTime, Window.ClientBounds, IsActive);
            InputManager.Update();
            DebugOptions.Update();
            


            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.D1)))
            //    test.Skills.Add(new Skill(0, 0, false, null));
            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.D2)))
            //    test.Skills.Add(new Skill(0, 0, false, new List<Skill> { test.Skills[0] }));
            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.D3)))
            //    test.Skills.Add(new Skill(0, 0, false, new List<Skill> { test.Skills[1] }));
            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.D4)))
            //    test.Skills.Add(new Skill(0, 0, false, new List<Skill> { test.Skills[2] }));
            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.D5)))
            //    test.Skills.Add(new Skill(0, 0, false, new List<Skill> { test.Skills[3] }));
            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.D6)))
            //    test.Skills.Add(new Skill(0, 0, false, new List<Skill> { test.Skills[4] }));

            if (InputManager.KBButtonPressed(true, Keys.Home))
                SheetManager.Initialize(GraphicsDevice);



            
            SheetManager.Update(GraphicsDevice);

            
            pong.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            EngineLoader.Draw(spriteBatch);
            CameraManager.Update();

            Camera cam;
            Viewport view;
            //EffectTest.LoadContent(GraphicsDevice, Content);

            DebugOptions.OnlyDrawNormals = true;

            IsFixedTimeStep = false;
            IsMouseVisible = false;
            graphics.SynchronizeWithVerticalRetrace = false;


            for (int i = 0; i < CameraManager.Cams.Count; i++)
                {
                        cam = CameraManager.Cams[i];

                        //view = new Viewport(cam.);
                        //view = GraphicsManager.ScaledViewport(view);

                    //if (view.X + view.Width <= graphics.PreferredBackBufferWidth && view.Y + view.Height <= graphics.PreferredBackBufferHeight && view.X >= 0 && view.Y >= 0)
                    {
                        //GraphicsDevice.Viewport = view;
                        cam.UpdateRenderTarget(new Rectangle(0, 0, (int)GraphicsManager.GameResolution.X, (int)GraphicsManager.GameResolution.Y), GraphicsDevice);
                        cam.subCameras.Clear();
                        //pong.DrawSkillTrees(spriteBatch, GraphicsDevice, cam);

                        //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, EffectTest.normalTest, cam.Transform);

                        //cam.DrawBack(spriteBatch);


            GraphicsDevice.SetRenderTarget(cam.RenderTarget);
            GraphicsDevice.Clear(Color.Black);



            DrawLighting(cam);
            DrawLightingUI(cam);

            FinalDraw(cam);
                        //for (int o = 0; o < cam.subCameras.Count; o++)
                        //{
                        //    cam.subCameras[o].Draw(spriteBatch);
                        //}
                        


                        //cam.DrawFront(spriteBatch);
                        //DrawRectangle3.Draw(GraphicsDevice, cam);



                        //EffectTest.EndDraw(GraphicsDevice, cam.RenderTarget);

                        //GraphicsDevice.SetRenderTarget(null);
                        //GraphicsDevice.Clear(Color.Black);




                        //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);


                        //spriteBatch.Draw(cam.RenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        ////spriteBatch.Draw(LightManager.NormalMap, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //spriteBatch.End();

                        


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

        private void DrawPreLighting(Camera cam)
        {
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
                    GraphicsDevice.SetRenderTarget(cam.RenderTarget);
                    GraphicsDevice.Clear(Color.Transparent);
                }
                else
                {
                    DebugOptions.DrawNormals = true;
                    GraphicsDevice.SetRenderTarget(cam.NormalMap);
                    GraphicsDevice.Clear(Color.Transparent);
                }

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Transform);

                pong.Draw(spriteBatch, GraphicsDevice, cam);

                spriteBatch.End();
            }

            List<Light> Lights = pong.GetLights();

            #region Apply Normals

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(GraphicsDevice, cam.RenderTarget);
                else
                    LightManager.StartNormalMap(GraphicsDevice);


                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(cam.RenderTarget, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(cam.NormalMap, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            LightManager.DrawFinal(GraphicsDevice, spriteBatch, Lights, cam);

            #endregion
        }

        private void DrawPostLighting(Camera cam)
        {
        }



        private void DrawPreLightingUI(Camera cam)
        {
        }

        private void DrawLightingUI(Camera cam)
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    DebugOptions.DrawNormals = false;
                    GraphicsDevice.SetRenderTarget(cam.RenderTargetUI);
                    GraphicsDevice.Clear(Color.Transparent);
                }
                else
                {
                    DebugOptions.DrawNormals = true;
                    GraphicsDevice.SetRenderTarget(cam.NormalMapUI);
                    GraphicsDevice.Clear(Color.Transparent);
                }
                
            }

            List<Light> Lights = pong.GetLights();

            #region Apply Normals

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(GraphicsDevice, cam.RenderTargetUI);
                else
                    LightManager.StartNormalMap(GraphicsDevice);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(cam.RenderTargetUI, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(cam.NormalMapUI, Vector2.Zero, Color.White);

                spriteBatch.End();

            }

            LightManager.DrawFinal(GraphicsDevice, spriteBatch, Lights, cam);

            #endregion


            pong.DrawSkillTrees(spriteBatch, GraphicsDevice, cam);
        }

        private void DrawPostLightingUI(Camera cam)
        {
        }


        private void FinalDraw(Camera cam)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            if (!InputManager.KBButtonPressed(false, Keys.LeftControl))
                EffectTest.BeginDraw(GraphicsDevice);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            

            if (!InputManager.KBButtonPressed(false, Keys.RightControl))
            {
                spriteBatch.Draw(cam.RenderTarget, Vector2.Zero, Color.White);
                spriteBatch.Draw(cam.RenderTargetUI, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(cam.NormalMap, Vector2.Zero, Color.White);
                spriteBatch.Draw(cam.NormalMapUI, Vector2.Zero, Color.White);
            }

            spriteBatch.End();

            if (!InputManager.KBButtonPressed(false, Keys.LeftControl))
                EffectTest.EndDraw(GraphicsDevice, cam.RenderTarget);
        }
    }
}
