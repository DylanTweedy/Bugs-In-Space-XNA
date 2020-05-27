using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    class InfoBox
    {
        private List<TextLine> Text;
        public bool Active = true;
        public int SelectedItem = -1;

        public Vector2 Position;
        public Vector2 TotalScale;

        public Color BackgroundColor;
        public Vector2 TextPadding;
        public BorderStyle Border;

        public float Scale;
        public float Alpha;
        public float Rotation;
        public Vector2 Alignment;

        Vector2 TextScale;
        Vector2 BoxSize;
        List<float> LineSizes = new List<float>();
        SpriteFont Font;
        string CurrentFont;
        float FinalScale;
        float LineSpacing;

        public InfoBox()
        {
            CurrentFont = "Helvetica";
            Font = FontManager.Fonts["Helvetica"];

            Scale = 20f;
            Alpha = 1f;
            Rotation = 0f;
            Alignment = new Vector2(-1f, 0f);

            BackgroundColor = new Color(55, 55, 55);
            TextPadding = new Vector2(10f, 10f);
            Border = new BorderStyle("Default", new Color(55, 55, 55), new Location(8f, 0f, 8f, 8f));
        }          
        
        public void LoadContent(ContentManager Content)
        {
            Text = new List<TextLine>();
            LineSizes = new List<float>();
            
            BoxSize = Vector2.Zero;
            TextScale = Vector2.Zero;
        }

        public void ClearList()
        {
            Text.Clear();
            LineSizes.Clear();
            BoxSize = Vector2.Zero;
            TextScale = Vector2.Zero;
        }

        public void Update(List<TextLine> text, string font, float scale)
        {
            if (font != null && FontManager.Fonts.ContainsKey(font))
                CurrentFont = font;
            else
                CurrentFont = "Helvetica";

            Font = FontManager.Fonts[CurrentFont];

            Text = text;
            LineSpacing = (Font.MeasureString("0").Y * 0.65f);
            Font.LineSpacing = (int)LineSpacing;

            Scale = scale;
            FinalScale = Scale / Font.LineSpacing;
            Scale = FinalScale;

            SetBox(0, 0);


            TotalScale = new Vector2((BoxSize.X * Scale) + (TextPadding.X), 
                (BoxSize.Y * Scale) + (TextPadding.Y));

            if (Active)
            {
                if (SelectedItem == -1)
                    SelectedItem = 0;
                UpdateControls();
            }
            else
                SelectedItem = -1;
        }

        public void UpdateControls()
        {
            if (InputManager.KBButtonPressed(true, EngineControls.MenuSelectUp))
                SelectedItem -= 1;
            else if (InputManager.KBButtonPressed(true, EngineControls.MenuSelectDown))
                SelectedItem += 1;

            if (SelectedItem >= Text.Count)
                SelectedItem = 0;
            else if (SelectedItem < 0)
                SelectedItem = Text.Count - 1;
        }
                
        private void SetBox(int Width, int Height)
        {
            #region

            //Vector2 size = Vector2.Zero;
            //List<string> lines = new List<string>();
            
            //if (Width > 0)
            //{
            //    for (int i = 0; i < Text.Count; i++)
            //    {
            //        lines = parseText(Text[i], Width);

            //        if (lines.Count > 1)
            //        {
            //            Text.InsertRange(i + 1, lines);
            //            Text.RemoveRange(i, 1);

            //            for (int o = 0; o < lines.Count - 1; o++)
            //            {
            //                size = Font.MeasureString(Text[i + o]);
            //                BoxSize.Y += size.Y;
            //            }

            //            i += lines.Count - 1;
            //        }
            //        else
            //        {
            //            size = Font.MeasureString(Text[i]);
            //            BoxSize.Y += size.Y;
            //        }
            //    }

            //    BoxSize.Y = Font.LineSpacing * Text.Count; 
            //    BoxSize.X = Width;

            //    TextSize = BoxSize;       
            //}
            //else
            //    for (int i = 0; i < Text.Count; i++)
            //    {
            //        string text = Text[i];

            //        while (text.Contains("[B]"))
            //        {
            //            text = text.Remove(text.IndexOf("[B]"), 3);
            //            if (text.Contains("[/B]"))
            //                text = text.Remove(text.IndexOf("[/B]"), 4);
            //        }
            //        while (text.Contains("[I]"))
            //        {
            //            text = text.Remove(text.IndexOf("[I]"), 3);
            //            if (text.Contains("[/I]"))
            //                text = text.Remove(text.IndexOf("[/I]"), 4);
            //        }
            //        while (text.Contains("[C("))
            //        {
            //            int index = text.IndexOf("[C");
            //            text = text.Remove(index, 2);
            //            text = text.Remove(index, (text.IndexOf(')', index) - index) + 2);
            //            if (text.Contains("[/C]"))
            //            text = text.Remove(text.IndexOf("[/C]"), 4);
            //        }
            //        while (text.Contains("[F("))
            //        {
            //            int index = text.IndexOf("[F");
            //            text = text.Remove(index, 2);
            //            text = text.Remove(index, (text.IndexOf(')', index) - index) + 2);
            //            if (text.Contains("[/F]"))
            //            text = text.Remove(text.IndexOf("[/F]"), 4);
            //        }

            //        size = Font.MeasureString(text);
            //        LineSizes.Add(size.X);

            //        BoxSize.Y += Font.LineSpacing;

            //        if (BoxSize.X < size.X)
            //            BoxSize.X = size.X;
            //    }

            #endregion

            LineSizes.Clear();
            BoxSize = Vector2.Zero;
            string fontName = CurrentFont;
            Color defaultColor = Color.White;
            string currentFont = fontName;
            Color color = defaultColor;
            bool italic = false;
            bool bold = false;


            //FinalScale = Scale / Font.LineSpacing;
            
            for (int i = 0; i < Text.Count; i++)
            {
                string text = Text[i].GetText();

                //size = Font.MeasureString(text);
                //LineSizes.Add(size.X);
                BoxSize.Y += Font.LineSpacing;


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
                            if (newFontName != null && FontManager.Fonts.ContainsKey(newFontName))
                                Font = FontManager.Fonts[newFontName];

                            //FinalScale = Scale / Font.LineSpacing;

                            if (italic)
                                Font.Spacing = -3f;
                            else
                                Font.Spacing = 0f;

                            currentOffset += (int)Font.MeasureString(smallWord).X;
                            currentOffset += (int)Font.MeasureString(" ").X;                            
                        }
                    }

                    currentOffset -= (int)Font.MeasureString(" ").X;          

                    if (BoxSize.X < currentOffset)
                        BoxSize.X = currentOffset;

                    LineSizes.Add(currentOffset);
                }
                else
                {
                    //size = Font.MeasureString(text);
                    LineSizes.Add(Font.MeasureString(text).X);
                    //FinalScale = Scale / Font.LineSpacing;
                    //BoxSize.Y += Font.LineSpacing;

                    if (BoxSize.X < LineSizes[LineSizes.Count - 1])
                        BoxSize.X = LineSizes[LineSizes.Count - 1];

                    Vector2 align = Vector2.Zero;
                }

            }

            TextScale = BoxSize;

            //if (BoxSize.Y < Height)
            //    BoxSize.Y = Height;
        }
        
        public void Draw()
        {
            SpriteBatch spriteBatch = GlobalVariables.spriteBatch;

            string fontName = CurrentFont;
            
            Vector2 Offset = Vector2.Zero;
            Vector2 Origin = BoxSize / 2;
            
            SkeletonTexture Box = new SkeletonTexture("Core", "Marker");
            
            if (BackgroundColor.A != 0)
                Box.Draw(Position, BackgroundColor * Alpha, Rotation, (BoxSize * Scale) + (TextPadding * 2f), SpriteEffects.None);
            
            if (Border != null)
            {
                Vector2 BoxScale = new Vector2((BoxSize.X * Scale * 0.5f) + (TextPadding.X), (BoxSize.Y * Scale * 0.5f) + (TextPadding.Y));
                Border.DrawBorders(Position, BoxScale, Rotation, Alpha);                    
            }

            Offset -= Vector2.Transform(new Vector2(0, (Font.LineSpacing * FinalScale) * 0.25f), Matrix.CreateRotationZ(Rotation));
            Color defaultColor = Color.White;


            string currentFont = fontName;
            Color color = defaultColor;
            bool italic = false;
            bool bold = false;

            for (int i = 0; i < Text.Count; i++)
            {
                Font = FontManager.Fonts[fontName];

                #region Input Tests

                if (!InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.RightControl))
                {
                    float RS = 0.3f;

                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Left))
                        Rotation += RS * (float)GlobalVariables.FrameTime;
                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Right))
                        Rotation -= RS * (float)GlobalVariables.FrameTime;
                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Up))
                        Rotation = 0f;
                }
                
                if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.RightControl))
                {
                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Left))
                        Alignment.X = -1;
                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Right))
                        Alignment.X = 1;
                    else
                        Alignment.X = 0;

                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Down))
                        Alignment.Y = -1;
                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Up))
                        Alignment.Y = 1;
                    else
                        Alignment.Y = 0;
                }

                #endregion

                string text = Text[i].GetText();
                Vector2 position = Position;
                Vector2 alignment = Alignment * FinalScale;

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
                        #region CleanUp

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

                        #endregion

                        if (smallWord != "")
                        {
                            if (smallWord == "a" || smallWord == "test")
                            {
                            }

                            Font = FontManager.Fonts[newFontName];
                            
                            if (italic)
                                Font.Spacing = -3f;
                            else
                                Font.Spacing = 0f;
                                                    
                            Vector2 stringSize = new Vector2(LineSizes[i] / 2f, TextScale.Y / 2f);
                            Vector2 align = Vector2.Zero;// = new Vector2(alignment.X * (-stringSize.X + Origin.X), (Font.LineSpacing * i * FinalScale) - (alignment.Y * ((BoxSize.Y / 2) - stringSize.Y))) + Origin - stringSize;

                            align.X = alignment.X * ((-stringSize.X + Origin.X) / FinalScale);
                            align.Y = ((LineSpacing * i) - (stringSize.Y) + (LineSpacing / 2f))
                                - (alignment.Y * ((Origin.Y - stringSize.Y) / FinalScale));

                            spriteBatch.DrawString(Font, smallWord, position + Offset, color * Alpha, Rotation,
                                new Vector2(LineSizes[i] / 2f, LineSpacing / 2f) - align - new Vector2(currentOffset, 0), FinalScale, SpriteEffects.None, 0f);
                            currentOffset += (int)Font.MeasureString(smallWord).X;
                            currentOffset += (int)Font.MeasureString(" ").X;
                        }
                    }
                }
                else
                {
                    Vector2 stringSize = new Vector2(LineSizes[i] / 2f, TextScale.Y / 2f);
                    Vector2 align = Vector2.Zero;
                    Font.LineSpacing = (int)(Font.MeasureString("0").Y * 0.65f);

                    align.X = alignment.X * ((-stringSize.X + Origin.X) / FinalScale);
                    align.Y = ((Font.LineSpacing * i) - (stringSize.Y) + (Font.LineSpacing / 2f))
                        - (alignment.Y * ((Origin.Y - stringSize.Y) / FinalScale));

                    spriteBatch.DrawString(Font, text, position + Offset, defaultColor * Alpha, Rotation, new Vector2(LineSizes[i] / 2f, Font.LineSpacing / 2f) - align, FinalScale, SpriteEffects.None, 0f);
                }
            }
        }
    }    
}
