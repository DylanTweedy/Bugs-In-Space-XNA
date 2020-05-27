using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    public class SpacePosition
    {
        public Vector2 Position;
        public BigInteger ChunkX;
        public BigInteger ChunkY;

        public Vector2 RoughPosition
        {
            get { return Position + new Vector2((float)ChunkX * GlobalVariables.CameraChunkSize, (float)ChunkY * GlobalVariables.CameraChunkSize); }
        }

        public SpacePosition()
        {
            Position = Vector2.Zero;
            ChunkX = 0;
            ChunkY = 0;
        }

        public SpacePosition(Vector2 position)
        {
            Position = position;
            ChunkX = 0;
            ChunkY = 0;

            Update();
        }

        public SpacePosition(SpacePosition position)
        {
            Position = position.Position;
            ChunkX = position.ChunkX;
            ChunkY = position.ChunkY;

            Update();
        }

        public SpacePosition(float x, float y)
        {
            Position = new Vector2(x, y);
            Update();
        }

        public SpacePosition(float x, float y, BigInteger chunkX, BigInteger chunkY)
        {
            Position = new Vector2(x, y);
            ChunkX = chunkX;
            ChunkY = chunkY;
            Update();
        }

        public void Update()
        {
            if (Position.X > GlobalVariables.CameraChunkSize / 2f)
            {
                ChunkX++;
                Position.X -= GlobalVariables.CameraChunkSize;

                if (Position.X > GlobalVariables.CameraChunkSize)
                {
                    float x = (float)Math.Floor(Position.X / GlobalVariables.CameraChunkSize);

                    ChunkX += (int)x;
                    Position.X -= GlobalVariables.CameraChunkSize * x;
                }
            }
            else if (Position.X <= -GlobalVariables.CameraChunkSize / 2f)
            {
                ChunkX--;
                Position.X += GlobalVariables.CameraChunkSize;

                if (Position.X <= -GlobalVariables.CameraChunkSize)
                {
                    float x = (float)Math.Floor(Position.X / -GlobalVariables.CameraChunkSize);

                    ChunkX -= (int)x;
                    Position.X += GlobalVariables.CameraChunkSize * x;
                }
            }

            if (Position.Y > GlobalVariables.CameraChunkSize / 2f)
            {
                ChunkY++;
                Position.Y -= GlobalVariables.CameraChunkSize;

                if (Position.Y > GlobalVariables.CameraChunkSize)
                {
                    float y = (float)Math.Floor(Position.Y / GlobalVariables.CameraChunkSize);

                    ChunkY += (int)y;
                    Position.Y -= GlobalVariables.CameraChunkSize * y;
                }
            }
            else if (Position.Y <= -GlobalVariables.CameraChunkSize / 2f)
            {
                ChunkY--;
                Position.Y += GlobalVariables.CameraChunkSize;

                if (Position.Y <= -GlobalVariables.CameraChunkSize)
                {
                    float y = (float)Math.Floor(Position.Y / -GlobalVariables.CameraChunkSize);

                    ChunkY -= (int)y;
                    Position.Y += GlobalVariables.CameraChunkSize * y;
                }
            }
        }

        public bool SameChunk(SpacePosition position)
        {
            if (ChunkX != position.ChunkX)
                return false;
            if (ChunkY != position.ChunkY)
                return false;

            return true;
        }

        public SpacePosition Invert(SpacePosition pos)
        {
            pos.Position = -pos.Position;
            pos.ChunkX = -pos.ChunkX;
            pos.ChunkY = -pos.ChunkY;

            return pos;
        }
        
        public float Length()
        {
            float length = Vector2.Distance(Vector2.Zero, Position);
            float chunkLength = Vector2.Distance(Vector2.Zero, new Vector2((float)ChunkX, (float)ChunkY)) * GlobalVariables.CameraChunkSize;

            return length + chunkLength;
        }

        static public SpacePosition CameraTransform(SpacePosition camera, SpacePosition position)
        {
            SpacePosition final = new SpacePosition(position);
            final.ChunkX -= camera.ChunkX;
            final.ChunkY -= camera.ChunkY;

            return final;
        }
        static public Vector2 ChunkDifference(SpacePosition position1, SpacePosition position2)
        {
            Vector2 difference = new Vector2((float)position1.ChunkX, (float)position1.ChunkY) - new Vector2((float)position2.ChunkX, (float)position2.ChunkY);
            return difference * GlobalVariables.CameraChunkSize;
        }
        static public float Distance(SpacePosition position1, SpacePosition position2)
        {
            float distance = Vector2.Distance(position1.Position, position2.Position);

            float distanceX = (float)(position1.ChunkX - position2.ChunkX) * GlobalVariables.CameraChunkSize;
            float distanceY = (float)(position1.ChunkY - position2.ChunkY) * GlobalVariables.CameraChunkSize;

            float chunkDistance = Vector2.Distance(Vector2.Zero, new Vector2(distanceX, distanceY));

            return distance + chunkDistance;
        }
        static public SpacePosition operator +(SpacePosition position1, SpacePosition position2)
        {
            SpacePosition position = new SpacePosition();

            position.Position = position1.Position + position2.Position;
            position.ChunkX = position1.ChunkX + position2.ChunkX;
            position.ChunkY = position1.ChunkY + position2.ChunkY;

            position.Update();

            return position;
        }
        static public SpacePosition operator -(SpacePosition position1, SpacePosition position2)
        {
            SpacePosition position = new SpacePosition();

            position.Position = position1.Position - position2.Position;
            position.ChunkX = position1.ChunkX - position2.ChunkX;
            position.ChunkY = position1.ChunkY - position2.ChunkY;

            position.Update();

            return position;
        }
        static public SpacePosition operator +(SpacePosition position1, Vector2 position2)
        {
            SpacePosition position = new SpacePosition();
            SpacePosition vectorPos = new SpacePosition(position2);

            position.Position = position1.Position + vectorPos.Position;
            position.ChunkX = position1.ChunkX + vectorPos.ChunkX;
            position.ChunkY = position1.ChunkY + vectorPos.ChunkY;

            position.Update();

            return position;
        }
        static public SpacePosition operator -(SpacePosition position1, Vector2 position2)
        {
            SpacePosition position = new SpacePosition();
            SpacePosition vectorPos = new SpacePosition(position2);

            position.Position = position1.Position - vectorPos.Position;
            position.ChunkX = position1.ChunkX - vectorPos.ChunkX;
            position.ChunkY = position1.ChunkY - vectorPos.ChunkY;

            position.Update();

            return position;
        }
        static public SpacePosition operator +(Vector2 position2, SpacePosition position1)
        {
            SpacePosition position = new SpacePosition();
            SpacePosition vectorPos = new SpacePosition(position2);

            position.Position = position1.Position + vectorPos.Position;
            position.ChunkX = position1.ChunkX + vectorPos.ChunkX;
            position.ChunkY = position1.ChunkY + vectorPos.ChunkY;

            position.Update();

            return position;
        }
        static public SpacePosition operator -(Vector2 position2, SpacePosition position1)
        {
            SpacePosition position = new SpacePosition();
            SpacePosition vectorPos = new SpacePosition(position2);

            position.Position = vectorPos.Position - position1.Position;
            position.ChunkX = vectorPos.ChunkX - position1.ChunkX;
            position.ChunkY = vectorPos.ChunkY - position1.ChunkY;

            position.Update();

            return position;
        }
        static public SpacePosition operator *(SpacePosition position1, float delta)
        {
            SpacePosition position = new SpacePosition();

            position.Position = position1.Position * delta;
            float remainder = 0f;

            if (position1.ChunkX != 0)
            {
                float chunkX = (float)position1.ChunkX * delta;
                remainder = chunkX - (float)Math.Floor((float)position1.ChunkX * delta);

                position.ChunkX = (BigInteger)(chunkX - remainder);
                position.Position.X += GlobalVariables.CameraChunkSize * remainder;
            }
            else
                position.ChunkX = 0;
            
            if (position1.ChunkY != 0)
            {
                float chunkY = (float)position1.ChunkY * delta;
                remainder = chunkY - (float)Math.Floor((float)position1.ChunkY * delta);

                position.ChunkY = (BigInteger)(chunkY - remainder);
                position.Position.Y += GlobalVariables.CameraChunkSize * remainder;
            }
            else
                position.ChunkY = 0;

            position.Update();

            return position;
        }
        static public SpacePosition operator /(SpacePosition position1, float delta)
        {
            SpacePosition position = new SpacePosition();

            position.Position = position1.Position / delta;
            float remainder = 0f;

            if (position1.ChunkX != 0)
            {
                float chunkX = (float)position1.ChunkX / delta;
                remainder = chunkX - (float)Math.Floor((float)position1.ChunkX / delta);

                position.ChunkX = (BigInteger)(chunkX - remainder);
                position.Position.X += GlobalVariables.CameraChunkSize * remainder;
            }
            else
                position.ChunkX = 0;

            if (position1.ChunkY != 0)
            {
                float chunkY = (float)position1.ChunkY / delta;
                remainder = chunkY - (float)Math.Floor((float)position1.ChunkY / delta);

                position.ChunkY = (BigInteger)(chunkY - remainder);
                position.Position.Y += GlobalVariables.CameraChunkSize * remainder;
            }
            else
                position.ChunkY = 0;

            position.Update();

            return position;
        }
        static public bool operator ==(SpacePosition left, SpacePosition right)
        {
            if (left.Position == right.Position)
                if (left.ChunkX == right.ChunkX && left.ChunkY == right.ChunkY)
                    return true;

            return false;
        }
        static public bool operator !=(SpacePosition left, SpacePosition right)
        {
            if (left.Position != right.Position || left.ChunkX != right.ChunkX || left.ChunkY != right.ChunkY)
                    return true;

            return false;
        }
    }
}
