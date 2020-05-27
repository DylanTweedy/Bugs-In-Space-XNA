using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class Timer
    {
        float duration;
        TimeSpan Time;
        TimeSpan PreviousTime;
        bool timerOn;
        bool finished;
        bool Loop;
        int count;
        
        public bool startTimer { get; set; }

        public bool TimerOn
        {
            get { return timerOn; }
            set { timerOn = value; }
        }

        public int Count
        {
            get { return count; }
        }

        public bool Finished
        {
            get { return finished; }
        }

        public Timer(float Duration, bool loop)
        {
            duration = Duration;
            Loop = loop;

            count = 0;
            startTimer = true;
            TimerOn = false;
            finished = false;
        }

        public void ResetTimer()
        {
            count = 0;
            startTimer = true;
            TimerOn = true;
            finished = false;
        }

        public void EditTimer(float Duration, bool loop)
        {
            duration = Duration;
            Loop = loop;
        }

        public void Update(GameTime gameTime)
        {
            if (startTimer)
            {
                PreviousTime = gameTime.TotalGameTime;
                Time = TimeSpan.FromSeconds(duration);
                startTimer = false;
                finished = false;
            }

            if (gameTime.TotalGameTime - PreviousTime > Time)
            {
                finished = true;
                count += 1;

                if (Loop)
                    startTimer = true;
                else
                    TimerOn = false;
            }
        }
    }
}
