using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    class Number
    {
        private struct NumberType
        {
            public string full;
            public string number;
        }

        public decimal Dec;
        public decimal Num;
        public decimal Sci;

        /// <summary>
        /// <para>New number.</para>
        /// <para>57 digits max.</para>
        /// </summary>
        /// <param name="d">Decimal number.</param>
        /// <param name="n">Whole number.</param>
        /// <param name="s">Whole number x (10^28 or 10000000000000000000000000000)</param>
        public Number(decimal d, decimal n, decimal s)
        {
            Dec = d;

            while (Dec >= 1)
            {
                Dec -= 1;
                Num += 1;
            }

            Num = n;

            while (Num >= 10000000000000000000000000000m)
            {
                Num -= 10000000000000000000000000000m;
                Sci += 1;
            }

            Sci = s;
        }

        /// <summary>
        /// <para>New number from double.</para>
        /// <para>NEEDS WORK. (Needs to actually be made.)</para>
        /// </summary>
        /// <param name="n">Number.</param>
        public Number(double n)
        {
        }

        /// <summary>
        /// Fixes a calculated Number.
        /// </summary>
        public void FixNumber()
        {
            decimal t;
            decimal a;

            t = Math.Truncate(Num);
            Dec += Num - t;
            Num = t;

            t = Math.Truncate(Sci);
            Num += (Sci - t) * 10000000000000000000000000000m;
            Sci = t;

            if (Dec >= 1)
            {
                a = (decimal)Math.Truncate(Dec);

                Dec -= a;
                Num += a;
            }

            if (Num >= 10000000000000000000000000000m)
            {
                a = (decimal)Math.Truncate(Num / 10000000000000000000000000000m);

                Num -= 10000000000000000000000000000m * a;
                Sci += 1 * a;
            }
        }

        /// <summary>
        /// Reset number to 0.
        /// </summary>
        public void ResetNumber()
        {
            Dec = 0;
            Num = 0;
            Sci = 0;
        }



        /// <summary>
        /// Returns a number that is a multiple of the original.
        /// </summary>
        /// <param name="multiply">Number multiplier.</param>
        /// <returns></returns>
        public void Multiply(double multiply)
        {
            if (multiply != 1)
            {
                Dec = (decimal)((double)Dec * multiply);
                Num = (decimal)((double)Num * multiply);
                Sci = Sci * (decimal)multiply;

                FixNumber();
            }
        }

        /// <summary>
        /// <para>Subtract number by a given amount.</para>
        /// <para>NEEDS WORK! (Not accurate. Errors with negative numbers.)</para>
        /// </summary>
        /// <param name="N">Number to subtract.</param>
        /// <returns></returns>
        public void Subtract(Number subtract)
        {
            //n.ResetNumber();

            //n.Dec = N.Dec - N2.Dec;
            //n.Num = N.Num - N2.Num;
            //n.Sci = N.Sci - N2.Sci;

            //return n;
        }

        /// <summary>
        /// <para>Add a number to this.</para>
        /// <para>NEEDS WORK. (Should work now, needs checking)</para>
        /// </summary>
        /// <param name="add">Number to add.</param>
        public void Add(Number add)
        {
            Dec += add.Dec;
            Num += add.Num;
            Sci += add.Sci;

            FixNumber();
        }

        /// <summary>
        /// <para>Add a double to this.</para>
        /// <para>NEEDS WORK. (Should work now, needs checking)</para>
        /// </summary>
        /// <param name="add">Number to add.</param>
        public void Add(double add)
        {
            double dec = add - Math.Truncate(add);
            add = Math.Truncate(add);

            if (dec > 0)
                Dec += (decimal)dec;

            if (add < 1e+28)
                Num += (decimal)add;
            else
            {
                add = (add / 1e+28);
                dec = (add - Math.Truncate(add)) * 1e+28;
                add = Math.Truncate(add);

                Sci += (decimal)add;
                Num += (decimal)dec;
            }
            FixNumber();
        }

    }
}
