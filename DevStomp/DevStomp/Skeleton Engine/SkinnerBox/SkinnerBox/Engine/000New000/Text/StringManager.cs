using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    static class StringManager
    {
        /// <summary>
        /// Emoticon informaton class.
        /// </summary>
        [Serializable()]
        public class Emoticon
        {
            public string EmoticonName;
            public List<string> TextureName = new List<string>();
            public List<string> TextureLocation = new List<string>();
            public List<float> Scale = new List<float>();
            public List<Color> Tint = new List<Color>();

            public int SelectedEmoticon = 0;

            public bool ContainsDuplicate(Emoticon emoticon)
            {
                bool emoticonName = false;
                bool textureName = false;
                bool textureLocation = false;
                bool scale = false;
                bool tint = false;

                if (EmoticonName == emoticon.EmoticonName)
                    emoticonName = true;

                for (int i = 0; i < TextureName.Count; i++)
                    for (int o = 0; o < emoticon.TextureName.Count; o++)                    
                        if (TextureName[i] == emoticon.TextureName[o])
                            textureName = true;

                for (int i = 0; i < TextureLocation.Count; i++)
                    for (int o = 0; o < emoticon.TextureLocation.Count; o++)
                        if (TextureLocation[i] == emoticon.TextureLocation[o])
                            textureLocation = true;
                
                for (int i = 0; i < Scale.Count; i++)
                    for (int o = 0; o < emoticon.Scale.Count; o++)
                        if (Scale[i] == emoticon.Scale[o])
                            scale = true;

                for (int i = 0; i < Tint.Count; i++)
                    for (int o = 0; o < emoticon.Tint.Count; o++)
                        if (Tint[i] == emoticon.Tint[o])
                            tint = true;

                if (emoticonName && textureName && textureLocation && scale && tint)
                    return true;

                return false;
            }

            public void AddEmoticon(Emoticon emoticon)
            {
                if (EmoticonName == emoticon.EmoticonName)
                {
                    for (int i = 0; i < emoticon.TextureName.Count; i++)
                        TextureName.Add(emoticon.TextureName[i]);

                    for (int i = 0; i < emoticon.TextureLocation.Count; i++)
                        TextureLocation.Add(emoticon.TextureLocation[i]);

                    for (int i = 0; i < emoticon.Scale.Count; i++)
                        Scale.Add(emoticon.Scale[i]);

                    for (int i = 0; i < emoticon.Tint.Count; i++)
                        Tint.Add(emoticon.Tint[i]);
                }
            }
        }

        /// <summary>
        /// Dictionary of Emoticons.
        /// </summary>
        static Dictionary<string, Emoticon> Emoticons = new Dictionary<string, Emoticon>();
        /// <summary>
        /// Emoticon data from the Emoticons.txt file.
        /// </summary>
        static string[] EmoticonLines;

        /// <summary>
        /// Loads Emoticons, if none exist create a new Emoticons file.
        /// </summary>
        static public void LoadEmoticons()
        {
            Emoticons = (Dictionary<string, Emoticon>)SaveFile.Load(SaveFile.SettingsLocation + "Emoticons");

            if (Emoticons == null)
                Emoticons = new Dictionary<string, Emoticon>();

            UpdateEmoticons();
        }

        ////
        // Make sure the emoticon.txt file exists before running.
        ////
        /// <summary>
        /// Updates Emoticons from the Emoticons.txt file.
        /// </summary>
        static public void UpdateEmoticons()
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

                    if (!Emoticons.ContainsKey(emoticon.EmoticonName))
                        Emoticons.Add(emoticon.EmoticonName, emoticon);
                    else if (!Emoticons[emoticon.EmoticonName].ContainsDuplicate(emoticon))
                        Emoticons[emoticon.EmoticonName].AddEmoticon(emoticon);                    
                }

                SaveFile.Save(Emoticons, SaveFile.SettingsLocation, "Emoticons");
            }
        }

        /// <summary>
        /// Check string for Emoticons.
        /// </summary>
        /// <param name="text">String to check.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts a string to a Color. "[C(R,G,B,A)] [/C]"
        /// </summary>
        /// <param name="ColorString">Color string.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts a string to a color string.
        /// </summary>
        /// <param name="text">String to convert.</param>
        /// <param name="color">Desired color.</param>
        /// <returns></returns>
        static public string ColorString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[C(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/C]");

            return text;
        }

        /// <summary>
        /// Create a highlighted string. "[H(R,G,B,A)] [/H]"
        /// </summary>
        /// <param name="text">String to be highlighted.</param>
        /// <param name="color">Desired color.</param>
        /// <returns></returns>
        static public string HighlightString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[H(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/H]");

            return text;
        }

        /// <summary>
        /// Creates an overlayed string (Faded highlight). "[O(R,G,B,A)] [/O]"
        /// </summary>
        /// <param name="text">String to have overlay.</param>
        /// <param name="color">Desired color.</param>
        /// <returns></returns>
        static public string OverlayString(string text, Color color)
        {
            Vector4 c = color.ToVector4();

            text = text.Insert(0, "[O(" + c.X + "," + c.Y + "," + c.Z + "," + c.W + ")]");
            text = text.Insert(text.Length, "[/O]");

            return text;
        }

        /// <summary>
        /// Creates a bold string. "[B] [/B]"
        /// </summary>
        /// <param name="text">String to bold.</param>
        /// <returns></returns>
        static public string BoldString(string text)
        {
            text = text.Insert(0, "[B]");
            text = text.Insert(text.Length, "[/B]");
            return text;
        }


        /// <summary>
        /// Creates an italicized string. "[I] [/I]"
        /// </summary>
        /// <param name="text">String to italicize.</param>
        /// <returns></returns>
        static public string ItalicString(string text)
        {
            text = text.Insert(0, "[I]");
            text = text.Insert(text.Length, "[/I]");

            return text;
        }
        
        /// <summary>
        /// Set the strings Font. "[F(FontName)] [/F]"
        /// </summary>
        /// <param name="text">Input string.</param>
        /// <param name="font">Desired font.</param>
        /// <returns></returns>
        static public string SetFont(string text, string font)
        {
            text = text.Insert(0, "[F(" + font + ")]");
            text = text.Insert(text.Length, "[/F]");

            return text;
        }

        /// <summary>
        /// Creates an underlined string. "[U] [/U]"
        /// </summary>
        /// <param name="text">String to underline.</param>
        /// <returns></returns>
        static public string UnderlineString(string text)
        {
            text = text.Insert(0, "[U]");
            text = text.Insert(text.Length, "[/U]");

            return text;
        }

        /// <summary>
        /// Creates an string with a strikethrough. "[S] [/S]"
        /// </summary>
        /// <param name="text">String to strikethrough.</param>
        /// <returns></returns>
        static public string StrikethroughString(string text)
        {
            text = text.Insert(0, "[S]");
            text = text.Insert(text.Length, "[/S]");

            return text;
        }

        ////
        //Make font size "Point". Gonna have to research what point actually is.
        ////

        /// <summary>
        /// Changes the font size of a string. "[P(Point)] [/P]"
        /// </summary>
        /// <param name="text">String to edit.</param>
        /// <param name="Point">Desired font size.</param>
        /// <returns></returns>
        static public string FontSizeString(string text, int Point)
        {
            text = text.Insert(0, "[P(" + Point + ")]");
            text = text.Insert(text.Length, "[/P]");

            return text;
        }

        /// <summary>
        /// Makes a string subscript. "[SUB] [/SUB]"
        /// </summary>
        /// <param name="text">String to make subscript.</param>
        /// <returns></returns>
        static public string SubscriptString(string text)
        {
            text = text.Insert(0, "[SUB]");
            text = text.Insert(text.Length, "[/SUB]");

            return text;
        }

        /// <summary>
        /// Makes a string superscript. "[SUP] [/SUP]"
        /// </summary>
        /// <param name="text">String to make superscript.</param>
        /// <returns></returns>
        static public string SuperscriptString(string text)
        {
            text = text.Insert(0, "[SUP]");
            text = text.Insert(text.Length, "[/SUP]");

            return text;
        }

        #region Insert Image

        /// <summary>
        /// Create an image string.
        /// </summary>
        /// <param name="TextureLocation">Texture file location.</param>
        /// <param name="TextureName">Texture name.</param>
        /// <returns></returns>
        static public string InsertImage(string TextureLocation, string TextureName)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");

            return InsertImage(TextureLocation, TextureName, 1f, Color.White);
        }

        /// <summary>
        /// Create an image string.
        /// </summary>
        /// <param name="TextureLocation">Texture file location.</param>
        /// <param name="TextureName">Texture name.</param>
        /// <param name="Scale">Desired scale. (1 = Line height.)</param>
        /// <returns></returns>
        static public string InsertImage(string TextureLocation, string TextureName, float Scale)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");

            return InsertImage(TextureLocation, TextureName, Scale, Color.White);
        }

        /// <summary>
        /// Create an image string.
        /// </summary>
        /// <param name="TextureLocation">Texture file location.</param>
        /// <param name="TextureName">Texture name.</param>
        /// <param name="Tint">Desired tint.</param>
        /// <returns></returns>
        static public string InsertImage(string TextureLocation, string TextureName, Color Tint)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");

            return InsertImage(TextureLocation, TextureName, 1f, Tint);
        }
        
        /// <summary>
        /// Create an image string.
        /// </summary>
        /// <param name="TextureLocation">Texture file location.</param>
        /// <param name="TextureName">Texture name.</param>
        /// <param name="Scale">Desired scale. (1 = Line height.)</param>
        /// <param name="Tint">Desired tint.</param>
        /// <returns></returns>
        static public string InsertImage(string TextureLocation, string TextureName, float Scale, Color Tint)
        {
            TextureLocation = TextureLocation.Replace("//", "\\");
            Vector4 tint = Tint.ToVector4();

            return "[IMG(" + TextureLocation + "," + TextureName + "," + Scale + "," + tint.X + "," + tint.Y + "," + tint.Z + "," + tint.W + ")]";
        }

        #endregion
    }
}
