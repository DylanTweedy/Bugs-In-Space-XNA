using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class TestLevel
    {
        Chunk[][] chunks;
        List<Texture2D> textures;

        int chunkSize;
        int tileSize;
        
        int chunkX;
        int chunkY;
        int chunkCount;

        int tilesPerRow;

        int levelWidth;
        int levelHeight;
        
        int selectedChunk;
        int selectedTile;
        Vector2 selectedChunkPos;
        Vector2 selectedTilePos;
        bool selectedTileAir;
        Color[] selectedTextureData;

        Vector2 levelPosition;

        #region  Properties

        public Chunk ReturnChunk(int x, int y)
        {
            return chunks[x][y];
        }

        public Vector2 LevelPosition
        {
            get { return levelPosition; }
        }
        
        public int ChunkSize
        {
            get { return chunkSize; }
        }

        public int TileSize
        {
            get { return tileSize; }
        }

        public int ChunkCount
        {
            get { return chunkCount; }
        }

        public int ChunkRows
        {
            get { return chunkX; }
        }

        public int ChunkColumns
        {
            get { return chunkY; }
        }

        public int TilesPerRow
        {
            get { return tilesPerRow; }
        }

        public int LevelWidth
        {
            get { return levelWidth; }
        }

        public int LevelHeight
        {
            get { return levelHeight; }
        }

        public int SelectedChunk
        {
            get { return selectedChunk; }
        }

        public int SelectedTile
        {
            get { return selectedTile; }
        }

        public Vector2 SelectedChunkPosition
        {
            get { return selectedChunkPos; }
        }

        public Vector2 SelectedTilePosition
        {
            get { return selectedTilePos; }
        }

        public bool SelectedTileAir
        {
            get { return selectedTileAir; }
        }

        #endregion

        public void Initialize()
        {
            //chunks = new List<Chunk>();
            textures = new List<Texture2D>();

            levelPosition = new Vector2(0, 0);

            tileSize = 16;
            chunkSize = 1024;
            tilesPerRow = chunkSize / tileSize;

            chunkX = 4;
            chunkY = 1;
            chunkCount = chunkX * chunkY;

            levelWidth = chunkSize * chunkX;
            levelHeight = chunkSize * chunkY;

            //int i = 0;

            chunks = new Chunk[chunkX][];

            for (int i = 0; i < chunks.Length; i++)
                chunks[i] = new Chunk[chunkY];

            for (int y = 0; y < chunks.Length; y++)
            {
                for (int x = 0; x < chunks[y].Length; x++)
                {
                    chunks[x][y] = new Chunk(levelPosition + new Vector2(chunkSize * x, chunkSize * y), tileSize);
                }
            }
        }
                

        public void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Images//test//test"));
            textures.Add(Content.Load<Texture2D>("Images//test//test3"));
            textures.Add(Content.Load<Texture2D>("Images//test//Untitled1"));
            textures.Add(Content.Load<Texture2D>("Images//test//Untitled2"));

            int t = 0;

            for (int y = 0; y < chunks.Length; y++)
            {
                for (int x = 0; x < chunks[y].Length; x++)
                {
                    chunks[x][y].LoadContent(textures[t]);
                    
                    t++;
                    if (t == textures.Count)
                        t = 0;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void CalculateLocation(Vector2 pos)
        {   
            if (pos.X < levelPosition.X || pos.Y < levelPosition.Y || pos.X >= levelPosition.X + levelWidth || pos.Y >= levelPosition.Y + levelHeight)
            {
                selectedChunk = -1;
                selectedTile = -1;
            }
            else
            {
                Vector2 chunkPositionNum = pos - levelPosition;

                int x = (int)(chunkPositionNum.X / chunkSize);
                int y = (int)(chunkPositionNum.Y / chunkSize);

                selectedChunk = (chunkX * (y + 1)) - (chunkX - x);
                selectedChunkPos = new Vector2((selectedChunk % chunkX) * chunkSize, (selectedChunk / chunkX) * chunkSize);

                int x2 = (int)Math.Abs((pos.X - selectedChunkPos.X) / tileSize);
                int y2 = (int)Math.Abs((pos.Y - selectedChunkPos.Y) / tileSize);

                selectedTile = (tilesPerRow * (y2 + 1)) - (tilesPerRow - x2);
                selectedTilePos = new Vector2((selectedTile % tilesPerRow) * tileSize, (selectedTile / tilesPerRow) * tileSize);
                
                //selectedTileAir = LevelLoader.level.ReturnChunk(selectedChunk).tiles[selectedTile].Transparent;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < chunks.Length; y++)
            {
                for (int x = 0; x < chunks[y].Length; x++)
                {
                    chunks[x][y].Draw(spriteBatch);
                }
            }
        }


    }
}
