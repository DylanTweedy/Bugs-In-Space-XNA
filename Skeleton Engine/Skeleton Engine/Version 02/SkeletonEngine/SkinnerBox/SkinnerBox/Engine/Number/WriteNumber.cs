using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeletonEngine
{
    public enum SeparatorType
    {
        None, Standard, Indian
    }

    public enum ShortenType
    {
        None, Short_Scale, Long_Scale, British_Long_Scale, Scientific, E_Notation, Vedic, Indian
    }

    static class WriteNumber
    {
        static string Whole;
        static string Decimal;

        static string Suffix;
        
        static private string AddSeparator(string number, SeparatorType Separators)
        {
            int a = 0;

            switch (Separators)
            {
                case SeparatorType.Standard:
                    if (number.Length > 3)
                    {
                        if (number.Contains('.'))
                            a = number.IndexOf('.') - 3;
                        else
                            a = number.Length - 3;

                        for (int i = a; i > 0; i -= 3)
                        {
                            number = number.Insert(i, ",");
                        }
                    }
                    break;

                case SeparatorType.Indian:
                    if (number.Length > 3)
                    {
                        if (number.Contains('.'))
                            if (number.Contains('.'))
                                a = number.IndexOf('.') - 3;
                            else
                                a = number.Length - 3;

                        for (int i = a; i > 0; i -= 2)
                        {
                            number = number.Insert(i, ",");
                        }
                    }
                    break;
            }

            return number;
        }

        static private void SetDecimal(int length)
        {
            if (Decimal.Length > length)
            {
                if (int.Parse(Decimal[length].ToString()) >= 5)
                {
                    //Cycle backwards through number rounding up where required.
                    for (int i = length - 1; i >= 0; i--)
                    {
                        StringBuilder builder = new StringBuilder(Decimal);

                        builder.Remove(i, 1);

                        int num = int.Parse(Decimal[i].ToString()) + 1;

                        //Round last decimal place.
                        if (num < 10)
                        {
                            builder.Insert(i, num + "");
                            Decimal = builder.ToString();
                            break;
                        }
                        else
                        {
                            builder.Insert(i, 0 + "");
                            Decimal = builder.ToString();

                            //Round number if necessary.
                            if (i - 1 < 0)
                                for (int o = Whole.Length - 1; o >= 0; o--)
                                {
                                    builder = new StringBuilder(Whole);

                                    builder.Remove(o, 1);

                                    num = int.Parse(Whole[o].ToString()) + 1;

                                    if (num < 10)
                                    {
                                        builder.Insert(o, num + "");
                                        Whole = builder.ToString();
                                        break;
                                    }
                                    else
                                    {
                                        builder.Insert(o, 0 + "");
                                        Whole = builder.ToString();

                                    }
                                }
                        }
                    }
                }

                //Remove decimal places after specified length.
                Decimal = Decimal.Remove(length);

                
                //Cycle backwards through number removing zeros.
                for (int i = Decimal.Length - 1; i >= 0; i--)
                {
                    if (Decimal[i] == '0')
                        Decimal = Decimal.Remove(i);
                    else
                        break;
                }
            }
        }

        static private void SetSuffix(ShortenType Shorten)
        {
            switch (Shorten)
            {
                #region Numbers
                case ShortenType.Short_Scale:

                    CheckSuffix(" Googol", 100);
                    CheckSuffix(" Untrigintillion", 97);
                    CheckSuffix(" Trigintillion", 94);
                    CheckSuffix(" Novemvigintillion", 91);
                    CheckSuffix(" Octovigintillion", 88);
                    CheckSuffix(" Septemvigintillion", 85);
                    CheckSuffix(" Sesvigintillion", 82);
                    CheckSuffix(" Quinquavigintillion", 79);
                    CheckSuffix(" Quattuorvigintillion", 76);
                    CheckSuffix(" Tresvigintillion", 73);
                    CheckSuffix(" Duovigintillion", 70);
                    CheckSuffix(" Unvigintillion", 67);
                    CheckSuffix(" Vigintillion", 64);
                    CheckSuffix(" Novendecillion", 61);
                    CheckSuffix(" Octodecillion", 58);
                    CheckSuffix(" Septendecillion", 55);
                    CheckSuffix(" Sexdecillion", 52);
                    CheckSuffix(" Quindecillion", 49);
                    CheckSuffix(" Quattuordecillion", 46);
                    CheckSuffix(" Tredecillion", 43);
                    CheckSuffix(" Duodecillion", 40);
                    CheckSuffix(" Undecillion", 37);
                    CheckSuffix(" Decillion", 34);
                    CheckSuffix(" Nonillion", 31);
                    CheckSuffix(" Octillion", 28);
                    CheckSuffix(" Septillion", 25);
                    CheckSuffix(" Sextillion", 22);
                    CheckSuffix(" Quintillion", 19);
                    CheckSuffix(" Quadrillion", 16);
                    CheckSuffix(" Trillion", 13);
                    CheckSuffix(" Billion", 10);
                    CheckSuffix(" Million", 7);
                    break;
                #endregion

                #region

                //#region Scientific
                //case ShortenType.Scientific:

                //    switch (full.Length)
                //    {
                //        case 57:
                //            full = full.Insert(full.Length - 56, ".");
                //            name = " × 10⁵⁶";
                //            break;

                //        case 56:
                //            full = full.Insert(full.Length - 45, ".");
                //            name = " × 10⁵⁵";
                //            break;

                //        case 55:
                //            full = full.Insert(full.Length - 54, ".");
                //            name = " × 10⁵⁴";
                //            break;

                //        case 54:
                //            full = full.Insert(full.Length - 53, ".");
                //            name = " × 10⁵³";
                //            break;

                //        case 53:
                //            full = full.Insert(full.Length - 52, ".");
                //            name = " × 10⁵²";
                //            break;

                //        case 52:
                //            full = full.Insert(full.Length - 51, ".");
                //            name = " × 10⁵¹";
                //            break;

                //        case 51:
                //            full = full.Insert(full.Length - 50, ".");
                //            name = " × 10⁵⁰";
                //            break;

                //        case 50:
                //            full = full.Insert(full.Length - 49, ".");
                //            name = " × 10⁴⁹";
                //            break;

                //        case 49:
                //            full = full.Insert(full.Length - 48, ".");
                //            name = " × 10⁴⁸";
                //            break;

                //        case 48:
                //            full = full.Insert(full.Length - 47, ".");
                //            name = " × 10⁴⁷";
                //            break;
                //        case 47:
                //            full = full.Insert(full.Length - 46, ".");
                //            name = " × 10⁴⁶";
                //            break;

                //        case 46:
                //            full = full.Insert(full.Length - 45, ".");
                //            name = " × 10⁴⁵";
                //            break;

                //        case 45:
                //            full = full.Insert(full.Length - 44, ".");
                //            name = " × 10⁴⁴";
                //            break;

                //        case 44:
                //            full = full.Insert(full.Length - 43, ".");
                //            name = " × 10⁴³";
                //            break;

                //        case 43:
                //            full = full.Insert(full.Length - 42, ".");
                //            name = " × 10⁴²";
                //            break;

                //        case 42:
                //            full = full.Insert(full.Length - 41, ".");
                //            name = " × 10⁴¹";
                //            break;

                //        case 41:
                //            full = full.Insert(full.Length - 40, ".");
                //            name = " × 10⁴⁰";
                //            break;

                //        case 40:
                //            full = full.Insert(full.Length - 39, ".");
                //            name = " × 10³⁹";
                //            break;

                //        case 39:
                //            full = full.Insert(full.Length - 38, ".");
                //            name = " × 10³⁸";
                //            break;

                //        case 38:
                //            full = full.Insert(full.Length - 37, ".");
                //            name = " × 10³⁷";
                //            break;

                //        case 37:
                //            full = full.Insert(full.Length - 36, ".");
                //            name = " × 10³⁶";
                //            break;

                //        case 36:
                //            full = full.Insert(full.Length - 35, ".");
                //            name = " × 10³⁵";
                //            break;

                //        case 35:
                //            full = full.Insert(full.Length - 34, ".");
                //            name = " × 10³⁴";
                //            break;

                //        case 34:
                //            full = full.Insert(full.Length - 33, ".");
                //            name = " × 10³³";
                //            break;

                //        case 33:
                //            full = full.Insert(full.Length - 32, ".");
                //            name = " × 10³²";
                //            break;

                //        case 32:
                //            full = full.Insert(full.Length - 31, ".");
                //            name = " × 10³¹";
                //            break;

                //        case 31:
                //            full = full.Insert(full.Length - 30, ".");
                //            name = " × 10³⁰";
                //            break;

                //        case 30:
                //            full = full.Insert(full.Length - 29, ".");
                //            name = " × 10²⁹";
                //            break;

                //        case 29:
                //            full = full.Insert(full.Length - 28, ".");
                //            name = " × 10²⁸";
                //            break;

                //        case 28:
                //            full = full.Insert(full.Length - 27, ".");
                //            name = " × 10²⁷";
                //            break;

                //        case 27:
                //            full = full.Insert(full.Length - 26, ".");
                //            name = " × 10²⁶";
                //            break;

                //        case 26:
                //            full = full.Insert(full.Length - 25, ".");
                //            name = " × 10²⁵";
                //            break;

                //        case 25:
                //            full = full.Insert(full.Length - 24, ".");
                //            name = " × 10²⁴";
                //            break;

                //        case 24:
                //            full = full.Insert(full.Length - 23, ".");
                //            name = " × 10²³";
                //            break;

                //        case 23:
                //            full = full.Insert(full.Length - 22, ".");
                //            name = " × 10²²";
                //            break;

                //        case 22:
                //            full = full.Insert(full.Length - 21, ".");
                //            name = " × 10²¹";
                //            break;

                //        case 21:
                //            full = full.Insert(full.Length - 20, ".");
                //            name = " × 10²⁰";
                //            break;

                //        case 20:
                //            full = full.Insert(full.Length - 19, ".");
                //            name = " × 10¹⁹";
                //            break;

                //        case 19:
                //            full = full.Insert(full.Length - 18, ".");
                //            name = " × 10¹⁸";
                //            break;

                //        case 18:
                //            full = full.Insert(full.Length - 17, ".");
                //            name = " × 10¹⁷";
                //            break;

                //        case 17:
                //            full = full.Insert(full.Length - 16, ".");
                //            name = " × 10¹⁶";
                //            break;

                //        case 16:
                //            full = full.Insert(full.Length - 15, ".");
                //            name = " × 10¹⁵";
                //            break;

                //        case 15:
                //            full = full.Insert(full.Length - 14, ".");
                //            name = " × 10¹⁴";
                //            break;

                //        case 14:
                //            full = full.Insert(full.Length - 13, ".");
                //            name = " × 10¹³";
                //            break;

                //        case 13:
                //            full = full.Insert(full.Length - 12, ".");
                //            name = " × 10¹²";
                //            break;

                //        case 12:
                //            full = full.Insert(full.Length - 11, ".");
                //            name = " × 10¹¹";
                //            break;

                //        case 11:
                //            full = full.Insert(full.Length - 10, ".");
                //            name = " × 10¹⁰";
                //            break;

                //        case 10:
                //            full = full.Insert(full.Length - 9, ".");
                //            name = " × 10⁹";
                //            break;

                //        case 9:
                //            full = full.Insert(full.Length - 8, ".");
                //            name = " × 10⁸";
                //            break;

                //        case 8:
                //            full = full.Insert(full.Length - 7, ".");
                //            name = " × 10⁷";
                //            break;

                //        case 7:
                //            full = full.Insert(full.Length - 6, ".");
                //            name = " × 10⁶";
                //            break;

                //    }
                //    break;
                //#endregion

                //#region E Notation
                //case ShortenType.E_Notation:
                //    switch (full.Length)
                //    {
                //        case 57:
                //            full = full.Insert(full.Length - 56, ".");
                //            name = "e+56";
                //            break;

                //        case 56:
                //            full = full.Insert(full.Length - 45, ".");
                //            name = "e+55";
                //            break;

                //        case 55:
                //            full = full.Insert(full.Length - 54, ".");
                //            name = "e+54";
                //            break;

                //        case 54:
                //            full = full.Insert(full.Length - 53, ".");
                //            name = "e+53";
                //            break;

                //        case 53:
                //            full = full.Insert(full.Length - 52, ".");
                //            name = "e+52";
                //            break;

                //        case 52:
                //            full = full.Insert(full.Length - 51, ".");
                //            name = "e+51";
                //            break;

                //        case 51:
                //            full = full.Insert(full.Length - 50, ".");
                //            name = "e+50";
                //            break;

                //        case 50:
                //            full = full.Insert(full.Length - 49, ".");
                //            name = "e+49";
                //            break;

                //        case 49:
                //            full = full.Insert(full.Length - 48, ".");
                //            name = "e+48";
                //            break;

                //        case 48:
                //            full = full.Insert(full.Length - 47, ".");
                //            name = "e+47";
                //            break;
                //        case 47:
                //            full = full.Insert(full.Length - 46, ".");
                //            name = "e+46";
                //            break;

                //        case 46:
                //            full = full.Insert(full.Length - 45, ".");
                //            name = "e+45";
                //            break;

                //        case 45:
                //            full = full.Insert(full.Length - 44, ".");
                //            name = "e+44";
                //            break;

                //        case 44:
                //            full = full.Insert(full.Length - 43, ".");
                //            name = "e+43";
                //            break;

                //        case 43:
                //            full = full.Insert(full.Length - 42, ".");
                //            name = "e+42";
                //            break;

                //        case 42:
                //            full = full.Insert(full.Length - 41, ".");
                //            name = "e+41";
                //            break;

                //        case 41:
                //            full = full.Insert(full.Length - 40, ".");
                //            name = "e+40";
                //            break;

                //        case 40:
                //            full = full.Insert(full.Length - 39, ".");
                //            name = "e+39";
                //            break;

                //        case 39:
                //            full = full.Insert(full.Length - 38, ".");
                //            name = "e+38";
                //            break;

                //        case 38:
                //            full = full.Insert(full.Length - 37, ".");
                //            name = "e+37";
                //            break;

                //        case 37:
                //            full = full.Insert(full.Length - 36, ".");
                //            name = "e+36";
                //            break;

                //        case 36:
                //            full = full.Insert(full.Length - 35, ".");
                //            name = "e+35";
                //            break;

                //        case 35:
                //            full = full.Insert(full.Length - 34, ".");
                //            name = "e+34";
                //            break;

                //        case 34:
                //            full = full.Insert(full.Length - 33, ".");
                //            name = "e+33";
                //            break;

                //        case 33:
                //            full = full.Insert(full.Length - 32, ".");
                //            name = "e+32";
                //            break;

                //        case 32:
                //            full = full.Insert(full.Length - 31, ".");
                //            name = "e+31";
                //            break;

                //        case 31:
                //            full = full.Insert(full.Length - 30, ".");
                //            name = "e+30";
                //            break;

                //        case 30:
                //            full = full.Insert(full.Length - 29, ".");
                //            name = "e+29";
                //            break;

                //        case 29:
                //            full = full.Insert(full.Length - 28, ".");
                //            name = "e+28";
                //            break;

                //        case 28:
                //            full = full.Insert(full.Length - 27, ".");
                //            name = "e+27";
                //            break;

                //        case 27:
                //            full = full.Insert(full.Length - 26, ".");
                //            name = "e+26";
                //            break;

                //        case 26:
                //            full = full.Insert(full.Length - 25, ".");
                //            name = "e+25";
                //            break;

                //        case 25:
                //            full = full.Insert(full.Length - 24, ".");
                //            name = "e+24";
                //            break;

                //        case 24:
                //            full = full.Insert(full.Length - 23, ".");
                //            name = "e+23";
                //            break;

                //        case 23:
                //            full = full.Insert(full.Length - 22, ".");
                //            name = "e+22";
                //            break;

                //        case 22:
                //            full = full.Insert(full.Length - 21, ".");
                //            name = "e+21";
                //            break;

                //        case 21:
                //            full = full.Insert(full.Length - 20, ".");
                //            name = "e+20";
                //            break;

                //        case 20:
                //            full = full.Insert(full.Length - 19, ".");
                //            name = "e+19";
                //            break;

                //        case 19:
                //            full = full.Insert(full.Length - 18, ".");
                //            name = "e+18";
                //            break;

                //        case 18:
                //            full = full.Insert(full.Length - 17, ".");
                //            name = "e+17";
                //            break;

                //        case 17:
                //            full = full.Insert(full.Length - 16, ".");
                //            name = "e+16";
                //            break;

                //        case 16:
                //            full = full.Insert(full.Length - 15, ".");
                //            name = "e+15";
                //            break;

                //        case 15:
                //            full = full.Insert(full.Length - 14, ".");
                //            name = "e+14";
                //            break;

                //        case 14:
                //            full = full.Insert(full.Length - 13, ".");
                //            name = "e+13";
                //            break;

                //        case 13:
                //            full = full.Insert(full.Length - 12, ".");
                //            name = "e+12";
                //            break;

                //        case 12:
                //            full = full.Insert(full.Length - 11, ".");
                //            name = "e+11";
                //            break;

                //        case 11:
                //            full = full.Insert(full.Length - 10, ".");
                //            name = "e+10";
                //            break;

                //        case 10:
                //            full = full.Insert(full.Length - 9, ".");
                //            name = "e+9";
                //            break;

                //        case 9:
                //            full = full.Insert(full.Length - 8, ".");
                //            name = "e+8";
                //            break;

                //        case 8:
                //            full = full.Insert(full.Length - 7, ".");
                //            name = "e+7";
                //            break;

                //        case 7:
                //            full = full.Insert(full.Length - 6, ".");
                //            name = "e+6";
                //            break;
                //    }
                //    break;
                //#endregion

                //#region Long Scale
                //case ShortenType.Long_Scale:
                //    if (full.Length >= 55)
                //    {
                //        full = full.Insert(full.Length - 54, ".");
                //        name = " Nonillion";
                //    }
                //    else if (full.Length >= 52)
                //    {
                //        full = full.Insert(full.Length - 51, ".");
                //        name = " Octilliard";
                //    }
                //    else if (full.Length >= 49)
                //    {
                //        full = full.Insert(full.Length - 48, ".");
                //        name = " Octillion";
                //    }
                //    else if (full.Length >= 46)
                //    {
                //        full = full.Insert(full.Length - 45, ".");
                //        name = " Septilliard";
                //    }
                //    else if (full.Length >= 43)
                //    {
                //        full = full.Insert(full.Length - 42, ".");
                //        name = " Septillion";
                //    }
                //    else if (full.Length >= 40)
                //    {
                //        full = full.Insert(full.Length - 39, ".");
                //        name = " Sextilliard";
                //    }
                //    else if (full.Length >= 37)
                //    {
                //        full = full.Insert(full.Length - 36, ".");
                //        name = " Sextillion";
                //    }
                //    else if (full.Length >= 34)
                //    {
                //        full = full.Insert(full.Length - 33, ".");
                //        name = " Quintilliard";
                //    }
                //    else if (full.Length >= 31)
                //    {
                //        full = full.Insert(full.Length - 30, ".");
                //        name = " Quintillion";
                //    }
                //    else if (full.Length >= 28)
                //    {
                //        full = full.Insert(full.Length - 27, ".");
                //        name = " Quadrilliard";
                //    }
                //    else if (full.Length >= 25)
                //    {
                //        full = full.Insert(full.Length - 24, ".");
                //        name = " Quadrillion";
                //    }
                //    else if (full.Length >= 22)
                //    {
                //        full = full.Insert(full.Length - 21, ".");
                //        name = " Trilliard";
                //    }
                //    else if (full.Length >= 19)
                //    {
                //        full = full.Insert(full.Length - 18, ".");
                //        name = " Trillion";
                //    }
                //    else if (full.Length >= 16)
                //    {
                //        full = full.Insert(full.Length - 15, ".");
                //        name = " Billiard";
                //    }
                //    else if (full.Length >= 13)
                //    {
                //        full = full.Insert(full.Length - 12, ".");
                //        name = " Billion";
                //    }
                //    else if (full.Length >= 10)
                //    {
                //        full = full.Insert(full.Length - 9, ".");
                //        name = " Milliard";
                //    }
                //    else if (full.Length >= 7)
                //    {
                //        full = full.Insert(full.Length - 6, ".");
                //        name = " Million";
                //    }
                //    break;
                //#endregion

                //#region British Long Scale
                //case ShortenType.British_Long_Scale:
                //    if (full.Length >= 55)
                //    {
                //        full = full.Insert(full.Length - 54, ".");
                //        name = " Nonillion";
                //    }
                //    else if (full.Length >= 49)
                //    {
                //        full = full.Insert(full.Length - 48, ".");
                //        name = " Octillion";
                //    }
                //    else if (full.Length >= 43)
                //    {
                //        full = full.Insert(full.Length - 42, ".");
                //        name = " Septillion";
                //    }
                //    else if (full.Length >= 37)
                //    {
                //        full = full.Insert(full.Length - 36, ".");
                //        name = " Sextillion";
                //    }
                //    else if (full.Length >= 31)
                //    {
                //        full = full.Insert(full.Length - 30, ".");
                //        name = " Quintillion";
                //    }
                //    else if (full.Length >= 25)
                //    {
                //        full = full.Insert(full.Length - 24, ".");
                //        name = " Quadrillion";
                //    }
                //    else if (full.Length >= 19)
                //    {
                //        full = full.Insert(full.Length - 18, ".");
                //        name = " Trillion";
                //    }
                //    else if (full.Length >= 13)
                //    {
                //        full = full.Insert(full.Length - 12, ".");
                //        name = " Billion";
                //    }
                //    else if (full.Length >= 7)
                //    {
                //        full = full.Insert(full.Length - 6, ".");
                //        name = " Million";
                //    }
                //    break;
                //#endregion

                //#region Vedic
                //case ShortenType.Vedic:
                //    if (full.Length >= 53)
                //    {
                //        full = full.Insert(full.Length - 52, ".");
                //        name = " Samudra";
                //    }
                //    else if (full.Length >= 48)
                //    {
                //        full = full.Insert(full.Length - 47, ".");
                //        name = " Mahā-kharva";
                //    }
                //    else if (full.Length >= 43)
                //    {
                //        full = full.Insert(full.Length - 42, ".");
                //        name = " Kharva";
                //    }
                //    else if (full.Length >= 38)
                //    {
                //        full = full.Insert(full.Length - 37, ".");
                //        name = " Mahā-padma";
                //    }
                //    else if (full.Length >= 33)
                //    {
                //        full = full.Insert(full.Length - 32, ".");
                //        name = " Padma";
                //    }
                //    else if (full.Length >= 28)
                //    {
                //        full = full.Insert(full.Length - 27, ".");
                //        name = " Mahā-vṛnda";
                //    }
                //    else if (full.Length >= 23)
                //    {
                //        full = full.Insert(full.Length - 22, ".");
                //        name = " Vṛnda";
                //    }
                //    else if (full.Length >= 18)
                //    {
                //        full = full.Insert(full.Length - 17, ".");
                //        name = " Mahā-śaṅku";
                //    }
                //    else if (full.Length >= 13)
                //    {
                //        full = full.Insert(full.Length - 12, ".");
                //        name = " Śaṅku";
                //    }
                //    else if (full.Length >= 8)
                //    {
                //        full = full.Insert(full.Length - 7, ".");
                //        name = " Koṭi";
                //    }
                //    else if (full.Length >= 6)
                //    {
                //        full = full.Insert(full.Length - 5, ".");
                //        name = " Lakṣa";
                //    }
                //    break;
                //#endregion

                //#region Indian
                //case ShortenType.Indian:

                //    if (full.Length >= 26)
                //    {
                //        full = full.Insert(full.Length - 25, ".");
                //        name = " Paraardh";
                //    }
                //    else if (full.Length >= 24)
                //    {
                //        full = full.Insert(full.Length - 23, ".");
                //        name = " Madhyam";
                //    }
                //    else if (full.Length >= 22)
                //    {
                //        full = full.Insert(full.Length - 21, ".");
                //        name = " Antya";
                //    }
                //    else if (full.Length >= 20)
                //    {
                //        full = full.Insert(full.Length - 19, ".");
                //        name = " Samudra";
                //    }
                //    else if (full.Length >= 18)
                //    {
                //        full = full.Insert(full.Length - 17, ".");
                //        name = " Shankh";
                //    }
                //    else if (full.Length >= 17)
                //    {
                //        full = full.Insert(full.Length - 16, ".");
                //        name = " Das Padm";
                //    }
                //    else if (full.Length >= 16)
                //    {
                //        full = full.Insert(full.Length - 15, ".");
                //        name = " Padm";
                //    }
                //    else if (full.Length >= 15)
                //    {
                //        full = full.Insert(full.Length - 14, ".");
                //        name = " Ek Karoṛ Karoṛ";
                //    }
                //    else if (full.Length >= 14)
                //    {
                //        full = full.Insert(full.Length - 13, ".");
                //        name = " Neel";
                //    }
                //    else if (full.Length >= 13)
                //    {
                //        full = full.Insert(full.Length - 12, ".");
                //        name = " Ek Lākh Karoṛ";
                //    }
                //    else if (full.Length >= 12)
                //    {
                //        full = full.Insert(full.Length - 11, ".");
                //        name = " Kharab";
                //    }
                //    else if (full.Length >= 11)
                //    {
                //        full = full.Insert(full.Length - 10, ".");
                //        name = " Ek Hazār Karoṛ";
                //    }
                //    else if (full.Length >= 10)
                //    {
                //        full = full.Insert(full.Length - 9, ".");
                //        name = " Arab";
                //    }
                //    else if (full.Length >= 9)
                //    {
                //        full = full.Insert(full.Length - 8, ".");
                //        name = " Das Karoṛ";
                //    }
                //    else if (full.Length >= 8)
                //    {
                //        full = full.Insert(full.Length - 7, ".");
                //        name = " Karoṛ";
                //    }
                //    else if (full.Length >= 7)
                //    {
                //        full = full.Insert(full.Length - 6, ".");
                //        name = " Adant";
                //    }
                //    else if (full.Length >= 6)
                //    {
                //        full = full.Insert(full.Length - 5, ".");
                //        name = " Lākh";
                //    }

                //    break;
                //#endregion

                #endregion
            }
        }

        static private void CheckSuffix(string suffix, int length)
        {
            if (Whole.Length >= length)
            {
                length--;
                Decimal = Decimal.Insert(0, Whole.Remove(0, Whole.Length - length));
                Whole = Whole.Remove(Whole.Length - length);
                Suffix = suffix + Suffix;
            }
        }

        static public string Write(BigDecimal num, SeparatorType separator, bool Decimals, ShortenType shortenType)
        {
            //if (num < 10)
            //    return Write(num, separator, 3, shortenType);
            //else if (num < 100)
            //    return Write(num, separator, 2, shortenType);
            //else if (num < 1000)
            //    return Write(num, separator, 1, shortenType);
            if (num < 1000000)
                return Write(num, separator, 0, shortenType);
            else
                return Write(num, separator, 3, shortenType); 
        }

        static public string Write(BigDecimal num, SeparatorType separator, int DecimalLength, ShortenType shortenType)
        {
            string fullNumber = num.ToString();

            if (fullNumber.Contains('.'))
            {
                string[] words = fullNumber.Split('.');

                Whole = words[0];
                Decimal = words[1];
            }
            else
            {
                Whole = fullNumber;
                Decimal = "";
            }
            Suffix = "";
            
            SetSuffix(shortenType);
            
            SetDecimal(DecimalLength);
            if (Decimal != string.Empty)
                return (AddSeparator(Whole, separator) + "." + Decimal + Suffix);
            else
                return (AddSeparator(Whole, separator) + Suffix);
        }
    }
}
