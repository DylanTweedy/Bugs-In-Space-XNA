using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhysicsTest
{
    class Entity
    {
        public Physics Physics;
        bool pc;
        Vector2 CF;
        Vector2 g;
        Vector2 TextureCenter;
        public List<int> CollidedEntities;

        //World Variables
        //Mass
        float Wm;
        //Radius
        float Wr;
        //Gravitational Constant (Newtons)
        float G;
        //World Border Type
        string Wbt;       

        //Draw Position
        Vector2 DrawP;

        //Temporary Image Handling.
        bool TextureCalculated;
        Texture2D EntityTexture;
        Color EntityTint;
        float EntityScale;
        Matrix EntityMatrix;
        float EntityRotation;
        Color[,] EntityTextureData;

        //Temporary Collision Detection.
        public RotatedRectangle EntityCollision;

        //Temporary Random.
        Random r;

        //Temporary Controls.
        KeyboardState kb;
        KeyboardState pkb;

        //Temp
        public int ScreenWidth;
        public int ScreenHeight;
        public Vector2 PP;

        //Debug
        Vector2 CollidePoint;
        Texture2D MarkerTexture;
        int Collidei;


        #region Properties

        public float Mass
        {
            get { return Physics.Mass; }
            set { Physics.Mass = value; }
        }

        public float Scale
        {
            get { return EntityScale; }
            set { EntityScale = value; }
        }

        public Vector2 Velocity
        {
            get { return Physics.Velocity; }
        }

        public Matrix Matrix
        {
            get { return EntityMatrix; }
        }

        public Color[,] TextureData
        {
            get { return EntityTextureData; }
        }

        public Vector2 Position
        {
            get { return Physics.Position; }
            set { Physics.Position = value; }
        }

        public Vector2 KineticEnergy
        {
            get { return Physics.KineticEnergy; }
        }

        public Vector2 CollisionForce
        {
            set { CF = value; }
        }

        public bool PlayerControlled
        {
            get { return pc; }
            set { pc = value; }
        }

        public int Width
        {
            get { return EntityTexture.Width; }
        }

        public int Height
        {
            get { return EntityTexture.Height; }
        }

        #endregion

        public void Initialize(Vector2 sP)
        {
            Physics = new Physics();
            Physics.Initialize(sP);

            CollidedEntities = new List<int>();
            Collidei = 0;

            //TEMP
            r = new Random();
            EntityTint = new Color(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            kb = Keyboard.GetState();
            EntityRotation = 0f;
            EntityScale = 1f;
            //EntityRotation = r.Next(1,1000) / 10f;
            //EntityScale = r.Next(50, 150) / 100f;
        }

        public void AddWorldVariables(float wm, float wr, float wg, string wbt)
        {
            Wm = wm;
            Wr = wr;
            Wbt = wbt;
            G = wg;
        }

        public void LoadContent(ContentManager cm)
        {
            EntityTexture = cm.Load<Texture2D>("Images//Marker2");
            MarkerTexture = cm.Load<Texture2D>("Images//MarkerTexture");
            TextureCenter = new Vector2(EntityTexture.Width / 2, EntityTexture.Height / 2);

            Physics.AddWorldVariables(Wm, Wr, G, Wbt);
            Physics.LoadContent();

            DrawP = new Vector2(Position.X - (EntityTexture.Width / 2), Position.Y - (EntityTexture.Height / 2));

            EntityCollision = new RotatedRectangle(new Rectangle((int)DrawP.X, (int)DrawP.Y, EntityTexture.Width, EntityTexture.Height), EntityRotation, EntityScale, EntityTexture.Height, EntityTexture.Width);
        }

        public void Update(GameTime gameTime)
        {
            if (pc)
                UpdateControls();
            //rotation += 0.01f;

            if (Collidei < 10)
                Collidei += 1;

            UpdateWorldBoundries(ScreenWidth, ScreenHeight, EntityTexture.Width, EntityTexture.Height);

            if (CollidedEntities.Count > 0)
            {
                int i = CollidedEntities.Count;

                if (CollidedEntities.Contains(10000))
                    i -= 1;
                if (CollidedEntities.Contains(10001))
                    i -= 1;

                if (i == 0)
                    i = 1;

                Physics.Force += CF / i;
            }

            Physics.Update(gameTime);

            DrawP = new Vector2(Position.X - (EntityTexture.Width / 2), Position.Y - (EntityTexture.Height / 2));

            EntityCollision = new RotatedRectangle(new Rectangle((int)DrawP.X, (int)DrawP.Y, EntityTexture.Width, EntityTexture.Height), EntityRotation, EntityScale, EntityTexture.Height, EntityTexture.Width);
            
            CF = Vector2.Zero;
            g = Vector2.Zero;

            for (int c = CollidedEntities.Count; c > 0; c--)
                CollidedEntities.RemoveAt(c - 1);

            TextureCalculated = false;
        }

        private void UpdateWorldBoundries(int ScreenWidth, int ScreenHeight, int Width, int Height)
        {
            if (Wbt == "SolidEdges")
            {
                Vector2 Boundry = new Vector2(ScreenWidth, ScreenHeight);

                if (EntityCollision.IntersectsBoundry(Boundry, "Top", EntityTexture.Width / 2, EntityTexture.Height / 2) || EntityCollision.IntersectsBoundry(Boundry, "Bottom", EntityTexture.Width / 2, EntityTexture.Height / 2))
                {
                    float Force = ((Mass * (Velocity.Y - (-Velocity.Y))) / Physics.Time);
                    Force += (Physics.GravitationalFieldStrength + Physics.Force.Y);

                    CF.Y -= Force;

                    if (CollidedEntities.Contains(10000))
                        CollidedEntities.Add(10001);
                    else
                        CollidedEntities.Add(10000);
                }

                if (EntityCollision.IntersectsBoundry(Boundry, "Left", EntityTexture.Width / 2, EntityTexture.Height / 2) || EntityCollision.IntersectsBoundry(Boundry, "Right", EntityTexture.Width / 2, EntityTexture.Height / 2))
                {
                    Vector2 Force = ((Mass * (Velocity - (-Velocity))) / Physics.Time);
                    Force.X += Physics.Force.X;

                    CF.X -= Force.X;

                    CollidedEntities.Add(0);
                }

                //Console.WriteLine(Bottom.Top);

                //if (EntityCollision.Intersects(Top, 1f) || EntityCollision.Intersects(Bottom, 1f))
                //{
                //    float Force = ((Mass * (Velocity.Y - (-Velocity.Y))) / Physics.Time);
                //    Force += (Physics.GravitationalFieldStrength + Physics.Force.Y);

                //    CF.Y -= Force;
                //}

                //if (!EntityCollision.Intersects(WorldBorderVertical, 1f))
                //{

                //}

                //if (!EntityCollision.Intersects(WorldBorderHorizontal, 1f))
                //{

                //}
            }
        }

        public void UpdateCollision(float m2, Vector2 v2, int EntityNumber, Vector2 CollisionPoint)
        {
            if (!CollidedEntities.Contains(EntityNumber))
            {
                Vector2 Force = ((m2 * (v2 - (-v2))) / Physics.Time) / 2;
                Force -= ((Physics.Mass * (Physics.Velocity - (-Physics.Velocity))) / Physics.Time) / 2;
                Force -= Physics.Force;

                CF += Force;

                CollidedEntities.Add(EntityNumber);

                //if (Position.X - CollisionPoint.X > 0)
                //    Position = new Vector2(CollisionPoint.X + (Width / 2), Position.Y);
                //else
                //    Position = new Vector2(CollisionPoint.X - (Width / 2), Position.Y);

                //if (Position.Y - CollisionPoint.Y < 0)
                //    Physics.NewPositionY = CollisionPoint.Y + Width;
                //else
                //    Physics.NewPositionY = CollisionPoint.Y - Width;


                CollidePoint = CollisionPoint;
                Collidei = 0;
                PP = Physics.CalculateProjectedPosition(Position);
            }
        }

        public void CalculateGraviationalFieldStrength(float m2, Vector2 P2)
        {
            double gr = 0;
            float r;
            Vector2 g2 = Vector2.Zero;

            Physics.DistanceCalculation(P2);

            if (Position - P2 != Vector2.Zero)
                gr = G * ((Mass * m2) / Math.Pow(Physics.Distance, 2));

            if (Position.X >= 0 && Position.X > P2.X)
                g2.X = -(G * ((Mass * m2) / (float)Math.Pow(Position.X - P2.X, 2)));
            else if (Position.X >= 0 && Position.X < P2.X)
                g2.X = (G * ((Mass * m2) / (float)Math.Pow(P2.X - Position.X, 2)));


            if (Position.Y >= 0 && Position.Y > P2.Y && Position.Y - P2.Y >= 1)
                g2.Y = -(G * ((Mass * m2) / (float)Math.Pow(Position.Y - P2.Y, 2)));
            else if (Position.Y >= 0 && Position.Y < P2.Y && P2.Y - Position.Y >= 1)
                g2.Y = (G * ((Mass * m2) / (float)Math.Pow(P2.Y - Position.Y, 2)));
            
            r = (float)(Math.Sqrt((g2.X * g2.X) + (g2.Y * g2.Y)));         


            g += g2;
        }

        public void CalculateTexture()
        {
            if (!TextureCalculated)
            {
                EntityMatrix =
                    Matrix.CreateTranslation(DrawP.X - Position.X, DrawP.Y - Position.Y, 0) *
                    Matrix.CreateRotationZ(EntityRotation) *
                    Matrix.CreateScale(EntityScale) *
                    Matrix.CreateTranslation(DrawP.X, DrawP.Y, 0);

                Color[] colors1D = new Color[EntityTexture.Width * EntityTexture.Height];
                EntityTexture.GetData(colors1D);

                EntityTextureData = new Color[EntityTexture.Width, EntityTexture.Height];
                for (int x = 0; x < EntityTexture.Width; x++)
                    for (int y = 0; y < EntityTexture.Height; y++)
                        EntityTextureData[x, y] = colors1D[x + y * EntityTexture.Width];

                TextureCalculated = true;
            }
        }

        public void UpdateControls()
        {
            pkb = kb;
            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Up))
                Physics.ForceY = -Physics.GravitationalFieldStrength - 1000;
            else if (kb.IsKeyDown(Keys.Down))
                Physics.ForceY = -Physics.GravitationalFieldStrength + 1000;

            if (kb.IsKeyDown(Keys.Left))
                Physics.ForceX = -1000;
            else if (kb.IsKeyDown(Keys.Right))
                Physics.ForceX = 1000;

            if (kb.IsKeyDown(Keys.D))
                EntityRotation += 0.01f;
            if (kb.IsKeyDown(Keys.A))
                EntityRotation -= 0.01f;

            if (kb.IsKeyDown(Keys.W))
                EntityScale += 0.01f;
            if (kb.IsKeyDown(Keys.S))
                EntityScale -= 0.01f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(EntityTexture, DrawP, null, EntityTint, EntityRotation, TextureCenter, EntityScale, SpriteEffects.None, 1f);
            //if (Collidei < 10)
                //sb.Draw(MarkerTexture, CollidePoint - new Vector2(MarkerTexture.Width / 2, MarkerTexture.Height / 2), Color.White);
        }
    }
}
