using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class DrawLine
    {
        public VertexBuffer vertexBuffer;
        public IndexBuffer indexBuffer;
        public VertexPositionColor[] vertices;
        public short[] indices;

        public void Initialize(GraphicsDevice graphics)
        {
            vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColor), 2, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphics, typeof(short), 2, BufferUsage.WriteOnly);

            vertices = new VertexPositionColor[2];
            indices = new short[2];

            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);

            indices[0] = 0;
            indices[1] = 1;
        }

        public void SetPoint1(Vector2 point, Color color)
        {
            vertices[0] = new VertexPositionColor(new Vector3(point.X, point.Y, 0), color);
        }

        public void SetPoint2(Vector2 point, Color color)
        {
            vertices[1] = new VertexPositionColor(new Vector3(point.X, point.Y, 0), color);
        }

        public void SetData()
        {
            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }
        
        public void ClearPoints(Vector2 point)
        {
            vertices[0] = new VertexPositionColor(new Vector3(point.X, point.Y, 0), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(point.X, point.Y, 0), Color.White);

            vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());
            indexBuffer.SetData(indices);
        }
        
        public void Draw(GraphicsDevice graphics,int camera)
        {
            Vector2 offset = -new Vector2(CameraManager.Cams[camera].PositionX * 2000000, CameraManager.Cams[camera].PositionY * 2000000);
            BasicEffect basicEffect = GlobalVariables.basicEffect;

            basicEffect.World = Matrix.CreateTranslation(offset.X, offset.Y, 0) * Matrix.CreateRotationZ(CameraManager.Cams[camera].Rotation);

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
                graphics.DrawIndexedPrimitives(PrimitiveType.LineStrip, 0, 0, 2, 0, 1);
            }
        }
    }
}
