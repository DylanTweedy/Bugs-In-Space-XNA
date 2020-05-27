using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SimplexNoise;
using Bitmap = System.Drawing.Bitmap;
using BMPColor = System.Drawing.Color;


namespace BackgroundTest
{
    class World6
    {      
        Chunk[][] chunks;
        List<Miner> miners;

        Texture2D minerTexture;

        byte tileSize;
        byte chunkTiles;

        byte chunkX;
        byte chunkY;

        int minerMinSpeed;
        int minerMaxSize;

        int CX;
        int CY;
        int TX;
        int TY;

        int selectedActiveBlock;

        ushort[][] chunkHolder;

        float caveSize;
        int minerChance;


        Vector2 Offset;
        Vector2 worldPosition;
        List<Texture2D> tileTextures;
        List<ActiveBlocks> activeBlocks;
        ushort tilesX;
        ushort tilesY;

        Random random;
        Random minerRandom;
        

        Vector2 position;

        Vector2 CameraPosition;
        float CameraZoom;

        int RenderTileX, RenderTileY, RenderTileX2, RenderTileY2;
        int RenderChunkX, RenderChunkY, RenderChunkX2, RenderChunkY2;

        private class ActiveBlocks
        {
            public byte CX;
            public byte CY;
            public ushort TX;
            public ushort TY;           
            public int BlockHealth = 0;
            public int BlockMaxHealth;
            public int PreviousHealth;
            public bool BlockFalling;
            public bool BlockHealing;
            public bool BlockDecaying;
            public ushort BlockID;
            public ushort ChangeBlock;

            public void Initialize(byte cx, byte cy, ushort tx, ushort ty, ushort blockID)
            {
                CX = cx;
                CY = cy;
                TX = tx;
                TY = ty;
                BlockID = blockID;

                if (BlockHealth == 0)
                {
                    BlockHealth = TileData.BlockHealth[BlockID];
                    BlockMaxHealth = BlockHealth;
                    PreviousHealth = BlockHealth;
                    ChangeBlock = 0;
                }
            }
        }

        private struct Tiles
        {
            public ushort BlockID;
            //public ushort Furniture;
            //public ushort Background;
            //public ushort Wire;
            //public ushort Foreground;
        }

        private struct Chunk
        {
            public Tiles[][] tiles;
            public ushort EdibleTiles;
            public ushort TilesEaten;
            public ushort Miners;
            public bool UpdateChunk;
            public ushort ActiveBlocks;
            public byte Phase;


