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
        public class SpeedControl
        {
            public float Timer;
            public float Duration;
            public float Speed;
            public float StartPoint;
            public float EndPoint;


            public SpeedControl(float duration, float speed, float startPoint, float endPoint)
            {
                Timer = 0f;
                Duration = duration;
                Speed = speed;
                StartPoint = startPoint;
                EndPoint = endPoint;
            }

            public float Update()
            {
                float StartMod;
                float EndMod;
                float FinalMod;

                if (Speed > 1f)
                {
                    StartMod = UsefulMethods.FindBetween(Timer, Duration * StartPoint, 0f, Speed, 1f, false);
                    EndMod = UsefulMethods.FindBetween(Timer, Duration, Duration * EndPoint, Speed, 1f, true);

                    FinalMod = StartMod;
                    if (StartMod > EndMod)
                        FinalMod = EndMod;
                }
                else
                {
                    StartMod = UsefulMethods.FindBetween(Timer, Duration * StartPoint, 0f, 1f, Speed, true);
                    EndMod = UsefulMethods.FindBetween(Timer, Duration, Duration * EndPoint, 1f, Speed, false);

                    FinalMod = StartMod;
                    if (StartMod < EndMod)
                        FinalMod = EndMod;
                }

                Timer += GlobalVariables.FrameTime;
                return FinalMod;
            }
        }

        static public GraphicsDevice graphicsDevice;
        static public GraphicsDeviceManager graphicsDeviceManager;
        static public SpriteBatch spriteBatch;
        static public GameWindow gameWindow;
        static public ContentManager contentManager;
        static public GameTime gameTime;

        static TimeSpan previousGameTimePassed;
        static TimeSpan GameTimePassed;
        static public BasicEffect basicEffect;

        //Framerate and gametime.
        static public float FrameTime;
        static public float WorldTime;
        static public TimeSpan elapsedTime = TimeSpan.Zero;
        static public int frameRate = 0;
        static public int frameCounter;
        static public float TimeMultiplier = 1f;

        //Speed Control
        static List<SpeedControl> SpeedModifers = new List<SpeedControl>();
        
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

        static public void Initialize(GameWindow gWindow, GraphicsDeviceManager gDeviceManager, GraphicsDevice gDevice, ContentManager cManager, SpriteBatch sBatch)
        {
            graphicsDevice = gDevice;
            graphicsDeviceManager = gDeviceManager;
            spriteBatch = sBatch;
            gameWindow = gWindow;
            contentManager = cManager;

            basicEffect = new BasicEffect(graphicsDevice);            
            RandomNumber = new Random();

            GameTimePassed = TimeSpan.Zero;
            previousGameTimePassed = GameTimePassed;

            ChunkSize = 32;
            TileSize = 16;
        }

        static public void ActivateSpeedChange(float duration, float speed, float startPoint, float endPoint)
        {
            SpeedModifers.Add(new SpeedControl(duration, speed, startPoint, endPoint));
        }

        static public void Update(GameTime GTime)
        {
            gameTime = GTime;

            MassConstant = 262144;
            //GravitationalConstant = 1f;


            previousGameTimePassed = GameTimePassed;

            //GameTime += TimeSpan.FromSeconds((float)gameTime.ElapsedGameTime.TotalSeconds * TimeMultiplier);
            FrameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            TimeMultiplier = 1f;

            for (int i = 0; i < SpeedModifers.Count; i++)
            {
                TimeMultiplier *= SpeedModifers[i].Update();
                
                if (SpeedModifers[i].Timer > SpeedModifers[i].Duration)
                {
                    SpeedModifers.RemoveAt(i);

                    if (i > 0)
                        i--;
                }
            }

            //DebugOptions.DebugDisplay.Add("" + TimeMultiplier);

            if (FrameTime > 1f / 24f)
                FrameTime = 1f / 24f; 

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
