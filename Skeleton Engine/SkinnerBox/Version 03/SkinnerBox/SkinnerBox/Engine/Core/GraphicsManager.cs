using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    static class GraphicsManager
    {
        /// <summary>
        /// Data on connected screens.
        /// </summary>
        [Serializable()]
        private class ScreenDataSave
        {
            /// <summary>
            /// Settings for a screen.
            /// </summary>
            [Serializable()]
            public class ScreenSettingsData
            {
                public byte ScreenMode;

                public Rectangle WindowedBounds;
                public FormWindowState WindowState;

                public Vector2 ResolutionWhenLocked;
                public int ResolutionNumber;
                public bool ResolutionLocked;
                
                public List<Vector2> SupportedResolutions = new List<Vector2>();

                /// <summary>
                /// Sets the default data for a display.
                /// </summary>
                /// <param name="supportedResolutions">The range of supported resolutions for this display.</param>
                public ScreenSettingsData(List<Vector2> supportedResolutions)
                {
                    ScreenMode = 0;

                    WindowedBounds = Rectangle.Empty;
                    WindowState = FormWindowState.Maximized;

                    ResolutionWhenLocked = supportedResolutions[0];
                    ResolutionNumber = 0;
                    ResolutionLocked = false;

                    SupportedResolutions = supportedResolutions;
                }
            }

            public int ActiveDisplay;
            public int NumberOfDisplays;
            public List<Rectangle> CurrentResolutions = new List<Rectangle>();
            public List<ScreenSettingsData> ScreenSettings = new List<ScreenSettingsData>();

            /// <summary>
            /// Sets the basic screen data for the system.
            /// </summary>
            /// <param name="activeDisplay">The primary screen of the system.</param>
            /// <param name="numberOfDisplays">The number of screens attached to the system.</param>
            public void Initialize(int activeDisplay, int numberOfDisplays)
            {
                ActiveDisplay = activeDisplay;
                NumberOfDisplays = numberOfDisplays;
            }

            /// <summary>
            /// Adds a list of supported resolutions for a screen.
            /// </summary>
            /// <param name="SupportedResolutions">A list of resolutions as Vector2</param>
            public void AddScreenSettings(List<Vector2> SupportedResolutions)
            {
                ScreenSettings.Add(new ScreenSettingsData(SupportedResolutions));             
            }
        }

        static Form GameForm;
        static GraphicsDeviceManager Graphics;

        static int ActiveScreenData;
        static Screen[] Screens;
        static List<ScreenDataSave> ScreenData;
        static ScreenDataSave.ScreenSettingsData CurrentSettings;
        static Screen CurrentScreen;
        static ScreenDataSave CurrentSetup;
        static FormWindowState PreviousWindowState;

        static bool Updated = false;
        static bool ResolutionLocked = true;

        #region Public Variables

        /// <summary>
        /// Gets the resolution of the game window.
        /// </summary>
        static public Vector2 WindowResolution {get; private set;}
        /// <summary>
        /// Gets the virtual resolution of the game.
        /// </summary>
        static public Vector2 GameResolution { get; private set; }
        /// <summary>
        /// The virtual resolution to window resolution scale.
        /// </summary>
        static public Vector2 ScreenScale { get; private set; }
        /// <summary>
        /// Matrix scaled for virtual resolutions.
        /// </summary>
        static public Matrix ScreenMatrix { get; private set; }
        /// <summary>
        /// How much the viewport is offset by if a virtual resolution is enabled.
        /// </summary>
        static public Vector2 ViewportOffset { get; private set; }
        /// <summary>
        /// Bounds of the game window.
        /// </summary>
        static public Rectangle ClientBounds { get; private set; }
        /// <summary>
        /// True if the game window has focus.
        /// </summary>
        static public bool WindowFocus { get; private set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the class.
        /// </summary>
        /// <param name="WindowHandle">Window.Handle in the main game class.</param>
        /// <param name="graphics">The GraphicsDeviceManager.</param>
        static public void Initialize(IntPtr WindowHandle, GraphicsDeviceManager graphics)
        {
            //Loads the initial screen data.
            LoadScreenData();
            
            //Initial startup variables.
            ScreenScale = Vector2.One;
            ScreenMatrix = Matrix.CreateScale(Vector3.One);
            ViewportOffset = Vector2.Zero;
            ClientBounds = new Rectangle();
            WindowFocus = true;

            //Set the initial graphics device and window hangle.
            Graphics = graphics;
            var control = Control.FromHandle(WindowHandle);
            GameForm = control.FindForm();
            GameForm.MaximizeBox = true;

            //Set the active monitor to match loaded data.
            SetMonitor(true, CurrentSetup.ActiveDisplay);

            PreviousWindowState = GameForm.WindowState;
        }

        /// <summary>
        /// Loads screen data if available, if not, set the current screen data and save.
        /// </summary>
        static private void LoadScreenData()
        {            
            Screens = Screen.AllScreens;

            //If screen save data exists then load it.
            ScreenData = Load();
            if (ScreenData == null)
                ScreenData = new List<ScreenDataSave>();

            //Get current screen data.
            ScreenDataSave Data = new ScreenDataSave();
            for (int i = 0; i < Screens.Length; i++)
            {
                Data.CurrentResolutions.Add(new Rectangle(Screens[i].Bounds.X, Screens[i].Bounds.Y, Screens[i].Bounds.Width, Screens[i].Bounds.Height));

                if (Screens[i].Primary)
                {
                    Data.Initialize(i, Screens.Length);
                    break;
                }
            }

            //Check saved screen data against current screen data.
            //If a match is found then set the active screen data.
            bool LoadedData = false;
            if (ScreenData.Count != 0)
            {
                for (int i = 0; i < ScreenData.Count; i++)
                {
                    bool LoadStuff = true;
                    
                    for (int o = 0; o < Data.CurrentResolutions.Count; o++)
                        if (ScreenData[i].CurrentResolutions.Count < o)
                            if (Data.CurrentResolutions[o] != ScreenData[i].CurrentResolutions[o])
                                LoadStuff = false;

                    if (LoadStuff)
                    {
                        ActiveScreenData = i;
                        LoadedData = true;
                        CurrentSetup = ScreenData[ActiveScreenData];
                    }
                }
            }

            //If a match is not found then generate default data for new screens.
            if (!LoadedData)
            {
                ScreenData.Add(Data);
                ActiveScreenData = ScreenData.Count - 1;
                CurrentSetup = ScreenData[ActiveScreenData];

                //Go through each screen and give it default settings.
                for (int i = 0; i < Screens.Length; i++)
                {
                    List<Vector2> SupportedResolutions = new List<Vector2>();

                    //Gets a sorted list of supported resolutions.
                    #region Resolutions

                    int SelectedScreen = 0;

                    for (int o = 0; o < GraphicsAdapter.Adapters.Count; o++)
                    {
                        Vector2 adapter = new Vector2(GraphicsAdapter.Adapters[o].CurrentDisplayMode.Width, GraphicsAdapter.Adapters[o].CurrentDisplayMode.Height);
                        Vector2 screen = new Vector2(Screens[i].Bounds.Width, Screens[i].Bounds.Height);
                        if (screen == adapter)
                            SelectedScreen = o;
                    }

                    foreach (DisplayMode mode in GraphicsAdapter.Adapters[SelectedScreen].SupportedDisplayModes)
                        SupportedResolutions.Add(new Vector2(mode.Width, mode.Height));

                    SupportedResolutions = SupportedResolutions.Distinct().ToList();

                    List<Vector2> Sorted = new List<Vector2>();
                    List<float> Numbers = new List<float>();

                    for (int o = 0; o < SupportedResolutions.Count; o++)
                        Numbers.Add(SupportedResolutions[o].X * SupportedResolutions[o].Y);

                    for (int o = 0; o < SupportedResolutions.Count; o++)
                    {
                        int lowest = 0;
                        float lowestFloat = float.MaxValue;

                        for (int u = 0; u < Numbers.Count; u++)
                            if (Numbers[u] < lowestFloat)
                            {
                                lowestFloat = Numbers[u];
                                lowest = u;
                            }


                        Numbers[lowest] = float.MaxValue;

                        Sorted.Add(SupportedResolutions[lowest]);
                    }

                    SupportedResolutions = Sorted;

                    #endregion

                    CurrentSetup.AddScreenSettings(SupportedResolutions);
                }

                //Save final screen data.
                Save();
            }

            //Sets a reference tot he current save data and the 
            CurrentSettings = CurrentSetup.ScreenSettings[CurrentSetup.ActiveDisplay];
            CurrentScreen = Screens[CurrentSetup.ActiveDisplay];
        }

        #endregion

        #region Window Control

        /// <summary>
        /// Sets the display monitor on multi-monitor set ups.
        /// </summary>
        /// <param name="MoveWindow">If the window is to be moved programmatically set to true.</param>
        /// <param name="ActiveDisplay">The screen to switch to.</param>
        static private void SetMonitor(bool MoveWindow, int ActiveDisplay)
        {
            //If the selected display is not in range send an error to the log.
            if (ActiveDisplay < Screens.Length && ActiveDisplay >= 0)
            {
                //Set the new display data.
                CurrentSetup.ActiveDisplay = ActiveDisplay;
                CurrentSettings = CurrentSetup.ScreenSettings[CurrentSetup.ActiveDisplay];
                CurrentScreen = Screens[CurrentSetup.ActiveDisplay];

                //if the window is to be moved then move it to the desired location.
                if (MoveWindow)
                {
                    GameForm.WindowState = FormWindowState.Normal;
                    if (CurrentSettings.WindowedBounds != Rectangle.Empty)
                        GameForm.Location = new System.Drawing.Point(CurrentSettings.WindowedBounds.X, CurrentSettings.WindowedBounds.Y);
                    else
                        GameForm.Location = new System.Drawing.Point(CurrentScreen.Bounds.Location.X, CurrentScreen.Bounds.Location.Y);

                    SetScreenMode(CurrentSettings.ScreenMode);
                }
            }
            else
                GlobalVariables.ErrorLog.Add("Incorrectly Set Active Monitor: GraphicsManager");
        }

        /// <summary>
        /// Choose how the window is displayed.
        /// </summary>
        /// <param name="screenMode">
        /// <para>0 = Windowed</para>
        /// <para>1 = Fullscreen</para>
        /// </param>
        static private void SetScreenMode(byte screenMode)
        {
            //Make sure selection is in bounds.
            if (screenMode < 2)
            {
                switch (screenMode)
                {
                    //Windowed
                    case 0:
                        //Set the window state.
                        GameForm.WindowState = CurrentSettings.WindowState;
                        GameForm.FormBorderStyle = FormBorderStyle.Sizable;
                        ResolutionLocked = CurrentSettings.ResolutionLocked;

                        //Set the window to its saved state. If no state exists then set the buffer to the locked resolution.
                        if (GameForm.WindowState == FormWindowState.Normal && CurrentSettings.WindowedBounds != Rectangle.Empty)
                        {
                            GameForm.Location = new System.Drawing.Point(CurrentSettings.WindowedBounds.X, CurrentSettings.WindowedBounds.Y);
                            GameForm.Width = CurrentSettings.WindowedBounds.Width;
                            GameForm.Height = CurrentSettings.WindowedBounds.Height;

                            Graphics.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                            Graphics.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                        }
                        else
                        {
                            Graphics.PreferredBackBufferWidth = (int)CurrentSettings.ResolutionWhenLocked.X;
                            Graphics.PreferredBackBufferHeight = (int)CurrentSettings.ResolutionWhenLocked.Y;
                        }

                        Graphics.IsFullScreen = false;
                        break;

                    //Fullscreen Windowed
                    case 1:
                        //Set the window state.
                        GameForm.WindowState = FormWindowState.Normal;
                        GameForm.FormBorderStyle = FormBorderStyle.None;
                        ResolutionLocked = true;
                        GameResolution = CurrentSettings.ResolutionWhenLocked;

                        //Set the window to fill the screen.
                        GameForm.Location = new System.Drawing.Point(CurrentScreen.Bounds.X, CurrentScreen.Bounds.Y);

                        Graphics.PreferredBackBufferWidth = CurrentScreen.Bounds.Width;
                        Graphics.PreferredBackBufferHeight = CurrentScreen.Bounds.Height;
                        Graphics.IsFullScreen = false;
                        break;
                }

                //Update for saving.
                CurrentSettings.ScreenMode = screenMode;
                Updated = true;

                Graphics.ApplyChanges();
                ResizeWindow();
                SetScale();
            }
            else
                GlobalVariables.ErrorLog.Add("Incorrectly Set Screen Mode: GraphicsManager");
        }

        /// <summary>
        /// Checks for game window resizing.
        /// </summary>
        static public void ResizeWindow()
        {
            //If the window has changed size and is not minimized.
            if ((GameForm.ClientSize.Width != Graphics.PreferredBackBufferWidth || GameForm.ClientSize.Height != Graphics.PreferredBackBufferHeight) && GameForm.WindowState != FormWindowState.Minimized)
            {
                //If windowed.
                if (CurrentSettings.ScreenMode == 0)
                {
                    //if window is not maximized set window to saved size.
                    //if saved size is not set then set the window size to half the screen resolution.
                    if (GameForm.WindowState != PreviousWindowState && GameForm.WindowState != FormWindowState.Maximized && CurrentSettings.WindowedBounds != Rectangle.Empty)
                    {
                        GameForm.Location = new System.Drawing.Point(CurrentSettings.WindowedBounds.X, CurrentSettings.WindowedBounds.Y);
                        GameForm.Width = CurrentSettings.WindowedBounds.Width;
                        GameForm.Height = CurrentSettings.WindowedBounds.Height;

                        Graphics.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                        Graphics.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                    }
                    else if (CurrentSettings.WindowedBounds == Rectangle.Empty)
                    {
                        GameForm.Location = new System.Drawing.Point(
                            CurrentScreen.Bounds.X + (CurrentScreen.Bounds.Width / 4),
                            CurrentScreen.Bounds.Y + (CurrentScreen.Bounds.Height / 4));

                        GameForm.Width = CurrentScreen.Bounds.Width / 2;
                        GameForm.Height = CurrentScreen.Bounds.Height / 2;

                        Graphics.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                        Graphics.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                    }
                    else
                    {
                        Graphics.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                        Graphics.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                    }
                }
                else
                {
                    //if fullscreen set the backbuffer to meet the window resolution.
                    Graphics.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                    Graphics.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                }

                Graphics.ApplyChanges();
                SetScale();
            }
        }

        /// <summary>
        /// Sets the game resolution based on whether or not it is locked.
        /// </summary>
        static private void SetScale()
        {
            //Sets the window resolution.
            WindowResolution = new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            ScreenMatrix = Matrix.CreateScale(Vector3.One);
            ScreenScale = Vector2.One;
            ViewportOffset = Vector2.Zero;

            if (ResolutionLocked)
            {
                GameResolution = CurrentSettings.ResolutionWhenLocked;

                //get the window to game resolution ratio.
                ScreenScale = WindowResolution / GameResolution;
                if (ScreenScale.X < ScreenScale.Y)
                {
                    ScreenScale = new Vector2(ScreenScale.X);
                    ScreenMatrix = Matrix.CreateScale(new Vector3(ScreenScale.X, ScreenScale.Y, 1));
                }
                else
                {
                    ScreenScale = new Vector2(ScreenScale.Y);
                    ScreenMatrix = Matrix.CreateScale(new Vector3(ScreenScale.X, ScreenScale.Y, 1));
                }

                //Offset the viewport if game resolution doesn't fit in the window. 
                ViewportOffset = (WindowResolution / 2f) - ((GameResolution / 2f) * ScreenScale);
            }
            else
                GameResolution = WindowResolution;
            
            //Arrange the game cameras to fit in resolution.
            CameraManager.SetupCameraSpace();
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the game window.
        /// </summary>
        /// <param name="clientBounds">Window.ClientBounds in the main game class.</param>
        /// <param name="windowFocus">IsActive in the main game class.</param>
        static public void Update(Rectangle clientBounds, bool windowFocus)
        {
            UpdateActiveMonitor(); 
            UpdateControls();
               
            ResizeWindow();

            ClientBounds = clientBounds;
            WindowFocus = windowFocus;

            UpdateSaveData();

            PreviousWindowState = GameForm.WindowState;
        }

        /// <summary>
        /// Check if the game window has been moved to another monitor.
        /// </summary>
        static private void UpdateActiveMonitor()
        {
            //Split the main window into four rectangle.
            List<Rectangle> Rectangles = new List<Rectangle>();
            Rectangle Rect = new Rectangle(GameForm.Bounds.X, GameForm.Bounds.Y, GameForm.Bounds.Width, GameForm.Bounds.Height);
            Rect.Width /= 2;
            Rect.Height /= 2;

            Rectangles.Add(Rect);
            Rectangles.Add(new Rectangle(Rect.X + Rect.Width, Rect.Y, Rect.Width, Rect.Height));
            Rectangles.Add(new Rectangle(Rect.X, Rect.Y + Rect.Height, Rect.Width, Rect.Height));
            Rectangles.Add(new Rectangle(Rect.X + Rect.Width, Rect.Y + Rect.Height, Rect.Width, Rect.Height));

            int Intersections = 0;

            //if more than 2 rectangles intersect another screen then switch the active display.
            for (int i = 0; i < Screens.Length; i++)
            {
                Rectangle ScreenBounds = new Rectangle(Screens[i].Bounds.X, Screens[i].Bounds.Y, Screens[i].Bounds.Width, Screens[i].Bounds.Height);
                Intersections = 0;

                if (i != ScreenData[ActiveScreenData].ActiveDisplay)
                    for (int o = 0; o < Rectangles.Count; o++)
                    {
                        if (Rectangles[o].Intersects(ScreenBounds))
                            Intersections++;

                        if (Intersections > 2)
                        {
                            SetMonitor(false, i);
                            i = Screens.Length;
                            break;
                        }
                    }
            }

            #region Old (Use if the 4 rectangle thing malfunctions.
            //if (!GameForm.Bounds.IntersectsWith(CurrentScreen.Bounds))
            //    for (int i = 0; i < Screens.Length; i++)
            //        if (GameForm.Bounds.IntersectsWith(Screens[i].Bounds))
            //        {
            //            SetMonitor(false, i);
            //            break;
            //        }
            #endregion
        }

        /// <summary>
        /// Updates the Graphics Controls.
        /// </summary>
        static private void UpdateControls()
        {
            //Move window to previous screen.
            if (InputManager.KBButtonPressed(true, EngineControls.GraphicsPreviousScreen))
            {
                int Display = CurrentSetup.ActiveDisplay - 1;
                if (Display < 0)
                    Display = CurrentSetup.NumberOfDisplays - 1;

                SetMonitor(true, Display);
            }
            //Move window to next screen.
            else if (InputManager.KBButtonPressed(true, EngineControls.GraphicsNextScreen))
            {
                int Display = CurrentSetup.ActiveDisplay + 1;
                if (Display >= CurrentSetup.NumberOfDisplays)
                    Display = 0;

                SetMonitor(true, Display);
            }
            //Toggles fullscreen mode.
            else if (InputManager.KBButtonPressed(true, EngineControls.GraphicsFullScreen))
            {
                byte ScreenMode = CurrentSettings.ScreenMode;

                if (ScreenMode == 0)
                    ScreenMode = 1;
                else
                    ScreenMode = 0;

                SetScreenMode(ScreenMode);
            }
            //Toggles locked resolution in windowed mode.
            else if (CurrentSettings.ScreenMode == 0 && InputManager.KBButtonPressed(true, EngineControls.GraphicsLockResolution))
            {
                CurrentSettings.ResolutionLocked = !CurrentSettings.ResolutionLocked;
                SetScreenMode(CurrentSettings.ScreenMode);
                Updated = true;
            }
            //Increase resolution in locked mode.
            else if ((CurrentSettings.ResolutionLocked || CurrentSettings.ScreenMode == 1) && InputManager.KBButtonPressed(true, EngineControls.GraphicsNextResolution))
            {
                CurrentSettings.ResolutionNumber++;

                if (CurrentSettings.ResolutionNumber >= CurrentSettings.SupportedResolutions.Count)
                    CurrentSettings.ResolutionNumber = 0;

                CurrentSettings.ResolutionWhenLocked = CurrentSettings.SupportedResolutions[CurrentSettings.ResolutionNumber];

                SetScreenMode(CurrentSettings.ScreenMode);
                Updated = true;
            }
            //Decrease resolution in locked mode.
            else if ((CurrentSettings.ResolutionLocked || CurrentSettings.ScreenMode == 1) && InputManager.KBButtonPressed(true, EngineControls.GraphicsPreviousResolution))
            {
                CurrentSettings.ResolutionNumber--;

                if (CurrentSettings.ResolutionNumber < 0)
                    CurrentSettings.ResolutionNumber = CurrentSettings.SupportedResolutions.Count - 1;

                CurrentSettings.ResolutionWhenLocked = CurrentSettings.SupportedResolutions[CurrentSettings.ResolutionNumber];

                SetScreenMode(CurrentSettings.ScreenMode);
                Updated = true;
            }
        }

        /// <summary>
        /// Check for setting changes and save when required.
        /// </summary>
        static private void UpdateSaveData()
        {
            if (CurrentSettings.ScreenMode == 0)
            {
                //Save whether or not the window is maximized
                if (CurrentSettings.WindowState != GameForm.WindowState && GameForm.WindowState != FormWindowState.Minimized)
                {
                    CurrentSettings.WindowState = GameForm.WindowState;
                    Updated = true;
                }

                //Save the bounds of the window if it changes.
                if (GameForm.WindowState == FormWindowState.Normal)
                {
                    Rectangle WindowState = new Rectangle(GameForm.Location.X, GameForm.Location.Y, GameForm.Width, GameForm.Height);

                    if (CurrentSettings.WindowedBounds != WindowState)
                    {
                        CurrentSettings.WindowedBounds = WindowState;
                        Updated = true;
                    }
                }

                //Save whether or not the windowed state has locked resolution enabled.
                if (CurrentSettings.ResolutionLocked != ResolutionLocked)
                {
                    CurrentSettings.ResolutionLocked = ResolutionLocked;
                    Updated = true;
                }
            }

            //If changes tot he settings exist then save.
            if (Updated)
            {
                Save();
                Updated = false;
            }
        }

        #endregion
        
        #region File Management

        /// <summary>
        /// Saves current display settings.
        /// </summary>
        static private void Save()
        {
            SaveFile.Save(ScreenData, SaveFile.SettingsLocation, SaveFile.ScreenDataName);
        }

        /// <summary>
        /// Loads existing display settings, returns null if none exist.
        /// </summary>
        /// <returns></returns>
        static private List<ScreenDataSave> Load()
        {
            return (List<ScreenDataSave>)SaveFile.Load(SaveFile.SettingsLocation + SaveFile.ScreenDataName);
        }

        #endregion

        /// <summary>
        /// Returns a viewport scaled and translated to the fit game resolution if it is locked.
        /// </summary>
        /// <param name="viewport">Viewport to be scaled.</param>
        /// <returns></returns>
        static public Viewport ScaledViewport(Viewport viewport)
        {
            if (ResolutionLocked)
            {
                //Scale viewport.
                viewport.X = (int)(viewport.X * GraphicsManager.ScreenScale.X);
                viewport.Y = (int)(viewport.Y * GraphicsManager.ScreenScale.Y);
                viewport.Width = (int)(viewport.Width * GraphicsManager.ScreenScale.X);
                viewport.Height = (int)(viewport.Height * GraphicsManager.ScreenScale.Y);

                //Center viewport.
                viewport.X += (int)ViewportOffset.X;
                viewport.Y += (int)ViewportOffset.Y;
            }

            return viewport;
        }






        #region SkinnerBox

        static public void SetResolution(Vector2 resolution)
        {
            ResolutionLocked = true;

            CurrentSettings.ResolutionWhenLocked = resolution;
            GameResolution = resolution;


        }










        #endregion
    }
}
