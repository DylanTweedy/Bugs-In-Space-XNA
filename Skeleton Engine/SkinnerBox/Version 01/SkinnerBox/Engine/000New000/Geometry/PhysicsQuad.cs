using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class Node
    {
        public float Mass;

        public Vector2 Position;
        public Vector2 PreviousPosition;

        public float PreviousTimestep = 1f;

        public Vector2 Force = Vector2.Zero;
        public Vector2 PreviousForce = Vector2.Zero;
        
        public Vector2 Velocity
        {
            get { return (Position - PreviousPosition) * GlobalVariables.WorldTime; }
        }

        public Node(Vector2 position, float mass)
        {
            Mass = mass;
            Position = position;            
        }

        public void Update()
        {
            GlobalVariables.WorldTime = 1f / 120f;

            Vector2 test = ((Position - PreviousPosition) * GlobalVariables.WorldTime / PreviousTimestep);

            PreviousPosition = Position;

            Position += 
                test +
                (Force * GlobalVariables.WorldTime);

            PreviousTimestep = GlobalVariables.WorldTime;

            PreviousForce = Force;
            Force = Vector2.Zero;
        }

        public float KineticEnergy()
        {
            return 0.5f * Mass * (Velocity.Length() * Velocity.Length()) * 100f;
        }
    }

    class Spring
    {
        Node Point1;
        Node Point2;

        public float Stiffness;
        public float RestDistance;
        public float Damping = 0.03f;

        public bool String = false;

        public Spring(Node point1, Node point2, float stiffness)
        {
            Point1 = point1;
            Point2 = point2;

            RestDistance = Vector2.Distance(Point1.Position, Point2.Position);
            Stiffness = stiffness;
        }

        float SpeedModifier = 100f;
        
        public void Update()
        {
            Stiffness = 1f;
            Damping = 1f;

            #region OLD

            if (true)
            {
                ////    F = -k(|x|-RestDistance)(x/|x|) - Dampingv            
                //float distancePoint = Vector2.Distance(Point1.Position, Point2.Position);
                //if (String && distancePoint < RestDistance) return;

                //Vector2 F1 = (Stiffness * (distancePoint - RestDistance) * (Vector2.Normalize(Point2.Position - Point1.Position)) / distancePoint) - (Damping * (Point1.Velocity - Point2.Velocity));
                //Vector2 F2 = (Stiffness * (distancePoint - RestDistance) * (Vector2.Normalize(Point1.Position - Point2.Position)) / distancePoint) - (Damping * (Point2.Velocity - Point1.Velocity));


                //if (F1 != F1 || F2 != F2) return;

                //// Add acceleration. Updating velocity\positions should happen after all springs are updated
                //Point1.Force += F1;
                //Point2.Force += F2;
            }

            #endregion

            #region OLD2

            if (true)
            {
                //Vector2 difference = Point1.Position - Point2.Position;
                //Vector2 normal = Vector2.Normalize(difference);
                //float distance = Vector2.Distance(Point1.Position, Point2.Position);

                //float restDifference = (RestDistance - distance) / 2f;

                //float force = restDifference;

                //Point1.Force += (normal * force);
                //Point2.Force -= (normal * force);
            }

            #endregion

            #region OLD3

            if (true)
            {
                //Vector2 posDiff = Point1.Position - Point2.Position;
                //Vector2 diffNormal = posDiff;
                //diffNormal.Normalize();
                //Vector2 relativeVelocity = Point1.Velocity - Point2.Velocity;
                //float diffLength = posDiff.Length();

                //float springError = diffLength - RestDistance;
                //float springStrength = springError * Damping;
                //springStrength = springError;
                ////if (!float.IsPositiveInfinity(_breakpoint) && (springError > _breakpoint || springError < -1 * _breakpoint))
                ////{
                ////    _isBroken = true;
                ////}
                //float temp = Vector2.Dot(posDiff, relativeVelocity);
                //float dampening = Damping * temp / diffLength;
                //if (dampening > 500f)
                //    dampening = 500f;

                //Vector2 _force = Vector2.Multiply(diffNormal, -(springStrength + dampening));
                //Point1.Force += (_force);
                //Point2.Force += (-_force);
            }

            #endregion

            #region OLD4

            if (true)
            {
                Vector2 direction = Point1.Position - Point2.Position;
                //check for zero vector
                if (direction != Vector2.Zero)
                {
                    float currLength;
                    Vector2 force;
                    //get length
                    currLength = direction.Length();
                    //normalize
                    direction.Normalize();
                    //add spring force
                    force = -Stiffness * ((currLength - RestDistance) * direction);
                    //add spring damping force
                    force += -Damping * Vector2.Dot((Point1.Velocity / GlobalVariables.WorldTime) -
                   (Point2.Velocity / GlobalVariables.WorldTime), direction) * direction;
                    //apply the equal and opposite forces to the objects
                    Point1.Force += force;
                    Point2.Force += -force;
                }
            }

            #endregion
        }

        public float PotentialEnergy()
        {
            float distance = Vector2.Distance(Point1.Position, Point2.Position) - RestDistance;

            return 0.5f * Stiffness * (distance * distance) * GlobalVariables.WorldTime * GlobalVariables.WorldTime;
        }
    }

    class PhysicsQuad
    {
        public SkeletonQuad MainQuad;
        public Node[] Points = new Node[4];
        public Spring[] Springs = new Spring[6];

        public PhysicsQuad(Vector2 position, float scale, Color color, string textureLocation, string textureName)
        {
            MainQuad = new SkeletonQuad(position, scale, color, textureLocation, textureName);

            MainQuad.Rotate(GlobalVariables.RandomNumber.Next(0, 314 * 2) / 100f);

            Vector3 pos1 = MainQuad.MainQuad.vertices[0].Position;
            Vector3 pos2 = MainQuad.MainQuad.vertices[1].Position;
            Vector3 pos3 = MainQuad.MainQuad.vertices[2].Position;
            Vector3 pos4 = MainQuad.MainQuad.vertices[3].Position;
            
            SetQuadPoints(
                new Vector2(pos1.X, pos1.Y), new Vector2(pos4.X, pos4.Y), 
                new Vector2(pos2.X, pos2.Y), new Vector2(pos3.X, pos3.Y));
        }

        public void SetQuadPoints(Vector2 TopLeft, Vector2 TopRight, Vector2 BottomLeft, Vector2 BottomRight)
        {
            Points[0] = new Node(TopLeft, 1f);
            Points[1] = new Node(BottomLeft, 1f);
            Points[2] = new Node(BottomRight, 1f);
            Points[3] = new Node(TopRight, 1f);

            MainQuad.MainQuad.vertices[0].Position = new Vector3(TopLeft, 0f);
            MainQuad.MainQuad.vertices[1].Position = new Vector3(BottomLeft, 0f);
            MainQuad.MainQuad.vertices[2].Position = new Vector3(BottomRight, 0f);
            MainQuad.MainQuad.vertices[3].Position = new Vector3(TopRight, 0f);

            Springs[0] = new Spring(Points[0], Points[1], -3f);
            Springs[1] = new Spring(Points[1], Points[2], -3f);
            Springs[2] = new Spring(Points[2], Points[3], -3f);
            Springs[3] = new Spring(Points[3], Points[0], -3f);
            Springs[4] = new Spring(Points[0], Points[2], -3f);
            Springs[5] = new Spring(Points[1], Points[3], -3f);
        }

        public void Update()
        {
            for (int i = 0; i < Springs.Length; i++)
            {
                Springs[i].Update();
            }

            //float totalVelocity = 0f;
            //float totalForce = 0f;

            //for (int i = 0; i < Points.Length; i++)
            //{
            //    totalVelocity += Points[i].Velocity.Length();
            //    totalForce += Points[i].Force.Length();
            //}

            //DebugOptions.DebugTextLines.Add(new TextLine("Total Velocity: " + totalVelocity));
            //DebugOptions.DebugTextLines.Add(new TextLine("Total Force: " + totalForce));
            //DebugOptions.DebugTextLines.Add(new TextLine("Total: " + (totalForce + totalVelocity)));

            for (int i = 0; i < Points.Length; i++)
                Points[i].Update();

            UpdateQuad();

            QuadManager.AddQuad(MainQuad);
        }

        private void UpdateQuad()
        {
            for (int i = 0; i < Points.Length; i++)            
                MainQuad.MainQuad.vertices[i].Position = new Vector3(Points[i].Position, 0f);                        
        }
    }
}
