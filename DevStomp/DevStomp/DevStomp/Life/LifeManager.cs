using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DevStomp
{
    static class LifeManager
    {
        static public List<Life> Entities;
        static public Vector2 CameraPos;
        static Random rand;

        static public void Initialize()
        {
            rand = new Random();
            CameraPos = Vector2.Zero;
            Entities = new List<Life>();
        }

        static public void Update()
        {
            if (Entities.Count > 0)
            {
                float LowX = Entities[0].Position.X;
                float LowY = Entities[0].Position.Y;
                float HighX = Entities[0].Position.X;
                float HighY = Entities[0].Position.Y;

                for (int i = 1; i < Entities.Count; i++)
                {
                    if (Entities[i].Position.X > HighX)
                        HighX = Entities[i].Position.X;
                    else if (Entities[i].Position.X < LowX)
                        LowX = Entities[i].Position.X;

                    if (Entities[i].Position.Y > HighY)
                        HighY = Entities[i].Position.Y;
                    else if (Entities[i].Position.Y < LowY)
                        LowY = Entities[i].Position.Y;
                }

                float DiffX = HighX - LowX;
                float DiffY = HighY - LowY;
                float CenterX = HighX - (DiffX * 0.5f);
                float CenterY = HighY - (DiffY * 0.5f);

                CameraPos = new Vector2(CenterX, CenterY);
                
                //DiffX /= WorldVariables.WindowWidth;
                //DiffY /= WorldVariables.WindowHeight;
                float distance = Math.Max(DiffX, DiffY);
                distance = Vector2.Distance(new Vector2(HighX, HighY), new Vector2(LowX, LowY));
                float zoom = 0.5f;
                //if (distance == DiffX)
                //    zoom = UsefulMethods.FindBetween(distance * 2, WorldVariables.Boundry, 1f, 1f, 0.15f, true);
                //else
                //    zoom = UsefulMethods.FindBetween((distance * 3), WorldVariables.Boundry, 1f, 1f, 0.15f, true);

                //zoom = UsefulMethods.FindBetween((distance * 3), WorldVariables.Boundry, 1f, 0.45f, 0.15f, true);

                if (!InputManager.MouseControl)
                CameraManager.Cams[0].SetZoom(zoom);

                Console.WriteLine(DiffX);
            }
            else
                CameraPos = Vector2.Zero;

            for (int i = 0; i < 4; i++)
                if (!InputManager.GPinUse[i] && InputManager.GP[i].IsButtonDown(Buttons.Start) && InputManager.pGP[i].IsButtonUp(Buttons.Start))
                {
                    InputManager.GPinUse[i] = true;
                    Entities.Add(new Life());
                    Entities[Entities.Count - 1].Initialize((byte)i, rand);
                }
                else if (InputManager.GPinUse[i] && InputManager.GP[i].IsButtonDown(Buttons.Start) && InputManager.pGP[i].IsButtonUp(Buttons.Start))
                    for (int o = 0; o < Entities.Count; o++)
                        if (Entities[o].CS == i)
                        {
                            InputManager.GPinUse[i] = false;
                            Entities.RemoveAt(o);
                        }

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Update();

                //for (int o = 0; o < Entities.Count; o++)
                //{
                //    if (Entities[i].rect.Intersects(Entities[o].rect) && i != o)
                //    {
                //        int diff = Entities[i].rect.X - Entities[o].rect.X;
                //        //diff += Entities[i].rect.Width / 2;

                //        Entities[i].Position.X += diff / 2;
                //        Entities[i].rect.X += diff / 2;
                //    }
                //}
            }
        }


        static public void DrawBack(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].DrawBack(spriteBatch);
            }
        }

        static public void Draw(SpriteBatch spriteBatch, byte cam)
        {
            SpineLoader.RenderStart(cam);

            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].Draw(spriteBatch, cam);
            }

            SpineLoader.RenderEnd();
        }

        static public void DrawFront(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].DrawFront(spriteBatch);
            }
        }

        static public void DrawUI(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entities[i].DrawUI(spriteBatch);
            }

        }
    }
}
