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
        static double timePassed;
        static TimeSpan time;
        static double frameTime;

        public static Random RandomNumber { get; set; }
        public static int WindowHeight { get; set; }
        public static int WindowWidth { get; set; }
        public static float WindowRadius { get; set; }
        public static float TimeScale { get; set; }
        public static bool WorldLoaded { get; set; }

        public static ColorPicker colorPicker;

        public static int ChunksVertical { get; set; }
        public static int ChunksHorizontal { get; set; }

        public static byte TileSize { get; set; }
        public static byte ChunkSize { get; set; }

        static public double FrameTime
        {
            get { return frameTime; }
        }



        static public void Initialize(int windowWidth, int windowHeight)
        {
            WorldLoaded = false;

            timePassed = 0;
            TimeScale = 1f;
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;

            RandomNumber = new Random();

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

            timePassed += time.TotalMilliseconds;
        }
    }
}
