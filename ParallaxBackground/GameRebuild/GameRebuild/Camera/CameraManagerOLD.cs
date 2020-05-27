using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRebuild
{
    static class CameraManagerOLD
    {
        #region Variables

        static List<CameraOLD> Cameras;
        static List<string> CameraLayouts;
        static List<Viewport> Viewports;
        static List<Vector2> ViewportCenters;
        static Viewport MainViewport;
        static int PreviousCameraCount;
        static int OriginalCameraLayout;
        static bool LayoutUpdate;
        static int PreviousFocusPoints;
        static int PreviousCameraLayout;
        static Vector2 TL;
        static Vector2 TR;
        static Vector2 BL;
        static Vector2 BR;
        static Vector2 CP;

        #endregion

        #region Properties

        public static Vector2 CenterPoint
        {
            get { return CP; }
        }

        public static List<CameraOLD> CamerasRead
        {
            get { return Cameras; }
        }

        public static List<Viewport> ViewportsRead
        {
            get { return Viewports; }
        }

        public static bool UpdateLayout
        {
            get { return LayoutUpdate; }
            set { LayoutUpdate = value; }
        }

        public static Vector2 ScreenCenter { get; set; }
        public static bool ResetCamera { get; set; }
        public static int CameraCount { get; set; }
        public static int Spacing { get; set; }
        public static int CameraLayout { get; set; }
        public static int FocusPoints { get; set; }

        #endregion
            
        public static void Initialize(Viewport viewport)
        {
            Cameras = new List<CameraOLD>();
            CameraLayouts = new List<string>();
            Viewports = new List<Viewport>();
            ViewportCenters = new List<Vector2>();
            CameraCount = 1;
            OriginalCameraLayout = 0;
            CameraLayout = OriginalCameraLayout;
            LayoutUpdate = true;
            MainViewport = viewport;
            ScreenCenter = new Vector2(MainViewport.Width / 2, MainViewport.Height / 2);
            FocusPoints = 1;
            PreviousFocusPoints = 1;
            Spacing = 210;
            
            CameraLayouts.Add("Split Screen");
            CameraLayouts.Add("Dynamic");

            Viewports.Add(new Viewport());
            AddCamera(ScreenCenter);            
        }

        public static void Update(GameTime gameTime)
        {
            if (PreviousCameraCount != CameraCount || PreviousCameraLayout != CameraLayout || ResetCamera || PreviousFocusPoints != FocusPoints)
                LayoutUpdate = true;

            #region LayoutUpdate

            if (LayoutUpdate)
            {
                if (ResetCamera)
                    CameraLayout = OriginalCameraLayout;
                
                if (CameraLayout == 0)
                {
                    if (FocusPoints > 0 && FocusPoints < 5)
                        CameraCount = FocusPoints;
                    else
                        CameraLayout = 1;
                }

                if (CameraLayout == 1)
                    CameraCount = 1;
                else if (CameraLayout == 2)
                    CameraCount = 2;

                int cCount = Cameras.Count;

                for (int i = 0; i < cCount; i++)
                {
                    if (Cameras.Count != 0)
                    {
                        RemoveCamera();
                    }
                }

                Viewports.Clear();
                ViewportCenters.Clear();

                for (int i = 0; i < CameraCount; i++)
                {
                    Viewports.Add(new Viewport());
                    Viewports[i] = MainViewport;

                    ViewportCenters.Add(new Vector2(Viewports[i].Width / 2, Viewports[i].Height / 2));
                }

                if (CameraLayout == 0)
                    SplitScreen();

                if (CameraLayout == 1)                
                    AddCamera(ViewportCenters[0]);

                if (CameraLayout == 2)
                    SideBySide();

                LayoutUpdate = false;
                PreviousCameraCount = CameraCount;
            }

            #endregion

            #region CameraLayout
            
            if (CameraLayout == 1)
            {
                Dynamic();

                Cameras[0].FocusDestination = CP;
            }

            #endregion

            for (int i = 0; i < Cameras.Count; i++)
            {
                Cameras[i].Update(gameTime);
            }

            PreviousFocusPoints = FocusPoints;
            PreviousCameraLayout = CameraLayout;
        }

        #region Layouts

        private static void SplitScreen()
        {
            if (CameraCount == 1)
            {
                AddCamera(ViewportCenters[0]);
            }
            else if (CameraCount == 2)
            {
                Viewport ViewportOne = MainViewport;
                Viewport ViewportTwo = MainViewport;

                ViewportOne.Width = ViewportOne.Width / 2;
                ViewportTwo.Width = ViewportTwo.Width / 2;

                ViewportTwo.X = ViewportOne.Width;

                Viewports[0] = ViewportOne;
                Viewports[1] = ViewportTwo;

                for (int i = 0; i < CameraCount; i++)
                {
                    if (Cameras.Count < CameraCount)
                    {
                        ViewportCenters[i] = new Vector2(Viewports[i].Width / 2, Viewports[i].Height / 2);

                        AddCamera(ViewportCenters[i]);
                    }
                }
            }
            else if (CameraCount == 3)
            {
                Viewport ViewportOne = MainViewport;
                Viewport ViewportTwo = MainViewport;
                Viewport ViewportThree = MainViewport;

                ViewportOne.Width = ViewportOne.Width / 2;
                ViewportTwo.Width = ViewportTwo.Width / 2;
                ViewportThree.Width = ViewportThree.Width / 2;

                ViewportTwo.Height = ViewportTwo.Height / 2;
                ViewportThree.Height = ViewportThree.Height / 2;

                ViewportTwo.X = ViewportOne.Width;
                ViewportThree.X = ViewportOne.Width;

                ViewportThree.Y = ViewportTwo.Height;

                Viewports[0] = ViewportOne;
                Viewports[1] = ViewportTwo;
                Viewports[2] = ViewportThree;

                for (int i = 0; i < CameraCount; i++)
                {
                    if (Cameras.Count < CameraCount)
                    {
                        ViewportCenters[i] = new Vector2(Viewports[i].Width / 2, Viewports[i].Height / 2);

                        AddCamera(ViewportCenters[i]);
                    }
                }
            }
            else if (CameraCount == 4)
            {
                Viewport ViewportOne = MainViewport;
                Viewport ViewportTwo = MainViewport;
                Viewport ViewportThree = MainViewport;
                Viewport ViewportFour = MainViewport;

                ViewportOne.Width = ViewportOne.Width / 2;
                ViewportTwo.Width = ViewportTwo.Width / 2;
                ViewportThree.Width = ViewportThree.Width / 2;
                ViewportFour.Width = ViewportFour.Width / 2;

                ViewportOne.Height = ViewportOne.Height / 2;
                ViewportTwo.Height = ViewportTwo.Height / 2;
                ViewportThree.Height = ViewportThree.Height / 2;
                ViewportFour.Height = ViewportFour.Height / 2;

                ViewportTwo.X = ViewportOne.Width;
                ViewportFour.X = ViewportThree.Width;

                ViewportThree.Y = ViewportOne.Height;
                ViewportFour.Y = ViewportTwo.Height;

                Viewports[0] = ViewportOne;
                Viewports[1] = ViewportTwo;
                Viewports[2] = ViewportThree;
                Viewports[3] = ViewportFour;

                for (int i = 0; i < CameraCount; i++)
                {
                    if (Cameras.Count < CameraCount)
                    {
                        ViewportCenters[i] = new Vector2(Viewports[i].Width / 2, Viewports[i].Height / 2);

                        AddCamera(ViewportCenters[i]);
                    }
                }
            }
        }

        private static void Dynamic()
        {
            int LX = 0;
            int RX = 0;
            int TY = 0;
            int BY = 0;
            
            TL = new Vector2(LX - 60, TY - 60);
            TR = new Vector2(RX + 60, TY - 60);
            BL = new Vector2(LX - 60, BY + 60);
            BR = new Vector2(RX + 60, BY + 60);

            CP = new Vector2(TL.X + ((TR.X - TL.X) / 2), TR.Y + ((BR.Y - TR.Y) / 2));

            float XPercentage = (float)(MainViewport.Width * 0.9) / (TR.X - TL.X);;
            float YPercentage = (float)(MainViewport.Height * 0.9) / (BR.Y - TL.Y);;
        }

        private static void SideBySide()
        {
            Viewport ViewportOne = MainViewport;
            Viewport ViewportTwo = MainViewport;

            ViewportOne.Width = ViewportOne.Width / 2;
            ViewportTwo.Width = ViewportTwo.Width / 2;

            ViewportTwo.X = ViewportOne.Width;

            Viewports[0] = ViewportOne;
            Viewports[1] = ViewportTwo;

            for (int i = 0; i < CameraCount; i++)
            {
                if (Cameras.Count < CameraCount)
                {
                    ViewportCenters[i] = new Vector2(Viewports[i].Width / 2, Viewports[i].Height / 2);

                    AddCamera(ViewportCenters[i]);
                }
            }
        }

        #endregion

        public static void ShakeCamera(float duration, int c, int intensity)
        {
            Cameras[c].Shake(duration, intensity);
        }

        #region Camera Count

        private static void AddCamera(Vector2 ViewportCenter)
        {
            int CameraNumber = Cameras.Count;

            Cameras.Add(new CameraOLD());
                                    
            Cameras[CameraNumber].Initialize(ViewportCenter);

            LayoutUpdate = true;
        }

        public static void AddCamera(Vector2 ViewportCenter, Vector2 Focus, float Rotation, float Zoom)
        {
            int CameraNumber = Cameras.Count;

            Cameras.Add(new CameraOLD());

            Cameras[CameraNumber].Initialize(ViewportCenter);

            LayoutUpdate = true;

            CameraCount += 1;
        }

        private static void RemoveCamera()
        {
            int CameraNumber = Cameras.Count - 1;

            Cameras.RemoveAt(CameraNumber);

            LayoutUpdate = true;
        }

        public static void RemoveCamera(int CameraNumber)
        {
            Cameras.RemoveAt(CameraNumber);

            LayoutUpdate = true;

            CameraCount -= 1;
        }

        #endregion

        public static bool IsInView(Vector2 position, int textureWidth, int textureHeight, float scale)
        {
            for (int i = 0; i < Cameras.Count; i++)
            {
                if (Cameras[i].IsInView(position, textureWidth, textureHeight, scale))
                    return true;
            }

            return false;
        }
    }
}
