using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    class ParticleEmitter
    {
        public List<Particle> Particles = new List<Particle>();
        float ParticleCounter;

        List<SkeletonTexture> Textures = new List<SkeletonTexture>();

        List<Color> Tints = new List<Color>();

        float TimeToLive;
        float ConeSize;
        float Strength;
        int ParticlesPerSecond;
        Random Rand;
        public bool Active = true;

        public ParticleEmitter(Random rand, float timeToLive, float strength, int particlesPerSecond, float coneSize)
        {
            TimeToLive = timeToLive;
            Strength = strength;
            ParticlesPerSecond = particlesPerSecond;
            ConeSize = coneSize;
            Rand = rand;
        }

        public ParticleEmitter(Random rand, float timeToLive, float strength, int particlesPerSecond)
        {
            TimeToLive = timeToLive;
            Strength = strength;
            ParticlesPerSecond = particlesPerSecond;
            ConeSize = 0f;
            Rand = rand;
        }

        public void AddTextures(SkeletonTexture tex, Color color)
        {
            Textures.Add(tex);
            Tints.Add(color);
        }

        public void Update(Vector2 Position, Vector2 Direction)
        {
            if (Active)
            {
                ParticleCounter += ParticlesPerSecond * GlobalVariables.WorldTime;

                while (ParticleCounter > 1f)
                {
                    Particles.Add(new Particle());
                    Particles[Particles.Count - 1].Position = Position;

                    if (Direction != Vector2.Zero)
                    {
                        Vector2 direction = Direction;
                        direction = Vector2.Transform(direction, Matrix.CreateRotationZ(((float)Rand.NextDouble() * ConeSize) - (ConeSize / 2f)));

                        Particles[Particles.Count - 1].Velocity = Direction * Strength;
                    }
                    else
                    {
                        Vector2 direction = new Vector2(1f, 0f);
                        direction = Vector2.Transform(direction, Matrix.CreateRotationZ((float)Rand.NextDouble() * MathHelper.TwoPi));

                        Particles[Particles.Count - 1].Velocity = direction * Strength;
                    }

                    if (Textures != null)
                    if (Textures.Count > 0)
                    {
                        int s = Rand.Next(0, Textures.Count);

                        Particles[Particles.Count - 1].Tex = Textures[s];
                        Particles[Particles.Count - 1].Tint = Tints[s];
                    }

                    ParticleCounter = 0f;
                }
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                float StartMod = UsefulMethods.FindBetween(Particles[i].TimeAlive, TimeToLive * 0.25f, 0f, 1f, 0f, false);
                float EndMod = UsefulMethods.FindBetween(Particles[i].TimeAlive, TimeToLive, TimeToLive * 0.75f, 1f, 0f, true);

                float FinalMod = StartMod;
                if (StartMod > EndMod)
                    FinalMod = EndMod;

                Particles[i].Alpha = FinalMod;
                Particles[i].Scale = Vector2.One * 10;

                Particles[i].Update();

                if (Particles[i].TimeAlive > TimeToLive)
                {
                    Particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Count; i++)
                Particles[i].DrawParticle(spriteBatch);
        }
    }
}
