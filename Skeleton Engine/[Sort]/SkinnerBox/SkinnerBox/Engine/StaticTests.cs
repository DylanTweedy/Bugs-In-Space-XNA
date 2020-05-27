using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class StaticTests
    {
        static public Texture2D TestCharacter;
        static public Texture2D Stars;
        static public Texture2D StarField;
        static public Texture2D Marker;
        static public Texture2D MarkerCheckered;
        static public Texture2D MarkerCircle;
        static public Texture2D MarkerTriangle;
        static public Texture2D StarTest;
        static public Texture2D DustTest;
        static public Texture2D TestCursor;
        static public SpriteFont font;
        static public Texture2D Stone;
        static public Texture2D Arrow;

        static public void LoadTextures(ContentManager content)
        {
            TestCharacter = content.Load<Texture2D>("Images//TestImages//TestCharacter");
            Stars = content.Load<Texture2D>("Images//TestImages//stars");
            StarField = content.Load<Texture2D>("Images//TestImages//StarField");
            Marker = content.Load<Texture2D>("Images//TestImages//Marker");
            MarkerCircle = content.Load<Texture2D>("Images//TestImages//MarkerCircle");
            MarkerTriangle = content.Load<Texture2D>("Images//TestImages//MarkerTriangle");
            StarTest = content.Load<Texture2D>("Images//TestImages//TestStar");
            DustTest = content.Load<Texture2D>("Images//TestImages//TestDust");
            TestCursor = content.Load<Texture2D>("Images//TestImages//Cursor");
            Stone = content.Load<Texture2D>("Images//TestImages//Rock");
            MarkerCheckered = content.Load<Texture2D>("Images//TestImages//MarkerCheckered");
            Arrow = content.Load<Texture2D>("Images//TestImages//Arrow");






            font = content.Load<SpriteFont>("Fonts//1");
        }
    }
}
