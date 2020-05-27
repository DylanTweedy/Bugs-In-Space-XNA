using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Numerics;

namespace SkeletonEngine
{
    static class TestingVariables
    {
        static public float EarthGravity = 980.7f;
    }

    static class DebugOptions
    {
        static public bool DebugActive = true;
        static public string DebugMode = null;
        static public int DebugInt = 0;
        static public bool DrawNormals = false;
        static public Keys ShowNormals = Keys.RightControl;

        static public List<TextLine> DebugTextLines = new List<TextLine>();
        static TextBox DebugTextBox = new TextBox();
        
        static List<TextLine> NameList = new List<TextLine>();

        static public GeometryVisualizer Geometry = new GeometryVisualizer();
        static public LineVisualizer Lines = new LineVisualizer();

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
            
            //long CameraX = (long)CameraManager.MainCameras[0].Position.X;
            //long CameraY = (long)CameraManager.MainCameras[0].Position.Y;

            //long PX = CameraManager.MainCameras[0].PositionX;
            //long PY = CameraManager.MainCameras[0].PositionY;

            //if (PX > 0)
            //    CameraX += 2000000 + ((PX - 1) * 2000000);
            //else if (PX < 0)
            //    CameraX -= 2000000 + ((PX + 1) * 2000000);
            //if (PY > 0)
            //    CameraY += 2000000 + ((PY - 1) * 2000000);
            //else if (PY < 0)
            //    CameraY -= 2000000 + ((PY + 1) * 2000000);


            //float rx = UsefulMethods.FindBetween(Math.Abs(PX), int.MaxValue, 0, 1f, 0f, false);
            //float gx = 1 - rx;
            //float ry = UsefulMethods.FindBetween(Math.Abs(PY), int.MaxValue, 0, 1f, 0f, false);
            //float gy = 1 - ry;



            DebugTextLines.Add(new TextLine("[B]FPS", FPS + "[/B]", true, ": "));
            DebugTextLines.Add(new TextLine("-------------------------Debug Menu ---------------------------"));
            //DebugTextLines.Add(new TextLine("[H(1,0,0,1)]Camera Zoom", CameraManager.Cams[0].Zoom.ToString(), true, ": "));
            //DebugTextLines.Add(new TextLine(StringManager.UnderlineString("Camera Position"), CameraManager.Cams[0].GetRoughPosition().ToString(), true, ": "));
            //DebugTextLines.Add(new TextLine(StringManager.SetFont("Game Re[P(17)]sol[/P]ution", "TimesNewRoman"), GraphicsManager.GameResolution.ToString(), true, ": "));
            //DebugTextLines.Add(new TextLine(StringManager.InsertImage("TestImages", "TestRectangle", 2f) +
            //    StringManager.InsertImage("TestImages", "TestRectangle", 2f) +

                //StringManager.StrikethroughString("G[I]a[/B]me Res") + "olu[/I]tion", GraphicsManager.GameResolution.ToString(), true, ": "));
            //DebugTextLines.Add(new TextLine(StringManager.InsertImage("TextImages\\DropDown", "ClosedDefault") + "W[P(13)]indow Resol[/P]ution", GraphicsManager.WindowResolution.ToString(), true, ": "));
            //DebugTextLines.Add(new TextLine(StringManager.InsertImage("TextImages\\RadioButton", "SelectedDefault") + "Subscript Test", "H" + StringManager.SubscriptString("2") + "O", true, ": "));
            
            //DebugTextLines.Add(new TextLine(StringManager.InsertImage("TextImages\\CheckBox", "SelectedDefault", 0.75f, Color.LightSalmon) + "Superscript Test", "E = mc" + StringManager.SuperscriptString("2"), true, ": "));
            
            //DebugTextLines.Add(new TextLine("Emoticons", "Hello :) Test", true, ": "));
            //DebugTextLines.Add(new TextLine("Numbers", WriteNumber.Write(105340846403543f, SeparatorType.Standard, true, ShortenType.Short_Scale), true, ": "));




            //DebugTextLines.AddRange(GraphicsManager.Information());
            DebugTextLines.AddRange(CameraManager.MainCameras[0].Information());

            #region Name Generation Test

