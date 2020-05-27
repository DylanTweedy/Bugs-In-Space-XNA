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

        public byte type;

        float orbitSpeed;
        float orbitRadius;
        float orbitRadian;
        float originalOrbitSpeed;
        float originalOrbitRadius;
        float originalOrbitRadian;
        
        float a;
        float b;

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

        public Orbit(float OrbitRadius, float OrbitSpeed, float Rotation, float OrbitRadian, Vector2 Position)
        {            
            orbitRadius = OrbitRadius;
            orbitSpeed = OrbitSpeed / 1000f;
            rotation = Rotation;
            orbitRadian = OrbitRadian;
            originalOrbitSpeed = OrbitSpeed;
            originalOrbitRadius = OrbitRadius;
            orbitPosition = Position;
        }

        public void Initialize(byte OrbitType, float A, float B)
        {
            type = OrbitType;

            switch (OrbitType)
            {
                case 2:
                //a = Width, b = Height                    
                case 3:
                    //a = Axis, b = Eccentricity;
                    a = A;
                    b = B;
                    break;
            }
        }

        public void UpdateSpeed(float Multiplier)
        {
            orbitSpeed = originalOrbitSpeed * Multiplier;
        }

        public void UpdateRadius(float Multiplier)
        {
            orbitRadius = originalOrbitRadius * Multiplier;
        }

        public Vector2 UpdatePosition(Vector2 OrbitPosition, float TimePassed)
        {
            orbitPosition = OrbitPosition;

            float r;            
            float x;
            float y;

            switch (type)
            {
                case 1:
                    //Circle
                    orbitPosition += new Vector2(orbitRadius * (float)Math.Cos(orbitRadian), orbitRadius * (float)Math.Sin(orbitRadian));
                    orbitRadian += orbitSpeed * TimePassed;
                    break;

                case 2:
                    //Oval
                    r = (a * b) / (float)Math.Sqrt(Math.Pow(b * Math.Cos(orbitRadian), 2) + Math.Pow(a * Math.Sin(orbitRadian), 2));
                    x = (r * (float)Math.Cos(orbitRadian + rotation));
                    y = (r * (float)Math.Sin(orbitRadian + rotation));

                    orbitPosition += new Vector2(orbitRadius * x, orbitRadius * y);
                    orbitRadian += orbitSpeed / r / r / r;
                    break;

                case 3:
                    //Elliptical
                    r = (float)(a * (1 - Math.Pow(b, 2)) / (1 + (b * Math.Cos(orbitRadian))));
                    x = (r * (float)Math.Cos(orbitRadian + rotation));
                    y = (r * (float)Math.Sin(orbitRadian + rotation));

                    orbitPosition += new Vector2(orbitRadius * x, orbitRadius * y);
                    orbitRadian += orbitSpeed / r / r;
                    break;
            }

            while (orbitRadian >= 6283f)
                orbitRadian -= 6283f;
            while (orbitRadian <= -6283f)
                orbitRadian += 6283f;
            
            return orbitPosition;
        }
    }
}
