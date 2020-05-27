using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class SheetTest
    {
        class SheetInfo
        {
            public int Elements;
            public List<Texture2D> Textures = new List<Texture2D>();
            public List<string> FileNames = new List<string>();
            public List<Rectangle> Rectangles = new List<Rectangle>();
        }
        
        static List<string> MainDirectories = new List<string>();
        //static List<Texture2D> SpriteSheets;

        static Dictionary<string, SheetInfo> SheetDictionary = new Dictionary<string, SheetInfo>();
        static TextureLoader textureLoader;

        static public void Initialize(GraphicsDevice graphicsDevice)
        {
            textureLoader = new TextureLoader(graphicsDevice);
            SheetDictionary.Clear();

            MainDirectories = Directory.GetDirectories("Images", "*", SearchOption.AllDirectories).ToList();

            //Get a list of texture directories.
            for (int i = 0; i < MainDirectories.Count; i++)            
                for (int o = 0; o < MainDirectories.Count; o++)                
                    if (MainDirectories[o].Contains(MainDirectories[i] + "\\"))
                    {
                        MainDirectories.RemoveAt(i);
                        i--;
                        break;
                    }
                


            for (int i = 0; i < MainDirectories.Count; i++)
            {
                //Get the texture group name.
                string name = MainDirectories[i].Replace("Images\\", "").Replace("\\", "-");
                SheetDictionary.Add(name, new SheetInfo());

                //Iterate through files in the folder.
                string[] Files = Directory.GetFiles(MainDirectories[i]);                
                for (int o = 0; o < Files.Length; o++)
                {
                    //if the file is not a PNG then skip.
                    if (Files[o].EndsWith(".png", true, CultureInfo.CurrentCulture))
                    {
                        //set the tetxture ID.
                        int t = SheetDictionary[name].Textures.Count;

                        //Add the texture.

                        SheetDictionary[name].Textures.Add(textureLoader.FromFile(Files[o]));

                        //using (FileStream filestream = File.Open(Files[o], FileMode.Open))
                        //    SheetDictionary[name].Textures.Add(Texture2D.FromStream(graphicsDevice, filestream));

                        //Add the file name.
                        string fileName = Files[o].Remove(Files[o].Length - 4);
                        fileName = fileName.Remove(0, fileName.LastIndexOf("\\") + 1);
                        SheetDictionary[name].FileNames.Add(fileName);

                        //Increase the number of elements in the object.
                        SheetDictionary[name].Elements++;

                        //Set the texture Rectangle.
                        SheetDictionary[name].Rectangles.Add(new Rectangle(0, 0, SheetDictionary[name].Textures[t].Width, SheetDictionary[name].Textures[t].Height));
                    }
                }
            }

            #region Create Atlas

            //Make sheet atlas.
            //foreach (SheetInfo Sheet in SheetDictionary.Values)
            //{
            //    int num = 4;

            //    while (num != 2048)
            //    {
            //        List<int> TexturesToRemove = new List<int>();
                    
            //        for (int i = 0; i < Sheet.Textures.Count; i++)                    
            //            if (Sheet.Textures[i].Width == Sheet.Textures[i].Height)                        
            //                if (Sheet.Textures[i].Width == num && Sheet.Textures[i].Height == num)
            //                {
            //                    TexturesToRemove.Add(i);
            //                }

            //        if (TexturesToRemove.Count > 2)
            //        {
            //        }                    

            //        num *= 2;
            //    }
            //}

            #endregion
        }

        static public Texture2D GetTexture(string textureName, int textureID)
        {
            return SheetDictionary[textureName].Textures[textureID];
        }

        static public Rectangle GetRectangle(string textureName, int textureID)
        {
            return SheetDictionary[textureName].Rectangles[textureID];
        }

        static public int GetRandomTextureID(string textureName, Random rand)
        {
            return rand.Next(0, SheetDictionary[textureName].Elements);
        }

        static public Texture2D GetRandomTexture(string textureName, Random rand)
        {
            return SheetDictionary[textureName].Textures[rand.Next(0, SheetDictionary[textureName].Elements)];
        }

        static public int GetTextureCount(string textureName)
        {
            return SheetDictionary[textureName].Elements;
        }

        static public Vector2 GetTextureOrigin(string textureName, int textureID)
        {
            return new Vector2(SheetDictionary[textureName].Textures[textureID].Width / 2, SheetDictionary[textureName].Textures[textureID].Height / 2);
        }

        #region Atlas code

        //Atlas creation code.
        //static public void LoadContent(GraphicsDevice graphics, string Name, int ImageSize)
        //{
        //    int size = 2048;

        //    SpriteSheets = new List<Texture2D>();

        //    ElementsPerSheet = (size / ImageSize) * (size / ImageSize);

        //    int Sheets = (int)(Textures.Count / (float)ElementsPerSheet) + 1;

        //    for (int b = 0; b < Sheets; b++)
        //    {
        //        for (int i = 0; i < ElementsPerSheet; i++)
        //        {
        //            int x = i * ImageSize;
        //            int y = 0;

        //            if (x >= size)
        //            {
        //                y = x / size;
        //                x -= size * y;
        //                y *= ImageSize;
        //            }

        //            Rectangles.Add(new Rectangle(x, y, ImageSize, ImageSize));
        //        }
        //    }

        //    Color[] texData;
        //    int sheet = -1;

        //    for (int i = 0; i < Textures.Count; i++)
        //    {
        //        if (Rectangles[i].X == 0 && Rectangles[i].Y == 0)
        //        {
        //            sheet++;
        //            SpriteSheets.Add(new Texture2D(graphics, size, size));
        //        }

        //        texData = new Color[Textures[i].Height * Textures[i].Width];
        //        Textures[i].GetData(texData);

        //        SpriteSheets[sheet].SetData(0, Rectangles[i], texData, 0, Textures[i].Height * Textures[i].Width);
        //    }

        //    //for (int i = 0; i < SpriteSheets.Count; i++)
        //    //{
        //    //    //using (FileStream f = new FileStream("C://test//" + Name + i + ".png", FileMode.Create))
        //    //    //    SpriteSheets[i].SaveAsPng(f, 2048, 2048);
        //    //}

        //    Textures.Clear();
        //    Textures.Capacity = 0;
        //}

        #endregion
    }
}
