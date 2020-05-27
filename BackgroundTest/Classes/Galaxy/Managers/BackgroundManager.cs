using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    static class BackgroundManager
    {
        class Galaxy
        {
            public byte Tier;
            public int area;
            public Vector2 position;
            public float rotation;
            public int KnownStars;
        }

        static List<Galaxy> Galaxies;
        static public SystemManager[][] systemManager;
        static List<Parallax> parallax;
        static int PreviousCameraCount;
        static public int ChunkX;
        static public int ChunkY;
        static public int CenterX;
        static public int CenterY;
        static public int ChunkSize;
        static public int SystemCount;
        static Random rand;

        static Texture2D test;
        static Texture2D Grid1;
        static Texture2D Grid2;
        static Texture2D GridBG;
        static float size;
        static int area;
        static int counter;

        static int underCounter;
        static int overCounter;
        static int cX;
        static int cY;
        static float Zoom;
        static float OnScreenSize;

        static bool UniverseView;
        static byte SelectedTier;

        static public void Initialize()
        {
            ChunkSize = 1000000;
            WorldVariables.GalaxyChunkSize = ChunkSize;
            ChunkX = 9999;
            ChunkY = 9999;
            area = 3;
            UniverseView = true;
            Galaxies = new List<Galaxy>();

            CenterX = ChunkX / 2;
            CenterY = ChunkY / 2;
            counter = 0;

            rand = new Random(23);
            
            for (int i = 0; i < 200; i++)
            {
                if (i == 0)
                {
                    Galaxies.Add(new Galaxy());
                    Galaxies[Galaxies.Count - 1].position = new Vector2(CenterX, CenterY);
                    Galaxies[Galaxies.Count - 1].area = 750;
                    Galaxies[Galaxies.Count - 1].rotation = 0f;
                    Galaxies[Galaxies.Count - 1].Tier = 10;
                }
                else
                {
                    Galaxies.Add(new Galaxy());
                    Galaxies[Galaxies.Count - 1].position = new Vector2(rand.Next(0, ChunkX), rand.Next(0, ChunkY));
                    Galaxies[Galaxies.Count - 1].area = (int)UsefulMethods.FindBetween((int)Vector2.Distance(Galaxies[Galaxies.Count - 1].position, new Vector2(CenterX, CenterY)), CenterX, 0, 400, 100, true);
                    Galaxies[Galaxies.Count - 1].rotation = (float)(rand.NextDouble() * MathHelper.TwoPi);

                    for (int o = 0; o < Galaxies.Count - 1; o++)
                    {
                        while (Vector2.Distance(Galaxies[i].position, Galaxies[o].position) < Galaxies[i].area + Galaxies[i].area)
                        {
                            Galaxies[Galaxies.Count - 1].position = new Vector2(rand.Next(0, ChunkX), rand.Next(0, ChunkY));
                            Galaxies[Galaxies.Count - 1].area = (int)UsefulMethods.FindBetween((int)Vector2.Distance(Galaxies[Galaxies.Count - 1].position, new Vector2(CenterX, CenterY)), CenterX, 0, 300, 50, true);
                            o = 0;
                        }
                    }

                    Vector2 Center = new Vector2(CenterX, CenterY);
                    float MaxDistance = Vector2.Distance(Vector2.Zero, Center);

                    Galaxies[Galaxies.Count - 1].Tier = (byte)UsefulMethods.FindBetween((int)Vector2.Distance(Galaxies[i].position, Center) - 750, (int)CenterX, 0, 10f, 0f, true);
                }
            }

            systemManager = new SystemManager[ChunkX][];
            for (int x = 0; x < systemManager.Length; x++)
            {
                Console.Clear();
                Console.WriteLine(x);

                systemManager[x] = new SystemManager[ChunkY];

                for (int y = 0; y < systemManager[x].Length; y++)
                {
                    //int count = (byte)GetCount(x, y);
                    //SystemCount += count;
                }
            }

            parallax = new List<Parallax>();

        }

        static public void LoadContent(ContentManager Content)
        {
            test = Content.Load<Texture2D>("Images//TestSquare");
            Grid1 = Content.Load<Texture2D>("Images//Galaxy//Explorer//grid1");
            Grid2 = Content.Load<Texture2D>("Images//Galaxy//Explorer//grid2");
            GridBG = Content.Load<Texture2D>("Images//Galaxy//Explorer//background1");
            size = (ChunkSize * 2f) / (float)GridBG.Width;
        }
        
        static public void Update(GameTime gameTime)
        {
            //Make updates happen near important entities

            cX = (int)CameraManager.CamerasRead[0].Location.X;
            cY = (int)CameraManager.CamerasRead[0].Location.Y;

            int counter2 = 0;

            int r = 3;
            int r2 = 1;

            bool systemLoaded = false;

            if (r == 1)
            {
            }
            while (r > r2)
            {
                int rP = r2 * r2;

                for (int x = -r2; x <= r2; x++)
                {
                    int height = (int)Math.Sqrt(rP - (x * x));

                        for (int y = -height; y <= height; y++)

                            {
                                Vector2 c = GetChunk(x + cX, y + cY);
                                int x2 = (int)c.X;
                                int y2 = (int)c.Y;
                                                        
                                if (systemManager[x2][y2] == null)
                                {
                                    if (!systemLoaded)
                                    {
                                        int systemCount = GetCount(x2, y2);

                                        systemManager[x2][y2] = new SystemManager();

                                        if (x != CenterX || y != CenterY)
                                            systemManager[x2][y2].Initialize(ChunkSize, systemCount, x2, y2, SelectedTier);
                                        else
                                            systemManager[x2][y2].Initialize(ChunkSize, 0, x2, y2, SelectedTier);

                                        systemLoaded = true;
                                    }
                                }
                                else if (!systemManager[x2][y2].SystemLoaded)
                                {
                                    systemLoaded = true;

                                    while (counter < 50)
                                    {
                                        if (systemManager[x2][y2].systems.Count >= systemManager[x2][y2].SystemCount)
                                        {
                                            systemManager[x2][y2].SystemLoaded = true;
                                            break;
                                        }
                                        else
                                        {
                                            AddSystems(x2, y2);
                                            counter++;
                                        }
                                    }

                                }
                                else if (x > -2 && x < 2 && y > -2 && y < 2)
                                {
                                    if (systemManager[x2][y2] != null)
                                        systemManager[x2][y2].Update(gameTime);
                                }
                                else
                                {
                                    if (counter2 >= overCounter)
                                    {
                                        if (underCounter < 7)
                                        {
                                            systemManager[x2][y2].Update(gameTime);
                                            underCounter++;
                                            overCounter++;
                                        }
                                    }
                                }
                            }
                }

            r2++;
        }
            
            if (counter > 0)
                counter = 0;
            
            if (overCounter >= Math.Pow(((area * 4) + 1), 2))
                overCounter = 0;

            //counter = 0;
            underCounter = 0;
        }

        static private Vector2 GetChunk(int x, int y)
        {
            int x2 = 0;
            int y2 = 0;

            if (x < 0)
                x2 = x + ChunkX;
            else if (x >= ChunkX)
                x2 = x - ChunkX;
            else
                x2 = x;

            if (y < 0)
                y2 = y + ChunkY;
            else if (y >= ChunkY)
                y2 = y - ChunkY;
            else
                y2 = y;

            return new Vector2(x2, y2);
        }

        static private void AddSystems(int x2, int y2)
        {
            if (systemManager[x2][y2].AddSystem())
            {
                int Check = systemManager[x2][y2].systems.Count - 1;
                bool breakSearch = false;

                for (int x3 = x2 - 1; x3 <= x2 + 1; x3++)
                    for (int y3 = y2 - 1; y3 <= y2 + 1; y3++)
                    {
                        if (breakSearch)
                            break;

                        if (x2 != x3 || y2 != y3)
                        {
                            Vector2 Offset = new Vector2((ChunkSize * 2) * (x3 - x2), (ChunkSize * 2) * (y3 - y2));
                            Vector2 C = GetChunk(x3, y3);
                            int x4 = (int)C.X;
                            int y4 = (int)C.Y;
                            
                            if (systemManager[x4][y4] != null)
                                for (int o = 0; o < systemManager[x4][y4].systems.Count; o++)
                                {
                                    Vector2 p1 = systemManager[x2][y2].systems[Check].Position;
                                    Vector2 p2 = systemManager[x4][y4].systems[o].Position + Offset;
                                    float distance = Vector2.Distance(p1, p2);

                                    if (distance < (systemManager[x2][y2].systems[Check].SystemRadius / 2) + (systemManager[x4][y4].systems[o].SystemRadius / 2))
                                    {
                                        systemManager[x2][y2].systems.RemoveAt(Check);
                                        breakSearch = true;
                                        break;
                                    }
                                }
                        }
                    }
            }
        }

        static public void UpdateParallax(GameTime gameTime, int Camera, ContentManager Content)
        {
            if (PreviousCameraCount != CameraManager.CameraCount)
            {
                parallax.Clear();

                for (int i = 0; i < CameraManager.CameraCount; i++)
                {
                    parallax.Add(new Parallax());
                    parallax[i].LoadContent(Content);
                }
            }

            parallax[Camera].Update(gameTime, Camera);

            PreviousCameraCount = CameraManager.CameraCount;
        }

        static private int GetCount(int x, int y)
        {
            int num = 0;
            SelectedTier = 0;

            for (int i = 0; i < Galaxies.Count; i++)
            {
                int area = Galaxies[i].area;
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(Galaxies[i].position.X, Galaxies[i].position.Y));
                int n = 0;

                if (distance < area / 2)
                {
                    if (distance < (area / 2) * 0.2f)
                        n = (int)(rand.Next(50, 150) * (UsefulMethods.FindBetween((int)distance, (int)((area / 2) * 0.2f), 0, 1f, 0.65f, true) * (rand.Next(9000, 11000) / 10000f)));
                    else if (distance < (area / 2) * 0.6f)
                        n = (int)(rand.Next(25, 100) * (UsefulMethods.FindBetween((int)distance, (int)((area / 2) * 0.6f), (int)((area / 2) * 0.2f), 0.5f, 0.2f, true) * (rand.Next(9000, 11000) / 10000f)));
                    else
                        n = (int)(rand.Next(10, 75) * (UsefulMethods.FindBetween((int)distance, (int)(area / 2), (int)((area / 2) * 0.6f), 0.2f, 0f, true) * (rand.Next(9000, 11000) / 10000f)));
                    
                    //Set bottom to core size.
                    float tier = UsefulMethods.FindBetween((int)distance, (int)(area / 2), 0, (Galaxies[i].Tier + 1) * 10, Galaxies[i].Tier * 10, true); 

                    if (tier > SelectedTier)
                        SelectedTier = (byte)tier;
                }

                num += n;
                Galaxies[i].KnownStars += n;
            }

            return num;
        }
        
        static public void Draw(SpriteBatch spriteBatch, int cameraNumber)
        {
            cX = (int)CameraManager.CamerasRead[cameraNumber].Location.X;
            cY = (int)CameraManager.CamerasRead[cameraNumber].Location.Y;            
            Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
            OnScreenSize = (ChunkSize * 2) * Zoom;
            
            Console.WriteLine(area);
            Console.WriteLine(cX + "," + cY);

            //UniverseView = false;
            //Test(spriteBatch);

            //if (UniverseView)            
            //    DrawUniverse(spriteBatch, cameraNumber);            
            //else            
                DrawDetail(spriteBatch, cameraNumber);
            
        }

        static private void DrawUniverse(SpriteBatch spriteBatch, int cameraNumber)
        {
            OnScreenSize = 64 * Zoom;

            if (OnScreenSize > 64f)
            {
                UniverseView = false;
                CameraManager.CamerasRead[cameraNumber].Zoom = 0.000016f;
                CameraManager.CamerasRead[cameraNumber].ZoomDestination = 0.000016f;
            }
            else
            {
                Vector2 MLocation = new Vector2(2500 * 64, 2500 * 64);
                Vector2 Offset = new Vector2(cX * 64, cY * 64);

                spriteBatch.Draw(StaticTests.Marker, MLocation - Offset, null, Color.White, 0f, new Vector2(512, 512), 1f, SpriteEffects.None, 0f);
            }
        }
        
        static private void DrawDetail(SpriteBatch spriteBatch, int cameraNumber)
        {
            area = (int)((((WorldVariables.WindowWidth / OnScreenSize) + 1) / 2) + 1);

            //if (OnScreenSize * (((area - 1) * 2) - 1) > WorldVariables.WindowWidth)
            //    area--;
            //else if (OnScreenSize * ((area * 2) - 1) < WorldVariables.WindowWidth)
            //    area++;

            //CameraManager.CamerasRead[0].Location = new Vector3(100, 2500, 1);

            #region OLD

            //if (OnScreenSize < 25)
            //{
            //    Vector2 Offset = new Vector2((ChunkSize * 2) * (CenterX - cX), (ChunkSize * 2) * (CenterY - cY));
            //    //spriteBatch.Draw(GalaxyObjectData.SystemCores[0].CoronaTextures[1], Vector2.Zero + Offset, null, Color.White, 0f, new Vector2(GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width / 2, GalaxyObjectData.SystemCores[0].CoronaTextures[1].Height / 2), ((ChunkSize * 2) / GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width) * 750, SpriteEffects.None, 0f);

            //    //spriteBatch.Draw(GalaxyObjectData.SystemCores[0].BodyTextures[1], Vector2.Zero + Offset, null, Color.White, 0f, new Vector2(GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width / 2, GalaxyObjectData.SystemCores[0].CoronaTextures[1].Height / 2), ((ChunkSize * 2) / GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width) * 750, SpriteEffects.None, 0f);

            //    area = 1;
            //}
            //else if (OnScreenSize < 50)
            //{
            //    float test = UsefulMethods.FindBetween((int)(OnScreenSize * 100), 5000, 2500, 1f, 0f, false);
            //    float test2 = 1 - test;
            //    Vector2 Offset;

            //    for (int x = cX - area; x <= cX + area; x++)
            //        for (int y = cY - area; y <= cY + area; y++)
            //        {
            //            int x2 = 0;
            //            int y2 = 0;
            //            Offset = new Vector2((ChunkSize * 2) * (x - cX), (ChunkSize * 2) * (y - cY));

            //            if (x < 0)
            //                x2 = x + ChunkX;
            //            else if (x >= ChunkX)
            //                x2 = x - ChunkX;
            //            else
            //                x2 = x;

            //            if (y < 0)
            //                y2 = y + ChunkY;
            //            else if (y >= ChunkY)
            //                y2 = y - ChunkY;
            //            else
            //                y2 = y;


            //            string s = x2.ToString() + y2.ToString();
            //            int seed = int.Parse(s);
            //            Random r = new Random(seed);
            //            int rot = r.Next(0, 4);
            //            float rotation = 0f;
            //            Texture2D tile;
            //            if (r.Next(0, 2) == 0)
            //                tile = StaticTests.Marker2;
            //            else
            //                tile = StaticTests.Marker3;

            //            switch (rot)
            //            {
            //                case 1:
            //                    rotation = (float)Math.PI / 2;
            //                    break;

            //                case 2:
            //                    rotation = (float)Math.PI;
            //                    break;

            //                case 3:
            //                    rotation = (float)Math.PI * 1.5f;
            //                    break;
            //            }


            //            if (x2 != CenterX || y2 != CenterY)
            //                spriteBatch.Draw(tile, Vector2.Zero + Offset, null, Color.White * test, rotation, new Vector2(StaticTests.Marker2.Width / 2, StaticTests.Marker2.Height / 2), (ChunkSize * 2) / StaticTests.Marker2.Width, SpriteEffects.None, 0f);
            //            else
            //                systemManager[x2][y2].Draw(spriteBatch, cameraNumber, Offset, test);
            //        }


            //    Offset = new Vector2((ChunkSize * 2) * (CenterX - cX), (ChunkSize * 2) * (CenterY - cY));
            //    //spriteBatch.Draw(GalaxyObjectData.SystemCores[0].CoronaTextures[1], Vector2.Zero + Offset, null, Color.White * test2, 0f, new Vector2(GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width / 2, GalaxyObjectData.SystemCores[0].CoronaTextures[1].Height / 2), ((ChunkSize * 2) / GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width) * 750, SpriteEffects.None, 0f);
            //    //spriteBatch.Draw(GalaxyObjectData.SystemCores[0].BodyTextures[1], Vector2.Zero + Offset, null, Color.White * test2, 0f, new Vector2(GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width / 2, GalaxyObjectData.SystemCores[0].CoronaTextures[1].Height / 2), ((ChunkSize * 2) / GalaxyObjectData.SystemCores[0].CoronaTextures[1].Width) * 750, SpriteEffects.None, 0f);

            //    area = 1;
            //}
            //else if (OnScreenSize < 100)
            //{
            //    for (int x = cX - area; x <= cX + area; x++)
            //        for (int y = cY - area; y <= cY + area; y++)
            //        {
            //            int x2 = 0;
            //            int y2 = 0;
            //            Vector2 Offset = new Vector2((ChunkSize * 2) * (x - cX), (ChunkSize * 2) * (y - cY));

            //            if (x < 0)
            //                x2 = x + ChunkX;
            //            else if (x >= ChunkX)
            //                x2 = x - ChunkX;
            //            else
            //                x2 = x;

            //            if (y < 0)
            //                y2 = y + ChunkY;
            //            else if (y >= ChunkY)
            //                y2 = y - ChunkY;
            //            else
            //                y2 = y;

            //            string s = x2.ToString() + y2.ToString();
            //            int seed = int.Parse(s);
            //            Random r = new Random(seed);
            //            int rot = r.Next(0, 4);
            //            float rotation = 0f;
            //            Texture2D tile;
            //            if (r.Next(0, 2) == 0)
            //                tile = StaticTests.Marker2;
            //            else
            //                tile = StaticTests.Marker3;


            //            switch (rot)
            //            {
            //                case 1:
            //                    rotation = (float)Math.PI / 2;
            //                    break;

            //                case 2:
            //                    rotation = (float)Math.PI;
            //                    break;

            //                case 3:
            //                    rotation = (float)Math.PI * 1.5f;
            //                    break;
            //            }

            //            if (x2 != CenterX || y2 != CenterY)
            //                spriteBatch.Draw(tile, Vector2.Zero + Offset, null, Color.White, rotation, new Vector2(StaticTests.Marker2.Width / 2, StaticTests.Marker2.Height / 2), (ChunkSize * 2) / StaticTests.Marker2.Width, SpriteEffects.None, 0f);
            //            else
            //                systemManager[x2][y2].Draw(spriteBatch, cameraNumber, Offset, 1f);
            //        }

            //    area = 1;
            //}
            //else if (OnScreenSize < 400)
            //{
            //    float test = UsefulMethods.FindBetween((int)(OnScreenSize * 100), 40000, 10000, 1f, 0f, false);
            //    float test2 = 1 - test;

            //    test *= 2;
            //    if (test > 1)
            //        test = 1;

            //    for (int x = cX - area; x <= cX + area; x++)
            //        for (int y = cY - area; y <= cY + area; y++)
            //        {
            //            int x2 = 0;
            //            int y2 = 0;
            //            Vector2 Offset = new Vector2((ChunkSize * 2) * (x - cX), (ChunkSize * 2) * (y - cY));

            //            if (x < 0)
            //                x2 = x + ChunkX;
            //            else if (x >= ChunkX)
            //                x2 = x - ChunkX;
            //            else
            //                x2 = x;

            //            if (y < 0)
            //                y2 = y + ChunkY;
            //            else if (y >= ChunkY)
            //                y2 = y - ChunkY;
            //            else
            //                y2 = y;

            //            string s = x2.ToString() + y2.ToString();
            //            int seed = int.Parse(s);
            //            Random r = new Random(seed);
            //            int rot = r.Next(0, 4);
            //            float rotation = 0f;
            //            Texture2D tile;
            //            if (r.Next(0, 2) == 0)
            //                tile = StaticTests.Marker2;
            //            else
            //                tile = StaticTests.Marker3;

            //            switch (rot)
            //            {
            //                case 1:
            //                    rotation = (float)Math.PI / 2;
            //                    break;

            //                case 2:
            //                    rotation = (float)Math.PI;
            //                    break;

            //                case 3:
            //                    rotation = (float)Math.PI * 1.5f;
            //                    break;
            //            }

            //            if (x2 != CenterX || y2 != CenterY)
            //                spriteBatch.Draw(tile, Vector2.Zero + Offset, null, Color.White * test2, rotation, new Vector2(StaticTests.Marker2.Width / 2, StaticTests.Marker2.Height / 2), (ChunkSize * 2) / StaticTests.Marker2.Width, SpriteEffects.None, 0f);
            //            else
            //                systemManager[x2][y2].Draw(spriteBatch, cameraNumber, Offset, test2);

            //            if (systemManager[x2][y2] != null)
            //                systemManager[x2][y2].Draw(spriteBatch, cameraNumber, Offset, test);
            //        }

            //    //area = 1;
            //}
            //else

            #endregion

            if (OnScreenSize <= 0.5f)
            {
                CameraManager.CamerasRead[cameraNumber].Zoom = 0.00000025f;
                CameraManager.CamerasRead[cameraNumber].ZoomDestination = 0.00000025f;
            }
            if (OnScreenSize < 64)
            {
                for (int i = 0; i < Galaxies.Count; i++)
                {
                    int x = (int)Galaxies[i].position.X;
                    int y = (int)Galaxies[i].position.Y;

                    int x2 = x;
                    int y2 = y;
                    
                    float distance = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2, y2));

                    float d1 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 + ChunkX, y2));
                    float d2 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 - ChunkX, y2));
                    float d3 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2, y2 + ChunkY));
                    float d4 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2, y2 - ChunkY));

                    if (Math.Abs(distance) > Math.Abs(d1))
                        distance = d1;
                    if (Math.Abs(distance) > Math.Abs(d2))
                        distance = d2;
                    if (Math.Abs(distance) > Math.Abs(d3))
                        distance = d3;
                    if (Math.Abs(distance) > Math.Abs(d4))
                        distance = d4;

                    d1 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 + ChunkX, y2 - ChunkY));
                    d2 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 - ChunkX, y2 + ChunkY));
                    d3 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 - ChunkX, y2 + ChunkY));
                    d4 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 + ChunkX, y2 - ChunkY));

                    if (Math.Abs(distance) > Math.Abs(d1))
                        distance = d1;
                    if (Math.Abs(distance) > Math.Abs(d2))
                        distance = d2;
                    if (Math.Abs(distance) > Math.Abs(d3))
                        distance = d3;
                    if (Math.Abs(distance) > Math.Abs(d4))
                        distance = d4;

                    d1 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 + ChunkX, y2 + ChunkY));
                    d2 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 - ChunkX, y2 - ChunkY));
                    d3 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 - ChunkX, y2 - ChunkY));
                    d4 = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2 + ChunkX, y2 + ChunkY));

                    if (Math.Abs(distance) > Math.Abs(d1))
                        distance = d1;
                    if (Math.Abs(distance) > Math.Abs(d2))
                        distance = d2;
                    if (Math.Abs(distance) > Math.Abs(d3))
                        distance = d3;
                    if (Math.Abs(distance) > Math.Abs(d4))
                        distance = d4;

                    if (x == 51 && y == 69)
                    {
                        Console.WriteLine(distance);
                    }

                    //if (distance > 5000)
                    //    distance -= 10000;
                    //if (distance < area + Galaxies[i].area)
                    {
                        int X = x - cX;
                        int Y = y - cY;

                        if (X < -ChunkX / 2)
                            X += ChunkX;
                        else if (X > ChunkX / 2)
                            X -= ChunkX;

                        if (Y < -ChunkY / 2)
                            Y += ChunkY;
                        else if (Y > ChunkY / 2)
                            Y -= ChunkY;

                        Vector2 Offset = new Vector2((ChunkSize * 2) * X, (ChunkSize * 2) * Y);
                        
                        float alpha = UsefulMethods.FindBetween((int)Math.Abs(distance * 10), 10000, 1500 + (Galaxies[i].area * 10), 1f, 0f, true);

                        if (alpha != 0)
                        {
                            spriteBatch.Draw(StaticTests.Galaxy, Vector2.Zero + Offset, null, Color.White * alpha, Galaxies[i].rotation, new Vector2(512, 512), size * Galaxies[i].area, SpriteEffects.None, 0f);

                            InfoBox.ClearList();
                            InfoBox.AddItem("Tier: " + Galaxies[i].Tier);
                            InfoBox.AddItem("Position: " + Galaxies[i].position);
                            InfoBox.AddItem("Distance: " + distance);
                            InfoBox.AddItem("Area: " + Galaxies[i].area);
                            InfoBox.AddItem("Star Systems: " + Galaxies[i].KnownStars);
                            InfoBox.Draw(spriteBatch, Vector2.Zero + Offset + new Vector2(Galaxies[i].area * ChunkSize, -(Galaxies[i].area * ChunkSize)), (size * Galaxies[i].area) * 5, alpha);
                        }
                    }
                }
            }
            else if (OnScreenSize < 512)
            {
                float a = UsefulMethods.FindBetween((int)(OnScreenSize * 100), 12800, 6400, 1f, 0f, false);
                float a2 = UsefulMethods.FindBetween((int)(OnScreenSize * 100), 12800, 6400, 1f, 0f, true);

                for (int x = cX - area; x <= cX + area; x++)
                    for (int y = cY - area; y <= cY + area; y++)
                    {
                        Vector2 Offset = new Vector2((ChunkSize * 2) * (x - cX), (ChunkSize * 2) * (y - cY));
                        //spriteBatch.Draw(test, Vector2.Zero + Offset, null, Color.DarkGray, 0f, new Vector2(GridBG.Width / 2, GridBG.Height / 2), size, SpriteEffects.None, 0f);
                        //spriteBatch.Draw(Grid1, Vector2.Zero + Offset, null, Color.White, 0f, new Vector2(GridBG.Width / 2, GridBG.Height / 2), size, SpriteEffects.None, 0f);
                        if (OnScreenSize > 64)                        
                            spriteBatch.Draw(Grid2, Vector2.Zero + Offset, null, Color.White * 0.4f * a, 0f, new Vector2(GridBG.Width / 2, GridBG.Height / 2), size, SpriteEffects.None, 0f);
                        

                        int x2 = 0;
                        int y2 = 0;

                        if (x < 0)
                            x2 = x + ChunkX;
                        else if (x >= ChunkX)
                            x2 = x - ChunkX;
                        else
                            x2 = x;

                        if (y < 0)
                            y2 = y + ChunkY;
                        else if (y >= ChunkY)
                            y2 = y - ChunkY;
                        else
                            y2 = y;

                        if (systemManager[x2][y2] != null)
                            systemManager[x2][y2].DrawSimple(spriteBatch, cameraNumber, Offset, a);
                        else
                        {
                            InfoBox.ClearList();
                            InfoBox.AddItem("???");
                            InfoBox.Draw(spriteBatch, Vector2.Zero - new Vector2(334800, 254800) + Offset, 20480, a);
                        }
                    }
                if (OnScreenSize < 128)
                {
                    for (int i = 0; i < Galaxies.Count; i++)
                    {
                        int x = (int)Galaxies[i].position.X;
                        int y = (int)Galaxies[i].position.Y;

                        int x2 = x;
                        int y2 = y;

                        float distance = Vector2.Distance(new Vector2(cX, cY), new Vector2(x2, y2));

                        if (distance < area + Galaxies[i].area)
                        {
                            Console.WriteLine(Galaxies[i].position);

                            int X = x - cX;
                            int Y = y - cY;

                            if (X < -ChunkX / 2)
                                X += ChunkX;
                            else if (X > ChunkX / 2)
                                X -= ChunkX;

                            if (Y < -ChunkY / 2)
                                Y += ChunkY;
                            else if (Y > ChunkY / 2)
                                Y -= ChunkY;

                            Vector2 Offset = new Vector2((ChunkSize * 2) * X, (ChunkSize * 2) * Y);

                            spriteBatch.Draw(StaticTests.Galaxy, Vector2.Zero + Offset, null, Color.White * a2, Galaxies[i].rotation, new Vector2(512, 512), size * Galaxies[i].area, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
            else
                for (int x = cX - area; x <= cX + area; x++)
                    for (int y = cY - area; y <= cY + area; y++)
                    {
                        int x2 = 0;
                        int y2 = 0;
                        Vector2 Offset = new Vector2((ChunkSize * 2) * (x - cX), (ChunkSize * 2) * (y - cY));

                        if (x < 0)
                            x2 = x + ChunkX;
                        else if (x >= ChunkX)
                            x2 = x - ChunkX;
                        else
                            x2 = x;

                        if (y < 0)
                            y2 = y + ChunkY;
                        else if (y >= ChunkY)
                            y2 = y - ChunkY;
                        else
                            y2 = y;

                        if (Zoom < 0.00075f)
                        {
                            float alpha = UsefulMethods.FindBetween((int)(Zoom * 1000000), 750, 250, 1f, 0f, true);
                            spriteBatch.Draw(Grid2, Vector2.Zero + Offset, null, Color.White * 0.4f * alpha, 0f, new Vector2(GridBG.Width / 2, GridBG.Height / 2), size, SpriteEffects.None, 0f);

                            if (systemManager[x2][y2] != null)
                                systemManager[x2][y2].DrawSimple(spriteBatch, cameraNumber, Offset, alpha);
                            else
                            {
                                InfoBox.ClearList();
                                InfoBox.AddItem("???");
                                InfoBox.Draw(spriteBatch, Vector2.Zero - new Vector2(334800, 254800) + Offset, 20480, 1f);
                            }
                        }

                        if (systemManager[x2][y2] != null)
                            systemManager[x2][y2].Draw(spriteBatch, cameraNumber, Offset, 1f);
                    }
        }

        static public void DrawParallax(SpriteBatch spriteBatch, int cameraNumber)
        {
            parallax[cameraNumber].Draw(spriteBatch, cameraNumber);
        }
    }
}
