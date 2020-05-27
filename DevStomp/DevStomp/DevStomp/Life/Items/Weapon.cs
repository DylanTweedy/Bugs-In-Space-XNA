using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevStomp
{
    class Weapon
    {
        public List<List<ValueType>> Actions;
        public int Laser = 0;
        public int LaserMax;
        public int LaserSpeed;

        public byte SizeType;
        public int SizeID;
        public int ID;

        public Vector2 Position;
        public float Rotation;
        public float Scale;
        public Color Tint;

        public float Timer;

        public Weapon()
        {
            if (GlobalVariables.RandomNumber.Next(0, 2) == 0)
            {
                LaserSpeed = 10;
                Laser = LaserSpeed;
            }

            SizeType = (byte)GlobalVariables.RandomNumber.Next(0, 3);
            SizeType = 0;
            LaserMax = 250;

            switch (SizeType)
            {
                case 0:
                    SizeID = GlobalVariables.RandomNumber.Next(0, SM.PSmall.Elements);
                    break;

                case 1:
                    SizeID = GlobalVariables.RandomNumber.Next(0, SM.PMedium.Elements);
                    break;

                case 2:
                    SizeID = GlobalVariables.RandomNumber.Next(0, SM.PLarge.Elements);
                    break;
            }
            
            ID = GlobalVariables.RandomNumber.Next(0, SM.Weapons.Elements);
            //ID = 2;

            Actions = ParticleListCreation.GenerateRandomList(GlobalVariables.RandomNumber.Next(0, 5));
            //Actions = ParticleListCreation.GenerateTestList();
        }

        public void Update()
        {
            for (int i = 0; i < Actions.Count; i++)
            {

                switch ((int)Actions[i][0])
                {
                    case 4:
                        if (Laser > 0)
                        {
                            Timer += (float)GlobalVariables.FrameTime;

                            if (Timer > 1f)
                                Actions[i][1] = (int)GlobalVariables.RandomNumber.Next(0, 32000);
                        }
                        break;

                    case 2:
                    case 3:
                    case 18:
                        if (Laser > 0)
                        {
                            Timer += (float)GlobalVariables.FrameTime;

                            if (Timer > 1f)
                                Actions[i][2] = (int)GlobalVariables.RandomNumber.Next(0, 32000);
                        }
                        break;
                }
            }

            if (Timer > 1f)
                Timer = 0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rect = SM.Weapons.Rectangles[ID];
            SpriteEffects SE = SpriteEffects.None;

            if (Rotation < -Math.PI)
                SE = SpriteEffects.FlipHorizontally;


            spriteBatch.Draw(SM.Weapons.S(ID), Position, rect, Tint, Rotation, new Vector2(rect.Width / 2, rect.Height - (rect.Height / 6)), Scale, SE, 1f);
        }
    }
}
