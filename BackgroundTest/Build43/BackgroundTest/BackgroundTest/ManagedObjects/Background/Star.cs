using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class Star
    {
        Sprite star;
        Sprite smallStar;
        Texture2D starTexture;
        Texture2D smallStarTexture;
        List<int> starClassListProb;
        List<string> starClassList;
        string starClass;
        bool giant;
        bool super;
        bool hyper;
        int temperature;
        float scale;
        Random rand;
        float rotation;
        Orbit orbit;
        Color tint;
        
        bool orbiting;
        //bool inView;
        bool starZoom;

        public Vector2 Position
        {
            get { return star.Position; }
        }

        public float Scale
        {
            get { return star.Scale; }
        }

        void SelectStarClass()
        {
            starClassListProb = new List<int>();
            starClassList = new List<string>();

            starClassList.Add("O"); starClassListProb.Add(10);
            starClassList.Add("B"); starClassListProb.Add(63);
            starClassList.Add("A"); starClassListProb.Add(300);
            starClassList.Add("F"); starClassListProb.Add(700);
            starClassList.Add("G"); starClassListProb.Add(1060);
            starClassList.Add("K"); starClassListProb.Add(1210);
            starClassList.Add("M"); starClassListProb.Add(5645);
            starClassList.Add("L"); starClassListProb.Add(25);
            starClassList.Add("T"); starClassListProb.Add(20);
            starClassList.Add("Y"); starClassListProb.Add(15);
            starClassList.Add("D"); starClassListProb.Add(45);
            starClassList.Add("Wolf-Rayet"); starClassListProb.Add(37);
            starClassList.Add("Protostar"); starClassListProb.Add(18);
            starClassList.Add("Pulsar"); starClassListProb.Add(13);
            starClassList.Add("Neutron"); starClassListProb.Add(123);
            starClassList.Add("Quark"); starClassListProb.Add(20);
            starClassList.Add("Boson"); starClassListProb.Add(5);
            starClassList.Add("Black Hole"); starClassListProb.Add(7);
            starClassList.Add("White Hole"); starClassListProb.Add(9);
            starClassList.Add("Square"); starClassListProb.Add(40);
            starClassList.Add("Rainbow"); starClassListProb.Add(32);
            starClassList.Add("Ring"); starClassListProb.Add(24);
            starClassList.Add("Dark Matter"); starClassListProb.Add(8);
            starClassList.Add("Anti-Matter"); starClassListProb.Add(6);
            starClassList.Add("Faction"); starClassListProb.Add(111);
            starClassList.Add("Sulphur"); starClassListProb.Add(34);
            starClassList.Add("Glorious"); starClassListProb.Add(70);
            starClassList.Add("Quantum"); starClassListProb.Add(13);
            starClassList.Add("Translocation"); starClassListProb.Add(13);
            starClassList.Add("Ancient"); starClassListProb.Add(50);
            starClassList.Add("Ghost"); starClassListProb.Add(27);

            int probability = 0;

            for (int i = 0; i < starClassListProb.Count; i++)
            {
                probability += starClassListProb[i];
            }

            int selection = rand.Next(0, probability);

            int counter = 0;
            int previousCounter = 0;

            for (int i = 0; i < starClassListProb.Count; i++)
            {
                counter += starClassListProb[i];

                if (selection >= previousCounter && selection <= counter)
                {
                    starClass = starClassList[i];

                    break;
                }

                previousCounter = counter;
            }

            if (rand.Next(0, 2500) <= 1)
            {
                super = true;
            }

            if (rand.Next(0, 250) <= 1)
                giant = true;

            if (super && rand.Next(0, 50) <= 1)
            {
                hyper = true;
            }
        }
        
        void SetupStarClass()
        {

            List<Texture2D> textureList = new List<Texture2D>();
            List<Texture2D> smallTextureList = new List<Texture2D>();

            if (starClass == "O")
            {
                textureList = BackgroundTextureManager.starTexturesO;
                smallTextureList = BackgroundTextureManager.smallStarTexturesO;

                temperature = rand.Next(33000, 50000);
                scale = (float)rand.Next(2000000, 3000000) / 100000f;
            }
            else if (starClass == "B")
            {
                textureList = BackgroundTextureManager.starTexturesB;
                smallTextureList = BackgroundTextureManager.smallStarTexturesB;

                temperature = rand.Next(10000, 33000);
                scale = (float)rand.Next(1500000, 2000000) / 100000f;
            }
            else if (starClass == "A")
            {
                textureList = BackgroundTextureManager.starTexturesA;
                smallTextureList = BackgroundTextureManager.smallStarTexturesA;

                temperature = rand.Next(7500, 10000);
                scale = (float)rand.Next(1000000, 1500000) / 100000f;
            }
            else if (starClass == "F")
            {
                textureList = BackgroundTextureManager.starTexturesF;
                smallTextureList = BackgroundTextureManager.smallStarTexturesF;

                temperature = rand.Next(6000, 7500);
                scale = (float)rand.Next(900000, 1000000) / 100000f;
            }
            else if (starClass == "G")
            {
                textureList = BackgroundTextureManager.starTexturesG;
                smallTextureList = BackgroundTextureManager.smallStarTexturesG;

                temperature = rand.Next(5200, 6000);
                scale = (float)rand.Next(750000, 900000) / 100000f;
            }
            else if (starClass == "K")
            {
                textureList = BackgroundTextureManager.starTexturesK;
                smallTextureList = BackgroundTextureManager.smallStarTexturesK;

                temperature = rand.Next(3700, 5200);
                scale = (float)rand.Next(500000, 750000) / 100000f;
            }
            else if (starClass == "M")
            {
                textureList = BackgroundTextureManager.starTexturesM;
                smallTextureList = BackgroundTextureManager.smallStarTexturesM;

                temperature = rand.Next(2000, 3700);
                scale = (float)rand.Next(400000, 500000) / 100000f;
            }
            else if (starClass == "L")
            {
                textureList = BackgroundTextureManager.starTexturesL;
                smallTextureList = BackgroundTextureManager.smallStarTexturesL;

                temperature = rand.Next(1300, 2000);
                scale = (float)rand.Next(375000, 400000) / 100000f;
            }
            else if (starClass == "T")
            {
                textureList = BackgroundTextureManager.starTexturesT;
                smallTextureList = BackgroundTextureManager.smallStarTexturesT;

                temperature = rand.Next(700, 1300);
                scale = (float)rand.Next(350000, 375000) / 100000f;
            }
            else if (starClass == "Y")
            {
                textureList = BackgroundTextureManager.starTexturesY;
                smallTextureList = BackgroundTextureManager.smallStarTexturesY;

                temperature = rand.Next(500, 700);
                scale = (float)rand.Next(325000, 350000) / 100000f;
            }
            else if (starClass == "D")
            {
                textureList = BackgroundTextureManager.starTexturesD;
                smallTextureList = BackgroundTextureManager.smallStarTexturesD;

                temperature = rand.Next(33000, 50000);
                scale = (float)rand.Next(100000, 325000) / 100000f;
            }
            else if (starClass == "Wolf-Rayet")
            {
                textureList = BackgroundTextureManager.starTexturesWR;
                smallTextureList = BackgroundTextureManager.smallStarTexturesWR;

                temperature = rand.Next(30000, 200000);
                scale = (float)rand.Next(1000000, 2500000) / 100000f;
            }
            else if (starClass == "Protostar")
            {
                textureList = BackgroundTextureManager.starTexturesProtostar;
                smallTextureList = BackgroundTextureManager.smallStarTexturesProtostar;

                temperature = rand.Next(500, 5000);
                scale = (float)rand.Next(300000, 2500000) / 100000f;
            }
            else if (starClass == "Pulsar")
            {
                textureList = BackgroundTextureManager.starTexturesPulsar;
                smallTextureList = BackgroundTextureManager.smallStarTexturesPulsar;

                temperature = rand.Next(1000000, 100000000);
                scale = (float)rand.Next(50000, 100000) / 100000f;
            }
            else if (starClass == "Neutron")
            {
                textureList = BackgroundTextureManager.starTexturesNeutron;
                smallTextureList = BackgroundTextureManager.smallStarTexturesNeutron;

                temperature = rand.Next(1000000, 100000000);
                scale = (float)rand.Next(50000, 100000) / 100000f;
            }
            else if (starClass == "Quark")
            {
                textureList = BackgroundTextureManager.starTexturesQuark;
                smallTextureList = BackgroundTextureManager.smallStarTexturesQuark;

                temperature = rand.Next(1000000, 100000000);
                scale = (float)rand.Next(25000, 50000) / 100000f;
            }
            else if (starClass == "Boson")
            {
                textureList = BackgroundTextureManager.starTexturesBoson;
                smallTextureList = BackgroundTextureManager.smallStarTexturesBoson;

                temperature = rand.Next(1000000, 100000000);
                scale = (float)rand.Next(10000, 25000) / 100000f;
            }
            else if (starClass == "Black Hole")
            {
                textureList = BackgroundTextureManager.starTexturesBlackHole;
                smallTextureList = BackgroundTextureManager.smallStarTexturesBlackHole;

                temperature = rand.Next(0, 10);
                scale = (float)rand.Next(50000, 200000) / 100000f;
            }
            else if (starClass == "White Hole")
            {
                textureList = BackgroundTextureManager.starTexturesWhiteHole;
                smallTextureList = BackgroundTextureManager.smallStarTexturesWhiteHole;

                temperature = rand.Next(0, 10);
                scale = (float)rand.Next(50000, 200000) / 100000f;
            }
            else if (starClass == "Square")
            {
                textureList = BackgroundTextureManager.starTexturesSquare;
                smallTextureList = BackgroundTextureManager.smallStarTexturesSquare;

                temperature = rand.Next(5000, 10000);
                scale = (float)rand.Next(400000, 900000) / 100000f;
            }
            else if (starClass == "Rainbow")
            {
                textureList = BackgroundTextureManager.starTexturesRainbow;
                smallTextureList = BackgroundTextureManager.smallStarTexturesRainbow;

                temperature = rand.Next(2500, 10000);
                scale = (float)rand.Next(400000, 900000) / 100000f;
            }
            else if (starClass == "Ring")
            {
                textureList = BackgroundTextureManager.starTexturesRing;
                smallTextureList = BackgroundTextureManager.smallStarTexturesRing;

                temperature = rand.Next(2500, 5000);
                scale = (float)rand.Next(50000, 400000) / 100000f;
            }
            else if (starClass == "Dark Matter")
            {
                textureList = BackgroundTextureManager.starTexturesDarkMatter;
                smallTextureList = BackgroundTextureManager.smallStarTexturesDarkMatter;

                temperature = rand.Next(1000, 3000);
                scale = (float)rand.Next(50000, 200000) / 100000f;
            }
            else if (starClass == "Anti-Matter")
            {
                textureList = BackgroundTextureManager.starTexturesAntiMatter;
                smallTextureList = BackgroundTextureManager.smallStarTexturesAntiMatter;

                temperature = rand.Next(2000, 4000);
                scale = (float)rand.Next(50000, 200000) / 100000f;
            }
            else if (starClass == "Faction")
            {
                textureList = BackgroundTextureManager.starTexturesFaction;
                smallTextureList = BackgroundTextureManager.smallStarTexturesFaction;

                temperature = rand.Next(2500, 10000);
                scale = (float)rand.Next(50000, 1500000) / 100000f;
            }
            else if (starClass == "Sulphur")
            {
                textureList = BackgroundTextureManager.starTexturesSulphur;
                smallTextureList = BackgroundTextureManager.smallStarTexturesSulphur;

                temperature = rand.Next(5000, 10000);
                scale = (float)rand.Next(500000, 700000) / 100000f;
            }
            else if (starClass == "Glorious")
            {
                textureList = BackgroundTextureManager.starTexturesGlorious;
                smallTextureList = BackgroundTextureManager.smallStarTexturesGlorious;

                temperature = rand.Next(2500, 20000);
                scale = (float)rand.Next(50000, 650000) / 100000f;
            }
            else if (starClass == "Quantum")
            {
                textureList = BackgroundTextureManager.starTexturesQuantum;
                smallTextureList = BackgroundTextureManager.smallStarTexturesQuantum;

                temperature = rand.Next(1000, 15000);
                scale = (float)rand.Next(100000, 800000) / 100000f;
            }
            else if (starClass == "Translocation")
            {
                textureList = BackgroundTextureManager.starTexturesTranslocation;
                smallTextureList = BackgroundTextureManager.smallStarTexturesTranslocation;

                temperature = rand.Next(1000, 15000);
                scale = (float)rand.Next(100000, 800000) / 100000f;
            }
            else if (starClass == "Ancient")
            {
                textureList = BackgroundTextureManager.starTexturesAncient;
                smallTextureList = BackgroundTextureManager.smallStarTexturesAncient;

                temperature = rand.Next(0, 1000);
                scale = (float)rand.Next(50000, 2000000) / 100000f;
            }
            else if (starClass == "Ghost")
            {
                textureList = BackgroundTextureManager.starTexturesGhost;
                smallTextureList = BackgroundTextureManager.smallStarTexturesGhost;

                temperature = rand.Next(0, 25000);
                scale = (float)rand.Next(100000, 150000) / 100000f;
            }

            if (giant)
            {
                scale += (float)rand.Next(20000, 30000) / 1000f;
            }

            if (super)
            {
                scale += (float)rand.Next(30000, 50000) / 1000f;
            }

            if (hyper)
            {
                scale += (float)rand.Next(50000, 75000) / 1000f;
            }

            scale += 2f;

            int texNumber = rand.Next(0, textureList.Count);

            starTexture = textureList[texNumber];
            smallStarTexture = smallTextureList[texNumber];
        }

        public Star(Random Rand)
        {
            rand = Rand;

            SelectStarClass();
            SetupStarClass();

            star = new Sprite(starTexture);
            star.Scale = scale;
            smallStar = new Sprite(smallStarTexture);
            smallStar.Scale = (scale * (star.Texture.Width / smallStar.Texture.Width)) * 2;

            star.Tint = new Color(rand.Next(200, 255), rand.Next(200, 255), rand.Next(200, 255));
            smallStar.Tint = star.Tint;
            tint = star.Tint;
        }

        public void SetupStar(Vector2 Pos, float Rot, int StarCount, float spe, float orbitRot, float biggestStar)
        {
            if (StarCount > 1)
            {
                orbiting = true;

                float rad = biggestStar * 250f * StarCount;
                float rot = orbitRot;
                
                orbit = new Orbit(rad, spe, rot, rot);

                orbit.InitializeCircle();                

                orbit.UpdatePosition(Pos);
                orbit.OriginalPosition = orbit.Position;
                orbit.OriginalRadian = orbit.OrbitRadian;

                star.Position = orbit.Position;
                smallStar.Position = orbit.Position;
            }
            else
            {
                star.Position = Pos;
                smallStar.Position = Pos;
            }
            
            rotation = (float)rand.Next(1, 30000) / 10000000f;

            star.Rotation = Rot;
            smallStar.Rotation = Rot;
        }

        public int CalculateRadius()
        {
            if (orbiting)
                return (int)orbit.OrbitRadius + (int)((star.Texture.Width / 2) * scale);
            else
                return (int)((star.Texture.Width / 2) * scale);
        }

        public void Update(GameTime gameTime, Vector2 OrbitCenter)
        {
            if (starZoom)
            {
                if (orbiting)
                {
                    orbit.UpdatePosition(OrbitCenter);

                    star.Position = orbit.Position;
                    smallStar.Position = orbit.Position;
                }

                star.Rotation += rotation;
            }
            else
            {
                if (orbiting)
                {
                    orbit.UpdatePosition(OrbitCenter);

                    star.Position = orbit.Position;
                    smallStar.Position = orbit.Position;
                }

                star.Rotation += rotation;
            }

            starZoom = false;
            //inView = false;
        }

        public void ResetOrbit(Vector2 OrbitCenter)
        {
            if (orbiting)
            {
                orbit.UpdatePosition(OrbitCenter);

                star.Position = orbit.Position;
                smallStar.Position = orbit.Position;
            }
            else
            {
                star.Position = OrbitCenter;
                smallStar.Position = OrbitCenter;
            }
        }

        public void UpdateOrbit(Vector2 OrbitCenter)
        {
            if (orbiting)
            {
                orbit.UpdatePosition(OrbitCenter);

                star.Position = orbit.Position;
                smallStar.Position = orbit.Position;
            }
            else
            {
                star.Position = OrbitCenter;
                smallStar.Position = OrbitCenter;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraNumber)
        {
            float smallScale = tint.A * (CameraManager.CamerasRead[cameraNumber].Zoom * 10) * (scale / 10);

            float smallZoomPercent = ((CameraManager.CamerasRead[cameraNumber].Zoom * 100000) / 70);

            if (CameraManager.CamerasRead[cameraNumber].Zoom <= 0.0007)
            {
                smallStar.Tint = new Color(
                    (scale / 5) * smallZoomPercent,
                    (scale / 5) * smallZoomPercent,
                    (scale / 5) * smallZoomPercent,
                    (scale / 5) * smallZoomPercent);
            }

            if (smallStar.Tint.A > 30)
            {
                if (CameraManager.CamerasRead[cameraNumber].IsInView(star.Position, star.Texture.Width, star.Texture.Height, star.Scale))
                {
                    //inView = true;

                    if (CameraManager.CamerasRead[cameraNumber].Zoom <= 0.0007)
                    {
                        smallStar.Scale = ((scale * (star.Texture.Width / smallStar.Texture.Width)) * 2) * (1 + (1 - smallZoomPercent)) * (1 + (1 - smallZoomPercent));

                        //if (star.Scale <= 3)
                        //    smallStar.Scale = (((star.Scale / (CameraManager.CamerasRead[cameraNumber].Zoom * 2f)) / (smallStar.Texture.Width / 2)) * 2) * smallScale;
                        //else
                        //    smallStar.Scale = ((star.Scale / (CameraManager.CamerasRead[cameraNumber].Zoom * 2f)) / (smallStar.Texture.Width / 2)) * smallScale;

                        
                       
                        smallStar.Draw(spriteBatch);
                    }
                    else
                    {
                        starZoom = true;
                        star.Draw(spriteBatch);
                    }
                }

                if (CameraManager.CamerasRead[cameraNumber].Zoom > 0.0007)
                {
                    starZoom = true;
                }
            }
        }
    }
}
