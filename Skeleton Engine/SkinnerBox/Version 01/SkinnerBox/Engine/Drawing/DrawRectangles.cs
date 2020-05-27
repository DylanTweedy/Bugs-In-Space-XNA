using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DevStomp
{
    static class DrawRectangles
    {
        static public List<DrawRectangle2> Rectangles;
        static public List<Color> Colors;
        static public GraphicsDevice Graphics;


        static public void Initialize(GraphicsDevice graphics)
        {
            Graphics = graphics;
            Rectangles = new List<DrawRectangle2>();
            Colors = new List<Color>();
        }

        static public void AddRectangle(Rectangle rect, Color color)
        {
            Rectangles.Add(new DrawRectangle2());
            Rectangles[Rectangles.Count - 1].Initialize(Graphics, rect);
            Colors.Add(color);
        }

        static public void Draw(GraphicsDevice graphics, int camera)
        {
            for (int i = 0; i < Rectangles.Count; i++)
            {
                Rectangles[i].Draw(graphics, Colors[i], camera);
            }
        }
    }
}
