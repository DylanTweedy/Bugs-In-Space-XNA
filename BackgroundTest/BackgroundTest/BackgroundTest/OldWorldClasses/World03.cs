using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimplexNoise;
using Bitmap = System.Drawing.Bitmap;
using BMPColor = System.Drawing.Color;

namespace BackgroundTest
{
    class World3
    {
        byte[][] chunk;
        ushort[][] tileID;
        byte[][] backgroundID;
        List<ushort[]> tileTest;


        bool[][] tileEmpty;
        Vector2 worldPosition;
        List<Texture2D> tileTextures;
        short tilesX;
        short tilesY;
        short chunkX;
        short chunkY;

        Random random;
        
        byte tileSize;
        byte chunkSize;

        Vector2 position;

        Vector2 CameraPosition;
        float CameraZoom;
        
        short RenderTileX;
        short RenderTileY;
        short RenderTileX2;
        short RenderTileY2;
        
        public void Initialize()
        {
            tileSize = WorldVariables.TileSize;
            chunkSize = WorldVariables.ChunkSize;

            random = new Random(100);
            worldPosition = Vector2.Zero;

            chunkX = 5;
            chunkY = 5;

            tilesX = (short)(chunkX * 64);
            tilesY = (short)(chunkY * 64);
            
            tileID = new ushort[tilesX][];

            for (int i = 0; i < tileID.Length; i++)
                tileID[i] = new ushort[tilesY];

            //chunk = new byte[chunkX][];

            //for (int i = 0; i < chunk.Length; i++)
            //    chunk[i] = new byte[chunkY];


            //backgroundID = new byte[tilesX][];

            //for (int i = 0; i < backgroundID.Length; i++)
            //    backgroundID[i] = new byte[tilesY];

            Noisetest();
            Test();    
 
            MinerTest();       
            //Trim(0, tileID.Length, 0, tileID[0].Length, 2, 3, true);


            //WorldGeneration();
        }

        ushort[][] tileHolder;
        ushort[][] tileHolder2;
        Bitmap bmp1;
        Bitmap bmp2;
        Bitmap bmp3;
        byte[] noiseSeed;

