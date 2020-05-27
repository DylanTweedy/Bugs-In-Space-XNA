using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Spine;

namespace BackgroundTest
{
    class Test
    {
        GamePadState currentGamepadState;
        GamePadState previousGamepadState;
        KeyboardState keyboardState;
        KeyboardState pKeyboardState;
               
        TestObject Hugo;
        Texture2D HugoSpriteSheet;
        public Physics physics;

        bool grounded;
        bool Jump;
        bool StopJump;
        float WallDirection;

        float jumpHeight;
        float jumpStop;

        Timer WallTimer;

        Texture2D Detector;

        Rectangle TestSensorR;
        Rectangle TestSensorL;

        Vector2 SensorL1;
        Vector2 SensorL2;
        Vector2 SensorR1;
        Vector2 SensorR2;
        int SideX;
        int SideY;


        Vector2 SensorD1;
        Vector2 SensorD2;
        int GroundX;
        int GroundY;

        Vector2 SensorU1;
        Vector2 SensorU2;
        int CeilingX;
        int CeilingY;


        float angle;

        int chunkX;
        int chunkY;
        int tileX;
        int tileY;
        int pixelX;
        int pixelY;

        int worldChunksX;
        int worldChunksY;

        int chunkSize;
        int tileSize;
        int levelWidth;
        int levelHeight;

        Vector2 levelPosition;

        int d1;
        int d2;
        bool ActiveChunk;
        int TileType;
        int SensorX;
        int SensorY;

        Skeleton skeleton;
        AnimationState state;
        SkeletonBounds bounds;

        public Vector2 Position
        {
            get { return Hugo.Position; }            
        }

        public float Rotation
        {
            get { return Hugo.Rotation; }
        }

        public void Initialize(float JumpHeight, float MaxSpeed, float Gravity)
        {
            physics = new Physics(MaxSpeed, Gravity, 1);

            physics.EnableGravity(true, 1f);
            grounded = false;

            Jump = false;
            StopJump = false;

            jumpHeight = -JumpHeight;
            jumpStop = jumpHeight * 0.65f;

            WallTimer = new Timer(0.3f, true);

            bounds = new SkeletonBounds();

            LoadLevel();
        }
         
        public void LoadContent(ContentManager Content)
        {
            HugoSpriteSheet = Content.Load<Texture2D>("Images//UniqueCharacters//HugoSheet");
            Hugo = new TestObject(HugoSpriteSheet);

            LoadCharacter(Content);

            //////////////////////////////
            //HugoSpriteSheet = Content.Load<Texture2D>("Images//TestRect");
            //Hugo = new TestObject(HugoSpriteSheet);

            //Hugo.AddObject(0, 0);
            //Hugo.AddAnimation("Test", 0, 0, 32, 64, 1, 1f, null);
            //Hugo.ObjectColour(Color.DarkGreen);

            //Hugo.AddObject(0, -32);
            //Hugo.AddAnimation("Test", 0, 0, 32, 32, 4, 0.1f, null);
            //Vector2[] test = new Vector2[4];
            //test[0] = new Vector2(2, 0);
            //test[1] = new Vector2(-2, 0);
            //test[2] = new Vector2(-2, 2);
            //test[3] = new Vector2(2, 2);
            //Hugo.AddMovementAnimation("Test", test, 0.1f, null);

            
            //Hugo.ObjectColour(Color.Red);
            ///////////////////////////////
            Detector = Content.Load<Texture2D>("Images//Collision//CollisionDot");

            TestSensorR = new Rectangle((int)Hugo.Position.X + 40, (int)Hugo.Position.Y, 1, 1);
            TestSensorL = new Rectangle((int)Hugo.Position.X - 40, (int)Hugo.Position.Y, 1, 1);

            GroundX = 16;
            GroundY = 31;
            SensorD1 = new Vector2(Hugo.Position.X - GroundX, Hugo.Position.Y + GroundY);
            SensorD2 = new Vector2(Hugo.Position.X + GroundX, Hugo.Position.Y + GroundY);

            CeilingX = 16;
            CeilingY = 31;
            SensorU1 = new Vector2(Hugo.Position.X + GroundX, Hugo.Position.Y - GroundY);
            SensorU2 = new Vector2(Hugo.Position.X - GroundX, Hugo.Position.Y - GroundY);

            SideX = 16;
            SideY = 12;
            SensorR1 = new Vector2(Hugo.Position.X + SideX, Hugo.Position.Y - SideY);
            SensorR2 = new Vector2(Hugo.Position.X + SideX, Hugo.Position.Y + SideY);
            SensorL1 = new Vector2(Hugo.Position.X - SideX, Hugo.Position.Y + SideY);
            SensorL2 = new Vector2(Hugo.Position.X - SideX, Hugo.Position.Y - SideY);
            
            LoadSkeleton();
        }

        private void LoadSkeleton()
        {
            String name = "skeleton2"; // "goblins";
            //if (WorldVariables.RandomNumber.Next(0, 2) == 0)
            //    name = "skeleton2";
            //else
                name = "skeleton";

            skeleton = SpineLoader.LoadSkeleton(name);
            //skeleton.SetSkin("goblin");
            //skeleton.SetSlotsToSetupPose();

            // Define mixing between animations.
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            if (name == "spineboy")
            {
                stateData.SetMix("walk", "jump", 0.2f);
                stateData.SetMix("jump", "walk", 0.4f);
            }

            
            //if (true)
            //{
            //    // Event handling for all animations.
            //    state.Start += Start;
            //    state.End += End;
            //    state.Complete += Complete;
            //    state.Event += Event;

            //    state.SetAnimation(0, "drawOrder", true);
            //}
            //else

            
            //TrackEntry entry = state.AddAnimation(0, "jump", false, 0);
            //entry.End += new EventHandler<StartEndArgs>(End); // Event handling for queued animations.
            //state.AddAnimation(0, "Walk1", true, 0);

            stateData.SetMix("Walk1", "Jump1", 0.5f);
            stateData.SetMix("Jump1", "Falling1", 0.6f);
            stateData.SetMix("Falling1", "Walk1", 0.7f);
            stateData.SetMix("Walk1", "Idle1", 1f);
            stateData.SetMix("Falling1", "Land1", 1f);
            stateData.SetMix("Land1", "Idle1", 0.3f);

            state = new AnimationState(stateData);
            state.SetAnimation(0, "Walk1", true);

            skeleton.SetAttachment("RHAttatchment1", null);
            skeleton.SetAttachment("RHAttachment2", null);

            skeleton.UpdateWorldTransform();

            Hugo.Position = new Vector2(5000, 20000);
        }

