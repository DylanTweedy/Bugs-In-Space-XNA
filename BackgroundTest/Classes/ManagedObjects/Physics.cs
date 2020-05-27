using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class Physics
    {
        #region Variables
        
        Timer ControlLock;

        float acc;
        float dec;
        float frc;
        float air;
        float grv;
        float maxVelocity;
        float gravityModifier;
        float gsp;
        //float previousgsp;
        float angle;

        public float originalMaxVelocity;
        //public float walkVelocity;
        //public float velocityDifference;

        Vector2 input;
        Vector2 vel;
        Vector2 previousVel;

        bool running;
        bool decelerating;
        bool gravityEnabled;
        bool wallSliding;
        bool airbourne;

        int physicsMode;
        int previousPhysicsMode;
        int wallMode;
        int previousWallMode;
        int gravityMode;
        int ceiling;

        bool wallWalking;
        bool sliding;
        bool falling;

        #endregion

        #region Properties

        public float GroundSpeed
        {
            get { return gsp; }
            set { gsp = value; }
        }

        public int GravityMode
        {
            get { return gravityMode; }
            set { gravityMode = value; }
        }

        public int WallMode
        {
            get { return wallMode; }
            set { wallMode = value; }
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public int PhysicsMode
        {
            get { return physicsMode; }
            set { physicsMode = value; }
        }

        public bool Decelerating
        {
            get { return decelerating; }
        }

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }

        public bool WallSliding
        {
            get { return wallSliding; }
            set { wallSliding = value; }
        }

        public float MaxVelocity
        {
            get { return maxVelocity; }
            set { maxVelocity = value; }
        }

        public Vector2 Velocity
        {
            get { return vel; }
            set { vel = value; }
        }

        public Vector2 Input
        {
            get { return input; }
            set { input = value; }
        }



        #endregion

        #region Abilities

        public bool WallWalking
        {
            get { return wallWalking; }
            set { wallWalking = value; }
        }

        #endregion

        public Physics(float MaxSpeed, float gravity, int grvMode)
        {
            maxVelocity = MaxSpeed;
            originalMaxVelocity = maxVelocity;
            //walkVelocity = maxVelocity * 0.65f;
            //velocityDifference = maxVelocity - walkVelocity;
            acc = MaxSpeed / 12f;
            frc = acc * 5;
            dec = acc;
            air = acc / 1.5f;
            grv = gravity;
            previousVel = vel;
            decelerating = false;
            running = false;
            wallSliding = false;
            physicsMode = 1;
            angle = 0;
            wallMode = 0;
            previousWallMode = wallMode;
            previousPhysicsMode = physicsMode;
            gravityMode = grvMode;
            wallWalking = false;
            airbourne = false;
            ControlLock = new Timer(0f, true);
            sliding = false;
            falling = false;

            ceiling = gravityMode + 2;
            if (ceiling > 4)
                ceiling -= 4;
        }
        
        public void EnableGravity(bool State, float GravityModifier)
        {
            gravityEnabled = State;
            gravityModifier = GravityModifier;
        }

        public void Update(GameTime gameTime)
        {
            if (input != Vector2.Zero || vel != Vector2.Zero || physicsMode != 0)
            {
                if (Math.Abs(input.X) < 0.2f)
                    input.X = Math.Sign(input.X) * 0.2f;

                //if (wallMode != 0 && previousWallMode == 0)
                //{
                //    ControlLock.ResetTimer();
                //    ControlLock.Update(gameTime);
                //}
                if (wallMode == ceiling && Input.X != 0 && !wallWalking)
                    input.X = -Math.Sign(vel.X);

                if (wallMode != 0 && !wallWalking)
                {
                    if (Math.Abs(angle) >= 1.57079633f && Math.Abs(gsp) < 3.5f)
                    {
                        input = Vector2.Zero;
                        WallMode = 0;
                        falling = true;
                    }

                    gsp += (0.125f) * ((float)Math.Sin(angle) + Math.Sign(Math.Sin(angle)));
                }

                if (input != Vector2.Zero)
                    falling = false;    

                if (wallMode != gravityMode)
                    sliding = true;

                if (wallMode != 0)
                {
                    if (airbourne)
                    {
                        //returning to ground.
                        if (Math.Abs(angle) < 0.785398163f)
                            gsp = vel.X;
                        else if (Math.Abs(vel.X) > vel.Y)
                            gsp = vel.X;
                        else
                            gsp = vel.Y * Math.Sign(angle);

                        airbourne = false;
                    }

                    if (input.X != 0)
                    {
                        if (Math.Sign(input.X) == Math.Sign(gsp))
                        {
                            //If input is not 1 decrease velocity.
                            if (maxVelocity * Math.Abs(input.X) < Math.Abs(gsp) && Math.Abs(input.X) != 1)
                                gsp += acc * -Math.Sign(input.X);

                            //Increase acceleration.
                            if (Math.Abs(gsp) < (maxVelocity * Math.Abs(input.X)))
                                gsp += acc * Math.Sign(input.X);
                        }
                        else
                        {
                            //Start deceleration animation.
                            if (Math.Abs(gsp) >= maxVelocity * 0.75f)
                                decelerating = true;

                            //Apply deceleration if X input is opposite to velocity.
                            gsp += dec * input.X;
                        }

                        sliding = false;

                    }
                    else
                    {
                        //If no input apply friction.                        
                        //if (wallMode == gravityMode || wallWalking)
                        
                        if (wallMode == ceiling)
                        {
                            gsp -= Math.Min(Math.Abs(gsp), 0.25f) * Math.Sign(gsp);
                            sliding = false;
                        }
                        else if (sliding && !wallWalking)
                        {
                            gsp -= 0.1f * Math.Sign(gsp);

                            if (Math.Abs(gsp) <= 0.5f)
                                sliding = false;
                        }
                        else if (Math.Abs(angle) < 0.65f && !wallWalking && Math.Abs(gsp) > 0.5f)
                            gsp *= 0.8f;
                        else if (Math.Abs(gsp) <= 0.5f)
                            gsp -= Math.Min(Math.Abs(gsp), 0.3f) * Math.Sign(gsp);
                        else if (wallWalking)
                            gsp = 0;




                        //else if (angle == 0)
                        //    gsp = 0;
                        //else
                        //    gsp -= Math.Min(Math.Abs(gsp), 0.3f) * Math.Sign(gsp);

                        decelerating = false;
                    }

                    if (Math.Abs(gsp) > maxVelocity)
                        gsp -= gsp * 0.01f;

                    vel.X = gsp * (float)Math.Cos(angle);
                    vel.Y = gsp * (float)Math.Sin(angle);

                }
                else
                {
                    airbourne = true;
                    
                    if (!ControlLock.Finished)
                    {
                        input = Vector2.Zero;
                        ControlLock.Update(gameTime);
                    }


                    if (!wallSliding)
                    {   
                        //Increase acceleration
                        if (Math.Abs(vel.X) < maxVelocity * Math.Abs(input.X) || Math.Sign(input.X) != Math.Sign(vel.X))
                            vel.X += air * input.X;

                        //Apply air drag if Y velocity is between 0 and -4
                        //and if X velocity is less than 0.125 
                        if (Math.Abs(vel.X) > 0.125 && input.X == 0 && !falling)
                            vel.X *= 0.93f;

                        vel.Y += grv;

                        if (Math.Abs(vel.X) > maxVelocity * 0.8f)
                            vel.X -= vel.X * 0.01f;
                    }
                    else
                    {
                        vel.Y += (grv * 0.6f) + (-Math.Abs(vel.X) * 0.2f);
                        vel.X = 0;
                    }

                    if (vel.Y > 16)
                    vel.Y = 16;

                    gsp = vel.X;

                }
                
                if (Math.Sign(gsp) != Math.Sign(previousVel.X))
                    decelerating = false;

                previousWallMode = wallMode;
                previousPhysicsMode = physicsMode;
                previousVel = vel;
            }
        }
    }
}
