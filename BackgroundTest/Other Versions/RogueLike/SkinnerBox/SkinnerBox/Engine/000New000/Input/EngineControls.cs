using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SkeletonEngine
{
    static class EngineControls
    {
        static public Keys DebugMode = Keys.F1;
        static public Keys DebugLockCursor = Keys.L;

        static public List<Keys> GraphicsFullScreen = new List<Keys>() { Keys.LeftAlt, Keys.Enter };
        static public List<Keys> GraphicsNextScreen = new List<Keys>() { Keys.LeftAlt, Keys.Right };
        static public List<Keys> GraphicsPreviousScreen = new List<Keys>() { Keys.LeftAlt, Keys.Left };
        
        //static public KeyboardInput MenuSelectUp = new KeyboardInput(Keys.Up);
        //static public KeyboardInput MenuSelectDown = new KeyboardInput(Keys.Down);
    }
}
