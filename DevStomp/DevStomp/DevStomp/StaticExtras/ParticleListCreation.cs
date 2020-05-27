using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DevStomp
{
    static class ParticleListCreation
    {
        static public List<List<ValueType>> GenerateRandomList(int count)
        {
            List<List<ValueType>> A = new List<List<ValueType>>();
            Random rand = GlobalVariables.RandomNumber;
            int num;

            for (int i = 0; i < count; i++)
            {
                A.Add(new List<ValueType>());
                num = rand.Next(0, 20);
                A[i].Add(num);

                switch (num)
                {
                    case 0:
                        A[i].Add(0.1f + (float)rand.NextDouble());
                        break;
                    case 1:
                        A[i].Add((float)rand.NextDouble() * (float)MathHelper.TwoPi);
                        A[i].Add(0.1f + (float)rand.NextDouble());
                        break;
                    case 2:
                        A[i].Add(0.1f + (float)rand.NextDouble());
                        A[i].Add(rand.Next(0, 32000));
                        break;
                    case 3:
                        A[i].Add((float)rand.NextDouble() * (float)MathHelper.TwoPi);
                        A[i].Add(rand.Next(0, 32000));
                        break;
                    case 4:
                        A[i].Add(rand.Next(0, 32000));
                        break;
                    case 5:
                        A[i].Add(100f + (float)(rand.NextDouble() * 400f));

                        if (rand.Next(0, 2) == 0)
                            A[i].Add((float)(rand.NextDouble() * 300f));
                        else
                            A[i].Add(0f);

                        A[i].Add(10f + (float)(rand.NextDouble() * 190f));
                        break;
                    case 6:
                        A[i].Add(5f + (float)(rand.NextDouble() * 15f));
                        if (rand.Next(0, 2) == 0)
                            A[i].Add((float)(rand.NextDouble() * 10f));
                        else
                            A[i].Add(0f);
                        A[i].Add(5f + (float)(rand.NextDouble() * 45f));
                        break;
                    case 7:
                        A[i].Add(rand.Next(0, 32000));
                        A[i].Add(rand.Next(64, 256));
                        A[i].Add(32);
                        break;
                    case 8:
                        A[i].Add((byte)rand.Next(0, 3));
                        A[i].Add(((float)rand.NextDouble() * 0.75f) + 0.25f);
                        A[i].Add(((float)rand.NextDouble() * 0.5f) + 0.5f);
                        break;
                    case 9:
                        if (rand.Next(0, 2) == 0)
                            A[i].Add(((float)rand.NextDouble() * 24f) + 1f);
                        else
                            A[i].Add(-(((float)rand.NextDouble() * 24f) + 1f));

                        A[i].Add((byte)rand.Next(0, 3));
                        A[i].Add(((float)rand.NextDouble() * 0.75f) + 0.25f);
                        A[i].Add(((float)rand.NextDouble() * 0.5f) + 0.5f);
                        break;
                    case 10:
                        if (rand.Next(0, 2) == 0)
                            A[i].Add((float)MathHelper.TwoPi * ((float)rand.NextDouble() * 4f));
                        else
                            A[i].Add(-((float)MathHelper.TwoPi * ((float)rand.NextDouble() * 4f)));
                        if (rand.Next(0, 2) == 0)
                        {
                            if (rand.Next(0, 2) == 0)
                                A[i].Add((float)MathHelper.TwoPi * (((float)rand.NextDouble() * 0.5f) + 0.5f));
                            else
                                A[i].Add(-((float)MathHelper.TwoPi * (((float)rand.NextDouble() * 0.5f) + 0.5f)));
                        }
                        else
                            A[i].Add(0f);
                        break;
                    case 11:
                        A[i].Add((byte)rand.Next(0, 5));
                        break;
                    case 12:
                        A[i].Add((int)rand.Next(3, 40));
                        break;
                    case 13:
                        A[i].Add((int)rand.Next(0, 30));
                        break;
                    case 14:
                        A[i].Add((int)rand.Next(0, 30));
                        break;
                    case 15:
                        A[i].Add((int)rand.Next(256, 4096));
                        break;
                    case 16:
                        A[i].Add((int)rand.Next(256, 4096));
                        break;
                    case 17:
                        A[i].Add((int)rand.Next(0, 32000));
                        A[i].Add((int)rand.Next(256, 4096));
                        break;
                    case 18:
                        A[i].Add((int)rand.Next(256, 4096));
                        A[i].Add((int)rand.Next(0, 32000));
                        break;
                    case 19:
                        A[i].Add(((float)rand.NextDouble() * 50f) + 50f); //amplitude
                        A[i].Add(((float)rand.NextDouble() * 3f) * 2f);
                        A[i].Add((float)rand.NextDouble() + 0.000001f);
                        A[i].Add(true);
                        break;



                }
            }

            return A;
        }


        static public List<List<ValueType>> GenerateTestList()
        {
            List<List<ValueType>> A = new List<List<ValueType>>();
            Random rand = GlobalVariables.RandomNumber;
            A.Add(new List<ValueType>());
            int i = 0;

            A[i].Add(19);
            A[i].Add(((float)rand.NextDouble() * 50f) + 50f); //amplitude
            A[i].Add(((float)rand.NextDouble() * 3f) * 2f);
            A[i].Add((float)rand.NextDouble() + 0.000001f);
            A[i].Add(true);


            //A.Add(new List<ValueType>());
            //i++;            
            //A[i].Add(4);
            //A[i].Add(rand.Next(0, 32000));

            return A;
        }
    }
}
