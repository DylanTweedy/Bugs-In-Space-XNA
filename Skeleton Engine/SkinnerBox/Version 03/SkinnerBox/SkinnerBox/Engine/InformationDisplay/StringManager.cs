using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    static class StringManager
    {
        static public string ColorString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[C(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/C]");

            return text;
        }

        static public string BoldString(string text)
        {
            text = text.Insert(0, "[B]");
            text = text.Insert(text.Length, "[/B]");

            return text;
        }

        static public string ItalicString(string text)
        {
            text = text.Insert(0, "[I]");
            text = text.Insert(text.Length, "[/I]");

            return text;
        }
        
        static public string SetFont(string text, string font)
        {
            text = text.Insert(0, "[F(" + font + ")]");
            text = text.Insert(text.Length, "[/F]");

            return text;
        }
    }
}
