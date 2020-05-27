using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class StaticInfoBox
    {
        static List<string> Text;
        //static string Text;
        static public Texture2D ColorWhite;
        static public Texture2D ColorBlack;
        static public Texture2D ColorBlock;
        static public Texture2D ColorPallette;
        static public Vector2 BoxSize;
        static public Vector2 TextSize;
        static public List<float> LineSizes;
        static SpriteFont Font;

        static public void LoadContent(ContentManager Content)
        {
            Text = new List<string>();
            LineSizes = new List<float>();

            ColorWhite = Content.Load<Texture2D>("Images//InfoBox//ColorWhite");
            ColorBlack = Content.Load<Texture2D>("Images//InfoBox//ColorBlack");
            ColorBlock = Content.Load<Texture2D>("Images//InfoBox//ColorBlock");
            ColorPallette = Content.Load<Texture2D>("Images//InfoBox//ColorPallette");

            BoxSize = Vector2.Zero;
            TextSize = Vector2.Zero;
        }

        static public void ClearList()
        {
            Text.Clear();
            LineSizes.Clear();
            BoxSize = Vector2.Zero;
            TextSize = Vector2.Zero;
            //Text = "";
        }


        static public void AddItem(string Info)
        {
            Text.Add(Info);
            //Text += " \n " + Info;
        }

        static private List<string> parseText(string text, int width)
        {
            string[] wordArray = text.Split(' ');
            List<string> Lines = new List<string>();
            string line = "";
            string fullLine = "";

            if (text.Contains('#'))
            {
                foreach (String word in wordArray)
                {
                    string smallWord = word;

                    if (word.StartsWith("#"))
                        smallWord = smallWord.Remove(0, word.IndexOf(')') + 1);
                    if (word.EndsWith("#"))
                        smallWord = smallWord.Remove(smallWord.Length - 1);

                    float length = Font.MeasureString(line + smallWord).Length();
                    if (length > width && line != String.Empty)
                    {
                        line = line.Remove(line.Length - 1);
                        LineSizes.Add(Font.MeasureString(line).Length());
                        Lines.Add(fullLine);
                        line = "";
                        fullLine = "";
                    }

                    line = line + smallWord + ' ';
                    fullLine = fullLine + word + ' ';
                }

                line = line.Remove(line.Length - 1);
                LineSizes.Add(Font.MeasureString(line).X);
            }
            else
            {
                foreach (String word in wordArray)
                {
                    float length = Font.MeasureString(fullLine + word).Length();
                    if (length > width && fullLine != String.Empty)
                    {
                        fullLine = fullLine.Remove(fullLine.Length - 1);
                        LineSizes.Add(Font.MeasureString(fullLine).X);
                        Lines.Add(fullLine);
                        fullLine = String.Empty;
                    }

                        fullLine = fullLine + word + ' ';
                }

                fullLine = fullLine.Remove(fullLine.Length - 1);
                LineSizes.Add(Font.MeasureString(fullLine).X);
            }

            Lines.Add(fullLine);

            for (int i = 0; i < Lines.Count; i++)
                if (Lines[i].EndsWith(" "))
                    Lines[i] = Lines[i].Remove(Lines[i].Length - 1);

            return Lines;
        }

        static private void SetBox(int Width, int Height)
        {
            Vector2 size = Vector2.Zero;
            BoxSize = Vector2.Zero;
            List<string> lines = new List<string>();
            
            if (Width > 0)
            {
                for (int i = 0; i < Text.Count; i++)
                {
                    lines = parseText(Text[i], Width);

                    if (lines.Count > 1)
                    {
                        Text.InsertRange(i + 1, lines);
                        Text.RemoveRange(i, 1);

                        for (int o = 0; o < lines.Count - 1; o++)
                        {
                            size = Font.MeasureString(Text[i + o]);
                            BoxSize.Y += size.Y;
                        }

                        i += lines.Count - 1;
                    }
                    else
                    {
                        size = Font.MeasureString(Text[i]);
                        BoxSize.Y += size.Y;
                    }
                }

                BoxSize.Y = Font.LineSpacing * Text.Count; 
                BoxSize.X = Width;

                TextSize = BoxSize;       
            }
            else
                for (int i = 0; i < Text.Count; i++)
                {
                    string text = Text[i];

                    while (text.Contains("[B]"))
                    {
                        text = text.Remove(text.IndexOf("[B]"), 3);
                        if (text.Contains("[/B]"))
                            text = text.Remove(text.IndexOf("[/B]"), 4);
                    }
                    while (text.Contains("[I]"))
                    {
                        text = text.Remove(text.IndexOf("[I]"), 3);
                        if (text.Contains("[/I]"))
                            text = text.Remove(text.IndexOf("[/I]"), 4);
                    }
                    while (text.Contains("[C("))
                    {
                        int index = text.IndexOf("[C");
                        text = text.Remove(index, 2);
                        text = text.Remove(index, (text.IndexOf(')', index) - index) + 2);
                        if (text.Contains("[/C]"))
                        text = text.Remove(text.IndexOf("[/C]"), 4);
                    }
                    while (text.Contains("[F("))
                    {
                        int index = text.IndexOf("[F");
                        text = text.Remove(index, 2);
                        text = text.Remove(index, (text.IndexOf(')', index) - index) + 2);
                        if (text.Contains("[/F]"))
                        text = text.Remove(text.IndexOf("[/F]"), 4);
                    }

                    size = Font.MeasureString(text);
                    LineSizes.Add(size.X);

                    BoxSize.Y += Font.LineSpacing;

                    if (BoxSize.X < size.X)
                        BoxSize.X = size.X;
                }

            TextSize = BoxSize;

            if (BoxSize.Y < Height)
                BoxSize.Y = Height;
        }

        static float Rotation = 0f;

        static public void Draw(SpriteBatch spriteBatch, Vector2 Position, float Scale, float Alpha)
        {
            string fontName = "Tahoma";
            Font = FontManager.Fonts[fontName];

            

            Scale *= 0.5f;
            
            SetBox(0, 0);

            Vector2 Offset = new Vector2(BoxSize.X / 2f, -BoxSize.Y / 2f) * Scale;
            Vector2 Origin = BoxSize / 2;
            
            spriteBatch.Draw(StaticTests.Marker, Position + Offset, null, Color.Gray * Alpha, Rotation, new Vector2(16, 16), (BoxSize / new Vector2(32, 32)) * Scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(StaticTests.Marker, Position + Offset, null, Color.Black * Alpha, Rotation, new Vector2(16, 16), 0.2f * Scale, SpriteEffects.None, 0f);

            Color defaultColor = Color.White;

            string currentFont = fontName;
            Color color = defaultColor;
            bool italic = false;
            bool bold = false;

            for (int i = 0; i < Text.Count; i++)
            {
                //spriteBatch.DrawString(Font1, Text[i], Position + Offset + ((new Vector2(0, Font1.LineSpacing) * i) * Scale), Color.White * Alpha, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);


                //spriteBatch.DrawString(Font1, Text, Position + (Offset * Scale) + new Vector2(300, 0), Color.White * Alpha, 0f, (BoxSize / 2f), Scale, SpriteEffects.None, 0f);

                float RS = 0.3f;
                Font = FontManager.Fonts[fontName];

                //if (!InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.RightControl))
                //{
                //    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Left))
                //        Rotation += RS * (float)GlobalVariables.FrameTime;
                //    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Right))
                //        Rotation -= RS * (float)GlobalVariables.FrameTime;
                //    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Up))
                //        Rotation = 0f;
                //}

                string text = Text[i];
                Vector2 position = Position;
                Vector2 Alignment = new Vector2(-1, 0);


                //if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.RightControl))
                //{
                //    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Left))
                //        Alignment.X = -1;
                //    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Right))
                //        Alignment.X = 1;
                //    else
                //        Alignment.X = 0;

                //    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Down))
                //        Alignment.Y = -1;
                //    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Up))
                //        Alignment.Y = 1;
                //    else
                //        Alignment.Y = 0;
                //}

                Alignment *= Scale;


                if ((text.Contains("[") && text.Contains("[/")) || bold || italic || currentFont != fontName || color != defaultColor)
                {
                    int currentOffset = 0;
                                        
                    for (int o = 1; o < text.Length - 1; o++)
                    {
                        if (text.ElementAt(o) == ']' && text.ElementAt(o + 1) != ' ' && text.ElementAt(o + 1) != '(')
                        {
                            text = text.Insert(o + 1, " ");
                            o++;
                        }
                        else if (text.ElementAt(o) == '[' && text.ElementAt(o - 1) != ' ')
                        {
                            text = text.Insert(o, " ");
                            o++;
                        }
                    }

                    string[] wordArray = text.Split(' ');
                    foreach (String word in wordArray)
                    {
                        string smallWord = word;
                        string newFontName = currentFont;

                        if (word.StartsWith("[B]"))
                        {
                            smallWord = word.Remove(0, 3);
                            bold = true;    
                        }
                        if (word.StartsWith("[I]"))
                        {
                            smallWord = word.Remove(0, 3);
                            italic = true;
                        }
                        if (word.StartsWith("[C("))
                        {
                            smallWord = word.Remove(0, 3);
                            Vector4 vec4 = defaultColor.ToVector4();

                            for (int o = 0; o < 4; o++)
                            {
                                switch (o)
                                {
                                    case 0:
                                        if (smallWord.Contains(','))
                                        {
                                            vec4.X = float.Parse(smallWord.Remove(smallWord.IndexOf(',')));
                                            smallWord = smallWord.Remove(0, smallWord.IndexOf(',') + 1);
                                        }
                                        break;
                                    case 1:
                                        if (smallWord.Contains(','))
                                        {
                                            vec4.Y = float.Parse(smallWord.Remove(smallWord.IndexOf(',')));
                                            smallWord = smallWord.Remove(0, smallWord.IndexOf(',') + 1);
                                        }
                                        break;
                                    case 2:
                                        if (smallWord.Contains(','))
                                        {
                                            vec4.Z = float.Parse(smallWord.Remove(smallWord.IndexOf(',')));
                                            smallWord = smallWord.Remove(0, smallWord.IndexOf(',') + 1);
                                        }
                                        break;
                                    case 3:
                                        if (smallWord.Contains(')'))
                                        {
                                            vec4.W = float.Parse(smallWord.Remove(smallWord.IndexOf(')')));
                                            smallWord = smallWord.Remove(0, smallWord.IndexOf(')') + 2);
                                        }
                                        break;
                                }
                            }

                            color = new Color(vec4.X, vec4.Y, vec4.Z, vec4.W);
                        }
                        if (word.StartsWith("[F("))
                        {
                            smallWord = word.Remove(0, 3);
                            smallWord = smallWord.Remove(smallWord.Length - 2);

                            if (FontManager.Fonts.ContainsKey(smallWord))
                                currentFont = smallWord;

                            smallWord = "";
                        }

                        if (smallWord.Contains("[/B]"))
                        {
                            bold = false;
                            smallWord = smallWord.Remove(smallWord.IndexOf("[/B]"));
                        }
                        if (smallWord.Contains("[/I]"))
                        {
                            italic = false;
                            smallWord = smallWord.Remove(smallWord.IndexOf("[/I]"));
                        }
                        if (smallWord.Contains("[/C]"))
                        {
                            color = defaultColor;
                            smallWord = smallWord.Remove(smallWord.IndexOf("[/C]"));
                        }
                        if (smallWord.Contains("[/F]"))
                        {
                            currentFont = fontName;
                            smallWord = smallWord.Remove(smallWord.IndexOf("[/F]"));
                        }

                        if (bold && italic)
                            newFontName = currentFont + "BoldItalic";
                        else
                        {
                        if (bold)
                            newFontName = currentFont + "Bold";
                        if (italic)
                            newFontName = currentFont + "Italic";
                        }

                        if (smallWord != "")
                        {
                            Font = FontManager.Fonts[newFontName];

                            if (italic)
                                Font.Spacing = -3f;
                            else
                                Font.Spacing = 0f;

                            Vector2 stringSize = new Vector2(LineSizes[i] / 2f, TextSize.Y / 2f);
                            Vector2 align = new Vector2(Alignment.X * (-stringSize.X + Origin.X), (Font.LineSpacing * i * Scale) - (Alignment.Y * ((BoxSize.Y / 2) - stringSize.Y))) + Origin - stringSize;

                            align.X = Alignment.X * ((-stringSize.X + Origin.X) / Scale);
                            align.Y = ((Font.LineSpacing * i) - (stringSize.Y) + (Font.LineSpacing / 2f))
                                - (Alignment.Y * ((Origin.Y - stringSize.Y) / Scale));

                            spriteBatch.DrawString(Font, smallWord, position + Offset, color * Alpha, Rotation, new Vector2(LineSizes[i] / 2f, Font.LineSpacing / 2f) - align - new Vector2(currentOffset, 0), Scale, SpriteEffects.None, 0f);
                            currentOffset += (int)Font.MeasureString(smallWord).X;
                            currentOffset += (int)Font.MeasureString(" ").X;
                        }
                    }
                }
                else
                {
                    Vector2 stringSize = new Vector2(LineSizes[i] / 2f, TextSize.Y / 2f);
                    Vector2 align = Vector2.Zero;

                    align.X = Alignment.X * ((-stringSize.X + Origin.X) / Scale);
                    align.Y = ((Font.LineSpacing * i) - (stringSize.Y) + (Font.LineSpacing / 2f))
                        - (Alignment.Y * ((Origin.Y - stringSize.Y) / Scale));
                    
                    spriteBatch.DrawString(Font, text, position + Offset, defaultColor * Alpha, Rotation, new Vector2(LineSizes[i] / 2f, Font.LineSpacing / 2f) - align, Scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
