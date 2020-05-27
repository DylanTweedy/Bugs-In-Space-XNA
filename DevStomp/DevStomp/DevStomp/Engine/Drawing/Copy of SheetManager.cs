using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class SM
    {
        static public Sheet StarsFront;
        static public Sheet StarsBack;
        static public Sheet StarsBig;

        static public Sheet PSmall;
        static public Sheet PMedium;
        static public Sheet PLarge;

        static public Sheet Particles;

        static public Sheet Weapons;
        static public Sheet Head;
        static public Sheet Chest;
        static public Sheet Gloves;
        static public Sheet Pants;
        static public Sheet Boots;

        static public Sheet PlanetBody;
        static public Sheet PlanetClouds;
        static public Sheet PlanetGlow;
        static public Sheet PlanetOverlay;
        static public Sheet PlanetShadows;

        static public Sheet GasBody;
        static public Sheet GasClouds;
        static public Sheet GasOverlay;

        static public Sheet AsteroidBody;
        static public Sheet AsteroidOverlay1;
        static public Sheet AsteroidOverlay2;
        static public Sheet AsteroidMask;

        static public Sheet StarBody;
        static public Sheet StarColor;
        static public Sheet StarGlow;

        static public void Initialize(ContentManager C, GraphicsDevice G)
        {
            StarsBack = SpriteSheet.Initialize(C, G, "Background//Stars//Back//", "Stars Back");
            StarsFront = SpriteSheet.Initialize(C, G, "Background//Stars//Front//", "Stars Front");
            StarsBig = SpriteSheet.Initialize(C, G, "Background//Stars//Big//", "Stars Big");

            PSmall = SpriteSheet.Initialize(C, G, "Projectiles//Small//", "Projectiles Small");
            PMedium = SpriteSheet.Initialize(C, G, "Projectiles//Medium//", "Projectiles Medium");
            PLarge = SpriteSheet.Initialize(C, G, "Projectiles//Large//", "Projectiles Large");

            Particles = SpriteSheet.Initialize(C, G, "Particles//", "Particles");

            Weapons = SpriteSheet.Initialize(C, G, "Items//Weapons//", "Weapons");

            Head = SpriteSheet.Initialize(C, G, "Items//Head//", "Head");
            Chest = SpriteSheet.Initialize(C, G, "Items//Chest//", "Chest");
            Gloves = SpriteSheet.Initialize(C, G, "Items//Gloves//", "Gloves");
            Pants = SpriteSheet.Initialize(C, G, "Items//Pants//", "Pants");
            Boots = SpriteSheet.Initialize(C, G, "Items//Boots//", "Boots");

            PlanetBody = SpriteSheet.Initialize(C, G, "Galaxy//Planet//Body//", "Planet Body");
            PlanetClouds = SpriteSheet.Initialize(C, G, "Galaxy//Planet//Clouds//", "Planet Clouds");
            PlanetGlow = SpriteSheet.Initialize(C, G, "Galaxy//Planet//Glow//", "Planet Glow");
            PlanetOverlay = SpriteSheet.Initialize(C, G, "Galaxy//Planet//Overlay//", "Planet Overlay");
            PlanetShadows = SpriteSheet.Initialize(C, G, "Galaxy//Planet//Shadows//", "Planet Shadows");

            GasBody = SpriteSheet.Initialize(C, G, "Galaxy//Gas Giant//Body//", "Gas Giant Body");
            GasClouds = SpriteSheet.Initialize(C, G, "Galaxy//Gas Giant//Clouds//", "Gas Giant Clouds");
            GasOverlay = SpriteSheet.Initialize(C, G, "Galaxy//Gas Giant//Overlay//", "Gas Giant Overlay");

            AsteroidBody = SpriteSheet.Initialize(C, G, "Galaxy//Asteroid//Body//", "Asteroid Body");
            AsteroidOverlay1 = SpriteSheet.Initialize(C, G, "Galaxy//Asteroid//Overlay1//", "Asteroid Overlay 1");
            AsteroidOverlay2 = SpriteSheet.Initialize(C, G, "Galaxy//Asteroid//Overlay2//", "Asteroid Overlay 2");
            AsteroidMask = SpriteSheet.Initialize(C, G, "Galaxy//Asteroid//Mask//", "Asteroid Mask");

            StarBody = SpriteSheet.Initialize(C, G, "Galaxy//Star//Body//", "Star Body");
            StarColor = SpriteSheet.Initialize(C, G, "Galaxy//Star//Color//", "Star Color");
            StarGlow = SpriteSheet.Initialize(C, G, "Galaxy//Star//Glow//", "Star Glow");
        }
    }
}
