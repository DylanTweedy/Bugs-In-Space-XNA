﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BugsInSpace
{
    class FrameAnimation : ICloneable
    {

        //Variables

        private Rectangle rectInitialFrame;
        private int iFrameCount = 1;
        private int iCurrentFrame = 0;
        private float fFrameLength = 0.2f;
        private float fFrameTimer = 0.0f;
        private int iPlayCount = 0;
        private string sNextAnimation = null;

        //Properties

        public int FrameCount
        {
            get { return iFrameCount; }
            set { iFrameCount = value; }
        }

        public float FrameLength
        {
            get { return fFrameLength; }
            set { fFrameLength = value; }
        }

        public int CurrentFrame
        {
            get { return iCurrentFrame; }
            set { iCurrentFrame = (int)MathHelper.Clamp(value, 0, iFrameCount - 1); }
        }

        public int FrameWidth
        {
            get { return rectInitialFrame.Width; }
        }

        public int FrameHeight
        {
            get { return rectInitialFrame.Height; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(rectInitialFrame.X + (rectInitialFrame.Width * iCurrentFrame), rectInitialFrame.Y, rectInitialFrame.Width, rectInitialFrame.Height);
            }
        }

        public int PlayCount
        {
            get { return iPlayCount; }
            set { iPlayCount = value; }
        }

        public string NextAnimation
        {
            get { return sNextAnimation; }
            set { sNextAnimation = value; }
        }

        //Constructors

        public FrameAnimation(Rectangle Firstframe, int Frames)
        {
            rectInitialFrame = Firstframe;
            iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
            fFrameLength = FrameLength;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames, float FrameLength, string strNextAnimation)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            iFrameCount = Frames;
            fFrameLength = FrameLength;
            sNextAnimation = strNextAnimation;
        }

        //Update

        public void Update(GameTime gameTime)
        {
            fFrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (fFrameTimer > fFrameLength)
            {
                fFrameTimer = 0.0f;
                iCurrentFrame = (iCurrentFrame + 1) % iFrameCount;
                if (iCurrentFrame == 0)
                {
                    iPlayCount = (int)MathHelper.Min(iPlayCount + 1, int.MaxValue);
                }
            }
        }

        //Methods

        object ICloneable.Clone()
        {
            return new FrameAnimation(this.rectInitialFrame.X, this.rectInitialFrame.Y, this.rectInitialFrame.Width, this.rectInitialFrame.Height, this.iFrameCount, this.fFrameLength, this.sNextAnimation);
        }
    }
}
