using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    static class ColorManager
    {
        static Random rand;

        public class HSB
        {
            public float H;
            public float S;
            public float B;
            public byte A;

            public HSB()
            {
            }

            public HSB(float h, float s, float b)
            {
                H = h;
                S = s;
                B = b;
                A = 255;
            }
        }

        static public void Initialize()
        {
            rand = new Random();
        }

        static public Color RandomColor()
        {
            return new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        static public Color RandomFullColor()
        {
            HSB hsb = new HSB();
            hsb.H = rand.Next(0, 360);
            hsb.S = 1;
            hsb.B = 1;
            hsb.A = 255;
            
            return HSB2RGB(hsb);
        }

        static public List<Color> GenerateRandomPalette()
        {
            List<Color> Palette = new List<Color>();
            
            return Palette;
        }

        static public Color test()
        {
            HSB hsb = new HSB();
            hsb.H = 190;
            hsb.S = 100;
            hsb.B = 50;
            hsb.A = 255;

            return HSB2RGB(hsb);
        }

        static public List<Color> FullBrightness(Color rgb)
        {
            List<Color> Palette = new List<Color>();

            HSB hsb = RGB2HSB(rgb);

            hsb.B = 1f;

            Palette.Add(rgb);
            Palette.Add(HSB2RGB(hsb));

            return Palette;
        }

        static public List<Color> Compliment(Color rgb)
        {
            List<Color> Palette = new List<Color>();

            HSB hsb = RGB2HSB(rgb);

            hsb.H -= 180;

            if (hsb.H < 0)
                hsb.H += 360;

            Palette.Add(rgb);
            Palette.Add(HSB2RGB(hsb));

            return Palette;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rgb"></param>
        /// <param name="compliment"></param>
        /// <param name="angle">Between 5 and 175</param>
        /// <returns></returns>
        static public List<Color> Triad(Color rgb, bool compliment, int angle)
        {
            List<Color> Palette = new List<Color>();

            if (compliment)
                Palette = Compliment(rgb);
            else
                Palette.Add(rgb);

            if (angle < 5)
                angle = 5;
            else if (angle > 175)
                angle = 175;

            HSB hsb = RGB2HSB(rgb);
            HSB hsb2 = RGB2HSB(rgb);
            HSB hsb3 = RGB2HSB(rgb);

            hsb.H += angle;
            hsb2.H -= angle;

            if (hsb.H >= 360)
                hsb.H -= 360;
            if (hsb2.H < 0)
                hsb2.H += 360;

            Palette.Add(HSB2RGB(hsb));
            Palette.Add(HSB2RGB(hsb2));

            return Palette;
        }

        static public List<Color> Spectrum(Color rgb)
        {
            List<Color> Palette = new List<Color>();
            HSB hsb = RGB2HSB(rgb);
            Palette.Add(rgb);

            for (int i = 0; i < 6; i++)
            {
                hsb.H += 51;

                if (hsb.H >= 360)
                    hsb.H -= 360;

                Palette.Add(HSB2RGB(hsb));
            }

            return Palette;
        }

        static public List<Color> GetShades(List<Color> rgb, float contrast)
        {
            List<Color> P = new List<Color>();
            for (int i = 0; i < rgb.Count; i++)
                P.AddRange(ColorManager.GetShades(rgb[i], contrast));

            return P;
        }

        static public List<Color> GetShades(Color rgb, float contrast)
        {
            List<Color> Palette = new List<Color>();
            List<HSB> hsb = new List<HSB>();

            for (int i = 0; i < 4; i++)
            {
                hsb.Add(RGB2HSB(rgb));

                switch (i)
                {
                    case 0:
                        hsb[i].B *= contrast * contrast;
                        break;
                    case 1:
                        hsb[i].B *= contrast;
                        break;
                    case 2:
                        Palette.Add(rgb);
                        hsb[i].S *= contrast;
                        break;
                    case 3:
                        hsb[i].S *= contrast * contrast;
                        break;
                }

                Palette.Add(HSB2RGB(hsb[i]));
            }

            return Palette;
        }

        static public Color HSB2RGB(HSB hsb)
        {
            Color rgb = new Color();
            float c = hsb.B * hsb.S;
            float x = c * (1 - Math.Abs(((hsb.H / 60) % 2) - 1));
            float m = hsb.B - c;

            if (hsb.H < 60)
                rgb = new Color(c, x, 0);
            else if (hsb.H < 120)
                rgb = new Color(x, c, 0);
            else if (hsb.H < 180)
                rgb = new Color(0, c, x);
            else if (hsb.H < 240)
                rgb = new Color(0, x, c);
            else if (hsb.H < 300)
                rgb = new Color(x, 0, c);
            else if (hsb.H < 360)
                rgb = new Color(c, 0, x);

            rgb.R += (byte)(m * 255);
            rgb.G += (byte)(m * 255);
            rgb.B += (byte)(m * 255);

            rgb.A = hsb.A;

            return rgb;
        }

        static public HSB RGB2HSB(Color rgb)
        {
            HSB hsb = new HSB();
            float r = rgb.R / 255f;
            float g = rgb.G / 255f;
            float b = rgb.B / 255f;
            float Cmax = Math.Max(r, Math.Max(g, b));
            float Cmin = Math.Min(r, Math.Min(g, b));
            float delta = Cmax - Cmin;

            //Hue calculation.
            if (Cmax == r)
                hsb.H = 60 * (((g - b) / delta) % 6);
            else if (Cmax == g)
                hsb.H = 60 * (((b - r) / delta) + 2);
            else if (Cmax == b)
                hsb.H = 60 * (((r - g) / delta) + 4);

            if (hsb.H < 0)
                hsb.H += 360;
            else if (hsb.H >= 360)
                hsb.H -= 360;

            //Saturation calculation.
            if (delta == 0)
                hsb.S = 0;
            else
                hsb.S = delta / Cmax;

            hsb.B = Cmax;
            hsb.A = rgb.A;

            return hsb;
        }

        static public Color SaturationMultiplier(Color color, float multiplier)
        {
            HSB hsb = RGB2HSB(color);
            hsb.S *= multiplier;
            return HSB2RGB(hsb);
        }
    }
}
