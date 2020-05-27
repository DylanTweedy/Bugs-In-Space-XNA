using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    static class StaticTests
    {
        static public Texture2D Marker;
        static public Texture2D Marker2;
        static public Texture2D Marker3;
        static public Texture2D Marker4;
        static public Texture2D Explosion;
        static public Texture2D StarTest;
        static public Texture2D Galaxy;
        static public Texture2D TestCharacter;

        static public void LoadTextures(ContentManager content)
        {
            Galaxy = content.Load<Texture2D>("Images//Galaxy//Galaxy//000");
            Marker = content.Load<Texture2D>("Images//Galaxy//Marker");
            Marker2 = content.Load<Texture2D>("Images//Galaxy//ChunkMarker");
            Marker3 = content.Load<Texture2D>("Images//Galaxy//ChunkMarker2");
            //Marker4 = content.Load<Texture2D>("Images//Galaxy//Marker2");
            Explosion = content.Load<Texture2D>("Images//Galaxy//Explosion");
            StarTest = content.Load<Texture2D>("Images//Galaxy//StarTest");
            TestCharacter = content.Load<Texture2D>("Images//TestCharacter");
        }
    }
}
