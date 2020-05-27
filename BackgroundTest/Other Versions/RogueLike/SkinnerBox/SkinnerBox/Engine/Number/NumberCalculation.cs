using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    /// <summary>
    /// Calculations for Number class.
    /// </summary>
    static class NCalc
    {
        static Number n = new Number(0, 0, 0);
        static decimal a;
        static decimal b;
        static decimal c;
        static decimal d;

        ///// <summary>
        ///// Calculates the distance between two objects.
        ///// </summary>
        ///// <param name="CD1">First chunk position.</param>
        ///// <param name="CD2">Second chunk position.</param>
        ///// <param name="D1">First space position.</param>
        ///// <param name="D2">Second space position.</param>
        ///// <returns></returns>
        //static public Number CalculateDistance(Vector2 CD1, Vector2 CD2, SpacePosition D1, SpacePosition D2)
        //{
        //    a = (decimal)Vector2.Distance(CD1, CD2) * 1000000000000000000000m;
        //    b = (decimal)Vector2.Distance(D1.L, D2.L);
        //    c = (decimal)Vector2.Distance(D1.M, D2.M) * 10000000m;
        //    d = (decimal)Vector2.Distance(D1.H, D2.H) * 100000000000000m;

        //    n.ResetNumber();
        //    n.Num = a + b + c + d;

        //    return n;
        //}

        ///// <summary>
        ///// Calculates the distance between two objects as a float.
        ///// </summary>
        ///// <param name="CD1">First chunk position.</param>
        ///// <param name="CD2">Second chunk position.</param>
        ///// <param name="D1">First space position.</param>
        ///// <param name="D2">Second space position.</param>
        ///// <returns></returns>
        //static public float CalculateDistanceF(Vector2 CD1, Vector2 CD2, SpacePosition D1, SpacePosition D2)
        //{
        //    a = (decimal)Vector2.Distance(CD1, CD2) * 1000000000000000000000m;
        //    b = (decimal)Vector2.Distance(D1.L, D2.L);
        //    c = (decimal)Vector2.Distance(D1.M, D2.M) * 10000000m;
        //    d = (decimal)Vector2.Distance(D1.H, D2.H) * 100000000000000m;

        //    return (float)(a + b + c + d);
        //}


        /// <summary>
        /// Returns the radius of the gravitational influence of an object.
        /// </summary>
        /// <param name="Mass">Object mass.</param>
        /// <returns></returns>
        static public Number GravityInfluence(Number Mass)
        {
            n.ResetNumber();
            n.Add(Math.Sqrt((double)((Mass.Num * 6.67384e-11m) + (Mass.Sci * 6.67384e-11m * 10000000000000000000000000000m))) * 1000);
            
            if (n.Dec >= 0.5m)
            {
                n.Num++;

                if (n.Num == 10000000000000000000000000000m)
                {
                    n.Num -= 10000000000000000000000000000m;
                    n.Sci++;
                }
            }

            n.Dec = 0;

            return n;
        }

        static public Number GetNumber(Number N)
        {
            n.ResetNumber();

            n.Dec = N.Dec;
            n.Num = N.Num;
            n.Sci = N.Sci;

            return n;
        }

        /// <summary>
        /// Returns the radius of the gravitational influence of an object as a float.
        /// </summary>
        /// <param name="Mass">Object mass.</param>
        /// <returns></returns>
        static public float GravityInfluenceF(Number Mass)
        {
            return (float)(Math.Sqrt((double)((Mass.Num * 6.67384e-11m) + (Mass.Sci * 6.67384e-11m * 10000000000000000000000000000m))) * 1000);
        }
        
        /// <summary>
        /// Returns a double roughly equal to the original number.
        /// </summary>
        /// <param name="N">Original number.</param>
        /// <returns></returns>
        static public double GetRoughNumber(Number N)
        {
            return ((double)N.Dec + (double)N.Num + ((double)N.Sci * 1e+28));
        }
        
        /// <summary>
        /// Gets temperature conversion.
        /// </summary>
        /// <param name="N">Original temperature (Kelvin).</param>
        /// <param name="Type">Desired temperature name.</param>
        /// <returns></returns>
        static public Number GetTempConversion(Number N, string Type)
        {           
            N.Num = N.Num - 273.15m;

            switch (Type)
            {
                case "Celcius":
                    break;

                case "Fahrenheit":
                    N.Multiply(1.8d);
                    N.Num += 32;
                    break;
            }

            N.FixNumber();
            return N;
        }
    }
}
