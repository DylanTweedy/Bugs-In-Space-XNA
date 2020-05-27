using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class EngineLoader
    {
        static public void InitializeEngine(IntPtr WindowHandle, GraphicsDeviceManager Graphics, GraphicsDevice graphicsDevice)
        {
            EngineControls.Initialize();

            GlobalVariables.Initialize(graphicsDevice);
            ColorManager.Initialize();
            CameraManager.Initialize();
            GraphicsManager.Initialize(WindowHandle, Graphics);




            TestingClass.Initialize();
        }

        static private void LoadContent()
        {
        }

        static public void Update(GameTime gameTime, Rectangle ClientBounds, bool WindowFocus)
        {
            GlobalVariables.Update(gameTime);
            GraphicsManager.Update(ClientBounds, WindowFocus);


            TestingClass.Update();
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            GraphicsManager.ResizeWindow();

            TestingClass.Draw();
        }
    }
}
