using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using SimplexNoise;
using Bitmap = System.Drawing.Bitmap;
using BMPColor = System.Drawing.Color;


namespace BackgroundTest
{
    class RenderBlocks
    {
        public Vector2 Position;
        public Vector2 TextureIndex;
        public Rectangle TextureRectangle;
        public Color Color;
        public float BlockRotation;

        public void Initialize(Vector2 position, Color color, float blockRotation, Vector2 textureIndex, Rectangle textureRectangle)
        {
            Position = position;
            Color = color;
            BlockRotation = blockRotation;
            TextureIndex = textureIndex;
            TextureRectangle = textureRectangle;
        }
    }

    class RenderSlopes
    {
        public Vector2 Position;
        public Vector2 TextureIndex;
        public Rectangle TextureRectangle;
        public Color Color;

        public void Initialize(Vector2 position, Color color, Vector2 textureIndex, Rectangle textureRectangle)
        {
            Position = position;
            Color = color;
            TextureIndex = textureIndex;
            TextureRectangle = textureRectangle;
        }
    }

    class ActiveBlocks
    {
        public ushort TX;
        public ushort TY;

        public int BlockHealth = 0;
        public int BlockMaxHealth;
        public int PreviousHealth;

        public bool BlockFalling;
        public bool BlockHealing;
        public bool BlockDecaying;

        public ushort BlockID;

        public void Initialize(ushort tx, ushort ty, ushort blockID)
        {
            TX = tx;
            TY = ty;
            BlockID = blockID;

            if (BlockHealth == 0)
            {
                BlockHealth = TileData.BlockHealth[BlockID];
                BlockMaxHealth = BlockHealth;
                PreviousHealth = BlockHealth;
            }
        }
    }

    struct TileInfo
    {
        //public byte ColorR;
        //public byte ColorG;
        //public byte ColorB;

        public Color BlockColor;

        public byte variation;
        public byte SlopeMask;
        public byte BlockMask;
        public byte rotation;
        public float textureRotation;
        public byte Deco;

        public void SetData(ushort ID, Random rand)
        {
            BlockColor = TileData.BlockColor[ID];
            variation = (byte)rand.Next(0, TileData.BlockTextureIDs[ID].Count);
            SlopeMask = (byte)rand.Next(0, TileData.SlopeMaskIDs[ID].Count);
            BlockMask = (byte)rand.Next(0, TileData.BlockMaskIDs[ID].Count);
            Deco = (byte)rand.Next(0, TileData.DecoTextureIDs[ID].Count);
            rotation = (byte)rand.Next(0, 4);
            textureRotation = (float)(rand.NextDouble() * MathHelper.TwoPi);
        }
    }

    struct Tiles
    {
        public ushort BlockID;
        //public ushort Furniture;
        //public ushort Background;
        //public ushort Wire;
        //public ushort Foreground;

        public bool RemoveBlock()
        {
            if (BlockID != 0)
            {
                BlockID = 0;

                return true;
            }
            else
                return false;
        }

        public bool ChangeBlock(ushort ID)
        {
            if (BlockID != ID && BlockID != 0)
            {
                BlockID = ID;

                return true;
            }
            else
                return false;
        }
    }

    struct Chunk
    {
        public Tiles[][] tiles;
        public Tiles[][] virtualTiles;
        public TileInfo[][] tileInfo;
        public List<ActiveBlocks> activeTiles;

        public ushort EdibleTiles;
        public ushort TilesEaten;
        public int BiomableTiles;
        public int TilesBiomed;
        public int SelectedActiveBlock;
        public ushort Miners;
        public ushort Biominators;
        public byte Phase;
        public byte SubPhase;
        public ushort ActiveBlocks;
        public bool ChunkActive;
        public bool VirtualChunkCreated;
        public byte ChunkType;
        public ushort ChunkSize;

        #region Tile Info

        public void SetTileInfo(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                tileInfo = new TileInfo[ChunkSize][];

                for (int x = 0; x < tiles.Length; x++)
                {
                    tileInfo[x] = new TileInfo[ChunkSize];

                    for (int y = 0; y < tileInfo.Length; y++)
                    {
                        tileInfo[x][y].SetData(tiles[x][y].BlockID, rand);
                    }
                }
            }
        }

        public void RemoveTileInfo(int tx, int ty)
        {
            tileInfo = null;
        }

        public Color GetTileColor(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo == null)
                    SetTileInfo(tx, ty, rand);

