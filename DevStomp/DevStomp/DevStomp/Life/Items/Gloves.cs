using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevStomp
{
    class Gloves
    {
        public List<List<ValueType>> Actions;

        public int ID;

        public Vector2 Position;
        public float Rotation;
        public float Scale;
        public Color Tint;

        public Gloves()
        {
            ID = GlobalVariables.RandomNumber.Next(0, SM.Gloves.Elements);
            Actions = ParticleListCreation.GenerateRandomList(1);
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rect = SM.Gloves.Rectangles[ID];
            SpriteEffects SE = SpriteEffects.None;

            if (Rotation < -Math.PI)
                SE = SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(SM.Gloves.S(ID), Position, rect, Tint, Rotation, new Vector2(rect.Width / 2, rect.Height - (rect.Height / 2)), Scale, SE, 1f);
        }
    }
}
