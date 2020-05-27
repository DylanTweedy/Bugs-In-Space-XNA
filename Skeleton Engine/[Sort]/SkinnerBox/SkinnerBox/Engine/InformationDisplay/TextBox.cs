using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    /// <summary>
    /// A line of text and its settings.
    /// </summary>
    [Serializable()]
    class TextLine
    {
        /// <summary>
        /// A dropdown submenu.
        /// </summary>
        public List<TextLine> SubMenu = new List<TextLine>();

        /// <summary>
        /// The line prefix.
        /// </summary>
        public string Prefix = "";
        /// <summary>
        /// The line text.
        /// </summary>
        public string Text = "";
        /// <summary>
        /// The separator between the prefix and the text.
        /// </summary>
        public string Separator = "";
        /// <summary>
        /// If true the displayed text will and the prefix and separator.
        /// </summary>
        public bool ShowPrefix = false;
        /// <summary>
        /// Line alignment: -1 being left and +1 being right.
        /// </summary>
        public float Alignment = 0f;
        /// <summary>
        /// Total size of the line.
        /// </summary>
        public Vector2 LineSize = Vector2.Zero;
        /// <summary>
        /// The center of the line.
        /// </summary>
        public Vector2 LinePosition = Vector2.Zero;
        
        /// <summary>
        /// The type of list: Bullet, Checklist, etc.
        /// </summary>
        public ListStyle Style = ListStyle.None;
        /// <summary>
        /// Data to accompany the list style.
        /// </summary>
        public float ListData = 0f;

        /// <summary>
        /// Whether or not the current line is selected.
        /// </summary>
        public bool LineSelected = false;
        /// <summary>
        /// The color of a selected lines box.
        /// </summary>
        public Color LineSelectedColor = Color.Black;
        /// <summary>
        /// if a selected line should invert its text color.
        /// </summary>
        public bool InvertTextColor = false;

        /// <summary>
        /// A line of text and its settings.
        /// </summary>
        /// <param name="text">The line text.</param>
        public TextLine(string text)
        {
            Text = text;
        }

        /// <summary>
        /// A line of text and its settings.
        /// </summary>
        /// <param name="name">The line prefix.</param>
        /// <param name="text">The line text.</param>
        /// <param name="showName">If true the displayed text will and the prefix and separator.</param>
        /// <param name="separator">The separator between the prefix and the text.</param>
        public TextLine(string name, string text, bool showName, string separator)
        {
            Prefix = name;
            Text = text;
            ShowPrefix = showName;
            Separator = separator;
        }

        /// <summary>
        /// Returns the full text line.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            if (ShowPrefix)
                return Prefix + Separator + Text;
            else
                return Text;
        }
    }

    [Serializable()]
    class TextBox
    {

        [Serializable()]
        class WordInfo
        {
            public string Text = "";
            public Vector2 Offset = Vector2.Zero;

            public WordSettings CurrentSettings = new WordSettings();

            public int Line = 0;

            public Vector2 WordSize = Vector2.Zero;

            public WordInfo(string text)
            {
                Text = text;
            }
        }

        [Serializable()]
        class FullWord
        {
            public string Word = "";
            public Vector2 Size = Vector2.Zero;
            public int Count = 0;

            public FullWord()
            {
            }

            public FullWord(string word, Vector2 size, int count)
            {
                Word = word;
                Size = size;
                Count = count;
            }
        }

        [Serializable()]
        public class WordSettings
        {
            public SkeletonTexture Texture = null;
            public Vector2 TextureDimensions = Vector2.Zero;
            public Vector2 PreviousTextureDimensions = Vector2.Zero;
            public float TextureScale = 1f;
            public Color TextureTint = Color.White;

            public bool Bold = false;
            public bool Italic = false;
            public bool Underline = false;
            public bool Strikethrough = false;
            public bool Superscript = false;
            public bool Subscript = false;
            public string Font = "Helvetica";
            public float FontSize = 24f;
            public Color FontColor = Color.White;
            public Color HighlightColor = Color.Transparent;
            public Color OverlayColor = Color.Transparent;

            public WordSettings()
            {
            }

            public WordSettings(WordSettings settings)
            {
                Duplicate(settings);
            }

            public bool CheckDuplicate(WordSettings settings)
            {
                if (settings.Bold != Bold)
                    return true;
                if (settings.Font != Font)
                    return true;
                if (settings.FontColor != FontColor)
                    return true;
                if (settings.FontSize != FontSize)
                    return true;
                if (settings.HighlightColor != HighlightColor)
                    return true;
                if (settings.Italic != Italic)
                    return true;
                if (settings.OverlayColor != OverlayColor)
                    return true;
                if (settings.Strikethrough != Strikethrough)
                    return true;
                if (settings.Subscript != Subscript)
                    return true;
                if (settings.Superscript != Superscript)
                    return true;
                if (settings.Underline != Underline)
                    return true;
                if (settings.Texture != Texture)
                    return true;
                if (settings.TextureDimensions != TextureDimensions)
                    return true;
                if (settings.PreviousTextureDimensions != PreviousTextureDimensions)
                    return true;
                if (settings.TextureScale != TextureScale)
                    return true;
                if (settings.TextureTint != TextureTint)
                    return true;

                return false;
            }

            public void Duplicate(WordSettings settings)
            {
                Bold = settings.Bold;
                Font = settings.Font;
                FontColor = settings.FontColor;
                FontSize = settings.FontSize;
                HighlightColor = settings.HighlightColor;
                Italic = settings.Italic;
                OverlayColor = settings.OverlayColor;
                Strikethrough = settings.Strikethrough;
                Subscript = settings.Subscript;
                Superscript = settings.Superscript;
                Underline = settings.Underline;
                Texture = settings.Texture;
                TextureDimensions = settings.TextureDimensions;
                PreviousTextureDimensions = settings.PreviousTextureDimensions;
                TextureScale = settings.TextureScale;
                TextureTint = settings.TextureTint;
            }
        }

        public Location FullBoxSize
        {
            get 
            { 
                float X = ((TextBoxSize.X + TextPadding.X) * Scale) / 2f;
                float Y = ((TextBoxSize.Y + TextPadding.Y) * Scale) / 2f;

                return new Location(
                    X + (Border.Thickness.Left * Scale), Y + (Border.Thickness.Top * Scale),
                    X + (Border.Thickness.Right * Scale), Y + (Border.Thickness.Bottom * Scale));                   
            }
        }

        private List<TextLine> OriginalLines = new List<TextLine>();
        private List<TextLine> Lines = new List<TextLine>();
        private List<WordInfo> Words = new List<WordInfo>();

        SpriteFont Font;

        public Vector2 TextBoxSize = Vector2.Zero;
        Vector2 PreviousTextBoxSize = Vector2.Zero;
        

        public Vector2 Position = Vector2.Zero;
        public float Scale = 1f;
        public float Rotation = 0f;
        public float Alpha = 1f;

        public Color BackgroundColor = Color.DarkGray;
        public Vector2 TextPadding = new Vector2(10f);
        public BorderStyle Border = new BorderStyle("Default", Color.DarkGray, new Location(8f));
        SkeletonTexture Box = new SkeletonTexture("Core", "Marker");

        public bool WrapToBox = false;
        bool PreviousWrapToBox = false;

        public WordSettings DefaultWordSettings = new WordSettings();
        WordSettings PreviousWordSettings = new WordSettings();
        
        public float LineSpacing = 0.7f;
        public float LetterSpacing = 0f;
        public float VerticalAlignment = 0f;

        float PreviousLineSpacing = 0.7f;
        float PreviousLetterSpacing = 0f;
        float PreviousVerticalAlignment = 0f;        


        public void Update(List<TextLine> text)
        {
            /////////////////////////////////////////
            DefaultWordSettings.FontColor = Color.White;
            LineSpacing = 1f;
            LetterSpacing = 1f;
            DefaultWordSettings.FontSize = 24f;
            WrapToBox = false;
            VerticalAlignment = -1f;
            Scale = 1f;
            if (WrapToBox)
                TextBoxSize = new Vector2(150, 500);
            Border.Thickness = new Location(0, 0, 8, 8);
            ////////////////////////////////////////
            
            if (IsTextNew(text))
            {
                OriginalLines = text.Clone();
                Lines = text.Clone();
                
                SplitToWords();
                SetLineSize();
                SetTextPosition();
            }
            
            for (int i = 0; i < Words.Count; i++)
                if (Words[i].CurrentSettings.Texture != null)
                {
                }

            PreviousTextBoxSize = TextBoxSize;

            PreviousWordSettings.Duplicate(DefaultWordSettings);
            PreviousLetterSpacing = LetterSpacing;
            PreviousLineSpacing = LineSpacing;
            PreviousVerticalAlignment = VerticalAlignment;
            PreviousWrapToBox = WrapToBox;
        }

        private bool IsTextNew(List<TextLine> text)
        {
            if (WrapToBox && TextBoxSize != PreviousTextBoxSize)
                return true;

            if (text.Count != OriginalLines.Count)
                return true;

            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].Alignment != OriginalLines[i].Alignment)
                    return true;
                if (text[i].LineSize != OriginalLines[i].LineSize)
                    return true;
                if (text[i].Prefix != OriginalLines[i].Prefix)
                    return true;
                if (text[i].ShowPrefix != OriginalLines[i].ShowPrefix)
                    return true;
                if (text[i].Style != OriginalLines[i].Style)
                    return true;
                if (text[i].Text != OriginalLines[i].Text)
                    return true;
                if (text[i].LineSelected != OriginalLines[i].LineSelected)
                    return true;
                if (text[i].LineSelectedColor != OriginalLines[i].LineSelectedColor)
                    return true;
            }
            
            if (PreviousLetterSpacing != LetterSpacing)
                return true;
            if (PreviousLineSpacing != LineSpacing)
                return true;
            if (PreviousVerticalAlignment != VerticalAlignment)
                return true;
            if (PreviousWrapToBox != WrapToBox)
                return true;
            
            /////////////////////////////////////////////////////////////////////
            for (int i = 0; i < Words.Count; i++)
                if (Words[i].CurrentSettings.Texture != null)
                {
                    Words[i].CurrentSettings.TextureDimensions.X = Words[i].CurrentSettings.Texture.Texture.Width;
                    Words[i].CurrentSettings.TextureDimensions.Y = Words[i].CurrentSettings.Texture.Texture.Height;

                    if (Words[i].CurrentSettings.TextureDimensions.X != Words[i].CurrentSettings.PreviousTextureDimensions.X)
                    {
                        Words[i].CurrentSettings.PreviousTextureDimensions.X = Words[i].CurrentSettings.TextureDimensions.X;
                        return true;
                    }
                    if (Words[i].CurrentSettings.TextureDimensions.Y != Words[i].CurrentSettings.PreviousTextureDimensions.Y)
                    {
                        Words[i].CurrentSettings.PreviousTextureDimensions.Y = Words[i].CurrentSettings.TextureDimensions.Y;
                        return true;
                    }
                }
            //////////////////////////////////////////////////////////////////////////

            return DefaultWordSettings.CheckDuplicate(PreviousWordSettings);
        }

        #region Set Word Info

        private void SplitToWords()
        {
            Words.Clear();
            string[] wordArray;

            for (int i = 0; i < Lines.Count; i++)
            {
                wordArray = Lines[i].GetText().Split(' ');

                for (int o = 0; o < wordArray.Length; o++)
                {
                    Words.Add(new WordInfo(wordArray[o]));

                    if (o != wordArray.Length - 1)
                        Words.Add(new WordInfo(" "));
                }

                Words.Add(new WordInfo("[BREAK]"));
            }
            
            for (int i = 0; i < Words.Count; i++)
            {
                Words[i].Text = StringManager.CheckEmoticon(Words[i].Text);

                if (ModifierSplit(Words[i].Text, "[IMG(", ")]", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[C(", ")]", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[H(", ")]", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[O(", ")]", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[F(", ")]", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[P(", ")]", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[B]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[I]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[U]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[S]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[SUB]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[SUP]", "", i) != null)
                {
                    i--;
                    continue;
                }

                if (ModifierSplit(Words[i].Text, "[/C]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/H]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/O]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/F]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/P]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/B]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/I]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/U]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/S]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/SUB]", "", i) != null)
                {
                    i--;
                    continue;
                }
                if (ModifierSplit(Words[i].Text, "[/SUP]", "", i) != null)
                {
                    i--;
                    continue;
                }


                if (ModifierSplit(Words[i].Text, "[BREAK]", "", i) != null)
                {
                    i--;
                    continue;
                }
            }
            
            FormatWords();
        }
        
        private List<string> ModifierSplit(string text, string modifierStart, string modifierEnd, int index)
        {
            int StartIndex = text.IndexOf(modifierStart);
            if (StartIndex == -1)
                return null;

            int EndIndex = 0;

            if (modifierEnd == null || modifierEnd == "")
                EndIndex = StartIndex + modifierStart.Length;
            else
            {
                EndIndex = text.IndexOf(modifierEnd, StartIndex) + modifierEnd.Length;
                if (EndIndex == -1)
                    return null;
            }

            if (text.Contains(modifierStart) && (text.Contains(modifierEnd) || modifierEnd == null || modifierEnd == ""))
                if (StartIndex != 0 || EndIndex != text.Length)
                {
                    string subString = text.Substring(StartIndex, EndIndex - StartIndex);

                    Words.RemoveAt(index);
                    List<string> wordArray = new List<string>();

                    if (StartIndex != 0)
                        wordArray.Add(text.Substring(0, StartIndex));

                    wordArray.Add(text.Substring(StartIndex, EndIndex - StartIndex));

                    if (EndIndex != text.Length)
                        wordArray.Add(text.Substring(EndIndex));

                    for (int o = 0; o < wordArray.Count; o++)
                    {
                        Words.Insert(index, new WordInfo(wordArray[o]));
                        index++;
                    }

                    return wordArray;
                }

            return null;
        }

        private void FormatWords()
        {
            WordSettings CurrentWordSettings = new WordSettings(DefaultWordSettings);

            for (int i = 0; i < Words.Count; i++)
            {
                switch (Words[i].Text)
                {
                    case "[B]":
                        CurrentWordSettings.Bold = true;
                        break;
                    case "[I]":
                        CurrentWordSettings.Italic = true;
                        break;
                    case "[U]":
                        CurrentWordSettings.Underline = true;
                        break;
                    case "[S]":
                        CurrentWordSettings.Strikethrough = true;
                        break;
                    case "[SUB]":
                        CurrentWordSettings.Subscript = true;
                        break;
                    case "[SUP]":
                        CurrentWordSettings.Superscript = true;

                        break;
                    case "[/B]":
                        CurrentWordSettings.Bold = false;
                        break;
                    case "[/I]":
                        CurrentWordSettings.Italic = false;
                        break;
                    case "[/U]":
                        CurrentWordSettings.Underline = false;
                        break;
                    case "[/S]":
                        CurrentWordSettings.Strikethrough = false;
                        break;
                    case "[/SUB]":
                        CurrentWordSettings.Subscript = false;
                        break;
                    case "[/SUP]":
                        CurrentWordSettings.Superscript = false;
                        break;
                    case "[/C]":
                        CurrentWordSettings.FontColor = DefaultWordSettings.FontColor;
                        break;
                    case "[/O]":
                        CurrentWordSettings.OverlayColor = DefaultWordSettings.OverlayColor;
                        break;
                    case "[/H]":
                        CurrentWordSettings.HighlightColor = DefaultWordSettings.HighlightColor;
                        break;
                    case "[/P]":
                        CurrentWordSettings.FontSize = DefaultWordSettings.FontSize;
                        break;
                    case "[/F]":
                        CurrentWordSettings.Font = DefaultWordSettings.Font;
                        break;
                }

                if (Words[i].Text.StartsWith("[IMG(") && Words[i].Text.EndsWith(")]"))
                {
                    string text = Words[i].Text;

                    string textureLocation;
                    string textureName;
                    float scale;
                    Color tint;

                    textureLocation = text.Remove(text.IndexOf(',')).Remove(0, 5);
                    text = text.Remove(0, text.IndexOf(',') + 1);

                    textureName = text.Remove(text.IndexOf(','));
                    text = text.Remove(0, text.IndexOf(',') + 1);

                    scale = float.Parse(text.Remove(text.IndexOf(',')));
                    text = text.Remove(0, text.IndexOf(',') + 1);

                    text = text.Insert(0, "[C(");
                    tint = StringToColor(text);
                    
                    CurrentWordSettings.Texture = new SkeletonTexture(textureLocation, textureName);
                    CurrentWordSettings.TextureScale = scale;
                    CurrentWordSettings.TextureTint = tint;


                    if (CurrentWordSettings.TextureScale > 1f)
                        CurrentWordSettings.TextureScale = 1f;
                    else if (CurrentWordSettings.TextureScale < 0f)
                        CurrentWordSettings.TextureScale = 0f;
                }
                if (Words[i].Text.StartsWith("[C(") && Words[i].Text.EndsWith(")]"))
                    CurrentWordSettings.FontColor = StringToColor(Words[i].Text);
                if (Words[i].Text.StartsWith("[H(") && Words[i].Text.EndsWith(")]"))
                    CurrentWordSettings.HighlightColor = StringToColor(Words[i].Text);
                if (Words[i].Text.StartsWith("[O(") && Words[i].Text.EndsWith(")]"))
                    CurrentWordSettings.OverlayColor = StringToColor(Words[i].Text);

                if (Words[i].Text.StartsWith("[F(") && Words[i].Text.EndsWith(")]"))
                {
                    string word = Words[i].Text;

                    word = word.Remove(0, 3);
                    word = word.Remove(word.Length - 2);

                    CurrentWordSettings.Font = word;
                }

                if (Words[i].Text.StartsWith("[P(") && Words[i].Text.EndsWith(")]"))
                {
                    string word = Words[i].Text;

                    word = word.Remove(0, 3);
                    word = word.Remove(word.Length - 2);

                    CurrentWordSettings.FontSize = int.Parse(word);
                }

                Words[i].CurrentSettings.Duplicate(CurrentWordSettings);
                CurrentWordSettings.Texture = null;
            }
            
            for (int i = 0; i < Words.Count; i++)
                if (Words[i].CurrentSettings.Subscript || Words[i].CurrentSettings.Superscript)
                    Words[i].CurrentSettings.FontSize = (int)(Words[i].CurrentSettings.FontSize * 0.6f);

          
        }

        private Color StringToColor(string ColorString)
        {
            ColorString = ColorString.Remove(0, 3);
            ColorString = ColorString.Remove(ColorString.Length - 2);

            return StringManager.StringToColor(ColorString);
        }

        #endregion

        private void SetLineSize()
        {
            int CurrentLine = 0;
            float WordScale = 0f;

            for (int i = 0; i < Words.Count; i++)
            {
                if (IsModifier(Words[i].Text))
                {
                    Words.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (Words[i].Text == "[BREAK]")
                {
                    CurrentLine++;
                    continue;
                }
                else if (Words[i].CurrentSettings.Texture == null)
                {
                    SetFontParameters(Words[i].CurrentSettings.Font, Words[i].CurrentSettings.Bold, Words[i].CurrentSettings.Italic);

                    WordScale = Words[i].CurrentSettings.FontSize / Font.LineSpacing;

                    Words[i].WordSize = Font.MeasureString(Words[i].Text) * WordScale;

                    Words[i].WordSize.X += LetterSpacing / 2f;
                    if (Words[i].WordSize.Y > Lines[CurrentLine].LineSize.Y)
                        Lines[CurrentLine].LineSize.Y = Words[i].WordSize.Y;

                    Lines[CurrentLine].LineSize.X += Words[i].WordSize.X;
                }
            }

            CurrentLine = 0;

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Text == "[BREAK]")
                {
                    CurrentLine++;
                    continue;
                }
                else if (Words[i].CurrentSettings.Texture != null)
                {
                    float textureWidth = Words[i].CurrentSettings.Texture.Texture.Width;
                    float textureHeight = Words[i].CurrentSettings.Texture.Texture.Height;
                    
                    float scale = Lines[CurrentLine].LineSize.Y / textureHeight;

                    Words[i].WordSize = new Vector2(textureWidth * scale, Lines[CurrentLine].LineSize.Y);
                    //Words[i].WordSize.X += LetterSpacing / 2f;
                    //Words[i].WordSize *= 2f;
                    
                    Lines[CurrentLine].LineSize.X += Words[i].WordSize.X;
                }
            }

            if (WrapToBox)
            {
                for (int i = 0; i < Lines.Count; i++)
                    if (TextBoxSize.X < Lines[i].LineSize.X)
                    {
                        ExpandSmallBox();
                        break;
                    }
            }
        }

        #region Position Words

        private void SetTextPosition()
        {
            if (!WrapToBox)
                SetBoxSize();

            int CurrentLine = 0;
            Vector2 CurrentOffset = Vector2.Zero;
            bool NewLine = true;
            Vector2 TextSize = Vector2.Zero;

            //////////////////////////////////////////
            for (int i = 0; i < Lines.Count; i++)
                Lines[i].Alignment = -1f;
            /////////////////////////////////////////////
            
            if (WrapToBox)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    TextSize.Y += Lines[i].LineSize.Y;
                    if (TextSize.X < Lines[i].LineSize.X)
                        TextSize.X = Lines[i].LineSize.X;
                }
            }
            else
                TextSize = TextBoxSize;

            for (int i = 0; i < Words.Count; i++)
            {
                if (NewLine)
                {
                    Vector2 HalfBox = TextBoxSize / 2f;
                    float HalfLine = Lines[CurrentLine].LineSize.X / 2f;
                    float HalfText = TextSize.Y / 2f;

                    if (i == 0)                    
                        CurrentOffset.Y = -HalfText + ((HalfBox.Y - HalfText) * VerticalAlignment);                    

                    CurrentOffset.X -= HalfLine + ((HalfBox.X - HalfLine) * -Lines[CurrentLine].Alignment);
                    Lines[CurrentLine].LinePosition = new Vector2(CurrentOffset.X, CurrentOffset.Y);
                }

                if (Words[i].Text == "[BREAK]")
                {
                    CurrentOffset.Y += Lines[CurrentLine].LineSize.Y * LineSpacing;
                    CurrentOffset.X = 0f;

                    NewLine = true;
                    Words.RemoveAt(i);
                    i--;
                    CurrentLine++;
                    continue;
                }

                Words[i].Offset = CurrentOffset;
                CurrentOffset.X += Words[i].WordSize.X;

                Words[i].Line = CurrentLine;
                if (Lines[CurrentLine].LineSize.Y > Words[i].WordSize.Y)
                {
                    if (Words[i].CurrentSettings.Subscript)
                        Words[i].Offset.Y += (Lines[CurrentLine].LineSize.Y - Words[i].WordSize.Y) * 0.9f;
                    else if (Words[i].CurrentSettings.Superscript)
                        Words[i].Offset.Y -= (Lines[CurrentLine].LineSize.Y - Words[i].WordSize.Y) * 0.1f;
                    else
                        Words[i].Offset.Y += (Lines[CurrentLine].LineSize.Y - Words[i].WordSize.Y) * 0.725f;
                }

                if (Words[i].CurrentSettings.Texture != null)
                {
                    Words[i].Offset += Words[i].WordSize / 2f;

                    float XOffset = Words[i].WordSize.X - (Words[i].WordSize.X * Words[i].CurrentSettings.TextureScale);
                    CurrentOffset.X -= XOffset;
                    Words[i].Offset.X -= XOffset / 2f;
                }
                

                NewLine = false;
                
                if (Lines[Words[i].Line].InvertTextColor)
                {
                    Words[i].CurrentSettings.FontColor = ColorManager.InvertColor(Words[i].CurrentSettings.FontColor);
                    Words[i].CurrentSettings.HighlightColor = ColorManager.InvertColor(Words[i].CurrentSettings.HighlightColor);
                }
            }


        }

        private void SetBoxSize()
        {
            TextBoxSize = Vector2.Zero;
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].LineSize.X > TextBoxSize.X)
                    TextBoxSize.X = Lines[i].LineSize.X;
                
                TextBoxSize.Y += Lines[i].LineSize.Y;

                if (i != 0)
                    TextBoxSize.Y += (LineSpacing - 1f) * 37.23f;
            }

        }

        private bool IsModifier(string text)
        {
            if (!text.StartsWith("["))
                return false;

                switch (text)
                {
                    case "[B]":
                        return true;
                    case "[I]":
                        return true;
                    case "[U]":
                        return true;
                    case "[S]":
                        return true;
                    case "[SUB]":
                        return true;
                    case "[SUP]":
                        return true;
                    case "[/B]":
                        return true;
                    case "[/I]":
                        return true;
                    case "[/U]":
                        return true;
                    case "[/S]":
                        return true;
                    case "[/SUB]":
                        return true;
                    case "[/SUP]":
                        return true;

                        
                    case "[/C]":
                        return true;
                    case "[/H]":
                        return true;
                    case "[/O]":
                        return true;
                    case "[/P]":
                        return true;
                    case "[/F]":
                        return true;
                }

                if (text.StartsWith("[C(") && text.EndsWith(")]"))
                    return true;
                if (text.StartsWith("[H(") && text.EndsWith(")]"))
                    return true;
                if (text.StartsWith("[O(") && text.EndsWith(")]"))
                    return true;
                if (text.StartsWith("[F(") && text.EndsWith(")]"))
                        return true;
                if (text.StartsWith("[P(") && text.EndsWith(")]"))
                        return true;

            return false;
        }

        private void ExpandSmallBox()
        {
            float CurrentWordSize = 0f;
            FullWord CurrentWord;

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Text == "[BREAK]")
                {
                    continue;
                }
                else
                {
                    CurrentWord = GetFullWordLength(i);
                    CurrentWordSize = CurrentWord.Size.X;

                    if (TextBoxSize.X < CurrentWordSize)
                        TextBoxSize.X = CurrentWordSize;

                    i += CurrentWord.Count - 1;
                }
            }
            
            CurrentWordSize = 0f;
            int CurrentLine = 0;
            
            Lines.Clear();

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Text == "[BREAK]")
                {
                    CurrentWordSize = 0f;
                    CurrentLine++;
                    continue;
                }
                else
                {
                    if (CurrentWordSize == 0f)
                        Lines.Add(new TextLine(""));

                    CurrentWord = GetFullWordLength(i);

                    if (CurrentWord.Word == " " && CurrentWordSize == 0)
                    {
                        Words.RemoveAt(i);
                        CurrentWord = GetFullWordLength(i);
                    }

                    CurrentWordSize += CurrentWord.Size.X;
                    Lines[CurrentLine].Text += CurrentWord.Word;

                    if (CurrentWordSize > TextBoxSize.X)
                    {
                        Lines[CurrentLine].LineSize.X -= CurrentWordSize;
                        Lines[CurrentLine].Text = Lines[CurrentLine].Text.Remove(Lines[CurrentLine].Text.Length - CurrentWord.Word.Length);

                        Words.Insert(i, new WordInfo("[BREAK]"));
                        CurrentWordSize = 0f;
                        CurrentLine++;
                        continue;
                    }

                    if (CurrentWord.Size.Y > Lines[CurrentLine].LineSize.Y)
                        Lines[CurrentLine].LineSize.Y = CurrentWord.Size.Y;

                    i += CurrentWord.Count - 1;

                }
            }

            for (int i = 0; i < Lines.Count; i++)
            {
            }


            float YSize = 0f;

            for (int i = 0; i < Lines.Count; i++)
                YSize += Lines[i].LineSize.Y;

            if (YSize > TextBoxSize.Y)
                TextBoxSize.Y = YSize;
        }

        private FullWord GetFullWordLength(int o)
        {
            FullWord WordLength = new FullWord();

            for (int i = o; i < Words.Count; i++)
            {
                if (Words[i].Text != " " && Words[i].Text != "[BREAK]")
                {
                    WordLength.Word += Words[i].Text;
                    WordLength.Size.X += Words[i].WordSize.X;
                    if (WordLength.Size.Y < Words[i].WordSize.Y)
                        WordLength.Size.Y = Words[i].WordSize.Y;
                    
                    WordLength.Count++;

                }
                else
                    break;
            }

            if (Words[o].Text == " ")
                return new FullWord(Words[o].Text, Words[o].WordSize, 1);
            else
                return WordLength;
        }


                
        #endregion

        private void SetFontParameters(string FontName, bool Bold, bool Italic)
        {
            if (Bold)
                FontName += "Bold";
            if (Italic)
                FontName += "Italic";

            if (Font != FontManager.Fonts[FontName])
                Font = FontManager.Fonts[FontName];

            Font.LineSpacing = (int)(Font.MeasureString("0").Y * 0.65f);
            Font.Spacing = LetterSpacing;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (BackgroundColor.A != 0)
                Box.Draw(spriteBatch, Position, BackgroundColor * Alpha, Rotation, (TextBoxSize + TextPadding) * Scale, SpriteEffects.None);

            if (Border != null)
            {
                Location Thickness = Border.Thickness.Duplicate();

                Border.Thickness.Multiply(Scale);
                Border.DrawBorders(spriteBatch, Position, ((TextBoxSize + TextPadding) * Scale) / 2f, Rotation, Alpha);
                Border.Thickness = Thickness;
            }

            Random test = new Random(233);

            //////////////////////////////////
            Rotation += 0.5f * GlobalVariables.WorldTime;
            Rotation = 0f;
            //////////////////////////////////

            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (Lines[i].LineSelected)
                    {
                        Box.Draw(spriteBatch, Position + Vector2.Transform(new Vector2(0, Lines[i].LinePosition.Y + (Lines[i].LineSize.Y / 2f)), Matrix.CreateRotationZ(Rotation)),
                            Lines[i].LineSelectedColor, Rotation, new Vector2(TextBoxSize.X + (TextPadding.X), Lines[i].LineSize.Y) * Scale, SpriteEffects.None);
                    }
                }

                for (int i = 0; i < Words.Count; i++)
                    if (Words[i].CurrentSettings.HighlightColor != Color.Transparent)
                        if (Words[i].CurrentSettings.Texture == null)
                            Box.Draw(spriteBatch, Position + Vector2.Transform((new Vector2(Words[i].Offset.X, Lines[Words[i].Line].LinePosition.Y) +
                                (new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y) / 2f)) * Scale, Matrix.CreateRotationZ(Rotation)),
                                Words[i].CurrentSettings.HighlightColor, Rotation, new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y * LineSpacing) * Scale, SpriteEffects.None);
                        else
                            Box.Draw(spriteBatch, Position + Vector2.Transform((new Vector2(Words[i].Offset.X - (Words[i].WordSize.X / 2f), Lines[Words[i].Line].LinePosition.Y) +
                                (new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y) / 2f)) * Scale, Matrix.CreateRotationZ(Rotation)),
                                Words[i].CurrentSettings.HighlightColor, Rotation, new Vector2(Words[i].WordSize.X * Words[i].CurrentSettings.TextureScale, Lines[Words[i].Line].LineSize.Y * LineSpacing) * Scale, SpriteEffects.None);

                for (int i = 0; i < Words.Count; i++)
                {
                    if (Words[i].CurrentSettings.Strikethrough)
                        Box.Draw(spriteBatch, Position + Vector2.Transform((new Vector2(Words[i].Offset.X, Lines[Words[i].Line].LinePosition.Y) +
                         (new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y) / 2f)) * Scale, Matrix.CreateRotationZ(Rotation)),
                         Words[i].CurrentSettings.FontColor,
                         Rotation, new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y / 15f) * Scale, SpriteEffects.None);

                    if (Words[i].CurrentSettings.Underline)
                        Box.Draw(spriteBatch, Position + Vector2.Transform((new Vector2(Words[i].Offset.X, Lines[Words[i].Line].LinePosition.Y) +
                         (new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y * 1.55f) / 2f)) * Scale, Matrix.CreateRotationZ(Rotation)),
                         Words[i].CurrentSettings.FontColor,
                         Rotation, new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y / 15f) * Scale, SpriteEffects.None);
                }

                for (int i = 0; i < Words.Count; i++)
                {
                    SetFontParameters(Words[i].CurrentSettings.Font, Words[i].CurrentSettings.Bold, Words[i].CurrentSettings.Italic);

                    if (Words[i].CurrentSettings.Texture == null)
                    {
                        spriteBatch.DrawString(Font, Words[i].Text,
                            Position + Vector2.Transform((Words[i].Offset * Scale), Matrix.CreateRotationZ(Rotation)),
                            Words[i].CurrentSettings.FontColor, Rotation, Vector2.Zero,
                            (Words[i].CurrentSettings.FontSize / Font.LineSpacing) * Scale, SpriteEffects.None, 0f);
                    }
                    else
                        Words[i].CurrentSettings.Texture.Draw(spriteBatch, Position + Vector2.Transform((Words[i].Offset * Scale), Matrix.CreateRotationZ(Rotation)),
                            Words[i].CurrentSettings.TextureTint, Rotation, Words[i].WordSize * Words[i].CurrentSettings.TextureScale * Scale, SpriteEffects.None);


                    if (Words[i].CurrentSettings.OverlayColor != Color.Transparent)
                        if (Words[i].CurrentSettings.Texture == null)
                            Box.Draw(spriteBatch, Position + Vector2.Transform((new Vector2(Words[i].Offset.X, Lines[Words[i].Line].LinePosition.Y) +
                                (new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y) / 2f)) * Scale, Matrix.CreateRotationZ(Rotation)),
                                Words[i].CurrentSettings.OverlayColor * 0.25f, Rotation, new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y * LineSpacing) * Scale, SpriteEffects.None);
                        else
                            Box.Draw(spriteBatch, Position + Vector2.Transform((new Vector2(Words[i].Offset.X - (Words[i].WordSize.X / 2f), Lines[Words[i].Line].LinePosition.Y) +
                            (new Vector2(Words[i].WordSize.X, Lines[Words[i].Line].LineSize.Y) / 2f)) * Scale, Matrix.CreateRotationZ(Rotation)),
                            Words[i].CurrentSettings.OverlayColor * 0.25f, Rotation, new Vector2(Words[i].WordSize.X * Words[i].CurrentSettings.TextureScale, Lines[Words[i].Line].LineSize.Y * LineSpacing) * Scale, SpriteEffects.None);

                }
            }
        }
    }
}
