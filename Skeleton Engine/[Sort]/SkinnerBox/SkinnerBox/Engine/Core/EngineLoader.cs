using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class EngineLoader
    {
        static public void InitializeEngine(IntPtr WindowHandle, GraphicsDeviceManager Graphics, GraphicsDevice graphicsDevice, ContentManager Content)
        {
            EngineControls.Initialize();

            GlobalVariables.Initialize(graphicsDevice);
            ColorManager.Initialize();
            CameraManager.Initialize();
            GraphicsManager.Initialize(WindowHandle, Graphics);
            

            LightManager.Initialize(graphicsDevice);
            EffectsManager.LoadContent(Content);
            StringManager.LoadEmoticons();

            TestingClass.Initialize();
        }

        static private void LoadContent()
        {
        }

        static public void Update(GameTime gameTime, Rectangle ClientBounds, bool WindowFocus)
        {
            GlobalVariables.Update(gameTime);
            //GraphicsManager.SetResolution(new Vector2(1280, 720));
            GraphicsManager.SetResolution(new Vector2(1920, 1080));
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
