using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine.UI
{
    class TextBox
    {
        class TextLine
        {
            public string Text;

            public TextLine(string text)
            {
                Text = text;
            }
        }

        List<TextLine> Text = new List<TextLine>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextBox(string text)
        {
            Initialize();
            //Add string to the list.
            Text.Add(new TextLine(text));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class from a list of strings.
        /// </summary>
        /// <param name="text">The list of strings.</param>
        public TextBox(List<string> text)
        {
            Initialize();
            //Adds each string line to the list.
            for (int i = 0; i < text.Count; i++)
                Text.Add(new TextLine(text[i]));
        }
        
        private void Initialize()
        {

        }

        public void AddLine(string text)
        {
            Text.Add(new TextLine(text));
        }
        
        public void Update()
        {

        }

        public void Draw()
        {
            for (int i = 0; i < Text.Count; i++)
                Graphics.spriteBatch.DrawString(FontManager.Fonts["Arial"], Text[i].Text, Vector2.Zero, Color.White);
        }
    }
}
