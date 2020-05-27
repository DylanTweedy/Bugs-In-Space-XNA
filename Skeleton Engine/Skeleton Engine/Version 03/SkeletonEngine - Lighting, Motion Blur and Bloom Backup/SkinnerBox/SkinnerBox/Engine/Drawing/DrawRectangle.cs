using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevStomp
{
    static class DrawRectangle
    {
        static public VertexBuffer vertexBuffer;
        static public IndexBuffer indexBuffer;
        static public List<VertexPositionColor> vertices;
        static public short[] indices;

        static public Color LineColor;


        static public void Initialize(GraphicsDevice graphics)
        {
            LineColor = Color.White;

            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), 4, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphics, typeof(short), 5, BufferUsage.WriteOnly);

            vertices = new List<VertexPositionColor>();
            indices = new short[5];

            for (int i = 0; i < 4; i++)
                vertices.Add(new VertexPositionColor(new Vector3(0, 0, 0), LineColor));

            for (short i = 0; i < indices.Length; i++)
                indices[i] = i;

            indices[indices.Length - 1] = 0;
        }

        static private void SetPoints(Rectangle points)
        {
            vertices.Clear();

            vertices.Add(new VertexPositionColor(new Vector3(points.X, points.Y, 0), LineColor));
            vertices.Add(new VertexPositionColor(new Vector3(points.X + points.Width, points.Y, 0), LineColor));
            vertices.Add(new VertexPositionColor(new Vector3(points.X + points.Width, points.Y + points.Height, 0), LineColor));
            vertices.Add(new VertexPositionColor(new Vector3(points.X, points.Y + points.Height, 0), LineColor));

            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }

        static private void SetColor(Color color)
        {
            LineColor = color;

            for (int i = 0; i < vertices.Count; i++)
                vertices[i] = new VertexPositionColor(vertices[i].Position, LineColor);
        }

        static public void Draw(GraphicsDevice graphics, Rectangle rect, Color color, int camera)
        {
            SetColor(color);
            SetPoints(rect);

            Vector2 offset = -new Vector2(CameraManager.Cams[camera].PositionX * 2000000f, CameraManager.Cams[camera].PositionY * 2000000f);
            BasicEffect basicEffect = GlobalVariables.basicEffect;

            basicEffect.World = Matrix.CreateTranslation(offset.X, offset.Y, 0) * Matrix.CreateRotationZ(CameraManager.Cams[camera].R);

            basicEffect.View = CameraManager.Cams[camera].Transform;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, CameraManager.Cams[camera].viewport.Width, CameraManager.Cams[camera].viewport.Height, 0, 1, 0);

            basicEffect.VertexColorEnabled = true;
            basicEffect.Texture = null;
            basicEffect.TextureEnabled = false;

            graphics.SetVertexBuffer(vertexBuffer);
            graphics.Indices = indexBuffer;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            rasterizerState.FillMode = FillMode.WireFrame;
            graphics.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawIndexedPrimitives(PrimitiveType.LineStrip, 0, 0, vertices.Count, 0, indices.Length - 1);
            }
        }
    }
}
