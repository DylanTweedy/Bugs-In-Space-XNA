using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    class InfoBox
    {
        List<ColorSelector> ColorSelectors;
        public Vector2 Position;

        public void Initialize()
        {
            ColorSelectors = new List<ColorSelector>();

            ColorSelectors.Add(new ColorSelector(Color.Red));
        }


        public void UpdatePosition(Vector2 Position)
        {
            for (int i = 0; i < ColorSelectors.Count; i++)
            {
                ColorSelectors[i].Position = Position;
            }
        }

        public Color SetColor(Color color)
        {
            ColorManager.HSB hsb = ColorManager.RGB2HSB(color);

            ColorSelectors[0].H = hsb.H;
            ColorSelectors[0].S = hsb.S;
            ColorSelectors[0].L = hsb.B;

            ColorSelectors[0].R = color.R;
            ColorSelectors[0].G = color.G;
            ColorSelectors[0].B = color.B;

            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C) && InputManager.pKB.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.C))
                return ColorManager.RandomColor();

            return color;            
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ColorSelectors.Count; i++)
            {
                ColorSelectors[i].Draw(spriteBatch);
            }
        }
    }
}
