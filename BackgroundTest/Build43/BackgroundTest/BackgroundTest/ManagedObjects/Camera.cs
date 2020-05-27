using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    public class Camera
    {
        private Vector2 _focus;
        private float _rotation;
        private float _zoom;
        TimeSpan Timer;
        TimeSpan PreviousTimer;
        int Intensity;
        bool CameraShake;
        bool StartTimer;
        Random Rand;
        Vector2 Origin;
        Vector2 ScreenCenter;
        Vector2 OriginalFocus;
        float worldTime;
        float time;
        int RandomNumberX;
        int RandomNumberY;

        public Vector2 Focus
        {
            get { return _focus; }
            set { _focus = value; }
        }

        public float Rotation 
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public float Zoom 
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public float WorldTime
        {
            get { return worldTime; }
            set { worldTime = value; }
        }

        public Vector2 PreviousFocus { get; set; }
        
        public float ControlDuration { get; set; }
        public Vector2 FocusPosition { get; set; }
        public float ZoomPosition { get; set; }
        public float RotationPosition { get; set; }

        public float FocusSpeed { get; set; }
        public float RotationSpeed { get; set; }
        public float ZoomSpeed { get; set; }


        public Matrix Transform { get; set; }
        public Matrix TransformRotateOnly { get; set; }

        public void Initialize(Vector2 Center)
        {
            ScreenCenter = Center;
            FocusPosition = Center;
            _focus = Center;
            Zoom = 1f;
            ZoomPosition = 1f;

            FocusSpeed = 1.25f;
            RotationSpeed = 1.25f;
            ZoomSpeed = 1.25f;

            CameraShake = false;
            worldTime = 1f;

            Rand = new Random();

            time = 0f;
            RandomNumberX = 0;
            RandomNumberY = 0;
        }

        public void Update(GameTime gameTime)
        {
            PreviousFocus = _focus;

            Origin = ScreenCenter / Zoom;

            if (StartTimer)
            {
                PreviousTimer = gameTime.TotalGameTime;
                Timer = TimeSpan.FromSeconds(ControlDuration);
                StartTimer = false;
            }

            if (CameraShake)
            {
                RandomNumberX = Rand.Next(-Intensity, Intensity);
                RandomNumberY = Rand.Next(-Intensity, Intensity);
                OriginalFocus = Focus;
                _focus.X = Focus.X + RandomNumberX;
                _focus.Y = Focus.Y + RandomNumberY;
            }

            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Focus.X, -Focus.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(Zoom);

            TransformRotateOnly = Matrix.Identity *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateTranslation(ScreenCenter.X, ScreenCenter.Y, 0);

            if (CameraShake)
            {
                Focus = OriginalFocus;
                
                if (gameTime.TotalGameTime - PreviousTimer > Timer && CameraShake)
                {
                    CameraShake = false;
                    Intensity = 0;
                }
            }

            time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _focus.X += (FocusPosition.X - Focus.X) * FocusSpeed * time;
            _focus.Y += (FocusPosition.Y - Focus.Y) * FocusSpeed * time;

            _rotation += (RotationPosition - Rotation) * RotationSpeed * time;
            _zoom += (ZoomPosition - Zoom) * ZoomSpeed * time;
        }

        public void NewLocation(Vector2 focus, float rotation, float zoom)
        {
            Focus = focus;
            Zoom = zoom;
            Rotation = rotation;

            FocusPosition = focus;
            ZoomPosition = zoom;
            RotationPosition = rotation;
        }

        public void NewPosition(Vector2 focus, float rotation, float zoom)
        {
            FocusPosition = focus;
            ZoomPosition = zoom;
            RotationPosition = rotation;
        }

        public void Shake(float controlDuration, int intensity)
        {
            CameraShake = true;
            StartTimer = true;
            Intensity = intensity;
            ControlDuration = controlDuration;



            Timer = TimeSpan.FromSeconds(controlDuration);            
        }

        public bool IsInView(Vector2 position, int textureWidth, int textureHeight, float scale)
        {
            float Distance = Vector2.Distance(position, _focus) * _zoom;
            float TextureRadius = Vector2.Distance(position, position + new Vector2((textureWidth * scale) / 2, (textureHeight * scale) / 2)) * _zoom;

            if (Distance > WorldVariables.WindowRadius + TextureRadius || Distance < -WorldVariables.WindowRadius - TextureRadius)
                return false;

            return true;
        }

        public bool IsInView(Vector2 position, int textureWidth, int textureHeight)
        {
            float Distance = Vector2.Distance(position, _focus) * _zoom;
            float TextureRadius = Vector2.Distance(position, position + new Vector2((textureWidth) / 2, (textureHeight) / 2)) * _zoom;

            if (Distance > (WorldVariables.WindowRadius + TextureRadius) / 2|| Distance < (-WorldVariables.WindowRadius - TextureRadius) / 2)
                return false;

            return true;
        }
    }
}
