using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameRebuild
{
    static class StaticTests
    {
        static public Texture2D TestCharacter;
        static public Texture2D Stars;
        static public Texture2D StarField;
        static public Texture2D Marker;
        static public Texture2D StarTest;
        static public Texture2D DustTest;
        static public Texture2D TestCursor;

        static public void LoadTextures(ContentManager content)
        {
            TestCharacter = content.Load<Texture2D>("Images//TestImages//TestCharacter");
            Stars = content.Load<Texture2D>("Images//TestImages//stars");
            StarField = content.Load<Texture2D>("Images//TestImages//StarField");
            Marker = content.Load<Texture2D>("Images//TestImages//Marker");
            StarTest = content.Load<Texture2D>("Images//TestImages//TestStar");
            DustTest = content.Load<Texture2D>("Images//TestImages//TestDust");
            TestCursor = content.Load<Texture2D>("Images//TestImages//Cursor");
        }
    }
}
