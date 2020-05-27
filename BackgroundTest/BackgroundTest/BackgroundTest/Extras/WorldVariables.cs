using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    static class WorldVariables
    {
        static TimeSpan previousGameTime;
        static TimeSpan previousRealTime;
        static public TimeSpan GameTime;

        static DateTime UniverseStart;

        static public TimeSpan EndTime;
        static public TimeSpan RealTime;

        static double frameTime;

        static public Random RandomNumber { get; set; }
        static public int WindowHeight { get; set; }
        static public int WindowWidth { get; set; }
        static public float WindowRadius { get; set; }
        static public float TimeScale { get; set; }
        static public bool WorldLoaded { get; set; }

        static public ColorPicker colorPicker;

        static public int ChunksVertical { get; set; }
        static public int ChunksHorizontal { get; set; }
        
        static public byte TileSize { get; set; }
        static public byte ChunkSize { get; set; }

        static public int ElementSeed;
        static public int GalaxyChunkSize;

        static public float TimeMultiplier;

        static public double FrameTime
        {
            get { return frameTime; }
        }



        static public void Initialize(int windowWidth, int windowHeight)
        {
            WorldLoaded = false;

            //GameTimePassed = TimeSpan.Zero;

            TimeScale = 1f;
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
            ElementSeed = 23;
            TimeMultiplier = 1f;

            RandomNumber = new Random();

            UniverseStart = DateTime.Parse("03-01-2013");
            EndTime = TimeSpan.FromDays(3652);

            RealTime = DateTime.Now.Subtract(UniverseStart);
            previousRealTime = RealTime;


            GameTime = RealTime;
            previousGameTime = RealTime;

            Vector2 radius = new Vector2(WindowWidth / 2, WindowHeight / 2);

            WindowRadius = Vector2.Distance(Vector2.Zero, radius);
            colorPicker = new ColorPicker(RandomNumber);

            ChunkSize = 64;
            TileSize = 16;
        }

        static public void SetCameraSize(int CameraNumber, int ChunkSize)
        {
            //Make work with multiple Cameras
            ChunksVertical = (int)(Math.Ceiling((WindowHeight / CameraManager.CamerasRead[CameraNumber].Zoom) / (float)ChunkSize) + 1);
            ChunksHorizontal = (int)(Math.Ceiling((WindowWidth / CameraManager.CamerasRead[CameraNumber].Zoom) / (float)ChunkSize) + 1);
        }

        static public void SetCameraSize(int ChunkSize)
        {
            //Make work with multiple Cameras
            ChunksVertical = (int)(Math.Ceiling((WindowHeight / 0.2) / (float)ChunkSize) + 1);
            ChunksHorizontal = (int)(Math.Ceiling((WindowWidth / 0.2) / (float)ChunkSize) + 1);
        }

        static public void Update(GameTime gameTime)
        {
            frameTime = gameTime.ElapsedGameTime.TotalSeconds;

            previousRealTime = RealTime;
            previousGameTime = GameTime;

            RealTime = DateTime.Now.Subtract(UniverseStart);
            GameTime += TimeSpan.FromSeconds((float)gameTime.ElapsedGameTime.TotalSeconds * TimeMultiplier);

            if (GameTime.TotalDays > EndTime.TotalDays * 1.2)
                GameTime -= TimeSpan.FromDays(EndTime.TotalDays * 1.2);
            else if (GameTime.TotalDays < 0)
                GameTime += TimeSpan.FromDays(EndTime.TotalDays * 1.2);

            //GameTimePassed = TimeSpan.FromSeconds((float)gameTime.ElapsedGameTime.TotalSeconds * TimeMultiplier);
            //GameTime += TimeSpan.FromMinutes(2763);

            //Console.WriteLine(GameTime);
        }
    }
}
