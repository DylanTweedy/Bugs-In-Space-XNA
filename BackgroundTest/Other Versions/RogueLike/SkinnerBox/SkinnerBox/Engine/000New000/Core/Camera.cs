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
        Final
    }

    [Serializable()]
    public class Camera
    {
        public Matrix Transform;

        public float MoveTime = 10f;

        public SpacePosition Position;
        public SpacePosition PreviousPosition;
        public SpacePosition PositionDestination;

        public float Rotation;
        public float RotationDestination;

        public float Zoom;
        public float ZoomDestination;

        public Vector2 Origin;


        public long PositionX;
        public long PositionY;
                
        public RenderTarget2D RenderTarget;
        public RenderTarget2D FinalRender;
        
        public string CameraName;

        public Rectangle Bounds = Rectangle.Empty;

        //GraphicsDeviceManager graphicsDeviceManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        public Camera(string cameraName)
        {
            spriteBatch = GlobalVariables.spriteBatch;
            graphicsDevice = GlobalVariables.graphicsDevice;
            Position = new SpacePosition();
            PositionDestination = new SpacePosition();
            Rotation = 0f;
            RotationDestination = 0f;
            Zoom = 1f;
            ZoomDestination = 1f;
            PreviousPosition = new SpacePosition(Position);
            CameraName = cameraName;
        }

        public Camera(Vector2 position, float rotation, float zoom, string cameraName)
        {
            spriteBatch = GlobalVariables.spriteBatch;
            graphicsDevice = GlobalVariables.graphicsDevice;
            Position = new SpacePosition(position);
            PositionDestination = new SpacePosition(position);
            Rotation = rotation;
            RotationDestination = rotation;
            Zoom = zoom;
            ZoomDestination = zoom;
            PreviousPosition = new SpacePosition(Position);
            CameraName = cameraName;
        }


        public void Update()
        {
            #region Temporary Input

            if (InputManager.MBPressed(false, MouseButton.Right))
            {
                MoveTime = 10f;
                PositionDestination -= Vector2.Transform(InputManager.MouseVelocity / Zoom, Matrix.CreateRotationZ(-Rotation));
            }

            if (InputManager.MScrollWheel != 0)
            {
                ZoomDestination += InputManager.MScrollWheel * 0.0005f * Zoom;
            }

            if (InputManager.MBPressed(false, new List<MouseButton>() { MouseButton.X1, MouseButton.X2 }))
            {
                RotationDestination = 0f;
            }
            else
            {
                if (InputManager.MBPressed(false, MouseButton.X1))
                {
                    RotationDestination += 0.75f * GlobalVariables.WorldTime;
                }

                if (InputManager.MBPressed(false, MouseButton.X2))
                {
                    RotationDestination -= 0.75f * GlobalVariables.WorldTime;
                }
            }

            #endregion
                        
            if (Zoom < 0.000000001f)
            {
                Zoom = 0.000000001f;
                ZoomDestination = 0.000000001f;
            }
            
            float time = GlobalVariables.WorldTime * MoveTime;
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
        
        private void UpdateMatrix()
        {
            Transform =
            Matrix.CreateTranslation(-Position.Position.X, -Position.Position.Y, 0) *
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
            graphicsDevice.SetRenderTarget(FinalRender);
            graphicsDevice.Clear(Color.Transparent);
        }

        public void SetRenderTarget(RenderMode renderMode)
        {
            switch (renderMode)
            {
                case RenderMode.Regular:
                    graphicsDevice.SetRenderTarget(RenderTarget);
                    break;

                case RenderMode.Final:
                    graphicsDevice.SetRenderTarget(FinalRender);
                    break;
            }
        }

        public void UpdateRenderTarget(int renderTargetWidth, int renderTargetHeight)
        {
            if (RenderTarget == null || (renderTargetWidth != RenderTarget.Width || renderTargetHeight != RenderTarget.Height))
            {
                RenderTarget = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                FinalRender = new RenderTarget2D(graphicsDevice, renderTargetWidth, renderTargetHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                
            }
        }

        public void DrawFinalRender()
        {
            if (RenderTarget != null)
            {
                graphicsDevice.SetRenderTarget(null);
                graphicsDevice.Clear(Color.Transparent);

                //FinalDraw
                graphicsDevice.SetRenderTarget(FinalRender);
                graphicsDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);

                spriteBatch.End();
            }
        }

        #endregion


        public List<TextLine> Information()
        {
            List<TextLine> Info = new List<TextLine>();

            Info.Add(new TextLine("Position", "X: " + Position.Position.X + " Y: " + Position.Position.Y, true, ": "));
            Info.Add(new TextLine("Chunk Position", "X: " + Position.ChunkX + " Y: " + Position.ChunkY, true, ": "));
            Info.Add(new TextLine("Rotation", "" + Rotation + " Radians - " + (Rotation * 57.2958f) + " Degrees", true, ": "));
            Info.Add(new TextLine("Zoom", "" + Zoom, true, ": "));
            
            return Info;
        }
    }
}
