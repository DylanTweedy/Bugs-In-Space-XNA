//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

//namespace DevStomp
//{
//    static class Projectiles
//    {
//        static public int Actions = 33;
//        static public int CollisionModifier = 2;

//        public class Projectile
//        {


//            public Vector2 Position;
//            public Vector2 StartingPosition;
//            public Vector2 Velocity;
//            public Vector2 Acceleration;
//            public float Damage;
//            public float StartingDamage;
//            public bool Collision;
//            public List<int> CollisionModifier;
//            public List<int> Actions;
//            public Vector2 Size;
//            public int SizeType;
//            public bool Active;
//            public int SizeID;
//            public int ID;
//            public float Time;
//            public Rectangle rect;
//            public Color color;
//            public float rotation;
//            public byte Owner;
//            public float AbsalouteSize;

//            public bool Laser;
//            public List<Vector2> LaserPos;
//            public List<Rectangle> LaserRect;
//            public List<Vector2> LaserSize;
//            public List<Color> LaserColor;
//            public List<float> LaserRotations;
//            public List<float> LaserAbsalouteSize;

//            public float DistanceTravelled = 0f;
//            public float Distance2 = 0f;
//            public int CollisionCount = 0;
//            public float RotationalAcceleration = 0f;
//            public float orbitRadius = 300f;
//            public float orbitRadian = 0f;
//            public float orbitSpeed = 0f;
//            public float orbitRadius2 = 300f;
//            public float orbitRadian2 = 0f;
//            public float orbitSpeed2 = 0f;

//            public Vector2 orbitPosition3 = Vector2.Zero;
//            public Vector2 orbitPPosition3 = Vector2.Zero;
//            public float orbitRadius3 = 300f;
//            public float orbitRadian3 = 0f;
//            public float orbitSpeed3 = 0f;
//            public Vector2 orbitPosition4 = Vector2.Zero;
//            public Vector2 orbitPPosition4 = Vector2.Zero;
//            public float orbitRadius4 = 300f;
//            public float orbitRadian4 = 0f;
//            public float orbitSpeed4 = 0f;
//            public bool CUP = true;

//            public Projectile(Vector2 pos, Vector2 vel, float dam, Vector2 size, byte sizeType, int id, Color c, bool laser, List<int> actions, List<int> collisions, byte owner)
//            {
//                Owner = owner;
//                Active = true;
//                Position = pos;
//                Velocity = vel;
//                Damage = dam;
//                StartingDamage = Damage;
//                Size = size;
//                SizeID = id;
//                CollisionModifier = collisions;
//                Actions = actions;
//                Time = 0f;
//                Collision = false;
//                color = ColorManager.FullBrightness(c)[1];
//                rotation = 0f;
//                Laser = laser;
//                AbsalouteSize = Vector2.Distance(new Vector2(Size.X, 0), new Vector2(0, Size.Y));

//                if (Laser)
//                {
//                    LaserPos = new List<Vector2>();
//                    LaserRect = new List<Rectangle>();
//                    LaserSize = new List<Vector2>();
//                    LaserColor = new List<Color>();
//                    LaserRotations = new List<float>();
//                    LaserAbsalouteSize = new List<float>();
//                    Damage *= 10f * (float)WorldVariables.FrameTime;
//                }

//                switch (sizeType)
//                {
//                    case 0:
//                        SizeType = 16;
//                        break;
//                    case 1:
//                        SizeType = 32;
//                        break;
//                    case 2:
//                        SizeType = 64;
//                        break;
//                }

//                rect = new Rectangle((int)(pos.X - ((SizeType * 0.9f * Size.X) / 2)), (int)(pos.Y - ((SizeType * 0.9f * Size.Y) / 2)), (int)(SizeType * 0.9f * Size.X), (int)(SizeType * 0.9f * Size.Y));
//                StartingPosition = Position;
//            }

