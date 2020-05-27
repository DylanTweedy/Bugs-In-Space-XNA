using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameRebuild
{
    static class InputManager
    {
        static public KeyboardState KB;
        static public KeyboardState pKB;
        static public MouseState M;
        static public MouseState pM;
        static public bool MouseOnScreen;

        static public GamePadState GP1;
        static GamePadState pGP1;
        static public GamePadState GP2;
        static GamePadState pGP2;
        static public GamePadState GP3;
        static GamePadState pGP3;
        static public GamePadState GP4;
        static GamePadState pGP4;

        static public void Initialize()
        {
        }

        static public void Update()
        {
            pKB = KB;
            KB = Keyboard.GetState();
            pM = M;
            M = Mouse.GetState();
            pGP1 = GP1;
            GP1 = GamePad.GetState(PlayerIndex.One);
            pGP2 = GP2;
            GP2 = GamePad.GetState(PlayerIndex.Two);
            pGP3 = GP3;
            GP3 = GamePad.GetState(PlayerIndex.Three);
            pGP4 = GP4;
            GP4 = GamePad.GetState(PlayerIndex.Four);

            if (M.X <= WorldVariables.WindowWidth && M.X >= 0 && M.Y <= WorldVariables.WindowHeight && M.Y >= 0)
                MouseOnScreen = true;
            else
                MouseOnScreen = false;
        }

    }
}
