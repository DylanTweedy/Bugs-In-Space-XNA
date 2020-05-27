using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    /// <summary>
    /// A Vector2 made of int's instead of float's.
    /// </summary>
    [Serializable()]    
    class int2D
    {
        /// <summary>
        /// The X Coordinate.
        /// </summary>
        public int X;
        /// <summary>
        /// The Y Coordinate.
        /// </summary>
        public int Y;

        /// <summary>
        /// Creates a new int2D.
        /// </summary>
        public int2D()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Creates a new int2D with X and Y values set.
        /// </summary>
        /// <param name="x">The X Coordinate.</param>
        /// <param name="y">The Y Coordinate.</param>
        public int2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Creates an int2D from a Vector2.
        /// </summary>
        /// <param name="v">The Vector2 to convert.</param>
        public int2D(Vector2 v)
        {
            X = (int)v.X;
            Y = (int)v.Y;
        }

        /// <summary>
        /// Creates an int2 from 2 floats.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public int2D(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public SpacePosition ToSpacePosition()
        {
            return new SpacePosition(new Vector2(X, Y));
        }

        public static bool operator ==(int2D left, int2D right)
        {
            if (left.X == right.X && left.Y == right.Y)
                return true;

            return false;
        }
        public static bool operator !=(int2D left, int2D right)
        {
            if (left.X != right.X || left.Y != right.Y)
                return true;

            return false;
        }
    }

    class ActiveParticle
    {
        public int X;
        public int Y;

        public Color ID;

        public float Timer = 0f;

        public ActiveParticle(int x, int y, Color color, float timer)
        {            
            X = x;
            Y = y;
            ID = color;
            Timer = timer;
        }
    }

    [Serializable()]      
    class Terrain : IDisposable
    {

        //SpriteBatch spriteBatch;
        //ContentManager contentManager;
        //GraphicsDevice graphicsDevice;

        public int2D Coordinates;

        public Texture2D Texture;
        int Size = GlobalVariables.ChunkSize;
        public Color[] Data;

        public int PixelCount = 0;
        
        public List<TerrainParticle> Particles = new List<TerrainParticle>();
        public List<ActiveParticle> ActiveParticles = new List<ActiveParticle>();
        
        public bool TimerStart = true;
        public float LoadTimer = 0f;
        
        TextBox test = new TextBox();

        public Terrain(int x, int y, Texture2D texture)
        {
            Coordinates = new int2D(x, y);

            LoadTexture(texture);
        }

        /// <summary>
        /// Loads a Blank texture for the Terrain chunk.
        /// </summary>
        public void LoadTexture(Texture2D texture)
        {
            UnloadTexture();

            if (texture == null)
                Texture = new Texture2D(GlobalVariables.graphicsDevice, Size, Size);
            else
                Texture = texture;

            Data = new Color[Size * Size];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = Color.Transparent;

            PixelCount = -1;
            UpdatePixelCount();
        }

        public void SetData()
        {
            Texture.SetData<Color>(Data);

            PixelCount = 0;

            for (int i = 0; i < Data.Length; i++)
                if (Data[i] != Color.Transparent)
                    PixelCount++;
        }

        /// <summary>
        /// Unloads the texture to save memory.
        /// </summary>
        private void UnloadTexture()
        {
            if (Texture != null)
            {
                Texture.Dispose();
                Texture = null;
            }
        }

        public void Dispose()
        {
            Texture.Dispose();
            Coordinates = null;
            Data = new Color[0];
            Particles.Clear();
            ActiveParticles.Clear();

            test = null;
        }

        public void SetData(int StartIndex, int ElementCount)
        {
            Texture.SetData<Color>(Data, StartIndex, ElementCount);
        }

        public void UpdatePixelCount()
        {
            Texture.GetData(Data);

            if (PixelCount == -1)
                for (int i = 0; i < Data.Length; i++)
                    if (Data[i] != Color.Transparent)
                        PixelCount++;
        }
        
        public void Update()
        {
            if (!TimerStart || ActiveParticles.Count > 0) 
                LoadTimer = 0f;
            else
                LoadTimer += GlobalVariables.FrameTime;
        }

        public void RenderPixels()
        {
            bool brushDataExists = TerrainBrush.ContainsData(Coordinates.X, Coordinates.Y);

            if (Particles.Count != 0 || brushDataExists)
            {
                SpriteBatch spriteBatch = GlobalVariables.spriteBatch;
                GraphicsDevice graphicsDevice = GlobalVariables.graphicsDevice;

                RenderTarget2D renderTarget = new RenderTarget2D(GlobalVariables.graphicsDevice, Size, Size);

                graphicsDevice.SetRenderTarget(renderTarget);
                graphicsDevice.Clear(Color.Transparent);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
                spriteBatch.Draw(Texture, Vector2.Zero, Color.White);

                if (Particles.Count != 0)
                {
                    SkeletonTexture tex = new SkeletonTexture("Core", "Marker");

                    for (int i = 0; i < Particles.Count; i++)
                    {
                        spriteBatch.Draw(tex.Texture, Particles[i].Position.Position, Particles[i].ID);

                        if (Particles[i].ID != Color.Transparent)
                        {
                            ActiveParticles.Add(new ActiveParticle((int)Particles[i].Position.Position.X, (int)Particles[i].Position.Position.Y, Particles[i].ID, Particles[i].Timer));
                            PixelCount++;
                        }
                        else 
                            PixelCount--;
                    }

                    spriteBatch.End();
                    Particles.Clear();
                }

                //////////////////////////////////////////
                //Both the brush and the particles don't work at the same time. Fix this.
                /////////////////////////////////////////
                if (brushDataExists)
                {
                    spriteBatch.End();

                    //if (Particles.Count != 0)
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);

                    TerrainBrush.Draw(Coordinates.X, Coordinates.Y, FillMode.Solid);

                    spriteBatch.End();


                    ///////////////////////////////////////
                    //Only the updated region needs its pixels counted.
                    ///////////////////////////////////////
                    PixelCount = -1;
                }

                graphicsDevice.SetRenderTarget(null);

                UnloadTexture();

                Texture = renderTarget;

                UpdatePixelCount();
            }
        }

        public void Draw(Camera camera, SpacePosition position, float rotation)
        {
            //rotation = 0f;

            SpacePosition center = position;

            if (Coordinates.X == 1 && Coordinates.Y == 1)
            {
            }

            position += new Vector2(Size * Coordinates.X, Size * Coordinates.Y);

            //position += new Vector2(Size / 2f);

            position = UsefulMethods.RotatePoint(position, center, rotation);

            if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.B))
                DrawSprites.DrawTranslated(camera, Texture, position, Color.White, rotation, new Vector2(Size / 2f), Vector2.One * 100f, SpriteEffects.None);
            else
                DrawSprites.DrawTranslated(camera, Texture, position, Color.White, rotation, new Vector2(Size / 2f), Vector2.One, SpriteEffects.None);

            //if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.LeftControl))
            if (DebugOptions.DebugActive)
                DrawInfo(camera, position, rotation);
        }

        private void DrawInfo(Camera camera, SpacePosition position, float rotation)
        {
            DebugOptions.Geometry.AddData(position, -rotation, 4, new Vector2(GlobalVariables.ChunkSize), Color.White);

            test.Position = position;
            test.TextBoxSize = new Vector2(Size);
            test.Border = null;
            test.Rotation = rotation;
            test.Scale = 1f;
            test.BackgroundColor = Color.Transparent;

            test.Update(new List<TextLine>() { new TextLine( 
                StringManager.FontSizeString(
                StringManager.ColorString("X: " + Coordinates.X + " Y: " + Coordinates.Y, Color.White), 85))
                { Alignment = 0f }
                ,
                new TextLine( 
                StringManager.FontSizeString(
                StringManager.ColorString("Pixel Count: " + PixelCount, Color.White), 50)) 
                                { Alignment = 0f }
            
            });
            test.TextBoxSize = new Vector2(Size * 0.975f);

            test.Draw(camera);
        }
    }
}
