using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    static class Geometry
    {
        static public float MinimumDistanceToLine(Vector2 LinePoint1, Vector2 LinePoint2, Vector2 Position)
        {
            // Return minimum distance between line segment vw and point p
            float l2 = Vector2.DistanceSquared(LinePoint1, LinePoint2);  // i.e. |w-v|^2 -  avoid a sqrt
            if (l2 == 0.0) 
                return Vector2.Distance(Position, LinePoint1);   // v == w case
            // Consider the line extending the segment, parameterized as v + t (w - v).
            // We find projection of point p onto the line. 
            // It falls where t = [(p-v) . (w-v)] / |w-v|^2
            float t = Vector2.Dot(Position - LinePoint1, LinePoint2 - LinePoint1) / l2;

            if (t < 0.0)
                return Vector2.Distance(Position, LinePoint1);       // Beyond the 'v' end of the segment
            else if (t > 1.0) 
                return Vector2.Distance(Position, LinePoint2);  // Beyond the 'w' end of the segment
            
            Vector2 projection = LinePoint1 + t * (LinePoint2 - LinePoint1);  // Projection falls on the segment
            return Vector2.Distance(Position, projection);
        }

        static public Vector2 ClosestPointOnLine(Vector2 LinePoint1, Vector2 LinePoint2, Vector2 Position)
        {
            float l2 = Vector2.DistanceSquared(LinePoint1, LinePoint2);
            if (l2 == 0.0)
                return LinePoint1;

            float t = Vector2.Dot(Position - LinePoint1, LinePoint2 - LinePoint1) / l2;
            
            if (t < 0.0)
                return LinePoint1;
            else if (t > 1.0)
                return LinePoint2;

            return LinePoint1 + t * (LinePoint2 - LinePoint1);
        }

        static public bool CircleIntersect(Vector2 Position1, float Radius1, Vector2 Position2, float Radius2)
        {
            return false;
        }







    }
}
