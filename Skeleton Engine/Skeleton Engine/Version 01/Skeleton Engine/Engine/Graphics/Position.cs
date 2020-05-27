using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    public class Location
    {
        public Vector2 Position;
        public BigInteger ChunkX;
        public BigInteger ChunkY;
        static public float ChunkSize = 500f;

        public Vector2 RoughPosition
        {
            get { return Position + new Vector2((float)ChunkX * ChunkSize, (float)ChunkY * ChunkSize); }
        }

        public Location()
        {
            Position = Vector2.Zero;
            ChunkX = 0;
            ChunkY = 0;
        }

        public Location(Vector2 position)
        {
            Position = position;
            ChunkX = 0;
            ChunkY = 0;

            Update();
        }

        public Location(Location position)
        {
            Position = position.Position;
            ChunkX = position.ChunkX;
            ChunkY = position.ChunkY;

            Update();
        }

        public Location(float x, float y)
        {
            Position = new Vector2(x, y);
            Update();
        }

        public Location(float x, float y, BigInteger chunkX, BigInteger chunkY)
        {
            Position = new Vector2(x, y);
            ChunkX = chunkX;
            ChunkY = chunkY;
            Update();
        }

        public void Update()
        {
            if (Position.X > ChunkSize / 2f)
            {
                ChunkX++;
                Position.X -= ChunkSize;

                if (Position.X > ChunkSize)
                {
                    float x = (float)Math.Floor(Position.X / ChunkSize);

                    ChunkX += (int)x;
                    Position.X -= ChunkSize * x;
                }
            }
            else if (Position.X <= -ChunkSize / 2f)
            {
                ChunkX--;
                Position.X += ChunkSize;

                if (Position.X <= -ChunkSize)
                {
                    float x = (float)Math.Floor(Position.X / -ChunkSize);

                    ChunkX -= (int)x;
                    Position.X += ChunkSize * x;
                }
            }

            if (Position.Y > ChunkSize / 2f)
            {
                ChunkY++;
                Position.Y -= ChunkSize;

                if (Position.Y > ChunkSize)
                {
                    float y = (float)Math.Floor(Position.Y / ChunkSize);

                    ChunkY += (int)y;
                    Position.Y -= ChunkSize * y;
                }
            }
            else if (Position.Y <= -ChunkSize / 2f)
            {
                ChunkY--;
                Position.Y += ChunkSize;

                if (Position.Y <= -ChunkSize)
                {
                    float y = (float)Math.Floor(Position.Y / -ChunkSize);

                    ChunkY -= (int)y;
                    Position.Y += ChunkSize * y;
                }
            }
        }

        public bool SameChunk(Location position)
        {
            if (ChunkX != position.ChunkX)
                return false;
            if (ChunkY != position.ChunkY)
                return false;

            return true;
        }

        public Location Invert(Location pos)
        {
            pos.Position = -pos.Position;
            pos.ChunkX = -pos.ChunkX;
            pos.ChunkY = -pos.ChunkY;

            return pos;
        }
        
        public float Length()
        {
            float length = Vector2.Distance(Vector2.Zero, Position);
            float chunkLength = Vector2.Distance(Vector2.Zero, new Vector2((float)ChunkX, (float)ChunkY)) * ChunkSize;

            return length + chunkLength;
        }

        static public Location CameraTransform(Location camera, Location position)
        {
            Location final = new Location(position);
            final.ChunkX -= camera.ChunkX;
            final.ChunkY -= camera.ChunkY;

            return final;
        }
        static public Vector2 ChunkDifference(Location position1, Location position2)
        {
            Vector2 difference = new Vector2((float)position1.ChunkX, (float)position1.ChunkY) - new Vector2((float)position2.ChunkX, (float)position2.ChunkY);
            return difference * ChunkSize;
        }
        static public float Distance(Location position1, Location position2)
        {
            float distance = Vector2.Distance(position1.Position, position2.Position);

            float distanceX = (float)(position1.ChunkX - position2.ChunkX) * ChunkSize;
            float distanceY = (float)(position1.ChunkY - position2.ChunkY) * ChunkSize;

            float chunkDistance = Vector2.Distance(Vector2.Zero, new Vector2(distanceX, distanceY));

            return distance + chunkDistance;
        }
        static public Location operator +(Location position1, Location position2)
        {
            Location position = new Location();

            position.Position = position1.Position + position2.Position;
            position.ChunkX = position1.ChunkX + position2.ChunkX;
            position.ChunkY = position1.ChunkY + position2.ChunkY;

            position.Update();
            return position;
        }
        static public Location operator -(Location position1, Location position2)
        {
            Location position = new Location();

            position.Position = position1.Position - position2.Position;
            position.ChunkX = position1.ChunkX - position2.ChunkX;
            position.ChunkY = position1.ChunkY - position2.ChunkY;

            position.Update();

            return position;
        }
        static public Location operator +(Location position1, Vector2 position2)
        {
            Location position = new Location();
            Location vectorPos = new Location(position2);

            position.Position = position1.Position + vectorPos.Position;
            position.ChunkX = position1.ChunkX + vectorPos.ChunkX;
            position.ChunkY = position1.ChunkY + vectorPos.ChunkY;

            position.Update();

            return position;
        }
        static public Location operator -(Location position1, Vector2 position2)
        {
            Location position = new Location();
            Location vectorPos = new Location(position2);

            position.Position = position1.Position - vectorPos.Position;
            position.ChunkX = position1.ChunkX - vectorPos.ChunkX;
            position.ChunkY = position1.ChunkY - vectorPos.ChunkY;

            position.Update();

            return position;
        }
        static public Location operator +(Vector2 position2, Location position1)
        {
            Location position = new Location();
            Location vectorPos = new Location(position2);

            position.Position = position1.Position + vectorPos.Position;
            position.ChunkX = position1.ChunkX + vectorPos.ChunkX;
            position.ChunkY = position1.ChunkY + vectorPos.ChunkY;

            position.Update();

            return position;
        }
        static public Location operator -(Vector2 position2, Location position1)
        {
            Location position = new Location();
            Location vectorPos = new Location(position2);

            position.Position = vectorPos.Position - position1.Position;
            position.ChunkX = vectorPos.ChunkX - position1.ChunkX;
            position.ChunkY = vectorPos.ChunkY - position1.ChunkY;

            position.Update();

            return position;
        }
        static public Location operator *(Location position1, float delta)
        {
            Location position = new Location();

            position.Position = position1.Position * delta;
            float remainder = 0f;

            if (position1.ChunkX != 0)
            {
                float chunkX = (float)position1.ChunkX * delta;
                remainder = chunkX - (float)Math.Floor((float)position1.ChunkX * delta);

                position.ChunkX = (BigInteger)(chunkX - remainder);
                position.Position.X += ChunkSize * remainder;
            }
            else
                position.ChunkX = 0;
            
            if (position1.ChunkY != 0)
            {
                float chunkY = (float)position1.ChunkY * delta;
                remainder = chunkY - (float)Math.Floor((float)position1.ChunkY * delta);

                position.ChunkY = (BigInteger)(chunkY - remainder);
                position.Position.Y += ChunkSize * remainder;
            }
            else
                position.ChunkY = 0;

            position.Update();

            return position;
        }
        static public Location operator /(Location position1, float delta)
        {
            Location position = new Location();

            position.Position = position1.Position / delta;
            float remainder = 0f;

            if (position1.ChunkX != 0)
            {
                float chunkX = (float)position1.ChunkX / delta;
                remainder = chunkX - (float)Math.Floor((float)position1.ChunkX / delta);

                position.ChunkX = (BigInteger)(chunkX - remainder);
                position.Position.X += ChunkSize * remainder;
            }
            else
                position.ChunkX = 0;

            if (position1.ChunkY != 0)
            {
                float chunkY = (float)position1.ChunkY / delta;
                remainder = chunkY - (float)Math.Floor((float)position1.ChunkY / delta);

                position.ChunkY = (BigInteger)(chunkY - remainder);
                position.Position.Y += ChunkSize * remainder;
            }
            else
                position.ChunkY = 0;

            position.Update();

            return position;
        }
        static public bool operator ==(Location left, Location right)
        {
            if (left.Position == right.Position)
                if (left.ChunkX == right.ChunkX && left.ChunkY == right.ChunkY)
                    return true;

            return false;
        }
        static public bool operator !=(Location left, Location right)
        {
            if (left.Position != right.Position || left.ChunkX != right.ChunkX || left.ChunkY != right.ChunkY)
                    return true;

            return false;
        }
    }
}
