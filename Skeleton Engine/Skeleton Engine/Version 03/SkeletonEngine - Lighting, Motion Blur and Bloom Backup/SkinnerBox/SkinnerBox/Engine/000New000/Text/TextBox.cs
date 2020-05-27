using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    #region TextLine Class
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
        public float Alignment = -1f;
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
        /// Whether or not to display emoticons.
        /// </summary>
        public bool ShowEmoticons = true;

        /// <summary>
        /// A line of text and its settings.
        /// </summary>
        /// <param name="text">The line text.</param>
        public TextLine(string text)
        {
            //Initialize the TextLine with just the text variable.
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
            //Initialize the TextLine with set variables.
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
            string text = Text;

            //If ShowPrefix is true insert prefix and separator.
            if (ShowPrefix)
                text = text.Insert(0, Prefix + Separator);

            ////
            //Add code for inserting ListStyles.
            ////


            //return the full text string.
            return text;
        }

        /// <summary>
        /// Copies variables from a TextLine object.
        /// </summary>
        /// <param name="textLine">TextLine to copy.</param>
        public void Duplicate(TextLine textLine)
        {
            SubMenu = textLine.SubMenu;
            Prefix = textLine.Prefix;
            Text = textLine.Text;
            Separator = textLine.Separator;
            ShowPrefix = textLine.ShowPrefix;
            Alignment = textLine.Alignment;
            LineSize = textLine.LineSize;
            LinePosition = textLine.LinePosition;
            Style = textLine.Style;
            ListData = textLine.ListData;
            LineSelected = textLine.LineSelected;
            LineSelectedColor = textLine.LineSelectedColor;
            InvertTextColor = textLine.InvertTextColor;
            ShowEmoticons = textLine.ShowEmoticons;            
        }
    }
    #endregion

    /// <summary>
    /// A standard text box.
    /// </summary>
    [Serializable()]
    class TextBox
    {
        ////
        //Make resizable.


        #region WordInfo Class
        /// <summary>
        /// Individual word information.
        /// </summary>
        [Serializable()]
        class WordInfo
        {
            /// <summary>
            /// The word.
            /// </summary>
            public string Text = "";
            /// <summary>
            /// The word position relative to the center of the text box.
            /// </summary>
            public Vector2 Offset = Vector2.Zero;

            /// <summary>
            /// The settings of the word. 
            /// </summary>
            public WordSettings CurrentSettings = new WordSettings();

            /// <summary>
            /// The line the word is on.
            /// </summary>
            public int Line = 0;

            /// <summary>
            /// The total dimensions of the word.
            /// </summary>
            public Vector2 WordSize = Vector2.Zero;

            /// <summary>
            /// Initializes the word.
            /// </summary>
            /// <param name="text">The word.</param>
            public WordInfo(string text)
            {
                Text = text;
            }
        }
        #endregion

        #region FullWord Class
        /// <summary>
        /// A full word, made of substrings. For use with WordWrap.
        /// </summary>
        [Serializable()]
        class FullWord
        {
            /// <summary>
            /// The whole word.
            /// </summary>
            public string Word = "";
            /// <summary>
            /// The dimensions of the FullWord.
            /// </summary>
            public Vector2 Size = Vector2.Zero;
            /// <summary>
            /// The number of substrings from the "Words" List within the word.
            /// </summary>
            public int Count = 0;
                       
            public FullWord()
            {
            }

            /// <summary>
            /// Create a FullWord
            /// </summary>
            /// <param name="word">The whole word.</param>
            /// <param name="size">The dimensions of the FullWord.</param>
            /// <param name="count">The number of substrings from the "Words" List within the word.</param>
            public FullWord(string word, Vector2 size, int count)
            {
                Word = word;
                Size = size;
                Count = count;
            }
        }
        #endregion

        #region WordSettings Class
        /// <summary>
        /// The settings for an individual word.
        /// </summary>
        [Serializable()]
        public class WordSettings
        {
            /// <summary>
            /// The texture to take place of the word.
            /// </summary>
            public SkeletonTexture Texture = null;
            /// <summary>
            /// The texture dimensions.
            /// </summary>
            public Vector2 TextureDimensions = Vector2.Zero;
            /// <summary>
            /// Dimensions of the texture on the previous frame.
            /// </summary>
            public Vector2 PreviousTextureDimensions = Vector2.Zero;
            /// <summary>
            /// Scale of the texture.
            /// </summary>
            public float TextureScale = 1f;
            /// <summary>
            /// Tint of the texture.
            /// </summary>
            public Color TextureTint = Color.White;

            /// <summary>
            /// Whether or not the word is bold.
            /// </summary>
            public bool Bold = false;
            /// <summary>
            /// Whether or not the word is italic.
            /// </summary>
            public bool Italic = false;
            /// <summary>
            /// Whether or not the word is Underlined.
            /// </summary>
            public bool Underline = false;
            /// <summary>
            /// Whether or not the word has strikethrough enabled.
            /// </summary>
            public bool Strikethrough = false;
            /// <summary>
            /// Whether or not the word is superscript.
            /// </summary>
            public bool Superscript = false;
            /// <summary>
            /// Whether or not the word is Subscript.
            /// /// </summary>
            public bool Subscript = false;

            /// <summary>
            /// The words font.
            /// </summary>
            public string Font = "Helvetica";
            /// <summary>
            /// The words font size.
            /// </summary>
            public float FontSize = 24f;
            /// <summary>
            /// The words font color.
            /// </summary>
            public Color FontColor = Color.White;
            /// <summary>
            /// the words highlight color.
            /// </summary>
            public Color HighlightColor = Color.Transparent;
            /// <summary>
            /// The overlay color. (Will be slightly transparent, used for selecting areas of text).
            /// </summary>
            public Color OverlayColor = Color.Transparent;

            /// <summary>
            /// Initialize a new WordSettings with default settings.
            /// </summary>
            public WordSettings()
            {
            }

            /// <summary>
            /// initialize a duplicate WordSettings.
            /// </summary>
            /// <param name="settings">The WordSettings to duplicate.</param>
            public WordSettings(WordSettings settings)
            {
                Duplicate(settings);
            }

            /// <summary>
            /// Check to see if two WordSettings are identical.
            /// </summary>
            /// <param name="settings">the WordSettings to test against.</param>
            /// <returns>Returns false if duplicate.</returns>
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

            /// <summary>
            /// Copies variables from a WordSettings object.
            /// </summary>
            /// <param name="settings">The WordSettings to duplicate.</param>
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
        #endregion

        #region Variables
        /// <summary>
        /// Returns the full size of the box.
        /// </summary>
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

        /// <summary>
        /// The original lines, used to check for changes during update.
        /// </summary>
        private List<TextLine> OriginalLines = new List<TextLine>();
        /// <summary>
        /// The current list of lines.
        /// </summary>
        private List<TextLine> Lines = new List<TextLine>();
        /// <summary>
        /// The words within the text box.
        /// </summary>
        private List<WordInfo> Words = new List<WordInfo>();
        
        /// <summary>
        /// The active SpriteFont.
        /// </summary>
        SpriteFont Font;

        /// <summary>
        /// The TextBox size.
        /// </summary>
        public Vector2 TextBoxSize = Vector2.Zero;
        /// <summary>
        /// The textbox size from the previous frame.
        /// </summary>
        Vector2 PreviousTextBoxSize = Vector2.Zero;
        
        ////
        // Position may need to be changed to fit in "infinite" space.
        ////
        /// <summary>
        /// The position of the TextBox.
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// Scale the text and box.
        /// </summary>
        public float Scale = 1f;
        /// <summary>
        /// Rotation of the TextBox. (Pi * 2 = One revolution).
        /// </summary>
        public float Rotation = 0f;
        /// <summary>
        /// Alpha value of the entire TextBox. Everything will be multiplied by this alpha value.
        /// </summary>
        public float Alpha = 1f;

        ////
        //Add background image functionality.
        SkeletonTexture Box = new SkeletonTexture("Core", "Marker");
        ////

        /// <summary>
        /// The tint of the background.
        /// </summary>
        public Color BackgroundColor = Color.DarkGray;
        /// <summary>
        /// Distance between the text and the edge of the box.
        /// </summary>
        public Vector2 TextPadding = new Vector2(10f);
        /// <summary>
        /// Border for the box.
        /// </summary>
        public BorderStyle Border = new BorderStyle("Default", Color.DarkGray, new Location(8f));        

        /// <summary>
        /// Whether or not to wrap the words to the box size. If false the box will expand to fit around the words.
        /// </summary>
        public bool WordWrap = false;
        /// <summary>
        /// The previous state of WordWrap.
        /// </summary>
        bool PreviousWordWrap = false;

        /// <summary>
        /// The default settings for each word if none are given.
        /// </summary>
        public WordSettings DefaultWordSettings = new WordSettings();
        /// <summary>
        /// The previous default settings for each word.
        /// </summary>
        WordSettings PreviousWordSettings = new WordSettings();
        
        /// <summary>
        /// Space inbetween each line.
        /// </summary>
        public float LineSpacing = 0.7f;
        /// <summary>
        /// Space inbetween each letter.
        /// </summary>
        public float LetterSpacing = 0f;

        /// <summary>
        /// Vertical alingment of the words in the box. (-1 = Top. +1 = Bottom).
        /// </summary>
        public float VerticalAlignment = 0f;

        /// <summary>
        /// Previous line spacing.
        /// </summary>
        float PreviousLineSpacing = 0.7f;
        /// <summary>
        /// Previous letter spacing.
        /// </summary>
        float PreviousLetterSpacing = 0f;
        /// <summary>
        /// Previous vertical alignment.
        /// </summary>
        float PreviousVerticalAlignment = 0f;

        #endregion

        /// <summary>
        /// Updates the text box, checks if the text has changed.
        /// </summary>
        /// <param name="text">The list of TextLines for the TextBox.</param>
        public void Update(List<TextLine> text)
        {
            ////
            //DefaultWordSettings.FontColor = Color.White;
            LineSpacing = 1f;
            TextBoxSize = Vector2.Zero;
            TextPadding = new Vector2(20f);
            //LetterSpacing = 1f;
            //DefaultWordSettings.FontSize = 24f;
            //VerticalAlignment = -1f;
            //Scale = 1f;
            WordWrap = false;
            if (WordWrap)
                TextBoxSize = new Vector2(150, 500);
            //Border.Thickness = new Location(0, 0, 8, 8);
            ////
            
            //If the text is new update the textbox.
            if (IsTextNew(text))
            {
                //Clone the text lines for reference.
                OriginalLines = text.Clone();
                Lines = text.Clone();

                // Sets the WordSettings and manages modifiers.
                SplitToWords();
                SetLineSize();
                SetTextPosition();
            }
            
            ////
            //Not sure what this is...?
            for (int i = 0; i < Words.Count; i++)
                if (Words[i].CurrentSettings.Texture != null)
                {
                }
            ////

            // Update the previous TextBox variables for next frame.
            PreviousTextBoxSize = TextBoxSize;

            PreviousWordSettings.Duplicate(DefaultWordSettings);
            PreviousLetterSpacing = LetterSpacing;
            PreviousLineSpacing = LineSpacing;
            PreviousVerticalAlignment = VerticalAlignment;
            PreviousWordWrap = WordWrap;
        }
        
        /// <summary>
        /// Checks to see if the TextLines have changed.
        /// </summary>
        /// <param name="text">TextLines to compare.</param>
        /// <returns>Returns true if a change is found.</returns>
        private bool IsTextNew(List<TextLine> text)
        {
            //If wordwrap is active and the text box has changed size return true.
            if (WordWrap && TextBoxSize != PreviousTextBoxSize)
                return true;

            //If the number of TextLines has changed return true.
            if (text.Count != OriginalLines.Count)
                return true;

            //Check if TextLine attributes have changed.
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
            
            //Check previous values against current values.
            if (PreviousLetterSpacing != LetterSpacing)
                return true;
            if (PreviousLineSpacing != LineSpacing)
                return true;
            if (PreviousVerticalAlignment != VerticalAlignment)
                return true;
            if (PreviousWordWrap != WordWrap)
                return true;
            
            ////
            //Add TextureManager code to detect if a texture has changed.
            //Might need code to set the dimensions of the texture as a Vector2 once it has changed.
            ////
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
            ////

            //Check if the WordSettings have changed.
            return DefaultWordSettings.CheckDuplicate(PreviousWordSettings);
        }

        // Sets the WordSettings and manages modifiers.
        // SplitToWords() and linked methods are here.
        #region Set Word Info

        /// <summary>
        /// Splits the TextLines into a list of words.
        /// </summary>
        private void SplitToWords()
        {
            Words.Clear();
            string[] wordArray;
                        
            for (int i = 0; i < Lines.Count; i++)
            {
                // Split the TextLine into words where a space is found.
                wordArray = Lines[i].GetText().Split(' ');

                // Add spaces to the wordArray.
                for (int o = 0; o < wordArray.Length; o++)
                {
                    Words.Add(new WordInfo(wordArray[o]));

                    if (o != wordArray.Length - 1)
                        Words.Add(new WordInfo(" "));
                }

                // Add "[BREAK]" at the end of the TextLine word collection to signal a new line.
                Words.Add(new WordInfo("[BREAK]"));
            }

            // Check words for modifiers.
            #region Modifier Split

            for (int i = 0; i < Words.Count; i++)
            {
                if (Lines[Words[i].Line].ShowEmoticons)
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

            #endregion

            // Adds the WordSettings to the list of words.
            FormatWords();
        }
        
        /// <summary>
        /// Splits a word further if it contains modifiers.
        /// </summary>
        /// <param name="text">The word to check for a modifier.</param>
        /// <param name="modifierStart">The start string to the modifier. (Goes before the word)</param>
        /// <param name="modifierEnd">The end string to the modifier. (Goes after the word, leave null or empty if n/a)</param>
        /// <param name="index">Index of the word in the "Words" array.</param>
        /// <returns>Returns the modified list of words.</returns>
        private List<string> ModifierSplit(string text, string modifierStart, string modifierEnd, int index)
        {
            // Set the start index of the modifier.
            int StartIndex = text.IndexOf(modifierStart);
            // If no modifier is found return null.
            if (StartIndex == -1)
                return null;

            int EndIndex = 0;

            // If modifierEnd is empty or null set EndIndex to the end of modifierStart.
            if (modifierEnd == null || modifierEnd == "")
                EndIndex = StartIndex + modifierStart.Length;
            // Otherwise set EndIndex to it's correct location fater the StartIndex.
            else
            {
                EndIndex = text.IndexOf(modifierEnd, StartIndex) + modifierEnd.Length;
                // If no EndIndex is found return null.
                ////
                // This shouldn't happen, so add debug code to log this.
                ////
                if (EndIndex == -1)
                    return null;
            }

            // Continue if modiferStart and modifierEnd is set correctly then continue. Otherwise return null.
            if (text.Contains(modifierStart) && (text.Contains(modifierEnd) || modifierEnd == null || modifierEnd == ""))
                if (StartIndex != 0 || EndIndex != text.Length)
                {
                    // Remove the original word.
                    Words.RemoveAt(index);

                    List<string> wordArray = new List<string>();
                    
                    // Add text before the modifer.
                    if (StartIndex != 0)
                        wordArray.Add(text.Substring(0, StartIndex));

                    // Add the modifier.
                    wordArray.Add(text.Substring(StartIndex, EndIndex - StartIndex));

                    // Add text after the modifier.
                    if (EndIndex != text.Length)
                        wordArray.Add(text.Substring(EndIndex));

                    // Insert the words into the class "Words" List.
                    for (int o = 0; o < wordArray.Count; o++)
                    {
                        Words.Insert(index, new WordInfo(wordArray[o]));
                        index++;
                    }

                    // Returns the wordArray
                    ////
                    // This isn't currently necessary, as the wordArray is inserted into the "Words" List.
                    // Can always change this to a bool true for if a modifier is present and false if otherwise?
                    ////
                    return wordArray;
                }

            return null;
        }

        /// <summary>
        /// Adds the WordSettings to the list of words.
        /// </summary>
        private void FormatWords()
        {
            WordSettings CurrentWordSettings = new WordSettings(DefaultWordSettings);

            for (int i = 0; i < Words.Count; i++)
            {
                // Checks for basic modifiers and sets values in CurrentWordSettings.
                #region Set basic WordSettings

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

                if (Words[i].Text.StartsWith("[C(") && Words[i].Text.EndsWith(")]"))
                    CurrentWordSettings.FontColor = StringToColor(Words[i].Text);
                if (Words[i].Text.StartsWith("[H(") && Words[i].Text.EndsWith(")]"))
                    CurrentWordSettings.HighlightColor = StringToColor(Words[i].Text);
                if (Words[i].Text.StartsWith("[O(") && Words[i].Text.EndsWith(")]"))
                    CurrentWordSettings.OverlayColor = StringToColor(Words[i].Text);
                #endregion

                // Checks for image modifiers and sets values in CurrentWordSettings.
                #region Set image WordSettings

                if (Words[i].Text.StartsWith("[IMG(") && Words[i].Text.EndsWith(")]"))
                {
                    string text = Words[i].Text;

                    string textureLocation;
                    string textureName;
                    float scale;
                    Color tint;

                    // Retrieves the texture Location, Name, Scale and Tint from the string.
                    ////
                    // Insert error handling and add to error log.
                    // As this information is retrieved from a string it could easily cause a crash.
                    ////
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

                    //Resize Textures to fit within LineSpacing Variations.
                    if (LineSpacing < 1f)
                        CurrentWordSettings.TextureScale *= LineSpacing;
                }

                #endregion

                // Checks for font modifiers and sets values in CurrentWordSettings.
                #region Set font WordSettings

                // Check font.
                if (Words[i].Text.StartsWith("[F(") && Words[i].Text.EndsWith(")]"))
                {
                    string word = Words[i].Text;

                    word = word.Remove(0, 3);
                    word = word.Remove(word.Length - 2);

                    CurrentWordSettings.Font = word;
                }
                // Check font size.
                if (Words[i].Text.StartsWith("[P(") && Words[i].Text.EndsWith(")]"))
                {
                    string word = Words[i].Text;

                    word = word.Remove(0, 3);
                    word = word.Remove(word.Length - 2);

                    CurrentWordSettings.FontSize = int.Parse(word);
                }

                #endregion
                
                // Set the CurrentSettings for the current "Word" in the loop.
                Words[i].CurrentSettings.Duplicate(CurrentWordSettings);

                // Set the CurrentWordSettings Texture to null ready for the next loop.
                CurrentWordSettings.Texture = null;
            }
            
            // If any of the words are subscript or superscript multiply their font size by 0.6.
            for (int i = 0; i < Words.Count; i++)
                if (Words[i].CurrentSettings.Subscript || Words[i].CurrentSettings.Superscript)
                    Words[i].CurrentSettings.FontSize = (int)(Words[i].CurrentSettings.FontSize * 0.6f);          
        }

        /// <summary>
        /// Converts a string to a Color.
        /// </summary>
        /// <param name="ColorString">The string for the color. "[C( R, G, B, A )]"</param>
        /// <returns>Returns a Color.</returns>
        private Color StringToColor(string ColorString)
        {
            // Remove "[C(" from the start.
            ColorString = ColorString.Remove(0, 3);
            // Remove ")]" from the end.
            ColorString = ColorString.Remove(ColorString.Length - 2);

            ////
            // Could return an error, add error handling and logging capabilities.
            ////

            //Return Color from StringManager.
            return StringManager.StringToColor(ColorString);
        }

        #endregion

        // Sets the LineSizes and adjusts the box size if WordWrap is active. 
        #region Set Line and Box Size

        /// <summary>
        /// Calculates the size of each line based on the word sizes.
        /// </summary>
        private void SetLineSize()
        {
            int CurrentLine = 0;
            float WordScale = 0f;
            
            for (int i = 0; i < Words.Count; i++)
            {
                // If word is a modifier, remove it.
                ////
                // Add functionality for displaying modifiers.
                // Possibly in player created text.
                // Maybe add a function to remove modifiers from a string, that way when players enter things such as names the modifiers cannot be abused.
                ////
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
                // Add the size of the current word to the LineSize.
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

            //Reset the CurrentLine to 0 ready for the next loop through the "Words" List.
            CurrentLine = 0;

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Text == "[BREAK]")
                {
                    CurrentLine++;
                    continue;
                }
                // If the current word is a texture add the size of the texture.
                // This is placed in a seperate loop as the height of the texture is determined by the height of the TextLine.
                else if (Words[i].CurrentSettings.Texture != null)
                {
                    float textureWidth = Words[i].CurrentSettings.Texture.Texture.Width;
                    float textureHeight = Words[i].CurrentSettings.Texture.Texture.Height;

                    float scale = Lines[CurrentLine].LineSize.Y / textureHeight;

                    Words[i].WordSize = new Vector2(textureWidth * scale, Lines[CurrentLine].LineSize.Y);

                    ////
                    // Not sure what this is...
                    //Words[i].WordSize.X += LetterSpacing / 2f;
                    //Words[i].WordSize *= 2f;
                    ////
                    
                    if (LineSpacing < 1f)
                        Lines[CurrentLine].LineSize.X += Words[i].WordSize.X * LineSpacing;
                    else
                        Lines[CurrentLine].LineSize.X += Words[i].WordSize.X;

                }
            }

            // If WordWrap is active and a line is larger than the textbox, change the TextLines to compensate.
            if (WordWrap)
            {
                for (int i = 0; i < Lines.Count; i++)
                    if (TextBoxSize.X < Lines[i].LineSize.X)
                    {
                        ExpandSmallBox();
                        break;
                    }
            }
            //Otherwise make sure the TextLines fit within the box, if not, change the BoxSize to compensate. 
            else
                SetBoxSize();
        }

        /// <summary>
        /// Checks to see if a string is a modifier.
        /// </summary>
        /// <param name="text">String to test.</param>
        /// <returns>Returns true if it is a modifier.</returns>
        private bool IsModifier(string text)
        {
            // If the string starts with "[" then it isn't a modifier, so return false.
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
        
        /// <summary>
        /// If the TextLines are longer than the TextBox, wrap the lines to fit within the box boundries.
        /// </summary>
        private void ExpandSmallBox()
        {
            float CurrentWordSize = 0f;
            float CurrentLineSize = 0f;
            FullWord CurrentWord;

            // Scans all the FullWords to check if they all fit within the box.
            // If the box is too small for any words then expand the box to fit the largest word.
            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Text == "[BREAK]")                
                    continue;                
                else
                {
                    CurrentWord = GetFullWord(i);
                    CurrentWordSize = CurrentWord.Size.X;

                    if (TextBoxSize.X < CurrentWordSize)
                        TextBoxSize.X = CurrentWordSize;

                    i += CurrentWord.Count - 1;
                }
            }

            int CurrentLine = 0;
            int OriginalLine = 0;

            //Clear the Lines List as we're making a new List.
            Lines.Clear();

            for (int i = 0; i < Words.Count; i++)
            {
                if (Words[i].Text == "[BREAK]")
                {
                    CurrentLineSize = 0f;
                    CurrentLine++;
                    OriginalLine++;
                    continue;
                }
                else
                {
                    //Creates new line and copies variables from original.
                    if (CurrentLineSize == 0f)
                    {
                        Lines.Add(new TextLine(""));
                        Lines[CurrentLine].Duplicate(OriginalLines[OriginalLine]);
                        Lines[CurrentLine].Text = "";
                    }

                    CurrentWord = GetFullWord(i);

                    // If the beginning of a line is a space, remove it. 
                    if (CurrentWord.Word == " " && CurrentLineSize == 0)
                    {
                        Words.RemoveAt(i);
                        CurrentWord = GetFullWord(i);
                    }

                    CurrentWordSize = CurrentWord.Size.X;

                    // Add the word size and text to the current line.
                    CurrentLineSize += CurrentWordSize;
                    Lines[CurrentLine].Text += CurrentWord.Word;
                    Lines[CurrentLine].LineSize.X += CurrentWordSize;

                    // if the new word makes the line larger than the box, remove it and create a new line.
                    if (CurrentLineSize > TextBoxSize.X)
                    {
                        Lines[CurrentLine].LineSize.X -= CurrentWordSize;
                        Lines[CurrentLine].Text = Lines[CurrentLine].Text.Remove(Lines[CurrentLine].Text.Length - CurrentWord.Word.Length);

                        Words.Insert(i, new WordInfo("[BREAK]"));
                        CurrentLineSize = 0f;
                        CurrentLine++;
                        continue;
                    }

                    // Set the LineSize.Y to the tallest word in the line.
                    if (CurrentWord.Size.Y > Lines[CurrentLine].LineSize.Y)
                        Lines[CurrentLine].LineSize.Y = CurrentWord.Size.Y;

                    i += CurrentWord.Count - 1;
                }
            }
            
            // Get the total Y dimension of the lines.
            // If it is larger than the TextBox Y dimension increase the TextBox size to compensate.
            float YSize = 0f;

            for (int i = 0; i < Lines.Count; i++)
                YSize += Lines[i].LineSize.Y;

            if (YSize > TextBoxSize.Y)
                TextBoxSize.Y = YSize;
        }

        /// <summary>
        /// Creates a full word from the "Words" List substrings.
        /// </summary>
        /// <param name="o">Index of the word to search at.</param>
        /// <returns>Returns a FullWord with text, size and index count.</returns>
        private FullWord GetFullWord(int o)
        {
            FullWord fullWord = new FullWord();

            //Gets the FullWord and values at the "o" index.
            for (int i = o; i < Words.Count; i++)
            {
                if (Words[i].Text != " " && Words[i].Text != "[BREAK]")
                {
                    fullWord.Word += Words[i].Text;
                    fullWord.Size.X += Words[i].WordSize.X;
                    if (fullWord.Size.Y < Words[i].WordSize.Y)
                        fullWord.Size.Y = Words[i].WordSize.Y;

                    fullWord.Count++;

                }
                else
                    break;
            }

            //If the word at index "o" is a space then return a space. Otherwise return the FullWord.
            if (Words[o].Text == " ")
                return new FullWord(Words[o].Text, Words[o].WordSize, 1);
            else
                return fullWord;
        }

        /// <summary>
        /// Make sure the TextLines fit within the box, if not, change the BoxSize to compensate.
        /// </summary>
        private void SetBoxSize()
        {
            Vector2 textBoxSize = Vector2.Zero;

            for (int i = 0; i < Lines.Count; i++)
            {
                // Set textBoxSize.X to the largest LineSize.X.
                if (Lines[i].LineSize.X > textBoxSize.X)
                    textBoxSize.X = Lines[i].LineSize.X;

                // Set textBoxSize.Y to the total LineSizes Y dimension.
                textBoxSize.Y += Lines[i].LineSize.Y * LineSpacing;
            }
            
            if (TextBoxSize.X < textBoxSize.X)
                TextBoxSize.X = textBoxSize.X;
            if (TextBoxSize.Y < textBoxSize.Y)
                TextBoxSize.Y = textBoxSize.Y;
        }

        #endregion

        #region Position Words


        /// <summary>
        /// Sets the text position relative to the TextBox.
        /// </summary>
        private void SetTextPosition()
        {
            int CurrentLine = 0;
            Vector2 CurrentOffset = Vector2.Zero;
            bool NewLine = true;
            Vector2 TextSize = Vector2.Zero;

            //If WordWrap is active set the TextSize to the size of the text.
            if (WordWrap)
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
                    
                    // If first line, set Y offset.
                    if (i == 0)
                    {
                        CurrentOffset.Y = -HalfText + ((HalfBox.Y - HalfText) * VerticalAlignment);

                        float LineSize = Lines[CurrentLine].LineSize.Y;
                        CurrentOffset.Y += (LineSpacing - 1) * (LineSize / 2f);
                    }

                    // Set X offset.
                    CurrentOffset.X -= HalfLine + ((HalfBox.X - HalfLine) * -Lines[CurrentLine].Alignment);
                                        
                    Lines[CurrentLine].LinePosition = new Vector2(CurrentOffset.X, CurrentOffset.Y);
                }

                if (Words[i].Text == "[BREAK]")
                {
                    // Add current line Height to Y offset.
                    CurrentOffset.Y += Lines[CurrentLine].LineSize.Y * LineSpacing;
                    // Set X offset to Center.
                    CurrentOffset.X = 0f;

                    NewLine = true;
                    Words.RemoveAt(i);
                    i--;
                    CurrentLine++;
                    continue;
                }

                // Set word offset to cuurent offset then add the X size.
                Words[i].Offset = CurrentOffset;
                CurrentOffset.X += Words[i].WordSize.X;

                // Set the CurrentLine for the Word.
                Words[i].Line = CurrentLine;
                
                // If the LineSize is bigger than the WordSize adjust the Y offset to compensate.
                if (Lines[CurrentLine].LineSize.Y > Words[i].WordSize.Y)
                {
                    if (Words[i].CurrentSettings.Subscript)
                        Words[i].Offset.Y += (Lines[CurrentLine].LineSize.Y - Words[i].WordSize.Y) * 0.99f;
                    else if (Words[i].CurrentSettings.Superscript)
                        Words[i].Offset.Y -= (Lines[CurrentLine].LineSize.Y - Words[i].WordSize.Y) * 0.01f;
                    else
                        Words[i].Offset.Y += (Lines[CurrentLine].LineSize.Y - Words[i].WordSize.Y) * 0.725f;
                }

                // If the current word is a texture adjust offset accordingly.
                ////
                // May interact badly with Subscript and Superscript.
                ////
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
                                
        #endregion

        /// <summary>
        /// Sets the current SpriteFont based on parameters.
        /// </summary>
        /// <param name="FontName">Name of the font.</param>
        /// <param name="Bold">If the font is bold.</param>
        /// <param name="Italic">If the font is italic.</param>
        private void SetFontParameters(string FontName, bool Bold, bool Italic)
        {
            if (Bold)
                FontName += "Bold";
            if (Italic)
                FontName += "Italic";

            ////
            //Add FontManager funtionality for returning a default font if input cannot be found.
            ////
            if (Font != FontManager.Fonts[FontName])
                Font = FontManager.Fonts[FontName];

            Font.LineSpacing = (int)(Font.MeasureString("0").Y * 0.65f);
            Font.Spacing = LetterSpacing;
        }

        /// <summary>
        /// Draw the TextBox.
        /// </summary>
        /// <param name="spriteBatch">XNA SpriteBatch</param>
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
            
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (Lines[i].LineSelected)
                    {
                        Box.Draw(spriteBatch, Position + Vector2.Transform(new Vector2(0, Lines[i].LinePosition.Y + (Lines[i].LineSize.Y / 2f)), Matrix.CreateRotationZ(Rotation)),
                            Lines[i].LineSelectedColor, Rotation, new Vector2(TextBoxSize.X + (TextPadding.X), Lines[i].LineSize.Y * LineSpacing) * Scale, SpriteEffects.None);
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
                            Words[i].CurrentSettings.TextureTint, Rotation, Words[i].WordSize * Words[i].CurrentSettings.TextureScale
 * Scale, SpriteEffects.None);


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
