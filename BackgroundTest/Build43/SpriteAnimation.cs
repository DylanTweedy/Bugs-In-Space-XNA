using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class SpriteAnimation
    {
        #region Variables

        Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        Dictionary<string, Animation> idleAnimations = new Dictionary<string, Animation>();
        Texture2D texture;
        Vector2 center;
        Vector2 position;
        Vector2 lastPosition;
        float rotation;
        float minTime;
        float maxTime;
        string currentAnimation;
        string beforeIdleAnimation;
        float scale;
        Timer timer;
        Random rand;
        int lastFrame;
        string previousAnimation;

        #endregion

        #region Properties

        public bool AutoRotate { get; set; }
        public Vector2 centerOffset { get; set; }
        public Color Tint { get; set; }

        //public Vector2 Center
        //{
        //    get { return center; }
        //}

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                lastPosition = position;
                position = value;
                UpdateRotation();
            }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public int Width
        {
            get { return texture.Width; }            
        }

        public int Height
        {
            get { return texture.Height; }
        }

        public Animation CurrentFrameAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(currentAnimation) && animations.ContainsKey(currentAnimation))
                    return animations[currentAnimation];
                else if (!string.IsNullOrEmpty(currentAnimation) && idleAnimations.ContainsKey(currentAnimation))
                    return idleAnimations[currentAnimation];
                else
                    return null;
            }
        }

        public string CurrentAnimation
        {
            get { return currentAnimation; }
            set
            {
                if (value == null)
                {
                    currentAnimation = value;
                    return;
                }

                if (animations.ContainsKey(value))
                {
                    currentAnimation = value;
                    animations[currentAnimation].CurrentFrame = 0;
                    animations[currentAnimation].PlayCount = 0;
                }

                if (idleAnimations.ContainsKey(value))
                {
                    currentAnimation = value;
                    idleAnimations[currentAnimation].CurrentFrame = 0;
                    idleAnimations[currentAnimation].PlayCount = 0;
                }
            }
        }

        public bool IdleAnimationActive
        {
            get { return timer.TimerOn; }
            set { timer.TimerOn = value; }
        }

        #endregion

        public SpriteAnimation(Texture2D Tex)
        {
            texture = Tex;
            center = new Vector2(texture.Width / 2, texture.Height / 2);
            Tint = Color.White;
            rotation = 0f;
            scale = 1f;
            timer = new Timer(0f, true);
            rand = new Random();
            previousAnimation = null;
            beforeIdleAnimation = null;
            currentAnimation = null;
            lastFrame = 0;
            centerOffset = Vector2.Zero;
        }

        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, string NextAnimation)
        {
            animations.Add(Name, new Animation(X, Y, Width, Height, Frames, FrameLength, NextAnimation));
            currentAnimation = Name;
        }

        public void AddIdleAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, float MinTime, float MaxTime)
        {
            idleAnimations.Add(Name, new Animation(X, Y, Width, Height, Frames, FrameLength, null));
            minTime = MinTime;
            maxTime = MaxTime;
            timer = new Timer(0f, true);
            currentAnimation = Name;
            timer.TimerOn = true;
        }

        public void MoveBy(float x, float y)
        {
            position.X += x;
            position.Y += y;
            UpdateRotation();
        }

        public void Animate(GameTime gameTime)
        {
            if (string.IsNullOrEmpty(currentAnimation))
                center = new Vector2(texture.Width / 2, texture.Height / 2);
            else
                center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);
        
            if (timer.TimerOn)
            {
                if (timer.Finished && timer.Count > 0)
                {
                    int selector = rand.Next(0, idleAnimations.Count);
                    
                    string[] sKeys = new string[idleAnimations.Count];
                    idleAnimations.Keys.CopyTo(sKeys, 0);

                    if (string.IsNullOrEmpty(currentAnimation))
                    {
                        lastFrame = 0;
                        beforeIdleAnimation = null;
                    }
                    else
                    {
                        lastFrame = CurrentFrameAnimation.CurrentFrame;
                        if (animations.ContainsKey(currentAnimation))
                            beforeIdleAnimation = currentAnimation;
                    }
                    
                    currentAnimation = sKeys[selector];

                    float speedChange = rand.Next((int)((CurrentFrameAnimation.OriginalSpeed * 100) * 0.8), (int)((CurrentFrameAnimation.OriginalSpeed * 100) * 1.2)) / 100f;
                    if (speedChange <= 0.03f)
                        speedChange = 0.04f;
                    CurrentFrameAnimation.AnimationSpeed = speedChange;

                    float duration = (rand.Next((int)(minTime * 10), (int)(maxTime * 10)) / 10) + CurrentFrameAnimation.AnimationSpeed;
                    timer.EditTimer(duration, true);

                    if (string.IsNullOrEmpty(currentAnimation))
                        center = new Vector2(texture.Width / 2, texture.Height / 2);
                    else
                        center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);
                }

                if (currentAnimation != null)
                    if (idleAnimations.ContainsKey(currentAnimation) && CurrentFrameAnimation.PlayCount > 0)
                    {
                        CurrentFrameAnimation.CurrentFrame = lastFrame;
                        CurrentFrameAnimation.PlayCount = 0;
                        CurrentAnimation = beforeIdleAnimation;
                    }

                if (string.IsNullOrEmpty(currentAnimation))
                    center = new Vector2(texture.Width / 2, texture.Height / 2);
                else
                    center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);

                timer.Update(gameTime);
            }

            if (CurrentFrameAnimation == null)
            {
                if (animations.Count > 0)
                {
                    string[] sKeys = new string[animations.Count];
                    animations.Keys.CopyTo(sKeys, 0);
                    currentAnimation = sKeys[0];
                }
                else
                {                                        
                    previousAnimation = currentAnimation;
                    lastPosition = position;

                    return;
                    
                }
            }

            if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
            {
                if (CurrentFrameAnimation.PlayCount > 0)
                {
                    currentAnimation = CurrentFrameAnimation.NextAnimation;
                }
            }

            CurrentFrameAnimation.Animate(gameTime);

            previousAnimation = currentAnimation;
            lastPosition = position;
        }



        void UpdateRotation()
        {
            if (AutoRotate)
            {
                float rY = (position.Y) - (lastPosition.Y);
                float rX = (position.X) - (lastPosition.X);
                
                rotation = (float)Math.Atan2(rY, rX) + ((float)Math.PI / 2);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(currentAnimation))
                spriteBatch.Draw(texture, position, CurrentFrameAnimation.FrameRectangle, Tint, rotation, center - centerOffset, scale, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(texture, position, null, Tint, rotation, center - centerOffset, scale, SpriteEffects.None, 0);
        }
    }
}
