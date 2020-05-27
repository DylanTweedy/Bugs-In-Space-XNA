using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class DrawManager
    {
        static SpriteBatch spriteBatch;
        static GraphicsDevice graphicsDevice;

        static public void Initialize()
        {
            spriteBatch = GlobalVariables.spriteBatch;
            graphicsDevice = GlobalVariables.graphicsDevice;
        }

        /// <summary>
        /// Draws to all cameras
        /// </summary>
        static public void BeginDrawAll()
        {

        }


    }
}
