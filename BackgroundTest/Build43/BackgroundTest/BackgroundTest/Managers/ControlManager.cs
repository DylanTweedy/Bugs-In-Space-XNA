using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace BackgroundTest.Managers
{
    class ControlManager
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        
        GamePadState currentGamepad1State;
        GamePadState previousGamepad1State;



        public void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousGamepad1State = currentGamepad1State;
            currentGamepad1State = GamePad.GetState(PlayerIndex.One);            
        }

    }
}
