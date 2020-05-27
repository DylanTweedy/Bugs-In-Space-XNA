using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsTest
{
    class Physics
    {
        //Using International Standard of Units

        //World Variables
        //Mass
        float Wm;
        //Radius
        float Wr;
        //Gravitational Constant (Newtons)
        float G;
        //World Border Type
        string Wbt;

        //Mass (Kilograms)
        float m;

        //Time (Seconds)
        float t;

        //Temperature (Kelvin)
        float T;
        
        //Length/Distance (Metres)
        float r;

        //Kinetic Energy (Joules)
        Vector2 KE;

        //Luminosity (Candela)
        //float Iv;
        //bool Luminous;

        //Position
        Vector2 P;
        Vector2 pP;
        Vector2 nP;

        //Acceleration (Metres/Second)
        Vector2 a;

        //Velocity (Metres/Second)
        Vector2 v;

        //Force
        Vector2 F;

        //Current (Amps)
        //float I;

        //Gavitational Field Strength
        float g;

        //Momentum (Newtons/Second)
        public Vector2 p;

        #region properties

        public float GravitationalFieldStrength
        {
            get { return g; }
        }

        public float GravitationalConstant
        {
            get { return G; }
        }

        public float Mass
        {
            get { return m; }
            set 
            { 
                m = value;
            }
        }

        public float Temperature
        {
            get { return T; }
            set
            {
                T = value;
            }
        }

        public Vector2 Position
        {
            get { return P; }
            set
            {
                pP = P;
                P = value;
            }
        }

        public Vector2 NewPosition
        {
            set
            {
                nP = value;
            }
        }

        public float NewPositionX
        {
            set
            {
                nP.X = value;
            }
        }

        public float NewPositionY
        {
            set
            {
                nP.Y = value;
            }
        }

        public Vector2 Acceleration
        {
            get { return a; }
            set
            {
                a = value;
            }
        }

        public Vector2 KineticEnergy
        {
            get { return KE; }
        }

        public float Distance
        {
            get { return r; }
        }

        public float X
        {
            get { return P.X; }
            set
            {
                pP.X = P.X;
                P.X = value;
            }
        }

        public float Y
        {
            get { return P.Y; }
            set
            {
                pP.Y = P.Y;
                P.Y = value;
            }
        }

        public Vector2 PreviousPosition
        {
            get { return pP; }
            set
            {
                pP = value;
            }
        }

        public Vector2 Velocity
        {
            get { return v; }
            set
            {
                v = value;
            }
        }

        public Vector2 Force
        {
            get { return F; }
            set
            {
                F = value;
            }
        }

        public float ForceX
        {
            get { return F.X; }
            set
            {
                F.X = value;
            }
        }

        public float ForceY
        {
            get { return F.Y; }
            set
            {
                F.Y = value;
            }
        }

        public float Time
        {
            get { return t; }           
        }

        #endregion

        public void Initialize(Vector2 sP)
        {
            P = sP;
        }

        public void AddWorldVariables(float wm, float wr, float wg, string wbt)
        {
            Wm = wm;
            Wr = wr;
            Wbt = wbt;
            G = wg;
        }

        public void PhysicalProperties()
        {
        }

        public void LoadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (nP != Vector2.Zero)
            {
                P = nP;
                nP = Vector2.Zero;
            }

            CalculateGraviationalFieldStrength();

            F.Y += g;

            a = (F / m);
            v += a * t;
            p = v * m;
            pP = P;
            P = P + (v);
            
            KE = (m * (v * v)) / 2;

            F = Vector2.Zero;
        }

        public Vector2 CalculateProjectedPosition(Vector2 position)
        {
            CalculateGraviationalFieldStrength();

            F.Y += g;
            
            a = (F / m);
            v += a * t;

            return (position + (v));
        }

        private void CalculateGraviationalFieldStrength()
        {
            g = (float)(G * ((m * Wm) / Math.Pow((P.Y - Wr), 2)));
        }

        public void CalculateGraviationalFieldStrength(float m2, Vector2 p2)
        {
            Vector2 g2 = Vector2.Zero;

            if (P.Y - p2.Y != 0)
                g2.Y = (float)(G * ((m * m2) / Math.Pow((P.Y - p2.Y), 2)));

            if (P.X - p2.X != 0)
                g2.X = (float)((G * ((m * m2)) / Math.Pow((P.X - p2.X), 2)));

            F += g2;
        }

        private void UpdateWorldGravity()
        {
        }

        public void UpdateWorldBoundries(int ScreenWidth, int ScreenHeight, int Width, int Height)
        {
            if (Wbt == "SolidEdges")
            {
                if (Y >= ScreenHeight - Height || Y <= 0 || X >= ScreenWidth - Width || X <= 0)
                {
                    Vector2 Force = ((m * (v - (-v))) / t);
                    Force.X += F.X;
                    Force.Y += (g + F.Y);

                    if (Y >= ScreenHeight - Height || Y <= 0)
                        ForceY -= Force.Y;

                    if (X >= ScreenWidth - Width || X <= 0)
                        ForceX -= Force.X;
                }
            }
        }

        public void DistanceCalculation(Vector2 P2)
        {
            float X;
            float Y;
            
            X = P.X - P2.X;
            Y = P.Y - P2.Y;

            r = (float)(Math.Sqrt((X * X) + (Y * Y)));
        }

        public void Collision(float m2, Vector2 v2)
        {
            Vector2 Force = ((m2 * (v2 - (-v2))) / t) / 2;
            Force -= ((m * (v - (-v))) / t) / 2;
            Force -= F;

            F += Force;
        }
    }
}
