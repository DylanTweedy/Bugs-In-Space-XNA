using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SkeletonEngine
{
    class LineVisualizer
    {
        public class ShapeData
        {
            public List<SpacePosition> Points = new List<SpacePosition>();
            public Color Tint;

            public ShapeData(List<SpacePosition> points, Color tint)
            {
                Points = points;
                Tint = tint;
            }
        }

        GraphicsDevice graphicsDevice;
        BasicEffect basicEffect;

        List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
        List<short> indices = new List<short>();

        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        List<ShapeData> Data = new List<ShapeData>();

        public LineVisualizer()
        {
            graphicsDevice = GlobalVariables.graphicsDevice;

            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
        }

        public void AddData(List<SpacePosition> points, Color tint)
        {
            Data.Add(new ShapeData(points, tint));
        }

        private void CreateVertices()
        {
            vertices.Clear();
            indices.Clear();

            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Points.Count == 1)
                    vertices.Add(new VertexPositionColorTexture(new Vector3(Data[i].Points[0].Position.X, Data[i].Points[0].Position.Y, 0.0f), Data[i].Tint, Vector2.Zero));

                for (int o = 0; o < Data[i].Points.Count; o++)
                    vertices.Add(new VertexPositionColorTexture(
                        new Vector3(Data[i].Points[o].Position.X, Data[i].Points[o].Position.Y, 0.0f), Data[i].Tint, Vector2.Zero));

                for (int o = 0; o < vertices.Count - 1; o++)
                {
                    indices.Add((short)o);
                    indices.Add((short)(o + 1));
                }
            }
        }

        private void RefreshBuffers()
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

        private void PreRender(Camera camera)
        {
            RefreshBuffers();

            //Set up matrix.
            basicEffect.World = Matrix.CreateTranslation(0f, 0f, 0f);

            if (camera != null)
            {
                basicEffect.View = camera.Transform;
                basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, camera.RenderTarget.Width, camera.RenderTarget.Height, 0f, 1f, 0f);
            }
            else
            {
                basicEffect.View = Matrix.CreateTranslation(0f, 0f, 0f);
                basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, GraphicsManager.VirtualResolution.X, GraphicsManager.VirtualResolution.Y, 0f, 1f, 0f);
            }


            //Enable vertex tinting.
            basicEffect.VertexColorEnabled = true;

            //Set the buffers.
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            //Set the rasterizerState.
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            //Set the fillmode.
            basicEffect.TextureEnabled = false;
            rasterizerState.FillMode = FillMode.WireFrame;

            graphicsDevice.RasterizerState = rasterizerState;
        }

        public void Draw(Camera camera)
        {
            if (Data.Count > 512)
                Data.RemoveRange(512, Data.Count - 512);

            CreateVertices();

            PreRender(camera);
            basicEffect.TextureEnabled = false;

            int points = 0;

            for (int i = 0; i < Data.Count; i++)
            {
                if (camera == null)
                    basicEffect.World = Matrix.CreateTranslation(0f, 0f, 0f);
                else
                {
                    //GlobalVariables.TempSP = camera.Position;
                    GlobalVariables.TempSP = new SpacePosition();
                    basicEffect.World = Matrix.CreateTranslation(GlobalVariables.TempSP.RoughPosition.X, GlobalVariables.TempSP.RoughPosition.Y, 0f);
                }

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, points, 0, vertices.Count, 0, vertices.Count - 1);
                    points += Data[i].Points.Count + 1;

                }

                Data.RemoveAt(i);
                i--;
            }
        }
    }

    class GeometryVisualizer
    {
        public class ShapeData
        {
            public int Points;
            public Vector2 Size;

            public Color Tint;
            public SpacePosition Position;
            public float Rotation;

            public ShapeData(SpacePosition position, float rotation, int points, Vector2 size, Color tint)
            {
                Position = position;
                Rotation = rotation;
                Points = points;
                Size = size / 2f;
                Tint = tint;
            }
        }

        GraphicsDevice graphicsDevice;
        BasicEffect basicEffect;

        List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
        List<short> indices = new List<short>();

        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        List<ShapeData> Data = new List<ShapeData>();

        public GeometryVisualizer()
        {
            graphicsDevice = GlobalVariables.graphicsDevice;

            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
        }

        public void AddData(SpacePosition position, float rotation, int points, Vector2 size, Color tint)
        {
            Data.Add(new ShapeData(position, rotation, points, size, tint));
        }

        private void CreateVertices(FillMode fillMode)
        {
            vertices.Clear();
            indices.Clear();

            for (int i = 0; i < Data.Count; i++)
            {
                float angle = MathHelper.TwoPi / Data[i].Points;

                vertices.Add(new VertexPositionColorTexture(Vector3.Zero, Data[i].Tint, Vector2.Zero));

                switch (Data[i].Points)
                {
                    case 4:
                        for (int o = 0; o <= Data[i].Points; o++)
                        {
                            Vector2 pointPos = new Vector2((float)Math.Sin((angle * o) + Data[i].Rotation + 0.7853982f) * Data[i].Size.X * 1.41421354f,
                                            (float)Math.Cos((angle * o) + Data[i].Rotation + 0.7853982f) * Data[i].Size.Y * 1.41421354f);

                            vertices.Add(new VertexPositionColorTexture(
                                new Vector3(pointPos.X, pointPos.Y, 0.0f), Data[i].Tint, Vector2.Zero));
                        }
                        break;

                    default:
                        for (int o = 0; o <= Data[i].Points; o++)
                        {
                            Vector2 pointPos = new Vector2((float)Math.Sin((angle * o) + Data[i].Rotation) * Data[i].Size.X,
                                            (float)Math.Cos((angle * o) + Data[i].Rotation) * Data[i].Size.Y);

                            vertices.Add(new VertexPositionColorTexture(
                                new Vector3(pointPos.X, pointPos.Y, 0.0f), Data[i].Tint, Vector2.Zero));
                        }
                        break;
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

        private void RefreshBuffers()
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

        private void PreRender(Camera camera, FillMode fillMode)
        {
            RefreshBuffers();

            //Set up matrix.
            basicEffect.World = Matrix.CreateTranslation(0f, 0f, 0f);

            if (camera != null)
            {
                basicEffect.View = camera.Transform;
                basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, camera.RenderTarget.Width, camera.RenderTarget.Height, 0f, 1f, 0f);
            }
            else
            {
                basicEffect.View = Matrix.CreateTranslation(0f, 0f, 0f);
                basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, GraphicsManager.VirtualResolution.X, GraphicsManager.VirtualResolution.Y, 0f, 1f, 0f);
            }


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

        public void Draw(Camera camera, FillMode fillMode)
        {
            if (Data.Count > 512)
                Data.RemoveRange(512, Data.Count - 512);

            CreateVertices(fillMode);

            PreRender(camera, fillMode);
            basicEffect.TextureEnabled = false;

            int points = 0;

            for (int i = 0; i < Data.Count; i++)
            {
                if (camera == null)
                    basicEffect.World = Matrix.CreateTranslation(Data[i].Position.RoughPosition.X, Data[i].Position.RoughPosition.Y, 0f);
                else
                {
                    GlobalVariables.TempSP = SpacePosition.CameraTransform(camera.Position, Data[i].Position);
                    basicEffect.World = Matrix.CreateTranslation(GlobalVariables.TempSP.RoughPosition.X, GlobalVariables.TempSP.RoughPosition.Y, 0f);
                }                

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
