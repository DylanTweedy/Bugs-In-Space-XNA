using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    [Serializable()]
    class TestImage
    {
        public byte[][] r;
        public ParallaxParticles para;

        public TestImage()
        {
            r = new byte[3][];
            //para = new Parallax(new Vector2(150, 50));
            //para.ParticleType = 10;
        }

        public TestImage(byte[] R, byte[] G, byte[] B)
        {
            //para = new Parallax(new Vector2(150, 50));
            //para.ParticleType = 10;
            r = new byte[3][];

            r[0] = R;
            r[1] = G;
            r[2] = B;


            //r = R;
            //g = G;
            //b = B;
        }





    }
}
