using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugsInSpace
{
    class CameraManager
    {
        #region Variables

        List<Camera> Cameras;
        List<string> CameraLayouts;
        List<Viewport> Viewports;
        List<Vector2> ViewportCenters;
        Viewport MainViewport;
        int PreviousCameraCount;
        int OriginalCameraLayout;
        bool LayoutUpdate;
        int PreviousFocusPoints;
        int PreviousCameraLayout;
        Vector2 TL;
        Vector2 TR;
        Vector2 BL;
        Vector2 BR;
        Vector2 CP;
        List<int> ControlNumber;
        List<bool> StartTimer;
        List<Vector2> TempFocus;
        List<float> TempRotation;
        List<float> TempZoom;
        List<float> ControlDuration;
        List<TimeSpan> Timer;
        List<TimeSpan> PreviousTimer;

        #endregion

        #region Properties

        public Vector2 CenterPoint
        {
            get { return CP; }
        }

        public Vector2 TopLeft
        {
            get { return TL; }
        }

        public Vector2 TopRight
        {
            get { return TR; }
        }

        public Vector2 BottomLeft
        {
            get { return BL; }
        }

        public Vector2 BottomRight
        {
            get { return BR; }
        }
        
        public List<Camera> CamerasRead
        {
            get { return Cameras; }
        }

        public List<Viewport> ViewportsRead
        {
            get { return Viewports; }
        }
        
        public bool UpdateLayout
        {
            get { return LayoutUpdate; }
            set { LayoutUpdate = value; }
        }
        
        public List<Vector2> FocusValues { get; set; }
        public List<float> RotationValues { get; set; }
        public List<float> ZoomValues { get; set; }
        public List<float> FocusSpeeds { get; set; }
        public List<float> RotationSpeeds { get; set; }
        public List<float> ZoomSpeeds { get; set; }
        public Vector2 ScreenCenter { get; set; }
        public bool ResetCamera { get; set; }
        public int CameraCount { get; set; }
        public int Spacing { get; set; }
        public int CameraLayout { get; set; }
        public int FocusPoints { get; set; }

        #endregion
        
        public void Initialize(Viewport viewport)
        {
            Cameras = new List<Camera>();
            CameraLayouts = new List<string>();
            Viewports = new List<Viewport>();
            ViewportCenters = new List<Vector2>();
            FocusValues = new List<Vector2>();
            RotationValues = new List<float>();
            ZoomValues = new List<float>();
            FocusSpeeds = new List<float>();
            RotationSpeeds = new List<float>();
            ZoomSpeeds = new List<float>();
            CameraCount = 1;
            OriginalCameraLayout = 0;
            CameraLayout = OriginalCameraLayout;
            LayoutUpdate = true;
            MainViewport = viewport;
            ScreenCenter = new Vector2(MainViewport.Width / 2, MainViewport.Height / 2);
            FocusPoints = 1;
            PreviousFocusPoints = 1;
            Spacing = 210;

            ControlNumber = new List<int>();
            StartTimer = new List<bool>();
            TempFocus = new List<Vector2>();
            TempRotation = new List<float>();
            TempZoom = new List<float>();
            ControlDuration = new List<float>();
            Timer = new List<TimeSpan>();
            PreviousTimer = new List<TimeSpan>();

            CameraLayouts.Add("Split Screen");
            CameraLayouts.Add("Dynamic");
        }

        public void Update(GameTime gameTime)
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

            if (CameraLayout == 0)
            {
                for (int i = 0; i < Cameras.Count; i++)
                {
                    Cameras[i].FocusPosition = FocusValues[i];
                    Cameras[i].RotationPosition = RotationValues[i];
                    Cameras[i].ZoomPosition = ZoomValues[i];
                }
            }
            
            if (CameraLayout == 1)
            {
                Dynamic();

                Cameras[0].FocusPosition = CP;
                Cameras[0].RotationPosition = RotationValues[0];
                Cameras[0].ZoomPosition = ZoomValues[0];
            }

            if (CameraLayout == 2)
            {
                Cameras[0].FocusPosition = new Vector2(FocusValues[0].X - (Spacing / 2), FocusValues[0].Y);
                Cameras[1].FocusPosition = new Vector2(FocusValues[0].X + (Spacing / 2), FocusValues[0].Y);

                for (int i = 0; i < Cameras.Count; i++)
                {
                    Cameras[i].RotationPosition = RotationValues[0];
                    Cameras[i].ZoomPosition = ZoomValues[0];
                }
            }

            #endregion

            for (int i = 0; i < Cameras.Count; i++)
            {
                if (ControlNumber[i] != 0)
                    Control(gameTime, i);

                Cameras[i].FocusSpeed = FocusSpeeds[i];
                Cameras[i].RotationSpeed = RotationSpeeds[i];
                Cameras[i].ZoomSpeed = ZoomSpeeds[i];

                Cameras[i].Update(gameTime);
            }
                        
            PreviousCameraLayout = CameraLayout;
        }

        #region Layouts

        private void SplitScreen()
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

                        ZoomValues[i] = 0.75f;
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

                        ZoomValues[i] = 0.6f;
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

                        ZoomValues[i] = 0.6f;
                    }
                }
            }
        }

        private void Dynamic()
        {
            int LX = 0;
            int RX = 0;
            int TY = 0;
            int BY = 0;

            for (int i = 0; i < FocusValues.Count; i++)
            {
                int Xpos = (int)FocusValues[i].X;
                int Ypos = (int)FocusValues[i].Y;

                if (i == 0)
                {
                    LX = Xpos;
                    RX = Xpos;
                    TY = Ypos;
                    BY = Ypos;
                }

                if (Xpos < LX)
                    LX = Xpos;
                if (Xpos > RX)
                    RX = Xpos;
                if (Ypos < TY)
                    TY = Ypos;
                if (Ypos > BY)
                    BY = Ypos;
            }

            TL = new Vector2(LX - 60, TY - 60);
            TR = new Vector2(RX + 60, TY - 60);
            BL = new Vector2(LX - 60, BY + 60);
            BR = new Vector2(RX + 60, BY + 60);

            CP = new Vector2(TL.X + ((TR.X - TL.X) / 2), TR.Y + ((BR.Y - TR.Y) / 2));

            float XPercentage = (float)(MainViewport.Width * 0.9) / (TR.X - TL.X);;
            float YPercentage = (float)(MainViewport.Height * 0.9) / (BR.Y - TL.Y);;

            if (TR.X - TL.X > (int)(MainViewport.Width * 0.9) || BR.Y - TL.Y > (int)(MainViewport.Height * 0.9))
            {
                if (XPercentage > YPercentage)
                {
                    ZoomValues[0] = YPercentage;
                }
                else
                {
                    ZoomValues[0] = XPercentage;
                }
            }
            else
                ZoomValues[0] = 1f;


            if (FocusPoints != PreviousFocusPoints && FocusPoints != 0)
            {
                FocusValues.Clear();

                for (int i = 0; i < FocusPoints; i++)
                {
                    FocusValues.Add(Vector2.Zero);
                }
            }

            PreviousFocusPoints = FocusPoints;
        }

        private void SideBySide()
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

                    ZoomValues[i] = 1f;
                }
            }
        }

        #endregion

        #region Camera Control

        public void Control(int controlNumber, float controlDuration, int c)
        {
            ControlNumber[c] = controlNumber;
            ControlDuration[c] = controlDuration;
            StartTimer[c] = true;
        }

        public void ControlInstant(float controlDuration, int c, Vector2 Focus, float Rotaion, float Zoom)
        {
            ControlNumber[c] = 1;
            ControlDuration[c] = controlDuration;
            TempFocus[c] = Focus;
            TempRotation[c] = Rotaion;
            TempZoom[c] = Zoom;
            StartTimer[c] = true;
        }

        public void ControlSlide(float controlDuration, int c, Vector2 Focus, float Rotaion, float Zoom)
        {
            ControlNumber[c] = 2;
            ControlDuration[c] = controlDuration;
            TempFocus[c] = Focus;
            TempRotation[c] = Rotaion;
            TempZoom[c] = Zoom;
            StartTimer[c] = true;
        }

        public void ControlShake(float controlDuration, int c, int intensity)
        {
            Cameras[c].Shake(controlDuration, intensity);
        }

        private void Control(GameTime gameTime, int c)
        {
            if (StartTimer[c])
            {
                PreviousTimer[c] = gameTime.TotalGameTime;
                Timer[c] = TimeSpan.FromSeconds(ControlDuration[c]);
                StartTimer[c] = false;
            }


            if (ControlNumber[c] == 1)
            {
                Cameras[c].NewLocation(TempFocus[c], TempRotation[c], TempZoom[c]);
            }

            if (ControlNumber[c] == 2)
            {
                Cameras[c].NewPosition(TempFocus[c], TempRotation[c], TempZoom[c]);
            }



            if (gameTime.TotalGameTime - PreviousTimer[c] > Timer[c])
            {
                TempFocus[c] = Vector2.Zero;
                TempRotation[c] = 0f;
                TempZoom[c] = 1f;
                ControlNumber[c] = 0;
                ControlDuration[c] = 0f;
            }
        }

        #endregion

        #region Camera Count

        private void AddCamera(Vector2 ViewportCenter)
        {
            int CameraNumber = Cameras.Count;

            Cameras.Add(new Camera());

            FocusValues.Add(ScreenCenter);
            RotationValues.Add(0f);
            ZoomValues.Add(1f);
            
            FocusSpeeds.Add(15f);
            RotationSpeeds.Add(4f);
            ZoomSpeeds.Add(15f);

            ControlNumber.Add(0);
            StartTimer.Add(false);
            TempFocus.Add(Vector2.Zero);
            TempRotation.Add(0f);
            TempZoom.Add(1f);
            ControlDuration.Add(0f);
            Timer.Add(TimeSpan.Zero);
            PreviousTimer.Add(TimeSpan.Zero);
            
            Cameras[CameraNumber].Initialize(ViewportCenter);

            LayoutUpdate = true;
        }

        public void AddCamera(Vector2 ViewportCenter, Vector2 Focus, float Rotation, float Zoom)
        {
            int CameraNumber = Cameras.Count;

            Cameras.Add(new Camera());

            FocusValues.Add(Focus);
            RotationValues.Add(Rotation);
            ZoomValues.Add(Zoom);

            FocusSpeeds.Add(1.25f);
            RotationSpeeds.Add(1.25f);
            ZoomSpeeds.Add(1.25f);

            Cameras[CameraNumber].Initialize(ViewportCenter);

            LayoutUpdate = true;

            CameraCount += 1;
        }

        private void RemoveCamera()
        {
            int CameraNumber = Cameras.Count - 1;

            Cameras.RemoveAt(CameraNumber);

            FocusValues.RemoveAt(CameraNumber);
            RotationValues.RemoveAt(CameraNumber);
            ZoomValues.RemoveAt(CameraNumber);

            FocusSpeeds.RemoveAt(CameraNumber);
            RotationSpeeds.RemoveAt(CameraNumber);
            ZoomSpeeds.RemoveAt(CameraNumber);

            LayoutUpdate = true;
        }

        public void RemoveCamera(int CameraNumber)
        {
            Cameras.RemoveAt(CameraNumber);

            FocusValues.RemoveAt(CameraNumber);
            RotationValues.RemoveAt(CameraNumber);
            ZoomValues.RemoveAt(CameraNumber);

            FocusSpeeds.RemoveAt(CameraNumber);
            RotationSpeeds.RemoveAt(CameraNumber);
            ZoomSpeeds.RemoveAt(CameraNumber);

            LayoutUpdate = true;

            CameraCount -= 1;
        }

        #endregion

        public bool IsInView(Vector2 position, Texture2D texture)
        {
            for (int i = 0; i < Cameras.Count; i++)
            {
                if (Cameras[i].IsInView(position, texture))
                    return true;
            }

            return false;
        }
    }
}
