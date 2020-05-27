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

        static public float Cross(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        static public Vector2 LineIntersect(Vector2 line1V1, Vector2 line1V2, Vector2 line2V1, Vector2 line2V2)
        {
            Vector2 intersection = new Vector2();

            Vector2 r = line1V2 - line1V1;
            Vector2 s = line2V2 - line2V1;
            float rxs = Cross(r,s);
            float qpxr = Cross(line2V1 - line1V1, r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (rxs == 0f && qpxr == 0f)
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping,

                //if (considerCollinearOverlapAsIntersect)
                //    if ((0 <= (line2V1 - line1V1) * r && (line2V1 - line1V1) * r <= r * r) || (0 <= (line1V1 - line2V1) * s && (line1V1 - line2V1) * s <= s * s))
                //        return Vector2.Zero;

                // 2. If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return Vector2.Zero;
            }

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (rxs == 0 && qpxr != 0)
                return Vector2.Zero;

            // t = (q - p) x s / (r x s)
            var t = Cross(line2V1 - line1V1, s) / rxs;

            // u = (q - p) x r / (r x s)

            var u = Cross(line2V1 - line1V1,r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if (rxs != 0 && (0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                // We can calculate the intersection point using either t or u.
                intersection = line1V1 + t * r;

                // An intersection was found.
                return intersection;
            }

            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return Vector2.Zero;
        }





    }
}
