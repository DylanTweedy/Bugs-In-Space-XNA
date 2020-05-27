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
    class World4
    {
        Chunk[][] chunks;
        List<Miner> miners;
        
        byte tileSize;
        byte chunkTiles;

        byte chunkX;
        byte chunkY;









        bool[][] tileEmpty;
        Vector2 worldPosition;
        List<Texture2D> tileTextures;
        short tilesX;
        short tilesY;

        Random random;
        Random minerRandom;
        

        Vector2 position;

        Vector2 CameraPosition;
        float CameraZoom;

        int RenderTileX, RenderTileY, RenderTileX2, RenderTileY2;
        int RenderChunkX, RenderChunkY, RenderChunkX2, RenderChunkY2;


        private struct Tiles
        {
            public ushort BlockID;
            public ushort Furniture;
            //public ushort Background;
            //public ushort Wire;
            //public ushort Foreground;
        }

        private struct Chunk
        {
            public Tiles[][] tiles;
            public ushort TilesEaten;
            public ushort EdibleTiles;


            public void Initialize(ushort ChunkSize, ushort edibleTiles)
            {
                tiles = new Tiles[ChunkSize][];
                EdibleTiles = edibleTiles;
                TilesEaten = 0;

                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        tiles[x][y].BlockID = 1;
                        tiles[x][y].Furniture = 0;
                    }
                }   
            }


        }

        private class Miner
        {
            public int X, Y;
            public byte Speed;
            public ushort TilesEaten;


            public int Direction;
            public bool Active;

            Random rand;
            int airTime;
            int maxAirTime;

            public bool Spawn;
            public byte SpawnChance;

            public bool UpSolid;
            public bool DownSolid;
            public bool LeftSolid;
            public bool RightSolid;
            public bool InAir;

            public bool DestroyBlock;

            bool Waiting;
            public bool GetInfo;
            public byte State;

            public void Initialize(int x, int y, byte speed, Random random)
            {
                X = x;
                Y = y;
                Speed = speed;
                rand = random;
                Active = true;
                SpawnChance = 1;
                airTime = 0;
            }

            public void Update(GameTime gameTime)
            {
                if (TilesEaten == 100)
                    Active = false;

                if (!InAir)
                {
                    if (!Waiting)
                    {
                        DirectonChange();
                        DestroyBlock = true;
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (airTime > maxAirTime)
                    {
                        Direction = rand.Next(0, 4);
                        maxAirTime = rand.Next(0, 64);
                        airTime = 0;
                    }

                    switch (Direction)
                    {
                        case 0:
                            Y--;
                            break;

                        case 1:
                            X++;
                            break;

                        case 2:
                            Y++;
                            break;

                        case 3:
                            X--;
                            break;
                    }
                    
                    airTime++;
                }

            }

            public void UpdateCheck()
            {
            }


            private void DirectonChange()
            {
                bool Choosing = true;

                while (Choosing)
                {
                    Direction = rand.Next(0, 4);
                    int d = 0;

                    switch (Direction)
                    {
                        case 0:
                            if (UpSolid)
                                Choosing = false;
                            break;

                        case 1:
                            if (RightSolid)
                                Choosing = false;
                            break;

                        case 2:
                            if (DownSolid)
                                Choosing = false;
                            break;

                        case 3:
                            if (LeftSolid)
                                Choosing = false;
                            break;
                    }

                    if (d == 5)
                    {
                        if (UpSolid)
                            Direction = 0;
                        if (RightSolid)
                            Direction = 1;
                        if (DownSolid)
                            Direction = 2;
                        if (LeftSolid)
                            Direction = 3;

                        Choosing = false;
                    }
                    else
                        d++;
                }

                int SpawnNumber = rand.Next(0, 100);

                switch (Direction)
                {
                    case 0:
                        Y--;
                        if (SpawnNumber < SpawnChance)
                            Spawn = true;
                        break;

                    case 1:
                        X++;
                        if (SpawnNumber < SpawnChance)
                            Spawn = true;
                        break;

                    case 2:
                        Y++;
                        if (SpawnNumber < SpawnChance)
                            Spawn = true;
                        break;

                    case 3:
                        X--;
                        if (SpawnNumber < SpawnChance)
                            Spawn = true;
                        break;
                }
            }
        }

        public void Initialize()
        {
            tileSize = WorldVariables.TileSize;
            chunkTiles = WorldVariables.ChunkSize;

            random = new Random(100);
            minerRandom = new Random();
            worldPosition = Vector2.Zero;

            chunkX = 10;
            chunkY = 10;

            tilesX = (short)(chunkX * chunkTiles);
            tilesY = (short)(chunkY * chunkTiles);
            
            miners = new List<Miner>();

            chunks = new Chunk[chunkX][];

            int i = 0;

            for (int x = 0; x < chunks.Length; x++)
            {
                chunks[x] = new Chunk[chunkY];

                for (int y = 0; y < chunks[x].Length; y++)
                {
                    chunks[x][y].Initialize(chunkTiles, (ushort)((chunkTiles * chunkTiles) * 0.6f));

                    //Change this when adding generation code.

                    if (minerRandom.Next(0, 2) == 0)
                    {                        
                        miners.Add(new Miner());
                        miners[miners.Count - 1].Initialize((ushort)((chunkTiles / 2) + (x * chunkTiles)), (ushort)((chunkTiles / 2) + (y * chunkTiles)), 1, minerRandom);
                    }

                    i++;
                }
            }
        }

        public void LoadContent()
        {
            //tileTextures = TileData.Textures;
        }
        int test = 0;

        public void Update(GameTime gameTime)
        {
            if (test == 1)
            {
                for (int i = miners.Count - 1; i >= 0; i--)
                {
                    MinerCheck(i);
                    
                    miners[i].Update(gameTime);

                    if (miners[i].X < 0)
                    {
                    }

                    if (miners[i].DestroyBlock)
                    {
                        int CX = (int)Math.Floor(miners[i].X / (float)chunkTiles);
                        int CY = (int)Math.Floor(miners[i].Y / (float)chunkTiles);

                        if (CX >= chunkX)
                        {
                            CX = 0;
                            miners[i].X = 0;
                        }
                        if (CX < 0)
                        {
                            CX = chunkX - 1;
                            miners[i].X = (chunkX * chunkTiles) - 1;
                        }
                        if (CY >= chunkY)
                        {
                            CY = chunkY - 1;
                            miners[i].Y = (chunkY * chunkTiles) - 1;
                            miners[i].Active = false;
                        }
                        if (CY < 0)
                        {
                            CY = 0;
                            miners[i].Y = 0;
                        }

                        int TX = miners[i].X - (CX * chunkTiles);
                        int TY = miners[i].Y - (CY * chunkTiles);

                        if (chunks[CX][CY].TilesEaten < chunks[CX][CY].EdibleTiles)
                        {
                            miners[i].TilesEaten++;
                            chunks[CX][CY].TilesEaten++;
                            chunks[CX][CY].tiles[TX][TY].BlockID = 0;
                        }

                        miners[i].DestroyBlock = false;
                    }

                    if (miners[i].Spawn)
                    {
                        miners.Add(new Miner());
                        miners[miners.Count - 1].Initialize(miners[i].X, miners[i].Y, 3, minerRandom);
                        miners[i].Spawn = false;
                    }

                    if (!miners[i].Active)
                        miners.RemoveAt(i);
                }

                test = 0;
            }

            test++;
        }

        private void MinerCheck(int m)
        {
            int CX = (int)Math.Floor(miners[m].X / (float)chunkTiles);
            int CY = (int)Math.Floor(miners[m].Y / (float)chunkTiles);

            if (CX >= chunkX)
            {
                CX = 0;
                miners[m].X = 0;
            }
            if (CX < 0)
            {
                CX = chunkX - 1;
                miners[m].X = (chunkX * chunkTiles) - 1;
            }
            if (CY >= chunkY)
            {
                CY = chunkY - 1;
                miners[m].Y = (chunkY * chunkTiles) - 1;
                miners[m].Active = false;
            }
            if (CY < 0)
            {
                CY = 0;
                miners[m].Y = 0;
            }

            int TX = miners[m].X - (CX * chunkTiles);
            int TY = miners[m].Y - (CY * chunkTiles);


            if (chunks[CX][CY].TilesEaten < chunks[CX][CY].EdibleTiles)
            {
                bool Edge = false;

                if (TX <= 1 || TX >= chunkTiles - 2 || TY <= 1 || TY >= chunkTiles - 2)
                    Edge = true;

                miners[m].InAir = false;
                miners[m].UpSolid = false;
                miners[m].DownSolid = false;
                miners[m].LeftSolid = false;
                miners[m].RightSolid = false;

                if (Edge)
                {
                    CX = miners[m].X / chunkTiles;
                    CY = miners[m].Y / chunkTiles;
                    TX = miners[m].X - (CX * chunkTiles);
                    TY = miners[m].Y - (CY * chunkTiles) - 1;

                    if (TY < 0)
                    {
                        TY += chunkTiles;
                        CY--;

                        if (CY < 0)
                            CY = 0;
                    }



                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        miners[m].UpSolid = true;

                    CX = miners[m].X / chunkTiles;
                    CY = miners[m].Y / chunkTiles;
                    TX = miners[m].X - (CX * chunkTiles) + 1;
                    TY = miners[m].Y - (CY * chunkTiles);

                    if (TX >= chunkTiles)
                    {
                        TX -= chunkTiles;
                        CX++;

                        if (CX >= chunkX)
                            CX = 0;
                    }

                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        miners[m].RightSolid = true;

                    CX = miners[m].X / chunkTiles;
                    CY = miners[m].Y / chunkTiles;
                    TX = miners[m].X - (CX * chunkTiles);
                    TY = miners[m].Y - (CY * chunkTiles) + 1;

                    if (TY >= chunkTiles)
                    {
                        TY -= chunkTiles;
                        CY++;

                        if (CY >= chunkY)
                            CY = chunkY - 1;
                    }

                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        miners[m].DownSolid = true;

                    CX = miners[m].X / chunkTiles;
                    CY = miners[m].Y / chunkTiles;
                    TX = miners[m].X - (CX * chunkTiles) - 1;
                    TY = miners[m].Y - (CY * chunkTiles);

                    if (TX < 0)
                    {
                        TX += chunkTiles;
                        CX--;

                        if (CX < 0)
                            CX = chunkX - 1;
                    }

                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        miners[m].LeftSolid = true;
                }
                else
                {
                    if (chunks[CX][CY].tiles[TX][TY - 1].BlockID != 0)
                        miners[m].UpSolid = true;

                    if (chunks[CX][CY].tiles[TX + 1][TY].BlockID != 0)
                        miners[m].RightSolid = true;

                    if (chunks[CX][CY].tiles[TX][TY + 1].BlockID != 0)
                        miners[m].DownSolid = true;

                    if (chunks[CX][CY].tiles[TX - 1][TY].BlockID != 0)
                        miners[m].LeftSolid = true;
                }

                if (!miners[m].UpSolid && !miners[m].RightSolid && !miners[m].DownSolid && !miners[m].LeftSolid)
                    miners[m].InAir = true;
            }
            else
                miners[m].InAir = true;
        }

        private void GetTile(int x, int y, bool absaloute)
        {
        }


        public void Draw(SpriteBatch spriteBatch, int CameraNumber)
        {
            //Get Draw Code to wrap.

            //WorldVariables.SetCameraSize(CameraNumber, chunkTiles * tileSize);

            CameraPosition = CameraManager.CamerasRead[CameraNumber].PreviousFocus;
            CameraZoom = CameraManager.CamerasRead[CameraNumber].Zoom;

            RenderTileX = (int)((((CameraPosition.X - worldPosition.X) / tileSize) - (((WorldVariables.WindowWidth / 2) / tileSize) / CameraZoom)) - 1);
            RenderTileY = (int)((((CameraPosition.Y - worldPosition.Y) / tileSize) - (((WorldVariables.WindowHeight / 2) / tileSize) / CameraZoom)) - 3);
            RenderTileX2 = (int)((((CameraPosition.X - worldPosition.X) / tileSize) + (((WorldVariables.WindowWidth / 2) / tileSize) / CameraZoom)) + 1);
            RenderTileY2 = (int)((((CameraPosition.Y - worldPosition.Y) / tileSize) + (((WorldVariables.WindowHeight / 2) / tileSize) / CameraZoom)) + 3);

            RenderChunkX = (RenderTileX / chunkTiles);
            RenderChunkY = (RenderTileY / chunkTiles);
            RenderChunkX2 = (RenderTileX2 / chunkTiles);
            RenderChunkY2 = (RenderTileY2 / chunkTiles);





            {
                if (RenderChunkX < 0)
                    RenderChunkX = 0;
                else if (RenderChunkX >= chunkX)
                    RenderChunkX = (short)(chunkX - 1);

                if (RenderChunkX2 < 0)
                    RenderChunkX2 = 0;
                else if (RenderChunkX2 >= chunkX)
                    RenderChunkX2 = (short)(chunkX - 1);

                if (RenderChunkY < 0)
                    RenderChunkY = 0;
                else if (RenderChunkY >= chunkY)
                    RenderChunkY = (short)(chunkY - 1);

                if (RenderChunkY2 < 0)
                    RenderChunkY2 = 0;
                else if (RenderChunkY2 >= chunkY)
                    RenderChunkY2 = (short)(chunkY - 1);
            }

            for (int x = RenderChunkX; x <= RenderChunkX2; x++)
            {
                for (int y = RenderChunkY; y <= RenderChunkY2; y++)
                {
                    for (int x2 = 0; x2 < chunkTiles; x2++)
                    {
                        for (int y2 = 0; y2 < chunkTiles; y2++)
                        {
                            if ((x * chunkTiles) + x2 > RenderTileX && (x * chunkTiles) + x2 < RenderTileX2 &&
                                (y * chunkTiles) + y2 > RenderTileY && (y * chunkTiles) + y2 < RenderTileY2)
                            {
                                position.X = worldPosition.X + (x2 * tileSize) + (x * (chunkTiles * tileSize));
                                position.Y = worldPosition.Y + (y2 * tileSize) + (y * (chunkTiles * tileSize));

                                if (chunks[x][y].tiles[x2][y2].BlockID == 1)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.SandyBrown);
                                else if (chunks[x][y].tiles[x2][y2].BlockID == 2)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.DimGray);
                                else if (chunks[x][y].tiles[x2][y2].BlockID == 3)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.Green);
                                else if (chunks[x][y].tiles[x2][y2].BlockID == 4)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.Blue);
                                else if (chunks[x][y].tiles[x2][y2].BlockID == 5)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.Red);
                                else if (chunks[x][y].tiles[x2][y2].BlockID == 6)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.Purple);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < miners.Count; i++)
            {
                position.X = worldPosition.X + (miners[i].X * tileSize);
                position.Y = worldPosition.Y + (miners[i].Y * tileSize);

                spriteBatch.Draw(tileTextures[6], position, Color.Purple);
            }
        }
    }
}
