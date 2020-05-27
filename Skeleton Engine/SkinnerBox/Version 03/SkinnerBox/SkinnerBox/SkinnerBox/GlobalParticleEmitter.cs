using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    class GlobalParticleEmitter
    {
        public class EmitterParticle
        {
            public Particle P;
            public float TimeToLive;
            public Vector2 StartSize;
            public Vector2 EndSize;

            public EmitterParticle(float timeToLive)
            {
                P = new Particle();
                TimeToLive = timeToLive;
            }
        }
        
        public List<EmitterParticle> Particles = new List<EmitterParticle>();
        float Timer;

        public void EmitParticles(List<SkeletonTexture> tex, List<Color> color, float ParticleCount, Vector2 Position, Random Rand, float timeToLive, float Strength, Vector2 StartSize, Vector2 EndSize, float Variant)
        {
            if (Timer > 0.017f)
            while (ParticleCount > 0f)
            {
                float variant = 1f + ((Variant / 2f) - ((float)Rand.NextDouble() * Variant));

                float test = 1f / 1.5f;

                if (ParticleCount >= 1f)
                {
                    Particles.Add(new EmitterParticle(timeToLive * variant));
                    Particles[Particles.Count - 1].P.Position = Position;

                    Vector2 Direction = Vector2.Transform(new Vector2(1f, 0f), Matrix.CreateRotationZ((float)Rand.NextDouble() * MathHelper.TwoPi));

                    Particles[Particles.Count - 1].P.Velocity = Direction * (Strength * variant);
                    Particles[Particles.Count - 1].P.Rotation = (float)Rand.NextDouble() * MathHelper.TwoPi;

                    Particles[Particles.Count - 1].P.Tex = tex[Rand.Next(0, tex.Count)];
                    Particles[Particles.Count - 1].P.Tint = color[Rand.Next(0, color.Count)];

                    Particles[Particles.Count - 1].StartSize = (StartSize * variant);
                    Particles[Particles.Count - 1].EndSize = (EndSize * variant);

                    ParticleCount--;
                }
                else if (Rand.NextDouble() > ParticleCount)
                    ParticleCount = 1f;
                else
                    ParticleCount = 0f;
            }
        }

        public void EmitParticles(List<SkeletonTexture> tex, List<Color> color, float ParticleCount, Vector2 Position, Random Rand, float timeToLive, float Strength, Vector2 Direction, float ConeSize, Vector2 StartSize, Vector2 EndSize, float Variant)
        {
            if (Timer > 0.017f)
            while (ParticleCount > 0f)
            {
                float variant = 1f + ((Variant / 2f) - ((float)Rand.NextDouble() * Variant));

                //float variant = 1f + (float)Rand.NextDouble();
                //if (Rand.Next(0, 2) == 0)
                //    variant = 1 / variant;

                if (ParticleCount >= 1f)
                {
                    Particles.Add(new EmitterParticle(timeToLive * variant));
                    Particles[Particles.Count - 1].P.Position = Position;

                    Direction = Vector2.Transform(Direction, Matrix.CreateRotationZ(((float)Rand.NextDouble() * (ConeSize * variant)) - ((ConeSize * variant) / 2f)));
                    if (Direction != Vector2.Zero)
                        Direction.Normalize();

                    Particles[Particles.Count - 1].P.Velocity = Direction * (Strength * variant);
                    Particles[Particles.Count - 1].P.Rotation = (float)Rand.NextDouble() * MathHelper.TwoPi;

                    Particles[Particles.Count - 1].P.Tex = tex[Rand.Next(0, tex.Count)];
                    Particles[Particles.Count - 1].P.Tint = color[Rand.Next(0, color.Count)];

                    Particles[Particles.Count - 1].StartSize = StartSize * variant;
                    Particles[Particles.Count - 1].EndSize = EndSize * variant;

                    ParticleCount--;
                }
                else if (Rand.NextDouble() > ParticleCount)
                    ParticleCount = 1f;
                else
                    ParticleCount = 0f;
            }
        }

        public void Update()
        {
            if (Timer > 0.017f)
                Timer = 0f;

            for (int i = 0; i < Particles.Count; i++)
            {
                float StartMod = UsefulMethods.FindBetween(Particles[i].P.TimeAlive, Particles[i].TimeToLive * 0.25f, 0f, 1f, 0f, false);
                float EndMod = UsefulMethods.FindBetween(Particles[i].P.TimeAlive, Particles[i].TimeToLive, Particles[i].TimeToLive * 0.75f, 1f, 0f, true);

                float FinalMod = StartMod;
                if (StartMod > EndMod)
                    FinalMod = EndMod;

                Particles[i].P.Alpha = FinalMod;
                
                if (Particles[i].StartSize.X > Particles[i].EndSize.X)
                    Particles[i].P.Scale.X = UsefulMethods.FindBetween(Particles[i].P.TimeAlive, Particles[i].TimeToLive, 0f, Particles[i].StartSize.X, Particles[i].EndSize.X, true);
                else
                    Particles[i].P.Scale.X = UsefulMethods.FindBetween(Particles[i].P.TimeAlive, Particles[i].TimeToLive, 0f, Particles[i].EndSize.X, Particles[i].StartSize.X, false);

                if (Particles[i].StartSize.Y > Particles[i].EndSize.Y)
                    Particles[i].P.Scale.Y = UsefulMethods.FindBetween(Particles[i].P.TimeAlive, Particles[i].TimeToLive, 0f, Particles[i].StartSize.Y, Particles[i].EndSize.Y, true);
                else
                    Particles[i].P.Scale.Y = UsefulMethods.FindBetween(Particles[i].P.TimeAlive, Particles[i].TimeToLive, 0f, Particles[i].EndSize.Y, Particles[i].StartSize.Y, false);
                

                Particles[i].P.Update();

                if (Particles[i].P.TimeAlive > Particles[i].TimeToLive)
                {
                    Particles.RemoveAt(i);
                    i--;
                }
            }

            Timer += GlobalVariables.WorldTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Count; i++)
                Particles[i].P.DrawParticle(spriteBatch);
        }
    }
}

