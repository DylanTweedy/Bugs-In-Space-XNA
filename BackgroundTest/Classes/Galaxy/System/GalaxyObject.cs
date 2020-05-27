using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class GalaxyObject
    {
        class ObjectInfo
        {
            public List<Texture2D> Textures;
            public List<Texture2D> SmallTextures;
            public List<Vector2> PositionOffset;
            public List<Color> Tints;
            public List<float> Rotations;
            public List<float> Alphas;
            public List<float> RotationIncrease;
            public List<SpriteEffects> Effects;
            public float OriginalScale;
            public float Scale;
            public float DestinationScale;
            public float Alpha;
            public float DestinationAlpha;

            public float LivingAlpha;
            public Vector3 Scales;
            public Vector4 RotationChanges;
            public Vector4 Severities;

            public void Initialize()
            {
                Textures = new List<Texture2D>();
                SmallTextures = new List<Texture2D>();
                PositionOffset = new List<Vector2>();

                if (Tints == null)
                    Tints = new List<Color>();
                Rotations = new List<float>();
                Alphas = new List<float>();
                RotationIncrease = new List<float>();
                Effects = new List<SpriteEffects>();

                OriginalScale = 0f;
                Scale = 0f;
                DestinationScale = 0f;
                Alpha = 0f;
                DestinationAlpha = 0f;

                Scales = Vector3.Zero;
                RotationChanges = Vector4.Zero;
                Severities = Vector4.Zero;
            }

            public void Draw(SpriteBatch spriteBatch, Vector2 Position, Vector2 Offset, float FinalAlpha, float SizeMultiplier, float ViewSize)
            {
                Vector2 Origin = Vector2.Zero;

                if (ViewSize < 1f)
                {
                }
                else if (ViewSize < 8)
                {
                    float a1 = UsefulMethods.FindBetween((int)(ViewSize * 100), 800, 1, 1f, 0f, false);

                    if (SmallTextures != null)
                        for (int i = 0; i < SmallTextures.Count; i++)
                        {
                            Origin.X = SmallTextures[i].Width / 2;
                            Origin.Y = SmallTextures[i].Height / 2;
                            spriteBatch.Draw(SmallTextures[i], Position + Offset + PositionOffset[i], null, Tints[i] * Alpha * FinalAlpha * a1, Rotations[i], Origin, Scale * SizeMultiplier * 32, Effects[i], 0f);
                        }
                }
                else if (ViewSize < 64)
                {
                    if (SmallTextures != null)
                        for (int i = 0; i < SmallTextures.Count; i++)
                        {
                            Origin.X = SmallTextures[i].Width / 2;
                            Origin.Y = SmallTextures[i].Height / 2;
                            spriteBatch.Draw(SmallTextures[i], Position + Offset + PositionOffset[i], null, Tints[i] * Alpha * FinalAlpha, Rotations[i], Origin, Scale * SizeMultiplier * 32, Effects[i], 0f);
                        }
                }
                else if (ViewSize < 128)
                {
                    float a1 = UsefulMethods.FindBetween((int)(ViewSize * 100), 12800, 6400, 1f, 0f, false);
                    float a2 = 1f - a1;


                    if (Textures != null)
                        for (int i = Textures.Count - 1; i >= 0; i--)
                        {
                            Origin.X = Textures[i].Width / 2;
                            Origin.Y = Textures[i].Height / 2;
                            spriteBatch.Draw(Textures[i], Position + Offset + PositionOffset[i], null, Tints[i] * Alphas[i] * Alpha * FinalAlpha * a1, Rotations[i], Origin, Scale * SizeMultiplier, Effects[i], 0f);
                        }

                    if (SmallTextures != null)
                        for (int i = 0; i < SmallTextures.Count; i++)
                        {
                            Origin.X = SmallTextures[i].Width / 2;
                            Origin.Y = SmallTextures[i].Height / 2;
                            spriteBatch.Draw(SmallTextures[i], Position + Offset + PositionOffset[i], null, Tints[i] * Alpha * FinalAlpha * a2, Rotations[i], Origin, Scale * SizeMultiplier * 32, Effects[i], 0f);
                        }
                }
                else
                {
                    if (Textures != null)
                        for (int i = Textures.Count - 1; i >= 0; i--)
                        {
                            Origin.X = Textures[i].Width / 2;
                            Origin.Y = Textures[i].Height / 2;
                            spriteBatch.Draw(Textures[i], Position + Offset + PositionOffset[i], null, Tints[i] * Alphas[i] * Alpha * FinalAlpha, Rotations[i], Origin, Scale * SizeMultiplier, Effects[i], 0f);
                        }
                }
            }
        }

        public float Scale
        {
            get
            {
                float f1;
                float f2;

                if (ObjectInfo1 != null)
                    f1 = ObjectInfo1.Scale * SizeMultiplier;
                else
                    f1 = 0;

                if (ObjectInfo2 != null)
                    f2 = ObjectInfo2.Scale * SizeMultiplier;
                else
                    f2 = 0;

                if (f1 > f2)
                    return f1;
                else
                    return f2;
            }
        }

        public float OriginalScale
        {
            get
            {
                float f1;
                float f2;

                if (ObjectInfo1 != null)
                    f1 = ObjectInfo1.Scale * SizeMultiplier;
                else
                    f1 = 0;

                if (ObjectInfo2 != null)
                    f2 = ObjectInfo2.Scale * SizeMultiplier;
                else
                    f2 = 0;

                if (f1 > f2 && ObjectInfo1 != null)
                    return ObjectInfo1.OriginalScale;
                else if (ObjectInfo2 != null)
                    return ObjectInfo2.OriginalScale;
                else
                    return 0;
                
            }

            set
            {
                float f1;
                float f2;

                if (ObjectInfo1 != null)
                    f1 = ObjectInfo1.Scale * SizeMultiplier;
                else
                    f1 = 0;

                if (ObjectInfo2 != null)
                    f2 = ObjectInfo2.Scale * SizeMultiplier;
                else
                    f2 = 0;

                if (f1 > f2 && ObjectInfo1 != null)
                {
                    ObjectInfo1.OriginalScale += value;
                    ObjectInfo1.Scale += value;
                    ObjectInfo1.Scales += new Vector3(value, value, value);
                }
                else if (ObjectInfo2 != null)
                {
                    ObjectInfo2.OriginalScale += value;
                    ObjectInfo1.Scale += value;
                    ObjectInfo1.Scales += new Vector3(value, value, value);
                }
            }
        }

        public Vector2 Position;

        List<Vector2> P;

        Vector2 Velocity;
        public byte Type;

        public string Name;
        public string Class;
        public TimeSpan TotalAge;
        public TimeSpan Age;
        public TimeSpan LifeSpan;
        public TimeSpan LastSeen;
        public float Temperature;
        public float Mass;
        public List<int> ElementDistrution;

        ObjectInfo ObjectInfo1;
        ObjectInfo ObjectInfo2;

        Vector3 DeathActions;
        
        bool Dead;

        bool DeathScale;
        float ExplosionDestinationScale;
        float ExplosionScale;
        bool DeathExplosion;

        public Orbit OrbitSystem;

        TimeSpan TimePassed;
        float fTimePassed;
        int GrowTime;
        Vector2 Origin;
        float RotationMultiplier;
        byte ObjectID;
        Random rand;
        int SystemSize;
        int CoreSize;
        bool AntiClockwise;
        List<byte> Iterations;
        int CurrentIteration;
        float SizeMultiplier;

        public void Setup(byte type, int systemSize, int coreSize, bool antiClockwise, float sizeMultiplier, bool first, Random generationRand)
        {
            Type = type;
            SystemSize = systemSize;
            CoreSize = coreSize;
            rand = new Random();
            LastSeen = TimeSpan.Zero;
            Age = TimeSpan.Zero;
            Origin = Vector2.Zero;
            AntiClockwise = antiClockwise;

            ObjectInfo1 = new ObjectInfo();
            ObjectInfo2 = new ObjectInfo();

            RotationMultiplier = 1f;
            DeathExplosion = false;
            DeathScale = false;
            CurrentIteration = 0;
            Iterations = new List<byte>();

            SizeMultiplier = sizeMultiplier;

            GetData(255, 1);

            switch (Type)
            {
                case 1:
                    break;
            }
        }

        public void SetupOrbit(int Count, int Iteration, float Speed, float Radius, byte type, float A, float B, Vector2 Posi, Random randd)
        {
            Position = Posi;

            if (type != 0)
            {
                float pos = (MathHelper.TwoPi / Count) * Iteration;

                OrbitSystem = new Orbit(Radius, Speed, pos, pos, Posi);
                OrbitSystem.Initialize(type, A, B);
            }
            else
            {
                float x = randd.Next(CoreSize / 2, SystemSize / 2);
                float y = randd.Next(CoreSize / 2, SystemSize / 2);
                if (randd.Next(0, 2) == 0)
                    x = -x;
                if (randd.Next(0, 2) == 0)
                    y = -y;
                Position += new Vector2(x, y);

                float dx = Position.X - Posi.X;
                float dy = Position.Y - Posi.Y;


                if (Math.Abs(dx) > Math.Abs(dy))
                    Velocity = new Vector2(dx / 100, -dy / 100);
                else
                    Velocity = new Vector2(-dx / 100, dy / 100);

                //Velocity = Vector2.Zero;


                //Velocity = new Vector2(rand.Next(0, 10000) - 5000, rand.Next(0, 10000) - 5000);
            }
        }

        private void GetData(byte ID, byte Item)
        {
            if (Item == 1)
            {
                if (ID == 255)
                {
                    TimeSpan TotalLife = TimeSpan.Zero;

                    while (Age > LifeSpan || ObjectID == 0)
                    {
                        GetNewObject(ID);

                        TotalLife = TimeSpan.Zero;
                        bool skip = false;
                        byte id = GalaxyObjectData.SelectedObject.ID;

                        for (int i = 0; i < Iterations.Count; i++)
                        {
                            if (Iterations[i] != 43)
                            {
                                GalaxyObjectData.SelectObjectID(Iterations[i]);
                                TotalLife += TimeSpan.FromDays(UsefulMethods.FindBetween(CoreSize, 300000, 8334, 1.5f, 0.5f, true) * GalaxyObjectData.SelectedObject.Lifespan);
                            }
                            else
                                skip = true;
                        }

                        GalaxyObjectData.SelectObjectID(id);

                        if (id == 43)
                        {
                            LifeSpan = TimeSpan.FromDays(WorldVariables.EndTime.TotalDays * 1.2) - TotalLife;
                            break;
                        }

                        if (TotalLife > WorldVariables.EndTime && TotalLife < TimeSpan.FromDays(WorldVariables.EndTime.TotalDays * 1.2) && !skip)
                        {
                            GalaxyObjectData.SelectObjectID(43);

                            CurrentIteration++;
                            Iterations.Add(43);
                            Age -= LifeSpan;
                            LifeSpan = TimeSpan.FromDays(WorldVariables.EndTime.TotalDays * 1.2) - TotalLife;
                            ObjectID = GalaxyObjectData.SelectedObject.ID;
                        }
                    }
                }
                else
                {
                    while (Age < TimeSpan.Zero && CurrentIteration != 0)
                        GetNewObject(Iterations[CurrentIteration - 1]);
                }

                Temperature = GalaxyObjectData.SelectedObject.Temp;
                Class = GalaxyObjectData.SelectedObject.Name;
                GrowTime = (int)(LifeSpan.TotalMinutes * 0.1);
            }
            else if (Item == 2)            
                GalaxyObjectData.SelectObjectID(ID);
            
            DrawingData(Item);
            LifeData(Item);
        }

        //private void DrawingData(byte Item)
        //{
        //    ObjectInfo Info = new ObjectInfo();
        //    Info.Initialize();

        //    for (int i = 0; i < GalaxyObjectData.SelectedObject.Layers.Length; i++)
        //        for (int o = 0; o < GalaxyObjectData.SelectedObject.LayerCount[i]; o++)
        //            Info.Textures.Add(GalaxyObjectData.SelectedObject.Layers[i][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[i].Count)]);

        //    if (Info.Tints.Count == 0)
        //        for (int i = 0; i < Info.Textures.Count; i++)
        //            Info.Tints.Add(UsefulMethods.RandomColour());

        //    for (int i = 0; i < Info.Textures.Count; i++)
        //    {
        //        int o = rand.Next(0, 3);

        //        switch (o)
        //        {
        //            case 0:
        //                Info.Effects.Add(SpriteEffects.None);
        //                break;

        //            case 1:
        //                Info.Effects.Add(SpriteEffects.FlipHorizontally);
        //                break;

        //            case 2:
        //                Info.Effects.Add(SpriteEffects.FlipVertically);
        //                break;
        //        }

        //        Info.PositionOffset.Add(Vector2.Zero);
        //        Info.Rotations.Add(rand.Next(0, (int)(MathHelper.TwoPi * 1000)) / 10000f);

        //        if (i == 0)
        //            Info.RotationIncrease.Add((float)rand.Next(10000, 15000) / 1000000f);
        //        else
        //            Info.RotationIncrease.Add(Info.RotationIncrease[0] * (1 + ((1 + i) / 2)));
        //    }

        //    for (int i = 0; i < Info.Rotations.Count; i++)
        //        if (AntiClockwise)
        //            Info.RotationIncrease[i] = -Info.RotationIncrease[i];

        //    Info.OriginalScale = (CoreSize / Info.Textures[0].Width) * GalaxyObjectData.SelectedObject.Scale * SizeMultiplier;
        //    Info.DestinationScale = Info.OriginalScale;
        //    Info.Scale = 0f;
        //    Info.Alpha = 0f;

        //    switch (Type)
        //    {
        //        case 1:
        //            Info.Alphas = new List<float>();
        //            Info.Alphas.Add(0.5f);
        //            Info.Alphas.Add(1f);
        //            Info.Alphas.Add(0.5f);
        //            Info.Alphas.Add(0.75f);
        //            Info.Alphas.Add(0.5f);
        //            Info.Alphas.Add(0.75f);
        //            break;
        //    }

        //    if (Item == 1)
        //        ObjectInfo1 = Info;
        //    else if (Item == 2)
        //        ObjectInfo2 = Info;
        //}

        private void DrawingData(byte Item)
        {
            List<Texture2D> Textures = new List<Texture2D>();
            List<Texture2D> SmallTextures = new List<Texture2D>();
            List<Vector2> PositionOffset = new List<Vector2>();
            List<Color> Tints = new List<Color>();
            List<float> Rotations = new List<float>();
            List<float> Alphas = new List<float>();
            List<float> RotationIncrease = new List<float>();
            List<SpriteEffects> Effects = new List<SpriteEffects>();
            float OriginalScale;
            float Scale;
            float DestinationScale;
            float Alpha;
               
            for (int i = 0; i < GalaxyObjectData.SelectedObject.Layers.Length; i++)
                for (int o = 0; o < GalaxyObjectData.SelectedObject.LayerCount[i]; o++)
                    Textures.Add(GalaxyObjectData.SelectedObject.Layers[i][rand.Next(0, GalaxyObjectData.SelectedObject.Layers[i].Count)]);

            for (int i = 0; i < GalaxyObjectData.SelectedObject.SmallLayers.Length; i++)
                SmallTextures.Add(GalaxyObjectData.SelectedObject.SmallLayers[i][rand.Next(0, GalaxyObjectData.SelectedObject.SmallLayers[i].Count)]);

            if (Tints.Count == 0)            
                switch (Type)
                {
                    case 1:
                        for (int i = 0; i < Textures.Count; i++)
                        {
                            Tints.Add(UsefulMethods.RandomColour());

                            byte n;
                            byte d;

                            n = Tints[Tints.Count - 1].R;
                            if (Tints[Tints.Count - 1].G > n)
                                n = Tints[Tints.Count - 1].G;
                            if (Tints[Tints.Count - 1].B > n)
                                n = Tints[Tints.Count - 1].B;
                            d = (byte)(255 - n);
                            Tints[Tints.Count - 1] = new Color(Tints[Tints.Count - 1].R + d, Tints[Tints.Count - 1].G + d, Tints[Tints.Count - 1].B + d);
                        }

                        break;

                    case 3:
                        for (int i = 0; i < Textures.Count; i++)
                            Tints.Add(Color.White);
                        
                        break;
                }
            

            for (int i = 0; i < Textures.Count; i++)
            {
                int o = rand.Next(0, 3);

                switch (o)
                {
                    case 0:
                        Effects.Add(SpriteEffects.None);
                        break;

                    case 1:
                        Effects.Add(SpriteEffects.FlipHorizontally);
                        break;

                    case 2:
                        Effects.Add(SpriteEffects.FlipVertically);
                        break;
                }

                PositionOffset.Add(Vector2.Zero);
                Rotations.Add(rand.Next(0, (int)(MathHelper.TwoPi * 1000)) / 10000f);

                if (i == 0)
                    RotationIncrease.Add((float)rand.Next(10000, 15000) / 1000000f);
                else
                    RotationIncrease.Add(RotationIncrease[0] * (1 + ((1 + i) / 2)));
            }

            for (int i = 0; i < Rotations.Count; i++)
                if (AntiClockwise)
                    RotationIncrease[i] = -RotationIncrease[i];

            OriginalScale = (CoreSize / Textures[0].Width) * GalaxyObjectData.SelectedObject.Scale * SizeMultiplier;
            DestinationScale = OriginalScale;
            Scale = 0f;
            Alpha = 0f;

            switch (Type)
            {
                case 1:
                    Alphas = new List<float>();
                    Alphas.Add(0.5f);
                    Alphas.Add(1f);
                    Alphas.Add(0.5f);
                    Alphas.Add(0.75f);
                    Alphas.Add(0.5f);
                    Alphas.Add(0.75f);
                    break;

                default:
                    Alphas = new List<float>();

                    for (int i = 0; i < Textures.Count; i++)
                        Alphas.Add(1f);
                    break;
            }

            if (Item == 1)
            {
                ObjectInfo1 = new ObjectInfo();
                ObjectInfo1.Initialize();

                for (int i = 0; i < Textures.Count; i++)
                    ObjectInfo1.Textures.Add(Textures[i]);
                for (int i = 0; i < SmallTextures.Count; i++)
                    ObjectInfo1.SmallTextures.Add(SmallTextures[i]);
                for (int i = 0; i < PositionOffset.Count; i++)
                    ObjectInfo1.PositionOffset.Add(PositionOffset[i]);
                for (int i = 0; i < Tints.Count; i++)
                    ObjectInfo1.Tints.Add(Tints[i]);
                for (int i = 0; i < Rotations.Count; i++)
                    ObjectInfo1.Rotations.Add(Rotations[i]);
                for (int i = 0; i < Alphas.Count; i++)
                    ObjectInfo1.Alphas.Add(Alphas[i]);
                for (int i = 0; i < RotationIncrease.Count; i++)
                    ObjectInfo1.RotationIncrease.Add(RotationIncrease[i]);
                for (int i = 0; i < Effects.Count; i++)
                    ObjectInfo1.Effects.Add(Effects[i]);

                ObjectInfo1.OriginalScale = OriginalScale;
                ObjectInfo1.Scale = Scale;
                ObjectInfo1.DestinationScale = DestinationScale;
                ObjectInfo1.Alpha = Alpha;
            }
            else if (Item == 2)
            {
                ObjectInfo2 = new ObjectInfo();
                ObjectInfo2.Initialize();

                for (int i = 0; i < Textures.Count; i++)
                    ObjectInfo2.Textures.Add(Textures[i]);
                for (int i = 0; i < SmallTextures.Count; i++)
                    ObjectInfo2.SmallTextures.Add(SmallTextures[i]);
                for (int i = 0; i < PositionOffset.Count; i++)
                    ObjectInfo2.PositionOffset.Add(PositionOffset[i]);
                for (int i = 0; i < Tints.Count; i++)
                    ObjectInfo2.Tints.Add(Tints[i]);
                for (int i = 0; i < Rotations.Count; i++)
                    ObjectInfo2.Rotations.Add(Rotations[i]);
                for (int i = 0; i < Alphas.Count; i++)
                    ObjectInfo2.Alphas.Add(Alphas[i]);
                for (int i = 0; i < RotationIncrease.Count; i++)
                    ObjectInfo2.RotationIncrease.Add(RotationIncrease[i]);
                for (int i = 0; i < Effects.Count; i++)
                    ObjectInfo2.Effects.Add(Effects[i]);

                ObjectInfo2.OriginalScale = OriginalScale;
                ObjectInfo2.Scale = Scale;
                ObjectInfo2.DestinationScale = DestinationScale;
                ObjectInfo2.Alpha = Alpha;
            }
        }

        //private void LifeData(byte ID, byte Item)
        //{
        //    ObjectInfo Info = new ObjectInfo();
            
        //    float sc = 1f;
        //    if (Item == 1)
        //    {
        //        Info = ObjectInfo1;
        //        sc = Info.OriginalScale;
        //    }
        //    else if (Item == 2)
        //    {
        //        Info = ObjectInfo2;
        //        sc = Info.OriginalScale;
        //    }
            
        //    switch (Type)
        //    {
        //        case 1:
        //            switch (ID)
        //            {
        //                case 24:
        //                case 41:
        //                case 42:
        //                    Info.Scales = new Vector3(sc * 0.7f, sc * 0.9f, sc * 1f);
        //                    break;

        //                default:
        //                    Info.Scales = new Vector3(sc * 0.8f, sc * 1.2f, sc * 2.5f);
        //                    break;
        //            }

        //            switch (ID)
        //            {
        //                case 24:
        //                case 41:
        //                case 42:
        //                    Info.RotationChanges = new Vector4(0.01f, 0.01f, 0.01f, 10f);
        //                    break;

        //                default:
        //                    Info.RotationChanges = new Vector4(1f, 3f, 10f, 25f);
        //                    break;
        //            }

        //            switch (ID)
        //            {
        //                case 24:
        //                case 41:
        //                case 42:
        //                    Info.Severities = new Vector4(0f, 0f, 0f, 0f);
        //                    break;

        //                default:
        //                    Info.Severities = new Vector4(0f, 0f, 75f, 100f);
        //                    break;
        //            }

        //            switch (ID)
        //            {
        //                default:
        //                    Info.LivingAlpha = 1f;
        //                    break;
        //            }
        //            break;
        //    }

        //    if (Item == 1)
        //        ObjectInfo1 = Info;
        //    else if (Item == 2)
        //        ObjectInfo2 = Info;
        //}

        private void LifeData(byte Item)
        {
            byte ID = GalaxyObjectData.SelectedObject.ID;
            float LivingAlpha = 0f;
            Vector3 Scales = Vector3.Zero;
            Vector4 RotationChanges = Vector4.Zero;
            Vector4 Severities = Vector4.Zero;

            float sc = 1f;
            if (Item == 1)
                sc = ObjectInfo1.OriginalScale;            
            else if (Item == 2)
                sc = ObjectInfo2.OriginalScale;

            switch (Type)
            {
                case 1:
                    switch (ID)
                    {
                        case 24:
                        case 41:
                        case 42:
                            Scales = new Vector3(sc * 0.7f, sc * 0.9f, sc * 1f);
                            break;

                        default:
                            Scales = new Vector3(sc * 0.8f, sc * 1.2f, sc * 2.5f);
                            break;
                    }

                    switch (ID)
                    {
                        case 24:
                        case 41:
                        case 42:
                            RotationChanges = new Vector4(0.01f, 0.01f, 0.01f, 10f);
                            break;

                        default:
                            RotationChanges = new Vector4(1f, 3f, 10f, 25f);
                            break;
                    }

                    switch (ID)
                    {
                        case 24:
                        case 41:
                        case 42:
                            Severities = new Vector4(0f, 0f, 0f, 0f);
                            break;

                        default:
                            Severities = new Vector4(0f, 0f, 75f, 100f);
                            break;
                    }

                    switch (ID)
                    {
                        default:
                            LivingAlpha = 1f;
                            break;
                    }
                    break;


                default:
                    Scales = new Vector3(sc * 1f, sc * 1f, sc * 1f);
                    Severities = new Vector4(0f, 0f, 0f, 0f);
                    RotationChanges = new Vector4(1f, 1f, 1f, 1f);
                    LivingAlpha = 1f;
                    break;
            }

            if (Item == 1)
            {
                ObjectInfo1.LivingAlpha = LivingAlpha;
                ObjectInfo1.Scales = Scales;
                ObjectInfo1.RotationChanges = RotationChanges;
                ObjectInfo1.Severities = Severities;
            }
            else if (Item == 2)
            {
                ObjectInfo2.LivingAlpha = LivingAlpha;
                ObjectInfo2.Scales = Scales;
                ObjectInfo2.RotationChanges = RotationChanges;
                ObjectInfo2.Severities = Severities;
            }
            
        }

        private void GetNewObject(byte ID)
        {     
            if (ID == 255)
            {
                bool NewObject = false;
                if (ObjectID == 0)
                    NewObject = true;

                if (!NewObject)
                    Age -= LifeSpan;

                if (CurrentIteration >= Iterations.Count - 1)
                {
                    if (NewObject)
                        GalaxyObjectData.CreateNew(Type);
                    else
                        DeathActions = GalaxyObjectData.SelectNext(ObjectID, Type);

                    CurrentIteration = Iterations.Count;
                    Iterations.Add(GalaxyObjectData.SelectedObject.ID);
                }
                else
                {
                    CurrentIteration++;
                    GalaxyObjectData.SelectObjectID(Iterations[CurrentIteration]);
                }
                
                LifeSpan = TimeSpan.FromDays(UsefulMethods.FindBetween(CoreSize, 300000, 8334, 1.5f, 0.5f, true) * GalaxyObjectData.SelectedObject.Lifespan);            
                ObjectID = GalaxyObjectData.SelectedObject.ID;   
            }
            else
            {
                GalaxyObjectData.SelectObjectID(ID);

                LifeSpan = TimeSpan.FromDays(UsefulMethods.FindBetween(CoreSize, 300000, 8334, 1.5f, 0.5f, true) * GalaxyObjectData.SelectedObject.Lifespan);
                Age += LifeSpan;
                ObjectID = GalaxyObjectData.SelectedObject.ID;
                CurrentIteration = CurrentIteration - 1;           
            }
        }

        private void CheckRotation(int i, int ObjectNum)
        {
            switch (ObjectNum)
            {
                case 1:
                    if (ObjectInfo1.Rotations[i] > MathHelper.TwoPi)
                        ObjectInfo1.Rotations[i] -= MathHelper.TwoPi * (int)(ObjectInfo1.Rotations[i] / MathHelper.TwoPi);
                    else if (ObjectInfo1.Rotations[i] < -MathHelper.TwoPi)
                        ObjectInfo1.Rotations[i] += MathHelper.TwoPi * (int)(Math.Abs(ObjectInfo1.Rotations[i]) / MathHelper.TwoPi);
                    break;

                case 2:
                    if (ObjectInfo2.Rotations[i] > MathHelper.TwoPi)
                        ObjectInfo2.Rotations[i] -= MathHelper.TwoPi * (int)(ObjectInfo2.Rotations[i] / MathHelper.TwoPi);
                    else if (ObjectInfo2.Rotations[i] < -MathHelper.TwoPi)
                        ObjectInfo2.Rotations[i] += MathHelper.TwoPi * (int)(Math.Abs(ObjectInfo2.Rotations[i]) / MathHelper.TwoPi);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            TimePassed = WorldVariables.GameTime - LastSeen;
            fTimePassed = (float)TimePassed.TotalSeconds;
            TotalAge += TimePassed;
            Age += TimePassed;
            
            //Temp stuff
            //LifeSpan = TimeSpan.FromMinutes(10);
            //////////////

            if (ObjectInfo1 != null)
                if (Type == 1)
                    Mass = CoreSize / 100;
                else
                    Mass = ObjectInfo1.OriginalScale;

            UpdateDrawVariables(gameTime);
            LastSeen = WorldVariables.GameTime;
        }

        private void UpdateDrawVariables(GameTime gameTime)
        {
            float TimePassed = (float)(WorldVariables.GameTime.TotalSeconds - LastSeen.TotalSeconds);

            if (Age < TimeSpan.Zero)
            {
                if (CurrentIteration != 0)
                    ReverseIteration();
            }
            else if (Age < TimeSpan.FromMinutes(GrowTime))
                EarlyLife(gameTime);
            else if (Age.TotalMinutes > LifeSpan.TotalMinutes * 0.8f && Age < LifeSpan)
                LateLife(gameTime);
            else if (Age > LifeSpan)
            {
                ObjectInfo2 = new ObjectInfo();
                ObjectInfo2.Initialize();
                ObjectInfo2.Textures = ObjectInfo1.Textures;
                ObjectInfo2.SmallTextures = ObjectInfo1.SmallTextures;
                ObjectInfo2.Tints = ObjectInfo1.Tints;
                ObjectInfo2.Scale = ObjectInfo1.Scale;
                ObjectInfo2.OriginalScale = ObjectInfo1.OriginalScale;
                ObjectInfo2.Alpha = ObjectInfo1.Alpha;
                ObjectInfo2.Scales = ObjectInfo1.Scales;
                ObjectInfo2.RotationChanges = ObjectInfo1.RotationChanges;
                ObjectInfo2.LivingAlpha = ObjectInfo1.LivingAlpha;
                Dead = true;
                ExplosionScale = 0f;

                for (int i = 0; i < ObjectInfo1.Rotations.Count; i++)
                {
                    float r = ObjectInfo1.Rotations[i];
                    ObjectInfo2.Rotations.Add(r);

                    r = ObjectInfo1.RotationIncrease[i];
                    ObjectInfo2.RotationIncrease.Add(r);

                    r = ObjectInfo1.Alphas[i];
                    ObjectInfo2.Alphas.Add(r);

                    SpriteEffects s = ObjectInfo1.Effects[i];
                    ObjectInfo2.Effects.Add(s);

                    Vector2 v = ObjectInfo1.PositionOffset[i];
                    ObjectInfo2.PositionOffset.Add(v);
                }

                GetData(255, 1);
            }
            else
                MidLife(gameTime);

            UpdateDeath(gameTime);

            //if (TimePassed > 1f)
            //    TimePassed = 1f;
            
            //if (Scale != DestinationScale)
            //    Scale += (DestinationScale - Scale) * TimePassed;
            
            //if (Alpha != 1f)
            //    Alpha += (1 - Alpha) * TimePassed;
            
            //for (int i = 0; i < Rotations.Count; i++)
            //{
            //    Rotations[i] += RotationIncrease[i] * RotationMultiplier * TimePassed;

            //    if (Rotations[i] > MathHelper.TwoPi)
            //        Rotations[i] -= MathHelper.TwoPi;
            //}
        }

        private void ReverseIteration()
        {
            GetData(Iterations[CurrentIteration - 1], 1);

            if (ObjectInfo2 != null)
            {
                ObjectInfo1 = new ObjectInfo();
                ObjectInfo1.Initialize();
                ObjectInfo1.Textures = ObjectInfo2.Textures;
                ObjectInfo1.SmallTextures = ObjectInfo2.SmallTextures;
                ObjectInfo1.Tints = ObjectInfo2.Tints;
                ObjectInfo1.Scale = ObjectInfo2.Scale;
                ObjectInfo1.OriginalScale = ObjectInfo2.OriginalScale;
                ObjectInfo1.Alpha = ObjectInfo2.Alpha;
                ObjectInfo1.Scales = ObjectInfo2.Scales;
                ObjectInfo1.RotationChanges = ObjectInfo2.RotationChanges;
                ObjectInfo1.LivingAlpha = ObjectInfo2.LivingAlpha;

                for (int i = 0; i < ObjectInfo2.Rotations.Count; i++)
                {
                    float r = ObjectInfo2.Rotations[i];
                    ObjectInfo1.Rotations.Add(r);

                    r = ObjectInfo2.RotationIncrease[i];
                    ObjectInfo1.RotationIncrease.Add(r);

                    r = ObjectInfo2.Alphas[i];
                    ObjectInfo1.Alphas.Add(r);

                    SpriteEffects s = ObjectInfo2.Effects[i];
                    ObjectInfo1.Effects.Add(s);

                    Vector2 v = ObjectInfo2.PositionOffset[i];
                    ObjectInfo1.PositionOffset.Add(v);
                }
            }
        }

        private void EarlyLife(GameTime gameTime)
        {
            Vector2 Offset = Vector2.Zero;
            int BetweenTime = (int)UsefulMethods.FindBetween((int)Age.TotalMinutes, GrowTime, 0, 100000f, 0f, false);
            float Multiplier = UsefulMethods.FindBetween(BetweenTime, 100000, 0, 1f, 0f, false);
            int Severity = (int)UsefulMethods.FindBetween((int)Math.Abs(fTimePassed * 10000), 200, 0, ObjectInfo1.Severities.X, 0f, false);
            float rotationChange = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.RotationChanges.X, 0f, false);
            ObjectInfo1.Scale = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.Scales.X, 0f, false);
            ObjectInfo1.Alpha = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.LivingAlpha, 0f, false);

            for (int i = 0; i < ObjectInfo1.Rotations.Count; i++)
            {
                ObjectInfo1.Rotations[i] += ObjectInfo1.RotationIncrease[i] * RotationMultiplier * fTimePassed * rotationChange;
                CheckRotation(i, 1);
            }

            for (int i = 0; i < ObjectInfo1.PositionOffset.Count; i++)
            {
                if (Severity == 0)
                    ObjectInfo1.PositionOffset[i] = Vector2.Zero;
                else
                {
                    Offset.X = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo1.OriginalScale * Multiplier;
                    Offset.Y = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo1.OriginalScale * Multiplier;
                    ObjectInfo1.PositionOffset[i] = Offset;
                }
            }

            if (ObjectInfo2 == null)
            {
                if (CurrentIteration != 0)
                    GetData(Iterations[CurrentIteration - 1], 2);
                //else
                //    GetData(
            }
        }

        private void MidLife(GameTime gameTime)
        {
            Vector2 Offset = Vector2.Zero;
            int BetweenTime = (int)UsefulMethods.FindBetween((int)Age.TotalMinutes, (int)(LifeSpan.TotalMinutes * 0.8f), GrowTime, 100000f, 0f, false);
            float Multiplier = UsefulMethods.FindBetween(BetweenTime, 100000, 0, 1f, 0f, false);
            int Severity = (int)UsefulMethods.FindBetween((int)Math.Abs(fTimePassed * 10000), 200, 0, ObjectInfo1.Severities.Y, 0f, false);
            float rotationChange = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.RotationChanges.Y, ObjectInfo1.RotationChanges.X, false);
            ObjectInfo1.Scale = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.Scales.Y, ObjectInfo1.Scales.X, false);
            ObjectInfo1.Alpha = ObjectInfo1.LivingAlpha;

            for (int i = 0; i < ObjectInfo1.Rotations.Count; i++)
            {
                ObjectInfo1.Rotations[i] += ObjectInfo1.RotationIncrease[i] * RotationMultiplier * fTimePassed * rotationChange;
                CheckRotation(i, 1);
            }
            
            for (int i = 0; i < ObjectInfo1.PositionOffset.Count; i++)
            {
                if (Severity == 0)
                    ObjectInfo1.PositionOffset[i] = Vector2.Zero;
                else
                {
                    Offset.X = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo1.OriginalScale * Multiplier;
                    Offset.Y = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo1.OriginalScale * Multiplier;
                    ObjectInfo1.PositionOffset[i] = Offset;
                }
            }
        }

        private void LateLife(GameTime gameTime)
        {
            Vector2 Offset = Vector2.Zero;            
            int BetweenTime = (int)UsefulMethods.FindBetween((int)Age.TotalMinutes, (int)(LifeSpan.TotalMinutes), (int)(LifeSpan.TotalMinutes * 0.8f), 100000f, 0f, false);            
            float Multiplier = UsefulMethods.FindBetween(BetweenTime, 100000, 0, 1f, 0f, false);
            int Severity = (int)UsefulMethods.FindBetween((int)Math.Abs(fTimePassed * 10000), 200, 0, ObjectInfo1.Severities.Z, 0f, false);
            float rotationChange = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.RotationChanges.Z, ObjectInfo1.RotationChanges.Y, false);
            ObjectInfo1.Scale = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo1.Scales.Z, ObjectInfo1.Scales.Y, false);
            ObjectInfo1.Alpha = ObjectInfo1.LivingAlpha;
            
            for (int i = 0; i < ObjectInfo1.Rotations.Count; i++)
            {
                ObjectInfo1.Rotations[i] += ObjectInfo1.RotationIncrease[i] * RotationMultiplier * fTimePassed * rotationChange;
                CheckRotation(i, 1);
            }

            for (int i = 0; i < ObjectInfo1.PositionOffset.Count; i++)
            {
                if (Severity == 0)
                    ObjectInfo1.PositionOffset[i] = Vector2.Zero;                
                else
                {
                    Offset.X = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo1.OriginalScale * Multiplier;
                    Offset.Y = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo1.OriginalScale * Multiplier;
                    ObjectInfo1.PositionOffset[i] = Offset;
                }
            }
        }

        private void UpdateDeath(GameTime gameTime)
        {
            if (ObjectInfo2 != null)
            {
                ObjectInfo2.Alpha = ObjectInfo2.LivingAlpha - UsefulMethods.FindBetween((int)(ObjectInfo1.Alpha * 10000), 10000, 0, ObjectInfo2.LivingAlpha, 0f, false);

                if (ObjectInfo2.Alpha == 0f)
                {
                    ObjectInfo2 = null;
                    Dead = false;
                    DeathExplosion = false;
                    DeathScale = false;
                    RotationMultiplier = 1f;
                }
                else
                {
                    Vector2 Offset = Vector2.Zero;
                    int BetweenTime = (int)UsefulMethods.FindBetween((int)Age.TotalMinutes, GrowTime, 0, 100000f, 0f, false);
                    float Multiplier = UsefulMethods.FindBetween(BetweenTime, 100000, 0, 1f, 0f, false);
                    int Severity = (int)UsefulMethods.FindBetween((int)Math.Abs(fTimePassed * 10000), 200, 0, ObjectInfo2.Severities.W, 0f, false);
                    float rotationChange = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo2.RotationChanges.W, ObjectInfo2.RotationChanges.Z, false);
                    ObjectInfo2.Scale = UsefulMethods.FindBetween(BetweenTime, 100000, 0, ObjectInfo2.Scales.Z, ObjectInfo1.Scale, true);

                    for (int i = 0; i < ObjectInfo2.Rotations.Count; i++)
                    {
                        ObjectInfo2.Rotations[i] += ObjectInfo2.RotationIncrease[i] * RotationMultiplier * fTimePassed * rotationChange;
                        CheckRotation(i, 2);
                    }

                    for (int i = 0; i < ObjectInfo2.PositionOffset.Count; i++)
                    {
                        if (Severity == 0)
                            ObjectInfo2.PositionOffset[i] = Vector2.Zero;
                        else
                        {
                            Offset.X = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo2.OriginalScale * Multiplier;
                            Offset.Y = (rand.Next(0, Severity) - (Severity / 2)) * ObjectInfo2.OriginalScale * Multiplier;
                            ObjectInfo2.PositionOffset[i] = Offset;
                        }
                    }
                }
            }
        }

        public void UpdateOrbit(Vector2 orbitCenter)
        {
            if (OrbitSystem != null)
                Position = OrbitSystem.UpdatePosition(orbitCenter, fTimePassed);
            else
                Position = orbitCenter;
        }

        public void UpdateGravityOrbit(List<Vector2> Positions, List<float> Masses)
        {
            float t = fTimePassed;
            if (t > 10)
                t = 10;

            Vector2 force = Vector2.Zero;
            for (int i = 0; i < Positions.Count; i++)
            {
                float Distance = Vector2.Distance(Position, Positions[i]);
                float distanceX = Position.X - Positions[i].X;
                float distanceY = Position.Y - Positions[i].Y;

                if (Distance != 0)
                    force -= new Vector2((1f / Distance) * distanceX, (1f / Distance) * distanceY) * Masses[i];
            }
            
            Vector2 Accerleration = force / Mass;
            Velocity += Accerleration * t;

            //Velocity += force;
            Position += Velocity * t;

        }
        public void Draw(SpriteBatch spriteBatch, int cameraNumber, Vector2 Offset, float FinalAlpha)
        {
            float Zoom = CameraManager.CamerasRead[cameraNumber].Zoom;
            float ActualSize = 0;
            float OnScreenSize = 0;

            switch (Type)
            {
                case 1:
                    if (ObjectInfo1 != null)
                    {
                        if (ObjectInfo1.Textures.Count != 0)
                            ActualSize = ObjectInfo1.Textures[0].Width * ObjectInfo1.Scale;
                        OnScreenSize = ActualSize * Zoom;

                        if (CameraManager.CamerasRead[cameraNumber].IsInView(Position + Offset, (int)ActualSize, (int)ActualSize, 1f))
                            ObjectInfo1.Draw(spriteBatch, Position, Offset, FinalAlpha, 1f, OnScreenSize);
                    }
                    if (ObjectInfo2 != null)
                    {
                        if (ObjectInfo2.Textures != null)
                            if (ObjectInfo2.Textures.Count != 0)
                                ActualSize = ObjectInfo2.Textures[0].Width * ObjectInfo2.Scale;
                        OnScreenSize = ActualSize * Zoom;

                        if (CameraManager.CamerasRead[cameraNumber].IsInView(Position + Offset, (int)ActualSize, (int)ActualSize, 1f))
                            ObjectInfo2.Draw(spriteBatch, Position, Offset, FinalAlpha, 1f, OnScreenSize);
                    }

                    //if (DeathExplosion)
                    //spriteBatch.Draw(StaticTests.Explosion, Position + Offset + PositionOffset2[i], null, Tints2[i] * Alphas2[i] * Alpha2, Rotations2[i], Origin, ExplosionScale * SystemScaleMultiplier, Effects2[i], 0f);

                    break;

                case 3:
                    if (ObjectInfo1 != null)
                    {
                        if (ObjectInfo1.Textures.Count != 0)
                            ActualSize = ObjectInfo1.Textures[0].Width * ObjectInfo1.Scale;
                        OnScreenSize = ActualSize * Zoom;

                        if (CameraManager.CamerasRead[cameraNumber].IsInView(Position + Offset, (int)ActualSize, (int)ActualSize, 1f))
                        {
                            ObjectInfo1.Draw(spriteBatch, Position, Offset, FinalAlpha, 1f, OnScreenSize);

                            if (P == null)
                                P = new List<Vector2>();
                            
                            P.Add(Position);
                         
                            float d = 0;
                            float d2 = Vector2.Distance(Vector2.Zero, Velocity) / 2000f;
                            int s = CoreSize / 5;

                            if (d2 < 1f)
                                d2 = 1f;
                            for (int i = 0; i < P.Count; i++)
                            {
                                if (i != P.Count - 1)
                                    d += Vector2.Distance(P[i], P[i + 1]);
                                else
                                    d += Vector2.Distance(P[P.Count - 1], Position);

                                if (d > s * d2)
                                    P.RemoveAt(0);
                            }


                            for (int i = 1; i < P.Count; i++)
                            {
                                float size = UsefulMethods.FindBetween(i, P.Count, 1, 0.99f, 0.01f, false);
                                ObjectInfo1.Draw(spriteBatch, P[i], Offset, FinalAlpha * (size / 2), size, OnScreenSize);
                            }
                        }
                    }
                    if (ObjectInfo2 != null)
                    {
                        if (ObjectInfo2.Textures != null)
                            if (ObjectInfo2.Textures.Count != 0)
                                ActualSize = ObjectInfo2.Textures[0].Width * ObjectInfo2.Scale;
                        OnScreenSize = ActualSize * Zoom;

                        if (CameraManager.CamerasRead[cameraNumber].IsInView(Position + Offset, (int)ActualSize, (int)ActualSize, 1f))
                        {
                            ObjectInfo2.Draw(spriteBatch, Position, Offset, FinalAlpha, 1f, OnScreenSize);

                            if (P == null)
                                P = new List<Vector2>();

                            P.Add(Position);
                            while (Math.Abs(Vector2.Distance(P[0], Position)) > 25000)
                                P.RemoveAt(0);

                            for (int i = 1; i < P.Count; i++)
                                ObjectInfo2.Draw(spriteBatch, P[i], Offset, FinalAlpha, 1f / (P.Count - i), OnScreenSize);
                        }
                    }
                    break;
            }
            

            if (ActualSize != 0)
                DrawInfo(spriteBatch, Offset, OnScreenSize, (int)CameraManager.CamerasRead[cameraNumber].Location.X, (int)CameraManager.CamerasRead[cameraNumber].Location.Y, FinalAlpha);
        }

        private void DrawInfo(SpriteBatch spriteBatch, Vector2 Offset, float OnScreenSize, int ChunkX, int ChunkY, float FinalAlpha)
        {
            int ActualTexWidth = ObjectInfo1.Textures[0].Width;
            int TexWidth = 2048;

            float alpha1 = UsefulMethods.FindBetween((int)(OnScreenSize * 60), (TexWidth * 15), (TexWidth / 2) * 15, 1f, 0f, false);
            float alpha2 = UsefulMethods.FindBetween((int)(OnScreenSize * 30), (int)((TexWidth * 15) * 1.5f), (int)((TexWidth * 15) * 1f), 1f, 0f, true);
               
            if (alpha1 != 0f && alpha2 != 0f)
            {

                InfoBox.ClearList();
                InfoBox.AddItem(Class);
                //InfoBox.AddItem("Planets: " + BackgroundManager.systemManager[ChunkX][ChunkY].systems[SystemID].PlanetCount);
                //InfoBox.AddItem("Comets: " + BackgroundManager.systemManager[ChunkX][ChunkY].systems[SystemID].CometCount);

                InfoBox.AddItem("ID: " + ObjectID);
                InfoBox.AddItem("Iteration: " + CurrentIteration);

                if (CurrentIteration != 0)
                    InfoBox.AddItem("Previous ID: " + Iterations[CurrentIteration - 1]);

                InfoBox.AddItem("Surface Temperature: " + Temperature + "K");
                InfoBox.AddItem("Mass: " + Math.Round(Mass, 2));
                InfoBox.AddItem("Scale: " + ObjectInfo1.Scale);
                InfoBox.AddItem("Total Age: " + TotalAge);
                InfoBox.AddItem("Age: " + Age);
                InfoBox.AddItem("Time: " + (LifeSpan - Age));
                
                float scale = ObjectInfo1.Scale * 3;

                if (ActualTexWidth != TexWidth)
                    scale /= 2;
                
                if (alpha1 > 0.05f && alpha2 == 1f)
                    InfoBox.Draw(spriteBatch, Position + Offset + (new Vector2(ActualTexWidth / 2, -(ActualTexWidth / 2)) * ObjectInfo1.Scale), scale, alpha1 * FinalAlpha);
                else if (alpha2 < 1f)
                    InfoBox.Draw(spriteBatch, Position + Offset + (new Vector2(ActualTexWidth / 2, -(ActualTexWidth / 2)) * ObjectInfo1.Scale), scale, alpha2 * FinalAlpha);
            }
        }
    }
}
