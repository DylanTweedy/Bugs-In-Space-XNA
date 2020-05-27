using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class SpacePosition
    {
        public Vector2 L;
        public Vector2 M;
        public Vector2 H;

        /// <summary>
        /// <para>New space position.</para>
        /// <para>NEEDS WORK. (Fix numbers over 10,000,000)</para>
        /// </summary>
        /// <param name="low">Small scale position.</param>
        /// <param name="medium">Medium scale position.</param>
        /// <param name="high">Large scale position.</param>
        public SpacePosition(Vector2 low, Vector2 medium, Vector2 high)
        {
            L = low;
            M = medium;
            H = high;
        }

        /// <summary>
        /// <para>Change position.</para>
        /// <para>NEEDS WORK. (This could be a lot better. It probably doesn't even work.)</para>
        /// </summary>
        /// <param name="add">Distance to move by.</param>
        public void ChangePosition(SpacePosition add)
        {
            if (add.L != Vector2.Zero)
            {
                L += add.L;

                //Shorten this formaula, along with the Number version.
                //GET JACK! D:
                while (L.X >= 10000000)
                {
                    L.X -= 10000000;
                    M.X += 1;
                }
                while (L.Y >= 10000000)
                {
                    L.Y -= 10000000;
                    M.Y += 1;
                }
                while (L.X <= -10000000)
                {
                    L.X += 10000000;
                    M.X -= 1;
                }
                while (L.Y <= -10000000)
                {
                    L.Y += 10000000;
                    M.Y -= 1;
                }
            }

            if (add.M != Vector2.Zero)
            {
                M += add.M;

                while (M.X >= 10000000)
                {
                    M.X -= 10000000;
                    H.X += 1;
                }
                while (M.Y >= 10000000)
                {
                    M.Y -= 10000000;
                    H.Y += 1;
                }
                while (M.X <= -10000000)
                {
                    M.X += 10000000;
                    H.X -= 1;
                }
                while (M.Y <= -10000000)
                {
                    M.Y += 10000000;
                    H.Y -= 1;
                }
            }

            if (add.H != Vector2.Zero)
            {
                H += add.H;
            }
        }

        /// <summary>
        /// <para>Change position based on Vector2.</para>
        /// <para>NEEDS WORK. (This could be a lot better. It probably doesn't even work.)</para>
        /// </summary>
        /// <param name="add">Distance to move by.</param>
        public void ChangePosition(Vector2 add)
        {
            float dec;

            if (add.X != 0)
            {
                if (Math.Abs(add.X) >= 1e+14)
                {
                    add.X /= 1e+14f;
                    dec = add.X - ((float)Math.Truncate(add.X)) * 1e+14f;
                    add.X = (float)Math.Truncate(add.X);

                    H.X += add.X;
                    M.X += dec;
                }
                else if (Math.Abs(add.X) >= 10000000)
                {
                    add.X /= 10000000;
                    dec = add.X - ((float)Math.Truncate(add.X)) * 1000000f;
                    add.X = (float)Math.Truncate(add.X);

                    M.X += add.X;
                    L.X += dec;
                }
                else
                {
                    add.X = (float)Math.Truncate(add.X);
                    L.X += add.X;
                }
            }

            if (add.Y != 0)
            {
                if (Math.Abs(add.Y) >= 1e+14)
                {
                    add.Y /= 1e+14f;
                    dec = (add.Y - (float)Math.Truncate(add.Y)) * 1e+14f;
                    add.Y = (float)Math.Truncate(add.Y);

                    H.Y += add.Y;
                    M.Y += dec;
                }
                else if (Math.Abs(add.Y) >= 10000000)
                {
                    add.Y /= 10000000;
                    dec = (add.Y - (float)Math.Truncate(add.Y)) * 1000000f;
                    add.Y = (float)Math.Truncate(add.Y);

                    M.Y += add.Y;
                    L.Y += dec;
                }
                else
                {
                    add.Y = (float)Math.Truncate(add.Y);
                    L.Y += add.Y;
                }
            }

            if (L.X >= 10000000)
            {
                float r = (float)Math.Truncate(L.X / 10000000);

                L.X -= 10000000 * r;
                M.X += 1 * r;
            }
            else if (L.X <= -10000000)
            {
                float r = (float)Math.Truncate(L.X / 10000000);

                L.X += 10000000 * r;
                M.X -= 1 * r;
            }

            if (L.Y >= 10000000)
            {
                float r = (float)Math.Truncate(L.Y / 10000000);

                L.Y -= 10000000 * r;
                M.Y += 1 * r;
            }
            else if (L.Y <= -10000000)
            {
                float r = (float)Math.Truncate(L.Y / 10000000);

                L.Y += 10000000 * r;
                M.Y -= 1 * r;
            }

            if (M.X >= 10000000)
            {
                float r = (float)Math.Truncate(M.X / 10000000);

                M.X -= 10000000 * r;
                H.X += 1 * r;
            }
            else if (M.X <= -10000000)
            {
                float r = (float)Math.Truncate(M.X / 10000000);

                M.X += 10000000 * r;
                H.X -= 1 * r;
            }

            if (M.Y >= 10000000)
            {
                float r = (float)Math.Truncate(M.Y / 10000000);

                M.Y -= 10000000 * r;
                H.Y += 1 * r;
            }
            else if (M.Y <= -10000000)
            {
                float r = (float)Math.Truncate(M.Y / 10000000);

                M.Y += 10000000 * r;
                H.Y -= 1 * r;
            }

            Console.WriteLine(L);
            Console.WriteLine(M);
            Console.WriteLine(H);

        }
    }
}
