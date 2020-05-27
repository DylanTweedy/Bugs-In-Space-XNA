using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    /// <summary>
    /// Contains all the drawing code for the entire game.
    /// </summary>
    static class MainDraw
    {
        static GraphicsDevice graphicsDevice;
        static Camera camera;

        /// <summary>
        /// Initializes the class GraphicsDevice
        /// </summary>
        static public void Initialize()
        {
            graphicsDevice = GlobalVariables.graphicsDevice;
        }

        static public void PreDraw()
        {
            TestingClass.PreDraw();
        }

        /// <summary>
        /// Main draw class. To be run last.
        /// </summary>
        static public void Draw()
        {
            PreDraw();

            //Generate a list of all the games cameras.
            List<Camera> cameraList = CameraManager.GetCameraList();

            for (int i = 0; i < cameraList.Count; i++)
            {
                //Set the current camera.
                camera = cameraList[i];

                //Clear the current cameras rendertargets.
                camera.ClearRenderTargets();
                camera.SetRenderTarget(RenderMode.Regular);
                graphicsDevice.Clear(Color.Black);
                
                DrawBack();
                DrawQuads();
                DrawFront();                
            }

            //Draw the main cameras to the GraphicsDevice and clear the main rendertarget.
            CameraManager.DrawMainCameras();

            DrawScreen();
        }

        /// <summary>
        /// Draws anything on the back layer.
        /// </summary>
        static private void DrawBack()
        {
            if (DebugOptions.DebugActive)
                DebugOptions.Draw(camera);

            DrawSprites.Begin(camera);

            //SkeletonTexture test = new SkeletonTexture("InfoBox", "ColorPallette");
            //test.Draw(InputManager.GetMousePosition(camera), Color.White, camera.Bounds.X + camera.Bounds.Y, 200f, SpriteEffects.None);

            SkeletonTexture test2 = new SkeletonTexture("Core", "Marker");
            test2.Draw(Vector2.Zero, Color.Gray, 0f, 1001f, SpriteEffects.None);
            
            DrawSprites.End();
        }

        /// <summary>
        /// Draws all quads in the middle layer.
        /// </summary>
        static private void DrawQuads()
        {
            #region Quad Test

            SkeletonQuad S1 = new SkeletonQuad(new Vector2(-200, -200), 100, Color.White, "Core", "Marker");
            SkeletonQuad S2 = new SkeletonQuad(new Vector2(0, 0), 100, Color.White, "InfoBox", "ColorBlack");
            SkeletonQuad S3 = new SkeletonQuad(new Vector2(200, 200), 100, Color.White, "InfoBox", "ColorPallette");


            //S1.AddSoloCamera("MainCamera2");
            //S1.AddSoloCamera("MainCamera3");

            //for (int n = 0; n < 1; n++)
            //{
            //    QuadManager.AddQuad(S1);
            //    QuadManager.AddQuad(S2);
            //    QuadManager.AddQuad(S3);
            //}

            #endregion

            //Quad Rendering
            if (DebugOptions.DebugActive && InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.RightControl))
                QuadManager.Draw(FillMode.WireFrame, camera);
            else
                QuadManager.Draw(FillMode.Solid, camera);
        }

        /// <summary>
        /// Draws the front layer.
        /// </summary>
        static private void DrawFront()
        {
            TestingClass.Draw(camera);
        }

        /// <summary>
        /// Draws anything to go over the entire screen.
        /// </summary>
        static private void DrawScreen()
        {
            #region Draw Border

            DrawSprites.Begin(null);

            BorderStyle border = new BorderStyle("", Color.White,
                new Location(16f));
            border.DrawBorders(GraphicsManager.VirtualResolution / 2f, GraphicsManager.VirtualResolution / 2f, 0f, 1f);

            DrawSprites.End();

            #endregion

            //Draw Debug
            if (DebugOptions.DebugActive)
                DebugOptions.Draw(null);


            #region Render Mouse
            DrawSprites.Begin(null);

            SkeletonTexture Cursor = new SkeletonTexture("TestImages", "Cursor");
            Cursor.Draw(
                InputManager.MousePosition + new Vector2(16f), 
                Color.White, 0f, 32f, SpriteEffects.None);
            
            DrawSprites.End();
            #endregion
        }
    }
}
