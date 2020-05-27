using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRebuild
{
    static class LifeManager
    {
        static List<Life> Entities;

        static public void Initialize()
        {
            Entities = new List<Life>();
            Entities.Add(new Life());
        }

        static public void Draw(SpriteBatch spriteBatch, byte cam)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Draw(spriteBatch, cam);
            }
        }
    }
}
