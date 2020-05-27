using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class DrawRectangle3
    {
        static public DynamicVertexBuffer vertexBuffer;
        static public DynamicIndexBuffer indexBuffer;
        static public List<VertexPositionColor> vertices;
        static public List<short> indices;
        static public List<Rectangle> Rectangles;
        static public List<Color> LineColors;
        static int PrevRects;

        static public void Initialize(GraphicsDevice graphics)
        {
            LineColors = new List<Color>();
            Rectangles = new List<Rectangle>();

            vertices = new List<VertexPositionColor>();
            indices = new List<short>();

            PrevRects = 0;
        }

        static private void AddPoints(GraphicsDevice graphics)
        {
            vertices.Clear();
            indices.Clear();

            for (int i = 0; i < Rectangles.Count; i++)
            {
                vertices.Add(new VertexPositionColor(new Vector3(Rectangles[i].X, Rectangles[i].Y, 0), LineColors[i]));
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(vertices.Count));
                vertices.Add(new VertexPositionColor(new Vector3(Rectangles[i].X + Rectangles[i].Width, Rectangles[i].Y, 0), LineColors[i]));
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(vertices.Count));
                vertices.Add(new VertexPositionColor(new Vector3(Rectangles[i].X + Rectangles[i].Width, Rectangles[i].Y + Rectangles[i].Height, 0), LineColors[i]));
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(vertices.Count));
                vertices.Add(new VertexPositionColor(new Vector3(Rectangles[i].X, Rectangles[i].Y + Rectangles[i].Height, 0), LineColors[i]));
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(vertices.Count - 4));
            }

            if (PrevRects != Rectangles.Count)
            {
                vertexBuffer = new DynamicVertexBuffer(graphics, typeof(VertexPositionColor), vertices.Count, BufferUsage.WriteOnly);
                indexBuffer = new DynamicIndexBuffer(graphics, typeof(short), indices.Count, BufferUsage.WriteOnly);
            }

            PrevRects = Rectangles.Count;

            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices.ToArray());
        }

        static public void AddRectangle(Rectangle rect, Color color)
        {
            Rectangles.Add(rect);
            LineColors.Add(color);
        }

        static private void SetBuffer()
        {

        }

        static public void Draw(GraphicsDevice graphics, Camera cam)
        {
            if (Rectangles.Count != 0)
            {
                //AddPoints(graphics);

                //Vector2 offset = -new Vector2(cam.PositionX * 2000000f, cam.PositionY * 2000000f);
                //BasicEffect basicEffect = GlobalVariables.basicEffect;

                //basicEffect.World = Matrix.CreateTranslation(offset.X, offset.Y, 0) * Matrix.CreateRotationZ(cam.Rotation);

                //basicEffect.View = cam.Transform;
                //basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, graphics.Viewport.Width, graphics.Viewport.Height, 0, 1, 0);

                //basicEffect.VertexColorEnabled = true;
                //basicEffect.Texture = null;
                //basicEffect.TextureEnabled = false;

                //graphics.SetVertexBuffer(vertexBuffer);
                //graphics.Indices = indexBuffer;

                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //graphics.RasterizerState = rasterizerState;

                //foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                //{
                //    pass.Apply();
                //    graphics.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, vertices.Count, 0, indices.Count - 1);
                //}
            }

            Rectangles.Clear();
            LineColors.Clear();
            vertices.Clear();
            indices.Clear();
        }
    }
}
