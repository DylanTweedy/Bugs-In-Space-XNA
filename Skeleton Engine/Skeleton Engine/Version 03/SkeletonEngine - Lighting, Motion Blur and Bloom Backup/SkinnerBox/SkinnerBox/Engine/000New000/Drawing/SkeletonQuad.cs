using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class SkeletonQuad
    {
        public Quad MainQuad;
        public SkeletonTexture Texture;
        public List<string> SoloCamera;

        public SkeletonQuad(Vector2 position, float scale, Color color, string textureLocation, string textureName)
        {
            MainQuad = new Quad(position, scale, scale);
            MainQuad.SetColor(color);
            Texture = new SkeletonTexture(textureLocation, textureName);
        }

        public void UpdateColor(Color color)
        {
            MainQuad.SetColor(color);
        }

        public void UpdateColors(Color TopLeft, Color TopRight, Color BottomLeft, Color BottomRight)
        {
            MainQuad.SetColors(TopLeft, TopRight, BottomLeft, BottomRight);
        }

        public void Move(Vector2 amount)
        {
            MainQuad.vertices[0].Position += new Vector3(amount, 0);
            MainQuad.vertices[1].Position += new Vector3(amount, 0);
            MainQuad.vertices[2].Position += new Vector3(amount, 0);
            MainQuad.vertices[3].Position += new Vector3(amount, 0);
        }

        public Vector2 GetCenter()
        {
            float XCenter = 
                MainQuad.vertices[0].Position.X + MainQuad.vertices[1].Position.X + 
                MainQuad.vertices[2].Position.X + MainQuad.vertices[3].Position.X / 4;
            float YCenter = 
                MainQuad.vertices[0].Position.Y + MainQuad.vertices[1].Position.Y + 
                MainQuad.vertices[2].Position.Y + MainQuad.vertices[3].Position.Y / 4;

            return new Vector2(XCenter, YCenter);
        }

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

        public void AddSoloCamera(string cameraName)
        {
            if (SoloCamera == null)
                SoloCamera = new List<string>();

            SoloCamera.Add(cameraName);
        }
    }
}
