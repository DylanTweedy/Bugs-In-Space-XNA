using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimplexNoise;
using Bitmap = System.Drawing.Bitmap;
using BMPColor = System.Drawing.Color;


namespace BackgroundTest
{
    class World11
    {      
        Chunk[][] chunks;
        List<Creator> creators;
        //List<Miner> miners;
        //List<Biominator> biominators;

        Texture2D minerTexture;

        byte tileSize;
        byte chunkTiles;

        byte chunkX;
        byte chunkY;

        float chunksHorizontal;
        float chunksVertical;

        int minerMinSpeed;
        int creatorMaxSize;
        int biominatorMaxSize;

        int CX;
        int CY;
        int TX;
        int TY;
        bool ActiveChunk;

        int chunkXMin;
        int chunkXMax;
        int chunkYMin;
        int chunkYMax;

        int drawnBlocksChanged;

        int selectedActiveBlock;

        ushort[][] chunkHolder;

        float caveSize;
        float biomeSize;
        int minerChance;
        
        Vector2 Offset;
        Vector2 worldPosition;
        //List<Texture2D> tileTextures;
        List<ActiveBlocks> activeBlocks;
        ushort tilesX;
        ushort tilesY;
        int WorldTilesX;
        int WorldTilesY;


        Random random;
        Random creatorRandom;

        double pressure;
        double AtmosphericPressure;
        double LowerMantlePressure;

        double temperature;
        double surfaceTemperature;
        double LowerMantleTemperature;

        Vector2 position;

        Vector2 CameraPosition;
        float CameraZoom;

        int RenderTileX, RenderTileY, RenderTileX2, RenderTileY2;
        int RenderChunkX, RenderChunkY, RenderChunkX2, RenderChunkY2;

        ushort[] leftHill;
        ushort[] rightHill;
        ushort SeaLevel;
        float SeaLevelMultiplier;
        ushort SeaLevelMax;

        MouseState mState;
        MouseState pMState;
        KeyboardState kState;
        KeyboardState pKState;

        List<ushort> stoneProb;
        List<ushort> stoneID;

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
            public int BiomableTiles;
            public int TilesBiomed;
            public ushort Miners;
            public ushort Biominators;
            public byte Phase;
            public byte SubPhase;
            public ushort ActiveBlocks;
            public bool ChunkActive;
            public byte ChunkType;

            public void LoadCaves(ushort ChunkSize, ushort LandID)
            {
                tiles = new Tiles[ChunkSize][];

                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        tiles[x][y].BlockID = LandID;
                    }
                }

