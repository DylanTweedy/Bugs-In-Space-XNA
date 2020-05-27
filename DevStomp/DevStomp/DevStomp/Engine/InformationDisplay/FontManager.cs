using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class FontManager
    {
        static public Dictionary<string, SpriteFont> Fonts = new Dictionary<string,SpriteFont>();
        static public string DefaultFont = "Helvetica";

        static public void LoadContent(ContentManager Content)
        {
            AddFont("Arial", Content);
            AddFont("Helvetica", Content);
            AddFont("Tahoma", Content);
            AddFont("TimesNewRoman", Content);
        }

        static public void AddFont(string Name, ContentManager Content)
        {
            Fonts.Add(Name, Content.Load<SpriteFont>("Fonts//" + Name));
            Fonts.Add(Name + "Italic", Content.Load<SpriteFont>("Fonts//" + Name + "-Italic"));
            Fonts.Add(Name + "Bold", Content.Load<SpriteFont>("Fonts//" + Name + "-Bold"));
            Fonts.Add(Name + "BoldItalic", Content.Load<SpriteFont>("Fonts//" + Name + "-BoldItalic"));

            Fonts[Name].Spacing = 0;
            Fonts[Name].LineSpacing = 31;
            Fonts[Name + "Italic"].Spacing = 0;
            Fonts[Name + "Italic"].LineSpacing = 31;
            Fonts[Name + "Bold"].Spacing = 0;
            Fonts[Name + "Bold"].LineSpacing = 31;
            Fonts[Name + "BoldItalic"].Spacing = 0;
            Fonts[Name + "BoldItalic"].LineSpacing = 31;
        }
    }
}
