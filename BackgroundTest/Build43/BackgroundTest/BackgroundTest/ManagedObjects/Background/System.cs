using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class System
    {
        List<Star> stars;
        Orbit orbit;
        Random rand;
        int systemRadius;
        int min;
        int galaxyScale;
        int maxSystemRadius;
        Vector2 systemCenter;
        int starsRadius;
        int starCount;
        bool InView;
        bool active;
        bool StarZoom;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Vector2 Position
        {
            get { return systemCenter; }
        }

        public int SystemRadius
        {
            get { return systemRadius; }
        }

        public void Initialize(Random Rand, int MaxSystemRadius)
        {
            rand = Rand;
            maxSystemRadius = MaxSystemRadius;
            InView = true;
            starsRadius = 0;
            stars = new List<Star>();
        }


        public void LoadContent(int centerRadius, int GalaxyScale)
        {
            min = centerRadius + 2500;

            galaxyScale = GalaxyScale;

            float rad = rand.Next(min, galaxyScale);
            float spe = ((float)rand.Next(1000, 10000) / ((float)rad / 2));
            float rot = (float)rand.Next(1, 62831) / 10000f;

            orbit = new Orbit(rad, spe, rot, rot);

            orbit.InitializeCircle();
            //orbit.InitializeEllipse((float)rand.Next(0, 500) / 100f, (float)rand.Next(0, 500) / 100f);

            orbit.UpdatePosition(Vector2.Zero);
            orbit.OriginalPosition = orbit.Position;
            orbit.OriginalRadian = orbit.OrbitRadian;

            systemCenter = orbit.Position;

            AddStars();

            float biggestStar = 0f;

            for (int i = 0; i < stars.Count; i++)
            {
                if (biggestStar < stars[i].Scale)
                    biggestStar = stars[i].Scale;
            }

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].SetupStar(orbit.Position, rot, starCount, spe * 250f, (6.2831f / (float)starCount) * i, biggestStar);

                int starRad = stars[i].CalculateRadius();

                if (starsRadius < starRad)
                    starsRadius = starRad;
            }
                       
            systemRadius = rand.Next(starsRadius, maxSystemRadius + starsRadius);
        }

        public void ResetPosition()
        {
            float rad = rand.Next(min, galaxyScale);
            float spe = ((float)rand.Next(1, 10000) / ((float)rad / 2));
            float rot = (float)rand.Next(1, 62831) / 10000f;

            orbit = new Orbit(rad, spe, rot, rot);

            orbit.InitializeCircle();

            orbit.UpdatePosition(Vector2.Zero);
            orbit.OriginalPosition = orbit.Position;
            orbit.OriginalRadian = orbit.OrbitRadian;

            systemCenter = orbit.Position;

            float biggestStar = 0f;

            for (int i = 0; i < stars.Count; i++)
            {
                if (biggestStar < stars[i].Scale)
                    biggestStar = stars[i].Scale;
            }
            
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].SetupStar(orbit.Position, rot, starCount, spe * 250f, (6.2831f / (float)starCount) * i, biggestStar);
                
                int starRad = stars[i].CalculateRadius();

                if (starsRadius < starRad)
                    starsRadius = starRad;
            }
        }
        
        void AddStars()
        {
            bool StarsReady = false;
            starCount = 1;

            while (!StarsReady)
            {
                if (rand.Next(0, 4) == 0)
                {
                    starCount += 1;
                }
                else
                    StarsReady = true;
            }

            for (int i = 0; i < starCount; i++)
            {
                stars.Add(new Star(rand));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (InView && StarZoom)
            {
                for (int i = 0; i < stars.Count; i++)
                {
                    stars[i].Update(gameTime, systemCenter);
                }
            }

            InView = false;
            StarZoom = false;
        }

        public void ResetOrbit()
        {
            orbit.OrbitRadian = orbit.OriginalRadian;

            orbit.UpdatePosition(Vector2.Zero);
            systemCenter = orbit.Position;

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].ResetOrbit(orbit.OriginalPosition);
            }
        }

        public void UpdateOrbit(float SpeedMultiplier, float RadiusMultiplier)
        {
            orbit.UpdateSpeed(SpeedMultiplier);
            orbit.UpdateRadius(RadiusMultiplier);

            orbit.UpdatePosition(Vector2.Zero);
            systemCenter = orbit.Position;

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].UpdateOrbit(orbit.Position);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber)
        {
            if (CameraManager.CamerasRead[cameraNumber].IsInView(systemCenter, systemRadius, systemRadius, 1f))
            {
                InView = true;

                if (CameraManager.CamerasRead[cameraNumber].Zoom >= 0.0007)
                {
                    StarZoom = true;
                }

                for (int i = 0; i < stars.Count; i++)
                {
                    stars[i].Draw(spriteBatch, cameraNumber);
                }
            }
            else
            {
            }
        }
    }
}
