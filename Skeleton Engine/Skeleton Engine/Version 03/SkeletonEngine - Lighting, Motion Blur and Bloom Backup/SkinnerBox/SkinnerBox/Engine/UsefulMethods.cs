using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
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
            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            return new Vector2(
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y));
        }
    }
}
