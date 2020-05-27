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

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Number n;
        Number n2;
        SpacePosition s;
        SpacePosition s2;

        List<Number> L1;
        List<SpacePosition> L2;

        GamePadState g;
        GamePadState gp;

        Vector2 g1;
        Vector2 g2;

        float t1X;
        float t2X;
        float t1Y;
        float t2Y;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Measurements.Initialize();

            n = new Number(0.01m, 0, 0);
            //s = new SpacePosition(new Vector2(9999999, 9999999), new Vector2(9999999, 9999999), new Vector2(9999999, 9999999));
            //s2 = new SpacePosition(new Vector2(-9999999, -9999999), new Vector2(-9999999, -9999999), new Vector2(-9999999, -9999999));
            s = new SpacePosition(Vector2.Zero, Vector2.Zero, Vector2.Zero);
            s2 = new SpacePosition(Vector2.Zero, Vector2.Zero, Vector2.Zero);

            L1 = new List<Number>();
            L2 = new List<SpacePosition>();

            g1 = Vector2.Zero;
            g2 = Vector2.Zero;

            t1X = 0f;
            t2X = 0f;
            t1Y = 0f;
            t2Y = 0f;


            //for (int i = 0; i < 10000; i++)
            //{
            //    L1.Add(new Number(0.123059680m, 1561618950, 489130880));
            //    L2.Add(new SpacePosition(new Vector2(500000, -500000), Vector2.Zero, Vector2.Zero));
            //}
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        float dis = 1f;
        float force;
        float acc;

        protected override void Update(GameTime gameTime)
        {
            Console.Clear();
            g = GamePad.GetState(PlayerIndex.One);

            if (n.Num == 0)
                n.Add(1);

            n.Multiply(1.001);
            n = new Number(0, 256, 0);

            Console.WriteLine(Write.Number(n, 1, 3, 1, 0, null));
            Console.WriteLine(Write.Number(n, 1, 3, 1, 1, "G:Metric"));
            Console.WriteLine(Write.Number(n, 1, 3, 1, 2, "G:Imperial"));
            Console.WriteLine(Write.Number(n, 1, 3, 1, 3, "G:Time 1"));
            //Console.WriteLine(Write.Number(n, 1, 3, 1, 4, "Celcius"));
            Console.WriteLine(Write.Number(n, 1, 3, 1, 4, "Fahrenheit"));

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }
    }
}
