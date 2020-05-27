using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameRebuild
{
    static class SM
    {
        static public Sheet StarsFront;
        static public Sheet StarsBack;
        static public Sheet StarsBig;
        
        static public void Initialize(ContentManager C, GraphicsDevice G)
        {
            StarsBack = SpriteSheet.Initialize(C, G, "Background//Stars//Back//", "Stars Back");
            StarsFront = SpriteSheet.Initialize(C, G, "Background//Stars//Front//", "Stars Front");
            StarsBig = SpriteSheet.Initialize(C, G, "Background//Stars//Big//", "Stars Big");
        }
    }
}
