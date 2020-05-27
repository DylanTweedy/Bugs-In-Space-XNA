using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
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
        Vector2 PreviousFocus;

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

        public float ControlDuration { get; set; }
        public Vector2 FocusPosition { get; set; }
        public float ZoomPosition { get; set; }
        public float RotationPosition { get; set; }

        public float FocusSpeed { get; set; }
        public float RotationSpeed { get; set; }
        public float ZoomSpeed { get; set; }


        public Matrix Transform { get; set; }


        public void Initialize(Vector2 Center)
        {
            ScreenCenter = Center;
            FocusPosition = Center;
            Zoom = 1f;
            ZoomPosition = 1f;

            FocusSpeed = 1.25f;
            RotationSpeed = 1.25f;
            ZoomSpeed = 1.25f;

            CameraShake = false;

            Rand = new Random();
        }

        public void Update(GameTime gameTime)
        {
            Origin = ScreenCenter / Zoom;

            if (StartTimer)
            {
                PreviousTimer = gameTime.TotalGameTime;
                Timer = TimeSpan.FromSeconds(ControlDuration);
                StartTimer = false;
            }

            if (CameraShake)
            {
                int RandomNumberX = Rand.Next(-Intensity, Intensity);
                int RandomNumberY = Rand.Next(-Intensity, Intensity);
                PreviousFocus = Focus;
                Focus = new Vector2(Focus.X + RandomNumberX, Focus.Y + RandomNumberY);
            }

            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Focus.X, -Focus.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(Zoom, Zoom, Zoom));

            if (CameraShake)
            {
                Focus = PreviousFocus;
                
                if (gameTime.TotalGameTime - PreviousTimer > Timer && CameraShake)
                {
                    CameraShake = false;
                    Intensity = 0;
                }
            }

            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

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

        public bool IsInView(Vector2 position, Texture2D texture)
        {
            if ((position.X + texture.Width) < (Focus.X - Origin.X) || (position.X) > (Focus.X + Origin.X))
                return false;
            if ((position.Y + texture.Height) < (Focus.Y - Origin.Y) || (position.Y) > (Focus.Y + Origin.Y))
                return false;

            return true;
        }
    }
}
