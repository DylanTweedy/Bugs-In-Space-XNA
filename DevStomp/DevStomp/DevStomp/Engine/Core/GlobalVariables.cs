using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class GlobalVariables
    {
        static TimeSpan previousGameTime;
        static TimeSpan GameTime;
        static public BasicEffect basicEffect;

        //Framerate and gametime.
        static public float FrameTime;
        static public float WorldTime;
        static public TimeSpan elapsedTime = TimeSpan.Zero;
        static public int frameRate = 0;
        static public int frameCounter;
        static public float TimeMultiplier = 1f;
        
        //Marching squares size.
        static public byte TileSize;
        static public byte ChunkSize;
        
        //Random generators.
        static public Random RandomNumber;
        static public int ElementSeed = 23;

        //Global numbers.
        static public float GravitationalConstant = 6.67384e-11f;
        static public long MassConstant = 262144;
        
        //Logs
        static public List<string> ErrorLog = new List<string>();

        static public void Initialize(GraphicsDevice graphics)
        {
            basicEffect = new BasicEffect(graphics);            
            RandomNumber = new Random();

            GameTime = TimeSpan.Zero;
            previousGameTime = GameTime;

            ChunkSize = 32;
            TileSize = 16;
        }
        
        static public void Update(GameTime gameTime)
        {
            MassConstant = 262144;
            //GravitationalConstant = 1f;


            previousGameTime = GameTime;

            //GameTime += TimeSpan.FromSeconds((float)gameTime.ElapsedGameTime.TotalSeconds * TimeMultiplier);
            FrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (FrameTime > 1f / 30f)
                FrameTime = 1f / 30f; 

            WorldTime = FrameTime * TimeMultiplier;


            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

            frameCounter++;
        }
    }
}
