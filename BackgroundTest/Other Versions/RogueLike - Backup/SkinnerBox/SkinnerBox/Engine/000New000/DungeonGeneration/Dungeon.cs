using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SkeletonEngine
{
    class Dungeon
    {
        public List<Room> Rooms = new List<Room>();

        public Dungeon()
        {
            Rooms.Add(new Room(new SpacePosition(), 5, 7));
        }

        public void Update()
        {
            for (int i = 0; i < Rooms.Count; i++)
                Rooms[i].Update();
        }

        public void Draw(Camera camera)
        {
            DungeonVisualizer.Draw(camera);


        }
    }
}
