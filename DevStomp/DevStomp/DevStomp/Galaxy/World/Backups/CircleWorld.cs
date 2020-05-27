using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DevStomp
{
    class CircleWorld
    {
        Chunk[][] TestChunk;
        public short QuadSize;
        public short ChunkSize;
        public int WorldX;
        public int WorldY;
        public List<Vector4> EditedPoints;
        public Vector2 CenterOfMass;
        public Vector2 Position;



        List<Marker> Markers;

        class Marker
        {
            public Color Color;
            public float Size;
            public Vector2 Position;

            public Marker(Vector2 Pos, Color C, float S)
            {
                Color = C;
                Position = Pos;
                Size = S;
            }
        }

        public void Initialize(Vector2 position, GraphicsDevice graphics)
        {
            #region OLD
            //basicEffect = new BasicEffect(graphics);

            //VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[12];

            //vertices[0] = new VertexPositionColorTexture(new Vector3(0, 0, 0), Color.Red, Vector2.Zero);
            //vertices[1] = new VertexPositionColorTexture(new Vector3(0, -50, 0), Color.Orange, Vector2.Zero);
            //vertices[2] = new VertexPositionColorTexture(new Vector3(50, 0, 0), Color.Yellow, Vector2.Zero);
            //vertices[3] = new VertexPositionColorTexture(new Vector3(50, -50, 0), Color.Green, Vector2.Zero);
            //vertices[4] = new VertexPositionColorTexture(new Vector3(100, 0, 0), Color.Blue, Vector2.Zero);
            //vertices[5] = new VertexPositionColorTexture(new Vector3(100, -50, 0), Color.Indigo, Vector2.Zero);
            //vertices[6] = new VertexPositionColorTexture(new Vector3(150, 0, 0), Color.Purple, Vector2.Zero);
            //vertices[7] = new VertexPositionColorTexture(new Vector3(150, -50, 0), Color.White, Vector2.Zero);
            //vertices[8] = new VertexPositionColorTexture(new Vector3(200, 0, 0), Color.Cyan, Vector2.Zero);
            //vertices[9] = new VertexPositionColorTexture(new Vector3(200, -50, 0), Color.Black, Vector2.Zero);
            //vertices[10] = new VertexPositionColorTexture(new Vector3(250, 0, 0), Color.DodgerBlue, Vector2.Zero);
            //vertices[11] = new VertexPositionColorTexture(new Vector3(250, -50, 0), Color.Crimson, Vector2.Zero);

            //vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColorTexture), 12, BufferUsage.WriteOnly);
            //vertexBuffer.SetData<VertexPositionColorTexture>(vertices);


            //short[] indices = new short[60];
            //indices[0] = 0; indices[1] = 1; indices[2] = 2;
            //indices[3] = 3; indices[4] = 4; indices[5] = 5;
            //indices[6] = 6; indices[7] = 7; indices[8] = 8;
            //indices[9] = 9; indices[10] = 10; indices[11] = 11;

            ////indices[3] = 1; indices[4] = 2; indices[5] = 3;
            ////indices[6] = 2; indices[7] = 3; indices[8] = 4;
            ////indices[9] = 3; indices[10] = 4; indices[11] = 5;
            ////indices[12] = 4; indices[13] = 5; indices[14] = 6;
            ////indices[15] = 5; indices[16] = 6; indices[17] = 7;
            ////indices[18] = 6; indices[19] = 7; indices[20] = 8;
            ////indices[21] = 7; indices[22] = 8; indices[23] = 9;
            ////indices[24] = 8; indices[25] = 9; indices[26] = 10;
            ////indices[27] = 9; indices[28] = 10; indices[29] = 11;

            ////indices[30] = 10; indices[31] = 11; indices[32] = 10;
            ////indices[33] = 11; indices[34] = 11; indices[35] = 11;

            //indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
            //indexBuffer.SetData(indices);
            #endregion
            //test = new Chunk();
            //test.Initialize(pos, graphics);

            WorldX = 10;
            WorldY = 10;

            QuadSize = 16;
            ChunkSize = 32;

            EditedPoints = new List<Vector4>();

            TestChunk = new Chunk[WorldX][];

            ////////////////////////////////////////////////////
            Markers = new List<Marker>();
            ////////////////////////////////////////////////////

            Position = new Vector2(-5000, 5000);

            for (int x = 0; x < TestChunk.Length; x++)
            {
                TestChunk[x] = new Chunk[WorldY];
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    TestChunk[x][y] = new Chunk();
                    TestChunk[x][y].SetupChunk();
                    //TestChunk[x][y].Initialize();       
                    TestChunk[x][y].InitializeBlank();
                }
            }

            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    SetEdges(x, y);
                }

            CenterOfMass = new Vector2((TestChunk.Length * QuadSize * ChunkSize) / 2, (TestChunk[0].Length * QuadSize * ChunkSize) / 2) - Position;


            DrawCircle(WorldX / 2, WorldY / 2, ChunkSize / 2, ChunkSize / 2, ((ChunkSize * WorldX) / 2.5f), 3);
            DrawCircle(WorldX / 2, WorldY / 2, ChunkSize / 2, ChunkSize / 2, ((ChunkSize * WorldX) / 2.5f) * 0.95f, 2);
            DrawCircle(WorldX / 2, WorldY / 2, ChunkSize / 2, ChunkSize / 2, ((ChunkSize * WorldX) / 2.5f) * 0.75f, 1);

            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    TestChunk[x][y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y) - CenterOfMass, QuadSize, ChunkSize);
                }
        }
        
        public Vector2 SetEdges(int X, int Y)
        {
            Vector2 Edges = Vector2.Zero;

            int XTarget;
            int YTarget;

            XTarget = X - 1;
            YTarget = Y;
            if (XTarget >= 0 && XTarget < WorldX)
            {
                for (int y = 0; y <= ChunkSize; y++)
                {
                    TestChunk[XTarget][YTarget].ID[ChunkSize][y] = TestChunk[X][Y].ID[0][y];
                    TestChunk[XTarget][YTarget].Interpolation[ChunkSize][y] = TestChunk[X][Y].Interpolation[0][y];
                }

                Edges.X = -1;
            }

            XTarget = X + 1;
            YTarget = Y;
            if (XTarget >= 0 && XTarget < WorldX)
            {
                for (int y = 0; y <= ChunkSize; y++)
                {
                    TestChunk[XTarget][YTarget].ID[0][y] = TestChunk[X][Y].ID[ChunkSize][y];
                    TestChunk[XTarget][YTarget].Interpolation[0][y] = TestChunk[X][Y].Interpolation[ChunkSize][y];
                }

                Edges.X = 1;
            }


            XTarget = X;
            YTarget = Y - 1;
            if (YTarget >= 0 && YTarget < WorldY)
            {
                for (int x = 0; x <= ChunkSize; x++)
                {
                    TestChunk[XTarget][YTarget].ID[x][ChunkSize] = TestChunk[X][Y].ID[x][0];
                    TestChunk[XTarget][YTarget].Interpolation[x][ChunkSize] = TestChunk[X][Y].Interpolation[x][0];
                }

                Edges.Y = -1;
            }

            XTarget = X;
            YTarget = Y + 1;
            if (YTarget >= 0 && YTarget < WorldY)
            {
                for (int x = 0; x <= ChunkSize; x++)
                {
                    TestChunk[XTarget][YTarget].ID[x][0] = TestChunk[X][Y].ID[x][ChunkSize];
                    TestChunk[XTarget][YTarget].Interpolation[x][0] = TestChunk[X][Y].Interpolation[x][ChunkSize];
                }

                Edges.Y = 1;
            }


            if (Edges.X != 0 && Edges.Y != 0)
            {
                XTarget = X + (int)Edges.X;
                YTarget = Y + (int)Edges.Y;

                TestChunk[XTarget][YTarget].ID[0][0] = TestChunk[X][Y].ID[ChunkSize][ChunkSize];
                TestChunk[XTarget][YTarget].Interpolation[0][0] = TestChunk[X][Y].Interpolation[ChunkSize][ChunkSize];

                TestChunk[XTarget][YTarget].ID[ChunkSize][ChunkSize] = TestChunk[X][Y].ID[0][0];
                TestChunk[XTarget][YTarget].Interpolation[ChunkSize][ChunkSize] = TestChunk[X][Y].Interpolation[0][0];

                TestChunk[XTarget][YTarget].ID[0][ChunkSize] = TestChunk[X][Y].ID[ChunkSize][0];
                TestChunk[XTarget][YTarget].Interpolation[0][ChunkSize] = TestChunk[X][Y].Interpolation[ChunkSize][0];

                TestChunk[XTarget][YTarget].ID[ChunkSize][0] = TestChunk[X][Y].ID[0][ChunkSize];
                TestChunk[XTarget][YTarget].Interpolation[ChunkSize][0] = TestChunk[X][Y].Interpolation[0][ChunkSize];
            }

            return Edges;
        }

        public void EditPoint(int CX, int CY, int X, int Y, float Addition)
        {      
            while (X > ChunkSize)
            {
                X -= ChunkSize;
                CX++;
            }
            while (X < 0)
            {
                X += ChunkSize;
                CX--;
            }

            while (Y > ChunkSize)
            {
                Y -= ChunkSize;
                CY++;
            }
            while (Y < 0)
            {
                Y += ChunkSize;
                CY--;
            }

            if (CX < 0 || CY < 0 || CX >= TestChunk.Length || CY >= TestChunk.Length)
            {
            }
            else
            {
                float Interp = TestChunk[CX][CY].Interpolation[X][Y] / 255f;
                Interp += Addition;

                if (Interp < 0f)
                    Interp = 0f;
                else if (Interp > 1f)
                    Interp = 1f;

                TestChunk[CX][CY].Interpolation[X][Y] = (byte)(Interp * 255f);

                if (Interp == 0)
                    TestChunk[CX][CY].ID[X][Y] = 0;
                else
                    TestChunk[CX][CY].ID[X][Y] = 1;

                EditedPoints.Add(new Vector4(CX, CY, X, Y));
                SetEdges(CX, CY);
            }
        }

        public void EditPoint(int CX, int CY, int X, int Y, float Addition, int ID)
        {
            while (X > ChunkSize)
            {
                X -= ChunkSize;
                CX++;
            }
            while (X < 0)
            {
                X += ChunkSize;
                CX--;
            }

            while (Y > ChunkSize)
            {
                Y -= ChunkSize;
                CY++;
            }
            while (Y < 0)
            {
                Y += ChunkSize;
                CY--;
            }

            if (CX < 0 || CY < 0 || CX >= TestChunk.Length || CY >= TestChunk.Length)
            {
            }
            else
            {
                float Interp = TestChunk[CX][CY].Interpolation[X][Y] / 255f;
                Interp += Addition;

                if (Interp < 0f)
                    Interp = 0f;
                else if (Interp > 1f)
                    Interp = 1f;

                TestChunk[CX][CY].Interpolation[X][Y] = (byte)(Interp * 255f);

                if (Interp == 0)
                    TestChunk[CX][CY].ID[X][Y] = 0;
                else
                    TestChunk[CX][CY].ID[X][Y] = (byte)ID;

                EditedPoints.Add(new Vector4(CX, CY, X, Y));
                SetEdges(CX, CY);
            }
        }

        private void DrawCircle(int CX, int CY, int QX, int QY, float Addition, int ID)
        {
            //List<Vector2> P = new List<Vector2>();
            //int r = 1;
            
            //while (Addition != 0)
            //{
            //    int x = r, y = 0;
            //    int radiusError = 1 - x;

            //    while (x >= y)
            //    {
            //        P.Add(new Vector2(x + QX, y + QY));
            //        P.Add(new Vector2(y + QX, x + QY));
            //        P.Add(new Vector2(-x + QX, y + QY));
            //        P.Add(new Vector2(-y + QX, x + QY));
            //        P.Add(new Vector2(-x + QX, -y + QY));
            //        P.Add(new Vector2(-y + QX, -x + QY));
            //        P.Add(new Vector2(x + QX, -y + QY));
            //        P.Add(new Vector2(y + QX, -x + QY));
            //        y++;
            //        if (radiusError < 0)
            //        {
            //            radiusError += 2 * y + 1;
            //        }
            //        else
            //        {
            //            x--;
            //            radiusError += 2 * (y - x + 1);
            //        }
            //    }

            //    P = P.Distinct().ToList();

            //    if (Math.Abs(Addition) / P.Count > 1f)
            //    {
            //        float Amount = Math.Sign(Addition);

            //        for (int o = 0; o < P.Count; o++)
            //        {
            //            EditPoint(CX, CY, (int)P[o].X, (int)P[o].Y, Amount);
            //            Addition -= Amount;
            //        }

            //        r++;
            //        P.Clear();
            //    }
            //    else
            //    {
            //        float Amount = Addition / P.Count;

            //        for (int o = 0; o < P.Count; o++)
            //        {
            //            EditPoint(CX, CY, (int)P[o].X, (int)P[o].Y, Amount);
            //            Addition -= Amount;
            //        }

            //        break;
            //    }
            //}

            if (true)
            {
            }

            //for (double i = 0.0; i < 360.0; i += 0.1)
            //{
            //    double angle = i * System.Math.PI / 180;
            //    int x = (int)(r * System.Math.Cos(angle));
            //    int y = (int)(r * System.Math.Sin(angle));

            //    EditPoint(CX, CY, QX + x, QY + y, Math.Sign(Addition));
            //}

            int r = (int)Math.Abs(Addition);

            for (int x = -r; x <= r; x++)
            {
                int height = (int)Math.Sqrt(r * r - x * x);

                for (int y = -height; y <= height; y++)
                {
                        EditPoint(CX, CY, QX + x, QY + y, Math.Sign(Addition), ID);
                }
            }
        }


        public void EditTile(Vector2 Pos, float Addition)
        {
            Pos += CenterOfMass;

            Vector2 SelectedChunk = Pos / (QuadSize * ChunkSize);
            SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
            SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);

            Vector2 ChunkPosition = (Pos / QuadSize) - (SelectedChunk * ChunkSize);
            ChunkPosition.X = (float)Math.Floor(ChunkPosition.X);
            ChunkPosition.Y = (float)Math.Floor(ChunkPosition.Y);

            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, Addition);
            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, Addition);
            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, Addition);
            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, Addition);
        }

        public bool IsPointInPolygon(Vector2 p, Vector2[] polygon)
        {
            if (polygon == null)
                return false;

            double minX = polygon[0].X;
            double maxX = polygon[0].X;
            double minY = polygon[0].Y;
            double maxY = polygon[0].Y;
            for (int i = 1; i < polygon.Length; i++)
            {
                Vector2 q = polygon[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minY = Math.Min(q.Y, minY);
                maxY = Math.Max(q.Y, maxY);
            }

            if (p.X < minX || p.X > maxX || p.Y < minY || p.Y > maxY)
            {
                return false;
            }

            // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                     p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public Vector2 CheckTile(Vector2 Pos, Vector2 PreviousPos)
        {
            Vector2 Dis = Pos - PreviousPos;
            float distance = Vector2.Distance(Vector2.Zero, Dis) / (QuadSize / 2);
            Vector2 collisionPoint;

            if (distance <= 1f)
            {
                collisionPoint = CheckQuadCollision(Pos);

                if (collisionPoint != Vector2.Zero)
                {
                    if (collisionPoint == Vector2.One)                    
                        return SolidCollision(Pos);
                    
                    Markers.Add(new Marker(collisionPoint, Color.Red, 1f));
                    return collisionPoint;
                }
            }
            else
            {
                Dis /= distance;

                for (int i = 0; i <= distance; i++)
                {
                    collisionPoint = CheckQuadCollision(PreviousPos + (Dis * i));
                    Markers.Add(new Marker(PreviousPos + (Dis * i), Color.Red, 1f));
                                      
                    if (collisionPoint != Vector2.Zero)
                    {
                        if (collisionPoint == Vector2.One)                        
                            return SolidCollision(PreviousPos + (Dis * i));                        

                        Markers.Add(new Marker(collisionPoint, Color.Red, 1f));
                        return collisionPoint;
                    }
                }
            }

            return Vector2.Zero;
        }

        public Vector2 SolidCollision(Vector2 Pos)
        {
            int radius = 1;
            List<Vector2> Points = new List<Vector2>();
            Vector2 collisionPoint = Vector2.Zero;
                        Vector2 half = new Vector2(0.5f, 0.5f);

            for (int x = -radius; x <= radius; x++)
                for (int y = -radius; y <= radius; y++)
                {
                    if (x == -radius || x == radius || y == -radius || y == radius)
                    {
                        Markers.Add(new Marker(Pos + new Vector2((QuadSize / 2) * x, (QuadSize / 2) * y), Color.Purple, 0.5f));

                        collisionPoint = CheckQuadCollision(Pos + new Vector2((QuadSize / 2) * x, (QuadSize / 2) * y));

                        if (collisionPoint != Vector2.One && collisionPoint != Vector2.Zero)
                        {
                            Points.Add(collisionPoint);
                        }

                        if (x == radius && y == radius && Points.Count == 0)
                        {
                            radius++;
                            x = -radius - 1;
                            y = -radius - 1;
                        }
                    }
                }

            List<float> distances = new List<float>();
            float distance = float.MaxValue;

            for (int i = 0; i < Points.Count; i++)            
                distances.Add(Vector2.Distance(Points[i], Pos));

            if (distances.Count > 1)
            {
                for (int i = 0; i < distances.Count - 1; i++)
                    distance = Math.Min(distances[i], distances[i + 1]);
            }
            else
                distance = distances[0];
            
            for (int i = 0; i < distances.Count; i++)
                if (distance == distances[i])
                {
                    Markers.Add(new Marker(Points[i], Color.Red, 1f));
                    return Points[i];
                }

            

            return collisionPoint;
        }

        public Vector2 CheckQuadCollision(Vector2 Pos)
        {
            ChunkPosition CP = GetChunkPosition(Pos);
            List<Vector2[]> Points = GetVertices(Pos, CP);

            if (Points != null)
                if (TestChunk[CP.CX][CP.CY].N[CP.QX][CP.QY] != 0)
                {
                    for (int p = 0; p < Points.Count; p++)
                        if (IsPointInPolygon(Pos, Points[p]))
                        {
                            List<float> Distances = new List<float>();
                            Vector2 FirstPoint = Vector2.Zero;
                            List<Vector2> ClosestPoints = new List<Vector2>();

                            for (int i = 0; i < Points[p].Length - 1; i++)
                                Distances.Add(DistanceToLine(Points[p][i], Points[p][i + 1], Pos));

                            for (int o = 0; o < Distances.Count; o++)
                            {
                                float smallest = Distances[0];
                                for (int i = 0; i < Distances.Count; i++)
                                    smallest = Math.Min(smallest, Distances[i]);

                                Vector2 ClosestPoint = Vector2.Zero;

                                for (int i = 0; i < Distances.Count; i++)
                                    if (smallest == Distances[i])
                                        ClosestPoint = GetClosetPoint(Points[p][i], Points[p][i + 1], Pos, true);

                                ClosestPoint = ClosestPoint - ((Pos - ClosestPoint) * 0.01f);

                                List<Vector2[]> ClosePoints = GetVertices(ClosestPoint, null);

                                bool InPolygon = false;

                                if (ClosePoints != null)
                                    for (int p2 = 0; p2 < ClosePoints.Count; p2++)
                                        if (IsPointInPolygon(ClosestPoint, ClosePoints[p2]))
                                            InPolygon = true;

                                if (!InPolygon)
                                    return ClosestPoint;
                                else
                                    ClosestPoints.Add(ClosestPoint);

                                for (int i = 0; i < Distances.Count; i++)
                                    if (smallest == Distances[i])
                                        Distances[i] = float.MaxValue;

                                if (o == 0)
                                    FirstPoint = ClosestPoint;
                            }

                            return Pos - new Vector2(0, QuadSize);

                            //Make it so it returns a position one quad away from the center of mass.
                            //return ;
                        }
                }
                else return Vector2.One;

            return Vector2.Zero;
        }

        private List<Vector2[]> GetVertices(Vector2 Position, ChunkPosition CP)
        {
            if (CP == null)
                CP = GetChunkPosition(Position);

            if (CP != null)
                if (TestChunk[CP.CX][CP.CY].ChunkQuads[CP.QX][CP.QY] != null)
                {
                    short[] Indices;
                    int indices;

                    List<Vector2[]> Points = new List<Vector2[]>();
                    VertexPositionColorTexture[] Vertices = TestChunk[CP.CX][CP.CY].ChunkQuads[CP.QX][CP.QY].vertices;
                    int vertices = Vertices.Length;

                    switch (TestChunk[CP.CX][CP.CY].N[CP.QX][CP.QY])
                    {
                        case 5:
                        case 10:
                            Indices = TestChunk[CP.CX][CP.CY].ChunkQuads[CP.QX][CP.QY].indices;
                            indices = Indices.Length;

                            for (int t = 0; t < indices / 3; t++)
                            {
                                Points.Add(new Vector2[4]);

                                Vector3 p1 = Vertices[Indices[0 + (t * 3)]].Position;
                                Vector3 p2 = Vertices[Indices[1 + (t * 3)]].Position;
                                Vector3 p3 = Vertices[Indices[2 + (t * 3)]].Position;

                                Points[t][0] = new Vector2(p1.X, p1.Y);
                                Points[t][1] = new Vector2(p2.X, p2.Y);
                                Points[t][2] = new Vector2(p3.X, p3.Y);
                                Points[t][3] = Points[t][0];
                            }
                            Vertices = null;
                            Indices = null;

                            return Points;




                        default:
                            Points.Add(new Vector2[vertices + 1]);

                            for (int i = 0; i < vertices; i++)
                            {
                                Vector3 p = Vertices[i].Position;
                                Points[0][i] = new Vector2(p.X, p.Y);
                            }
                            Points[0][vertices] = Points[0][0];
                            Vertices = null;

                            return Points;
                    }

                }

            return null;
        }

        private ChunkPosition GetChunkPosition(Vector2 Position)
        {
            Position += CenterOfMass;

            Vector2 chunk = Position / (QuadSize * ChunkSize);
            chunk.X = (float)Math.Floor(chunk.X);
            chunk.Y = (float)Math.Floor(chunk.Y);

            Vector2 quad = (Position / QuadSize) - (chunk * ChunkSize);
            quad.X = (float)Math.Floor(quad.X);
            quad.Y = (float)Math.Floor(quad.Y);

            if (chunk.X >= 0 && chunk.X < WorldX && chunk.Y >= 0 && chunk.Y < WorldY)
                return new ChunkPosition((int)chunk.X, (int)chunk.Y, (int)quad.X, (int)quad.Y);
                       
            return null;
        }

        public Vector2 GetClosetPoint(Vector2 A, Vector2 B, Vector2 P, bool segmentClamp)
        {
            Vector2 AP = P - A;
            Vector2 AB = B - A;
            float ab2 = AB.X * AB.X + AB.Y * AB.Y;
            float ap_ab = AP.X * AB.X + AP.Y * AB.Y;
            float t = ap_ab / ab2;
            if (segmentClamp)
            {
                if (t < 0.0f) t = 0.0f;
                else if (t > 1.0f) t = 1.0f;
            }
            Vector2 Closest = A + AB * t;
            return Closest;
        }

        float DistanceToLine(Vector2 v, Vector2 w, Vector2 p)
        {
            // Return minimum distance between line segment vw and point p
            float l2 = Vector2.DistanceSquared(v, w);  // i.e. |w-v|^2 -  avoid a sqrt
            if (l2 == 0.0) return Vector2.Distance(p, v);   // v == w case
            // Consider the line extending the segment, parameterized as v + t (w - v).
            // We find projection of point p onto the line. 
            // It falls where t = [(p-v) . (w-v)] / |w-v|^2
            float t = Vector2.Dot(p - v, w - v) / l2;
            
            if (t < 0.0) return Vector2.Distance(p, v);       // Beyond the 'v' end of the segment
            else if (t > 1.0) return Vector2.Distance(p, w);  // Beyond the 'w' end of the segment

            Vector2 projection = v + t * (w - v);  // Projection falls on the segment
            return Vector2.Distance(p, projection);
        }

        public bool CheckCollision(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            float denominator = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
            float numerator1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
            float numerator2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));

            // Detect coincident lines (has a problem, read below)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

        public Vector2 IntersectionPoint(Vector2 Position, Vector2 PreviousPosition, Vector2 Point1, Vector2 Point2)
        {
            // Get A,B,C of first line - points : ps1 to pe1
            float A1 = Position.Y - PreviousPosition.Y;
            float B1 = PreviousPosition.X - Position.X;
            float C1 = A1 * PreviousPosition.X + B1 * PreviousPosition.Y;

            // Get A,B,C of second line - points : ps2 to pe2
            float A2 = Point1.Y - Point2.Y;
            float B2 = Point2.X - Point1.X;
            float C2 = A2 * Point2.X + B2 * Point2.Y;

            // Get delta and check if the lines are parallel
            float delta = A1 * B2 - A2 * B1;
            if (delta == 0)            
                return Vector2.Zero;            
            else return
            new Vector2(
            (B2 * C1 - B1 * C2) / delta,
            (A1 * C2 - A2 * C1) / delta);
        }

        public void Update(GraphicsDevice graphics)
        {
            Vector2 MousePosition = new Vector2(InputManager.M.X - (GlobalVariables.WindowWidth / 2), InputManager.M.Y - (GlobalVariables.WindowHeight / 2));

            float WorldAngle = -CameraManager.Cams[0].R;
            float Distance = Vector2.Distance(Vector2.Zero, MousePosition);
            float MouseAngle = (float)Math.Atan2(MousePosition.X, -MousePosition.Y) + WorldAngle;
            MousePosition = new Vector2((float)Math.Sin(MouseAngle), -(float)Math.Cos(MouseAngle)) * Distance;

            Vector2 CameraPosition = CameraManager.Cams[0].P + (MousePosition / CameraManager.Cams[0].Z) + CenterOfMass;
            
            Vector2 SelectedChunk = CameraPosition / (QuadSize * ChunkSize);
            SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
            SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);


            Vector2 ChunkPosition = (CameraPosition / QuadSize) - (SelectedChunk * ChunkSize);
            ChunkPosition.X = (float)Math.Round(ChunkPosition.X);
            ChunkPosition.Y = (float)Math.Round(ChunkPosition.Y);
            
            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F1))
            {
                //Serializer.SerializeObject("test", TestChunk[0][0]);
            }

            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F2))
            {
                TestChunk[0][0] = Serializer.DeSerializeObject("test");
                TestChunk[0][0].SetupChunk();
                TestChunk[0][0].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * 0, QuadSize * ChunkSize * 0) - CenterOfMass, QuadSize, ChunkSize);
            }

            if (SelectedChunk.X >= 0 && SelectedChunk.X < WorldX && SelectedChunk.Y >= 0 && SelectedChunk.Y < WorldY)
            {
                if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                {
                    DrawCircle((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, -1f, 0);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, -0.1f);
                }
                else if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                {
                    DrawCircle((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, 1f, 1);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, 0.1f);
                }
            }

            if (EditedPoints.Count > 0)
            {
                EditedPoints = EditedPoints.Distinct().ToList();
                List<Vector2> EditChunks = new List<Vector2>();
                for (int i = 0; i < EditedPoints.Count; i++)
                {
                    EditChunks.Add(new Vector2(EditedPoints[i].X, EditedPoints[i].Y));
                    if (EditedPoints[i].Z == 0)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X - 1, EditedPoints[i].Y));
                    }

                    if (EditedPoints[i].W == 0)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X, EditedPoints[i].Y - 1));
                    }

                    if (EditedPoints[i].Z == ChunkSize)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X + 1, EditedPoints[i].Y));
                    }

                    if (EditedPoints[i].W == ChunkSize)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X, EditedPoints[i].Y + 1));
                    }
                }
                EditChunks = EditChunks.Distinct().ToList();
                for (int i = 0; i < EditChunks.Count; i++)                
                    if (EditChunks[i].X < 0 || EditChunks[i].Y < 0 || EditChunks[i].X >= TestChunk.Length || EditChunks[i].Y >= TestChunk.Length)
                    {
                        EditChunks.RemoveAt(i);
                        i--;
                    }

                for (int i = 0; i < EditChunks.Count; i++)
                {
                    TestChunk[(int)EditChunks[i].X][(int)EditChunks[i].Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * EditChunks[i].X, QuadSize * ChunkSize * EditChunks[i].Y) - CenterOfMass, QuadSize, ChunkSize);
                }


                //if (Edge.X != 0 && Edge.Y != 0)
                //{
                //    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y) - CenterOfMass, QuadSize, ChunkSize);
                //    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)) - CenterOfMass, QuadSize, ChunkSize);
                //    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)) - CenterOfMass, QuadSize, ChunkSize);
                //    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * SelectedChunk.Y) - CenterOfMass, QuadSize, ChunkSize);
                //}
                //else if (Edge.X != 0 || Edge.Y != 0)
                //{
                //    if (Edge.X != 0)
                //        TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);

                //    if (Edge.Y != 0)
                //        TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);

                //    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);
                //}

                EditedPoints.Clear();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    //if (TestChunk[x][y] != null)
                        //TestChunk[x][y].Draw(spriteBatch, graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y) - CenterOfMass, QuadSize);
                }

            spriteBatch.Draw(StaticTests.Marker, Position, null, Color.Yellow * 0.5f, 0f,
                new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(10f, 10f), SpriteEffects.None, 1f);

            //for (int i = 0; i < CollisionLines.Count; i++)
            //    spriteBatch.Draw(StaticTests.Marker, CollisionLines[i] - new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), null, Color.Orange, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 1f);

            //for (int i = 0; i < CollisionChunks.Count; i++)            
            //    spriteBatch.Draw(StaticTests.Marker, CollisionChunks[i] - CenterOfMass - new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), Color.Green);

            for (int i = 0; i < Markers.Count; i++)
                spriteBatch.Draw(StaticTests.Marker, Markers[i].Position - new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), null, Markers[i].Color, 0f, Vector2.Zero, Markers[i].Size, SpriteEffects.None, 1f);

            Markers.Clear();
            Markers.Clear();
            Markers.Clear();
        }
    }
}
