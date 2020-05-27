using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class Line
    {
        static public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float Thickness)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            Vector2 normal = edge;
            normal.Normalize();
            normal *= Thickness / 2f;


            Texture2D Tex = StaticTests.Marker;

            spriteBatch.Draw(Tex,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X + (int)normal.Y,
                    (int)start.Y - (int)normal.X,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    (int)Thickness), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }


    }
}
