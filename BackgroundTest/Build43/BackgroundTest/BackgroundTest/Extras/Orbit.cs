using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Orbit
    {
        Vector2 orbitPosition;
        Vector2 originalPosition;



        float rotation;

        float orbitSpeed;
        float orbitRadius;
        float orbitRadian;
        float originalOrbitSpeed;
        float originalOrbitRadius;
        float originalOrbitRadian;

        bool OvalOrbit;
        bool CircleOrbit;
        bool EllipticOrbit;
        
        float x;
        float y;
        float r;
        float a;
        float b;
        float e;

        public Vector2 Position
        {
            get { return orbitPosition; }
        }

        public Vector2 OriginalPosition
        {
            get { return originalPosition; }
            set { originalPosition = value; }
        }

        public float OriginalRadian
        {
            get { return originalOrbitRadian; }
            set { originalOrbitRadian = value; }
        }

        public float OrbitRadian
        {
            get { return orbitRadian; }
            set { orbitRadian = value; }
        }

        public float OrbitRadius
        {
            get { return orbitRadius; }
            set { orbitRadius = value; }
        }

        public Orbit(float OrbitRadius, float OrbitSpeed, float Rotation, float OrbitRadian)
        {            
            orbitRadius = OrbitRadius;
            orbitSpeed = OrbitSpeed / 1000f;
            rotation = Rotation;
            orbitRadian = OrbitRadian;
            originalOrbitSpeed = OrbitSpeed;
            originalOrbitRadius = OrbitRadius;
        }

        public void InitializeCircle()
        {
            CircleOrbit = true;
        }

        public void InitializeOval(float Width, float Height)
        {
            a = Width;
            b = Height;
            OvalOrbit = true;
        }

        public void InitializeEllipse(float Eccentricity, float Axis)
        {
            e = Eccentricity;
            a = Axis;
            EllipticOrbit = true;
        }

        public void UpdateSpeed(float Multiplier)
        {
            orbitSpeed = originalOrbitSpeed * Multiplier;
        }

        public void UpdateRadius(float Multiplier)
        {
            orbitRadius = originalOrbitRadius * Multiplier;
        }

        public void UpdatePosition(Vector2 OrbitPosition)
        {
            orbitPosition = OrbitPosition;
            
            if (OvalOrbit)
                r = (a * b) / (float)Math.Sqrt(Math.Pow(b * Math.Cos(orbitRadian), 2) + Math.Pow(a * Math.Sin(orbitRadian), 2));

            if (EllipticOrbit)
                r = (float)(a * (1 - Math.Pow(e, 2)) / (1 + (e * Math.Cos(orbitRadian))));

            if (!CircleOrbit)
            {
                x = (r * (float)Math.Cos(orbitRadian + rotation));
                y = (r * (float)Math.Sin(orbitRadian + rotation));
            }

            if (CircleOrbit)
            {
                orbitPosition += new Vector2(orbitRadius * (float)Math.Cos(orbitRadian), orbitRadius * (float)Math.Sin(orbitRadian));

                orbitRadian += orbitSpeed;

                if (orbitRadian >= 6.28318530718f)
                    orbitRadian = 0f;
                else if (orbitRadian <= 0f)
                    orbitRadian = 6.28318530718f;
            }

            if (EllipticOrbit)
            {
                orbitPosition += new Vector2(orbitRadius * x, orbitRadius * y);

                orbitRadian += orbitSpeed / r / r;

                //if (orbitRadian >= 6.28318530718f)
                //    orbitRadian = 0f;
                //else if (orbitRadian <= 0f)
                //    orbitRadian = 6.28318530718f;
            }

            if (OvalOrbit)
            {
                orbitPosition += new Vector2(orbitRadius * x, orbitRadius * y);

                orbitRadian += orbitSpeed / r / r / r;

                if (orbitRadian >= 6.28318530718f)
                    orbitRadian = 0f;
                else if (orbitRadian <= 0f)
                    orbitRadian = 6.28318530718f;
            }
        }
    }
}