//            public void Update()
//            {
//                if (Laser)
//                {
//                    int length = (int)Vector2.Distance(Velocity * 50f, Vector2.Zero);
//                    Velocity /= 3f;

//                    for (int i = 0; i < length; i++)
//                    {
//                        if (!Collision && Active)
//                        {
//                            UpdateStuff();
//                            LaserPos.Add(Position);
//                            LaserRect.Add(rect);
//                            LaserSize.Add(Size);
//                            LaserColor.Add(color);
//                            LaserRotations.Add(rotation);
//                            LaserAbsalouteSize.Add(AbsalouteSize);
//                        }
//                    }

//                    Active = false;
//                }
//                else
//                    UpdateStuff();
//            }

//            private void UpdateStuff()
//            {
//                rect.X = (int)(Position.X - ((SizeType * 0.9f * Size.X) / 2));
//                rect.Y = (int)(Position.Y - ((SizeType * 0.9f * Size.Y) / 2));
//                rect.Width = (int)(SizeType * 0.9f * Size.X);
//                rect.Height = (int)(SizeType * 0.9f * Size.Y);

//                Acceleration = Vector2.Zero;
//                Time += (float)WorldVariables.FrameTime;
//                Damage = StartingDamage * (1 - (Time / 120f));

//                ProjectileAction();
//                Physics();
//                CheckCollision();
//                CollisionAction();

//                AbsalouteSize = Vector2.Distance(new Vector2(Size.X, 0), new Vector2(0, Size.Y));

//                float maxVel = 40f;
//                if (Velocity.X > maxVel)
//                    Velocity.X = maxVel;
//                else if (Velocity.X < -maxVel)
//                    Velocity.X = -maxVel;

//                if (Velocity.Y > maxVel)
//                    Velocity.Y = maxVel;
//                else if (Velocity.Y < -maxVel)
//                    Velocity.Y = -maxVel;

//                if (Damage <= 0)
//                    Active = false;
//            }

//            private void Physics()
//            {
//                Velocity += Acceleration;
//                DistanceTravelled += Vector2.Distance(Velocity + Position, Position);
//                Distance2 += Vector2.Distance(Velocity + Position, Position);
//                Position += Velocity;
//            }

//            private void CheckCollision()
//            {
//                if (Position.X + ((SizeType / 2) * Size.X * 0.9f) > WorldVariables.Boundry / 2)
//                    Collision = true;
//                else if (Position.X - ((SizeType / 2) * Size.X * 0.9f) < -WorldVariables.Boundry / 2)
//                    Collision = true;

//                if (Position.Y + ((SizeType / 2) * Size.Y * 0.9f) > 0)
//                    Collision = true;
//                else if (Position.Y - ((SizeType / 2) * Size.Y * 0.9f) < -WorldVariables.Boundry)
//                    Collision = true;

//                for (int i = 0; i < LifeManager.Entities.Count; i++)
//                    if (LifeManager.Entities[i].health > 0)
//                        if (LifeManager.Entities[i].rect.Intersects(rect))
//                        {
//                            float dam = Damage / Vector2.Distance(Size, Vector2.Zero);

//                            if (LifeManager.Entities[i].CS != Owner)
//                            {
//                                Collision = true;
//                                if (WorldVariables.RandomNumber.Next(0, 100) == 0)
//                                {
//                                    Particles.AddParticles(Position - new Vector2(-25, -25), 100, 2, 0.2f);
//                                    Particles.AddParticles(Position - new Vector2(25, 25), 100, 2, 0.2f);
//                                    Particles.AddParticles(Position, 100, 2, 0.2f);
//                                    Particles.AddParticles(Position - new Vector2(-25, 25), 100, 2, 0.2f);
//                                    Particles.AddParticles(Position - new Vector2(25, -25), 100, 2, 0.2f);

