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
    }
}
