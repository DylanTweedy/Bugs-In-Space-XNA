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
        Final
    }

    [Serializable()]
    public class Camera
    {
        public Matrix Transform;

        public float MoveTime = 10f;

        public Vector2 Position;
        public Vector2 PreviousPosition;
        public Vector2 PositionDestination;

        public float Rotation;
        float RotationDestination;

        public float Zoom;
        public float ZoomDestination;

        public Vector2 Origin;


        public long PositionX;
        public long PositionY;
                
        public RenderTarget2D RenderTarget;
        public RenderTarget2D NormalMap;

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

        public string CameraName;

        //GraphicsDeviceManager graphicsDeviceManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        public Camera(string cameraName)
        {
            spriteBatch = GlobalVariables.spriteBatch;
            graphicsDevice = GlobalVariables.graphicsDevice;
            Position = Vector2.Zero;
            PositionDestination = Vector2.Zero;
            Rotation = 0f;
            RotationDestination = 0f;
            Zoom = 1f;
            ZoomDestination = 1f;
            PreviousPosition = Position;
            CameraName = cameraName;
        }

        public Camera(Vector2 position, float rotation, float zoom, string cameraName)
        {
            spriteBatch = GlobalVariables.spriteBatch;
            graphicsDevice = GlobalVariables.graphicsDevice;
            Position = position;
            PositionDestination = position;
            Rotation = rotation;
            RotationDestination = rotation;
            Zoom = zoom;
            ZoomDestination = zoom;
            PreviousPosition = Position;
            CameraName = cameraName;
        }


        public void Update()
        {
            #region Temporary Input

            if (InputManager.MButtonPressed(false, 0))
            {
                MoveTime = 10f;
                PositionDestination -= InputManager.MouseVelocity / Zoom;
            }

            if (InputManager.MScrollWheel != 0)
            {
                ZoomDestination += InputManager.MScrollWheel * 0.0005f * Zoom;
            }

            #endregion

            RotationDestination += 0.2f * GlobalVariables.WorldTime;
            RotationDestination = 0f;
            
            if (Zoom < 0.00001f)
            {
                Zoom = 0.00001f;
                ZoomDestination = 0.00001f;
            }

            UpdatePositionRange();

            float time = GlobalVariables.FrameTime * MoveTime;
            if (time > 1)
                time = 1f;

            if (Position != PositionDestination)
                Position += ((PositionDestination - Position) * time);
            if (Rotation != RotationDestination)
                Rotation += ((RotationDestination - Rotation) * time);
            if (Zoom != ZoomDestination)
                Zoom += ((ZoomDestination - Zoom) * time);
            
            if (RenderTarget != null)
                Origin = new Vector2(RenderTarget.Width / 2f, RenderTarget.Height / 2f) / Zoom;
            

            UpdateMatrix();
            
            PreviousPosition = Position;
        }
        
        private void UpdatePositionRange()
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

        private void UpdateMatrix()
        {
            Transform =
            Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
            Matrix.CreateScale(Zoom);
        }
        
        public bool IsOnScreen(float ObjectRadius, Vector2 ObjectPosition)
        {
            bool OnScreen = true;

            return OnScreen;
        }

        #region Render Code

        public void ClearRenderTargets()
        {
            graphicsDevice.SetRenderTarget(RenderTarget);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(RenderTargetBloom);
            graphicsDevice.Clear(Color.Transparent);

            graphicsDevice.SetRenderTarget(NormalMap);
            graphicsDevice.Clear(Color.Transparent);
            graphicsDevice.SetRenderTarget(NormalMapBloom);
            graphicsDevice.Clear(Color.Transparent);

            graphicsDevice.SetRenderTarget(FinalRender);
            graphicsDevice.Clear(Color.Transparent);
        }

        public void SetRenderTarget(RenderMode renderMode)
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
                    
                case RenderMode.Final:
                    graphicsDevice.SetRenderTarget(FinalRender);
                    break;

            }
        }

        public void UpdateRenderTarget(int renderTargetWidth, int renderTargetHeight)
        {
            int BufferSize = (int)((BlurResolution / BlurAmount) / 4);

            if (RenderTarget == null || (renderTargetWidth != RenderTarget.Width || renderTargetHeight != RenderTarget.Height) || BufferSize != RenderBuffer.Count)
            {
                RenderTarget = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMap = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                RenderTargetBloom = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMapBloom = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                FinalRender = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
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
                        RenderBuffer.Add(new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                            graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents));
                    }
                }
                else
                    for (int i = 0; i < RenderBuffer.Count; i++)
                    {
                        RenderBuffer[i] = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                            graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                    }
            }
        }

        public void UpdateRenderTargets(int Width, int Height)
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

        public void ApplyNormals(List<Light> Lights)
        {
            UpdateRenderTargets(RenderTarget.Width, RenderTarget.Height);
                     
            for (int i = 0; i < Lights.Count; i++)
            {
                Lights[i].Position = Vector3.Transform(Lights[i].Position, Transform);
                Lights[i].LightDecay = (int)(Lights[i].LightDecay * Zoom);
            }

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(RenderTargetBloom, ColorMapTemp, NormalMapTemp, ShadowMapTemp);
                else
                    LightManager.StartNormalMap();


                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(NormalMapBloom, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            LightManager.DrawFinal(Lights, this);
            
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    LightManager.StartColorMap(RenderTarget, ColorMapTemp, NormalMapTemp, ShadowMapTemp);
                else
                    LightManager.StartNormalMap();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                if (i == 0)
                    spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(NormalMap, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            LightManager.DrawFinal(Lights, this);
        }
        
        private void UpdateBloomTargets(int width, int height)
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

        private void PreDrawBloom()
        {
            if (BloomSceneTarget == null || (BloomSceneTarget.Width != RenderTarget.Width || BloomSceneTarget.Height != RenderTarget.Height))
                UpdateBloomTargets(RenderTarget.Width, RenderTarget.Height);

            BloomEffect.BeginDraw(graphicsDevice, BloomSceneTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            if (!InputManager.KBButtonPressed(false, DebugOptions.ShowNormals))
                spriteBatch.Draw(RenderTargetBloom, Vector2.Zero, Color.White);
            else
                spriteBatch.Draw(NormalMapBloom, Vector2.Zero, Color.White);

            spriteBatch.End();
            BloomEffect.EndDraw(spriteBatch, graphicsDevice, RenderTargetBloom, BloomTarget1, BloomTarget2);

        }

        private void PreDrawMotionBlur()
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

        public void DrawFinalRender()
        {
            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Transparent);

            PreDrawBloom();
            PreDrawMotionBlur();

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
            
            spriteBatch.End();


            if (!InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.LeftControl))
                if (RenderBuffer.Count > 1)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);

                    spriteBatch.Draw(RenderBuffer[0], Vector2.Zero, Color.Gray * 1f);

                    spriteBatch.End();
                }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            
            spriteBatch.End();
        }

        #endregion
    }
}
