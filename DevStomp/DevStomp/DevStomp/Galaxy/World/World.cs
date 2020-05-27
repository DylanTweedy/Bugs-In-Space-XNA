using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace DevStomp
{
    class World
    {
        public struct ParticleState
        {
            public SpacePosition P;
            public Vector2 Velocity;
            public Vector2 Acceleration;
            public float Rotation;

            public List<long> Chemistry;

            public long ActualMass;
            public float Mass;

            public float Area;
            public float Density;
            public float Radius;

            public float AngularVelocityClouds;
            public float AngularVelocity;
            public float OverlayRotation;
            public float CloudRotation;

            public float AngularMomentum;




            public float ObjectRadius;
            public float ObjectMass;


            public void Initialize(byte ObjectType)
            {
                Rotation = (float)GlobalVariables.RandomNumber.NextDouble() * ((float)Math.PI * 2f);
                CloudRotation = (float)GlobalVariables.RandomNumber.NextDouble() * ((float)Math.PI * 2f);

                AngularVelocity = (float)GlobalVariables.RandomNumber.NextDouble();
                AngularVelocityClouds = (float)GlobalVariables.RandomNumber.NextDouble();


                AngularVelocity = 0f;
            }

            public void Update()
            {
                UpdateMotion();   
            }

            private void UpdateMotion()
            {
                Rotation += AngularVelocity * GlobalVariables.WorldTime;
                CloudRotation += AngularVelocityClouds * GlobalVariables.WorldTime;
                
                if (Rotation > (float)Math.PI * 2f)
                    Rotation -= ((float)Math.PI * 2f) * (Rotation / ((float)Math.PI * 2f));
                else if (Rotation < (float)Math.PI * -2f)
                    Rotation -= ((float)Math.PI * -2f) * (Rotation / ((float)Math.PI * -2f));

                if (CloudRotation > (float)Math.PI * 2f)
                    CloudRotation -= ((float)Math.PI * 2f) * (CloudRotation / ((float)Math.PI * 2f));
                else if (CloudRotation < (float)Math.PI * -2f)
                    CloudRotation -= ((float)Math.PI * -2f) * (CloudRotation / ((float)Math.PI * -2f));

                Velocity += Acceleration * GlobalVariables.WorldTime;
                P.Position += Velocity * GlobalVariables.WorldTime;

                P.Update();
            }
                       

            public void testAngular()
            {
                AngularMomentum = ((ObjectMass * ObjectRadius * ObjectRadius) / 2f) * AngularVelocity;
            }


        }

        public ParticleState State;


        Chunk[][] TestChunk;
        public short QuadSize;
        public short ChunkSize;
        public int WorldX;
        public int WorldY;
        public List<Vector4> EditedPoints;
        
        public Vector2 Offset;

        //public State state = new State();

        public Vector2 LoadOffset = Vector2.Zero;


        List<Marker> Markers;
        List<Vector2> ActiveChunks = new List<Vector2>();
        List<Vector2> InactiveChunks = new List<Vector2>();

        public bool Active = true;

        public int WorldID;
        public string WorldName;


        float TransitionLevel;
        
        public float CenterMass;

        public Trail OrbitTrail;
        public byte ObjectType = 255;
        byte previousObjectType = 255;


        byte IDBody;
        byte IDClouds;
        byte IDGlow;
        byte IDOverlay;
        byte IDShadows;
        byte IDSmall;
        byte IDDust = 255;

        Color ColorBody;
        Color ColorClouds;
        Color ColorOverlay;

        public byte LightElement;
        public bool ChemistryChange;
        public short UpperLimit;
        public float CloudPercent;

        
        int SelectedPlanet = -1;

        Color ObjectColour = Color.White;


        //if (WorldID == SelectedPlanet)
        //{
        //    CameraManager.Cams[0].SetPosition(P.Position);
        //    CameraManager.Cams[0].PositionX = P.PositionX;
        //    CameraManager.Cams[0].PositionY = P.PositionY;

        //    float z = WorldVariables.WindowHeight / Radius;
        //    //CameraManager.Cams[0].SetZoom(z / 4f);
        //}

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

        public void Initialize(Vector2 position, GraphicsDevice graphics, int id, float centerMass, float scale, Vector2 velocity, float cloudPercent)
        {
            CenterMass = centerMass;

            WorldID = id;
            WorldName = "Test";

            State = new ParticleState();

            State.Radius = scale;
            CloudPercent = cloudPercent;
            TransitionLevel = 0.1f;
            
            QuadSize = GlobalVariables.TileSize;
            ChunkSize = GlobalVariables.ChunkSize;

            //Mass = (1e+12f * (float)size) * (1e+12f * (float)size);

            EditedPoints = new List<Vector4>();

            //TestChunk = new Chunk[WorldX][];

            ////////////////////////////////////////////////////
            Markers = new List<Marker>();
            ////////////////////////////////////////////////////

            State.P = new SpacePosition();
            State.P.Position = position;
            State.P.Update();
            Offset = -new Vector2((WorldX * QuadSize * ChunkSize) / 2, (WorldY * QuadSize * ChunkSize) / 2);

            State.Velocity = velocity;

            OrbitTrail = new Trail();
            OrbitTrail.Initialize(graphics, 50, 10000f);

            State.Initialize(ObjectType);

            SetupChemistry();
        }

        List<Color> Pallet = new List<Color>();

        public void SetupTextures()
        {
            if (previousObjectType != ObjectType)
            {
                //switch (ObjectType)
                //{
                //    case 0:
                //        State.OverlayRotation = 0f;
                //        IDBody = (byte)GlobalVariables.RandomNumber.Next(0, SM.AsteroidBody.Elements);
                //        IDClouds = (byte)GlobalVariables.RandomNumber.Next(0, SM.AsteroidMask.Elements);
                //        IDGlow = (byte)GlobalVariables.RandomNumber.Next(0, SM.AsteroidOverlay1.Elements);
                //        IDOverlay = (byte)GlobalVariables.RandomNumber.Next(0, SM.AsteroidOverlay2.Elements);
                //        IDSmall = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBack.Elements);
                //        IDShadows = 0;
                //        if (IDDust == 255)
                //            IDDust = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBig.Elements);
                //        break;

                //    case 1:
                //        State.OverlayRotation = (float)GlobalVariables.RandomNumber.NextDouble() * ((float)Math.PI * 2f);
                //        IDBody = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetBody.Elements);
                //        IDClouds = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetClouds.Elements);
                //        IDGlow = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetGlow.Elements);
                //        IDOverlay = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetOverlay.Elements);
                //        IDShadows = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetShadows.Elements);
                //        IDSmall = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBack.Elements);
                //        if (IDDust == 255)
                //            IDDust = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBig.Elements);
                //        break;

                //    case 2:
                //        State.OverlayRotation = (float)GlobalVariables.RandomNumber.NextDouble() * ((float)Math.PI * 2f);
                //        IDBody = (byte)GlobalVariables.RandomNumber.Next(0, SM.GasBody.Elements);
                //        IDClouds = (byte)GlobalVariables.RandomNumber.Next(0, SM.GasClouds.Elements);
                //        IDGlow = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetGlow.Elements);
                //        IDOverlay = (byte)GlobalVariables.RandomNumber.Next(0, SM.GasOverlay.Elements);
                //        IDShadows = (byte)GlobalVariables.RandomNumber.Next(0, SM.PlanetShadows.Elements);
                //        IDSmall = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBack.Elements);
                //        if (IDDust == 255)
                //            IDDust = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBig.Elements);
                //        break;

                //    case 3:
                //        State.OverlayRotation = (float)GlobalVariables.RandomNumber.NextDouble() * ((float)Math.PI * 2f);
                //        IDBody = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarBody.Elements);
                //        IDClouds = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarColor.Elements);
                //        IDGlow = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarGlow.Elements);
                //        IDOverlay = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarBody.Elements);
                //        IDSmall = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBack.Elements);
                //        IDShadows = 0;
                //        if (IDDust == 255)
                //            IDDust = (byte)GlobalVariables.RandomNumber.Next(0, SM.StarsBig.Elements);
                //        break;
                //}

                if (Pallet.Count == 0)
                {
                    Pallet = ColorManager.Triad(ColorManager.RandomFullColor(), false, GlobalVariables.RandomNumber.Next(30, 151));

                    if (ObjectType == 0)
                    {
                        byte tint = (byte)GlobalVariables.RandomNumber.Next(155, 256);
                        Pallet[0] = new Color(tint, tint, tint);
                    }

                    for (int i = 0; i < Pallet.Count; i++)
                    {
                        Pallet[i] = ColorManager.SaturationMultiplier(Pallet[i], GlobalVariables.RandomNumber.Next(0, 5000) / 10000f);
                    }

                    ColorBody = Pallet[0];
                    ColorClouds = Pallet[1];
                    ColorOverlay = Pallet[2];
                }
            }
        }


        private void SetupChemistry()
        {
            State.Area = (float)Math.PI * (State.Radius * State.Radius);

            if (State.Area < 1f)
                State.Area = 1f;

            State.Chemistry = ElementTable.BuildTable(GlobalVariables.MassConstant * (long)(State.Area / (State.Radius * 2)));

            ChemistryChange = true;
            UpdateChemistry();
        }

        public void UpdateChemistry()
        {
            if (CloudPercent != 0)
            {
                float change;
                change = (1f - CloudPercent);

                change /= 10f;

                if (change < 0.00001f)
                    change = 0.00001f;

                CloudPercent -= change * (float)GlobalVariables.FrameTime;

                if (CloudPercent < 0.01f)
                    CloudPercent = 0f;

                State.ObjectMass = State.Mass * (1 - CloudPercent);
                State.ObjectRadius = State.Radius * (1f - CloudPercent);

                SetWorldSize();
            }

            if (ObjectType == 3)
            {
                int mult = (int)(10000000000f * GlobalVariables.FrameTime);

                if (State.Chemistry[LightElement] > 2 * mult)
                {
                    State.Chemistry[LightElement] -= 2 * mult;
                    State.Chemistry[LightElement + 1] += 1 * mult;
                    UpdateScale();
                }
                else
                {
                    State.Chemistry[LightElement] -= State.Chemistry[LightElement];
                    State.Chemistry[LightElement + 1] += State.Chemistry[LightElement];
                    LightElement++;
                    ChemistryChange = true;
                }
            }

            if (ChemistryChange)
            {
                State.ActualMass = 0;

                for (int i = State.Chemistry.Count - 1; i >= 0; i--)
                {
                    State.ActualMass += State.Chemistry[i] * (i);
                }

                for (int i = 1; i < State.Chemistry.Count; i++)
                    if (State.Chemistry[i] != 0)
                    {
                        LightElement = (byte)i;
                        break;
                    }


                State.Mass = (float)State.ActualMass * 10f;
                State.ObjectMass = State.Mass * ((1 - CloudPercent) * (1 - CloudPercent));

                UpdateScale();
                ChemistryChange = false;
            }
        }


        #region World Editing

        public Vector2 SetEdges(int X, int Y)
        {
            Vector2 Edges = Vector2.Zero;

            int XTarget;
            int YTarget;

            XTarget = X - 1;
            YTarget = Y;
            if (XTarget >= 0 && XTarget < WorldX && TestChunk[XTarget][YTarget] != null)
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
            if (XTarget >= 0 && XTarget < WorldX && TestChunk[XTarget][YTarget] != null)
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
            if (YTarget >= 0 && YTarget < WorldY && TestChunk[XTarget][YTarget] != null)
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
            if (YTarget >= 0 && YTarget < WorldY && TestChunk[XTarget][YTarget] != null)
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

            if (CX < 0 || CY < 0 || CX >= TestChunk.Length || (TestChunk[CX] != null && CY >= TestChunk[CX].Length))
            {
                //if (CX == -1 || CY == -1 || CX == TestChunk.Length || CY == TestChunk[CX].Length)
                {
                    //RESIZE WORLD CODE
                    //This works for x + 1 and y + 1 (more or less).

                    //if (CX == TestChunk.Length)
                    //{
                    //    Array.Resize(ref TestChunk, TestChunk.Length + 1);

                    //    WorldX++;
                    //    Offset.X = -(WorldX * QuadSize * ChunkSize) / 2;
                    //    Position.X += (QuadSize * ChunkSize) / 2;

                    //    TestChunk[CX] = new Chunk[WorldY];                        
                    //}

                    //if (TestChunk[CX] != null && CY == TestChunk[CX].Length)
                    //{
                    //    Array.Resize(ref TestChunk[CX], TestChunk[CX].Length + 1);

                    //    WorldY++;
                    //    Offset.Y = -(WorldY * QuadSize * ChunkSize) / 2;
                    //    Position.Y += (QuadSize * ChunkSize) / 2;
                    //}
                }
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
            //    int ScaleiusError = 1 - x;

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
            //        if (ScaleiusError < 0)
            //        {
            //            ScaleiusError += 2 * y + 1;
            //        }
            //        else
            //        {
            //            x--;
            //            ScaleiusError += 2 * (y - x + 1);
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

        #endregion

        #region Collision Detection

        public CollisionData GetCollisionData(Vector2 Pos, Vector2 PreviousPos)
        {
            //Pos.X = (float)Math.Round(Pos.X) + 0.1f;
            //Pos.Y = (float)Math.Round(Pos.Y) + 0.1f;

            if ((Pos.X / QuadSize) - Math.Floor(Pos.X / QuadSize) < 0.001f)
            {
                Pos.X -= 1f;
            }


            if (DebugOptions.DebugActive)
                Markers.Add(new Marker(Pos, Color.Pink, 0.5f));

            Vector2 Dis = Pos - PreviousPos;
            float distance = Vector2.Distance(Vector2.Zero, Dis) / (QuadSize / 2);
            CollisionData collisionPoint;

            if (distance <= 1f)
            {
                collisionPoint = CheckQuadCollision(Pos);

                if (collisionPoint != null)
                {
                    if (collisionPoint.Solid)
                    {
                        collisionPoint = SolidCollision(Pos);
                        collisionPoint.DistanceLeft = Vector2.Distance(collisionPoint.Position, Pos);
                        return collisionPoint;
                    }

                    if (DebugOptions.DebugActive)
                        Markers.Add(new Marker(collisionPoint.Position, Color.Red, 1f));
                    collisionPoint.DistanceLeft = Vector2.Distance(collisionPoint.Position, Pos);
                    return collisionPoint;
                }
            }
            else
            {
                Dis /= distance;

                for (int i = 0; i <= distance; i++)
                {
                    collisionPoint = CheckQuadCollision(PreviousPos + (Dis * i));

                    if (DebugOptions.DebugActive)
                        Markers.Add(new Marker(PreviousPos + (Dis * i), Color.Orange, 0.2f));

                    if (collisionPoint != null)
                    {
                        if (collisionPoint.Solid)
                        {
                            collisionPoint = SolidCollision(PreviousPos + (Dis * i));
                            collisionPoint.DistanceLeft = Vector2.Distance(collisionPoint.Position, Pos);
                            return collisionPoint;
                        }

                        if (DebugOptions.DebugActive)
                            Markers.Add(new Marker(collisionPoint.Position, Color.Red, 1f));
                        collisionPoint.DistanceLeft = Vector2.Distance(collisionPoint.Position, Pos);
                        return collisionPoint;
                    }
                }
            }

            return null;
        }

        CollisionData SolidCollision(Vector2 Pos)
        {
            int r = 0;
            float steps;
            
            List<Vector2> Points = new List<Vector2>();
            CollisionData collisionPoint = new CollisionData(Vector2.Zero, 0f, 0f);
            Vector2 half = new Vector2(0.5f, 0.5f);

            while (Points.Count == 0)
            {
                r++;
                steps = 360f / (8f * r);

                for (float i = 0; i < 360.0; i += steps)
                {
                    double angle = i * System.Math.PI / 180;
                    int x = (int)(r * System.Math.Cos(angle));
                    int y = (int)(r * System.Math.Sin(angle));

                    if (DebugOptions.DebugActive)
                        Markers.Add(new Marker(Pos + new Vector2((QuadSize / 2) * x, (QuadSize / 2) * y), Color.Purple, 0.5f));

                    collisionPoint = CheckQuadCollision(Pos + new Vector2((QuadSize / 2) * x, (QuadSize / 2) * y));

                    if (collisionPoint != null && !collisionPoint.Solid)
                    {
                        Points.Add(collisionPoint.Position);
                    }
                }
            }
                                    
            List<float> distances = new List<float>();
            float distance = float.MaxValue;

            for (int i = 0; i < Points.Count; i++)            
                distances.Add(Vector2.Distance(Points[i], Pos));

            if (distances.Count > 1)
            {
                for (int i = 0; i < distances.Count; i++)
                    distance = Math.Min(distances[i], distance);
            }
            else
                distance = distances[0];
            
            for (int i = 0; i < distances.Count; i++)
                if (distance == distances[i])
                {
                    if (DebugOptions.DebugActive)
                        Markers.Add(new Marker(Points[i], Color.Red, 1f));
                    return new CollisionData(Points[i], 0f, 0f);
                }

            return collisionPoint;
        }

        CollisionData CheckQuadCollision(Vector2 Pos)
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

                                //Vector2 ClosestPoint = Vector2.Zero;
                                CollisionData ClosestPoint = new CollisionData(Vector2.Zero, 0f, 0f);

                                for (int i = 0; i < Distances.Count; i++)
                                    if (smallest == Distances[i])
                                    {
                                        ClosestPoint = new CollisionData(GetClosetPoint(Points[p][i], Points[p][i + 1], Pos, true), 0f, (float)Math.Atan2(Points[p][i].Y - Points[p][i + 1].Y, Points[p][i].X - Points[p][i + 1].X));

                                        switch (TestChunk[CP.CX][CP.CY].N[CP.QX][CP.QY])
                                        {
                                            case 12:
                                                ClosestPoint.Angle += (float)Math.PI;
                                                break;
                                        }                                    
                                    }
                                ClosestPoint.Position -= ((Pos - ClosestPoint.Position) * 0.01f);

                                List<Vector2[]> ClosePoints = GetVertices(ClosestPoint.Position, null);

                                bool InPolygon = false;

                                if (ClosePoints != null)
                                    for (int p2 = 0; p2 < ClosePoints.Count; p2++)
                                        if (IsPointInPolygon(ClosestPoint.Position, ClosePoints[p2]))
                                            InPolygon = true;

                                if (!InPolygon)
                                {                                    


                                    return ClosestPoint;
                                }
                                else
                                    ClosestPoints.Add(ClosestPoint.Position);

                                for (int i = 0; i < Distances.Count; i++)
                                    if (smallest == Distances[i])
                                        Distances[i] = float.MaxValue;

                                if (o == 0)
                                    FirstPoint = ClosestPoint.Position;
                            }

                            //return Pos - new Vector2(0, QuadSize);

                            //Make it so it returns a position one quad away from the center of mass.
                            //return ;
                        }
                }
                else return new CollisionData();

            return null;
        }

        #region Collision Checks
        ChunkPosition GetChunkPosition(Vector2 Pos)
        {
            Pos -= State.P.Position + Offset;

            Vector2 chunk = Pos / (QuadSize * ChunkSize);
            chunk.X = (float)Math.Floor(chunk.X);
            chunk.Y = (float)Math.Floor(chunk.Y);

            Vector2 quad = (Pos / QuadSize) - (chunk * ChunkSize);
            quad.X = (float)Math.Floor(quad.X);
            quad.Y = (float)Math.Floor(quad.Y);

            if (chunk.X >= 0 && chunk.X < WorldX && chunk.Y >= 0 && chunk.Y < WorldY)
                return new ChunkPosition((int)chunk.X, (int)chunk.Y, (int)quad.X, (int)quad.Y);

            return null;
        }

        List<Vector2[]> GetVertices(Vector2 Pos, ChunkPosition CP)
        {
            if (CP == null)
                CP = GetChunkPosition(Pos);

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
        
        public bool IsPointInPolygon(Vector2 p, Vector2[] polygon)
        {
            if (polygon == null)
                return false;

            float minX = polygon[0].X;
            float maxX = polygon[0].X;
            float minY = polygon[0].Y;
            float maxY = polygon[0].Y;
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
        
        Vector2 GetClosetPoint(Vector2 A, Vector2 B, Vector2 P, bool segmentClamp)
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
        
        #region OtherCollision
        //bool CheckCollision(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        //{
        //    float denominator = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
        //    float numerator1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
        //    float numerator2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));

        //    // Detect coincident lines (has a problem, read below)
        //    if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

        //    float r = numerator1 / denominator;
        //    float s = numerator2 / denominator;

        //    return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        //}

        //Vector2 IntersectionPoint(Vector2 Position, Vector2 PreviousPosition, Vector2 Point1, Vector2 Point2)
        //{
        //    // Get A,B,C of first line - points : ps1 to pe1
        //    float A1 = Position.Y - PreviousPosition.Y;
        //    float B1 = PreviousPosition.X - Position.X;
        //    float C1 = A1 * PreviousPosition.X + B1 * PreviousPosition.Y;

        //    // Get A,B,C of second line - points : ps2 to pe2
        //    float A2 = Point1.Y - Point2.Y;
        //    float B2 = Point2.X - Point1.X;
        //    float C2 = A2 * Point2.X + B2 * Point2.Y;

        //    // Get delta and check if the lines are parallel
        //    float delta = A1 * B2 - A2 * B1;
        //    if (delta == 0)            
        //        return Vector2.Zero;            
        //    else return
        //    new Vector2(
        //    (B2 * C1 - B1 * C2) / delta,
        //    (A1 * C2 - A2 * C1) / delta);
        //}
        #endregion
        #endregion
        #endregion

        Vector2 GetChunk(Vector2 Pos)
        {
            Pos -= State.P.Position + Offset;

            Vector2 chunk = Pos / (QuadSize * ChunkSize);
            chunk.X = (float)Math.Floor(chunk.X);
            chunk.Y = (float)Math.Floor(chunk.Y);

            return chunk;
        }

        public void SetWorldSize()
        {
            previousObjectType = ObjectType;

            if (UpperLimit == 0)
                UpperLimit = (short)GlobalVariables.RandomNumber.Next(1750, 2000);

            int size = (int)((State.Radius / (QuadSize * ChunkSize)) * 2f);
            if (size > UpperLimit)
                size = UpperLimit;
            WorldX = size;
            WorldY = size;

            if (State.ObjectMass < 1e+10f)
                ObjectType = 0;
            else if (State.ObjectMass < 1e+11f)
                ObjectType = 1;
            else if (State.ObjectMass < 1e+13f)
                ObjectType = 2;
            else
                ObjectType = 3;

            SetupTextures();
        }

        public void UpdateScale()
        {
            long m = 0;

            for (int i = 1; i < State.Chemistry.Count; i++)
                m += State.Chemistry[i];
            
            //float r = Radius;
            State.Radius = m / GlobalVariables.MassConstant;

            State.ObjectRadius = State.Radius * (1f - CloudPercent);
            State.Area = (float)Math.PI * (State.Radius * State.Radius);
            State.Density = State.Mass / State.Area;

            SetWorldSize();
        }


        public void Update(GraphicsDevice graphics)
        {
            //if (WorldX > WorldY)
            //    Radius = (WorldX / 2f) * QuadSize * ChunkSize;
            //else
            //    Radius = (WorldY / 2f) * QuadSize * ChunkSize;

            //if (ObjectType == 0)
            //    Radius *= 3;
            //if (ObjectType == 5)
            //    Radius *= 250;
            
            UpdateChemistry();
            CheckPosition();

            State.Update();
            //UpdatePosition();

            #region WorldEdit

            if (TestChunk != null)
            {
                GetActiveChunks(graphics);



                //Vector2 MousePosition = new Vector2(InputManager.M.X - (WorldVariables.WindowWidth / 2), InputManager.M.Y - (WorldVariables.WindowHeight / 2));

                //float WorldAngle = -CameraManager.Cams[0].R;
                //float Distance = Vector2.Distance(Vector2.Zero, MousePosition);
                //float MouseAngle = (float)Math.Atan2(MousePosition.X, -MousePosition.Y) + WorldAngle;
                //MousePosition = new Vector2((float)Math.Sin(MouseAngle), -(float)Math.Cos(MouseAngle)) * Distance;



                //Vector2 CameraPosition = CameraManager.Cams[0].P + (MousePosition / CameraManager.Cams[0].Z) + CenterOfMass;
                Vector2 CameraPosition = InputManager.GetMousePosition(0) - (State.P.Position + Offset);

                Vector2 SelectedChunk = CameraPosition / (QuadSize * ChunkSize);
                SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
                SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);


                Vector2 ChunkPosition = (CameraPosition / QuadSize) - (SelectedChunk * ChunkSize);
                ChunkPosition.X = (float)Math.Round(ChunkPosition.X);
                ChunkPosition.Y = (float)Math.Round(ChunkPosition.Y);

                //if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                //{
                //    DrawCircle((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, -1f, 0);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, -0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, -0.1f);
                //}
                //else if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                //{
                //    DrawCircle((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, 1f, 1);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, 0.1f);
                //    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, 0.1f);
                //}


                UpdateEditedPoints(graphics);

                SortChunks();
            }
            #endregion


            UpdatePath(graphics);

            //CollisionTimer += (float)WorldVariables.FrameTime;
        }

        private void UpdatePath(GraphicsDevice graphics)
        {

            Vector2 rough = State.P.RoughPosition;

            //SpacePosition NewP = new SpacePosition();
            //NewP.Position = P.Position;
            //NewP.PositionX = P.PositionX;
            //NewP.PositionY = P.PositionY;

            //Locations.Add(NewP);


            //if (Locations.Count > max)
            //{
            //    Locations.RemoveAt(0);
            //}


            Color c = Color.White;

            switch (ObjectType)
            {
                case 0:
                    if (WorldID == 0)
                        c = Color.White;
                    else
                        c = Color.Red;
                    break;

                case 1:
                    c = Color.Orange;
                    break;

                case 2:
                    c = Color.Green;
                    break;

                case 3:
                    c = Color.Blue;
                    break;

                case 4:
                    c = Color.Yellow;
                    break;
            }

            //if (StrongID == PreviousStrongID)
            //OrbitTrail.MovePoints(StrongPull.GetRough() - PreviousStrongPull.GetRough());

                //OrbitTrail.ClearPoints(P.GetRough());

            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
            {
                OrbitTrail.ClearPoints(rough);

                OrbitTrail.SetColor(c);
                //OrbitTrail.SetLength(500, 500000f, graphics, rough);
                OrbitTrail.SetLength(500, 500000000000000f, graphics, rough);
            }
            else
                OrbitTrail.AddPoint(rough);
        }
                
        private void UpdateEditedPoints(GraphicsDevice graphics)
        {
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
                    //TestChunk[(int)EditChunks[i].X][(int)EditChunks[i].Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * EditChunks[i].X, QuadSize * ChunkSize * EditChunks[i].Y) - CenterOfMass, QuadSize, ChunkSize);
                    TestChunk[(int)EditChunks[i].X][(int)EditChunks[i].Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * EditChunks[i].X, QuadSize * ChunkSize * EditChunks[i].Y) + (State.P.Position + Offset), QuadSize, ChunkSize);
                }

                EditedPoints.Clear();
            }
        }

        private void SortChunks()
        {
            int o = 0;

            if (TestChunk != null)
                for (int i = 0; i < InactiveChunks.Count; i++)
                {
                    int X = (int)InactiveChunks[i].X;
                    int Y = (int)InactiveChunks[i].Y;

                    //Create a way so inactive rows get set to null.
                    //Need to make sure that there are no chunks in memory on that row first
                    //As the world is explored you may run out of memory without this.

                    if (TestChunk[X] != null)
                    {
                        if (TestChunk[X][Y] != null)
                        {
                            Serializer.SerializeObject(WorldID + " - " + WorldName, +X + "-" + Y, TestChunk[X][Y]);
                            TestChunk[X][Y].Clear();
                            TestChunk[X][Y] = null;
                            InactiveChunks.RemoveAt(i);
                        }
                        else
                            InactiveChunks.RemoveAt(i);
                    }
                    else
                        InactiveChunks.RemoveAt(i);

                    i--;
                    o++;

                    if (o >= 10)
                        break;

                }
        }

        public void GetActiveChunks(GraphicsDevice graphics)
        {
            int X = 0;
            int Y = 0;

            //Rotation += 0.75f * (float)WorldVariables.FrameTime;



            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                CameraManager.Cams[0].SetRotation(CameraManager.Cams[0].Rotation + (0.5f * (float)GlobalVariables.FrameTime));
            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                CameraManager.Cams[0].SetRotation(CameraManager.Cams[0].Rotation - (0.5f * (float)GlobalVariables.FrameTime));


            CameraManager.Cams[0].SetRotation(0f);


            List<Vector2> activeChunks = new List<Vector2>();

            for (int i = 0; i < CameraManager.Cams.Count; i++)
            {
                if (CameraManager.Cams[i].Zoom >= 0.1f)
                {
                    //Something tells me this needs the cameras dimension.

                    //Vector2 min = GetChunk(CameraManager.Cams[i].P - (CameraManager.Cams[i].WindowDimensions / 2 / CameraManager.Cams[i].Z));
                    //Vector2 max = GetChunk(CameraManager.Cams[i].P + (CameraManager.Cams[i].WindowDimensions / 2 / CameraManager.Cams[i].Z));

                    //for (int x2 = (int)min.X - 2; x2 <= max.X + 2; x2++)
                    //    for (int y2 = (int)min.Y - 2; y2 <= max.Y + 2; y2++)
                    //    {
                    //        int x = x2;
                    //        int y = y2;

                    //        Vector2 relativeLocation = new Vector2(x2 - (WorldX / 2f), y2 - (WorldY / 2f));
                    //        Vector2 halfWorld = new Vector2(WorldX / 2f, WorldY / 2f);





                    //        float distance = Vector2.Distance(halfWorld, new Vector2(x2, y2));
                    //        float centerAngle = UsefulMethods.VectorToAngle(relativeLocation);
                    //        centerAngle -= State.Rotation;

                    //        Vector2 finalRotation = distance * UsefulMethods.AngleToVector(centerAngle) + halfWorld;


                    //        //TODO: This can probably be done much more efficiently. ALSO get working for Camera rotation.




                    //        //relativeLocation = GetChunk(CameraManager.Cams[i].P);
                    //        //relativeLocation = new Vector2(finalRotation.X - relativeLocation.X, finalRotation.Y - relativeLocation.X);




                    //        //distance = Vector2.Distance(halfWorld, new Vector2(finalRotation.X, finalRotation.Y));
                    //        //centerAngle = UsefulMethods.VectorToAngle(relativeLocation);
                    //        //centerAngle += CameraManager.Cams[i].R;

                    //        //finalRotation = distance * UsefulMethods.AngleToVector(centerAngle) + halfWorld;

                    //        x = (int)Math.Round(finalRotation.X);
                    //        y = (int)Math.Round(finalRotation.Y);


                            
                    //        if (x >= 0 && y >= 0 && x < WorldX && y < WorldY)
                    //        {
                    //            activeChunks.Add(new Vector2(x, y));

                    //            //TODO: This can probably be done much more efficiently.

                    //            x++;

                    //            if (x >= 0 && y >= 0 && x < WorldX && y < WorldY)
                    //                activeChunks.Add(new Vector2(x, y));

                    //            x--;
                    //            y++;

                    //            if (x >= 0 && y >= 0 && x < WorldX && y < WorldY)
                    //                activeChunks.Add(new Vector2(x, y));

                    //            x++;

                    //            if (x >= 0 && y >= 0 && x < WorldX && y < WorldY)
                    //                activeChunks.Add(new Vector2(x, y));
                    //        }
                    //    }
                }
            }
            
            activeChunks = activeChunks.Distinct().ToList();

            InactiveChunks.AddRange(ActiveChunks.Except(activeChunks).ToList());
            InactiveChunks = InactiveChunks.Distinct().ToList();
            InactiveChunks = InactiveChunks.Except(activeChunks).ToList();

            ActiveChunks = activeChunks;
            int o = 0;


                for (int i = 0; i < ActiveChunks.Count; i++)
                {
                    X = (int)ActiveChunks[i].X;
                    Y = (int)ActiveChunks[i].Y;


                    if (TestChunk[X] == null)
                        TestChunk[X] = new Chunk[WorldY];

                    if (TestChunk[X].Length < WorldY)
                        Array.Resize(ref TestChunk[X], WorldY);


                    if (TestChunk[X][Y] == null)
                    {
                        TestChunk[X][Y] = Serializer.DeSerializeObject(WorldID + " - " + WorldName + "\\" + X + "-" + Y);


                        if (TestChunk[X][Y] == null)
                        {
                            TestChunk[X][Y] = new Chunk();

                            float x = (X + 0.5f) - (WorldX / 2f);
                            float y = (Y + 0.5f) - (WorldY / 2f);
                            x *= x;
                            y *= y;

                            if (x + y <= (WorldX / 2) * (WorldX / 2))
                                TestChunk[X][Y].InitializeRandom();
                            else
                                TestChunk[X][Y].InitializeBlank();
                        }

                        TestChunk[X][Y].SetupChunk();
                        //SetEdges(X, Y);
                        TestChunk[X][Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * X, QuadSize * ChunkSize * Y) + (State.P.Position + Offset), QuadSize, ChunkSize);

                        o++;

                        if (o >= 3)
                            break;
                    }
                }
        }

        private void CheckPosition()
        {
            for (int i = 0; i < CameraManager.Cams.Count; i++)
            {
                if (CameraManager.Cams[i].Zoom > TransitionLevel)
                {
                    float Distance = Vector2.Distance(State.P.RoughPosition, CameraManager.Cams[i].GetRoughPosition());

                    if (Distance < State.Radius * 2)
                    {
                        if (TestChunk == null)
                            TestChunk = new Chunk[WorldX][];
                    }
                    else if (InactiveChunks.Count == 0 && ActiveChunks.Count == 0)
                        TestChunk = null;
                }
                else if (InactiveChunks.Count == 0 && ActiveChunks.Count == 0)
                    TestChunk = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera cam)
        {
        //    int OffsetX = (int)(State.P.PositionX - cam.PositionX);
        //    int OffsetY = (int)(State.P.PositionY - cam.PositionY);
        //    Vector2 DrawOffset = new Vector2(OffsetX * 2000000f, OffsetY * 2000000f);
        //    float objectScale = (State.Radius * 2f) * cam.Zoom;
        //    float originalObjectScale = objectScale;
        //    int size = 0;
        //        //SM.PlanetBody.Size;
        //    Vector2 halfsize = new Vector2(size / 2f, size / 2f);
        //    float Scale = (float)(State.Radius * 2f) / size;
        //    float multiplier = 1f;
        //    float Alpha = 1f;

        //    float scaleHigh = 32;
        //    float scaleLow = 16;

        //    if (ObjectType == 3)
        //    {
        //        scaleHigh = 96;
        //        scaleLow = 32;
        //    }

        //    //if (ObjectType == 0 || ObjectType == 1)
        //        //OrbitTrail.Draw(graphics, camera);

        //    #region Resize Appearance

        //    if (!InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.L))
        //        switch (ObjectType)
        //        {
        //            //case 0:
        //            //    if (Scale * CameraManager.Cams[camera].Z < 0.06f)
        //            //    {
        //            //        multiplier = (0.06f / (Scale * CameraManager.Cams[camera].Z));
        //            //        Scale *= multiplier;
        //            //        objectScale *= multiplier;
        //            //    }
        //            //    break;
        //            //case 1:
        //            //    if (Scale * CameraManager.Cams[camera].Z < 0.06f)
        //            //    {
        //            //        multiplier = (0.04f / (Scale * CameraManager.Cams[camera].Z));
        //            //        Scale *= multiplier;
        //            //        objectScale *= multiplier;
        //            //    }
        //            //    break;
        //            //case 2:
        //            //    if (Scale * CameraManager.Cams[camera].Z < 0.06f)
        //            //    {
        //            //        multiplier = (0.03f / (Scale * CameraManager.Cams[camera].Z));
        //            //        Scale *= multiplier;
        //            //        objectScale *= multiplier;
        //            //    }
        //            //    break;
        //            //case 3:
        //            //    if (Scale * CameraManager.Cams[camera].Z < 0.06f)
        //            //    {
        //            //        multiplier = (0.02f / (Scale * CameraManager.Cams[camera].Z));
        //            //        Scale *= multiplier;
        //            //        objectScale *= multiplier;
        //            //    }
        //            //    break;
        //            //case 4:
        //            //    if (Scale * CameraManager.Cams[camera].Z < 0.06f)
        //            //    {
        //            //        multiplier = (0.015f / (Scale * CameraManager.Cams[camera].Z));
        //            //        Scale *= multiplier;
        //            //        objectScale *= multiplier;
        //            //    }
        //            //    break;

        //            default:
        //                //if (Scale * CameraManager.Cams[camera].Z < 0.01f)
        //                if (objectScale < 4f)
        //                {
        //                    multiplier = 4f / objectScale;
        //                    //multiplier = (0.005f / (Scale * CameraManager.Cams[camera].Z));
        //                    Scale *= multiplier;
        //                    objectScale *= multiplier;
        //                }
        //                break;
        //        }

        //    #endregion

        //    #region Texture Selection

        //    Texture2D TexSmall = null;
        //    Texture2D TexBody = null;
        //    Texture2D TexClouds = null;
        //    Texture2D TexGlow = null;
        //    Texture2D TexOverlay = null;
        //    Texture2D TexShadows = null;
        //    Texture2D TexDust = SM.StarsBig.S(IDDust);



        //    switch (ObjectType)
        //    {
        //        case 0:
        //            if (objectScale > 16)
        //            {
        //                TexBody = SM.AsteroidBody.S(IDBody);
        //                TexGlow = SM.AsteroidOverlay1.S(IDGlow);
        //                TexOverlay = SM.AsteroidOverlay2.S(IDOverlay);
        //                TexClouds = null;
        //                TexShadows = null;
        //            }
        //            TexSmall = SM.StarsBack.S(IDSmall);
        //            break;

        //        case 1:
        //            if (objectScale > 16)
        //            {
        //                TexBody = SM.PlanetBody.S(IDBody);
        //                TexClouds = SM.PlanetClouds.S(IDClouds);
        //                TexGlow = SM.PlanetGlow.S(IDGlow);
        //                TexOverlay = SM.PlanetOverlay.S(IDOverlay);
        //                TexShadows = SM.PlanetShadows.S(IDShadows);
        //            }
        //            TexSmall = SM.StarsBack.S(IDSmall);
        //            break;

        //        case 2:
        //            if (objectScale > 16)
        //            {
        //                TexBody = SM.GasBody.S(IDBody);
        //                TexClouds = SM.GasClouds.S(IDClouds);
        //                TexGlow = SM.PlanetGlow.S(IDGlow);
        //                TexOverlay = SM.GasOverlay.S(IDOverlay);
        //                TexShadows = SM.PlanetShadows.S(IDShadows);
        //            }

        //            TexSmall = SM.StarsBack.S(IDSmall);
        //            break;

        //        case 3:
        //            if (objectScale > 16)
        //            {
        //                TexBody = SM.StarBody.S(IDBody);
        //                TexGlow = SM.StarGlow.S(IDGlow);
        //                TexOverlay = SM.StarBody.S(IDOverlay);
        //                TexClouds = SM.StarColor.S(IDGlow);
        //                TexShadows = null;
        //            }
        //            TexSmall = SM.StarsBack.S(IDSmall);
        //            break;
        //    }

        //    #endregion


        //    #region OLD
        //    //float gravForce = (WorldVariables.GravitationalConstant * (float)Mass) / 1000000000000f;
        //    //float acc = (gravForce / (float)Mass);


        //    //if (WorldID != 0)
        //    //spriteBatch.Draw(StaticTests.MarkerCircle, P.Position + DrawOffset, null, Color.Green * 0.5f, 0f,
        //    //    new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), gravForce / StaticTests.Marker.Width, SpriteEffects.None, 1f);
            



        //    //for (int i = 0; i < Locations.Count; i++)
        //    //{
        //    //    float worldX = (((float)(QuadSize * ChunkSize) / 32) * WorldX);
        //    //    float worldY = (((float)(QuadSize * ChunkSize) / 32) * WorldY);

        //    //    Vector2 offset = new Vector2(CameraManager.Cams[camera].PositionX * 2000000, CameraManager.Cams[camera].PositionY * 2000000);

        //    //    spriteBatch.Draw(StaticTests.MarkerCircle, Locations[i].GetRough() - offset, null, Color.Yellow, Rotation,
        //    //    new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(worldX, worldY) / (2), SpriteEffects.None, 0f);
        //    //}

        //    //float test = (Radius * 2f) / size;
        //    //float test2 = (float)(QuadSize * ChunkSize * WorldX) / size;
        //    //float Scale = ((float)(QuadSize * ChunkSize) / size) * WorldX;

        //    //Scale = Radius / size;

        //    //if (ObjectType == 0)
        //    //    Scale *= 3;
        //    #endregion


        //    if (cam.Zoom >= 0.1f)
        //    {
        //        #region Terrain Drawing

        //        if (TestChunk != null)
        //        {
        //            for (int i = 0; i < ActiveChunks.Count; i++)
        //            {
        //                int X = (int)ActiveChunks[i].X;
        //                int Y = (int)ActiveChunks[i].Y;

        //                //Change to new camera code.
        //                if (TestChunk[X] != null)
        //                    if (TestChunk[X].Length > Y && TestChunk[X][Y] != null)
        //                        TestChunk[X][Y].Draw(spriteBatch, graphics, State.P.Position + DrawOffset, new Vector2(QuadSize * ChunkSize * X, QuadSize * ChunkSize * Y) + Offset, QuadSize, State.Rotation, 0);
        //            }
        //        }

        //        #endregion
        //    }
        //    else
        //    {
        //        float worldX = (((float)(QuadSize * ChunkSize) / 32) * WorldX);
        //        float worldY = (((float)(QuadSize * ChunkSize) / 32) * WorldY);

        //        if (objectScale > scaleLow)
        //        {
        //            float alpha = 1f;
        //            if (objectScale < scaleHigh)
        //                Alpha = UsefulMethods.FindBetween(objectScale, scaleHigh, scaleLow, 1f, 0f, false);                    
                    
        //            #region Arrows

        //            bool Acc = cam.settings.SpaceView.DrawAcceleration;
        //            bool Vel = cam.settings.SpaceView.DrawVelocity;
                    
        //            if (Acc || Vel)
        //            {
        //                float ArrowSize = Scale * 8f;

        //                if (objectScale >= 200f)
        //                    alpha = UsefulMethods.FindBetween(objectScale, 400f, 200f, 1f, 0f, true);

        //                if (Acc)
        //                {
        //                    spriteBatch.Draw(StaticTests.Arrow, State.P.Position + DrawOffset, null, ObjectColour * alpha * Alpha, UsefulMethods.VectorToAngle(State.Acceleration),
        //                            new Vector2(StaticTests.Arrow.Width / 2, StaticTests.Arrow.Height), ArrowSize, SpriteEffects.None, 1f);
        //                }
        //                if (Vel)
        //                {
        //                    spriteBatch.Draw(StaticTests.Arrow, State.P.Position + DrawOffset, null, ObjectColour * alpha * Alpha, UsefulMethods.VectorToAngle(State.Velocity),
        //                            new Vector2(StaticTests.Arrow.Width / 2, StaticTests.Arrow.Height), ArrowSize, SpriteEffects.None, 1f);
        //                }
        //            }

        //            #endregion

        //            if (CloudPercent != 0f)
        //            {
        //                spriteBatch.Draw(TexDust, State.P.Position + DrawOffset, SM.StarsBig.Rectangles[IDBody], ColorBody * Alpha * 0.5f, State.CloudRotation, halfsize / 2f,
        //                    (Scale) + (Scale * CloudPercent * 10f), SpriteEffects.None, 0f);

        //                spriteBatch.Draw(TexDust, State.P.Position + DrawOffset, SM.StarsBig.Rectangles[IDClouds], ColorOverlay * Alpha * 0.5f, State.Rotation, halfsize / 2f,
        //                    (Scale) + (Scale * CloudPercent * 10f), SpriteEffects.FlipHorizontally, 0f);
        //            }
                    
        //            if (CloudPercent != 1f)
        //            {
        //                spriteBatch.Draw(TexBody, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDBody], ColorBody * Alpha, State.Rotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);

        //                switch (ObjectType)
        //                {
        //                    case 0:
        //                        spriteBatch.Draw(TexOverlay, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDOverlay], ColorOverlay * 0.7f * Alpha, State.Rotation + State.OverlayRotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexGlow, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDGlow], ColorOverlay * 0.7f * Alpha, State.Rotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        break;

        //                    case 3:
        //                        spriteBatch.Draw(TexClouds, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDClouds], ColorBody * 0.2f * Alpha, -State.Rotation * 2f, halfsize, Scale * 2f * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexClouds, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDClouds], ColorBody * 0.2f * Alpha, State.Rotation * 2f, halfsize, Scale * 2f * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexBody, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDBody], Color.White * 0.5f * Alpha, State.Rotation + State.OverlayRotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexGlow, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDGlow], ColorBody * 1f * Alpha, State.CloudRotation + State.OverlayRotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexGlow, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDGlow], Color.White * 0.7f * Alpha, State.CloudRotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        break;

        //                    default:
        //                        spriteBatch.Draw(TexOverlay, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDOverlay], ColorOverlay * 0.7f * Alpha, State.Rotation + State.OverlayRotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexClouds, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDClouds], ColorClouds * 1f * Alpha, State.Rotation - State.OverlayRotation, halfsize, Scale * 1.01f * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        spriteBatch.Draw(TexGlow, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDGlow], ColorOverlay * 0.8f * Alpha, State.Rotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        float angle = UsefulMethods.VectorToAngle(-State.P.RoughPosition);
        //                        angle = 0f;
        //                        spriteBatch.Draw(TexShadows, State.P.Position + DrawOffset, SM.PlanetBody.Rectangles[IDShadows], Color.White * Alpha, State.Rotation, halfsize, Scale * (1f - CloudPercent), SpriteEffects.None, 0f);
        //                        break;
        //                }
        //            }


        //            if (Alpha != 1f)
        //                spriteBatch.Draw(TexSmall, State.P.Position + DrawOffset, SM.StarsBack.Rectangles[IDSmall], ColorBody * (1f - Alpha), State.Rotation, new Vector2(16, 16), Scale * 128, SpriteEffects.None, 0f);

        //            #region Information Box

        //            if (cam.settings.SpaceView.ShowInfo)
        //            {
        //                alpha = 1f;
        //                if (objectScale >= 200f)
        //                    alpha = UsefulMethods.FindBetween(objectScale, 350f, 200f, 1f, 0f, true);
        //                else if (objectScale <= 125f)
        //                    alpha = UsefulMethods.FindBetween(objectScale, 125f, 64f, 1f, 0f, false);

        //                if (alpha > 0f)
        //                {
        //                    string WorldType = "";

        //                    switch (ObjectType)
        //                    {
        //                        case 0:
        //                            WorldType = "Asteroid";
        //                            break;

        //                        case 1:
        //                            WorldType = "Planetoid";
        //                            break;

        //                        case 2:
        //                            WorldType = "Gas Giant";
        //                            break;

        //                        case 3:
        //                            WorldType = "Star";
        //                            break;
        //                    }


        //                    StaticInfoBox.AddItem("Type: " + WorldType);
        //                    StaticInfoBox.AddItem("Acceleration: " + Vector2.Distance(Vector2.Zero, State.Acceleration));
        //                    StaticInfoBox.AddItem("Velocity: " + Vector2.Distance(Vector2.Zero, State.Velocity));
        //                    StaticInfoBox.AddItem("Mass: " + Write.Number(new Number(State.ObjectMass), 1, 0, 1, 1, "Grams"));
        //                    StaticInfoBox.AddItem("Area: " + (float)State.Area);
        //                    StaticInfoBox.AddItem("Scale: " + Write.Number(new Number(State.ObjectRadius), 1, 0, 1, 2, "Centimeters"));
        //                    StaticInfoBox.AddItem("Density: " + State.Density);
        //                    StaticInfoBox.AddItem("Cloud Percent: " + CloudPercent);

        //                    if (State.AngularVelocity != 0)
        //                        StaticInfoBox.AddItem("Revolution: " + Write.Number(new Number(((float)Math.PI * 2f) / Math.Abs(State.AngularVelocity)), 1, 0, 1, 3, "G:Time 1"));
        //                    else
        //                        StaticInfoBox.AddItem("Revolution: Never");
        //                    StaticInfoBox.AddItem(ElementTable.Elements[LightElement].Name + ": " + Write.Number(new Number(State.Chemistry[LightElement]), 1, 0, 1, 0, null));
        //                    StaticInfoBox.AddItem(ElementTable.Elements[LightElement + 1].Name + ": " + Write.Number(new Number(State.Chemistry[LightElement + 1]), 1, 0, 1, 0, null));
        //                    StaticInfoBox.AddItem("ID: " + WorldID);
        //                    //StaticInfoBox.Draw(spriteBatch, P.Position + DrawOffset + new Vector2(Radius * 1.1f, -Radius * 1.1f), Radius / 30f, 1f);
        //                    StaticInfoBox.Draw(spriteBatch, State.P.Position + DrawOffset + new Vector2(State.Radius * 2.5f, -State.Radius * 1.1f), (State.Radius / 100f) * multiplier, alpha);
        //                    StaticInfoBox.ClearList();
        //                }
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            spriteBatch.Draw(TexSmall, State.P.Position + DrawOffset, SM.StarsBack.Rectangles[IDSmall], ColorBody, State.Rotation, new Vector2(16, 16), Scale * 128, SpriteEffects.None, 0f);
        //        }
        //    }

        //    if (DebugOptions.DebugActive)
        //    {
        //        spriteBatch.Draw(StaticTests.MarkerCircle, State.P.Position + DrawOffset, null, Color.Yellow * 0.5f, 0f,
        //            new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(10f, 10f), SpriteEffects.None, 1f);

        //        for (int i = 0; i < Markers.Count; i++)
        //            spriteBatch.Draw(StaticTests.MarkerCircle, Markers[i].Position - new Vector2((StaticTests.Marker.Width / 2) * Markers[i].Size, (StaticTests.Marker.Height / 2) * Markers[i].Size), null, Markers[i].Color, 0f, Vector2.Zero, Markers[i].Size, SpriteEffects.None, 0f);

        //        Markers.Clear();
        //    }

            
        }
    }
}
