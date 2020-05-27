using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PhysicsTest
{

    class RotatedRectangle
    {
        public Rectangle CollisionRectangle;
        public float FinalRotation;
        public Vector2 Origin;
        public Rectangle FinalRectangle;
        public Vector2 CollisionPoint;
        public List<Vector2> CollidingVectors;
        public int CollidingPixels;
        public float FinalScale;
        Color[,] tex1;
        Matrix mat1;
        Color[,] tex2;
        Matrix mat2;
        int texHeight;
        int texWidth;
        public bool ExtraDetection = false;

        public Color[,] Texture1
        {
            set { tex1 = value; }
        }

        public Color[,] Texture2
        {
            set { tex2 = value; }
        }

        public Matrix Matrix1
        {
            set { mat1 = value; }
        }

        public Matrix Matrix2
        {
            set { mat2 = value; }
        }

        public RotatedRectangle(Rectangle theRectangle, float theInitialRotation, float InitialScale, int TextureHeight, int TextureWidth)
        {
            CollisionRectangle = theRectangle;
            FinalRotation = theInitialRotation;
            FinalScale = InitialScale;
            texWidth = TextureWidth;
            texHeight = TextureHeight;

            Origin = new Vector2((int)theRectangle.Width / 2, (int)theRectangle.Height / 2);

            CollidingPixels = 0;
        }

        public void ChangePosition(Vector2 NewPosition)
        {
            CollisionRectangle.X = (int)NewPosition.X;
            CollisionRectangle.Y = (int)NewPosition.Y;
        }

        public bool Intersects(Rectangle theRectangle, float Scale)
        {
            return Intersects(new RotatedRectangle(theRectangle, 0.0f, Scale, theRectangle.Height, theRectangle.Width));
        }

        public bool Intersects(RotatedRectangle theRectangle)
        {
            FinalRectangle = Rectangle.Intersect(CollisionRectangle, theRectangle.CollisionRectangle);

            List<Vector2> aRectangleAxis = new List<Vector2>();
            CollidingVectors = new List<Vector2>();
            aRectangleAxis.Add(UpperRightCorner() - UpperLeftCorner());
            aRectangleAxis.Add(UpperRightCorner() - LowerRightCorner());
            aRectangleAxis.Add(theRectangle.UpperLeftCorner() - theRectangle.LowerLeftCorner());
            aRectangleAxis.Add(theRectangle.UpperLeftCorner() - theRectangle.UpperRightCorner());

            foreach (Vector2 aAxis in aRectangleAxis)
            {
                if (!IsAxisCollision(theRectangle, aAxis))
                {
                    return false;
                }
            }

            return true;
        }

        public bool TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            CollisionPoint = Vector2.Zero;

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 200)
                            {
                                if (tex2[x2, y2].A > 200)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    if (CollisionPoint == Vector2.Zero)
                                        CollisionPoint = screenPos;
                                    CollidingVectors.Add(screenPos);
                                    CollidingPixels += 1;
                                    //TexturesCollideArea(tex1, mat1, tex2, mat2);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            //float HighestX = 0;
            //float HighestY = 0;
            //Console.WriteLine(CollidingPixels);
            //for (int pos = 0; pos < CollidingVectors.Count; pos++)
            //{
            //    for (int pos2 = 0; pos2 < CollidingVectors.Count; pos2++)
            //    {
            //        if (CollidingVectors[pos].X > CollidingVectors[pos2].X && CollidingVectors[pos].X > HighestX)
            //            HighestX = CollidingVectors[pos].X;
            //        if (CollidingVectors[pos].Y > CollidingVectors[pos2].Y && CollidingVectors[pos].Y > HighestY)
            //            HighestY = CollidingVectors[pos].Y;
            //    }
            //}

            //for (int v = 0; v < CollidingVectors.Count; v++)
            //    CollidingVectors.RemoveAt(v);

            //CollisionPoint = new Vector2(CollisionPoint.X + ((HighestX - CollisionPoint.X) / 2), CollisionPoint.Y + ((HighestY - CollisionPoint.Y) / 2));
            
            //if (CollidingPixels > 0)
            //    return true;

            return false;
        }

        private void TexturesCollideArea(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 200)
                            {
                                if (tex2[x2, y2].A > 200)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);

                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine(CollisionPoint.X);
            //Console.WriteLine("HighestX: " + HighestX);
            //Console.WriteLine("HighestY: " + HighestY);
        }

        public bool IntersectsBoundry(Vector2 Boundry, String Border, int HalfWidth, int HalfHeight)
        {
            Vector2 UpperLeft = UpperLeftCorner();
            Vector2 LowerLeft = LowerLeftCorner();
            Vector2 UpperRight = UpperRightCorner();
            Vector2 LowerRight = LowerRightCorner();

            if ((UpperLeft.X >= Boundry.X || LowerLeft.X >= Boundry.X || UpperRight.X >= Boundry.X || LowerRight.X >= Boundry.X) && Border == "Right")
                return true;

            if ((UpperLeft.X <= 0 || LowerLeft.X <= 0 || UpperRight.X <= 0 || LowerRight.X <= 0) && Border == "Left")
                return true;

            if ((UpperLeft.Y >= Boundry.Y + HalfHeight || LowerLeft.Y >= Boundry.Y + HalfHeight || UpperRight.Y >= Boundry.Y + HalfHeight || LowerRight.Y >= Boundry.Y + HalfHeight) && Border == "Bottom")
                return true;

            if ((UpperLeft.Y <= 0 + HalfHeight || LowerLeft.Y <= 0 + HalfHeight || UpperRight.Y <= 0 + HalfHeight || LowerRight.Y <= 0 + HalfHeight) && Border == "Top")
                return true;

            return false;
        }

        public int BoxHeight()
        {
            Vector2 aUpperLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Top);
            aUpperLeft = RotatePoint(aUpperLeft, aUpperLeft + Origin, FinalRotation);

            Vector2 aUpperRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Top);
            aUpperRight = RotatePoint(aUpperRight, aUpperRight + new Vector2(-Origin.X, Origin.Y), FinalRotation);

            Vector2 aLowerLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Bottom);
            aLowerLeft = RotatePoint(aLowerLeft, aLowerLeft + new Vector2(Origin.X, -Origin.Y), FinalRotation);

            Vector2 aLowerRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom);
            aLowerRight = RotatePoint(aLowerRight, aLowerRight + new Vector2(-Origin.X, -Origin.Y), FinalRotation);

            float m1 = aUpperLeft.Y - aLowerRight.Y;
            float m2 = aUpperRight.Y - aLowerLeft.Y;

            if (m1 < 0)
                m1 *= -1;

            if (m2 < 0)
                m2 *= -1;

            if (m1 > m2)
            {
                return (int)m1;
            }
            else
                return (int)m2;
        }

        public int BoxWidth()
        {
            Vector2 aUpperLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Top);
            aUpperLeft = RotatePoint(aUpperLeft, aUpperLeft + Origin, FinalRotation);

            Vector2 aUpperRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Top);
            aUpperRight = RotatePoint(aUpperRight, aUpperRight + new Vector2(-Origin.X, Origin.Y), FinalRotation);

            Vector2 aLowerLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Bottom);
            aLowerLeft = RotatePoint(aLowerLeft, aLowerLeft + new Vector2(Origin.X, -Origin.Y), FinalRotation);

            Vector2 aLowerRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom);
            aLowerRight = RotatePoint(aLowerRight, aLowerRight + new Vector2(-Origin.X, -Origin.Y), FinalRotation);

            float m1 = aUpperLeft.X - aLowerRight.X;
            float m2 = aUpperRight.X - aLowerLeft.X;

            if (m1 < 0)
                m1 *= -1;

            if (m2 < 0)
                m2 *= -1;

            if (m1 > m2)
            {
                return (int)m1;
            }
            else
                return (int)m2;
        }

        private bool IsAxisCollision(RotatedRectangle theRectangle, Vector2 aAxis)
        {
            List<int> aRectangleAScalars = new List<int>();
            aRectangleAScalars.Add(GenerateScalar(theRectangle.UpperLeftCorner(), aAxis));
            aRectangleAScalars.Add(GenerateScalar(theRectangle.UpperRightCorner(), aAxis));
            aRectangleAScalars.Add(GenerateScalar(theRectangle.LowerLeftCorner(), aAxis));
            aRectangleAScalars.Add(GenerateScalar(theRectangle.LowerRightCorner(), aAxis));

            List<int> aRectangleBScalars = new List<int>();
            aRectangleBScalars.Add(GenerateScalar(UpperLeftCorner(), aAxis));
            aRectangleBScalars.Add(GenerateScalar(UpperRightCorner(), aAxis));
            aRectangleBScalars.Add(GenerateScalar(LowerLeftCorner(), aAxis));
            aRectangleBScalars.Add(GenerateScalar(LowerRightCorner(), aAxis));

            int aRectangleAMinimum = aRectangleAScalars.Min();
            int aRectangleAMaximum = aRectangleAScalars.Max();
            int aRectangleBMinimum = aRectangleBScalars.Min();
            int aRectangleBMaximum = aRectangleBScalars.Max();

            if (aRectangleBMinimum <= aRectangleAMaximum && aRectangleBMaximum >= aRectangleAMaximum)
            {
                return true;
            }
            else if (aRectangleAMinimum <= aRectangleBMaximum && aRectangleAMaximum >= aRectangleBMaximum)
            {
                return true;
            }

            return false;
        }

        private bool IsAxisCollision(Vector2 Boundry, string Border)
        {
            Vector2 UpperLeft = UpperLeftCorner();
            Vector2 LowerLeft = LowerLeftCorner();
            Vector2 UpperRight = UpperRightCorner();
            Vector2 LowerRight = LowerRightCorner();
            
            if ((UpperLeft.X >= Boundry.X || LowerLeft.X >= Boundry.X || UpperRight.X >= Boundry.X || LowerRight.X >= Boundry.X ) && Border == "Right")
                return true;

            if ((UpperLeft.X <= 0 || LowerLeft.X <= 0 || UpperRight.X <= 0 || LowerRight.X <= 0) && Border == "Left")
                return true;

            if ((UpperLeft.Y >= Boundry.Y || LowerLeft.Y >= Boundry.Y || UpperRight.Y >= Boundry.Y || LowerRight.Y >= Boundry.Y) && Border == "Bottom")
                return true;

            if ((UpperLeft.Y <= 0 || LowerLeft.Y <= 0 || UpperRight.Y <= 0 || LowerRight.Y <= 0) && Border == "Top")
                return true;

            return false;
        }

        private int GenerateScalar(Vector2 theRectangleCorner, Vector2 theAxis)
        {
            //Using the formula for Vector projection. Take the corner being passed in
            //and project it onto the given Axis
            float aNumerator = (theRectangleCorner.X * theAxis.X) + (theRectangleCorner.Y * theAxis.Y);
            float aDenominator = (theAxis.X * theAxis.X) + (theAxis.Y * theAxis.Y);
            float aDivisionResult = aNumerator / aDenominator;
            Vector2 aCornerProjected = new Vector2(aDivisionResult * theAxis.X, aDivisionResult * theAxis.Y);

            //Now that we have our projected Vector, calculate a scalar of that projection
            //that can be used to more easily do comparisons
            float aScalar = (theAxis.X * aCornerProjected.X) + (theAxis.Y * aCornerProjected.Y);
            return (int)aScalar;
        }

        private Vector2 RotatePoint(Vector2 Point, Vector2 pointOrigin, float Rotation)
        {
            Vector2 TranslatedPoint = new Vector2();
            TranslatedPoint.X = (float)(pointOrigin.X + (Point.X - pointOrigin.X) * Math.Cos(Rotation)
                - (Point.Y - pointOrigin.Y) * Math.Sin(Rotation));
            TranslatedPoint.Y = (float)(pointOrigin.Y + (Point.Y - pointOrigin.Y) * Math.Cos(Rotation)
                + (Point.X - pointOrigin.X) * Math.Sin(Rotation));
            return TranslatedPoint;
        }

        private Vector2 ScalePoint(Vector2 Point, Vector2 pointOrigin, float Scale)
        {
            Vector2 TranslatedPoint = new Vector2();
            
            if (pointOrigin.X > Point.X)
                TranslatedPoint.X = pointOrigin.X - ((texWidth / 2) * Scale);
            else if (pointOrigin.X < Point.X)
                TranslatedPoint.X = ((texWidth / 2) * Scale) + pointOrigin.X;
            else
                TranslatedPoint.X = Point.X;

            if (pointOrigin.Y > Point.Y)
                TranslatedPoint.Y = pointOrigin.Y - ((texHeight / 2) * Scale);
            else if (pointOrigin.Y < Point.Y)
                TranslatedPoint.Y = ((texHeight / 2) * Scale) + pointOrigin.Y;
            else
                TranslatedPoint.Y = Point.Y;

            return TranslatedPoint;
        }

        public Vector2 UpperLeftCorner()
        {
            Vector2 aUpperLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Top);
            aUpperLeft = ScalePoint(aUpperLeft, aUpperLeft + Origin, FinalScale);
            aUpperLeft = RotatePoint(aUpperLeft, aUpperLeft + Origin, FinalRotation);
            return aUpperLeft;
        }

        public Vector2 UpperRightCorner()
        {
            Vector2 aUpperRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Top);
            aUpperRight = ScalePoint(aUpperRight, aUpperRight + new Vector2(-Origin.X, Origin.Y), FinalScale);
            aUpperRight = RotatePoint(aUpperRight, aUpperRight + new Vector2(-Origin.X, Origin.Y), FinalRotation);
            return aUpperRight;
        }

        public Vector2 LowerLeftCorner()
        {
            Vector2 aLowerLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Bottom);
            aLowerLeft = ScalePoint(aLowerLeft, aLowerLeft + new Vector2(Origin.X, -Origin.Y), FinalScale);
            aLowerLeft = RotatePoint(aLowerLeft, aLowerLeft + new Vector2(Origin.X, -Origin.Y), FinalRotation);
            return aLowerLeft;
        }

        public Vector2 LowerRightCorner()
        {
            Vector2 aLowerRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom);
            aLowerRight = ScalePoint(aLowerRight, aLowerRight + new Vector2(-Origin.X, -Origin.Y), FinalScale);
            aLowerRight = RotatePoint(aLowerRight, aLowerRight + new Vector2(-Origin.X, -Origin.Y), FinalRotation);
            return aLowerRight;
        }

        public int X
        {
            get { return CollisionRectangle.X; }
        }

        public int Y
        {
            get { return CollisionRectangle.Y; }
        }

        public int Width
        {
            get { return CollisionRectangle.Width; }
        }

        public int Height
        {
            get { return CollisionRectangle.Height; }
        }

    }
}
