using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PhysicsTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        int ScreenHeight;
        int ScreenWidth;
        EntityManager entityManager;

        public static Game1 Instance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            Content.RootDirectory = "Content";

            Instance = this;
        }

        protected override void Initialize()
        {
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            entityManager = new EntityManager();
            entityManager.Initialize(ScreenWidth, ScreenHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(GraphicsDevice);
            entityManager.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        int i = 1;

        protected override void Update(GameTime gameTime)
        {


            if (entityManager.Entities.Count < 1)
            {
                entityManager.AddObject(this.Content, new Vector2(100, 100), true, 55);
                entityManager.AddObject(this.Content, new Vector2(((ScreenWidth / 10) * i) + 100, 100), false, 55);
            }

            //if (i < 9)
            //{
            //    entityManager.AddObject(this.Content, new Vector2(((ScreenWidth / 10) * i) + 100, 100), false, 55);
            //    entityManager.AddObject(this.Content, new Vector2(((ScreenWidth / 10) * i) + 100, 200), false, 55);
            //    entityManager.AddObject(this.Content, new Vector2(((ScreenWidth / 10) * i) + 100, 300), false, 55);
            //}

            i += 1;

            entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        private void AddObject()
        {            

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            sb.Begin();

            entityManager.Draw(sb);

            sb.End();

            base.Draw(gameTime);
        }
    }
}