        private void LoadCharacter(ContentManager Content)
        {
            Color BodyTone = WorldVariables.colorPicker.RandomColour();
            Color SkinTone = WorldVariables.colorPicker.HumanSkinTones(10);
            Color ClothesTone = WorldVariables.colorPicker.RandomColour();
            //Color ClothesTone = new Color(WorldVariables.RandomNumber.Next(50, 255), 0, 0);
            
            Color Compliment = WorldVariables.colorPicker.SchemeCompliment(ClothesTone);

            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("Jump1", 576, 0, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Jump2", 640, 0, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Jump3", 704, 0, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Break", 768, 0, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Walking", 0, 0, 64, 64, 8, 0.07f, null);
            Hugo.AddAnimation("WallSliding", 896, 0, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Running", 0, 192, 64, 64, 6, 0.13f, null);
            Hugo.AddAnimation("Sliding", 384, 192, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Idle", 512, 0, 64, 64, 1, 0f, null);
            Hugo.ObjectColour(ClothesTone);

            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("Jump1", 576, 128, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Jump2", 640, 128, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Jump3", 704, 128, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Break", 768, 128, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Walking", 0, 128, 64, 64, 8, 0.07f, null);
            Hugo.AddAnimation("WallSliding", 896, 128, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Running", 0, 320, 64, 64, 6, 0.13f, null);
            Hugo.AddAnimation("Sliding", 384, 320, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Idle", 512, 128, 64, 64, 1, 0f, null);
            Hugo.ObjectColour(Compliment);


            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("head", 64 * 2, 384, 64, 64, 1, 0, null);
            Hugo.ObjectColour(SkinTone);

            Hugo.AddMAnimation("Test", 0.1f, null);
            Hugo.AddMAnimationVector("Test", 1, 0);
            Hugo.AddMAnimationVector("Test", -1, 1);
            Hugo.AddMAnimationVector("Test", 1, 0);
            Hugo.AddMAnimationVector("Test", -1, -2);


            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("happy", 64 * WorldVariables.RandomNumber.Next(0, 15), 832, 64, 64, 1, 0, null);
            //Hugo.AddIdleAnimation(3, "shocked", 192, 896, 64, 64, 1, 0.15f, 1.5f, 5f);

            Hugo.AddMAnimation("Test", Hugo.ReturnMAnimattionList(2, "Test"), 0.1f, null);

            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("neutral", 64 * WorldVariables.RandomNumber.Next(1, 15), 640, 64, 64, 1, 0, null);
            Hugo.AddIdleAnimation(4, "blink", 0, 640, 64, 64, 1, 0.15f, 1.5f, 5f);

            Hugo.AddMAnimation("Test", Hugo.ReturnMAnimattionList(2, "Test"), 0.1f, null);

            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("hair", 64 * WorldVariables.RandomNumber.Next(0, 15), 448, 64, 64, 1, 0, null);
            Hugo.ObjectColour(WorldVariables.colorPicker.HumanHairTones(1000));

            Hugo.AddMAnimation("Test", Hugo.ReturnMAnimattionList(2, "Test"), 0.1f, null);


            Hugo.AddObject(0, 0);
            Hugo.AddAnimation("Jump1", 576, 64, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Jump2", 640, 64, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Jump3", 704, 64, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Break", 768, 64, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Walking", 0, 64, 64, 64, 8, 0.07f, null);
            Hugo.AddAnimation("WallSliding", 896, 64, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Running", 0, 256, 64, 64, 6, 0.13f, null);
            Hugo.AddAnimation("Sliding", 384, 256, 64, 64, 1, 0f, null);
            Hugo.AddAnimation("Idle", 512, 64, 64, 64, 1, 0f, null);


            Hugo.ObjectColour(SkinTone);

            Hugo.ChangeAnimation(0, "Walking");
            Hugo.ChangeAnimation(1, "Walking");
            Hugo.ChangeAnimation(6, "Walking");
            Hugo.ChangeMovementAnimation(2, "Test");
            Hugo.ChangeMovementAnimation(3, "Test");
            Hugo.ChangeMovementAnimation(4, "Test");
            Hugo.ChangeMovementAnimation(5, "Test");
        }

        public void LoadLevel()
        {
            //levelPosition = LevelLoader.level.LevelPosition;

            //chunkSize = LevelLoader.level.ChunkSize;
            //tileSize = LevelLoader.level.TileSize;

            //worldChunksX = LevelLoader.level.ChunkRows;
            //worldChunksY = LevelLoader.level.ChunkColumns;

            //levelWidth = LevelLoader.level.LevelWidth;
            //levelHeight = LevelLoader.level.LevelHeight;

            levelPosition = Vector2.Zero;

            chunkSize = 0;
            tileSize = 0;

            worldChunksX = 0;
            worldChunksY = 0;

            levelWidth = 0;
            levelHeight = 0;
        }

        int test = 0;
        int test2 = 0;

        public void Update(GameTime gameTime)
        {
            previousGamepadState = currentGamepadState;
            currentGamepadState = GamePad.GetState(PlayerIndex.One);

            pKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            Hugo.AnimationSpeed(1.5f);





            if (!grounded && (state.GetCurrent(0).Animation.Name == "Walk1" || state.GetCurrent(0).Animation.Name == "Idle1"))
            {
                test++;


                if (test > 10)
                {
                    state.SetAnimation(0, "Jump1", false);
                    state.AddAnimation(0, "Falling1", true, -0.5f);

                    test = 0;
                }
            }
            else
            {
                test = 0;

                if (grounded && state.GetCurrent(0).Animation.Name != "Run1" && Math.Abs(physics.GroundSpeed) > 0.01)
                    state.SetAnimation(0, "Run1", true);
                else if (grounded && state.GetCurrent(0).Animation.Name != "Idle1" && state.GetCurrent(0).Animation.Name != "Land1" && Math.Abs(physics.GroundSpeed) < 0.01)
                {
                    state.SetAnimation(0, "Land1", false);
                    state.AddAnimation(0, "Idle1", true, 0);
                }
            }

            if (state.GetCurrent(0).Animation.Name == "Walk1" && Math.Abs(physics.GroundSpeed) > 1f)
                state.TimeScale = Math.Abs(physics.GroundSpeed / 10);
            else
                state.TimeScale = 1f;
            





            //Hugo.AutoRotate = true;

            //Console.WriteLine(DateTime.Now.TimeOfDay);


            Hugo.Position += new Vector2(physics.MaxVelocity * currentGamepadState.ThumbSticks.Left.X, physics.MaxVelocity * -currentGamepadState.ThumbSticks.Left.Y);




            if (keyboardState.IsKeyDown(Keys.W))
                Hugo.Position += new Vector2(0, -physics.MaxVelocity);
            if (keyboardState.IsKeyDown(Keys.A))
                Hugo.Position += new Vector2(-physics.MaxVelocity, 0);
            if (keyboardState.IsKeyDown(Keys.S))
                Hugo.Position += new Vector2(0, physics.MaxVelocity);
            if (keyboardState.IsKeyDown(Keys.D))
                Hugo.Position += new Vector2(physics.MaxVelocity, 0);

            //if (currentGamepadState.IsButtonDown(Buttons.DPadDown) && !previousGamepadState.IsButtonDown(Buttons.DPadDown))
            //    Hugo.Position += new Vector2(0, 1);
            //if (currentGamepadState.IsButtonDown(Buttons.DPadRight) && !previousGamepadState.IsButtonDown(Buttons.DPadRight))
            //    Hugo.Position += new Vector2(1, 0);
            //if (currentGamepadState.IsButtonDown(Buttons.DPadUp) && !previousGamepadState.IsButtonDown(Buttons.DPadUp))
            //    Hugo.Position += new Vector2(0, -1);
            //if (currentGamepadState.IsButtonDown(Buttons.DPadLeft) && !previousGamepadState.IsButtonDown(Buttons.DPadLeft))
            //    Hugo.Position += new Vector2(-1, 0);
            
            //Update physics and move character.
            //UpdateInput(gameTime);
            //UpdatePhysics(gameTime);
            //Hugo.MoveBy(physics.Velocity.X, physics.Velocity.Y);

            UpdateSensorLocation();
            TestSensors();
            UpdateSensorLocation();   
            //UpdateCollision();


            //Console.WriteLine("Collision: " + Hugo.Position);

            UpdateSensorLocation();


            //If not on ground then must be airbourne.
            if (!grounded)
                physics.WallMode = 0;
            else
            {
                ////If not sliding then must be walking.
                //if (physics.PhysicsMode != 2)
                //    physics.PhysicsMode = 0;

                if (physics.WallMode == 0)
                        physics.WallMode = 1;
                
                //You're not on the wall, you're on the ground. Stupid.
                physics.WallSliding = false;
            }


            //UpdateAnimation(gameTime);
            UpdateSkeleton(gameTime);
            Hugo.Update(gameTime);
        }

        private void UpdateSkeleton(GameTime gameTime)
        {           
            state.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);

            state.Apply(skeleton);

            skeleton.RootBone.ScaleX = 0.12f;
            skeleton.RootBone.ScaleY = 0.12f;

            skeleton.X = Position.X;
            skeleton.Y = Position.Y + 32;

            

            if (physics.GroundSpeed > 0)
            {
                skeleton.FlipX = true;
                skeleton.RootBone.Rotation = Rotation * 57.2957795f;
            }
            else if (physics.GroundSpeed < 0)
            {
                skeleton.FlipX = false;
                skeleton.RootBone.Rotation = -Rotation * 57.2957795f;
            }

            skeleton.FindBone("Spine").Rotation = (physics.GroundSpeed) + 90;
            skeleton.FindBone("Pelvis").Rotation = ((physics.GroundSpeed / 2) + 90);
            skeleton.FindBone("Head").Rotation = -(physics.GroundSpeed);
            //skeleton.FindBone("LeftHandAttachment").ScaleX = 4f;
            //skeleton.FindBone("LeftHandAttachment").ScaleY = 4f;

            if (rota > 359)
                rota = 0;


            skeleton.UpdateWorldTransform();
            
        }

        int rota = 0;

        private void UpdateSensorLocation()
        {
            switch (physics.WallMode)
            {
                case 0:
                    SensorD1.X = Hugo.Position.X - GroundX;
                    SensorD2.X = Hugo.Position.X + GroundX;
                    SensorD1.Y = Hugo.Position.Y + GroundY;
                    SensorD2.Y = Hugo.Position.Y + GroundY;

                    SensorU1.X = Hugo.Position.X + CeilingX;
                    SensorU2.X = Hugo.Position.X - CeilingX;
                    SensorU1.Y = Hugo.Position.Y - CeilingY;
                    SensorU2.Y = Hugo.Position.Y - CeilingY;

                    SensorR1.X = Hugo.Position.X + SideX;
                    SensorR2.X = Hugo.Position.X + SideX;
                    SensorR1.Y = Hugo.Position.Y - SideY;
                    SensorR2.Y = Hugo.Position.Y + SideY;

                    SensorL1.X = Hugo.Position.X - SideX;
                    SensorL2.X = Hugo.Position.X - SideX;
                    SensorL1.Y = Hugo.Position.Y + SideY;
                    SensorL2.Y = Hugo.Position.Y - SideY;

                    break;

                case 1:
                    SensorD1.X = Hugo.Position.X - GroundX;
                    SensorD2.X = Hugo.Position.X + GroundX;
                    SensorD1.Y = Hugo.Position.Y + GroundY;
                    SensorD2.Y = Hugo.Position.Y + GroundY;

                    SensorU1.X = Hugo.Position.X + CeilingX;
                    SensorU2.X = Hugo.Position.X - CeilingX;
                    SensorU1.Y = Hugo.Position.Y - CeilingY;
                    SensorU2.Y = Hugo.Position.Y - CeilingY;

                    SensorR1.X = Hugo.Position.X + SideX;
                    SensorR2.X = Hugo.Position.X + SideX;
                    SensorR1.Y = Hugo.Position.Y - SideY;
                    SensorR2.Y = Hugo.Position.Y + SideY;

                    SensorL1.X = Hugo.Position.X - SideX;
                    SensorL2.X = Hugo.Position.X - SideX;
                    SensorL1.Y = Hugo.Position.Y + SideY;
                    SensorL2.Y = Hugo.Position.Y - SideY;

                    break;

                case 2:
                    SensorD1.X = Hugo.Position.X + GroundY;
                    SensorD2.X = Hugo.Position.X + GroundY;
                    SensorD1.Y = Hugo.Position.Y + GroundX;
                    SensorD2.Y = Hugo.Position.Y - GroundX;

                    SensorU1.X = Hugo.Position.X - CeilingY;
                    SensorU2.X = Hugo.Position.X - CeilingY;
                    SensorU1.Y = Hugo.Position.Y - CeilingX;
                    SensorU2.Y = Hugo.Position.Y + CeilingX;

                    SensorR1.X = Hugo.Position.X - SideY;
                    SensorR2.X = Hugo.Position.X + SideY;
                    SensorR1.Y = Hugo.Position.Y - SideX;
                    SensorR2.Y = Hugo.Position.Y - SideX;
                    
                    SensorL1.X = Hugo.Position.X + SideY;
                    SensorL2.X = Hugo.Position.X - SideY;
                    SensorL1.Y = Hugo.Position.Y + SideX;
                    SensorL2.Y = Hugo.Position.Y + SideX;

                    TestSensorR = new Rectangle((int)Hugo.Position.X, (int)Hugo.Position.Y - 16, 1, 1);
                    TestSensorL = new Rectangle((int)Hugo.Position.X, (int)Hugo.Position.Y + 16, 1, 1);

                    break;

                case 3:
                    SensorD1.X = Hugo.Position.X + GroundX;
                    SensorD2.X = Hugo.Position.X - GroundX;
                    SensorD1.Y = Hugo.Position.Y - GroundY;
                    SensorD2.Y = Hugo.Position.Y - GroundY;

                    SensorU1.X = Hugo.Position.X - CeilingX;
                    SensorU2.X = Hugo.Position.X + CeilingX;
                    SensorU1.Y = Hugo.Position.Y + CeilingY;
                    SensorU2.Y = Hugo.Position.Y + CeilingY;

                    SensorR1.X = Hugo.Position.X - SideX;
                    SensorR2.X = Hugo.Position.X - SideX;
                    SensorR1.Y = Hugo.Position.Y + SideY;
                    SensorR2.Y = Hugo.Position.Y - SideY;
                    
                    SensorL1.X = Hugo.Position.X + SideX;
                    SensorL2.X = Hugo.Position.X + SideX;
                    SensorL1.Y = Hugo.Position.Y - SideY;
                    SensorL2.Y = Hugo.Position.Y + SideY;

                    break;

                case 4:
                    SensorD1.X = Hugo.Position.X - GroundY;
                    SensorD2.X = Hugo.Position.X - GroundY;
                    SensorD1.Y = Hugo.Position.Y - GroundX;
                    SensorD2.Y = Hugo.Position.Y + GroundX;

                    SensorU1.X = Hugo.Position.X + CeilingY;
                    SensorU2.X = Hugo.Position.X + CeilingY;
                    SensorU1.Y = Hugo.Position.Y + CeilingX;
                    SensorU2.Y = Hugo.Position.Y - CeilingX;

                    SensorR1.X = Hugo.Position.X + SideY;
                    SensorR2.X = Hugo.Position.X - SideY;
                    SensorR1.Y = Hugo.Position.Y + SideX;
                    SensorR2.Y = Hugo.Position.Y + SideX;
                    
                    SensorL1.X = Hugo.Position.X - SideY;
                    SensorL2.X = Hugo.Position.X + SideY;
                    SensorL1.Y = Hugo.Position.Y - SideX;
                    SensorL2.Y = Hugo.Position.Y - SideX;

                    TestSensorR = new Rectangle((int)Hugo.Position.X, (int)Hugo.Position.Y + 16, 1, 1);
                    TestSensorL = new Rectangle((int)Hugo.Position.X, (int)Hugo.Position.Y - 16, 1, 1);

                    break;
            }



        }

        public void UpdateInput(GameTime gameTime)
        {
            //Character Input
            physics.Input = currentGamepadState.ThumbSticks.Left;
            if (currentGamepadState.IsButtonDown(Buttons.A) && !previousGamepadState.IsButtonDown(Buttons.A))
            {
                Jump = true;
            }
            else
            {
                Jump = false;

                if (currentGamepadState.IsButtonUp(Buttons.A) && previousGamepadState.IsButtonDown(Buttons.A))
                {
                    StopJump = true;
                }
                else
                    StopJump = false;
            }

            if (physics.PhysicsMode == 1)
            {
                //If jump button is released, slow jump. 
                if (StopJump && physics.Velocity.Y < jumpStop)
                    physics.Velocity = new Vector2(physics.Velocity.X, jumpStop);
            }

            //If jump button is pressed, jump.
            if (Jump && grounded)
            {
                if (physics.WallMode == physics.GravityMode)
                    physics.Velocity = new Vector2((physics.Velocity.X - (jumpHeight * (float)Math.Sin(physics.Angle))), jumpHeight * (float)Math.Cos(physics.Angle));
                else
                    physics.Velocity = new Vector2((physics.Velocity.X - (jumpHeight * (float)Math.Sin(physics.Angle))), physics.Velocity.Y + (jumpHeight * (float)Math.Cos(physics.Angle)));
                physics.PhysicsMode = 1;
                physics.WallMode = 0;
                grounded = false;
            }
        }
        
        public void UpdatePhysics(GameTime gameTime)
        {
            physics.Update(gameTime);
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            //Change animation speed according to velocity.
            if (physics.Velocity != Vector2.Zero)
                Hugo.AnimationSpeed(Math.Abs(physics.Velocity.X) / 3);

            if (physics.Velocity != Vector2.Zero || Hugo.CurrentAnimation != "Idle" || physics.Input != Vector2.Zero)
            {
                //Reset waiting animation Timer.

                switch (physics.PhysicsMode)
                {
                    case 0:
                        if (Math.Abs(physics.Velocity.X) > 0 && !physics.Decelerating)
                        {
                            if (Hugo.CurrentAnimation != "Walking" && Math.Abs(physics.Velocity.X) <= physics.MaxVelocity * 0.8f)
                            {
                                Hugo.ChangeAnimation(0, "Walking");
                                Hugo.ChangeAnimation(1, "Walking");
                                Hugo.ChangeAnimation(6, "Walking");
                            }
                            else if (Hugo.CurrentAnimation != "Running" && Math.Abs(physics.Velocity.X) > physics.MaxVelocity * 0.8f)
                            {
                                Hugo.ChangeAnimation(0, "Running");
                                Hugo.ChangeAnimation(1, "Running");
                                Hugo.ChangeAnimation(6, "Running");
                            }
                        }
                        else if (physics.Decelerating)
                        {
                            Hugo.ChangeAnimation(0, "Break");
                            Hugo.ChangeAnimation(1, "Break");
                            Hugo.ChangeAnimation(6, "Break");
                        }
                        else
                        {
                            Hugo.ChangeAnimation(0, "Idle");
                            Hugo.ChangeAnimation(1, "Idle");
                            Hugo.ChangeAnimation(6, "Idle");
                        }

                        break;

                    case 1:
                        if (physics.WallSliding)
                        {
                            if (Hugo.CurrentAnimation != "WallSliding")
                            {
                                Hugo.ChangeAnimation(0, "WallSliding");
                                Hugo.ChangeAnimation(1, "WallSliding");
                                Hugo.ChangeAnimation(6, "WallSliding");
                            }
                        }
                        else if (physics.Velocity.Y < jumpHeight * 0.6)
                        {
                            if (Hugo.CurrentAnimation != "Jump1")
                            {
                                Hugo.ChangeAnimation(0, "Jump1");
                                Hugo.ChangeAnimation(1, "Jump1");
                                Hugo.ChangeAnimation(6, "Jump1");
                            }
                        }
                        else if (physics.Velocity.Y < jumpHeight * 0.3)
                        {
                            if (Hugo.CurrentAnimation != "Jump2")
                            {
                                Hugo.ChangeAnimation(0, "Jump2");
                                Hugo.ChangeAnimation(1, "Jump2");
                                Hugo.ChangeAnimation(6, "Jump2");
                            }
                        }
                        else if (Hugo.CurrentAnimation != "Jump3")
                        {
                            Hugo.ChangeAnimation(0, "Jump3");
                            Hugo.ChangeAnimation(1, "Jump3");
                            Hugo.ChangeAnimation(6, "Jump3");
                        }

                        break;



                    case 2:
                        if (Hugo.CurrentAnimation != "Sliding")
                        {
                            Hugo.ChangeAnimation(0, "Sliding");
                            Hugo.ChangeAnimation(1, "Sliding");
                            Hugo.ChangeAnimation(6, "Sliding");
                        }

                        break;
                }
            }
            else
            {
                //start waiting animation.
            }
            
            if (!physics.WallSliding)
            {
                //Flip sprite based on direction travelling.
                if (physics.Velocity.X > 0)
                    Hugo.ObjectEffect(SpriteEffects.FlipHorizontally);
                else if (physics.Velocity.X < 0)
                    Hugo.ObjectEffect(SpriteEffects.None);
            }
            else
            {
                //No flip if Wall Sliding.
                if (WallDirection == 1)
                    Hugo.ObjectEffect(SpriteEffects.None);
                else if (WallDirection == -1)
                    Hugo.ObjectEffect(SpriteEffects.FlipHorizontally);
            }
        }
        
        private void GroundSensors()
        {            
            int searchMode;

            if (physics.WallMode == 0)
                searchMode = physics.GravityMode;
            else
                searchMode = physics.WallMode;

            SensorLocationScan(SensorD1);
            if (ActiveChunk)
                d1 = FindDepth(searchMode, false);
            else
                d1 = -1;

            SensorLocationScan(SensorD2);
            if (ActiveChunk)
                d2 = FindDepth(searchMode, false);
            else
                d2 = -1;

            float rotAngle;

            if (d1 < 0 && d2 < 0)
            {
                grounded = false;
                rotAngle = 0;
            }
            else
            {
                grounded = true;

                float xDiff = (GroundX * 2) * ((float)Math.PI / 4);
                float yDiff = (d1 - d2);
                angle = (float)(Math.Atan2(yDiff, xDiff));

                int finalD;

                if (d1 > d2)
                    finalD = d1;
                else
                    finalD = d2;


                int realD = finalD;
                rotAngle = angle;

                finalD = (int)(finalD - (20 * (1 - Math.Cos(Math.Sin(angle)))));
                finalD = (int)(finalD - (5 * Math.Abs(Math.Sin(4 * angle))));


                switch (physics.WallMode)
                {
                    case 1:
                        if (grounded)
                            Hugo.Position = new Vector2(Hugo.Position.X, (int)(Hugo.Position.Y - (finalD)));
                        break;

                    case 2:
                        rotAngle -= (float)(Math.PI / 2);
                        if (grounded)
                            Hugo.Position = new Vector2(Hugo.Position.X - (finalD), (int)(Hugo.Position.Y));
                        break;

                    case 3:
                        rotAngle += (float)(Math.PI);
                        if (grounded)
                            Hugo.Position = new Vector2(Hugo.Position.X, (int)(Hugo.Position.Y + (finalD)));
                        break;

                    case 4:
                        rotAngle += (float)(Math.PI / 2);
                        if (grounded)
                            Hugo.Position = new Vector2(Hugo.Position.X + (finalD), (int)(Hugo.Position.Y));
                        break;
                }

                int wallmode = physics.WallMode;
                
                bool scanArea = false;

                if (rotAngle >= -0.785398163f && rotAngle <= 0.785398163f && physics.WallMode != 1)
                {
                    physics.WallMode = 1;
                    scanArea = true;
                }
                else if (rotAngle > 0.785398163f && rotAngle < 2.35619449f && physics.WallMode != 4)
                {
                    physics.WallMode = 4;
                    scanArea = true;
                }
                else if (rotAngle >= 2.35619449f || rotAngle <= -2.35619449f && physics.WallMode != 3)
                {
                    physics.WallMode = 3;
                    scanArea = true;
                }
                else if (rotAngle < -0.785398163f && rotAngle > -2.35619449f && physics.WallMode != 2)
                {
                    physics.WallMode = 2;
                    scanArea = true;
                }

                if (scanArea)
                {
                    UpdateSensorLocation();

                    if (physics.WallMode == 0)
                        searchMode = physics.GravityMode;
                    else
                        searchMode = physics.WallMode;

                    SensorLocationScan(SensorD1);
                    if (ActiveChunk)
                        d1 = FindDepth(searchMode, false);

                    SensorLocationScan(SensorD2);
                    if (ActiveChunk)
                        d2 = FindDepth(searchMode, false);

                    if (d1 < 0 && d2 < 0)
                    {
                        physics.WallMode = wallmode;
                        UpdateSensorLocation();

                        rotAngle = 0;

                        switch (physics.WallMode)
                        {
                            case 1:
                                if (grounded)
                                    Hugo.Position = new Vector2(Hugo.Position.X, (int)(Hugo.Position.Y - (realD - finalD)));
                                break;

                            case 2:
                                rotAngle -= (float)(Math.PI / 2);
                                if (grounded)
                                    Hugo.Position = new Vector2(Hugo.Position.X - (realD - finalD), (int)(Hugo.Position.Y));
                                break;

                            case 3:
                                rotAngle += (float)(Math.PI);
                                if (grounded)
                                    Hugo.Position = new Vector2(Hugo.Position.X, (int)(Hugo.Position.Y + (realD - finalD)));
                                break;

                            case 4:
                                rotAngle += (float)(Math.PI / 2);
                                if (grounded)
                                    Hugo.Position = new Vector2(Hugo.Position.X + (realD - finalD), (int)(Hugo.Position.Y));
                                break;
                        }
                    }
                }
            }

            if (physics.WallMode != 0)
                Hugo.Rotation -= (Hugo.Rotation - rotAngle) * 0.2f;
                        
            angle = rotAngle;
            physics.Angle = angle;
        }

        private void CeilingSensors()
        {
            int searchMode;

            if (physics.WallMode == 0)
            {
                searchMode = physics.GravityMode + 2;

                if (searchMode > 4)
                    searchMode -= 4;
            }
            else
            {
                searchMode = physics.WallMode + 2;

                if (searchMode > 4)
                    searchMode -= 4;
            }

            SensorLocationScan(SensorU1);
            if (ActiveChunk)
                d1 = FindDepth(searchMode, false);
            else
                d1 = -1;

            SensorLocationScan(SensorU2);
            if (ActiveChunk)
                d2 = FindDepth(searchMode, false);
            else
                d2 = -1;

            float rotAngle = 0;

            if (d1 <= 0 && d2 <= 0)
            {
            }
            else
            {
                float xDiff = (GroundX * 2) * ((float)Math.PI / 4);
                float yDiff = (d1 - d2);
                angle = (float)(Math.Atan2(yDiff, xDiff));

                int finalD;

                if (d1 > d2)
                    finalD = d1;
                else
                    finalD = d2;

                rotAngle = angle;
                
                Hugo.Position = new Vector2(Hugo.Position.X, (int)(Hugo.Position.Y + finalD));


                int wallmode = physics.WallMode;

                bool scanArea = false;

                if (rotAngle > 0.785398163f && rotAngle < 2.35619449f)
                {
                    physics.WallMode = 2;
                    scanArea = true;
                }
                else if (rotAngle < -0.785398163f && rotAngle > -2.35619449f)
                {
                    physics.WallMode = 4;
                    scanArea = true;
                }

                if (scanArea)
                {
                    UpdateSensorLocation();

                    searchMode = physics.WallMode;

                    SensorLocationScan(SensorD1);
                    if (ActiveChunk)
                        d1 = FindDepth(searchMode, false);

                    SensorLocationScan(SensorD2);
                    if (ActiveChunk)
                        d2 = FindDepth(searchMode, false);

                    if (d1 < 0 && d2 < 0)
                    {
                        physics.WallMode = wallmode;
                        UpdateSensorLocation();

                        rotAngle = 0;
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                    }
                    else
                    {
                        angle = rotAngle + ((float)Math.PI / 2);
                        Hugo.Rotation -= (Hugo.Rotation - angle) * 0.2f;
                        physics.Angle = angle;
                    }
                }
                else
                    physics.Velocity = new Vector2(physics.Velocity.X, 0);
            }


        }

        private void LeftSensors()
        {
            int searchMode;

            if (physics.WallMode == 0)
            {
                searchMode = physics.GravityMode - 1;

                if (searchMode < 1)
                    searchMode += 4;
            }
            else
            {
                searchMode = physics.WallMode - 1;

                if (searchMode < 1)
                    searchMode += 4;
            }

            SensorLocationScan(SensorL1);
            if (ActiveChunk)
                d1 = FindDepth(searchMode, true);
            else
                d1 = -1;

            SensorLocationScan(SensorL2);
            if (ActiveChunk)
                d2 = FindDepth(searchMode, true);
            else
                d2 = -1;

            int finalD = 0;

            if (d1 > d2)
                finalD = d1;
            else
                finalD = d2;

            if (finalD > 0)
                switch (physics.WallMode)
                {
                    case 0:
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X + finalD, Hugo.Position.Y);
                        break;

                    case 1:
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X + finalD, Hugo.Position.Y);
                        break;

                    case 2:
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X, Hugo.Position.Y + finalD);
                        break;

                    case 3:
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X - finalD, Hugo.Position.Y);
                        break;

                    case 4:
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X, Hugo.Position.Y - finalD);
                        break;
                }