            public void Initialize(ushort ChunkSize, ushort edibleTiles, bool testLand)
            {
                tiles = new Tiles[ChunkSize][];
                EdibleTiles = edibleTiles;
                Miners = 0;
                UpdateChunk = false;
                ActiveBlocks = 0;
                Phase = 1;

                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        if (testLand)
                        tiles[x][y].BlockID = 2;
                    }
                }   
            }
        }

        private class Miner
        {
            public int X, Y;
            public int TilesEaten, MaxTiles;


            public int Direction;
            public bool Active;

            Random rand;
            int airTime;
            int maxAirTime;
            float originalSpeed;
            float speed;
            int tileSize;

            public bool Spawn;
            public byte SpawnChance;

            public bool UpSolid;
            public bool DownSolid;
            public bool LeftSolid;
            public bool RightSolid;
            public bool InAir;

            public bool DestroyBlock;

            public int SChunkX;
            public int SChunkY;

            public bool Moving;
            public Vector2 Position;

            public int Size;
            bool sizeChange;


            public void Initialize(int x, int y, float Speed, int TileSize, int maxTiles, int sChunkX, int sChunkY, int size, Random random)
            {
                X = x;
                Y = y;
                originalSpeed = Speed;
                rand = random;
                Active = true;
                SpawnChance = 1;
                airTime = 0;
                MaxTiles = maxTiles;
                SChunkX = sChunkX;
                SChunkY = sChunkY;
                Moving = false;
                tileSize = TileSize;
                Size = size;
                sizeChange = false;
                speed = originalSpeed - Size;

                Position = new Vector2(X * tileSize, Y * tileSize);
            }

            public void Update(GameTime gameTime)
            {
                if (!InAir)
                {
                    if (!Moving)
                    {
                        if (TilesEaten >= MaxTiles || rand.Next(0, 200) == 0)
                            Active = false;
                        else if (TilesEaten >= MaxTiles / 2 && !sizeChange)
                        {
                            Size = rand.Next(1, 4);
                            sizeChange = true;
                            speed = originalSpeed - Size;
                        }
                        
                        DirectonChange();

                        DestroyBlock = true;
                    }
                    else
                        Move();

                }
                else
                {
                    if (!Moving)
                        AirMovement();
                    else
                        Move();
                }
            }

            private void Move()
            {
                switch (Direction)
                {
                    case 0:
                        if (InAir)
                            Position.Y -= 1.5f * speed;
                        else
                            Position.Y -= 1f * speed;

                        if (Position.Y <= Y * tileSize)
                        {
                            Position.Y = Y * tileSize;
                            Moving = false;
                        }

                        break;


                    case 1:
                        if (InAir)
                            Position.X += 1.5f * speed;
                        else
                            Position.X += 1f * speed;

                        if (Position.X >= X * tileSize)
                        {
                            Position.X = X * tileSize;
                            Moving = false;
                        }

                        break;


                    case 2:
                        if (InAir)
                            Position.Y += 1.5f * speed;
                        else
                            Position.Y += 1f * speed;

                        if (Position.Y >= Y * tileSize)
                        {
                            Position.Y = Y * tileSize;
                            Moving = false;
                        }

                        break;

                    case 3:
                        if (InAir)
                            Position.X -= 1.5f * speed;
                        else
                            Position.X -= 1f * speed;

                        if (Position.X <= X * tileSize)
                        {
                            Position.X = X * tileSize;
                            Moving = false;
                        }

                        break;
                }
            }

            private void AirMovement()
            {
                if (rand.Next(0, 500) == 0)
                    Active = false;

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

                Moving = true;

                airTime++;
            }

            private void DirectonChange()
            {
                bool Choosing = true;
                int d = 0;

                while (Choosing)
                {
                    Direction = rand.Next(0, 4);

                    if (TilesEaten > MaxTiles * 0.8 && Direction == 0)
                        Direction = rand.Next(1, 4);

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
                        else if (RightSolid)
                            Direction = 1;
                        else if (DownSolid)
                            Direction = 2;
                        else if (LeftSolid)
                            Direction = 3;
                        else
                            Direction = 2;

                        Choosing = false;
                    }
                    else
                        d++;
                }

                int SpawnNumber = rand.Next(0, 100 / Size);

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

                Moving = true;
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

            tilesX = (ushort)(chunkX * chunkTiles);
            tilesY = (ushort)(chunkY * chunkTiles);
            
            miners = new List<Miner>();
            minerMinSpeed = 10000;
            minerMaxSize = 4;

            caveSize = 0.7f;
            minerChance = 80;

            activeBlocks = new List<ActiveBlocks>();
            
            chunkHolder = new ushort[chunkTiles][];

            for (int a = 0; a < chunkHolder.Length; a++)
                chunkHolder[a] = new ushort[chunkTiles];
            
            chunks = new Chunk[chunkX][];


            int i = 0;

            for (int x = 0; x < chunks.Length; x++)
            {
                chunks[x] = new Chunk[chunkY];

                for (int y = 0; y < chunks[x].Length; y++)
                {
                    bool testLand = false;

                    if (y >= 3)
                        testLand = true;

                    chunks[x][y].Initialize(chunkTiles, (ushort)((chunkTiles * chunkTiles) * caveSize), testLand);

                    //Change this when adding generation code.

                    if (minerRandom.Next(0, 100) < minerChance && testLand)
                    {
                        miners.Add(new Miner());
                        miners[miners.Count - 1].Initialize(
                            (ushort)(minerRandom.Next(0, chunkTiles) + (x * chunkTiles)),
                            (ushort)(minerRandom.Next(0, chunkTiles) + (y * chunkTiles)),
                            random.Next(minerMinSpeed, tileSize * 1000) / 1000f,
                            tileSize,
                            minerRandom.Next(150, 250),
                            x,
                            y,
                            minerRandom.Next(1, minerMaxSize + 1),
                            minerRandom);
                        chunks[x][y].Miners++;                        
                    }

                    i++;
                }
            }
        }

        public void LoadContent(GraphicsDeviceManager Graphics, ContentManager Content)
        {
            //tileTextures = TileData.Textures;
            LoadParticles(Graphics, Content);

            minerTexture = Content.Load<Texture2D>("Images//Miner");

            Offset = new Vector2(tileTextures[6].Width / 2, tileTextures[6].Height / 2);
        }

        public void LoadParticles(GraphicsDeviceManager Graphics, ContentManager Content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            //Console.WriteLine("Miners: " + miners.Count);

            UpdateMiners(gameTime);
            UpdateChunks();
        }



        public void UpdateMiners(GameTime gameTime)
        {
            //for (int i = miners.Count - 1; i >= 0; i--)
            for (int i = 0; i < miners.Count; i++)
            {
                if (i < 1000)
                {
                    if (!miners[i].Moving)
                    {
                        MinerCheck(i);

                        if (chunks[miners[i].SChunkX][miners[i].SChunkY].Miners == 1)
                            miners[i].SpawnChance = 2;
                        else
                            miners[i].SpawnChance = 1;
                                               
                        miners[i].Update(gameTime);

                        if (miners[i].DestroyBlock)
                        {
                            miners[i].TilesEaten++;
                            chunks[miners[i].SChunkX][miners[i].SChunkY].TilesEaten++;

                            GetTile(miners[i].X, miners[i].Y, i);
                            RemoveBlock(CX, CY, TX, TY);

                            miners[i].DestroyBlock = false;

                            if (miners[i].Size > 1)
                            {
                                for (int x = -miners[i].Size + 1; x <= miners[i].Size - 1; x++)
                                {
                                    for (int y = -miners[i].Size + 1; y <= miners[i].Size - 1; y++)
                                    {
                                        GetTile(miners[i].X + x, miners[i].Y + y);

                                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        {
                                            RemoveBlock(CX, CY, TX, TY);
                                            miners[i].TilesEaten++;
                                            chunks[miners[i].SChunkX][miners[i].SChunkY].TilesEaten++;
                                        }
                                    }
                                }
                            }
                        }

                        if (chunks[miners[i].SChunkX][miners[i].SChunkY].Miners == 1)
                            miners[i].Active = true;

                        if (chunks[miners[i].SChunkX][miners[i].SChunkY].TilesEaten > chunks[miners[i].SChunkX][miners[i].SChunkY].EdibleTiles)
                        {
                            chunks[miners[i].SChunkX][miners[i].SChunkY].EdibleTiles += (ushort)(chunks[miners[i].SChunkX][miners[i].SChunkY].Miners * 2); ;
                            miners[i].Active = false;
                        }

                        if (miners[i].Spawn)
                        {
                            miners.Add(new Miner());
                            miners[miners.Count - 1].Initialize(
                                miners[i].X,
                                miners[i].Y,
                                random.Next(minerMinSpeed, tileSize * 1000) / 1000f,
                                tileSize,
                                minerRandom.Next(100, 250),
                                miners[i].SChunkX,
                                miners[i].SChunkY,
                                minerRandom.Next(1, minerMaxSize + 1),
                                minerRandom);
                            chunks[miners[i].SChunkX][miners[i].SChunkY].Miners++;
                            miners[i].Spawn = false;
                        }

                        if (!miners[i].Active)
                        {
                            chunks[miners[i].SChunkX][miners[i].SChunkY].Miners--;
                            miners.RemoveAt(i);
                        }
                    }
                    else
                        miners[i].Update(gameTime);
                }
                //change to if miner on-screen
                //else if (i < 1000)
                //{
                //    if (miners[i].Y + miners[i].Size < chunkY * chunkTiles)
                //    {
                //        GetTile(miners[i].X, miners[i].Y + miners[i].Size);
                //        if (chunks[CX][CY].tiles[TX][TY].BlockID == 0)
                //            miners[i].Y++;
                //    }
                //    else
                //        miners[i].Active = false;

                //    if (!miners[i].Active)
                //        miners.RemoveAt(i);
                //}
            }
        }

        private void MinerCheck(int i)
        {
            GetTile(miners[i].X, miners[i].Y, i);

            if (CX <= miners[i].SChunkX - 2 || CX >= miners[i].SChunkX + 2 || CY <= miners[i].SChunkY - 2 || CY >= miners[i].SChunkY + 2 || chunks[miners[i].SChunkX][miners[i].SChunkY].TilesEaten > chunks[miners[i].SChunkX][miners[i].SChunkY].EdibleTiles)
            {
                if (miners[i].SChunkX - 2 < 0 && CX > chunkX - 2)
                {
                }
                else if (miners[i].SChunkX + 2 >= chunkX && CX < 1)
                {
                }
                else if (chunks[CX][CY].Miners <= 1)
                {
                }
                else
                    miners[i].Active = false;
            }

            if (miners[i].Active)
            {
                GetTile(miners[i].X, miners[i].Y - miners[i].Size);
                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                    miners[i].UpSolid = true;
                else
                    miners[i].UpSolid = false;

                GetTile(miners[i].X + miners[i].Size, miners[i].Y);
                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                    miners[i].RightSolid = true;
                else
                    miners[i].RightSolid = false;

                GetTile(miners[i].X, miners[i].Y + miners[i].Size);
                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                    miners[i].DownSolid = true;
                else
                    miners[i].DownSolid = false;

                GetTile(miners[i].X - miners[i].Size, miners[i].Y);
                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                    miners[i].LeftSolid = true;
                else
                    miners[i].LeftSolid = false;

                if (!miners[i].UpSolid && !miners[i].RightSolid && !miners[i].DownSolid && !miners[i].LeftSolid)
                    miners[i].InAir = true;
                else
                    miners[i].InAir = false;  
            }
        }

        private void Smooth2(int cx, int cy)
        {
            for (int cx2 = cx - 1; cx2 <= cx + 1; cx2++)
            {
                for (int cy2 = cy - 1; cy2 <= cy + 1; cy2++)
                {
                    for (int x2 = 0; x2 < chunkHolder.Length; x2++)
                    {
                        for (int y2 = 0; y2 < chunkHolder[x2].Length; y2++)
                        {
                            chunkHolder[x2][y2] = 1;
                        }
                    }

                    GetTile(cx2, cy2, 0, 0);

                    int cx3 = CX;
                    int cy3 = CY;

                    for (int x2 = 0; x2 < chunks[cx3][cy3].tiles.Length; x2++)
                    {
                        for (int y2 = 0; y2 < chunks[cx3][cy3].tiles[x2].Length; y2++)
                        {
                            int i = 0;

                            for (int x3 = x2 - 1; x3 <= x2 + 1; x3++)
                            {
                                for (int y3 = y2 - 1; y3 <= y2 + 1; y3++)
                                {
                                    if (x2 == x3 && y2 == y3)
                                    {
                                    }
                                    else
                                    {
                                        GetTile(cx3, cy3, x3, y3);

                                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                            i++;
                                    }
                                }
                            }

                            if (i < 5 && cx2 == cx)
                                chunkHolder[x2][y2] = 0;
                            else if (i < 4)
                                chunkHolder[x2][y2] = 0;
                            else
                                chunkHolder[x2][y2] = 1;

                            ///////////////////////////////////////////////////
                            if (chunks[cx3][cy3].tiles[x2][y2].BlockID == 0)
                            {
                                GetTile(cx3, cy3, x2, y2 + 1);

                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                {
                                    int ACX = CX;
                                    int ACY = CY;
                                    int ATX = TX;
                                    int ATY = TY;

                                    int control = random.Next(0, 100);
                                    int depth = 0;

                                    if (control >= 90)
                                        depth = 3;
                                    else if (control >= 40)
                                        depth = 2;
                                    else if (control >= 0)
                                        depth = 1;

                                    for (int d = 0; d < depth; d++)
                                    {
                                        //This one could be grass.
                                        if (d >= 1)
                                            GetTile(ACX, ACY, ATX, ATY + d);

                                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        {
                                            if (!ActiveBlockExists(CX, CY, TX, TY))
                                            {
                                                //chunks[CX][CY].tiles[TX][TY].BlockID = 1;
                                                activeBlocks.Add(new ActiveBlocks());
                                                chunks[CX][CY].ActiveBlocks++;

                                                activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, chunks[CX][CY].tiles[TX][TY].BlockID);

                                                if (d == 0)
                                                    activeBlocks[activeBlocks.Count - 1].ChangeBlock = 3;
                                                else
                                                    activeBlocks[activeBlocks.Count - 1].ChangeBlock = 1;

                                                chunks[CX][CY].UpdateChunk = true;
                                            }
                                            else if (activeBlocks[selectedActiveBlock].ChangeBlock == 0)
                                            {
                                                //chunks[CX][CY].tiles[TX][TY].BlockID = 1;

                                                //CopyBlockInfo(selectedActiveBlock);

                                                if (d == 0)
                                                    activeBlocks[selectedActiveBlock].ChangeBlock = 3;
                                                else
                                                    activeBlocks[selectedActiveBlock].ChangeBlock = 1;

                                                chunks[CX][CY].UpdateChunk = true;
                                            }
                                        }
                                    }
                                }
                            }
                            ///////////////////////////////
                        }
                    }

                    for (int x2 = 0; x2 < chunkHolder.Length; x2++)
                    {
                        for (int y2 = 0; y2 < chunkHolder[x2].Length; y2++)
                        {
                            if (chunkHolder[x2][y2] == 0)
                            {
                                if (chunks[cx3][cy3].tiles[x2][y2].BlockID != 0)
                                {
                                    if (!ActiveBlockExists(cx3, cy3, x2, y2))
                                    {
                                        activeBlocks.Add(new ActiveBlocks());
                                        chunks[cx3][cy3].ActiveBlocks++;
                                        activeBlocks[activeBlocks.Count - 1].Initialize((byte)cx3, (byte)cy3, (ushort)x2, (ushort)y2, chunks[cx3][cy3].tiles[x2][y2].BlockID);
                                        activeBlocks[activeBlocks.Count - 1].BlockHealth = (activeBlocks[activeBlocks.Count - 1].BlockMaxHealth / 3) - 1;

                                        chunks[cx3][cy3].UpdateChunk = true;
                                    }
                                    else if (activeBlocks[selectedActiveBlock].BlockHealth > activeBlocks[selectedActiveBlock].BlockMaxHealth / 3)
                                    {
                                        activeBlocks[selectedActiveBlock].BlockHealth = (activeBlocks[selectedActiveBlock].BlockMaxHealth / 3) - 1;
                                        chunks[cx3][cy3].UpdateChunk = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            chunks[cx][cy].Phase++;
        }

        private void Smooth(int cx, int cy)
        {
            int cx2 = cx;
            int cy2 = cy;

            switch (chunks[cx][cy].Phase)
            {
                case 2:
                    break;

                case 3:
                    cx2--;
                    cy2--;
                    break;

                case 4:
                    cy2--;
                    break;

                case 5:
                    cx2++;
                    cy2--;
                    break;

                case 6:
                    cx2--;
                    break;

                case 7:
                    cx2++;
                    break;

                case 8:
                    cx2--;
                    cy2++;
                    break;

                case 9:
                    cy2++;
                    break;

                case 10:
                    cx2++;
                    cy2++;
                    break;
            }

            GetTile(cx2, cy2, 0, 0);

            int cx3 = CX;
            int cy3 = CY;

            for (int x2 = 0; x2 < chunks[cx3][cy3].tiles.Length; x2++)
            {
                for (int y2 = 0; y2 < chunks[cx3][cy3].tiles[x2].Length; y2++)
                {
                    int i = 0;

                    for (int x3 = x2 - 1; x3 <= x2 + 1; x3++)
                    {
                        for (int y3 = y2 - 1; y3 <= y2 + 1; y3++)
                        {
                            if (x2 == x3 && y2 == y3)
                            {
                            }
                            else
                            {
                                GetTile(cx3, cy3, x3, y3);

                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                    i++;
                            }
                        }
                    }

                    if (i < 5 && cx2 == cx)
                        chunkHolder[x2][y2] = 0;
                    else if (i < 4)
                        chunkHolder[x2][y2] = 0;
                    else
                        chunkHolder[x2][y2] = 1;

                    ///////////////////////////////////////////////////
                    if (chunks[cx3][cy3].tiles[x2][y2].BlockID == 0)
                    {
                        GetTile(cx3, cy3, x2, y2 + 1);

                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        {
                            int ACX = CX;
                            int ACY = CY;
                            int ATX = TX;
                            int ATY = TY;

                            int control = random.Next(0, 100);
                            int depth = 0;

                            if (control >= 95)
                                depth = 6;
                            else if (control >= 80)
                                depth = 5;
                            else if (control >= 60)
                                depth = 4;
                            else if (control >= 40)
                                depth = 3;
                            else if (control >= 20)
                                depth = 2;
                            else if (control >= 0)
                                depth = 1;

                            for (int d = 0; d < depth; d++)
                            {
                                //This one could be grass.
                                if (d >= 1)
                                    GetTile(ACX, ACY, ATX, ATY + d);

                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                {
                                    if (!ActiveBlockExists(CX, CY, TX, TY))
                                    {
                                        //chunks[CX][CY].tiles[TX][TY].BlockID = 1;
                                        activeBlocks.Add(new ActiveBlocks());
                                        chunks[CX][CY].ActiveBlocks++;

                                        activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, chunks[CX][CY].tiles[TX][TY].BlockID);

                                        if (d == 0)
                                            activeBlocks[activeBlocks.Count - 1].ChangeBlock = 3;
                                        else
                                            activeBlocks[activeBlocks.Count - 1].ChangeBlock = 1;

                                        chunks[CX][CY].UpdateChunk = true;
                                    }
                                    else if (activeBlocks[selectedActiveBlock].ChangeBlock == 0)
                                    {
                                        //chunks[CX][CY].tiles[TX][TY].BlockID = 1;

                                        //CopyBlockInfo(selectedActiveBlock);

                                        if (d == 0)
                                            activeBlocks[selectedActiveBlock].ChangeBlock = 3;
                                        else
                                            activeBlocks[selectedActiveBlock].ChangeBlock = 1;

                                        chunks[CX][CY].UpdateChunk = true;
                                    }
                                }
                            }
                        }
                    }
                    ///////////////////////////////
                }
            }

            for (int x2 = 0; x2 < chunkHolder.Length; x2++)
            {
                for (int y2 = 0; y2 < chunkHolder[x2].Length; y2++)
                {
                    if (chunkHolder[x2][y2] == 0)
                    {
                        if (chunks[cx3][cy3].tiles[x2][y2].BlockID != 0)
                        {
                            if (!ActiveBlockExists(cx3, cy3, x2, y2))
                            {
                                activeBlocks.Add(new ActiveBlocks());
                                chunks[cx3][cy3].ActiveBlocks++;
                                activeBlocks[activeBlocks.Count - 1].Initialize((byte)cx3, (byte)cy3, (ushort)x2, (ushort)y2, chunks[cx3][cy3].tiles[x2][y2].BlockID);
                                activeBlocks[activeBlocks.Count - 1].BlockHealth = (activeBlocks[activeBlocks.Count - 1].BlockMaxHealth / 3) - 1;

                                chunks[cx3][cy3].UpdateChunk = true;
                            }
                            else if (activeBlocks[selectedActiveBlock].BlockHealth > activeBlocks[selectedActiveBlock].BlockMaxHealth / 3)
                            {
                                activeBlocks[selectedActiveBlock].BlockHealth = (activeBlocks[selectedActiveBlock].BlockMaxHealth / 3) - 1;
                                chunks[cx3][cy3].UpdateChunk = true;
                            }
                        }
                    }
                }
            }

            chunks[cx][cy].Phase++;
        }

        private void Dirtify(int cx, int cy)
        {
            //for (int cx2 = cx - 1; cx2 <= cx + 1; cx2++)
            //{
            //    for (int cy2 = cy - 1; cy2 <= cy + 1; cy2++)
            //    {
            //        GetTile(cx2, cy2, 0, 0);

            //        int cx3 = CX;
            //        int cy3 = CY;

            //        for (int tx = 0; tx < chunks[cx3][cy3].tiles.Length; tx++)
            //        {
            //            for (int ty = 0; ty < chunks[cx3][cy3].tiles[tx].Length; ty++)
            //            {
            //                if (chunks[cx3][cy3].tiles[tx][ty].BlockID == 0)
            //                {
            //                    GetTile(cx3, cy3, tx, ty + 1);

            //                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
            //                    {
            //                        int ACX = CX;
            //                        int ACY = CY;
            //                        int ATX = TX;
            //                        int ATY = TY;

            //                        int control = random.Next(0, 100);
            //                        int depth = 0;

            //                        if (control >= 90)
            //                            depth = 3;
            //                        else if (control >= 40)
            //                            depth = 2;
            //                        else if (control >= 0)
            //                            depth = 1;

            //                        for (int d = 0; d < depth; d++)
            //                        {
            //                            //This one could be grass.
            //                            if (d >= 1)
            //                                GetTile(ACX, ACY, ATX, ATY + d);

            //                            if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
            //                            {
            //                                if (!ActiveBlockExists(CX, CY, TX, TY))
            //                                {
            //                                    //chunks[CX][CY].tiles[TX][TY].BlockID = 1;
            //                                    activeBlocks.Add(new ActiveBlocks());
            //                                    chunks[CX][CY].ActiveBlocks++;

            //                                    activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, chunks[CX][CY].tiles[TX][TY].BlockID);

            //                                    if (d == 0)
            //                                        activeBlocks[activeBlocks.Count - 1].ChangeBlock = 3;
            //                                    else
            //                                        activeBlocks[activeBlocks.Count - 1].ChangeBlock = 1;

            //                                    chunks[CX][CY].UpdateChunk = true;
            //                                }
            //                                else if (activeBlocks[selectedActiveBlock].ChangeBlock == 0)
            //                                {
            //                                    //chunks[CX][CY].tiles[TX][TY].BlockID = 1;

            //                                    //CopyBlockInfo(selectedActiveBlock);

            //                                    if (d == 0)
            //                                        activeBlocks[selectedActiveBlock].ChangeBlock = 3;
            //                                    else
            //                                        activeBlocks[selectedActiveBlock].ChangeBlock = 1;

            //                                    chunks[CX][CY].UpdateChunk = true;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            chunks[cx][cy].Phase++;
        }

        private void GetTile(int x, int y)
        {
            if (x < 0)
                x += chunkTiles * chunkX;
            else if (x >= chunkTiles * chunkX)
                x -= chunkTiles * chunkX;

            if (y < 0)
                y = 0;
            else if (y >= chunkTiles * chunkY)
                y = (chunkTiles * chunkY) - 1;

            CX = (int)Math.Floor(x / (float)chunkTiles);
            CY = (int)Math.Floor(y / (float)chunkTiles);            
            TX = x - (CX * chunkTiles);
            TY = y - (CY * chunkTiles);
        }

        private void GetTile(int x, int y, int i)
        {
            if (x < 0)
            {
                x += chunkTiles * chunkX;
                miners[i].X = x;
            }
            else if (x >= chunkTiles * chunkX)
            {
                x -= chunkTiles * chunkX;
                miners[i].X = x;
            }

            if (y < 0)
            {
                y = 0;
                miners[i].Y = y;
            }
            else if (y >= chunkTiles * chunkY)
            {
                y = (chunkTiles * chunkY) - 1;
                miners[i].Y = y;
            }

            CX = (int)Math.Floor(x / (float)chunkTiles);
            CY = (int)Math.Floor(y / (float)chunkTiles);
            TX = x - (CX * chunkTiles);
            TY = y - (CY * chunkTiles);
        }

        private void GetTile(int cx, int cy, int tx, int ty)
        {
            if (tx < 0)
            {
                cx--;
                tx = chunkTiles - 1;
            }
            else if (tx >= chunkTiles)
            {
                cx++;
                tx = 0;
            }

            if (ty < 0)
            {
                cy--;
                ty = chunkTiles - 1;
            }
            else if (ty >= chunkTiles)
            {
                cy++;
                ty = 0;
            }

            if (cx < 0)
                cx = chunkX - 1;
            else if (cx >= chunkX)
                cx = 0;

            if (cy < 0)
            {
                cy = 0;
                ty = 0;
            }
            else if (cy >= chunkY)
            {
                cy = chunkY - 1;
                ty = chunkTiles - 1;
            }

            CX = cx;
            CY = cy;
            TX = tx;
            TY = ty;
        }

        public void RemoveBlock(int cx, int cy, int tx, int ty)
        {
            chunks[cx][cy].tiles[tx][ty].BlockID = 0;

            if (ActiveBlockExists(cx, cy, tx, ty))
                if (activeBlocks[selectedActiveBlock].BlockFalling)
                    activeBlocks[selectedActiveBlock].BlockFalling = false;

            int CX0 = cx;
            int CY0 = cy;
            int TX0 = tx;
            int TY0 = ty;

            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {      
                    GetTile(CX0, CY0, TX0 + x, TY0 + y);

                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                    {
                        int CX2 = CX;
                        int CY2 = CY;
                        int TX2 = TX;
                        int TY2 = TY;

                        int i = 0;

                        GetTile(CX2, CY2, TX2, TY2 - 1);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        GetTile(CX2, CY2, TX2 + 1, TY2);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        GetTile(CX2, CY2, TX2, TY2 + 1);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        GetTile(CX2, CY2, TX2 - 1, TY2);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;
                        
                        if (i == 0)
                            if (!ActiveBlockExists(CX2, CY2, TX2, TY2))
                            {
                                activeBlocks.Add(new ActiveBlocks());
                                chunks[CX2][CY2].ActiveBlocks++;
                                activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX2, (byte)CY2, (ushort)TX2, (ushort)TY2, chunks[CX2][CY2].tiles[TX2][TY2].BlockID);
                                chunks[CX2][CY2].UpdateChunk = true;
                            }
                            else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                            {                                
                                activeBlocks[selectedActiveBlock].BlockFalling = true;
                                chunks[CX2][CY2].UpdateChunk = true;
                            }
                    }
                }
            }
        }

        public void CheckBlock(int cx, int cy, int tx, int ty)
        {
            if (cy == chunkY - 1 && ty == chunkTiles - 1)
                chunks[cx][cy].tiles[tx][ty].BlockID = 0;

            if (ActiveBlockExists(cx, cy, tx, ty))
                if (activeBlocks[selectedActiveBlock].BlockFalling)
                    activeBlocks[selectedActiveBlock].BlockFalling = false;

            int CX0 = cx;
            int CY0 = cy;
            int TX0 = tx;
            int TY0 = ty;

            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    GetTile(CX0, CY0, TX0 + x, TY0 + y);

                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                    {
                        int CX2 = CX;
                        int CY2 = CY;
                        int TX2 = TX;
                        int TY2 = TY;

                        int i = 0;

                        GetTile(CX2, CY2, TX2, TY2 - 1);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        GetTile(CX2, CY2, TX2 + 1, TY2);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        GetTile(CX2, CY2, TX2, TY2 + 1);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        GetTile(CX2, CY2, TX2 - 1, TY2);
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            i++;

                        if (i == 0)
                            if (!ActiveBlockExists(CX2, CY2, TX2, TY2))
                            {
                                activeBlocks.Add(new ActiveBlocks());
                                chunks[CX2][CY2].ActiveBlocks++;
                                activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX2, (byte)CY2, (ushort)TX2, (ushort)TY2, chunks[CX2][CY2].tiles[TX2][TY2].BlockID);
                                chunks[CX2][CY2].UpdateChunk = true;
                            }
                            else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                            {
                                activeBlocks[selectedActiveBlock].BlockFalling = true;
                                chunks[CX2][CY2].UpdateChunk = true;
                            }
                    }
                }
            }
        }

        private void UpdateChunks()
        {
            int min = 0;
            int max = activeBlocks.Count;

            if (activeBlocks.Count > 1000)
            {
                max = random.Next(1000, activeBlocks.Count);
                min = max - 1000;
            }

            for (int i = activeBlocks.Count - 1; i >= 0; i--)
            {
                #region Falling Blocks

                if (activeBlocks[i].BlockFalling)
                {
                    int ACX = activeBlocks[i].CX;
                    int ACY = activeBlocks[i].CY;
                    int ATX = activeBlocks[i].TX;
                    int ATY = activeBlocks[i].TY;

                    if (chunks[ACX][ACY].tiles[ATX][ATY].BlockID == 0)
                    {
                        if (activeBlocks[i].BlockFalling)
                            activeBlocks[i].BlockFalling = false;
                    }
                    else
                    {
                        GetTile(ACX, ACY, ATX, ATY + 1);

                        if (chunks[CX][CY].tiles[TX][TY].BlockID == 0)
                        {
                            chunks[CX][CY].tiles[TX][TY].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                            chunks[ACX][ACY].tiles[ATX][ATY].BlockID = 0;
                            activeBlocks[i].BlockFalling = false;

                            if (!ActiveBlockExists(CX, CY, TX, TY))
                            {
                                activeBlocks.Add(new ActiveBlocks());
                                chunks[CX][CY].ActiveBlocks++;
                                activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, chunks[CX][CY].tiles[TX][TY].BlockID);
                                chunks[CX][CY].UpdateChunk = true;
                            }
                            else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                            {
                                CopyBlockInfo(i);
                                activeBlocks[selectedActiveBlock].BlockFalling = true;
                                chunks[CX][CY].UpdateChunk = true;
                            }
                        }
                        else
                        {
                            if (activeBlocks[i].BlockFalling)
                                activeBlocks[i].BlockFalling = false;

                            CheckBlock(ACX, ACY, ATX, ATY);
                        }
                    }
                }

                #endregion

                if (i < max && i >= min)
                {
                    #region Changing Blocks

                    if (activeBlocks[i].ChangeBlock != 0)
                    {
                        int ACX = activeBlocks[i].CX;
                        int ACY = activeBlocks[i].CY;
                        int ATX = activeBlocks[i].TX;
                        int ATY = activeBlocks[i].TY;

                        if (chunks[ACX][ACY].tiles[ATX][ATY].BlockID != 0)
                        {
                            if (activeBlocks.Count > 1000)
                            {
                                chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
                                activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                                activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
                                activeBlocks[i].ChangeBlock = 0;
                            }
                            else if (random.Next(0, 25) == 0)
                            {
                                chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
                                activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                                activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
                                activeBlocks[i].ChangeBlock = 0;
                            }
                        }
                        else
                        {
                            activeBlocks[i].ChangeBlock = 0;
                        }
                    }

                    #endregion

                    #region Health Check

                    if (activeBlocks[i].BlockMaxHealth != activeBlocks[i].BlockHealth)
                    {
                        if (activeBlocks[i].BlockHealth > activeBlocks[i].BlockMaxHealth)
                        {
                            //Overcharged Block
                            ///////////////////////////////////////////////////////////////
                            activeBlocks[i].BlockHealth = activeBlocks[i].BlockMaxHealth;
                            activeBlocks[i].BlockHealing = false;
                            activeBlocks[i].BlockDecaying = false;
                            activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
                            ////////////////////////////////////////////////////////////////
                        }
                        else if (activeBlocks[i].BlockHealth >= activeBlocks[i].BlockMaxHealth / 2 && activeBlocks[i].BlockHealth < activeBlocks[i].BlockMaxHealth)
                        {
                            activeBlocks[i].BlockHealing = true;
                            activeBlocks[i].BlockDecaying = false;
                            activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
                        }
                        else if (activeBlocks[i].BlockHealth < activeBlocks[i].BlockMaxHealth / 2 && activeBlocks[i].BlockHealth > 0)
                        {
                            activeBlocks[i].BlockHealing = false;
                            activeBlocks[i].BlockDecaying = true;
                            activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
                        }
                        else
                        {
                            RemoveBlock(activeBlocks[i].CX, activeBlocks[i].CY, activeBlocks[i].TX, activeBlocks[i].TY);
                            activeBlocks[i].BlockHealing = false;
                            activeBlocks[i].BlockDecaying = false;
                            activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
                        }
                    }
                    else
                    {
                        activeBlocks[i].BlockHealing = false;
                        activeBlocks[i].BlockDecaying = false;
                        activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
                    }

                    #endregion

                    #region Decaying Block

                    if (activeBlocks[i].BlockDecaying)
                        //Maybe change to time based thingy
                        if (activeBlocks.Count > 1000)                        
                            activeBlocks[i].BlockHealth = 0;                        
                        else if (random.Next(0, 5) == 0)
                                activeBlocks[i].BlockHealth -= random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
                        
                    #endregion

                    #region Regenerating Block

                    if (activeBlocks[i].BlockHealing)
                        //Maybe change to time based thingy
                        if (activeBlocks.Count > 1000)
                            activeBlocks[i].BlockHealth = activeBlocks[i].BlockMaxHealth;
                        else if (random.Next(0, 5) == 0)
                            activeBlocks[i].BlockHealth += random.Next(0, activeBlocks[i].BlockMaxHealth / 5);

                    #endregion
                }

                if (!CheckActiveBlock(i))
                {
                    chunks[CX][CY].ActiveBlocks--;
                    activeBlocks.RemoveAt(i);
                }
            }

            //Console.WriteLine("Active Blocks: " + activeBlocks.Count);

            for (int x = chunks.Length - 1; x >= 0; x--)
            {
                for (int y = chunks[x].Length - 1; y >= 0; y--)
                {
                    if (chunks[x][y].Miners == 0 && chunks[x][y].Phase == 1)
                        chunks[x][y].Phase++;

                    //if (chunks[x][y].Phase == 3 && activeBlocks.Count < 1000)                    
                    //    Dirtify(x, y);

                    if (chunks[x][y].Phase > 1 && chunks[x][y].Phase <= 10 && activeBlocks.Count < 2500 && !chunkControl)
                    //if (chunks[x][y].Phase > 1 && chunks[x][y].Phase <= 10 && !chunkControl)
                    {
                        Smooth(x, y);
                        chunkControl = true;
                    }
                    
                    if (chunks[x][y].UpdateChunk)
                    {
                    }

                    if (chunks[x][y].ActiveBlocks == 0)
                        chunks[x][y].UpdateChunk = false;
                    else
                        chunks[x][y].UpdateChunk = true;
                }
            }

            chunkControl = false;
        }

        bool chunkControl = false;

        private bool ActiveBlockExists(int cx, int cy, int tx, int ty)
        {
            for (int i = 0; i < activeBlocks.Count; i++)
            {
                if (activeBlocks[i].CX == cx)
                    if (activeBlocks[i].CY == cy)
                        if (activeBlocks[i].TX == tx)
                            if (activeBlocks[i].TY == ty)
                            {
                                selectedActiveBlock = i;
                                return true;
                            }
            }
            return false;
        }

        private bool CheckActiveBlock(int i)
        {
            if (!activeBlocks[i].BlockFalling)
                if (activeBlocks[i].BlockHealth == activeBlocks[i].BlockMaxHealth || (activeBlocks[i].BlockHealth <= 0 && !activeBlocks[i].BlockDecaying))
                    if (activeBlocks[i].ChangeBlock == 0)
                    return false;

            return true;
        }

        private void CopyBlockInfo(int i)
        {
            activeBlocks[selectedActiveBlock].BlockHealth = activeBlocks[i].BlockHealth;
            activeBlocks[selectedActiveBlock].BlockDecaying = activeBlocks[i].BlockDecaying;
            activeBlocks[selectedActiveBlock].BlockFalling = activeBlocks[i].BlockFalling;
            activeBlocks[selectedActiveBlock].BlockHealing = activeBlocks[i].BlockHealing;
            activeBlocks[selectedActiveBlock].BlockMaxHealth = activeBlocks[i].BlockMaxHealth;
            activeBlocks[selectedActiveBlock].ChangeBlock = activeBlocks[i].ChangeBlock;
            activeBlocks[selectedActiveBlock].Initialize(activeBlocks[i].CX, activeBlocks[i].CY, activeBlocks[i].TX, activeBlocks[i].TY, activeBlocks[i].BlockID);
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

                                if (chunks[x][y].tiles[x2][y2].BlockID == 1 || chunks[x][y].tiles[x2][y2].BlockID == 3)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.White);

                                //if (chunks[x][y].tiles[x2][y2].BlockID == 1)
                                //    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.SandyBrown);
                                else if (chunks[x][y].tiles[x2][y2].BlockID == 2)
                                    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.DimGray);
                                //else if (chunks[x][y].tiles[x2][y2].BlockID == 3)
                                //    spriteBatch.Draw(tileTextures[chunks[x][y].tiles[x2][y2].BlockID], position, Color.Green);
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
                if (miners[i].X > RenderTileX && miners[i].X < RenderTileX2 && miners[i].Y > RenderTileY && miners[i].Y < RenderTileY2)
                {
                    position.X = worldPosition.X + (miners[i].Position.X) + tileTextures[6].Width / 2;
                    position.Y = worldPosition.Y + (miners[i].Position.Y) + tileTextures[6].Height / 2;

                    spriteBatch.Draw(minerTexture, position, null, Color.White, 0f, Offset, miners[i].Size + (miners[i].Size - 1), SpriteEffects.None, 0f);
                }
            }
        }
    }
}
