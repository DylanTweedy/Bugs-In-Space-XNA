using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public class Camera
    {
        //[Serializable()]
        //public class SubCameras
        //{
        //    public List<Camera> Cams = new List<Camera>();
        //    public List<Rectangle> Viewports = new List<Rectangle>();

        //    Rectangle RectangleOut;
        //    Rectangle RectangleIn;
        //    int CameraPadding = 8;
        //    public Vector2 CameraRegion = Vector2.Zero;
        //    bool ResizeCamera = false;
        //    bool MoveCamera = false;
        //    int SelectedCamera;

        //    public void AddCamera(Vector2 position, float rotation, float zoom, Rectangle viewport)
        //    {
        //        Cams.Add(new Camera(position, rotation, zoom, InputType.None));
        //        Viewports.Add(viewport);
        //        SelectedCamera = -1;

        //        //Cams[Cams.Count - 1].viewport = viewport;                                
        //    }

        //    public void Update(Rectangle viewport)
        //    {
        //        if (InputManager.M.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
        //        {
        //            ResizeCamera = false;
        //            MoveCamera = false;
        //            SelectedCamera = -1;
        //            CameraRegion = Vector2.Zero;
        //        }

        //        for (int i = 0; i < Cams.Count; i++)
        //        {
        //            //RectangleOut = Cams[i].viewport;

        //            if (!ResizeCamera && InputManager.MouseRectangle.Intersects(RectangleOut))
        //                SelectedCamera = i;
        //        }
                
        //        if (SelectedCamera != -1)
        //            CameraSize(SelectedCamera);
        //        //CameraSize(SelectedCamera, viewport);

        //        for (int i = 0; i < Cams.Count; i++)
        //        {
        //            //Cams[i].viewport = new Rectangle(Viewports[i].X + viewport.X, Viewports[i].Y + viewport.Y, Viewports[i].Width, Viewports[i].Height);
                    
        //            //if (Cams[i].viewport.X < viewport.X)
        //            //    Cams[i].viewport.X = viewport.X;

        //            //if (Cams[i].viewport.X + Cams[i].viewport.Width > viewport.X + viewport.Width)
        //            //{
        //            //    Cams[i].viewport.X -= (Cams[i].viewport.X + Cams[i].viewport.Width) - (viewport.X + viewport.Width);

        //            //    if (Cams[i].viewport.X + Cams[i].viewport.Width > viewport.X + viewport.Width)
        //            //        Cams[i].viewport.Width = (viewport.X + viewport.Width) - Cams[i].viewport.X;
        //            //}

        //            //if (Cams[i].viewport.Y < viewport.Y)
        //            //    Cams[i].viewport.Y = viewport.Y;

        //            //if (Cams[i].viewport.Y + Cams[i].viewport.Height > viewport.Y + viewport.Height)
        //            //{
        //            //    Cams[i].viewport.Y -= (Cams[i].viewport.Y + Cams[i].viewport.Height) - (viewport.Y + viewport.Height);

        //            //    if (Cams[i].viewport.Y + Cams[i].viewport.Height > viewport.Y + viewport.Height)
        //            //        Cams[i].viewport.Height = (viewport.Y + viewport.Height) - Cams[i].viewport.Y;
        //            //}

        //            Cams[i].Update();
        //        }
        //    }

        //    //private void CameraSize(int i, Rectangle viewport)
        //    private void CameraSize(int i)
        //    {
        //        //RectangleOut = Cams[i].viewport;
        //        //RectangleIn = Cams[i].viewport;

        //        RectangleIn.X += CameraPadding;
        //        RectangleIn.Y += CameraPadding;
        //        RectangleIn.Width -= CameraPadding * 2;
        //        RectangleIn.Height -= CameraPadding * 2;

        //        if (!ResizeCamera && !InputManager.MouseRectangle.Intersects(RectangleIn))
        //        {
        //            if (InputManager.MousePosition.X <= RectangleOut.X + CameraPadding)
        //                CameraRegion.X = -1;
        //            else if (InputManager.MousePosition.X >= (RectangleOut.X + RectangleOut.Width) - CameraPadding)
        //                CameraRegion.X = 1;

        //            if (InputManager.MousePosition.Y <= RectangleOut.Y + CameraPadding)
        //                CameraRegion.Y = -1;
        //            else if (InputManager.MousePosition.Y >= (RectangleOut.Y + RectangleOut.Height) - CameraPadding)
        //                CameraRegion.Y = 1;
        //        }

        //        if (InputManager.MButtonPressed(true, 0))
        //        {
        //            if (CameraRegion != Vector2.Zero)
        //                ResizeCamera = true;
        //            else
        //                MoveCamera = true;
        //        }

        //        Rectangle vp = Viewports[i];

        //        if (ResizeCamera)
        //        {
        //            if (CameraRegion.X == -1)
        //            {
        //                vp.Width -= (int)InputManager.MouseVelocity.X;
        //                vp.X += (int)InputManager.MouseVelocity.X;
        //            }
        //            else if (CameraRegion.X == 1)
        //                vp.Width += (int)InputManager.MouseVelocity.X;


        //            if (CameraRegion.Y == -1)
        //            {
        //                vp.Height -= (int)InputManager.MouseVelocity.Y;
        //                vp.Y += (int)InputManager.MouseVelocity.Y;
        //            }
        //            else if (CameraRegion.Y == 1)
        //                vp.Height += (int)InputManager.MouseVelocity.Y;


        //            //if (vp.X < viewport.X)
        //            //{
        //            //    vp.Width -= viewport.X - vp.X;
        //            //    vp.X = viewport.X;
        //            //}

        //            //if (vp.X + vp.Width > viewport.X + viewport.Width)
        //            //    vp.Width = (viewport.X + viewport.Width) - vp.X;

        //            //if (vp.Y < viewport.Y)
        //            //{
        //            //    vp.Height -= viewport.Y - vp.Y;
        //            //    vp.Y = viewport.Y;
        //            //}

        //            //if (vp.Y + vp.Height > viewport.Y + viewport.Height)
        //            //    vp.Height = (viewport.Y + viewport.Height) - vp.Y;
        //        }

        //        if (MoveCamera)
        //        {
        //            vp.X += (int)InputManager.MouseVelocity.X;
        //            vp.Y += (int)InputManager.MouseVelocity.Y;
                    
        //            //if (vp.X < viewport.X)                    
        //            //    vp.X = viewport.X;
                          
        //            //if (vp.X + vp.Width > viewport.X + viewport.Width)
        //            //    vp.X = (viewport.X + viewport.Width) - vp.Width;

        //            //if (vp.Y < viewport.Y)
        //            //    vp.Y = viewport.Y;                    

        //            //if (vp.Y + vp.Height > viewport.Y + viewport.Height)
        //            //    vp.Y = (viewport.Y + viewport.Height) - vp.Height;
        //        }

        //        Viewports[i] = vp;                
        //    }
        //}



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
        public RenderTarget2D NormalMapUI;
       
        

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

        public void UpdateRenderTarget(Rectangle rect, GraphicsDevice graphicsDevice)
        {
            if (RenderTarget == null)
            {
                RenderTarget = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMap = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);


                RenderTargetUI = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMapUI = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
            }
            if (rect.Width != RenderTarget.Width || rect.Height != RenderTarget.Height)
            {
                RenderTarget = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMap = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                RenderTargetUI = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                NormalMapUI = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);

                //RenderTarget = new RenderTarget2D(graphicsDevice, rect.Width, rect.Height);
            }
        }

        //public List<string> Write()
        //{
        //    List<string> Info = new List<string>();

        //    Info.Add("");
        //    Info.Add("Position X: " + Position.X);
        //    Info.Add("Position Y: " + Position.Y);
        //    Info.Add("Rotation: " + R);
        //    Info.Add("Zoom: " + Z);
        //    Info.Add("");
        //    Info.Add("Position Destination X: " + PositionDestination.X);
        //    Info.Add("Position Destination Y:" + PositionDestination.Y);
        //    Info.Add("Rotation Destination: " + RD);
        //    Info.Add("Zoom Destination: " + ZD);
        //    Info.Add("");
        //    Info.Add("Position Speed: " + PT);
        //    Info.Add("Rotation Speed: " + RT);
        //    Info.Add("Zoom Speed: " + ZT);

        //    return Info;
        //}
    }
}
