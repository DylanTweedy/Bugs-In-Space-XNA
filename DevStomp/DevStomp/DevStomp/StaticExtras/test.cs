

            //            case 14:
            //                if (JustSpawned)
            //                {
            //                    if (WorldVariables.RandomNumber.Next(0, 2) == 0)
            //                    {
            //                        float angle = (float)Math.Atan2(Velocity.X, Velocity.Y);
            //                        angle += (float)(Math.PI / 8);
            //                        Velocity = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * Vector2.Distance(Velocity, Vector2.Zero);
            //                    }
            //                    else if (WorldVariables.RandomNumber.Next(0, 2) == 0)
            //                    {
            //                        float angle = (float)Math.Atan2(Velocity.X, Velocity.Y);
            //                        angle -= (float)(Math.PI / 8);
            //                        Velocity = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * Vector2.Distance(Velocity, Vector2.Zero);
            //                    }
            //                }
            //                break;


            //            case 20:
            //                if (Laser)
            //                    Acceleration.Y = -0.02f;
            //                else
            //                    Acceleration.Y = -0.2f;
            //                break;

            //            case 21:
            //                if (Laser)
            //                    Acceleration.X = 0.02f;
            //                else
            //                    Acceleration.X = 0.2f;
            //                break;


            //            case 22:
            //                if (Laser)
            //                    Acceleration.X = -0.02f;
            //                else
            //                    Acceleration.X = -0.2f;
            //                break;

            //            case 23:
            //                distX = -Position.X;
            //                distY = -(WorldVariables.Boundry / 2) - Position.Y;
                            
            //                Acceleration.X += distX * (float)WorldVariables.FrameTime * 0.1f;
            //                Acceleration.Y += distY * (float)WorldVariables.FrameTime * 0.1f;

            //                if (Laser)
            //                    Acceleration *= 0.1f;
            //                break;

            //            case 24:
            //                Velocity *= 1.01f;
            //                break;

            //            case 25:
            //                Velocity *= 0.99f;
            //                break;

            //            case 26:
            //                if (JustSpawned)
            //                for (o = 0; o < LifeManager.Entities.Count; o++)
            //                {
            //                    if (Owner == LifeManager.Entities[o].CS)                                
            //                        LifeManager.Entities[o].Position -= Velocity;                                
            //                }
            //                break;

            //            case 27:
            //                for (e = 0; e < Projectiles.P.Count; e++)
            //                {
            //                    if (ID != e && Time > 0.5f && Projectiles.P[e].Active)
            //                        if (Projectiles.P[e].Laser)
            //                        {
            //                            for (o = 0; o < Projectiles.P[e].LaserRect.Count; o++)
            //                            {
            //                                //if (rect.Intersects(Projectiles.P[e].rect))
            //                                if (rect.Intersects(Projectiles.P[e].LaserRect[o]))
            //                                {
            //                                    Damage += Projectiles.P[e].Damage;
            //                                    Projectiles.P[e].Damage += Damage;


            //                                    Size *= 10f;
            //                                    Projectiles.P[e].LaserSize[o] *= 10f;

            //                                    if (Laser)                                                
            //                                        Collision = true;

            //                                    if (o + 1 < Projectiles.P[e].LaserRect.Count)
            //                                    {
            //                                        Projectiles.P[e].LaserRect.RemoveRange(o + 1, Projectiles.P[e].LaserRect.Count - o - 1);
            //                                        Projectiles.P[e].LaserPos.RemoveRange(o + 1, Projectiles.P[e].LaserPos.Count - o - 1);
            //                                        Projectiles.P[e].LaserColor.RemoveRange(o + 1, Projectiles.P[e].LaserColor.Count - o - 1);
            //                                        Projectiles.P[e].LaserRotations.RemoveRange(o + 1, Projectiles.P[e].LaserRotations.Count - o - 1);
            //                                        Projectiles.P[e].LaserSize.RemoveRange(o + 1, Projectiles.P[e].LaserSize.Count - o - 1);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        else if (rect.Intersects(Projectiles.P[e].rect))
            //                        {
            //                            float s1 = AbsalouteSize;
            //                            float s2 = Projectiles.P[e].AbsalouteSize;

            //                            if (s1 > s2 && !Laser && s1 != s2 && s1 + s2 < 40f)
            //                            {
            //                                Damage += Projectiles.P[e].Damage;
            //                                Size += Projectiles.P[e].Size;
            //                                Projectiles.P[e].Active = false;
            //                                Projectiles.P[e].Collision = true;
            //                            }
            //                            else if (s1 + s2 < 40f)
            //                            {
            //                                Projectiles.P[e].Damage += Damage;
            //                                Projectiles.P[e].Size += Size;

            //                                if (!Laser)
            //                                    Active = false;

            //                                Collision = true;
            //                            }
            //                            else
            //                                Collision = true;
            //                        }
            //                }
            //                break;



            //            case 30:
            //                if (WorldVariables.RandomNumber.Next(0, 50) == 0)
            //                {
            //                    List<Color> cl = new List<Color>();
            //                    cl.Add(color);
            //                    Particles.P.Add(new Particles.Particle(Position, WorldVariables.RandomNumber, cl, AbsalouteSize, 0.1f));
            //                    //Particles.P[Particles.P.Count - 1].Velocity = new Vector2((float)(WorldVariables.RandomNumber.NextDouble() * 2f) - 1f, (float)(WorldVariables.RandomNumber.NextDouble() * 2f) - 1f) * ((float)WorldVariables.RandomNumber.NextDouble() * 3f);

            //                    Particles.P.Add(new Particles.Particle(Position, WorldVariables.RandomNumber, cl, AbsalouteSize, 0.1f));
            //                    Particles.P.Add(new Particles.Particle(Position, WorldVariables.RandomNumber, cl, AbsalouteSize, 0.1f));
            //                    Particles.P[Particles.P.Count - 1].Velocity = -Velocity * 0.2f;
            //                    Particles.P[Particles.P.Count - 2].Velocity = -Velocity * 0.2f;
            //                    Particles.P[Particles.P.Count - 3].Velocity = -Velocity * 0.2f;
            //                }
            //                break;

            //            case 31:
            //                distX = StartingPosition.X - Position.X;
            //                distY = StartingPosition.Y - Position.Y;

            //                Acceleration.X += distX * (float)WorldVariables.FrameTime * 0.1f;
            //                Acceleration.Y += distY * (float)WorldVariables.FrameTime * 0.1f;

            //                if (Laser)
            //                    Acceleration *= 0.1f;
            //                break;

            //            case 32:
            //                for (o = 0; o < LifeManager.Entities.Count; o++)
            //                {
            //                    distX = LifeManager.Entities[o].Position.X - Position.X;
            //                    distY = LifeManager.Entities[o].Position.Y - Position.Y;

            //                    Acceleration.X += distX * (float)WorldVariables.FrameTime * 0.1f;
            //                    Acceleration.Y += distY * (float)WorldVariables.FrameTime * 0.1f;

            //                    if (Laser)
            //                        Acceleration *= 0.1f;
            //                }
            //                break;