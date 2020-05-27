using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;



namespace SkeletonEngine
{
    /// <summary>
    /// List of possible mouse buttons.
    /// </summary>
    public enum MouseButton
    {
        Left,
        Right,
        Middle,
        X1,
        X2
    }
    
    /// <summary>
    /// List of possible input types.
    /// </summary>
    public enum InputType
    {
        None,
        AI,
        GamepadOne,
        GamepadTwo,
        GamepadThree,
        GamepadFour,
        Keyboard
    }

    /// <summary>
    /// Manages all forms of game input.
    /// </summary>
    static class InputManager
    {
        //For locking cursor to window.
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);
        [DllImport("user32.dll")]
        static extern void ClipCursor(IntPtr rect);  
        
        
        static private KeyboardState KB;
        static private KeyboardState pKB;

        static public MouseState M;
        static public MouseState pM;
        
        /// <summary>
        /// Whether or not the cursor is locked to the window.
        /// </summary>
        static public bool LockCursor = false;

        /// <summary>
        /// Amount the mouse wheel has scrolled.
        /// </summary>
        static public int MScrollWheel { get; private set; }
        /// <summary>
        /// The position of the mouse on screen.
        /// </summary>
        static public Vector2 MousePosition { get; private set; }
        /// <summary>
        /// The previous position of the mouse on screen.
        /// </summary>
        static public Vector2 MousePreviousPosition { get; private set; }
        /// <summary>
        /// The velocity of the mouse.
        /// </summary>
        static public Vector2 MouseVelocity { get; private set; }


        /// <summary>
        /// Sets up the base variables.
        /// </summary>
        static public void Initialize()
        {
        }

        /// <summary>
        /// Updates the state of the input devices.
        /// </summary>
        static public void Update()
        {
            UpdateKeyboard();
            UpdateGamePad();
            UpdateMouse();
        }

        #region GamePad

        /// <summary>
        /// Updates the state of the gamepads.
        /// </summary>
        static private void UpdateGamePad()
        {
        }

        #endregion

        #region Keyboard

        /// <summary>
        /// Updates the state of the keyboard.
        /// </summary>
        static private void UpdateKeyboard()
        {
            pKB = KB;
            KB = Keyboard.GetState();
        }
        
        /// <summary>
        /// Checks to see if a keyboard key is pressed.
        /// </summary>
        /// <param name="once">Whether or not the key is checked for a single frame.</param>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        static public bool KBPressed(bool once, Keys key)
        {
            if (once)
            {
                if (KB.IsKeyDown(key) && pKB.IsKeyUp(key))
                    return true;

            }
            else if (KB.IsKeyDown(key))
                return true;
            
            return false;
        }

