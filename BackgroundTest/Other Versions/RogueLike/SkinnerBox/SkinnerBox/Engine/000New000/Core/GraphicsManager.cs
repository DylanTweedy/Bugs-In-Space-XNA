using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SkeletonEngine
{
    [Serializable()]
    static class GraphicsManager
    {
        /// <summary>
        /// Returns the aspect ratio of a set resolution.
        /// </summary>
        /// <param name="x">Resolution width.</param>
        /// <param name="y">Resolution height.</param>
        /// <returns></returns>
        static private Vector2 GetAspectRatio(int x, int y)
        {
            int GCD = UsefulMethods.GlobalCommonDenominator(x, y);
            return new Vector2(x / GCD, y / GCD);
        }
        
        /// <summary>
        /// Lowers the virtual resolution if it goes above 4096 x 4096.
        /// </summary>
        /// <param name="x">Resolution width.</param>
        /// <param name="y">Resolution Height</param>
        /// <returns>Returns modified resolution.</returns>
        static private Vector2 GetVirtualResolution(float x, float y)
        {
            //If X is larger than 4096 then scale down.
            if (x > 4096 && x > y)
            {
                float Aspect = x / y;
                float Difference = x - 4096;

                return new Vector2(x - Difference, y - (Difference / Aspect));

            }
            //If Y is larger than 4096 then scale down.
            else if (y > 4096)
            {
                float Aspect = x / y;
                float Difference = y - 4096;

                return new Vector2(x - (Difference * Aspect), y - Difference);
            }
            else return new Vector2(x, y);
        }

        /// <summary>
        /// Hold all information for the display set-up.
        /// </summary>
        [Serializable()]        
        private class DisplayData
        {
            /// <summary>
            /// Settings whilst in full screen mode.
            /// </summary>
            [Serializable()]
            public class FullScreenData
            {
                public Vector2 ScreenResolution { get; private set; }
                public Vector2 MaxResolution { get; private set; }
                public Vector2 AspectRatio { get; private set; }
                public Vector2 VirtualResolution;
                public Vector2 VirtualAspectRatio;

                /// <summary>
                /// Sets the data to its default values.
                /// </summary>
                /// <param name="Resolution">The resolution of the current window.</param>
                public FullScreenData(Vector2 Resolution)
                {
                    ScreenResolution = Resolution;
                    MaxResolution = GetVirtualResolution(Resolution.X, Resolution.Y);
                    AspectRatio = GraphicsManager.GetAspectRatio((int)Resolution.X, (int)Resolution.Y);
                    VirtualResolution = MaxResolution;
                    VirtualAspectRatio = GraphicsManager.GetAspectRatio((int)MaxResolution.X, (int)MaxResolution.Y);   
                }
            }

            /// <summary>
            /// Settings whilst in windowed mode.
            /// </summary>
            [Serializable()]
            public class WindowedData
            {
                public Vector2 Resolution;
                public Vector2 MaxResolution { get; private set; }
                public Vector2 AspectRatio;
                public Vector2 VirtualResolution;
                public Vector2 VirtualAspectRatio;
                public FormWindowState WindowState;
                public Rectangle RestoredBounds;

                /// <summary>
                /// Sets the data to its default values.
                /// </summary>
                /// <param name="Resolution">The resolution of the current window.</param>
                public WindowedData(Vector2 resolution)
                {
                    Resolution = resolution;
                    MaxResolution = GetVirtualResolution(Resolution.X, Resolution.Y);
                    AspectRatio = GraphicsManager.GetAspectRatio((int)Resolution.X, (int)Resolution.Y);
                    VirtualResolution = MaxResolution;
                    VirtualAspectRatio = GraphicsManager.GetAspectRatio((int)MaxResolution.X, (int)MaxResolution.Y);   
                    WindowState = FormWindowState.Maximized;
                    RestoredBounds = Rectangle.Empty;
                }
            }

            /// <summary>
            /// Class to hold the physical display information.
            /// </summary>
            [Serializable()]
            public class Display
            {
                /// <summary>
                /// The bounds of the current display.
                /// </summary>
                public Rectangle Bounds { get; private set; }
                /// <summary>
                /// Whether or not the display is set as the primary.
                /// </summary>
                public bool Primary { get; private set; }
                /// <summary>
                /// The name of the device on the system.
                /// </summary>
                public string DeviceName { get; private set; }

                /// <summary>
                /// Sets the variables.
                /// </summary>
                /// <param name="bounds">The bounds of the current display. "Screen.Bounds"</param>
                /// <param name="primary">Whether or not the display is set as the primary.</param>
                /// <param name="deviceName">The name of the device on the system.</param>
                public Display(Rectangle bounds, bool primary, string deviceName)
                {
                    Bounds = bounds;
                    Primary = primary;
                    DeviceName = deviceName;
                }

                /// <summary>
                /// Checks whether or not two classes are duplicates.
                /// </summary>
                /// <param name="display">The class to check against.</param>
                /// <returns>Returns true if both classes are the same.</returns>
                public bool IsDuplicate(Display display)
                {
                    bool duplicate = true;

                    if (Bounds != display.Bounds)
                        duplicate = false;
                    if (Primary != display.Primary)
                        duplicate = false;
                    if (DeviceName != display.DeviceName)
                        duplicate = false;

                    return duplicate;
                }
            }

            /// <summary>
            /// Whether or not full screen mode is active.
            /// </summary>
            public bool FullScreen = false;
            /// <summary>
            /// The set virtual resolution. Vector2.Zero for dynamic.
            /// </summary>
            public Vector2 LockedResolution = Vector2.Zero;

            /// <summary>
            /// A list of the displays on the system.
            /// </summary>
            public List<Display> Displays = new List<Display>();

            /// <summary>
            /// The current active display.
            /// </summary>
            public int ActiveDisplay;
            /// <summary>
            /// The settings for full screen mode. (each entry in the list is for each display.) 
            /// </summary>
            public List<FullScreenData> FullScreenSettings = new List<FullScreenData>();
            /// <summary>
            /// The settings for windowed mode. (each entry in the list is for each display.) 
            /// </summary>
            public List<WindowedData> WindowedSettings = new List<WindowedData>();

            /// <summary>
            /// Sets the default screen data for the system.
            /// </summary>
            public DisplayData()
            {
                //For each screen create a display class. 
                Screen[] screens = Screen.AllScreens;
                
                for (int i = 0; i < screens.Length; i++)
                {
                    Displays.Add(new Display(new Rectangle(
                        screens[i].Bounds.X, screens[i].Bounds.Y, 
                        screens[i].Bounds.Width, screens[i].Bounds.Height), 
                        screens[i].Primary, screens[i].DeviceName));
                }

                //Set each display to its default settings.
                for (int i = 0; i < Displays.Count; i++)
                {
                    FullScreenSettings.Add(new FullScreenData(new Vector2(Displays[i].Bounds.Width, Displays[i].Bounds.Height)));
                    WindowedSettings.Add(new WindowedData(new Vector2(Displays[i].Bounds.Width, Displays[i].Bounds.Height)));
                    
                    if (Displays[i].Primary)
                        ActiveDisplay = i;
                }
            }
        }
        
        /// <summary>
        /// The game window.
        /// </summary>
        static Form GameForm;
        /// <summary>
        /// The previous state of the current window.
        /// </summary>
        static FormWindowState PreviousWindowState;
        /// <summary>
        /// The GraphicsDeviceManager class.
        /// </summary>
        static GraphicsDeviceManager graphicsDeviceManager;
        /// <summary>
        /// The GameWindow class.
        /// </summary>
        static GameWindow gameWindow;

        /// <summary>
        /// Currently active save data in use.
        /// </summary>
        static int ActiveScreenData;
        /// <summary>
        /// A list of the different display setups saved.
        /// </summary>
        static List<DisplayData> DisplayDataSaves;

        /// <summary>
        /// Currently active save data in use. For easier access.
        /// </summary>
        static DisplayData CurrentSetup;
        /// <summary>
        /// If the settings have changed set to true ready for saving.
        /// </summary>
        static bool Updated = false;

        /// <summary>
        /// Currently active full screen settings. For easier access.
        /// </summary>
        static DisplayData.FullScreenData FullScreenData;
        /// <summary>
        /// Currently active windowed settings. For easier access.
        /// </summary>
        static DisplayData.WindowedData WindowedData;


        #region Public Variables

        /// <summary>
        /// The current display in use.
        /// </summary>
        static public int ActiveDisplay { get; private set; }
        /// <summary>
        /// Whether or not full screen mode is active.
        /// </summary>
        static public bool FullScreen { get; private set; }

        /// <summary>
        /// The game window resolution.
        /// </summary>
        static public Vector2 WindowResolution { get; private set; }
        /// <summary>
        /// The game resolution.
        /// </summary>
        static public Vector2 VirtualResolution { get; private set; }

        /// <summary>
        /// The matrix for the screen.
        /// </summary>
        static public Matrix ScreenMatrix { get; private set; }

        static public Vector2 ViewportOffset { get; private set; }
        static public Vector2 ScreenScale { get; private set; }


        /// <summary>
        /// The bounds of the game window.
        /// </summary>
        static public Rectangle ClientBounds { get; private set; }
        /// <summary>
        /// whether or not the window is currently in focus.
        /// </summary>
        static public bool WindowFocus { get; private set; }

        #endregion
        
        #region Initialization

        /// <summary>
        /// Initializes the GraphicsManager.
        /// </summary>
        /// <param name="window">The GameWindow class.</param>
        /// <param name="graphics">The GraphicsDeviceManager class.</param>
        static public void Initialize()
        {
            //Set defaults.
            ActiveDisplay = 0;
            FullScreen = false;
            ScreenMatrix = Matrix.CreateScale(Vector3.One);
            ClientBounds = new Rectangle();
            WindowFocus = true;

            //Set default window states.
            gameWindow = GlobalVariables.gameWindow;
            graphicsDeviceManager = GlobalVariables.graphicsDeviceManager;
            var control = Control.FromHandle(gameWindow.Handle);
            GameForm = control.FindForm();
            GameForm.MaximizeBox = true;
            gameWindow.AllowUserResizing = true;

            //Loads the screen Data.
            LoadScreenData();
            
            //Set monitor to active display.
            SetMonitor(true, CurrentSetup.ActiveDisplay);
            
            PreviousWindowState = GameForm.WindowState;
            UpdateData();
        }

        /// <summary>
        /// Loads the data if it exitsts, otherwise, creates a new data set.
        /// </summary>
        static private void LoadScreenData()
        {
            //Load data from location.
            DisplayDataSaves = (List<DisplayData>)SaveFile.Load(SaveFile.SettingsLocation + "ScreenData");

            //If loaded data does not exist create a new list.
            if (DisplayDataSaves == null)
                DisplayDataSaves = new List<DisplayData>();

            DisplayData Data = new DisplayData();
            bool SaveExists = false;

            //If a save matching the current setup exists then set it as the active data set.
            for (int i = 0; i < DisplayDataSaves.Count; i++)
            {
                bool Dupliacte = true;

                if (DisplayDataSaves[i].Displays.Count == Data.Displays.Count)
                    for (int o = 0; o < DisplayDataSaves[i].Displays.Count; o++)
                        if (!DisplayDataSaves[i].Displays[o].IsDuplicate(Data.Displays[o]))
                            Dupliacte = false;

                if (Dupliacte)
                {
                    SaveExists = true;
                    CurrentSetup = DisplayDataSaves[i];
                    ActiveScreenData = i;
                }
            }

            //If a save does not exist then create a new set with default values.
            if (!SaveExists)
            {
                DisplayDataSaves.Add(Data);
                CurrentSetup = DisplayDataSaves[DisplayDataSaves.Count - 1];
                ActiveScreenData = DisplayDataSaves.Count - 1;
                GameForm.WindowState = FormWindowState.Maximized;
            }
                        
            Save();
        }

        #endregion

        #region Window Control

        /// <summary>
        /// Set the current active monitor.
        /// </summary>
        /// <param name="MoveWindow">Whether or not move the window to its saved location.</param>
        /// <param name="activeDisplay">The new active display.</param>
        static private void SetMonitor(bool MoveWindow, int activeDisplay)
        {
            //Makes sure the display is in bounds.
            if (activeDisplay >= CurrentSetup.Displays.Count)
                activeDisplay = 0;
            else if (activeDisplay < 0)
                activeDisplay = CurrentSetup.Displays.Count - 1;

            CurrentSetup.ActiveDisplay = activeDisplay;

            //Change data sets to match new display.
            FullScreenData = CurrentSetup.FullScreenSettings[activeDisplay];
            WindowedData = CurrentSetup.WindowedSettings[activeDisplay];

            if (MoveWindow)
            {
                //Move window to displays location.
                if (CurrentSetup.FullScreen)
                {
                    GameForm.Location = new System.Drawing.Point(CurrentSetup.Displays[activeDisplay].Bounds.Location.X, CurrentSetup.Displays[activeDisplay].Bounds.Location.Y);
                }
                else
                {
                    //Set state to normal before moving otherwise buggy behaviour occurs.
                    GameForm.WindowState = FormWindowState.Normal;

                    //If windowed settings exist then set display to saved location.
                    if (CurrentSetup.WindowedSettings[activeDisplay].RestoredBounds != Rectangle.Empty)
                        GameForm.Location = new System.Drawing.Point(CurrentSetup.WindowedSettings[activeDisplay].RestoredBounds.X, CurrentSetup.WindowedSettings[activeDisplay].RestoredBounds.Y);
                    else                    
                        GameForm.Location = new System.Drawing.Point(CurrentSetup.Displays[activeDisplay].Bounds.Location.X, CurrentSetup.Displays[activeDisplay].Bounds.Location.Y);
                    
                    //Set window to saved state.
                    GameForm.WindowState = CurrentSetup.WindowedSettings[activeDisplay].WindowState;
                }

                SetScreenMode(CurrentSetup.FullScreen);
            }
            
            //Set updated to true to save.
            Updated = true;
        }

        /// <summary>
        /// Sets whether the game is in full screen mode or not.
        /// </summary>
        /// <param name="fullScreen">True for full screen mode.</param>
        static private void SetScreenMode(bool fullScreen)
        {
            DisplayData.Display CurrentScreen = CurrentSetup.Displays[CurrentSetup.ActiveDisplay];

            if (!fullScreen)
            {
                DisplayData.WindowedData Data = CurrentSetup.WindowedSettings[CurrentSetup.ActiveDisplay];

                //Set the window state.
                GameForm.WindowState = Data.WindowState;
                GameForm.FormBorderStyle = FormBorderStyle.Sizable;

                //Set the window to its saved state. If no state exists then set the buffer to the locked resolution.
                if (GameForm.WindowState == FormWindowState.Normal && Data.RestoredBounds != Rectangle.Empty)
                {
                    GameForm.Location = new System.Drawing.Point(Data.RestoredBounds.X, Data.RestoredBounds.Y);
                    GameForm.Width = Data.RestoredBounds.Width;
                    GameForm.Height = Data.RestoredBounds.Height;

                    graphicsDeviceManager.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                    graphicsDeviceManager.PreferredBackBufferHeight = GameForm.ClientSize.Height;

                    graphicsDeviceManager.ApplyChanges();

                    GameForm.Location = new System.Drawing.Point(Data.RestoredBounds.X, Data.RestoredBounds.Y);
                    GameForm.Width = Data.RestoredBounds.Width;
                    GameForm.Height = Data.RestoredBounds.Height;
                }
                else
                {
                    graphicsDeviceManager.PreferredBackBufferWidth = (int)Data.Resolution.X;
                    graphicsDeviceManager.PreferredBackBufferHeight = (int)Data.Resolution.Y;
                }
            }
            else
            {
                DisplayData.FullScreenData Data = CurrentSetup.FullScreenSettings[CurrentSetup.ActiveDisplay];

                //Set the window state.
                GameForm.WindowState = FormWindowState.Normal;
                GameForm.FormBorderStyle = FormBorderStyle.None;
                
                //Set the resolution.
                VirtualResolution = Data.VirtualResolution;
                
                GameForm.Location = new System.Drawing.Point(CurrentScreen.Bounds.X, CurrentScreen.Bounds.Y);

                graphicsDeviceManager.PreferredBackBufferWidth = CurrentScreen.Bounds.Width;
                graphicsDeviceManager.PreferredBackBufferHeight = CurrentScreen.Bounds.Height;
            }

            CurrentSetup.FullScreen = fullScreen;
            Updated = true;

            //Apply changes and update.
            graphicsDeviceManager.ApplyChanges();
            SetScale();
        }
             
        /// <summary>
        /// Updates the BackBuffer if window has been resized.
        /// </summary>
        static public void ResizeWindow()
        {
            DisplayData.Display CurrentScreen = CurrentSetup.Displays[CurrentSetup.ActiveDisplay];

            if ((GameForm.ClientSize.Width != graphicsDeviceManager.PreferredBackBufferWidth || GameForm.ClientSize.Height != graphicsDeviceManager.PreferredBackBufferHeight) && GameForm.WindowState != FormWindowState.Minimized)
            {
                if (!CurrentSetup.FullScreen)
                {
                    //If windowstate has changed to restored set window to saved bounds.
                    if (GameForm.WindowState == FormWindowState.Normal && GameForm.WindowState != PreviousWindowState && WindowedData.RestoredBounds != Rectangle.Empty)
                    {
                        GameForm.Location = new System.Drawing.Point(WindowedData.RestoredBounds.X, WindowedData.RestoredBounds.Y);
                        GameForm.Width = WindowedData.RestoredBounds.Width;
                        GameForm.Height = WindowedData.RestoredBounds.Height;

                        graphicsDeviceManager.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                        graphicsDeviceManager.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                    }
                    //If saved bounds do not exist set window to center.
                    else if (GameForm.WindowState == FormWindowState.Normal && WindowedData.RestoredBounds == Rectangle.Empty)
                    {
                        GameForm.Location = new System.Drawing.Point(
                            CurrentScreen.Bounds.X + (CurrentScreen.Bounds.Width / 4),
                            CurrentScreen.Bounds.Y + (CurrentScreen.Bounds.Height / 4));

                        GameForm.Width = (int)(CurrentScreen.Bounds.Width * 0.75f);
                        GameForm.Height = (int)(CurrentScreen.Bounds.Height * 0.75f);

                        graphicsDeviceManager.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                        graphicsDeviceManager.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                    }
                    else
                    {
                        graphicsDeviceManager.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                        graphicsDeviceManager.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                    }

                    WindowedData.Resolution = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight);
                }
                else
                {
                    graphicsDeviceManager.PreferredBackBufferWidth = GameForm.ClientSize.Width;
                    graphicsDeviceManager.PreferredBackBufferHeight = GameForm.ClientSize.Height;
                }

                graphicsDeviceManager.ApplyChanges();
                Updated = true;
            }

            SetScale();
        }

        /// <summary>
        /// Sets the camera matix and scale.
        /// </summary>
        static private void SetScale()
        {
            WindowResolution = new Vector2(graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight);
            ScreenMatrix = Matrix.CreateScale(Vector3.One);
            ScreenScale = Vector2.One;
            ViewportOffset = Vector2.Zero;

            //Get the window to game resolution ratio.
            ScreenScale = WindowResolution / VirtualResolution;

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
            ViewportOffset = (WindowResolution / 2f) - ((VirtualResolution / 2f) * ScreenScale);

            //Update the matrix to account for the offset.
            ScreenMatrix = ScreenMatrix * Matrix.CreateTranslation(new Vector3(ViewportOffset, 0f));
        }

        #endregion

        #region Update

        /// <summary>
        /// Main update method.
        /// </summary>
        /// <param name="windowFocus">Whether or not the window has focus. Game1 IsActive parameter.</param>
        static public void Update(bool windowFocus)
        {
            UpdateControls();
            UpdateActiveMonitor(); 
               
            ResizeWindow();

            ClientBounds = gameWindow.ClientBounds;
            WindowFocus = windowFocus;

            UpdateSaveData();

            PreviousWindowState = GameForm.WindowState;

            SetScale();
            UpdateData();

            UpdateSaveData();
        }

        /// <summary>
        /// Update class parameters to match CurrentSetup.
        /// </summary>
        static private void UpdateData()
        {
            ActiveDisplay = CurrentSetup.ActiveDisplay;
            FullScreen = CurrentSetup.FullScreen;

            WindowedData = CurrentSetup.WindowedSettings[ActiveDisplay];
            FullScreenData = CurrentSetup.FullScreenSettings[ActiveDisplay];

            if (CurrentSetup.LockedResolution != Vector2.Zero)
            {
                WindowedData.VirtualResolution = CurrentSetup.LockedResolution;
                FullScreenData.VirtualResolution = CurrentSetup.LockedResolution;
            }
            else
            {
                WindowedData.VirtualResolution = WindowedData.Resolution;
                FullScreenData.VirtualResolution = FullScreenData.ScreenResolution;
            }

            WindowedData.AspectRatio = GetAspectRatio((int)WindowedData.Resolution.X, (int)WindowedData.Resolution.Y);
            WindowedData.VirtualAspectRatio = GetAspectRatio((int)WindowedData.VirtualResolution.X, (int)WindowedData.VirtualResolution.Y);
            FullScreenData.VirtualAspectRatio = GetAspectRatio((int)FullScreenData.VirtualResolution.X, (int)FullScreenData.VirtualResolution.Y);

            if (FullScreen)
            {
                VirtualResolution = CurrentSetup.FullScreenSettings[ActiveDisplay].VirtualResolution;
            }
            else
            {
                WindowedData.WindowState = GameForm.WindowState;
                if (WindowedData.WindowState == FormWindowState.Normal)
                {
                    //If restored bounds has changed then update for saving.
                    if (WindowedData.RestoredBounds != new Rectangle(GameForm.Bounds.X, GameForm.Bounds.Y, GameForm.Bounds.Width, GameForm.Bounds.Height))
                    {
                        WindowedData.RestoredBounds = new Rectangle(GameForm.Bounds.X, GameForm.Bounds.Y, GameForm.Bounds.Width, GameForm.Bounds.Height);
                        Updated = true;
                    }
                }

                VirtualResolution = CurrentSetup.WindowedSettings[ActiveDisplay].VirtualResolution;
            }
        }

        /// <summary>
        /// Checks to see if the game window has been moved to a new window.
        /// </summary>
        static private void UpdateActiveMonitor()
        {
            if (GameForm.WindowState == FormWindowState.Normal)
            {
                //Split window into 4 rectangles.
                List<Rectangle> Rectangles = new List<Rectangle>();
                Rectangle Rect = new Rectangle(GameForm.Bounds.X, GameForm.Bounds.Y, GameForm.Bounds.Width, GameForm.Bounds.Height);
                Rect.Width /= 2;
                Rect.Height /= 2;

                Rectangles.Add(Rect);
                Rectangles.Add(new Rectangle(Rect.X + Rect.Width, Rect.Y, Rect.Width, Rect.Height));
                Rectangles.Add(new Rectangle(Rect.X, Rect.Y + Rect.Height, Rect.Width, Rect.Height));
                Rectangles.Add(new Rectangle(Rect.X + Rect.Width, Rect.Y + Rect.Height, Rect.Width, Rect.Height));

                int Intersections = 0;

                //If more than 2 rectangles intersect with another window then set active display to new monitor.
                for (int i = 0; i < CurrentSetup.Displays.Count; i++)
                {
                    Rectangle ScreenBounds = new Rectangle(
                        CurrentSetup.Displays[i].Bounds.X, CurrentSetup.Displays[i].Bounds.Y,
                        CurrentSetup.Displays[i].Bounds.Width, CurrentSetup.Displays[i].Bounds.Height);
                    Intersections = 0;

                    if (i != CurrentSetup.ActiveDisplay)
                        for (int o = 0; o < Rectangles.Count; o++)
                        {
                            if (Rectangles[o].Intersects(ScreenBounds))
                                Intersections++;

                            if (Intersections > 2)
                            {
                                SetMonitor(false, i);
                                i = CurrentSetup.Displays.Count;
                                break;
                            }
                        }
                }
            }
        }

        /// <summary>
        /// Update input controls.
        /// </summary>
        static private void UpdateControls()
        {
            //Move window to previous screen.
            if (InputManager.KBPressed(true, EngineControls.GraphicsPreviousScreen))            
                SetMonitor(true, CurrentSetup.ActiveDisplay - 1);            
            //Move window to next screen.
            else if (InputManager.KBPressed(true, EngineControls.GraphicsNextScreen))
                SetMonitor(true, CurrentSetup.ActiveDisplay + 1);
            //Toggles fullscreen mode.
            else if (InputManager.KBPressed(true, EngineControls.GraphicsFullScreen))
                SetScreenMode(!CurrentSetup.FullScreen);
        }

        /// <summary>
        /// If updated will save the screen data.
        /// </summary>
        static private void UpdateSaveData()
        {
            //If changes to the settings exist then save.
            if (Updated)
            {
                Save();
                Updated = false;
            }
        }

        #endregion
        
        /// <summary>
        /// Saves the screen data.
        /// </summary>
        static private void Save()
        {
            #region SaveData
            SaveFile.Save(DisplayDataSaves, SaveFile.SettingsLocation, "ScreenData");
            #endregion
        }
        
        //static public Viewport ScaledViewport(Viewport viewport)
        //{
        //        //Scale viewport.
        //        viewport.X = (int)(viewport.X * GraphicsManager.ScreenScale.X);
        //        viewport.Y = (int)(viewport.Y * GraphicsManager.ScreenScale.Y);
        //        viewport.Width = (int)(viewport.Width * GraphicsManager.ScreenScale.X);
        //        viewport.Height = (int)(viewport.Height * GraphicsManager.ScreenScale.Y);

        //        //Center viewport.
        //        viewport.X += (int)ViewportOffset.X;
        //        viewport.Y += (int)ViewportOffset.Y;

        //    return viewport;
        //}
        
        /// <summary>
        /// Returns a list of information about the active display.
        /// </summary>
        /// <returns></returns>
        static public List<TextLine> Information()
        {
            List<TextLine> Info = new List<TextLine>();

            Info.Add(new TextLine("Fullscreen", "" + FullScreen, true, ": "));
            Info.Add(new TextLine("Display Count", "" + CurrentSetup.Displays.Count, true, ": "));
            Info.Add(new TextLine("Active Display", "" + (CurrentSetup.ActiveDisplay + 1) + "/" + CurrentSetup.Displays.Count + " '" + CurrentSetup.ActiveDisplay + "'", true, ": "));

            if (FullScreen)
            {
                DisplayData.FullScreenData data = CurrentSetup.FullScreenSettings[ActiveDisplay];

                Info.Add(new TextLine("Resolution", "" + data.ScreenResolution.X + " x " + data.ScreenResolution.Y, true, ": "));
                Info.Add(new TextLine("Aspect Ratio", "" + data.AspectRatio.X + ":" + data.AspectRatio.Y, true, ": "));
                Info.Add(new TextLine("Virtual Resolution", "" + data.VirtualResolution.X + " x " + data.VirtualResolution.Y, true, ": "));
                Info.Add(new TextLine("Virtual Aspect Ratio", "" + data.VirtualAspectRatio.X + ":" + data.VirtualAspectRatio.Y, true, ": "));
                Info.Add(new TextLine("Max Virtual Resolution", "" + data.MaxResolution.X + " x " + data.MaxResolution.Y, true, ": "));
            }
            else
            {
                DisplayData.WindowedData data = CurrentSetup.WindowedSettings[ActiveDisplay];

                Info.Add(new TextLine("Resolution", "" + data.Resolution.X + " x " + data.Resolution.Y, true, ": "));
                Info.Add(new TextLine("Aspect Ratio", "" + data.AspectRatio.X + ":" + data.AspectRatio.Y, true, ": "));
                Info.Add(new TextLine("Virtual Resolution", "" + data.VirtualResolution.X + " x " + data.VirtualResolution.Y, true, ": "));
                Info.Add(new TextLine("Virtual Aspect Ratio", "" + data.VirtualAspectRatio.X + ":" + data.VirtualAspectRatio.Y, true, ": "));
                Info.Add(new TextLine("Max Virtual Resolution", "" + data.MaxResolution.X + " x " + data.MaxResolution.Y, true, ": "));
                Info.Add(new TextLine("Window State", "" + data.WindowState, true, ": "));
                Info.Add(new TextLine("Restored Position", "" + data.RestoredBounds.X + " , " + data.RestoredBounds.Y, true, ": "));
                Info.Add(new TextLine("Restored Size", "" + data.RestoredBounds.Width + " x " + data.RestoredBounds.Height, true, ": "));
            }

            return Info;
        }

        /// <summary>
        /// Sets the virtual resolution. Vector2.Zero for resolution equal to the window size.
        /// </summary>
        /// <param name="resolution">Desired resolution.</param>
        static public void SetResolution(Vector2 resolution)
        {
            CurrentSetup.LockedResolution = GetVirtualResolution(resolution.X, resolution.Y);
        }
    }
}
