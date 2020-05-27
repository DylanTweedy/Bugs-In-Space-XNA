using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class System
    {
        public Vector2 Position;
        public int SystemRadius;

        public int StarCount;
        public int PlanetCount;
        public int CometCount;

        List<GalaxyObject> GalaxyObjects;
        List<byte> OrbitID;
        //List<SystemCoreObject> CoreObjects;
        //List<SystemOrbital> Orbitals;
        //List<SystemObject> Objects;
        Random rand;
        int min;
        int galaxyScale;
        int CoreObjectRadius;
        int CoreObjectCount;
        int OrbitalCount;
        bool InView;
        bool ObjectView;
        float rad;
        float spe;
        float rot;
        List<int> ElementDistribrution;
        int ID;
        byte Tier;

        TimeSpan CreationTime;
        float CreationAlpha;
        Texture2D Marker;
        bool AntiClockwise;

        public void Initialize(Random Rand, int id, int chunkSize, byte tier)
        {
            rand = Rand;
            InView = true;
            SystemRadius = rand.Next(25000, 600000);
            CoreObjectRadius = SystemRadius / 3;
            GalaxyObjects = new List<GalaxyObject>();
            OrbitID = new List<byte>();
            Marker = StaticTests.Marker;
            ElementDistribrution = ElementTable.BuildTable(CoreObjectRadius * (tier + 1));
            ID = id;
            galaxyScale = chunkSize;
            Position = new Vector2(rand.Next(0, galaxyScale * 2) - galaxyScale, rand.Next(0, galaxyScale * 2) - galaxyScale);
            CreationTime = WorldVariables.RealTime;
            Tier = (byte)(tier + (rand.Next(0, 5) - 2));
        }

        #region OLD


        public void LoadCore()
        {
            //rad = rand.Next(min, galaxyScale);
            //spe = ((float)rand.Next(1000, 10000) / ((float)rad / 2));
            //rot = (float)rand.Next(1, 62831) / 10000f;

            //Position = Vector2.Zero;

            //CoreObjects.Add(new SystemCoreObject());
            //CoreObjects[CoreObjects.Count - 1].Core = true;
            //CoreObjects[CoreObjects.Count - 1].CreateCoreObject(rand);

            //float biggestStar = 0f;
            //CoreObjectRadius = 0;

            //for (int i = 0; i < CoreObjects.Count; i++)
            //    if (biggestStar < CoreObjects[i].OriginalScale * 0.8f)
            //        biggestStar = (CoreObjects[i].OriginalScale * 0.8f);

            //for (int i = 0; i < CoreObjects.Count; i++)
            //    CoreObjects[i].SetupCoreObject(Position, rot, CoreObjectCount, spe * 250f, (6.2831f / (float)CoreObjectCount) * i, biggestStar, ID);

            //for (int u = 0; u < CoreObjects.Count; u++)
            //{
            //    int radius = CoreObjects[u].CalculateRadius();

            //    if (CoreObjectRadius < radius)
            //        CoreObjectRadius = radius;
            //}

            //if (CoreObjects.Count == 1)
            //    SystemRadius = 3000000;

        }

        private void AddCoreObjects()
        {
            //bool StarsReady = false;
            //CoreObjectCount = 1;

            //while (!StarsReady)
            //{
            //    if (rand.Next(0, 4) == 0)
            //    {
            //        CoreObjectCount += 1;
            //    }
            //    else
            //        StarsReady = true;
            //}

            //for (int i = 0; i < CoreObjectCount; i++)
            //{
            //    CoreObjects.Add(new SystemCoreObject());
            //    CoreObjects[CoreObjects.Count - 1].CreateCoreObject(rand);
            //}

        }

        private void SetupCoreObjects()
        {
            //float biggestStar = 0f;
            //CoreObjectRadius = 0;

            //rad = rand.Next(min, galaxyScale);
            //spe = ((float)rand.Next(1000, 50000) / ((float)rad / 2));
            //rot = (float)rand.Next(1, 62831) / 10000f;

            //for (int i = 0; i < CoreObjects.Count; i++)
            //    if (biggestStar < CoreObjects[i].OriginalScale * 0.8f)
            //        biggestStar = (CoreObjects[i].OriginalScale * 0.8f);

            //for (int i = 0; i < CoreObjects.Count; i++)
            //    CoreObjects[i].SetupCoreObject(Position, rot, CoreObjectCount, spe, (6.2831f / (float)CoreObjectCount) * i, biggestStar, ID);

            //for (int u = 0; u < CoreObjects.Count; u++)
            //{
            //    int radius = CoreObjects[u].CalculateRadius();

            //    if (CoreObjectRadius < radius)
            //        CoreObjectRadius = radius;
            //}
        }

        //private void SetupOrbitals()
        //{
        //for (int i = 0; i < Orbitals.Count; i++)
        //{
        //    rad = rand.Next((int)((CoreObjectRadius / 2) * 1.05f), (int)((SystemRadius / 2) * 0.95f));
        //    spe = UsefulMethods.FindBetween((int)rad, (SystemRadius / 2), (CoreObjectRadius / 2), 0.3f, 0.01f, true);
        //    rot = (float)rand.Next(1, 62831) / 10000f;

        //    Orbitals[i].SetupOrbital(Position, rot, CoreObjectCount, spe, rand.Next(0, 100), CoreObjectRadius / 2, rad);
        //}

        //for (int u = 0; u < Orbitals.Count; u++)
        //{
        //    int radius = CoreObjects[u].CalculateRadius();

        //    if (CoreObjectRadius < radius)
        //        CoreObjectRadius = radius;
        //}
        //}

        //private void AddOrbitals()
        //{
        //OrbitalCount = rand.Next(0, 30);

        //for (int i = 0; i < OrbitalCount; i++)
        //{
        //    Orbitals.Add(new SystemOrbital());
        //    Orbitals[Orbitals.Count - 1].CreateOrbital(rand);
        //    PlanetCount++;
        //}
        //}

        //private void SetupObjects()
        //{
        //for (int i = 0; i < Objects.Count; i++)
        //{
        //    rad = rand.Next((int)((CoreObjectRadius / 2) * 1.05f), (int)((SystemRadius / 2) * 0.95f));
        //    spe = UsefulMethods.FindBetween((int)rad, (SystemRadius / 2), (CoreObjectRadius / 2), 0.5f, 0.3f, true);
        //    rot = (float)rand.Next(1, 62831) / 10000f;

        //    Objects[i].SetupOrbital(Position, rot, CoreObjectCount, spe * 6, rand.Next(0, 100), CoreObjectRadius / 2, rad);
        //}
        //}

        //private void AddObjects()
        //{
        //OrbitalCount = (int)(UsefulMethods.FindBetween((int)CoreObjectRadius, 250000, 0, 50f, 2f, false) * rand.NextDouble());

        //for (int i = 0; i < OrbitalCount; i++)
        //{
        //    Objects.Add(new SystemObject());
        //    Objects[Objects.Count - 1].CreateOrbital(rand);
        //    CometCount++;
        //}
        //}

        //private void CheckOrbitalConflicts()
        //{
        //for (int i = Orbitals.Count - 1; i >= 0; i--)
        //{
        //    int counter = 0;
        //    bool checking = true;

        //    while (checking)
        //    {
        //        checking = false;

        //        for (int o = 0; o < Orbitals.Count; o++)
        //            if (o != i)
        //            {
        //                float dist = (float)Math.Abs(Orbitals[i].orbit.OrbitRadius - Orbitals[o].orbit.OrbitRadius);
        //                float size = (Orbitals[i].ActualSize / 2) + (Orbitals[o].ActualSize / 2);

        //                if (size > dist)
        //                {
        //                    rad = rand.Next((int)((CoreObjectRadius / 2) * 1.05f), (int)((SystemRadius / 2) * 0.95f));
        //                    spe = UsefulMethods.FindBetween((int)rad, (SystemRadius / 2), (CoreObjectRadius / 2), 0.3f, 0.01f, true);
        //                    rot = (float)rand.Next(1, 62831) / 10000f;

        //                    Orbitals[i].SetupOrbital(Position, rot, CoreObjectCount, spe, rand.Next(0, 100), CoreObjectRadius / 2, rad);
        //                    checking = true;
        //                }
        //            }

        //        counter++;

        //        if (counter > 7)
        //        {
        //            Orbitals.RemoveAt(i);
        //            checking = false;
        //            PlanetCount--;
        //        }
        //    }
        //}
        //}

        #endregion

        public void LoadContent()
        {
            if (rand.Next(0, 2) == 0)
                AntiClockwise = true;
            else
                AntiClockwise = false;

            AddStars();
            AddPlanets();
            AddObjects();
            //min = centerRadius + 2500;


            //int CoreObjectRadius = GalaxyObjects[GalaxyObjects.Count - 1].CalculateRadius();
            //AddCoreObjects();
            //SetupCoreObjects();

            //if (CoreObjects.Count == 1)
            //    SystemRadius = rand.Next((CoreObjectRadius * 3), (CoreObjectRadius * 6));
            //else
            //SystemRadius = rand.Next((CoreObjectRadius * 4), (CoreObjectRadius * 4));
            //AddOrbitals();
            //SetupOrbitals();
            //CheckOrbitalConflicts();

            //AddObjects();
            //SetupObjects();
        }

        private List<int> GetPercentage(float multiplier)
        {
            List<int> Elements = new List<int>();
            int Count = (int)((CoreObjectRadius * 2) * multiplier);
            bool Calculating = true;

            while (Calculating)
            {
                break;
            }

            return Elements;
        }

        private void AddStars()
        {
            bool AddingOrbitals = true;
            int OrbitalsAdd = 1;

            while (AddingOrbitals)
            {
                if (rand.Next(0, 2) == 0)
                    OrbitalsAdd++;
                else
                    AddingOrbitals = false;
            }

            float SizeMultiplier = 1f;
            if (OrbitalsAdd > 1)
                SizeMultiplier = 0.4f;

            float Speed1 = rand.Next(10, 100000) / 100000f;
            if (AntiClockwise)
                Speed1 = -Speed1;

            for (int i = 0; i < OrbitalsAdd; i++)
            {
                GalaxyObjects.Add(new GalaxyObject());
                OrbitID.Add(255);
                GalaxyObjects[GalaxyObjects.Count - 1].Setup(1, SystemRadius, CoreObjectRadius, AntiClockwise, SizeMultiplier, true, rand);

                if (OrbitalsAdd > 1)
                    GalaxyObjects[GalaxyObjects.Count - 1].SetupOrbit(OrbitalsAdd, i, Speed1, CoreObjectRadius / 3, 0, 0f, 0f, Position, rand);

                AddingOrbitals = true;
                int OrbitalsAdd2 = 0;

                while (AddingOrbitals)
                {
                    if (rand.Next(0, 3) == 0 && (CoreObjectRadius / 2) * SizeMultiplier >= 1024)
                        OrbitalsAdd2++;
                    else
                        AddingOrbitals = false;
                }

                float SizeMultiplier2 = SizeMultiplier * 0.4f;
                float Speed2 = rand.Next(10, 100000) / 100000f;

                if (AntiClockwise)
                    Speed2 = -Speed2;

                byte Current = (byte)(GalaxyObjects.Count - 1);

                for (int o = 0; o < OrbitalsAdd2; o++)
                {
                    OrbitID.Add(Current);
                    GalaxyObjects.Add(new GalaxyObject());
                    GalaxyObjects[GalaxyObjects.Count - 1].Setup(1, SystemRadius, CoreObjectRadius, AntiClockwise, SizeMultiplier2, true, rand);
                    GalaxyObjects[GalaxyObjects.Count - 1].SetupOrbit(OrbitalsAdd2, o, Speed2, CoreObjectRadius / 4, 0, 0f, 0f, Position, rand);
                }
            }
        }

        private void AddPlanets()
        {

        }

        private void AddObjects()
        {
            int a = rand.Next(0, 50);
            for (int i = 0; i < a; i++)
            {
                GalaxyObjects.Add(new GalaxyObject());
                OrbitID.Add(254);
                GalaxyObjects[GalaxyObjects.Count - 1].Setup(3, SystemRadius, CoreObjectRadius, AntiClockwise, 1f, true, rand);

                float Speed = rand.Next(10, 100000) / 100000f;

                GalaxyObjects[GalaxyObjects.Count - 1].SetupOrbit(1, i, Speed, CoreObjectRadius / 3, 0, 0f, 0f, Position, rand);
            }
        }

        public void ResetPosition()
        {
            Position = new Vector2(rand.Next(0, galaxyScale * 2) - galaxyScale, rand.Next(0, galaxyScale * 2) - galaxyScale);
            SystemRadius = (int)(SystemRadius * 0.9f);
            CoreObjectRadius = (int)(CoreObjectRadius * 0.9f);
        }

        public void Update(GameTime gameTime)
        {
            //if (ObjectView)
            //    for (int i = 0; i < Objects.Count; i++)
            //        Objects[i].Update(gameTime, Position);

            if (InView)
            {
                //for (int i = 0; i < Orbitals.Count; i++)                
                //    Orbitals[i].Update(gameTime, Position);

                for (int i = 0; i < GalaxyObjects.Count; i++)
                {
                    GalaxyObjects[i].Update(gameTime);

                    if (OrbitID[i] == 255)
                        GalaxyObjects[i].UpdateOrbit(Position);
                    else if (OrbitID[i] == 254)
                    {
                        List<Vector2> Positions = new List<Vector2>();
                        List<float> Masses = new List<float>();

                        for (int o = 0; o < GalaxyObjects.Count; o++)
                        {
                            if (o != i)
                            {
                                Positions.Add(GalaxyObjects[o].Position);
                                Masses.Add(GalaxyObjects[o].Mass);
                            }
                        }

                        GalaxyObjects[i].UpdateGravityOrbit(Positions, Masses);
                    }
                    else
                        GalaxyObjects[i].UpdateOrbit(GalaxyObjects[OrbitID[i]].Position);

                    //if (CoreObjects[i].RecalculateStar)
                    //{
                    //    CoreObjectRadius = 0;

                    //    for (int u = 0; u < CoreObjects.Count; u++)
                    //    {
                    //        int radius = CoreObjects[u].CalculateRadius();

                    //        if (CoreObjectRadius < radius)
                    //            CoreObjectRadius = radius;                            
                    //    }

                    //    CoreObjects[i].RecalculateStar = false;
                    //}
                }

                for (int i = 0; i < GalaxyObjects.Count; i++)
                    for (int o = 0; o < GalaxyObjects.Count; o++)
                        if (i != o)
                        {
                            float scale1 = GalaxyObjects[i].Scale;
                            float scale2 = GalaxyObjects[o].Scale;
                            float d = Vector2.Distance(GalaxyObjects[i].Position, GalaxyObjects[o].Position);
                            float s = (512 * scale1) + (512 * scale2);

                            if (d < s)
                            {
                                byte n = 0;

                                if (scale1 <= scale2)
                                    n = 1;

                                if (n == 1)
                                {
                                    GalaxyObjects[o].Mass += GalaxyObjects[i].Mass * 0.001f;
                                    GalaxyObjects[o].OriginalScale = (GalaxyObjects[i].OriginalScale * 0.005f);

                                    GalaxyObjects[i].Mass -= GalaxyObjects[i].Mass * 0.001f;
                                    GalaxyObjects[i].OriginalScale = -(GalaxyObjects[i].OriginalScale * 0.01f);

                                    if (GalaxyObjects[i].Mass <= 1 || GalaxyObjects[i].OriginalScale <= 1)
                                        GalaxyObjects.RemoveAt(i);

                                    break;
                                }
                            }
                        }
            }

            InView = false;
            ObjectView = false;
        }

        public void DrawSimple(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalTint)
        {
            float scale = (SystemRadius / 64);

            if (scale * CameraManager.CamerasRead[cameraNumber].Zoom > 0.025)
            {
                float a = rand.Next(9000, 10000) / 10000f;
                spriteBatch.Draw(StaticTests.StarTest, Position + Offset, null, Color.White * FinalTint * a, 0f, new Vector2(32, 32), scale * a, SpriteEffects.None, 0f);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalTint)
        {
            int time;

            if (WorldVariables.RealTime - CreationTime < TimeSpan.FromSeconds(30))
            {
                time = (int)((WorldVariables.RealTime.TotalSeconds - CreationTime.TotalSeconds) * 100);
                CreationAlpha = UsefulMethods.FindBetween(time, 100, 0, 1f, 0f, false);
            }
            else
                CreationAlpha = 1f;

            FinalTint = FinalTint * CreationAlpha;


            //if (CameraManager.CamerasRead[cameraNumber].IsInView(Position + Offset, SystemRadius * 2, SystemRadius * 2, 1f))
            //{
            //    float Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
            //    float OnScreenSize = SystemRadius * Zoom;

            //    if (OnScreenSize > 60f)
            //        for (int i = 0; i < Objects.Count; i++)
            //            Objects[i].Draw(spriteBatch, cameraNumber, Offset, FinalTint);

            //    ObjectView = true;
            //}

            if (CameraManager.CamerasRead[cameraNumber].IsInView(Position + Offset, SystemRadius, SystemRadius, 1f))
            {
                InView = true;

                float Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
                float OnScreenSize = CoreObjectRadius * Zoom;
                bool DrawMarker = true;

                if (Zoom < 0.0005f)
                    FinalTint *= UsefulMethods.FindBetween((int)(Zoom * 1000000), 500, 250, 1f, 0f, false);

                if (OnScreenSize < 4f)
                {
                    InView = false;
                }
                else if (OnScreenSize < 20f)
                {
                    float a1 = UsefulMethods.FindBetween((int)(OnScreenSize * 100), 2000, 400, 1f, 0f, false);

                    if (DrawMarker & OnScreenSize > 0.1f)
                    {
                        float markerScale = (SystemRadius) / Marker.Width;
                        spriteBatch.Draw(Marker, Position + Offset, null, Color.Green * 0.5f * a1 * FinalTint, 0f, new Vector2(Marker.Width / 2, Marker.Height / 2), markerScale, SpriteEffects.None, 0f);

                        markerScale = CoreObjectRadius / Marker.Width;
                        spriteBatch.Draw(Marker, Position + Offset, null, Color.Red * 0.5f * a1 * FinalTint, 0f, new Vector2(Marker.Width / 2, Marker.Height / 2), markerScale, SpriteEffects.None, 0f);
                    }
                }
                else
                {
                    if (DrawMarker)
                    {
                        float markerScale = (SystemRadius) / Marker.Width;
                        spriteBatch.Draw(Marker, Position + Offset, null, Color.Green * 0.5f * FinalTint, 0f, new Vector2(Marker.Width / 2, Marker.Height / 2), markerScale, SpriteEffects.None, 0f);

                        markerScale = CoreObjectRadius / Marker.Width;
                        spriteBatch.Draw(Marker, Position + Offset, null, Color.Red * 0.5f * FinalTint, 0f, new Vector2(Marker.Width / 2, Marker.Height / 2), markerScale, SpriteEffects.None, 0f);

                        InfoBox.ClearList();
                        InfoBox.AddItem("" + Tier);
                        InfoBox.Draw(spriteBatch, Position + Offset + new Vector2(1024 * markerScale, -1024 * markerScale), markerScale * 10, 1f * FinalTint);
                    }

                    //if (OnScreenSize > 30f)                    
                    //    for (int i = 0; i < Orbitals.Count; i++)
                    //        Orbitals[i].Draw(spriteBatch, cameraNumber, Offset, FinalTint);
                }

                for (int i = 0; i < GalaxyObjects.Count; i++)
                {
                    GalaxyObjects[i].Draw(spriteBatch, cameraNumber, Offset, FinalTint);
                }
            }
        }
    }
}
