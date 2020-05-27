using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace DevStomp
{
    class Quadtree
    {
        private int MAX_OBJECTS = 10;
        private int MAX_LEVELS = 5;

        private int level;
        private List<Rectangle> objects;
        private Rectangle bounds;
        private Quadtree[] nodes;

        int verticalMidpoint;
        int horizontalMidpoint;

        /*
         * Constructor
         */
        public Quadtree(int pLevel, Rectangle pBounds)
        {
            level = pLevel;
            objects = new List<Rectangle>();
            bounds = pBounds;
            nodes = new Quadtree[4];

            verticalMidpoint = bounds.X + (bounds.Width / 2);
            horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            //if (level < MAX_LEVELS)
            //    split();
        }

        public void clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].clear();
                    nodes[i] = null;
                }
            }
        }

        private void split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            verticalMidpoint = bounds.X + (bounds.Width / 2);
            horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        
        //Determine which node the object belongs to. -1 means
        //object cannot completely fit within a child node and is part
        //of the parent node        

        private int getIndex(Rectangle pRect)
        {
            int index = -1;
            
            // Object can completely fit within the top quadrants
            bool topQuadrant = (pRect.Y < horizontalMidpoint && pRect.Y + pRect.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (pRect.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (pRect.X < verticalMidpoint && pRect.X + pRect.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (pRect.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        //Insert the object into the quadtree. If the node
        //exceeds the capacity, it will split and add all
        //objects to their corresponding nodes.

        public void insert(Rectangle pRect)
        {
            int index = getIndex(pRect);

            if (index >= 0) // pRect should always go in a child if possible
            {
                if (nodes[0] != null) // we have children
                    nodes[index].insert(pRect); // so put pRect into the child
                else // we don't have children
                {
                    if (level < MAX_LEVELS)
                    {
                        split(); // so we make children if possible
                        nodes[index].insert(pRect); // and put pRect into the child
                    }
                    else // node can't split any more so put it here
                        objects.Add(pRect);
                }
            }
            else
                objects.Add(pRect); // index == -1 so add here



            //if (nodes[0] != null)
            //{
            //    int index = getIndex(pRect);

            //    if (index != -1)
            //    {
            //        nodes[index].insert(pRect);
            //        return;
            //    }
            //}

            //objects.Add(pRect);

            //if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            //{
            //    if (nodes[0] == null)
            //    {
            //        split();
            //    }

            //    int i = 0;
            //    while (i < objects.Count)
            //    {
            //        int index = getIndex(objects[i]);

            //        if (index != -1)
            //        {
            //            nodes[index].insert(objects[i]);
            //            objects.RemoveAt(i);
            //        }
            //        else
            //        {
            //            i++;
            //        }
            //    }
            //}
        }

        public List<Rectangle> retrieve(List<Rectangle> returnObjects, Rectangle pRect)
        {
            int index = getIndex(pRect);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].retrieve(returnObjects, pRect);
            }

            returnObjects.AddRange(objects);

            return returnObjects;
        }

        public void Draw(GraphicsDevice graphicsDevice, Color color)
        {
            //bounds.X = 0;
            //bounds.Y = 0;
            //bounds.Width = 5000;
            //bounds.Height = 5000;


            //

            //for (int i = 0; i < objects.Count; i++)
            //    
            if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.B) && level == 0)
            {
                clear();

                insert(new Rectangle(
                            GlobalVariables.RandomNumber.Next(0, 4990),
                            GlobalVariables.RandomNumber.Next(0, 4990),
                            GlobalVariables.RandomNumber.Next(5, 50),
                            GlobalVariables.RandomNumber.Next(5, 50))
                            );
            }

            DrawRectangle3.AddRectangle(bounds, Color.Red);

            for (int i = 0; i < objects.Count; i++)
                DrawRectangle3.AddRectangle(objects[i], Color.White);

            for (int i = 0; i < nodes.Length; i++)
                if (nodes[i] != null)
                    nodes[i].Draw(graphicsDevice, color);





            if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.A) && level == 0)
            {
                clear();
                //DrawRectangles.Rectangles.Clear();

                Rectangle test = new Rectangle(GlobalVariables.RandomNumber.Next(0, 4990), GlobalVariables.RandomNumber.Next(0, 4990), 10, 10);

                for (int i = 0; i < 2000; i++)
                {
                    //insert(new Rectangle(
                    //    GlobalVariables.RandomNumber.Next(0, 4990),
                    //    GlobalVariables.RandomNumber.Next(0, 4990),
                    //    GlobalVariables.RandomNumber.Next(5, 50),
                    //    GlobalVariables.RandomNumber.Next(5, 50))
                    //    );

                    insert(test);
                }
            }


            //DrawRectangle.Draw(graphicsDevice, bounds, color);

            //for (int i = 0; i < objects.Count; i++)
            //    DrawRectangle.Draw(graphicsDevice, objects[i], color);

        }
    }
}
