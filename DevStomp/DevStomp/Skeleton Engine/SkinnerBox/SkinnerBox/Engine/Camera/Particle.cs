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
        public SkeletonTexture Tex;

        public Vector2 Position;
        public Vector2 TruePosition
        {
            get { return Position + PositionModifier; }
            set { Position = value - PositionModifier; }
        }

        public float Area
        {
            get { return Scale.X * Scale.Y; }
        }

        public Vector2 PreviousPosition;

        public Vector2 Scale;
        public float Alpha = 1f;
        public Color Tint = Color.White;
        public bool Glow;

        public Vector2 Acceleration;        
        public Vector2 Velocity;

        public float Rotation;
        public float RotationalVelocity;
        public bool RotateWithMovement;

        public bool Remove;

        public Vector2 PositionModifier;
        public float TimeAlive;

        public List<ParticleMovement> Movement = new List<ParticleMovement>();
        public List<Light> Lights = new List<Light>();

        public void Update()
        {
            PreviousPosition = TruePosition;

            Velocity += Acceleration * GlobalVariables.WorldTime;
            Position += Velocity * GlobalVariables.WorldTime;
            //Position += EnvironmentVelocity * GlobalVariables.WorldTime;

            Acceleration = Vector2.Zero;
            TimeAlive += GlobalVariables.WorldTime;


            if (RotateWithMovement)
            {
                float Rot = UsefulMethods.VectorToAngle(-(TruePosition - PreviousPosition));

                float diff = Rotation - Rot;

                if (diff > MathHelper.Pi)
                    diff -= MathHelper.TwoPi;
                else if (diff < -MathHelper.Pi)
                    diff += MathHelper.TwoPi;

                Rotation -= diff * GlobalVariables.WorldTime * 15f;

                if (Rotation > MathHelper.Pi)
                    Rotation -= MathHelper.TwoPi;
                else if (Rotation < -MathHelper.Pi)
                    Rotation += MathHelper.TwoPi;
            }
            else Rotation += RotationalVelocity * GlobalVariables.WorldTime;

        }
        
        public void DrawParticle(SpriteBatch spriteBatch)
        {
            if (Tex != null)
                Tex.Draw(TruePosition, Tint * Alpha, Rotation, Scale, SpriteEffects.None);

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
