using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BugsInSpace
{
    public class KeyboardUpdate
    {
        private Keys[] lastPressedKeys;
        public string Input;
        string inputCheck;
        public int state;
        public bool readyToSave;
        GamePadState currentGamepadState;
        GamePadState previousGamepadState;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        Keys[] pressedKeys;
        int function;
        public int controlSystem;
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
        public bool Select;
        public bool Back;
        public bool Special;
        public bool Fire;
        public bool GamepadActive;
        public bool Quit;
        public float LeftStickY;
        public float LeftStickX;
        public bool SelectUp;
        public bool SelectDown;
        public bool SelectLeft;
        public bool SelectRight;
        TimeSpan MoveTime1;
        TimeSpan previousMoveTime1;
        TimeSpan MoveTime2;
        TimeSpan previousMoveTime2;
        TimeSpan MoveTime3;
        TimeSpan previousMoveTime3;
        TimeSpan MoveTime4;
        TimeSpan previousMoveTime4;
        TimeSpan MoveTime5;
        TimeSpan previousMoveTime5;
        TimeSpan MoveTime6;
        TimeSpan previousMoveTime6;
        public TimeSpan VibrateTime;
        TimeSpan previousVibrateTime;
        public bool Vibrate;
        PlayerIndex Controller;

        public void Initialize(int ControlSystem, int Function)
        {
            lastPressedKeys = new Keys[0];
            Input = "";
            state = 1;
            readyToSave = false;
            controlSystem = ControlSystem;
            function = Function;
            previousMoveTime1 = TimeSpan.Zero;
            MoveTime1 = TimeSpan.FromSeconds(1f);
            previousMoveTime2 = TimeSpan.Zero;
            MoveTime2 = TimeSpan.FromSeconds(1f);
            previousMoveTime3 = TimeSpan.Zero;
            MoveTime3 = TimeSpan.FromSeconds(1f);
            previousMoveTime4 = TimeSpan.Zero;
            MoveTime4 = TimeSpan.FromSeconds(1f);
            previousMoveTime5 = TimeSpan.Zero;
            MoveTime5 = TimeSpan.FromSeconds(1f);
            previousMoveTime6 = TimeSpan.Zero;
            MoveTime6 = TimeSpan.FromSeconds(1f);
            previousVibrateTime = TimeSpan.Zero;
            VibrateTime = TimeSpan.FromSeconds(0.15f);
            if (controlSystem == 2)
                Controller = PlayerIndex.One;
            if (controlSystem == 3)
                Controller = PlayerIndex.Two;
            if (controlSystem == 4)
                Controller = PlayerIndex.Three;
            if (controlSystem == 5)
                Controller = PlayerIndex.Four;
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem(controlSystem);

            #region Keyboard

            if (controlSystem == 1)
            {
                GamepadActive = true;

                if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
                {
                    Left = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Left) && currentKeyboardState.IsKeyUp(Keys.Left)) || (previousKeyboardState.IsKeyDown(Keys.A) && currentKeyboardState.IsKeyUp(Keys.A)))
                {
                    Left = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                {
                    Right = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Right) && currentKeyboardState.IsKeyUp(Keys.Right)) || (previousKeyboardState.IsKeyDown(Keys.D) && currentKeyboardState.IsKeyUp(Keys.D)))
                {
                    Right = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
                {
                    Up = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Up) && currentKeyboardState.IsKeyUp(Keys.Up)) || (previousKeyboardState.IsKeyDown(Keys.W) && currentKeyboardState.IsKeyUp(Keys.W)))
                {
                    Up = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
                {
                    Down = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Down) && currentKeyboardState.IsKeyUp(Keys.Down)) || (previousKeyboardState.IsKeyDown(Keys.S) && currentKeyboardState.IsKeyUp(Keys.S)))
                {
                    Down = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
                {
                    SelectLeft = false;
                    if (gameTime.TotalGameTime - previousMoveTime1 > MoveTime1)
                    {
                        previousMoveTime1 = gameTime.TotalGameTime;
                        SelectLeft = true;
                    }
                    MoveTime1 = TimeSpan.FromSeconds(0.3);
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Left) && currentKeyboardState.IsKeyUp(Keys.Left)) || (previousKeyboardState.IsKeyDown(Keys.A) && currentKeyboardState.IsKeyUp(Keys.A)))
                {
                    SelectLeft = false;
                    MoveTime1 = TimeSpan.FromSeconds(0);
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.A))
                {
                    SelectRight = false;
                    if (gameTime.TotalGameTime - previousMoveTime2 > MoveTime2)
                    {
                        previousMoveTime2 = gameTime.TotalGameTime;
                        SelectRight = true;
                    }
                    MoveTime2 = TimeSpan.FromSeconds(0.3);
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Right) && currentKeyboardState.IsKeyUp(Keys.Right)) || (previousKeyboardState.IsKeyDown(Keys.A) && currentKeyboardState.IsKeyUp(Keys.A)))
                {
                    SelectRight = false;
                    MoveTime2 = TimeSpan.FromSeconds(0);
                }

                if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.A))
                {
                    SelectUp = false;
                    if (gameTime.TotalGameTime - previousMoveTime3 > MoveTime3)
                    {
                        previousMoveTime3 = gameTime.TotalGameTime;
                        SelectUp = true;
                    }
                    MoveTime3 = TimeSpan.FromSeconds(0.3);
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Up) && currentKeyboardState.IsKeyUp(Keys.Up)) || (previousKeyboardState.IsKeyDown(Keys.A) && currentKeyboardState.IsKeyUp(Keys.A)))
                {
                    SelectUp = false;
                    MoveTime3 = TimeSpan.FromSeconds(0);
                }

                if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.A))
                {
                    SelectDown = false;
                    if (gameTime.TotalGameTime - previousMoveTime4 > MoveTime4)
                    {
                        previousMoveTime4 = gameTime.TotalGameTime;
                        SelectDown = true;
                    }
                    MoveTime4 = TimeSpan.FromSeconds(0.3);
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Down) && currentKeyboardState.IsKeyUp(Keys.Down)) || (previousKeyboardState.IsKeyDown(Keys.A) && currentKeyboardState.IsKeyUp(Keys.A)))
                {
                    SelectDown = false;
                    MoveTime4 = TimeSpan.FromSeconds(0);
                }

                if (currentKeyboardState.IsKeyDown(Keys.Space))
                {
                    Fire = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Space) && currentKeyboardState.IsKeyUp(Keys.Space)))
                {
                    Fire = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Back = false;
                    if (gameTime.TotalGameTime - previousMoveTime6 > MoveTime6)
                    {
                        previousMoveTime6 = gameTime.TotalGameTime;
                        Back = true;
                    }
                    MoveTime6 = TimeSpan.FromDays(1f);
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Escape) && currentKeyboardState.IsKeyUp(Keys.Escape)))
                {
                    MoveTime6 = TimeSpan.FromSeconds(0);
                    Back = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Quit = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Escape) && currentKeyboardState.IsKeyUp(Keys.Escape)))
                {
                    Quit = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Space) || currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    Select = false;
                    if (gameTime.TotalGameTime - previousMoveTime5 > MoveTime5)
                    {
                        previousMoveTime5 = gameTime.TotalGameTime;
                        Select = true;
                    }
                    MoveTime5 = TimeSpan.FromDays(1f);
                }
                if ((previousKeyboardState.IsKeyDown(Keys.Space) && currentKeyboardState.IsKeyUp(Keys.Space)) || (previousKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyUp(Keys.Enter)))
                {
                    MoveTime5 = TimeSpan.FromSeconds(0);
                    Select = false;
                }

                if (currentKeyboardState.IsKeyDown(Keys.LeftControl) || currentKeyboardState.IsKeyDown(Keys.RightControl) || currentKeyboardState.IsKeyDown(Keys.LeftAlt) || currentKeyboardState.IsKeyDown(Keys.RightAlt))
                {
                    Special = true;
                }
                if ((previousKeyboardState.IsKeyDown(Keys.LeftControl) && currentKeyboardState.IsKeyUp(Keys.LeftControl)) || (previousKeyboardState.IsKeyDown(Keys.RightControl) && currentKeyboardState.IsKeyUp(Keys.RightControl) ||
                    previousKeyboardState.IsKeyDown(Keys.LeftAlt) && currentKeyboardState.IsKeyUp(Keys.LeftAlt)) || (previousKeyboardState.IsKeyDown(Keys.RightAlt) && currentKeyboardState.IsKeyUp(Keys.RightAlt)))
                {
                    Special = false;
                }
            }

            #endregion

            #region Gamepad

            if (controlSystem == 2 || controlSystem == 3 || controlSystem == 4 || controlSystem == 5)
            {
                if (currentGamepadState.IsConnected == false)
                    GamepadActive = false;
                if (currentGamepadState.IsConnected)
                    GamepadActive = true;

                if (Vibrate)
                {
                    GamePad.SetVibration(Controller, 1, 1);
                    previousVibrateTime = gameTime.TotalGameTime;
                    Vibrate = false;
                }

                if (gameTime.TotalGameTime - previousVibrateTime > VibrateTime)
                {
                    GamePad.SetVibration(Controller, 0, 0);
                }

                LeftStickX = currentGamepadState.ThumbSticks.Left.X;
                LeftStickY = currentGamepadState.ThumbSticks.Left.Y;

                if (currentGamepadState.IsButtonDown(Buttons.DPadLeft) || currentGamepadState.ThumbSticks.Left.X < 0)
                {
                    Left = true;
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadLeft) && currentGamepadState.IsButtonUp(Buttons.DPadLeft)) || currentGamepadState.ThumbSticks.Left.X == 0)
                {
                    Left = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadRight) || currentGamepadState.ThumbSticks.Left.X > 0)
                {
                    Right = true;
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadRight) && currentGamepadState.IsButtonUp(Buttons.DPadRight)) || currentGamepadState.ThumbSticks.Left.X == 0)
                {
                    Right = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadUp) || currentGamepadState.ThumbSticks.Left.Y > 0)
                {
                    Up = true;
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadUp) && currentGamepadState.IsButtonUp(Buttons.DPadUp)) || currentGamepadState.ThumbSticks.Left.Y == 0)
                {
                    Up = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadDown) || currentGamepadState.ThumbSticks.Left.Y < 0)
                {
                    Down = true;
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadDown) && currentGamepadState.IsButtonUp(Buttons.DPadDown)) || currentGamepadState.ThumbSticks.Left.Y == 0)
                {
                    Down = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadLeft) || currentGamepadState.ThumbSticks.Left.X < 0)
                {
                    SelectLeft = false;
                    if (gameTime.TotalGameTime - previousMoveTime1 > MoveTime1)
                    {
                        previousMoveTime1 = gameTime.TotalGameTime;
                        SelectLeft = true;
                    }
                    MoveTime1 = TimeSpan.FromSeconds(0.3);
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadLeft) && currentGamepadState.IsButtonUp(Buttons.DPadLeft)) || currentGamepadState.ThumbSticks.Left.X == 0)
                {
                    SelectLeft = false;
                    MoveTime1 = TimeSpan.FromSeconds(0);
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadRight) || currentGamepadState.ThumbSticks.Left.X > 0)
                {
                    SelectRight = false;
                    if (gameTime.TotalGameTime - previousMoveTime2 > MoveTime2)
                    {
                        previousMoveTime2 = gameTime.TotalGameTime;
                        SelectRight = true;
                    }
                    MoveTime2 = TimeSpan.FromSeconds(0.3);
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadRight) && currentGamepadState.IsButtonUp(Buttons.DPadRight)) || currentGamepadState.ThumbSticks.Left.X == 0)
                {
                    SelectRight = false;
                    MoveTime2 = TimeSpan.FromSeconds(0);
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadUp) || currentGamepadState.ThumbSticks.Left.Y > 0)
                {
                    SelectUp = false;
                    if (gameTime.TotalGameTime - previousMoveTime3 > MoveTime3)
                    {
                        previousMoveTime3 = gameTime.TotalGameTime;
                        SelectUp = true;
                    }
                    MoveTime3 = TimeSpan.FromSeconds(0.3);
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadUp) && currentGamepadState.IsButtonUp(Buttons.DPadUp)) || currentGamepadState.ThumbSticks.Left.Y == 0)
                {
                    SelectUp = false;
                    MoveTime3 = TimeSpan.FromSeconds(0);
                }

                if (currentGamepadState.IsButtonDown(Buttons.DPadDown) || currentGamepadState.ThumbSticks.Left.Y < 0)
                {
                    SelectDown = false;
                    if (gameTime.TotalGameTime - previousMoveTime4 > MoveTime4)
                    {
                        previousMoveTime4 = gameTime.TotalGameTime;
                        SelectDown = true;
                    }
                    MoveTime4 = TimeSpan.FromSeconds(0.3);
                }
                else if ((previousGamepadState.IsButtonDown(Buttons.DPadDown) && currentGamepadState.IsButtonUp(Buttons.DPadDown)) || currentGamepadState.ThumbSticks.Left.Y == 0)
                {
                    SelectDown = false;
                    MoveTime4 = TimeSpan.FromSeconds(0);
                }

                if (currentGamepadState.IsButtonDown(Buttons.Back) || currentGamepadState.IsButtonDown(Buttons.B))
                {
                    Back = false;
                    if (gameTime.TotalGameTime - previousMoveTime6 > MoveTime6)
                    {
                        previousMoveTime6 = gameTime.TotalGameTime;
                        Back = true;
                    }
                    MoveTime6 = TimeSpan.FromDays(1f);
                }
                if ((previousGamepadState.IsButtonDown(Buttons.Back) && currentGamepadState.IsButtonUp(Buttons.Back)) || (previousGamepadState.IsButtonDown(Buttons.B) && currentGamepadState.IsButtonUp(Buttons.B)))
                {
                    MoveTime6 = TimeSpan.FromSeconds(0);
                    Back = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.Start))
                {
                    Quit = true;
                }
                if ((previousGamepadState.IsButtonDown(Buttons.Start) && currentGamepadState.IsButtonUp(Buttons.Start)))
                {
                    Quit = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.Start) || currentGamepadState.IsButtonDown(Buttons.A))
                {
                    Select = false;
                    if (gameTime.TotalGameTime - previousMoveTime5 > MoveTime5)
                    {
                        previousMoveTime5 = gameTime.TotalGameTime;
                        Select = true;
                    }
                    MoveTime5 = TimeSpan.FromDays(1f);
                }
                if ((previousGamepadState.IsButtonDown(Buttons.Start) && currentGamepadState.IsButtonUp(Buttons.Start)) || (previousGamepadState.IsButtonDown(Buttons.A) && currentGamepadState.IsButtonUp(Buttons.A)))
                {
                    MoveTime5 = TimeSpan.FromSeconds(0);
                    Select = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.A))
                {
                    Fire = true;
                }
                if (previousGamepadState.IsButtonDown(Buttons.A) && currentGamepadState.IsButtonUp(Buttons.A))
                {
                    Fire = false;
                }

                if (currentGamepadState.IsButtonDown(Buttons.X))
                {
                    Special = true;
                }
                if (previousGamepadState.IsButtonDown(Buttons.X) && currentGamepadState.IsButtonUp(Buttons.X))
                {
                    Special = false;
                }
            }

            #endregion

            if (function == 1)
            {
                foreach (Keys key in lastPressedKeys)
                {
                    if (!pressedKeys.Contains(key) && state == 1)
                        OnKeyUpInput(key);
                }
                foreach (Keys key in pressedKeys)
                {
                    if (!lastPressedKeys.Contains(key) && state == 1)
                        OnKeyDownInput(key);
                }
                lastPressedKeys = pressedKeys;
            }
        }

        private void ControlSystem(int ControlSystem)
        {
            if (ControlSystem == 1)
            {
                previousKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();
                pressedKeys = currentKeyboardState.GetPressedKeys();
            }

            if (controlSystem == 2 || controlSystem == 3 || controlSystem == 4 || controlSystem == 5)
            {
                previousGamepadState = currentGamepadState;
                currentGamepadState = GamePad.GetState(Controller);
            }
        }

        private void OnKeyDownInput(Keys key)
        {
            if (key == Keys.Back && Input.Length > 0)
                Input = Input.Remove(Input.Length - 1, 1);
            else if (key == Keys.Space)
                Input = Input.Insert(Input.Length, " ");

            inputCheck = key.ToString();

            if (inputCheck.Length == 1 && Input.Length < 12)
                Input += key.ToString();

            if (key == Keys.Enter)
            {
                state = 0;
                readyToSave = true;
            }

        }

        private void OnKeyUpInput(Keys key)
        {
            //do stuff
        }
    }
}
