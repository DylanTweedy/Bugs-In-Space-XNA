using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace BackgroundTest

{
    static class GalaxyObjectData2
    {
        public class SystemCoreData
        {
            public string Name;
            public string Description;
            public List<Texture2D> BodyTextures;
            public List<Texture2D> StarTextures;
            public List<Texture2D> CoronaTextures;
            public List<Texture2D> GlowTextures;
            public int Type;
            public int TempMin;
            public int TempMax;
            public float ScaleMin;
            public float ScaleMax;
            public int LifespanMin;
            public int LifespanMax;
            public int Probability;
            public float Mass;
            public byte ID;

            public void AddCore(string name, int type, string description)
            {
                Name = name;
                Type = type;
                Description = description;

                BodyTextures = new List<Texture2D>();
                StarTextures = new List<Texture2D>();
                CoronaTextures = new List<Texture2D>();
                GlowTextures = new List<Texture2D>();
            }

            public void AddData(int tempMin, int tempMax, float scaleMin, float scaleMax, int lifespanMin, int lifespanMax, int probability, float mass, byte id)
            {
                TempMin = tempMin;
                TempMax = tempMax;
                ScaleMin = scaleMin;
                ScaleMax = scaleMax;
                LifespanMin = lifespanMin;
                LifespanMax = lifespanMax;
                Probability = probability;
                Mass = mass;
                ID = id;
            }
        }

        public class OrbitalData
        {
            public string Name;
            public string Description;
            public List<Texture2D> BodyTextures;
            public List<Texture2D> OverlayTextures;
            public List<Texture2D> GlowTextures;
            public List<Texture2D> CloudTextures;
            public List<Texture2D> RingTextures;
            public List<Texture2D> ShadowTextures;
            public int Type;
            public float ScaleMin;
            public float ScaleMax;
            public int Probability;
            public float Mass;
            public byte ID;

            public void AddOrbital(string name, int type, string description)
            {
                Name = name;
                Type = type;
                Description = description;

                BodyTextures = new List<Texture2D>();
                GlowTextures = new List<Texture2D>();
                OverlayTextures = new List<Texture2D>();
                CloudTextures = new List<Texture2D>();
                RingTextures = new List<Texture2D>();
                ShadowTextures = new List<Texture2D>();
            }

            public void AddData(float scaleMin, float scaleMax, int probability, float mass, byte id)
            {
                ScaleMin = scaleMin;
                ScaleMax = scaleMax;
                Probability = probability;
                Mass = mass;
                ID = id;
            }
        }

        public class ObjectData
        {
            public string Name;
            public string Description;
            public List<Texture2D> BodyTextures;
            public List<Texture2D> GlowTextures;
            public float ScaleMin;
            public float ScaleMax;
            public int Probability;
            public float Mass;
            public byte ID;

            public void AddObject(string name, string description)
            {
                Name = name;
                Description = description;

                BodyTextures = new List<Texture2D>();
                GlowTextures = new List<Texture2D>();
            }

            public void AddData(float scaleMin, float scaleMax, int probability, float mass, byte id)
            {
                ScaleMin = scaleMin;
                ScaleMax = scaleMax;
                Probability = probability;
                Mass = mass;
                ID = id;
            }
        }

        class TextureHolder
        {
            public List<Texture2D> CoreBodyTextures;
            public List<Texture2D> CoreStarTextures;
            public List<Texture2D> CoreCoronaTextures;
            public List<Texture2D> CoreGlowTextures;
            public List<Texture2D> OrbitalBodyTextures;
            public List<Texture2D> OrbitalOverlayTextures;
            public List<Texture2D> OrbitalCloudTextures;
            public List<Texture2D> OrbitalGlowTextures;
            public List<Texture2D> OrbitalShadows;
            public List<Texture2D> OrbitalRings;
            public List<Texture2D> ObjectBodyTextures;
            public List<Texture2D> ObjectGlowTextures;

            public void Initialize()
            {
                CoreBodyTextures = new List<Texture2D>();
                CoreStarTextures = new List<Texture2D>();
                CoreCoronaTextures = new List<Texture2D>();
                CoreGlowTextures = new List<Texture2D>();
                OrbitalBodyTextures = new List<Texture2D>();
                OrbitalOverlayTextures = new List<Texture2D>();
                OrbitalCloudTextures = new List<Texture2D>();
                OrbitalGlowTextures = new List<Texture2D>();
                OrbitalShadows = new List<Texture2D>();
                OrbitalRings = new List<Texture2D>();
                ObjectBodyTextures = new List<Texture2D>();
                ObjectGlowTextures = new List<Texture2D>();
            }
        }

        static public List<SystemCoreData> SystemCores;
        static public List<OrbitalData> SystemOrbitals;
        static public List<ObjectData> SystemObjects;
        static public SystemCoreData SelectedCore;
        static public OrbitalData SelectedOrbital;
        static public ObjectData SelectedObject;
        static TextureHolder NoTexture;
        static ContentManager content;

        static bool LoadTextures;
        static int TextureCounter;
        static Random rand;

        static public void Initialize(ContentManager Content)
        {
            SystemCores = new List<SystemCoreData>();
            SystemOrbitals = new List<OrbitalData>();
            SystemObjects = new List<ObjectData>();
            SelectedCore = new SystemCoreData();
            SelectedOrbital = new OrbitalData();
            NoTexture = new TextureHolder();
            NoTexture.Initialize();
            content = Content;
            rand = new Random();

            NoTextures();
            AddStars();
            AddOrbitals();
            AddObjects();
        }

        static private void AddStars()
        {
            AddStar("Galactic Core", "", 0, 0, 0f, 0f, 0, 0, 0, 0f);

            AddStar("Class O Star", "", 33000, 50000, 20f, 30f, 60, 120, 12, 90f);
            AddStar("Class B Star", "", 10000, 33000, 15f, 20f, 120, 200, 59, 16f);
            AddStar("Class A Star", "", 7500, 10000, 10f, 15f, 200, 300, 68, 2.7f);
            AddStar("Class F Star", "", 6000, 7500, 9f, 10f, 300, 400, 70, 1.8f);
            AddStar("Class G Star", "", 5200, 6000, 7.5f, 9f, 400, 500, 75, 1.2f);
            AddStar("Class K Star", "", 3700, 5200, 5f, 7.5f, 750, 1000, 81, 0.87f);
            AddStar("Class M Star", "", 2000, 3700, 4f, 5f, 1000, 2500, 98, 0.74f);
            AddStar("Class L Star", "", 1300, 2000, 3.75f, 4f, 2500, 5000, 25, 0.12f);
            AddStar("Class T Star", "", 700, 1300, 3.5f, 3.75f, 10000, 25000, 20, 0.09f);
            AddStar("Class Y Star", "", 500, 700, 3.25f, 3.5f, 25000, 40000, 15, 0.03f);
            AddStar("White Dwarf", "", 33000, 50000, 1f, 3.25f, 40000, 45000, 45, 1.09f);
            AddStar("Wolf-Rayet Star", "", 30000, 200000, 10f, 25f, 240, 300, 27, 23f);
            AddStar("Glorious Star", "", 2500, 20000, 0.5f, 6.5f, 15, 60, 25, 14.5f);
            AddStar("Pulsar", "", 1000000, 100000000, 0.5f, 1f, 180, 240, 13, 4f);
            AddStar("Neutron Star", "", 1000000, 100000000, 0.5f, 1f, 120, 180, 46, 7f);
            AddStar("Quark Star", "", 1000000, 100000000, 0.25f, 0.5f, 90, 120, 10, 19f);
            AddStar("Boson Star", "", 1000000, 100000000, 0.1f, 0.25f, 60, 90, 4, 23f);
            AddStar("Quantum Star", "", 1000, 15000, 1f, 8f, 15, 10080, 40, 5f);
            AddStar("Translocation Star", "", 1000, 15000, 1f, 8f, 15, 10080, 7, 0.07f);
            AddStar("Ancient Core", "", 0, 1000, 0.5f, 20f, 15, 40320, 25, 50f);
            AddStar("Ghost Star", "", 0, 25000, 1f, 1.5f, 15, 20160, 14, 0.6f);
            AddStar("Rainbow Star", "", 2500, 10000, 4f, 9f, 500, 1000, 16, 1.13f);
        }

        static private void AddOrbitals()
        {
            AddOrbital("Tiny", "", 1f, 2.5f, 9, 0f);
            AddOrbital("Normal", "", 2.5f, 3.75f, 6, 0f);
            AddOrbital("Huge", "", 3.75f, 4.5f, 3, 0f);
            AddOrbital("Gas Giant", "", 4.5f, 7f, 8, 0f);
        }

        static private void AddObjects()
        {
            AddObject("Generic Comet", "", 0.25f, 1.75f, 8, 0f);
        }

        static private void AddStar(string Name, string Description, int tempMin, int tempMax, float scaleMin, float scaleMax, int lifespanMin, int lifespanMax, int probability, float mass)
        {
            SystemCores.Add(new SystemCoreData());
            if (Name == "Galactic Core")
                SystemCores[SystemCores.Count - 1].AddCore(Name, 0, Description);
            else
                SystemCores[SystemCores.Count - 1].AddCore(Name, 1, Description);

            SystemCores[SystemCores.Count - 1].AddData(tempMin, tempMax, scaleMin, scaleMax, lifespanMin, lifespanMax, probability, mass, (byte)(SystemCores.Count - 1));

            AddTexture("SystemCoreObjects", "Stars", "Body", Name);
            AddTexture("SystemCoreObjects", "Stars", "Corona", Name);
            AddTexture("SystemCoreObjects", "Stars", "Glow", Name);
            AddTexture("SystemCoreObjects", "Stars", "Small", Name);
        }

        static private void AddOrbital(string Name, string Description, float scaleMin, float scaleMax, int probability, float mass)
        {
            SystemOrbitals.Add(new OrbitalData());
            SystemOrbitals[SystemOrbitals.Count - 1].AddOrbital(Name, 2, Description);

            SystemOrbitals[SystemOrbitals.Count - 1].AddData(scaleMin, scaleMax, probability, mass, (byte)(SystemCores.Count - 1));

            AddTexture("SystemOrbital", "Planets", "Body", Name);
            AddTexture("SystemOrbital", "Planets", "Overlay", Name);
            AddTexture("SystemOrbital", "Planets", "Clouds", Name);
            AddTexture("SystemOrbital", "Planets", "Glow", Name);
            AddTexture("SystemOrbital", "Planets", "Shadows", Name);
            AddTexture("SystemOrbital", "Planets", "Rings", Name);
        }

        static private void AddObject(string Name, string Description, float scaleMin, float scaleMax, int probability, float mass)
        {
            SystemObjects.Add(new ObjectData());
            SystemObjects[SystemObjects.Count - 1].AddObject(Name, Description);

            SystemObjects[SystemObjects.Count - 1].AddData(scaleMin, scaleMax, probability, mass, (byte)(SystemCores.Count - 1));

            AddTexture("SystemObject", "Comets", "Body", Name);
            AddTexture("SystemObject", "Comets", "Glow", Name);
        }
        
        static private void NoTextures()
        {
            int type = 0;
            LoadTextures = true;
            TextureCounter = 0;

            while (LoadTextures)
            {
                string number = "" + TextureCounter;

                if (TextureCounter < 100)
                    number = 0 + number;

                if (TextureCounter < 10)
                    number = 0 + number;

                try
                {
                    switch (type)
                    {
                        case 0:
                            NoTexture.CoreBodyTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemCoreObjects//Stars//Body//" + number));
                            break;
                        case 1:
                            NoTexture.CoreCoronaTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemCoreObjects//Stars//Corona//" + number));
                            break;
                        case 2:
                            NoTexture.CoreGlowTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemCoreObjects//Stars//Glow//" + number));
                            break;
                        case 3:
                            NoTexture.CoreStarTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemCoreObjects//Stars//Small//" + number));
                            break;
                        case 4:
                            NoTexture.OrbitalBodyTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemOrbital//Planets//Body//" + number));
                            break;
                        case 5:
                            NoTexture.OrbitalCloudTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemOrbital//Planets//Clouds//" + number));
                            break;
                        case 6:
                            NoTexture.OrbitalGlowTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemOrbital//Planets//Glow//" + number));
                            break;
                        case 7:
                            NoTexture.OrbitalShadows.Add(content.Load<Texture2D>("Images//Galaxy//SystemOrbital//Planets//Shadows//" + number));
                            break;
                        case 8:
                            NoTexture.OrbitalOverlayTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemOrbital//Planets//Overlay//" + number));
                            break;
                        case 9:
                            NoTexture.OrbitalRings.Add(content.Load<Texture2D>("Images//Galaxy//SystemOrbital//Planets//Rings//" + number));
                            break;
                        case 10:
                            NoTexture.ObjectBodyTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemObject//Comets//Body//" + number));
                            break;
                        case 11:
                            NoTexture.ObjectGlowTextures.Add(content.Load<Texture2D>("Images//Galaxy//SystemObject//Comets//Glow//" + number));
                            break;
                        default:
                            LoadTextures = false;
                            break;
                    }
                }
                catch (ContentLoadException r)
                {
                    type++;
                    TextureCounter = -1;
                }

                TextureCounter++;
            }
        }

        static private void AddTexture(string PrimaryLocation, string ObjectType, string SubType, string Name)
        {
            TextureCounter = 0;
            LoadTextures = true;
            while (LoadTextures)
            {
                string number = "" + TextureCounter;

                if (TextureCounter < 100)
                    number = 0 + number;

                if (TextureCounter < 10)
                    number = 0 + number;

                AddTextures(Name + number, PrimaryLocation + "//", ObjectType + "//", SubType + "//");
            }
        }

        static void AddTextures(string texture, string PrimaryLocation, string ObjectType, string SubType)
        {
            //SystemCores[SystemCores.Count - 1].BodyTextures

            try
            {
                if (SubType == "Body//" && ObjectType == "Stars//")
                    SystemCores[SystemCores.Count - 1].BodyTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Corona//" && ObjectType == "Stars//")
                    SystemCores[SystemCores.Count - 1].CoronaTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Glow//" && ObjectType == "Stars//")
                    SystemCores[SystemCores.Count - 1].GlowTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Small//" && ObjectType == "Stars//")
                    SystemCores[SystemCores.Count - 1].StarTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Body//" && ObjectType == "Planets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].BodyTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Clouds//" && ObjectType == "Planets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].CloudTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Glow//" && ObjectType == "Planets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].GlowTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Shadows//" && ObjectType == "Planets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].ShadowTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Overlay//" && ObjectType == "Planets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].OverlayTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Rings//" && ObjectType == "Planets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].RingTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Body//" && ObjectType == "Comets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].RingTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
                else if (SubType == "Glow//" && ObjectType == "Comets//")
                    SystemOrbitals[SystemOrbitals.Count - 1].RingTextures.Add(content.Load<Texture2D>("Images//Galaxy//" + PrimaryLocation + ObjectType + "0//" + SubType + texture));
            }
            catch (ContentLoadException e)
            {
                if (TextureCounter == 0)
                {
                    if (SubType == "Body//" && ObjectType == "Stars//")
                        SystemCores[SystemCores.Count - 1].BodyTextures = NoTexture.CoreBodyTextures;
                    else if (SubType == "Corona//" && ObjectType == "Stars//")
                        SystemCores[SystemCores.Count - 1].CoronaTextures = NoTexture.CoreCoronaTextures;
                    else if (SubType == "Glow//" && ObjectType == "Stars//")
                        SystemCores[SystemCores.Count - 1].GlowTextures = NoTexture.CoreGlowTextures;
                    else if (SubType == "Small//" && ObjectType == "Stars//")
                        SystemCores[SystemCores.Count - 1].StarTextures = NoTexture.CoreStarTextures;
                    else if (SubType == "Body//" && ObjectType == "Planets//")
                        SystemOrbitals[SystemOrbitals.Count - 1].BodyTextures = NoTexture.OrbitalBodyTextures;
                    else if (SubType == "Clouds//" && ObjectType == "Planets//")
                        SystemOrbitals[SystemOrbitals.Count - 1].CloudTextures = NoTexture.OrbitalCloudTextures;
                    else if (SubType == "Glow//" && ObjectType == "Planets//")
                        SystemOrbitals[SystemOrbitals.Count - 1].GlowTextures = NoTexture.OrbitalGlowTextures;
                    else if (SubType == "Shadows//" && ObjectType == "Planets//")
                        SystemOrbitals[SystemOrbitals.Count - 1].ShadowTextures = NoTexture.OrbitalShadows;
                    else if (SubType == "Overlay//" && ObjectType == "Planets//")
                        SystemOrbitals[SystemOrbitals.Count - 1].OverlayTextures = NoTexture.OrbitalOverlayTextures;
                    else if (SubType == "Rings//" && ObjectType == "Planets//")
                        SystemOrbitals[SystemOrbitals.Count - 1].RingTextures = NoTexture.OrbitalRings;
                    else if (SubType == "Body//" && ObjectType == "Comets//")
                        SystemObjects[SystemObjects.Count - 1].BodyTextures = NoTexture.ObjectBodyTextures;
                    else if (SubType == "Glow//" && ObjectType == "Comets//")
                        SystemObjects[SystemObjects.Count - 1].GlowTextures = NoTexture.ObjectGlowTextures;
                }
                LoadTextures = false;
            }

            TextureCounter++;
        }

        static public void SelectNewCore()
        {
            int probability = 0;

            for (int i = 0; i < SystemCores.Count; i++)
            {
                probability += SystemCores[i].Probability;
            }

            int selection = rand.Next(0, probability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < SystemCores.Count; i++)
            {
                counter += SystemCores[i].Probability;

                if (selection >= previousCounter && selection <= counter)                
                    SelectedCore = SystemCores[i];
                
                previousCounter = counter;
            }
        }

        static public void SelectNewOrbital()
        {
            int probability = 0;

            for (int i = 0; i < SystemOrbitals.Count; i++)
            {
                probability += SystemOrbitals[i].Probability;
            }

            int selection = rand.Next(0, probability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < SystemOrbitals.Count; i++)
            {
                counter += SystemOrbitals[i].Probability;

                if (selection >= previousCounter && selection <= counter)
                    SelectedOrbital = SystemOrbitals[i];

                previousCounter = counter;
            }
        }

        static public void SelectNewObject()
        {
            int probability = 0;

            for (int i = 0; i < SystemObjects.Count; i++)
            {
                probability += SystemObjects[i].Probability;
            }

            int selection = rand.Next(0, probability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < SystemObjects.Count; i++)
            {
                counter += SystemObjects[i].Probability;

                if (selection >= previousCounter && selection <= counter)
                    SelectedObject = SystemObjects[i];

                previousCounter = counter;
            }
        }
    }
}
