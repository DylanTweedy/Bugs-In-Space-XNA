using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class Quad
    {
        public VertexPositionColorTexture[] vertices;

        public Quad(Vector2 Position, float Width, float Height)
        {
            Width /= 2f;
            Height /= 2f;

            vertices = new VertexPositionColorTexture[4];

            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X - Width, Position.Y - Height, 0), Color.White, new Vector2(0, 0));
            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X - Width, Position.Y + Height, 0), Color.White, new Vector2(0, 1));
            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + Width, Position.Y + Height, 0), Color.White, new Vector2(1, 1));
            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + Width, Position.Y - Height, 0), Color.White, new Vector2(1, 0));
        }

        public void SetColor(Color color)
        {
            vertices[0].Color = color;
            vertices[1].Color = color;
            vertices[3].Color = color;
            vertices[2].Color = color;
        }

        public void SetColors(Color TopLeft, Color TopRight, Color BottomLeft, Color BottomRight)
        {
            vertices[0].Color = TopLeft;
            vertices[1].Color = BottomLeft;
            vertices[2].Color = BottomRight;
            vertices[3].Color = TopRight;
        }
    }

    static class QuadManager
    {
        static public BasicEffect basicEffect;
        static public VertexBuffer vertexBuffer;
        static public IndexBuffer indexBuffer;
        static public List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
        static public List<int> indices = new List<int>();

        static GraphicsDevice graphicsDevice;
        static SpriteBatch spriteBatch;

        static byte[] BaseIndices = new byte[6];

        static List<SkeletonQuad> Quads = new List<SkeletonQuad>();

        static public void Initialize()
        {
            graphicsDevice = GlobalVariables.graphicsDevice;
            spriteBatch = GlobalVariables.spriteBatch;
            basicEffect = new BasicEffect(graphicsDevice);

            BaseIndices[0] = 0;
            BaseIndices[1] = 1;
            BaseIndices[2] = 3;
            BaseIndices[3] = 1;
            BaseIndices[4] = 3;
            BaseIndices[5] = 2;
        }

        static public void AddQuad(SkeletonQuad quad)
        {
            Quads.Add(quad);
        }

        static private void LoadQuads()
        {
            for (int i = 0; i < Quads.Count; i++)
                for (int o = 0; o < BaseIndices.Length; o++)
                    indices.Add(BaseIndices[o] + (4 * i));
            
            for (int i = 0; i < Quads.Count; i++)
                vertices.AddRange(Quads[i].MainQuad.vertices);
        }
        
        static private void RefreshBuffers()
        {
            if (vertices.Count > 0)
            {
                vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), vertices.Count, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColorTexture>(vertices.ToArray());
                indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Count, BufferUsage.WriteOnly);
                indexBuffer.SetData(indices.ToArray());
            }
            else
            {
                vertexBuffer = null;
                indexBuffer = null;
            }
        }

        static public void PreRender(FillMode fillMode, Camera cam)
        {
            RefreshBuffers();

            basicEffect.World = Matrix.CreateTranslation(0, 0, 0f);
            basicEffect.View = cam.Transform;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, cam.FinalRender.Width, cam.FinalRender.Height, 0, 1f, 0f);

            basicEffect.VertexColorEnabled = true;

            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.WireFrame;

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

        static public void Draw(FillMode fillMode, Camera cam)
        {
            LoadQuads();

            if (Quads.Count != 0)
            {
                PreRender(fillMode, cam);

                for (int i = 0; i < Quads.Count; i++)
                {
                    if (Quads[i].SoloCamera == null || Quads[i].SoloCamera.Contains(cam.CameraName))
                    {
                        basicEffect.Texture = Quads[i].Texture.Texture;

                        foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, i * 4, 0, 4, 0, 2);
                        }
                    }
                }
            }

            Quads.Clear();
            vertices.Clear();
            indices.Clear();
        }
    }
}
