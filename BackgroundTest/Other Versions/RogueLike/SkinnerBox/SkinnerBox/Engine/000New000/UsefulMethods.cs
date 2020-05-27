using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SkeletonEngine
{
    static class UsefulMethods
    {
        static Random rand = new Random();

        static public float FindBetween(float pos, float top, float bottom, float max, float min, bool reverse)
        {
            float diff = bottom - top;
            float value;

            if (reverse)
                value = (((pos - top) / diff) * (max - min)) + min;
            else
                value = ((1 - ((pos - top) / diff)) * (max - min)) + min;

            if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }

        static public float FindBetween(float pos, float top, float bottom, float max, float min, bool reverse, float exponent)
        {
            if (exponent != 1f)
            {
                pos = (float)Math.Pow(pos, exponent);
                top = (float)Math.Pow(top, exponent);
            }

            float diff = bottom - top;
            float value;

            if (reverse)
                value = (((pos - top) / diff) * (max - min)) + min;
            else
                value = ((1 - ((pos - top) / diff)) * (max - min)) + min;

            if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }

        static public float RandomBetween(float max, float min)
        {
            float top = 1f;
            float bottom = 1f;
            float pos = (float)rand.NextDouble();

            float diff = bottom - top;
            float value;

            value = ((1 - ((pos - top) / diff)) * (max - min)) + min;

            if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }

        static public Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        static public float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }

        static public float RadiansToDegrees(float radian)
        {
            return radian * 57.2957795f;
        }

        static public float DegreesToRadians(float degrees)
        {
            return degrees * 0.0174532925f;
        }

        static public void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static public Vector2 RandomPosRectangle(Vector2 Min, Vector2 Max)
        {
            Vector2 result = Vector2.Zero;

            if (Min.X < Max.X)
                result.X = rand.Next((int)Min.X, (int)Max.X);
            else
                result.X = rand.Next((int)Max.X, (int)Min.X);

            if (Min.Y < Max.Y)
                result.Y = rand.Next((int)Min.Y, (int)Max.Y);
            else
                result.Y = rand.Next((int)Max.Y, (int)Min.Y);

            return result;
        }

        static public int GlobalCommonDenominator(int a, int b)
        {
            if (b == 0)
                return a;
            return GlobalCommonDenominator(b, a % b);
        }

        //Changes the first letter of a string to a capital.
        static public string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        static public Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, float radians)
        {
            if (radians == 0f)
                return pointToRotate;

            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            return new Vector2(
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y));
        }

        static public SpacePosition RotatePoint(SpacePosition pointToRotate, SpacePosition centerPoint, float radians)
        {
            if (radians == 0f)
                return pointToRotate;

            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            if (pointToRotate.ChunkX == centerPoint.ChunkX && pointToRotate.ChunkY == centerPoint.ChunkY)
            {
                SpacePosition pos = new SpacePosition();
                    
                    pos.ChunkX = centerPoint.ChunkX;
                pos.ChunkY = centerPoint.ChunkY;
                                        
                    pos += (new Vector2(
                    (cosTheta * (pointToRotate.Position.X - centerPoint.Position.X) -
                    sinTheta * (pointToRotate.Position.Y - centerPoint.Position.Y) + centerPoint.Position.X),
                    (sinTheta * (pointToRotate.Position.X - centerPoint.Position.X) +
                    cosTheta * (pointToRotate.Position.Y - centerPoint.Position.Y) + centerPoint.Position.Y)));
                
                return pos;
            }
            else
            {
                Vector2 chunks = RotatePoint(new Vector2((float)pointToRotate.ChunkX, (float)pointToRotate.ChunkY),
                    new Vector2((float)centerPoint.ChunkX, (float)centerPoint.ChunkY), radians);
                
                SpacePosition final = new SpacePosition();
                final.ChunkX = (BigInteger)Math.Floor(chunks.X);
                final.ChunkY = (BigInteger)Math.Floor(chunks.Y);

                final.Position = new Vector2((chunks.X - (float)Math.Floor(chunks.X)) * GlobalVariables.CameraChunkSize,
                    (chunks.Y - (float)Math.Floor(chunks.Y)) * GlobalVariables.CameraChunkSize);

                final.Update();

                Vector2 pos = new Vector2(
                (cosTheta * (pointToRotate.Position.X - centerPoint.Position.X) -
                sinTheta * (pointToRotate.Position.Y - centerPoint.Position.Y) + centerPoint.Position.X),
                (sinTheta * (pointToRotate.Position.X - centerPoint.Position.X) +
                cosTheta * (pointToRotate.Position.Y - centerPoint.Position.Y) + centerPoint.Position.Y));

                final += pos;


                return final;




                //Vector2 chunks = new Vector2((float)pointToRotate.ChunkX, (float)pointToRotate.ChunkY) -
                //    new Vector2((float)centerPoint.ChunkX, (float)centerPoint.ChunkY);

                //chunks = Vector2.Transform(chunks, Matrix.CreateRotationZ(radians));

                //Vector2 pos = new Vector2(
                //(cosTheta * (pointToRotate.Position.X - centerPoint.Position.X) -
                //sinTheta * (pointToRotate.Position.Y - centerPoint.Position.Y) + centerPoint.Position.X),
                //(sinTheta * (pointToRotate.Position.X - centerPoint.Position.X) +
                //cosTheta * (pointToRotate.Position.Y - centerPoint.Position.Y) + centerPoint.Position.Y));

                //SpacePosition final = new SpacePosition();
                //final = new SpacePosition(chunks * GlobalVariables.CameraChunkSize);
                //final.Update();
                //final += new SpacePosition() { ChunkX = centerPoint.ChunkX, ChunkY = centerPoint.ChunkY, Position = Vector2.Zero };
                //final += pos;
                
                //return final;
            }

        }

        static public List<int2D> PlotLine(int x1, int y1, int x2, int y2)
        {
            List<int2D> Points = new List<int2D>();

            int deltax = Math.Abs(x2 - x1);		// The difference between the x's
            int deltay = Math.Abs(y2 - y1);		// The difference between the y's
            int x = x1;				   	// Start x off at the first pixel
            int y = y1;				   	// Start y off at the first pixel

            int xinc1;
            int xinc2;
            int yinc1;
            int yinc2;
            int den;
            int num;
            int numadd;
            int numpixels;

            if (x2 >= x1)			 	// The x-values are increasing
            {
                xinc1 = 1;
                xinc2 = 1;
            }
            else						  // The x-values are decreasing
            {
                xinc1 = -1;
                xinc2 = -1;
            }

            if (y2 >= y1)			 	// The y-values are increasing
            {
                yinc1 = 1;
                yinc2 = 1;
            }
            else						  // The y-values are decreasing
            {
                yinc1 = -1;
                yinc2 = -1;
            }

            if (deltax >= deltay)	 	// There is at least one x-value for every y-value
            {
                xinc1 = 0;				  // Don't change the x when numerator >= denominator
                yinc2 = 0;				  // Don't change the y for every iteration
                den = deltax;
                num = deltax / 2;
                numadd = deltay;
                numpixels = deltax;	 	// There are more x-values than y-values
            }
            else						  // There is at least one y-value for every x-value
            {
                xinc2 = 0;				  // Don't change the x for every iteration
                yinc1 = 0;				  // Don't change the y when numerator >= denominator
                den = deltay;
                num = deltay / 2;
                numadd = deltax;
                numpixels = deltay;	 	// There are more y-values than x-values
            }

            for (int curpixel = 0; curpixel <= numpixels; curpixel++)
            {
                Points.Add(new int2D(x, y));		 	// Draw the current pixel
                num += numadd;			  // Increase the numerator by the top of the fraction
                if (num >= den)		 	// Check if numerator >= denominator
                {
                    num -= den;		   	// Calculate the new numerator value
                    x += xinc1;		   	// Change the x as appropriate
                    y += yinc1;		   	// Change the y as appropriate
                }
                x += xinc2;			 	// Change the x as appropriate
                y += yinc2;			 	// Change the y as appropriate
            }
            return Points;
        }
        
        static public int Return1DPosition(int x, int y, int size)
        {
            if (x < 0)
                return -1;
            else if (x >= size)
                return -1;

            if (y < 0)
                return -1;
            else if (y >= size)
                return -1;

            return x + (y * size);
        }

        static public int2D Return2DPosition(int i, int size)
        {
            return new int2D(i % size, i / size);
        }

        static public List<Vector2> MidpointCircle(int CircleX, int CircleY, int Radius)
        {
            List<Vector2> CirclePoints = new List<Vector2>();

            int x = Radius, y = 0;//local coords     
            int cd2 = 0;    //current distance squared - radius squared

            if (Radius <= 0)
            {
                CirclePoints.Add(new Vector2(CircleX, CircleY));
                return CirclePoints;
            }
            CirclePoints.Add(new Vector2(CircleX - Radius, CircleY));
            CirclePoints.Add(new Vector2(CircleX + Radius, CircleY));
            CirclePoints.Add(new Vector2(CircleX, CircleY - Radius));
            CirclePoints.Add(new Vector2(CircleX, CircleY + Radius));

            while (x > y)    //only formulate 1/8 of circle
            {
                cd2 -= (--x) - (++y);
                if (cd2 < 0) cd2 += x++;

                CirclePoints.Add(new Vector2(CircleX - x, CircleY - y));//upper left left
                CirclePoints.Add(new Vector2(CircleX - y, CircleY - x));//upper upper left
                CirclePoints.Add(new Vector2(CircleX + y, CircleY - x));//upper upper right
                CirclePoints.Add(new Vector2(CircleX + x, CircleY - y));//upper right right
                CirclePoints.Add(new Vector2(CircleX - x, CircleY + y));//lower left left
                CirclePoints.Add(new Vector2(CircleX - y, CircleY + x));//lower lower left
                CirclePoints.Add(new Vector2(CircleX + y, CircleY + x));//lower lower right
                CirclePoints.Add(new Vector2(CircleX + x, CircleY + y));//lower right right
            }

            return CirclePoints.Distinct().ToList();
        }

        static public List<Vector2> GetIntervals(Vector2 center, Vector2 pointToRotate, float radians, int steps, bool randomSelect)
        {
            List<Vector2> intervals = new List<Vector2>();

            intervals.Add(center - (center - pointToRotate));

            for (int i = 1; i <= steps; i++)
            {
                if (randomSelect && GlobalVariables.RandomNumber.NextDouble() > 0.5f)
                {
                    intervals.Add(RotatePoint(pointToRotate, center, radians * i));
                    intervals.Add(RotatePoint(pointToRotate, center, -radians * i));
                }
                else
                {
                    intervals.Add(RotatePoint(pointToRotate, center, -radians * i));
                    intervals.Add(RotatePoint(pointToRotate, center, radians * i));
                }
            }

            return intervals;
        }
    }
}