//                                    dam *= 4;
//                                }
//                                LifeManager.Entities[i].health -= dam;
//                            }
//                            else if (Time > 0.5f)
//                            {
//                                Collision = true;
//                                if (WorldVariables.RandomNumber.Next(0, 100) == 0)
//                                {
//                                    Particles.AddParticles(Position - new Vector2(-25, -25), 100, 2, 0.2f);
//                                    Particles.AddParticles(Position - new Vector2(25, 25), 100, 2, 0.2f);
//                                    Particles.AddParticles(Position, 100, 2, 0.2f);
//                                    Particles.AddParticles(Position - new Vector2(-25, 25), 100, 2, 0.2f);
//                                    Particles.AddParticles(Position - new Vector2(25, -25), 100, 2, 0.2f);

//                                    dam *= 4;
//                                }
//                                LifeManager.Entities[i].health -= dam;
//                            }

//                            if (LifeManager.Entities[i].health <= 0)
//                                for (int o = 0; o < LifeManager.Entities.Count; o++)
//                                {
//                                    if (LifeManager.Entities[o].CS == Owner && o != i)
//                                        LifeManager.Entities[o].Kills++;
//                                }
//                        }
//            }

//            private void ProjectileAction()
//            {
//                for (int i = 0; i < Actions.Count; i++)
//                {
//                    bool JustSpawned = false;
//                    byte Alpha = color.A;
//                    if (Position == StartingPosition)
//                        JustSpawned = true;
//                    int o;
//                    int e;
//                    float distX;
//                    float distY;

//                    switch (Actions[i])
//                    {
//                        case 0:
//                            if (DistanceTravelled > 512)
//                            {
//                                if (WorldVariables.RandomNumber.Next(0, 20) == 0)
//                                    Velocity.X *= -1;
//                                if (WorldVariables.RandomNumber.Next(0, 20) == 0)
//                                    Velocity.Y *= -1;
//                                if (WorldVariables.RandomNumber.Next(0, 5) == 0)
//                                {
//                                    float y = Velocity.Y;
//                                    Velocity.Y = Velocity.X;
//                                    Velocity.X = y;
//                                }
//                            }
//                            break;

//                        case 1:
//                            if (Laser)
//                                Acceleration.Y = 0.02f;
//                            else
//                                Acceleration.Y = 0.2f;
//                            break;

//                        case 2:
//                            Size *= 1.05f;
//                            if (Size.X > 2.5f || Size.Y > 2.5f)
//                                Size /= 5f;
//                            break;

//                        case 3:
//                            Size *= 0.95f;
//                            if (Size.X < 0.75f || Size.Y < 0.75f)
//                                Size *= 5f;
//                            break;

//                        case 4:
//                            Size.X *= 0.95f;
//                            if (Size.X < 0.5f)
//                                Size.X *= 5f;
//                            break;

//                        case 5:
//                            Size.X *= 1.05f;
//                            if (Size.X > 5f)
//                                Size.X /= 5f;
//                            break;

//                        case 6:
//                            Size.Y *= 0.95f;
//                            if (Size.Y < 0.5f)
//                                Size.Y *= 5f;
//                            break;

//                        case 7:
//                            Size.Y *= 1.05f;
//                            if (Size.Y > 5f)
//                                Size.Y /= 5f;
//                            break;

//                        case 8:
//                            rotation += (float)(Math.PI * WorldVariables.FrameTime);
//                            break;

//                        case 9:
//                            rotation -= (float)(Math.PI * WorldVariables.FrameTime);
//                            break;

//                        case 10:
//                            RotationalAcceleration += (float)((Math.PI / 2) * WorldVariables.FrameTime);
//                            rotation += RotationalAcceleration;
//                            break;

//                        case 11:
//                            RotationalAcceleration -= (float)((Math.PI / 2) * WorldVariables.FrameTime);
//                            rotation += RotationalAcceleration;
//                            break;

//                        case 12:
//                            if (JustSpawned)
//                            {
//                                if (WorldVariables.RandomNumber.Next(0, 2) == 0)
//                                    Velocity.X *= -1f;
//                                if (WorldVariables.RandomNumber.Next(0, 2) == 0)
//                                    Velocity.Y *= -1f;
//                            }
//                            break;

