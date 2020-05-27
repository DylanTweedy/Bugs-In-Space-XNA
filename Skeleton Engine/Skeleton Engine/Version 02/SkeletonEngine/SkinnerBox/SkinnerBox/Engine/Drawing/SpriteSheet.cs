//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//namespace SkeletonEngine
//{
//    public class Sheet
//    {
//        public List<Texture2D> Sheets;
//        public List<Rectangle> Rectangles;
//        public int Elements;
//        public int ElementsPerSheet;
//        public int Size;

//        public Texture2D S(int ID)
//        {
//            int r = (int)Math.Floor((float)ID / (float)ElementsPerSheet);            
//            return Sheets[r];
//        }
//    }

//    static class SpriteSheet
//    {
//        static ContentManager content;

//        public static List<Texture2D> SpriteSheets;
//        public static List<Texture2D> Textures;
        
//        public static List<Rectangle> Rectangles;

//        static GraphicsDevice graphicsDevice;

//        static int tileWidth;
//        static int tileHeight;
//        static int TextureCounter;
//        static bool LoadTextures;
//        static int Elements;
//        static int ElementsPerSheet;

//        static Random random;

//        static public Sheet Initialize(ContentManager Content, GraphicsDevice graphics, string Path, string Name)
//        {
//            content = Content;
//            graphicsDevice = graphics;
//            random = new Random();

//            Textures = new List<Texture2D>();
//            //IDs = new List<int>[65535];
//            //for (int i = 0; i < IDs.Length; i++)
//            //    IDs[i] = new List<int>();
            
//            Rectangles = new List<Rectangle>();
                        
//            AddTexture(Path);
//            LoadContent(graphics, Name);

//            Sheet load = new Sheet();
//            load.Sheets = SpriteSheets;
//            load.Rectangles = Rectangles;
//            load.Elements = Elements;
//            load.ElementsPerSheet = ElementsPerSheet;
//            load.Size = tileWidth;

//            return load;
//        }                 

//        static void AddTexture(string Path)
//        {
//            TextureCounter = 0;
//            LoadTextures = true;
//            while (LoadTextures)
//            {
//                string number = "" + TextureCounter;

//                if (TextureCounter < 100)
//                    number = 0 + number;

//                if (TextureCounter < 10)
//                    number = 0 + number;

//                AddTextures("Images//" + Path + number, (ushort)TextureCounter);
//            }
//        }

//        static void AddTextures(string texture, ushort ID)
//        {
//            try
//            {
//                string[] filePaths = Directory.GetDirectories("Images");


//                FileStream filestream = new FileStream(texture + ".PNG", FileMode.Open);
//                Texture2D myTexture = Texture2D.FromStream(graphicsDevice, filestream);

//                Textures.Add(myTexture);

//                //Textures.Add(content.Load<Texture2D>(texture));

//                if (TextureCounter == 0)
//                {
//                    tileWidth = Textures[0].Width;
//                    tileHeight = Textures[0].Height;

//                }
//            }
//            catch (FileNotFoundException e)
//            {
//                LoadTextures = false;
//                Elements = Textures.Count;
//            }
//            catch (ContentLoadException e)
//            {
//                //if (TextureCounter == 0)
//                //{
//                //    Textures.Add(content.Load<Texture2D>("Images//Blocks//NoTexture"));
//                //    IDs[ID].Add(Textures.Count - 1);
//                //}

//                LoadTextures = false;
//                Elements = Textures.Count;
//            }

//            TextureCounter++;
//        }

//        static public void LoadContent(GraphicsDevice graphics, string Name)
//        {
//            int size = 2048;

//            SpriteSheets = new List<Texture2D>();

//            ElementsPerSheet = (size / tileWidth) * (size / tileHeight);

//            int Sheets = (int)(Textures.Count / (float)ElementsPerSheet) + 1;

//            for (int b = 0; b < Sheets; b++)
//            {
//                for (int i = 0; i < ElementsPerSheet; i++)
//                {
//                    int x = i * tileWidth;
//                    int y = 0;

//                    if (x >= size)
//                    {
//                        y = x / size;
//                        x -= size * y;
//                        y *= tileHeight;
//                    }

//                    Rectangles.Add(new Rectangle(x, y, tileWidth, tileHeight));
//                }
//            }

//            Color[] texData;
//            int sheet = -1;

//            for (int i = 0; i < Textures.Count; i++)
//            {
//                if (Rectangles[i].X == 0 && Rectangles[i].Y == 0)
//                {
//                    sheet++;
//                    SpriteSheets.Add(new Texture2D(graphics, size, size));
//                }

//                texData = new Color[Textures[i].Height * Textures[i].Width];
//                Textures[i].GetData(texData);

//                SpriteSheets[sheet].SetData(0, Rectangles[i], texData, 0, Textures[i].Height * Textures[i].Width);
//            }

//            for (int i = 0; i < SpriteSheets.Count; i++)
//            {
//                //using (FileStream f = new FileStream("C://test//" + Name + i + ".png", FileMode.Create))
//                //    SpriteSheets[i].SaveAsPng(f, 2048, 2048);
//            }

//            Textures.Clear();
//            Textures.Capacity = 0;
//        }
//    }
//}

