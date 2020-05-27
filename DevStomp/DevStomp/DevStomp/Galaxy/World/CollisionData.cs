using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DevStomp
{
    class CollisionData
    {
        public Vector2 Position;
        public float DistanceLeft;
        public float Angle;
        public bool Solid;

        public CollisionData()
        {
            Solid = true;
        }

        public CollisionData(Vector2 position, float distanceLeft, float angle)
        {
            Position = position;
            DistanceLeft = distanceLeft;
            Angle = angle;
            Solid = false;
        }
    }
}
