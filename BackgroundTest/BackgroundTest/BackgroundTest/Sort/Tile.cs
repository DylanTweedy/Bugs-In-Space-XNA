using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Tile
    {
        //Rectangle bounds;
        Vector2 position;
        //Color[][] texData;
        byte tileType;
        byte tileID;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public byte TileID
        {
            get { return tileID; }
            set { tileID = value; }
        }

        //public Rectangle Bounds
        //{
        //    get { return bounds; }
        //    set { bounds = value; }
        //}

        //public Color[][] TextureData
        //{
        //    get { return texData; }
        //    set { texData = value; }
        //}

        public byte TileType
        {
            get { return tileType; }
            set { tileType = value; }
        }

        public Tile(Rectangle b, Vector2 p)
        {
            //bounds = b;
            position = p;
        }

        public void LoadTileData(int tileSize, Texture2D tex)
        {
            //Texture2D Texture = tex;
            //Color[] LoadedTexture = new Color[tileSize * tileSize];
            //Texture.GetData(0, bounds, LoadedTexture, 0, tileSize * tileSize);

            //texData = new Color[tileSize][];
            //for (int i = 0; i < texData.Length; i++)
            //    texData[i] = new Color[tileSize];

            //for (int y = 0; y < tileSize; y++)
            //{
            //    for (int x = 0; x < tileSize; x++)
            //    {
            //        // Assumes row major ordering of the array.
            //        texData[x][y] = LoadedTexture[(y * tileSize) + x];
            //    }
            //}
        }

        public void LoadContent()
        {
        }

        public void Update()
        {
        }

        public void Draw()
        {
        }

    }
}