//                        case 13:
//                            if (JustSpawned)
//                            {
//                                if (WorldVariables.RandomNumber.Next(0, 2) == 0)
//                                    Velocity *= -1f;
//                            }
//                            break;

//                        case 14:
//                            if (JustSpawned)
//                            {
//                                if (WorldVariables.RandomNumber.Next(0, 2) == 0)
//                                {
//                                    float angle = (float)Math.Atan2(Velocity.X, Velocity.Y);
//                                    angle += (float)(Math.PI / 8);
//                                    Velocity = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * Vector2.Distance(Velocity, Vector2.Zero);
//                                }
//                                else if (WorldVariables.RandomNumber.Next(0, 2) == 0)
//                                {
//                                    float angle = (float)Math.Atan2(Velocity.X, Velocity.Y);
//                                    angle -= (float)(Math.PI / 8);
//                                    Velocity = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * Vector2.Distance(Velocity, Vector2.Zero);
//                                }
//                            }
//                            break;

//                        case 15:
//                            orbitSpeed = Vector2.Distance(Vector2.Zero, Velocity) * 10f * (float)WorldVariables.FrameTime;
//                            orbitRadius += 50f * (float)WorldVariables.FrameTime;
//                            if (JustSpawned)
//                                orbitRadian = -(float)Math.Atan2(Velocity.X, Velocity.Y) + (float)(Math.PI / 2f);

//                            Position = StartingPosition;
//                            Position += new Vector2(orbitRadius * (float)Math.Cos(orbitRadian), orbitRadius * (float)Math.Sin(orbitRadian));
//                            orbitRadian += (orbitSpeed * (float)WorldVariables.FrameTime) * 0.9f;
//                            break;

//                        case 16:
//                            orbitSpeed2 = Vector2.Distance(Vector2.Zero, Velocity) * 10f * (float)WorldVariables.FrameTime;
//                            orbitRadius2 += 50f * (float)WorldVariables.FrameTime;
//                            if (JustSpawned)
//                                orbitRadian2 = -(float)Math.Atan2(Velocity.X, Velocity.Y) + (float)(Math.PI / 2f);

//                            Position = StartingPosition;
//                            Position += new Vector2(orbitRadius2 * (float)Math.Cos(orbitRadian2), orbitRadius2 * (float)Math.Sin(orbitRadian2));
//                            orbitRadian2 -= (orbitSpeed2 * (float)WorldVariables.FrameTime) * 1.1f;
//                            break;

//                        case 17:
//                            if (CUP)
//                            {
//                                color.R += 25;
//                                color.G += 25;
//                                color.B += 25;
//                            }
//                            else
//                            {
//                                color.R -= 25;
//                                color.G -= 25;
//                                color.B -= 25;
//                            }

//                            if (color.R == 255 && color.G == 255 && color.B == 255)
//                                CUP = false;
//                            else if (color.R == 0 && color.G == 0 && color.B == 0)
//                                CUP = true;

//                            color.A = Alpha;
//                            break;

//                        case 18:
//                            color = ColorManager.Compliment(color)[1];
//                            break;

//                        case 19:
//                            color = ColorManager.RandomFullColor();
//                            break;

//                        case 20:
//                            if (Laser)
//                                Acceleration.Y = -0.02f;
//                            else
//                                Acceleration.Y = -0.2f;
//                            break;

//                        case 21:
//                            if (Laser)
//                                Acceleration.X = 0.02f;
//                            else
//                                Acceleration.X = 0.2f;
//                            break;


//                        case 22:
//                            if (Laser)
//                                Acceleration.X = -0.02f;
//                            else
//                                Acceleration.X = -0.2f;
//                            break;

//                        case 23:
//                            distX = -Position.X;
//                            distY = -(WorldVariables.Boundry / 2) - Position.Y;

