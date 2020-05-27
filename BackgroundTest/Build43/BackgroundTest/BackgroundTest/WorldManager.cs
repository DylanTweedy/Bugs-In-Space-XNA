using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    static class WorldManager
    {
        public static List<World> Worlds;


        static public void Initialize()
        {
            Worlds = new List<World>();

            for (int i = 0; i < 1; i++)
            {
                Worlds.Add(new World());
                Worlds[i].Initialize();
            }
        }

        static public void LoadContent(GraphicsDeviceManager Graphics, ContentManager Content, GraphicsDevice graphicsDevice)
        {
            for (int i = 0; i < Worlds.Count; i++)
            {
                Worlds[i].LoadContent(Graphics, Content, graphicsDevice);                
            }
        }

        static public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Worlds.Count; i++)
            {
                Worlds[i].Update(gameTime);
            }
        }

        static public void PreDraw(SpriteBatch spriteBatch, int CameraNumber, GraphicsDevice graphics)
        {
            for (int i = 0; i < Worlds.Count; i++)
            {
                Worlds[i].PreDraw(spriteBatch, graphics, CameraNumber);
            }
        }

        static public void Draw(SpriteBatch spriteBatch, int CameraNumber)
        {
            for (int i = 0; i < Worlds.Count; i++)
            {
                Worlds[i].Draw(spriteBatch, CameraNumber);
            }
        }
    }
}
