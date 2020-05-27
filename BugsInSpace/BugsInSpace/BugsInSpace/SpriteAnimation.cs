using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BugsInSpace
{
    class SpriteAnimation
    {
        //Declarations

        public Texture2D t2dTexture;
        bool bAnimating = true;
        Color colorTint = Color.White;
        Vector2 v2Position = new Vector2(0, 0);
        Vector2 v2LastPosition = new Vector2(0, 0);
        public Vector2 acceleration;
        public Vector2 velocity;
        Dictionary<string, FrameAnimation> faAnimations = new Dictionary<string, FrameAnimation>();
        string sCurrentAnimation = null;
        bool bRotateByPosition = false;
        float fRotation = 0f;
        public Vector2 v2Center;
        int iWidth;
        int iHeight;
        float circle;
        float rotationAngle;
        public Vector2 rotationVector;
        public float maxSpeed = 20;
        float stoppingSpeed;
        public float scale = 1f;
        public Color[] textureData;
        public float OriginalMaxSpeed;

        //Properties

        public Vector2 Position
        {
            get { return v2Position; }
            set
            {
                v2LastPosition = v2Position;
                v2Position = value;
                UpdateRotation();
            }
        }

        public int X
        {
            get { return (int)v2Position.X; }
            set
            {
                v2LastPosition.X = v2Position.X;
                v2Position.Y = value;
                UpdateRotation();
            }
        }

        public int Y
        {
            get { return (int)v2Position.Y; }
            set
            {
                v2LastPosition.Y = v2Position.Y;
                v2Position.Y = value;
                UpdateRotation();
            }
        }

        public int Width
        {
            get { return iWidth; }
        }

        public int Height
        {
            get { return iHeight; }
        }

        public bool AutoRotate
        {
            get { return bRotateByPosition; }
            set { bRotateByPosition = value; }
        }

        public float Rotation
        {
            get { return fRotation; }
            set { fRotation = value; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(X, Y, iWidth, iHeight); }
        }

        public Texture2D Texture
        {
            get { return t2dTexture; }
        }

        public Color Tint
        {
            get { return colorTint; }
            set { colorTint = value; }
        }

        public bool IsAnimating
        {
            get { return bAnimating; }
            set { bAnimating = value; }
        }

        public FrameAnimation CurrentFrameAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(sCurrentAnimation))
                    return faAnimations[sCurrentAnimation];
                else
                    return null;
            }
        }

        public string CurrentAnimation
        {
            get { return sCurrentAnimation; }
            set
            {
                if (faAnimations.ContainsKey(value))
                {
                    sCurrentAnimation = value;
                    faAnimations[sCurrentAnimation].CurrentFrame = 0;
                    faAnimations[sCurrentAnimation].PlayCount = 0;
                }
            }
        }

        //Constructors

        public SpriteAnimation(Texture2D Texture)
        {
            t2dTexture = Texture;
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, string NextAnimation)
        {
            faAnimations.Add(Name, new FrameAnimation(X, Y, Width, Height, Frames, FrameLength, NextAnimation));
            iWidth = Width;
            iHeight = Height;
            v2Center = new Vector2(iWidth / 2, iHeight / 2);
        }

        public void MoveBy(float x, float y)
        {
            v2LastPosition = v2Position;
            acceleration.X = x;
            acceleration.Y = y;
            UpdateRotation();
        }

        public void MoveX(float x)
        {
            v2LastPosition = v2Position;
            acceleration.X = x;
            UpdateRotation();
        }

        public void MoveY(float y)
        {
            v2LastPosition = v2Position;
            acceleration.Y = y;
            UpdateRotation();
        }

        public void SetPosX(int x)
        {
            v2LastPosition = v2Position;
            v2Position.X = x;
            UpdateRotation();
        }

        public void SetPosY(int y)
        {
            v2LastPosition = v2Position;
            v2Position.Y = y;
            UpdateRotation();
        }

        public void Physics(float MaxSpeed, float StoppingSpeed)
        {
            maxSpeed = MaxSpeed;
            stoppingSpeed = StoppingSpeed;
            OriginalMaxSpeed = MaxSpeed;
        }

        public void Scale(float Scale)
        {
            scale = Scale;
        }

        public void SetTextureData()
        {
            textureData = new Color[t2dTexture.Width * t2dTexture.Height];
            t2dTexture.GetData(textureData);
        }

        //Methods

        public FrameAnimation GetAnimationByName(string Name)
        {
            if (faAnimations.ContainsKey(Name))
            {
                return faAnimations[Name];
            }
            else
            {
                return null;
            }
        }

        //Update

        public void Update(GameTime gameTime)
        {
            v2LastPosition.X = v2Position.X;
            velocity.X += acceleration.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (velocity.X < -maxSpeed)
            {
                velocity.X = -maxSpeed;
            }
            if (velocity.X > maxSpeed)
            {
                velocity.X = maxSpeed;
            }

            if (acceleration.X == 0 && velocity.X > 0.0f)
            {
                velocity.X -= stoppingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.X > 0.0f && velocity.X < stoppingSpeed / 2)
                {
                    velocity.X = 0;
                }
            }

            if (acceleration.X == 0 && velocity.X < 0.0f)
            {
                velocity.X += stoppingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.X < 0.0f && velocity.X > -stoppingSpeed / 2)
                {
                    velocity.X = 0;
                }
            }

            v2Position.X += velocity.X;

            v2LastPosition.Y = v2Position.Y;
            velocity.Y += acceleration.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (velocity.Y < -maxSpeed)
            {
                velocity.Y = -maxSpeed;
            }
            if (velocity.Y > maxSpeed)
            {
                velocity.Y = maxSpeed;
            }

            if (acceleration.Y == 0 && velocity.Y > 0.0f)
            {
                velocity.Y -= stoppingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.Y > 0.0f && velocity.Y < stoppingSpeed / 2)
                {
                    velocity.Y = 0;
                }
            }

            if (acceleration.Y == 0 && velocity.Y < 0.0f)
            {
                velocity.Y += stoppingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (velocity.Y < 0.0f && velocity.Y > -stoppingSpeed / 2)
                {
                    velocity.Y = 0;
                }
            }

            v2Position.Y += velocity.Y;







            if (acceleration.X == 0 && v2Position.X > v2LastPosition.X)
            {

                v2Position.X -= 0.1f;

            }

            if (acceleration.X == 0 && v2Position.X < v2LastPosition.X)
            {

                v2Position.X += 0.1f;

            }

            if (acceleration.Y == 0 && v2Position.Y > v2LastPosition.Y)
            {

                v2Position.Y -= 0.1f;

            }
            if (acceleration.Y == 0 && v2Position.Y < v2LastPosition.Y)
            {

                v2Position.Y += 0.1f;
            }




            UpdateRotation();








            if (bAnimating)
            {
                if (CurrentFrameAnimation == null)
                {
                    if (faAnimations.Count > 0)
                    {
                        string[] sKeys = new string[faAnimations.Count];
                        faAnimations.Keys.CopyTo(sKeys, 0);
                        CurrentAnimation = sKeys[0];
                    }
                    else
                    {
                        return;
                    }
                }

                CurrentFrameAnimation.Update(gameTime);

                if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
                {
                    if (CurrentFrameAnimation.PlayCount > 0)
                    {
                        CurrentAnimation = CurrentFrameAnimation.NextAnimation;
                    }
                }
            }
        }

        void UpdateRotation()
        {
            if (bRotateByPosition && v2Position != v2LastPosition)
            {
                circle = MathHelper.Pi * 2.25f;
                rotationAngle = (float)Math.Atan2(v2Position.Y - v2LastPosition.Y, v2Position.X - v2LastPosition.X) + 1.57f;

                float Xposition = v2Position.X - v2LastPosition.X;
                float Yposition = v2Position.Y - v2LastPosition.Y;
                bool InvertX = false;
                bool InvertY = false;
                
                if (Xposition < 0)
                {
                    InvertX = true;
                    Xposition = -Xposition;
                }

                if (Yposition < 0)
                {
                    InvertY = true;
                    Yposition = -Yposition;
                }

                float FinalX = 1;
                float FinalY = 1;

                if (Xposition > Yposition)
                {
                    FinalX = Xposition / Xposition;
                    FinalY = Yposition / Xposition;
                }
                else if (Yposition > Xposition)
                {
                    FinalX = Xposition / Yposition;
                    FinalY = Yposition / Yposition;
                }

                if (FinalX + FinalY < 1)
                {
                    FinalX *= 2;
                    FinalY *= 2;

                    if (FinalX + FinalY < 1)
                    {
                        FinalX *= 2;
                        FinalY *= 2;

                        if (FinalX + FinalY < 1)
                        {
                            FinalX *= 2;
                            FinalY *= 2;

                            if (FinalX + FinalY < 1)
                            {
                                FinalX *= 2;
                                FinalY *= 2;
                            }
                        }
                    }

                    if (FinalX + FinalY > 1.5)
                    {
                        FinalX *= 0.75f;
                        FinalY *= 0.75f;
                    }
                }

                if (InvertX)
                    FinalX = -FinalX;
                if (InvertY)
                    FinalY = -FinalY;

                rotationVector = new Vector2(FinalX, FinalY);
                fRotation = rotationAngle % circle;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int XOffset, int YOffset)
        {
            if (bAnimating)
                spriteBatch.Draw(t2dTexture, (v2Position + new Vector2(XOffset, YOffset) + v2Center), CurrentFrameAnimation.FrameRectangle, colorTint, fRotation, v2Center, scale, SpriteEffects.None, 0);
        }
    }
}
