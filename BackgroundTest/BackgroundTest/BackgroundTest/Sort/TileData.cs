using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    public struct TileData
    {
        static ContentManager content;

        public static List<Texture2D> BlockSheets;
        public static List<Texture2D> BlockTextures;
        public static List<int>[] BlockTextureIDs;

        public static List<Texture2D> MaskSheets;
        public static List<Texture2D> MaskTextures;
        public static List<int>[] SlopeMaskIDs;
        public static List<int>[] BlockMaskIDs;

        public static List<Texture2D> DecoSheets;
        public static List<Texture2D> DecoTextures;
        public static List<int>[] DecoTextureIDs;

        public static List<Rectangle> Rectangles;
        public static List<string> BlockNames;
        public static List<int> BlockHealth;
        public static List<Color> BlockColor;
        public static List<string> Description;
        public static List<ushort> BlockType;
        public static List<int> Probablities;

        static int tileSize;
        static int TextureCounter;
        static bool LoadTextures;
        public static float SizeModifier;

        static Random random;
        static int RockProbability;

        static public void Initialize(ContentManager Content)
        {
            content = Content;
            random = new Random();

            BlockTextures = new List<Texture2D>();
            BlockTextureIDs = new List<int>[65535];
            for (int i = 0; i < BlockTextureIDs.Length; i++)
                BlockTextureIDs[i] = new List<int>();

            MaskTextures = new List<Texture2D>();

            BlockMaskIDs = new List<int>[65535];
            for (int i = 0; i < BlockMaskIDs.Length; i++)
                BlockMaskIDs[i] = new List<int>();

            SlopeMaskIDs = new List<int>[65535];
            for (int i = 0; i < SlopeMaskIDs.Length; i++)
                SlopeMaskIDs[i] = new List<int>();

            DecoTextures = new List<Texture2D>();
            DecoTextureIDs = new List<int>[65535];
            for (int i = 0; i < DecoTextureIDs.Length; i++)
                DecoTextureIDs[i] = new List<int>();
                      
            BlockNames = new List<string>();
            BlockHealth = new List<int>();
            BlockColor = new List<Color>();
            Rectangles = new List<Rectangle>();
            Description = new List<string>();
            BlockType = new List<ushort>();
            Probablities = new List<int>();

            tileSize = WorldVariables.TileSize;
            SizeModifier = 0.5f;

            BlockNames.Add(null);
            BlockHealth.Add(0);
            BlockColor.Add(Color.White);
            BlockType.Add(0);
            Probablities.Add(0);

            BlockTextures.Add(content.Load<Texture2D>("Images//Blocks//NoTexture"));
            BlockTextureIDs[BlockTextures.Count - 1].Add(BlockTextures.Count - 1);

            MaskTextures.Add(content.Load<Texture2D>("Images//Blocks//Masks//NoBlockMask"));
            BlockMaskIDs[0].Add(MaskTextures.Count - 1);
            MaskTextures.Add(content.Load<Texture2D>("Images//Blocks//Masks//NoSlopeMask"));
            SlopeMaskIDs[0].Add(MaskTextures.Count - 1);

            DecoTextures.Add(content.Load<Texture2D>("Images//Blocks//Decorations//NoDecoration"));
            DecoTextureIDs[0].Add(DecoTextures.Count - 1);

            //AddBlocks();
            TestCompounds();
            AddTypeMasks();
            AddTypeDecorations();
        }

        static private void AddBlocks()
        {
            AddIgneousRock("Andesite", 10, 250, 250, 255, 255, 1);
            AddIgneousRock("Anorthosite", 10, 207, 200, 195, 255, 1);
            AddIgneousRock("Aplite", 10, 243, 202, 174, 255, 1);
            AddIgneousRock("Basalt", 10, 45, 45, 45, 255, 1);
            AddIgneousRock("Adakite", 10, 217, 195, 178, 255, 1);
            AddIgneousRock("Hawaiite", 10, 195, 144, 134, 255, 1);
            AddIgneousRock("Basanite", 10, 125, 125, 150, 255, 1);
            AddIgneousRock("Boninite", 10, 199, 188, 184, 255, 1);
            AddIgneousRock("Carbonatite", 10, 195, 187, 167, 255, 1);
            AddIgneousRock("Charnockite", 10, 194, 178, 139, 255, 1);
            AddIgneousRock("Enderbite", 10, 196, 196, 206, 255, 1);
            AddIgneousRock("Dacite", 10, 156, 159, 193, 255, 1);
            AddIgneousRock("Diabase", 10, 193, 178, 163, 255, 1);
            AddIgneousRock("Diorite", 10, 169, 178, 209, 255, 1);
            AddIgneousRock("Dunite", 10, 219, 228, 164, 255, 1);
            AddIgneousRock("Essexite", 10, 193, 198, 194, 255, 1);
            AddIgneousRock("Foidolite", 10, 199, 208, 205, 255, 1);
            AddIgneousRock("Gabbro", 10, 75, 82, 158, 255, 1);
            AddIgneousRock("Granite", 10, 193, 184, 164, 255, 1);
            AddIgneousRock("Granodiorite", 10, 187, 210, 210, 255, 1);
            AddIgneousRock("Granophyre", 10, 223, 156, 123, 255, 1);
            AddIgneousRock("Harzburgite", 10, 196, 211, 185, 255, 1);
            AddIgneousRock("Hornblendite", 10, 125, 130, 145, 255, 1);
            AddIgneousRock("Hyaloclastite", 10, 168, 164, 163, 255, 1);
            AddIgneousRock("Icelandite", 10, 214, 166, 163, 255, 1);
            AddIgneousRock("Ignimbrite", 10, 209, 183, 178, 255, 1);
            AddIgneousRock("Ijolite", 10, 205, 205, 205, 255, 1);
            AddIgneousRock("Kimberlite", 10, 191, 201, 200, 255, 1);
            AddIgneousRock("Komatiite", 10, 217, 196, 175, 255, 1);
            AddIgneousRock("Lamproite", 10, 206, 181, 151, 255, 1);
            AddIgneousRock("Lamprophyre", 10, 200, 190, 175, 255, 1);
            AddIgneousRock("Latite", 10, 212, 178, 167, 255, 1);
            AddIgneousRock("Lherzolite", 10, 165, 200, 161, 255, 1);
            AddIgneousRock("Monzogranite", 10, 202, 163, 155, 255, 1);
            AddIgneousRock("Monzonite", 10, 220, 208, 214, 255, 1);
            AddIgneousRock("Nepheline Syenite", 10, 203, 204, 200, 255, 1);
            AddIgneousRock("Nephelinite", 10, 204, 202, 197, 255, 1);
            AddIgneousRock("Norite", 10, 221, 195, 180, 255, 1);
            AddIgneousRock("Obsidian", 10, 100, 100, 100, 255, 1);
            AddIgneousRock("Pegmatite", 10, 200, 200, 200, 255, 1);
            AddIgneousRock("Peridotite", 10, 207, 204, 103, 255, 1);
            AddIgneousRock("Phonolite", 10, 200, 200, 200, 255, 1);
            AddIgneousRock("Picrite", 10, 187, 199, 220, 255, 1);

            AddIgneousRock("Porphyry", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Pumice", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Pyroxenite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Quartz Diorite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Quartz Monzonite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Rhyodacite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Rhyolite", 10, 255, 255, 255, 255, 0);

            AddIgneousRock("Comendite", 10, 193, 202, 209, 255, 1);
            AddIgneousRock("Pantellerite", 10, 183, 205, 182, 255, 1);

            AddIgneousRock("Scoria", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Sovite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Syenite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Tachylyte", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Tephrite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Tonalite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Trachyandesite", 10, 255, 255, 255, 255, 0);

            AddIgneousRock("Benmoreite", 10, 212, 182, 153, 255, 1);
            AddIgneousRock("Basaltic Trachyandesite", 10, 205, 197, 206, 255, 1);
            AddIgneousRock("Mugearite", 10, 200, 200, 200, 255, 1);

            AddIgneousRock("Shoshonite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Trachyte", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Troctolite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Trondhjemite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Tuff", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Websterite", 10, 255, 255, 255, 255, 0);
            AddIgneousRock("Wehrlite", 10, 255, 255, 255, 255, 0);
        }

        static private void TestCompounds()
        {
            for (int i = 0; i < ElementTable.Compounds.Count; i++)
            {
                BlockNames.Add(ElementTable.Compounds[i].Name);
                BlockHealth.Add((int)(ElementTable.Compounds[i].Hardness * 10f));
                BlockColor.Add(Color.White);
                BlockType.Add(1);
                Probablities.Add(1);

                for (int t = 0; t < ElementTable.Compounds[i].Textures.Count; t++)
                {
                    BlockTextures.Add(ElementTable.Compounds[i].Textures[t]);
                    BlockTextureIDs[BlockNames.Count - 1].Add(BlockTextures.Count - 1);
                }
            }
        }

        static void AddTypeDecorations()
        {
            AddTypeDecoration(1, "Rocks");
        }

        static void AddTypeMasks()
        {
            AddTypeMask(1, "Rocks");
        }

        static private void AddIgneousRock(string Name, int Health, byte R, byte G, byte B, byte A, int Probability)
        {
            LoadTextures = true;

            BlockNames.Add(Name);
            BlockHealth.Add(Health);
            BlockColor.Add(new Color(R,G,B,A));
            BlockType.Add(1);
            Probablities.Add(Probability);

            AddTexture("Rocks//Igneous//");
        }

        static void AddTexture(string Path)
        {
            TextureCounter = 0;
            LoadTextures = true;
            while (LoadTextures)
            {
                string number = "" + TextureCounter;

                if (TextureCounter < 100)
                    number = 0 + number;

                if (TextureCounter < 10)
                    number = 0 + number;

                AddTextures(Path + BlockNames[BlockNames.Count - 1] + number, (ushort)(BlockNames.Count - 1));
            }
        }

        static void AddTextures(string texture, ushort ID)
        {
            try
            {
                BlockTextures.Add(content.Load<Texture2D>("Images//Blocks//" + texture));
                BlockTextureIDs[ID].Add(BlockTextures.Count - 1);
            }
            catch (ContentLoadException e)
            {
                if (TextureCounter == 0)
                {
                    BlockTextures.Add(content.Load<Texture2D>("Images//Blocks//NoTexture"));
                    BlockTextureIDs[ID].Add(BlockTextures.Count - 1);
                }

                LoadTextures = false;
            }

            TextureCounter++;
        }

        static void AddTypeMask(byte bType, string type)
        {
            TextureCounter = 0;
            LoadTextures = true;
            while (LoadTextures)
                AddMasks(bType, type, true);

            TextureCounter = 0;
            LoadTextures = true;
            while (LoadTextures)
                AddMasks(bType, type, false);
        }

        static void AddTypeDecoration(byte bType, string type)
        {
            TextureCounter = 0;
            LoadTextures = true;
            while (LoadTextures)
                AddDecoration(bType, type);
        }
        
        static void AddMasks(byte bType, string type, bool block)
        {
            byte blockType = bType;

            string number = "" + TextureCounter;

            if (TextureCounter < 100)
                number = 0 + number;

            if (TextureCounter < 10)
                number = 0 + number;

            try
            {
                if (block)
                {
                    MaskTextures.Add(content.Load<Texture2D>("Images//Blocks//Masks//" + type + "//Blocks//" + number));

                    for (int i = 0; i < BlockType.Count; i++)
                    {
                        if (BlockType[i] == bType)
                            BlockMaskIDs[i].Add(MaskTextures.Count - 1);
                    }
                    
                }
                else
                {
                    MaskTextures.Add(content.Load<Texture2D>("Images//Blocks//Masks//" + type + "//Slopes//" + number));

                    for (int i = 0; i < BlockType.Count; i++)
                    {
                        if (BlockType[i] == bType)
                            SlopeMaskIDs[i].Add(MaskTextures.Count - 1);
                    }
                }
            }
            catch (ContentLoadException e)
            {
                if (TextureCounter == 0)
                {
                    if (block)
                    {
                        MaskTextures.Add(content.Load<Texture2D>("Images//Blocks//Masks//NoBlockMask"));

                        for (int i = 0; i < BlockType.Count; i++)
                        {
                            if (BlockType[i] == bType)
                                BlockMaskIDs[i].Add(MaskTextures.Count - 1);
                        }
                    }
                    else
                    {
                        MaskTextures.Add(content.Load<Texture2D>("Images//Blocks//Masks//NoSlopeMask"));

                        for (int i = 0; i < BlockType.Count; i++)
                        {
                            if (BlockType[i] == bType)
                                SlopeMaskIDs[i].Add(MaskTextures.Count - 1);
                        }
                    }
                }

                LoadTextures = false;
            }

            TextureCounter++;
        }

        static void AddDecoration(byte bType, string type)
        {
            byte blockType = bType;

            string number = "" + TextureCounter;

            if (TextureCounter < 100)
                number = 0 + number;

            if (TextureCounter < 10)
                number = 0 + number;

            try
            {
                DecoTextures.Add(content.Load<Texture2D>("Images//Blocks//Decorations//" + type + "//" + number));

                for (int i = 0; i < BlockType.Count; i++)
                {
                    if (BlockType[i] == bType)
                        DecoTextureIDs[i].Add(DecoTextures.Count - 1);
                }
            }
            catch (ContentLoadException e)
            {
                if (TextureCounter == 0)
                {
                    DecoTextures.Add(content.Load<Texture2D>("Images//Blocks//Decorations//NoDecoration"));

                    for (int i = 0; i < BlockType.Count; i++)
                    {
                        if (BlockType[i] == bType)
                            DecoTextureIDs[i].Add(DecoTextures.Count - 1);
                    }
                }

                LoadTextures = false;
            }

            TextureCounter++;
        }

        static public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            int size = 2048;

            BlockSheets = new List<Texture2D>();
            BlockSheets.Add(new Texture2D(graphics, size, size));

            MaskSheets = new List<Texture2D>();
            MaskSheets.Add(new Texture2D(graphics, size, size));

            DecoSheets = new List<Texture2D>();
            DecoSheets.Add(new Texture2D(graphics, size, size));

            int ElementsPerSheet = (size / (int)(tileSize / SizeModifier)) * (size / (int)(tileSize / SizeModifier));

            int SheetsNeededTextures = (int)(BlockTextures.Count / (float)ElementsPerSheet) + 1;
            int SheetsNeededMasks = (int)(MaskTextures.Count / (float)ElementsPerSheet) + 1;
            int SheetsNeededDeco = (int)(DecoTextures.Count / (float)ElementsPerSheet) + 1;

            int highCount = SheetsNeededDeco + SheetsNeededMasks + SheetsNeededTextures;

            for (int b = 0; b < highCount; b++)
            {
                for (int i = 0; i < ElementsPerSheet; i++)
                {
                    int x = i * (int)(tileSize / SizeModifier);
                    int y = 0;

                    if (x >= size)
                    {
                        y = x / size;
                        x -= size * y;
                        y *= (int)(tileSize / SizeModifier);
                    }

                    Rectangles.Add(new Rectangle(x, y, (int)(tileSize / SizeModifier), (int)(tileSize / SizeModifier)));
                }
            }

            Color[] texData;
            int sheet = 0;

            for (int i = 0; i < BlockTextures.Count; i++)
            {
                texData = new Color[BlockTextures[i].Height * BlockTextures[i].Width];
                BlockTextures[i].GetData(texData);

                BlockSheets[sheet].SetData(0, Rectangles[i], texData, 0, BlockTextures[i].Height * BlockTextures[i].Width);

                if (Rectangles[i].X == 2016 && Rectangles[i].Y == 2016)
                {
                    sheet++;
                    BlockSheets.Add(new Texture2D(graphics, size, size));
                }
            }

            sheet = 0;

            for (int i = 0; i < MaskTextures.Count; i++)
            {
                texData = new Color[MaskTextures[i].Height * MaskTextures[i].Width];
                MaskTextures[i].GetData(texData);

                MaskSheets[sheet].SetData(0, Rectangles[i], texData, 0, MaskTextures[i].Height * MaskTextures[i].Width);

                if (Rectangles[i].X == 2016 && Rectangles[i].Y == 2016)
                    sheet++;
            }

            sheet = 0;

            for (int i = 0; i < DecoTextures.Count; i++)
            {
                texData = new Color[DecoTextures[i].Height * DecoTextures[i].Width];
                DecoTextures[i].GetData(texData);

                DecoSheets[sheet].SetData(0, Rectangles[i], texData, 0, DecoTextures[i].Height * DecoTextures[i].Width);

                if (Rectangles[i].X == 2016 && Rectangles[i].Y == 2016)
                    sheet++;
            }

            using (FileStream f = new FileStream("C://test//" + "block" + ".png", FileMode.Create))
                BlockSheets[0].SaveAsPng(f, 2048, 2048);

            using (FileStream f = new FileStream("C://test//" + "mask" + ".png", FileMode.Create))
                MaskSheets[0].SaveAsPng(f, 2048, 2048);

            using (FileStream f = new FileStream("C://test//" + "deco" + ".png", FileMode.Create))
                DecoSheets[0].SaveAsPng(f, 2048, 2048);

            BlockTextures.Clear();
            MaskTextures.Clear();
            DecoTextures.Clear();

            BlockTextures.Capacity = 0;
            MaskTextures.Capacity = 0;
            DecoTextures.Capacity = 0;

            LoadRockProbability();
        }

        static private void LoadRockProbability()
        {
            RockProbability = 0;

            for (int i = 0; i < Probablities.Count; i++)
            {
                if (BlockType[i] == 1)
                    RockProbability += Probablities[i];
            }
        }

        static public ushort GetRockID()
        {
            int selection = random.Next(0, RockProbability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < Probablities.Count; i++)
            {
                if (BlockType[i] == 1)
                {
                    counter += Probablities[i];

                    if (selection >= previousCounter && selection <= counter)
                        return (ushort)i;
                    

                    previousCounter = counter;
                }
            }

            return 0;
        }
    }
}
