using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SkeletonEngine
{
    static public class DungeonVisualizer
    {
        static private GeometryVisualizer Visualizer = new GeometryVisualizer();
        static public float TileSize = 32f;

        static public void Add(SpacePosition position, byte tileType)
        {
            Visualizer.AddData(position, 0f, 4, new Vector2(TileSize), GetTileTypeColor(tileType));
        }

        static private Color GetTileTypeColor(byte tileType)
        {
            switch (tileType)
            {
                case 0:
                    return Color.White;

                case 1:
                    return Color.Red;

                case 2:
                    return Color.Blue;

                default:
                    return Color.White;
            }
        }

        static public void Draw(Camera camera)
        {
            Visualizer.Draw(camera, FillMode.WireFrame);
        }
    }

    class RoomTile
    {
        byte TileType;
        ushort TileChoice;
        SpacePosition Position;

        #region Initialize

        public void Initialize(SpacePosition position, byte tileType, ushort tileChoice)
        {
            Position = position;
            TileType = tileType;
            TileChoice = tileChoice;
            
        }

        public void Initialize(SpacePosition position, string tileType, ushort tileChoice)
        {
            Position = position;
            TileType = ConvertStringToByte(tileType);
            TileChoice = tileChoice;
        }

        private byte ConvertStringToByte(string tileType)
        {
            switch (tileType)
            {
                case "empty":
                    return 0;

                case "wall":
                    return 1;

                case "door":
                    return 2;

                default:
                    return 0;
            }
        }

        private void SetPosition(SpacePosition position)
        {
            Position = position;
        }

        #endregion

        public void Update()
        {            
            DungeonVisualizer.Add(Position, TileType);
        }

        public void Draw(Camera camera)
        {
        }
    }

    class Room
    {
        public RoomTile[][] FloorPlan;
        public int Width;
        public int Height;
        public SpacePosition Position;

        private void TestInitialize()
        {

        }

        public Room(SpacePosition position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;

            FloorPlan = new RoomTile[Width][];
            for (int i = 0; i < Width; i++)
                FloorPlan[i] = new RoomTile[Height];

            SpacePosition tilePosition = Position - new SpacePosition((Width / 2f) * DungeonVisualizer.TileSize, ((Height / 2f) * DungeonVisualizer.TileSize));
            tilePosition += new Vector2(DungeonVisualizer.TileSize / 2f);


            for (int x = 0; x < FloorPlan.Length; x++)
                for (int y = 0; y < FloorPlan[x].Length; y++)
                {
                    FloorPlan[x][y] = new RoomTile();
                    FloorPlan[x][y].Initialize(tilePosition + new SpacePosition(DungeonVisualizer.TileSize * x, DungeonVisualizer.TileSize * y), 0, 0);
                }
        }

        public void Update()
        {
            for (int x = 0; x < FloorPlan.Length; x++)
                for (int y = 0; y < FloorPlan[x].Length; y++)
                    FloorPlan[x][y].Update();
        }

        public void Draw(Camera camera)
        {
        }
    }
}
