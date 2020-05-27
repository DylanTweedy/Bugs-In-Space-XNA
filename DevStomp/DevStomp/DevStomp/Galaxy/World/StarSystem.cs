using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace DevStomp
{
    class StarSystem
    {
        Vector2 Position;
        List<World> Worlds;
        GraphicsDevice graphicsDevice;
        //Asteroids test;
        List<Asteroids> test2;
        Quadtree quad;

        float CenterMass;
        Vector2 MouseOrigin;

        public void Initialize(Vector2 position, GraphicsDevice graphics)
        {
            quad = new Quadtree(0, new Rectangle(0, 0, 5000, 5000));
            graphicsDevice = graphics;

            CenterMass = (2621440f * 30000f) * (2621440f * 30000f);

            Worlds = new List<World>();
            //AddWorlds();

            //test2 = new List<Asteroids>();

            //    test2.Add(new Asteroids());
            //    test2[0].Initialize(0.5f, new SpacePosition(), 8000f);            
        }

        private void AddWorlds()
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 0);
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 1);
            //}

            //for (int i = 0; i < 25; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 2);
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 3);
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 4);
            //}


            //for (int i = 0; i < 2; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 0);
            //}

            //for (int i = 0; i < 20; i++)
            //{
            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(new Vector2(2000000f, 0), graphicsDevice, Worlds.Count - 1, CenterMass, 4);
            //}


        }

        //OLDSTUFF
        Vector2 PP = Vector2.Zero;
        int a = 0;

        float b21;
        float b22;
        float b23;

        float b1;
        float b2;
        float b3;
                
        public void Update(GraphicsDevice graphics)
        {
            DebugOptions.DebugDisplay.Add("Object Count: " + Worlds.Count);

            //CameraManager.Cams[0].SetPosition(Worlds[0].State.P.Position + (UsefulMethods.AngleToVector(Worlds[0].State.Rotation) * (Worlds[0].State.Radius * 0.6f)));

            //CameraManager.Cams[0].P = Worlds[0].State.P.Position + (UsefulMethods.AngleToVector(Worlds[0].State.Rotation) * (Worlds[0].State.Radius * 0.6f));
            //CameraManager.Cams[0].PositionX = Worlds[0].State.P.PositionX;
            //CameraManager.Cams[0].PositionY = Worlds[0].State.P.PositionY;
            //CameraManager.Cams[0].R = -Worlds[0].State.Rotation;
            //CameraManager.Cams[0].R = 0;

            //if (DebugOptions.DebugInt != 0 && InputManager.M.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && InputManager.pM.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            //{
            //    Vector2 pos = InputManager.GetMousePosition(0);

            //    Worlds.Add(new World());
            //    Worlds[Worlds.Count - 1].Initialize(pos, graphicsDevice, Worlds.Count - 1, CenterMass, GlobalVariables.RandomNumber.Next(100, 1000000));
            //}

            if (DebugOptions.DebugInt != 0 && InputManager.M.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && InputManager.pM.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                MouseOrigin = InputManager.GetMousePosition(0);
            }

            if (DebugOptions.DebugInt != 0 && DebugOptions.DebugInt != 3 && InputManager.M.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && InputManager.pM.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Vector2 pos = InputManager.GetMousePosition(0);
                
                float dis = Vector2.Distance(pos, MouseOrigin);

                Worlds.Add(new World());
                //Worlds[Worlds.Count - 1].Initialize(MouseOrigin, graphicsDevice, Worlds.Count - 1, CenterMass, dis, Vector2.Zero, (float)GlobalVariables.RandomNumber.NextDouble());
                Worlds[Worlds.Count - 1].Initialize(MouseOrigin, graphicsDevice, Worlds.Count - 1, CenterMass, dis, Vector2.Zero, 0.5f);

                MouseOrigin = Vector2.Zero;
            }

            for (int i = 0; i < Worlds.Count; i++)
                Worlds[i].State.Acceleration = Vector2.Zero;

            float IMass;
            float OMass;


            if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Up))            
                GlobalVariables.TimeMultiplier *= 2f;
            
            else if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Down))            
                GlobalVariables.TimeMultiplier /= 2f;
            

            float CollisionMult = 0.75f;

            for (int i = 0; i < Worlds.Count; i++)
            {
                IMass = Worlds[i].State.ObjectMass;

                if (Worlds[i].State.P.RoughPosition != Worlds[i].State.P.RoughPosition)
                {
                    Worlds.RemoveAt(i);
                    continue;
                }

                for (int o = i + 1; o < Worlds.Count; o++)
                {
                    if (Worlds[o].State.P.RoughPosition != Worlds[o].State.P.RoughPosition)
                    {
                        Worlds.RemoveAt(o);
                        continue;
                    }

                    OMass = Worlds[o].State.ObjectMass;

                    if (Vector2.Distance(Worlds[i].State.P.RoughPosition, Worlds[o].State.P.RoughPosition) > (Worlds[i].State.ObjectRadius + Worlds[o].State.ObjectRadius) * CollisionMult)
                    {
                        Vector2 angvec = (Worlds[i].State.P.RoughPosition - Worlds[o].State.P.RoughPosition);
                        float r = angvec.Length();

                        float gravForce = GlobalVariables.GravitationalConstant * ((IMass * OMass) / (r * r));
                        float acc = (-gravForce / IMass);
                        float acc2 = (-gravForce / OMass);

                        angvec.Normalize();

                        Worlds[i].State.Acceleration += angvec * acc;
                        Worlds[o].State.Acceleration += -angvec * acc2;
                    }
                    else
                    {
                        Vector2 V1 = Worlds[i].State.Velocity;
                        Vector2 V2 = Worlds[o].State.Velocity;


                        float KE1 = 0.5f * (IMass * (V1.Length() * V1.Length()));
                        float KE2 = 0.5f * (OMass * (V2.Length() * V2.Length()));

                        float BE1 = 3f * (GlobalVariables.GravitationalConstant * (IMass * IMass));
                        float BE2 = 3f * (GlobalVariables.GravitationalConstant * (OMass * OMass));

                        BE1 /= GlobalVariables.TimeMultiplier;
                        BE2 /= GlobalVariables.TimeMultiplier;

                        float KE = KE1 + KE2;

                        float m1 = (KE2 / BE1) / 2f;
                        float m2 = (KE1 / BE2) / 2f;

                        //if (false)
                        {
                            #region Bounce

                            if (Worlds[i].State.Velocity != Worlds[i].State.Velocity || float.IsInfinity(Worlds[i].State.Velocity.X))
                                Worlds[i].State.Velocity = Vector2.Zero;
                            if (Worlds[o].State.Velocity != Worlds[o].State.Velocity || float.IsInfinity(Worlds[o].State.Velocity.X))
                                Worlds[o].State.Velocity = Vector2.Zero;

                            Vector2 Pos1 = Worlds[i].State.P.RoughPosition;
                            Vector2 Pos2 = Worlds[o].State.P.RoughPosition;

                            Vector2 collisionNormal = Pos1 - Pos2;
                            if (collisionNormal != Vector2.Zero)
                                collisionNormal *= 1f / collisionNormal.Length();

                            float Intersection = Vector2.Distance(Worlds[i].State.P.RoughPosition, Worlds[o].State.P.RoughPosition)
                            - ((Worlds[i].State.ObjectRadius + Worlds[o].State.ObjectRadius) * CollisionMult)
                            ;

                            float diff = Math.Abs(IMass - OMass);
                            diff = UsefulMethods.FindBetween(diff, IMass + OMass, 0f, 1f, 0f, false);

                            Worlds[i].State.P.Position -= collisionNormal * (Intersection * diff);
                            Worlds[o].State.P.Position += collisionNormal * (Intersection * (1 - diff));

                            if (false)
                            {
                                #region Transfer Angular Momentum

                                diff = Math.Abs(Worlds[i].State.ObjectMass - Worlds[o].State.ObjectMass);
                                diff = UsefulMethods.FindBetween(diff, Worlds[i].State.ObjectMass + Worlds[o].State.ObjectMass, 0f, 1f, 0f, false);

                                float ang1 = Worlds[i].State.AngularMomentum * (1 - diff);
                                float ang2 = ang1;

                                ang1 /= ((Worlds[i].State.ObjectMass * Worlds[i].State.ObjectRadius * Worlds[i].State.ObjectRadius) / 2f);
                                ang2 /= ((Worlds[o].State.ObjectMass * Worlds[o].State.ObjectRadius * Worlds[o].State.ObjectRadius) / 2f);

                                Worlds[i].State.AngularVelocity -= ang1;
                                Worlds[o].State.AngularVelocity += ang2;


                                ang1 = Worlds[o].State.AngularMomentum * (diff);
                                ang2 = ang1;

                                ang1 /= ((Worlds[i].State.ObjectMass * Worlds[i].State.ObjectRadius * Worlds[i].State.ObjectRadius) / 2f);
                                ang2 /= ((Worlds[o].State.ObjectMass * Worlds[o].State.ObjectRadius * Worlds[o].State.ObjectRadius) / 2f);

                                Worlds[i].State.AngularVelocity += ang1;
                                Worlds[o].State.AngularVelocity -= ang2;

                                #endregion
                            }

                            if (true)
                            {
                                //UsefulMethods.VectorToAngle(collisionNormal);



                                Vector2 CollisionPoint1 = (Pos1 - Pos2);
                                Vector2 CollisionStrength1 = V2;

                                CollisionPoint1.Normalize();
                                if (CollisionStrength1 != Vector2.Zero)
                                    CollisionStrength1.Normalize();

                                b1 = UsefulMethods.VectorToAngle(CollisionPoint1);
                                b2 = UsefulMethods.VectorToAngle(CollisionStrength1);
                                b3 = (b2 - b1);


                                Vector2 CollisionPoint2 = (Pos2 - Pos1);
                                Vector2 CollisionStrength2 = V1;

                                CollisionPoint2.Normalize();
                                if (CollisionStrength2 != Vector2.Zero)
                                    CollisionStrength2.Normalize();

                                b21 = UsefulMethods.VectorToAngle(CollisionPoint2);
                                b22 = UsefulMethods.VectorToAngle(CollisionStrength2);
                                b23 = (b22 - b21);

                                Vector2 k1 = V1 - V2;
                                                                
                                Vector2 k2 = V2 - V1;

                                Worlds[i].State.AngularVelocity = (k2.Length() * -(float)Math.Sin(b3)) / Worlds[i].State.ObjectRadius * diff;
                                Worlds[o].State.AngularVelocity = (k1.Length() * -(float)Math.Sin(b23)) / Worlds[o].State.ObjectRadius * (1 - diff);


                                if (Worlds[i].State.AngularVelocity != Worlds[i].State.AngularVelocity)
                                    Worlds[i].State.AngularVelocity = 0f;
                                if (Worlds[o].State.AngularVelocity != Worlds[o].State.AngularVelocity)
                                    Worlds[o].State.AngularVelocity = 0f;



                            }

                            if (false)
                            {
                                #region Rotation

                                Worlds[i].State.testAngular();
                                Worlds[o].State.testAngular();


                                //Vector2 CollisionPoint1 = (Pos1 - Pos2);
                                //Vector2 CollisionStrength1 = V2;

                                //CollisionPoint1.Normalize();
                                //if (CollisionStrength1 != Vector2.Zero)
                                //    CollisionStrength1.Normalize();

                                //float t1 = UsefulMethods.VectorToAngle(CollisionPoint1);
                                //float t2 = UsefulMethods.VectorToAngle(CollisionStrength1);
                                //float t3 = (t2 - t1) / ((float)Math.PI * 3000f);


                                //Vector2 CollisionPoint2 = (Pos2 - Pos1);
                                //Vector2 CollisionStrength2 = V1;

                                //CollisionPoint2.Normalize();
                                //if (CollisionStrength2 != Vector2.Zero)
                                //    CollisionStrength2.Normalize();

                                //float t21 = UsefulMethods.VectorToAngle(CollisionPoint2);
                                //float t22 = UsefulMethods.VectorToAngle(CollisionStrength2);
                                //float t23 = (t22 - t21) / ((float)Math.PI * 3000f);
                                
                                //Worlds[i].State.AngularVelocity += (-t3) * (1 - diff);
                                //Worlds[o].State.AngularVelocity += (t23) * diff;

                                //if (Worlds[i].State.AngularVelocity != Worlds[i].State.AngularVelocity)
                                //    Worlds[i].State.AngularVelocity = 0f;
                                //if (Worlds[o].State.AngularVelocity != Worlds[o].State.AngularVelocity)
                                //    Worlds[o].State.AngularVelocity = 0f;


                                //if (Worlds[i].State.AngularVelocity > ((float)Math.PI * 2f) / 60f)
                                //    Worlds[i].State.AngularVelocity = ((float)Math.PI * 2f) / 60f;
                                //else if (Worlds[i].State.AngularVelocity < -((float)Math.PI * 2f) / 60f)
                                //    Worlds[i].State.AngularVelocity = -((float)Math.PI * 2f) / 60f;


                                //if (Worlds[o].State.AngularVelocity > ((float)Math.PI * 2f) / 60f)
                                //    Worlds[o].State.AngularVelocity = ((float)Math.PI * 2f) / 60f;
                                //else if (Worlds[o].State.AngularVelocity < -((float)Math.PI * 2f) / 60f)
                                //    Worlds[o].State.AngularVelocity = -((float)Math.PI * 2f) / 60f;

                                #endregion
                            }


                            if (V1 != Vector2.Zero && V2 != Vector2.Zero)
                            {
                                //Decompose v1 in parallel and orthogonal part
                                var v1Dot = Vector2.Dot(collisionNormal, V1);
                                var v1Collide = collisionNormal * v1Dot;
                                var v1Remainder = V1 - v1Collide;

                                //Decompose v2 in parallel and orthogonal part
                                var v2Dot = Vector2.Dot(collisionNormal, V2);
                                var v2Collide = collisionNormal * v2Dot;
                                var v2Remainder = V2 - v2Collide;

                                var v1Length = v1Collide.Length() * Math.Sign(v1Dot);
                                var v2Length = v2Collide.Length() * Math.Sign(v2Dot);
                                var commonVelocity = 2 * (IMass * v1Length + OMass * v2Length) / (IMass + OMass);
                                var v1LengthAfterCollision = commonVelocity - v1Length;
                                var v2LengthAfterCollision = commonVelocity - v2Length;
                                v1Collide = v1Collide * (v1LengthAfterCollision / v1Length);
                                v2Collide = v2Collide * (v2LengthAfterCollision / v2Length);

                                Worlds[i].State.Velocity = (v1Collide + v1Remainder) * 1f;
                                Worlds[o].State.Velocity = (v2Collide + v2Remainder) * 1f;

                                if (Worlds[i].State.Velocity != Worlds[i].State.Velocity || float.IsInfinity(Worlds[i].State.Velocity.X))
                                    Worlds[i].State.Velocity = Vector2.Zero;
                                if (Worlds[o].State.Velocity != Worlds[o].State.Velocity || float.IsInfinity(Worlds[o].State.Velocity.X))
                                    Worlds[o].State.Velocity = Vector2.Zero;
                            }
                            
                            #endregion
                        }

                        if (false)
                        {
                            #region Merge New

                            if (IMass > OMass && (Worlds[i].CloudPercent < 0.9 && Worlds[o].CloudPercent < 0.9f))
                            {
                                for (int u = 0; u < Worlds[i].State.Chemistry.Count; u++)
                                {
                                    Worlds[i].State.Chemistry[u] += Worlds[o].State.Chemistry[u] / 10;
                                    Worlds[o].State.Chemistry[u] -= Worlds[o].State.Chemistry[u] / 10;
                                }

                                Worlds[i].ChemistryChange = true;
                                Worlds[o].ChemistryChange = true;

                                Worlds[o].CloudPercent = 1f;
                                Worlds.Add(new World());
                                Worlds[Worlds.Count - 1].Initialize(Worlds[o].State.P.RoughPosition, graphics, Worlds.Count - 1, 0f, 0f, Worlds[o].State.Velocity, 1f);
                                Worlds.Add(new World());
                                Worlds[Worlds.Count - 1].Initialize(Worlds[o].State.P.RoughPosition, graphics, Worlds.Count - 1, 0f, 0f, Worlds[o].State.Velocity, 1f);

                                for (int u = 0; u < Worlds[i].State.Chemistry.Count; u++)
                                {
                                    Worlds[Worlds.Count - 1].State.Chemistry[u] += Worlds[o].State.Chemistry[u] / 3;
                                    Worlds[Worlds.Count - 2].State.Chemistry[u] += Worlds[o].State.Chemistry[u] / 3;
                                    Worlds[o].State.Chemistry[u] = Worlds[o].State.Chemistry[u] / 3;
                                }

                                float angle = UsefulMethods.VectorToAngle(Worlds[o].State.Velocity);

                                Worlds[Worlds.Count - 1].State.Velocity = UsefulMethods.AngleToVector(angle + ((float)Math.PI * 0.25f));
                                Worlds[Worlds.Count - 2].State.Velocity = UsefulMethods.AngleToVector(angle - ((float)Math.PI * 0.25f));

                                Worlds[Worlds.Count - 1].State.P.Position += Worlds[Worlds.Count - 1].State.Velocity * 10f;
                                Worlds[Worlds.Count - 2].State.P.Position += Worlds[Worlds.Count - 2].State.Velocity * 10f;

                                Worlds[Worlds.Count - 1].ChemistryChange = true;
                                Worlds[Worlds.Count - 2].ChemistryChange = true;

                                Worlds[Worlds.Count - 1].Update(graphics);
                                Worlds[Worlds.Count - 2].Update(graphics);
                                Worlds[o].Update(graphics);
                            }
                            else if (Worlds[i].CloudPercent < 0.9 && Worlds[o].CloudPercent < 0.9f)
                            {
                                //for (int u = 0; u < Worlds[o].State.Chemistry.Count; u++)
                                //{
                                //    Worlds[o].State.Chemistry[u] += Worlds[o].State.Chemistry[u] / 10;
                                //    Worlds[i].State.Chemistry[u] -= Worlds[o].State.Chemistry[u] / 10;
                                //}

                                //Worlds[i].ChemistryChange = true;
                                //Worlds[o].ChemistryChange = true;

                                //Worlds[i].CloudPercent = 1f;
                                //Worlds.Add(new World());
                                //Worlds[Worlds.Count - 1].Initialize(Worlds[i].State.P.RoughPosition, graphics, Worlds.Count - 1, 0f, 0f, Worlds[i].State.Velocity, 1f);
                                //Worlds.Add(new World());
                                //Worlds[Worlds.Count - 1].Initialize(Worlds[i].State.P.RoughPosition, graphics, Worlds.Count - 1, 0f, 0f, Worlds[i].State.Velocity, 1f);

                                //for (int u = 0; u < Worlds[i].State.Chemistry.Count; u++)
                                //{
                                //    Worlds[Worlds.Count - 1].State.Chemistry[u] += Worlds[i].State.Chemistry[u] / 3;
                                //    Worlds[Worlds.Count - 2].State.Chemistry[u] += Worlds[i].State.Chemistry[u] / 3;
                                //    Worlds[i].State.Chemistry[u] = Worlds[i].State.Chemistry[u] / 3;
                                //}


                                //float angle = UsefulMethods.VectorToAngle(Worlds[i].State.Velocity);

                                //Worlds[Worlds.Count - 1].State.Velocity = UsefulMethods.AngleToVector(angle + ((float)Math.PI * 0.25f));
                                //Worlds[Worlds.Count - 2].State.Velocity = UsefulMethods.AngleToVector(angle - ((float)Math.PI * 0.25f));

                                //Worlds[Worlds.Count - 1].State.P.Position += Worlds[Worlds.Count - 1].State.Velocity;
                                //Worlds[Worlds.Count - 2].State.P.Position += Worlds[Worlds.Count - 2].State.Velocity;

                                //Worlds[Worlds.Count - 1].ChemistryChange = true;
                                //Worlds[Worlds.Count - 2].ChemistryChange = true;

                                //Worlds[Worlds.Count - 1].Update(graphics);
                                //Worlds[Worlds.Count - 2].Update(graphics);
                            }


                            #endregion
                        }



                        if (false)
                        {
                            #region Merge

                            float mult = 1f;

                            if (IMass > OMass)
                            {
                                //if (m1 < 1f)
                                //    mult = m1;

                                IMass += OMass * mult;
                                Worlds[i].State.Area += Worlds[o].State.Area * mult;
                                Worlds[i].State.ObjectRadius = (float)Math.Sqrt(Worlds[i].State.Area / Math.PI);
                                Worlds[i].SetWorldSize();

                                if (mult < 1f)
                                {
                                    OMass -= OMass * mult;
                                    Worlds[o].State.Area -= Worlds[o].State.Area * mult;
                                    Worlds[o].State.ObjectRadius = (float)Math.Sqrt(Worlds[o].State.Area / Math.PI);
                                    Worlds[o].SetWorldSize();
                                }
                                else
                                {
                                    for (int u = 0; u < Worlds[i].State.Chemistry.Count; u++)
                                        Worlds[i].State.Chemistry[u] += Worlds[o].State.Chemistry[u];
                                    Worlds.RemoveAt(o);
                                }

                                //Worlds[i].Velocity -= Worlds[o].Velocity * mult;
                                //Worlds[o].Velocity -= Worlds[i].Velocity * mult;
                            }
                            else
                            {
                                //if (m2 < 1f)
                                //    mult = m2;

                                OMass += IMass * mult;
                                Worlds[o].State.Area += Worlds[i].State.Area * mult;
                                Worlds[o].State.ObjectRadius = (float)Math.Sqrt(Worlds[o].State.Area / Math.PI);
                                Worlds[o].SetWorldSize();

                                if (mult < 1f)
                                {
                                    IMass -= IMass * mult;
                                    Worlds[i].State.Area -= Worlds[i].State.Area * mult;
                                    Worlds[i].State.ObjectRadius = (float)Math.Sqrt(Worlds[i].State.Area / Math.PI);
                                    Worlds[i].SetWorldSize();
                                }
                                else
                                {
                                    for (int u = 0; u < Worlds[o].State.Chemistry.Count; u++)
                                        Worlds[o].State.Chemistry[u] += Worlds[i].State.Chemistry[u];
                                    Worlds.RemoveAt(i);
                                }

                                //Worlds[o].Velocity -= Worlds[o].Velocity * mult;
                                //Worlds[i].Velocity -= Worlds[i].Velocity * mult;
                            }

                            #endregion
                        }
                    }
                }
            }



            DebugOptions.DebugDisplay.Add("Gravity Multiplier: " + GlobalVariables.TimeMultiplier);

            //if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.R))
            //    Worlds.RemoveAt(2);

            //if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.S))
            //    for (int i = 0; i < Worlds.Count; i++)
            //        Worlds[i].State.Velocity.Y = 0f;

            //if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.W))
            //    for (int i = 0; i < Worlds.Count; i++)
            //        Worlds[i].State.Velocity.Y += 1000f;

            float AllMass = 0f;
            float AllAngular = 0f;

            for (int i = 0; i < Worlds.Count; i++)
            {
                Worlds[i].Update(graphics);

                AllMass += Worlds[i].State.Mass;
                Worlds[i].State.testAngular();
                AllAngular += Math.Abs(Worlds[i].State.AngularMomentum);

                //if (GlobalVariables.RandomNumber.Next(0, 500) == 0 && Worlds[i].ObjectType == 3 && Worlds[i].WorldID != 0)
                //{
                //    float mult = 50f;

                //    //Worlds[i].Mass /= mult;

                //    for (int o = 0; o < Worlds[i].Chemistry.Count; o++)
                //    {
                //        Worlds[i].Chemistry[o] /= (long)mult;
                //    }

                //    //Worlds[i].SetWorldSize();
                //    //Worlds[i].Radius = (float)Math.Sqrt(Worlds[i].Area / Math.PI);


                //    for (int u = 1; u <= mult; u++)
                //    {
                //        float speed = 0.005f / GravityMultiplier;

                //        Worlds.Add(new World());
                //        Vector2 velocity = Worlds[i].Velocity;
                //        float angle = 360f / u;
                //        Vector2 move = new Vector2(speed, speed);

                //        angle += UsefulMethods.VectorToAngle(move);
                //        move = UsefulMethods.AngleToVector(angle) * speed;
                //        move.Normalize();


                //        Worlds[Worlds.Count - 1].Initialize(Worlds[i].P.RoughPosition + (move * (Worlds[i].Radius)), graphics, Worlds.Count - 1, CenterMass, 0, Vector2.Zero);
                //        for (int o = 0; o < Worlds[i].Chemistry.Count; o++)
                //            Worlds[Worlds.Count - 1].Chemistry[o] = Worlds[i].Chemistry[o];
                //        Worlds[Worlds.Count - 1].ChemistryChange = true;
                //        Worlds[Worlds.Count - 1].Update(graphics);
                //    }

                //    Worlds.RemoveAt(i);
                //    continue;
                //}
            }

            DebugOptions.DebugDisplay.Add("Objects Mass: " + AllMass);
            DebugOptions.DebugDisplay.Add("Angular Momentum: " + AllAngular);

            if (DebugOptions.DebugInt == 3 && InputManager.M.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && InputManager.pM.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                for (int i = Worlds.Count - 1; i >= 0; i--)
                    Worlds.RemoveAt(i);


        }
        
        

        //private void OLDUPDATE(GraphicsDevice graphics)
        //{
        //    //int x = 0;
        //    //float G = 6.67384e-11f;
        //    //int m1 = 0;
        //    //int m2 = 0;
        //    //int r = 0;

        //    //CameraManager.Cams[0].P = Worlds[1].P.Position;
        //    //CameraManager.Cams[0].PositionX = Worlds[1].P.PositionX;
        //    //CameraManager.Cams[0].PositionY = Worlds[1].P.PositionY;

        //    //CameraManager.Cams[0].SetPosition(Worlds[1].P.Position);
        //    //CameraManager.Cams[0].PositionX = Worlds[1].P.PositionX;
        //    //CameraManager.Cams[0].PositionY = Worlds[1].P.PositionY;

        //    //SetupSystem();

        //    GlobalVariables.GravitationalConstant = 6.67384e-11f;
        //    //* 100000000f * 100000000f;

        //    //for (int o = 0; o < 100; o++)
        //    for (int i = 0; i < Worlds.Count; i++)
        //    {
        //        //SpacePosition p = new SpacePosition();
        //        //p.Position = Vector2.Zero;
        //        //float r = Vector2.Distance(p.GetRough(), Worlds[i].P.GetRough());

        //        //float gravForce = WorldVariables.GravitationalConstant * (((float)Worlds[i].Mass * CenterMass) / (r * r));
        //        //float acc = (-gravForce / (float)Worlds[i].Mass);

        //        //float angle = (float)Math.Atan2(Worlds[i].P.GetRough().X - p.GetRough().X, -Worlds[i].P.GetRough().Y - p.GetRough().Y);
        //        //Vector2 angvec = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));

        //        //angvec *= acc;

        //        //Worlds[i].Acceleration = angvec;



        //        //Worlds[i].Mass = 1.8E+11f;

        //        Worlds[i].State.Acceleration = Vector2.Zero;

        //        //Worlds[i].CenterMass = (float)Worlds[0].Mass + (float)Worlds[1].Mass + (float)Worlds[2].Mass + (float)Worlds[3].Mass;
        //        //Worlds[i].CenterMass = (float)Worlds[0].Mass + (float)Worlds[1].Mass;
        //        Worlds[i].CenterMass = (float)Worlds[0].State.Mass;

        //    }




        //    for (int i = 0; i < Worlds.Count; i++)
        //        for (int o = i + 1; o < Worlds.Count; o++)
        //        {
        //            if (Vector2.Distance(Worlds[i].P.RoughPosition, Worlds[o].P.RoughPosition) > Worlds[i].State.Radius + Worlds[o].State.Radius)
        //            {
        //                float r = Vector2.Distance(Worlds[o].P.RoughPosition, Worlds[i].P.RoughPosition);

        //                float gravForce = GlobalVariables.GravitationalConstant * (((float)Worlds[i].State.Mass * (float)Worlds[o].State.Mass) / (r * r));
        //                float acc = (-gravForce / (float)Worlds[i].State.Mass);
        //                float acc2 = (-gravForce / (float)Worlds[o].State.Mass);

        //                float angle = (float)Math.Atan2(Worlds[i].P.RoughPosition.X - Worlds[o].P.RoughPosition.X, -Worlds[i].P.RoughPosition.Y + Worlds[o].P.RoughPosition.Y);
        //                Vector2 angvec = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));

        //                Worlds[i].State.Acceleration += angvec * acc;
        //                Worlds[o].State.Acceleration += -angvec * acc2;


        //                if (float.IsNaN(Worlds[i].State.Acceleration.X))
        //                    Worlds[i].State.Acceleration = new Vector2(0, 0);
        //                if (float.IsNaN(Worlds[o].State.Acceleration.X))
        //                    Worlds[o].State.Acceleration = new Vector2(0, 0);
        //            }
        //            else if ((Worlds[i].LastCollision != o && Worlds[o].LastCollision != i) || (Worlds[i].CollisionTimer > 5f && Worlds[o].CollisionTimer > 5f))
        //            {
        //                //float v1x;
        //                //float v1y;
        //                //float v2x;
        //                //float v2y;

        //                //float m1 = Worlds[i].Mass;
        //                //float m2 = Worlds[o].Mass;

        //                //float v1 = Vector2.Distance(Vector2.Zero, Worlds[i].Velocity);
        //                //float v2 = Vector2.Distance(Vector2.Zero, Worlds[o].Velocity);

        //                ////v1 = ((m1 - m2) / (m1 + m2)) * v1o;
        //                ////v2 = ((2 * m1) / (m1 + m2)) * v1o;

        //                ////float t1 = v1 + v2;
        //                ////float t2 = v1o + v2o;

        //                //float theta1 = (UsefulMethods.VectorToAngle(Worlds[i].Velocity));
        //                //float theta2 = (UsefulMethods.VectorToAngle(Worlds[o].Velocity));

        //                //float phi = (UsefulMethods.VectorToAngle(Worlds[o].P.RoughPosition - Worlds[i].P.RoughPosition)) + ((float)Math.PI / 2f);

        //                //v1x = (float)(((((v1 * Math.Cos(theta1 - phi) * (m1 - m2)) + (2f * m2 * v2 * Math.Cos(theta2 - phi))) / (m1 + m2)) * Math.Cos(phi)) + (v1 * Math.Sin(theta1 - phi) * Math.Cos(phi + (Math.PI / 2f))));
        //                //v1y = (float)(((((v1 * Math.Cos(theta1 - phi) * (m1 - m2)) + (2f * m2 * v2 * Math.Cos(theta2 - phi))) / (m1 + m2)) * Math.Sin(phi)) + (v1 * Math.Sin(theta1 - phi) * Math.Cos(phi + (Math.PI / 2f))));

        //                //v2x = (float)(((((v2 * Math.Cos(theta2 - phi) * (m2 - m1)) + (2f * m1 * v1 * Math.Cos(theta1 - phi))) / (m2 + m1)) * Math.Cos(phi)) + (v2 * Math.Sin(theta2 - phi) * Math.Cos(phi + (Math.PI / 2f))));
        //                //v2y = (float)(((((v2 * Math.Cos(theta2 - phi) * (m2 - m1)) + (2f * m1 * v1 * Math.Cos(theta1 - phi))) / (m2 + m1)) * Math.Sin(phi)) + (v2 * Math.Sin(theta2 - phi) * Math.Cos(phi + (Math.PI / 2f))));

        //                //Worlds[i].Velocity = new Vector2(v1x, v1y);
        //                //Worlds[o].Velocity = new Vector2(v2x, v2y);

        //                //Worlds[i].LastCollision = o;
        //                //Worlds[o].LastCollision = i;
        //                //Worlds[i].CollisionTimer = 0f;
        //                //Worlds[o].CollisionTimer = 0f;


        //                if (true)
        //                {
        //                }



        //                #region old

        //                //Worlds[i].Velocity.X *= -0.9f;
        //                //Worlds[o].Velocity.X *= -0.9f; 

        //                if (Worlds[i].Mass > Worlds[o].Mass)
        //                {
        //                    //Worlds[i].WorldX += Worlds[o].WorldX;
        //                    //Worlds[i].WorldY += Worlds[o].WorldY;

        //                    //if (Worlds[i].WorldX > Worlds[i].WorldY)
        //                    //    Worlds[i].Radius = Worlds[i].WorldX * Worlds[i].QuadSize * Worlds[i].ChunkSize;
        //                    //else
        //                    //    Worlds[i].Radius = Worlds[i].WorldY * Worlds[i].QuadSize * Worlds[i].ChunkSize;

        //                    //float multiplier = (float)(Worlds[o].Mass / Worlds[i].Mass);

        //                    //Worlds[i].Velocity = Worlds[i].Velocity + (Worlds[i].Velocity * multiplier);

        //                    //Worlds[i].WorldName += "n";

        //                    //Worlds[i].Offset = -new Vector2((Worlds[i].WorldX * Worlds[i].QuadSize * Worlds[i].ChunkSize) / 2, (Worlds[i].WorldY * Worlds[i].QuadSize * Worlds[i].ChunkSize) / 2);

        //                    //Worlds.RemoveAt(o);


        //                }
        //                else
        //                {
        //                    //Worlds[o].WorldX += Worlds[i].WorldX;
        //                    //Worlds[o].WorldY += Worlds[i].WorldY;

        //                    //if (Worlds[o].WorldX > Worlds[o].WorldY)
        //                    //    Worlds[o].Radius = Worlds[o].WorldX * Worlds[o].QuadSize * Worlds[o].ChunkSize;
        //                    //else
        //                    //    Worlds[o].Radius = Worlds[o].WorldY * Worlds[o].QuadSize * Worlds[o].ChunkSize;

        //                    //float multiplier = (float)(Worlds[i].Mass / Worlds[o].Mass);

        //                    //Worlds[o].Velocity = Worlds[o].Velocity + (Worlds[o].Velocity * multiplier);

        //                    //Worlds[o].WorldName += "n";

        //                    //Worlds[o].Offset = -new Vector2((Worlds[o].WorldX * Worlds[o].QuadSize * Worlds[o].ChunkSize) / 2, (Worlds[o].WorldY * Worlds[o].QuadSize * Worlds[o].ChunkSize) / 2);

        //                    //Worlds.RemoveAt(i);
        //                    //break;
        //                }

        //                #endregion
        //            }
        //        }

        //    for (int i = 0; i < Worlds.Count; i++)
        //    {
        //        //if (Worlds[i].ObjectType == 4)
        //        //{
        //        //    Worlds[i].OrbitTrail.MovePoints(Worlds[1].P.RoughPosition - PP);
        //        //}

        //        Worlds[i].Update(graphics);
        //    }

        //    PP = Worlds[1].P.RoughPosition;



        //    test2[a].Update();

        //    for (int i = 0; i < test2.Count; i++)
        //    {
        //        test2[i].P = Worlds[i].P;
        //        test2[i].Scale = (Worlds[i].Radius / (test2[i].grid.Length / 8f)) / 32;
        //    }


        //    a++;
        //    if (a == test2.Count)
        //        a = 0;

        //}

        //private void SetupSystem()
        //{
        //    if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
        //    {
        //        Worlds[0].Mass = 1.8E+28f;

        //        Worlds[0].P.PositionY = 0;
        //        Worlds[0].P.Position = Vector2.Zero;

        //        Worlds[0].P.PositionX = 0;
        //        Worlds[0].Velocity = Vector2.Zero;

        //        //int dis;
        //        //Vector2 StarPos;
        //        //Vector2 RoughPos;
        //        //float Distance3;
        //        //float Force3;
        //        //Vector2 VecAngle;
        //        //float angle;

        //        int Center;

        //        for (int i = 1; i < Worlds.Count; i++)
        //        {
        //            //float area = (float)Math.PI * ((WorldX / 2f) * (WorldY / 2f));


        //            //Mass = (262144f * area);
        //            //Mass = (262144f * (float)WorldX) * (262144f * (float)WorldX);


        //            switch (Worlds[i].ObjectType)
        //            {
        //                case 0:
        //                case 1:
        //                    //Worlds[i].P.PositionX = 1 + Worlds[i].WorldID;
        //                    //Worlds[i].P.PositionY = 1 + Worlds[i].WorldID;
        //                    //Worlds[i].P.Position = Vector2.Zero;

        //                    //dis = 1000;

        //                    //Worlds[i].P.PositionX = WorldVariables.RandomNumber.Next(0, dis) - (dis / 2);
        //                    //Worlds[i].P.PositionY = WorldVariables.RandomNumber.Next(0, dis) - (dis / 2);
        //                    //Worlds[i].P.Position = new Vector2(WorldVariables.RandomNumber.Next(0, 2000000), WorldVariables.RandomNumber.Next(0, 2000000)) - new Vector2(1000000f, 1000000f);


        //                    //StarPos = Vector2.Zero;
        //                    //Worlds[i].P.SetRough();
        //                    //RoughPos = Worlds[i].P.RoughPosition;


        //                    //Distance3 = Vector2.Distance(StarPos, RoughPos);
        //                    //Force3 = (float)Math.Sqrt((WorldVariables.GravitationalConstant * (Worlds[0].Mass + Worlds[i].Mass)) / Distance3);

        //                    ////Force3 = 0f;


        //                    //VecAngle = RoughPos - StarPos;
        //                    //angle = UsefulMethods.VectorToAngle(VecAngle);
        //                    //angle += (float)Math.PI / 2f;


        //                    //Worlds[i].Velocity = UsefulMethods.AngleToVector(angle) * Force3;

        //                    SetOrbit(i, 0);





        //                    break;

        //                case 2:
        //                case 3:
        //                case 4:
        //                    while (true)
        //                    {
        //                        Center = WorldVariables.RandomNumber.Next(1, Worlds.Count);

        //                        if (Worlds[Center].ObjectType == 0 || Worlds[Center].ObjectType == 1 || Worlds[Center].ObjectType == 2 || Worlds[Center].ObjectType == 3)
        //                            if (Worlds[Center].ObjectType == 0 || Worlds[Center].ObjectType == 1)
        //                            break;
        //                    }

        //                    SetOrbit(i, Center);
        //                    break;
        //            }

        //            #region 4 suns

        //            //if (WorldID == 0 || WorldID == 1 || WorldID == 2 || WorldID == 3)
        //            //{
        //            //    long mass = 0;

        //            //    for (int i = Chemistry.Count - 1; i >= 0; i--)
        //            //    {
        //            //        mass += Chemistry[i] * (i + 1);
        //            //    }

        //            //    Mass = (float)mass * 100000f;
        //            //    Mass = 1.8E+18f;


        //            //    WorldX = 1640 * 3;
        //            //    WorldY = 1640 * 3;
        //            //    Radius = (WorldX / 2f) * QuadSize * ChunkSize;

        //            //    P.Position = Vector2.Zero;

        //            //    float v = 15000000f;

        //            //    if (WorldID == 0)
        //            //    {
        //            //        P.PositionY = 0;
        //            //        P.PositionX = 3;
        //            //        Velocity = new Vector2(0f, v);
        //            //    }
        //            //    else if (WorldID == 1)
        //            //    {
        //            //        P.PositionY = 0;
        //            //        P.PositionX = -3;
        //            //        Velocity = new Vector2(0f, -v);
        //            //    }                    
        //            //    else if (WorldID == 2)
        //            //    {
        //            //        P.PositionX = 0;
        //            //        P.PositionY = 3;
        //            //        Velocity = new Vector2(-v, 0f);
        //            //    }
        //            //    else
        //            //    {
        //            //        P.PositionX = 0;
        //            //        P.PositionY = -3;
        //            //        Velocity = new Vector2(v, 0);
        //            //    }
        //            //}

        //            #endregion

        //            #region 2 suns

        //            //if (WorldID == 0 || WorldID == 1)
        //            //{
        //            //    long mass = 0;

        //            //    for (int i = Chemistry.Count - 1; i >= 0; i--)
        //            //    {
        //            //        mass += Chemistry[i] * (i + 1);
        //            //    }

        //            //    Mass = (float)mass * 100000f;
        //            //    Mass = 1.8E+14f;


        //            //    WorldX = 1640 * 3;
        //            //    WorldY = 1640 * 3;
        //            //    Radius = (WorldX / 2f) * QuadSize * ChunkSize;

        //            //    P.PositionY = 0;
        //            //    P.Position = Vector2.Zero;


        //            //    if (WorldID == 0)
        //            //    {
        //            //        P.PositionX = 3;
        //            //        Velocity = new Vector2(0f, 2000000f);
        //            //    }
        //            //    else
        //            //    {
        //            //        P.PositionX = -3;
        //            //        Velocity = new Vector2(0f, -2000000f);
        //            //    }
        //            //}

        //            #endregion

        //        }
        //    }
        //}

        //private void SetOrbit(int i, int c)
        //{
        //    float MaxDistance = (float)Math.Sqrt(WorldVariables.GravitationalConstant * (Worlds[c].Mass + Worlds[i].Mass));
        //    float MinDistance = Worlds[i].Radius + Worlds[c].Radius;
        //    //MinDistance = (float)Math.Ceiling(MinDistance / 2000000f);

        //    if (i == 0)
        //        MaxDistance = 100f;

        //    //if (MaxDistance > 1000)
        //    //    MaxDistance = 1000;
            
        //    int dis = (int)(MaxDistance / 2000000f);

        //    //dis = 100;

        //    Worlds[i].P.PositionX = Worlds[c].P.PositionX + (WorldVariables.RandomNumber.Next(0, dis) - (dis / 2));
        //    Worlds[i].P.PositionY = Worlds[c].P.PositionY + (WorldVariables.RandomNumber.Next(0, dis) - (dis / 2));
        //    Worlds[i].P.Position = Worlds[c].P.Position + (new Vector2(WorldVariables.RandomNumber.Next(0, 2000000), WorldVariables.RandomNumber.Next(0, 2000000)) - new Vector2(1000000f, 1000000f));
        //    Worlds[i].P.Update();

        //    Vector2 StarPos = Worlds[c].P.RoughPosition;
        //    Vector2 RoughPos = Worlds[i].P.RoughPosition;


        //    float Distance3 = Vector2.Distance(StarPos, RoughPos);
        //    float Force3 = (float)Math.Sqrt((WorldVariables.GravitationalConstant * (Worlds[c].Mass + Worlds[i].Mass)) / Distance3);

        //    //Force3 = 0f;


        //    Vector2 VecAngle = RoughPos - StarPos;
        //    float angle = UsefulMethods.VectorToAngle(VecAngle);
        //    angle += (float)Math.PI / 2f;

        //    PP = StarPos;
        //    Worlds[i].Velocity = Worlds[c].Velocity + (UsefulMethods.AngleToVector(angle) * Force3);

        //    //Worlds[i].Velocity = Vector2.Zero;
        //}

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Camera cam)
        {
            //for (int i = 0; i < test2.Count; i++)
            //{
            //    float rad = test2[i].Scale * 32;

            //    if (rad * CameraManager.Cams[0].Z > 0.5f)
            //    test2[i].Draw(spriteBatch);
            //}
                        
            //spriteBatch.Draw(StaticTests.MarkerCircle, Vector2.Zero - new Vector2(CameraManager.Cams[0].PositionX * 2000000, CameraManager.Cams[0].PositionY * 2000000), null, Color.Orange, 0f, new Vector2(StaticTests.MarkerCircle.Width / 2, StaticTests.MarkerCircle.Height / 2), 1000000f / StaticTests.MarkerCircle.Width, SpriteEffects.None, 0f);

            for (int i = Worlds.Count - 1; i >= 0; i--)
            {
                Worlds[i].Draw(spriteBatch, graphicsDevice, cam);
            }


            Vector2 pos = InputManager.GetMousePosition(0);
            float dis = Vector2.Distance(pos, MouseOrigin);

            if (DebugOptions.DebugInt != 0 && InputManager.M.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                spriteBatch.Draw(StaticTests.MarkerCircle, MouseOrigin - new Vector2(CameraManager.Cams[0].PositionX * 2000000, CameraManager.Cams[0].PositionY * 2000000), null, Color.White, 0f, new Vector2(16, 16), dis / 16f, SpriteEffects.None, 0f);

            quad.Draw(graphicsDevice, Color.Red);
        }
    }
}
