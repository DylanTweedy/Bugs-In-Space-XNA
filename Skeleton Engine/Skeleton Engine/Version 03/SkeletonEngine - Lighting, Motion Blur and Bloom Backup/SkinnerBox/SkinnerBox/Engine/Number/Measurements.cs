using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeletonEngine
{
    static class Measurements
    {
        public class Conversion
        {
            public string Name;
            public string NameSingle;
            public string Symbol;
            public double Multiplier;
            public double GValue;

            public Conversion(string name, string nameSingle, string symbol, double multiplier)
            {
                Name = name;
                NameSingle = nameSingle;
                Symbol = symbol;
                Multiplier = multiplier;
                GValue = 1 / multiplier;
            }
        }

        static public Dictionary<string, List<string>>[] Groups;
        static public Dictionary<string, Conversion>[] ConversionData;

        static List<string> Group;

        /// <summary>
        /// <para>Add a conversion to current group.</para>
        /// </summary>
        /// 
        /// <param name="name">
        /// <para>Unit name.</para>
        /// </param>
        /// 
        /// <param name="nameSingle">
        /// <para>Unit singular name. (eg. Inches would be Inch.)</para>
        /// </param>
        /// 
        /// <param name="symbol">
        /// <para>Unit symbol. (Use "ICON#" for symbols that need images.)</para>
        /// </param>
        /// 
        /// <param name="multiplier">
        /// <para>How much of this unit is in 1 gram?</para>
        /// </param>
        /// 
        /// <param name="type">
        /// <para>1 = Mass</para>
        /// <para>2 = Distance</para>
        /// <para>3 = Time</para>
        /// <para>4 = Temperature</para>
        /// <para>5 = Ampere</para>
        /// <para>6 = Mole</para>
        /// <para>7 = Candela</para>
        /// </param>
        static void AddConversion(string name, string nameSingle, string symbol, double multiplier, byte type)
        {
            ConversionData[type].Add(name, new Conversion(name, nameSingle, symbol, multiplier));
            Group.Add(name);
        }

        /// <summary>
        /// <para>Creates a measurement group.</para>
        /// </summary>
        /// 
        /// <param name="groupName">
        /// <para>Group name.</para>
        /// </param>
        /// 
        /// <param name="type">
        /// <para>1 = Mass</para>
        /// <para>2 = Distance</para>
        /// <para>3 = Time</para>
        /// <para>4 = Temperature</para>
        /// <para>5 = Ampere</para>
        /// <para>6 = Mole</para>
        /// <para>7 = Candela</para>
        /// </param>
        static void CreateGroup(string groupName, byte type)
        {
            List<string> groups = new List<string>();

            string o;

            while (Group.Count != 0)
            {
                o = ConversionData[type][Group[0]].Name;

                for (int i = 0; i < Group.Count; i++)
                {
                    if (ConversionData[type][o].Multiplier > ConversionData[type][Group[i]].Multiplier)
                        o = ConversionData[type][Group[i]].Name;
                }

                groups.Add(o);
                Group.Remove(o);
            }

            Groups[type].Add(groupName, groups);
        }

        /// <summary>
        /// Clears current group.
        /// </summary>
        static void ClearGroup()
        {
            Group.Clear();
        }

        /// <summary>
        /// Add existing Unit to current group.
        /// </summary>
        /// <param name="name">Unit name.</param>
        static void AddToGroup(string name)
        {
            Group.Add(name);
        }

        /// <summary>
        /// Mass conversion.
        /// </summary>
        /// <param name="type">Number reference.</param>
        static void Mass(byte type)
        {
            AddConversion("Grams", "Gram", "g", 1f, type);
            AddConversion("Kilograms", "Kilogram", "kg", 1e-3, type);
            AddConversion("Tonnes", "Tonne", "t", 1e-6, type);
            AddConversion("Kilotonnes", "Kilotonne", "kt", 1e-9, type);
            AddConversion("Megatonnes", "Megatonne", "Mt", 1e-12, type);
            AddConversion("Gigatonnes", "Gigatonne", "Gt", 1e-15, type);
            AddConversion("Teratonnes", "Teratonne", "Tt", 1e-18, type);
            AddConversion("Petatonnes", "Petatonne", "Pt", 1e-21, type);
            AddConversion("Exatonnes", "Exatonne", "Et", 1e-24, type);
            AddConversion("Zettatonnes", "Zettatonne", "Zt", 1e-27, type);
            AddConversion("Yottatonnes", "Yottatonne", "Yt", 1e-30, type);
            AddConversion("Solar Mass", "Solar Mass", "ICON01", 5.02739933e-34, type);
            CreateGroup("Metric", type);
            
            AddToGroup("Grams");
            AddToGroup("Kilograms");
            AddToGroup("Solar Mass");
            AddConversion("Megagrams", "Megagram", "Mg", 1e-6, type);
            AddConversion("Gigagrams", "Gigagram", "Gg", 1e-9, type);
            AddConversion("Teragrams", "Teragram", "Tg", 1e-12, type);
            AddConversion("Petagrams", "Petagram", "Pg", 1e-15, type);
            AddConversion("Exagrams", "Exagram", "Eg", 1e-18, type);
            AddConversion("Zettagrams", "Zettagram", "Zg", 1e-21, type);
            AddConversion("Yottagrams", "Yottagram", "Yg", 1e-24, type);
            CreateGroup("SI Grams", type);
            
            AddToGroup("Solar Mass");
            AddConversion("Grains", "Grains", "gr", 15.4323584, type);
            AddConversion("Drams", "Dram", "dr", 0.564383391f, type);
            AddConversion("Ounces", "Ounce", "oz", 0.035274f, type);
            AddConversion("Pounds", "Pound", "lb", 0.00220462f, type);
            AddConversion("Stone", "Stone", "st", 0.000157473f, type);
            AddConversion("Quarters", "Quarter", "qtr", 0.00007873642f, type);
            AddConversion("Hundredweights", "Hundredweight", "cwt", 0.0000196841f, type);
            AddConversion("Tons", "Ton", "t", 0.00000098421f, type);
            CreateGroup("Imperial", type);

            AddConversion("Humps", "Hump", "H", 1, type);
            AddConversion("Bumps", "Bump", "B", 1 / 30d, type);
            AddConversion("Dumps", "Dump", "D", 1 / 900d, type);
            AddConversion("Rumps", "Rump", "R", 1 / 27000d, type);
            AddConversion("Lumps", "Lump", "L", 1 / 810000d, type);
            AddConversion("Numps", "Nump", "W", 1 / 24300000d, type);
            AddConversion("Mumps", "Mump", "M", 1 / 729000000d, type);
            AddConversion("Stumps", "Stump", "St", 1 / 21870000000d, type);
            AddConversion("Frumps", "Frump", "Fr", 1 / 656100000000d, type);
            AddConversion("Plumps", "Plump", "Pl", 1 / 1.9683e+13d, type);
            AddConversion("Krumps", "Krump", "Kr", 1 / 5.9049e+14d, type);
            AddConversion("Chumps", "Chump", "Ch", 1 / 1.77147e+16d, type);
            AddConversion("Brumps", "Brump", "Br", 1 / 5.31441e+17d, type);
            AddConversion("Wuggletumps", "Wuggletump", "", 1 / 1.594323e+19d, type);
            AddConversion("Wugglebumps", "Wugglebump", "", 1 / 4.782969e+20d, type);
            AddConversion("Wuggleyumps", "Wuggleyump", "", 1 / 1.4348907e+22d, type);
            AddConversion("Wuggleflumps", "Wuggleflump", "", 1 / 4.3046721e+23d, type);
            AddConversion("Wuggletrumps", "Wuggletrump", "", 1 / 1.2914016e+25d, type);
            AddConversion("Hugglegrumps", "Hugglegrump", "", 1 / 3.8742049e+26d, type);
            AddConversion("Hugglewumps", "Hugglewump", "", 1 / 1.1622615e+28d, type);
            AddConversion("Hugglekumps", "Hugglekump", "", 1 / 3.4867844e+29d, type);
            AddConversion("Hugglejumps", "Hugglejump", "", 1 / 1.0460353e+31d, type);
            AddConversion("Pugglegumps", "Pugglegump", "", 1 / 3.138106e+32d, type);
            AddConversion("Puggleflumps", "Puggleflump", "", 1 / 9.4143179e+33d, type);
            AddConversion("Puggletumps", "Puggletump", "", 1 / 2.8242954e+35d, type);
            AddConversion("Snugglepumps", "Snugglepump", "", 1 / 8.4728861e+36d, type);
            AddConversion("Snuggledrumps", "Snuggledrump", "", 1 / 2.5418658e+38d, type);
            AddConversion("Geromies", "Geromy", "", 1 / 7.6255975e+39, type);
            CreateGroup("Umps", type);
        }

        /// <summary>
        /// Distance conversion.
        /// </summary>
        /// <param name="type">Number reference.</param>
        static void Distance(byte type)
        {
            AddConversion("Centimetres", "Centimetre", "cm", 1, type);
            AddConversion("Metres", "Metre", "m", 1e-2, type);
            AddConversion("Kilometres", "Kilometre", "km", 1e-5, type);
            AddConversion("Megametres", "Megametre", "Mm", 1e-8, type);
            AddConversion("Gigametres", "Gigametre", "Gm", 1e-11, type);
            AddConversion("Terametres", "Terametre", "Tm", 1e-14, type);
            AddConversion("Petametres", "Petametre", "Pm", 1e-17, type);
            AddConversion("Exametres", "Exametre", "Em", 1e-20, type);
            AddConversion("Zettametres", "Zettametre", "Zm", 1e-23, type);
            AddConversion("Yottametres", "Yottametre", "Ym", 1e-26, type);
            AddConversion("Astronomical Units", "Astronomical Unit", "AU", 6.68458712e-14, type);
            AddConversion("Light-years", "Light-year", "ly", 1.05702341e-18, type);
            CreateGroup("Metric", type);

            AddConversion("Centimeters", "Centimeter", "cm", 1, type);
            AddConversion("Meters", "Meter", "m", 1e-2, type);
            AddConversion("Kilometers", "Kilometer", "km", 1e-5, type);
            AddConversion("Megameters", "Megameter", "Mm", 1e-8, type);
            AddConversion("Gigameters", "Gigameter", "Gm", 1e-11, type);
            AddConversion("Terameters", "Terameter", "Tm", 1e-14, type);
            AddConversion("Petameters", "Petameter", "Pm", 1e-17, type);
            AddConversion("Exameters", "Exameter", "Em", 1e-20, type);
            AddConversion("Zettameters", "Zettameter", "Zm", 1e-23, type);
            AddConversion("Yottameters", "Yottameter", "Ym", 1e-26, type);
            AddToGroup("Astronomical Units");
            AddConversion("Light Years", "Light Year", "ly", 1.05702341e-18, type);
            CreateGroup("Metric 2", type);

            AddConversion("Thou", "Thou", "th", 393.700787, type);
            AddConversion("Inches", "Inch", "in", 0.393700787, type);
            AddConversion("Feet", "Foot", "ft", 0.032808399, type);
            AddConversion("Yards", "Yard", "yd", 0.010936133, type);
            AddConversion("Chains", "Chain", "ch", 0.000497096954, type);
            AddConversion("Furlongs", "Furlong", "fur", 4.97096954e-5, type);
            AddConversion("Miles", "Mile", "mi", 6.21371192e-6, type);
            AddConversion("Leagues", "League", "lea", 1.79985601e-6, type);
            AddToGroup("Astronomical Units");
            AddConversion("Parsecs", "Parsec", "pc", 3.24077929e-19, type);
            CreateGroup("Imperial", type);

            AddToGroup("Inches");
            AddConversion("Foot", "Foot", "ft", 0.032808399, type);
            AddToGroup("Miles");
            AddToGroup("Astronomical Units");
            AddToGroup("Parsecs");
            CreateGroup("Imperial 2", type);
        }

        /// <summary>
        /// <para>Time conversion.</para>
        /// <para>NEEDS WORK. (Measures earth time, may need to come back to this.)</para>
        /// </summary>
        /// <param name="type">Number reference.</param>
        static void Time(byte type)
        {
            AddConversion("Seconds", "Second", "s", 1, type);
            AddConversion("Minutes", "Minute", "m", 1 / 60d, type);
            AddConversion("Hours", "Hour", "h", 1 / 3600d, type);
            AddConversion("Days", "Day", "d", 1 / 86400d, type);
            AddConversion("Weeks", "Week", "w", 1 / 604800d, type);
            AddConversion("Months", "Month", "m", 1 / 2.62974e+6d, type);
            AddConversion("Years", "Year", "y", 1 / 3.15569e+7d, type);
            CreateGroup("Time 1", type);

            AddToGroup("Seconds");
            AddToGroup("Minutes");
            AddToGroup("Hours");
            AddToGroup("Days");
            AddToGroup("Weeks");
            AddConversion("Fortnights", "Fortnight", "f", 1 / 1209600d, type);
            AddToGroup("Months");
            AddToGroup("Years");
            AddConversion("Decades", "Decade", "D", 1 / 3.15569e+8d, type);
            AddConversion("Centuries", "Century", "C", 1 / 3.15569e+9d, type);
            AddConversion("Millennia", "Millenium", "Mi", 1 / 3.1556926e+10d, type);
            AddConversion("Mega-annums", "Mega-annum", "Me", 1 / 3.15569e+13d, type);
            AddConversion("Giga-annums", "Giga-annum", "G", 1 / 3.15569e+16d, type);
            CreateGroup("Time 2", type);

            AddToGroup("Seconds");
            AddToGroup("Minutes");
            AddToGroup("Hours");
            AddToGroup("Days");
            AddToGroup("Weeks");
            AddToGroup("Months");
            CreateGroup("Time 3", type);

            AddToGroup("Seconds");
            AddToGroup("Minutes");
            AddToGroup("Hours");
            AddToGroup("Days");
            CreateGroup("Time 4", type);

            AddConversion("Instants", "Instant", "I", 1 / 0.01d, type);
            AddConversion("Jiffies", "Jiffy", "J", 1 / 0.5d, type);
            AddConversion("Moments", "Moment", "M", 1 / 80d, type);
            AddConversion("Generations", "Generation", "G", 1 / 8.2048e+8d, type);
            AddConversion("Ages", "Age", "A", 1 / 7.88923e+9d, type);
            AddConversion("Epochs", "Epoch", "Ep", 1 / 1.57785e+10d, type);
            AddConversion("Eons", "Eon", "Eo", 1 / 4.73354e+10d, type);
            CreateGroup("Unspecified", type);

            AddToGroup("Seconds");
            AddConversion("Decaseconds", "Decasecond", "das", 1 / 1E+1d, type);
            AddConversion("Hectoseconds", "Hectosecond", "hs", 1 / 1E+2d, type);
            AddConversion("Kiloseconds", "Kilosecond", "ks", 1 / 1E+3d, type);
            AddConversion("Megaseconds", "Megasecond", "Ms", 1 / 1E+6d, type);
            AddConversion("Gigasecond", "Gigasecond", "Gs", 1 / 1E+9d, type);
            AddConversion("Teraseconds", "Terasecond", "Ts", 1 / 1E+12d, type);
            AddConversion("Petaseconds", "Petasecond", "Ps", 1 / 1E+15d, type);
            AddConversion("Exaseconds", "Exasecond", "Es", 1 / 1E+18d, type);
            AddConversion("Zettaseconds", "Zettasecond", "Zs", 1 / 1E+21d, type);
            AddConversion("Yottaseconds", "Yottasecond", "Ys", 1 / 1E+24d, type);
            CreateGroup("Seconds", type);
        }

        /// <summary>
        /// Temperature conversion.
        /// </summary>
        /// <param name="type">Number reference.</param>
        static void Temperature(byte type)
        {
            AddConversion("Kelvin", "Kelvin", "K", 1, type);
            AddConversion("Celcius", "Celcius", "K", 1, type);
            AddConversion("Fahrenheit", "Fahrenheit", "K", 1, type);
            ClearGroup();
        }

        /// <summary>
        /// Loads measurement information.
        /// </summary>
        static public void Initialize()
        {
            Groups = new Dictionary<string, List<string>>[7];
            ConversionData = new Dictionary<string, Conversion>[7];

            for (int i = 0; i < 7; i++)
            {
                Groups[i] = new Dictionary<string, List<string>>();
                ConversionData[i] = new Dictionary<string, Conversion>();
            }

            Group = new List<string>();

            Mass(1);
            Distance(2);
            Time(3);
            Temperature(4);
        }
    }
}
