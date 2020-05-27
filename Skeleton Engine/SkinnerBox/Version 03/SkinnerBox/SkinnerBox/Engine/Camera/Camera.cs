using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    public enum RenderMode
    {
        Regular,
        Bloom,
        UI,
        Overlay,
        Final
    }

    [Serializable()]
    public class Camera
    {
        [Serializable()]
        public class SubCamera
        {
            public Texture2D Tex;
            public Vector2 Position;

            public SubCamera(Texture2D tex, Vector2 position)
            {
                Tex = tex;
                Position = position;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(Tex, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        public Matrix Transform;

        public Vector2 Position;
        public Vector2 PreviousPosition;
        public Vector2 PositionDestination;

        public float MoveTime = 10f;


        public float Rotation;
        float RotationDestination;

        public float Zoom;
        //float PreviousZoom;
        public float ZoomDestination;

        public Vector2 Origin;

        //public Rectangle viewport;

        public long PositionX;
        public long PositionY;

        public List<SubCamera> subCameras = new List<SubCamera>();
        public CameraSettings settings = new CameraSettings();
        public int Parent = -1;

        //float PT;
        //float RT;
        //float ZT;

        InputType Controller;

        public List<ParallaxParticles> Para = new List<ParallaxParticles>();

        public RenderTarget2D RenderTarget;
        public RenderTarget2D NormalMap;

        public RenderTarget2D RenderTargetUI;
        public RenderTarget2D RenderTargetOverlay;
        public RenderTarget2D NormalMapUI;

        public RenderTarget2D RenderTargetBloom;
        public RenderTarget2D NormalMapBloom;

        public RenderTarget2D FinalRender;

        public RenderTarget2D ColorMapTemp;
        public RenderTarget2D NormalMapTemp;
        public RenderTarget2D ShadowMapTemp;

        public RenderTarget2D BloomSceneTarget;
        public RenderTarget2D BloomTarget1;
        public RenderTarget2D BloomTarget2;

        public List<RenderTarget2D> RenderBuffer = new List<RenderTarget2D>();
        public float Rendertimer;
        public float BlurResolution = 60f;
        public float BlurAmount = 6f;

        //NewParallax Para2;

        public Camera()
        {
            //viewport = Rectangle.Empty;

            Position = Vector2.Zero;
            PositionDestination = Vector2.Zero;
            Rotation = 0f;
            RotationDestination = 0f;
            Zoom = 1f;
            ZoomDestination = 1f;
            PreviousPosition = Position;
        }

        public Camera(Vector2 position, float rotation, float zoom, InputType input)
        {
            //viewport = Rectangle.Empty;

            Position = position;
            PositionDestination = position;
            Rotation = rotation;
            RotationDestination = rotation;
            Zoom = zoom;
            ZoomDestination = zoom;
            PreviousPosition = Position;

            List<ParticleMovement> move = new List<ParticleMovement>();

            for (int i = 0; i < Para.Count; i++)
                Intensity.Add(1f);
        }

        private void UpdatePosition()
        {
            if (Position.X >= 1000000f)
            {
                PositionX++;
                Position.X -= 2000000f;
                PositionDestination.X -= 2000000f;
                PreviousPosition.X -= 2000000f;

                if (Position.X >= 2000000f)
                {
                    float x = Position.X / 2000000f;

                    PositionX += (int)x;
                    Position.X -= 2000000f * x;
                    PositionDestination.X -= 2000000f * x;
                    PreviousPosition.X -= 2000000f * x;
                }
            }

            if (Position.X <= -1000000f)
            {
                PositionX--;
                Position.X += 2000000f;
                PositionDestination.X += 2000000f;
                PreviousPosition.X += 2000000f;

                if (Position.X <= -2000000f)
                {
                    float x = Position.X / -2000000f;

                    PositionX -= (int)x;
                    Position.X += 2000000f * x;
                    PositionDestination.X += 2000000f * x;
                    PreviousPosition.X += 2000000f * x;
                }
            }

            if (Position.Y >= 1000000f)
            {
                PositionY++;
                Position.Y -= 2000000f;
                PositionDestination.Y -= 2000000f;
                PreviousPosition.Y -= 2000000f;

                if (Position.Y >= 2000000f)
                {
                    float y = Position.Y / 2000000f;

                    PositionY += (int)y;
                    Position.Y -= 2000000f * y;
                    PositionDestination.Y -= 2000000f * y;
                    PreviousPosition.Y -= 2000000f * y;
                }
            }

            if (Position.Y <= -1000000f)
            {
                PositionY--;
                Position.Y += 2000000f;
                PositionDestination.Y += 2000000f;
                PreviousPosition.Y += 2000000f;

                if (Position.Y <= -2000000f)
                {
                    float y = Position.Y / -2000000f;

                    PositionY -= (int)y;
                    Position.Y += 2000000f * y;
                    PositionDestination.Y += 2000000f * y;
                    PreviousPosition.Y += 2000000f * y;
                }
            }

        }

        public void Update()
        {
            RotationDestination += 0.2f * GlobalVariables.WorldTime;
            RotationDestination = 0f;

            List<ParticleMovement> move = new List<ParticleMovement>();

            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.P)))
            //{
            //    move.Add(new PM.ApplyAcceleration(Wind + new Vector2(0, Gravity)));
            //    move.Add(new PM.SetMaxSpeed((600f)));

            //    Para[0].SetState(0.4f, 0.75f, 0.5f, "Liquid", ColorManager.GetShades(Color.SkyBlue, 0.75f), 0.755f, 0.9f, false, true, move);
            //    Para[1].SetState(0.4f, 0.75f, 0.5f, "Liquid", ColorManager.GetShades(Color.SkyBlue, 0.75f), 0.75f, 0.9f, false, true, move);
            //    Para[2].SetState(0.75f, 2f, 0.5f, "Liquid", ColorManager.GetShades(Color.SkyBlue, 0.75f), 0.5f, 0.75f, false, true, move);
            //}

            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.O)))
            //{
            //    move.Add(new PM.ApplyAcceleration(Wind + new Vector2(0, Gravity)));
            //    move.Add(new PM.RandomMovement(rand, Intensity[0], true, new Vector2(1f, 0)));
            //    move.Add(new PM.RandomMovement(rand, Intensity[0], true, new Vector2(0, 1f)));
            //    move.Add(new PM.SetMaxSpeed((200f)));

            //    Para[0].SetState(0.4f, 1f, 0.5f, "Dust", new List<Color>() { Color.White }, 1.25f, 1f, false, true, move);
            //    Para[1].SetState(0.4f, 1f, 0.5f, "Dust", new List<Color>() { Color.White }, 1.25f, 1f, false, false, move);
            //    Para[2].SetState(0.75f, 2f, 0.5f, "Dust", new List<Color>() { Color.White }, 0.75f, 0.75f, false, false, move);
            //}


            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.I)))
            //{
            //    Para[3].SetState(0.25f, 1f, 1.5f, "Stars", ColorManager.Compliment(ColorManager.RandomFullColor()), 1f, 0.75f, true, false, move);
            //    Para[4].SetState(0.1f, 1f, 0.25f, "GasCloud", ColorManager.Compliment(ColorManager.RandomFullColor()), 100f, 0.5f, false, false, move);
            //    Para[5].SetState(1f, 1.5f, 0f, "Dust", new List<Color>() { Color.White }, 1f, 1f, true, false, move);
            //}

            //if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.U)))
            //{
            //    move.Add(new PM.Wave(rand, 50f, 0.75f, 0.75f, WaveTypeEnum.Sine));
            //    move.Add(new PM.Wave(rand, 50f, 1.5f, 0.75f, WaveTypeEnum.Triangle));
            //    move.Add(new PM.ApplyAcceleration(new Vector2(0, -Gravity)));
            //    move.Add(new PM.SetMaxSpeed((250f)));

            //    Para[0].SetState(0.1f, 0.75f, 0.33f, "Test", new List<Color>() { Color.White }, 5f, 1f, false, true, move);
            //    Para[1].SetState(0.1f, 1.25f, 0.33f, "Test", new List<Color>() { Color.White }, 5f, 1f, false, true, move);
            //    Para[2].SetState(0.1f, 2f, 0.33f, "Test", new List<Color>() { Color.White }, 5f, 1f, false, true, move); 
            //}

            if (InputManager.MButtonPressed(false, 0))
            {
                MoveTime = 10f;
                PositionDestination -= InputManager.MouseVelocity / Zoom;




                //CameraManager.Cams[0].PositionDestination = CameraManager.Cams[0].Position;
            }

            if (InputManager.MScrollWheel != 0)
            {
                ZoomDestination += InputManager.MScrollWheel * 0.0005f * Zoom;
                //ZoomDestination = Zoom;
            }

            if (Zoom < 0.00001f)
            {
                Zoom = 0.00001f;
                ZoomDestination = 0.00001f;
            }


            settings.Update();
            UpdatePosition();

            if (Position != PositionDestination)
                Position += ((PositionDestination - Position) * ((float)GlobalVariables.FrameTime * MoveTime));
            if (Rotation != RotationDestination)
                Rotation += ((RotationDestination - Rotation) * ((float)GlobalVariables.FrameTime * MoveTime));
            if (Zoom != ZoomDestination)
                Zoom += ((ZoomDestination - Zoom) * ((float)GlobalVariables.FrameTime * MoveTime));

            //Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f) / Zoom;

            if (RenderTarget != null)
                Origin = new Vector2(RenderTarget.Width / 2f, RenderTarget.Height / 2f) / Zoom;

            //if (Origin.X >= 1000000 || Origin.Y >= 1000000)
            //{
            //    Z++;
            //    ZD++;
            //    PZ++;
            //    Origin = WorldVariables.ScreenCenter / Z;
            //}


            UpdateMatrix();


            UpdateParallax();

            PreviousPosition = Position;
            //PreviousZoom = Zoom;

            //subCameras.Update(viewport);
        }

        private void UpdateParallax()
        {
            Gravity = 400f;

            WindAcceleration = Vector2.Zero;

            float max = 1000f;

            WindAcceleration.X = 1 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false);
            WindAcceleration.Y = 1 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false);

            WindAcceleration.X *= (float)rand.NextDouble() * 1000f;
            WindAcceleration.Y *= (float)rand.NextDouble() * 100f;

            Wind += WindAcceleration * GlobalVariables.WorldTime;


            if (InputManager.KBButtonPressed(false, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Left)))
                Wind.X -= 500f * GlobalVariables.WorldTime;
            if (InputManager.KBButtonPressed(false, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Right)))
                Wind.X += 500f * GlobalVariables.WorldTime;
            if (InputManager.KBButtonPressed(false, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Up)))
                Wind.Y -= 500f * GlobalVariables.WorldTime;
            if (InputManager.KBButtonPressed(false, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Down)))
                Wind.Y += 500f * GlobalVariables.WorldTime;

            if (Wind.X > max)
                Wind.X = max;
            if (Wind.X < -max)
                Wind.X = -max;

            if (Wind.Y > max / 2)
                Wind.Y = max / 2;
            if (Wind.Y < -max / 4)
                Wind.Y = -max / 4;

            //Wind = Vector2.Zero;

            //DebugOptions.DebugDisplay.Add("" + Wind);
            //DebugOptions.DebugDisplay.Add("" + Intensity[0]);

            for (int i = 0; i < Para.Count; i++)
            {


                if (InputManager.KBButtonPressed(false, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.NumPad4)))
                    Intensity[i] -= 1000f * GlobalVariables.WorldTime;
                if (InputManager.KBButtonPressed(false, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.NumPad6)))
                    Intensity[i] += 1000f * GlobalVariables.WorldTime;

                if (Intensity[i] > 2000f)
                    Intensity[i] = 2f;
                if (Intensity[i] < 1f)
                    Intensity[i] = 1f;


                Para[i].Update(this, Intensity[i]);
            }
        }

        Vector2 WindAcceleration;
        Random rand = new Random();

        public Vector2 Wind;
        public float Gravity;
        public List<float> Intensity = new List<float>();

        private void UpdateMatrix()
        {
            Transform =
                //Matrix.Identity *
            Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
            Matrix.CreateScale(Zoom);
        }

        public void SetPosition(Vector2 p)
        {
            Position = p;
            PositionDestination = p;
            UpdateMatrix();
        }

        public void SetRotation(float r)
        {
            Rotation = r;
            RotationDestination = r;
            UpdateMatrix();
        }

        public void SetZoom(float z)
        {
            Zoom = z;
            ZoomDestination = z;
            UpdateMatrix();
        }

        //public void MovePosition(Vector2 p, float t)
        //{
        //    PositionDestination += p;
        //    PT = t;
        //}

        //public void SetPosition(Vector2 p, float t)
        //{
        //    PositionDestination = p;
        //    PT = t;
        //}

        //public void SetRotation(float r, float t)
        //{
        //    RD += r;
        //    RT = t;
        //}

        //public void SetZoom(float z, float t)
        //{
        //    ZD += z;
        //    ZT = t;

        //    if (ZD <= 0)
        //        ZD = 0;
        //}

        public Vector2 GetRoughPosition()
        {
            Vector2 P2;

            P2 = Position;
            P2.X += PositionX * 2000000f;
            P2.Y += PositionY * 2000000f;

            return P2;
        }

        public Vector2 GetOffset()
        {
            return new Vector2(2000000f * PositionX, 2000000f * PositionY);
        }

        public void DrawBack(SpriteBatch spriteBatch)
        {
            for (int i = Para.Count - 1; i >= 0; i--)
                Para[i].Draw(spriteBatch, this, false);
        }

        public void DrawFront(SpriteBatch spriteBatch)
        {
            for (int i = Para.Count - 1; i >= 0; i--)
                Para[i].Draw(spriteBatch, this, true);

            //Para2.Draw(spriteBatch, Transform);
        }

        public bool IsOnScreen(float ObjectRadius, Vector2 ObjectPosition)
        {
            bool OnScreen = true;








            return OnScreen;
        }

        public void ClearRenderTargets(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(RenderTarget);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(RenderTargetBloom);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(RenderTargetUI);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(RenderTargetOverlay);
            graphicsDevice.Clear(Color.Transparent);

            graphicsDevice.SetRenderTarget(NormalMap);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(NormalMapBloom);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(NormalMapUI);
            graphicsDevice.Clear(Color.Transparent);

            graphicsDevice.SetRenderTarget(FinalRender);
            graphicsDevice.Clear(Color.Transparent);
        }

        public void SetRenderTarget(GraphicsDevice graphicsDevice, RenderMode renderMode)
        {
            switch (renderMode)
            {
                case RenderMode.Regular:
                    if (!DebugOptions.DrawNormals)
                        graphicsDevice.SetRenderTarget(RenderTarget);
                    else
                        graphicsDevice.SetRenderTarget(NormalMap);
                    break;

                case RenderMode.Bloom:
                    if (!DebugOptions.DrawNormals)
                        graphicsDevice.SetRenderTarget(RenderTargetBloom);
                    else
                        graphicsDevice.SetRenderTarget(NormalMapBloom);
                    break;

                case RenderMode.UI:
                    if (!DebugOptions.DrawNormals)
                        graphicsDevice.SetRenderTarget(RenderTargetUI);
                    else
                        graphicsDevice.SetRenderTarget(NormalMapUI);
                    break;

                case RenderMode.Overlay:
                    graphicsDevice.SetRenderTarget(RenderTargetOverlay);
                    break;

                case RenderMode.Final:
                    graphicsDevice.SetRenderTarget(FinalRender);
                    break;

            }
        }

        public void UpdateRenderTarget(Rectangle rect, GraphicsDevice graphicsDevice)
        {
            int BufferSize = (int)(BlurResolution / BlurAmount);

            if (RenderTarget == null || (rect.Width != RenderTarget.Width || rect.Height != RenderTarget.Height) || BufferSize != RenderBuffer.Count)
            {
                RenderTarget = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMap = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                RenderTargetUI = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                RenderTargetOverlay = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMapUI = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                RenderTargetBloom = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMapBloom = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                FinalRender = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);


                if (BufferSize < RenderBuffer.Count)
                {
                    RenderBuffer.RemoveRange(BufferSize, RenderBuffer.Count - BufferSize);
                    RenderBuffer.RemoveAt(RenderBuffer.Count - 1);
                }

                if (RenderBuffer.Count < BufferSize)
                {
                    if (RenderBuffer.Count != 0)
                        RenderBuffer.RemoveAt(RenderBuffer.Count - 1);

                    int BCount = RenderBuffer.Count;
                    for (int i = 0; i < BufferSize - BCount; i++)
                    {
                        RenderBuffer.Add(new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                            graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents));
                    }
                }
                else
                    for (int i = 0; i < RenderBuffer.Count; i++)
                    {
                        RenderBuffer[i] = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                            graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                    }



            }
        }

        public void UpdateRenderTargets(GraphicsDevice graphicsDevice, int Width, int Height)
        {
            if (ColorMapTemp == null || ColorMapTemp.Width != Width || ColorMapTemp.Height != Height)
            {
                ColorMapTemp = new RenderTarget2D(graphicsDevice, Width, Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                NormalMapTemp = new RenderTarget2D(graphicsDevice, Width, Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                ShadowMapTemp = new RenderTarget2D(graphicsDevice, Width, Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
            }

        }

        public void ApplyNormals(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, List<Light> Lights)
        {
            UpdateRenderTargets(graphicsDevice, RenderTarget.Width, RenderTarget.Height);

            for (int i = 0; i < Lights.Count; i++)
            {
                Lights[i].Position = Vector3.Transform(Lights[i].Position, Transform);
                Lights[i].LightDecay = (int)(Lights[i].LightDecay * Zoom);
            }

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(graphicsDevice, RenderTargetBloom, ColorMapTemp, NormalMapTemp, ShadowMapTemp);
                else
                    LightManager.StartNormalMap(graphicsDevice);


                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(NormalMapBloom, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            LightManager.DrawFinal(graphicsDevice, spriteBatch, Lights, this);



            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(graphicsDevice, RenderTarget, ColorMapTemp, NormalMapTemp, ShadowMapTemp);
                else
                    LightManager.StartNormalMap(graphicsDevice);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(NormalMap, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            LightManager.DrawFinal(graphicsDevice, spriteBatch, Lights, this);
        }

        public void ApplyNormalsUI(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, List<Light> Lights)
        {
            for (int i = 0; i < Lights.Count; i++)
            {
                Lights[i].Position = Vector3.Transform(Lights[i].Position, Transform);
                Lights[i].LightDecay = (int)(Lights[i].LightDecay * Zoom);
            }

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(graphicsDevice, RenderTargetUI, ColorMapTemp, NormalMapTemp, ShadowMapTemp);
                else
                    LightManager.StartNormalMap(graphicsDevice);


                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(RenderTargetUI, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(NormalMapUI, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            LightManager.DrawFinal(graphicsDevice, spriteBatch, Lights, this);
        }

        private void UpdateBloomTargets(GraphicsDevice graphicsDevice, int width, int height)
        {
            PresentationParameters pp = graphicsDevice.PresentationParameters;
            SurfaceFormat format = pp.BackBufferFormat;

            BloomSceneTarget = new RenderTarget2D(graphicsDevice, width, height, false,
                                                   format, pp.DepthStencilFormat, pp.MultiSampleCount,
                                                   RenderTargetUsage.PreserveContents);

            width /= 2;
            height /= 2;

            BloomTarget1 = new RenderTarget2D(graphicsDevice, width, height, false, format, DepthFormat.None);
            BloomTarget2 = new RenderTarget2D(graphicsDevice, width, height, false, format, DepthFormat.None);
        }

        private void PreDrawBloom(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (BloomSceneTarget == null || (BloomSceneTarget.Width != RenderTarget.Width || BloomSceneTarget.Height != RenderTarget.Height))
                UpdateBloomTargets(graphicsDevice, RenderTarget.Width, RenderTarget.Height);

            BloomEffect.BeginDraw(graphicsDevice, BloomSceneTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, Color.White);
            else
                spriteBatch.Draw(NormalMapBloom, Vector2.Zero, Color.White);

            spriteBatch.End();
            BloomEffect.EndDraw(spriteBatch, graphicsDevice, RenderTargetBloom, BloomTarget1, BloomTarget2);

        }

        private void PreDrawMotionBlur(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (RenderBuffer.Count > 1)
            {
                if (Rendertimer > 1f / BlurResolution)
                {
                    for (int i = RenderBuffer.Count - 1; i > 0; i--)
                    {
                        RenderBuffer[i] = RenderBuffer[i - 1];
                        RenderBuffer[i - 1] = null;
                    }

                    RenderBuffer[0] = RenderBuffer[RenderBuffer.Count - 1];
                    RenderBuffer[RenderBuffer.Count - 1] = null;

                    graphicsDevice.SetRenderTarget(RenderBuffer[1]);
                    graphicsDevice.Clear(Color.Transparent);


                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                    if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                        //spriteBatch.Draw(RenderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
                    else
                        spriteBatch.Draw(NormalMap, Vector2.Zero, Color.White);

                    if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                        //spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, Color.White);
                    else
                        spriteBatch.Draw(NormalMapBloom, Vector2.Zero, Color.White);


                    spriteBatch.Draw(RenderTargetOverlay, Vector2.Zero, Color.White);

                    spriteBatch.End();

                    Rendertimer -= 1f / BlurResolution;
                }

                while (Rendertimer > 1f / BlurResolution)
                    Rendertimer -= 1f / BlurResolution;

                graphicsDevice.SetRenderTarget(RenderBuffer[0]);
                graphicsDevice.Clear(Color.Transparent);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                for (int i = 1; i < RenderBuffer.Count - 2; i++)
                {
                    //spriteBatch.Draw(RenderBuffer[i], new Rectangle(384 * i, 0, 384, 216), Color.White * ((i + 1f) / (RenderBuffer.Count + 1f)));

                    float Alpha = UsefulMethods.FindBetween(i + 1, RenderBuffer.Count, 0, 1f, 0f, true, 0.4f);

                    //spriteBatch.Draw(RenderBuffer[i], Vector2.Zero, null, Color.White * Alpha, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(RenderBuffer[i], Vector2.Zero, Color.White * Alpha);
                }

                spriteBatch.End();

                Rendertimer += GlobalVariables.WorldTime;
            }
        }

        public void DrawFinalRender(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Transparent);

            PreDrawBloom(spriteBatch, graphicsDevice);
            PreDrawMotionBlur(spriteBatch, graphicsDevice);

            //FinalDraw
            graphicsDevice.SetRenderTarget(FinalRender);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            //Draw Regular.  
            if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
            else
                spriteBatch.Draw(NormalMap, Vector2.Zero, Color.White);

            //Draw Bloom.
            if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, Color.White);
            else
                spriteBatch.Draw(NormalMapBloom, Vector2.Zero, Color.White);

            //Draw UI.
            if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                spriteBatch.Draw(RenderTargetUI, Vector2.Zero, Color.White);
            else
                spriteBatch.Draw(NormalMapUI, Vector2.Zero, Color.White);
            
            spriteBatch.End();


            if (!InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.LeftControl))
                if (RenderBuffer.Count > 1)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);

                    spriteBatch.Draw(RenderBuffer[0], Vector2.Zero, Color.Gray * 1f);

                    spriteBatch.End();
                }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            //Draw Overlay.
            spriteBatch.Draw(RenderTargetOverlay, Vector2.Zero, Color.White);

            spriteBatch.End();
        }
    }
}
