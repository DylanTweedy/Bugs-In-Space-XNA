using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class ParallaxingBackground
    {

        #region variables

        Texture2D starTexture;
        MobileSprite star;
        List<MobileSprite> stars;
        Random random;
        int starType;
        List<MobileSprite> objects;
        List<MobileSprite> objectOverlays;
        List<MobileSprite> backgrounds;
        int ObjectIndex;
        MobileSprite BackgroundObject;
        Texture2D BackgroundObjectTexture;
        MobileSprite BackgroundObjectOverlay;
        Texture2D BackgroundObjectOverlayTexture;
        MobileSprite Background;
        Texture2D BackgroundTexture;
        float scale;
        int BackgroundIndex;
        public bool Initializing;
        List<float> rotationSpeeds;
        float RotationSpeed;
        public bool InitializeOnce;
        bool loaded;
        int loading;
        TimeSpan Timer;
        TimeSpan previousTimer;
        public float speed;

        #endregion

        #region Textures

        Texture2D Planet1;
        Texture2D Planet3;
        Texture2D Planet10;
        Texture2D Planet11;
        Texture2D Planet12;
        Texture2D Planet13;
        Texture2D Planet14;
        Texture2D Planet1Overlay;
        Texture2D Planet2Overlay;
        Texture2D Planet3Overlay;
        Texture2D Planet4Overlay;
        Texture2D Planet5Overlay;
        Texture2D Planet6Overlay;
        Texture2D Planet6Overlay2;
        Texture2D Planet7Overlay;
        Texture2D Planet8Overlay;
        Texture2D Planet9Overlay;
        Texture2D Planet10Overlay;
        Texture2D Planet11Overlay;

        Texture2D Star1;
        Texture2D Star2;
        Texture2D Star3;
        Texture2D Star4;

        Texture2D Nebula1;
        Texture2D Nebula2;
        Texture2D Nebula3;
        Texture2D Galaxy1;
        Texture2D Overlay;
        Texture2D StarField;

        #endregion

        public int screenWidth
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Width; }
        }
        public int screenHeight
        {
            get { return Game1.Instance.GraphicsDevice.Viewport.Height; }
        }

        public void Initialize()
        {
            random = new Random();
            stars = new List<MobileSprite>();
            objects = new List<MobileSprite>();
            objectOverlays = new List<MobileSprite>();
            backgrounds = new List<MobileSprite>();
            starType = 1;
            previousTimer = TimeSpan.Zero;
            Timer = TimeSpan.FromSeconds(5f);
            scale = 0;
            Initializing = true;
            rotationSpeeds = new List<float>();
            InitializeOnce = false;
            loaded = false;
            loading = 1;
            speed = 1;
        }

        public void LoadContent()
        {
            LoadTextures();
        }

        private void LoadTextures()
        {
            if (loading == 1)
                Planet1 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet1");
            else if (loading == 2)
                Planet3 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet3");
            else if (loading == 3)
                Planet10 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet10");
            else if (loading == 4)
                Planet11 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet11");
            else if (loading == 5)
                Planet12 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet12");
            else if (loading == 6)
                Planet13 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet13");
            else if (loading == 7)
                Planet14 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet14");
            else if (loading == 8)
                Planet1Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet1Overlay");
            else if (loading == 9)
                Planet2Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet2Overlay");
            else if (loading == 10)
                Planet3Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet3Overlay");
            else if (loading == 11)
                Planet4Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet4Overlay");
            else if (loading == 12)
                Planet5Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet5Overlay");
            else if (loading == 13)
                Planet6Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet6Overlay");
            else if (loading == 14)
                Planet6Overlay2 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet6Overlay2");
            else if (loading == 15)
                Planet7Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet7Overlay");
            else if (loading == 16)
                Planet8Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet8Overlay");
            else if (loading == 17)
                Planet9Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet9Overlay");
            else if (loading == 18)
                Planet10Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet10Overlay");
            else if (loading == 19)
                Planet11Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Planet11Overlay");
            else if (loading == 20)
                Star1 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Star1");
            else if (loading == 21)
                Star2 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Star2");
            else if (loading == 22)
                Star3 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Star3");
            else if (loading == 23)
                Star4 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Star4");
            else if (loading == 24)
                Nebula1 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Nebula1");
            else if (loading == 25)
                Nebula2 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Nebula2");
            else if (loading == 26)
                Nebula3 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Nebula3");
            else if (loading == 27)
                Galaxy1 = Game1.Instance.Content.Load<Texture2D>("Images//Background//Galaxy1");
            else if (loading == 28)
                Overlay = Game1.Instance.Content.Load<Texture2D>("Images//Background//Overlay");
            else if (loading == 29)
                StarField = Game1.Instance.Content.Load<Texture2D>("Images//Background//StarField");

            loading += 1;

            if (loading > 29)
            {
                loaded = true;
                loading = 1;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (speed != 0)
            {
                if (loaded == false)
                {
                    LoadTextures();
                }
                else
                {
                    if (stars.Count < 750)
                    {
                        AddStar();
                        AddStar();
                        AddStar();
                        AddStar();
                        AddStar();
                    }

                    if (Initializing)
                        Timer = TimeSpan.FromSeconds(0.1f);
                    else
                        Timer = TimeSpan.FromSeconds(10f / (speed + 0.001));

                    for (int s = 0; s < stars.Count; s++)
                    {
                        stars[s].Sprite.MoveY(100f);
                        stars[s].Sprite.maxSpeed = stars[s].Sprite.OriginalMaxSpeed * speed;
                        stars[s].Update(gameTime);

                        if (Initializing)
                        {
                            stars[s].Position = new Vector2(random.Next(0 - stars[s].Sprite.Width, screenWidth), random.Next(0 - stars[s].Sprite.Height, screenHeight));
                        }

                        if (stars[s].Sprite.Position.Y > screenHeight)
                        {
                            stars[s].Position = new Vector2(random.Next(0 - star.Sprite.Width, screenWidth), -100 - star.Sprite.Height - (star.Sprite.velocity.Y * 2));
                            stars[s].Sprite.Physics(((float)random.Next(100, 2500) / 1000), 1f);
                        }
                    }

                    for (int o = 0; o < objects.Count; o++)
                    {
                        objects[o].Sprite.MoveY(100f);
                        objects[o].Sprite.Rotation += rotationSpeeds[o];
                        objectOverlays[o].Sprite.Rotation = objects[o].Sprite.Rotation;
                        objects[o].Sprite.maxSpeed = objects[o].Sprite.OriginalMaxSpeed * speed;

                        if (Initializing)
                        {
                            objects[o].Position = new Vector2(random.Next(0 - (objects[o].Sprite.Width / 2), (screenWidth - objects[o].Sprite.Width) + (objects[o].Sprite.Width / 2)), random.Next(0 - (objects[o].Sprite.Height * 3), screenHeight - (objects[o].Sprite.Height / 2)));
                        }

                        objects[o].Update(gameTime);
                        objectOverlays[o].Position = objects[o].Position;
                        objectOverlays[o].Update(gameTime);

                        if (objects[o].Sprite.Position.Y > screenHeight)
                        {
                            objects.RemoveAt(o);
                            objectOverlays.RemoveAt(o);
                            rotationSpeeds.RemoveAt(o);
                        }
                    }

                    for (int b = 0; b < backgrounds.Count; b++)
                    {
                        backgrounds[b].Sprite.MoveY(100f);
                        backgrounds[b].Sprite.maxSpeed = backgrounds[b].Sprite.OriginalMaxSpeed * speed;

                        if (Initializing)
                        {
                            backgrounds[b].Position = new Vector2(random.Next(0 - backgrounds[b].Sprite.Width, screenWidth), random.Next(0 - backgrounds[b].Sprite.Height, screenHeight));
                        }

                        backgrounds[b].Update(gameTime);

                        if (backgrounds[b].Sprite.Position.Y > screenHeight)
                        {
                            backgrounds.RemoveAt(b);
                        }
                    }

                    if (gameTime.TotalGameTime - previousTimer > Timer)
                    {
                        previousTimer = gameTime.TotalGameTime;

                        if (objects.Count < 4)
                            AddObject();
                    }

                    if (backgrounds.Count < 50)
                        AddBackground();

                    if (InitializeOnce)
                    {
                        Initializing = false;
                        InitializeOnce = false;
                    }
                }
            }
        }

        private void AddStar()
        {
            starType = random.Next(1, 5);

            if (starType == 1)
            {
                starTexture = Star1;
                star = new MobileSprite(starTexture);
                star.Sprite.AddAnimation("default", 0, 0, 16, 16, 4, ((float)random.Next(3, 100) / 100f));
                star.Sprite.Scale((float)((random.Next(30, 100) / 100f)));
            }

            if (starType == 2)
            {
                starTexture = Star2;
                star = new MobileSprite(starTexture);
                star.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                star.Sprite.Scale((float)((random.Next(10, 50) / 100f)));
            }

            if (starType == 3)
            {
                starTexture = Star3;
                star = new MobileSprite(starTexture);
                star.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                star.Sprite.Scale((float)((random.Next(10, 50) / 100f)));
            }
            
            if (starType == 4)
            {
                starTexture = Star4;
                star = new MobileSprite(starTexture);
                star.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 10f);
                star.Sprite.Scale((float)((random.Next(10, 50) / 100f)));
            }

            star.Sprite.Rotation = (float)((float)random.Next(1, 25) / 10);
            star.Sprite.CurrentAnimation = "default";
            star.Sprite.Tint = new Color(random.Next(150, 255), random.Next(150, 255), 255);
            star.Position = new Vector2(random.Next(0 - star.Sprite.Width, screenWidth), 0 - star.Sprite.Height);
            star.IsMoving = false;
            star.IsActive = true;
            star.Sprite.Physics(((float)random.Next(1000, 2500) / 1000f), 1f);

            stars.Add(star);
        }

        private void AddObject()
        {
            ObjectIndex = random.Next(1, 15);
            //ObjectIndex = 6;

            scale = (float)((float)random.Next(30, 100) / 100f);

            RotationSpeed = 0f;

            if (ObjectIndex == 1)
            {
                BackgroundObjectTexture = Planet1;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet1Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 2)
            {
                BackgroundObjectTexture = Planet1;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet2Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 3)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet3Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }


            if (ObjectIndex == 4)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet4Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 5)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet5Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 6)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                if (random.Next(1, 15) == 1)
                    BackgroundObjectOverlayTexture = Planet6Overlay2;
                else 
                    BackgroundObjectOverlayTexture = Planet6Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 7)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet7Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 8)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet8Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 9)
            {
                BackgroundObjectTexture = Planet3;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet9Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 10)
            {
                BackgroundObjectTexture = Planet10;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet10Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 11)
            {
                BackgroundObjectTexture = Planet11;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1536, 1536, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);

                BackgroundObjectOverlayTexture = Planet11Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1536, 1536, 1, 1);
            }

            if (ObjectIndex == 12)
            {
                BackgroundObjectTexture = Planet12;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1536, 1536, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);

                BackgroundObjectOverlayTexture = Planet11Overlay;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1536, 1536, 1, 1);
            }

            if (ObjectIndex == 13)
            {
                BackgroundObjectTexture = Planet13;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet13;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            if (ObjectIndex == 14)
            {
                BackgroundObjectTexture = Planet14;
                BackgroundObject = new MobileSprite(BackgroundObjectTexture);
                BackgroundObject.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                BackgroundObject.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                RotationSpeed = (float)((float)random.Next(-35, 35) / 10000);

                BackgroundObjectOverlayTexture = Planet14;
                BackgroundObjectOverlay = new MobileSprite(BackgroundObjectOverlayTexture);
                BackgroundObjectOverlay.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
            }

            BackgroundObject.Sprite.CurrentAnimation = "default";
            BackgroundObject.Sprite.Tint = new Color(random.Next(50, 255), random.Next(50, 255), random.Next(50, 255));
            BackgroundObject.Position = new Vector2(random.Next(0 - (int)(BackgroundObject.Sprite.Width / 1.25f), (screenWidth - BackgroundObject.Sprite.Width) + (int)(BackgroundObject.Sprite.Width / 1.25)), 0 - BackgroundObject.Sprite.Height);
            BackgroundObject.IsMoving = false;
            BackgroundObject.IsActive = true;
            BackgroundObject.Sprite.Physics((float)(random.Next(2500, 3000) / 1000f), 1f);
            BackgroundObject.Sprite.Scale(scale);

            BackgroundObjectOverlay.Sprite.CurrentAnimation = "default";
            BackgroundObjectOverlay.Sprite.Tint = Color.White;
            BackgroundObjectOverlay.Position = BackgroundObject.Position;
            BackgroundObjectOverlay.IsMoving = false;
            BackgroundObjectOverlay.IsActive = true;
            BackgroundObjectOverlay.Sprite.Scale(scale);
            BackgroundObjectOverlay.Sprite.Rotation = BackgroundObject.Sprite.Rotation;

            objects.Add(BackgroundObject);
            objectOverlays.Add(BackgroundObjectOverlay);
            rotationSpeeds.Add(RotationSpeed);
        }

        private void AddBackground()
        {
            BackgroundIndex = random.Next(1, 6);
            //BackgroundIndex = 3;

            scale = (float)((float)random.Next(500, 1500) / 1000f);

            if (BackgroundIndex == 1)
            {
                BackgroundTexture = Nebula1;
                Background = new MobileSprite(BackgroundTexture);
                Background.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                Background.Sprite.Tint = new Color(255, 255, 255, 155);
                Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
            }

            if (BackgroundIndex == 2)
            {
                BackgroundTexture = Nebula2;
                Background = new MobileSprite(BackgroundTexture);
                Background.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                Background.Sprite.Tint = new Color(random.Next(0, 128), 0, random.Next(50, 255), 155);
                Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
            }

            if (BackgroundIndex == 3)
            {
                BackgroundTexture = Nebula3;
                Background = new MobileSprite(BackgroundTexture);
                Background.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                Background.Sprite.Tint = new Color(random.Next(0, 255), random.Next(0, 128), random.Next(50, 255), 155);
                Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
            }

            if (BackgroundIndex == 4)
            {

                if (random.Next(1, 10) == 1)
                    BackgroundTexture = Galaxy1;
                else
                    BackgroundTexture = Nebula3;
                Background = new MobileSprite(BackgroundTexture);
                Background.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
            }

            if (BackgroundIndex == 5)
            {
                BackgroundTexture = Overlay;
                Background = new MobileSprite(BackgroundTexture);
                Background.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
            }

            if (backgrounds.Count < 30)
            {
                if (random.Next(1, 10) == 1)
                {
                    BackgroundTexture = StarField;
                    Background = new MobileSprite(BackgroundTexture);
                    Background.Sprite.AddAnimation("default", 0, 0, 1024, 1024, 1, 1);
                    Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                }
                else
                {
                    BackgroundTexture = Star2;
                    Background = new MobileSprite(BackgroundTexture);
                    Background.Sprite.AddAnimation("default", 0, 0, 64, 64, 1, 1);
                    Background.Sprite.Rotation = (float)((float)random.Next(1, 100) / 10);
                }
            }

            Background.Sprite.CurrentAnimation = "default";
            Background.Position = new Vector2(random.Next(0 - (Background.Sprite.Width / 2), screenWidth - (Background.Sprite.Width / 2)), 0 - (Background.Sprite.Height * 1.5f));
            Background.IsMoving = false;
            Background.IsActive = true;
            Background.Sprite.Physics((float)((float)random.Next(1, 2500) / 10000f), 1f);
            Background.Sprite.Scale(scale);

            backgrounds.Add(Background);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (loaded)
            {
                for (int b = 0; b < backgrounds.Count; b++)
                {
                    backgrounds[b].Draw(spriteBatch);
                }

                for (int s = 0; s < stars.Count; s++)
                {
                    stars[s].Draw(spriteBatch);
                }

                for (int o = 0; o < objects.Count; o++)
                {
                    objects[o].Draw(spriteBatch);
                    objectOverlays[o].Draw(spriteBatch);
                }
            }
        }
    }
}