        /// <summary>
        /// Checks to see if a series of keyboard keys are pressed.
        /// </summary>
        /// <param name="once">Whether or not the keys are checked for a single frame.</param>
        /// <param name="keys">The keys to check.</param>
        /// <returns></returns>
        static public bool KBPressed(bool once, List<Keys> keys)
        {
            if (once)
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    if (i != keys.Count - 1)
                    {
                        if (!KB.IsKeyDown(keys[i]))
                            return false;
                    }
                    else
                        if (KB.IsKeyDown(keys[i]) && pKB.IsKeyUp(keys[i]))
                            return true;
                }
            }
            else
            {
                for (int i = 0; i < keys.Count; i++)
                    if (!KB.IsKeyDown(keys[i]))
                        return false;

                return true;
            }

            return false;
        }

        #endregion

        #region Mouse

        /// <summary>
        /// Updates the state of the mouse.
        /// </summary>
        static private void UpdateMouse()
        {
            MousePreviousPosition = MousePosition;
            pM = M;

            #region Lock Cursor

            if (LockCursor && GraphicsManager.WindowFocus)
            {
                Rectangle rect = GraphicsManager.ClientBounds;
                rect.Width += rect.X;
                rect.Height += rect.Y;

                rect.X += (int)(GraphicsManager.ViewportOffset.X);
                rect.Y += (int)(GraphicsManager.ViewportOffset.Y);
                rect.Width -= (int)(GraphicsManager.ViewportOffset.X);
                rect.Height -= (int)(GraphicsManager.ViewportOffset.Y);

                ClipCursor(ref rect);
            }
            else
            {
                IntPtr rect = new IntPtr();
                ClipCursor(rect);
            }

            #endregion

            M = Mouse.GetState();
            
            //Sets the mouse position and velocity.
            MouseVelocity = new Vector2(M.X - pM.X,M.Y - pM.Y) / GraphicsManager.ScreenScale;
            MousePosition = (new Vector2(M.X, M.Y) - GraphicsManager.ViewportOffset) / GraphicsManager.ScreenScale;
                       
            //Sets the mouse scroll value.
            if (!GraphicsManager.WindowFocus)
                MScrollWheel = 0;
            else
                MScrollWheel = M.ScrollWheelValue - pM.ScrollWheelValue;

        }

        /// <summary>
        /// Gets the mouse position within a camera.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        static public SpacePosition GetMousePosition(Camera camera)
        {
            Vector2 pos = (MousePosition - (new Vector2(camera.Bounds.Width, camera.Bounds.Height) / 2f)) / camera.Zoom;
            pos = Vector2.Transform(pos, Matrix.CreateRotationZ(-camera.Rotation));
            
            return new SpacePosition(pos) + camera.Position;
        }

        /// <summary>
        /// Gets the previous mouse position within a camera.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        static public SpacePosition GetPreviousMousePosition(Camera camera)
        {
            Vector2 pos = (MousePreviousPosition - (new Vector2(camera.Bounds.Width, camera.Bounds.Height) / 2f)) / camera.Zoom;
            pos = Vector2.Transform(pos, Matrix.CreateRotationZ(-camera.Rotation));

            return new SpacePosition(pos) + camera.Position;
        }

        /// <summary>
        /// Checks to see if a mouse button is pressed.
        /// </summary>
        /// <param name="once">Whether or not the button is checked for a single frame.</param>
        /// <param name="button">The button to check.</param>
        /// <returns></returns>
        static public bool MBPressed(bool once, MouseButton button)
        {
            if (GraphicsManager.WindowFocus)
            {
                ButtonState current = ButtonState.Released;
                ButtonState previous = ButtonState.Released;

                switch (button)
                {
                    case MouseButton.Left:
                        current = M.LeftButton;
                        previous = pM.LeftButton;
                        break;

                    case MouseButton.Right:
                        current = M.RightButton;
                        previous = pM.RightButton;
                        break;

                    case MouseButton.Middle:
                        current = M.MiddleButton;
                        previous = pM.MiddleButton;
                        break;

                    case MouseButton.X1:
                        current = M.XButton1;
                        previous = pM.XButton1;
                        break;

                    case MouseButton.X2:
                        current = M.XButton2;
                        previous = pM.XButton2;
                        break;
                }

                if (once)
                {
                    if (current == ButtonState.Pressed && previous == ButtonState.Released)
                        return true;
                }
                else if (current == ButtonState.Pressed)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks to see if a series of mouse buttons are pressed.
        /// </summary>
        /// <param name="once">Whether or not the buttons are checked for a single frame.</param>
        /// <param name="buttons">The buttons to check.</param>
        /// <returns></returns>
        static public bool MBPressed(bool once, List<MouseButton> buttons)
        {
            if (GraphicsManager.WindowFocus)
            {
                List<ButtonState> current = new List<ButtonState>();
                List<ButtonState> previous = new List<ButtonState>();

                for (int i = 0; i < buttons.Count; i++)
                {
                    current.Add(ButtonState.Released);
                    previous.Add(ButtonState.Released);

                    switch (buttons[i])
                    {
                        case MouseButton.Left:
                            current[i] = M.LeftButton;
                            previous[i] = pM.LeftButton;
                            break;

                        case MouseButton.Right:
                            current[i] = M.RightButton;
                            previous[i] = pM.RightButton;
                            break;

                        case MouseButton.Middle:
                            current[i] = M.MiddleButton;
                            previous[i] = pM.MiddleButton;
                            break;

                        case MouseButton.X1:
                            current[i] = M.XButton1;
                            previous[i] = pM.XButton1;
                            break;

                        case MouseButton.X2:
                            current[i] = M.XButton2;
                            previous[i] = pM.XButton2;
                            break;
                    }
                }

                if (once)
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        if (i != buttons.Count - 1)
                        {
                            if (current[i] == ButtonState.Released)
                                return false;
                        }
                        else
                            if (current[i] == ButtonState.Pressed && previous[i] == ButtonState.Released)
                                return true;
                    }
                }
                else
                {
                    for (int i = 0; i < buttons.Count; i++)
                        if (current[i] == ButtonState.Released)
                            return false;

                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
