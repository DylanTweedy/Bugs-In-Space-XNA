using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;



namespace SkeletonEngine
{
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

    class KeyboardInput
    {
        public List<Keys> Modifiers = new List<Keys>();
        public Keys KeyboardButton;

        public KeyboardInput(Keys keyboardButton)
        {
            KeyboardButton = keyboardButton;
        }
        public void AddModifier(Keys Modifier)
        {
            switch (Modifier)
            {
                case Keys.LeftAlt:
                case Keys.LeftControl:
                case Keys.LeftShift:
                case Keys.RightAlt:
                case Keys.RightControl:
                case Keys.RightShift:
                    Modifiers.Add(Modifier);
                    break;

                default:
                    GlobalVariables.ErrorLog.Add("Invalid Modifer Added to KeyboardInput.");
                    break;
            }
        }
    }

    static class InputManager
    {
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);
        [DllImport("user32.dll")]
        static extern void ClipCursor(IntPtr rect);  


        static public KeyboardState KB;
        static public KeyboardState pKB;

        static public MouseState M;
        static public MouseState pM;
        static public bool LockCursor = false;
        static public bool PreviousLockCursor;


        static public int MScrollWheel;
        static public Vector2 MousePosition = Vector2.Zero;
        static public Vector2 MouseVelocity = Vector2.Zero;
        static public Rectangle MouseRectangle = new Rectangle(0, 0, 2, 2);

        static public List<GamePadState> GP;
        static public List<GamePadState> pGP;
        static public List<bool> GPinUse;


        static public void Initialize()
        {
            GP = new List<GamePadState>();
            pGP = new List<GamePadState>();
            GPinUse = new List<bool>();

            for (int i = 0; i < 4; i++)
            {
                GP.Add(new GamePadState());
                pGP.Add(new GamePadState());
                GPinUse.Add(false);
            }
        }

        static public void Update()
        {
            pKB = KB;
            KB = Keyboard.GetState();

            for (int i = 0; i < GP.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        pGP[i] = GP[i];
                        GP[i] = GamePad.GetState(PlayerIndex.One);
                        break;
                    case 1:
                        pGP[i] = GP[i];
                        GP[i] = GamePad.GetState(PlayerIndex.Two);
                        break;
                    case 2:
                        pGP[i] = GP[i];
                        GP[i] = GamePad.GetState(PlayerIndex.Three);
                        break;
                    case 3:
                        pGP[i] = GP[i];
                        GP[i] = GamePad.GetState(PlayerIndex.Four);
                        break;
                }
            }

            UpdateMouse();

