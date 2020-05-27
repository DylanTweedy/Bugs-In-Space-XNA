using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace DevStomp
{
    [Serializable()]
    class Chunk : ISerializable
    {
        public Quad[][] ChunkQuads;
        public byte[][] ID;
        public byte[][] N;
        public byte[][] Interpolation;

        public BasicEffect basicEffect;
        public VertexBuffer vertexBuffer;
        public IndexBuffer indexBuffer;
        public VertexPositionColorTexture[] vertices;
        public short[] indices;

        public Chunk()
        {
        }

        public Chunk(SerializationInfo info, StreamingContext ctxt)
        {
            this.ID = (byte[][])info.GetValue("ID", typeof(byte[][]));
            this.Interpolation = (byte[][])info.GetValue("Interpolation", typeof(byte[][]));
        }

        public void Clear()
        {
            for (int x = 0; x < ChunkQuads.Length; x++)
                for (int y = 0; y < ChunkQuads[x].Length; y++)
                    if (ChunkQuads[x][y] != null)
                        ChunkQuads[x][y].Clear();

            ChunkQuads = null;
            ID = null;
            N = null;
            Interpolation = null;
            basicEffect = null;
            vertexBuffer = null;
            indexBuffer = null;
            vertices = null;
            indices = null;
        }

        public void Initialize()
        {
            ID = new byte[GlobalVariables.ChunkSize + 1][];
            Interpolation = new byte[GlobalVariables.ChunkSize + 1][];

            for (int x = 0; x < ID.Length; x++)
            {
                ID[x] = new byte[GlobalVariables.ChunkSize + 1];
                for (int y = 0; y < ID[x].Length; y++)
                {
                    ID[x][y] = (byte)GlobalVariables.RandomNumber.Next(0, 4);
                }
            }

            bool Interpolate;
            for (int x = 0; x < ID.Length; x++)
            {
                Interpolation[x] = new byte[GlobalVariables.ChunkSize + 1];
                for (int y = 0; y < ID[x].Length; y++)
                {
                    Interpolate = false;

                    if (ID[x][y] != 0)
                    {
                        if (x - 1 >= 0)
                            if (ID[x - 1][y] != 0)
                                Interpolate = true;
                            else if (y - 1 >= 0)
                                if (ID[x][y - 1] != 0)
                                    Interpolate = true;

                                else if (x + 1 < ID.Length)
                                    if (ID[x + 1][y] != 0)
                                        Interpolate = true;
                                    else if (y + 1 < ID[x].Length)
                                        if (ID[x][y + 1] != 0)
                                            Interpolate = true;
                                        else
                                            Interpolation[x][y] = 255;


                    }
                    else
                        Interpolation[x][y] = 0;

                    if (Interpolate)
                        Interpolation[x][y] = (byte)(GlobalVariables.RandomNumber.NextDouble() * 255);
                }
            }

        }

        public void InitializeBlank()
        {
            ID = new byte[GlobalVariables.ChunkSize + 1][];
            Interpolation = new byte[GlobalVariables.ChunkSize + 1][];

            for (int x = 0; x < ID.Length; x++)
            {
                ID[x] = new byte[GlobalVariables.ChunkSize + 1];
                for (int y = 0; y < ID[x].Length; y++)                
                    ID[x][y] = 0;                
            }

            for (int x = 0; x < ID.Length; x++)
            {
                Interpolation[x] = new byte[GlobalVariables.ChunkSize + 1];
                for (int y = 0; y < ID[x].Length; y++)                
                        Interpolation[x][y] = 0;                
            }
        }

        public void InitializeSolid(byte id)
        {
            ID = new byte[GlobalVariables.ChunkSize + 1][];
            Interpolation = new byte[GlobalVariables.ChunkSize + 1][];

            for (int x = 0; x < ID.Length; x++)
            {
                ID[x] = new byte[GlobalVariables.ChunkSize + 1];
                Interpolation[x] = new byte[GlobalVariables.ChunkSize + 1];
                for (int y = 0; y < ID[x].Length; y++)
                {
                    ID[x][y] = id;
                    Interpolation[x][y] = 255;
                }
            }
        }

        public void InitializeRandom()
        {
            ID = new byte[GlobalVariables.ChunkSize + 1][];
            Interpolation = new byte[GlobalVariables.ChunkSize + 1][];

            for (int x = 0; x < ID.Length; x++)
            {
                ID[x] = new byte[GlobalVariables.ChunkSize + 1];
                Interpolation[x] = new byte[GlobalVariables.ChunkSize + 1];
                for (int y = 0; y < ID[x].Length; y++)
                {
                    if (GlobalVariables.RandomNumber.Next(0, 7) != 0)
                    {
                        ID[x][y] = (byte)GlobalVariables.RandomNumber.Next(1, 4);
                        Interpolation[x][y] = (byte)GlobalVariables.RandomNumber.Next(1, 256);
                    }
                    else
                    {
                        ID[x][y] = 0;
                        Interpolation[x][y] = 0;
                    }
                }
            }
        }

        public void SetupChunk()
        {
            ChunkQuads = new Quad[GlobalVariables.ChunkSize][];
            basicEffect = GlobalVariables.basicEffect;
        }

        public void CalculateChunk(GraphicsDevice graphics, Vector2 Position, int QuadSize, int ChunkSize)
        {            
            byte[] IDs = new byte[4];
            int VerticiesLength = 0;
            int IndicesLength = 0;
            N = new byte[ChunkSize][];

            Position = Vector2.Zero;

            for (int x = 0; x < ChunkQuads.Length; x++)
            {
                ChunkQuads[x] = new Quad[ChunkSize];
                N[x] = new byte[ChunkSize];

                for (int y = 0; y < ChunkQuads[x].Length; y++)
                {
                    N[x][y] = 0;

                    if (Interpolation[x][y] == 0)
                        N[x][y] += 8;
                    if (Interpolation[x + 1][y] == 0)
                        N[x][y] += 4;
                    if (Interpolation[x + 1][y + 1] == 0)
                        N[x][y] += 2;
                    if (Interpolation[x][y + 1] == 0)
                        N[x][y] += 1;

                    IDs[0] = ID[x][y];
                    IDs[1] = ID[x + 1][y];
                    IDs[2] = ID[x + 1][y + 1];
                    IDs[3] = ID[x][y + 1];

                    float[] IntX = new float[4];
                    if (N[x][y] != 15 && N[x][y] != 0)
                    {
                        //IntX[0] = (float)Interpolation[x][y] / 255f;
                        //IntX[1] = (float)Interpolation[x + 1][y] / 255f;
                        //IntX[2] = (float)Interpolation[x + 1][y + 1] / 255f;
                        //IntX[3] = (float)Interpolation[x][y + 1] / 255f;

                        float max = 0.75f;
                        float min = 0.25f;

                        IntX[0] = UsefulMethods.FindBetween(Interpolation[x][y], 255f, 0f, max, min, false);
                        IntX[1] = UsefulMethods.FindBetween(Interpolation[x + 1][y], 255f, 0f, max, min, false);
                        IntX[2] = UsefulMethods.FindBetween(Interpolation[x + 1][y + 1], 255f, 0f, max, min, false);
                        IntX[3] = UsefulMethods.FindBetween(Interpolation[x][y + 1], 255f, 0f, max, min, false);
                    }

                    if (N[x][y] != 15)
                    {
                        ChunkQuads[x][y] = new Quad();
                        ChunkQuads[x][y].Initialize(Position + new Vector2(QuadSize * x, QuadSize * y), QuadSize, N[x][y], IntX, IDs);
                        VerticiesLength += ChunkQuads[x][y].vertices.Length;
                        IndicesLength += ChunkQuads[x][y].indices.Length;
                    }
                }
            }


            vertices = new VertexPositionColorTexture[VerticiesLength];
            indices = new short[IndicesLength];

            int V = 0;
            int I = 0;

            for (int x = 0; x < ChunkQuads.Length; x++)
                for (int y = 0; y < ChunkQuads[x].Length; y++)
                {
                    if (ChunkQuads[x][y] != null)
                    {
                        for (int i = 0; i < ChunkQuads[x][y].indices.Length; i++)
                        {
                            indices[I] = (short)(ChunkQuads[x][y].indices[i] + V);
                            I++;
                        }

                        for (int i = 0; i < ChunkQuads[x][y].vertices.Length; i++)
                        {
                            vertices[V] = ChunkQuads[x][y].vertices[i];
                            V++;
                        }
                    }
                }

            RefreshBuffers(graphics);
        }
        
        public void RefreshBuffers(GraphicsDevice graphics)
        {
            if (vertices.Length > 0)
            {
                vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColorTexture), vertices.Length, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColorTexture>(vertices);
                indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
                indexBuffer.SetData(indices);
            }
            else
            {
                vertexBuffer = null;
                indexBuffer = null;
            }
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("ID", this.ID);
            info.AddValue("Interpolation", this.Interpolation);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Vector2 position, Vector2 Offset, short quadSize, float Rotation, int camera)
        {
            //* Matrix.CreateTranslation((5 * 32 * 16) / 2, (5 * 32 * 16) / 2, 0)
            //Matrix.CreateTranslation((32 * 16) / 2, (32 * 16) / 2, 0)
            //Matrix.CreateTranslation(position.X, position.Y, 0)
            if (indexBuffer != null)
            {
                basicEffect.World =

                    Matrix.CreateTranslation(Offset.X, Offset.Y, 0) *
                    //Matrix.CreateTranslation(quadSize * 16, quadSize * 16, 0) *



                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateTranslation(position.X, position.Y, 0)

                    




                    ;

                

                basicEffect.View = CameraManager.Cams[camera].Transform;
                basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, CameraManager.Cams[camera].viewport.Width, CameraManager.Cams[camera].viewport.Height, 0, 1, 0);

                basicEffect.VertexColorEnabled = true;
                basicEffect.Texture = StaticTests.Stone;
                basicEffect.TextureEnabled = true;

                graphics.SetVertexBuffer(vertexBuffer);
                graphics.Indices = indexBuffer;

                RasterizerState rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                rasterizerState.FillMode = FillMode.Solid;

                position += Offset;

                if (DebugOptions.DebugActive)
                {
                    rasterizerState.FillMode = FillMode.WireFrame;
                    basicEffect.TextureEnabled = false;

                    spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * ChunkQuads.Length, quadSize * ChunkQuads.Length) + position, null, Color.Yellow * 0.5f, 0f,
                    new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(0.25f, (ChunkQuads.Length * quadSize) / (StaticTests.Marker.Height / 2)), SpriteEffects.None, 0f);

                    spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * ChunkQuads.Length, quadSize * ChunkQuads.Length) + position, null, Color.Yellow * 0.5f, 0f,
                        new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2((ChunkQuads.Length * quadSize) / (StaticTests.Marker.Width / 2), 0.25f), SpriteEffects.None, 0f);
                }
                graphics.RasterizerState = rasterizerState;

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexBuffer.VertexCount, 0, indexBuffer.IndexCount / 3);
                }
            }

            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt))
            {
                for (int x = 0; x < ChunkQuads.Length + 1; x++)
                    for (int y = 0; y < ChunkQuads.Length + 1; y++)
                    {
                        spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * x, quadSize * y) + position, null, Color.LightGreen * 0.1f, 0f,
                            new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(2f, 0.1f), SpriteEffects.None, 1f);

                        spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * x, quadSize * y) + position, null, Color.LightGreen * 0.1f, 0f,
                            new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(0.1f, 2f), SpriteEffects.None, 1f);
                    }

                
            }
        }
    }
}
