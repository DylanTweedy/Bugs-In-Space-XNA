using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class PlanetSetup
    {
        List<Sprite> planets;
        List<Texture2D> textures;
        List<Orbit> orbits;
        Random rand;
        //Vector2 position;
        //Planet planet;
        float rotation;
        float scale;
        int centerRadius;
        int planetCount;
        //float systemCenter;
        int systemRadius;




        public int SystemRadius
        {
            get { return systemRadius; }
        }
            

        public PlanetSetup(int CenterRadius, Random Rand)
        {
            centerRadius = CenterRadius;

            planets = new List<Sprite>();
            textures = new List<Texture2D>();
            rand = Rand;
            orbits = new List<Orbit>();
            systemRadius = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            LoadTextures(Content);
            LoadPlanets();

            SetupSystem();
        }

        void LoadTextures(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet1"));
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet10"));
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet11"));
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet12"));
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet13"));
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet14"));
            textures.Add(Content.Load<Texture2D>("Images//Background//Planets//Planet3"));
        }

        void LoadPlanets()
        {
            //Lower chance of high planet count
            planetCount = rand.Next(0, 7);

            for (int i = 0; i < planetCount; i++)
            {
                Texture2D tex = textures[rand.Next(0, textures.Count)];

                scale = (float)rand.Next(7500, 30000) / 10000f;

                planets.Add(new Sprite(tex));
                planets[i].Scale = scale;
                planets[i].Tint = new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
            }
        }

        void SetupSystem()
        {
            for (int i = 0; i < planetCount; i++)
            {
                rotation = (float)rand.Next(1, 100000) / 10000000f;

                int min = centerRadius + (planets[i].Width / 2);

                float rad = rand.Next(0, 1000000) / 100f;
                rad += min;
                float spe = ((float)rand.Next(1, 10000000) / (float)rad) / 1000f;
                float rot = (float)rand.Next(1, 62831) / 10000f;
                                    
                //orbits.Add(new Orbit(rad, spe, rot, rot, Vector2.Zero));

                orbits[i].InitializeCircle();

                if (systemRadius < (int)rad)
                    systemRadius = (int)rad;
            }

            systemRadius += systemRadius;
        }

        public void Update(GameTime gameTime, Vector2 SystemCenter)
        {
            for (int i = 0; i < planets.Count; i++)
            {
                //orbits[i].UpdatePosition();

                planets[i].Position = orbits[i].Position;
                planets[i].Rotation += rotation;
                planets[i].Animate(gameTime);
            }
        }

        void UpdateShadow()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].Draw(spriteBatch);
            }
        }
    }
}
