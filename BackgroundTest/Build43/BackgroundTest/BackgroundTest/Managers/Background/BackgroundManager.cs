using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class BackgroundManager
    {
        SystemManager systemManager;
        List<Parallax> parallax;
        int PreviousCameraCount;

        public void Initialize()
        {
            systemManager = new SystemManager();
            systemManager.Initialize();
            parallax = new List<Parallax>();

        }

        public void LoadContent(ContentManager Content)
        {
            for (int i = 0; i < 10; i++)
            {
                systemManager.AddSystem();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!WorldVariables.WorldLoaded)
            {
                for (int i = 0; i < 10; i++)
                {
                    systemManager.AddSystem();
                }
            }

            systemManager.Update(gameTime);
        }

        public void UpdateParallax(GameTime gameTime, int Camera, ContentManager Content)
        {
            if (PreviousCameraCount != CameraManager.CameraCount)
            {
                parallax.Clear();

                for (int i = 0; i < CameraManager.CameraCount; i++)
                {
                    parallax.Add(new Parallax());
                    parallax[i].LoadContent(Content);
                }
            }

            parallax[Camera].Update(gameTime, Camera);

            PreviousCameraCount = CameraManager.CameraCount;
        }

        public void UpdateGalaxyOrbit(float SpeedMultiplier, float RadiusMultiplier)
        {
            systemManager.UpdateSystemOrbit(SpeedMultiplier, RadiusMultiplier);
        }

        public void ResetGalaxyOrbits()
        {
            systemManager.ResetOrbits();
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber)
        {
            systemManager.Draw(spriteBatch, cameraNumber);

        }

        public void DrawParallax(SpriteBatch spriteBatch, int cameraNumber)
        {
            parallax[cameraNumber].Draw(spriteBatch, cameraNumber);
        }
    }
}
