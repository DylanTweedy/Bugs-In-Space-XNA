using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace DevStomp
{
    class Asteroids
    {
        public float[][] grid;
        float total;
        float rotation;
        float RotationalVelocity;
        public SpacePosition P;
        public float Scale;

        public void Initialize(float rotationalVelocity, SpacePosition p, float scale)
        {
            int size = 50;

            grid = new float[size][];

            for (int i = 0; i < size; i++)            
                grid[i] = new float[size];

            P = p;
            rotation = 0f;
            RotationalVelocity = rotationalVelocity;
            Scale = scale;


            for (int x = 0; x < grid.Length; x++)
                for (int y = 0; y < grid.Length; y++)
                {
                    grid[x][y] = 0f;
                }
        }

        public void Update()
        {
            //Initialize(true);

            float[][] grid2 = new float[grid.Length][];

            
            for (int x = 0; x < grid.Length; x++)            
                grid2[x] = new float[grid.Length];

            for (int x = 0; x < grid.Length; x++)            
                for (int y = 0; y < grid.Length; y++)
                {
                    grid2[x][y] = grid[x][y];
                }


            if (grid != grid2)
            {
            }

            //int xx = WorldVariables.RandomNumber.Next(0, grid.Length);
            //int yy = WorldVariables.RandomNumber.Next(0, grid.Length);
            //float numm = grid[xx][yy];
            //grid[xx][yy] -= numm;

            //float ang = UsefulMethods.VectorToAngle(new Vector2(xx, yy));
            //ang += (float)Math.PI / 2f;
            //Vector2 angvec = UsefulMethods.AngleToVector(ang);

            //xx += (int)Math.Round(angvec.X);
            //yy += (int)Math.Round(angvec.Y);
            //grid[xx][yy] += numm;

            for (int x = 0; x < grid.Length; x++)
                for (int y = 0; y < grid.Length; y++)
                {

                    //float angle = UsefulMethods.VectorToAngle(new Vector2(x - (grid.Length / 2f), y - (grid.Length / 2f)));
                    //angle += (float)Math.PI / 2f;

                    //Vector2 vec = UsefulMethods.AngleToVector(angle);
                    //int x2 = (int)Math.Round(vec.X);
                    //int y2 = (int)Math.Round(vec.Y);

                    //angle = UsefulMethods.VectorToAngle(new Vector2(x2, y2));
                    //angle += (float)Math.PI / 4f;
                    //vec = UsefulMethods.AngleToVector(angle);
                    //int x3 = (int)Math.Round(vec.X);
                    //int y3 = (int)Math.Round(vec.Y);

                    //angle = UsefulMethods.VectorToAngle(new Vector2(x2, y2));
                    //angle -= (float)Math.PI / 4f;
                    //vec = UsefulMethods.AngleToVector(angle);
                    //int x4 = (int)Math.Round(vec.X);
                    //int y4 = (int)Math.Round(vec.Y);

                    //angle = UsefulMethods.VectorToAngle(new Vector2(x2, y2));
                    //angle += (float)Math.PI / 2f;
                    //vec = UsefulMethods.AngleToVector(angle);
                    //int x5 = (int)Math.Round(vec.X);
                    //int y5 = (int)Math.Round(vec.Y);

                    //angle = UsefulMethods.VectorToAngle(new Vector2(x2, y2));
                    //angle -= (float)Math.PI / 2f;
                    //vec = UsefulMethods.AngleToVector(angle);
                    //int x6 = (int)Math.Round(vec.X);
                    //int y6 = (int)Math.Round(vec.Y);


                    ////if (x2 >= 0 && x2 < grid.Length && y2 >= 0 && y2 < grid.Length && (x2 != x || y2 != y))
                    ////    if (grid[x][y] > grid[x2][y2])
                    ////    {
                    ////        float dest = grid[x2][y2] + grid[x][y] / 2f;
                    ////        float move = (grid[x][y] - dest);
                    ////        //*(float)WorldVariables.FrameTime;
                    ////        //* 0.1f;

                    ////        grid[x][y] -= move;
                    ////        grid[x2][y2] += move;
                    ////    }

                    //if (x2 >= 0 && x2 < grid.Length && y2 >= 0 && y2 < grid.Length && (x2 != x || y2 != y))
                    //    if (grid[x][y] > grid[x2][y2])
                    //    {
                    //        float dest = grid[x2][y2] + grid[x][y] / 2f;
                    //        float move = (grid[x][y] - dest);
                    //        //*(float)WorldVariables.FrameTime;
                    //        //* 0.1f;

                    //        grid2[x][y] -= move;
                    //        grid2[x2][y2] += move;
                    //    }

                    Vector2 half = new Vector2(grid.Length / 2f, grid.Length / 2f);

                    float inner = 0.01f;


                    for (int x2 = -1; x2 < 2; x2++)
                        if (x + x2 >= 0 && x + x2 < grid.Length)
                            for (int y2 = -1; y2 < 2; y2++)
                                if (x != 0 && y != 0)
                                if (y + y2 >= 0 && y + y2 < grid.Length)
                                {
                                    Vector2 pos = new Vector2(x + x2, y + y2);
                                    float distance = Vector2.Distance(half, pos);                                    
                                    float rDistance = (float)Math.Abs(Vector2.Distance(half, pos + new Vector2(x2, y2)));


                                    float rDistance1 = (float)Math.Round(Math.Abs(Math.Abs(Vector2.Distance(half, pos + new Vector2(x2, y2))) - ((grid.Length / 2f) * (((1f - inner) / 2f) + inner))));
                                    float rDistance2 = (float)Math.Round(Math.Abs(Math.Abs(Vector2.Distance(half, pos)) - ((grid.Length / 2f) * (((1f - inner) / 2f) + inner))));
                                    
                                    if (distance < half.X && distance > half.X * inner)
                                    {
                                        if (grid[x][y] > grid[x + x2][y + y2])
                                        {
                                            float divider = 9f;
                                            int num = 0;

                                            if (x == 0 || x == grid.Length - 1)
                                                num++;
                                            if (y == 0 || y == grid.Length - 1)
                                                num++;

                                            switch (num)
                                            {
                                                case 1:
                                                    divider -= 3f;
                                                    break;

                                                case 2:
                                                    divider -= 5f;
                                                    break;
                                            }

                                            float dest = (grid[x][y]) / divider;

                                            //if (rDistance < distance)
                                            //    dest /= rDistance / 10f;
                                            if (rDistance1 > rDistance2)
                                                dest *= 0.5f;
                                                //dest /= 2f;

                                            float move = dest - (grid[x + x2][y + y2] / divider);



                                            //move *= (float)WorldVariables.FrameTime;
                                            //* 0.1f;

                                            grid2[x][y] -= move;
                                            grid2[x + x2][y + y2] += move;
                                        }
                                    }
                                }


                    if (grid[x][y] != 0f)
                    {
                        Vector2 pos2 = new Vector2(x, y);
                        float distance2 = Vector2.Distance(half, pos2);
                        float angle = 0f;
                        Vector2 vec = Vector2.Zero;
                        int x2 = 0;
                        int y2 = 0;


                        if (distance2 > half.X)
                        {
                            angle = UsefulMethods.VectorToAngle(pos2 - half);
                            vec = -UsefulMethods.AngleToVector(angle);
                            x2 = (int)Math.Round(vec.X);
                            y2 = (int)Math.Round(vec.Y);

                            grid2[x][y] -= grid[x][y];
                            grid2[x + x2][y + y2] += grid[x][y];
                        }
                        if (distance2 < half.X * inner)
                        {
                            angle = UsefulMethods.VectorToAngle(pos2 - half);
                            vec = UsefulMethods.AngleToVector(angle);
                            x2 = (int)Math.Round(vec.X);
                            y2 = (int)Math.Round(vec.Y);

                            grid2[x][y] -= grid[x][y];
                            grid2[x + x2][y + y2] += grid[x][y];
                        }
                    }
                }



            grid = grid2;
            
            total = 0f;
            
            for (int x = 0; x < grid.Length; x++)
                for (int y = 0; y < grid.Length; y++)
                {
                    if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                    {
                        grid[x][y] = 0f;

                        if (GlobalVariables.RandomNumber.Next(0, 10) == 0)
                            grid[x][y] = GlobalVariables.RandomNumber.Next(1, 10);
                    }
                    
                    total += grid[x][y];
                }


            //Console..WriteLine("Total: " + total);


            Vector2 m = InputManager.GetMousePosition(0);

            m /= 32f;

            if (m.X >= 0 && m.X < grid.Length && m.Y >= 0 && m.Y < grid.Length)
            {
                Vector2 vel = Vector2.Zero;
                Vector2 pM = new Vector2(InputManager.M.X, InputManager.M.Y);
                Vector2 M = new Vector2(InputManager.pM.X, InputManager.pM.Y);



                float dis = 0f;

                if (M - pM != Vector2.Zero)
                {
                    vel = UsefulMethods.AngleToVector(UsefulMethods.VectorToAngle(M - pM));
                    dis = Vector2.Distance(M, pM)
                         / 32f
                         ;
                }

                if (vel != Vector2.Zero)
                {
                    bool calc = true;
                    int i = 1;

                    if (dis > 1)
                    { }

                    int x2;
                    int y2;
                    int px2 = (int)m.X;
                    int py2 = (int)m.Y;

                    while (calc)
                    {
                        x2 = (int)Math.Round((vel.X * i));
                        y2 = (int)Math.Round((vel.Y * i));

                            if ((int)m.X + x2 >= 0 && (int)m.X + x2 < grid.Length && (int)m.Y + y2 >= 0 && (int)m.Y + y2 < grid.Length)
                            {
                                float move = grid[(int)m.X + x2][(int)m.Y + y2];

                                grid[(int)m.X + x2][(int)m.Y + y2] -= move;
                                grid[(int)m.X][(int)m.Y] += move;
                            }

                        i++;

                        if (i > Math.Ceiling(dis))
                            calc = false;

                        px2 = (int)m.X + x2;
                        py2 = (int)m.Y + y2;
                    }
                }

                //Vector2 vel = Vector2.Zero;
                //Vector2 M = new Vector2(InputManager.M.X, InputManager.M.Y);
                //Vector2 pM = new Vector2(InputManager.pM.X, InputManager.pM.Y);


                //if (M - pM != Vector2.Zero)
                //vel = UsefulMethods.AngleToVector(UsefulMethods.VectorToAngle(M - pM));
                
                //if (vel != Vector2.Zero)
                //{
                //    int x2 = (int)Math.Round(vel.X);
                //    int y2 = (int)Math.Round(vel.Y);

                //    if ((int)m.X + x2 >= 0 && (int)m.X + x2 < grid.Length && (int)m.Y + y2 >= 0 && (int)m.Y + y2 < grid.Length)
                //    {
                //        float move = grid[(int)m.X][(int)m.Y];

                //        grid[(int)m.X + x2][(int)m.Y + y2] += move;
                //        grid[(int)m.X][(int)m.Y] -= move;
                //    }
                //}
            }

            P.Update();
            rotation += RotationalVelocity * (float)GlobalVariables.FrameTime;

            if (rotation > Math.PI)
                rotation -= (float)Math.PI * 2;
            else if (rotation < -Math.PI)
                rotation += (float)Math.PI * 2;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Scale = 8000f;



            Vector2 size = new Vector2(StaticTests.Marker.Width, StaticTests.Marker.Width);
            float tint = total / ((grid.Length * grid.Length) / 2f);
            Vector2 pos = P.RoughPosition;
            Vector2 offset = CameraManager.Cams[0].GetOffset();
            
            for (int x = 0; x < grid.Length; x++)
                for (int y = 0; y < grid.Length; y++)
                {
                    float xDis = x - (grid.Length / 2f);
                    float yDis = y - (grid.Length / 2f);

                    //spriteBatch.Draw(StaticTests.Marker, size * new Vector2(x, y), null, Color.White * UsefulMethods.FindBetween(grid[x][y], tint, 0f, 1f, 0f, false), 0f, (size / scale) / 2f, scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(StaticTests.Marker, 
                                                
                        //size * scale * new Vector2(xDis, yDis)
                        pos - offset
                                                
                        , null, Color.White * UsefulMethods.FindBetween(grid[x][y], tint, 0f, 1f, 0f, false), rotation,

                        (size / 2f)
                        -
                        (new Vector2(xDis, yDis) * size)
                        
                        //* UsefulMethods.AngleToVector(rotation)
                        , Scale, SpriteEffects.None, 0f);

                }
        }
    }
}
