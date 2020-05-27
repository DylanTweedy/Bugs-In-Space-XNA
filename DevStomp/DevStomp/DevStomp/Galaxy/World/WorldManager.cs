using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace DevStomp
{
    static class WorldManager
    {
        static public StarSystem system;
        static GraphicsDevice graphicsDevice;

        static public void Initialize(GraphicsDevice graphics)
        {
            graphicsDevice = graphics;

            system = new StarSystem();
            system.Initialize(new Vector2(0, 0), graphicsDevice);
        }

        static public void Update(GraphicsDevice graphics)
        {
            system.Update(graphics);
        }

        static public void Draw(SpriteBatch spriteBatch, Camera cam)
        {
            system.Draw(spriteBatch, graphicsDevice, cam);
        }
    }
}
