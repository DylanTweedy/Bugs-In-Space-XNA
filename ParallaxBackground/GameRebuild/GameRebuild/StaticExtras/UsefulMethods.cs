using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameRebuild
{
    static class UsefulMethods
    {
        static Random rand = new Random();

        static public float FindBetween(float pos, float top, float bottom, float max, float min, bool reverse)
        {
            float diff = bottom - top;
            float value = ((1 - ((pos - top) / diff)) * (max - min)) + min;

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
    }
}
