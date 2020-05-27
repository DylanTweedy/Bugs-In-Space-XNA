using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    static class InfoBox
    {
        static SpriteFont Font;
        static List<string> Text;

        static public void LoadContent(ContentManager Content)
        {
            Text = new List<string>();
            Font = Content.Load<SpriteFont>("Fonts//TestFont");            
        }

        static public void ClearList()
        {
            Text.Clear();
        }

        static public void AddItem(string Info)
        {
            Text.Add(Info);
        }

        static public void Draw(SpriteBatch spriteBatch, Vector2 Position, float Scale, float Alpha)
        {
            Vector2 Offset = new Vector2(0, 24);
            
            for (int i = 0; i < Text.Count; i++)
            {
                spriteBatch.DrawString(Font, Text[i], Position + ((Offset * i) * Scale), Color.White * Alpha, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
