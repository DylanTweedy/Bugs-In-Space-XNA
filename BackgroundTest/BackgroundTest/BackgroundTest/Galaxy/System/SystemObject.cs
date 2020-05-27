using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class SystemObject
    {
        public Vector2 Position;
        public float Scale;
        public float OriginalScale;
        public float Rotation;
        public byte ID;
        public float LargestScale;
        public float Mass;
        public float ActualSize;

        Color Tint;

        float AlphaTint;
        float FinalAlphaTint;

        Texture2D BodyTexture;
        Texture2D GlowTexture;
        Texture2D GlowTexture2;

        Vector2 Origin;
        string OrbitalClass;
        float OriginalRotation;
        Random rand;
        public float rotation;
        public Orbit orbit;
        TimeSpan Age;
        TimeSpan LastSeen;
        int TwinkleSpeed;

        Vector2 Position1;
        Vector2 Position2;
        Vector2 Position3;
        Vector2 Position4;
        Vector2 Position5;
        Vector2 Position6;
        Vector2 Position7;
        Vector2 Position8;
        Vector2 Position9;
        Vector2 Position10;
        Vector2 Position11;
        Vector2 Position12;
        Vector2 Position13;
        Vector2 Position14;
        Vector2 Position15;
        
        private void GetData()
        {
            //GalaxyObjectData.SelectNewObject(3);

            //OriginalScale = rand.Next((int)(GalaxyObjectData.SelectedObject.ScaleMin * 10000f), (int)(GalaxyObjectData.SelectedObject.ScaleMax * 10000f)) / 10000f;            
            //Scale = OriginalScale;

            //int lc = GalaxyObjectData.SelectedObject.Layers.Length;

            //int l = rand.Next(0, lc);
            //BodyTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];
            //l = rand.Next(0, lc);
            //GlowTexture = GalaxyObjectData.SelectedObject.Layers[l][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[l].Count)];

            //ID = GalaxyObjectData.SelectedObject.ID;
            //OrbitalClass = GalaxyObjectData.SelectedObject.Name;
            //Mass = GalaxyObjectData.SelectedObject.Mass * UsefulMethods.FindBetween((int)(OriginalScale * 1000f), (int)(GalaxyObjectData.SelectedObject.ScaleMin * 3000f), (int)(GalaxyObjectData.SelectedObject.ScaleMax * 3000f), 1f, 0f, false);

            //Tint = Color.LightCyan;
        }

        public void CreateOrbital(Random Rand)
        {
            rand = Rand;
            LastSeen = WorldVariables.GameTime;

            GetData();
            
            Age = TimeSpan.Zero;
            LastSeen = WorldVariables.GameTime;

            rotation = (float)rand.Next(10000, 15000) / 1000000f;
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
            //orbit.InitializeEllipse((0.4f * (float)rand.NextDouble()), 1f);

            //orbit.UpdatePosition(Pos);
            orbit.OriginalPosition = orbit.Position;
            orbit.OriginalRadian = orbit.OrbitRadian;
            orbit.OrbitRadius = rad;

            Position = orbit.Position;

            Position1 = Position;
            Position2 = Position;
            Position3 = Position;
            Position4 = Position;
            Position5 = Position;
            Position6 = Position;
            Position7 = Position;
            Position8 = Position;
            Position9 = Position;
            Position10 = Position;
            Position11 = Position;
            Position12 = Position;
            Position13 = Position;
            Position14 = Position;
            Position15 = Position;
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
            float Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
            float ActualSize = BodyTexture.Width * Scale;
            float OnScreenSize = ActualSize * Zoom;

            if (OnScreenSize < 0.5f)
            {
            }
            else if (OnScreenSize <= 0.5f)
            {
                float alpha1 = UsefulMethods.FindBetween((int)(OnScreenSize * 10), 80, 10, 1f, 0f, false);

                Origin.X = BodyTexture.Width / 2;
                Origin.Y = BodyTexture.Height / 2;

                float ShadowRotation = orbit.OrbitRadian - ((float)Math.PI / 2f);

                spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint * 0.4f * alpha1, -Rotation * 4f, Origin, Scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(BodyTexture, Position + Offset, null, Tint * alpha1, Rotation * 5f, Origin, Scale, SpriteEffects.None, 0f);
            }
            else
            {
                Origin.X = BodyTexture.Width / 2;
                Origin.Y = BodyTexture.Height / 2;

                float ShadowRotation = orbit.OrbitRadian;

                spriteBatch.Draw(GlowTexture, Position15 + Offset, null, Tint * 0.1f, Rotation * 6.5f, Origin, Scale * 0.1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position14 + Offset, null, Tint * 0.2f, Rotation * 5.4f, Origin, Scale * 0.2f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position13 + Offset, null, Tint * 0.3f, Rotation * 4.3f, Origin, Scale * 0.3f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position12 + Offset, null, Tint * 0.4f, Rotation * 3.2f, Origin, Scale * 0.4f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position11 + Offset, null, Tint * 0.5f, Rotation * 2.1f, Origin, Scale * 0.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position10 + Offset, null, Tint * 0.6f, Rotation *   2f, Origin, Scale * 0.6f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position9 + Offset, null, Tint * 0.7f, Rotation * 1.9f, Origin, Scale * 0.7f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position8 + Offset, null, Tint * 0.8f, Rotation * 1.8f, Origin, Scale * 0.8f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position7 + Offset, null, Tint * 0.9f, Rotation * 1.7f, Origin, Scale * 0.9f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position6 + Offset, null, Tint, Rotation * 1.6f, Origin, Scale * 1.0f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position5 + Offset, null, Tint, Rotation * 1.5f, Origin, Scale * 1.1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position4 + Offset, null, Tint, Rotation * 1.4f, Origin, Scale * 1.2f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position3 + Offset, null, Tint, Rotation * 1.3f, Origin, Scale * 1.3f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position2 + Offset, null, Tint, Rotation * 1.2f, Origin, Scale * 1.4f, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position1 + Offset, null, Tint, Rotation, Origin, Scale * 1.5f, SpriteEffects.None, 0f);

                spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint * 0.4f, -Rotation * 4f, Origin, Scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint * 0.4f, Rotation * 4f, Origin, Scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(BodyTexture, Position + Offset, null, Tint, Rotation * 5f, Origin, Scale, SpriteEffects.None, 0f);
                
                Position15 = Position14;
                Position14 = Position13;
                Position13 = Position12;
                Position12 = Position11;
                Position11 = Position10;
                Position10 = Position9;
                Position9 = Position8;
                Position8 = Position7;
                Position7 = Position6;
                Position6 = Position5;
                Position5 = Position4;
                Position4 = Position3;
                Position3 = Position2;
                Position2 = Position1;
                
                    Position1 = Position;

                DrawInfo(spriteBatch, Offset, OnScreenSize);
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
