using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    public struct TileData2
    {
        static ContentManager content;

        public static Texture2D[] TexturesTestSheet;
        public static List<Texture2D> TexturesTest;

        public static List<Rectangle> Rectangles;
        public static List<string> BlockNames;
        public static List<int> BlockHealth;
        public static List<Color> BlockColor;
        public static List<string> Description;

        public static List<ushort> IgneousIDs;

        static int tileSize;
        public static float SizeModifier;

        static public void Initialize(ContentManager Content)
        {
            content = Content;
            TexturesTest = new List<Texture2D>();


            BlockNames = new List<string>();
            BlockHealth = new List<int>();
            BlockColor = new List<Color>();
            Rectangles = new List<Rectangle>();
            Description = new List<string>();

            tileSize = WorldVariables.TileSize;
            SizeModifier = 0.5f;

            BlockNames.Add(null);
            BlockHealth.Add(0);
            BlockColor.Add(Color.White);
            TexturesTest.Add(null);

            IgneousIDs = new List<ushort>();

            AddBlocks();
        }

        static private void AddBlocks()
        {
            AddIgneousRock("Andesite", 10, 255, 255, 255, 255);
            AddIgneousRock("Anorthosite", 10, 255, 255, 255, 255);
            AddIgneousRock("Aplite", 10, 255, 255, 255, 255);
            AddIgneousRock("Basalt", 10, 255, 255, 255, 255);
            AddIgneousRock("Adakite", 10, 255, 255, 255, 255);
            AddIgneousRock("Hawaiite", 10, 255, 255, 255, 255);
            AddIgneousRock("Basanite", 10, 255, 255, 255, 255);
            AddIgneousRock("Boninite", 10, 255, 255, 255, 255);
            AddIgneousRock("Carbonatite", 10, 255, 255, 255, 255);
            AddIgneousRock("Charnockite", 10, 255, 255, 255, 255);
            AddIgneousRock("Enderbite", 10, 255, 255, 255, 255);
            AddIgneousRock("Dacite", 10, 255, 255, 255, 255);
            AddIgneousRock("Diabase", 10, 255, 255, 255, 255);
            AddIgneousRock("Diorite", 10, 255, 255, 255, 255);
            AddIgneousRock("Dunite", 10, 255, 255, 255, 255);
            AddIgneousRock("Essexite", 10, 255, 255, 255, 255);
            AddIgneousRock("Foidolite", 10, 255, 255, 255, 255);
            AddIgneousRock("Gabbro", 10, 255, 255, 255, 255);
            AddIgneousRock("Granite", 10, 255, 255, 255, 255);
            AddIgneousRock("Granodiorite", 10, 255, 255, 255, 255);
            AddIgneousRock("Granophyre", 10, 255, 255, 255, 255);
            AddIgneousRock("Harzburgite", 10, 255, 255, 255, 255);
            AddIgneousRock("Hornblendite", 10, 255, 255, 255, 255);
            AddIgneousRock("Hyaloclastite", 10, 255, 255, 255, 255);
            AddIgneousRock("Icelandite", 10, 255, 255, 255, 255);
            AddIgneousRock("Ignimbrite", 10, 255, 255, 255, 255);
            AddIgneousRock("Ijolite", 10, 255, 255, 255, 255);
            AddIgneousRock("Kimberlite", 10, 255, 255, 255, 255);
            AddIgneousRock("Komatiite", 10, 255, 255, 255, 255);
            AddIgneousRock("Lamproite", 10, 255, 255, 255, 255);
            AddIgneousRock("Lamprophyre", 10, 255, 255, 255, 255);
            AddIgneousRock("Latite", 10, 255, 255, 255, 255);
            AddIgneousRock("Lherzolite", 10, 255, 255, 255, 255);
            AddIgneousRock("Monzogranite", 10, 255, 255, 255, 255);
            AddIgneousRock("Monzonite", 10, 255, 255, 255, 255);
            AddIgneousRock("Nepheline Syenite", 10, 255, 255, 255, 255);
            AddIgneousRock("Nephelinite", 10, 255, 255, 255, 255);
            AddIgneousRock("Norite", 10, 255, 255, 255, 255);
            AddIgneousRock("Obsidian", 10, 255, 255, 255, 255);
            AddIgneousRock("Pegmatite", 10, 255, 255, 255, 255);
            AddIgneousRock("Peridotite", 10, 255, 255, 255, 255);
            AddIgneousRock("Phonolite", 10, 255, 255, 255, 255);
            AddIgneousRock("Picrite", 10, 255, 255, 255, 255);
            AddIgneousRock("Porphyry", 10, 255, 255, 255, 255);
            AddIgneousRock("Pumice", 10, 255, 255, 255, 255);
            AddIgneousRock("Pyroxenite", 10, 255, 255, 255, 255);
            AddIgneousRock("Quartz Diorite", 10, 255, 255, 255, 255);
            AddIgneousRock("Quartz Monzonite", 10, 255, 255, 255, 255);
            AddIgneousRock("Rhyodacite", 10, 255, 255, 255, 255);
            AddIgneousRock("Rhyolite", 10, 255, 255, 255, 255);
            AddIgneousRock("Comendite", 10, 255, 255, 255, 255);
            AddIgneousRock("Pantellerite", 10, 255, 255, 255, 255);
            AddIgneousRock("Scoria", 10, 255, 255, 255, 255);
            AddIgneousRock("Sovite", 10, 255, 255, 255, 255);
            AddIgneousRock("Syenite", 10, 255, 255, 255, 255);
            AddIgneousRock("Tachylyte", 10, 255, 255, 255, 255);
            AddIgneousRock("Tephrite", 10, 255, 255, 255, 255);
            AddIgneousRock("Tonalite", 10, 255, 255, 255, 255);
            AddIgneousRock("Trachyandesite", 10, 255, 255, 255, 255);
            AddIgneousRock("Benmoreite", 10, 255, 255, 255, 255);
            AddIgneousRock("Basaltic Trachyandesite", 10, 255, 255, 255, 255);
            AddIgneousRock("Mugearite", 10, 255, 255, 255, 255);
            AddIgneousRock("Shoshonite", 10, 255, 255, 255, 255);
            AddIgneousRock("Trachyte", 10, 255, 255, 255, 255);
            AddIgneousRock("Troctolite", 10, 255, 255, 255, 255);
            AddIgneousRock("Trondhjemite", 10, 255, 255, 255, 255);
            AddIgneousRock("Tuff", 10, 255, 255, 255, 255);
            AddIgneousRock("Websterite", 10, 255, 255, 255, 255);
            AddIgneousRock("Wehrlite", 10, 255, 255, 255, 255);
        }

        static private void AddIgneousRock(string Name, int Health, byte R, byte G, byte B, byte A)
        {
            BlockNames.Add(Name);
            BlockHealth.Add(Health);
            BlockColor.Add(new Color(R,G,B,A));


            //TexturesTest.Add(content.Load<Texture2D>("Images//Blocks//Rocks//Igneous" + Name));
            TexturesTest.Add(content.Load<Texture2D>("Images//Blocks//Stonex2"));
            IgneousIDs.Add((ushort)(BlockNames.Count - 1));
        }

        static public void LoadContent(ContentManager Content, Texture2D blankTexture)
        {
            TexturesTestSheet = new Texture2D[16];
            TexturesTestSheet[0] = blankTexture;

            int ElementsPerSheet = (blankTexture.Width / (int)(tileSize / SizeModifier)) * (blankTexture.Width / (int)(tileSize / SizeModifier));

            for (int i = 0; i <= ElementsPerSheet; i++)
            {
                int x = i * (int)(tileSize / SizeModifier);
                int y = 0;

                if (x >= blankTexture.Width)
                {
                    y = x / blankTexture.Width;
                    x -= blankTexture.Width * y;
                    y *= (int)(tileSize / SizeModifier);
                }

                Rectangles.Add(new Rectangle(x, y, (int)(tileSize / SizeModifier), (int)(tileSize / SizeModifier)));
            }

            Color[] texData;

            for (int i = 1; i < BlockNames.Count; i++)
            {
                texData = new Color[TexturesTest[i].Height * TexturesTest[i].Width];
                TexturesTest[i].GetData(texData);

                TexturesTestSheet[0].SetData(0, Rectangles[i], texData, 0, TexturesTest[i].Height * TexturesTest[i].Width);
            }            
        }

        

    }
}
