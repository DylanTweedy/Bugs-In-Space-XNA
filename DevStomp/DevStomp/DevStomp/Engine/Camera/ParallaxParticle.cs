using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public class Particle
    {
        public Texture2D Tex;

        public Vector2 Position;
        public Vector2 PreviousPosition;

        public float Scale;
        public float Alpha;
        public Color Tint;
        public bool Glow;

        public Vector2 Acceleration;
        public Vector2 Velocity;
        public float Rotation;
        public bool RotateWithMovement;

        public bool Remove;

        public Vector2 PositionModifier;
        public float TimeAlive;

        public List<ParticleMovement> Movement = new List<ParticleMovement>();

        public void Update()
        {
            Velocity += Acceleration * GlobalVariables.WorldTime;
            Position += Velocity * GlobalVariables.WorldTime;
            //Position += EnvironmentVelocity * GlobalVariables.WorldTime;

            Acceleration = Vector2.Zero;
            TimeAlive += GlobalVariables.WorldTime;
        }


        //public byte MoveType;
    }
    
    class ParallaxParticle
    {
        public float MinDepth;
        public float MaxDepth;
        public float Depth;

        //public float DrawScale;
        //public Vector2 EnvironmentVelocity;
    }

}
