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
    static class SheetManager
    {
        class SheetInfo
        {
            public int Elements;
            public Dictionary<string, TextureInfo> Textures = new Dictionary<string, TextureInfo>();
        }

        class TextureInfo
        {
            public Texture2D Texture;
            public Texture2D PreviousTexture;
            public DateTime LastWriteTime;
            public DateTime LoadTime;


            public TextureInfo(Texture2D texture, DateTime lastWriteTime, DateTime loadTime, Texture2D previousTexture)
            {
                Texture = texture;
                LastWriteTime = lastWriteTime;
                LoadTime = loadTime;
                PreviousTexture = previousTexture;
            }

        }
        

        static List<string> MainDirectories = new List<string>();


        static Dictionary<string, SheetInfo> SheetDictionary = new Dictionary<string, SheetInfo>();
        static TextureLoader textureLoader;



        static float Timer = float.MaxValue;
        static float Timer2 = float.MaxValue;

        static int DirectoryCounter = 0;
        static int FileCounter = 0;
        static string CurrentDirectory;
        static List<string> CurrentFileList;

        static public void Initialize(GraphicsDevice graphicsDevice)
        {
            textureLoader = new TextureLoader(graphicsDevice);
            Timer = float.MaxValue;
            Timer2 = float.MaxValue;

            DirectoryCounter = 0;
            FileCounter = 0;
            CurrentDirectory = "";
            CurrentFileList = new List<string>();

            SheetDictionary.Clear();
            MainDirectories.Clear();
        }
                
        static public void Update(GraphicsDevice graphicsDevice)
        {
            if (Timer > 60f)
            {
                UpdateDirectories();
                Timer = 0f;
            }

            //for (int d = 0; d < MainDirectories.Count; d++)
            //{


            //if (Timer2 > 0.5f)
            {
                //if (GlobalVariables.frameRate > 60)
                if (AddDirectory(DirectoryCounter, false))
                    DirectoryCounter++;

                Timer2 = 0f;
            }
            //}

            if (DirectoryCounter >= MainDirectories.Count)
                DirectoryCounter = 0;

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

            Timer += GlobalVariables.FrameTime;
            Timer2 += GlobalVariables.FrameTime;
        }

        static private void UpdateDirectories()
        {
            MainDirectories = Directory.GetDirectories("Images", "*", SearchOption.AllDirectories).ToList();

            for (int d = 0; d < MainDirectories.Count; d++)
            {
                List<string> files = Directory.GetFiles(MainDirectories[d]).ToList();

                for (int f = 0; f < files.Count; f++)
                {
                    if (!files[f].ToLower().EndsWith("png"))
                    {
                        files.RemoveAt(f);
                        f--;
                    }
                }

                if (files.Count == 0)
                {
                    MainDirectories.RemoveAt(d);
                    d--;
                }
                else if (MainDirectories[d] == "Images\\Core")
                {
                    while(!AddDirectory(d, true));
                }
            }

        }

        static private bool AddDirectory(int d, bool WholeDirectory)
        {
            if (FileCounter == 0)
            {
                CurrentFileList = Directory.GetFiles(MainDirectories[d]).ToList();

                for (int f = 0; f < CurrentFileList.Count; f++)
                {
                    if (!CurrentFileList[f].ToLower().EndsWith("png"))
                    {
                        CurrentFileList.RemoveAt(f);
                        f--;
                    }
                }
                
                CurrentDirectory = MainDirectories[d].Replace("Images\\", "").Replace("\\", "-");

                if (!SheetDictionary.ContainsKey(CurrentDirectory))
                    SheetDictionary.Add(CurrentDirectory, new SheetInfo());
            }

            string fileName = CurrentFileList[FileCounter].Remove(CurrentFileList[FileCounter].Length - 4);
            fileName = fileName.Remove(0, fileName.LastIndexOf("\\") + 1);

            if (!SheetDictionary[CurrentDirectory].Textures.ContainsKey(fileName))
            {
                AddTexture(CurrentDirectory, fileName, CurrentFileList[FileCounter], null);
                SheetDictionary[CurrentDirectory].Elements++;
            }

            if (SheetDictionary[CurrentDirectory].Textures[fileName].LastWriteTime != File.GetLastWriteTime(CurrentFileList[FileCounter]))
            {
                Texture2D previousTexture = null;

                if (SheetDictionary[CurrentDirectory].Textures[fileName].Texture != null)
                    previousTexture = SheetDictionary[CurrentDirectory].Textures[fileName].Texture;

                SheetDictionary[CurrentDirectory].Textures.Remove(fileName);
                AddTexture(CurrentDirectory, fileName, CurrentFileList[FileCounter], previousTexture);
            }

            FileCounter++;

            if (FileCounter >= CurrentFileList.Count)
            {
                FileCounter = 0;
                return true;
            }
            else return false;
        }

        static public bool TextureMissing(string directory, string fileName)
        {
            if (SheetDictionary.ContainsKey(directory))
                if (SheetDictionary[directory].Textures.ContainsKey(fileName))
                    return false;

            return true;
        }

        static private void AddTexture(string directory, string fileName, string filePath, Texture2D previousTexture)
        {
            SheetDictionary[directory].Textures.Add(fileName, new TextureInfo(textureLoader.FromFile(filePath), File.GetLastWriteTime(filePath), DateTime.Now, previousTexture));
        }

        static public TimeSpan GetTextureTime(string directory, string fileName)
        {
            if (SheetDictionary.ContainsKey(directory))
                if (SheetDictionary[directory].Textures.ContainsKey(fileName))
                    return DateTime.Now - SheetDictionary[directory].Textures[fileName].LoadTime;

            return TimeSpan.FromSeconds(1);
        }

        static public Texture2D GetRenderTexture(string directory, string fileName)
        {
            if (DebugOptions.DrawNormals)
            {
                fileName += "_Normal";

                if (SheetDictionary.ContainsKey(directory))
                    if (SheetDictionary[directory].Textures.ContainsKey(fileName))
                        return SheetDictionary[directory].Textures[fileName].Texture;

                fileName = fileName.Replace("_Normal", "");
            }
            

            if (SheetDictionary.ContainsKey(directory))
                if (SheetDictionary[directory].Textures.ContainsKey(fileName))
                    return SheetDictionary[directory].Textures[fileName].Texture;

            return SheetDictionary["Core"].Textures["MissingTexture"].Texture;
        }

        static public Texture2D GetPreviousTexture(string directory, string fileName)
        {
            if (SheetDictionary.ContainsKey(directory))
                if (SheetDictionary[directory].Textures.ContainsKey(fileName))
                    if (SheetDictionary[directory].Textures[fileName].PreviousTexture != null)
                        return SheetDictionary[directory].Textures[fileName].PreviousTexture;

            return SheetDictionary["Core"].Textures["MissingTexture"].Texture;
        }

        static public SkeletonTexture GetRandomTexture(string directory, Random rand)
        {
            SkeletonTexture Tex = new SkeletonTexture(directory, SheetDictionary[directory].Textures.ElementAt(rand.Next(0, SheetDictionary[directory].Elements)).Key);
            return Tex;
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
