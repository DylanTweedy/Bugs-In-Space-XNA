using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhysicsTest
{
    class EntityManager
    {
        public List<Entity> Entities;

        int ScreenWidth;
        int ScreenHeight;

        Vector2 TotalKE;
        Vector2 pTotalKE;

        World WorldPhysics;

        public void Initialize(int screenWidth, int screenHeight)
        {
            Entities = new List<Entity>();
            WorldPhysics = new World();
            WorldPhysics.Initialize();

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void LoadContent()
        {
            WorldPhysics.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            TotalKE = Vector2.Zero;

            UpdateCollisions();

            for (int e = 0; e < Entities.Count; e++)
            {
                Entities[e].Update(gameTime);

                TotalKE += Entities[e].KineticEnergy;
            }

            //Console.WriteLine(Entities[0].KineticEnergy);
            //Console.WriteLine(Entities[0].Velocity.Y);

            pTotalKE = TotalKE;
        }

        private void UpdateCollisions()
        {
            for (int e = 0; e < Entities.Count; e++)
            {
                for (int e2 = 0; e2 < Entities.Count; e2++)
                {
                    if (e != e2)
                    {
                        float m1 = Entities[e].Mass;
                        Vector2 p1 = Entities[e].Position;

                        float m2 = Entities[e2].Mass;
                        Vector2 p2 = Entities[e2].Position;

                        Entities[e].CalculateGraviationalFieldStrength(m2, p2);

                        if (Entities[e].EntityCollision.Intersects(Entities[e2].EntityCollision) && !Entities[e].CollidedEntities.Contains(e2))
                        {
                            Entities[e].CalculateTexture();
                            Entities[e2].CalculateTexture();

                            if (Entities[e].EntityCollision.TexturesCollide(Entities[e].TextureData, Entities[e].Matrix, Entities[e2].TextureData, Entities[e2].Matrix))
                            {
                                Vector2 v1 = Entities[e].Velocity;
                                Vector2 v2 = Entities[e2].Velocity;

                                Entities[e].UpdateCollision(m2, v2, e2, Entities[e].EntityCollision.CollisionPoint);
                                Entities[e2].UpdateCollision(m1, v1, e, Entities[e2].EntityCollision.CollisionPoint);
                            }
                        }                    
                    }
                }
            }
        }

        private void LoadWorldBoundries()
        {

        }

        public void AddObject(ContentManager cm, Vector2 SpawnPosition, bool PlayerControlled, float Mass)
        {
            Entity E;

            E = new Entity();
            E.Initialize(SpawnPosition);

            E.ScreenWidth = ScreenWidth;
            E.ScreenHeight = ScreenHeight;

            E.AddWorldVariables(WorldPhysics.Mass, WorldPhysics.Radius, WorldPhysics.GravitationalConstant, WorldPhysics.BorderType);
            E.LoadContent(cm);
            E.Mass = Mass;
            E.PlayerControlled = PlayerControlled;


            Entities.Add(E);
        }

        public void Draw(SpriteBatch sb)
        {
            for (int e = 0; e < Entities.Count; e++)
            {
                Entities[e].Draw(sb);
            }
        }


    }
}
