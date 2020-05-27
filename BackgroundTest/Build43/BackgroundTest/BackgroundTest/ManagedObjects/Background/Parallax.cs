using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class Parallax
    {
        List<Sprite> starImages;
        List<Texture2D> starTextures;
        List<float> speeds;
        List<float> scales;
        Vector2 speed;
        List<Sprite> images;
        List<Vector2> positions;
        List<int> sheets;
        List<Texture2D> textures;
        Random rand;
        Vector2 Pos;
        Vector2 ScreenZero;
        Vector2 ScreenMax;
        int X;
        int Y;
        int s;
        Vector2 MoveSpeed;
        float distance;
        int tint;
        int Counter;
        Timer timer;
        float speedX;
        float speedY;
        float speedXChange;
        float speedYChange;
        float time;

        public Parallax()
        {
            starImages = new List<Sprite>();
            starTextures = new List<Texture2D>();
            images = new List<Sprite>();
            positions = new List<Vector2>();
            textures = new List<Texture2D>();
            scales = new List<float>();
            speeds = new List<float>();
            sheets = new List<int>();
            speed = Vector2.Zero;
            rand = new Random();
            Pos = Vector2.Zero;
            ScreenZero = Vector2.Zero;
            ScreenMax = Vector2.Zero;
            X = 0;
            Y = 0;
            s = 0;
            MoveSpeed = Vector2.Zero;
            distance = 0;
            tint = 255;
            Counter = 0;
            timer = new Timer(rand.Next(30, 300), true);
            speedX = ((float)rand.Next(0, 200) / 100f) - 1f;
            speedY = ((float)rand.Next(0, 200) / 100f) - 1f;
            speedXChange = speedX;
            speedYChange = speedY;
            time = 0f;
        }


        public void LoadContent(ContentManager Content)
        {
            LoadStarImages(Content);

            for (int i = 0; i < 500; i++)
            {
                starImages.Add(new Sprite(starTextures[rand.Next(0, starTextures.Count)]));

                starImages[i].AddAnimation("default", 0, 0, 16, 16, 1, 0f, null);

                starImages[i].Scale = (float)rand.Next(100, 500) / 1000f;
                scales.Add(starImages[i].Scale);
                starImages[i].Rotation = (float)rand.Next(0, 10000) / 1000f;

                starImages[i].Position = new Vector2(rand.Next(0, WorldVariables.WindowWidth * 3), rand.Next(0, WorldVariables.WindowHeight * 3));
                starImages[i].Position -= new Vector2(WorldVariables.WindowWidth, WorldVariables.WindowHeight);

                speeds.Add((float)rand.Next(50000, 100000) / 100000f);
            }

            LoadTextures(Content);

            for (int i = 0; i < textures.Count; i++)
            {
                X = (int)(((float)(WorldVariables.WindowWidth * 3) / (float)textures[i].Width)) + 2;
                Y = (int)(((float)(WorldVariables.WindowWidth * 3) / (float)textures[i].Height)) + 2;
                s = X * Y;
                sheets.Add(s);

                for (int x = 0; x < X; x++)
                {
                    for (int y = 0; y < Y; y++)
                    {
                        positions.Add(new Vector2(x * textures[i].Width, y * textures[i].Height) - (new Vector2(X * textures[i].Width, Y * textures[i].Height) / 2) + new Vector2(textures[i].Width / 2, textures[i].Height / 2));
                    }
                }

                for (int o = 0; o < sheets[i]; o++)
                {
                    images.Add(new Sprite(textures[i]));
                }
            }

            for (int o = 0; o < positions.Count; o++)
            {
                if (rand.Next(0, 2) == 0)
                    images[o].SpriteEffect = SpriteEffects.FlipHorizontally;
                else
                    images[o].SpriteEffect = SpriteEffects.None;

                if (rand.Next(0, 2) == 0)
                    images[o].SpriteEffect = SpriteEffects.FlipVertically;
                else
                    images[o].SpriteEffect = SpriteEffects.None;

                images[o].Position = positions[o];
            }
        }

        void LoadStarImages(ContentManager Content)
        {
            starTextures.Add(Content.Load<Texture2D>("Images//Background//Star5"));
        }

        void LoadTextures(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("Images//Background//IMG_2367transparencysmall2"));
            textures.Add(Content.Load<Texture2D>("Images//Background//IMG_2367transparencysmall"));
            textures.Add(Content.Load<Texture2D>("Images//Background//IMG_2367transparencysmall2"));
            textures.Add(Content.Load<Texture2D>("Images//Background//IMG_2367transparencysmall"));
            textures.Add(Content.Load<Texture2D>("Images//Background//IMG_2367transparencysmall2"));
            textures.Add(Content.Load<Texture2D>("Images//Background//IMG_2367transparencysmall"));
        }

        public void Update(GameTime gameTime, int Camera)
        {
            //only draw on screen stars
            //change static star drift every so often

            timer.Update(gameTime);

            speed = (CameraManager.CamerasRead[Camera].Focus - CameraManager.CamerasRead[Camera].PreviousFocus);

            if (Vector2.Distance(Vector2.Zero, speed) < 25)
            {
                if (timer.Finished)
                {
                    speedXChange = ((float)rand.Next(0, 200) / 100f) - 1f;
                    speedYChange = ((float)rand.Next(0, 200) / 100f) - 1f;

                    timer.EditTimer(rand.Next(30, 300), true);
                }

                time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (speedXChange != speedX)
                {
                    speedX += (speedXChange - speedX) * time;
                }

                if (speedYChange != speedY)
                {
                    speedY += (speedYChange - speedY) * time;
                }

                speed.X += speedX;
                speed.Y += speedY;
            }

            if (Vector2.Distance(Vector2.Zero, speed * CameraManager.CamerasRead[Camera].Zoom) < 0.1f)
            {
            }
            else
            {
                for (int i = 0; i < starImages.Count; i++)
                {
                    MoveSpeed = (speed * speeds[i]) * CameraManager.CamerasRead[Camera].Zoom;
                    distance = Vector2.Distance(Vector2.Zero, MoveSpeed);
                    tint = (int)(255f * (CameraManager.ZoomValues[Camera]));

                    if (distance >= 1)
                    {
                        starImages[i].AutoRotate = true;

                        if (distance > 40)
                        {
                            starImages[i].AbsalouteScale(scales[i], scales[i] * 40);
                        }
                        else
                        {
                            starImages[i].AbsalouteScale(scales[i], scales[i] * distance);
                        }
                    }
                    else
                    {
                        starImages[i].AbsalouteScale(scales[i], scales[i]);
                        starImages[i].AutoRotate = false;
                        starImages[i].Animate(gameTime);
                    }

                    if (tint > 155)
                        tint = 155;
                    else if (tint < 55)
                        tint = 55;

                    starImages[i].Tint = new Color(tint, tint, tint, tint);

                    starImages[i].Position = starImages[i].Position - MoveSpeed;
                    starImages[i].AutoRotate = false;

                    Pos = starImages[i].Position;
                    ScreenZero = Vector2.Zero - new Vector2(WorldVariables.WindowWidth, WorldVariables.WindowHeight);
                    ScreenMax = ScreenZero + new Vector2(WorldVariables.WindowWidth * 3, WorldVariables.WindowHeight * 3);

                    if (ScreenZero.X > starImages[i].Position.X)
                        Pos.X += WorldVariables.WindowWidth * 3;
                    else if (ScreenMax.X < starImages[i].Position.X)
                        Pos.X -= WorldVariables.WindowWidth * 3;

                    if (ScreenZero.Y > starImages[i].Position.Y)
                        Pos.Y += WorldVariables.WindowHeight * 3;
                    else if (ScreenMax.Y < starImages[i].Position.Y)
                        Pos.Y -= WorldVariables.WindowHeight * 3;

                    starImages[i].Position = Pos;
                }

                for (int i = 0; i < images.Count; i++)
                {
                    int tint = (int)(255f * CameraManager.ZoomValues[Camera]);

                    if (tint > 155)
                        tint = 155;
                    else if (tint < 55)
                        tint = 55;

                    images[i].Tint = new Color(tint, tint, tint, tint);
                }

                Counter = 0;

                for (int i = 0; i < textures.Count; i++)
                {
                    for (int o = 0; o < sheets[i]; o++)
                    {
                        images[Counter].Position -= ((speed / 5) * ((i + 1f) / textures.Count)) * (CameraManager.ZoomValues[Camera] * 2);
                        images[Counter].Animate(gameTime);

                        Counter += 1;
                    }
                }

                Counter = 0;

                s = 0;

                for (int i = 0; i < textures.Count; i++)
                {
                    s += sheets[i];

                    for (int o = 0; o < sheets[i]; o++)
                    {
                        if (images[Counter].Position.X < positions[s - sheets[i]].X - textures[i].Width)
                        {
                            images[Counter].Position = new Vector2(positions[s - 1].X + (images[Counter].Position.X - (positions[s - sheets[i]].X - textures[i].Width)), images[Counter].Position.Y);

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipHorizontally;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipVertically;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;
                        }
                        else if (images[Counter].Position.X > textures[i].Width + positions[s - 1].X)
                        {
                            images[Counter].Position = new Vector2(positions[s - sheets[i]].X + (images[Counter].Position.X - (textures[i].Width + positions[s - 1].X)), images[Counter].Position.Y);

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipHorizontally;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipVertically;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;
                        }

                        if (images[Counter].Position.Y < positions[s - sheets[i]].Y - textures[i].Height)
                        {
                            images[Counter].Position = new Vector2(images[Counter].Position.X, positions[s - 1].Y + (images[Counter].Position.Y - (positions[s - sheets[i]].Y - textures[i].Height)));

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipHorizontally;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipVertically;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;
                        }
                        else if (images[Counter].Position.Y > textures[i].Height + positions[s - 1].Y)
                        {
                            images[Counter].Position = new Vector2(images[Counter].Position.X, positions[s - sheets[i]].Y + (images[Counter].Position.Y - (textures[i].Height + positions[s - 1].Y)));

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipHorizontally;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;

                            if (rand.Next(0, 2) == 0)
                                images[Counter].SpriteEffect = SpriteEffects.FlipVertically;
                            else
                                images[Counter].SpriteEffect = SpriteEffects.None;
                        }

                        Counter += 1;
                    }
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, int cameraNumber)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].Draw(spriteBatch);
            }

            for (int i = 0; i < starImages.Count; i++)
            {
                starImages[i].Draw(spriteBatch);
            }
        }
    }
}
