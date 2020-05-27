using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static public class Graphics
    {
        static public bool WindowFocus = true;
        static public SpriteBatch spriteBatch;
        static public Vector2 ScreenScale = Vector2.Zero;
        static public Vector2 ViewportOffset = Vector2.Zero;
        static public Rectangle ClientBounds;


        /// <summary>
        /// Initializes the Graphics module.
        /// </summary>
        /// <param name="sb">The SpriteBatch.</param>
        static public void Initialize(SpriteBatch sb)
        {
            spriteBatch = sb;
        }
    }
}
