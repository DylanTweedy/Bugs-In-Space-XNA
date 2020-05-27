using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    /// <summary>
    /// Generates a random string from three components.
    /// </summary>
    [Serializable()]
    class RandomStringGenerator
    {
        /// <summary>
        /// The words to go at the beginning of the string.
        /// </summary>
        List<string> Prefix;
        /// <summary>
        /// The words to go in the middle of the string.
        /// </summary>
        List<string> Middle;
        /// <summary>
        /// The words to go at the end of the string.
        /// </summary>
        List<string> Suffix;

        /// <summary>
        /// List of vowels.
        /// </summary>
        List<string> Vowels = new List<string> { "a", "e", "i", "o", "u" };

        Random rand;

        /// <summary>
        /// Generates a random string from three components.
        /// </summary>
        /// <param name="PrefixLocation">The words to go at the beginning of the string.</param>
        /// <param name="BeginningLocation">The words to go in the middle of the string.</param>
        /// <param name="EndLocation">The words to go at the end of the string.</param>
        /// <param name="AddVowelVariation">Whether or not to add vowels to the list. 
        /// X = prefix, Y = middle, Z = suffix
        /// 0 = false</param>
        public RandomStringGenerator(string PrefixLocation, string BeginningLocation, string EndLocation, Vector3 AddVowelVariation, int RandomSeed)
        {
            //Create random.
            if (RandomSeed == -1)
                rand = new Random();
            else
                rand = new Random(RandomSeed);

            string[] lines;

            #region Load Code (Needs work, check EverNote)

            if (PrefixLocation != string.Empty)
            {
                lines = System.IO.File.ReadAllLines(PrefixLocation);
                Prefix = lines.ToList();
            }
            else
                Prefix = new List<string>();

            if (BeginningLocation != string.Empty)
            {
            lines = System.IO.File.ReadAllLines(BeginningLocation);
            Middle = lines.ToList();
            }
            else
                Middle = new List<string>();

            if (EndLocation != string.Empty)
            {
            lines = System.IO.File.ReadAllLines(EndLocation);
            Suffix = lines.ToList();
            }
            else
                Suffix = new List<string>();

            #endregion

            #region Vowel Variations

            if (AddVowelVariation != Vector3.Zero)
            {
                if (AddVowelVariation.X != 0f)
                    for (int o = 0; o < Vowels.Count; o++)
                    {
                        Prefix.Add(Vowels[o]);
                        for (int u = 0; u < Vowels.Count; u++)
                            Prefix.Add(Vowels[o] + Vowels[u]);
                    }

                if (AddVowelVariation.Y != 0f)
                    for (int o = 0; o < Vowels.Count; o++)
                    {
                        Middle.Add(Vowels[o]);
                        for (int u = 0; u < Vowels.Count; u++)
                            Middle.Add(Vowels[o] + Vowels[u]);
                    }

                if (AddVowelVariation.Z != 0f)
                    for (int o = 0; o < Vowels.Count; o++)
                    {
                        Suffix.Add(Vowels[o]);
                        for (int u = 0; u < Vowels.Count; u++)
                            Suffix.Add(Vowels[o] + Vowels[u]);
                    }
            }

            #endregion

            //Make all text lowercase.
            for (int i = 0; i < Prefix.Count; i++)
                Prefix[i] = Prefix[i].ToLower();
            for (int i = 0; i < Middle.Count; i++)
                Middle[i] = Middle[i].ToLower();
            for (int i = 0; i < Suffix.Count; i++)
                Suffix[i] = Suffix[i].ToLower();

            #region Remove duplicates.

            for (int i = 0; i < Prefix.Count; i++)
                for (int o = i + 1; o < Prefix.Count; o++)
                    if (Prefix[i] == Prefix[o])
                    {
                        Prefix.RemoveAt(i);
                        o--;
                    }
            for (int i = 0; i < Middle.Count; i++)
                for (int o = i + 1; o < Middle.Count; o++)
                    if (Middle[i] == Middle[o])
                    {
                        Middle.RemoveAt(i);
                        o--;
                    }
            for (int i = 0; i < Suffix.Count; i++)
                for (int o = i + 1; o < Suffix.Count; o++)
                    if (Suffix[i] == Suffix[o])
                    {
                        Suffix.RemoveAt(i);
                        o--;
                    }

            #endregion
        }
        
        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="PrefixChance">Chance of a prefix in percent. Between 0 and 100.</param>
        /// <param name="MaxMiddles">Maximum number of times the middle word can repeatidly generate.</param>
        /// <param name="SuffixChance">Chance of a suffix in percent. Between 0 and 100.</param>
        /// <param name="Spaces">Whether or not to place spaces between generated words.</param>
        /// <param name="Capitalize">Whether or not to capitalize the begining of each word.</param>
        /// <param name="Hyphenate">Whether or not to randomly add hyphens between repeated middle words.</param>
        /// <returns></returns>
        public string Generate(int PrefixChance, int MaxMiddles, int SuffixChance, bool Spaces, bool Capitalize, bool Hyphenate)
        {
            bool capitalize = Capitalize;
            string Name = "";

            #region Add Prefix

            if (PrefixChance > 0 && Prefix.Count > 0)
                if (PrefixChance > rand.Next(0, 100))
                {
                    if (Capitalize)
                        Name = Name + UsefulMethods.UppercaseFirst(Prefix[rand.Next(0, Prefix.Count)]);
                    else
                        Name = Name + Prefix[rand.Next(0, Prefix.Count)];

                    if (Spaces)
                        Name += " ";
                }

            #endregion

            #region Add Middle

            if (MaxMiddles > 0 && Middle.Count > 0)
            {
                if (MaxMiddles <= 0)
                    MaxMiddles = 1;

                int MiddleCount = rand.Next(1, MaxMiddles + 1);

                for (int i = 0; i < MiddleCount; i++)
                {
                    if (capitalize)
                        Name = Name + UsefulMethods.UppercaseFirst(Middle[rand.Next(0, Middle.Count)]);
                    else
                        Name = Name + Middle[rand.Next(0, Middle.Count)];

                    if (Spaces)
                        Name += " ";

                    if (i != MiddleCount - 1)
                    {
                        if (Hyphenate && rand.Next(0, 2) == 0)
                        {
                            capitalize = Capitalize;
                            Name = Name + "-";
                        }
                        else
                        {
                            capitalize = false;
                            SuffixChance /= 2;
                        }
                    }
                }
            }

            #endregion

            //Remove one space if two end at the end of the string.
            if (Name.Length >= 2)
                while (Name.ElementAt(Name.Length - 2) == ' ')
                    Name = Name.Remove(Name.Length - 2);

            //Add Suffix
            if (SuffixChance > 0 && Suffix.Count > 0)
                if (SuffixChance > rand.Next(0, 100))
                {
                    Name = Name + Suffix[rand.Next(0, Suffix.Count)];
                }

            //Remove spaces at the end of the string.
            if (Name.Length != 0)
                while (Name.ElementAt(Name.Length - 1) == ' ')
                    Name = Name.Remove(Name.Length - 1);

            return Name;
        }
    }
}
