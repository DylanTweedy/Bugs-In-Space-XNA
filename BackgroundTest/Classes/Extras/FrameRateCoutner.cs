using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    public class FrameRateCounter
    {
        static public SpriteFont spriteFont;

        static public int frameRate = 0;
        static public int frameCounter = 0;
        static public TimeSpan elapsedTime = TimeSpan.Zero;


        static public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("Fonts//TestFont");

        }


        //protected override void UnloadGraphicsContent(bool unloadAllContent)
        //{
        //    if (unloadAllContent)
        //        content.Unload();
        //}


        static public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }


        static public void Draw(SpriteBatch spriteBatch)
        {
            frameCounter++;

            string fps = string.Format("fps: {0}", frameRate);

            spriteBatch.DrawString(spriteFont, fps, new Vector2(33, 33), Color.Black);
            spriteBatch.DrawString(spriteFont, fps, new Vector2(32, 32), Color.White);
        }
    }
}
