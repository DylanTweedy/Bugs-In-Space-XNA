using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace BackgroundTest

{
    static class GalaxyObjectData
    {
        public class ObjectHolder
        {
            public string Name;
            public string Description;
            public List<Texture2D>[] Layers;
            public List<int> LayerCount;
            public List<Texture2D>[] SmallLayers;
            public List<byte> Evolutions;
            public List<Vector3> EvolutionActions;
            public int Type;
            public int Temp;
            public float Scale;
            public float Lifespan;
            public int Probability;
            public float Mass;
            public byte ID;

            public void AddObject(string name, int type, string description)
            {
                Name = name;
                Type = type;
                Description = description;
            }

            public void AddData(int temp, float scale, float lifespan, int probability, float mass, byte id)
            {
                Temp = temp;
                Scale = scale;
                Lifespan = lifespan;
                Probability = probability;
                Mass = mass;
                ID = id;
            }            
        }
        
        public class TextureHolder
        {
            public string Type;
            public List<string> LayerNames;
            public List<Texture2D>[] Layers;
            public List<int> LayerCount;
            public List<string> SmallLayerNames;
            public List<Texture2D>[] SmallLayers;
        }

        static public List<ObjectHolder> GalaxyObjects;
        static public ObjectHolder SelectedObject;
        static List<TextureHolder> NoTexture;
        static ContentManager content;

        static bool LoadTextures;
        static int TextureCounter;
        static Random rand;

        static public void Initialize(ContentManager Content)
        {
            GalaxyObjects = new List<ObjectHolder>();
            SelectedObject = new ObjectHolder();
            NoTexture = new List<TextureHolder>();
            content = Content;
            rand = new Random();

            NoTextures();
            AddObjects();
            AddEvolutions();
        }

        static private void AddObjects()
        {
            AddObject("Galactic Core", "", 0, 0f, 0, 0, 0f, 0, 0);

            AddObject("Class O Star", "", 50000, 1f, 10, 12, 90f, 1, 1);
            AddObject("Class B Star", "", 33000, 0.9f, 10, 59, 16f, 1, 2);
            AddObject("Class A Star", "", 10000, 0.8f, 10, 68, 2.7f, 1, 3);
            AddObject("Class F Star", "", 7500, 0.7f, 10, 70, 1.8f, 1, 4);
            AddObject("Class G Star", "", 6000, 0.6f, 10, 75, 1.2f, 1, 5);
            AddObject("Class K Star", "", 5200, 0.5f, 10, 81, 0.87f, 1, 6);
            AddObject("Class M Star", "", 3700, 0.4f, 10, 98, 0.74f, 1, 7);
            AddObject("Class L Star", "", 2000, 0.3f, 10, 25, 0.12f, 1, 8);
            AddObject("Class T Star", "", 1300, 0.2f, 10, 20, 0.09f, 1, 9);
            AddObject("Class Y Star", "", 700, 0.15f, 10, 15, 0.03f, 1, 10);
            AddObject("White Dwarf", "", 50000, 0.1f, 10, 45, 1.09f, 1, 11);
            AddObject("Wolf-Rayet Star", "", 200000, 0.9f, 10, 27, 23f, 1, 12);
            AddObject("Glorious Star", "", 20000, 0.7f, 10, 25, 14.5f, 1, 13);
            AddObject("Pulsar", "", 100000000, 0.09f, 10, 13, 4f, 1, 14);
            AddObject("Neutron Star", "", 100000000, 0.07f, 10, 46, 7f, 1, 15);
            AddObject("Quark Star", "", 100000000, 0.05f, 10, 10, 19f, 1, 16);
            AddObject("Boson Star", "", 100000000, 0.025f, 10, 4, 23f, 1, 17);
            AddObject("Quantum Star", "", 15000, 0.5f, 10, 40, 5f, 1, 18);
            AddObject("Translocation Star", "", 15000, 0.6f, 10, 7, 0.07f, 1, 19);
            AddObject("Ancient Core", "", 1000, 0.43f, 10, 25, 50f, 1, 20);
            AddObject("Ghost Star", "", 25000, 0.6f, 10, 14, 0.6f, 1, 21);
            AddObject("Rainbow Star", "", 10000, 0.55f, 10, 16, 1.13f, 1, 22);

            AddObject("Black Hole", "", 0, 0.12f, 10, 10, 0f, 1, 23);
            AddObject("Protostar", "", 0, 2.5f, 10, 10, 0f, 1, 24);
            AddObject("Glory Hole", "", 0, 0.17f, 10, 10, 0f, 1, 25);
            AddObject("Elemental Core", "", 0, 0.1f, 10, 10, 0f, 1, 26);
            AddObject("White Hole", "", 0, 0.16f, 10, 10, 0f, 1, 27);
            AddObject("Sub-Space Rupture", "", 0, 0.75f, 10, 10, 0f, 1, 28);
            AddObject("Organic Crux", "", 0, 0.44f, 10, 10, 0f, 1, 29);
            AddObject("Corpus", "", 0, 0.41f, 10, 10, 0f, 1, 30);
            AddObject("Quadrate Star", "", 0, 0.3f, 10, 10, 0f, 1, 31);
            AddObject("Anti-Matter Star", "", 0, 0.5f, 10, 10, 0f, 1, 32);
            AddObject("Wormhole", "", 0, 0.68f, 10, 10, 0f, 1, 33);

            AddObject("Ring", "", 0, 0.54f, 10, 10, 0f, 1, 34);
            AddObject("Magic", "", 0, 0.53f, 10, 10, 0f, 1, 35);
            AddObject("Dark", "", 0, 0.51f, 10, 10, 0f, 1, 36);
            AddObject("Volatile", "", 0, 0.38f, 10, 10, 0f, 1, 37);
            AddObject("Poison", "", 0, 0.42f, 10, 10, 0f, 1, 38);
            AddObject("Cold", "", 0, 0.69f, 10, 10, 0f, 1, 39);
            AddObject("Thunder", "", 0, 0.61f, 10, 10, 0f, 1, 40);

            AddObject("Planetary Nebula", "", 0, 3f, 10, 10, 0f, 1, 41);
            AddObject("Supernova Nebula", "", 0, 3.5f, 10, 10, 0f, 1, 42);

            AddObject("Phoenix Gate", "", 0, 1f, 10, 10, 0f, 1, 43);

            AddObject("Tiny", "", 0, 2.5f, 0, 9, 0f, 2, 44);
            AddObject("Normal", "", 0, 3.75f, 0, 6, 0f, 2, 45);
            AddObject("Huge", "", 0, 4.5f, 0, 3, 0f, 2, 46);

            AddObject("Gas Giant", "", 0, 7f, 0, 8, 0f, 2, 47);

            AddObject("Generic Comet", "", 0, 0.05f, 0, 8, 0f, 3, 48);
        }
        
        static private void AddObject(string name, string description, int temp, float scale, float lifespan, int probability, float mass, int type, byte id)
        {
            //Set LifeSpans
            lifespan = rand.Next(100, 300);
            //lifespan *= 10;

            GalaxyObjects.Add(new ObjectHolder());
            GalaxyObjects[GalaxyObjects.Count - 1].AddObject(name, type, description);
            GalaxyObjects[GalaxyObjects.Count - 1].AddData(temp, scale, lifespan, probability, mass, id);

            AddTextures(name, type);
        }
        
        static private void NoTextures()
        {
            AddType("Galactic Core");
            AddType("Star");
            AddType("Planet");
            AddType("Comet");
            //AddType("Asteroid");
            //AddType("Meteoroid");
        }

        static private void AddType(string type)
        {
            NoTexture.Add(new TextureHolder());
            int p = NoTexture.Count - 1;

            NoTexture[p].Type = type;

            List<string> Layers = new List<string>();
            List<int> LayerCount = new List<int>();
            List<string> SmallLayers = new List<string>();

            switch (type)
            {
                case "Star":
                    Layers.Add("Glow");
                    LayerCount.Add(1);
                    Layers.Add("Body");
                    LayerCount.Add(2);
                    Layers.Add("Corona");
                    LayerCount.Add(2);

                    SmallLayers.Add("Small");
                    break;

                case "Planet":
                    Layers.Add("Body");
                    LayerCount.Add(1);
                    Layers.Add("Clouds");
                    LayerCount.Add(2);
                    Layers.Add("Glow");
                    LayerCount.Add(1);
                    Layers.Add("Overlay");
                    LayerCount.Add(1);
                    Layers.Add("Rings");
                    LayerCount.Add(1);
                    Layers.Add("Shadows");
                    LayerCount.Add(1);
                    break;

                case "Comet":
                    Layers.Add("Body");
                    LayerCount.Add(1);
                    Layers.Add("Glow");
                    LayerCount.Add(1);

                    SmallLayers.Add("Small");
                    break;

                case "Galactic Core":
                    Layers.Add("Body");
                    LayerCount.Add(1);
                    break;
            }

            NoTexture[p].Layers = new List<Texture2D>[Layers.Count];
            NoTexture[p].LayerCount = LayerCount;

            for (int i = 0; i < Layers.Count; i++)
            {
                NoTexture[p].Layers[i] = new List<Texture2D>();
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
                        NoTexture[p].Layers[i].Add(content.Load<Texture2D>("Images//Galaxy//" + type + "//NoTexture//" + Layers[i] + "//" + number));
                    }
                    catch (ContentLoadException r)
                    {
                        LoadTextures = false;
                    }

                    TextureCounter++;
                }
            }

            NoTexture[p].LayerNames = Layers;

            NoTexture[p].SmallLayers = new List<Texture2D>[SmallLayers.Count];

            for (int i = 0; i < SmallLayers.Count; i++)
            {
                NoTexture[p].SmallLayers[i] = new List<Texture2D>();
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
                        NoTexture[p].SmallLayers[i].Add(content.Load<Texture2D>("Images//Galaxy//" + type + "//NoTexture//" + SmallLayers[i] + "//" + number));
                    }
                    catch (ContentLoadException r)
                    {
                        LoadTextures = false;
                    }

                    TextureCounter++;
                }
            }

            NoTexture[p].SmallLayerNames = SmallLayers;
        }
        
        static private void AddTextures(string name, int type)
        {
            int c = GalaxyObjects.Count - 1;
            int p = NoTexture[type].Layers.Length;

            GalaxyObjects[c].Layers = new List<Texture2D>[p];
            GalaxyObjects[c].LayerCount = NoTexture[type].LayerCount;

            string Type = NoTexture[type].Type;

            for (int i = 0; i < p; i++)
            {
                GalaxyObjects[c].Layers[i] = new List<Texture2D>();

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
                        GalaxyObjects[c].Layers[i].Add(content.Load<Texture2D>("Images//Galaxy//" + Type + "//" + name + "//" + NoTexture[type].LayerNames[i] + "//" + number));
                    }
                    catch (ContentLoadException r)
                    {
                        if (TextureCounter == 0)
                            GalaxyObjects[c].Layers[i] = NoTexture[type].Layers[i];

                        LoadTextures = false;
                    }

                    TextureCounter++;
                }
            }

            p = NoTexture[type].SmallLayers.Length;

            GalaxyObjects[c].SmallLayers = new List<Texture2D>[p];
            
            for (int i = 0; i < p; i++)
            {
                GalaxyObjects[c].SmallLayers[i] = new List<Texture2D>();

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
                        GalaxyObjects[c].SmallLayers[i].Add(content.Load<Texture2D>("Images//Galaxy//" + Type + "//" + name + "//" + NoTexture[type].SmallLayerNames[i] + "//" + number));
                    }
                    catch (ContentLoadException r)
                    {
                        if (TextureCounter == 0)
                            GalaxyObjects[c].SmallLayers[i] = NoTexture[type].SmallLayers[i];

                        LoadTextures = false;
                    }

                    TextureCounter++;
                }
            }   
        }
        
        static private void AddEvolutions()
        {
            for (int o = 0; o < GalaxyObjects.Count; o++)
            {
                List<byte> Evolutions = new List<byte>();
                List<Vector3> EvolutionActions = new List<Vector3>();

                #region Set Evolutions

                switch (GalaxyObjects[o].ID)
                {
                    case 23:
                        Evolutions.Add(24);
                        EvolutionActions.Add(new Vector3(0, 0, 0));
                        ////////////////////////////////
                        //Unique Stars
                        ////////////////////////////////
                        break;

                    case 24:
                        Evolutions.Add(1);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(2);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(3);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(4);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(5);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(6);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(7);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(8);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(9);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(10);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 41:
                        Evolutions.Add(24);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 42:
                        Evolutions.Add(24);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 1:
                        Evolutions.Add(17);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(12);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(42);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 2:
                        Evolutions.Add(16);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(18);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        Evolutions.Add(42);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 3:
                        Evolutions.Add(14);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(19);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(42);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 4:
                        Evolutions.Add(15);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(21);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(42);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 5:
                        Evolutions.Add(13);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(11);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(41);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 6:
                        Evolutions.Add(11);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(22);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        Evolutions.Add(41);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 7:
                        Evolutions.Add(11);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(31);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        Evolutions.Add(41);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 8:
                        Evolutions.Add(36);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        Evolutions.Add(35);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 9:
                        Evolutions.Add(29);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(36);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 10:
                        Evolutions.Add(36);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        Evolutions.Add(32);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 11:
                        Evolutions.Add(27);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(39);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        Evolutions.Add(26);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        break;

                    case 12:
                        Evolutions.Add(23);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 13:
                        Evolutions.Add(29);
                        EvolutionActions.Add(new Vector3(1, 1, 1));
                        Evolutions.Add(25);
                        EvolutionActions.Add(new Vector3(1, 1, 1));
                        Evolutions.Add(35);
                        EvolutionActions.Add(new Vector3(1, 1, 1));
                        break;

                    case 14:
                        Evolutions.Add(23);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(15);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        break;

                    case 15:
                        Evolutions.Add(23);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(16);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        break;

                    case 16:
                        Evolutions.Add(23);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(17);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        break;

                    case 17:
                        Evolutions.Add(23);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 18:
                        Evolutions.Add(17);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        break;

                    case 19:
                        Evolutions.Add(15);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 20:
                        Evolutions.Add(11);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        break;

                    case 21:
                        Evolutions.Add(14);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 22:
                        Evolutions.Add(34);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(39);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        Evolutions.Add(40);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        Evolutions.Add(31);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(38);
                        EvolutionActions.Add(new Vector3(1, 1, 0));
                        Evolutions.Add(36);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        break;

                    case 25:
                        Evolutions.Add(21);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(19);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(18);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        break;

                    case 26:
                        Evolutions.Add(23);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        break;

                    case 27:
                        Evolutions.Add(20);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 28:
                        Evolutions.Add(28);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 29:
                        Evolutions.Add(30);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        Evolutions.Add(21);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 30:
                        Evolutions.Add(13);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        Evolutions.Add(27);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        break;

                    case 31:
                        Evolutions.Add(38);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        Evolutions.Add(35);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 32:
                        Evolutions.Add(33);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        Evolutions.Add(37);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        break;

                    case 33:
                        Evolutions.Add(24);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        break;

                    case 34:
                        Evolutions.Add(33);
                        EvolutionActions.Add(new Vector3(0, 0, 1));
                        Evolutions.Add(37);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        Evolutions.Add(11);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        Evolutions.Add(28);
                        EvolutionActions.Add(new Vector3(1, 0, 0));
                        break;

                    case 35:
                        Evolutions.Add(20);
                        EvolutionActions.Add(new Vector3(1, 1, 1));
                        break;

                    case 36:
                        Evolutions.Add(38);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        Evolutions.Add(33);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        break;

                    case 38:
                        Evolutions.Add(29);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        break;

                    case 39:
                        Evolutions.Add(32);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        Evolutions.Add(36);
                        EvolutionActions.Add(new Vector3(0, 1, 0));
                        break;

                    case 40:
                        Evolutions.Add(11);
                        EvolutionActions.Add(new Vector3(1, 0, 1));
                        Evolutions.Add(37);
                        EvolutionActions.Add(new Vector3(0, 1, 1));
                        break;

                    default:
                        for (int i = 0; i < GalaxyObjects.Count; i++)
                            if (GalaxyObjects[o].Type == GalaxyObjects[i].Type)
                            {
                                Evolutions.Add(GalaxyObjects[i].ID);
                                EvolutionActions.Add(new Vector3(1, 0, 0));
                            }
                        break;
                }

                #endregion
                
                GalaxyObjects[o].Evolutions = Evolutions;
                GalaxyObjects[o].EvolutionActions = EvolutionActions;
            }
        }

        static public void CreateNew(byte type)
        {
            byte id = 0;

            switch (type)
            {
                case 1:
                    id = 24;
                    break;

                case 2:
                    break;

                case 3:
                    id = 48;
                    break;
            }

            for (int i = 0; i < GalaxyObjects.Count; i++)
                if (GalaxyObjects[i].ID == id)
                {
                    SelectedObject = GalaxyObjects[i];
                    break;
                }
        }


        static public Vector3 SelectNext(int ID, byte type)
        {
                    int probability = 0;

                    for (int i = 0; i < GalaxyObjects.Count; i++)
                        if (GalaxyObjects[ID].Type == GalaxyObjects[i].Type)
                            for (int o = 0; o < GalaxyObjects[ID].Evolutions.Count; o++)
                                if (GalaxyObjects[i].ID == GalaxyObjects[ID].Evolutions[o])
                                    probability += GalaxyObjects[i].Probability;

                    int selection = rand.Next(0, probability);

                    int counter = 0;
                    int previousCounter = 0;

                    for (int i = 0; i < GalaxyObjects.Count; i++)
                        if (GalaxyObjects[ID].Type == GalaxyObjects[i].Type)
                            for (int o = 0; o < GalaxyObjects[ID].Evolutions.Count; o++)
                                if (GalaxyObjects[i].ID == GalaxyObjects[ID].Evolutions[o])
                                {
                                    counter += GalaxyObjects[i].Probability;

                                    if (selection >= previousCounter && selection <= counter)
                                    {
                                        SelectedObject = GalaxyObjects[i];
                                        return GalaxyObjects[ID].EvolutionActions[o];
                                    }

                                    previousCounter = counter;
                                }

            return Vector3.Zero;
        }

        static public Vector3 SelectNextPlanet(int ID)
        {
            return Vector3.Zero;
        }

        static public void SelectNewObject(int type)
        {
            int probability = 0;

            for (int i = 0; i < GalaxyObjects.Count; i++)
                if (type == GalaxyObjects[i].Type)
                    probability += GalaxyObjects[i].Probability;
            
            int selection = rand.Next(0, probability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < GalaxyObjects.Count; i++)
                if (type == GalaxyObjects[i].Type)
                {
                    counter += GalaxyObjects[i].Probability;

                    if (selection >= previousCounter && selection <= counter)
                        SelectedObject = GalaxyObjects[i];

                    previousCounter = counter;
                }
        }

        static public void SelectObjectID(byte ID)
        {
            for (int i = 0; i < GalaxyObjects.Count; i++)
                if (ID == GalaxyObjects[i].ID)
                {
                    SelectedObject = GalaxyObjects[i];
                    break;
                }
        }
    }
}
