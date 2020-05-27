using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class Sprite
    {
        #region Variables

        public Dictionary<string, MovementAnimation> movementAnimations;
        Dictionary<string, Animation> animations;
        Dictionary<string, Animation> idleAnimations;
        Texture2D texture;
        Vector2 center;
        Vector2 position;
        Vector2 lastPosition;
        float rotation;
        float minTime;
        float maxTime;
        string currentAnimation;
        string movementAnimation;
        Vector2 scale;
        Timer timer;
        Random rand;
        string previousAnimation;
        SpriteEffects spriteEffect;
        Vector2 centerOffset;
        bool randomActive;

        #endregion

        #region Properties

        public bool AutoRotate { get; set; }
        public Vector2 originalOffset { get; set; }
        public Color Tint { get; set; }
        
        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
            set { spriteEffect = value; }
        }

        public string MovementAnimation
        {
            get { return movementAnimation; }
            set { movementAnimation = value; }
        }

        public float Scale
        {
            get { return scale.X; }
            set
            {
                scale.X = value;
                scale.Y = value;
            }
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
                if (currentAnimation == null)
                    return null;

                if (!randomActive && animations.ContainsKey(currentAnimation))
                    return animations[currentAnimation];
                else if (randomActive && idleAnimations.ContainsKey(currentAnimation))
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

        public MovementAnimation CurrentMovementAnimation
        {
            get
            {
                if (movementAnimation != null)
                {
                    if (movementAnimations.ContainsKey(movementAnimation))
                        return movementAnimations[movementAnimation];
                    else
                        return null;
                }
                else return null;
            }
        }

        public bool IdleAnimationActive
        {
            get { return timer.TimerOn; }
            set { timer.TimerOn = value; }
        }

        #endregion

        public Sprite(Texture2D Tex)
        {
            movementAnimations = new Dictionary<string, MovementAnimation>();
            animations = new Dictionary<string, Animation>();
            idleAnimations = new Dictionary<string, Animation>();
            texture = Tex;
            center = new Vector2(texture.Width / 2, texture.Height / 2);
            Tint = Color.White;
            rotation = 0f;
            scale = new Vector2(1f, 1f);
            timer = new Timer(0f, true);
            rand = WorldVariables.RandomNumber;
            previousAnimation = null;
            currentAnimation = null;
            centerOffset = Vector2.Zero;
            originalOffset = centerOffset;
            spriteEffect = SpriteEffects.None;
            randomActive = false;
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
            timer = new Timer(rand.Next((int)(minTime * 1000), (int)(maxTime * 1000)) / 1000f, true);
            currentAnimation = Name;
            timer.TimerOn = true;
        }

        public void AddMAnimation(string Name, float FrameLength, string NextAnimation)
        {
            movementAnimations.Add(Name, new MovementAnimation(new List<Vector2>(), FrameLength, NextAnimation));
        }

        public void AddMAnimation(string Name, List<Vector2> VectorList, float FrameLength, string NextAnimation)
        {
            movementAnimations.Add(Name, new MovementAnimation(VectorList, FrameLength, NextAnimation));
        }

        public void AddMAninationVector(string Name, int x, int y)
        {
            movementAnimations[Name].frameVector.Add(new Vector2(x, y));
        }
        
        public void AbsalouteScale(float x, float y)
        {
            scale.X = x;
            scale.Y = y;
        }

        public void MoveBy(float x, float y)
        {
            position.X += x;
            position.Y += y;
            UpdateRotation();
        }

        public void AnimationSpeed(float SpeedMultiplier)
        {
            if (CurrentFrameAnimation != null)
                CurrentFrameAnimation.AnimationSpeed = CurrentFrameAnimation.OriginalSpeed / SpeedMultiplier;
            if (CurrentMovementAnimation != null)
                CurrentMovementAnimation.AnimationSpeed = CurrentMovementAnimation.OriginalSpeed / SpeedMultiplier;
        }

        public void Animate(GameTime gameTime)
        {
            if (string.IsNullOrEmpty(currentAnimation))
                center = new Vector2(texture.Width / 2, texture.Height / 2);
            else if (CurrentFrameAnimation != null)
                center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);

            if (CurrentFrameAnimation == null)
            {
                if (animations.Count > 0)
                {
                    string[] sKeys = new string[animations.Count];
                    animations.Keys.CopyTo(sKeys, 0);
                    CurrentAnimation = sKeys[0];
                }
            }
            else
            {
                CurrentFrameAnimation.Animate(gameTime);

                if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
                    if (CurrentFrameAnimation.PlayCount > 0)
                        CurrentAnimation = CurrentFrameAnimation.NextAnimation;
            }

            if (timer.TimerOn)
            {
                if (CurrentFrameAnimation != null && idleAnimations.ContainsKey(currentAnimation))
                    if (CurrentFrameAnimation.PlayCount > 0)
                    {
                        currentAnimation = previousAnimation;
                        randomActive = false;
                        timer = new Timer(rand.Next((int)(minTime * 1000), (int)(maxTime * 1000)) / 1000f, true);
                        timer.TimerOn = true;
                    }

                if (timer.Finished && !idleAnimations.ContainsKey(currentAnimation))
                {
                    randomActive = true;
                    int selector = rand.Next(0, idleAnimations.Count);

                    string[] sKeys = new string[idleAnimations.Count];
                    idleAnimations.Keys.CopyTo(sKeys, 0);

                    previousAnimation = CurrentAnimation;
                    CurrentAnimation = sKeys[selector];
                }

                timer.Update(gameTime);
            }

            if (movementAnimation != null)
            {
                if (movementAnimations.ContainsKey(movementAnimation))
                {
                    if (movementAnimations[movementAnimation].frameVector.Count != 0)
                    {
                        movementAnimations[movementAnimation].Animate(gameTime);
                        int frame = movementAnimations[movementAnimation].CurrentFrame;
                        centerOffset = originalOffset + movementAnimations[movementAnimation].frameVector[frame];
                    }
                }
            }
            else
                centerOffset = originalOffset;


        }


        //public void Animate(GameTime gameTime)
        //{
        //    if (string.IsNullOrEmpty(currentAnimation))
        //        center = new Vector2(texture.Width / 2, texture.Height / 2);
        //    else
        //        center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);



        //    if (timer.TimerOn)
        //    {
        //        if (timer.Finished && timer.Count > 0)
        //        {
        //            int selector = rand.Next(0, idleAnimations.Count);

        //            string[] sKeys = new string[idleAnimations.Count];
        //            idleAnimations.Keys.CopyTo(sKeys, 0);

        //            if (string.IsNullOrEmpty(currentAnimation))
        //            {
        //                lastFrame = 0;
        //                beforeIdleAnimation = null;
        //            }
        //            else
        //            {
        //                lastFrame = CurrentFrameAnimation.CurrentFrame;
        //                if (animations.ContainsKey(currentAnimation))
        //                    beforeIdleAnimation = currentAnimation;
        //            }

        //            currentAnimation = sKeys[selector];

        //            float speedChange = rand.Next((int)((CurrentFrameAnimation.OriginalSpeed * 100) * 0.8), (int)((CurrentFrameAnimation.OriginalSpeed * 100) * 1.2)) / 100f;
        //            if (speedChange <= 0.03f)
        //                speedChange = 0.04f;
        //            CurrentFrameAnimation.AnimationSpeed = speedChange;

        //            float duration = (rand.Next((int)(minTime * 10), (int)(maxTime * 10)) / 10) + CurrentFrameAnimation.AnimationSpeed;
        //            timer.EditTimer(duration, true);

        //            if (string.IsNullOrEmpty(currentAnimation))
        //                center = new Vector2(texture.Width / 2, texture.Height / 2);
        //            else
        //                center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);
        //        }

        //        if (currentAnimation != null)
        //            if (idleAnimations.ContainsKey(currentAnimation) && CurrentFrameAnimation.PlayCount > 0)
        //            {
        //                CurrentFrameAnimation.CurrentFrame = lastFrame;
        //                CurrentFrameAnimation.PlayCount = 0;
        //                CurrentAnimation = beforeIdleAnimation;
        //            }

        //        if (string.IsNullOrEmpty(currentAnimation))
        //            center = new Vector2(texture.Width / 2, texture.Height / 2);
        //        else
        //            center = new Vector2(CurrentFrameAnimation.FrameRectangle.Width / 2, CurrentFrameAnimation.FrameRectangle.Height / 2);

        //        timer.Update(gameTime);
        //    }

        //    if (CurrentFrameAnimation == null)
        //    {
        //        if (animations.Count > 0)
        //        {
        //            string[] sKeys = new string[animations.Count];
        //            animations.Keys.CopyTo(sKeys, 0);
        //            currentAnimation = sKeys[0];
        //        }
        //        else
        //        {
        //            previousAnimation = currentAnimation;
        //            lastPosition = position;

        //            return;

        //        }

        //        centerOffset = originalOffset;
        //    }
        //    else
        //    {
        //        if (movementAnimations.ContainsKey(currentAnimation))
        //            centerOffset = originalOffset + movementAnimations[currentAnimation].frameVector[CurrentFrameAnimation.CurrentFrame];
        //        else
        //            centerOffset = originalOffset;
        //    }

        //    if (!String.IsNullOrEmpty(CurrentFrameAnimation.NextAnimation))
        //    {
        //        if (CurrentFrameAnimation.PlayCount > 0)
        //        {
        //            currentAnimation = CurrentFrameAnimation.NextAnimation;
        //        }
        //    }

        //    CurrentFrameAnimation.Animate(gameTime);

        //    previousAnimation = currentAnimation;
        //    lastPosition = position;
        //}



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
                spriteBatch.Draw(texture, position, CurrentFrameAnimation.FrameRectangle, Tint, rotation, center - centerOffset, scale, spriteEffect, 0);
            else
                spriteBatch.Draw(texture, position, null, Tint, rotation, center - centerOffset, scale, spriteEffect, 0);
        }
    }
}
