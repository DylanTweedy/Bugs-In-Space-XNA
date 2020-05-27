using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeletonEngine;
using Microsoft.Xna.Framework;

namespace SkinnerBox
{
    static public class Interations
    {
        static public void BallObjectInteraction(Ball B, PongObject O)
        {
            if (O.Active)
            {
                Vector2 normal = B.P.TruePosition - O.P.TruePosition;
                float distance = normal.Length();
                PM.ApplyAcceleration acc;
                
                float cone;
                Vector2 Direction;
                Vector2 heading;
                               
                
                if (normal != Vector2.Zero)
                    normal.Normalize();


                //if (B.Types.Contains("TurretVacuum"))
                //{
                //}


                for (int t = 0; t < O.Types.Count; t++)
                    switch (O.Types[t])
                    {
                        case "Vortex":
                            float newDistance = distance;

                            if (distance < 128f)
                                newDistance = 128f;

                            //acc = new PM.ApplyAcceleration(-normal * ((1e+7f * O.P.Alpha) / (distance * distance)));

                            float TimeLeft = UsefulMethods.FindBetween(O.Timer, O.Duration, 0f, 2f, 0.1f, false);
                            acc = new PM.ApplyAcceleration(-normal * (((5e+6f * O.P.Alpha * O.P.Scale.Length() * TimeLeft * TimeLeft) / 25f) / (newDistance * newDistance)));
                            acc.Update(B.P);

                            //acc.Update(B.P);

                            break;

                        case "Black Hole":
                            if (distance < O.P.Scale.Length() / 8f)
                            {
                                B.Value *= O.Floats["BlackHoleMultiplier"];
                                B.EatTimer = 0f;

                                O.CollectedBalls.Add(B);
                                B.P.Remove = true;
                            }

                            if (distance < 256f)
                                B.P.Position += -normal * (400f) * GlobalVariables.WorldTime;
                            break;

                        case "TurretMain":
                            if (distance < (O.P.Scale.X / 3f) + (B.P.Scale.X / 2f))
                            {
                                Vector2 collisionNormal = O.P.TruePosition - B.P.TruePosition;
                                if (collisionNormal != Vector2.Zero)
                                    collisionNormal *= 1f / collisionNormal.Length();

                                float Intersection = Vector2.Distance(O.P.TruePosition, B.P.TruePosition)
                                - ((O.P.Scale.X / 3f) + (B.P.Scale.X / 2f));

                                B.P.Position += collisionNormal * Intersection;
                                B.P.Velocity = -collisionNormal * B.P.Velocity.Length();

                                if (B.Owner != null)
                                {
                                    O.Owner = B.Owner;
                                    O.P.Tint = B.Owner.PrimaryColor;
                                }
                                
                                //if (B.Types.Contains("TurretVacuum"))
                                //{
                                //    B.P.Position -= (O.P.TruePosition - B.P.TruePosition;
                                //}
                            }
                            break;

                        case "TurretGold":
                            if (distance < (O.P.Scale.X / 3f) + (B.P.Scale.X / 2f))
                            {
                                B.P.Glow = true;
                                B.Types.Add("Bonus");
                            }
                            break;

                        case "TurretVacuum":
                            float coneSize = MathHelper.Pi / 8f;
                            cone = (float)Math.Cos(coneSize);
                            Direction = UsefulMethods.AngleToVector(O.P.Rotation + MathHelper.PiOver2);
                            heading = O.P.TruePosition - B.P.TruePosition;

                            if (heading != Vector2.Zero)
                                heading.Normalize();

                            if (Vector2.Dot(Direction, heading) > cone) // Target is within the cone.
                            {
                                float dis = Vector2.Distance(O.P.TruePosition, B.P.TruePosition);

                                acc = new PM.ApplyAcceleration(-normal * (5e+5f / (dis)));
                                acc.Update(B.P);

                                if (distance < (O.P.Scale.X / 3f) + (B.P.Scale.X / 2f))
                                {
                                    O.CollectedBalls.Add(B);
                                    B.P.Remove = true;
                                }
                            }

                            break;
                    }
            }
        }
    }
}
