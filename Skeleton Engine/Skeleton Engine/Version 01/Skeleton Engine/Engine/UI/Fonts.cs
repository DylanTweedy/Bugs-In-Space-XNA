using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class FontManager
    {
        static public Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        static public string DefaultFont = "Helvetica";

        static public void LoadContent(ContentManager Content)
        {
            Fonts.Add("Default", Content.Load<SpriteFont>("Fonts//1.xnb"));

            //AddFont("Arial", Content);
            //AddFont("Helvetica", Content);
            //AddFont("Tahoma", Content);
            //AddFont("TimesNewRoman", Content);
        }

        static public void AddFont(string Name, ContentManager Content)
        {
            Fonts.Add(Name, Content.Load<SpriteFont>("Content//Fonts//" + Name));
            Fonts.Add(Name + "Italic", Content.Load<SpriteFont>("Content//Fonts//" + Name + "-Italic"));
            Fonts.Add(Name + "Bold", Content.Load<SpriteFont>("Content//Fonts//" + Name + "-Bold"));
            Fonts.Add(Name + "BoldItalic", Content.Load<SpriteFont>("Content//Fonts//" + Name + "-BoldItalic"));

            Fonts[Name].Spacing = 0;
            Fonts[Name].LineSpacing = (int)(Fonts[Name].MeasureString("0").Y * 0.65f);
            Fonts[Name + "Italic"].Spacing = 0;
            Fonts[Name + "Italic"].LineSpacing = (int)(Fonts[Name + "Italic"].MeasureString("0").Y * 0.65f);
            Fonts[Name + "Bold"].Spacing = 0;
            Fonts[Name + "Bold"].LineSpacing = (int)(Fonts[Name + "Bold"].MeasureString("0").Y * 0.65f);
            Fonts[Name + "BoldItalic"].Spacing = 0;
            Fonts[Name + "BoldItalic"].LineSpacing = (int)(Fonts[Name + "BoldItalic"].MeasureString("0").Y * 0.65f);
        }
    }
}
