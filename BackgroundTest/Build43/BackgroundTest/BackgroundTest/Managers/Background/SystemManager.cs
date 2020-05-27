using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class SystemManager
    {
        List<System> systems;
        
        //List<Texture2D> textureList;
        //List<Texture2D> smallTextureList;

        //List<Texture2D> starTexturesO;
        //List<Texture2D> starTexturesB;
        //List<Texture2D> starTexturesA;
        //List<Texture2D> starTexturesF;
        //List<Texture2D> starTexturesG;
        //List<Texture2D> starTexturesK;
        //List<Texture2D> starTexturesM;
        //List<Texture2D> starTexturesL;
        //List<Texture2D> starTexturesT;
        //List<Texture2D> starTexturesY;
        //List<Texture2D> starTexturesD;
        //List<Texture2D> starTexturesWR;
        //List<Texture2D> starTexturesProtostar;
        //List<Texture2D> starTexturesPulsar;
        //List<Texture2D> starTexturesNeutron;
        //List<Texture2D> starTexturesQuark;
        //List<Texture2D> starTexturesBoson;
        //List<Texture2D> starTexturesBlackHole;

        //List<Texture2D> smallStarTexturesO;
        //List<Texture2D> smallStarTexturesB;
        //List<Texture2D> smallStarTexturesA;
        //List<Texture2D> smallStarTexturesF;
        //List<Texture2D> smallStarTexturesG;
        //List<Texture2D> smallStarTexturesK;
        //List<Texture2D> smallStarTexturesM;
        //List<Texture2D> smallStarTexturesL;
        //List<Texture2D> smallStarTexturesT;
        //List<Texture2D> smallStarTexturesY;
        //List<Texture2D> smallStarTexturesD;
        //List<Texture2D> smallStarTexturesWR;
        //List<Texture2D> smallStarTexturesProtostar;
        //List<Texture2D> smallStarTexturesPulsar;
        //List<Texture2D> smallStarTexturesNeutron;
        //List<Texture2D> smallStarTexturesQuark;
        //List<Texture2D> smallStarTexturesBoson;
        //List<Texture2D> smallStarTexturesBlackHole;

        List<Vector2> systemPositions;
        List<int> systemSizes;
        Random rand;
        int maxSystemRadius;
        int systemCount;
        int galaxyScale;
        int centerRadius;

        //double list?

        public void Initialize()
        {
            //starTexturesO = new List<Texture2D>();
            //starTexturesB = new List<Texture2D>();
            //starTexturesA = new List<Texture2D>();
            //starTexturesF = new List<Texture2D>();
            //starTexturesG = new List<Texture2D>();
            //starTexturesK = new List<Texture2D>();
            //starTexturesM = new List<Texture2D>();
            //starTexturesL = new List<Texture2D>();
            //starTexturesT = new List<Texture2D>();
            //starTexturesY = new List<Texture2D>();
            //starTexturesD = new List<Texture2D>();
            //starTexturesWR = new List<Texture2D>();
            //starTexturesProtostar = new List<Texture2D>();
            //starTexturesPulsar = new List<Texture2D>();
            //starTexturesNeutron = new List<Texture2D>();
            //starTexturesQuark = new List<Texture2D>();
            //starTexturesBoson = new List<Texture2D>();
            //starTexturesBlackHole = new List<Texture2D>();

            //smallStarTexturesO = new List<Texture2D>();
            //smallStarTexturesB = new List<Texture2D>();
            //smallStarTexturesA = new List<Texture2D>();
            //smallStarTexturesF = new List<Texture2D>();
            //smallStarTexturesG = new List<Texture2D>();
            //smallStarTexturesK = new List<Texture2D>();
            //smallStarTexturesM = new List<Texture2D>();
            //smallStarTexturesL = new List<Texture2D>();
            //smallStarTexturesT = new List<Texture2D>();
            //smallStarTexturesY = new List<Texture2D>();
            //smallStarTexturesD = new List<Texture2D>();
            //smallStarTexturesWR = new List<Texture2D>();
            //smallStarTexturesProtostar = new List<Texture2D>();
            //smallStarTexturesPulsar = new List<Texture2D>();
            //smallStarTexturesNeutron = new List<Texture2D>();
            //smallStarTexturesQuark = new List<Texture2D>();
            //smallStarTexturesBoson = new List<Texture2D>();
            //smallStarTexturesBlackHole = new List<Texture2D>();

            systems = new List<System>();
            systemPositions = new List<Vector2>();
            systemSizes = new List<int>();
            rand = new Random();
            maxSystemRadius = 100000;
            systemCount = 7500;
            galaxyScale = 10000000;
            centerRadius = 10000;

        }

        //public void LoadContent(ContentManager Content)
        //{
        //    //LoadStarTextures(Content);

        //    for (int i = 0; i < systemCount; i++)
        //    {
        //        systems.Add(new System());
        //        systems[i].Initialize(rand, rand.Next(maxSystemRadius - 100, maxSystemRadius + 100));

        //        //LoadStarTextureList(i);

        //        //int texNumber = rand.Next(0, textureList.Count);
                
        //        //Texture2D tex = textureList[texNumber];
        //        //Texture2D smallTex = smallTextureList[texNumber];

        //        systems[i].LoadContent(Content, centerRadius, galaxyScale);
        //        systemPositions.Add(systems[i].Position);
        //        systemSizes.Add(systems[i].SystemRadius);

        //        bool conflict = true;

        //        while (conflict)
        //        {
        //            conflict = false;

        //            for (int s = 0; s < systems.Count - 1; s++)
        //            {
        //                Vector2 d = systems[s].Position - systems[systems.Count - 1].Position;
        //                int distance = (int)d.Length();

        //                if (distance < systems[s].SystemRadius + systems[systems.Count - 1].SystemRadius)
        //                {
        //                    conflict = true;
        //                    systems[systems.Count - 1].ResetPosition();
        //                }
        //            }

        //            Console.Clear();
        //            Console.WriteLine(systems.Count);
        //        }
        //    }
        //}

        public void AddSystem()
        {
            //LoadStarTextures(Content);

            if (systems.Count < systemCount)
            {
                systems.Add(new System());
                systems[systems.Count - 1].Initialize(rand, rand.Next(maxSystemRadius - 100, maxSystemRadius + 100));

                //LoadStarTextureList(i);

                //int texNumber = rand.Next(0, textureList.Count);

                //Texture2D tex = textureList[texNumber];
                //Texture2D smallTex = smallTextureList[texNumber];

                systems[systems.Count - 1].LoadContent(centerRadius, galaxyScale);
                systemPositions.Add(systems[systems.Count - 1].Position);
                systemSizes.Add(systems[systems.Count - 1].SystemRadius);

                for (int s = 0; s < systems.Count - 1; s++)
                {
                    Vector2 d = systems[s].Position - systems[systems.Count - 1].Position;
                    int distance = (int)d.Length();

                    bool conflict = false;

                    if (distance < systems[s].SystemRadius + systems[systems.Count - 1].SystemRadius)
                    {
                        conflict = true;
                        systems[systems.Count - 1].ResetPosition();
                    }

                    if (conflict)
                        systems.RemoveAt(systems.Count - 1);
                }

                Console.Clear();
                Console.WriteLine(systems.Count);

                //while (conflict)
                //{
                //    conflict = false;

                //    for (int s = 0; s < systems.Count - 1; s++)
                //    {
                //        Vector2 d = systems[s].Position - systems[systems.Count - 1].Position;
                //        int distance = (int)d.Length();

                //        if (distance < systems[s].SystemRadius + systems[systems.Count - 1].SystemRadius)
                //        {
                //            conflict = true;
                //            systems[systems.Count - 1].ResetPosition();
                //        }
                //    }

                //    Console.Clear();
                //    Console.WriteLine(systems.Count);
                //}
            }
        }

        //void LoadStarTextures(ContentManager Content)
        //{
        //    starTexturesO.Add(Content.Load<Texture2D>("Images//Background//Stars//O//OStar01"));
        //    smallStarTexturesO.Add(Content.Load<Texture2D>("Images//Background//Stars//O//OSmallStar01"));

        //    starTexturesB.Add(Content.Load<Texture2D>("Images//Background//Stars//B//BStar01"));
        //    smallStarTexturesB.Add(Content.Load<Texture2D>("Images//Background//Stars//B//BSmallStar01"));

        //    starTexturesA.Add(Content.Load<Texture2D>("Images//Background//Stars//A//AStar01"));
        //    smallStarTexturesA.Add(Content.Load<Texture2D>("Images//Background//Stars//A//ASmallStar01"));

        //    starTexturesF.Add(Content.Load<Texture2D>("Images//Background//Stars//F//FStar01"));
        //    smallStarTexturesF.Add(Content.Load<Texture2D>("Images//Background//Stars//F//FSmallStar01"));

        //    starTexturesG.Add(Content.Load<Texture2D>("Images//Background//Stars//G//GStar01"));
        //    smallStarTexturesG.Add(Content.Load<Texture2D>("Images//Background//Stars//G//GSmallStar01"));

        //    starTexturesK.Add(Content.Load<Texture2D>("Images//Background//Stars//K//KStar01"));
        //    smallStarTexturesK.Add(Content.Load<Texture2D>("Images//Background//Stars//K//KSmallStar01"));

        //    starTexturesM.Add(Content.Load<Texture2D>("Images//Background//Stars//M//MStar01"));
        //    smallStarTexturesM.Add(Content.Load<Texture2D>("Images//Background//Stars//M//MSmallStar01"));

        //    starTexturesL.Add(Content.Load<Texture2D>("Images//Background//Stars//L//LStar01"));
        //    smallStarTexturesL.Add(Content.Load<Texture2D>("Images//Background//Stars//L//LSmallStar01"));

        //    starTexturesT.Add(Content.Load<Texture2D>("Images//Background//Stars//T//TStar01"));
        //    smallStarTexturesT.Add(Content.Load<Texture2D>("Images//Background//Stars//T//TSmallStar01"));

        //    starTexturesY.Add(Content.Load<Texture2D>("Images//Background//Stars//Y//YStar01"));
        //    smallStarTexturesY.Add(Content.Load<Texture2D>("Images//Background//Stars//Y//YSmallStar01"));

        //    starTexturesD.Add(Content.Load<Texture2D>("Images//Background//Stars//D//DStar01"));
        //    smallStarTexturesD.Add(Content.Load<Texture2D>("Images//Background//Stars//D//DSmallStar01"));

        //    starTexturesWR.Add(Content.Load<Texture2D>("Images//Background//Stars//WR//WRStar01"));
        //    smallStarTexturesWR.Add(Content.Load<Texture2D>("Images//Background//Stars//WR//WRSmallStar01"));

        //    starTexturesProtostar.Add(Content.Load<Texture2D>("Images//Background//Stars//Protostar//Protostar01"));
        //    smallStarTexturesProtostar.Add(Content.Load<Texture2D>("Images//Background//Stars//Protostar//ProtostarSmall01"));

        //    starTexturesPulsar.Add(Content.Load<Texture2D>("Images//Background//Stars//Pulsar//Pulsar01"));
        //    smallStarTexturesPulsar.Add(Content.Load<Texture2D>("Images//Background//Stars//Pulsar//PulsarSmall01"));

        //    starTexturesNeutron.Add(Content.Load<Texture2D>("Images//Background//Stars//Neutron//Neutron01"));
        //    smallStarTexturesNeutron.Add(Content.Load<Texture2D>("Images//Background//Stars//Neutron//NeutronSmall01"));

        //    starTexturesQuark.Add(Content.Load<Texture2D>("Images//Background//Stars//Quark//Quark01"));
        //    smallStarTexturesQuark.Add(Content.Load<Texture2D>("Images//Background//Stars//Quark//QuarkSmall01"));

        //    starTexturesBoson.Add(Content.Load<Texture2D>("Images//Background//Stars//Boson//Boson01"));
        //    smallStarTexturesBoson.Add(Content.Load<Texture2D>("Images//Background//Stars//Boson//BosonSmall01"));

        //    starTexturesBlackHole.Add(Content.Load<Texture2D>("Images//Background//Stars//BlackHole//BlackHole01"));
        //    smallStarTexturesBlackHole.Add(Content.Load<Texture2D>("Images//Background//Stars//BlackHole//BlackHoleSmall01"));
        //}

        //void LoadStarTextureList(int i)
        //{
        //    if (systems[i].StarClass == "O")
        //    {
        //        textureList = starTexturesO;
        //        smallTextureList = smallStarTexturesO;
        //    }
        //    else if (systems[i].StarClass == "B")
        //    {
        //        textureList = starTexturesB;
        //        smallTextureList = smallStarTexturesB;
        //    }
        //    else if (systems[i].StarClass == "A")
        //    {
        //        textureList = starTexturesA;
        //        smallTextureList = smallStarTexturesA;
        //    }
        //    else if (systems[i].StarClass == "F")
        //    {
        //        textureList = starTexturesF;
        //        smallTextureList = smallStarTexturesF;
        //    }
        //    else if (systems[i].StarClass == "G")
        //    {
        //        textureList = starTexturesG;
        //        smallTextureList = smallStarTexturesG;
        //    }
        //    else if (systems[i].StarClass == "K")
        //    {
        //        textureList = starTexturesK;
        //        smallTextureList = smallStarTexturesK;
        //    }
        //    else if (systems[i].StarClass == "M")
        //    {
        //        textureList = starTexturesM;
        //        smallTextureList = smallStarTexturesM;
        //    }
        //    else if (systems[i].StarClass == "L")
        //    {
        //        textureList = starTexturesL;
        //        smallTextureList = smallStarTexturesL;
        //    }
        //    else if (systems[i].StarClass == "T")
        //    {
        //        textureList = starTexturesT;
        //        smallTextureList = smallStarTexturesT;
        //    }
        //    else if (systems[i].StarClass == "Y")
        //    {
        //        textureList = starTexturesY;
        //        smallTextureList = smallStarTexturesY;
        //    }
        //    else if (systems[i].StarClass == "D")
        //    {
        //        textureList = starTexturesD;
        //        smallTextureList = smallStarTexturesD;
        //    }
        //    else if (systems[i].StarClass == "Wolf-Rayet")
        //    {
        //        textureList = starTexturesWR;
        //        smallTextureList = smallStarTexturesWR;
        //    }
        //    else if (systems[i].StarClass == "Protostar")
        //    {
        //        textureList = starTexturesProtostar;
        //        smallTextureList = smallStarTexturesProtostar;
        //    }
        //    else if (systems[i].StarClass == "Pulsar")
        //    {
        //        textureList = starTexturesPulsar;
        //        smallTextureList = smallStarTexturesPulsar;
        //    }
        //    else if (systems[i].StarClass == "Neutron")
        //    {
        //        textureList = starTexturesNeutron;
        //        smallTextureList = smallStarTexturesNeutron;
        //    }
        //    else if (systems[i].StarClass == "Quark")
        //    {
        //        textureList = starTexturesQuark;
        //        smallTextureList = smallStarTexturesQuark;
        //    }
        //    else if (systems[i].StarClass == "Boson")
        //    {
        //        textureList = starTexturesBoson;
        //        smallTextureList = smallStarTexturesBoson;
        //    }
        //    else if (systems[i].StarClass == "Black Hole")
        //    {
        //        textureList = starTexturesBlackHole;
        //        smallTextureList = smallStarTexturesBlackHole;
        //    }
        //}

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Update(gameTime);
            }
        }

        public void UpdateSystemOrbit(float SpeedMultiplier, float RadiusMultiplier)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].UpdateOrbit(SpeedMultiplier, RadiusMultiplier);
            }
        }

        public void ResetOrbits()
        {
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].ResetOrbit();
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Draw(spriteBatch, cameraNumber);
            }
        }
    }
}
