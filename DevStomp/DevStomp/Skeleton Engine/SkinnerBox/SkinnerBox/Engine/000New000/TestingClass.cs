using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SkeletonEngine
{
    static class TestingClass
    {
        #region QuadTest

        //static List<PhysicsQuad> Quads = new List<PhysicsQuad>();

        //static public void Initialize()
        //{
        //    point1 = new Node(new Vector2(0, 50), 1f);
        //    point2 = new Node(new Vector2(0, -50), 1f);
        //    spring = new Spring(point1, point2, 1f);
        //}

        //static Spring spring;
        //static Node point1;
        //static Node point2;

        //static float KEMax = float.MinValue;
        //static float KEMin = float.MaxValue;
        //static float PEMax = float.MinValue;
        //static float PEMin = float.MaxValue;
        //static float MEMax = float.MinValue;
        //static float MEMin = float.MaxValue;

        //static public void Update()
        //{
        //    if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.Enter))
        //    {
        //        point1.Position.X = 0f;
        //        point2.Position.X = 0f;
        //        point1.PreviousPosition.X = 0f;
        //        point2.PreviousPosition.X = 0f;

        //        point1.Position.Y = 75f;
        //        point2.Position.Y = -75f;

        //        point1.PreviousPosition.Y = 75f;
        //        point2.PreviousPosition.Y = -75f;
                                
        //        KEMax = float.MinValue;
        //        KEMin = float.MaxValue;
        //        PEMax = float.MinValue;
        //        PEMin = float.MaxValue;
        //        MEMax = float.MinValue;
        //        MEMin = float.MaxValue;
        //    }

        //    point1.Update();
        //    point2.Update();
        //    spring.Update();

        //    float KE = point1.KineticEnergy() + point2.KineticEnergy();
        //    float PE = spring.PotentialEnergy();
        //    float ME = KE + PE;
                        
        //    if (KEMax < KE)
        //        KEMax = KE;
        //    if (KEMin > KE)
        //        KEMin = KE;
        //    if (PEMax < PE)
        //        PEMax = PE;
        //    if (PEMin > PE)
        //        PEMin = PE;
        //    if (MEMax < ME)
        //        MEMax = ME;
        //    if (MEMin > ME)
        //        MEMin = ME;

        //    DebugOptions.DebugTextLines.Add(new TextLine("Kinectic Energy: " + KE));
        //    DebugOptions.DebugTextLines.Add(new TextLine("Potential Energy: " + PE));
        //    DebugOptions.DebugTextLines.Add(new TextLine("Mechanical Energy: " + ME));
            
        //    DebugOptions.DebugTextLines.Add(new TextLine("KE Max: " + KEMax));
        //    DebugOptions.DebugTextLines.Add(new TextLine("KE Min: " + KEMin));
        //    DebugOptions.DebugTextLines.Add(new TextLine("PE Max: " + PEMax));
        //    DebugOptions.DebugTextLines.Add(new TextLine("PE Min: " + PEMin));
        //    DebugOptions.DebugTextLines.Add(new TextLine("ME Max: " + MEMax));
        //    DebugOptions.DebugTextLines.Add(new TextLine("ME Min: " + MEMin));



        //    #region quads

        //    if (InputManager.KBPressed(true, Microsoft.Xna.Framework.Input.Keys.Enter))
        //        Quads.Clear();

        //    if (InputManager.MBPressed(true, MouseButton.Left))
        //    {
        //        Quads.Add(new PhysicsQuad(InputManager.GetMousePosition(CameraManager.MainCameras[0]),
        //           32f, Color.White, "InfoBox", "ColorPallette"));
        //    }

        //    for (int i = 0; i < Quads.Count; i++)
        //    {

        //        for (int o = 0; o < Quads[i].Points.Length; o++)
        //        {
        //        }



        //        for (int o = 0; o < Quads[i].Points.Length; o++)
        //        {
        //        }


        //        for (int o = 0; o < Quads[i].Points.Length; o++)
        //        {
        //            if (o == 0)
        //                if (Quads[i].Points[o].Velocity == Vector2.Zero)
        //                    Quads[i].Points[o].Force += Vector2.Transform(new Vector2(0f, 50f), Matrix.CreateRotationZ(GlobalVariables.RandomNumber.Next(0, 3140 * 2) / 1000f));

        //            //Quads[i].Points[o].Force += Vector2.Transform(new Vector2(0f, 0.5f), Matrix.CreateRotationZ(-CameraManager.MainCameras[0].Rotation));

        //            if (Quads[i].Points[o].Position.Y > 500f)
        //            {
        //                Quads[i].Points[o].Position.Y = 500f;

        //                Quads[i].Points[o].PreviousPosition.Y = Quads[i].Points[o].Position.Y +
        //                     (Quads[i].Points[o].Position.Y - Quads[i].Points[o].PreviousPosition.Y);
        //                //Quads[i].Points[o].Velocity *= 0.75f;
        //            }
        //            if (Quads[i].Points[o].Position.Y < -500f)
        //            {
        //                Quads[i].Points[o].Position.Y = -500f;


        //                Quads[i].Points[o].PreviousPosition.Y = Quads[i].Points[o].Position.Y +
        //                     (Quads[i].Points[o].Position.Y - Quads[i].Points[o].PreviousPosition.Y);
        //                //Quads[i].Points[o].Velocity *= 0.75f;
        //            }
        //            if (Quads[i].Points[o].Position.X > 500f)
        //            {
        //                Quads[i].Points[o].Position.X = 500f;


        //                Quads[i].Points[o].PreviousPosition.X = Quads[i].Points[o].Position.X +
        //                     (Quads[i].Points[o].Position.X - Quads[i].Points[o].PreviousPosition.X);
        //                //Quads[i].Points[o].Velocity *= 0.75f;
        //            }
        //            if (Quads[i].Points[o].Position.X < -500f)
        //            {
        //                Quads[i].Points[o].Position.X = -500f;

        //                Quads[i].Points[o].PreviousPosition.X = Quads[i].Points[o].Position.X +
        //                     (Quads[i].Points[o].Position.X - Quads[i].Points[o].PreviousPosition.X);
        //                //Quads[i].Points[o].Velocity *= 0.75f;
        //            }
        //        }




        //        Quads[i].Update();
        //    }


        //    for (int q1 = 0; q1 < Quads.Count; q1++)
        //        for (int q2 = q1 + 1; q2 < Quads.Count; q2++)
        //        {
        //            Vector2 Q1Center = Quads[q1].MainQuad.GetCenter();
        //            Vector2 Q2Center = Quads[q2].MainQuad.GetCenter();

        //            float Q1radius = Quads[q1].MainQuad.GetRadius(Q1Center);
        //            float Q2radius = Quads[q2].MainQuad.GetRadius(Q2Center);

        //            if (Vector2.Distance(Q1Center, Q2Center) < (Q1radius + Q2radius) * 0.75f)
        //            {
        //                //SkeletonQuad S1 = new SkeletonQuad(intersect, 16, Color.Red, "Core", "Marker");
        //                //QuadManager.AddQuad(S1);

        //                Vector2 collisionNormal = Q1Center - Q2Center;
        //                if (collisionNormal != Vector2.Zero)
        //                    collisionNormal *= 1f / collisionNormal.Length();

        //                float Intersection = Vector2.Distance(Q1Center, Q2Center) - ((Q1radius + Q2radius) * 0.75f);



        //                //Vector2 intersect = Q1Center - Q2Center;

        //                //for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                //{
        //                //    Vector2 normal = intersect - Quads[q1].Points[p].Position;
        //                //    normal.Normalize();

        //                //    Quads[q1].Points[p].Position -= collisionNormal * Intersection;
        //                //    Quads[q1].Points[p].Velocity = (normal * Quads[q1].Points[p].Velocity.Length());
        //                //}

        //                //for (int p = 0; p < Quads[q2].Points.Length; p++)
        //                //{
        //                //    Vector2 normal = intersect - Quads[q2].Points[p].Position;
        //                //    normal.Normalize();

        //                //    Quads[q1].Points[p].Position -= collisionNormal * Intersection;
        //                //    Quads[q2].Points[p].Velocity = (normal * Quads[q2].Points[p].Velocity.Length());
        //                //}

        //                //Try whiteboard idea.
        //                #region OLD3 (Best so far)

        //                float Q1Min = float.MaxValue;
        //                int Q1point = 0;
        //                float Q2Min = float.MaxValue;
        //                int Q2point = 0;

        //                for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                {
        //                    float dis = Vector2.Distance(Q1Center - Q2Center, Quads[q1].Points[p].Position);

        //                    if (dis < Q1Min)
        //                    {
        //                        Q1Min = dis;
        //                        Q1point = p;
        //                    }
        //                }

        //                for (int p = 0; p < Quads[q2].Points.Length; p++)
        //                {
        //                    float dis = Vector2.Distance(Q1Center - Q2Center, Quads[q1].Points[p].Position);

        //                    if (dis < Q2Min)
        //                    {
        //                        Q2Min = dis;
        //                        Q2point = p;
        //                    }
        //                }

        //                for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                {
        //                    Quads[q1].Points[p].Position -= collisionNormal * Intersection;
        //                    //if (p == Q1point)
        //                    //    Quads[q1].Points[p].PreviousPosition = Quads[q1].Points[p].Position - (collisionNormal * (Quads[q1].Points[p].Velocity.Length() / GlobalVariables.WorldTime));

        //                }

        //                for (int p = 0; p < Quads[q2].Points.Length; p++)
        //                {
        //                    Quads[q2].Points[p].Position += collisionNormal * Intersection;
        //                    //if (p == Q2point)
        //                    //    Quads[q2].Points[p].PreviousPosition = Quads[q2].Points[p].Position - (-collisionNormal * (Quads[q2].Points[p].Velocity.Length() / GlobalVariables.WorldTime));
        //                }

        //                #endregion

        //                #region OLD2

        //                //float Q1Min = float.MaxValue;
        //                //float Q1Max = float.MinValue;
        //                //float Q2Min = float.MaxValue;
        //                //float Q2Max = float.MinValue;

        //                //for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                //{
        //                //    float dis = Vector2.Distance(Q1Center - Q2Center, Quads[q1].Points[p].Position);

        //                //    if (dis > Q1Max)
        //                //        Q1Max = dis;
        //                //    if (dis < Q1Min)
        //                //        Q1Min = dis;
        //                //}

        //                //for (int p = 0; p < Quads[q2].Points.Length; p++)
        //                //{
        //                //    float dis = Vector2.Distance(Q1Center - Q2Center, Quads[q1].Points[p].Position);

        //                //    if (dis > Q2Max)
        //                //        Q2Max = dis;
        //                //    if (dis < Q2Min)
        //                //        Q2Min = dis;
        //                //}

        //                //float StartTotal = 0f;
        //                //float EndTotal = 0f;

        //                //for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                //    StartTotal += Quads[q1].Points[p].Velocity.Length();

        //                //for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                //{
        //                //    Quads[q1].Points[p].Position -= collisionNormal * Intersection;
        //                //    Quads[q1].Points[p].Velocity = (collisionNormal * Quads[q1].Points[p].Velocity.Length()) * 
        //                //        UsefulMethods.FindBetween(Vector2.Distance(Q1Center - Q2Center, Quads[q1].Points[p].Position), Q1Max, Q1Min, 2f, 0f, false);
        //                //}

        //                //for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                //    EndTotal += Quads[q1].Points[p].Velocity.Length();

        //                //for (int p = 0; p < Quads[q2].Points.Length; p++)
        //                //{
        //                //    Quads[q2].Points[p].Position += collisionNormal * Intersection;
        //                //    Quads[q2].Points[p].Velocity = (-collisionNormal * Quads[q2].Points[p].Velocity.Length()) *
        //                //        UsefulMethods.FindBetween(Vector2.Distance(Q1Center - Q2Center, Quads[q2].Points[p].Position), Q2Max, Q2Min, 1f, 0f, false);
        //                //}

        //                #endregion

        //                #region OLD

        //                //for (int p = 0; p < Quads[q1].Points.Length; p++)
        //                //{
        //                //    Quads[q1].Points[p].Position -= collisionNormal * Intersection;
        //                //    Quads[q1].Points[p].Velocity = (collisionNormal * Quads[q1].Points[p].Velocity.Length()) * 0.95f;
        //                //}

        //                //for (int p = 0; p < Quads[q2].Points.Length; p++)
        //                //{
        //                //    Quads[q2].Points[p].Position += collisionNormal * Intersection;
        //                //    Quads[q2].Points[p].Velocity = (-collisionNormal * Quads[q2].Points[p].Velocity.Length()) * 0.95f;
        //                //}

        //                #endregion
        //            }
        //        }

        //    #endregion
        //}

        //static public void Draw(Camera camera)
        //{
        //    DrawSprites.Begin(camera);

        //    SkeletonTexture tex = new SkeletonTexture("Core", "Marker");
        //    tex.Draw(Vector2.Zero, Color.White, 0f, new Vector2(10f, point1.Position.Y + -point2.Position.Y), SpriteEffects.None);

        //    tex.Draw(point1.Position, Color.Red, 0f, 8f, SpriteEffects.None);
        //    tex.Draw(point2.Position, Color.Red, 0f, 8f, SpriteEffects.None);



        //    SkeletonTexture tex2 = new SkeletonTexture("Core", "Arrow");
        //    Vector2 force1 = point1.PreviousForce;
        //    force1.Normalize();
        //    Vector2 velocity1 = point1.Velocity;
        //    velocity1.Normalize();
        //    float forceRadians1 = (float)Math.Atan2(force1.X, force1.Y);
        //    float velocityRadians1 = (float)Math.Atan2(velocity1.X, velocity1.Y);

        //    float ForceScale1 = UsefulMethods.FindBetween(point1.PreviousForce.Length(), 5f, 0f, 40f, 0f, false);
        //    float VelocityScale1 = UsefulMethods.FindBetween(point1.Velocity.Length(), 200f, 0f, 40f, 0f, false);

        //    tex2.Draw(UsefulMethods.RotatePoint(point1.Position - new Vector2(0, -ForceScale1 / 2f), point1.Position, forceRadians1), Color.Blue, forceRadians1, new Vector2(20, ForceScale1), SpriteEffects.FlipVertically);
        //    tex2.Draw(UsefulMethods.RotatePoint(point1.Position - new Vector2(0, -VelocityScale1 / 2f), point1.Position, velocityRadians1), Color.Green, velocityRadians1, new Vector2(20, VelocityScale1), SpriteEffects.FlipVertically);

        //    Vector2 force2 = point2.PreviousForce;
        //    force2.Normalize();
        //    Vector2 velocity2 = point2.Velocity;
        //    velocity2.Normalize();
        //    float forceRadians2 = (float)Math.Atan2(force2.X, force2.Y);
        //    float velocityRadians2 = (float)Math.Atan2(velocity2.X, velocity2.Y);

        //    float ForceScale2 = UsefulMethods.FindBetween(point2.PreviousForce.Length(), 5f, 0f, 40f, 0f, false);
        //    float VelocityScale2 = UsefulMethods.FindBetween(point2.Velocity.Length(), 200f, 0f, 40f, 0f, false);

        //    tex2.Draw(UsefulMethods.RotatePoint(point2.Position - new Vector2(0, -ForceScale2 / 2f), point2.Position, forceRadians2), Color.Blue, forceRadians2, new Vector2(20, ForceScale2), SpriteEffects.FlipVertically);
        //    tex2.Draw(UsefulMethods.RotatePoint(point2.Position - new Vector2(0, -VelocityScale2 / 2f), point2.Position, velocityRadians2), Color.Green, velocityRadians2, new Vector2(20, VelocityScale2), SpriteEffects.FlipVertically);


        //    DrawSprites.End();
        //}

        #endregion

        static public Planet terrain;
        static public RenderTarget2D testTexture;

        static public float TestPublicRadius = 250f;

        static bool FollowCamera = false;

        static public void Initialize()
        {
            terrain = new Planet();
        }

        static Terrain testChunk;
        static Color[] testColor;


        static public void Update()
        {
            if (InputManager.KBPressed(true, Keys.L))
            {
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
                testColor = new Color[512 * 512];
                testColor = null;
            }



            TestPublicRadius = 100f;

            terrain.Update();

            bool CenterCamera = false;

            if (InputManager.KBPressed(true, Keys.D1))
                CenterCamera = true;
            if (InputManager.KBPressed(true, Keys.D2))
                FollowCamera = !FollowCamera;

            if (CenterCamera)
            {
                CameraManager.MainCameras[0].PositionDestination = terrain.Position;
                CameraManager.MainCameras[0].Position = terrain.Position;
            }

            if (FollowCamera)
            {
                //CameraManager.MainCameras[0].RotationDestination = -terrain.Rotation;
                //CameraManager.MainCameras[0].Rotation = -terrain.Rotation;

                for (int i = 0; i < GlobalVariables.PhysicsSteps; i++)
                {
                    //CameraManager.MainCameras[0].PositionDestination += terrain.Velocity * GlobalVariables.PhysicsTime;
                    //CameraManager.MainCameras[0].Position += terrain.Velocity * GlobalVariables.PhysicsTime;

                    CameraManager.MainCameras[0].PositionDestination = terrain.RotateWithPlanet(CameraManager.MainCameras[0].Position);
                    CameraManager.MainCameras[0].Position = CameraManager.MainCameras[0].PositionDestination;


                }


                SpacePosition rotation = CameraManager.MainCameras[0].Position - terrain.Position;
                CameraManager.MainCameras[0].RotationDestination = -UsefulMethods.VectorToAngle(rotation.RoughPosition);
                CameraManager.MainCameras[0].Rotation = -UsefulMethods.VectorToAngle(rotation.RoughPosition);
            }

            DebugOptions.DebugTextLines.AddRange(terrain.Information());

        }
        static public void PreDraw()
        {
            terrain.Chunks.ManageDiskData();
            terrain.Chunks.ManageDiskData();
            terrain.Chunks.ManageDiskData();
            terrain.Chunks.ManageDiskData();
        }

        static public void Draw(Camera camera)
        {

            //if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.LeftControl))
                if (DebugOptions.DebugActive)
                    DrawSpaceChunks(camera);

            DrawSprites.Begin(camera);

            DebugOptions.Geometry.AddData(new SpacePosition(terrain.Position), 0f, 5000, new Vector2(GlobalVariables.ChunkSize * TestPublicRadius * 2f), Color.Turquoise);

            DebugOptions.Geometry.Draw(CameraManager.MainCameras[0], FillMode.Solid);

            DrawSprites.End();

            DrawSprites.Begin(camera);
            terrain.Draw(camera);

            DrawSprites.End();


            DrawSprites.Begin(camera);
            DebugOptions.Geometry.Draw(CameraManager.MainCameras[0], FillMode.WireFrame);
            DrawSprites.End();

            


            List<SpacePosition> test = new List<SpacePosition>();
            test.Add(new SpacePosition(0, 0));
            test.Add(new SpacePosition(0.1f, 0));
            //test.Add(new SpacePosition(435, 643));
            //test.Add(new SpacePosition(264, 653));
            //test.Add(new SpacePosition(63, 356));
            //test.Add(new SpacePosition(67, 640));



            DebugOptions.Lines.AddData(test, Color.Red);

            DrawSprites.Begin(camera);
            DebugOptions.Lines.Draw(camera);
            DrawSprites.End();
        }

        static public void DrawSpaceChunks(Camera camera)
        {
            Vector2 res = new Vector2(camera.FinalRender.Width, camera.FinalRender.Height);
            float zoom = camera.Zoom;

            float chunksX = 0f;
            float chunksY = 0f;

            if (zoom != 0f)
            {
                chunksX = (res.X / GlobalVariables.CameraChunkSize) / zoom;
                chunksY = (res.Y / GlobalVariables.CameraChunkSize) / zoom;
                
                float zoomLevel = 1f;

                while (chunksX > 10f)
                {
                    zoomLevel *= 5f;

                    chunksX /= 5f;
                    chunksY /= 5f;
                }



                Vector2 centerChunk = new Vector2((float)camera.Position.ChunkX, (float)camera.Position.ChunkY);

                TextBox test = new TextBox();



                DrawSprites.Begin(camera);

                for (int x = 0; x < chunksX + 2; x++)
                    for (int y = 0; y < chunksY + 2; y++)
                    {
                        SpacePosition pos = new SpacePosition();
                        string stringX = "";



                        pos.Position += new Vector2((GlobalVariables.CameraChunkSize / 2f) * (zoomLevel - 1f));

                        BigInteger posX = camera.Position.ChunkX;
                        if (posX < 0)
                            posX -= (BigInteger)zoomLevel;

                        pos.ChunkX += (posX / (BigInteger)zoomLevel) * (BigInteger)zoomLevel;
                        pos.ChunkX += (BigInteger)(Math.Round(x - ((chunksX + 1) / 2f)) * zoomLevel);
                       
                        BigInteger posY = camera.Position.ChunkY;
                        if (posY < 0)
                            posY -= (BigInteger)zoomLevel;

                        pos.ChunkY += (posY / (BigInteger)zoomLevel) * (BigInteger)zoomLevel;
                        pos.ChunkY += (BigInteger)(Math.Round(y - ((chunksY + 1) / 2f)) * zoomLevel);
                        

                        DebugOptions.Geometry.AddData(pos,
                            MathHelper.Pi, 4, new Vector2(GlobalVariables.CameraChunkSize * zoomLevel), Color.White);



                        test.Position = pos;
                        test.TextBoxSize = new Vector2(GlobalVariables.CameraChunkSize * zoomLevel);
                        test.Border = null;
                        test.Rotation = 0f;
                        test.Scale = 15000f * zoomLevel;
                        test.BackgroundColor = Color.Transparent;

                        test.Update(new List<TextLine>() { new TextLine( 
                        StringManager.FontSizeString(
                        StringManager.ColorString("X: " + pos.ChunkX + " Y: " + pos.ChunkY, Color.White), 10))
                        { Alignment = 0f }               
            
                        });
                        test.TextBoxSize = new Vector2(GlobalVariables.CameraChunkSize * zoomLevel * 0.975f);

                        test.Draw(camera);

                    }

                DrawSprites.End();


                DrawSprites.Begin(camera);
                DebugOptions.Geometry.Draw(camera, FillMode.WireFrame);
                DrawSprites.End();



            }
        }
    }
}
