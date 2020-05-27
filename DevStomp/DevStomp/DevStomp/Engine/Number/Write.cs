using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeletonEngine
{
    static class Write
    {
        private struct NumberType
        {
            public string full;
            public string number;
        }

        /// <summary>
        /// <para>Cleans up a number string.</para>
        /// <para>NEEDS WORK. (Decimal cuts off the end, rather than rounding.)</para>
        /// </summary>
        /// <param name="full">Full number.</param>
        /// <param name="Decimals">Decimal points to round to.</param>
        /// <param name="d">Full numbers decimal points.</param>
        /// <param name="Seperators">
        /// <para>Where to place commas.</para>
        /// <para>0 = Disabled</para>
        /// <para>1 = 1,000,000 Standard</para>
        /// <para>2 = 10,00,000 Indian</para>
        /// </param>
        /// <returns></returns>
        static private string CleanUp(string full, int Decimals, string d, byte Seperators)
        {
            #region Commas

            if (full.Length > 3)
            {
                int a = 0;

                switch (Seperators)
                {
                    case 1:
                        if (full.Contains('.'))
                            a = full.IndexOf('.') - 3;
                        else
                            a = full.Length - 3;

                        for (int i = a; i > 0; i -= 3)
                        {
                            full = full.Insert(i, ",");
                        }
                        break;

                    case 2:
                        if (full.Contains('.'))
                            a = full.IndexOf('.') - 3;
                        else
                            a = full.Length - 3;

                        for (int i = a; i > 0; i -= 2)
                        {
                            full = full.Insert(i, ",");
                        }
                        break;
                }
            }

            #endregion

            #region Decimals

            if (full.Contains('.'))
            {
                if (full.IndexOf('.') + Decimals < full.Length)
                    full = full.Remove(full.IndexOf('.') + Decimals);
                else if (d != "")
                {
                    d = d.Remove(0, 1);
                    full = full + d;

                    if (full.IndexOf('.') + Decimals < full.Length)
                        full = full.Remove(full.IndexOf('.') + Decimals);
                }
            }
            else
            {
                full = full + d;

                if (full.Contains('.'))
                    if (full.IndexOf('.') + Decimals < full.Length)
                        full = full.Remove(full.IndexOf('.') + Decimals);
            }

            if (Decimals > 0)
            {
                int o = 0;

                if (full.Contains('.'))
                    for (int i = full.IndexOf('.') + 1; i < full.Length; i++)
                    {
                        if (full.ElementAt(i) == '0')
                            o++;

                        if (o == full.Length - (full.IndexOf('.') + 1))
                            full = full.Remove(full.IndexOf('.'));
                    }
            }

            #endregion

            return full;
        }

        /// <summary>
        /// Shortens a number string in a given way.
        /// </summary>
        /// <param name="Shorten">
        /// <para>Shorten type.</para>
        /// <para>0 = Disabled </para>
        /// <para>1 = Numbers (Million)</para>
        /// <para>2 = Scientific (1 x 10⁷)</para>
        /// <para>3 = E Notation (1E+7)</para>
        /// <para>4 = Long Scale (Millards/Billiards)</para>
        /// <para>5 = Long Scale 2</para>
        /// <para>6 = Vedic</para>
        /// <para>7 = Indian</para>
        /// </param>
        /// <param name="full">Full number.</param>
        /// <returns></returns>
        static private NumberType Short(byte Shorten, string full)
        {
            NumberType n = new NumberType();
            string number = "";

            switch (Shorten)
            {
                #region Numbers
                case 1:
                    if (full.Length >= 55)
                    {
                        full = full.Insert(full.Length - 54, ".");
                        number = " Septendecillion";
                    }
                    else if (full.Length >= 52)
                    {
                        full = full.Insert(full.Length - 51, ".");
                        number = " Sexdecillion";
                    }
                    else if (full.Length >= 49)
                    {
                        full = full.Insert(full.Length - 48, ".");
                        number = " Quindecillion";
                    }
                    else if (full.Length >= 46)
                    {
                        full = full.Insert(full.Length - 45, ".");
                        number = " Quattuordecillion";
                    }
                    else if (full.Length >= 43)
                    {
                        full = full.Insert(full.Length - 42, ".");
                        number = " Tredecillion";
                    }
                    else if (full.Length >= 40)
                    {
                        full = full.Insert(full.Length - 39, ".");
                        number = " Duodecillion";
                    }
                    else if (full.Length >= 37)
                    {
                        full = full.Insert(full.Length - 36, ".");
                        number = " Undecillion";
                    }
                    else if (full.Length >= 34)
                    {
                        full = full.Insert(full.Length - 33, ".");
                        number = " Decillion";
                    }
                    else if (full.Length >= 31)
                    {
                        full = full.Insert(full.Length - 30, ".");
                        number = " Nonillion";
                    }
                    else if (full.Length >= 28)
                    {
                        full = full.Insert(full.Length - 27, ".");
                        number = " Octillion";
                    }
                    else if (full.Length >= 25)
                    {
                        full = full.Insert(full.Length - 24, ".");
                        number = " Septillion";
                    }
                    else if (full.Length >= 22)
                    {
                        full = full.Insert(full.Length - 21, ".");
                        number = " Sextillion";
                    }
                    else if (full.Length >= 19)
                    {
                        full = full.Insert(full.Length - 18, ".");
                        number = " Quintillion";
                    }
                    else if (full.Length >= 16)
                    {
                        full = full.Insert(full.Length - 15, ".");
                        number = " Quadrillion";
                    }
                    else if (full.Length >= 13)
                    {
                        full = full.Insert(full.Length - 12, ".");
                        number = " Trillion";
                    }
                    else if (full.Length >= 10)
                    {
                        full = full.Insert(full.Length - 9, ".");
                        number = " Billion";
                    }
                    else if (full.Length >= 7)
                    {
                        full = full.Insert(full.Length - 6, ".");
                        number = " Million";
                    }
                    break;
                #endregion

                #region Scientific
                case 2:

                    switch (full.Length)
                    {
                        case 57:
                            full = full.Insert(full.Length - 56, ".");
                            number = " × 10⁵⁶";
                            break;

                        case 56:
                            full = full.Insert(full.Length - 45, ".");
                            number = " × 10⁵⁵";
                            break;

                        case 55:
                            full = full.Insert(full.Length - 54, ".");
                            number = " × 10⁵⁴";
                            break;

                        case 54:
                            full = full.Insert(full.Length - 53, ".");
                            number = " × 10⁵³";
                            break;

                        case 53:
                            full = full.Insert(full.Length - 52, ".");
                            number = " × 10⁵²";
                            break;

                        case 52:
                            full = full.Insert(full.Length - 51, ".");
                            number = " × 10⁵¹";
                            break;

                        case 51:
                            full = full.Insert(full.Length - 50, ".");
                            number = " × 10⁵⁰";
                            break;

                        case 50:
                            full = full.Insert(full.Length - 49, ".");
                            number = " × 10⁴⁹";
                            break;

                        case 49:
                            full = full.Insert(full.Length - 48, ".");
                            number = " × 10⁴⁸";
                            break;

                        case 48:
                            full = full.Insert(full.Length - 47, ".");
                            number = " × 10⁴⁷";
                            break;
                        case 47:
                            full = full.Insert(full.Length - 46, ".");
                            number = " × 10⁴⁶";
                            break;

                        case 46:
                            full = full.Insert(full.Length - 45, ".");
                            number = " × 10⁴⁵";
                            break;

                        case 45:
                            full = full.Insert(full.Length - 44, ".");
                            number = " × 10⁴⁴";
                            break;

                        case 44:
                            full = full.Insert(full.Length - 43, ".");
                            number = " × 10⁴³";
                            break;

                        case 43:
                            full = full.Insert(full.Length - 42, ".");
                            number = " × 10⁴²";
                            break;

                        case 42:
                            full = full.Insert(full.Length - 41, ".");
                            number = " × 10⁴¹";
                            break;

                        case 41:
                            full = full.Insert(full.Length - 40, ".");
                            number = " × 10⁴⁰";
                            break;

                        case 40:
                            full = full.Insert(full.Length - 39, ".");
                            number = " × 10³⁹";
                            break;

                        case 39:
                            full = full.Insert(full.Length - 38, ".");
                            number = " × 10³⁸";
                            break;

                        case 38:
                            full = full.Insert(full.Length - 37, ".");
                            number = " × 10³⁷";
                            break;

                        case 37:
                            full = full.Insert(full.Length - 36, ".");
                            number = " × 10³⁶";
                            break;

                        case 36:
                            full = full.Insert(full.Length - 35, ".");
                            number = " × 10³⁵";
                            break;

                        case 35:
                            full = full.Insert(full.Length - 34, ".");
                            number = " × 10³⁴";
                            break;

                        case 34:
                            full = full.Insert(full.Length - 33, ".");
                            number = " × 10³³";
                            break;

                        case 33:
                            full = full.Insert(full.Length - 32, ".");
                            number = " × 10³²";
                            break;

                        case 32:
                            full = full.Insert(full.Length - 31, ".");
                            number = " × 10³¹";
                            break;

                        case 31:
                            full = full.Insert(full.Length - 30, ".");
                            number = " × 10³⁰";
                            break;

                        case 30:
                            full = full.Insert(full.Length - 29, ".");
                            number = " × 10²⁹";
                            break;

                        case 29:
                            full = full.Insert(full.Length - 28, ".");
                            number = " × 10²⁸";
                            break;

                        case 28:
                            full = full.Insert(full.Length - 27, ".");
                            number = " × 10²⁷";
                            break;

                        case 27:
                            full = full.Insert(full.Length - 26, ".");
                            number = " × 10²⁶";
                            break;

                        case 26:
                            full = full.Insert(full.Length - 25, ".");
                            number = " × 10²⁵";
                            break;

                        case 25:
                            full = full.Insert(full.Length - 24, ".");
                            number = " × 10²⁴";
                            break;

                        case 24:
                            full = full.Insert(full.Length - 23, ".");
                            number = " × 10²³";
                            break;

                        case 23:
                            full = full.Insert(full.Length - 22, ".");
                            number = " × 10²²";
                            break;

                        case 22:
                            full = full.Insert(full.Length - 21, ".");
                            number = " × 10²¹";
                            break;

                        case 21:
                            full = full.Insert(full.Length - 20, ".");
                            number = " × 10²⁰";
                            break;

                        case 20:
                            full = full.Insert(full.Length - 19, ".");
                            number = " × 10¹⁹";
                            break;

                        case 19:
                            full = full.Insert(full.Length - 18, ".");
                            number = " × 10¹⁸";
                            break;

                        case 18:
                            full = full.Insert(full.Length - 17, ".");
                            number = " × 10¹⁷";
                            break;

                        case 17:
                            full = full.Insert(full.Length - 16, ".");
                            number = " × 10¹⁶";
                            break;

                        case 16:
                            full = full.Insert(full.Length - 15, ".");
                            number = " × 10¹⁵";
                            break;

                        case 15:
                            full = full.Insert(full.Length - 14, ".");
                            number = " × 10¹⁴";
                            break;

                        case 14:
                            full = full.Insert(full.Length - 13, ".");
                            number = " × 10¹³";
                            break;

                        case 13:
                            full = full.Insert(full.Length - 12, ".");
                            number = " × 10¹²";
                            break;

                        case 12:
                            full = full.Insert(full.Length - 11, ".");
                            number = " × 10¹¹";
                            break;

                        case 11:
                            full = full.Insert(full.Length - 10, ".");
                            number = " × 10¹⁰";
                            break;

                        case 10:
                            full = full.Insert(full.Length - 9, ".");
                            number = " × 10⁹";
                            break;

                        case 9:
                            full = full.Insert(full.Length - 8, ".");
                            number = " × 10⁸";
                            break;

                        case 8:
                            full = full.Insert(full.Length - 7, ".");
                            number = " × 10⁷";
                            break;

                        case 7:
                            full = full.Insert(full.Length - 6, ".");
                            number = " × 10⁶";
                            break;

                    }
                    break;
                #endregion

                #region E Notation
                case 3:
                    switch (full.Length)
                    {
                        case 57:
                            full = full.Insert(full.Length - 56, ".");
                            number = "e+56";
                            break;

                        case 56:
                            full = full.Insert(full.Length - 45, ".");
                            number = "e+55";
                            break;

                        case 55:
                            full = full.Insert(full.Length - 54, ".");
                            number = "e+54";
                            break;

                        case 54:
                            full = full.Insert(full.Length - 53, ".");
                            number = "e+53";
                            break;

                        case 53:
                            full = full.Insert(full.Length - 52, ".");
                            number = "e+52";
                            break;

                        case 52:
                            full = full.Insert(full.Length - 51, ".");
                            number = "e+51";
                            break;

                        case 51:
                            full = full.Insert(full.Length - 50, ".");
                            number = "e+50";
                            break;

                        case 50:
                            full = full.Insert(full.Length - 49, ".");
                            number = "e+49";
                            break;

                        case 49:
                            full = full.Insert(full.Length - 48, ".");
                            number = "e+48";
                            break;

                        case 48:
                            full = full.Insert(full.Length - 47, ".");
                            number = "e+47";
                            break;
                        case 47:
                            full = full.Insert(full.Length - 46, ".");
                            number = "e+46";
                            break;

                        case 46:
                            full = full.Insert(full.Length - 45, ".");
                            number = "e+45";
                            break;

                        case 45:
                            full = full.Insert(full.Length - 44, ".");
                            number = "e+44";
                            break;

                        case 44:
                            full = full.Insert(full.Length - 43, ".");
                            number = "e+43";
                            break;

                        case 43:
                            full = full.Insert(full.Length - 42, ".");
                            number = "e+42";
                            break;

                        case 42:
                            full = full.Insert(full.Length - 41, ".");
                            number = "e+41";
                            break;

                        case 41:
                            full = full.Insert(full.Length - 40, ".");
                            number = "e+40";
                            break;

                        case 40:
                            full = full.Insert(full.Length - 39, ".");
                            number = "e+39";
                            break;

                        case 39:
                            full = full.Insert(full.Length - 38, ".");
                            number = "e+38";
                            break;

                        case 38:
                            full = full.Insert(full.Length - 37, ".");
                            number = "e+37";
                            break;

                        case 37:
                            full = full.Insert(full.Length - 36, ".");
                            number = "e+36";
                            break;

                        case 36:
                            full = full.Insert(full.Length - 35, ".");
                            number = "e+35";
                            break;

                        case 35:
                            full = full.Insert(full.Length - 34, ".");
                            number = "e+34";
                            break;

                        case 34:
                            full = full.Insert(full.Length - 33, ".");
                            number = "e+33";
                            break;

                        case 33:
                            full = full.Insert(full.Length - 32, ".");
                            number = "e+32";
                            break;

                        case 32:
                            full = full.Insert(full.Length - 31, ".");
                            number = "e+31";
                            break;

                        case 31:
                            full = full.Insert(full.Length - 30, ".");
                            number = "e+30";
                            break;

                        case 30:
                            full = full.Insert(full.Length - 29, ".");
                            number = "e+29";
                            break;

                        case 29:
                            full = full.Insert(full.Length - 28, ".");
                            number = "e+28";
                            break;

                        case 28:
                            full = full.Insert(full.Length - 27, ".");
                            number = "e+27";
                            break;

                        case 27:
                            full = full.Insert(full.Length - 26, ".");
                            number = "e+26";
                            break;

                        case 26:
                            full = full.Insert(full.Length - 25, ".");
                            number = "e+25";
                            break;

                        case 25:
                            full = full.Insert(full.Length - 24, ".");
                            number = "e+24";
                            break;

                        case 24:
                            full = full.Insert(full.Length - 23, ".");
                            number = "e+23";
                            break;

                        case 23:
                            full = full.Insert(full.Length - 22, ".");
                            number = "e+22";
                            break;

                        case 22:
                            full = full.Insert(full.Length - 21, ".");
                            number = "e+21";
                            break;

                        case 21:
                            full = full.Insert(full.Length - 20, ".");
                            number = "e+20";
                            break;

                        case 20:
                            full = full.Insert(full.Length - 19, ".");
                            number = "e+19";
                            break;

                        case 19:
                            full = full.Insert(full.Length - 18, ".");
                            number = "e+18";
                            break;

                        case 18:
                            full = full.Insert(full.Length - 17, ".");
                            number = "e+17";
                            break;

                        case 17:
                            full = full.Insert(full.Length - 16, ".");
                            number = "e+16";
                            break;

                        case 16:
                            full = full.Insert(full.Length - 15, ".");
                            number = "e+15";
                            break;

                        case 15:
                            full = full.Insert(full.Length - 14, ".");
                            number = "e+14";
                            break;

                        case 14:
                            full = full.Insert(full.Length - 13, ".");
                            number = "e+13";
                            break;

                        case 13:
                            full = full.Insert(full.Length - 12, ".");
                            number = "e+12";
                            break;

                        case 12:
                            full = full.Insert(full.Length - 11, ".");
                            number = "e+11";
                            break;

                        case 11:
                            full = full.Insert(full.Length - 10, ".");
                            number = "e+10";
                            break;

                        case 10:
                            full = full.Insert(full.Length - 9, ".");
                            number = "e+9";
                            break;

                        case 9:
                            full = full.Insert(full.Length - 8, ".");
                            number = "e+8";
                            break;

                        case 8:
                            full = full.Insert(full.Length - 7, ".");
                            number = "e+7";
                            break;

                        case 7:
                            full = full.Insert(full.Length - 6, ".");
                            number = "e+6";
                            break;
                    }
                    break;
                #endregion

                #region Long Scale
                case 4:
                    if (full.Length >= 55)
                    {
                        full = full.Insert(full.Length - 54, ".");
                        number = " Nonillion";
                    }
                    else if (full.Length >= 52)
                    {
                        full = full.Insert(full.Length - 51, ".");
                        number = " Octilliard";
                    }
                    else if (full.Length >= 49)
                    {
                        full = full.Insert(full.Length - 48, ".");
                        number = " Octillion";
                    }
                    else if (full.Length >= 46)
                    {
                        full = full.Insert(full.Length - 45, ".");
                        number = " Septilliard";
                    }
                    else if (full.Length >= 43)
                    {
                        full = full.Insert(full.Length - 42, ".");
                        number = " Septillion";
                    }
                    else if (full.Length >= 40)
                    {
                        full = full.Insert(full.Length - 39, ".");
                        number = " Sextilliard";
                    }
                    else if (full.Length >= 37)
                    {
                        full = full.Insert(full.Length - 36, ".");
                        number = " Sextillion";
                    }
                    else if (full.Length >= 34)
                    {
                        full = full.Insert(full.Length - 33, ".");
                        number = " Quintilliard";
                    }
                    else if (full.Length >= 31)
                    {
                        full = full.Insert(full.Length - 30, ".");
                        number = " Quintillion";
                    }
                    else if (full.Length >= 28)
                    {
                        full = full.Insert(full.Length - 27, ".");
                        number = " Quadrilliard";
                    }
                    else if (full.Length >= 25)
                    {
                        full = full.Insert(full.Length - 24, ".");
                        number = " Quadrillion";
                    }
                    else if (full.Length >= 22)
                    {
                        full = full.Insert(full.Length - 21, ".");
                        number = " Trilliard";
                    }
                    else if (full.Length >= 19)
                    {
                        full = full.Insert(full.Length - 18, ".");
                        number = " Trillion";
                    }
                    else if (full.Length >= 16)
                    {
                        full = full.Insert(full.Length - 15, ".");
                        number = " Billiard";
                    }
                    else if (full.Length >= 13)
                    {
                        full = full.Insert(full.Length - 12, ".");
                        number = " Billion";
                    }
                    else if (full.Length >= 10)
                    {
                        full = full.Insert(full.Length - 9, ".");
                        number = " Milliard";
                    }
                    else if (full.Length >= 7)
                    {
                        full = full.Insert(full.Length - 6, ".");
                        number = " Million";
                    }
                    break;
                #endregion

                #region Long Scale 2
                case 5:
                    if (full.Length >= 55)
                    {
                        full = full.Insert(full.Length - 54, ".");
                        number = " Nonillion";
                    }
                    else if (full.Length >= 49)
                    {
                        full = full.Insert(full.Length - 48, ".");
                        number = " Octillion";
                    }
                    else if (full.Length >= 43)
                    {
                        full = full.Insert(full.Length - 42, ".");
                        number = " Septillion";
                    }
                    else if (full.Length >= 37)
                    {
                        full = full.Insert(full.Length - 36, ".");
                        number = " Sextillion";
                    }
                    else if (full.Length >= 31)
                    {
                        full = full.Insert(full.Length - 30, ".");
                        number = " Quintillion";
                    }
                    else if (full.Length >= 25)
                    {
                        full = full.Insert(full.Length - 24, ".");
                        number = " Quadrillion";
                    }
                    else if (full.Length >= 19)
                    {
                        full = full.Insert(full.Length - 18, ".");
                        number = " Trillion";
                    }
                    else if (full.Length >= 13)
                    {
                        full = full.Insert(full.Length - 12, ".");
                        number = " Billion";
                    }
                    else if (full.Length >= 7)
                    {
                        full = full.Insert(full.Length - 6, ".");
                        number = " Million";
                    }
                    break;
                #endregion

                #region Vedic
                case 6:
                    if (full.Length >= 53)
                    {
                        full = full.Insert(full.Length - 52, ".");
                        number = " Samudra";
                    }
                    else if (full.Length >= 48)
                    {
                        full = full.Insert(full.Length - 47, ".");
                        number = " Mahā-kharva";
                    }
                    else if (full.Length >= 43)
                    {
                        full = full.Insert(full.Length - 42, ".");
                        number = " Kharva";
                    }
                    else if (full.Length >= 38)
                    {
                        full = full.Insert(full.Length - 37, ".");
                        number = " Mahā-padma";
                    }
                    else if (full.Length >= 33)
                    {
                        full = full.Insert(full.Length - 32, ".");
                        number = " Padma";
                    }
                    else if (full.Length >= 28)
                    {
                        full = full.Insert(full.Length - 27, ".");
                        number = " Mahā-vṛnda";
                    }
                    else if (full.Length >= 23)
                    {
                        full = full.Insert(full.Length - 22, ".");
                        number = " Vṛnda";
                    }
                    else if (full.Length >= 18)
                    {
                        full = full.Insert(full.Length - 17, ".");
                        number = " Mahā-śaṅku";
                    }
                    else if (full.Length >= 13)
                    {
                        full = full.Insert(full.Length - 12, ".");
                        number = " Śaṅku";
                    }
                    else if (full.Length >= 8)
                    {
                        full = full.Insert(full.Length - 7, ".");
                        number = " Koṭi";
                    }
                    else if (full.Length >= 6)
                    {
                        full = full.Insert(full.Length - 5, ".");
                        number = " Lakṣa";
                    }
                    break;
                #endregion

                #region Indian
                case 7:

                    if (full.Length >= 26)
                    {
                        full = full.Insert(full.Length - 25, ".");
                        number = " Paraardh";
                    }
                    else if (full.Length >= 24)
                    {
                        full = full.Insert(full.Length - 23, ".");
                        number = " Madhyam";
                    }
                    else if (full.Length >= 22)
                    {
                        full = full.Insert(full.Length - 21, ".");
                        number = " Antya";
                    }
                    else if (full.Length >= 20)
                    {
                        full = full.Insert(full.Length - 19, ".");
                        number = " Samudra";
                    }
                    else if (full.Length >= 18)
                    {
                        full = full.Insert(full.Length - 17, ".");
                        number = " Shankh";
                    }
                    else if (full.Length >= 17)
                    {
                        full = full.Insert(full.Length - 16, ".");
                        number = " Das Padm";
                    }
                    else if (full.Length >= 16)
                    {
                        full = full.Insert(full.Length - 15, ".");
                        number = " Padm";
                    }
                    else if (full.Length >= 15)
                    {
                        full = full.Insert(full.Length - 14, ".");
                        number = " Ek Karoṛ Karoṛ";
                    }
                    else if (full.Length >= 14)
                    {
                        full = full.Insert(full.Length - 13, ".");
                        number = " Neel";
                    }
                    else if (full.Length >= 13)
                    {
                        full = full.Insert(full.Length - 12, ".");
                        number = " Ek Lākh Karoṛ";
                    }
                    else if (full.Length >= 12)
                    {
                        full = full.Insert(full.Length - 11, ".");
                        number = " Kharab";
                    }
                    else if (full.Length >= 11)
                    {
                        full = full.Insert(full.Length - 10, ".");
                        number = " Ek Hazār Karoṛ";
                    }
                    else if (full.Length >= 10)
                    {
                        full = full.Insert(full.Length - 9, ".");
                        number = " Arab";
                    }
                    else if (full.Length >= 9)
                    {
                        full = full.Insert(full.Length - 8, ".");
                        number = " Das Karoṛ";
                    }
                    else if (full.Length >= 8)
                    {
                        full = full.Insert(full.Length - 7, ".");
                        number = " Karoṛ";
                    }
                    else if (full.Length >= 7)
                    {
                        full = full.Insert(full.Length - 6, ".");
                        number = " Adant";
                    }
                    else if (full.Length >= 6)
                    {
                        full = full.Insert(full.Length - 5, ".");
                        number = " Lākh";
                    }

                    break;
                #endregion
            }

            n.full = full;
            n.number = number;

            return n;
        }


        /// <summary>
        /// <para>Returns a number as a readable string.</para>
        /// </summary>
        /// 
        /// <param name="N"> 
        /// <para>Original number.</para>
        ///  </param>
        ///  
        /// <param name="Shorten"> 
        /// <para>Shorten type.</para>
        /// <para>0 = Disabled </para>
        /// <para>1 = Numbers (Million)</para>
        /// <para>2 = Scientific (1 x 10⁷)</para>
        /// <para>3 = E Notation (1E+7)</para>
        /// <para>4 = Long Scale (Millards/Billiards)</para>
        /// <para>5 = Long Scale 2</para>
        /// <para>6 = Vedic</para>
        /// <para>7 = Indian</para>
        ///  </param>
        ///  
        /// <param name="Decimals"> 
        /// <para>The number of decimal places to show.</para>
        ///  </param>
        ///  
        /// <param name="Seperator"> 
        /// <para>Where to place commas.</para>
        /// <para>0 = Disabled</para>
        /// <para>1 = 1,000,000 Standard</para>
        /// <para>2 = 10,00,000 Indian</para>
        ///  </param>
        ///  
        /// <param name="Type"> 
        /// <para>Type of measurement.</para>
        /// <para>0 = Number</para>
        /// <para>1 = Mass</para>
        /// <para>2 = Distance</para>
        /// <para>3 = Time</para>
        /// <para>4 = Temperature</para>
        /// <para>5 = Ampere</para>
        /// <para>6 = Mole</para>
        /// <para>7 = Candela</para>
        /// </param>
        /// 
        /// <param name="Group"> 
        /// <para>Unit of measurement.</para>
        /// <para>Start with "G:" (eg. "G:Metric") for a group).</para>
        /// </param>
        static public string Number(Number nu, byte Shorten, int Decimals, byte Seperator, byte Type, string Group)
        {
            string name = "";
            string symbol = "";
            string nameSingle = "";
            string name2;
            double rN;
            int i = 0;
            bool group = false;
            Number N2 = NCalc.GetNumber(nu);
            Number N = NCalc.GetNumber(nu);

            if (Group != null)
            {
                if (Group.Contains("G:"))
                {
                    group = true;
                    Group = Group.Remove(0, 2);
                }

                #region Group

                if (group && Measurements.Groups[Type].ContainsKey(Group))
                {
                    if (Type != 0)
                        while (true)
                        {
                            rN = NCalc.GetRoughNumber(N);
                            if (Measurements.Groups[Type][Group].Count > i)
                                name2 = Measurements.Groups[Type][Group][i];
                            else break;
                            if (rN >= Measurements.ConversionData[Type][name2].GValue)
                            {
                                name = name2;
                                nameSingle = Measurements.ConversionData[Type][name2].NameSingle;
                                symbol = Measurements.ConversionData[Type][name2].Symbol;

                                N.Multiply(Measurements.ConversionData[Type][name2].Multiplier);
                                break;
                            }
                            i++;
                        }
                }

                #endregion 

                #region Single

                else if (Measurements.ConversionData[Type].ContainsKey(Group))
                {
                    name = Group;
                    nameSingle = Measurements.ConversionData[Type][Group].NameSingle;
                    symbol = Measurements.ConversionData[Type][Group].Symbol;

                    switch (Type)
                    {
                        case 1:
                        case 2:
                        case 3:
                            N.Multiply(Measurements.ConversionData[Type][Group].Multiplier);
                            break;

                        case 4:
                            if (name != "Kelvin")
                                N = NCalc.GetTempConversion(N, name);
                                
                            break;
                    }
                }

                #endregion

                else if (Group.Contains("G:"))
                    return "THIS GROUP DOES NOT EXIST.";
                else
                    return "THIS UNIT DOES NOT EXIST.";
            }

            #region String Construction

            string full;
            string d = "" + N.Dec;
            string n = "" + N.Num;
            string s = "" + N.Sci;
            string number = "";
            bool Neg = false;

            if (Decimals > 0)
                Decimals++;

            if (n.Contains('-') || d.Contains('-') || s.Contains('-'))
            {
                if (d.Contains('-'))
                    d = d.Remove(d.IndexOf('-'), 1);
                if (n.Contains('-'))
                    n = n.Remove(n.IndexOf('-'), 1);
                if (s.Contains('-'))
                    s = s.Remove(s.IndexOf('-'), 1);
                Neg = true;
            }

            d = d.Remove(0, 1);

            if (n.Contains('.'))
                n = n.Remove(n.IndexOf('.'));
            #endregion

            if (N.Sci == 0)
                full = n;
            else
            {
                int c = n.Count();
                if (c < 28)
                {
                    int a = 28 - c;
                    string add = "";

                    for (i = 0; i < a; i++)
                        add = add + "0";

                    n = add + n;
                }

                full = s + n;

                if (Shorten == 0)
                    full = full + d;
            }

            if (Shorten != 0)
            {
                NumberType ShortN = Short(Shorten, full);
                full = ShortN.full;
                number = ShortN.number;
            }

            full = CleanUp(full, Decimals, d, Seperator);
            full = full + number + " " + name;
            if (Neg)
                full = "-" + full;

            if (full.ElementAt(full.Length - 1) == ' ')
                full = full.Remove(full.Length - 1);

            return full;
        }
        
        ///// <summary>
        ///// Writes X position.
        ///// </summary>
        ///// <param name="S">Space position.</param>
        /////
        ///// <param name="Shorten"> 
        ///// <para>Shorten type.</para>
        ///// <para>0 = Disabled </para>
        ///// <para>1 = Numbers (Million)</para>
        ///// <para>2 = Scientific (1 x 10⁷)</para>
        ///// <para>3 = E Notation (1E+7)</para>
        ///// <para>4 = Long Scale (Millards/Billiards)</para>
        ///// <para>5 = Long Scale 2</para>
        ///// <para>6 = Vedic</para>
        ///// <para>7 = Indian</para>
        /////  </param>
        /////  
        ///// <param name="Seperator"> 
        ///// <para>Where to place commas.</para>
        ///// <para>0 = Disabled</para>
        ///// <para>1 = 1,000,000 Standard</para>
        ///// <para>2 = 10,00,000 Indian</para>
        /////  </param>
        ///// <returns></returns>
        //static public string PositionX(SpacePosition S, byte Shorten, byte Seperator)
        //{
        //    string number = "";
        //    Number x = new Number(0, (decimal)S.L.X + ((decimal)S.M.X * 10000000m) + ((decimal)S.H.X * 100000000000000m), 0);
        //    number = "X: " + Number(x, Shorten, 0, Seperator, 0, null);

        //    return number;
        //}

        ///// <summary>
        ///// Writes Y position.
        ///// </summary>
        ///// <param name="S">Space position.</param>
        /////
        ///// <param name="Shorten"> 
        ///// <para>Shorten type.</para>
        ///// <para>0 = Disabled </para>
        ///// <para>1 = Numbers (Million)</para>
        ///// <para>2 = Scientific (1 x 10⁷)</para>
        ///// <para>3 = E Notation (1E+7)</para>
        ///// <para>4 = Long Scale (Millards/Billiards)</para>
        ///// <para>5 = Long Scale 2</para>
        ///// <para>6 = Vedic</para>
        ///// <para>7 = Indian</para>
        /////  </param>
        /////  
        ///// <param name="Seperator"> 
        ///// <para>Where to place commas.</para>
        ///// <para>0 = Disabled</para>
        ///// <para>1 = 1,000,000 Standard</para>
        ///// <para>2 = 10,00,000 Indian</para>
        /////  </param>
        ///// <returns></returns>
        //static public string PositionY(SpacePosition S, byte Shorten, byte Seperator)
        //{
        //    string number = "";
        //    Number y = new Number(0, (decimal)S.L.Y + ((decimal)S.M.Y * 10000000m) + ((decimal)S.H.Y * 100000000000000m), 0);
        //    number = "Y: " + Number(y, Shorten, 0, Seperator, 0, null);

        //    return number;
        //}
    }
}
