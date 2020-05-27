using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Life
    {
        Number Mass;
        SpacePosition SP;
        Vector2 Position;
        Vector2 Chunk;
        
        public Life()
        {
            Mass = new Number(0, 0, 0);
            SP = new SpacePosition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
            Position = Vector2.Zero;
            Chunk = Vector2.Zero;
        }




    }
}
