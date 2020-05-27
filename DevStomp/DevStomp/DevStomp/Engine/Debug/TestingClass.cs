using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class TestingClass
    {
        static List<ParallaxParticles> Weather = new List<ParallaxParticles>();
        

        static public void Initialize()
        {       
            Weather.Add(new ParallaxParticles());
            Weather.Add(new ParallaxParticles());
            Weather.Add(new ParallaxParticles());
        }

        static public void Update()
        {
        }

        static public void Draw()
        {
        }
    }
}
