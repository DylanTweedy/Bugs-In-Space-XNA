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
        static public List<Camera> Cams;


        static public void Initialize()
        {
            Cams = new List<Camera>();
            Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
            //Cams.Add(new Camera(Vector3.Zero, Vector2.Zero, 0f, 1f));
            //Cams.Add(new Camera(Vector3.Zero, Vector2.Zero, 0f, 1f));
            //Cams.Add(new Camera(Vector3.Zero, Vector2.Zero, 0f, 1f));
            //Cams.Add(new Camera(Vector3.Zero, Vector2.Zero, 0f, 1f));
            //Cams.Add(new Camera(Vector3.Zero, Vector2.Zero, 0f, 1f, new Viewport(0, 0, 100, 100)));


            //Cams[Cams.Count - 1].subCameras.AddCamera(Vector2.Zero, 0f, 1f, new Rectangle(5, 5, 200, 200));

            SetupCameraSpace();
        }

        static public void SetupCameraSpace()
        {
            Vector2 Res = GraphicsManager.GameResolution;

            switch (Cams.Count)
            {
                case 1:
                    Cams[0].viewport = new Rectangle(0, 0, (int)Res.X, (int)Res.Y);
                    break;

                case 2:
                    if (EngineSettings.SplitScreenHorizontal)
                    {
                        Cams[0].viewport = new Rectangle(0, 0, (int)Res.X, (int)Res.Y / 2);
                        Cams[1].viewport = new Rectangle(0, (int)Res.Y / 2, (int)Res.X, (int)Res.Y / 2);
                    }
                    else
                    {
                        Cams[0].viewport = new Rectangle(0, 0, (int)Res.X / 2, (int)Res.Y);
                        Cams[1].viewport = new Rectangle((int)Res.X / 2, 0, (int)Res.X / 2, (int)Res.Y);
                    }
                    break;

                case 3:
                    if (EngineSettings.SplitScreenHorizontal)
                    {
                        Cams[0].viewport = new Rectangle(0, 0, (int)Res.X, (int)Res.Y / 2);
                        Cams[1].viewport = new Rectangle(0, (int)Res.Y / 2, (int)Res.X / 2, (int)Res.Y / 2);
                        Cams[2].viewport = new Rectangle((int)Res.X / 2, (int)Res.Y / 2, (int)Res.X / 2, (int)Res.Y / 2);
                    }
                    else
                    {
                        Cams[0].viewport = new Rectangle(0, 0, (int)Res.X / 2, (int)Res.Y);
                        Cams[1].viewport = new Rectangle((int)Res.X / 2, 0, (int)Res.X / 2, (int)Res.Y / 2);
                        Cams[2].viewport = new Rectangle((int)Res.X / 2, (int)Res.Y / 2, (int)Res.X / 2, (int)Res.Y / 2);
                    }
                    break;

                case 4:
                    Cams[0].viewport = new Rectangle(0, 0, (int)Res.X / 2, (int)Res.Y / 2);
                    Cams[1].viewport = new Rectangle((int)Res.X / 2, 0, (int)Res.X / 2, (int)Res.Y / 2);
                    Cams[2].viewport = new Rectangle(0, (int)Res.Y / 2, (int)Res.X / 2, (int)Res.Y / 2);
                    Cams[3].viewport = new Rectangle((int)Res.X / 2, (int)Res.Y / 2, (int)Res.X / 2, (int)Res.Y / 2);
                    break;
            }
        }

        static public void Update()
        {
            //if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Enter))
            //{
            //    EngineSettings.SplitScreenHorizontal = !EngineSettings.SplitScreenHorizontal;
            //    SetupCameraSpace();
            //}


            if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.D1))
            {
                Cams.Clear();
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                SetupCameraSpace();
            }
            else if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.D2))
            {
                Cams.Clear();
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                SetupCameraSpace();
            }
            else if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.D3))
            {
                Cams.Clear();
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                SetupCameraSpace();
            }
            else if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.D4))
            {
                Cams.Clear();
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                Cams.Add(new Camera(Vector2.Zero, 0f, 1f, InputType.None));
                SetupCameraSpace();
            }

            for (int i = 0; i < Cams.Count; i++)
            {
                Cams[i].Update();
            }
        }

        static public void AddCamera()
        {

        }
    }
}
