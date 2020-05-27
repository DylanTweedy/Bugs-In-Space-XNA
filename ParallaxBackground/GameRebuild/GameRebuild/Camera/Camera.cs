using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRebuild
{
    class Camera
    {
        public Matrix Transform;

        Vector3 WorldPosition;
        public Vector2 P;
        Vector2 PP;
        Vector2 PD;
        float PT;
        public float R;
        float RD;
        float RT;
        public float Z;
        float PZ;
        float ZD;
        float ZT;
        Vector2 Origin;
        Parallax Para;
        public Vector2 WindowDimensions;

        public Camera(Vector3 worldPosition, Vector2 position, float rotation, float zoom)
        {
            WorldPosition = worldPosition;
            P = position;
            PD = position;
            R = rotation;
            RD = rotation;
            Z = zoom;
            ZD = zoom;
            PP = P;
            PZ = Z;

            Para = new Parallax(P);
        }

        public void Update()
        {
            if (P != PD)
                P += ((PD - P) * ((float)WorldVariables.FrameTime * PT));
            if (R != RD)
                R += ((RD - R) * ((float)WorldVariables.FrameTime * RT));
            if (Z != ZD)
                Z += ((ZD - Z) * ((float)WorldVariables.FrameTime * ZT));

            Origin = WorldVariables.ScreenCenter / Z;

            Transform = Matrix.Identity *
            Matrix.CreateTranslation(-P.X, -P.Y, 0) *
            Matrix.CreateRotationZ(R) *
            Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
            Matrix.CreateScale(Z);

            Para.Update(WindowDimensions, PP - P, P, R, Z);

            PP = P;
            PZ = Z;
        }

        public void SetPosition(Vector2 p)
        {
            P = p;
            PD = p;
        }

        public void SetRotation(float r)
        {
            R = r;
            RD = r;
        }

        public void SetZoom(float z)
        {
            Z = z;
            ZD = z;
        }

        public void SetPosition(Vector2 p, float t)
        {
            PD += p;
            PT = t;
        }

        public void SetRotation(float r, float t)
        {
            RD += r;
            RT = t;
        }

        public void SetZoom(float z, float t)
        {
            ZD += z;
            ZT = t;

            if (ZD <= 0)
                ZD = 0;
        }

        public void DrawBack(SpriteBatch spriteBatch)
        {
            Para.DrawBack(spriteBatch);
        }

        public void DrawFront(SpriteBatch spriteBatch)
        {
            Para.DrawFront(spriteBatch);
        }

        public List<string> Write()
        {
            List<string> Info = new List<string>();

            Info.Add("World Position X: " + WorldPosition.X);
            Info.Add("World Position Y: " + WorldPosition.Y);
            Info.Add("World Position Z: " + WorldPosition.Z);
            Info.Add("");
            Info.Add("Position X: " + P.X);
            Info.Add("Position Y: " + P.Y);
            Info.Add("Rotation: " + R);
            Info.Add("Zoom: " + Z);
            Info.Add("");
            Info.Add("Position Destination X: " + PD.X);
            Info.Add("Position Destination Y:" + PD.Y);
            Info.Add("Rotation Destination: " + RD);
            Info.Add("Zoom Destination: " + ZD);
            Info.Add("");
            Info.Add("Position Speed: " + PT);
            Info.Add("Rotation Speed: " + RT);
            Info.Add("Zoom Speed: " + ZT);

            return Info;
        }
    }
}
