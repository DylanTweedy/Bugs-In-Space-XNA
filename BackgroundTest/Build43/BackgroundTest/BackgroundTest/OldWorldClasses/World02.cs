using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class World2
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

            chunkX = 20;
            chunkY = 20;

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
                        
            WorldGeneration();
        }

        ushort[][] tileHolder;
        ushort[][] tileHolder2;

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
                //if (s == 0)
                //{
                //    if (random.Next(0, 2) == 0)
                //    {
                //        for (int a = 0; a < ((xMax - xMin) * (yMax - yMin)) / 4; a++)
                //        {
                //            //Mutation
                //            int x = random.Next(xMin, xMax);
                //            int y = random.Next(yMin, yMax);

                //            if (tileID[x][y] == tile)
                //                tileID[x][y] = 0;
                //            else
                //                tileID[x][y] = tile;
                //        }
                //    }
                //}

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

        public void Update(GameTime gameTime)
        {
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
                    }
                }
            }
        }
    }
}
