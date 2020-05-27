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
        TimeSpan Timer;
        TimeSpan PreviousTimer;
        int Intensity;
        bool CameraShake;
        bool StartTimer;
        Random Rand;
        Vector2 Origin;
        Vector2 ScreenCenter;
        Vector2 OriginalFocus;
        float time;
        int RandomNumberX;
        int RandomNumberY;


        public Vector2 Focus;
        public float Rotation;
        public float Zoom;
        public float WorldTime;
        
        public Vector3 Location;
        public Vector2 PreviousFocus;

        public float ControlDuration;

        public Vector2 FocusDestination;
        public float ZoomDestination;
        public float RotationDestination;

        public float FocusSpeed;
        public float RotationSpeed;
        public float ZoomSpeed;

        public Matrix Transform;
        public Matrix TransformRotateOnly;

        public void Initialize(Vector2 Center)
        {
            ScreenCenter = Center;
            FocusDestination = Center;
            Focus = Center;
            Zoom = 1f;
            ZoomDestination = 1f;

            FocusSpeed = 1.25f;
            RotationSpeed = 1.25f;
            ZoomSpeed = 1.25f;

            CameraShake = false;
            WorldTime = 1f;

            Rand = new Random();

            time = 0f;
            RandomNumberX = 0;
            RandomNumberY = 0;

            Location = new Vector3(2500, 2500, 1);
        }

        public void Update(GameTime gameTime)
        {
            PreviousFocus = Focus;

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
                Focus.X = Focus.X + RandomNumberX;
                Focus.Y = Focus.Y + RandomNumberY;
            }

            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Focus.X, -Focus.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(Zoom);

            TransformRotateOnly = Matrix.Identity * Matrix.CreateRotationZ(Rotation) * Matrix.CreateTranslation(ScreenCenter.X, ScreenCenter.Y, 0);

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

            Focus.X += (FocusDestination.X - Focus.X) * FocusSpeed * time;
            Focus.Y += (FocusDestination.Y - Focus.Y) * FocusSpeed * time;
            Rotation += (RotationDestination - Rotation) * RotationSpeed * time;
            Zoom += (ZoomDestination - Zoom) * ZoomSpeed * time;
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
            float Distance = Vector2.Distance(position, Focus) * Zoom;
            float TextureRadius = Vector2.Distance(position, position + new Vector2((textureWidth * scale) / 2, (textureHeight * scale) / 2)) * Zoom;

            if (Distance > WorldVariables.WindowRadius + TextureRadius || Distance < -WorldVariables.WindowRadius - TextureRadius)
                return false;

            return true;
        }
    }
}
