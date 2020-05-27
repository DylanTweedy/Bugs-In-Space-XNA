using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class SystemCoreObject
    {
        public Vector2 Position;
        public float Scale;
        public float OriginalScale;
        public float Rotation;
        public int Temperature;
        public byte ID;
        public float LargestScale;
        public float Mass;
        public bool Core = false;
        public int SystemID;

        Color Tint1;
        Color Tint2;
        float AlphaTint;
        float FinalAlphaTint;

        Texture2D BodyTexture;
        Texture2D CoronaTexture;
        Texture2D GlowTexture;
        Texture2D SmallTexture;
        Texture2D OverlayTexture;
        Texture2D CoronaTexture2;
        Texture2D GlowTexture2;
        Texture2D SmallTexture2;

        bool Active;
        Vector2 Origin;
        string starClass;
        float OriginalRotation;
        Random rand;
        public float rotation;
        Orbit orbit;
        TimeSpan Age;
        TimeSpan LastSeen;
        TimeSpan LifeSpan;
        TimeSpan Timer1;
        TimeSpan Timer2;
        float RenderScale;
        bool CountUp;
        int SC;
        int TwinkleSpeed;
        float finalScale;
        int starCount;
        public bool RecalculateStar;

        bool orbiting;
        bool Dying;
        TimeSpan DyingCounter;
        
        //Make Rotations both clockwise and anti-clockwise
        //Make Orbits both clockwise and anti-clockwise
        //Add special star actions
        //Make stars engulf others when dying
        //Give long dead stars an accurate age. Using current age, lifespan and current gametime.
        //Make dying stars size based on time, rather than a per frame change, because these stars won't change whilst off screen. 
        
        private void GetData()
        {
            //if (!Core)
            {
                Tint1 = UsefulMethods.RandomColour() * 2;
                Tint2 = UsefulMethods.RandomColour() * 2;

                byte n;
                byte d;

                n = Tint1.R;
                if (Tint1.G > n)
                    n = Tint1.G;
                if (Tint1.B > n)
                    n = Tint1.B;
                d = (byte)(255 - n);
                Tint1.R += d;
                Tint1.G += d;
                Tint1.B += d;

                n = Tint2.R;
                if (Tint2.G > n)
                    n = Tint2.G;
                if (Tint2.B > n)
                    n = Tint2.B;
                d = (byte)(255 - n);
                Tint2.R += d;
                Tint2.G += d;
                Tint2.B += d;
            }
        }

        public void CreateCoreObject(Random Rand)
        {
            Active = true;
            rand = Rand;
            LastSeen = WorldVariables.GameTime;
                 
            bool FindStar = true;
            while (FindStar)
            {
                GetData();

                if (OriginalScale < LargestScale * 2f || LargestScale == 0)
                    FindStar = false;
            }

            SetupObjects();
            LifeSpan = TimeSpan.FromMinutes(rand.Next(400, 800) / 100f);
        
            //smallStar.Scale = (Scale * (star.Texture.Width / smallStar.Texture.Width)) * 2;
            //star.Tint = new Color(rand.Next(200, 255), rand.Next(200, 255), rand.Next(200, 255));
                    
            Age = TimeSpan.Zero;
            LastSeen = WorldVariables.GameTime;

            rotation = (float)rand.Next(10000, 15000) / 100000000f;
            OriginalRotation = rotation;
            finalScale = 0f;
            AlphaTint = 0f;
            Rotation = 0f;
            DyingCounter = TimeSpan.Zero;
            Dying = false;
        }

        public void SetupCoreObject(Vector2 Pos, float Rot, int StarCount, float spe, float orbitRot, float biggestStar, int id)
        {
            TwinkleSpeed = rand.Next(1, 10);
            SystemID = id;

            starCount = StarCount;
            Origin = Vector2.Zero;

            LargestScale = biggestStar;

            if (StarCount > 1)
            {
                orbiting = true;

                float rad = (600f * biggestStar * StarCount);
                float rot = orbitRot;
                
                //if (orbit == null)
                //    orbit = new Orbit(rad, spe, rot, rot);

                orbit.Initialize(1, 0, 0);

                //orbit.UpdatePosition(Pos);
                orbit.OriginalPosition = orbit.Position;
                orbit.OriginalRadian = orbit.OrbitRadian;
                orbit.OrbitRadius = rad;

                Position = orbit.Position;
            }
            else            
                Position = Pos;           
        }

        public int CalculateRadius()
        {
            if (orbiting)
                return (int)((orbit.OrbitRadius * 2) + (BodyTexture.Width * OriginalScale));
            else
                return (int)(BodyTexture.Width * OriginalScale);
        }

        private void DestroyStar()
        {
            Scale = 0f;
            Rotation = 0f;
            DyingCounter = TimeSpan.Zero;

            if (!Core)
                CreateCoreObject(rand);
        }

        private void AgingStar()
        {
            float CountStart = (float)LifeSpan.TotalMinutes * 0.2f;
            float minutesLeft = (float)(LifeSpan.TotalMinutes - Age.TotalMinutes);

            float sizeIncrease = 1 + ((CountStart - minutesLeft) / CountStart);

            Scale = OriginalScale * sizeIncrease;
            Scale = OriginalScale * sizeIncrease;
            AlphaTint = 1f;

            float increasedPercent = (15f - ((OriginalScale / Scale) * 15f)) + 1;

            Rotation += rotation * increasedPercent;

            finalScale = Scale;
            RecalculateStar = true;
        }

        private void DyingStar()
        {
            float percentToRemove = -0f;

            if (!Dying)
            {
                percentToRemove = -0.05f * ((float)DyingCounter.TotalSeconds / 1f);
                //DyingCounter += WorldVariables.GameTimePassed;

                if (DyingCounter > TimeSpan.FromSeconds(1))
                {
                    Dying = true;
                    DyingCounter = TimeSpan.Zero;
                }

                Tint1 *= 1 - percentToRemove;
                Tint2 *= 1 - percentToRemove;
            }
            else
            {
                Tint1 *= 0.95f;
                Tint2 *= 0.95f;

                percentToRemove = 0.1f * ((float)DyingCounter.TotalSeconds / 0.5f);

                //DyingCounter += WorldVariables.GameTimePassed;

            }

            Tint1.A = 255;
            Tint2.A = 255;

            Scale -= 35f * percentToRemove;
            float increasedPercent = 25f;

            Rotation += rotation * increasedPercent;

            if (Scale < OriginalScale / 2f)
                AlphaTint -= percentToRemove;

            RecalculateStar = true;

            if (Scale <= 0f)
                DestroyStar();
        }

        public void Update(GameTime gameTime, Vector2 OrbitCenter)
        {
            Age += WorldVariables.GameTime - LastSeen;

            if (Age > LifeSpan && Age - LifeSpan > TimeSpan.FromMinutes(1) && !Core)
            {
                DestroyStar();
                Age = TimeSpan.FromMinutes(rand.Next(20, (int)((float)LifeSpan.TotalMinutes * 80f)) / 100f);
                Scale = OriginalScale;
                RecalculateStar = true;
            }

            if (Rotation != Rotation)
                DestroyStar();

            if (Age.TotalMinutes < LifeSpan.TotalMinutes * 0.8f)
            {
                if (Age.TotalMinutes < 0.2)
                {
                    AlphaTint = (float)Age.TotalMinutes * 5;
                    Scale = OriginalScale * AlphaTint;
                    RecalculateStar = true;
                }
                else
                {
                    AlphaTint = 1f;
                    Scale = OriginalScale;
                }
            }
            else if (Age < LifeSpan && !Core)
                AgingStar();
            else if (Age >= LifeSpan && !Core)
                DyingStar();
            else if (Core)
            {
                AlphaTint = 1f;
                Scale = OriginalScale;
            }
            
            if (orbiting)
            {
                //orbit.UpdatePosition(OrbitCenter);

                Position = orbit.Position;
            }            
            Rotation += rotation;
            FinalAlphaTint = AlphaTint;

            ObjectActions(gameTime);

            LastSeen = WorldVariables.GameTime;
        }

        private void SetupObjects()
        {
            switch (starClass)
            {
                case "Wolf-Rayet Star":
                    break;

                case "Glorious Star":
                    break;

                case "Pulsar":
                    break;

                case "Quantum Star":
                    Timer1 = TimeSpan.Zero;
                    Timer2 = TimeSpan.FromSeconds(rand.Next(1, 10));
                    break;

                case "Translocation Star":
                    break;

                case "Ancient Core":
                    break;

                case "Ghost Star":
                    break;

                case "Rainbow Star":
                    Timer1 = TimeSpan.Zero;
                    Timer2 = TimeSpan.Zero;
                    break;
            }
        }

        private void ObjectActions(GameTime gameTime)
        {
            bool Reset1 = false;
            bool Reset2 = false;

            switch (starClass)
            {
                case "Wolf-Rayet Star":
                    break;

                case "Glorious Star":
                    break;

                case "Pulsar":
                    break;

                case "Quantum Star":

                    Timer1 += gameTime.ElapsedGameTime;

                    if (Timer1 > Timer2)
                    {
                        if (Active)
                            Active = false;
                        else
                            Active = true;

                        SetupObjects();
                    }
                    break;

                case "Translocation Star":
                    break;

                case "Ancient Core":
                    //Something else
                    Rotation -= rotation * 0.7f;
                    Tint1 = Color.White;
                    Tint2 = Color.White;
                    break;

                case "Ghost Star":
                    FinalAlphaTint = AlphaTint * 0.1f;
                    break;

                case "Rainbow Star":
                    Timer1 += gameTime.ElapsedGameTime;
                    Timer2 += gameTime.ElapsedGameTime;

                    if (Timer1.TotalMilliseconds > 800)
                    {
                        Tint1 = UsefulMethods.RandomColour();
                        Reset1 = true;
                    }
                    if (Timer1.TotalMilliseconds > 700)
                    {
                        Tint2 = UsefulMethods.RandomColour();
                        Reset2 = true;
                    }
                    break;
            }

            if (Reset1)
                Timer1 = TimeSpan.Zero;
            if (Reset2)
                Timer2 = TimeSpan.Zero;
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalTint)
        {
            if (Active)
            {
                float Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
                float ActualSize = BodyTexture.Width * Scale;
                float OnScreenSize = ActualSize * Zoom;
                int OnScreenInt = (int)(OnScreenSize * 10);

                if (OnScreenSize < SmallTexture.Width / 16)
                {
                }
                else if (OnScreenSize <= SmallTexture.Width)
                {
                    if (SC > TwinkleSpeed)
                        CountUp = false;
                    else if (SC < -TwinkleSpeed)
                        CountUp = true;

                    if (CountUp)
                        SC++;
                    else
                        SC--;

                    RenderScale = ((Scale * (BodyTexture.Width / SmallTexture.Width)) * 2f);
                    RenderScale += (SC * ActualSize) / 3000f;
                    if (Dying)
                        RenderScale *= 1.3f;

                    Color Tint1S = Tint1;
                    Color Tint2S = Tint2;

                    Tint1S.R = (byte)(UsefulMethods.FindBetween(OnScreenInt, ((SmallTexture.Width) * 10), 0, 255, Tint1S.R, true));
                    Tint1S.G = (byte)(UsefulMethods.FindBetween(OnScreenInt, ((SmallTexture.Width) * 10), 0, 255, Tint1S.G, true));
                    Tint1S.B = (byte)(UsefulMethods.FindBetween(OnScreenInt, ((SmallTexture.Width) * 10), 0, 255, Tint1S.B, true));
                    Tint2S.R = (byte)(UsefulMethods.FindBetween(OnScreenInt, ((SmallTexture.Width) * 10), 0, 255, Tint1S.R, true));
                    Tint2S.G = (byte)(UsefulMethods.FindBetween(OnScreenInt, ((SmallTexture.Width) * 10), 0, 255, Tint1S.G, true));
                    Tint2S.B = (byte)(UsefulMethods.FindBetween(OnScreenInt, ((SmallTexture.Width) * 10), 0, 255, Tint1S.B, true));

                    Tint1S.A = 255;
                    Tint2S.A = 255;


                    if (OnScreenSize <= SmallTexture.Width / 4)
                    {
                        float alpha = UsefulMethods.FindBetween((int)(OnScreenSize * 100), (SmallTexture.Width / 4) * 100, (SmallTexture.Width / 16) * 100, 1f, 0f, false);

                        Origin.X = SmallTexture.Width / 2;
                        Origin.Y = SmallTexture.Height / 2;

                        spriteBatch.Draw(SmallTexture, Position + Offset, null, Tint1S * FinalAlphaTint * alpha * FinalTint, Rotation * 3f, Origin, RenderScale, SpriteEffects.None, 1f);
                    }
                    else if (OnScreenSize <= SmallTexture.Width / 2)
                    {
                        Origin.X = SmallTexture.Width / 2;
                        Origin.Y = SmallTexture.Height / 2;

                        spriteBatch.Draw(SmallTexture, Position + Offset, null, Tint1S * FinalAlphaTint * FinalTint, Rotation * 3f, Origin, RenderScale, SpriteEffects.None, 1f);
                    }
                    else
                    {
                        float alpha1 = UsefulMethods.FindBetween(OnScreenInt, (SmallTexture.Width * 10), ((SmallTexture.Width / 2) * 10), 1f, 0f, false);
                        float alpha2 = 1 - alpha1;

                        Origin.X = BodyTexture.Width / 2;
                        Origin.Y = BodyTexture.Height / 2;

                        spriteBatch.Draw(CoronaTexture, Position + Offset, null, Color.White * 0.4f * FinalAlphaTint * alpha1 * FinalTint, Rotation * 3f, Origin, Scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(CoronaTexture2, Position + Offset, null, Color.White * 0.4f * FinalAlphaTint * alpha1 * FinalTint, -Rotation, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint1 * 0.4f * FinalAlphaTint * alpha1 * FinalTint, Rotation, Origin, Scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(GlowTexture2, Position + Offset, null, Tint2 * 0.75f * FinalAlphaTint * alpha1 * FinalTint, -Rotation * 1.5f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.Draw(BodyTexture, Position + Offset, null, Tint1 * FinalAlphaTint * alpha1 * FinalTint, Rotation * 2f, Origin, Scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(OverlayTexture, Position + Offset, null, Tint2 * 0.5f * FinalAlphaTint * alpha1 * FinalTint, Rotation * 3f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);

                        Origin.X = SmallTexture.Width / 2;
                        Origin.Y = SmallTexture.Height / 2;

                        spriteBatch.Draw(SmallTexture, Position + Offset, null, Tint1S * FinalAlphaTint * alpha2 * FinalTint, Rotation * 3f, Origin, RenderScale, SpriteEffects.None, 1f);
                    }
                }
                else
                {
                    Origin.X = BodyTexture.Width / 2;
                    Origin.Y = BodyTexture.Height / 2;

                    float A = FinalAlphaTint * FinalTint;
                    Color Tint1S = Tint1;
                    Color Tint2S = Tint2;



                    if (OnScreenSize > BodyTexture.Width)
                    {
                        float a = UsefulMethods.FindBetween((int)OnScreenSize, BodyTexture.Width * 4, BodyTexture.Width, 1f, 0.1f, true);

                        //A *= a;

                        Tint1S.A = (byte)(Tint1S.A * a);
                        Tint2S.A = (byte)(Tint2S.A * a);
                    }

                    spriteBatch.Draw(CoronaTexture, Position + Offset, null, Tint1S * 0.4f * A, Rotation * 3f, Origin, Scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(CoronaTexture2, Position + Offset, null, Tint2S * 0.4f * A, -Rotation, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.Draw(GlowTexture, Position + Offset, null, Tint1S * 0.4f * A, Rotation, Origin, Scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GlowTexture2, Position + Offset, null, Tint2S * 0.75f * A, -Rotation * 1.5f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.Draw(BodyTexture, Position + Offset, null, Tint1S * A, Rotation * 2f, Origin, Scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(OverlayTexture, Position + Offset, null, Tint2S * 0.5f * A, Rotation * 3f, Origin, Scale, SpriteEffects.FlipHorizontally, 0f);

                    if (Age < LifeSpan)
                        DrawInfo(spriteBatch, Offset, OnScreenSize, (int)CameraManager.CamerasRead[cameraNumber].Location.X, (int)CameraManager.CamerasRead[cameraNumber].Location.Y);
                }
            }
        }

        private void DrawInfo(SpriteBatch spriteBatch, Vector2 Offset, float OnScreenSize, int ChunkX, int ChunkY)
        {
            float alpha1 = UsefulMethods.FindBetween((int)(OnScreenSize * 60), (BodyTexture.Width * 10), (BodyTexture.Width / 2) * 10, 1f, 0f, false);
            float alpha2 = UsefulMethods.FindBetween((int)(OnScreenSize * 30), (int)((BodyTexture.Width * 10) * 1.5f), (int)((BodyTexture.Width * 10) * 1f), 1f, 0f, true);

            InfoBox.ClearList();
            InfoBox.AddItem(starClass);
            //InfoBox.AddItem("Planets: " + BackgroundManager.systemManager[ChunkX][ChunkY].systems[SystemID].PlanetCount);
            //InfoBox.AddItem("Comets: " + BackgroundManager.systemManager[ChunkX][ChunkY].systems[SystemID].CometCount);

            InfoBox.AddItem(Temperature + "K");
            InfoBox.AddItem("Mass: " + Math.Round(Mass, 2));

            string AgeHours = "" + Age.Hours;
            string AgeMinutes = "" + Age.Minutes;
            string AgeSeconds = "" + Age.Seconds;

            if (Age.Hours < 10)
                AgeHours = "0" + AgeHours;
            if (Age.Minutes < 10)
                AgeMinutes = "0" + AgeMinutes;
            if (Age.Seconds < 10)
                AgeSeconds = "0" + AgeSeconds;

            string AgeString = AgeHours + ":" + AgeMinutes + ":" + AgeSeconds;

            InfoBox.AddItem("Age: " + AgeString);

            TimeSpan time = LifeSpan - Age;

            AgeHours = "" + time.Hours;
            AgeMinutes = "" + time.Minutes;
            AgeSeconds = "" + time.Seconds;

            if (time.Hours < 10)
                AgeHours = "0" + AgeHours;
            if (time.Minutes < 10)
                AgeMinutes = "0" + AgeMinutes;
            if (time.Seconds < 10)
                AgeSeconds = "0" + AgeSeconds;

            AgeString = AgeHours + ":" + AgeMinutes + ":" + AgeSeconds;

            InfoBox.AddItem("Time: " + AgeString);
            
            if (alpha1 > 0.05f && alpha2 == 1f)
                InfoBox.Draw(spriteBatch, Position + Offset + (new Vector2(BodyTexture.Width / 2, -(BodyTexture.Width / 2)) * Scale), Scale * 5, alpha1);
            else if (alpha2 < 1f)
                InfoBox.Draw(spriteBatch, Position + Offset + (new Vector2(BodyTexture.Width / 2, -(BodyTexture.Width / 2)) * Scale), Scale * 5, alpha2);
        }
    }
}