            if (!GraphicsManager.WindowFocus)
            {
                IntPtr rect = new IntPtr();
                ClipCursor(rect);
            }
        }

        static private bool IsModiferPressed()
        {
            if (KB.IsKeyDown(Keys.LeftAlt) ||
                KB.IsKeyDown(Keys.LeftControl) ||
                KB.IsKeyDown(Keys.LeftShift) ||
                KB.IsKeyDown(Keys.RightAlt) ||
                KB.IsKeyDown(Keys.RightControl) ||
                KB.IsKeyDown(Keys.RightShift))
                return true;
            else
                return false;

        }

        static public bool KBButtonPressed(bool Once, KeyboardInput key)
        {
            if (key.Modifiers.Count == 0 && IsModiferPressed())
                return false;
            
            if (Once)
            {
                if (KB.IsKeyDown(key.KeyboardButton) && pKB.IsKeyUp(key.KeyboardButton))
                {
                    if (key.Modifiers.Count == 0)
                        return true;
                    else
                    {
                        int Mods = 0;

                        for (int i = 0; i < key.Modifiers.Count; i++)
                        {
                            if (KB.IsKeyDown(key.Modifiers[i]))
                                Mods++;
                        }

                        if (Mods == key.Modifiers.Count)
                            return true;
                    }
                }
            }
            else if (KB.IsKeyDown(key.KeyboardButton))
            {
                if (key.Modifiers.Count == 0)
                    return true;
                else
                {
                    int Mods = 0;

                    for (int i = 0; i < key.Modifiers.Count; i++)
                    {
                        if (KB.IsKeyDown(key.Modifiers[i]))
                            Mods++;
                    }

                    if (Mods == key.Modifiers.Count)
                        return true;
                }
            }

            return false;
        }


        static public bool KBButtonPressed(bool Once, Keys key)
        {
            if (Once)
            {
                if (KB.IsKeyDown(key) && pKB.IsKeyUp(key))
                    return true;

            }
            else if (KB.IsKeyDown(key))
                return true;


            return false;
        }

        #region Mouse
        
        /// <summary>
        /// Update current mouse information.
        /// </summary>
        static private void UpdateMouse()
        {
            if (LockCursor && GraphicsManager.WindowFocus)
            {
                //Lock cursor to window.
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
            
            M = Mouse.GetState();
            
            //Sets the current mouse state.
            MouseVelocity.X = (M.X - pM.X) / GraphicsManager.ScreenScale.X;
            MouseVelocity.Y = (M.Y - pM.Y) / GraphicsManager.ScreenScale.Y;
            
            MousePosition.X = (M.X - GraphicsManager.ViewportOffset.X) / GraphicsManager.ScreenScale.X;
            MousePosition.Y = (M.Y - GraphicsManager.ViewportOffset.Y) / GraphicsManager.ScreenScale.Y;
            
            MScrollWheel = M.ScrollWheelValue - pM.ScrollWheelValue;
            MouseRectangle.X = (int)MousePosition.X - 1;
            MouseRectangle.Y = (int)MousePosition.Y - 1;

            if (!GraphicsManager.WindowFocus)
                MScrollWheel = 0;


            //if (MousePosition.X > GraphicsManager.GameResolution.X)
            //    MousePosition.X = GraphicsManager.GameResolution.X;
            //else if (MousePosition.X < 0)
            //    MousePosition.X = 0;

            //if (MousePosition.Y > GraphicsManager.GameResolution.Y)
            //    MousePosition.Y = GraphicsManager.GameResolution.Y;
            //else if (MousePosition.Y < 0)
            //    MousePosition.Y = 0;





            pM = M;
        }

        static public Vector2 GetMousePosition(int camera)
        {
            Vector2 MousePosition = new Vector2(M.X - (GraphicsManager.WindowResolution.X / 2f), M.Y - (GraphicsManager.WindowResolution.Y / 2f));

            //MousePosition += ;

            float WorldAngle = -CameraManager.Cams[camera].Rotation;
            float Distance = Vector2.Distance(Vector2.Zero, MousePosition);
            float MouseAngle = (float)Math.Atan2(MousePosition.X, -MousePosition.Y) + WorldAngle;

            MousePosition = new Vector2((float)Math.Sin(MouseAngle), -(float)Math.Cos(MouseAngle)) * Distance;

            return CameraManager.Cams[camera].Position + (new Vector2(2000000, 2000000) * new Vector2(CameraManager.Cams[camera].PositionX, CameraManager.Cams[camera].PositionY)) + (MousePosition / CameraManager.Cams[camera].Zoom);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Once"></param>
        /// If the button is to only be pressed once and not held down, set to true.
        /// <param name="button">
        /// <para>0 = Left Button</para>
        /// <para>1 = Right Button</para>
        /// <para>2 = Middle Button</para>
        /// <para>3 = X Button 1</para>
        /// <para>4 = X Button 2</para>
        /// </param>
        /// <returns></returns>
        static public bool MButtonPressed(bool Once, int button)
        {
            //If window is out of focus disable mouse buttons.
            if (GraphicsManager.WindowFocus)
            {
                ButtonState Current = ButtonState.Released;
                ButtonState Previous = ButtonState.Released;

                switch (button)
                {
                    case 0:
                        Current = M.LeftButton;
                        Previous = pM.LeftButton;
                        break;

                    case 1:
                        Current = M.RightButton;
                        Previous = pM.RightButton;
                        break;

                    case 2:
                        Current = M.MiddleButton;
                        Previous = pM.MiddleButton;
                        break;

                    case 3:
                        Current = M.XButton1;
                        Previous = pM.XButton1;
                        break;

                    case 4:
                        Current = M.XButton2;
                        Previous = pM.XButton2;
                        break;
                }

                if (Once)
                {
                    if (Current == ButtonState.Pressed && Previous == ButtonState.Released)
                        return true;
                }
                else if (Current == ButtonState.Pressed)
                    return true;
            }

            return false;
        }

        #endregion
    }
}