//                            Acceleration.X += distX * (float)WorldVariables.FrameTime * 0.1f;
//                            Acceleration.Y += distY * (float)WorldVariables.FrameTime * 0.1f;

//                            if (Laser)
//                                Acceleration *= 0.1f;
//                            break;

//                        case 24:
//                            Velocity *= 1.01f;
//                            break;

//                        case 25:
//                            Velocity *= 0.99f;
//                            break;

//                        case 26:
//                            if (JustSpawned)
//                                for (o = 0; o < LifeManager.Entities.Count; o++)
//                                {
//                                    if (Owner == LifeManager.Entities[o].CS)
//                                        LifeManager.Entities[o].Position -= Velocity;
//                                }
//                            break;

//                        case 27:
//                            for (e = 0; e < Projectiles.P.Count; e++)
//                            {
//                                if (ID != e && Time > 0.5f && Projectiles.P[e].Active)
//                                    if (Projectiles.P[e].Laser)
//                                    {
//                                        for (o = 0; o < Projectiles.P[e].LaserRect.Count; o++)
//                                        {
//                                            //if (rect.Intersects(Projectiles.P[e].rect))
//                                            if (rect.Intersects(Projectiles.P[e].LaserRect[o]))
//                                            {
//                                                Damage += Projectiles.P[e].Damage;
//                                                Projectiles.P[e].Damage += Damage;


//                                                Size *= 10f;
//                                                Projectiles.P[e].LaserSize[o] *= 10f;

//                                                if (Laser)
//                                                    Collision = true;

//                                                if (o + 1 < Projectiles.P[e].LaserRect.Count)
//                                                {
//                                                    Projectiles.P[e].LaserRect.RemoveRange(o + 1, Projectiles.P[e].LaserRect.Count - o - 1);
//                                                    Projectiles.P[e].LaserPos.RemoveRange(o + 1, Projectiles.P[e].LaserPos.Count - o - 1);
//                                                    Projectiles.P[e].LaserColor.RemoveRange(o + 1, Projectiles.P[e].LaserColor.Count - o - 1);
//                                                    Projectiles.P[e].LaserRotations.RemoveRange(o + 1, Projectiles.P[e].LaserRotations.Count - o - 1);
//                                                    Projectiles.P[e].LaserSize.RemoveRange(o + 1, Projectiles.P[e].LaserSize.Count - o - 1);
//                                                }
//                                            }
//                                        }
//                                    }
//                                    else if (rect.Intersects(Projectiles.P[e].rect))
//                                    {
//                                        float s1 = AbsalouteSize;
//                                        float s2 = Projectiles.P[e].AbsalouteSize;

//                                        if (s1 > s2 && !Laser && s1 != s2 && s1 + s2 < 40f)
//                                        {
//                                            Damage += Projectiles.P[e].Damage;
//                                            Size += Projectiles.P[e].Size;
//                                            Projectiles.P[e].Active = false;
//                                            Projectiles.P[e].Collision = true;
//                                        }
//                                        else if (s1 + s2 < 40f)
//                                        {
//                                            Projectiles.P[e].Damage += Damage;
//                                            Projectiles.P[e].Size += Size;

//                                            if (!Laser)
//                                                Active = false;

//                                            Collision = true;
//                                        }
//                                        else
//                                            Collision = true;
//                                    }
//                            }
//                            break;


//                        case 28:
//                            orbitSpeed3 = Vector2.Distance(Vector2.Zero, Velocity) * 30f * (float)WorldVariables.FrameTime;
//                            orbitRadius3 = 200f;
//                            if (JustSpawned)
//                                orbitRadian3 = -(float)Math.Atan2(Velocity.X, Velocity.Y) + (float)(Math.PI / 2f);

//                            orbitPPosition3 = orbitPosition3;
//                            orbitPosition3 = Vector2.Zero;
//                            orbitPosition3 += new Vector2(orbitRadius3 * (float)Math.Cos(orbitRadian3), orbitRadius3 * (float)Math.Sin(orbitRadian3));
//                            orbitRadian3 += (orbitSpeed3 * (float)WorldVariables.FrameTime);

