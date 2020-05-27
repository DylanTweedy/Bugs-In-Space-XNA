using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class MovementAnimation
    {
        #region Variables

        public List<Vector2> frameVector;
        int currentFrame;
        float originalSpeed;
        float animationSpeed;
        float frameTimer;
        int playCount;
        string nextAnimation;

        #endregion

        #region Properties
        
        public float OriginalSpeed
        {
            get { return originalSpeed; }
        }

        public float AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frameVector.Count - 1); }
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

        public MovementAnimation(List<Vector2> FrameVector, float FrameLength, string strNextAnimation)
        {
            frameVector = FrameVector;            
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
                currentFrame = (currentFrame + 1) % frameVector.Count;
                if (currentFrame == 0)
                {
                    playCount = (int)MathHelper.Min(playCount + 1, int.MaxValue);
                }
            }
        }
    }
}
