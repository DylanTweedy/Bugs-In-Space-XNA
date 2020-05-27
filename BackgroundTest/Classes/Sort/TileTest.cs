using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class TileTest
    {
        public Texture2D Texture;
        public int tileSize;
        public int X;
        public int Y;
        public Rectangle Bounds;

        public Vector2 position;

        public void Initialize()
        {
            tileSize = 64;
            position = new Vector2(-512, -200);
        }


        public void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Images\\test\\Untitled");
            X = Texture.Width / tileSize;
            Y = Texture.Height / tileSize;

            Bounds = new Rectangle((int)position.X, (int)position.Y, 1024, 1024);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    spriteBatch.Draw(Texture, new Vector2(position.X + (tileSize * x), position.Y + (tileSize * y)), new Rectangle(tileSize * x, tileSize * y, tileSize, tileSize), Color.White);
                }
            }
        }
    }
}
