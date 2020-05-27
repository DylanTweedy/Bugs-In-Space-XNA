using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class ColorSelector
    {
        public float H;
        public float S;
        public float L;
        
        public byte R;
        public byte G;
        public byte B;

        public Vector2 Position;

        public ColorSelector(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;

            ColorManager.HSB hsb = ColorManager.RGB2HSB(color);

            H = hsb.H;
            S = hsb.S;
            L = hsb.B;
        }





        public void Draw(SpriteBatch spriteBatch)
        {
            //Color test = ColorManager.HSB2RGB(new ColorManager.HSB(H, 1f, 1f));

            //spriteBatch.Draw(StaticInfoBox.ColorBlock, Position, test);
            //spriteBatch.Draw(StaticInfoBox.ColorWhite, Position, Color.White);
            //spriteBatch.Draw(StaticInfoBox.ColorBlack, Position, Color.White);

            //float scaleX = 0.2f;

            //spriteBatch.Draw(StaticInfoBox.ColorPallette, Position + new Vector2(600, 0), null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, 1f), SpriteEffects.None, 0f);
            
            //Vector2 scale = Vector2.One;
            
            //spriteBatch.Draw(StaticTests.MarkerCircle, Position + new Vector2(StaticInfoBox.ColorBlock.Width * S, StaticInfoBox.ColorBlock.Height * (1 - L)), null, Color.Gray, 0f, new Vector2(StaticTests.MarkerCircle.Width / 2), scale * 1.25f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(StaticTests.MarkerCircle, Position + new Vector2(StaticInfoBox.ColorBlock.Width * S, StaticInfoBox.ColorBlock.Height * (1 - L)), null, ColorManager.HSB2RGB(new ColorManager.HSB(H, S, L)), 0f, new Vector2(StaticTests.MarkerCircle.Width / 2), scale, SpriteEffects.None, 0f);


            //scale.X = 3f;


            //spriteBatch.Draw(StaticTests.Marker, Position + new Vector2(600, 0) + new Vector2((StaticInfoBox.ColorPallette.Width / 2) * scaleX, (1 - (H / 360f)) * StaticInfoBox.ColorPallette.Height), null, Color.Gray, 0f, new Vector2(StaticTests.Marker.Width / 2), scale * 1.25f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(StaticTests.Marker, Position + new Vector2(600, 0) + new Vector2((StaticInfoBox.ColorPallette.Width / 2) * scaleX, (1 - (H / 360f)) * StaticInfoBox.ColorPallette.Height), null, ColorManager.HSB2RGB(new ColorManager.HSB(H, 1f, 1f)), 0f, new Vector2(StaticTests.Marker.Width / 2), scale, SpriteEffects.None, 0f);


            //string RGB = "R:" + R + " G:" + G + " B:" + B;
            //string HSB = "H:" + Math.Round(H) +" S:" + (Math.Round(S, 2) * 100) + " B:" + (Math.Round(L, 2) * 100);
            //scale = new Vector2(3f, 3f);

            //spriteBatch.DrawString(StaticTests.font, HSB, Position + new Vector2(350, -75), Color.White, 0f, StaticTests.font.MeasureString(HSB) / 2, scale, SpriteEffects.None, 0f);
            //spriteBatch.DrawString(StaticTests.font, RGB, Position + new Vector2(350, -150), Color.White, 0f, StaticTests.font.MeasureString(RGB) / 2, scale, SpriteEffects.None, 0f);            
        }
    }
}
