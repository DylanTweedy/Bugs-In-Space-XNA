using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRebuild
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

        public void Draw(SpriteBatch spriteBatch, byte cam)
        {
            //spriteBatch.Draw(StaticTests.StarField, Position - new Vector2(StaticTests.StarField.Width / 2, StaticTests.StarField.Height / 2), Color.White);
            spriteBatch.Draw(StaticTests.TestCharacter, Position - new Vector2(StaticTests.TestCharacter.Width / 2, StaticTests.TestCharacter.Height / 2), Color.Green);
        }
    }
}