        private void MinerTest()
        {
            for (int x = 0; x < tilesX; x++)
            {
                for (int y = 0; y < tilesY; y++)
                {
                    tileID[x][y] = 1;
                }
            }

            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 10; y++)
                {
                    tileID[(int)(tilesX * (x / 10f)) - 1][(int)(tilesY * (y / 10f)) - 1] = 6;
                }
            }
        }

        private void Noisetest()
        {
            noiseSeed = new byte[512];
            Random rand = new Random(101);
            rand.NextBytes(noiseSeed);

            Noise.perm = noiseSeed;

            bmp1 = new Bitmap(tilesX, tilesY);

            for (int x = 0; x < tilesX; x++)
            {
                for (int y = 0; y < tilesY; y++)
                {
                    int calc = (int)(((Noise.Generate(x / 20f, y / 20f) + 1) / 2) * 255);
                    bmp1.SetPixel(x, y, BMPColor.FromArgb(calc, calc, calc));
                }
            }
        }

        private void Test()
        {
            tileHolder = new ushort[tilesX][];

            for (int t = 0; t < tileHolder.Length; t++)
                tileHolder[t] = new ushort[tilesY];

            for (int x = 0; x < tilesX; x++)
            {
                for (int y = 0; y < tilesY; y++)
                {
                    int dest1 = (int)(255 / (1 + (5 * ((float)y / tilesY))));
                    int dest2 = (int)(255 / (1.5 + (5 * ((float)y / tilesY))));

                    int c1 = bmp1.GetPixel(x, y).R;
                    
                    if (c1 > dest1)
                        tileHolder[x][y] = 1;
                    else
                        tileHolder[x][y] = 0;

                    if (tileHolder[x][y] != 0)
                    tileID[x][y] = tileHolder[x][y];
                }
            }
        }


        private void WorldGeneration()
        {
            //int HillSize = 500;
            //int AirSize = 100;
            //int SolidSize = 50;

            int chunkX2 = chunkX / 8;
            int chunkY2 = chunkY / 8;

            //for (int x = 0; x < chunkX2; x++)
            //{
            //    for (int y = 0; y < chunkY2; y++)
            //    {

            //        int xMin = (tileID.Length / chunkX2) * x;
            //        int xMax = (tileID.Length / chunkX2) * (x + 1);

            //        int yMin = (tileID[0].Length / chunkY2) * y;
            //        int yMax = (tileID[0].Length / chunkY2) * (y + 1);

            //        int density = (int)(50 + (10 * ((float)y / chunkY2)));

            //        GenerateCavesTest(xMin, xMax, yMin, yMax, 1, 3, density, true, false, true);
            //        Trim(xMin, xMax, yMin, yMax, 2, 3, true);
            //    }
            //}


            //GenerateAir(0, tileID.Length, 0, AirSize);
            //GenerateHills(0, tileID.Length, AirSize, AirSize + HillSize, 1, 10);
            //GenerateSolid(0, tileID.Length, AirSize + HillSize, AirSize + HillSize + SolidSize, 1);           
            //GenerateRandoms(0, tileID.Length, (HillSize + AirSize + SolidSize), tileID[0].Length, 1);
            //GenerateCaves(0, tileID.Length, (HillSize + AirSize + SolidSize), tileID[0].Length, 1);

            //Smooth(0, tileID.Length, 0, AirSize + HillSize + SolidSize, 4);
            
            //GenerateStone(0, tileID.Length, 0, tileID[0].Length, 2);

            GenerateCavesTest(0, tileID.Length, 0, (tileID[0].Length / 4), 1, 3, 50, true, false, true);
            Trim(0, tileID.Length, 0, (tileID[0].Length / 4), 2, 3, true);

            GenerateCavesTest(0, tileID.Length, (tileID[0].Length / 4), (tileID[0].Length / 4) * 2, 1, 3, 52, true, false, true);
            Trim(0, tileID.Length, (tileID[0].Length / 4), (tileID[0].Length / 4) * 2, 2, 3, true);

            GenerateCavesTest(0, tileID.Length, (tileID[0].Length / 4) * 2, (tileID[0].Length / 4) * 3, 1, 3, 54, true, false, true);
            Trim(0, tileID.Length, (tileID[0].Length / 4) * 2, (tileID[0].Length / 4) * 3, 2, 3, true);

            GenerateCavesTest(0, tileID.Length, (tileID[0].Length / 4) * 3, (tileID[0].Length / 4) * 4, 1, 3, 56, true, false, true);
            Trim(0, tileID.Length, (tileID[0].Length / 4) * 3, (tileID[0].Length / 4) * 4, 2, 3, true);

            //GenerateCavesTest(0, tileID.Length, 0, tileID[0].Length, 2, 1, 55, false, true);
            GenerateCavesTest(0, tileID.Length, 0, tileID[0].Length, 1, 1, 37, false, true, false);
            GenerateCavesTest(0, tileID.Length, 0, tileID[0].Length, 3, 1, 35, false, true, false);
            GenerateCavesTest(0, tileID.Length, 0, tileID[0].Length, 4, 1, 33, false, true, false);
            GenerateCavesTest(0, tileID.Length, 0, tileID[0].Length, 5, 1, 30, false, true, false);

            tileHolder = null;
            tileHolder2 = null;

            GC.Collect();

        }

        private void Trim(int xMin, int xMax, int yMin, int yMax, ushort tile, int iterations, bool jagged)
        {
            byte i;
            bool wall;

            tileHolder2 = new ushort[tilesX][];

            for (int t = 0; t < tileHolder2.Length; t++)
                tileHolder2[t] = new ushort[tilesY];

            for (int s = 0; s < iterations; s++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        i = 0;

                        if (tileHolder[x][y] == 0)
                            wall = false;
                        else
                            wall = true;

                        if (x - 1 > 0 || y - 1 > 0 || x + 1 <= tileID.Length || y + 1 <= tileID[0].Length)
                        {
                            if (x - 1 >= xMin && y - 1 >= yMin)
                            {
                                if (tileHolder[x - 1][y - 1] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (y - 1 >= yMin)
                            {
                                if (tileHolder[x][y - 1] != 0)
                                    i += 4;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (x + 1 < xMax && y - 1 >= yMin)
                            {
                                if (tileHolder[x + 1][y - 1] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (x - 1 >= xMin)
                            {
                                if (tileHolder[x - 1][y] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (x + 1 < xMax)
                            {
                                if (tileHolder[x + 1][y] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (x - 1 >= xMin && y + 1 < yMax)
                            {
                                if (tileHolder[x - 1][y + 1] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (y + 1 < yMax)
                            {
                                if (tileHolder[x][y + 1] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }

                            if (x + 1 < xMax && y + 1 < yMax)
                            {
                                if (tileHolder[x + 1][y + 1] != 0)
                                    i++;
                            }
                            else
                            {
                                tileHolder2[x][y] = tile;
                                continue;
                            }
                        }
                        else
                        {
                            if (x - 1 >= xMin && y - 1 >= yMin)
                                if (tileHolder[x - 1][y - 1] != 0)
                                    i++;
                            if (y - 1 >= yMin)
                                if (tileHolder[x][y - 1] != 0)
                                    i++;
                            if (x + 1 < xMax && y - 1 >= yMin)
                                if (tileHolder[x + 1][y - 1] != 0)
                                    i++;
                            if (x - 1 >= xMin)
                                if (tileHolder[x - 1][y] != 0)
                                    i++;
                            if (x + 1 < xMax)
                                if (tileHolder[x + 1][y] != 0)
                                    i++;
                            if (x - 1 >= xMin && y + 1 < yMax)
                                if (tileHolder[x - 1][y + 1] != 0)
                                    i++;
                            if (y + 1 < yMax)
                                if (tileHolder[x][y + 1] != 0)
                                    i++;
                            if (x + 1 < xMax && y + 1 < yMax)
                                if (tileHolder[x + 1][y + 1] != 0)
                                    i++;
                        }

                        if (wall && i >= 7 && !jagged)
                            tileHolder2[x][y] = tile;
                        else if (wall && i >= random.Next(5, 9) && jagged)
                            tileHolder2[x][y] = tile;
                        else
                            tileHolder2[x][y] = 0;
                    }
                }

                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        if (tileHolder2[x][y] == tile)
                            tileHolder[x][y] = tile;
                        else
                            tileHolder[x][y] = 0;
                    }
                }
            }

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    i = 0;

                    if (tileHolder[x][y] == 0)
                        wall = false;
                    else
                        wall = true;

                    if (x - 1 >= xMin && y - 1 >= yMin)
                        if (tileHolder[x - 1][y - 1] != 0)
                            i++;
                    if (y - 1 >= yMin)
                        if (tileHolder[x][y - 1] != 0)
                            i++;
                    if (x + 1 < xMax && y - 1 >= yMin)
                        if (tileHolder[x + 1][y - 1] != 0)
                            i++;
                    if (x - 1 >= xMin)
                        if (tileHolder[x - 1][y] != 0)
                            i++;
                    if (x + 1 < xMax)
                        if (tileHolder[x + 1][y] != 0)
                            i++;
                    if (x - 1 >= xMin && y + 1 < yMax)
                        if (tileHolder[x - 1][y + 1] != 0)
                            i++;
                    if (y + 1 < yMax)
                        if (tileHolder[x][y + 1] != 0)
                            i++;
                    if (x + 1 < xMax && y + 1 < yMax)
                        if (tileHolder[x + 1][y + 1] != 0)
                            i++;

                    if (wall && i == 0)
                        tileHolder[x][y] = 0;
                }
            }


            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if (tileHolder[x][y] != 0)
                        tileID[x][y] = tileHolder[x][y];
                }
            }
        }

        private void GenerateCavesTest(int xMin, int xMax, int yMin, int yMax, ushort tile, int iterations, int density, bool replaceAir, bool reverse, bool preserveEdges)
        {
            byte i;
            bool wall;

            tileHolder = new ushort[tilesX][];

            for (int t = 0; t < tileHolder.Length; t++)
                tileHolder[t] = new ushort[tilesY];

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if (random.Next(0, 100) < density)
                        tileHolder[x][y] = tile;
                    else
                        tileHolder[x][y] = 0;
                }
            }

            for (int s = 0; s < iterations; s++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        i = 0;

                        if (tileHolder[x][y] == 0)
                            wall = false;
                        else
                            wall = true;


                        if ((x - 1 > 0 || y - 1 > 0 || x + 1 <= tileID.Length || y + 1 <= tileID[0].Length) && preserveEdges)
                        {
                            if (x - 1 >= xMin && y - 1 >= yMin)
                            {
                                if (tileHolder[x - 1][y - 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (y - 1 >= yMin)
                            {
                                if (tileHolder[x][y - 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (x + 1 < xMax && y - 1 >= yMin)
                            {
                                if (tileHolder[x + 1][y - 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (x - 1 >= xMin)
                            {
                                if (tileHolder[x - 1][y] != 0)
                                    i++;
                            }
                            else i++;
                            if (x + 1 < xMax)
                            {
                                if (tileHolder[x + 1][y] != 0)
                                    i++;
                            }
                            else i++;
                            if (x - 1 >= xMin && y + 1 < yMax)
                            {
                                if (tileHolder[x - 1][y + 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (y + 1 < yMax)
                            {
                                if (tileHolder[x][y + 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (x + 1 < xMax && y + 1 < yMax)
                            {
                                if (tileHolder[x + 1][y + 1] != 0)
                                    i++;
                            }
                            else i++;
                        }
                        else
                        {
                            if (x - 1 >= xMin && y - 1 >= yMin)
                                if (tileHolder[x - 1][y - 1] != 0)
                                    i++;
                            if (y - 1 >= yMin)
                                if (tileHolder[x][y - 1] != 0)
                                    i++;
                            if (x + 1 < xMax && y - 1 >= yMin)
                                if (tileHolder[x + 1][y - 1] != 0)
                                    i++;
                            if (x - 1 >= xMin)
                                if (tileHolder[x - 1][y] != 0)
                                    i++;
                            if (x + 1 < xMax)
                                if (tileHolder[x + 1][y] != 0)
                                    i++;
                            if (x - 1 >= xMin && y + 1 < yMax)
                                if (tileHolder[x - 1][y + 1] != 0)
                                    i++;
                            if (y + 1 < yMax)
                                if (tileHolder[x][y + 1] != 0)
                                    i++;
                            if (x + 1 < xMax && y + 1 < yMax)
                                if (tileHolder[x + 1][y + 1] != 0)
                                    i++;
                        }


                        if (reverse)
                        {
                            if ((wall & i >= 4) || (!wall & i >= 5))
                                tileHolder[x][y] = tile;
                            else
                                tileHolder[x][y] = 0;
                        }
                        else
                        {
                            if ((!wall & i >= 4) || (wall & i >= 5))
                                tileHolder[x][y] = tile;
                            else
                                tileHolder[x][y] = 0;
                        }

                    }
                }
            }

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    i = 0;

                    if (tileHolder[x][y] == 0)
                        wall = false;
                    else
                        wall = true;

                    if (x - 1 >= xMin && y - 1 >= yMin)
                        if (tileHolder[x - 1][y - 1] != 0)
                            i++;
                    if (y - 1 >= yMin)
                        if (tileHolder[x][y - 1] != 0)
                            i++;
                    if (x + 1 < xMax && y - 1 >= yMin)
                        if (tileHolder[x + 1][y - 1] != 0)
                            i++;
                    if (x - 1 >= xMin)
                        if (tileHolder[x - 1][y] != 0)
                            i++;
                    if (x + 1 < xMax)
                        if (tileHolder[x + 1][y] != 0)
                            i++;
                    if (x - 1 >= xMin && y + 1 < yMax)
                        if (tileHolder[x - 1][y + 1] != 0)
                            i++;
                    if (y + 1 < yMax)
                        if (tileHolder[x][y + 1] != 0)
                            i++;
                    if (x + 1 < xMax && y + 1 < yMax)
                        if (tileHolder[x + 1][y + 1] != 0)
                            i++;

                    if (wall && i == 0)
                        tileHolder[x][y] = 0;
                }
            }

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if (tileHolder[x][y] != 0 && replaceAir)
                        tileID[x][y] = tileHolder[x][y];
                    else if (tileHolder[x][y] != 0 && tileID[x][y] != 0 && !replaceAir)
                        tileID[x][y] = tileHolder[x][y];
                }
            }
        }

        private void GenerateAir(int xMin, int xMax, int yMin, int yMax, ushort tile, int iterations, int density, bool replaceAir, bool reverse, bool preserveEdges)
        {
            byte i;
            bool wall;

            tileHolder = new ushort[tilesX][];

            for (int t = 0; t < tileHolder.Length; t++)
                tileHolder[t] = new ushort[tilesY];

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if (random.Next(0, 100) < density)
                        tileHolder[x][y] = tile;
                    else
                        tileHolder[x][y] = 0;
                }
            }

            for (int s = 0; s < iterations; s++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        i = 0;

                        if (tileHolder[x][y] == 0)
                            wall = false;
                        else
                            wall = true;


                        if ((x - 1 > 0 || y - 1 > 0 || x + 1 <= tileID.Length || y + 1 <= tileID[0].Length) && preserveEdges)
                        {
                            if (x - 1 >= xMin && y - 1 >= yMin)
                            {
                                if (tileHolder[x - 1][y - 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (y - 1 >= yMin)
                            {
                                if (tileHolder[x][y - 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (x + 1 < xMax && y - 1 >= yMin)
                            {
                                if (tileHolder[x + 1][y - 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (x - 1 >= xMin)
                            {
                                if (tileHolder[x - 1][y] != 0)
                                    i++;
                            }
                            else i++;
                            if (x + 1 < xMax)
                            {
                                if (tileHolder[x + 1][y] != 0)
                                    i++;
                            }
                            else i++;
                            if (x - 1 >= xMin && y + 1 < yMax)
                            {
                                if (tileHolder[x - 1][y + 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (y + 1 < yMax)
                            {
                                if (tileHolder[x][y + 1] != 0)
                                    i++;
                            }
                            else i++;
                            if (x + 1 < xMax && y + 1 < yMax)
                            {
                                if (tileHolder[x + 1][y + 1] != 0)
                                    i++;
                            }
                            else i++;
                        }
                        else
                        {
                            if (x - 1 >= xMin && y - 1 >= yMin)
                                if (tileHolder[x - 1][y - 1] != 0)
                                    i++;
                            if (y - 1 >= yMin)
                                if (tileHolder[x][y - 1] != 0)
                                    i++;
                            if (x + 1 < xMax && y - 1 >= yMin)
                                if (tileHolder[x + 1][y - 1] != 0)
                                    i++;
                            if (x - 1 >= xMin)
                                if (tileHolder[x - 1][y] != 0)
                                    i++;
                            if (x + 1 < xMax)
                                if (tileHolder[x + 1][y] != 0)
                                    i++;
                            if (x - 1 >= xMin && y + 1 < yMax)
                                if (tileHolder[x - 1][y + 1] != 0)
                                    i++;
                            if (y + 1 < yMax)
                                if (tileHolder[x][y + 1] != 0)
                                    i++;
                            if (x + 1 < xMax && y + 1 < yMax)
                                if (tileHolder[x + 1][y + 1] != 0)
                                    i++;
                        }


                        if (reverse)
                        {
                            if ((wall & i >= 4) || (!wall & i >= 5))
                                tileHolder[x][y] = tile;
                            else
                                tileHolder[x][y] = 0;
                        }
                        else
                        {
                            if ((!wall & i >= 4) || (wall & i >= 5))
                                tileHolder[x][y] = tile;
                            else
                                tileHolder[x][y] = 0;
                        }

                    }
                }
            }

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    i = 0;

                    if (tileHolder[x][y] == 0)
                        wall = false;
                    else
                        wall = true;

                    if (x - 1 >= xMin && y - 1 >= yMin)
                        if (tileHolder[x - 1][y - 1] != 0)
                            i++;
                    if (y - 1 >= yMin)
                        if (tileHolder[x][y - 1] != 0)
                            i++;
                    if (x + 1 < xMax && y - 1 >= yMin)
                        if (tileHolder[x + 1][y - 1] != 0)
                            i++;
                    if (x - 1 >= xMin)
                        if (tileHolder[x - 1][y] != 0)
                            i++;
                    if (x + 1 < xMax)
                        if (tileHolder[x + 1][y] != 0)
                            i++;
                    if (x - 1 >= xMin && y + 1 < yMax)
                        if (tileHolder[x - 1][y + 1] != 0)
                            i++;
                    if (y + 1 < yMax)
                        if (tileHolder[x][y + 1] != 0)
                            i++;
                    if (x + 1 < xMax && y + 1 < yMax)
                        if (tileHolder[x + 1][y + 1] != 0)
                            i++;

                    if (wall && i == 0)
                        tileHolder[x][y] = 0;
                }
            }

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if (tileHolder[x][y] != 0 && replaceAir)
                        tileID[x][y] = 0;
                    else if (tileHolder[x][y] != 0 && tileID[x][y] != 0 && !replaceAir)
                        tileID[x][y] = 0;
                }
            }
        }
        
        private void GenerateRandoms(int xMin, int xMax, int yMin, int yMax, ushort tile)
        {
            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    if (random.Next(0, 2) == 0)
                        tileID[x][y] = tile;
                    else
                        tileID[x][y] = 0;
                }
            }
        }

        private void GenerateAir(int xMin, int xMax, int yMin, int yMax)
        {
            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    tileID[x][y] = 0;
                }
            }
        }

        private void GenerateSolid(int xMin, int xMax, int yMin, int yMax, ushort tile)
        {
            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    tileID[x][y] = tile;
                }
            }
        }

        private void Smooth(int xMin, int xMax, int yMin, int yMax, int iterations)
        {
            byte i;
            bool wall;

            for (int s = 0; s <= iterations; s++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        i = 0;

                        if (tileID[x][y] == 0)
                            wall = true;
                        else
                            wall = false;

                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            if (tileID[x - 1][y - 1] != 0)
                                i++;
                        }
                        else
                            continue;
                            

                        if (y - 1 >= 0)
                        {
                            if (tileID[x][y - 1] != 0)
                                i++;
                        }
                        else
                            continue;
                        if (x + 1 < xMax && y - 1 >= 0)
                        {
                            if (tileID[x + 1][y - 1] != 0)
                                i++;
                        }
                        else
                            continue;
                        if (x - 1 >= 0)
                        {
                            if (tileID[x - 1][y] != 0)
                                i++;
                        }
                        else
                            continue;
                        if (x + 1 < xMax)
                        {
                            if (tileID[x + 1][y] != 0)
                                i++;
                        }
                        else
                            continue;
                        if (x - 1 >= 0 && y + 1 < yMax)
                        {
                            if (tileID[x - 1][y + 1] != 0)
                                i++;
                        }
                        else
                            continue;
                        if (y + 1 < yMax)
                        {
                            if (tileID[x][y + 1] != 0)
                                i++;
                        }
                        else
                            continue;
                        if (x + 1 < xMax && y + 1 < yMax)
                        {
                            if (tileID[x + 1][y + 1] != 0)
                                i++;
                        }
                        else
                            continue;

                        if ((wall & i >= 4) || (!wall & i >= 5))
                            tileID[x][y] = 1;
                        else
                            tileID[x][y] = 0;
                    }
                }
            }
        }

        private void GenerateHills(int xMin, int xMax, int yMin, int yMax, ushort tile, int roughness)
        {
            int Pos = yMax - ((yMax - yMin) / 2); 
            bool Up = true;

            for (int x = xMin; x < xMax; x++)
            {
                if (random.Next(0, (int)(roughness * 1.5)) == 0)
                {
                    if (Up)
                        Up = false;
                    else
                        Up = true;                    
                }

                if (Up)
                    Pos += random.Next(0, roughness);
                else
                    Pos -= random.Next(0, roughness);

                if (Pos >= yMax)
                {
                    Pos -= (Pos - yMax) - random.Next(0, 5);
                    Up = false;
                }
                else if (Pos <= yMin)
                {
                    Pos += (yMin - Pos) - random.Next(0, 5);
                    Up = true;
                }
                
                for (int y = yMin; y < yMax; y++)
                {
                    if (y >= Pos)
                        tileID[x][y] = 1;
                }
            }
        }

        private void GenerateCaves(int xMin, int xMax, int yMin, int yMax, ushort tile)
        {
            byte i;
            bool wall;

            for (int s = 0; s <= 3; s++)
            {
                if (s == 0)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        for (int a = 0; a < ((xMax - xMin) * (yMax - yMin)) / 4; a++)
                        {
                            //Mutation
                            int x = random.Next(xMin, xMax);
                            int y = random.Next(yMin, yMax);

                            if (tileID[x][y] == tile)
                                tileID[x][y] = 0;
                            else
                                tileID[x][y] = tile;
                        }
                    }
                }

                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        i = 0;

                        if (tileID[x][y] == 0)
                            wall = true;
                        else
                            wall = false;

                        if (x - 1 >= 0 && y - 1 >= 0)
                            if (tileID[x - 1][y - 1] != 0)
                                i++;
                        if (y - 1 >= 0)
                            if (tileID[x][y - 1] != 0)
                                i++;
                        if (x + 1 < xMax && y - 1 >= 0)
                            if (tileID[x + 1][y - 1] != 0)
                                i++;
                        if (x - 1 >= 0)
                            if (tileID[x - 1][y] != 0)
                                i++;
                        if (x + 1 < xMax)
                            if (tileID[x + 1][y] != 0)
                                i++;
                        if (x - 1 >= 0 && y + 1 < yMax)
                            if (tileID[x - 1][y + 1] != 0)
                                i++;
                        if (y + 1 < yMax)
                            if (tileID[x][y + 1] != 0)
                                i++;
                        if (x + 1 < xMax && y + 1 < yMax)
                            if (tileID[x + 1][y + 1] != 0)
                                i++;

                        if ((wall & i >= 4) || (!wall & i >= 5))
                            tileID[x][y] = tile;
                        else
                            tileID[x][y] = 0;
                    }
                }
            }
        }

        private void GenerateStone(int xMin, int xMax, int yMin, int yMax, ushort tile)
        {
            byte i;
            bool wall;

            for (int s = 0; s <= 1; s++)
            {
                if (s == 0)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        for (int a = 0; a < ((xMax - xMin) * (yMax - yMin)) / 4; a++)
                        {
                            //Mutation
                            int x = random.Next(xMin, xMax);
                            int y = random.Next(yMin, yMax);

                            if (tileID[x][y] == tile)
                                tileID[x][y] = 0;
                            else
                                tileID[x][y] = tile;
                        }
                    }
                }

                for (int x = xMin; x < xMax; x++)
                {
                    for (int y = yMin; y < yMax; y++)
                    {
                        i = 0;

                        if (tileID[x][y] == 0)
                            wall = true;
                        else
                            wall = false;

                        if (x - 1 >= 0 && y - 1 >= 0)
                            if (tileID[x - 1][y - 1] != 0)
                                i++;
                        if (y - 1 >= 0)
                            if (tileID[x][y - 1] != 0)
                                i++;
                        if (x + 1 < xMax && y - 1 >= 0)
                            if (tileID[x + 1][y - 1] != 0)
                                i++;
                        if (x - 1 >= 0)
                            if (tileID[x - 1][y] != 0)
                                i++;
                        if (x + 1 < xMax)
                            if (tileID[x + 1][y] != 0)
                                i++;
                        if (x - 1 >= 0 && y + 1 < yMax)
                            if (tileID[x - 1][y + 1] != 0)
                                i++;
                        if (y + 1 < yMax)
                            if (tileID[x][y + 1] != 0)
                                i++;
                        if (x + 1 < xMax && y + 1 < yMax)
                            if (tileID[x + 1][y + 1] != 0)
                                i++;

                        if (((wall & i >= 4) || (!wall & i >= 5)) && tileID[x][y] == 1)
                            tileID[x][y] = 2;
                    }
                }
            }
        }

        public void LoadContent()
        {
            //tileTextures = TileData.Textures;
        }

        int t = 0;
        int p = 0;
        int t2 = 0;
        int Mcount = 0;
        int tilesEaten = 0;
        int ready = 0;

        public void Update(GameTime gameTime)
        {
            random = new Random();

            t2++;

            if (tilesEaten > (tilesX * tilesY) * 0.4 && ready <= 3)
            {
                tileHolder = new ushort[tilesX][];

                for (int r = 0; r < tileHolder.Length; r++)
                    tileHolder[r] = new ushort[tilesY];

                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        if (x > 1 && x < tileID.Length - 1 && y > 1 && y < tileID.Length - 1)
                        {
                            int i = 0;

                            for (int x2 = x - 1; x2 <= x + 1; x2++)
                            {
                                for (int y2 = y - 1; y2 <= y + 1; y2++)
                                {
                                    if (x == x2 && y == y2)
                                    {

                                    }
                                    else
                                        if (tileID[x2][y2] != 0)
                                            i++;
                                }
                            }

                            if (ready == 0)
                            {
                                if (i < 5)
                                    tileHolder[x][y] = 0;
                                else
                                    tileHolder[x][y] = 1;
                            }
                            else if (ready > 0)
                            {
                                if (i < 5)
                                    tileHolder[x][y] = 0;
                                else
                                    tileHolder[x][y] = 1;
                            }
                        }
                    }
                }

                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        if (tileHolder[x][y] == 0)
                            tileID[x][y] = 0;
                    }
                }

                ready++;
            }


            if (t2 == 600000)
            {
                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        int i2 = 0;
                        bool wall = true;

                        if (tileID[x][y] == 0)
                            wall = false;

                        if (x > 20 && x < tileID.Length - 20 && y > 20 && y < tileID.Length - 20)
                        {
                            if (wall)
                            {
                                if (tileID[x + 1][y] != 0)
                                    i2++;
                                if (tileID[x - 1][y] != 0)
                                    i2++;
                                if (tileID[x][y + 1] == 0)
                                    i2++;
                                if (tileID[x][y - 1] == 0)
                                    i2++;


                                if (i2 == 4)
                                    tileID[x][y] = 0;
                                else
                                {
                                    i2 = 0;

                                    if (tileID[x + 1][y] == 0)
                                        i2++;
                                    if (tileID[x - 1][y] == 0)
                                        i2++;
                                    if (tileID[x][y + 1] != 0)
                                        i2++;
                                    if (tileID[x][y - 1] != 0)
                                        i2++;


                                    if (i2 == 4)
                                        tileID[x][y] = 0;
                                    else
                                    {
                                        i2 = 0;
                                        for (int x2 = x - 1; x2 <= x + 1; x2++)
                                        {
                                            for (int y2 = y - 1; y2 <= y + 1; y2++)
                                            {
                                                if (x == x2 && y == y2)
                                                {
                                                }
                                                else if (tileID[x2][y2] != 0)
                                                    i2++;
                                            }
                                        }

                                        if (i2 == 1)
                                            tileID[x][y] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                t2 = 0;
            }

            if (p == 30000)
            {
                tileHolder = new ushort[tilesX][];

                for (int r = 0; r < tileHolder.Length; r++)
                    tileHolder[r] = new ushort[tilesY];

                tileHolder2 = new ushort[tilesX][];

                for (int r = 0; r < tileHolder2.Length; r++)
                    tileHolder2[r] = new ushort[tilesY];

                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        int i = 0;
                        bool wall = true;

                        if (tileID[x][y] == 0)
                            wall = false;

                        if (x > 2 && x < tileID.Length - 3 && y > 2 && y < tileID.Length - 3)
                        {
                            for (int x2 = x - 1; x2 <= x + 1; x2++)
                            {
                                for (int y2 = y - 1; y2 <= y + 1; y2++)
                                {
                                    if (x == x2 && y == y2)
                                    {
                                    }
                                    else if (tileID[x2][y2] != 0)
                                        i++;
                                }
                            }

                            if (tileID[x + 1][y] != 0)
                                i++;
                            if (tileID[x - 1][y] != 0)
                                i++;
                            if (tileID[x][y + 1] != 0)
                                i++;
                            if (tileID[x][y - 1] != 0)
                                i++;
                            
                            if (!wall && i == 8)
                                tileID[x][y] = 1;
                        }
                        
                        if (wall && i == 0 && tileID[x][y] != 6)
                        {
                            tileID[x][y] = 0;
                            if (y + 1 != tileID[x].Length)
                            {
                                tileHolder[x][y + 1] = 1;
                            }
                        }

                        if (x >= 2 && x <= tileID.Length - 3 && y >= 2 && y <= tileID.Length - 3)
                        {
                            for (int x2 = x - 2; x2 <= x; x2++)
                            {
                                for (int y2 = y - 2; y2 <= y; y2++)
                                {
                                    if (tileID[x2][y2] != 0)
                                        i++;
                                }
                            }
                        }

                        if (wall && i == 4 && tileID[x][y] != 6)
                        {
                            tileID[x][y] = 0;
                        }
                    }
                }

                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        if (tileHolder[x][y] != 0)
                            tileID[x][y] = tileHolder[x][y];
                    }
                }

                


                p = 0;
            }

            if (t == 5)
            {
                tileHolder = new ushort[tilesX][];

                for (int r = 0; r < tileHolder.Length; r++)
                    tileHolder[r] = new ushort[tilesY];

                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        if (tileID[x][y] == 6)
                        {
                            if (x <= 2 || x >= tileID.Length - 3 || y <= 2 || y >= tileID.Length - 3)
                            {
                                tileID[x][y] = 0;
                            }
                            else
                            {
                                bool searching = true;

                                while (searching)
                                {
                                    if (random.Next(0, 150) == 0 || tilesEaten > (tilesX * tilesY) * 0.4)
                                    {
                                        tileID[x][y] = 0;
                                        tileHolder[x][y] = 0;
                                        searching = false;
                                        break;
                                    }

                                    #region Direction

                                    int d = random.Next(0, 4);

                                    if (tileID[x][y - 1] != 0 && d == 0)
                                    {
                                        tileHolder[x][y - 1] = 6;
                                        if (tileID[x][y] != 0)
                                        {
                                            tileID[x][y] = 0;
                                            tilesEaten++;
                                        }

                                        if (random.Next(0, 100) < 4)
                                        {
                                            int d2 = random.Next(0, 4);

                                            if (d2 == 0)
                                                tileHolder[x][y - 2] = 6;
                                            if (d2 == 1)
                                                tileHolder[x + 1][y - 1] = 6;
                                            if (d2 == 2)
                                                tileHolder[x][y] = 6;
                                            if (d2 == 3)
                                                tileHolder[x - 1][y - 1] = 6;
                                        }

                                        searching = false;
                                    }
                                    else if (tileID[x + 1][y] != 0 && d == 1)
                                    {
                                        tileHolder[x + 1][y] = 6;
                                        if (tileID[x][y] != 0)
                                        {
                                            tileID[x][y] = 0;
                                            tilesEaten++;
                                        }

                                        if (random.Next(0, 100) < 4)
                                        {
                                            int d2 = random.Next(0, 4);

                                            if (d2 == 0)
                                                tileHolder[x + 1][y - 1] = 6;
                                            if (d2 == 1)
                                                tileHolder[x + 2][y] = 6;
                                            if (d2 == 2)
                                                tileHolder[x + 1][y + 1] = 6;
                                            if (d2 == 3)
                                                tileHolder[x][y] = 6;
                                        }

                                        searching = false;
                                    }
                                    else if (tileID[x][y + 1] != 0 && d == 2)
                                    {
                                        tileHolder[x][y + 1] = 6;
                                        if (tileID[x][y] != 0)
                                        {
                                            tileID[x][y] = 0;
                                            tilesEaten++;
                                        }

                                        if (random.Next(0, 100) < 4)
                                        {
                                            int d2 = random.Next(0, 4);

                                            if (d2 == 0)
                                                tileHolder[x][y] = 6;
                                            if (d2 == 1)
                                                tileHolder[x + 1][y + 1] = 6;
                                            if (d2 == 2)
                                                tileHolder[x][y + 2] = 6;
                                            if (d2 == 3)
                                                tileHolder[x - 1][y + 1] = 6;
                                        }

                                        searching = false;
                                    }
                                    else if (tileID[x - 1][y] != 0 && d == 3)
                                    {
                                        tileHolder[x - 1][y] = 6;
                                        if (tileID[x][y] != 0)
                                        {
                                            tileID[x][y] = 0;
                                            tilesEaten++;
                                        }

                                        if (random.Next(0, 100) < 4)
                                        {
                                            int d2 = random.Next(0, 4);

                                            if (d2 == 0)
                                                tileHolder[x - 1][y - 1] = 6;
                                            if (d2 == 1)
                                                tileHolder[x][y] = 6;
                                            if (d2 == 2)
                                                tileHolder[x - 1][y + 1] = 6;
                                            if (d2 == 3)
                                                tileHolder[x - 2][y] = 6;
                                        }

                                        searching = false;
                                    }
                                    else if (tileID[x - 1][y] == 0 && tileID[x][y - 1] == 0 && tileID[x + 1][y] == 0 && tileID[x][y + 1] == 0)
                                    {
                                        if (tileID[x][y] != 0)
                                        {
                                            tileID[x][y] = 0;
                                            tilesEaten++;
                                        }

                                        int d2 = random.Next(0, 4);

                                        if (d2 == 0)
                                            tileHolder[x][y - 1] = 6;
                                        if (d2 == 1)
                                            tileHolder[x + 1][y] = 6;
                                        if (d2 == 2)
                                            tileHolder[x][y + 1] = 6;
                                        if (d2 == 3)
                                            tileHolder[x - 1][y] = 6;

                                        searching = false;
                                    }

                                    #endregion

                                    if (random.Next(0, 15) == 0)
                                    {
                                        for (int x2 = x - 1; x2 <= x + 1; x2++)
                                        {
                                            for (int y2 = y - 1; y2 <= y + 1; y2++)
                                            {
                                                if (x == x2 && y == y2)
                                                {
                                                }
                                                else if (tileID[x2][y2] != 0 && tileID[x2][y2] != 6)
                                                {
                                                    tileID[x2][y2] = 0;
                                                    tilesEaten++;
                                                }
                                            }
                                        }

                                        if (tileID[x - 2][y] != 0 && tileID[x - 2][y] != 6 && random.Next(0, 2) == 0)
                                        {
                                            tileID[x - 2][y] = 0;
                                            tilesEaten++;
                                        }
                                        if (tileID[x][y + 2] != 0 && tileID[x][y + 2] != 6 && random.Next(0, 2) == 0)
                                        {
                                            tileID[x][y + 2] = 0;
                                            tilesEaten++;
                                        }
                                        if (tileID[x][y - 2] != 0 && tileID[x][y - 2] != 6 && random.Next(0, 2) == 0)
                                        {
                                            tileID[x][y - 2] = 0;
                                            tilesEaten++;
                                        }
                                        if (tileID[x + 2][y] != 0 && tileID[x + 2][y] != 6 && random.Next(0, 2) == 0)
                                        {
                                            tileID[x + 2][y] = 0;
                                            tilesEaten++;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                Mcount = 0;

                for (int x = 0; x < tilesX; x++)
                {
                    for (int y = 0; y < tilesY; y++)
                    {
                        if (tileHolder[x][y] == 6)
                        {
                            tileID[x][y] = tileHolder[x][y];
                            Mcount++;
                        }
                    }
                }


                t = 0;
            }

            //Console.WriteLine("Active Miners: " + Mcount);
            //Console.WriteLine("Tiles Eaten: " + tilesEaten);
            //Console.WriteLine("Total Tiles: " + (tilesX * tilesY));
            p++;
            t++;
        }

        public void Draw(SpriteBatch spriteBatch, int CameraNumber)
        {
            //WorldVariables.SetCameraSize(CameraNumber, chunkSize * tileSize);

            CameraPosition = CameraManager.CamerasRead[CameraNumber].PreviousFocus;
            CameraZoom = CameraManager.CamerasRead[CameraNumber].Zoom;

            RenderTileX = (short)((((CameraPosition.X - worldPosition.X) / tileSize) - (((WorldVariables.WindowWidth / 2) / tileSize) / CameraZoom)) - 1);
            RenderTileY = (short)((((CameraPosition.Y - worldPosition.Y) / tileSize) - (((WorldVariables.WindowHeight / 2) / tileSize) / CameraZoom)) - 1);
            RenderTileX2 = (short)((((CameraPosition.X - worldPosition.X) / tileSize) + (((WorldVariables.WindowWidth / 2) / tileSize) / CameraZoom)) + 1);
            RenderTileY2 = (short)((((CameraPosition.Y - worldPosition.Y) / tileSize) + (((WorldVariables.WindowHeight / 2) / tileSize) / CameraZoom)) + 1);

            //RenderTileX = (short)(RenderTileX * CameraZoom);
            //RenderTileY = (short)(RenderTileY * CameraZoom);
            //RenderTileX2 = (short)(RenderTileX2 / CameraZoom);
            //RenderTileY2 = (short)(RenderTileY2 / CameraZoom);

            {
                if (RenderTileX < 0)
                    RenderTileX = 0;
                else if (RenderTileX >= chunkSize * chunkX)
                    RenderTileX = (short)((chunkSize * chunkX) - 1);

                if (RenderTileX2 < 0)
                    RenderTileX2 = 0;
                else if (RenderTileX2 >= chunkSize * chunkX)
                    RenderTileX2 = (short)((chunkSize * chunkX) - 1);

                if (RenderTileY < 0)
                    RenderTileY = 0;
                else if (RenderTileY >= chunkSize * chunkY)
                    RenderTileY = (short)((chunkSize * chunkY) - 1);

                if (RenderTileY2 < 0)
                    RenderTileY2 = 0;
                else if (RenderTileY2 >= chunkSize * chunkY)
                    RenderTileY2 = (short)((chunkSize * chunkY) - 1);
            }

            
            for (int x = RenderTileX; x <= RenderTileX2; x++)
            {
                for (int y = RenderTileY; y <= RenderTileY2; y++)
                {
                    if (tileID[x][y] != 0)
                    {
                        position.X = worldPosition.X + (x * tileSize);
                        position.Y = worldPosition.Y + (y * tileSize);

                        if (tileID[x][y] == 1)
                            spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.SandyBrown);
                        else if (tileID[x][y] == 2)
                            spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.DimGray);
                        else if (tileID[x][y] == 3)
                            spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.Green);
                        else if (tileID[x][y] == 4)
                            spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.Blue);
                        else if (tileID[x][y] == 5)
                            spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.Red);
                        else if (tileID[x][y] == 6)
                            spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.Purple);
                    }
                }
            }
        }
    }
}