            if (false)
            {
                if (NameList.Count == 0 || InputManager.KBPressed(true, Keys.F2))
                {
                    RandomStringGenerator Titles = new RandomStringGenerator(
                           "Data\\Names-Title.txt",
                           string.Empty,
                           string.Empty,
                           new Vector3(0, 0, 0),
                           -1
                           );

                    RandomStringGenerator Names = new RandomStringGenerator(
                           "Data\\Names-Prefix.txt",
                           "Data\\Names-Beginning.txt",
                           "Data\\Names-End.txt",
                           new Vector3(0, 0, 1),
                           -1
                           );

                    RandomStringGenerator Objects = new RandomStringGenerator(
                           "Data\\Descriptive.txt",
                           "Data\\Object-Name.txt",
                           "Data\\Object-Prefix.txt",
                           new Vector3(0, 0, 0),
                           -1
                           );

                    NameList.Clear();
                    string text = string.Empty;

                    for (int i = 0; i < 15; i++)
                    {
                        text += Titles.Generate(25, 1, 0, true, true, false);

                        if (text == string.Empty)
                            text += Names.Generate(10, 1, 90, false, true, true) + " " + Names.Generate(10, 1, 90, false, true, true) + "'s ";
                        else
                            text += " " + Names.Generate(10, 2, 90, false, true, true) + "'s ";

                        text += Objects.Generate(100, 1, 0, true, false, false) + " of the ";

                        text += Objects.Generate(0, 0, 100, true, false, false) + ".";

                        NameList.Add(new TextLine(text));

                        text = "";
                    }
                }

                DebugTextLines.AddRange(NameList);
            }

            #endregion

            for (int i = 0; i < DebugTextLines.Count; i++)
            {
                DebugTextLines[i].Style = ListStyle.Number;

                if (i == 0)
                {
                    DebugTextLines[i].LineSelected = true;
                    DebugTextLines[i].LineSelectedColor = Color.White;
                    DebugTextLines[i].InvertTextColor = true;
                }
            }


            //DebugBox.Update(DebugDisplay, null, 24f);
            Location BoxSize = DebugTextBox.FullBoxSize;
            DebugTextBox.Position = new SpacePosition(BoxSize.Left, BoxSize.Top);
            DebugTextBox.Scale = 0.6f;
            //DebugTextBox.Position = Vector2.Zero;
            //DebugBoxNew.Rotation = (float)GlobalVariables.elapsedTime.TotalSeconds * MathHelper.Pi * 2f;
            DebugTextBox.Rotation = 0f;
            DebugTextBox.DefaultWordSettings.HighlightColor = Color.Transparent;
            DebugTextBox.DefaultWordSettings.OverlayColor = Color.Transparent;
            DebugTextBox.BackgroundColor = new Color(55, 55, 55);
            DebugTextBox.LetterSpacing = 1f;
            DebugTextBox.Border.Thickness.Left = 0f;
            DebugTextBox.Border.Thickness.Top = 0f;
            DebugTextBox.Update(DebugTextLines);


            if (InputManager.KBPressed(true, Microsoft.Xna.Framework.Input.Keys.F1))
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

            if (InputManager.KBPressed(true, EngineControls.DebugLockCursor))            
                    InputManager.LockCursor = !InputManager.LockCursor;


            DebugTextLines.Clear();
        }

        static public void Draw(Camera camera)
        {
            //if (DebugActive)
            {
                //StaticInfoBox.Draw(spriteBatch, new Vector2(StaticInfoBox.BoxSize.X + 500f, (GraphicsManager.GameResolution.Y / 2f)), 24f, 1f, 0f,
                    //null, Vector2.Zero, new Vector2(-1, 0), new Color(55, 55, 55), new Vector2(10f, 10f), new BorderStyle("Default", new Color(55, 55, 55), new Location(8f, 0f, 8f, 8f)));

                //DebugBox.Draw(spriteBatch);
                DrawSprites.Begin(camera);


                DebugTextBox.Draw(camera);

                DrawSprites.End();

                //DebugBox.Draw(spriteBatch, new Vector2(0, 0), 1f, 0f,
                // new Vector2(-1, 0), new Color(55, 55, 55), new Vector2(10f, 10f), new BorderStyle("Default", new Color(55, 55, 55), new Location(8f, 0f, 8f, 8f)));









                //DebugTextLines.Clear();
            }
        }
    }
}
