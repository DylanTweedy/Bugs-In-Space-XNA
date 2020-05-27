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
        //static public Keys GraphicsToggle = Keys.LeftAlt;

        static public KeyboardInput GraphicsFullScreen = new KeyboardInput(Keys.Enter);
        static public KeyboardInput GraphicsNextScreen = new KeyboardInput(Keys.Right);
        static public KeyboardInput GraphicsPreviousScreen = new KeyboardInput(Keys.Left);
        static public KeyboardInput GraphicsNextResolution = new KeyboardInput(Keys.Up);
        static public KeyboardInput GraphicsPreviousResolution = new KeyboardInput(Keys.Down);
        static public KeyboardInput GraphicsLockResolution = new KeyboardInput(Keys.L);

        static public void Initialize()
        {
            GraphicsFullScreen.AddModifier(Keys.LeftAlt);
            GraphicsNextScreen.AddModifier(Keys.LeftAlt);
            GraphicsPreviousScreen.AddModifier(Keys.LeftAlt);
            GraphicsNextResolution.AddModifier(Keys.LeftAlt);
            GraphicsPreviousResolution.AddModifier(Keys.LeftAlt);
            GraphicsLockResolution.AddModifier(Keys.LeftAlt);






        }
    }
}
