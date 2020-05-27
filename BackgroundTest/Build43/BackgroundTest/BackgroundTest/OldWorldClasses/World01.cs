using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class World1
    {
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

            random = new Random();
            worldPosition = Vector2.Zero;

            chunkX = 100;
            chunkY = 100;

            tilesX = (short)(chunkX * 64);
            tilesY = (short)(chunkY * 64);



            tileID = new ushort[tilesX][];

            for (int i = 0; i < tileID.Length; i++)
                tileID[i] = new ushort[tilesY];

            


            //backgroundID = new byte[tilesX][];

            //for (int i = 0; i < backgroundID.Length; i++)
            //    backgroundID[i] = new byte[tilesY];

            
            WorldGeneration();
        }

        private void WorldGeneration()
        {
            for (int x = 0; x < tileID.Length; x++)
            {
                for (int y = 0; y < tileID[x].Length; y++)
                {
                    if (y == 100 && y > random.Next(100, 103))
                    {
                        tileID[x][y] = 1;
                    }
                    if (y > 1000)
                    {
                        tileID[x][y] = (ushort)random.Next(0, 2);
                    }
                    else
                    {
                        tileID[x][y] = 0;
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

            RenderTileX = (short)((((CameraPosition.X - worldPosition.X) / tileSize) - ((WorldVariables.WindowWidth / 2) / tileSize)) - 1);
            RenderTileY = (short)((((CameraPosition.Y - worldPosition.Y) / tileSize) - ((WorldVariables.WindowHeight / 2) / tileSize)) - 1);
            RenderTileX2 = (short)((RenderTileX + (WorldVariables.WindowWidth / tileSize)) + 1);
            RenderTileY2 = (short)((RenderTileY + (WorldVariables.WindowHeight / tileSize)) + 1);

            {
                if (RenderTileX < 0)
                    RenderTileX = 0;
                else if (RenderTileX > chunkSize * chunkX)
                    RenderTileX = (short)(chunkSize * chunkX);

                if (RenderTileX2 < 0)
                    RenderTileX2 = 0;
                else if (RenderTileX2 >= chunkSize * chunkX)
                    RenderTileX2 = (short)((chunkSize * chunkX) - 1);

                if (RenderTileY < 0)
                    RenderTileY = 0;
                else if (RenderTileY > chunkSize * chunkY)
                    RenderTileY = (short)(chunkSize * chunkY);

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

                        spriteBatch.Draw(tileTextures[tileID[x][y]], position, Color.SandyBrown);
                    }
                }
            }
        }
    }
}