//                            Position += orbitPosition3 - orbitPPosition3;
//                            break;

//                        case 29:
//                            orbitSpeed4 = Vector2.Distance(Vector2.Zero, Velocity) * 30f * (float)WorldVariables.FrameTime;
//                            orbitRadius4 = 200f;
//                            if (JustSpawned)
//                                orbitRadian4 = -(float)Math.Atan2(Velocity.X, Velocity.Y) + (float)(Math.PI / 2f);

//                            orbitPPosition4 = orbitPosition4;
//                            orbitPosition4 = Vector2.Zero;
//                            orbitPosition4 += new Vector2(orbitRadius4 * (float)Math.Cos(orbitRadian4), orbitRadius4 * (float)Math.Sin(orbitRadian4));
//                            orbitRadian4 -= (orbitSpeed4 * (float)WorldVariables.FrameTime);

//                            Position += orbitPosition4 - orbitPPosition4;
//                            break;

//                        case 30:
//                            if (WorldVariables.RandomNumber.Next(0, 50) == 0)
//                            {
//                                List<Color> cl = new List<Color>();
//                                cl.Add(color);
//                                Particles.P.Add(new Particles.Particle(Position, WorldVariables.RandomNumber, cl, AbsalouteSize, 0.1f));
//                                //Particles.P[Particles.P.Count - 1].Velocity = new Vector2((float)(WorldVariables.RandomNumber.NextDouble() * 2f) - 1f, (float)(WorldVariables.RandomNumber.NextDouble() * 2f) - 1f) * ((float)WorldVariables.RandomNumber.NextDouble() * 3f);

//                                Particles.P.Add(new Particles.Particle(Position, WorldVariables.RandomNumber, cl, AbsalouteSize, 0.1f));
//                                Particles.P.Add(new Particles.Particle(Position, WorldVariables.RandomNumber, cl, AbsalouteSize, 0.1f));
//                                Particles.P[Particles.P.Count - 1].Velocity = -Velocity * 0.2f;
//                                Particles.P[Particles.P.Count - 2].Velocity = -Velocity * 0.2f;
//                                Particles.P[Particles.P.Count - 3].Velocity = -Velocity * 0.2f;
//                            }
//                            break;

//                        case 31:
//                            distX = StartingPosition.X - Position.X;
//                            distY = StartingPosition.Y - Position.Y;

//                            Acceleration.X += distX * (float)WorldVariables.FrameTime * 0.1f;
//                            Acceleration.Y += distY * (float)WorldVariables.FrameTime * 0.1f;

//                            if (Laser)
//                                Acceleration *= 0.1f;
//                            break;

//                        case 32:
//                            for (o = 0; o < LifeManager.Entities.Count; o++)
//                            {
//                                distX = LifeManager.Entities[o].Position.X - Position.X;
//                                distY = LifeManager.Entities[o].Position.Y - Position.Y;

//                                Acceleration.X += distX * (float)WorldVariables.FrameTime * 0.1f;
//                                Acceleration.Y += distY * (float)WorldVariables.FrameTime * 0.1f;

//                                if (Laser)
//                                    Acceleration *= 0.1f;
//                            }
//                            break;
//                    }

//                    if (Size.X > 40f)
//                        Size.X = 40f;
//                    if (Size.Y > 40f)
//                        Size.Y = 40f;
//                    if (Size.X < 1f)
//                        Size.X = 1f;
//                    if (Size.Y < 1f)
//                        Size.Y = 1f;



//                }
//            }

//            private void CollisionAction()
//            {
//                if (Collision)
//                {
//                    //Position -= Velocity * 3f;

//                    for (int i = 0; i < CollisionModifier.Count; i++)
//                    {
//                        switch (CollisionModifier[i])
//                        {
//                            case 0:
//                                Particles.AddParticles(Position, 10, 0, 0.6f);
//                                Velocity *= -1f;
//                                break;

