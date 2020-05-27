using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class ColorPicker
    {
        Random rand;
        ColorConverter converter;
        bool rare;

        public bool Rare
        {
            get { return rare; }
            set { rare = value; }
        }

        public ColorPicker(Random Rand)
        {
            rand = Rand;
            converter = new ColorConverter();
        }

        public Color RandomColour()
        {
            return converter.HSL2RGB(
                    rand.Next(0, 359),
                    rand.Next(10, 90),
                    rand.Next(10, 90), 255);
        }

        public Color HumanSkinTones(int RareChance)
        {
            bool rare = false;

            if (rand.Next(RareChance, 100000) <= RareChance)
                rare = true;

            if (rare)
            {
                return RandomColour();
            }
            else
            {
                return converter.HSL2RGB(
                rand.Next(20, 40),
                rand.Next(40, 80),
                rand.Next(40, 80), 255);
            }
        }

        public Color HumanHairTones(int RareChance)
        {
            bool rare = false;

            if (rand.Next(RareChance, 100000) <= RareChance)
                rare = true;

            if (rare)
            {
                return RandomColour();
            }
            else
            {
                int type = rand.Next(0, 4);
                
                switch (type)
                {
                    case 1:
                        return converter.HSL2RGB(
                        rand.Next(20, 30),
                        rand.Next(30, 60),
                        rand.Next(20, 50), 255);

                    case 2:
                        return converter.HSL2RGB(
                        rand.Next(10, 30),
                        rand.Next(90, 100),
                        rand.Next(50, 60), 255);

                    case 3:
                        return converter.HSL2RGB(
                        rand.Next(35, 50),
                        rand.Next(80, 100),
                        rand.Next(60, 80), 255);

                    default:
                        return converter.HSL2RGB(
                        rand.Next(0, 50),
                        0,
                        rand.Next(10, 15), 255);
                }
            }
        }

        public Color SchemeCompliment(Color Original)
        {
            converter.RGB2HSL(Original);

            converter.h += 180;
            if (converter.h >= 360)
                converter.h -= 360;
            
            return converter.HSL2RGB(converter.h, converter.s, converter.l, Original.A);
        }

    }
}
