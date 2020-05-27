using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    class SpacePosition
    {
        class ExactPosition
        {
            decimal X;
            decimal Y;


        }

        public Vector2 Position;
        public long PositionX;
        public long PositionY;
        public Vector2 RoughPosition;

        public void Update()
        {
            if (Position.X >= 1000000f)
            {
                PositionX++;
                Position.X -= 2000000f;

                if (Position.X >= 2000000f)
                {
                    float x = Position.X / 2000000f;

                    PositionX += (int)x;
                    Position.X -= 2000000f * x;
                }
            }

            if (Position.X <= -1000000f)
            {
                PositionX--;
                Position.X += 2000000f;

                if (Position.X <= -2000000f)
                {
                    float x = Position.X / -2000000f;

                    PositionX -= (int)x;
                    Position.X += 2000000f * x;
                }
            }

            if (Position.Y >= 1000000f)
            {
                PositionY++;
                Position.Y -= 2000000f;

                if (Position.Y >= 2000000f)
                {
                    float y = Position.Y / 2000000f;

                    PositionY += (int)y;
                    Position.Y -= 2000000f * y;
                }
            }

            if (Position.Y <= -1000000f)
            {
                PositionY--;
                Position.Y += 2000000f;

                if (Position.Y <= -2000000f)
                {
                    float y = Position.Y / -2000000f;

                    PositionY -= (int)y;
                    Position.Y += 2000000f * y;
                }
            }

            SetRough();
        }

        public void SetRough()
        {
            RoughPosition = Position;
            RoughPosition.X += PositionX * 2000000f;
            RoughPosition.Y += PositionY * 2000000f;
        }
    }
}
