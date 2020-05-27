using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevStomp
{
    class DrawRectangle2
    {
        public VertexBuffer vertexBuffer;
        public IndexBuffer indexBuffer;
        public List<VertexPositionColor> vertices;
        public short[] indices;
        Rectangle Rect;

        public Color LineColor;


        public void Initialize(GraphicsDevice graphics, Rectangle rect)
        {
            Rect = rect;
            LineColor = Color.White;

            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), 4, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphics, typeof(short), 5, BufferUsage.WriteOnly);

            vertices = new List<VertexPositionColor>();
            indices = new short[5];

            for (short i = 0; i < indices.Length; i++)
                indices[i] = i;

            indices[indices.Length - 1] = 0;
        }

        private void SetPoints()
        {
            vertices.Clear();

            vertices.Add(new VertexPositionColor(new Vector3(Rect.X, Rect.Y, 0), LineColor));
            vertices.Add(new VertexPositionColor(new Vector3(Rect.X + Rect.Width, Rect.Y, 0), LineColor));
            vertices.Add(new VertexPositionColor(new Vector3(Rect.X + Rect.Width, Rect.Y + Rect.Height, 0), LineColor));
            vertices.Add(new VertexPositionColor(new Vector3(Rect.X, Rect.Y + Rect.Height, 0), LineColor));

            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }

        private void SetColor(Color color)
        {
            LineColor = color;

            for (int i = 0; i < vertices.Count; i++)
                vertices[i] = new VertexPositionColor(vertices[i].Position, LineColor);
        }

        public void Draw(GraphicsDevice graphics, Color color, int camera)
        {
            SetColor(color);
            SetPoints();
            
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
