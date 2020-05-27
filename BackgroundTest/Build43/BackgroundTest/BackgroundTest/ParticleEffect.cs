using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class ParticleEffect
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        List<Particle> Particles;
        private List<Texture2D> textures;
        public int ParticleType;
        public Color color;
        public Color originalColor;
        public bool AddParticles;
        public bool MoveUp;
        public float scale;
        public int total;
        public Vector2 Velocity;

        private class Particle
        {
            public byte ID { get; set; }
            public Vector2 Position { get; set; }
            public Vector2 Velocity { get; set; }
            public int Width;
            public int Height;
            public float Angle { get; set; }
            public float AngularVelocity { get; set; }
            public Color Color { get; set; }
            public float Size { get; set; }
            public int TTL { get; set; }

            public Particle(byte id, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int ttl, int width, int height)
            {
                ID = id;
                Position = position;
                Velocity = velocity;
                Angle = angle;
                AngularVelocity = angularVelocity;
                Color = color;
                Size = size;
                TTL = ttl;
                Width = width;
                Height = height;
            }

            public void Update()
            {
                TTL--;
                Position += Velocity;
                Angle += AngularVelocity;
            }

            public void Draw(SpriteBatch spriteBatch, Texture2D texture)
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, Width, Height);
                Vector2 origin = new Vector2(Width / 2, Height / 2);

                spriteBatch.Draw(texture, Position, sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
            }
        }

        public void Initialize(int particleType, Color ParticleColor, Vector2 location)
        {
            EmitterLocation = location;
            this.Particles = new List<Particle>();
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

        public void LoadContent(ContentManager Content)
        {
            if (ParticleType == 1)
            {
                Texture2D texture1;
                //texture1 = Content.Load<Texture2D>("Images//Particles//Particle1");
                texture1 = Content.Load<Texture2D>("Star1");
                textures.Add(texture1);
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
                        Particles.Add(GenerateNewParticle());
                    }
                }

                for (int particle = 0; particle < Particles.Count; particle++)
                {
                    Particles[particle].Update();
                    if (Particles[particle].TTL <= 0)
                    {
                        Particles.RemoveAt(particle);
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

            byte ID = 0;

            return new Particle(ID, position, velocity, angle, angularVelocity, color, size * scale, ttl, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ParticleType != 0)
            {
                for (int index = 0; index < Particles.Count; index++)
                {
                    Particles[index].Draw(spriteBatch, textures[Particles[index].ID]);
                }
            }
        }
    }
}
