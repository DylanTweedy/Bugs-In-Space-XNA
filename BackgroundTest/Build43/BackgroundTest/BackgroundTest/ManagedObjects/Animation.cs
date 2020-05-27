using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class Animation
    {
        #region Variables

        Rectangle firstFrame;
        int frameCount;
        int currentFrame;
        float originalSpeed;
        float animationSpeed;
        float frameTimer;
        int playCount;
        string nextAnimation;

        #endregion

        #region Properties

        //public int FrameCount
        //{
        //    get { return frameCount; }
        //    set { frameCount = value; }
        //}

        //public int FrameWidth
        //{
        //    get { return firstFrame.Width; }
        //}

        //public int FrameHeight
        //{
        //    get { return firstFrame.Height; }
        //}

        public float OriginalSpeed
        {
            get { return originalSpeed; }
        }

        public float AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }


        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(firstFrame.X + (firstFrame.Width * currentFrame), firstFrame.Y, firstFrame.Width, firstFrame.Height);
            }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frameCount - 1); }
        }

        public int PlayCount
        {
            get { return playCount; }
            set { playCount = value; }
        }

        public string NextAnimation
        {
            get { return nextAnimation; }
            set { nextAnimation = value; }
        }

        #endregion

        #region Constructors

        public Animation(int X, int Y, int Width, int Height, int Frames, float FrameLength, string strNextAnimation)
        {
            firstFrame = new Rectangle(X, Y, Width, Height);
            frameCount = Frames;
            animationSpeed = FrameLength;
            originalSpeed = FrameLength;
            nextAnimation = strNextAnimation;

            currentFrame = 0;
            frameTimer = 0.0f;
            playCount = 0;
        }

        #endregion

        public void Animate(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (frameTimer > animationSpeed)
            {
                frameTimer = 0.0f;
                currentFrame = (currentFrame + 1) % frameCount;
                if (currentFrame == 0)
                {
                    playCount = (int)MathHelper.Min(playCount + 1, int.MaxValue);
                }
            }
        }
    }
}