                return tileInfo[tx][ty].BlockColor;
            }

            return Color.White;
        }

        public byte GetVariation(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo == null)
                    SetTileInfo(tx, ty, rand);

                return tileInfo[tx][ty].variation;
            }

            return 0;
        }

        public byte GetSlopeMask(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo == null)
                    SetTileInfo(tx, ty, rand);

                return tileInfo[tx][ty].SlopeMask;
            }

            return 0;
        }

        public byte GetBlockMask(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo == null)
                    SetTileInfo(tx, ty, rand);

                return tileInfo[tx][ty].BlockMask;
            }

            return 0;
        }

        public byte GetRotation(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo == null)
                    SetTileInfo(tx, ty, rand);

                return tileInfo[tx][ty].rotation;
            }

            return 0;
        }

        public byte GetDeco(int tx, int ty, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo == null)
                    SetTileInfo(tx, ty, rand);

                return tileInfo[tx][ty].Deco;
            }

            return 0;
        }

        #endregion

        public bool CheckForActive(int tx, int ty)
        {
            for (int i = 0; i < activeTiles.Count; i++)
            {
                if (activeTiles[i].TX == tx)
                    if (activeTiles[i].TY == ty)
                    {
                        SelectedActiveBlock = i;
                        return true;
                    }
            }

            return false;
        }

        public bool CheckForBlock(int tx, int ty, int TileID)
        {
            if (ChunkActive)
            {
                if (tiles[tx][ty].BlockID == TileID)
                    return true;

                return false;
            }
            else
            {
                if (!VirtualChunkCreated)
                    CreateVirtualChunk();

                if (virtualTiles[tx][ty].BlockID == TileID)
                    return true;

                return false;
            }
        }

        public bool RemoveBlock(int tx, int ty)
        {
            if (ChunkActive)
                return tiles[tx][ty].RemoveBlock();
            else
            {
                if (!VirtualChunkCreated)
                    CreateVirtualChunk();

                return virtualTiles[tx][ty].RemoveBlock();
            }
        }

        public bool ChangeBlock(int tx, int ty, ushort ID, Random rand)
        {
            if (ChunkActive)
            {
                if (tileInfo != null)
                    tileInfo[tx][ty].SetData(ID, rand);

                return tiles[tx][ty].ChangeBlock(ID);
            }
            else
            {
                if (!VirtualChunkCreated)
                    CreateVirtualChunk();

                return virtualTiles[tx][ty].ChangeBlock(ID);
            }
        }

        public void CreateVirtualChunk()
        {
            virtualTiles = new Tiles[ChunkSize][];

            for (int x = 0; x < virtualTiles.Length; x++)
            {
                virtualTiles[x] = new Tiles[ChunkSize];

                for (int y = 0; y < virtualTiles.Length; y++)
                {
                    virtualTiles[x][y].BlockID = 65535;
                }
            }

            VirtualChunkCreated = true;
        }

        public void LoadChunk(ushort ChunkSize, ushort LandID)
        {
            tiles = new Tiles[ChunkSize][];

            if (!VirtualChunkCreated)
            {
                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        tiles[x][y].BlockID = LandID;
                    }
                }
            }
            else
            {
                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        if (virtualTiles[x][y].BlockID != 65535)
                        {
                            tiles[x][y].BlockID = virtualTiles[x][y].BlockID;
                        }
                        else
                        {
                            tiles[x][y].BlockID = LandID;
                        }
                    }
                }

                virtualTiles = null;
                VirtualChunkCreated = false;
            }

            ChunkActive = true;
        }

        public void LoadChunk(ushort ChunkSize, ushort LandID, ushort LandID2, Random rand)
        {
            tiles = new Tiles[ChunkSize][];

            if (!VirtualChunkCreated)
            {
                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        int variance = rand.Next(0, ChunkSize / 2) - (ChunkSize / 4);

                        ushort BlockID;

                        if (y > (ChunkSize / 2) + variance)
                            BlockID = LandID;
                        else
                            BlockID = LandID2;

                        tiles[x][y].BlockID = BlockID;
                    }
                }
            }
            else
            {
                for (int x = 0; x < tiles.Length; x++)
                {
                    tiles[x] = new Tiles[ChunkSize];

                    for (int y = 0; y < tiles.Length; y++)
                    {
                        if (virtualTiles[x][y].BlockID != 65535)
                        {
                            tiles[x][y].BlockID = virtualTiles[x][y].BlockID;
                        }
                        else
                        {
                            int variance = rand.Next(0, ChunkSize / 2) - (ChunkSize / 4);

                            ushort BlockID;

                            if (y > (ChunkSize / 2) + variance)
                                BlockID = LandID;
                            else
                                BlockID = LandID2;

                            tiles[x][y].BlockID = BlockID;
                        }
                    }
                }

                virtualTiles = null;
                VirtualChunkCreated = false;
            }

            ChunkActive = true;
        }

        public void LoadInfo(ushort edibleTiles, ushort biomableTiles)
        {
            EdibleTiles = edibleTiles;
            BiomableTiles = biomableTiles;
        }

        public void Initialize(ushort chunkSize)
        {
            ChunkSize = chunkSize;
            ChunkActive = false;
            VirtualChunkCreated = false;
            Miners = 0;
            Phase = 0;
            SubPhase = 0;
            ActiveBlocks = 0;
            activeTiles = new List<ActiveBlocks>();
        }
    }

    class Creator
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

    class RemovingBlocks
    {
        public int CX;
        public int CY;
        public int TX;
        public int TY;
        public bool fast;

        public void Initialize(int cx, int cy, int tx, int ty)
        {
            CX = cx;
            CY = cy;
            TX = tx;
            TY = ty;
        }

        public bool CheckBlock(byte cx, byte cy, ushort tx, ushort ty)
        {
            if (CX == cx)
                if (CY == cy)
                    if (TX == tx)
                        if (TY == ty)
                            return true;

            return false;
        }
    }

    //Console.WriteLine(StopWatch.Time());

    class World
    {
        #region variables

        Chunk[][] chunks;      

        List<Creator> creators;

        Texture2D minerTexture;

        int loadedChunksX = 0;
        int loadedChunksY = 0;
        bool loadWholeWorld = false;

        ushort LandID;

        byte tileSize;
        byte chunkTiles;

        byte chunkX;
        byte chunkY;

        float chunksHorizontal;
        float chunksVertical;

        int creatorMaxSize;

        int CX;
        int CY;
        int TX;
        int TY;
        bool ActiveChunk;


        int drawnBlocksChanged;
        
        int ActiveChunks;

        ushort[][] chunkHolder;

        
        Vector2 Offset;
        Vector2 worldPosition;
        //List<Texture2D> tileTextures;
        //List<ActiveBlocks> activeBlocks;
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

        List<Texture2D> drawingTexture;
        
        Texture2D testStencil;
        Texture2D testSlopeStencil;
        
        Matrix m;
        AlphaTestEffect a;
        DepthStencilState s1;
        DepthStencilState s2;

        List<RenderBlocks> renderBlocks;
        List<RenderSlopes> renderSlopes;
        Vector2 BlockOrigin;
        Vector2 SlopeOrigin;
        RenderTarget2D[][] BlockRenderTargets;
        RenderTarget2D[][] SlopeRenderTargets;
        int CurrentRenderChunk;
        int RenderChunksX;
        int RenderChunksY;

        int TextureSize;
        int HalfTextureSize;
        int QuarterTextureSize;
        int TilesPerChunk;

        List<RemovingBlocks> removingBlocks;

        #endregion
        
        public void Initialize()
        {
            ActiveChunks = 0;

            pressure = 0;
            temperature = 0;

            tileSize = WorldVariables.TileSize;
            chunkTiles = WorldVariables.ChunkSize;

            TilesPerChunk = chunkTiles * chunkTiles;

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

            LandID = 1;

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
            creatorMaxSize = 4;

            //activeBlocks = new List<ActiveBlocks>();
            removingBlocks = new List<RemovingBlocks>();

            //SetHillData();

            chunkHolder = new ushort[chunkTiles][];

            for (int a = 0; a < chunkHolder.Length; a++)
                chunkHolder[a] = new ushort[chunkTiles];

            chunks = new Chunk[chunkX][];

            for (int x = 0; x < chunks.Length; x++)
            {
                chunks[x] = new Chunk[chunkY];

                for (int y = 0; y < chunks.Length; y++)
                    chunks[x][y].Initialize(chunkTiles);
            }
        }
        
        #region Land

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
        //            leftHill[h] = rightHill[h - 1];
        //        }
        //        else
        //        {
        //            if (LandType == 0)
        //            {
        //                TerrainHeight = random.Next(0, MaxHeight / 3);
        //                TerrainWidth = random.Next(2, MaxWidth / 2);
        //                TerrainWidth *= 2;
        //                i = 0;
        //                if (random.Next(0, 2) == 0)
        //                    Inverted = true;
        //                else
        //                    Inverted = false;
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

        //private void CreateHills(int x, int y)
        //{
        //    int LeftDepth = leftHill[x];
        //    int RightDepth = rightHill[x];
        //    int Difference = Math.Abs(LeftDepth - RightDepth);
        //    bool up;

        //    int HighPoint;
        //    int LowPoint;

        //    if (LeftDepth > RightDepth)
        //    {
        //        HighPoint = RightDepth;
        //        LowPoint = LeftDepth;
        //    }
        //    else
        //    {
        //        HighPoint = LeftDepth;
        //        LowPoint = RightDepth;
        //    }

        //    HighPoint = (int)((float)HighPoint / chunkTiles) * chunkTiles;

        //    if (LeftDepth > RightDepth)
        //        up = true;
        //    else
        //        up = false;

        //    int V = LeftDepth;
        //    int VFinal = V;
        //    float increase = (float)Difference / (float)chunkTiles;
        //    float dec = 0;

        //    for (int x2 = 0; x2 < chunkTiles; x2++)
        //    {
        //        for (int y2 = HighPoint; y2 <= LowPoint; y2++)
        //        {
        //            GetTile((x * chunkTiles) + x2, y2);
                    
        //            if (!chunks[CX][CY].ChunkActive)
        //            {
        //                chunks[CX][CY].LoadChunk(chunkTiles, 1);
        //            }

        //            if (y2 < VFinal)
        //                chunks[CX][CY].tiles[TX][TY].BlockID = 0;
        //        }

        //        //int increase = random.Next(0, (int)((Difference / 48f) + 2));

        //        dec += increase - (int)increase;


        //        if (up)
        //        {
        //            V -= (int)increase;

        //            if (dec >= 1)
        //                V--;
        //        }
        //        else
        //        {
        //            V += (int)increase;

        //            if (dec >= 1)
        //                V++;
        //        }

        //        if (random.Next(0, 2) == 0)
        //            VFinal = V + random.Next(0, 2);
        //        else
        //            VFinal = V - random.Next(0, 2);

        //        if (dec >= 1)
        //            dec -= 1;
        //    }

        //    //if (chunks[x][y].ChunkActive)
        //    //    for (int x2 = 0; x2 < chunks[x][y].tiles.Length; x2++)
        //    //    {
        //    //        for (int y2 = 0; y2 < chunks[x][y].tiles[x2].Length; y2++)
        //    //        {
        //    //            chunks[x][y].tiles[x2][y2].variation = (byte)random.Next(0, TileData.BlockTextureIDs[chunks[x][y].tiles[x2][y2].BlockID].Count);
        //    //        }
        //    //    }
        //}

        #endregion

        public float FindBetween(int pos, int top, int bottom, float max, float min, bool reverse)
        {
            int diff = bottom - top;
            float value = ((1 - ((pos - top) / (float)diff)) * (max - min)) + min;

            if (reverse)
                value = (((pos - top) / (float)diff) * (max - min)) + min;
            else
                value = ((1 - ((pos - top) / (float)diff)) * (max - min)) + min;

            if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }

        private void LoadChunk(int x, int y)
        {
            float caveMultiplier = FindBetween(y, (SeaLevel / chunkTiles), (chunkY - 3), 0.7f, 0.05f, false);
            float biomeMultiplier = FindBetween(y, (SeaLevel / chunkTiles), (chunkY - 3), 5f, 0.7f, true);

            int minerChance = (int)FindBetween(y, (SeaLevel / chunkTiles), (chunkY - 3), 100f, 55f, true); ;

            if (y > chunkY - 3)
                minerChance = 0;

            float caveSize = TilesPerChunk * caveMultiplier;
            float biomeSize = TilesPerChunk * biomeMultiplier;

            ushort RockID = TileData.GetRockID();

            if (y * chunkTiles > SeaLevel)
            {
                if (y * chunkTiles > SeaLevel)
                {
                    chunks[x][y].LoadInfo((ushort)(caveSize), (ushort)(biomeSize));
                    chunks[x][y].LoadChunk(chunkTiles, LandID);
                }
                else
                {
                    chunks[x][y].LoadInfo((ushort)(caveSize), (ushort)(biomeSize));
                    chunks[x][y].LoadChunk(chunkTiles, LandID, 1, random);
                }

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

                AddCreator(
                    creatorRandom.Next(0, chunkTiles) + (x * chunkTiles),
                    creatorRandom.Next(0, chunkTiles) + (y * chunkTiles),
                    RockID,
                    x,
                    y,
                    creatorRandom.Next(150, 250),
                    1,
                    creatorRandom.Next(0, 3));
            }
            else
            {
                #region OLD HILLS

                // if (!chunks[x][y].ChunkActive)
                // {
                //     CreateHills(x, y);

                //     int hillBotton;
                //     int hillTop;

                //     if (leftHill[x] > rightHill[x])
                //     {
                //         hillBotton = leftHill[x];
                //         hillTop = rightHill[x];
                //     }
                //     else
                //     {
                //         hillBotton = rightHill[x];
                //         hillTop = leftHill[x];
                //     }

                //     GetTile(x * chunkTiles, hillBotton);

                //     if (y > CY)
                //         if (!chunks[x][y].ChunkActive)
                //         {
                //             chunks[x][y].LoadChunk(chunkTiles, 1);

                //             if (creatorRandom.Next(0, 100) < 10)
                //             {
                //                 AddCreator(
                //                     creatorRandom.Next(0, chunkTiles) + (x * chunkTiles),
                //                     creatorRandom.Next(0, chunkTiles) + (y * chunkTiles),
                //                     0,
                //                     x,
                //                     y,
                //                     creatorRandom.Next(150, 250),
                //                     0,
                //                     creatorRandom.Next(0, 3));
                //             }
                //         }

                //     //GetTile(x * chunkTiles, hillTop);

                //     //if (y < CY)
                //     //    if (!chunks[x][y].ChunkActive)
                //     //        chunks[x][y].LoadLand(chunkTiles, 6);
                //} 

                #endregion
            }
        }

        public void LoadContent(GraphicsDeviceManager Graphics, ContentManager Content, GraphicsDevice graphicsDevice)
        {
            //tileTextures = TileData.Textures;
            LoadParticles(Graphics, Content);

            minerTexture = Content.Load<Texture2D>("Images//Miner");
                                  
            Offset = new Vector2(minerTexture.Width / 2, minerTexture.Height / 2);

            LoadStencils(Content, graphicsDevice);
        }

        private void LoadStencils(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            drawingTexture = new List<Texture2D>();
            renderBlocks = new List<RenderBlocks>();
            renderSlopes = new List<RenderSlopes>();

            //Change this
            TextureSize = 32;
            HalfTextureSize = TextureSize / 2;
            QuarterTextureSize = TextureSize / 4;

            BlockOrigin = new Vector2(HalfTextureSize, HalfTextureSize);
            SlopeOrigin = new Vector2(QuarterTextureSize, QuarterTextureSize);

            int RenderTargetX = TextureSize * chunkTiles;
            int RenderTargetY = TextureSize * chunkTiles;

            WorldVariables.SetCameraSize(chunkTiles * tileSize);

            BlockRenderTargets = new RenderTarget2D[WorldVariables.ChunksHorizontal][];
            SlopeRenderTargets = new RenderTarget2D[WorldVariables.ChunksHorizontal][];
            
            for (int i = 0; i < BlockRenderTargets.Length; i++)
            {
                BlockRenderTargets[i] = new RenderTarget2D[WorldVariables.ChunksVertical];
                SlopeRenderTargets[i] = new RenderTarget2D[WorldVariables.ChunksVertical];
            }

            for (int x = 0; x < BlockRenderTargets.Length; x++)
                for (int y = 0; y < BlockRenderTargets[x].Length; y++)
                {
                    BlockRenderTargets[x][y] = new RenderTarget2D(graphicsDevice, RenderTargetX, RenderTargetY, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
                    SlopeRenderTargets[x][y] = new RenderTarget2D(graphicsDevice, RenderTargetX, RenderTargetY, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);                    
                }
            
            // load assets
            testSlopeStencil = Content.Load<Texture2D>("Images//Blocks//TestSlopeMask");
            testStencil = Content.Load<Texture2D>("Images//Blocks//TestMask");

            m = Matrix.CreateOrthographicOffCenter(0, RenderTargetX, RenderTargetY, 0, 0, 1);

            a = new AlphaTestEffect(graphicsDevice)
            {
                Projection = m
            };

            s1 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };

            s2 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.LessEqual,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };
        }

        public void LoadParticles(GraphicsDeviceManager Graphics, ContentManager Content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            pressure = 0;
            temperature = 0;

            Console.WriteLine("Creators: " + creators.Count);
            //Console.WriteLine("Active Blocks: " + activeBlocks.Count);
            Console.WriteLine("Removing Blocks: " + removingBlocks.Count);

            ActiveChunks = 0;
            //for (int x = 0; x < chunks.Length; x++)
            //    for (int y = 0; y < chunks[0].Length; y++)
            //    {
            //        //Count chunks as loaded, rather than per update.

            //        if (chunks[x][y].ChunkActive)
            //            ActiveChunks++;
            //    }
            
            Console.WriteLine("Active Chunks: " + ActiveChunks + "/" + (chunks.Length * chunks[0].Length));

            if (creators.Count != 0)
                Creators(gameTime);
            
            UpdateChunks();
            InputTests();
            GetTemperature();

            RemoveTaggedBlocks();

            ScanViewChunks(0);
        }

        private void GetTemperature()
        {
            int depth = (int)CameraManager.CamerasRead[0].Focus.Y;
            depth /= 16;

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




            Console.WriteLine("Temperature: " + temperature + "K");
            //Console.WriteLine("Pressure: " + pressure + pascal);
            Console.WriteLine("Pressure: " + pressure + "bar");
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

            //if (mState.LeftButton == ButtonState.Pressed)
            //{
            //    GetTile((int)((mState.X / tileSize) + (CameraManager.CamerasRead[0].Focus.X / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize) + 1), (int)((mState.Y / tileSize) + (CameraManager.CamerasRead[0].Focus.Y / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)));
            //    ColorCircle(CX, CY, TX, TY, 4, Color.Red);
            //}

            if (kState.IsKeyDown(Keys.L) && pKState.IsKeyUp(Keys.L))            
                loadWholeWorld = true;

            if (loadWholeWorld)
            {
                if (creators.Count < 10000)
                {
                    if (!chunks[loadedChunksX][loadedChunksY].ChunkActive)
                        LoadChunk(loadedChunksX, loadedChunksY);

                    loadedChunksX++;

                    if (loadedChunksX == chunkX - 1)
                    {
                        loadedChunksY++;
                        loadedChunksX = 0;

                        if (loadedChunksY == chunkY - 1)
                        {
                            loadedChunksX = 0;
                            loadedChunksY = 0;
                            loadWholeWorld = false;
                        }
                    }
                }

                Console.WriteLine(loadedChunksX);
                Console.WriteLine(loadedChunksY);
            }

            #region Mouse Control

            //if (mState.LeftButton == ButtonState.Pressed)
            //{
            //    GetTile((int)((mState.X / tileSize) + (CameraManager.CamerasRead[0].Focus.X / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize) + 1), (int)((mState.Y / tileSize) + (CameraManager.CamerasRead[0].Focus.Y / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)));
            //    DrawCircle(CX, CY, TX, TY, 64, 0);
            //}

            //if (mState.RightButton == ButtonState.Pressed)
            //{
            //    GetTile((int)((mState.X / tileSize) + (CameraManager.CamerasRead[0].Focus.X / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize) + 1), (int)((mState.Y / tileSize) + (CameraManager.CamerasRead[0].Focus.Y / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)));
            //    chunks[CX][CY].tiles[TX][TY].BlockID = 3;
            //}

            #endregion
        }

        private void DrawCircle(int cx, int cy, int tx, int ty, int radius, ushort blockID)
        {
            int ACX = cx;
            int ACY = cy;
            int ATX = tx;
            int ATY = ty;

            int r = radius;
            int ox = ATX, oy = ATY;

            //StopWatch.Start();

            drawnBlocksChanged = 0;

            if (radius == 1)
            {
                if (blockID == 0)
                    chunks[CX][CY].RemoveBlock(tx, ty);
                else
                    chunks[CX][CY].ChangeBlock(tx, ty, blockID, random);
            }
            else
            {
                if (blockID == 0)
                {
                    for (int x = -r; x <= r; x++)
                    {
                        int height = (int)Math.Sqrt(r * r - x * x);

                        for (int y = -height; y <= height; y++)
                        {
                            GetTile(ACX, ACY, x + ox, y + oy);

                            if (chunks[CX][CY].RemoveBlock(TX, TY))
                                drawnBlocksChanged++;
                        }
                    }
                }
                else
                {
                    for (int x = -r; x <= r; x++)
                    {
                        int height = (int)Math.Sqrt(r * r - x * x);

                        for (int y = -height; y <= height; y++)
                        {
                            GetTile(ACX, ACY, x + ox, y + oy);

                            if (chunks[CX][CY].ChangeBlock(TX, TY, blockID, random))
                                drawnBlocksChanged++;
                        }
                    }
                }
            }
        }

        private void ColorCircle(int cx, int cy, int tx, int ty, int radius, Color color)
        {
            //Set method in chunk for safety.

            int ACX = cx;
            int ACY = cy;
            int ATX = tx;
            int ATY = ty;

            int r = radius;
            int ox = ATX, oy = ATY;

            //StopWatch.Start();

            drawnBlocksChanged = 0;

            if (radius == 1)
            {
                chunks[cy][cx].tileInfo[tx][ty].BlockColor = color;
            }
            else
            {
                    for (int x = -r; x <= r; x++)
                    {
                        int height = (int)Math.Sqrt(r * r - x * x);

                        for (int y = -height; y <= height; y++)
                        {
                            GetTile(ACX, ACY, x + ox, y + oy);

                            if (chunks[CY][CX].tileInfo != null)
                                chunks[CY][CX].tileInfo[TX][TY].BlockColor = color;
                        }
                    }
            }
        }
        
        private void Creators(GameTime gameTime)
        {
            int ActiveCreators = 2500;

            if (creators.Count > 100000)
                ActiveCreators /= 2;
            if (creators.Count > 250000)
                ActiveCreators /= 2;
            if (creators.Count > 500000)
                ActiveCreators /= 2;

            int min = 0;
            int max = creators.Count;

            if (creators.Count > ActiveCreators)
            {
                max = random.Next(ActiveCreators, creators.Count);
                min = max - ActiveCreators;

                CreatorUpdate(gameTime, min, max);
            }
            else
            {
                int multiplier = (int)(ActiveCreators / (float)creators.Count);
                multiplier++;

                for (int i = 0; i < multiplier; i++)
                {
                    min = 0;
                    max = creators.Count;

                    if (creators.Count > ActiveCreators)
                    {
                        max = random.Next(ActiveCreators, creators.Count);
                        min = max - ActiveCreators;
                    }

                    CreatorUpdate(gameTime, min, max);

                    multiplier = (int)(ActiveCreators / (float)creators.Count);
                    multiplier++;
                }
            }
        }

        private void CreatorUpdate(GameTime gameTime, int min, int max)
        {
            for (int i = min; i < max; i++)
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

                            DrawCircle(CX, CY, TX, TY, creators[i].Size, 0);
                            creators[i].TilesChanged += drawnBlocksChanged;
                            chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten += (ushort)drawnBlocksChanged;
                        }

                        if (chunks[creators[i].SChunkX][creators[i].SChunkY].Miners == 1)
                            creators[i].Active = true;

                        if (chunks[creators[i].SChunkX][creators[i].SChunkY].TilesEaten > chunks[creators[i].SChunkX][creators[i].SChunkY].EdibleTiles)                        
                            creators[i].Active = false;


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

                            DrawCircle(CX, CY, TX, TY, creators[i].Size, creators[i].BlockID);
                            creators[i].TilesChanged += drawnBlocksChanged;
                            chunks[creators[i].SChunkX][creators[i].SChunkY].TilesBiomed += (ushort)drawnBlocksChanged;
                        }


                        if (chunks[creators[i].SChunkX][creators[i].SChunkY].Biominators == 1)
                            creators[i].Active = true;

                        if (chunks[creators[i].SChunkX][creators[i].SChunkY].TilesBiomed > chunks[creators[i].SChunkX][creators[i].SChunkY].BiomableTiles)
                        {
                            creators[i].Active = false;
                        }

                        if (creators[i].Spawn && !creators[i].InAir && chunks[creators[i].SChunkX][creators[i].SChunkY].Biominators < 100)
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

        private void MinerCheck(int i)
        {
            if (creators[i].Active)
            {
                int CreatorX = creators[i].X;
                int CreatorY = creators[i].Y;
                int CreatorSize = creators[i].Size;

                if (CreatorSize == 1)
                    CreatorSize -= 1;
                
                GetTile(CreatorX, CreatorY - CreatorSize - 1);
                if (chunks[CX][CY].CheckForBlock(TX, TY, 0))
                    creators[i].UpSolid = false;
                else
                    creators[i].UpSolid = true;

                GetTile(CreatorX + CreatorSize + 1, CreatorY);
                if (chunks[CX][CY].CheckForBlock(TX, TY, 0))
                    creators[i].RightSolid = false;
                else
                    creators[i].RightSolid = true;

                GetTile(CreatorX, CreatorY + CreatorSize + 1);
                if (chunks[CX][CY].CheckForBlock(TX, TY, 0))
                    creators[i].DownSolid = false;
                else
                    creators[i].DownSolid = true;

                GetTile(CreatorX - CreatorSize - 1, CreatorY);
                if (chunks[CX][CY].CheckForBlock(TX, TY, 0))
                    creators[i].LeftSolid = false;
                else
                    creators[i].LeftSolid = true;

                if (!creators[i].UpSolid && !creators[i].RightSolid && !creators[i].DownSolid && !creators[i].LeftSolid)
                    creators[i].InAir = true;
                else
                    creators[i].InAir = false;
            }
        }
        
        private void BiominatorCheck(int i)
        {
            if (creators[i].Active)
            {
                int CreatorX = creators[i].X;
                int CreatorY = creators[i].Y;
                int CreatorSize = creators[i].Size;

                if (CreatorSize == 1)
                    CreatorSize -= 1;

                int blockID;

                GetTile(CreatorX, CreatorY - CreatorSize - 1);
                if (!chunks[CX][CY].CheckForBlock(TX, TY, 0) && !chunks[CX][CY].CheckForBlock(TX, TY, creators[i].BlockID))
                    creators[i].UpSolid = true;
                else
                    creators[i].UpSolid = false;

                GetTile(CreatorX + CreatorSize + 1, CreatorY);
                if (!chunks[CX][CY].CheckForBlock(TX, TY, 0) && !chunks[CX][CY].CheckForBlock(TX, TY, creators[i].BlockID))
                    creators[i].RightSolid = true;
                else
                    creators[i].RightSolid = false;

                GetTile(CreatorX, CreatorY + CreatorSize + 1);
                if (!chunks[CX][CY].CheckForBlock(TX, TY, 0) && !chunks[CX][CY].CheckForBlock(TX, TY, creators[i].BlockID))
                    creators[i].DownSolid = true;
                else
                    creators[i].DownSolid = false;

                GetTile(CreatorX - CreatorSize - 1, CreatorY);
                if (!chunks[CX][CY].CheckForBlock(TX, TY, 0) && !chunks[CX][CY].CheckForBlock(TX, TY, creators[i].BlockID))
                    creators[i].LeftSolid = true;
                else
                    creators[i].LeftSolid = false;

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
                                if (!chunks[cx3][cy3].CheckForActive(x2, y2))
                                {
                                    chunks[cx3][cy3].activeTiles.Add(new ActiveBlocks());
                                    chunks[cx3][cy3].activeTiles[chunks[cx3][cy3].activeTiles.Count - 1].Initialize((ushort)x2, (ushort)y2, chunks[cx3][cy3].tiles[x2][y2].BlockID);
                                    chunks[cx3][cy3].activeTiles[chunks[cx3][cy3].activeTiles.Count - 1].BlockDecaying = true;
                                }
                                else
                                    chunks[cx3][cy3].activeTiles[chunks[cx3][cy3].SelectedActiveBlock].BlockDecaying = true;
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

                                        ///////////////////////////////////////////////////
                                        ushort dirt = 0;
                                        //chunks[CX][CY].tiles[TX][TY].ChangeBlock(dirt, random);
                                        ///////////////////////////////////////////////////
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
        
        private void RemoveTaggedBlocks()
        {
            int BlocksToRemove = 1000;
            BlocksToRemove = (removingBlocks.Count / 10) + 25;

            int min = 0;
            int max = removingBlocks.Count;

            if (removingBlocks.Count > BlocksToRemove)
            {
                max = random.Next(BlocksToRemove, removingBlocks.Count);
                min = max - BlocksToRemove;
            }

            for (int i = max - 1; i >= min; i--)
            {
                int cx = removingBlocks[i].CX;
                int cy = removingBlocks[i].CY;
                int tx = removingBlocks[i].TX;
                int ty = removingBlocks[i].TY;

                chunks[cx][cy].tiles[tx][ty].BlockID = 0;

                if (chunks[cx][cy].CheckForActive(tx, ty))
                    chunks[cx][cy].activeTiles.RemoveAt(chunks[cx][cy].SelectedActiveBlock);

                removingBlocks.RemoveAt(i);
            }
        }

        //public void RemoveBlock(int cx, int cy, int tx, int ty, bool fast)
        //{
        //    if (chunks[cx][cy].ChunkActive)
        //    {
        //        removingBlocks.Add(new RemovingBlocks());
        //        removingBlocks[removingBlocks.Count - 1].Initialize(cx, cy, tx, ty);
        //    }
        //    else
        //    {
        //        if (!chunks[cx][cy].VirtualChunkCreated)                
        //            chunks[cx][cy].CreateVirtualChunk(chunkTiles);

        //        if (chunks[cx][cy].virtualTiles[tx][ty].BlockID != 0)
        //        {
        //            chunks[cx][cy].virtualTiles[tx][ty].BlockID = 0;
        //        }
        //    }
        //}

        public void RemoveBlock2(int cx, int cy, int tx, int ty, bool fast)
        {
            if (chunks[cx][cy].ChunkActive)
            {
                //selectedTile = chunks[cx][cy].tiles[tx][ty].BlockID;

                //if (selectedTile != 0)
                {
                    
                    chunks[cx][cy].tiles[tx][ty].BlockID = 0;

                    //if (ActiveBlockExists(cx, cy, tx, ty))
                    //    activeBlocks.RemoveAt(selectedActiveBlock);

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


                                        //if (i == 0)
                                        //    if (!ActiveBlockExists(CX2, CY2, TX2, TY2))
                                        //    {
                                        //        activeBlocks.Add(new ActiveBlocks());
                                        //        activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
                                        //        activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX2, (byte)CY2, (ushort)TX2, (ushort)TY2, chunks[CX2][CY2].tiles[TX2][TY2].BlockID);
                                        //    }
                                        //    else if (!activeBlocks[selectedActiveBlock].BlockFalling)
                                        //    {
                                        //        activeBlocks[selectedActiveBlock].BlockFalling = true;
                                        //    }
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

            if (chunks[cx][cy].CheckForActive(tx, ty))
                chunks[cx][cy].activeTiles[chunks[cx][cy].SelectedActiveBlock].BlockFalling = false;

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
                                if (!chunks[CX2][CY2].CheckForActive(TX2, TY2))
                                {
                                    chunks[CX2][CY2].activeTiles.Add(new ActiveBlocks());
                                    chunks[CX2][CY2].activeTiles[chunks[CX2][CY2].activeTiles.Count - 1].BlockFalling = true;
                                    chunks[CX2][CY2].activeTiles[chunks[CX2][CY2].activeTiles.Count - 1].Initialize((ushort)TX2, (ushort)TY2, chunks[CX2][CY2].tiles[TX2][TY2].BlockID);
                                }
                                else if (!chunks[CX2][CY2].activeTiles[chunks[CX2][CY2].SelectedActiveBlock].BlockFalling)
                                {
                                    chunks[CX2][CY2].activeTiles[chunks[CX2][CY2].SelectedActiveBlock].BlockFalling = true;
                                }
                        }
                }
            }
        }

        #endregion

        private void UpdateChunks()
        {
            #region Update Active Blocks OLD

            //if (activeBlocks.Count != 0)
            //{

            //    int min = 0;
            //    int max = activeBlocks.Count;

            //    if (activeBlocks.Count > 1000)
            //    {
            //        max = random.Next(1000, activeBlocks.Count);
            //        min = max - 1000;
            //    }

            //    int speed;

            //    if (activeBlocks.Count > 2000)
            //        speed = 3;
            //    else if (activeBlocks.Count > 1000)
            //        speed = 2;
            //    else
            //        speed = 1;

            //    for (int i = activeBlocks.Count - 1; i >= 0; i--)
            //    {
            //        bool removeBlock = false;

            //        #region Falling Blocks

            //        if (activeBlocks[i].BlockFalling)
            //        {
            //            int ACX = activeBlocks[i].CX;
            //            int ACY = activeBlocks[i].CY;
            //            int ATX = activeBlocks[i].TX;
            //            int ATY = activeBlocks[i].TY;

            //            if (chunks[ACX][ACY].tiles[ATX][ATY].BlockID == 0)
            //            {
            //                if (activeBlocks[i].BlockFalling)
            //                    activeBlocks[i].BlockFalling = false;
            //            }
            //            else
            //            {
            //                GetTile(ACX, ACY, ATX, ATY + 1);

            //                if (ActiveChunk)
            //                {
            //                    if (chunks[CX][CY].tiles[TX][TY].BlockID == 0)
            //                    {
            //                        if (!ActiveBlockExists(CX, CY, TX, TY))
            //                        {
            //                            activeBlocks.Add(new ActiveBlocks());
            //                            CopyBlockInfo(i, activeBlocks.Count - 1, (byte)CX, (byte)CY, (ushort)TX, (ushort)TY);
            //                            activeBlocks[activeBlocks.Count - 1].BlockFalling = true;
            //                            //activeBlocks[activeBlocks.Count - 1].Initialize((byte)CX, (byte)CY, (ushort)TX, (ushort)TY, chunks[CX][CY].tiles[TX][TY].BlockID);
            //                        }
            //                        else if (!activeBlocks[selectedActiveBlock].BlockFalling)
            //                        {
            //                            CopyBlockInfo(i, selectedActiveBlock, (byte)CX, (byte)CY, (ushort)TX, (ushort)TY);
            //                            activeBlocks[selectedActiveBlock].BlockFalling = true;
            //                        }

            //                        chunks[CX][CY].tiles[TX][TY].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
            //                        chunks[ACX][ACY].tiles[ATX][ATY].BlockID = 0;
            //                        activeBlocks[i].BlockFalling = false;
            //                    }
            //                    else
            //                    {
            //                        if (activeBlocks[i].BlockFalling)
            //                            activeBlocks[i].BlockFalling = false;

            //                        CheckBlock(ACX, ACY, ATX, ATY);
            //                    }
            //                }
            //                else
            //                {
            //                    if (activeBlocks[i].BlockFalling)
            //                        activeBlocks[i].BlockFalling = false;

            //                    activeBlocks[i].BlockHealth = 0;
            //                }
            //            }
            //        }

            //        #endregion

            //        if (i < max && i >= min)
            //        {
            //            #region Changing Blocks

            //            if (activeBlocks[i].ChangeBlock != 0)
            //            {
            //                int ACX = activeBlocks[i].CX;
            //                int ACY = activeBlocks[i].CY;
            //                int ATX = activeBlocks[i].TX;
            //                int ATY = activeBlocks[i].TY;

            //                if (chunks[ACX][ACY].tiles[ATX][ATY].BlockID != 0)
            //                {
            //                    switch (speed)
            //                    {
            //                        case 1:
            //                            if (random.Next(0, 25) == 0)
            //                            {
            //                                chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
            //                                activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
            //                                chunks[ACX][ACY].tiles[ATX][ATY].variation = (byte)random.Next(0, TileData.BlockTextureIDs[activeBlocks[i].BlockID].Count);
            //                                chunks[ACX][ACY].tiles[ATX][ATY].SlopeMask = (byte)random.Next(0, TileData.SlopeMaskIDs[activeBlocks[i].BlockID].Count);
            //                                chunks[ACX][ACY].tiles[ATX][ATY].BlockMask = (byte)random.Next(0, TileData.BlockMaskIDs[activeBlocks[i].BlockID].Count);
            //                                chunks[ACX][ACY].tiles[ATX][ATY].BlockColor = TileData.BlockColor[activeBlocks[i].BlockID];
            //                                activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
            //                                activeBlocks[i].ChangeBlock = 0;
            //                            }
            //                            break;

            //                        case 2:
            //                            if (random.Next(0, 10) == 0)
            //                            {
            //                                chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
            //                                activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
            //                                chunks[ACX][ACY].tiles[ATX][ATY].variation = (byte)random.Next(0, TileData.BlockTextureIDs[activeBlocks[i].BlockID].Count);
            //                                chunks[ACX][ACY].tiles[ATX][ATY].SlopeMask = (byte)random.Next(0, TileData.SlopeMaskIDs[activeBlocks[i].BlockID].Count);
            //                                chunks[ACX][ACY].tiles[ATX][ATY].BlockMask = (byte)random.Next(0, TileData.BlockMaskIDs[activeBlocks[i].BlockID].Count);
            //                                chunks[ACX][ACY].tiles[ATX][ATY].BlockColor = TileData.BlockColor[activeBlocks[i].BlockID];
            //                                activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
            //                                activeBlocks[i].ChangeBlock = 0;
            //                            }
            //                            break;

            //                        case 3:
            //                            chunks[ACX][ACY].tiles[ATX][ATY].BlockID = activeBlocks[i].ChangeBlock;
            //                            activeBlocks[i].BlockID = chunks[ACX][ACY].tiles[ATX][ATY].BlockID;
            //                            chunks[ACX][ACY].tiles[ATX][ATY].variation = (byte)random.Next(0, TileData.BlockTextureIDs[activeBlocks[i].BlockID].Count);
            //                            chunks[ACX][ACY].tiles[ATX][ATY].SlopeMask = (byte)random.Next(0, TileData.SlopeMaskIDs[activeBlocks[i].BlockID].Count);
            //                            chunks[ACX][ACY].tiles[ATX][ATY].BlockMask = (byte)random.Next(0, TileData.BlockMaskIDs[activeBlocks[i].BlockID].Count);
            //                            chunks[ACX][ACY].tiles[ATX][ATY].BlockColor = TileData.BlockColor[activeBlocks[i].BlockID];
            //                            activeBlocks[i].BlockMaxHealth = TileData.BlockHealth[activeBlocks[i].BlockID];
            //                            activeBlocks[i].ChangeBlock = 0;
            //                            break;
            //                    }
            //                }
            //                else
            //                {
            //                    activeBlocks[i].ChangeBlock = 0;
            //                }
            //            }

            //            #endregion

            //            #region Health Check

            //            if (activeBlocks[i].BlockMaxHealth != activeBlocks[i].BlockHealth)
            //            {
            //                if (activeBlocks[i].BlockHealth > activeBlocks[i].BlockMaxHealth)
            //                {
            //                    //Overcharged Block
            //                    ///////////////////////////////////////////////////////////////
            //                    activeBlocks[i].BlockHealth = activeBlocks[i].BlockMaxHealth;
            //                    activeBlocks[i].BlockHealing = false;
            //                    activeBlocks[i].BlockDecaying = false;
            //                    activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
            //                    ////////////////////////////////////////////////////////////////
            //                }
            //                else if (activeBlocks[i].BlockHealth >= activeBlocks[i].BlockMaxHealth / 2 && activeBlocks[i].BlockHealth < activeBlocks[i].BlockMaxHealth)
            //                {
            //                    activeBlocks[i].BlockHealing = true;
            //                    activeBlocks[i].BlockDecaying = false;
            //                    activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
            //                }
            //                else if (activeBlocks[i].BlockHealth < activeBlocks[i].BlockMaxHealth / 2 && activeBlocks[i].BlockHealth > 0)
            //                {
            //                    activeBlocks[i].BlockHealing = false;
            //                    activeBlocks[i].BlockDecaying = true;
            //                    activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
            //                }
            //                else
            //                {
            //                    removeBlock = true;
            //                }
            //            }
            //            else
            //            {
            //                activeBlocks[i].BlockHealing = false;
            //                activeBlocks[i].BlockDecaying = false;
            //                activeBlocks[i].PreviousHealth = activeBlocks[i].BlockHealth;
            //            }

            //            #endregion

            //            #region Decaying Block

            //            if (activeBlocks[i].BlockDecaying)
            //                //Maybe change to time based thingy
            //                switch (speed)
            //                {
            //                    case 1:
            //                        if (random.Next(0, 25) == 0)
            //                            activeBlocks[i].BlockHealth -= random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
            //                        break;

            //                    case 2:
            //                        if (random.Next(0, 10) == 0)
            //                            activeBlocks[i].BlockHealth -= random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
            //                        break;

            //                    case 3:
            //                        activeBlocks[i].BlockHealth = 0;
            //                        break;
            //                }

            //            #endregion

            //            #region Regenerating Block

            //            if (activeBlocks[i].BlockHealing)
            //                switch (speed)
            //                {
            //                    case 1:
            //                        if (random.Next(0, 25) == 0)
            //                            activeBlocks[i].BlockHealth += random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
            //                        break;

            //                    case 2:
            //                        if (random.Next(0, 10) == 0)
            //                            activeBlocks[i].BlockHealth += random.Next(0, activeBlocks[i].BlockMaxHealth / 5);
            //                        break;

            //                    case 3:
            //                        activeBlocks[i].BlockHealth = activeBlocks[i].BlockMaxHealth;
            //                        break;
            //                }

            //            #endregion
            //        }

            //        if (removeBlock)
            //            RemoveBlock(activeBlocks[i].CX, activeBlocks[i].CY, activeBlocks[i].TX, activeBlocks[i].TY, false);
            //        else if (!CheckActiveBlock(i))
            //            activeBlocks.RemoveAt(i);
            //        else
            //            chunks[activeBlocks[i].CX][activeBlocks[i].CY].ActiveBlocks++;
            //    }
            //}

            //#endregion

            #endregion

            #region Update Chunk

            //bool ChunkSelect = true;

            //int x = 0;
            //int y = 0;

            //if (random.Next(0, 2) == 0)
            //{
            //    while (ChunkSelect)
            //    {
            //        x = random.Next(0, chunkX);
            //        y = random.Next(0, chunkY);

            //        if (chunks[x][y].ChunkActive)
            //            ChunkSelect = false;
            //    }
            //}
            //else
            //{
            //    if (RenderChunkX > RenderChunkX2)
            //    {
            //        RenderChunkX = 0;
            //        RenderChunkX2 = 0;
            //    }

            //    if (RenderChunkY > RenderChunkY2)
            //    {
            //        RenderChunkY = 0;
            //        RenderChunkY2 = 0;
            //    }

            //    x = random.Next(RenderChunkX, RenderChunkX2 + 1);
            //    y = random.Next(RenderChunkY, RenderChunkY2 + 1);
            //}

            //if (chunks[x][y].ChunkActive)
            //{
            //    if (chunks[x][y].Miners == 0 && chunks[x][y].Phase == 1)
            //        chunks[x][y].Phase++;

                //if (chunks[x][y].Phase == 2 && !chunkControl && chunks[x][y].ActiveBlocks <= 64)
                //{
                //    Dirtify(x, y);
                //    chunkControl = true;
                //}

                //if (chunks[x][y].Phase == 2 && !chunkControl)
                //{
                //    Smooth(x, y);
                //    chunkControl = true;
                //}

                //chunks[x][y].ActiveBlocks = 0;
            //}

            //chunkControl = false;

            #endregion
        }
        
        //private bool ActiveBlockExists(int cx, int cy, int tx, int ty)
        //{
        //    for (int i = 0; i < activeBlocks.Count; i++)
        //    {
        //        if (activeBlocks[i].CheckBlock(cx, cy, tx, ty))
        //            return true;
        //    }
            
        //    return false;
        //}

        //private bool CheckActiveBlock(int i)
        //{
        //    if (!activeBlocks[i].BlockFalling)
        //        if (activeBlocks[i].BlockHealth == activeBlocks[i].BlockMaxHealth || (activeBlocks[i].BlockHealth <= 0 && !activeBlocks[i].BlockDecaying))
        //            if (activeBlocks[i].ChangeBlock == 0)
        //            return false;

        //    return true;
        //}

        //private void CopyBlockInfo(int i, int r, byte cx, byte cy, ushort tx, ushort ty)
        //{
        //    activeBlocks[r].BlockHealth = activeBlocks[i].BlockHealth;
        //    activeBlocks[r].BlockDecaying = activeBlocks[i].BlockDecaying;
        //    activeBlocks[r].BlockFalling = activeBlocks[i].BlockFalling;
        //    activeBlocks[r].BlockHealing = activeBlocks[i].BlockHealing;
        //    activeBlocks[r].BlockMaxHealth = activeBlocks[i].BlockMaxHealth;
        //    activeBlocks[r].ChangeBlock = activeBlocks[i].ChangeBlock;
        //    activeBlocks[r].CX = cx;
        //    activeBlocks[r].CY = cy;
        //    activeBlocks[r].TX = tx;
        //    activeBlocks[r].TY = ty;
        //}
        
        private void ScanViewChunks(int CameraNumber)
        {
            CheckActiveChunks(CameraManager.CamerasRead[CameraNumber].Focus, CameraManager.CamerasRead[CameraNumber].Zoom);
        }

        private Vector2 GetCurrentChunk(Vector2 pos)
        {
            int x = (int)((pos.X - worldPosition.X) / (chunkTiles * tileSize));
            int y = (int)((pos.Y - worldPosition.Y) / (chunkTiles * tileSize));
            
            return new Vector2(x, y);
        }

        private void CheckActiveChunks(Vector2 pos, float zoom)
        {
            Vector2 location = GetCurrentChunk(pos);

            int x2 = (int)location.X;
            int y2 = (int)location.Y;

            Console.WriteLine("Chunk: " + x2 + " " + y2);



            float size = zoom;

            for (int x = x2 - (int)Math.Ceiling(chunksHorizontal / size); x <= x2 + (int)Math.Ceiling(chunksHorizontal / size); x++)
            {
                for (int y = y2 - (int)Math.Ceiling(chunksVertical / size); y <= y2 + (int)Math.Ceiling(chunksVertical / size); y++)
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

        private void SetupWorldSpace(int CameraNumber)
        {
            CameraPosition = CameraManager.CamerasRead[CameraNumber].PreviousFocus;
            CameraZoom = CameraManager.CamerasRead[CameraNumber].Zoom;

            RenderTileX = (int)((((CameraPosition.X - worldPosition.X) / tileSize) - (((WorldVariables.WindowWidth / 2) / tileSize) / CameraZoom)) - 1);
            RenderTileY = (int)((((CameraPosition.Y - worldPosition.Y) / tileSize) - (((WorldVariables.WindowHeight / 2) / tileSize) / CameraZoom)) - 3);
            RenderTileX2 = (int)((((CameraPosition.X - worldPosition.X) / tileSize) + (((WorldVariables.WindowWidth / 2) / tileSize) / CameraZoom)) + 2);
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
        }

        public void PreDraw(SpriteBatch spriteBatch, GraphicsDevice graphics, int CameraNumber)
        {
            renderBlocks.Clear();
            renderSlopes.Clear();

            //WorldVariables.SetCameraSize(CameraNumber, chunkTiles * tileSize);

            SetupWorldSpace(CameraNumber);
            
            Matrix mat = CameraManager.CamerasRead[0].Transform;
            Rectangle rect;
            float rotation = 0f;
            int ARB;
            int ARB2 = 0;
            int ARS;
            int ARS2 = 0;
            CurrentRenderChunk = 0;
            Vector2 virtualPos;
            Vector2 Origin;
            RenderChunksX = RenderChunkX2 - RenderChunkX;
            RenderChunksY = RenderChunkY2 - RenderChunkY;
            
            a.View = mat;
            a.View = Matrix.Identity;

            int CounterX = 0;
            int CounterY = 0;
            
            for (int x = RenderChunkX; x <= RenderChunkX2; x++)
            {
                for (int y = RenderChunkY; y <= RenderChunkY2; y++)
                {
                    graphics.SetRenderTarget(BlockRenderTargets[CounterX][CounterY]);

                    int xp = 0;
                    int yp = 0;

                    graphics.Clear(ClearOptions.Target | ClearOptions.Stencil, new Color(0, 0, 0, 1), 0, 0);
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, s1, null, a);

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

                                    if (ID != 0)
                                    {
                                        renderBlocks.Add(new RenderBlocks());
                                        ARB = renderBlocks.Count - 1;

                                        position.X = (worldPosition.X + (x2 * tileSize) + (x * (chunkTiles * tileSize)));
                                        position.Y = (worldPosition.Y + (y2 * tileSize) + (y * (chunkTiles * tileSize)));

                                        Origin = new Vector2(15.75f, 15.75f);

                                        #region Rotation

                                        //chunks[x][y].tileInfo[x2][y2].rotation = (byte)random.Next(0, 4);

                                        float StencilRotation = 0f;

                                        switch (chunks[x][y].GetRotation(x2, y2, random))
                                        {
                                            case 0:
                                                StencilRotation = 0f;
                                                break;

                                            case 1:
                                                StencilRotation = (float)Math.PI / 2;
                                                break;

                                            case 2:
                                                StencilRotation = (float)Math.PI;
                                                break;

                                            case 3:
                                                StencilRotation = (float)Math.PI * 1.5f;
                                                break;
                                        }

                                        #endregion

                                        virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;

                                        Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), TextureSize, TextureSize);

                                        renderBlocks[ARB].Initialize(position, chunks[x][y].GetTileColor(x2, y2, random), rotation, new Vector2(CounterX, CounterY), TextureRectangle);

                                        rect = TileData.Rectangles[TileData.BlockMaskIDs[ID][chunks[x][y].GetBlockMask(x2, y2, random)]];

                                        spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, StencilRotation, Origin, 1f, SpriteEffects.None, 0f);
                                    }
                                }
                            }
                            yp++;
                        }
                        yp = 0;
                        xp++;
                    }
                    spriteBatch.End();

                    xp = 0;
                    yp = 0;

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, s2, null, a);

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

                                    if (ID != 0)
                                    {
                                        int var = chunks[x][y].GetVariation(x2, y2, random);
                                        if (var >= TileData.BlockTextureIDs[ID].Count)
                                            var = 0;

                                        Origin = new Vector2(16, 16);

                                        rect = TileData.Rectangles[TileData.BlockTextureIDs[ID][var]];
                                        virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;

                                        #region Tile Edges

                                        //bool Left = false;
                                        //bool Right = false;
                                        //bool Up = false;
                                        //bool Down = false;

                                        //GetTile(x, y, x2 - 1, y2);

                                        //if (ActiveChunk)
                                        //    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        //        Left = true;

                                        //GetTile(x, y, x2 + 1, y2);

                                        //if (ActiveChunk)
                                        //    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        //        Right = true;

                                        //GetTile(x, y, x2, y2 - 1);

                                        //if (ActiveChunk)
                                        //    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        //        Up = true;

                                        //GetTile(x, y, x2, y2 + 1);

                                        //if (ActiveChunk)
                                        //    if (chunks[CX][CY].tiles[TX][TY].BlockID != 0)
                                        //        Down = true;

                                        ////Up = true;
                                        ////Down = true;
                                        ////Left = true;
                                        ////Right = true;

                                        ////rect = new Rectangle(0, 0, 32, 32);

                                        //if (Up && Down && Left && Right)
                                        //{
                                        //}
                                        //else
                                        //{
                                        //}

                                        //if (Up)
                                        //{
                                        //    rect.Y += 8;
                                        //    rect.Height -= 8;
                                        //    origin.Y -= 4;
                                        //}
                                        //else
                                        //    position.Y -= 4;


                                        //if (Down)
                                        //{
                                        //    rect.Height -= 8;
                                        //    origin.Y -= 4;
                                        //}
                                        //else
                                        //    position.Y += 4;

                                        //if (Left)
                                        //{
                                        //    rect.X += 8;
                                        //    rect.Width -= 8;
                                        //    origin.X -= 4;
                                        //}
                                        //else
                                        //    position.X -= 4;

                                        //if (Right)
                                        //{
                                        //    rect.Width -= 8;
                                        //    origin.X -= 4;
                                        //}
                                        //else
                                        //    position.X += 4;

                                        #endregion

                                        rotation = chunks[x][y].tileInfo[x2][y2].textureRotation;
                                        //rotation = (float)(random.NextDouble() + MathHelper.TwoPi);
                                        //rotation = 0f;

                                        //spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, renderBlocks[ARB2].BlockRotation, Origin, 1f, SpriteEffects.None, 0f);
                                        spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                        
                                        ARB2++;
                                    }
                                }
                            }
                            yp++;
                        }
                        yp = 0;
                        xp++;
                    }

                    spriteBatch.End();

                    xp = 0;
                    yp = 0;

                    graphics.SetRenderTarget(SlopeRenderTargets[CounterX][CounterY]);                    
                    graphics.Clear(ClearOptions.Target | ClearOptions.Stencil, new Color(0, 0, 0, 1), 0, 0);

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, s1, null, a);

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

                                    if (ID != 0)
                                    {
                                        #region Variables

                                        bool Left = false;
                                        bool Right = false;
                                        bool Up = false;
                                        bool Down = false;

                                        int blockIDL = 0;
                                        int blockIDU = 0;
                                        int blockIDR = 0;
                                        int blockIDD = 0;

                                        int lVar = 0;
                                        int uVar = 0;
                                        int rVar = 0;
                                        int dVar = 0;

                                        Color lColor = Color.White;
                                        Color uColor = Color.White;
                                        Color rColor = Color.White;
                                        Color dColor = Color.White;

                                        #endregion

                                        #region Check Tiles

                                        GetTile(x, y, x2 - 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDL = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            lVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            lColor = chunks[CX][CY].GetTileColor(TX, TY, random);
                                            
                                            if (blockIDL != 0)
                                                Left = true;
                                        }

                                        GetTile(x, y, x2 + 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDR = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            rVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            rColor = chunks[CX][CY].GetTileColor(TX, TY, random);
                                            
                                            if (blockIDR != 0)
                                                Right = true;
                                        }

                                        GetTile(x, y, x2, y2 - 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDU = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            uVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            uColor = chunks[CX][CY].GetTileColor(TX, TY, random);

                                            if (blockIDU != 0)
                                                Up = true;
                                        }

                                        GetTile(x, y, x2, y2 + 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDD = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            dVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            dColor = chunks[CX][CY].GetTileColor(TX, TY, random);

                                            if (blockIDD != 0)
                                                Down = true;
                                        }

                                        #endregion

                                        Origin = new Vector2(QuarterTextureSize, QuarterTextureSize);
                                        Origin -= new Vector2(0.25f, 0.25f);
                                        virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                        rotation = 0f;

                                        #region Draw

                                        if (Up && Left && blockIDU == blockIDL && blockIDU != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDU][uVar]];
                                            rect.Width /= 2;
                                            rect.Height /= 2;
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            
                                            ///////////////////////////
                                            Vector2 position2 = position - new Vector2(4, 4);
                                            
                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position2, uColor, new Vector2(CounterX, CounterY), TextureRectangle);



                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.X += 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI / 2;
                                                    rect.Y += 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X += 16;
                                                    rect.Y += 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        if (Down && Left && blockIDD == blockIDL && blockIDD != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDL][lVar]];
                                            rect.Y += HalfTextureSize;
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            virtualPos += new Vector2(0, HalfTextureSize);
                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            /////////////////////////////////////
                                            Vector2 position2 = position - new Vector2(4, -4);

                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position2, lColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI / 2f;
                                                    rect.X += 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.Y -= 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X += 16;
                                                    rect.Y -= 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        if (Up && Right && blockIDU == blockIDR && blockIDU != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDR][rVar]];
                                            rect.X += HalfTextureSize;
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            virtualPos += new Vector2(HalfTextureSize, 0);
                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            ////////////////////////////////////////
                                            Vector2 position2 = position + new Vector2(4, -4);

                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position2, rColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI / 2f;
                                                    rect.X -= 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.Y += 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X -= 16;
                                                    rect.Y += 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        if (Down && Right && blockIDD == blockIDR && blockIDD != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDD][dVar]];
                                            rect.X += HalfTextureSize;
                                            rect.Y += HalfTextureSize;
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            virtualPos += new Vector2(HalfTextureSize, HalfTextureSize);
                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            ////////////////////////////////
                                            Vector2 position2 = position + new Vector2(4, 4);


                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position2, dColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.X -= 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI / 2;
                                                    rect.Y -= 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X -= 16;
                                                    rect.Y -= 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Variables

                                        bool Left = false;
                                        bool Right = false;
                                        bool Up = false;
                                        bool Down = false;

                                        int blockIDL = 0;
                                        int blockIDU = 0;
                                        int blockIDR = 0;
                                        int blockIDD = 0;

                                        int lVar = 0;
                                        int uVar = 0;
                                        int rVar = 0;
                                        int dVar = 0;

                                        Color lColor = Color.White;
                                        Color uColor = Color.White;
                                        Color rColor = Color.White;
                                        Color dColor = Color.White;

                                        #endregion

                                        #region Check Tiles

                                        GetTile(x, y, x2 - 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDL = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            lVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            lColor = chunks[CX][CY].GetTileColor(TX, TY, random);

                                            if (blockIDL != 0)
                                                Left = true;
                                        }

                                        GetTile(x, y, x2 + 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDR = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            rVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            rColor = chunks[CX][CY].GetTileColor(TX, TY, random);

                                            if (blockIDR != 0)
                                                Right = true;
                                        }

                                        GetTile(x, y, x2, y2 - 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDU = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            uVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            uColor = chunks[CX][CY].GetTileColor(TX, TY, random);

                                            if (blockIDU != 0)
                                                Up = true;
                                        }

                                        GetTile(x, y, x2, y2 + 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDD = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            dVar = chunks[CX][CY].GetSlopeMask(TX, TY, random);
                                            dColor = chunks[CX][CY].GetTileColor(TX, TY, random);

                                            if (blockIDD != 0)
                                                Down = true;
                                        }

                                        #endregion

                                        Origin = new Vector2(QuarterTextureSize, QuarterTextureSize);
                                        Origin -= new Vector2(0.25f, 0.25f);
                                        virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                        rotation = 0f;

                                        #region Draw

                                        if (Up && Left && blockIDU != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDU][uVar]];
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position, uColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.X += 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI / 2;
                                                    rect.Y += 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X += 16;
                                                    rect.Y += 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        if (Down && Left && blockIDL != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDL][lVar]];
                                            rect.Y += HalfTextureSize;
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            virtualPos += new Vector2(0, HalfTextureSize);
                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position, lColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI / 2f;
                                                    rect.X += 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.Y -= 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X += 16;
                                                    rect.Y -= 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        if (Up && Right && blockIDR != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDR][rVar]];
                                            rect.X += HalfTextureSize;
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            virtualPos += new Vector2(HalfTextureSize, 0);
                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);

                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position, rColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI / 2f;
                                                    rect.X -= 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.Y += 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X -= 16;
                                                    rect.Y += 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        if (Down && Right && blockIDD != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.SlopeMaskIDs[blockIDD][dVar]];
                                            rect.X += HalfTextureSize;
                                            rect.Y += HalfTextureSize;
                                            rect.Width /= 2;
                                            rect.Height /= 2;

                                            virtualPos += new Vector2(HalfTextureSize, HalfTextureSize);
                                            position = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
                                            Rectangle TextureRectangle = new Rectangle((int)(virtualPos.X - Origin.Y), (int)(virtualPos.Y - Origin.Y), HalfTextureSize, HalfTextureSize);


                                            renderSlopes.Add(new RenderSlopes());
                                            ARS = renderSlopes.Count - 1;
                                            renderSlopes[ARS].Initialize(position, dColor, new Vector2(CounterX, CounterY), TextureRectangle);

                                            #region Rotation

                                            switch (chunks[x][y].GetRotation(x2, y2, random))
                                            {
                                                case 1:
                                                    rotation = (float)Math.PI * 1.5f;
                                                    rect.X -= 16;
                                                    break;

                                                case 2:
                                                    rotation = (float)Math.PI / 2;
                                                    rect.Y -= 16;
                                                    break;

                                                case 3:
                                                    rotation = (float)Math.PI;
                                                    rect.X -= 16;
                                                    rect.Y -= 16;
                                                    break;
                                            }

                                            #endregion

                                            spriteBatch.Draw(TileData.MaskSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            rotation = 0f;
                                        }

                                        #endregion
                                    }
                                }
                            }
                            yp++;
                        }
                        yp = 0;
                        xp++;
                    }
                    spriteBatch.End();

                    xp = 0;
                    yp = 0;

                    spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, s2, null, a);

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

                                    if (ID != 0)
                                    {
                                        #region Variables

                                        bool Left = false;
                                        bool Right = false;
                                        bool Up = false;
                                        bool Down = false;

                                        int blockIDL = 0;
                                        int blockIDU = 0;
                                        int blockIDR = 0;
                                        int blockIDD = 0;

                                        int lVar = 0;
                                        int uVar = 0;
                                        int rVar = 0;
                                        int dVar = 0;

                                        #endregion

                                        #region Check Tiles

                                        GetTile(x, y, x2 - 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDL = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            lVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDL != 0)
                                                Left = true;
                                        }

                                        GetTile(x, y, x2 + 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDR = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            rVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDR != 0)
                                                Right = true;
                                        }

                                        GetTile(x, y, x2, y2 - 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDU = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            uVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDU != 0)
                                                Up = true;
                                        }

                                        GetTile(x, y, x2, y2 + 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDD = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            dVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDD != 0)
                                                Down = true;
                                        }

                                        #endregion

                                        Origin = new Vector2(QuarterTextureSize, QuarterTextureSize);
                                        virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;

                                        rotation = 0f;

                                        #region Draw

                                        if (Up && Left && blockIDU == blockIDL && blockIDU != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDU][uVar]];
                                            //try centering all slope textures, should look much better.
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            ARS2++;
                                        }

                                        if (Down && Left && blockIDD == blockIDL && blockIDD != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDL][lVar]];
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;
                                            virtualPos += new Vector2(0, HalfTextureSize);

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            ARS2++;
                                        }

                                        if (Up && Right && blockIDU == blockIDR && blockIDU != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDR][rVar]];
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;
                                            virtualPos += new Vector2(HalfTextureSize, 0);

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            ARS2++;
                                        }

                                        if (Down && Right && blockIDD == blockIDR && blockIDD != ID)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDD][dVar]];
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;
                                            virtualPos += new Vector2(HalfTextureSize, HalfTextureSize);

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            ARS2++;
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Variables

                                        bool Left = false;
                                        bool Right = false;
                                        bool Up = false;
                                        bool Down = false;

                                        int blockIDL = 0;
                                        int blockIDU = 0;
                                        int blockIDR = 0;
                                        int blockIDD = 0;

                                        int lVar = 0;
                                        int uVar = 0;
                                        int rVar = 0;
                                        int dVar = 0;

                                        #endregion

                                        #region Check Tiles

                                        GetTile(x, y, x2 - 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDL = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            lVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDL != 0)
                                                Left = true;
                                        }

                                        GetTile(x, y, x2 + 1, y2);

                                        if (ActiveChunk)
                                        {
                                            blockIDR = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            rVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDR != 0)
                                                Right = true;
                                        }

                                        GetTile(x, y, x2, y2 - 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDU = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            uVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDU != 0)
                                                Up = true;
                                        }

                                        GetTile(x, y, x2, y2 + 1);

                                        if (ActiveChunk)
                                        {
                                            blockIDD = chunks[CX][CY].tiles[TX][TY].BlockID;
                                            dVar = chunks[CX][CY].GetVariation(TX, TY, random);

                                            if (blockIDD != 0)
                                                Down = true;
                                        }

                                        #endregion

                                        Origin = new Vector2(QuarterTextureSize, QuarterTextureSize);
                                        virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;

                                        rotation = 0f;

                                        #region Draw

                                        if (Up && Left && blockIDU != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDU][uVar]];
                                            //try centering all slope textures, should look much better.
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            ARS2++;
                                        }

                                        if (Down && Left && blockIDL != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDL][lVar]];
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;
                                            virtualPos += new Vector2(0, HalfTextureSize);

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            ARS2++;
                                        }

                                        if (Up && Right && blockIDR != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDR][rVar]];
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;
                                            virtualPos += new Vector2(HalfTextureSize, 0);

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            virtualPos = new Vector2(xp * TextureSize, yp * TextureSize) + Origin;
                                            ARS2++;
                                        }

                                        if (Down && Right && blockIDD != 0)
                                        {
                                            rect = TileData.Rectangles[TileData.BlockTextureIDs[blockIDD][dVar]];
                                            rect.X += QuarterTextureSize;
                                            rect.Y += QuarterTextureSize;
                                            rect.Width -= HalfTextureSize;
                                            rect.Height -= HalfTextureSize;
                                            virtualPos += new Vector2(HalfTextureSize, HalfTextureSize);

                                            spriteBatch.Draw(TileData.BlockSheets[0], virtualPos, rect, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0f);
                                            ARS2++;
                                        }

                                        #endregion
                                    }
                                }
                            }
                            yp++;
                        }
                        yp = 0;
                        xp++;
                    }

                    spriteBatch.End();
                    
                    CurrentRenderChunk++;
                    CounterY++;
                }

                if (x != RenderChunkX2)
                {
                    CounterY = 0;
                    CounterX++;                  
                }
 
            }



            graphics.SetRenderTarget(null);

            //int i = 0;

            if (kState.IsKeyDown(Keys.Space) && pKState.IsKeyUp(Keys.Space))
                for (int x = 0; x <= RenderChunksX; x++)
                {
                    for (int y = 0; y <= RenderChunksY; y++)
                    {
                        using (FileStream f = new FileStream("C://test//" + x + "." + y + ".B" + ".png", FileMode.Create))                        
                            BlockRenderTargets[x][y].SaveAsPng(f, 2048, 2048);

                        using (FileStream f = new FileStream("C://test//" + x + "." + y + ".S" + ".png", FileMode.Create))
                            SlopeRenderTargets[x][y].SaveAsPng(f, 2048, 2048);
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch, int CameraNumber)
        {
            //Get Draw Code to wrap.

            #region OldDraw

            //for (int x = RenderChunkX; x <= RenderChunkX2; x++)
            //{
            //    for (int y = RenderChunkY; y <= RenderChunkY2; y++)
            //    {
            //        for (int x2 = 0; x2 < chunkTiles; x2++)
            //        {
            //            for (int y2 = 0; y2 < chunkTiles; y2++)
            //            {
            //                if (chunks[x][y].ChunkActive)
            //                {    
            //                    if ((x * chunkTiles) + x2 > RenderTileX && (x * chunkTiles) + x2 < RenderTileX2 &&
            //                        (y * chunkTiles) + y2 > RenderTileY && (y * chunkTiles) + y2 < RenderTileY2)
            //                    {
            //                        int ID = chunks[x][y].tiles[x2][y2].BlockID;

            //                        //Texture2D tex = ;

            //                        if (ID != 0)
            //                        {
            //                            position.X = worldPosition.X + (x2 * tileSize) + (x * (chunkTiles * tileSize));
            //                            position.Y = worldPosition.Y + (y2 * tileSize) + (y * (chunkTiles * tileSize));

            //                            Rectangle rect;
            //                            Color color;
            //                            int var = chunks[x][y].tiles[x2][y2].variation;

            //                            color = chunks[x][y].tiles[x2][y2].BlockColor;

            //                            if (var >= TileData.BlockTextureIDs[ID].Count)
            //                                var = 0;

            //                            rect = TileData.Rectangles[TileData.BlockTextureIDs[ID][var]];                                        

            //                            Vector2 origin = new Vector2(16, 16);

            //                            float rotation = 0f;

            //                            spriteBatch.Draw(TileData.BlockSheets[0], position, rect, color, rotation, origin, 1f, SpriteEffects.None, 0f);
            //                        }
            //                        else
            //                        {
            //                            bool Left = false;
            //                            bool Right = false;
            //                            bool Up = false;
            //                            bool Down = false;
            //                            Vector2 pos = Vector2.Zero;

            //                            int blockIDL = 0;
            //                            int blockIDU = 0;
            //                            int blockIDR = 0;
            //                            int blockIDD = 0;

            //                            int lVar = 0;
            //                            int uVar = 0;
            //                            int rVar = 0;
            //                            int dVar = 0;

            //                            Color lColor = Color.White;
            //                            Color uColor = Color.White;
            //                            Color rColor = Color.White;
            //                            Color dColor = Color.White;

            //                            GetTile(x, y, x2 - 1, y2);

            //                            if (ActiveChunk)
            //                            {
            //                                blockIDL = chunks[CX][CY].tiles[TX][TY].BlockID;
            //                                lVar = chunks[CX][CY].tiles[TX][TY].slopeVariation;
            //                                lColor = chunks[CX][CY].tiles[TX][TY].BlockColor;

            //                                if (lVar >= TileData.SlopeTextureIDs[blockIDL].Count)
            //                                {
            //                                    lVar = random.Next(0, TileData.SlopeTextureIDs[blockIDL].Count);
            //                                    chunks[CX][CY].tiles[TX][TY].slopeVariation = (byte)lVar;
            //                                }

            //                                if (blockIDL != 0)
            //                                    Left = true;
            //                            }

            //                            GetTile(x, y, x2 + 1, y2);

            //                            if (ActiveChunk)
            //                            {
            //                                blockIDR = chunks[CX][CY].tiles[TX][TY].BlockID;
            //                                rVar = chunks[CX][CY].tiles[TX][TY].slopeVariation;
            //                                rColor = chunks[CX][CY].tiles[TX][TY].BlockColor;

            //                                if (rVar >= TileData.SlopeTextureIDs[blockIDR].Count)
            //                                {
            //                                    rVar = random.Next(0, TileData.SlopeTextureIDs[blockIDR].Count);
            //                                    chunks[CX][CY].tiles[TX][TY].slopeVariation = (byte)rVar;
            //                                }

            //                                if (blockIDR != 0)
            //                                    Right = true;
            //                            }

            //                            GetTile(x, y, x2, y2 - 1);

            //                            if (ActiveChunk)
            //                            {
            //                                blockIDU = chunks[CX][CY].tiles[TX][TY].BlockID;
            //                                uVar = chunks[CX][CY].tiles[TX][TY].slopeVariation;
            //                                uColor = chunks[CX][CY].tiles[TX][TY].BlockColor;

            //                                if (uVar >= TileData.SlopeTextureIDs[blockIDU].Count)
            //                                {
            //                                    uVar = random.Next(0, TileData.SlopeTextureIDs[blockIDU].Count);
            //                                    chunks[CX][CY].tiles[TX][TY].slopeVariation = (byte)uVar;
            //                                }

            //                                if (blockIDU != 0)
            //                                    Up = true;
            //                            }

            //                            GetTile(x, y, x2, y2 + 1);

            //                            if (ActiveChunk)
            //                            {
            //                                blockIDD = chunks[CX][CY].tiles[TX][TY].BlockID;
            //                                dVar = chunks[CX][CY].tiles[TX][TY].slopeVariation;
            //                                dColor = chunks[CX][CY].tiles[TX][TY].BlockColor;

            //                                if (dVar >= TileData.SlopeTextureIDs[blockIDD].Count)
            //                                {
            //                                    lVar = random.Next(0, TileData.SlopeTextureIDs[blockIDD].Count);
            //                                    chunks[CX][CY].tiles[TX][TY].slopeVariation = (byte)dVar;
            //                                }

            //                                if (blockIDD != 0)
            //                                    Down = true;
            //                            }

            //                            Vector2 origin = new Vector2(8, 8);
                                        
            //                            Rectangle rect;

            //                            float rotation = 0f;

            //                            if (Up && Left && blockIDU == blockIDL)
            //                            {
            //                                pos = new Vector2(((x * chunkTiles) + x2) * tileSize, ((y * chunkTiles) + y2) * tileSize);

            //                                rect = TileData.Rectangles[TileData.SlopeTextureIDs[blockIDU][uVar]];
            //                                rect.Width -= 16;
            //                                rect.Height -= 16;

            //                                switch (chunks[x][y].tiles[x2][y2].rotation)
            //                                {
            //                                    case 1:
            //                                        rotation = (float)Math.PI * 1.5f;
            //                                        rect.X += 16;
            //                                        break;

            //                                    case 2:
            //                                        rotation = (float)Math.PI / 2;
            //                                        rect.Y += 16;
            //                                        break;

            //                                    case 3:
            //                                        rotation = (float)Math.PI;
            //                                        rect.X += 16;
            //                                        rect.Y += 16;
            //                                        break;
            //                                }
                                         
            //                                spriteBatch.Draw(TileData.SlopeSheets[0], pos, rect, uColor, rotation, origin, 1f, SpriteEffects.None, 0f);
            //                            }

            //                            if (Down && Left && blockIDD == blockIDL)
            //                            {
            //                                pos = new Vector2(((x * chunkTiles) + x2) * tileSize, (((y * chunkTiles) + y2) * tileSize));
            //                                rect = TileData.Rectangles[TileData.SlopeTextureIDs[blockIDL][lVar]];
            //                                rect.Width -= 16;
            //                                rect.Height -= 16;
                                            
            //                                switch (chunks[x][y].tiles[x2][y2].rotation)
            //                                {
            //                                    case 0:
            //                                        rect.Y += 16;
            //                                        break;

            //                                    case 1:
            //                                        rotation = (float)Math.PI;
            //                                        rect.X += 16;
            //                                        break;

            //                                    case 2:
            //                                        rotation = (float)Math.PI / 2f;
            //                                        rect.X += 16;
            //                                        rect.Y += 16;
            //                                        break;

            //                                    case 3:
            //                                        rotation = (float)Math.PI * 1.5f;
            //                                        break;
            //                                }

            //                                spriteBatch.Draw(TileData.SlopeSheets[0], pos, rect, lColor, rotation, origin, 1f, SpriteEffects.None, 0f);
            //                            }

            //                            if (Up && Right && blockIDU == blockIDR)
            //                            {
            //                                pos = new Vector2((((x * chunkTiles) + x2) * tileSize), ((y * chunkTiles) + y2) * tileSize);
            //                                rect = TileData.Rectangles[TileData.SlopeTextureIDs[blockIDR][rVar]];
            //                                rect.Width -= 16;
            //                                rect.Height -= 16;

            //                                switch (chunks[x][y].tiles[x2][y2].rotation)
            //                                {
            //                                    case 0:                                                    
            //                                        rect.X += 16;
            //                                        break;

            //                                    case 1:
            //                                        rotation = (float)Math.PI;
            //                                        rect.Y += 16;
            //                                        break;


            //                                    case 2:
            //                                        rotation = (float)Math.PI * 1.5f;
            //                                        rect.X += 16;
            //                                        rect.Y += 16;
            //                                        break;

            //                                    case 3:
            //                                        rotation = (float)Math.PI / 2f;
            //                                        break;
            //                                }

            //                                spriteBatch.Draw(TileData.SlopeSheets[0], pos, rect, rColor, rotation, origin, 1f, SpriteEffects.None, 0f);
            //                            }

            //                            if (Down && Right && blockIDD == blockIDR)
            //                            {
            //                                pos = new Vector2((((x * chunkTiles) + x2) * tileSize), (((y * chunkTiles) + y2) * tileSize));
            //                                rect = TileData.Rectangles[TileData.SlopeTextureIDs[blockIDD][dVar]];
            //                                rect.Width -= 16;
            //                                rect.Height -= 16;

            //                                switch (chunks[x][y].tiles[x2][y2].rotation)
            //                                {
            //                                    case 0:                                                    
            //                                        rect.X += 16;
            //                                        rect.Y += 16;
            //                                        break;

            //                                    case 1:
            //                                        rotation = (float)Math.PI / 2f;
            //                                        rect.X += 16;
            //                                        break;

            //                                    case 2:
            //                                        rotation = (float)Math.PI * 1.5f;
            //                                        rect.Y += 16;
            //                                        break;

            //                                    case 3:
            //                                        rotation = (float)Math.PI;
            //                                        break;
            //                                }
            //                                spriteBatch.Draw(TileData.SlopeSheets[0], pos, rect, dColor, rotation, origin, 1f, SpriteEffects.None, 0f);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            int TexX;
            int TexY;
            Vector2 RBPos;
            Rectangle RBRect;
            Color RBColor;
            float RBRotation;



            for (int i = 0; i < renderBlocks.Count; i++)
            {
                TexX = (int)renderBlocks[i].TextureIndex.X;
                TexY = (int)renderBlocks[i].TextureIndex.Y;
                RBPos = renderBlocks[i].Position;
                RBRect = renderBlocks[i].TextureRectangle;
                RBColor = renderBlocks[i].Color;
                RBRotation = renderBlocks[i].BlockRotation;

                //RBColor = Color.Yellow;

                spriteBatch.Draw(BlockRenderTargets[TexX][TexY], RBPos, RBRect, RBColor, RBRotation, BlockOrigin, 1f, SpriteEffects.None, 0f);
            }

            for (int i = 0; i < renderSlopes.Count; i++)
            {
                TexX = (int)renderSlopes[i].TextureIndex.X;
                TexY = (int)renderSlopes[i].TextureIndex.Y;
                RBPos = renderSlopes[i].Position;
                RBRect = renderSlopes[i].TextureRectangle;
                RBColor = renderSlopes[i].Color;

                spriteBatch.Draw(SlopeRenderTargets[TexX][TexY], RBPos, RBRect, RBColor, 0f, SlopeOrigin, 1f, SpriteEffects.None, 0f);
            }

            //for (int x = 0; x <= RenderChunksX; x++)
            //{
            //    for (int y = 0; y <= RenderChunksY; y++)
            //    {
            //        spriteBatch.Draw(BlockRenderTargets[x][y], CameraManager.CamerasRead[0].Focus - new Vector2(1280 / 2, 720 / 2) + new Vector2(x * 2048, y * 2048), Color.White);
            //    }
            //}
            
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
            spriteBatch.Draw(TileData.MaskSheets[0], new Vector2(2048, 0), Color.White);
            spriteBatch.Draw(TileData.DecoSheets[0], new Vector2(4096, 0), Color.White);
        }
    }
}
