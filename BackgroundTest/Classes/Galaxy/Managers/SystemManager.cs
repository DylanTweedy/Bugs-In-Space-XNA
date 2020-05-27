using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class SystemManager
    {
        public List<System> systems;
        public int SystemCount;
        public bool SystemLoaded;
       
        Random rand;
        int Seed;
        int ChunkSize;
        byte Tier;
        //int centerRadius;
        
        public void Initialize(int chunkSize, int systemCount, int x, int y, byte tier)
        {
            Seed = int.Parse(x.ToString() + y.ToString());

            systems = new List<System>();
            rand = new Random(Seed);
            ChunkSize = chunkSize;
            //centerRadius = 10000;
            SystemCount = systemCount;
            SystemLoaded = false;
            Tier = tier;
        }

        public bool AddSystem()
        {
            systems.Add(new System());
            systems[systems.Count - 1].Initialize(rand, systems.Count - 1, ChunkSize, Tier);
            
            bool removed = false;
            int counter = 0;
            bool conflict = CheckConflict(systems.Count - 1);

            while (conflict)
            {
                systems[systems.Count - 1].ResetPosition();
                conflict = CheckConflict(systems.Count - 1);

                if (counter > 50)
                {
                    //conflict = false;
                    //removed = true;
                }
                else
                    counter++;
            }

            if (removed)
            {
                systems.RemoveAt(systems.Count - 1);
                return false;
            }
            else
            {
                systems[systems.Count - 1].LoadContent();
                return true;
            }
            
        }

        private bool CheckConflict(int SystemID)
        {           
            for (int i = 0; i < systems.Count; i++)
            {
                if (i != SystemID)
                {
                    Vector2 p1 = systems[i].Position;
                    Vector2 p2 = systems[SystemID].Position;
                    float distance = Vector2.Distance(p1, p2);

                    if (distance < (systems[i].SystemRadius / 2) + (systems[SystemID].SystemRadius / 2))
                        return true;                    
                }
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < systems.Count; i++)            
                systems[i].Update(gameTime);
        }

        public void DrawSimple(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalTint)
        {
            for (int i = 0; i < systems.Count; i++)                
                systems[i].DrawSimple(spriteBatch, cameraNumber, Offset, FinalTint);
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalTint)
        {
            //CameraManager.CamerasRead[0].Location = new Vector3(1250, 1250, 1);

            for (int i = 0; i < systems.Count; i++)            
                systems[i].Draw(spriteBatch, cameraNumber, Offset, FinalTint);
            
        }
    }
}
