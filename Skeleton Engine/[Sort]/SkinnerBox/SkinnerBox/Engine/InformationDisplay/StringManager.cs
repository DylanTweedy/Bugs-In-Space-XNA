using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    static class StringManager
    {
        [Serializable()]
        public class Emoticon
        {
            public string EmoticonName;
            public List<string> TextureName = new List<string>();
            public List<string> TextureLocation = new List<string>();
            public List<float> Scale = new List<float>();
            public List<Color> Tint = new List<Color>();

            public int SelectedEmoticon = 0;
        }

        static Dictionary<string, Emoticon> Emoticons = new Dictionary<string, Emoticon>();
        static string[] EmoticonLines;

        static public void LoadEmoticons()
        {
            string[] lines = System.IO.File.ReadAllLines("Images\\TextImages\\Emoticons\\Emoticons.txt");

            if (lines != EmoticonLines)
            {
                EmoticonLines = lines;
                Emoticons.Clear();

                Emoticon emoticon = new Emoticon();

                for (int i = 0; i < EmoticonLines.Length; i++)
                {
                    string[] words = EmoticonLines[i].Split(' ');

                    if (words.Length != 5)
                        continue;

                    emoticon.EmoticonName = words[0];
                    emoticon.TextureName.Add(words[1]);
                    emoticon.TextureLocation.Add(words[2]);
                    emoticon.Scale.Add(float.Parse(words[3]));
                    emoticon.Tint.Add(StringToColor(words[4]));

                    Emoticons.Add(emoticon.EmoticonName, emoticon);
                }
            }
        }

        static public string CheckEmoticon(string text)
        {
            if (Emoticons.ContainsKey(text))
            {
                Emoticon emoticon = Emoticons[text];
                int SelectedEmoticon = emoticon.SelectedEmoticon;

                return StringManager.InsertImage(
                    emoticon.TextureLocation[SelectedEmoticon], emoticon.TextureName[SelectedEmoticon],
                    emoticon.Scale[SelectedEmoticon], emoticon.Tint[SelectedEmoticon]);
            }
            else
                return text;
        }

        static public Color StringToColor(string ColorString)
        {
            string[] values = ColorString.Split(',');
            float[] floatValues = new float[4] {1f, 1f, 1f, 1f};
            Color color = Color.White;

            if (!(values.Length == 3 || values.Length == 4))
                return color;

            float value;
            for (int i = 0; i < values.Length; i++)
            {
                value = 1f;
                value = float.Parse(values[i]);

                if (value > 1f)
                    value /= 255f;

                floatValues[i] = value;
            }

            color = new Color(floatValues[0], floatValues[1], floatValues[2], floatValues[3]);
            return color;
        }

        static public string ColorString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[C(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/C]");

            return text;
        }

        static public string HighlightString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[H(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/H]");

            return text;
        }

        static public string OverlayString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[O(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/O]");

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

        static public string UnderlineString(string text)
        {
            text = text.Insert(0, "[U]");
            text = text.Insert(text.Length, "[/U]");

            return text;
        }

        static public string StrikethroughString(string text)
        {
            text = text.Insert(0, "[S]");
            text = text.Insert(text.Length, "[/S]");

            return text;
        }

        static public string FontSizeString(string text, int Point)
        {
            text = text.Insert(0, "[P(" + Point + ")]");
            text = text.Insert(text.Length, "[/P]");

            return text;
        }


        static public string SubscriptString(string text)
        {
            text = text.Insert(0, "[SUB]");
            text = text.Insert(text.Length, "[/SUB]");

            return text;
        }

        static public string SuperscriptString(string text)
        {
            text = text.Insert(0, "[SUP]");
            text = text.Insert(text.Length, "[/SUP]");

            return text;
        }

        #region Insert Image

        static public string InsertImage(string TextureLocation, string TextureName)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");

            return InsertImage(TextureLocation, TextureName, 1f, Color.White);
        }

        static public string InsertImage(string TextureLocation, string TextureName, float Scale)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");

            return InsertImage(TextureLocation, TextureName, Scale, Color.White);
        }

        static public string InsertImage(string TextureLocation, string TextureName, Color Tint)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");

            return InsertImage(TextureLocation, TextureName, 1f, Tint);
        }
        
        static public string InsertImage(string TextureLocation, string TextureName, float Scale, Color Tint)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");
            Vector4 tint = Tint.ToVector4();

            return "[IMG(" + TextureLocation + "," + TextureName + "," + Scale + "," + tint.X + "," + tint.Y + "," + tint.Z + "," + tint.W + ")]";
        }

        #endregion
    }
}
