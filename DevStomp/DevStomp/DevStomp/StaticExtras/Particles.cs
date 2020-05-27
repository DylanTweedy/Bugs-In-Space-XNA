using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DevStomp
{
    static class Particles
    {
        public class Particle
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public int ID;
            public float time;
            public float Scale;
            public List<Color> c;
            public float LifeTime;

            public Particle(Vector2 pos, Random rand, List<Color> Cl, float scale, float lifeTime)
            {
                c = Cl;
                Position = pos;
                ID = rand.Next(0, SM.Particles.Elements);
                time = 0f;
                Scale = scale;
                LifeTime = lifeTime;
            }
        }

        static public List<Particle> P;
        static Random rand;

        static public void Initialize()
        {
            P = new List<Particle>();
            rand = new Random();
        }

        static public void AddParticles(Vector2 pos, int count, byte colorType, float lifeTime)
        {
            List<Color> Cl = new List<Color>();
            float scale = 1f;

            switch (colorType)
            {
                case 0:
                    Cl = ColorManager.GetShades(Color.DarkOrange, 0.8f);
                    Cl.AddRange(ColorManager.GetShades(Color.Red, 0.8f));
                    Cl.AddRange(ColorManager.GetShades(Color.Yellow, 0.8f));
                    scale = 1f;
                    break;

                case 1:
                    Cl = ColorManager.GetShades(Color.Blue, 0.8f);
                    Cl.AddRange(ColorManager.GetShades(Color.LightBlue, 0.8f));
                    Cl.AddRange(ColorManager.GetShades(Color.White, 0.8f));
                    scale = 10f;
                    break;

                case 2:
                    Cl = ColorManager.GetShades(Color.Red, 0.8f);
                    scale = 3.5f;
                    break;

                case 3:
                    Cl.Add(Color.White);
                    scale = 1.75f;
                    break;

                case 4:
                    Cl.AddRange(ColorManager.GetShades(Color.Purple, 0.8f));
                    scale = 3.75f;
                    break;

                case 5:
                    Cl.AddRange(ColorManager.GetShades(Color.Purple, 0.8f));
                    scale = 100f;
                    break;
            }

            
            for (int i = 0; i < count; i++)
            {
                P.Add(new Particle(pos, rand, Cl, scale + (float)(rand.NextDouble() - 0.5f), lifeTime));
                P[P.Count - 1].Velocity = new Vector2((float)(rand.NextDouble() * 2f) - 1f, (float)(rand.NextDouble() * 2f) - 1f) * ((float)rand.NextDouble() * 3f);
            }
        }

        static public void Update()
        {
            while (P.Count > 5000)
            {
                P.RemoveAt(rand.Next(0, P.Count));
            }

            for (int i = P.Count - 1; i >= 0; i--)
            {
                P[i].Position += P[i].Velocity;
                P[i].time += (float)GlobalVariables.FrameTime;

                if (P[i].time > P[i].LifeTime)
                {
                    P.RemoveAt(i);
                    i--;
                }
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < P.Count; i++)
            {
                Rectangle rect = SM.Particles.Rectangles[P[i].ID];
                float t = UsefulMethods.FindBetween(((P[i].LifeTime - P[i].time) + (float)GlobalVariables.FrameTime), P[i].LifeTime, 0f, 1f, 0f, false);
                spriteBatch.Draw(SM.Particles.S(P[i].ID), P[i].Position, rect, P[i].c[rand.Next(0, P[i].c.Count)] * t, 0f, new Vector2(rect.Width / 2, rect.Height / 2), P[i].Scale, SpriteEffects.None, 1f);
            }
        }
    }
}
