using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackgroundTest
{
    class holder
    {

        public void Collision()
        {
            #region old collision

            //if ((Hugo.Position.Y) >= (WorldVariables.WindowHeight / 2) - 31)
            //{
            //    grounded = true;
            //    Hugo.Position = new Vector2(Hugo.Position.X, (WorldVariables.WindowHeight / 2 - 31));
            //    physics.Velocity = new Vector2(physics.Velocity.X, 0);
            //}
            //else
            //    grounded = false;


            //if ((Hugo.Position.X) >= (WorldVariables.WindowWidth / 2) - 16)
            //{
            //    Hugo.Position = new Vector2(((WorldVariables.WindowWidth / 2) - 16), Hugo.Position.Y);

            //    if (physics.Velocity.X != 0)
            //        WallDirection = Math.Sign(physics.Velocity.X);

            //    if (physics.PhysicsMode == 1)
            //    {
            //        physics.WallSliding = true;                    
            //    }
            //}
            //else if ((Hugo.Position.X) <= (-WorldVariables.WindowWidth / 2) + 16)
            //{
            //    Hugo.Position = new Vector2(((-WorldVariables.WindowWidth / 2) + 16), Hugo.Position.Y);

            //    if (physics.Velocity.X != 0)
            //        WallDirection = Math.Sign(physics.Velocity.X);
            //    if (physics.PhysicsMode == 1)
            //    {
            //        physics.WallSliding = true;
            //    }
            //}

            #endregion

            #region Sidecollision

            //if (physics.Velocity.X > 0)
            //if (true)
            //{
            //    LevelLoader.level.CalculateLocation(new Vector2(SDetector1.X, SDetector1.Y));

            //    chunk1 = LevelLoader.level.SelectedChunk;

            //    if (chunk1 != -1)
            //    {
            //        tile1 = LevelLoader.level.SelectedTile;
            //        chunk1Pos = LevelLoader.level.SelectedChunkPosition;
            //        tile1Pos = LevelLoader.level.SelectedTilePosition;
            //        transparent1 = LevelLoader.level.SelectedTileAir;

            //        Depth1 = PixelsRight(Detector, SDetector1, LevelLoader.level.ReturnChunk(chunk1), tile1);
            //    }
            //    else
            //    {
            //        transparent1 = true;
            //        Depth1 = 0;
            //    }

            //    Hugo.Position = new Vector2(Hugo.Position.X - (Depth1), Hugo.Position.Y);


            //    if (Depth1 > 0)
            //    {
            //        if (physics.Velocity.X != 0)
            //            WallDirection = Math.Sign(physics.Velocity.X);
            //        if (physics.PhysicsMode == 1)
            //        {
            //            physics.WallSliding = true;
            //        }

            //        physics.Velocity = new Vector2(0, physics.Velocity.Y);
            //    }


            //    Console.WriteLine(Depth1);
            //}
            //else if (physics.Velocity.X < 0)
            //{
            //    //Check left Sensors
            //}

            #endregion
        }

        #region Sliding Physics
        
                    //        //Sliding
                    //case 2:
                    //    if (Math.Sign(gsp) != Math.Sign(Math.Sin(angle)))
                    //        gsp += (0.078125f) * (float)Math.Sin(angle);
                    //    else
                    //        gsp += (0.3125f) * (float)Math.Sin(angle);

                    //    if (input.X != 0)
                    //        //Apply deceleration if X input is opposite to velocity.
                    //        if (Math.Sign(input.X) != Math.Sign(gsp))
                    //            gsp += 0.125f * Math.Sign(input.X);

                    //    //Apply friction.
                    //    //might need to check this...
                    //    gsp -= Math.Min(Math.Abs(gsp), 0.025f) * Math.Sign(gsp);

                    //    //If X velocity is less that 1 stop sliding.
                    //    //Alternatively if X velocity is more than
                    //    //2.5 times max velocity then cap velocity.
                    //    if (Math.Abs(gsp) < 1f)
                    //        physicsMode = 0;
                    //    else if (Math.Abs(gsp) > maxVelocity * 2.5f)
                    //        gsp = (maxVelocity * 2.5f) * Math.Sign(gsp);

                    //    vel.X = gsp * (float)Math.Cos(angle);

                    //    break;

        #endregion

        #region Sliding Input

            //        if (physics.PhysicsMode == 0)
            //{
            //    //If walking and input Y is -1 (Down), activate slide mode. 
            //    if (physics.Input.Y == -1 && Math.Abs(physics.Velocity.X) > 2f)
            //        physics.PhysicsMode = 2;
            //}

        #endregion

        #region Wallslide Input

        //        if (physics.WallSliding)
            //{
            //    //Let go of wall if input X is away from wall.
            //    if (Math.Sign(physics.Input.X) == -Math.Sign(WallDirection))
            //    {
            //        WallTimer.Update(gameTime);

            //        if (WallTimer.Finished)
            //            physics.WallSliding = false;
            //    }
            //    else
            //        WallTimer.ResetTimer();

            //    //Jump off wall at 45 degree angle.
            //    if (Jump)
            //    {
            //        physics.Velocity = new Vector2((jumpHeight * 0.8f) * WallDirection, (jumpHeight * 0.8f));
            //        physics.WallSliding = false;
            //    }
            //}

        #endregion



























    }
}
