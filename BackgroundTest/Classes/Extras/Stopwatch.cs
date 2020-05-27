using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;


namespace BackgroundTest
{
    static class StopWatch
    {
        static Stopwatch stopWatch = new Stopwatch();

        static public void Start()
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        static public string Time()
        {
            stopWatch.Stop();

            return "Time elapsed: " + stopWatch.Elapsed;
        }        
    }
}
