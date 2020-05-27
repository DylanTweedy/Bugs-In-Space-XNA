using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsTest
{
    class World
    {
        //World Mass (Kilograms)
        float m;

        //Radius (Metres)
        float r;

        //Border Type
        string Bt;

        //Gravitational Constant
        double G;

        public float Mass
        {
            get { return m; }
            set { m = value; }
        }

        public float Radius
        {                    
            get { return r; }
            set { r = value; }
        }

        public string BorderType
        {
            get { return Bt; }
            set { Bt = value; }
        }

        public float GravitationalConstant
        {
            get { return (float)G; }
        }

        public void Initialize()
        {
            m = (float)(5.9722 * Math.Pow(10, 24));
            //m = 1;
            r = 6353000;
            G = 6.67428 * Math.Pow(10, -11);
            Bt = "SolidEdges";
        }

        public void LoadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