                ChunkActive = true;
            }

            public void LoadCaves(ushort ChunkSize, ushort LandID, ushort LandID2, Random rand)
            {
                tiles = new Tiles[ChunkSize][];

                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        int variance = rand.Next(0, ChunkSize / 2);
                        variance -= ChunkSize / 4;

                        if (y > (ChunkSize / 2) + variance)
                            tiles[x][y].BlockID = LandID;
                        else
                            tiles[x][y].BlockID = LandID2;
                    }
                }

                ChunkActive = true;
            }

            public void LoadLand(ushort ChunkSize, ushort LandID)
            {
                tiles = new Tiles[ChunkSize][];

                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        tiles[x][y].BlockID = LandID;
                    }
                }

                Phase = 3;
                EdibleTiles = (ushort)((ChunkSize * ChunkSize) * 2);

                ChunkActive = true;
            }

            public void LoadSky(ushort ChunkSize)
            {
                tiles = new Tiles[ChunkSize][];

                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        tiles[x][y].BlockID = 0;
                    }
                }

                ChunkActive = true;
            }

            public void Initialize(ushort edibleTiles, ushort biomableTiles)
            {
                ChunkActive = false;
                EdibleTiles = edibleTiles;
                BiomableTiles = biomableTiles;
                Miners = 0;
                Phase = 1;
                SubPhase = 1;
                ActiveBlocks = 0;
            }
        }

        private class Creator
        {
            public int X, Y;
            public int TilesChanged, MaxTiles;

            public int Behaviour;

            public int Direction;
            public bool Active;
            public bool ChangeBlock;
            public ushort BlockID;

            Random rand;
            int airTime;
            int maxAirTime;
            int tileSize;

            public bool Spawn;
            public byte SpawnChance;

            public bool UpSolid;
            public bool DownSolid;
            public bool LeftSolid;
            public bool RightSolid;
            public bool InAir;

            public int SChunkX;
            public int SChunkY;

            public int Size;

            public void Initialize(int x, int y, int TileSize, int maxTiles, int sChunkX, int sChunkY, int size, int direction, Random random, int behaviour, ushort blockID)
            {
                X = x;
                Y = y;
                rand = random;
                Active = true;
                SpawnChance = 2;
                airTime = 0;
                MaxTiles = maxTiles;
                SChunkX = sChunkX;
                SChunkY = sChunkY;
                tileSize = TileSize;
                Size = size;
                Behaviour = behaviour;
                BlockID = blockID;
                Direction = direction;
            }

            public void Update(GameTime gameTime)
            {
                if (!InAir)
                {
                    if (TilesChanged >= MaxTiles || rand.Next(0, 200) == 0)
                        Active = false;

                    DirectonChange();
                    ChangeBlock = true;

                }
                else
                    AirMovement();
            }

            private void AirMovement()
            {
                if (airTime > maxAirTime)
                {
                    Direction = rand.Next(0, 4);
                    maxAirTime = rand.Next(0, 64);
                    airTime = 0;
                }

                switch (Behaviour)
                {
                    case 0:

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
                        break;

                    case 1:

                        switch (Direction)
                        {
                            case 0:
                                Y -= Size;
                                break;

                            case 1:
                                X += Size;
                                break;

                            case 2:
                                Y += Size;
                                break;

                            case 3:
                                X -= Size;
                                break;
                        }
                        break;
                }
                airTime++;
            }

            private void DirectonChange()
            {
                int SpawnNumber;
                bool Choosing;
                int d;
                switch (Behaviour)
                {
                    #region Miner

                    case 0:
                        Choosing = true;
                        d = 0;

                        while (Choosing)
                        {
                            Direction = rand.Next(0, 4);

                            if (TilesChanged > MaxTiles * 0.8 && Direction == 0)
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

                        SpawnNumber = rand.Next(0, 100 / Size);

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
                        break;

                    #endregion

                    #region Block Changer

                    case 1:
                        Choosing = true;
                        d = 0;

                        while (Choosing)
                        {
                            Direction = rand.Next(0, 4);

                            if (TilesChanged > MaxTiles * 0.8 && Direction == 0)
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

                        SpawnNumber = rand.Next(0, 100 / Size);

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
                        //SpawnNumber = rand.Next(0, 100 / Size);

                        //switch (Direction)
                        //{
                        //    case 0:
                        //        Y -= Size + 1;
                        //        if (SpawnNumber < SpawnChance)
                        //            Spawn = true;
                        //        break;

                        //    case 1:
                        //        X += Size + 1;
                        //        if (SpawnNumber < SpawnChance)
                        //            Spawn = true;
                        //        break;

                        //    case 2:
                        //        Y += Size + 1;
                        //        if (SpawnNumber < SpawnChance)
                        //            Spawn = true;
                        //        break;

                        //    case 3:
                        //        X -= Size + 1;
                        //        if (SpawnNumber < SpawnChance)
                        //            Spawn = true;
                        //        break;
                        //}
                        break;

                    #endregion
                }
            }
        }

        //private class Miner
        //{
        //    public int X, Y;
        //    public int TilesEaten, MaxTiles;
            
        //    public int Direction;
        //    public bool Active;

        //    Random rand;
        //    int airTime;
        //    int maxAirTime;
        //    float originalSpeed;
        //    float speed;
        //    int tileSize;

        //    public bool Spawn;
        //    public byte SpawnChance;

        //    public bool UpSolid;
        //    public bool DownSolid;
        //    public bool LeftSolid;
        //    public bool RightSolid;
        //    public bool InAir;

        //    public bool DestroyBlock;

        //    public int SChunkX;
        //    public int SChunkY;

        //    public Vector2 Position;

        //    public int Size;
        //    bool sizeChange;


        //    public void Initialize(int x, int y, float Speed, int TileSize, int maxTiles, int sChunkX, int sChunkY, int size, Random random)
        //    {
        //        X = x;
        //        Y = y;
        //        originalSpeed = Speed;
        //        rand = random;
        //        Active = true;
        //        SpawnChance = 1;
        //        airTime = 0;
        //        MaxTiles = maxTiles;
        //        SChunkX = sChunkX;
        //        SChunkY = sChunkY;
        //        tileSize = TileSize;
        //        Size = size;
        //        sizeChange = false;
        //        speed = originalSpeed - Size;

        //        Position = new Vector2(X * tileSize, Y * tileSize);
        //    }
            
        //    public void Update(GameTime gameTime)
        //    {
        //        if (!InAir)
        //        {
        //                if (TilesEaten >= MaxTiles || rand.Next(0, 200) == 0)
        //                    Active = false;
        //                //else if (TilesEaten >= MaxTiles / 2 && !sizeChange)
        //                //{
        //                //    Size = rand.Next(1, 4);
        //                //    sizeChange = true;
        //                //    speed = originalSpeed - Size;
        //                //}
                        
        //                DirectonChange();
        //                DestroyBlock = true;

        //        }
        //        else
        //        {
        //                AirMovement();
        //        }
        //    }
                        
        //    private void AirMovement()
        //    {
        //        if (rand.Next(0, 500) == 0)
        //            Active = false;

        //        if (airTime > maxAirTime)
        //        {
        //            Direction = rand.Next(0, 4);
        //            maxAirTime = rand.Next(0, 64);
        //            airTime = 0;
        //        }

        //        switch (Direction)
        //        {
        //            case 0:
        //                Y--;
        //                break;

        //            case 1:
        //                X++;
        //                break;

        //            case 2:
        //                Y++;
        //                break;

        //            case 3:
        //                X--;
        //                break;
        //        }
                
        //        airTime++;
        //    }

        //    private void DirectonChange()
        //    {
        //        bool Choosing = true;
        //        int d = 0;

        //        while (Choosing)
        //        {
        //            Direction = rand.Next(0, 4);

        //            if (TilesEaten > MaxTiles * 0.8 && Direction == 0)
        //                Direction = rand.Next(1, 4);

        //            switch (Direction)
        //            {
        //                case 0:
        //                    if (UpSolid)
        //                        Choosing = false;
        //                    break;

        //                case 1:
        //                    if (RightSolid)
        //                        Choosing = false;
        //                    break;

        //                case 2:
        //                    if (DownSolid)
        //                        Choosing = false;
        //                    break;

        //                case 3:
        //                    if (LeftSolid)
        //                        Choosing = false;
        //                    break;
        //            }

        //            if (d == 5)
        //            {
        //                if (UpSolid)
        //                    Direction = 0;
        //                else if (RightSolid)
        //                    Direction = 1;
        //                else if (DownSolid)
        //                    Direction = 2;
        //                else if (LeftSolid)
        //                    Direction = 3;
        //                else
        //                    Direction = 2;

        //                Choosing = false;
        //            }
        //            else
        //                d++;
        //        }

        //        int SpawnNumber = rand.Next(0, 100 / Size);

        //        switch (Direction)
        //        {
        //            case 0:
        //                Y--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 1:
        //                X++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 2:
        //                Y++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 3:
        //                X--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;
        //        }
        //    }
        //}

        //private class Biominator
        //{
        //    public int X, Y;
        //    public int TilesBiominated, MaxTiles;
        //    public ushort BlockID;

        //    public int Direction;
        //    public bool Active;

        //    Random rand;
        //    int airTime;
        //    int maxAirTime;
        //    float originalSpeed;
        //    float speed;
        //    int tileSize;

        //    public bool Spawn;
        //    public byte SpawnChance;
        //    public bool ChangeBlock;
            
        //    public int SChunkX;
        //    public int SChunkY;

        //    public bool Moving;
        //    public Vector2 Position;
        //    public int HomeLand;

        //    public int Size;
        //    bool sizeChange;


        //    public void Initialize(int x, int y, float Speed, int TileSize, int maxTiles, int sChunkX, int sChunkY, int size, int direction, ushort blockID, Random random)
        //    {
        //        X = x;
        //        Y = y;
        //        originalSpeed = Speed;
        //        rand = random;
        //        Active = true;
        //        SpawnChance = 1;
        //        airTime = 0;
        //        MaxTiles = maxTiles;
        //        SChunkX = sChunkX;
        //        SChunkY = sChunkY;
        //        Moving = false;
        //        tileSize = TileSize;
        //        Size = size;
        //        sizeChange = false;
        //        speed = originalSpeed - Size;
        //        Direction = direction;
        //        BlockID = blockID;
        //        HomeLand = 0;

        //        Position = new Vector2(X * tileSize, Y * tileSize);
        //    }

        //    public void SpeedFinish()
        //    {
        //        if (!Moving)
        //        {
        //            if (TilesBiominated >= MaxTiles)
        //                Active = false;
        //            else if (TilesBiominated >= MaxTiles / 2 && !sizeChange)
        //            {
        //                Size = rand.Next(1, 4);
        //                sizeChange = true;
        //                speed = originalSpeed - Size;
        //            }

        //            DirectonChange();
        //            //InstantMove();
        //            Moving = false;

        //            ChangeBlock = true;
        //        }
        //    }

        //    public void Update(GameTime gameTime)
        //    {
        //        if (!Moving)
        //        {
        //            if (TilesBiominated >= MaxTiles || rand.Next(0, 5) == 0)
        //                Active = false;
        //            else if (TilesBiominated >= MaxTiles / 2 && !sizeChange)
        //            {
        //                Size = rand.Next(1, 4);
        //                sizeChange = true;
        //                speed = originalSpeed - Size;
        //            }

        //            DirectonChange();

        //            ChangeBlock = true;
        //        }
        //        else
        //            Move();
        //    }

        //    private void DirectonChange()
        //    {
        //        int DirectionModifier = rand.Next(0, 4);
                
        //        int SpawnNumber = rand.Next(0, 100 / Size);

        //        switch (Direction)
        //        {
        //            case 0:
        //                Y--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 1:
        //                X++;
        //                Y--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 2:
        //                X++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 3:
        //                Y++;
        //                X++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 4:
        //                Y++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 5:
        //                X--;
        //                Y++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 6:
        //                X--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 7:
        //                X--;
        //                Y--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;
        //        }

        //        switch (DirectionModifier)
        //        {
        //            case 0:
        //                Y--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 1:
        //                X++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 2:
        //                Y++;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;

        //            case 3:
        //                X--;
        //                if (SpawnNumber < SpawnChance)
        //                    Spawn = true;
        //                break;
        //        }

        //        Moving = true;
        //    }

        //    private void Move()
        //    {
        //        Position.X = X * tileSize;
        //        Position.Y = Y * tileSize;

        //        Moving = false;
        //    }
        //}

        public void Initialize()
        {
            pressure = 0;
            temperature = 0;

            tileSize = WorldVariables.TileSize;
            chunkTiles = WorldVariables.ChunkSize;
            
            random = new Random();
            creatorRandom = new Random();
            worldPosition = Vector2.Zero;

            chunkX = 65;
            chunkY = 65;

            //Earth 1
            AtmosphericPressure = random.Next(0, 10000000) / 100000f;
            //Earth 1,600,000
            LowerMantlePressure = AtmosphericPressure + (random.Next(100, 10000) * (chunkY / 2));


            //Earth - 288K
            surfaceTemperature = random.Next(100, 1000);
            //Earth - 1173K
            LowerMantleTemperature = surfaceTemperature + (random.Next(15, 400) * (chunkY / 2));


            //LowerMantlePressure = (long)random.Next((AtmosphericPressure + (10000 * chunkY)) / 100000, 100000000) * 100000;

            WorldTilesX = chunkTiles * chunkX;
            WorldTilesY = chunkTiles * chunkY;

            SeaLevelMultiplier = 0.25f;
            SeaLevel = (ushort)((chunkY * chunkTiles) * SeaLevelMultiplier);
            SeaLevelMax = (ushort)(SeaLevel / 3);
            
            chunksHorizontal = WorldVariables.WindowWidth / (float)(chunkTiles * tileSize);
            chunksVertical = WorldVariables.WindowHeight / (float)(chunkTiles * tileSize);

            tilesX = (ushort)(chunkX * chunkTiles);
            tilesY = (ushort)(chunkY * chunkTiles);
            
            creators = new List<Creator>();
            minerMinSpeed = 10000;
            creatorMaxSize = 4;
            biominatorMaxSize = 3;

            caveSize = 0.7f;
            biomeSize = 2f;
            minerChance = 80;

            activeBlocks = new List<ActiveBlocks>();

            SetHillData();

            chunkHolder = new ushort[chunkTiles][];

            for (int a = 0; a < chunkHolder.Length; a++)
                chunkHolder[a] = new ushort[chunkTiles];
            
            chunks = new Chunk[chunkX][];
                        
            for (int x = 0; x < chunks.Length; x++)
            {
                chunks[x] = new Chunk[chunkY];

                for (int y = 0; y < chunks[x].Length; y++)
                    chunks[x][y].Initialize((ushort)((chunkTiles * chunkTiles) * caveSize), (ushort)((chunkTiles * chunkTiles) * biomeSize));                
            }

            //biominators = new List<Biominator>();

            /////////////////////////////////////////
            //AddBiominators(5, 5, 3, true);
            //AddBiominators(3, 5, 4, true);
            //AddCreator(
            //        (ushort)(32),
            //        (ushort)(32),
            //        (ushort)(random.Next(3, 5)),
            //        5,
            //        5,
            //        creatorRandom.Next(150, 250),
            //        1,
            //        creatorRandom.Next(0, 3));
            ////////////////////////////////////////
        }

        #region Land

        private void SetHillData()
        {
            leftHill = new ushort[chunkX];
            rightHill = new ushort[chunkX];

            int LandType = 0;
            ////////////////////////////
            // 1 = Hills
            // 2 = Mountains
            // 3 = FlatLand
            // 4 = Plateau
            // 5 = Cliffs
            ////////////////////////////

            int TerrainWidth = 0;
            int TerrainHeight = 0;
            int Variance = 0;
            int MaxHeight = SeaLevel / 2;
            int MaxWidth = chunkX / 10;
            bool Inverted = false;
            bool Up;
            int i = 0;

            for (int h = 0; h < chunkX; h++)
            {
                if (h == 0)
                {
                    leftHill[h] = SeaLevel;

                    if (random.Next(0, 2) == 0)
                        rightHill[h] = (ushort)(SeaLevel + random.Next(0, 100));
                    else
                        rightHill[h] = (ushort)(SeaLevel - random.Next(0, 100));
                }
                else if (h == chunkX - 1)
                {
                    rightHill[h] = SeaLevel;
                    leftHill[h] = rightHill[h - 1];
                }
                else
                {
                    if (LandType == 0)
                    {
                        TerrainHeight = random.Next(0, MaxHeight / 3);
                        TerrainWidth = random.Next(2, MaxWidth / 2);
                        TerrainWidth *= 2;
                        i = 0;
                        if (random.Next(0, 2) == 0)
                            Inverted = true;
                        else
                            Inverted = false;
                    }

                    leftHill[h] = rightHill[h - 1];

                    if (i == TerrainWidth)
                    {
                        LandType = 0;
                    }
                    else
                    {
                        if (i >= TerrainWidth / 2)
                        {
                            if (Inverted)
                                Inverted = false;
                            else
                                Inverted = true;
                        }

                        if (Inverted)
                            rightHill[h] = (ushort)(leftHill[h] + (TerrainHeight / (TerrainWidth / 2)));
                        else
                            rightHill[h] = (ushort)(leftHill[h] - (TerrainHeight / (TerrainWidth / 2)));

                        i++;
                    }
                }
            }
        }

        //private void SetHillData()
        //{
        //    leftHill = new ushort[chunkX];
        //    rightHill = new ushort[chunkX];

        //    int LandType = 0;
        //    ////////////////////////////
        //    // 1 = Hills
        //    // 2 = Mountains
        //    // 3 = FlatLand
        //    // 4 = Plateau
        //    // 5 = Cliffs
        //    ////////////////////////////

        //    int TerrainWidth = 0;
        //    int TerrainHeight = 0;
        //    int Variance = 0;
        //    int MaxHeight = SeaLevel / 2;
        //    int MaxWidth = chunkX / 10;
        //    bool Inverted = false;
        //    bool Up;
        //    int i = 0;

        //    for (int h = 0; h < chunkX; h++)
        //    {
        //        if (h == 0)
        //        {
        //            leftHill[h] = SeaLevel;

        //            if (random.Next(0, 2) == 0)
        //                rightHill[h] = (ushort)(SeaLevel + random.Next(0, 100));
        //            else
        //                rightHill[h] = (ushort)(SeaLevel - random.Next(0, 100));
        //        }
        //        else if (h == chunkX - 1)
        //        {
        //            rightHill[h] = SeaLevel;

        //            //if (random.Next(0, 2) == 0)
        //            //    leftHill[h] = (ushort)(SeaLevel + random.Next(0, 100));
        //            //else
        //            //    leftHill[h] = (ushort)(SeaLevel - random.Next(0, 100));
        //        }
        //        else
        //        {
        //            if (LandType == 0)
        //            {
        //                int chance = random.Next(0, 100);

        //                //if (chance < 30)
        //                {
        //                    LandType = 1;
        //                    TerrainHeight = random.Next(MaxHeight / 1000, MaxHeight / 50);
        //                    TerrainWidth = random.Next(2, MaxWidth / 2);
        //                    TerrainWidth *= 2;
        //                    Variance = 4;
        //                }
        //                //else if (chance < 50)
        //                //{
        //                //    LandType = 2;
        //                //    TerrainHeight = random.Next(MaxHeight / 2, MaxHeight);
        //                //    TerrainWidth = random.Next(2, MaxWidth);
        //                //    Variance = 7;
        //                //}
        //                //else if (chance < 70)
        //                //{
        //                //    LandType = 3;
        //                //    TerrainHeight = random.Next(0, MaxHeight / 100);
        //                //    TerrainWidth = random.Next(2, MaxWidth);
        //                //    Variance = 2;
        //                //}
        //                //else if (chance < 90)
        //                //{
        //                //    LandType = 4;
        //                //    TerrainHeight = random.Next(MaxHeight / 3, MaxHeight / 2);
        //                //    TerrainWidth = random.Next(2, MaxWidth);
        //                //    Variance = 15;
        //                //}
        //                //else if (chance < 100)
        //                //{
        //                //    LandType = 5;
        //                //    TerrainHeight = random.Next(MaxHeight / 3, MaxHeight / 2);
        //                //    TerrainWidth = random.Next(2, MaxWidth);
        //                //    Variance = 15;
        //                //}



        //                if (random.Next(0, 2) == 0)
        //                {
        //                    Inverted = true;
        //                    Up = false;
        //                }
        //                else
        //                {
        //                    Inverted = false;
        //                    Up = true;
        //                }

        //                //if (h + TerrainWidth >= chunkX)
        //                //    TerrainWidth = (chunkX - h) - 1;

        //                //if (leftHill[h] + TerrainHeight < MaxHeight)
        //                //    TerrainHeight = MaxHeight - leftHill[h];

        //                i = 0;
        //            }

        //            leftHill[h] = rightHill[h - 1];

        //            if (i == TerrainWidth)
        //            {
        //                LandType = 0;
        //            }
        //            else
        //            {
        //                if (i >= TerrainWidth / 2)
        //                {
        //                    if (Inverted)
        //                        Inverted = false;
        //                    else
        //                        Inverted = true;
        //                }

        //                if (Inverted)
        //                    rightHill[h] = (ushort)(leftHill[h] + (TerrainHeight / (TerrainWidth / 2)));
        //                else
        //                    rightHill[h] = (ushort)(leftHill[h] - (TerrainHeight / (TerrainWidth / 2)));

        //                i++;
        //            }

        //        }
        //    }
        //}

        private void LoadChunk(int x, int y)
        {
            ushort LandID = 2;

            if (y * chunkTiles > SeaLevel + SeaLevelMax)
            {
                if (y * chunkTiles > SeaLevel + SeaLevelMax + chunkTiles)
                    chunks[x][y].LoadCaves(chunkTiles, LandID);
                else
                    chunks[x][y].LoadCaves(chunkTiles, LandID, 1, random);

                if (creatorRandom.Next(0, 100) < minerChance)
                {
                    AddCreator(
                        (ushort)(creatorRandom.Next(0, chunkTiles) + (x * chunkTiles)),
                        (ushort)(creatorRandom.Next(0, chunkTiles) + (y * chunkTiles)),
                        0,
                        x,
                        y,
                        creatorRandom.Next(150, 250),
                        0,
                        creatorRandom.Next(0, 3));
                }

                //if (creatorRandom.Next(0, 100) < 25)
                {
                    AddCreator(
                        (ushort)(creatorRandom.Next(0, chunkTiles) + (x * chunkTiles)),
                        (ushort)(creatorRandom.Next(0, chunkTiles) + (y * chunkTiles)),
                        (ushort)(random.Next(3, 8)),
                        x,
                        y,
                        creatorRandom.Next(150, 250),
                        1,
                        creatorRandom.Next(0, 3));
                }
            }
            else
            {
                if (!chunks[x][y].ChunkActive)
                {
                    CreateHills(x, y);

                    int hillBotton;
                    int hillTop;

                    if (leftHill[x] > rightHill[x])
                    {
                        hillBotton = leftHill[x];
                        hillTop = rightHill[x];
                    }
                    else
                    {
                        hillBotton = rightHill[x];
                        hillTop = leftHill[x];
                    }

                    GetTile(x * chunkTiles, hillBotton);

                    if (y > CY)
                        if (!chunks[x][y].ChunkActive)
                        {
                            chunks[x][y].LoadLand(chunkTiles, 1);

                            if (creatorRandom.Next(0, 100) < 10)
                            {
                                AddCreator((ushort)(creatorRandom.Next(0, chunkTiles) + (x * chunkTiles)),
                                    (ushort)(creatorRandom.Next(0, chunkTiles) + (y * chunkTiles)),
                                    0,
                                    x,
                                    y,
                                    creatorRandom.Next(150, 250),
                                    0,
                                    creatorRandom.Next(0, 3));
                            }
                        }

                    //GetTile(x * chunkTiles, hillTop);

                    //if (y < CY)
                    //    if (!chunks[x][y].ChunkActive)
                    //        chunks[x][y].LoadLand(chunkTiles, 6);
               } 
            }
        }

        //private void CreateHills(int x, int y)
        //{
        //    int LeftDepth = leftHill[x];
        //    int RightDepth = rightHill[x];
        //    int Difference = Math.Abs(LeftDepth - RightDepth);
        //    bool up;

        //    if (LeftDepth > RightDepth)
        //        up = true;
        //    else
        //        up = false;

        //    int Left = (int)(chunkTiles / (leftHill[x] / (float)(chunkTiles)));
        //    int Right = (int)(chunkTiles / (leftHill[x] / (float)(chunkTiles)));

        //    int V = Left;

        //    for (int x2 = 0; x2 < chunkTiles; x2++)
        //    {
        //        for (int y2 = 0; y2 < chunkTiles; y2++)
        //        {
        //            if (y2 < V)
        //                chunks[x][y].tiles[x2][y2].BlockID = 0;
        //        }

        //        int increase = random.Next(0, (int)((Difference / 10f) + 2));

        //        if (up)
        //        {
        //            V -= increase;

        //            if (V > Right)
        //                V += increase * 2;
        //        }
        //        else
        //        {
        //            V += increase;

        //            if (V < Right)
        //                V -= increase * 2;
        //        }
                

        //    }
        //}

        private void CreateHills(int x, int y)
        {
            int LeftDepth = leftHill[x];
            int RightDepth = rightHill[x];
            int Difference = Math.Abs(LeftDepth - RightDepth);
            bool up;

            int HighPoint;
            int LowPoint;

            if (LeftDepth > RightDepth)
            {
                HighPoint = RightDepth;
                LowPoint = LeftDepth;
            }
            else
            {
                HighPoint = LeftDepth;
                LowPoint = RightDepth;
            }

            HighPoint = (int)((float)HighPoint / chunkTiles) * chunkTiles;

            if (LeftDepth > RightDepth)
                up = true;
            else
                up = false;

            int V = LeftDepth;
            int VFinal = V;
            float increase = (float)Difference / (float)chunkTiles;
            float dec = 0;
            
            for (int x2 = 0; x2 < chunkTiles; x2++)
            {
                for (int y2 = HighPoint; y2 <= LowPoint; y2++)
                {
                    GetTile((x * chunkTiles) + x2, y2);

                    if (TY == 0)
                    {
                    }

                    if (!chunks[CX][CY].ChunkActive)
                        chunks[CX][CY].LoadLand(chunkTiles, 1);

                    if (y2 < VFinal)
                        chunks[CX][CY].tiles[TX][TY].BlockID = 0;
                }

                //int increase = random.Next(0, (int)((Difference / 48f) + 2));

                dec += increase - (int)increase;


                if (up)
                {
                    V -= (int)increase;

                    if (dec >= 1)
                        V--;
                }
                else
                {
                    V += (int)increase;

                    if (dec >= 1)
                        V++;
                }

                if (random.Next(0, 2) == 0)
                    VFinal = V + random.Next(0, 2);
                else
                    VFinal = V - random.Next(0, 2);

                if (dec >= 1)
                    dec -= 1;
            }
        }

        #endregion

        public void LoadContent(GraphicsDeviceManager Graphics, ContentManager Content)
        {
            //tileTextures = TileData.Textures;
            LoadParticles(Graphics, Content);

            minerTexture = Content.Load<Texture2D>("Images//Miner");

            Offset = new Vector2(minerTexture.Width / 2, minerTexture.Height / 2);
        }

        public void LoadParticles(GraphicsDeviceManager Graphics, ContentManager Content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            pressure = 0;
            temperature = 0;

            //Console.WriteLine("Creators: " + creators.Count);
            //Console.WriteLine("Active Blocks: " + activeBlocks.Count);

            CheckActiveChunks(CameraManager.CamerasRead[0].Focus);

            if (creators.Count != 0)
                Creators(gameTime);

            UpdateChunks();

            GetTemperature();
        }

        private void GetTemperature()
        {
            int depth = (int)CameraManager.CamerasRead[0].Focus.Y;
            depth /= 16;

            int graphType;

            //string pascal = "Pa";

            if (depth < SeaLevel)
            {
                pressure = Math.Round(((double)depth / SeaLevel) * AtmosphericPressure, 2);
                temperature = Math.Round(((double)depth / SeaLevel) * surfaceTemperature, 2);
            }
            else
            {
                pressure = Math.Round((((double)(depth - SeaLevel) / ((chunkY * chunkTiles) - SeaLevel)) * LowerMantlePressure) + AtmosphericPressure, 2);
                temperature = Math.Round((((double)(depth - SeaLevel) / ((chunkY * chunkTiles) - SeaLevel)) * LowerMantleTemperature) + surfaceTemperature, 2);
            }

            //if (depth < SeaLevel)
            //{
            //    pressure = ((long)(((float)depth / SeaLevel) * 100) * AtmosphericPressure) / 100;
            //}
            //else
            //{
            //    pressure = (((long)(((float)(depth - SeaLevel) / ((chunkY * chunkTiles) - SeaLevel)) * 100) * LowerMantlePressure) / 100) + AtmosphericPressure;
            //}
            
            //if (pressure >= 1000000000)
            //{
            //    pascal = "GPa";
            //    pressure /= 1000000000;
            //}
            //else if (pressure >= 1000000)
            //{
            //    pascal = "MPa";
            //    pressure /= 1000000;
            //}
            //else if (pressure >= 1000)
            //{
            //    pascal = "kPa";
            //    pressure /= 1000;
            //}
            //else if (pressure >= 100)
            //{
            //    pascal = "hPa";
            //    pressure /= 100;
            //}




            //Console.WriteLine("Temperature: " + temperature + "K");
            ////Console.WriteLine("Pressure: " + pressure + pascal);
            //Console.WriteLine("Pressure: " + pressure + "bar");
        }

        private void InputTests()
        {
            pMState = mState;
            mState = Mouse.GetState();

            pKState = kState;
            kState = Keyboard.GetState();

            //if (mState.LeftButton == ButtonState.Pressed)
            //{
            //    GetTile((int)((mState.X / tileSize) + (CameraManager.CamerasRead[0].Focus.X / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize) + 1), (int)((mState.Y / tileSize) + (CameraManager.CamerasRead[0].Focus.Y / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)));
            //    RemoveBlock(CX, CY, TX, TY);
            //}

            if (kState.IsKeyDown(Keys.L) && pKState.IsKeyUp(Keys.L))
            {
                for (int x = 0; x < chunks.Length; x++)
                {
                    for (int y = 0; y < chunks[x].Length; y++)
                        LoadChunk(x, y);
                }
            }

            if (mState.LeftButton == ButtonState.Pressed)
            {
                GetTile((int)((mState.X / tileSize) + (CameraManager.CamerasRead[0].Focus.X / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize) + 1), (int)((mState.Y / tileSize) + (CameraManager.CamerasRead[0].Focus.Y / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)));
                //RemoveBlock(CX, CY, TX, TY);

                DrawCircle(CX, CY, TX, TY, 4, 0);
            }

            if (mState.RightButton == ButtonState.Pressed)
            {
                GetTile((int)((mState.X / tileSize) + (CameraManager.CamerasRead[0].Focus.X / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize) + 1), (int)((mState.Y / tileSize) + (CameraManager.CamerasRead[0].Focus.Y / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)));
                chunks[CX][CY].tiles[TX][TY].BlockID = 3;
            }
        }

        private void DrawCircle(int cx, int cy, int tx, int ty, int radius, int blockID)
        {
            drawnBlocksChanged = 0;

            if (blockID == 0)
            {
                int ACX = cx;
                int ACY = cy;
                int ATX = tx;
                int ATY = ty;

                int r = radius;
                int ox = ATX, oy = ATY;

                for (int x = -r; x <= r; x++)
                {
                    int height = (int)Math.Sqrt(r * r - x * x);

                    for (int y = -height; y <= height; y++)
                    {
                        GetTile(ACX, ACY, x + ox, y + oy);

                        if (ActiveChunk)
                            if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                            {
                                RemoveBlock(CX, CY, TX, TY, true);
                                drawnBlocksChanged++;
                            }
                    }
                }
            }
            else
            {
                int ACX = cx;
                int ACY = cy;
                int ATX = tx;
                int ATY = ty;

                int r = radius;
                int ox = ATX, oy = ATY;

                for (int x = -r; x <= r; x++)
                {
                    int height = (int)Math.Sqrt(r * r - x * x);

                    for (int y = -height; y <= height; y++)
                    {
                        GetTile(ACX, ACY, x + ox, y + oy);

                        if (ActiveChunk)
                            if (chunks[CX][CY].tiles[TX][TY].BlockID != blockID && !ActiveBlockExists(CX, CY, TX, TY))
                            {
                                ChangeBlock((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, (ushort)blockID, true);
                                drawnBlocksChanged++;
                            }
                    }
                }
            }
        }

        private void CheckActiveChunks(Vector2 pos)
        {
            int x2 = (int)((pos.X - worldPosition.X) / (chunkTiles * tileSize));
            int y2 = (int)((pos.Y - worldPosition.Y) / (chunkTiles * tileSize));

            //Console.WriteLine("Chunk: " + x2 + " " + y2);

            float size = 2f;

            for (int x = x2 - (int)Math.Ceiling(chunksHorizontal * size); x <= x2 + (int)Math.Ceiling(chunksHorizontal * size); x++)
            {
                for (int y = y2 - (int)Math.Ceiling(chunksVertical * size); y <= y2 + (int)Math.Ceiling(chunksVertical * size); y++)
                {
                    int x3 = x;
                    int y3 = y;

                    if (x3 >= chunkX)
                        x3 = x3 - chunkX;
                    if (x3 < 0)
                        x3 = x3 + chunkX;

                    if (y3 >= chunkY)
                        y3 = chunkY - 1;
                    if (y3 < 0)
                        y3 = 0;

                    if (!chunks[x3][y3].ChunkActive)
                        LoadChunk(x3, y3);
                }
            }
        }
        
        private void Creators(GameTime gameTime)
        {
            int min = 0;
            int max = creators.Count;

            if (creators.Count > 50)
            {
                max = random.Next(50, creators.Count);
                min = max - 50;
            }

            for (int i = min; i < max; i++)
            {
                GetTile(creators[i].X, creators[i].Y);
                if (ActiveChunk)
                {
                    switch (creators[i].Behaviour)
                    {
                        #region Miners

                        case 0:
                            MinerCheck(i);
                            
                            creators[i].Update(gameTime);

                            if (creators[i].ChangeBlock)
                            {
                                GetTile(creators[i].X, creators[i].Y);
                                creators[i].ChangeBlock = false;

                                if (ActiveChunk)
                                {
                                    if (creators[i].Size == 1)
                                    {
                                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        {
                                            RemoveBlock(CX, CY, TX, TY, true);
                                            creators[i].TilesChanged++;
                                            chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten++;
                                        }
                                    }
                                    else
                                    {
                                        DrawCircle(CX, CY, TX, TY, creators[i].Size, 0);
                                        creators[i].TilesChanged += drawnBlocksChanged;
                                        chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten += (ushort)drawnBlocksChanged;
                                    }
                                }
                            }

                            if (chunks[creators[i].SChunkX][creators[i].SChunkY].Miners == 1)
                                creators[i].Active = true;

                            if (chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten > chunks[creators[i].SChunkX][creators[i].SChunkY].EdibleTiles)
                            {
                                creators[i].Active = false;
                            }

                            if (creators[i].Spawn && !creators[i].InAir)
                            {
                                AddCreator(creators[i].X,
                                    creators[i].Y,
                                    0,
                                    creators[i].SChunkX,
                                    creators[i].SChunkY,
                                    creatorRandom.Next(100, 250),
                                    0,
                                    creatorRandom.Next(0, 3));

                                creators[i].Spawn = false;
                            }

                            if (!creators[i].Active)
                            {
                                chunks[creators[i].SChunkX][creators[i].SChunkY].Miners--;
                                max--;
                                creators.RemoveAt(i);
                            }

                            break;

                        #endregion

                        #region Biominators

                        case 1:
                            BiominatorCheck(i);
                            
                            creators[i].Update(gameTime);

                            if (creators[i].ChangeBlock)
                            {
                                GetTile(creators[i].X, creators[i].Y);
                                creators[i].ChangeBlock = false;

                                if (ActiveChunk)
                                {
                                    if (creators[i].Size == 1)
                                    {
                                        if (chunks[CX][CY].tiles[TX][TY].BlockID != creators[i].BlockID)
                                        {
                                            ChangeBlock((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, creators[i].BlockID, true);
                                            creators[i].TilesChanged++;
                                            chunks[creators[i].SChunkX][creators[i].SChunkY].TilesBiomed++;
                                        }
                                    }
                                    else
                                    {
                                        DrawCircle(CX, CY, TX, TY, creators[i].Size, creators[i].BlockID);
                                        creators[i].TilesChanged += drawnBlocksChanged;
                                        chunks[creators[i].SChunkX][creators[i].SChunkY].TilesBiomed += (ushort)drawnBlocksChanged;
                                    }
                                }
                            }

                            if (chunks[creators[i].SChunkX][creators[i].SChunkY].Miners == 1)
                                creators[i].Active = true;

                            if (chunks[creators[i].SChunkX][creators[i].SChunkY].TilesBiomed > chunks[creators[i].SChunkX][creators[i].SChunkY].BiomableTiles)
                            {
                                creators[i].Active = false;
                            }

                            if (creators[i].Spawn && !creators[i].InAir)
                            {
                                AddCreator(creators[i].X,
                                    creators[i].Y,
                                    creators[i].BlockID,
                                    creators[i].SChunkX,
                                    creators[i].SChunkY,
                                    creatorRandom.Next(100, 250),
                                    1,
                                    creatorRandom.Next(0, 3));

                                creators[i].Spawn = false;
                            }

                            if (!creators[i].Active)
                            {
                                chunks[creators[i].SChunkX][creators[i].SChunkY].Biominators--;
                                max--;
                                creators.RemoveAt(i);
                            }

                            break;

                        #endregion
                    }

                }
            }
        }

        private void StoneProbabilityTable()
        {
            stoneProb = new List<ushort>();
            stoneID = new List<ushort>();

            //starClassList.Add("O"); starClassListProb.Add(10);
            
            int probability = 0;

            for (int i = 0; i < stoneProb.Count; i++)
            {
                probability += stoneProb[i];
            }

            int selection = creatorRandom.Next(0, probability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < stoneProb.Count; i++)
            {
                counter += stoneProb[i];

                if (selection >= previousCounter && selection <= counter)
                {
                    //starClass = starClassList[i];

                    break;
                }

                previousCounter = counter;
            }
        }

        private void MinerCheck(int i)
        {
            if (CX <= creators[i].SChunkX - 2 || CX >= creators[i].SChunkX + 2 || CY <= creators[i].SChunkY - 2 || CY >= creators[i].SChunkY + 2 || chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten > chunks[creators[i].SChunkX][creators[i].SChunkY].EdibleTiles)
            {
                if (creators[i].SChunkX - 2 < 0 && CX > chunkX - 2)
                {
                }
                else if (creators[i].SChunkX + 2 >= chunkX && CX < 1)
                {
                }
                else
                {
                    creators[i].X = (creators[i].SChunkX * chunkTiles) + (chunkTiles / 2);
                    creators[i].Y = (creators[i].SChunkY * chunkTiles) + (chunkTiles / 2);
                }
            }

            if (creators[i].Active)
            {
                int CreatorX = creators[i].X;
                int CreatorY = creators[i].Y;
                int CreatorSize = creators[i].Size;

                if (CreatorSize == 1)
                    CreatorSize -= 1;


                GetTile(CreatorX, CreatorY - CreatorSize - 1);
                if (ActiveChunk)
                {
                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        creators[i].UpSolid = true;
                    else
                        creators[i].UpSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX + CreatorSize + 1, CreatorY);
                if (ActiveChunk)
                {
                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        creators[i].RightSolid = true;
                    else
                        creators[i].RightSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX, CreatorY + CreatorSize + 1);
                if (ActiveChunk)
                {
                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        creators[i].DownSolid = true;
                    else
                        creators[i].DownSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX - CreatorSize - 1, CreatorY);
                if (ActiveChunk)
                {
                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        creators[i].LeftSolid = true;
                    else
                        creators[i].LeftSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                if (!creators[i].UpSolid && !creators[i].RightSolid && !creators[i].DownSolid && !creators[i].LeftSolid)
                    creators[i].InAir = true;
                else
                    creators[i].InAir = false;
            }
        }
        
        private void BiominatorCheck(int i)
        {
            //if (CX <= creators[i].SChunkX - 2 || CX >= creators[i].SChunkX + 2 || CY <= creators[i].SChunkY - 2 || CY >= creators[i].SChunkY + 2 || chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten > chunks[creators[i].SChunkX][creators[i].SChunkY].EdibleTiles)
            //{
            //    if (creators[i].SChunkX - 2 < 0 && CX > chunkX - 2)
            //    {
            //    }
            //    else if (creators[i].SChunkX + 2 >= chunkX && CX < 1)
            //    {
            //    }
            //    else
            //    {
            //        creators[i].X = (creators[i].SChunkX * chunkTiles) + (chunkTiles / 2);
            //        creators[i].Y = (creators[i].SChunkY * chunkTiles) + (chunkTiles / 2);
            //    }
            //}

            if (creators[i].Active)
            {
                int CreatorX = creators[i].X;
                int CreatorY = creators[i].Y;
                int CreatorSize = creators[i].Size;

                if (CreatorSize == 1)
                    CreatorSize -= 1;

                int blockID;
                
                GetTile(CreatorX, CreatorY - CreatorSize - 1);
                if (ActiveChunk)
                {
                    blockID = chunks[CX][CY].tiles[TX][TY].BlockID;

                    if (blockID != creators[i].BlockID && blockID != 0)
                        creators[i].UpSolid = true;
                    else
                        creators[i].UpSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX + CreatorSize + 1, CreatorY);
                if (ActiveChunk)
                {
                    blockID = chunks[CX][CY].tiles[TX][TY].BlockID;

                    if (blockID != creators[i].BlockID && blockID != 0)
                        creators[i].RightSolid = true;
                    else
                        creators[i].RightSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX, CreatorY + CreatorSize + 1);
                if (ActiveChunk)
                {
                    blockID = chunks[CX][CY].tiles[TX][TY].BlockID;

                    if (blockID != creators[i].BlockID && blockID != 0)
                        creators[i].DownSolid = true;
                    else
                        creators[i].DownSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX - CreatorSize - 1, CreatorY);
                if (ActiveChunk)
                {
                    blockID = chunks[CX][CY].tiles[TX][TY].BlockID;

                    if (blockID != creators[i].BlockID && blockID != 0)
                        creators[i].LeftSolid = true;
                    else
                        creators[i].LeftSolid = false;
                }
                else
                    creators[i].UpSolid = true;

                if (!creators[i].UpSolid && !creators[i].RightSolid && !creators[i].DownSolid && !creators[i].LeftSolid)
                    creators[i].InAir = true;
                else
                    creators[i].InAir = false;
            }
        }
        
        private void AddCreator(int x, int y, ushort blockID, int sChunkX, int sChunkY, int maxEditableTiles, int behaviour, int direction)
        {
            creators.Add(new Creator());
            creators[creators.Count - 1].Initialize(
                x,
                y,
                tileSize,
                maxEditableTiles,
                sChunkX,
                sChunkY,
                creatorRandom.Next(1, creatorMaxSize + 1),
                direction,
                creatorRandom,
                behaviour,
                blockID
                );

            switch (behaviour)
            {
                case 0:
                    chunks[sChunkX][sChunkY].Miners++;
                    break;

                case 1:
                    chunks[sChunkX][sChunkY].Biominators++;
                    break;
            }
        }
                
        private void ChangeBlock(byte cx, byte cy, ushort tx, ushort ty, ushort blockID, bool heal)
        {
            if (ActiveChunk)
                if (chunks[cx][cy].tiles[tx][ty].BlockID != 0)
                {
                    int targetHealth = TileData.BlockHealth[blockID];

                    if (!ActiveBlockExists(cx, cy, tx, ty))
                    {
                        activeBlocks.Add(new ActiveBlocks());
                        activeBlocks[activeBlocks.Count - 1].Initialize((byte)cx, (byte)cy, (ushort)tx, (ushort)ty, chunks[cx][cy].tiles[tx][ty].BlockID);
                        activeBlocks[activeBlocks.Count - 1].ChangeBlock = blockID;
                        activeBlocks[activeBlocks.Count - 1].BlockMaxHealth = targetHealth;
                        if (heal)
                            activeBlocks[activeBlocks.Count - 1].BlockHealth = targetHealth;
                    }
                    else if (activeBlocks[selectedActiveBlock].ChangeBlock == 0)
                    {
                        activeBlocks[selectedActiveBlock].ChangeBlock = blockID;
                        activeBlocks[selectedActiveBlock].BlockMaxHealth = targetHealth;
                        if (heal)
                            activeBlocks[selectedActiveBlock].BlockHealth = targetHealth;
                    }
                }
        }

        private void SetChunkType()
        {
        }

        private void PhaseControl()
        {

        }

        private void Smooth(int cx, int cy)
        {
            int cx2 = cx;
            int cy2 = cy;

            #region Phase

            switch (chunks[cx][cy].SubPhase)
            {
                case 1:
                    break;

                case 2:
                    cx2--;
                    cy2--;
                    break;

                case 3:
                    cy2--;
                    break;

                case 4:
                    cx2++;
                    cy2--;
                    break;

                case 5:
                    cx2--;
                    break;

                case 6:
                    cx2++;
                    break;

                case 7:
                    cx2--;
                    cy2++;
                    break;

                case 8:
                    cy2++;
                    break;

                case 9:
                    cx2++;
                    cy2++;
                    break;
            }

            #endregion

            GetTile(cx2, cy2, 0, 0);

            if (ActiveChunk)
            {
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

                                    if (ActiveChunk)
                                    {
                                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                            i++;
                                    }
                                    else i++;
                                }
                            }
                        }

                        if (i < 5 && cx2 == cx)
                            chunkHolder[x2][y2] = 0;
                        else if (i < 4)
                            chunkHolder[x2][y2] = 0;
                        else
                            chunkHolder[x2][y2] = 1;

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
                                    activeBlocks[activeBlocks.Count - 1].Initialize((byte)cx3, (byte)cy3, (ushort)x2, (ushort)y2, chunks[cx3][cy3].tiles[x2][y2].BlockID);
                                    activeBlocks[activeBlocks.Count - 1].BlockHealth = (activeBlocks[activeBlocks.Count - 1].BlockMaxHealth / 3) - 1;
                                }
                                else if (activeBlocks[selectedActiveBlock].BlockHealth > activeBlocks[selectedActiveBlock].BlockMaxHealth / 3)
                                {
                                    activeBlocks[selectedActiveBlock].BlockHealth = (activeBlocks[selectedActiveBlock].BlockMaxHealth / 3) - 1;
                                }
                            }
                        }                        
                    }
                }
                chunks[cx][cy].SubPhase++;
            }

            if (chunks[cx][cy].SubPhase > 9)
            {
                chunks[cx][cy].Phase++;
                chunks[cx][cy].SubPhase = 1;
            }
        }

        private void Dirtify(int cx, int cy)
        {
            int cx2 = cx;
            int cy2 = cy;

            #region Phase

            switch (chunks[cx][cy].SubPhase)
            {
                case 1:
                    break;

                case 2:
                    cx2--;
                    cy2--;
                    break;

                case 3:
                    cy2--;
                    break;

                case 4:
                    cx2++;
                    cy2--;
                    break;

                case 5:
                    cx2--;
                    break;

                case 6:
                    cx2++;
                    break;

                case 7:
                    cx2--;
                    cy2++;
                    break;

                case 8:
                    cy2++;
                    break;

                case 9:
                    cx2++;
                    cy2++;
                    break;
            }

            #endregion

            GetTile(cx2, cy2, 0, 0);

            if (ActiveChunk)
            {
                int cx3 = CX;
                int cy3 = CY;

                for (int x2 = 0; x2 < chunks[cx3][cy3].tiles.Length; x2++)
                {
                    for (int y2 = 0; y2 < chunks[cx3][cy3].tiles[x2].Length; y2++)
                    {
                        if (chunks[cx3][cy3].tiles[x2][y2].BlockID == 0)
                        {
                            GetTile(cx3, cy3, x2, y2 + 1);

                            if (ActiveChunk)
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

                                        ChangeBlock((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, 1, false);
                                    }
                                }
                        }
                    }
                }
                chunks[cx][cy].SubPhase++;
            }

            if (chunks[cx][cy].SubPhase > 9)
            {
                chunks[cx][cy].Phase++;
                chunks[cx][cy].SubPhase = 1;
            }
        }

        #region Tile Checks

        private void GetTile(int x, int y)
        {
            if (x < 0)
                x += WorldTilesX;
            else if (x >= WorldTilesX)
                x -= WorldTilesX;

            if (y < 0)
                y = 0;
            else if (y >= WorldTilesY)
                y = (WorldTilesY) - 1;

            //CX = (int)Math.Floor(x / (float)chunkTiles);
            //CY = (int)Math.Floor(y / (float)chunkTiles);     
            CX = (int)(x / (float)chunkTiles);
            CY = (int)(y / (float)chunkTiles);     
            TX = x - (CX * chunkTiles);
            TY = y - (CY * chunkTiles);

            if (chunks[CX][CY].ChunkActive)
                ActiveChunk = true;
            else
                ActiveChunk = false;
        }

        //private void GetMinerTile(int x, int y, int i)
        //{
        //    if (x < 0)
        //    {
        //        x += WorldTilesX;
        //        miners[i].X = x;
        //    }
        //    else if (x >= WorldTilesX)
        //    {
        //        x -= WorldTilesX;
        //        miners[i].X = x;
        //    }

        //    if (y < 0)
        //    {
        //        y = 0;
        //        miners[i].Y = y;
        //    }
        //    else if (y >= WorldTilesY)
        //    {
        //        y = (WorldTilesY) - 1;
        //        miners[i].Y = y;
        //    }

        //    CX = (int)(x / (float)chunkTiles);
        //    CY = (int)(y / (float)chunkTiles);
        //    //CX = (int)Math.Floor(x / (float)chunkTiles);
        //    //CY = (int)Math.Floor(y / (float)chunkTiles);
        //    TX = x - (CX * chunkTiles);
        //    TY = y - (CY * chunkTiles);
            
        //    if (chunks[CX][CY].ChunkActive)
        //        ActiveChunk = true;
        //    else
        //        ActiveChunk = false;
        //}

        //private void GetBiominatorTile(int x, int y, int i)
        //{
        //    if (x < 0)
        //    {
        //        x += WorldTilesX;
        //        biominators[i].X = x;
        //    }
        //    else if (x >= WorldTilesX)
        //    {
        //        x -= WorldTilesX;
        //        biominators[i].X = x;
        //    }

        //    if (y < 0)
        //    {
        //        y = 0;
        //        biominators[i].Y = y;
        //    }
        //    else if (y >= WorldTilesY)
        //    {
        //        y = (WorldTilesY) - 1;
        //        biominators[i].Y = y;
        //    }

        //    //CX = (int)Math.Floor(x / (float)chunkTiles);
        //    //CY = (int)Math.Floor(y / (float)chunkTiles);
        //    CX = (int)(x / (float)chunkTiles);
        //    CY = (int)(y / (float)chunkTiles);
        //    TX = x - (CX * chunkTiles);
        //    TY = y - (CY * chunkTiles);

        //    if (chunks[CX][CY].ChunkActive)
        //        ActiveChunk = true;
        //    else
        //        ActiveChunk = false;
        //}

        private void GetTile(int cx, int cy, int tx, int ty)
        {
            if (tx < 0)
            {
                cx--;
                tx += chunkTiles;
            }
            else if (tx >= chunkTiles)
            {
                cx++;
                tx -= chunkTiles;
            }

            if (ty < 0)
            {
                cy--;
                ty += chunkTiles;
            }
            else if (ty >= chunkTiles)
            {
                cy++;
                ty -= chunkTiles;
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
            
            if (chunks[CX][CY].ChunkActive)
                ActiveChunk = true;
            else
                ActiveChunk = false;
        }

        //ushort selectedTile;

        public void RemoveBlock(int cx, int cy, int tx, int ty, bool fast)
        {           

            if (chunks[cx][cy].ChunkActive)
            {
                //selectedTile = chunks[cx][cy].tiles[tx][ty].BlockID;

                //if (selectedTile != 0)
                {
                    drawnBlocksChanged++;
                    chunks[cx][cy].tiles[tx][ty].BlockID = 0;

                    if (ActiveBlockExists(cx, cy, tx, ty))
                        activeBlocks.RemoveAt(selectedActiveBlock);

                    if (!fast)
                    {
                        int CX0 = cx;
                        int CY0 = cy;
                        int TX0 = tx;
                        int TY0 = ty;

                        for (int x = -2; x <= 2; x++)
                        {
                            for (int y = -2; y <= 2; y++)
                            {
                                GetTile(CX0, CY0, TX0 + x, TY0 + y);

                                if (ActiveChunk)
                                {
                                    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                    {
                                        int CX2 = CX;
                                        int CY2 = CY;
                                        int TX2 = TX;
                                        int TY2 = TY;

                                        int i = 0;

                                        GetTile(CX2, CY2, TX2, TY2 - 1);
                                        if (ActiveChunk)
                                        {
                                            if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                                i++;
                                        }
                                        else i++;


                                        GetTile(CX2, CY2, TX2 + 1, TY2);
                                        if (ActiveChunk)
                                        {
                                            if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                                i++;
                                        }
                                        else i++;

                                        GetTile(CX2, CY2, TX2, TY2 + 1);
                                        if (ActiveChunk)
                                        {
                                            if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                                i++;
                                        }
                                        else i++;

                                        GetTile(CX2, CY2, TX2 - 1, TY2);
                                        if (ActiveChunk)
                                        {
                                            if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                                i++;
                                        }
                                        else i++;


                                        if (i == 0)
                                            if (!ActiveBlockExists(CX2, CY2, TX2, TY2))
                                            {
                                                activeBlocks.Add(new ActiveBlocks());
                                                activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                                activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX2, (byte)CY2, (ushort)TX2, (ushort)TY2, chunks[CX2][CY2].tiles[TX2][TY2].BlockID);
                                            }
                                            else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                                            {
                                                activeBlocks[selectedActiveBlock].BlockFalling = true;
                                            }
                                    }
                                }
                            }
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
                    if (ActiveChunk)
                        if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                        {
                            int CX2 = CX;
                            int CY2 = CY;
                            int TX2 = TX;
                            int TY2 = TY;

                            int i = 0;

                            GetTile(CX2, CY2, TX2, TY2 - 1);
                            if (ActiveChunk)
                            {
                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                    i++;
                            }
                            else i++;

                            GetTile(CX2, CY2, TX2 + 1, TY2);
                            if (ActiveChunk)
                            {
                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                    i++;
                            }
                            else i++;

                            GetTile(CX2, CY2, TX2, TY2 + 1);
                            if (ActiveChunk)
                            {
                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                    i++;
                            }
                            else i++;

                            GetTile(CX2, CY2, TX2 - 1, TY2);
                            if (ActiveChunk)
                            {
                                if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                    i++;
                            }
                            else i++;

                            if (i == 0)
                                if (!ActiveBlockExists(CX2, CY2, TX2, TY2))
                                {
                                    activeBlocks.Add(new ActiveBlocks());
                                    activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                    activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX2, (byte)CY2, (ushort)TX2, (ushort)TY2, chunks[CX2][CY2].tiles[TX2][TY2].BlockID);
                                }
                                else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                                {
                                    activeBlocks[selectedActiveBlock].BlockFalling = true;
                                }
                        }
                }
            }
        }

        #endregion

        private void UpdateChunks()
        {
            #region Update Active Blocks

            if (activeBlocks.Count != 0)
            {

                int min = 0;
                int max = activeBlocks.Count;

                if (activeBlocks.Count > 1000)
                {
                    max = random.Next(1000, activeBlocks.Count);
                    min = max - 1000;
                }

                int speed;

                if (activeBlocks.Count > 2000)
                    speed = 3;
                else if (activeBlocks.Count > 1000)
                    speed = 2;
                else
                    speed = 1;

                for (int i = activeBlocks.Count - 1; i >= 0; i--)
                {
                    bool removeBlock = false;

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

                            if (ActiveChunk)
                            {
                                if (chunks[CX][CY].tiles[TX][TY].BlockID == 0)
                                {
                                    if (!ActiveBlockExists(CX, CY, TX, TY))
                                    {
                                        activeBlocks.Add(new ActiveBlocks());
                                        CopyBlockInfo(i, activeBlocks.Count - 1, (byte)CX, (byte)CY, (ushort)TX, (ushort)TY);
                                        activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                        //activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, chunks[CX][CY].tiles[TX][TY].BlockID);
                                    }
                                    else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                                    {
                                        CopyBlockInfo(i, selectedActiveBlock, (byte)CX, (byte)CY, (ushort)TX, (ushort)TY);
                                        activeBlocks[selectedActiveBlock].BlockFalling = true;
                                    }

                                    chunks[CX][CY].tiles[TX][TY].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                                    chunks[ACX][ACY].tiles[ATX][ATY].BlockID = 0;
                                    activeBlocks[i].BlockFalling = false;
                                }
                                else
                                {
                                    if (activeBlocks[i].BlockFalling)
                                        activeBlocks[i].BlockFalling = false;

                                    CheckBlock(ACX, ACY, ATX, ATY);
                                }
                            }
                            else
                            {
                                if (activeBlocks[i].BlockFalling)
                                    activeBlocks[i].BlockFalling = false;

                                activeBlocks[i].BlockHealth = 0;
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
                                switch (speed)
                                {
                                    case 1:
                                        if (random.Next(0, 25) == 0)
                                        {
                                            chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
                                            activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                                            activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
                                            activeBlocks[i].ChangeBlock = 0;
                                        }
                                        break;

                                    case 2:
                                        if (random.Next(0, 10) == 0)
                                        {
                                            chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
                                            activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                                            activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
                                            activeBlocks[i].ChangeBlock = 0;
                                        }
                                        break;

                                    case 3:
                                        chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
                                        activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
                                        activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
                                        activeBlocks[i].ChangeBlock = 0;
                                        break;
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
                                removeBlock = true;
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
                            switch (speed)
                            {
                                case 1:
                                    if (random.Next(0, 25) == 0)
                                        activeBlocks[i].BlockHealth -= random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
                                    break;

                                case 2:
                                    if (random.Next(0, 10) == 0)
                                        activeBlocks[i].BlockHealth -= random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
                                    break;

                                case 3:
                                    activeBlocks[i].BlockHealth = 0;
                                    break;
                            }

                        #endregion

                        #region Regenerating Block

                        if (activeBlocks[i].BlockHealing)
                            switch (speed)
                            {
                                case 1:
                                    if (random.Next(0, 25) == 0)
                                        activeBlocks[i].BlockHealth += random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
                                    break;

                                case 2:
                                    if (random.Next(0, 10) == 0)
                                        activeBlocks[i].BlockHealth += random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
                                    break;

                                case 3:
                                    activeBlocks[i].BlockHealth = activeBlocks[i].BlockMaxHealth;
                                    break;
                            }

                        #endregion
                    }

                    if (removeBlock)
                        RemoveBlock(activeBlocks[i].CX, activeBlocks[i].CY, activeBlocks[i].TX, activeBlocks[i].TY, false);
                    else if (!CheckActiveBlock(i))
                        activeBlocks.RemoveAt(i);
                    else
                        chunks[activeBlocks[i].CX][activeBlocks[i].CY].ActiveBlocks++;
                }
            }

            #endregion

            #region Update Chunk

            bool ChunkSelect = true;

            int x = 0;
            int y = 0;

            if (random.Next(0, 2) == 0)
            {
                while (ChunkSelect)
                {
                    x = random.Next(0, chunkX);
                    y = random.Next(0, chunkY);

                    if (chunks[x][y].ChunkActive)
                        ChunkSelect = false;
                }
            }
            else
            {
                if (RenderChunkX > RenderChunkX2)
                {
                    RenderChunkX = 0;
                    RenderChunkX2 = 0;
                }

                if (RenderChunkY > RenderChunkY2)
                {
                    RenderChunkY = 0;
                    RenderChunkY2 = 0;
                }

                x = random.Next(RenderChunkX, RenderChunkX2 + 1);
                y = random.Next(RenderChunkY, RenderChunkY2 + 1);
            }

            if (chunks[x][y].ChunkActive)
            {
                if (chunks[x][y].Miners == 0 && chunks[x][y].Phase == 1)
                    chunks[x][y].Phase++;

                //if (chunks[x][y].Phase == 2 && !chunkControl && chunks[x][y].ActiveBlocks <= 64)
                //{
                //    Dirtify(x, y);
                //    chunkControl = true;
                //}

                if (chunks[x][y].Phase == 2 && !chunkControl)
                {
                    Smooth(x, y);
                    chunkControl = true;
                }

                chunks[x][y].ActiveBlocks = 0;
            }

            chunkControl = false;

            #endregion
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

        private void CopyBlockInfo(int i, int r, byte cx, byte cy, ushort tx, ushort ty)
        {
            activeBlocks[r].BlockHealth = activeBlocks[i].BlockHealth;
            activeBlocks[r].BlockDecaying = activeBlocks[i].BlockDecaying;
            activeBlocks[r].BlockFalling = activeBlocks[i].BlockFalling;
            activeBlocks[r].BlockHealing = activeBlocks[i].BlockHealing;
            activeBlocks[r].BlockMaxHealth = activeBlocks[i].BlockMaxHealth;
            activeBlocks[r].ChangeBlock = activeBlocks[i].ChangeBlock;
            activeBlocks[r].CX = cx;
            activeBlocks[r].CY = cy;
            activeBlocks[r].TX = tx;
            activeBlocks[r].TY = ty;
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
                            if (chunks[x][y].ChunkActive)
                            {    
                                if ((x * chunkTiles) + x2 > RenderTileX && (x * chunkTiles) + x2 < RenderTileX2 &&
                                    (y * chunkTiles) + y2 > RenderTileY && (y * chunkTiles) + y2 < RenderTileY2)
                                {
                                    int ID = chunks[x][y].tiles[x2][y2].BlockID;
                                    //Texture2D tex = ;


                                    if (ID != 0)
                                    {
                                        position.X = worldPosition.X + (x2 * tileSize) + (x * (chunkTiles * tileSize));
                                        position.Y = worldPosition.Y + (y2 * tileSize) + (y * (chunkTiles * tileSize));

                                        spriteBatch.Draw(TileData.BlockSheets[0], position, TileData.Rectangles[ID], TileData.BlockColor[ID], 0f, new Vector2(8,8), TileData.SizeModifier, SpriteEffects.None, 0f);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < creators.Count; i++)
            {
                if (creators[i].X > RenderTileX && creators[i].X < RenderTileX2 && creators[i].Y > RenderTileY && creators[i].Y < RenderTileY2)
                {
                    position.X = worldPosition.X + (creators[i].X * tileSize) + (minerTexture.Width / 2f) - (tileSize / 2);
                    position.Y = worldPosition.Y + (creators[i].Y * tileSize) + (minerTexture.Height / 2f) - (tileSize / 2);

                    switch (creators[i].Behaviour)
                    {
                        case 0:
                            spriteBatch.Draw(minerTexture, position, null, Color.White, 0f, Offset, creators[i].Size + (creators[i].Size - 1), SpriteEffects.None, 0f);
                            break;

                        case 1:
                            spriteBatch.Draw(minerTexture, position, null, Color.Green, 0f, Offset, creators[i].Size + (creators[i].Size - 1), SpriteEffects.None, 0f);
                            break;
                    }
                }
            }

            spriteBatch.Draw(TileData.BlockSheets[0], Vector2.Zero, Color.White);
        }
    }
}
