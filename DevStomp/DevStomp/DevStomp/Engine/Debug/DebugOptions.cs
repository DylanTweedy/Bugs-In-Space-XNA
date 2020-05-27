using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class DebugOptions
    {
        static public bool DebugActive;
        static public string DebugMode = null;
        static public int DebugInt = 0;
        static public List<string> DebugDisplay = new List<string>();



        static public void Update()
        {
            string FPS;
            int fps = GlobalVariables.frameRate;

            if (fps > 55)
                FPS = StringManager.ColorString(fps.ToString(), Color.Green);
            else if (fps > 40)
                FPS = StringManager.ColorString(fps.ToString(), Color.Orange);
            else
                FPS = StringManager.ColorString(fps.ToString(), Color.Red);

            long CameraX = (long)CameraManager.Cams[0].Position.X;
            long CameraY = (long)CameraManager.Cams[0].Position.Y;

            long PX = CameraManager.Cams[0].PositionX;
            long PY = CameraManager.Cams[0].PositionY;

            if (PX > 0)
                CameraX += 2000000 + ((PX - 1) * 2000000);
            else if (PX < 0)
                CameraX -= 2000000 + ((PX + 1) * 2000000);
            if (PY > 0)
                CameraY += 2000000 + ((PY - 1) * 2000000);
            else if (PY < 0)
                CameraY -= 2000000 + ((PY + 1) * 2000000);


            float rx = UsefulMethods.FindBetween(Math.Abs(PX), int.MaxValue, 0, 1f, 0f, false);
            float gx = 1 - rx;
            float ry = UsefulMethods.FindBetween(Math.Abs(PY), int.MaxValue, 0, 1f, 0f, false);
            float gy = 1 - ry;
            
            string test =StringManager.ColorString(Write.Number(new Number(CameraY), 0, 0, 1, 0, null), new Color(ry, gy, 0f, 1f));

            DebugDisplay.Add("[B]FPS: " + FPS + "");
            DebugDisplay.Add("Debug State: " + DebugOptions.DebugMode);
            DebugDisplay.Add("Camera Position: " + "X: " + StringManager.ColorString(Write.Number(new Number(CameraX), 0, 0, 1, 0, null), new Color(rx, gx, 0f, 1f)) + " Y: " + test);
            DebugDisplay.Add("Camera Zoom: " + CameraManager.Cams[0].Zoom);

            if (InputManager.KBButtonPressed(true, Controls.DebugMode))
            {
                if (DebugActive)
                    DebugActive = false;
                else
                    DebugActive = true;
            }
            
            if (InputManager.MScrollWheel != 0)
            if (InputManager.MScrollWheel > 0)
                DebugInt++;
            else if (InputManager.MScrollWheel < 0)
                DebugInt--;

            if (DebugInt > 3)
                DebugInt = 0;
            if (DebugInt < 0)
                DebugInt = 3;

            //if (InputManager.MouseControl)
                DebugInt = 0;

            switch (DebugInt)
            {
                case 0:
                    DebugMode = "Nothing";
                    break;

                case 1:
                    DebugMode = "Add Object";
                    break;

                case 2:
                    DebugMode = "Add Random Object";
                    break;

                case 3:
                    DebugMode = "Remove All";
                    break;
            }

            if (InputManager.KBButtonPressed(true, Controls.DebugLockCursor))
            {
                if (InputManager.LockCursor)
                    InputManager.LockCursor = false;
                else                     
                    InputManager.LockCursor = true;
                
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            //if (DebugActive)
            {
                for (int i = 0; i < DebugDisplay.Count; i++)
                    StaticInfoBox.AddItem(DebugDisplay[i]);

                StaticInfoBox.Draw(spriteBatch, new Vector2(0, GraphicsManager.GameResolution.Y - StaticInfoBox.BoxSize.Y), 1f, 1f);
                
                StaticInfoBox.ClearList();
                DebugDisplay.Clear();
            }
        }
    }
}