            UpdateSensorLocation();
        }

        private void RightSensors()
        {
            int searchMode;

            if (physics.WallMode == 0)
            {
                searchMode = physics.GravityMode + 1;

                if (searchMode > 4)
                    searchMode -= 4;
            }
            else
            {
                searchMode = physics.WallMode + 1;

                if (searchMode > 4)
                    searchMode -= 4;
            }

            SensorLocationScan(SensorR1);
            if (ActiveChunk)
                d1 = FindDepth(searchMode, true);
            else
                d1 = -1;

            SensorLocationScan(SensorR2);
            if (ActiveChunk)
                d2 = FindDepth(searchMode, true);
            else
                d2 = -1;

            int finalD = 0;

            if (d1 > d2)
                finalD = d1;
            else
                finalD = d2;
            
            if (finalD > 0)
                switch (physics.WallMode)
                {
                    case 0:
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X - finalD, Hugo.Position.Y);
                        break;

                    case 1:
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X - finalD, Hugo.Position.Y);
                        break;

                    case 2:
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X, Hugo.Position.Y - finalD);
                        break;

                    case 3:
                        physics.Velocity = new Vector2(0, physics.Velocity.Y);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X + finalD, Hugo.Position.Y);
                        break;

                    case 4:
                        physics.Velocity = new Vector2(physics.Velocity.X, 0);
                        physics.GroundSpeed = 0;
                        Hugo.Position = new Vector2(Hugo.Position.X, Hugo.Position.Y + finalD);
                        break;
                }

            UpdateSensorLocation();
        }

        public void TestSensors()
        {
            if (physics.WallMode == 0)
                Hugo.Rotation -= (Hugo.Rotation - 0) * 0.1f;

            //if (Hugo.Position.Y > 1024 * 131)
            //{
            //    Hugo.Position = new Vector2(1000, 0);
            //    physics.Velocity = Vector2.Zero;
            //}

            if (physics.GroundSpeed < 0)
                LeftSensors();
            else if (physics.GroundSpeed > 0)
                RightSensors();
               


            //SideSensors();
            

            if (physics.Velocity.Y < 0 && physics.WallMode == 0)
                CeilingSensors();
            
            GroundSensors();
                

            
            //LevelLoader.level.CalculateLocation(new Vector2(TestSensorU.X, TestSensorU.Y));

            //chunk1 = LevelLoader.level.SelectedChunk;

            //if (chunk1 != -1)
            //{
            //    tile1 = LevelLoader.level.SelectedTile;
            //    chunk1Pos = LevelLoader.level.SelectedChunkPosition;
            //    tile1Pos = LevelLoader.level.SelectedTilePosition;
            //    transparent1 = LevelLoader.level.SelectedTileAir;

            //    Depth1 = SearchAir(new Vector2(TestSensorU.X, TestSensorU.Y), 3);
            //}
            //else
            //{
            //    transparent1 = true;
            //    Depth1 = 0;
            //}

            //Hugo.Position = new Vector2(Hugo.Position.X, (int)(Hugo.Position.Y + (Depth1)));
            //if (Depth1 > 0 && physics.Velocity.Y < 0)
            //    physics.Velocity = new Vector2(physics.Velocity.X, 0);



        }

        private void SensorLocationScan(Vector2 sensor)
        {
            SensorX = (int)sensor.X - (int)levelPosition.X;
            SensorY = (int)sensor.Y - (int)levelPosition.Y;

            if (SensorX >= levelWidth || SensorX < 0 || SensorY >= levelHeight || SensorY < 0)
            {
                ActiveChunk = false;
            }
            else
            {
                ActiveChunk = true;

                chunkY = (int)SensorY / chunkSize;
                chunkX = (int)SensorX / chunkSize;

                tileX = ((int)SensorX - (chunkSize * chunkX)) / tileSize;
                tileY = ((int)SensorY - (chunkSize * chunkY)) / tileSize;

                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                pixelX = ((int)SensorX - (chunkSize * chunkX) - (tileSize * tileX));
                pixelY = ((int)SensorY - (chunkSize * chunkY) - (tileSize * tileY));
            }
        }
        
        private int FindDepth(int SearchMode, bool SolidOnly)
        {
            int depth = 0;
            bool searching = true;
            short searchDirection;
            int pixelAlpha;

            //Searching Up

            if (TileType == 0)
                searchDirection = 0;
            else if (TileType == 1)
                searchDirection = 1;
            else
            {
                pixelAlpha = 0;

                if (pixelAlpha > 128)
                    searchDirection = 1;
                else
                    searchDirection = 0;
            }


            switch (SearchMode)
            {
                #region Search Down

                case 1:
                    while (searching)
                    {
                        if (searchDirection == 0)
                        {
                            switch (TileType)
                            {
                                case 0:
                                    depth -= (tileSize - pixelY);
                                    tileY++;
                                    pixelY = 0;

                                    if (tileY == chunkSize / tileSize)
                                    {
                                        tileY = 0;
                                        chunkY++;

                                        if (chunkY == worldChunksY)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;



                                case 1:
                                    searching = false;

                                    break;



                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha <= 128)
                                    {
                                        pixelY++;
                                        depth--;

                                        if (pixelY == tileSize)
                                        {
                                            pixelY = 0;
                                            tileY++;

                                            if (tileY == chunkSize / tileSize)
                                            {
                                                tileY = 0;
                                                chunkY++;

                                                if (chunkY == worldChunksY)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (TileType)
                            {
                                case 0:
                                    searching = false;
                                    break;



                                case 1:
                                    depth += (pixelY + 1);
                                    tileY--;
                                    pixelY = (tileSize - 1);

                                    if (tileY == -1)
                                    {
                                        tileY += (chunkSize / tileSize);
                                        chunkY--;

                                        if (chunkY == -1)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;



                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha > 128)
                                    {
                                        pixelY--;
                                        depth++;

                                        if (pixelY == -1)
                                        {
                                            pixelY = (tileSize - 1);
                                            tileY--;

                                            if (tileY == -1)
                                            {
                                                tileY += (chunkSize / tileSize);
                                                chunkY--;

                                                if (chunkY == -1)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }

                        if (Math.Abs(depth) >= 128)
                            searching = false;

                        if (depth < 0 && SolidOnly)
                            searching = false;
                    }

                    break;

                #endregion

                #region Search Right

                case 2:
                    while (searching)
                    {
                        if (searchDirection == 0)
                        {
                            switch (TileType)
                            {
                                case 0:
                                    depth -= (tileSize - pixelX);
                                    tileX++;
                                    pixelX = 0;

                                    if (tileX == chunkSize / tileSize)
                                    {
                                        tileX = 0;
                                        chunkX++;

                                        if (chunkX == worldChunksX)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;



                                case 1:
                                    searching = false;

                                    break;



                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha <= 128)
                                    {
                                        pixelX++;
                                        depth--;

                                        if (pixelX == tileSize)
                                        {
                                            pixelX = 0;
                                            tileX++;

                                            if (tileX == chunkSize / tileSize)
                                            {
                                                tileX = 0;
                                                chunkX++;

                                                if (chunkX == worldChunksY)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (TileType)
                            {
                                case 0:
                                    searching = false;
                                    break;



                                case 1:
                                    depth += (pixelX + 1);
                                    tileX--;
                                    pixelX = (tileSize - 1);

                                    if (tileX == -1)
                                    {
                                        tileX += (chunkSize / tileSize);
                                        chunkX--;

                                        if (chunkX == -1)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;



                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha > 128)
                                    {
                                        pixelX--;
                                        depth++;

                                        if (pixelX == -1)
                                        {
                                            pixelX = (tileSize - 1);
                                            tileX--;

                                            if (tileX == -1)
                                            {
                                                tileX += (chunkSize / tileSize);
                                                chunkX--;

                                                if (chunkX == -1)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        if (Math.Abs(depth) >= 128)
                            searching = false;

                        if (depth < 0 && SolidOnly)
                            searching = false;
                    }

                    break;

                #endregion

                #region Search Up

                case 3:
                    while (searching)
                    {
                        if (searchDirection == 0)
                        {
                            switch (TileType)
                            {
                                case 0:
                                    depth -= (pixelY + 1);
                                    tileY--;
                                    pixelY = (tileSize - 1);

                                    if (tileY == -1)
                                    {
                                        tileY += (chunkSize / tileSize);
                                        chunkY--;

                                        if (chunkY == -1)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;

                                case 1:
                                    searching = false;
                                    break;


                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha <= 128)
                                    {
                                        pixelY--;
                                        depth--;

                                        if (pixelY == -1)
                                        {
                                            pixelY = (tileSize - 1);
                                            tileY--;

                                            if (tileY == -1)
                                            {
                                                tileY += (chunkSize / tileSize);
                                                chunkY--;

                                                if (chunkY == -1)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (TileType)
                            {
                                case 0:
                                    searching = false;

                                    break;

                                case 1:
                                    depth += (tileSize - pixelY);
                                    tileY++;
                                    pixelY = 0;

                                    if (tileY == chunkSize / tileSize)
                                    {
                                        tileY = 0;
                                        chunkY++;

                                        if (chunkY == worldChunksY)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;

                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha > 128)
                                    {
                                        pixelY++;
                                        depth++;

                                        if (pixelY == tileSize)
                                        {
                                            pixelY = 0;
                                            tileY++;

                                            if (tileY == chunkSize / tileSize)
                                            {
                                                tileY = 0;
                                                chunkY++;

                                                if (chunkY == worldChunksY)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        if (Math.Abs(depth) >= 128)
                            searching = false;

                        if (depth < 0 && SolidOnly)
                            searching = false;
                    }

                    break;

                #endregion

                #region Search Left

                case 4:
                    while (searching)
                    {
                        if (searchDirection == 0)
                        {
                            switch (TileType)
                            {
                                case 0:
                                    depth -= (pixelX + 1);
                                    tileX--;
                                    pixelX = (tileSize - 1);

                                    if (tileX == -1)
                                    {
                                        tileX += (chunkSize / tileSize);
                                        chunkX--;

                                        if (chunkX == -1)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;

                                case 1:
                                    searching = false;
                                    break;


                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha <= 128)
                                    {
                                        pixelX--;
                                        depth--;

                                        if (pixelX == -1)
                                        {
                                            pixelX = (tileSize - 1);
                                            tileX--;

                                            if (tileX == -1)
                                            {
                                                tileX += (chunkSize / tileSize);
                                                chunkX--;

                                                if (chunkX == -1)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (TileType)
                            {
                                case 0:
                                    searching = false;

                                    break;

                                case 1:
                                    depth += (tileSize - pixelX);
                                    tileX++;
                                    pixelX = 0;

                                    if (tileX == chunkSize / tileSize)
                                    {
                                        tileX = 0;
                                        chunkX++;

                                        if (chunkX == worldChunksX)
                                        {
                                            ActiveChunk = false;
                                            searching = false;
                                        }
                                    }

                                    //if (ActiveChunk)
                                        //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;

                                    break;

                                case 2:
                                    pixelAlpha = 0;

                                    if (pixelAlpha > 128)
                                    {
                                        pixelX++;
                                        depth++;

                                        if (pixelX == tileSize)
                                        {
                                            pixelX = 0;
                                            tileX++;

                                            if (tileX == chunkSize / tileSize)
                                            {
                                                tileX = 0;
                                                chunkX++;

                                                if (chunkX == worldChunksX)
                                                {
                                                    ActiveChunk = false;
                                                    searching = false;
                                                }
                                            }

                                            //if (ActiveChunk)
                                                //TileType = LevelLoader.level.chunks[chunkX][chunkY].tiles[tileX][tileY].TileType;
                                        }
                                    }
                                    else
                                    {
                                        searching = false;
                                    }

                                    break;
                            }
                        }
                        if (Math.Abs(depth) >= 128)
                            searching = false;

                        if (depth < 0 && SolidOnly)
                            searching = false;
                    }

                    break;

                #endregion
            }

            if (searchDirection == 0)
            {
                depth++;
            }
        
        
        
            return depth;
        }       

        public void Draw(SpriteBatch spriteBatch)
        {
            //Hugo.Draw(spriteBatch);

            SpineLoader.RenderDraw(skeleton);


            //float scale = 2f;

            //spriteBatch.Draw(Detector, SensorD1, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Detector, SensorD2, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);
            
            //spriteBatch.Draw(Detector, SensorR1, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Detector, SensorR2, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);
            
            //spriteBatch.Draw(Detector, SensorU1, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Detector, SensorU2, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);

            //spriteBatch.Draw(Detector, SensorL1, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Detector, SensorL2, null, Color.White, 0f, new Vector2(0.5f, 0.5f), scale, SpriteEffects.None, 0f);

        }

        
    }
}
