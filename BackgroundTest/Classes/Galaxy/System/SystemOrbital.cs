using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class SystemOrbital
    {
        public Vector2 Position;
        public float Scale;
        public float OriginalScale;
        public float Rotation;
        public byte ID;
        public float LargestScale;
        public float Mass;
        public float ActualSize;

        Color Tint1;
        Color Tint2;
        float AlphaTint;
        float FinalAlphaTint;

        Texture2D BodyTexture;
        Texture2D CloudTexture;
        Texture2D GlowTexture;
        Texture2D OverlayTexture;
        Texture2D CloudTexture2;
        Texture2D GlowTexture2;
        Texture2D ShadowTexture;
        Texture2D PlanetaryRingTexture;
        bool PlanetaryRing;
        float RingRotation;

        bool Active;
        Vector2 Origin;
        string OrbitalClass;
        float OriginalRotation;
        Random rand;
        public float rotation;
        public Orbit orbit;
        TimeSpan Age;
        TimeSpan LastSeen;
        int TwinkleSpeed;
        float OverlayRotation;
        
        //Make Rotations both clockwise and anti-clockwise
        //Make Orbits both clockwise and anti-clockwise
        //Add special star actions
        //Make stars engulf others when dying
        //Give long dead stars an accurate age. Using current age, lifespan and current gametime.
        //Make dying stars size based on time, rather than a per frame change, because these stars won't change whilst off screen. 

        private void GetData()
        {
        //    GalaxyObjectData.SelectNewObject(2);

        //    OriginalScale = rand.Next((int)(GalaxyObjectData.SelectedObject.ScaleMin * 10000f), (int)(GalaxyObjectData.SelectedObject.ScaleMax * 10000f)) / 10000f;
        //    Scale = OriginalScale;

        //    if (Scale > 4 && rand.Next(0, 7) == 0)
        //        PlanetaryRing = true;
        //    else
        //        PlanetaryRing = false;

        //    int lc = GalaxyObjectData.SelectedObject.Layers.Length;

        //    int l = rand.Next(0, lc);
        //    BodyTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    CloudTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    GlowTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    OverlayTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    CloudTexture2 = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    GlowTexture2 = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    ShadowTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
        //    l = rand.Next(0, lc);
        //    PlanetaryRingTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];

        //    ID = GalaxyObjectData.SelectedObject.ID;
        //    OrbitalClass = GalaxyObjectData.SelectedObject.Name;
        //    OverlayRotation = (float)(MathHelper.TwoPi * rand.NextDouble());
        //    RingRotation = (float)(MathHelper.TwoPi * rand.NextDouble());
        //    Mass = GalaxyObjectData.SelectedObject.Mass * UsefulMethods.FindBetween((int)(OriginalScale * 1000f), (int)(GalaxyObjectData.SelectedObject.ScaleMin * 3000f), (int)(GalaxyObjectData.SelectedObject.ScaleMax * 3000f), 1f, 0f, false);

        //    Tint1 = UsefulMethods.RandomColour();
        //    Tint2 = UsefulMethods.RandomColour();
        }

        public void CreateOrbital(Random Rand)
        {
            Active = true;
            rand = Rand;
            LastSeen = WorldVariables.GameTime;

            bool FindOrbital = true;
            while (FindOrbital)
            {
                GetData();

                if (OriginalScale < LargestScale * 2f || LargestScale == 0)
                    FindOrbital = false;
            }
            
            //smallStar.Scale = (Scale * (star.Texture.Width / smallStar.Texture.Width)) * 2;
            //star.Tint = new Color(rand.Next(200, 255), rand.Next(200, 255), rand.Next(200, 255));

            Age = TimeSpan.Zero;
            LastSeen = WorldVariables.GameTime;

            rotation = (float)rand.Next(10000, 15000) / 100000000f;
            OriginalRotation = rotation;
            AlphaTint = 0f;
            Rotation = 0f;
        }

        public void SetupOrbital(Vector2 Pos, float Rot, int StarCount, float spe, float orbitRot, int StarRadius, float rad)
        {
            bool SizeCheck = true;
            while (SizeCheck)
            {
                ActualSize = BodyTexture.Width * OriginalScale;

                if (ActualSize > StarRadius * 1.75f)
                {
                    OriginalScale *= 0.75f;
                    Mass *= 0.75f;
                    Scale = OriginalScale;
                }
                else SizeCheck = false;
            }

            ActualSize = BodyTexture.Width * OriginalScale;

            TwinkleSpeed = rand.Next(1, 10);

            Origin = Vector2.Zero;


            //orbit = new Orbit(rad, spe, Rot, Rot);
            //orbit.InitializeCircle();

            //orbit.UpdatePosition(Pos);
            orbit.OriginalPosition = orbit.Position;
            orbit.OriginalRadian = orbit.OrbitRadian;
            orbit.OrbitRadius = rad;

            Position = orbit.Position;
        }

        public int CalculateRadius()
        {
            return (int)((orbit.OrbitRadius * 2) + (BodyTexture.Width * OriginalScale));
        }

        private void DestroyOrbital()
        {
            //Scale = 0f;
            //Rotation = 0f;
            //DyingCounter = TimeSpan.Zero;

            //if (!Core)
            //    CreateOrbital(rand);
        }

        public void Update(GameTime gameTime, Vector2 OrbitCenter)
        {
            Age += WorldVariables.GameTime - LastSeen;

            if (Rotation != Rotation)
                DestroyOrbital();

            //orbit.UpdatePosition(OrbitCenter);
            Position = orbit.Position;

            Rotation += rotation;
            FinalAlphaTint = AlphaTint;

            LastSeen = WorldVariables.GameTime;
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalTint)
        {
            if (Active)
            {
                float Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
                float ActualSize = BodyTexture.Width * Scale;
                float OnScreenSize = ActualSize * Zoom;

                if (OnScreenSize < 4)
                {
                }
                else if (OnScreenSize <= 8)
                {
                        float alpha1 = UsefulMethods.FindBetween((int)(OnScreenSize * 10), 80, 50, 1f, 0f, false);

                        Origin.X = BodyTexture.Width / 2;
                        Origin.Y = BodyTexture.Height / 2;

                        float ShadowRotation = orbit.OrbitRadian - ((float)Math.PI / 2f);

                        spriteBatch.Draw(BodyTexture, Position + Offset, null, Tint1 * alpha1, Rotation * 5f, Origin, Scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(OverlayTexture, Position + Offset, null, Tint2 * alpha1, (Rotation + OverlayRotation) * 5f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint1 * 0.4f * alpha1, Rotation * 2f, Origin, Scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(GlowTexture2, Position + Offset, null, Tint2 * 0.4f * alpha1, -Rotation * 3f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.Draw(CloudTexture, Position + Offset, null, Tint1 * alpha1, Rotation * 6f, Origin, Scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(CloudTexture2, Position + Offset, null, Tint2 * alpha1, Rotation * 4f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.Draw(ShadowTexture, Position + Offset, null, Color.White * alpha1, ShadowRotation, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);  
         
                    if (PlanetaryRing)
                        spriteBatch.Draw(PlanetaryRingTexture, Position + Offset, null, Tint1 * alpha1, ShadowRotation + RingRotation, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);  
                }
                else
                {
                    Origin.X = BodyTexture.Width / 2;
                    Origin.Y = BodyTexture.Height / 2;

                    float ShadowRotation = orbit.OrbitRadian - ((float)Math.PI / 2f);

                    spriteBatch.Draw(BodyTexture, Position + Offset, null, Tint1, Rotation * 5f, Origin, Scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(OverlayTexture, Position + Offset, null, Tint2, (Rotation + OverlayRotation) * 5f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint1 * 0.4f, Rotation * 2f, Origin, Scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GlowTexture2, Position + Offset, null, Tint2 * 0.4f, -Rotation * 3f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.Draw(CloudTexture, Position + Offset, null, Tint1 * 0.7f, Rotation * 6f, Origin, Scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(CloudTexture2, Position + Offset, null, Tint2 * 0.7f, Rotation * 4f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.Draw(ShadowTexture, Position + Offset, null, Color.White, ShadowRotation, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);

                    if (PlanetaryRing)
                        spriteBatch.Draw(PlanetaryRingTexture, Position + Offset, null, Tint1, ShadowRotation + RingRotation, Origin, Scale, SpriteEffects.FlipHorizontally, 0f); 

                    DrawInfo(spriteBatch, Offset, OnScreenSize);
                }
            }
        }

        private void DrawInfo(SpriteBatch spriteBatch, Vector2 Offset, float OnScreenSize)
        {
            float alpha1 = UsefulMethods.FindBetween((int)(OnScreenSize * 60), (BodyTexture.Width * 10), (BodyTexture.Width / 2) * 10, 1f, 0f, false);
            float alpha2 = UsefulMethods.FindBetween((int)(OnScreenSize * 30), (int)((BodyTexture.Width * 10) * 1.5f), (int)((BodyTexture.Width * 10) * 1f), 1f, 0f, true);

            InfoBox.ClearList();
            InfoBox.AddItem(OrbitalClass);
            InfoBox.AddItem("Mass: " + Math.Round(Mass, 2));

            //string AgeHours = "" + Age.Hours;
            //string AgeMinutes = "" + Age.Minutes;
            //string AgeSeconds = "" + Age.Seconds;

            //if (Age.Hours < 10)
            //    AgeHours = "0" + AgeHours;
            //if (Age.Minutes < 10)
            //    AgeMinutes = "0" + AgeMinutes;
            //if (Age.Seconds < 10)
            //    AgeSeconds = "0" + AgeSeconds;

            //string AgeString = AgeHours + ":" + AgeMinutes + ":" + AgeSeconds;

            string AgeString = "";
            if (Age.Days > 356)
                AgeString = (Age.Days / 356) + " Years";
            else if (Age.Days > 0)
                AgeString = Age.Days + " Days";
            else if (Age.Hours > 0)
                AgeString = Age.Hours + " Hours";
            else if (Age.Minutes > 0)
                AgeString = Age.Minutes + " Minutes";
            else if (Age.Seconds > 0)
                AgeString = Age.Seconds + " Seconds";


            InfoBox.AddItem("Age: " + AgeString);
            
            if (alpha1 > 0.05f && alpha2 == 1f)
                InfoBox.Draw(spriteBatch, Position + Offset + (new Vector2(BodyTexture.Width / 2, -(BodyTexture.Width / 2)) * Scale), Scale * 5, alpha1);
            else if (alpha2 < 1f)
                InfoBox.Draw(spriteBatch, Position + Offset + (new Vector2(BodyTexture.Width / 2, -(BodyTexture.Width / 2)) * Scale), Scale * 5, alpha2);
        }
    }
    
}
