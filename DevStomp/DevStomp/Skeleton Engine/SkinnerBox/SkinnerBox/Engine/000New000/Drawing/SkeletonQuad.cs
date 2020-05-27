using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    /// <summary>
    /// A 4 sided shape made from 4 points.
    /// </summary>
    class Quad
    {
        /// <summary>
        /// List of the quads vertices.
        /// </summary>
        public VertexPositionColorTexture[] vertices;

        /// <summary>
        /// Creates a new quad.
        /// </summary>
        /// <param name="Position">The position of the center of the quad.</param>
        /// <param name="Width">The total width of the quad.</param>
        /// <param name="Height">The total height of the quad.</param>
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

        /// <summary>
        /// Sets a color for the whole quad.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            vertices[0].Color = color;
            vertices[1].Color = color;
            vertices[3].Color = color;
            vertices[2].Color = color;
        }

        /// <summary>
        /// Sets a color for each quad point.
        /// </summary>
        /// <param name="TopLeft"></param>
        /// <param name="TopRight"></param>
        /// <param name="BottomLeft"></param>
        /// <param name="BottomRight"></param>
        public void SetColors(Color TopLeft, Color TopRight, Color BottomLeft, Color BottomRight)
        {
            vertices[0].Color = TopLeft;
            vertices[1].Color = BottomLeft;
            vertices[2].Color = BottomRight;
            vertices[3].Color = TopRight;
        }
    }

    /// <summary>
    /// A quad with extended functionality.
    /// </summary>
    class SkeletonQuad
    {
        /// <summary>
        /// The base quad.
        /// </summary>
        public Quad MainQuad;
        /// <summary>
        /// The texture to apply to the quad.
        /// </summary>
        public SkeletonTexture Texture;
        /// <summary>
        /// A camera not in this list won't be able to render this quad.
        /// </summary>
        public List<string> SoloCamera;

        /// <summary>
        /// Creates a new SkeletonQuad
        /// </summary>
        /// <param name="position">The position of the quad.</param>
        /// <param name="scale">The size of the quad.</param>
        /// <param name="color">The color to tint the quad.</param>
        /// <param name="textureLocation">The location of the texture file.</param>
        /// <param name="textureName">The texture files name.</param>
        public SkeletonQuad(Vector2 position, float scale, Color color, string textureLocation, string textureName)
        {
            MainQuad = new Quad(position, scale, scale);
            MainQuad.SetColor(color);
            Texture = new SkeletonTexture(textureLocation, textureName);
        }

        /// <summary>
        /// Sets a color for the whole quad.
        /// </summary>
        /// <param name="color"></param>
        public void UpdateColor(Color color)
        {
            MainQuad.SetColor(color);
        }

        /// <summary>
        /// Sets a color for each quad point.
        /// </summary>
        /// <param name="TopLeft"></param>
        /// <param name="TopRight"></param>
        /// <param name="BottomLeft"></param>
        /// <param name="BottomRight"></param>
        public void UpdateColors(Color TopLeft, Color TopRight, Color BottomLeft, Color BottomRight)
        {
            MainQuad.SetColors(TopLeft, TopRight, BottomLeft, BottomRight);
        }
                
        /// <summary>
        /// Move the whole quad by a certain amount.
        /// </summary>
        /// <param name="amount">Amount to move by.</param>
        public void Move(Vector2 amount)
        {
            MainQuad.vertices[0].Position += new Vector3(amount, 0);
            MainQuad.vertices[1].Position += new Vector3(amount, 0);
            MainQuad.vertices[2].Position += new Vector3(amount, 0);
            MainQuad.vertices[3].Position += new Vector3(amount, 0);
        }

        /// <summary>
        /// Returns the center of the quad.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCenter()
        {
            float XCenter = 
                (MainQuad.vertices[0].Position.X + MainQuad.vertices[1].Position.X + 
                MainQuad.vertices[2].Position.X + MainQuad.vertices[3].Position.X) / 4;
            float YCenter = 
                (MainQuad.vertices[0].Position.Y + MainQuad.vertices[1].Position.Y + 
                MainQuad.vertices[2].Position.Y + MainQuad.vertices[3].Position.Y) / 4;

            return new Vector2(XCenter, YCenter);
        }

        /// <summary>
        /// Gets the radius of the quad.
        /// </summary>
        /// <param name="center">The center of the quad. ( GetCenter() )</param>
        /// <returns></returns>
        public float GetRadius(Vector2 center)
        {
            float radius = 0f;

            for (int i = 0; i < MainQuad.vertices.Length; i++)
            {
                float distance = Vector2.Distance(center, new Vector2(MainQuad.vertices[0].Position.X, MainQuad.vertices[0].Position.Y));
                if (distance > radius)
                    radius = distance;
            }

            return radius;
        }

        /// <summary>
        /// Rotate quad around its center point.
        /// </summary>
        /// <param name="radians">Radians to rotate by.</param>
        public void Rotate(float radians)
        {
            Vector2 Center = GetCenter();

            MainQuad.vertices[0].Position = new Vector3(UsefulMethods.RotatePoint(
                new Vector2(MainQuad.vertices[0].Position.X, MainQuad.vertices[0].Position.Y), Center, radians), 0f);
            MainQuad.vertices[1].Position = new Vector3(UsefulMethods.RotatePoint(
                new Vector2(MainQuad.vertices[1].Position.X, MainQuad.vertices[1].Position.Y), Center, radians), 0f);
            MainQuad.vertices[2].Position = new Vector3(UsefulMethods.RotatePoint(
                new Vector2(MainQuad.vertices[2].Position.X, MainQuad.vertices[2].Position.Y), Center, radians), 0f);
            MainQuad.vertices[3].Position = new Vector3(UsefulMethods.RotatePoint(
                new Vector2(MainQuad.vertices[3].Position.X, MainQuad.vertices[3].Position.Y), Center, radians), 0f);
        }

        /// <summary>
        /// Adds a flag to only allow the quad to be rendered by a specific set of cameras.
        /// </summary>
        /// <param name="cameraName">The camera to add to the list.</param>
        public void AddSoloCamera(string cameraName)
        {
            if (SoloCamera == null)
                SoloCamera = new List<string>();

            SoloCamera.Add(cameraName);
        }
    }
}