//                            case 1:
//                                Particles.AddParticles(Position, 10, 0, 0.6f);

//                                float angle = (float)Math.Atan2(Velocity.X, -Velocity.Y);

//                                if (angle > 0)
//                                    angle -= (float)Math.PI / 2f;
//                                else
//                                    angle += (float)Math.PI / 2f;

//                                Velocity = new Vector2((float)Math.Sin(angle) * 10f, -(float)Math.Cos(angle) * 10f);
//                                break;
//                        }
//                    }

//                    if (CollisionModifier.Count == 0 || CollisionCount > 60)
//                    {
//                        Particles.AddParticles(Position, 10, 0, 0.6f);
//                        Active = false;
//                    }

//                    Collision = false;
//                    CollisionCount++;
//                }
//            }
//        }

//        static public List<Projectile> P;

//        static public void Initialize()
//        {
//            P = new List<Projectile>();
//        }

//        static public void Update()
//        {
//            for (int i = 0; i < P.Count; i++)
//            {
//                P[i].ID = i;
//                if (!P[i].Active && P[i].Laser)
//                {
//                    P.RemoveAt(i);
//                    i--;
//                    continue;
//                }

//                P[i].Update();

//                if (!P[i].Active && !P[i].Laser)
//                {
//                    P.RemoveAt(i);
//                    i--;
//                }
//            }

//            Console.WriteLine(P.Count);
//        }

//        static public void Draw(SpriteBatch spriteBatch)
//        {
//            Texture2D t = StaticTests.Marker;
//            Rectangle r = new Rectangle();

//            for (int i = 0; i < P.Count; i++)
//            {
//                switch (P[i].SizeType)
//                {
//                    case 16:
//                        t = SM.PSmall.S(P[i].SizeID);
//                        r = SM.PSmall.Rectangles[P[i].SizeID];
//                        break;
//                    case 32:
//                        t = SM.PMedium.S(P[i].SizeID);
//                        r = SM.PMedium.Rectangles[P[i].SizeID];
//                        break;
//                    case 64:
//                        t = SM.PLarge.S(P[i].SizeID);
//                        r = SM.PLarge.Rectangles[P[i].SizeID];
//                        break;
//                }

//                if (P[i].Laser)
//                    for (int o = 0; o < P[i].LaserPos.Count; o++)
//                    {
//                        if (o != P[i].LaserPos.Count - 1)
//                        {
//                            float dis = Vector2.Distance(P[i].LaserPos[o], P[i].LaserPos[o + 1]) / (r.Width / 3f);

//                            if (Math.Ceiling(dis) > 1f)
//                            {
//                                Vector2 dif = P[i].LaserPos[o + 1] - P[i].LaserPos[o];

//                                for (int p = 0; p < dis; p++)
//                                {
//                                    spriteBatch.Draw(t, P[i].LaserPos[o] + ((dif / dis) * p), r, P[i].LaserColor[o], P[i].LaserRotations[o], new Vector2(r.Width / 2, r.Height / 2), P[i].LaserSize[o], SpriteEffects.None, 1f);
//                                }
//                            }
//                            else
//                                spriteBatch.Draw(t, P[i].LaserPos[o], r, P[i].LaserColor[o], P[i].LaserRotations[o], new Vector2(r.Width / 2, r.Height / 2), P[i].LaserSize[o], SpriteEffects.None, 1f);
//                        }
//                        else
//                        spriteBatch.Draw(t, P[i].LaserPos[o], r, P[i].LaserColor[o], P[i].LaserRotations[o], new Vector2(r.Width / 2, r.Height / 2), P[i].LaserSize[o], SpriteEffects.None, 1f);
//                    }
//                else
//                    spriteBatch.Draw(t, P[i].Position, r, P[i].color, P[i].rotation, new Vector2(r.Width / 2, r.Height / 2), P[i].Size, SpriteEffects.None, 1f);
//            }
//        }
//    }
//}
