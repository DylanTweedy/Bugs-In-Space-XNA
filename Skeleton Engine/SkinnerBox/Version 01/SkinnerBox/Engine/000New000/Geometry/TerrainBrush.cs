using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class TerrainBrush
    {
        public class BrushData
        {
            public int ChunkX;
            public int ChunkY;
            public int Points;
            public float Size;
            public Color BrushColor;
            public Vector2 Position;

            public BrushData(int chunkX, int chunkY, Vector2 position, int points, float size, Color brushColor)
            {
                Position = position;
                Points = points;
                Size = size;
                BrushColor = brushColor;
                ChunkX = chunkX;
                ChunkY = chunkY;
            }
        }

        static GraphicsDevice graphicsDevice;

        static BasicEffect basicEffect;

        static List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
        static List<short> indices = new List<short>();

        static VertexBuffer vertexBuffer;
        static IndexBuffer indexBuffer;

        static public List<BrushData> Data = new List<BrushData>();

        static public float CanvasSize;

        static public void Initialize(float canvasSize)
        {
            CanvasSize = canvasSize;

            graphicsDevice = GlobalVariables.graphicsDevice;

            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
        }

        static public bool ContainsData(int chunkX, int chunkY)
        {
            for (int i = 0; i < Data.Count; i++)
                if (Data[i].ChunkX == chunkX && Data[i].ChunkY == chunkY)
                    return true;

            return false;
        }

        static private void CreateVertices(FillMode fillMode)
        {
            vertices.Clear();
            indices.Clear();

            bool TerrainHeight = false;


            for (int i = 0; i < Data.Count; i++)
            {
                float angle = MathHelper.TwoPi / Data[i].Points;

                vertices.Add(new VertexPositionColorTexture(Vector3.Zero, Data[i].BrushColor, Vector2.Zero));

                Random rand = new Random(4);


                //Variables, Play with these for unique results!
                float peakheight = 250;
                float flatness = 5;
                int offset = -0;

                float size = Data[i].Size;
                bool Up = true;

                for (int o = 0; o <= Data[i].Points; o++)
                {
                    if (TerrainHeight)
                    {
                        //Generate basic terrain sine
                        //double height = ((rand.NextDouble() * peakheight) / ((rand.NextDouble() + 1) * flatness)) + offset;
                        double height = (rand.NextDouble() * rand.NextDouble() * rand.NextDouble() * peakheight) / ((rand.NextDouble() + 1) * flatness);

                        if (Up)
                        {
                            size += (int)height;
                            if (size > Data[i].Size + peakheight)
                                Up = false;
                        }
                        else
                        {
                            size -= (int)height;
                            if (size < Data[i].Size - peakheight)
                                Up = true;
                        }

                        if (rand.NextDouble() > 0.8)
                            Up = !Up;
                    }

                    vertices.Add(new VertexPositionColorTexture(
                        new Vector3((float)Math.Sin(angle * o) * size,
                                    (float)Math.Cos(angle * o) * size,
                                     0.0f), Data[i].BrushColor, Vector2.Zero));
                }

                if (fillMode == FillMode.Solid)
                    for (int o = 1; o < vertices.Count; o++)
                    {
                        indices.Add((short)o);
                        indices.Add((short)(o + 1));
                        indices.Add(0);
                    }
                else
                    for (int o = 1; o < vertices.Count; o++)
                    {
                        indices.Add((short)o);
                        indices.Add((short)(o + 1));
                    }
            }
        }

        static private void RefreshBuffers()
        {
            if (vertices.Count > 0)
            {
                vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), vertices.Count, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColorTexture>(vertices.ToArray());
                indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Count, BufferUsage.WriteOnly);
                indexBuffer.SetData(indices.ToArray());
            }
            else
            {
                vertexBuffer = null;
                indexBuffer = null;
            }
        }

        static private void PreRender(FillMode fillMode)
        {
            RefreshBuffers();

            //Set up matrix.
            basicEffect.World = Matrix.CreateTranslation(0f, 0f, 0f);
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, CanvasSize, CanvasSize, 0f, 1f, 0f);

            //Enable vertex tinting.
            basicEffect.VertexColorEnabled = true;

            //Set the buffers.
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            //Set the rasterizerState.
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            //Set the fillmode.
            if (fillMode == FillMode.Solid)
            {
                basicEffect.TextureEnabled = true;
                rasterizerState.FillMode = FillMode.Solid;
            }
            else
            {
                basicEffect.TextureEnabled = false;
                rasterizerState.FillMode = FillMode.WireFrame;
            }

            graphicsDevice.RasterizerState = rasterizerState;

        }

        static public void Draw(int chunkX, int chunkY, FillMode fillMode)
        {
            if (Data.Count > 512)
                Data.RemoveRange(512, Data.Count - 512);

            CreateVertices(fillMode);

            PreRender(fillMode);
            basicEffect.TextureEnabled = false;

            int points = 0;

            for (int i = 0; i < Data.Count; i++)
                if (Data[i].ChunkX == chunkX && Data[i].ChunkY == chunkY)
                {
                    basicEffect.View = Matrix.CreateTranslation(Data[i].Position.X, Data[i].Position.Y, 0f);

                    foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        if (fillMode == FillMode.Solid)
                        {
                            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, points, 0, Data[i].Points + 1, 0, Data[i].Points);
                            points += Data[i].Points + 1;
                        }
                        else
                        {
                            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, points, 0, Data[i].Points + 1, 0, Data[i].Points);
                            points += Data[i].Points + 2;
                        }
                    }

                    Data.RemoveAt(i);
                    i--;
                }
        }
    }
}
