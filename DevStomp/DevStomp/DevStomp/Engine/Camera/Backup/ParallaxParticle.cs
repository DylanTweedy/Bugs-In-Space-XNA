using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    class ParallaxParticle
    {
        public Vector2 Position;
        public Vector2 PreviousPosition;
        public float Scale;
        public float Alpha;
        public float TextureScale;

        public float Speed;
        public float Angle;

        public bool Remove;

        public float SmallestScale;
        public float LargestScale;
        public float DrawScale;

        public bool Glow;

        public bool RotateWithMovement;
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 EnvironmentVelocity;

        public byte MoveType;

        public Texture2D Tex;
        public Color Tint;
    }

    class ParallaxParticleMovement
    {
        public void Update()
        {
        }
    }


}
