using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRebuild
{
    static class CameraManager
    {
        static public List<Camera> Cams;

        static public void Initialize()
        {
            Cams = new List<Camera>();
            Cams.Add(new Camera(Vector3.Zero, Vector2.Zero, 0f, 1f));

            Cams[0].WindowDimensions = new Vector2(WorldVariables.WindowWidth, WorldVariables.WindowHeight);
        }

        static public void Update()
        {
            for (int i = 0; i < Cams.Count; i++)
            {
                Cams[i].Update();
            }
        }

        static public void DrawBack(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Cams.Count; i++)
            {
                Cams[i].DrawBack(spriteBatch);
            }
        }

        static public void DrawFront(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Cams.Count; i++)
            {
                Cams[i].DrawFront(spriteBatch);
            }
        }
    }
}
