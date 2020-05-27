using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class CameraManager
    {
        static public GraphicsDevice graphicsDevice;
        static public SpriteBatch spriteBatch;

        static public List<Camera> MainCameras = new List<Camera>();
        static public bool SplitScreenHorizonal = true;
        
        static public void Initialize()
        {
            graphicsDevice = GlobalVariables.graphicsDevice;
            spriteBatch = GlobalVariables.spriteBatch;

            AddMainCamera(Vector2.Zero, 0f, 1f);
        }
        
        static public void Update()
        {
            #region Temporary Input Code

            if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Add)))
                AddMainCamera(Vector2.Zero, 0f, 1f);
            if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Subtract)))
                RemoveMainCamera(MainCameras.Count - 1);

            SplitScreenHorizonal = false;

            #endregion

            UpdateMainCameras();

            for (int i = 0; i < MainCameras.Count; i++)
            {
                MainCameras[i].Update();
                
                //Cams[i].SetRenderTarget(RenderMode.Regular);
            }
        }

        static private void UpdateMainCameras()
        {
            Vector2 Resolution = GraphicsManager.VirtualResolution;

            switch (MainCameras.Count)
            {
                case 1:
                    MainCameras[0].UpdateRenderTarget((int)Resolution.X, (int)Resolution.Y);
                    break;

                case 2:
                    if (SplitScreenHorizonal)
                        for (int i = 0; i < MainCameras.Count; i++)
                            MainCameras[i].UpdateRenderTarget((int)Resolution.X, (int)(Resolution.Y / 2f));
                    else
                        for (int i = 0; i < MainCameras.Count; i++)
                            MainCameras[i].UpdateRenderTarget((int)(Resolution.X / 2f), (int)Resolution.Y);
                    break;

                case 3:
                    for (int i = 0; i < MainCameras.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (SplitScreenHorizonal)
                                MainCameras[0].UpdateRenderTarget((int)Resolution.X, (int)(Resolution.Y / 2f));
                            else
                                MainCameras[0].UpdateRenderTarget((int)(Resolution.X / 2f), (int)Resolution.Y);
                        }
                        else
                            MainCameras[i].UpdateRenderTarget((int)(Resolution.X / 2f), (int)(Resolution.Y / 2f));
                    }
                    break;

                case 4:
                    for (int i = 0; i < MainCameras.Count; i++)
                        MainCameras[i].UpdateRenderTarget((int)(Resolution.X / 2f), (int)(Resolution.Y / 2f));
                    break;
            }
        }

        static public void AddMainCamera(Vector2 Position, float Rotation, float Zoom)
        {
            if (MainCameras.Count < 4)            
                MainCameras.Add(new Camera(Position, Rotation, Zoom, "MainCamera" + MainCameras.Count));
            
        }

        static public void RemoveMainCamera(int Camera)
        {
            if (MainCameras.Count > 1)
                if (MainCameras.Count > Camera)
                {
                    MainCameras.RemoveAt(Camera);
                }
        }

        static public void DrawMainCameras()
        {
            List<Vector2> Positions = new List<Vector2>();

            for (int i = 0; i < MainCameras.Count; i++)
                MainCameras[i].DrawFinalRender();

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Purple);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GraphicsManager.ScreenMatrix);

            Positions.Add(Vector2.Zero);

            switch (MainCameras.Count)
            {
                case 2:
                    if (SplitScreenHorizonal)                    
                        Positions.Add(new Vector2(0f, MainCameras[0].FinalRender.Height));
                    else
                        Positions.Add(new Vector2(MainCameras[0].FinalRender.Width, 0f));     
                    break;

                case 3:
                    if (SplitScreenHorizonal)
                    {
                        Positions.Add(new Vector2(0f, MainCameras[0].FinalRender.Height));
                        Positions.Add(new Vector2(MainCameras[1].FinalRender.Width, MainCameras[0].FinalRender.Height));
                    }
                    else
                    {
                        Positions.Add(new Vector2(MainCameras[0].FinalRender.Width, 0f));
                        Positions.Add(new Vector2(MainCameras[0].FinalRender.Width, MainCameras[1].FinalRender.Height));
                    }
                    break;

                case 4:
                    Positions.Add(new Vector2(MainCameras[0].FinalRender.Width, 0f));
                    Positions.Add(new Vector2(0f, MainCameras[0].FinalRender.Height));
                    Positions.Add(new Vector2(MainCameras[0].FinalRender.Width, MainCameras[0].FinalRender.Height));
                    break;
            }


            for (int i = 0; i < MainCameras.Count; i++)            
                spriteBatch.Draw(MainCameras[i].FinalRender, Positions[i], null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                
            spriteBatch.End();
        }
    }
}
