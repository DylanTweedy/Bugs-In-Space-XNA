using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class Trail
    {
        public VertexBuffer vertexBuffer;
        public IndexBuffer indexBuffer;
        public List<VertexPositionColor> vertices;
        public short[] indices;

        public int Length;
        public float Resolution;
        public Color LineColor;


        public void Initialize(GraphicsDevice graphics, int length, float resolution)
        {
            Length = length;
            Resolution = resolution;
            LineColor = Color.White;

            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphics, typeof(short), Length, BufferUsage.WriteOnly);

            vertices = new List<VertexPositionColor>();
            indices = new short[Length];

            for (int i = 0; i < Length; i++)
                vertices.Add(new VertexPositionColor(new Vector3(0, 0, 0), LineColor));

            for (short i = 0; i < indices.Length; i++)
                indices[i] = i;
        }

        public void AddPoint(Vector2 point)
        {
            if (Vector2.Distance(point, new Vector2(vertices[vertices.Count - 2].Position.X, vertices[vertices.Count - 2].Position.Y)) > Resolution)
            {
                vertices.Add(new VertexPositionColor(new Vector3(point.X, point.Y, 0), LineColor));
                vertices.RemoveAt(0);
            }
            else
                vertices[vertices.Count - 1] = new VertexPositionColor(new Vector3(point.X, point.Y, 0), LineColor);

            for (int i = 0; i < vertices.Count; i++)            
                vertices[i] = new VertexPositionColor(vertices[i].Position, LineColor * (i / (float)Length));
            
            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }

        public void MovePoints(Vector2 distance)
        {
            for (int i = 0; i < vertices.Count; i++)
                vertices[i] = new VertexPositionColor(vertices[i].Position + new Vector3(distance.X, distance.Y, 0), LineColor * (i / (float)Length));

            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }

        public void ClearPoints(Vector2 point)
        {
            vertices.Clear();
            for (int i = 0; i < Length; i++)
                vertices.Add(new VertexPositionColor(new Vector3(point.X, point.Y, 0), LineColor));
            
            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }

        public void SetLength(int length, float resolution, GraphicsDevice graphics, Vector2 point)
        {
            Length = length;
            Resolution = resolution;

            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphics, typeof(short), Length, BufferUsage.WriteOnly);

            vertices = new List<VertexPositionColor>();
            indices = new short[Length];

            for (int i = 0; i < Length; i++)
                vertices.Add(new VertexPositionColor(new Vector3(point.X, point.Y, 0), LineColor));

            for (short i = 0; i < indices.Length; i++)
                indices[i] = i;
        }

        public void SetColor(Color color)
        {
            LineColor = color;

            for (int i = 0; i < vertices.Count; i++)
                vertices[i] = new VertexPositionColor(vertices[i].Position, LineColor * (i / (float)Length));
        }

        public void Draw(GraphicsDevice graphics, int camera)
        {
            if (camera == 1)
            {
            }

            Vector2 offset = -new Vector2(CameraManager.Cams[camera].PositionX * 2000000f, CameraManager.Cams[camera].PositionY * 2000000f);
            Vector2 offset2 = -new Vector2(CameraManager.Cams[camera].PositionX, CameraManager.Cams[camera].PositionY);
            Vector2 offset3 = -new Vector2(2000000f, 2000000f);
            BasicEffect basicEffect = GlobalVariables.basicEffect;

            basicEffect.World = Matrix.CreateTranslation(offset.X, offset.Y, 0) * Matrix.CreateRotationZ(CameraManager.Cams[camera].Rotation);
            //basicEffect.World = Matrix.CreateTranslation(offset2.X, offset2.Y, 0) * Matrix.CreateTranslation(offset3.X, offset3.Y, 0) * Matrix.CreateRotationZ(CameraManager.Cams[camera].R);

            basicEffect.View = CameraManager.Cams[camera].Transform;
            //basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, CameraManager.Cams[camera].viewport.Width, CameraManager.Cams[camera].viewport.Height, 0, 1, 0);

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
