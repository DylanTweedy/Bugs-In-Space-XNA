using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    static class UsefulMethods
    {
        static Random rand = new Random();

        static public float FindBetween(int pos, int top, int bottom, float max, float min, bool reverse)
        {
            int diff = bottom - top;
            float value = ((1 - ((pos - top) / (float)diff)) * (max - min)) + min;

            if (reverse)
                value = (((pos - top) / (float)diff) * (max - min)) + min;
            else
                value = ((1 - ((pos - top) / (float)diff)) * (max - min)) + min;

            if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }

        static public Color RandomColour()
        {
            return new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }
    }
}
