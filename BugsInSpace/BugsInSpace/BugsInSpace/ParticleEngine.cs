using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
namespace BugsInSpace
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        public List<Particle> particles;
        private List<Texture2D> textures;
        public int ParticleType;
        public Color color;
        public Color originalColor;
        public bool AddParticles;
        public bool MoveUp;
        public float scale;
        public int total;
        public Vector2 Velocity;

        public ParticleEngine(int particleType, Color ParticleColor, Vector2 location)
        {
            EmitterLocation = location;
            this.particles = new List<Particle>();
            random = new Random();
            ParticleType = particleType;
            color = ParticleColor;
            textures = new List<Texture2D>();
            AddParticles = true;
            MoveUp = false;
            originalColor = color;
            scale = 1f;
            total = 25;
            Velocity = Vector2.Zero;
        }

        public void LoadContent()
        {
            if (ParticleType == 1)
            {
                Texture2D texture1;
                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle1");
                textures.Add(texture1);
            }

            if (ParticleType == 2)
            {
                Texture2D texture1;
                Texture2D texture2;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle1");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle2");

                textures.Add(texture1);
                textures.Add(texture2);
            }

            if (ParticleType == 3)
            {
                Texture2D texture1;
                Texture2D texture2;
                Texture2D texture3;
                Texture2D texture4;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle1");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle2");
                texture3 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle3");
                texture4 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle9");

                textures.Add(texture1);
                textures.Add(texture2);
                textures.Add(texture3);
                textures.Add(texture4);
            }

            if (ParticleType == 4)
            {
                Texture2D texture1;
                Texture2D texture2;
                Texture2D texture3;
                Texture2D texture4;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle1");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle4");
                texture3 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle5");
                texture4 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle6");

                textures.Add(texture1);
                textures.Add(texture2);
                textures.Add(texture3);
                textures.Add(texture4);
            }

            if (ParticleType == 5)
            {
                Texture2D texture1;
                Texture2D texture2;
                Texture2D texture3;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle1");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle7");
                texture3 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle8");

                textures.Add(texture1);
                textures.Add(texture2);
                textures.Add(texture3);
            }

            if (ParticleType == 6)
            {
                Texture2D texture1;
                Texture2D texture2;
                Texture2D texture3;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle7");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle8");
                texture3 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle9");

                textures.Add(texture1);
                textures.Add(texture2);
                textures.Add(texture3);
            }

            if (ParticleType == 7)
            {
                Texture2D texture1;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle10");

                textures.Add(texture1);
            }


            if (ParticleType == 8)
            {
                Texture2D texture1;
                Texture2D texture2;
                Texture2D texture3;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle10");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle11");
                texture3 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle4");

                textures.Add(texture1);
                textures.Add(texture2);
                textures.Add(texture3);
            }

            if (ParticleType == 9)
            {
                Texture2D texture1;
                Texture2D texture2;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle10");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle12");

                textures.Add(texture1);
                textures.Add(texture2);
            }

            if (ParticleType == 10)
            {
                Texture2D texture1;
                Texture2D texture2;

                texture1 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle1");
                texture2 = Game1.Instance.Content.Load<Texture2D>("Images//Particles//Particle2");

                textures.Add(texture1);
                textures.Add(texture2);
            }
        }

        public void EmitterLocationUpdate(float XLocationMin, float XLocationMax, float YLocationMin, float YlocationMax)
        {
            EmitterLocation = new Vector2(random.Next((int)XLocationMin, (int)XLocationMax), random.Next((int)YLocationMin, (int)YlocationMax));
        }

        public void EmitterLocationUpdate(Vector2 Location)
        {
            EmitterLocation = Location;
        }

        public void Update()
        {
            if (ParticleType != 0)
            {
                if (AddParticles)
                {
                    for (int i = 0; i < total; i++)
                    {
                        particles.Add(GenerateNewParticle());
                    }
                }

                for (int particle = 0; particle < particles.Count; particle++)
                {
                    particles[particle].Update();
                    if (particles[particle].TTL <= 0)
                    {
                        particles.RemoveAt(particle);
                        particle--;
                    }
                }
            }
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = Vector2.Zero;
            if (MoveUp == false)
            {
                velocity = new Vector2(
                                        1f * (float)(random.NextDouble() * 2 - 1),
                                        1f * (float)(random.NextDouble() * 2 - 1));
            }
            if (MoveUp)
            {
                velocity = new Vector2(
                        0,
                        1f * (float)(random.NextDouble() * 3));
            }
            if (Velocity != Vector2.Zero)
            {
                velocity += Velocity;
            }

            if (ParticleType == 10)
                color = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            else
                color = new Color(random.Next(originalColor.R - 50, originalColor.R + 50),
                                random.Next(originalColor.G - 50, originalColor.G + 50),
                                random.Next(originalColor.B - 50, originalColor.B + 50));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble();
            int ttl = 5 + random.Next(10);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size * scale, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ParticleType != 0)
            {
                for (int index = 0; index < particles.Count; index++)
                {
                    particles[index].Draw(spriteBatch);
                }
            }
        }
    }
}
