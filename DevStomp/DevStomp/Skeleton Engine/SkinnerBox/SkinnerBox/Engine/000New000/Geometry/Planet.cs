using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    [Serializable()]
    class TerrainParticle
    {
        public SpacePosition PreviousPosition = new SpacePosition();
        public SpacePosition Position = new SpacePosition();
        public SpacePosition Velocity = new SpacePosition();
        public SpacePosition Acceleration = new SpacePosition();

        public Color ID;
        public float Timer = 0f;

        public TerrainParticle()
        {
        }

        public TerrainParticle(SpacePosition position, Color color, float timer)
        {
            Position = position;
            ID = color;
            Timer = timer;
        }

        public void Update()
        {
            PreviousPosition = Position;

            Velocity += Acceleration * GlobalVariables.PhysicsTime;
            Position += Velocity * GlobalVariables.PhysicsTime;

            Acceleration = new SpacePosition();
        }

        public void Draw(Camera camera)
        {
            SkeletonTexture test = new SkeletonTexture("Core", "Marker");
            GlobalVariables.TempSP = SpacePosition.CameraTransform(camera.Position, Position);

            if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.B))
                test.Draw(GlobalVariables.TempSP.RoughPosition, ID, 0f, new Vector2(30000f), SpriteEffects.None, 0f);
            else
                test.Draw(GlobalVariables.TempSP.RoughPosition, ID, 0f, new Vector2(30f), SpriteEffects.None, 0f);
        }

    }

    [Serializable()]
    class Planet
    {
        [Serializable()]
        class PixelLocation
        {
            public int ChunkX;
            public int ChunkY;
            public int PixelX;
            public int PixelY;

            public int PixelID;

            public PixelLocation()
            {
            }

            public PixelLocation(PixelLocation pixel)
            {
                ChunkX = pixel.ChunkX;
                ChunkY = pixel.ChunkY;
                PixelX = pixel.PixelX;
                PixelY = pixel.PixelY;

                PixelID = pixel.PixelID;
            }

            public PixelLocation(int cX, int cY, int pX, int pY)
            {
                ChunkX = cX;
                ChunkY = cY;
                PixelX = pX;
                PixelY = pY;

                PixelID = UsefulMethods.Return1DPosition(pX, pY, GlobalVariables.ChunkSize);
            }

            public void SetInBounds()
            {
                bool change = false;

                while (PixelX < 0)
                {
                    PixelX += GlobalVariables.ChunkSize;
                    ChunkX--;
                    change = true;
                }
                while (PixelX >= GlobalVariables.ChunkSize)
                {
                    PixelX -= GlobalVariables.ChunkSize;
                    ChunkX++;
                    change = true;
                }

                while (PixelY < 0)
                {
                    PixelY += GlobalVariables.ChunkSize;
                    ChunkY--;
                    change = true;
                }
                while (PixelY >= GlobalVariables.ChunkSize)
                {
                    PixelY -= GlobalVariables.ChunkSize;
                    ChunkY++;
                    change = true;
                }

                if (change)
                    UpdateID();
            }

            public void UpdateLocation(int x, int y)
            {
                PixelX += x;
                PixelY += y;

                SetInBounds();
            }

            private void UpdateID()
            {
                PixelID = UsefulMethods.Return1DPosition(PixelX, PixelY, GlobalVariables.ChunkSize);
            }
        }

        [Serializable()]
        public class ChunkManager
        {
            public Location PlanetSize;

            public Dictionary<Vector2, Terrain> ActiveChunks = new Dictionary<Vector2, Terrain>();
            public List<int2D> LoadedChunks = new List<int2D>();

            public List<Vector2> RemoveChunks = new List<Vector2>();
            public List<Vector2> AddChunks = new List<Vector2>();

            public string Name;
            public string PlanetPath;

            public ChunkManager(string name)
            {
                Name = name;
                PlanetPath = SaveFile.SaveData + "Planets/" + Name;
                
                PlanetSize = new Location();
            }

            public bool ChunkLoaded(int x, int y)
            {
                if (ActiveChunks.ContainsKey(new Vector2(x, y)))
                    return true;
                else return false;
            }

            public bool ChunkSaved(int x, int y)
            {
                if (File.Exists(PlanetPath + "/" + x + "-" + y + ".png"))
                    return true;
                else return false;
            }

            public Terrain GetChunk(int x, int y)
            {
                if (x > PlanetSize.Right + 1)
                    return null;
                else if (x < PlanetSize.Left - 1)
                    return null;

                if (y > PlanetSize.Bottom + 1)
                    return null;
                else if (y < PlanetSize.Top - 1)
                    return null;


                if (!ChunkLoaded(x, y))
                {
                    AddChunks.Add(new Vector2(x, y));
                    return null;





                    //if (ChunkSaved(x, y))
                    //{
                    //    if (!LoadChunk(x, y))
                    //        return null;
                    //}
                    //else
                    //    CreateChunk(x, y);
                }
                
                Terrain chunk = ActiveChunks[new Vector2(x, y)];
                
                if (chunk.PixelCount > 0)
                    ExpandBounds(x, y);

                chunk.TimerStart = false;
                chunk.LoadTimer = 0f;

                return chunk;
            }

            private void ExpandBounds(int x, int y)
            {
                if (x > PlanetSize.Right)
                    PlanetSize.Right = x;
                else if (x < PlanetSize.Left)
                    PlanetSize.Left = x;

                if (y > PlanetSize.Bottom)
                    PlanetSize.Bottom = y;
                else if (y < PlanetSize.Top)
                    PlanetSize.Top = y;
            }

            public void CreateChunk(int x, int y)
            {
                Terrain chunk = new Terrain(x, y, null);
                ActiveChunks.Add(new Vector2(x, y), chunk);

                float radius = TestingClass.TestPublicRadius;

                //Temporary terrain generation.
                TerrainBrush.Data.Add(new TerrainBrush.BrushData(
                    x, y, new Vector2((GlobalVariables.ChunkSize / 2f) + (GlobalVariables.ChunkSize * -x),
                    (GlobalVariables.ChunkSize / 2f) + (GlobalVariables.ChunkSize * -y)), 500, GlobalVariables.ChunkSize * radius, Color.LightBlue));

                ExpandBounds((int)radius, (int)radius);
                ExpandBounds((int)-radius, (int)-radius);


                chunk.RenderPixels();
                //Temporary terrain generation.
            }

            private bool LoadChunk(int x, int y)
            {
                Texture2D tex = TextureLoader.FromFile(PlanetPath + "/" + x + "-" + y + ".png");

                if (tex == null)
                    return false;

                Terrain chunk = new Terrain(x, y, tex);

                ActiveChunks.Add(new Vector2(x, y), chunk);
                LoadedChunks.Add(new int2D(x, y));

                return true;
            }

            private bool SaveChunk(int x, int y)
            {
                Terrain chunk = ActiveChunks[new Vector2(x, y)];

                if (!Directory.Exists(SaveFile.SaveData + "Planets/" + Name))
                    Directory.CreateDirectory(SaveFile.SaveData + "Planets/" + Name);

                try
                {
                    TextureLoader.SaveAsPNG(chunk.Texture, SaveFile.SaveData + "Planets/" + Name + "/" + chunk.Coordinates.X + "-" + chunk.Coordinates.Y + ".png");

                    return true;
                }
                catch (IOException e)
                {
                    GlobalVariables.ErrorLog.Add(e.Message);
                    return false;
                }
            }

            public void ClearPlanet()
            {
                ActiveChunks.Clear();
                LoadedChunks.Clear();
                AddChunks.Clear();
                RemoveChunks.Clear();
                PlanetSize = new Location();

                if (Directory.Exists(SaveFile.SaveData + "Planets/" + Name))
                    Directory.Delete(SaveFile.SaveData + "Planets/" + Name, true);
            }

            public void Update()
            {
                ManageLoadedArea();
                ManageUnLoadedArea();

                LoadedChunks.Clear();
            }

            private void ManageLoadedArea()
            {
                LoadedChunks = LoadedChunks.Distinct().ToList();

                if (LoadedChunks.Count != 0)
                {
                    int x;
                    int y;

                    for (int i = 0; i < LoadedChunks.Count; i++)
                    {
                        x = LoadedChunks[i].X;
                        y = LoadedChunks[i].Y;

                        Terrain chunk = GetChunk(x, y);
                    }
                }
            }

            private void ManageUnLoadedArea()
            {
                foreach (KeyValuePair<Vector2, Terrain> chunk in ActiveChunks)
                {
                    if (chunk.Value.PixelCount == 0)
                        RemoveChunks.Add(chunk.Key);
                    else
                    {
                        int2D key = new int2D(chunk.Key);
                        bool Unload = true;

                        for (int i = 0; i < LoadedChunks.Count; i++)
                            if (LoadedChunks[i].X == key.X && LoadedChunks[i].Y == key.Y)
                                Unload = false;

                        if (Unload)
                        {
                            chunk.Value.TimerStart = true;

                            if (chunk.Value.LoadTimer > 1f)
                                    RemoveChunks.Add(chunk.Key);
                                
                        }
                    }
                }
            }

            public void ManageDiskData()
            {
                RemoveChunks = RemoveChunks.Distinct().ToList();
                AddChunks = AddChunks.Distinct().ToList();

                int x;
                int y;
                int ID;

                if (RemoveChunks.Count != 0)
                {
                    ID = 0;

                    x = (int)RemoveChunks[ID].X;
                    y = (int)RemoveChunks[ID].Y;

                    if (SaveChunk(x, y))
                    {
                        ActiveChunks[RemoveChunks[ID]].Dispose();
                        ActiveChunks[RemoveChunks[ID]] = null;
                        ActiveChunks.Remove(RemoveChunks[ID]);
                        RemoveChunks.RemoveAt(ID);
                    }
                }

                if (AddChunks.Count != 0)
                {
                    ID = AddChunks.Count - 1;

                    x = (int)AddChunks[ID].X;
                    y = (int)AddChunks[ID].Y;

                    if (!ChunkLoaded(x, y))
                    {
                        if (ChunkSaved(x, y))
                        {
                            LoadChunk(x, y);
                        }
                        else
                            CreateChunk(x, y);
                    }

                    AddChunks.RemoveAt(ID);
                }
            }

        }

        public SpacePosition Position = new SpacePosition();
        public float Rotation;
        public SpacePosition Acceleration = new SpacePosition();
        public SpacePosition Velocity = new SpacePosition();



        List<TerrainParticle> Particles = new List<TerrainParticle>();

        public ChunkManager Chunks;



        SpacePosition HighlightedChunk = new SpacePosition();
        SpacePosition HighlightedPixel = new SpacePosition();

        public float RotationalVelocity = MathHelper.Pi / 4f;

        /// <summary>
        /// 1 == Exact accuracy, higher numbers skip that many pixel checks.
        /// </summary>
        float ParticleAccuracy = 20f;



        public Planet()
        {
            Chunks = new ChunkManager("Test");
        }

        public void LoadArea(SpacePosition location, int width, int height)
        {
            PixelLocation chunkOrigin = GetPixel(location);
            width /= GlobalVariables.ChunkSize;
            height /= GlobalVariables.ChunkSize;

            for (int x = chunkOrigin.ChunkX - width; x <= chunkOrigin.ChunkX + width; x++)
                for (int y = chunkOrigin.ChunkY - height; y <= chunkOrigin.ChunkY + height; y++)
                    Chunks.LoadedChunks.Add(new int2D(x, y));
        }


        #region Planet Properties
        
        private float RocheLimit()
        {
            float x = (Chunks.PlanetSize.Right - Chunks.PlanetSize.Left) * GlobalVariables.ChunkSize;
            float y = (Chunks.PlanetSize.Bottom - Chunks.PlanetSize.Top) * GlobalVariables.ChunkSize;


            float radius = (x / 2f) / 100f;
            float area = (x * y) / 100f;
            float mass = (x * y) / 1000f;

            return (72f * radius * (float)Math.Pow(mass / area, 1f / 3f)) * 100f;
        }

        private float AirDrag(SpacePosition velocity, float diameter)
        {
            float vel = velocity.Length() / 100f;
            float coefficient = 0.25f;
            float density = 1.2f;

            float final = 0.5f * density * (vel * vel) * coefficient * diameter;

            return final;
        }
        
        public SpacePosition RotateWithPlanet(SpacePosition position)
        {
            float radians = RotationalVelocity * GlobalVariables.PhysicsTime;

            position = UsefulMethods.RotatePoint(position, Position, radians);
            position += Velocity * GlobalVariables.PhysicsTime;

            return position;
        }

        #endregion

        #region Brush Drawing

        private void AddBrushPoint(PixelLocation pixel, int points, float brushSize, Color color)
        {
            float canvasSize = GlobalVariables.ChunkSize;

            Location collide = new Location(
                pixel.PixelX - brushSize, pixel.PixelY - brushSize, pixel.PixelX + brushSize, pixel.PixelY + brushSize);

            AddBrushPoint(pixel.ChunkX, pixel.ChunkY, pixel.PixelX, pixel.PixelY, points, brushSize, color);

            if (collide.Left < 0)
            {
                AddBrushPoint(pixel.ChunkX - 1, pixel.ChunkY, pixel.PixelX + (int)canvasSize, pixel.PixelY, points, brushSize, color);

                if (collide.Top < 0)
                    AddBrushPoint(pixel.ChunkX - 1, pixel.ChunkY - 1, pixel.PixelX + (int)canvasSize, pixel.PixelY + (int)canvasSize, points, brushSize, color);

                if (collide.Bottom >= canvasSize)
                    AddBrushPoint(pixel.ChunkX - 1, pixel.ChunkY + 1, pixel.PixelX + (int)canvasSize, pixel.PixelY - (int)canvasSize, points, brushSize, color);
            }
            if (collide.Top < 0)
                AddBrushPoint(pixel.ChunkX, pixel.ChunkY - 1, pixel.PixelX, pixel.PixelY + (int)canvasSize, points, brushSize, color);

            if (collide.Right >= canvasSize)
            {
                AddBrushPoint(pixel.ChunkX + 1, pixel.ChunkY, pixel.PixelX - (int)canvasSize, pixel.PixelY, points, brushSize, color);

                if (collide.Top < 0)
                    AddBrushPoint(pixel.ChunkX + 1, pixel.ChunkY - 1, pixel.PixelX - (int)canvasSize, pixel.PixelY + (int)canvasSize, points, brushSize, color);

                if (collide.Bottom >= canvasSize)
                    AddBrushPoint(pixel.ChunkX + 1, pixel.ChunkY + 1, pixel.PixelX - (int)canvasSize, pixel.PixelY - (int)canvasSize, points, brushSize, color);

            }
            if (collide.Bottom >= canvasSize)
                AddBrushPoint(pixel.ChunkX, pixel.ChunkY + 1, pixel.PixelX, pixel.PixelY - (int)canvasSize, points, brushSize, color);
        }

        private void AddBrushPoint(int ChunkX, int ChunkY, int PixelX, int PixelY, int points, float brushSize, Color color)
        {
            Terrain chunk = Chunks.GetChunk(ChunkX, ChunkY);
            if (chunk != null)
                TerrainBrush.Data.Add(new TerrainBrush.BrushData(ChunkX, ChunkY, new Vector2(PixelX, PixelY), points, brushSize, color));
        }

        #endregion 
        
        #region Pixel Searching

        /// <summary>
        /// Gets the pixel position from a Vector2.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns></returns>
        private PixelLocation GetPixel(SpacePosition position)
        {

            position = UsefulMethods.RotatePoint(position, Position, -Rotation);

            position += new Vector2(GlobalVariables.ChunkSize / 2f);
            position -= Position;

            int ChunkX = (int)Math.Floor((position.Position.X / GlobalVariables.ChunkSize));
            int ChunkY = (int)Math.Floor((position.Position.Y / GlobalVariables.ChunkSize));

            position -= new Vector2(ChunkX * GlobalVariables.ChunkSize, ChunkY * GlobalVariables.ChunkSize);

            ChunkX += (int)((GlobalVariables.CameraChunkSize / GlobalVariables.ChunkSize) * (float)position.ChunkX);
            ChunkY += (int)((GlobalVariables.CameraChunkSize / GlobalVariables.ChunkSize) * (float)position.ChunkY);

            return new PixelLocation(ChunkX, ChunkY, (int)position.Position.X, (int)position.Position.Y);
        }

        private SpacePosition GetPosition(PixelLocation pixel)
        {
            SpacePosition position = (new SpacePosition(new Vector2(pixel.ChunkX * GlobalVariables.ChunkSize, pixel.ChunkY * GlobalVariables.ChunkSize)) +
                new Vector2(pixel.PixelX, pixel.PixelY)) + Position;

            position -= new Vector2(GlobalVariables.ChunkSize / 2f);

            position = UsefulMethods.RotatePoint(position, Position, Rotation);
            
            return position;
        }

        ///// <summary>
        ///// Gets the pixel position from a Vector2.
        ///// </summary>
        ///// <param name="position">The position to check.</param>
        ///// <returns></returns>
        //private PixelLocation GetPixel(SpacePosition position)
        //{
        //    position = UsefulMethods.RotatePoint(position, Position, -Rotation);

        //    position += new Vector2(GlobalVariables.ChunkSize / 2f);
        //    position -= Position;

        //    //int ChunkX = (int)Math.Floor((position.Position.X / GlobalVariables.ChunkSize) + ((float)position.ChunkX * 2000000f));
        //    //int ChunkY = (int)Math.Floor((position.Position.Y / GlobalVariables.ChunkSize) + ((float)position.ChunkY * 2000000f));
        //    int ChunkX = (int)Math.Floor((position.Position.X / GlobalVariables.ChunkSize));
        //    int ChunkY = (int)Math.Floor((position.Position.Y / GlobalVariables.ChunkSize));

        //    position -= new Vector2(ChunkX * GlobalVariables.ChunkSize, ChunkY * GlobalVariables.ChunkSize);

        //    return new PixelLocation(ChunkX, ChunkY, (int)position.Position.X, (int)position.Position.Y);
        //}

        //private SpacePosition GetPosition(PixelLocation pixel)
        //{
        //    SpacePosition position = (new Vector2(pixel.ChunkX * GlobalVariables.ChunkSize, pixel.ChunkY * GlobalVariables.ChunkSize) +
        //        new Vector2(pixel.PixelX, pixel.PixelY)) + Position;

        //    position -= new Vector2(GlobalVariables.ChunkSize / 2f);

        //    position = UsefulMethods.RotatePoint(position, Position, Rotation);

        //    return position;
        //}
        #endregion

        #region Line Searching

        /// <summary>
        /// Check the pixels between two points for a solid pixel.
        /// </summary>
        /// <param name="startX">The first vectors X coordinate.</param>
        /// <param name="startY">The first vectors Y coordinate.</param>
        /// <param name="lastX">The second vectors X coordinate.</param>
        /// <param name="lastY">The second vectors Y coordinate.</param>
        /// <param name="accuracy">The accuracy of the check.</param>
        /// <returns></returns>
        private PixelLocation OptimizedLine(int startX, int startY, int lastX, int lastY, float accuracy)
        {
            int deltax = (int)Math.Abs(lastX - startX);
            int deltay = (int)Math.Abs(lastY - startY);
            int x = (int)startX;
            int y = (int)startY;
            int xinc1, xinc2, yinc1, yinc2;
            // Determine whether x and y is increasing or decreasing 
            if (lastX >= startX)
            { // The x-values are increasing     
                xinc1 = 1;
                xinc2 = 1;
            }
            else
            { // The x-values are decreasing
                xinc1 = -1;
                xinc2 = -1;
            }
            if (lastY >= startY)
            { // The y-values are increasing
                yinc1 = 1;
                yinc2 = 1;
            }
            else
            { // The y-values are decreasing
                yinc1 = -1;
                yinc2 = -1;
            }
            int den, num, numadd, numpixels;
            if (deltax >= deltay)
            { // There is at least one x-value for every y-value
                xinc1 = 0; // Don't change the x when numerator >= denominator
                yinc2 = 0; // Don't change the y for every iteration
                den = deltax;
                num = deltax / 2;
                numadd = deltay;
                numpixels = deltax; // There are more x-values than y-values
            }
            else
            { // There is at least one y-value for every x-value
                xinc2 = 0; // Don't change the x for every iteration
                yinc1 = 0; // Don't change the y when numerator >= denominator
                den = deltay;
                num = deltay / 2;
                numadd = deltax;
                numpixels = deltay; // There are more y-values than x-values
            }
            int prevX = (int)startX;
            int prevY = (int)startY;

            PixelLocation pixel;

            int count = 0;

            for (int curpixel = 0; curpixel <= numpixels; curpixel++)
            {
                if (count > numpixels / accuracy || curpixel == numpixels)
                {
                    pixel = GetPixel(new SpacePosition(x, y));

                    Terrain chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);

                    if (chunk != null)
                        if (chunk.Data[pixel.PixelID] == Color.Transparent)
                            return pixel;

                    count = 0;
                }

                count++;

                prevX = x;
                prevY = y;

                num += numadd; // Increase the numerator by the top of the fraction

                if (num >= den)
                {  // Check if numerator >= denominator
                    num -= den; // Calculate the new numerator value
                    x += xinc1; // Change the x as appropriate
                    y += yinc1; // Change the y as appropriate
                }

                x += xinc2; // Change the x as appropriate
                y += yinc2; // Change the y as appropriate
            }
            return null; // nothing was found
        }
        
        /// <summary>
        /// Searches for empty pixels in a cross shape.
        /// </summary>
        /// <param name="x">The X coordinate to search.</param>
        /// <param name="y">The Y coordinate to search.</param>
        /// <returns></returns>
        private PixelLocation CrossSearch(float x, float y, BigInteger spaceChunkX, BigInteger spaceChunkY)
        {
            //////////////////////////////////////////////////////////////////////////////////////
            //Make search start at normal from velocity? Kinda unnecessary, as it checks at the pixel level. Probably won't notice.
            //Use the searching algorithm in USefulMethods.Intervals instead of using 8 seperate pieces of code.

            PixelLocation pixel = new PixelLocation();

            bool searching = true;
            int radius = 1;
            Terrain chunk;

            while (searching)
            {
                pixel = GetPixel(new SpacePosition(x, y - radius, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x + radius, y - radius, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x + radius, y, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x + radius, y + radius, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x, y + radius, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x - radius, y + radius, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x - radius, y, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                pixel = GetPixel(new SpacePosition(x - radius, y - radius, spaceChunkX, spaceChunkY));
                chunk = Chunks.GetChunk(pixel.ChunkX, pixel.ChunkY);
                if (chunk != null && pixel.PixelID != -1)
                    if (chunk.Data[pixel.PixelID] == Color.Transparent)
                    {
                        searching = false;
                        break;
                    }

                radius++;

                if (radius > 10000)
                    return null;
            }

            return pixel;
        }

        #endregion

        private void Temp()
        {
            //Rotation = 0f;
            RotationalVelocity = MathHelper.Pi / 8f;
            //RotationalVelocity = 0f;
            Velocity = new SpacePosition(100f, 0f);
            //Velocity = new SpacePosition(0f, 0f);


            if (InputManager.KBPressed(true, Microsoft.Xna.Framework.Input.Keys.C))
                Particles.Clear();

            if (InputManager.KBPressed(true, Microsoft.Xna.Framework.Input.Keys.Back))
                Chunks.ClearPlanet();

            if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.A))
                LoadArea(new SpacePosition(InputManager.GetMousePosition(CameraManager.MainCameras[0])), 512 * 3, 512 * 3);


            LoadArea(Position, 2500, 2500);
            LoadArea(CameraManager.MainCameras[0].Position, 1024, 1024);



            ////////////////////////
            // Temp create particles.
            if (InputManager.MBPressed(false, MouseButton.Left))
            {
                for (int i = 0; i < 100; i++)
                {
                    Particles.Add(new TerrainParticle());

                    Particles[Particles.Count - 1].Velocity = new SpacePosition(InputManager.MouseVelocity * 1000f * GlobalVariables.WorldTime);

                    Particles[Particles.Count - 1].Position = new SpacePosition(InputManager.GetMousePosition(CameraManager.MainCameras[0]));

                    Particles[Particles.Count - 1].ID = Color.Green;

                    Vector2 test = new Vector2(GlobalVariables.RandomNumber.Next(0, 500) - 250, GlobalVariables.RandomNumber.Next(0, 500) - 250);

                    //Particles[Particles.Count - 1].Position += test;
                    Particles[Particles.Count - 1].Velocity += test;
                }
            }
            ///////////////////////////
            // Temp create brush
            if (InputManager.MBPressed(false, MouseButton.Middle))
            {
                Vector2 mousePrevious = InputManager.GetPreviousMousePosition(CameraManager.MainCameras[0]).RoughPosition;
                Vector2 mouseCurrent = InputManager.GetMousePosition(CameraManager.MainCameras[0]).RoughPosition;

                List<int2D> brushPoints = UsefulMethods.PlotLine((int)mousePrevious.X, (int)mousePrevious.Y, (int)mouseCurrent.X, (int)mouseCurrent.Y);

                PixelLocation pixel;

                for (int i = 0; i < brushPoints.Count; i++)
                {
                    pixel = GetPixel(brushPoints[i].ToSpacePosition());
                    AddBrushPoint(pixel, 10, 25f, Color.White);
                }
            }


        }

        public void Update()
        {
            //////////////////
            Temp();
            //////////////////////////////


            UpdatePlanetMovement();
            UpdateParticlePhysics();


            //CenterOfMass = Position;





            Chunks.Update();
            foreach (Terrain chunk in Chunks.ActiveChunks.Values)
            {
                chunk.Update();
                chunk.RenderPixels();
            }
        }

        private void UpdatePlanetMovement()
        {
            for (int i = 0; i < GlobalVariables.PhysicsSteps; i++)
            {
                Rotation += RotationalVelocity * GlobalVariables.PhysicsTime;
                if (Rotation > MathHelper.Pi * 2f)
                    Rotation -= MathHelper.Pi * 2f;

                Velocity += Acceleration * GlobalVariables.PhysicsTime;
                Position += Velocity * GlobalVariables.PhysicsTime;
            }
        }

        private void UpdateParticlePhysics()
        {
            //if(InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.P))
            for (int p = 0; p < GlobalVariables.PhysicsSteps; p++)
            {
                for (int i = 0; i < Particles.Count; i++)
                {
                    Particles[i].Position = RotateWithPlanet(Particles[i].Position);
                    Particles[i].Velocity = UsefulMethods.RotatePoint(Particles[i].Velocity, new SpacePosition(), RotationalVelocity * GlobalVariables.PhysicsTime);

                    SpacePosition target = Position;
                    Vector2 normal = (Particles[i].Position - target).RoughPosition;

                    normal.Normalize();

                    Particles[i].Acceleration = new SpacePosition(-normal * TestingVariables.EarthGravity);




                    float vel = (Particles[i].Velocity + Velocity).Length();

                    float drag = 1f - (vel / (GlobalVariables.SpeedOfLight * 100f));
                    if (drag < 0)
                        drag = 0;

                    Particles[i].Acceleration *= drag;



                    float testDrag1 = AirDrag(Particles[i].Velocity, 0.01f);
                    //float testDrag2 = AirDrag(Particles[i].Velocity, 1f);
                    //float testDrag3 = AirDrag(Particles[i].Velocity, 100f);

                    normal = Particles[i].Velocity.RoughPosition;
                    if (normal != Vector2.Zero)
                        normal.Normalize();

                    Particles[i].Acceleration += -normal * testDrag1;




                    Particles[i].Update();

                    //Particles[i].Velocity *= 0.999f;
                }
                //////////////////////////////////////

                ParticleCollision();
                UpdateActiveParticles();
            }

        }

        /// <summary>
        /// Checks if particles are colliding with the terrain.
        /// </summary>
        private void ParticleCollision()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                PixelLocation position = GetPixel(Particles[i].Position);
                Terrain chunk1 = Chunks.GetChunk(position.ChunkX, position.ChunkY);

                if (chunk1 != null)
                    if (chunk1.Data[position.PixelID] != Color.Transparent)
                    {
                        PixelLocation finalPixel = CrossSearch(Particles[i].Position.Position.X, Particles[i].Position.Position.Y,
                            Particles[i].Position.ChunkX, Particles[i].Position.ChunkY);

                        if (finalPixel != null)
                        {
                            Terrain chunk = Chunks.GetChunk(finalPixel.ChunkX, finalPixel.ChunkY);

                            if (chunk != null)
                            {
                                chunk.Data[finalPixel.PixelID] = Particles[i].ID;
                                Particles[i].Position = new SpacePosition(finalPixel.PixelX, finalPixel.PixelY);

                                chunk.Particles.Add(Particles[i]);

                                Particles.RemoveAt(i);
                                i--;
                            }
                        }
                    }
            }
        }

        private void UpdateActiveParticles()
        {
            //foreach (Terrain chunk in Chunks.ActiveChunks.Values)
            List<Terrain> chunks = Chunks.ActiveChunks.Values.ToList();
            Terrain chunk;

            for (int c = 0; c < chunks.Count; c++)
            {
                chunk = chunks[c];

                if (chunk.ActiveParticles.Count != 0)
                {
                    Vector2 centerOfMass = (new Vector2(GlobalVariables.ChunkSize) * chunk.Coordinates.ToVector2()) + new Vector2(GlobalVariables.ChunkSize / 2f);

                    //Falling.
                    for (int i = 0; i < chunk.ActiveParticles.Count; i++)
                    {
                        //Increment timer.
                        chunk.ActiveParticles[i].Timer += GlobalVariables.WorldTime;

                        //Stop old particles.
                        if (chunk.ActiveParticles[i].Timer > 3f)
                        {
                            chunk.ActiveParticles.RemoveAt(i);
                            i--;
                            //breakInterval = true;
                            continue;
                        }

                        Vector2 pos = new Vector2(chunk.ActiveParticles[i].X, chunk.ActiveParticles[i].Y);



                        Vector2 normal = (GetPosition(new PixelLocation(chunk.Coordinates.X, chunk.Coordinates.Y, (int)pos.X, (int)pos.Y)) - Position).RoughPosition;
                        normal.Normalize();
                        normal = UsefulMethods.RotatePoint(normal, Vector2.Zero, -Rotation);

                        Vector2 down = pos - normal;

                        //PixelLocation down1 = new PixelLocation(chunk.Coordinates.X, chunk.Coordinates.Y, (int)Math.Round(down.X), (int)Math.Round(down.Y));
                        PixelLocation down1 = new PixelLocation(chunk.Coordinates.X, chunk.Coordinates.Y, (int)Math.Round(down.X), (int)Math.Round(down.Y));
                        Terrain selectedChunk = chunk;

                        if (down1.PixelID < 0 || down1.PixelID >= chunk.Data.Length)
                        {
                            down1.SetInBounds();
                            selectedChunk = Chunks.GetChunk(down1.ChunkX, down1.ChunkY);
                        }

                        if (selectedChunk != null && selectedChunk.Data[down1.PixelID] == Color.Transparent)
                        {
                            Vector2 down2Vec = down - normal;

                            PixelLocation down2 = new PixelLocation(selectedChunk.Coordinates.X, selectedChunk.Coordinates.Y, (int)Math.Round(down2Vec.X), (int)Math.Round(down2Vec.Y));
                            if (down2.PixelID < 0 || down2.PixelID >= chunk.Data.Length)
                                down2.SetInBounds();

                            Terrain downChunk = Chunks.GetChunk(down2.ChunkX, down2.ChunkY);
                            
                            if (downChunk != null && downChunk.Data[down2.PixelID] == Color.Transparent)
                            {
                                Particles.Add(new TerrainParticle(GetPosition(new PixelLocation(down2.ChunkX,
                                    down2.ChunkY, down2.PixelX, down2.PixelY)), chunk.ActiveParticles[i].ID, 0f));

                                chunk.Particles.Add(new TerrainParticle(new SpacePosition(pos), Color.Transparent, 0f));
                                chunk.Data[UsefulMethods.Return1DPosition(chunk.ActiveParticles[i].X,
                                    chunk.ActiveParticles[i].Y, GlobalVariables.ChunkSize)] = Color.Transparent;

                                chunk.ActiveParticles.RemoveAt(i);
                                i--;
                                continue;
                            }
                            else
                            {
                                selectedChunk.Particles.Add(new TerrainParticle(new SpacePosition(new Vector2(down1.PixelX, down1.PixelY)), chunk.ActiveParticles[i].ID, 0f));
                                selectedChunk.Data[down1.PixelID] = chunk.ActiveParticles[i].ID;
                                chunk.Particles.Add(new TerrainParticle(new SpacePosition(pos), Color.Transparent, 0f));
                                chunk.Data[UsefulMethods.Return1DPosition(chunk.ActiveParticles[i].X, chunk.ActiveParticles[i].Y, GlobalVariables.ChunkSize)] = Color.Transparent;

                                chunk.ActiveParticles.RemoveAt(i);
                                i--;
                                continue;
                            }

                        }
                    }

                    chunk.RenderPixels();
                }
            }
        }
        


        public void Draw(Camera camera)
        {
            DebugOptions.Geometry.AddData(Position, 0f, 100, new Vector2((-Chunks.PlanetSize.Top + Chunks.PlanetSize.Bottom) * GlobalVariables.ChunkSize), Color.HotPink);
            DebugOptions.Geometry.AddData(Position, 0f, 100, new Vector2(RocheLimit()), Color.Red);
            DebugOptions.Geometry.AddData(Position, 0f, 100, new Vector2(RocheLimit()) / 5.5f, Color.Yellow);
            DebugOptions.Geometry.AddData(Position, 0f, 100, new Vector2(RocheLimit()) * 5f, Color.LightGreen);
 
            foreach (Terrain chunk in Chunks.ActiveChunks.Values)
                chunk.Draw(camera, Position, Rotation);
            
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Draw(camera);
            }

            TestDraw(camera);


            if (InputManager.KBPressed(true, Microsoft.Xna.Framework.Input.Keys.R))
                Rotation = 0f;
        }

        private void TestDraw(Camera camera)
        {           

            SkeletonTexture test = new SkeletonTexture("Core", "Marker");
            test.Draw(Position.RoughPosition, Color.White, Rotation, 50f, SpriteEffects.None);

            //test.Draw(Position.RoughPosition, Color.Red * 0.5f, 0f, RocheLimit(), SpriteEffects.None);
            //test.Draw(Position.RoughPosition, Color.Blue * 0.5f, 0f, RocheLimit() / 5f, SpriteEffects.None);
            //test.Draw(Position.RoughPosition, Color.Green * 0.5f, 0f, RocheLimit() * 5f, SpriteEffects.None);
            
            SpacePosition mousePos = InputManager.GetMousePosition(camera);

            DrawSprites.DrawTranslated(camera, test.Texture, mousePos, Color.Red * 0.5f, 0f, new Vector2(0.5f), new Vector2(25f / camera.Zoom), SpriteEffects.None);
            DrawSprites.DrawTranslated(camera, test.Texture, mousePos + new SpacePosition(0f, -200f / camera.Zoom), Color.Red * 0.5f, 0f, new Vector2(0.5f), new Vector2(25f / camera.Zoom), SpriteEffects.None);
            DrawSprites.DrawTranslated(camera, test.Texture, mousePos + 
                UsefulMethods.RotatePoint(new SpacePosition(0f, -200f / camera.Zoom), new SpacePosition(), Rotation), Color.Red * 0.5f, 0f, new Vector2(0.5f), new Vector2(25f / camera.Zoom), SpriteEffects.None);






            TextBox text = new TextBox();

            text.Position = mousePos;
            text.TextBoxSize = new Vector2(GlobalVariables.CameraChunkSize / camera.Zoom);
            text.Border = null;
            text.Rotation = 0f;
            text.Scale = 2f / camera.Zoom;
            text.BackgroundColor = Color.Transparent;

            text.Update(new List<TextLine>() { new TextLine( 
                        StringManager.FontSizeString(
                        StringManager.ColorString("" + mousePos.RoughPosition, Color.White), 10))
                        { Alignment = 0f }               
            
                        ,
                        
                        new TextLine( 
                        StringManager.FontSizeString(
                        StringManager.ColorString("" + InputManager.MousePosition, Color.White), 10))
                        { Alignment = 0f }               
            
                        });

            text.TextBoxSize = new Vector2((GlobalVariables.CameraChunkSize / camera.Zoom) * 0.975f);

            text.Draw(camera);





            //PixelLocation pixel = GetPixel

            //HighlightedChunk = GetPosition(new PixelLocation(2, 1, GlobalVariables.ChunkSize / 2, GlobalVariables.ChunkSize / 2));
            //HighlightedPixel = GetPosition(GetPixel(new SpacePosition(50, 50)));

            //test.Draw(HighlightedChunk.RoughPosition, Color.Red * 0.5f, Rotation, GlobalVariables.ChunkSize, SpriteEffects.None);
            //test.Draw(HighlightedPixel.RoughPosition, Color.Red * 0.5f, Rotation, 10f, SpriteEffects.None);

            SpacePosition normal = InputManager.GetMousePosition(CameraManager.MainCameras[0]) - Position;
            float ang = UsefulMethods.VectorToAngle(normal.RoughPosition);

            Vector2 scale = new Vector2(30, 50);

            DrawSprites.DrawTranslated(camera, test.Texture, InputManager.GetMousePosition(camera), Color.Red * 0.5f, ang, Vector2.Zero, scale, SpriteEffects.None);
            //test.Draw(InputManager.GetMousePosition(CameraManager.MainCameras[0]), Color.Red * 0.5f, ang, new Vector2(25, 50), SpriteEffects.None);
        }

        public List<TextLine> Information()
        {
            List<TextLine> text = new List<TextLine>();

            text.Add(new TextLine("Left: " + Chunks.PlanetSize.Left + " Top: " + Chunks.PlanetSize.Top +
                " Right: " + Chunks.PlanetSize.Right + " Bottom: " + Chunks.PlanetSize.Bottom));

            text.Add(new TextLine("Dynamic Particles: " + Particles.Count));

            text.Add(new TextLine("Active Chunks: " + Chunks.ActiveChunks.Count));

            return text;
        }
    }
}
