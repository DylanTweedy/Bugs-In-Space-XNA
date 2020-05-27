//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Spine;

//namespace DevStomp
//{
//    class Life2
//    {
//        public Vector2 Position;
//        public Vector2 PreviousPosition;
//        public Vector2 Velocity;
//        Vector2 Acceleration;
//        float Rotation;

//        public byte CS;
//        Random rand;

//        public float health;
//        float Power;
//        float PPower;
//        public Rectangle rect;

//        Skeleton skeleton;
//        AnimationState state;

//        int AnimationState;
//        float MaxVelocity;

//        bool CanJump;
//        float Aim;
//        Color Color0;
//        Color Color1;
//        Color Color2;
//        Color Color3;
//        Color Color4;

//        int Deaths;

//        bool JetPackReady;
//        float JetPackTimer;
//        float BulletSpeed;
//        public int Kills;

//        public List<int> CollisionModifier;
//        public List<int> Actions;

//        public bool DoubleJump;

//        Inventory inv;
//        float WorldAngle;
//        Vector2 ActualVelocity;
//        float VelocityAngle;
//        float JumpTimer;

//        float LandAngle;

//        public void Initialize(byte controlScheme, Random Rand)
//        {
//            rand = Rand;

//            //float WorldRadius = (Math.Max(WorldManager.worlds.WorldX, WorldManager.worlds.WorldY) * WorldManager.worlds.ChunkSize * WorldManager.worlds.QuadSize) / 2;

//            //Position = new Vector2(0, -10 - ((WorldManager.worlds.WorldY * WorldManager.worlds.ChunkSize * WorldManager.worlds.QuadSize) / 2));
//            Velocity = Vector2.Zero;
//            Acceleration = Vector2.Zero;
//            Rotation = 0f;
//            CS = controlScheme;
//            MaxVelocity = 1500f;
//            AnimationState = -1;
//            CanJump = true;
//            Aim = 0f;
//            health = 1f;
//            Power = 1f;
//            JetPackReady = true;
//            DoubleJump = false;
//            JetPackTimer = 0f;
//            BulletSpeed = 10f + ((float)rand.NextDouble() * 40f);
//            rect = new Rectangle(0, 0, 60, 100);
//            Particles.AddParticles(Position - new Vector2(0, 50), 100, 1, 1.5f);
//            Particles.AddParticles(Position - new Vector2(30, 100), 100, 1, 1.5f);
//            Particles.AddParticles(Position, 100, 1, 1.5f);
//            Particles.AddParticles(Position - new Vector2(30, 50), 100, 1, 1.5f);
//            Particles.AddParticles(Position - new Vector2(-30, 50), 100, 1, 1.5f);
//            Actions = new List<int>();
//            CollisionModifier = new List<int>();

//            inv = new Inventory();
//            inv.Initialize();

//            int count = rand.Next(0, 10);
//            for (int i = 0; i < count; i++)
//            {
//                bool found = true;
//                while (true)
//                {
//                    int a = rand.Next(0, Projectiles.Actions);

//                    for (int o = 0; o < Actions.Count; o++)
//                    {
//                        if (Actions[o] == a)
//                        {
//                            found = false;
//                            break;
//                        }
//                        else
//                            found = true;
//                    }

//                    if (found)
//                    {
//                        Actions.Add(a);
//                        break;
//                    }
//                }
//            }         
            
//            count = rand.Next(0, Projectiles.CollisionModifier + 1);
//            for (int i = 0; i < count; i++)            
//                CollisionModifier.Add(rand.Next(0, Projectiles.CollisionModifier));
            
//            ActionTests();
//            LoadSkeleton();

//            PreviousPosition = Position;
//        }
                
//        private void ActionTests()
//        {
//            //Actions.Clear();
//            //CollisionModifier.Clear();
//            //Actions.Add(28);

//            //bool found = false;
//            //for (int o = 0; o < Actions.Count; o++)
//            //{
//            //    if (Actions[o] == 32)
//            //        found = true;
//            //}
//            //if (!found)
//            //    Actions.Add(32);
//        }

//        private void LoadSkeleton()
//        {
//            String name = "skeleton2"; // "goblins";
//            //if (WorldVariables.RandomNumber.Next(0, 2) == 0)
//            //    name = "skeleton2";
//            //else
//            name = "skeleton";

//            skeleton = SpineLoader.LoadSkeleton(name);
//            //skeleton.SetSkin("goblin");
//            //skeleton.SetSlotsToSetupPose();

//            // Define mixing between animations.
//            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
//            if (name == "spineboy")
//            {
//                stateData.SetMix("walk", "jump", 0.2f);
//                stateData.SetMix("jump", "walk", 0.4f);
//            }


//            //if (true)
//            //{
//            //    // Event handling for all animations.
//            //    state.Start += Start;
//            //    state.End += End;
//            //    state.Complete += Complete;
//            //    state.Event += Event;

//            //    state.SetAnimation(0, "drawOrder", true);
//            //}
//            //else


//            //TrackEntry entry = state.AddAnimation(0, "jump", false, 0);
//            //entry.End += new EventHandler<StartEndArgs>(End); // Event handling for queued animations.
//            //state.AddAnimation(0, "Walk1", true, 0);

//            stateData.SetMix("Walk1", "Jump1", 0.5f);
//            stateData.SetMix("Jump1", "Falling1", 0.6f);
//            stateData.SetMix("Falling1", "Walk1", 0.7f);
//            stateData.SetMix("Walk1", "Idle1", 1f);
//            stateData.SetMix("Falling1", "Land1", 1f);
//            stateData.SetMix("Land1", "Idle1", 0.3f);

//            state = new AnimationState(stateData);
//            state.SetAnimation(0, "Walk1", true);

//            SetupColors();

//            float s = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 0.25f, 0.15f, false);
//            s = 0.2f;

//            skeleton.RootBone.ScaleX = s;
//            skeleton.RootBone.ScaleY = s;
            
//            skeleton.FindBone("Body").ScaleX = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 0.75f, 2f, false);
//            skeleton.FindBone("Body").ScaleY = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 0.75f, 2f, false);
//            skeleton.FindBone("Body").ScaleX = 1;
//            skeleton.FindBone("Body").ScaleY = 1;
//            skeleton.FindBone("Pelvis").ScaleX = skeleton.FindBone("Body").ScaleX;
//            skeleton.FindBone("Pelvis").ScaleY = skeleton.FindBone("Body").ScaleY;

//            skeleton.FindSlot("LHAttatchment1").Attachment = null;
//            skeleton.FindSlot("LHAttachment2").Attachment = null;
//            skeleton.FindSlot("RHAttatchment1").Attachment = null;
//            skeleton.FindSlot("RHAttachment2").Attachment = null;

//            skeleton.UpdateWorldTransform();
//        }

//        private void SetupColors()
//        {
//            List<Color> colors = new List<Color>();
//            float r;
//            float g;
//            float b;

//            colors.Add(Color.Black);
//            colors.Add(Color.White);
//            colors.Add(Color.Brown);
//            colors.Add(Color.Yellow);
//            colors.Add(Color.Orange);
//            colors.Add(Color.Red);
//            colors.Add(Color.Green);
//            colors.Add(Color.Purple);
//            colors.Add(Color.Blue);

//            colors = ColorManager.GetShades(colors, 0.75f);
//            int i = rand.Next(0, colors.Count);

//            r = (float)colors[i].R / 255f;
//            g = (float)colors[i].G / 255f;
//            b = (float)colors[i].B / 255f;
//            Color1 = colors[i];

//            skeleton.FindSlot("Beard").R = r;
//            skeleton.FindSlot("Beard").G = g;
//            skeleton.FindSlot("Beard").B = b;
//            skeleton.FindSlot("Moustache").R = r;
//            skeleton.FindSlot("Moustache").G = g;
//            skeleton.FindSlot("Moustache").B = b;
//            skeleton.FindSlot("LeftEyebrow").R = r;
//            skeleton.FindSlot("LeftEyebrow").G = g;
//            skeleton.FindSlot("LeftEyebrow").B = b;
//            skeleton.FindSlot("RightEyebrow").R = r;
//            skeleton.FindSlot("RightEyebrow").G = g;
//            skeleton.FindSlot("RightEyebrow").B = b;
//            skeleton.FindSlot("Hair").R = r;
//            skeleton.FindSlot("Hair").G = g;
//            skeleton.FindSlot("Hair").B = b;
            
//            colors = ColorManager.Triad(colors[i], true, 30);
//            r = (float)colors[2].R / 255f;
//            g = (float)colors[2].G / 255f;
//            b = (float)colors[2].B / 255f;
//            Color2 = colors[2];

//            skeleton.FindSlot("Pelvis").R = r;
//            skeleton.FindSlot("Pelvis").G = g;
//            skeleton.FindSlot("Pelvis").B = b;
//            skeleton.FindSlot("LeftUpLeg").R = r;
//            skeleton.FindSlot("LeftUpLeg").G = g;
//            skeleton.FindSlot("LeftUpLeg").B = b;
//            skeleton.FindSlot("LeftDownLeg").R = r;
//            skeleton.FindSlot("LeftDownLeg").G = g;
//            skeleton.FindSlot("LeftDownLeg").B = b;
//            skeleton.FindSlot("RightUpLeg").R = r;
//            skeleton.FindSlot("RightUpLeg").G = g;
//            skeleton.FindSlot("RightUpLeg").B = b;
//            skeleton.FindSlot("RightDownLeg").R = r;
//            skeleton.FindSlot("RightDownLeg").G = g;
//            skeleton.FindSlot("RightDownLeg").B = b;

//            r = (float)colors[3].R / 255f;
//            g = (float)colors[3].G / 255f;
//            b = (float)colors[3].B / 255f;
//            Color3 = colors[3];

//            skeleton.FindSlot("Spine").R = r;
//            skeleton.FindSlot("Spine").G = g;
//            skeleton.FindSlot("Spine").B = b;
//            skeleton.FindSlot("LeftUpArm").R = r;
//            skeleton.FindSlot("LeftUpArm").G = g;
//            skeleton.FindSlot("LeftUpArm").B = b;
//            skeleton.FindSlot("LeftDownArm").R = r;
//            skeleton.FindSlot("LeftDownArm").G = g;
//            skeleton.FindSlot("LeftDownArm").B = b;
//            skeleton.FindSlot("RightUpArm").R = r;
//            skeleton.FindSlot("RightUpArm").G = g;
//            skeleton.FindSlot("RightUpArm").B = b;
//            skeleton.FindSlot("RightDownArm").R = r;
//            skeleton.FindSlot("RightDownArm").G = g;
//            skeleton.FindSlot("RightDownArm").B = b;

//            r = (float)colors[1].R / 255f;
//            g = (float)colors[1].G / 255f;
//            b = (float)colors[1].B / 255f;
//            Color0 = colors[1];

//            skeleton.FindSlot("Iris").R = r;
//            skeleton.FindSlot("Iris").G = g;
//            skeleton.FindSlot("Iris").B = b;
//            skeleton.FindSlot("LHAttatchment1").R = r;
//            skeleton.FindSlot("LHAttatchment1").G = g;
//            skeleton.FindSlot("LHAttatchment1").B = b;
//            skeleton.FindSlot("RHAttatchment1").R = r;
//            skeleton.FindSlot("RHAttatchment1").G = g;
//            skeleton.FindSlot("RHAttatchment1").B = b;
            
//            if (rand.Next(0, 2) == 0)
//                skeleton.FindSlot("Beard").Attachment = null;
//            if (rand.Next(0, 2) == 0)
//                skeleton.FindSlot("Moustache").Attachment = null;
//            if (rand.Next(0, 2) == 0)
//                skeleton.FindSlot("Hair").Attachment = null;
//            if (rand.Next(0, 2) == 0)
//            {
//                skeleton.FindSlot("LeftEyebrow").Attachment = null;
//                skeleton.FindSlot("RightEyebrow").Attachment = null;
//            }


//            colors.Clear();
//            colors.Add(new Color(255, 206, 185));
//            colors.Add(new Color(98, 52, 32));
//            colors.Add(new Color(222, 175, 93));

//            colors = ColorManager.GetShades(colors, 0.9f);
//            i = rand.Next(0, colors.Count);

//            r = (float)colors[i].R / 255f;
//            g = (float)colors[i].G / 255f;
//            b = (float)colors[i].B / 255f;
//            Color4 = colors[i];

//            skeleton.FindSlot("Head").R = r;
//            skeleton.FindSlot("Head").G = g;
//            skeleton.FindSlot("Head").B = b;
//            skeleton.FindSlot("RightHand").R = r;
//            skeleton.FindSlot("RightHand").G = g;
//            skeleton.FindSlot("RightHand").B = b;
//            skeleton.FindSlot("LeftHand").R = r;
//            skeleton.FindSlot("LeftHand").G = g;
//            skeleton.FindSlot("LeftHand").B = b;
//            skeleton.FindSlot("RightFoot").R = r;
//            skeleton.FindSlot("RightFoot").G = g;
//            skeleton.FindSlot("RightFoot").B = b;
//            skeleton.FindSlot("LeftFoot").R = r;
//            skeleton.FindSlot("LeftFoot").G = g;
//            skeleton.FindSlot("LeftFoot").B = b;
//        }

//        public void Update()
//        {
//            //WorldAngle = (float)Math.Atan2(WorldManager.worlds.Offset.Y - Position.Y, WorldManager.worlds.Offset.X - Position.X);
//            WorldAngle -= (float)Math.PI / 2f;
//            WorldAngle = 0f;

//            PPower = Power;
//            PreviousPosition = Position;

//            Acceleration = Vector2.Zero;

//            switch (CS)
//            {
//                case 0:
//                case 1:
//                case 2:
//                case 3:
//                    Acceleration.X += (270f * InputManager.GP[CS].ThumbSticks.Left.X);

//                    if (InputManager.GP[CS].IsButtonDown(Buttons.LeftShoulder) && CanJump && InputManager.pGP[CS].IsButtonUp(Buttons.LeftShoulder))
//                    {
//                        Acceleration.Y -= 2000f; ;
//                        CanJump = false;
//                        DoubleJump = false;
//                    }
//                    if (InputManager.GP[CS].IsButtonUp(Buttons.LeftShoulder) && !CanJump && InputManager.pGP[CS].IsButtonDown(Buttons.LeftShoulder))
//                    {
//                        DoubleJump = true;
//                    }



//                    if (InputManager.GP[CS].IsButtonDown(Buttons.LeftShoulder) && !CanJump && DoubleJump && Power > 0 && JetPackReady)
//                    {
//                        Acceleration.Y -= 75f;
//                        Power -= (float)WorldVariables.FrameTime * UsefulMethods.FindBetween(Math.Abs(Velocity.Y), 20f, 0.5f, 0.5f, 0.001f, false);
//                        Particles.AddParticles(Position - new Vector2(10, 50), 5, 1, 0.4f);
//                    }


//                    if (InputManager.GP[CS].Triggers.Right > 0 && InputManager.pGP[CS].Triggers.Right == 0 && InputManager.GP[CS].IsButtonDown(Buttons.RightShoulder))
//                    {
//                        inv.RHWeapon = new Weapon();
//                    }

//                    if (InputManager.GP[CS].Triggers.Left > 0 && InputManager.pGP[CS].Triggers.Left == 0 && InputManager.GP[CS].IsButtonDown(Buttons.RightShoulder))
//                    {
//                        inv.LHWeapon = new Weapon();
//                    }

//                    if (Math.Abs(InputManager.GP[CS].ThumbSticks.Right.X) > 0.1f || Math.Abs(InputManager.GP[CS].ThumbSticks.Right.Y) > 0.1f)
//                    {
//                        Aim = (float)Math.Atan2(InputManager.GP[CS].ThumbSticks.Right.X, -InputManager.GP[CS].ThumbSticks.Right.Y);
//                        Aim *= 57.2957795f;
//                        Aim += 180f;
//                        Aim += Rotation * 57.2957795f;
//                        Aim -= WorldAngle * 57.2957795f;
                        
//                    }
//                    else Aim = -1f;


//                    if (InputManager.GP[CS].Triggers.Left > 0 && Aim != -1)
//                    {
//                        Projectiles.P.Add(new Projectiles.Projectile(
//                            Position + new Vector2(skeleton.FindBone("LeftHand").WorldX, skeleton.FindBone("LeftHand").WorldY),
//                            new Vector2(InputManager.GP[CS].ThumbSticks.Right.X, -InputManager.GP[CS].ThumbSticks.Right.Y) * BulletSpeed,
//                            0.0035f,
//                            new Vector2(1f, 1f),
//                            inv.LHWeapon.SizeType,
//                            inv.LHWeapon.SizeID,
//                            ColorManager.Triad(Color0, false, 150)[1],
//                            inv.LHWeapon.Laser,
//                            inv.ActionsLH,
//                            CollisionModifier,
//                            CS));

//                        if (inv.LHWeapon.Laser > 0)
//                            inv.LHWeapon.Laser += inv.LHWeapon.LaserSpeed;

//                        if (inv.LHWeapon.Laser > inv.LHWeapon.LaserMax)
//                            inv.LHWeapon.Laser = inv.LHWeapon.LaserMax;
                        
//                    }
//                    else if (inv.LHWeapon.Laser > 0)
//                        inv.LHWeapon.Laser = inv.LHWeapon.LaserSpeed;

//                    if (InputManager.GP[CS].Triggers.Right > 0 && Aim != -1)
//                    {
//                        Projectiles.P.Add(new Projectiles.Projectile(
//                            Position + new Vector2(skeleton.FindBone("RightHand").WorldX, skeleton.FindBone("RightHand").WorldY),
//                            new Vector2(InputManager.GP[CS].ThumbSticks.Right.X, -InputManager.GP[CS].ThumbSticks.Right.Y) * BulletSpeed,
//                            0.0035f,
//                            new Vector2(1f, 1f),
//                            inv.LHWeapon.SizeType,
//                            inv.LHWeapon.SizeID,
//                            ColorManager.Triad(Color0, false, 150)[2],
//                            inv.RHWeapon.Laser,
//                            inv.ActionsLH,
//                            CollisionModifier,
//                            CS));
//                        if (inv.RHWeapon.Laser > 0)
//                            inv.RHWeapon.Laser += inv.RHWeapon.LaserSpeed;

//                        if (inv.RHWeapon.Laser > inv.RHWeapon.LaserMax)
//                            inv.RHWeapon.Laser = inv.RHWeapon.LaserMax;
//                    }
//                    else if (inv.RHWeapon.Laser > 0)
//                        inv.RHWeapon.Laser = inv.RHWeapon.LaserSpeed;



//                    break;
//            }

//            if (PPower == Power)
//            Power += (float)WorldVariables.FrameTime * 0.15f;
//            if (Power > 1f)
//                Power = 1f;
//            if (Power <= 0f)
//            {
//                Power = 0f;
//                JetPackReady = false;
//            }

//            if (!JetPackReady)
//            {
//                JetPackTimer += (float)WorldVariables.FrameTime;
//                if (JetPackTimer > 5f)
//                {
//                    JetPackTimer = 0f;
//                    JetPackReady = true;
//                }
//            }


//            Gravity();
//            Physics();
            
//            Velocity += Acceleration;
            
//            float Distance = Vector2.Distance(Vector2.Zero, Velocity);
//            VelocityAngle = (float)Math.Atan2(Velocity.X, -Velocity.Y) + LandAngle;
//            ActualVelocity = new Vector2((float)Math.Sin(VelocityAngle), -(float)Math.Cos(VelocityAngle)) * Distance;
//            float test = Vector2.Distance(Vector2.Zero, ActualVelocity);
            
//            Position += (ActualVelocity * (float)WorldVariables.FrameTime);




//            Collision();
//            UpdateSkeleton();

//            rect.X = (int)Position.X - 30;
//            rect.Y = (int)Position.Y - 105;

//            if (health <= 0 || health != health)
//            {
//                Initialize(CS, rand);
//                Deaths++;
//            }

//            UpdateInventory();



//            //float bounds = WorldManager.worlds.WorldBoundry;

//            //if (Position.X >= bounds)            
//            //    Position.X -= bounds * 2;            
//            //else if (Position.X < -bounds)            
//            //    Position.X += bounds * 2;
//        }

//        private void UpdateInventory()
//        {
//            inv.LHWeapon.Position = new Vector2(skeleton.FindBone("LeftHand").WorldX, skeleton.FindBone("LeftHand").WorldY) + Position;
//            inv.RHWeapon.Position = new Vector2(skeleton.FindBone("RightHand").WorldX, skeleton.FindBone("RightHand").WorldY) + Position;

//            inv.head.Position = new Vector2(skeleton.FindSlot("Hair").Bone.WorldX, skeleton.FindSlot("Hair").Bone.WorldY) + Position;
            
//            //LHWeapon.Rotation = Aim / 57.2957795f;
//            //RHWeapon.Rotation = Aim / 57.2957795f;
//            if (skeleton.FlipX)
//            {
//                inv.LHWeapon.Rotation = (skeleton.FindBone("LeftHandAttachment").WorldRotation - 90) / 57.2957795f;
//                inv.RHWeapon.Rotation = (skeleton.FindBone("RightHandAttachment").WorldRotation - 90) / 57.2957795f;
//                inv.head.Rotation = (skeleton.FindBone("Head").WorldRotation - 90) / 57.2957795f;
//            }
//            else
//            {
//                inv.LHWeapon.Rotation = -(skeleton.FindBone("LeftHandAttachment").WorldRotation - 90) / 57.2957795f;
//                inv.RHWeapon.Rotation = -(skeleton.FindBone("RightHandAttachment").WorldRotation - 90) / 57.2957795f;
//                inv.head.Rotation = -(skeleton.FindBone("Head").WorldRotation - 90) / 57.2957795f;
//            }
//            inv.LHWeapon.Scale = 1f;
//            inv.RHWeapon.Scale = 1f;
//            inv.head.Scale = 1f;

//            inv.LHWeapon.Tint = Color1;
//            inv.RHWeapon.Tint = Color2;
//            inv.head.Tint = Color0;

//            inv.Update();
//        }

//        private void Collision()
//        {
//            Console.WriteLine("Velocity: " + Velocity);

//            Vector2 Pos = Position;
//            //if (Math.Abs(Pos.X) < 1f)
//            //    Pos.X = (float)Math.Round(Pos.X);
//            //if (Math.Abs(Pos.Y) < 1f)
//            //    Pos.Y = (float)Math.Round(Pos.Y);
            
//            //CollisionData data = WorldManager.worlds.GetCollisionData(Pos, PreviousPosition);


//            //if (data != null)
//            //{
//            //    Console.WriteLine("Angle: " + data.Angle);
//            //    Console.WriteLine("Distance: " + data.DistanceLeft);

//            //    Position = data.Position;
//            //    Rotation = data.Angle;
//            //    LandAngle = data.Angle;
//            //    Velocity.Y = 0;
//            //    CanJump = true;
//            //    JumpTimer = 0f;

//            //    if (data.DistanceLeft > 2f)
//            //    {
//            //        Vector2 SelectedChunk = (data.Position - WorldManager.worlds.Offset) / (WorldManager.worlds.QuadSize * WorldManager.worlds.ChunkSize);
//            //        SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
//            //        SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);

//            //        Vector2 ChunkPosition = ((data.Position - WorldManager.worlds.Offset) / WorldManager.worlds.QuadSize) - (SelectedChunk * WorldManager.worlds.ChunkSize);
//            //        ChunkPosition.X = (float)Math.Round(ChunkPosition.X);
//            //        ChunkPosition.Y = (float)Math.Round(ChunkPosition.Y);

//            //        //WorldManager.worlds.EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, -0.9f);
//            //    }
//            //}
//            //else
//            //{
//            //    JumpTimer += (float)WorldVariables.FrameTime;
//            //    LandAngle = 0f;
//            //    Velocity = ActualVelocity;

//            //    if (JumpTimer > 0.1f)
//            //    {
//            //        CanJump = false;
//            //    }
//            //}



//            //else
//            //    CanJump = false;

//        }

//        private void Gravity()
//        {       
//            //if (!CanJump)
                      
//            float WorldSize = WorldManager.worlds.WorldX * WorldManager.worlds.WorldY * WorldManager.worlds.QuadSize * WorldManager.worlds.ChunkSize;
                        
//            float r;

//            if (WorldManager.worlds.WorldType == 0)
//                r = Math.Abs(Position.Y - WorldManager.worlds.CenterOfMass.Y);
//            else
//                r = Vector2.Distance(WorldManager.worlds.Offset, Position);
            
//            Acceleration.Y += (WorldSize / r);
//        }
        
//        private void Physics()
//        {
//            //if (Math.Abs(Acceleration.X) < 0.0001f)
//            //{
//            //    Acceleration.X = 0;
//            //    Velocity.X *= 0.85f;

//            //    if (Math.Abs(Velocity.X) < 0.1f)
//            //        Velocity.X = 0;
//            //}
//            //else if (Math.Sign(Acceleration.X) != Math.Sign(Velocity.X))
//            //{
//            //    Velocity.X *= 0.85f;

//            //    if (Math.Abs(Velocity.X) < 0.1f)
//            //        Velocity.X = 0;
//            //}


//            //try using absaloute.


//            //if (Velocity.X > MaxVelocity)
//            //    Velocity.X = MaxVelocity;
//            //if (Velocity.X < -MaxVelocity)
//            //    Velocity.X = -MaxVelocity;

//            //if (Velocity.Y > MaxVelocity)
//            //    Velocity.Y = MaxVelocity;
//            //if (Velocity.Y < -MaxVelocity)
//            //    Velocity.Y = -MaxVelocity;


//            //if (Position.Y > 0)
//            //{
//            //    Position.Y = 0;
//            //    Velocity.Y = 0;
//            //    CanJump = true;
//            //}

//            //if (Position.X > (WorldVariables.Boundry / 2) - 24)
//            //{
//            //    Position.X = (WorldVariables.Boundry / 2) - 24;
//            //    Velocity.X = 0;
//            //}
//            //if (Position.X  < (-WorldVariables.Boundry / 2) + 24)
//            //{
//            //    Position.X = (-WorldVariables.Boundry / 2) + 24;
//            //    Velocity.X = 0;
//            //}

//        }

//        private void UpdateSkeleton()
//        {
//            //if (CanJump)
//            //    Velocity.X *= 0.9f;

//            skeleton.X = Position.X;
//            skeleton.Y = Position.Y;

//            //if (!CanJump)
//            //    Rotation = WorldAngle;
//            //else
//                //Rotation = VelocityAngle;

//            if (Velocity.X > 0)
//            {
//                skeleton.FlipX = true;
//                skeleton.RootBone.Rotation = Rotation * 57.2957795f;
//            }
//            else if (Velocity.X < 0)
//            {
//                skeleton.FlipX = false;
//                skeleton.RootBone.Rotation = -Rotation * 57.2957795f;
//            }

//            //skeleton.FindBone("Spine").Rotation += 1f;
//            //skeleton.FindBone("Pelvis").Rotation = 90f;
//            //skeleton.FindBone("Head").Rotation += 1f;
            
//            if (!CanJump)
//            {
//                if (AnimationState != 3)
//                {
//                    AnimationState = 3;
//                    state.SetAnimation(0, "Falling1", true);
//                }
//                state.Update((float)WorldVariables.FrameTime);
//            }
//            else if (Math.Abs(Velocity.X) > MaxVelocity * 0.95f)
//            {
//                if (AnimationState != 2)
//                {
//                    AnimationState = 2;
//                    state.SetAnimation(0, "Run1", true);
//                }
//                state.Update(2f * (float)WorldVariables.FrameTime * (Math.Abs(Velocity.X) / MaxVelocity));
//            }
//            else if (Math.Abs(Velocity.X) > 0.5f)
//            {
//                if (AnimationState != 1)
//                {
//                    AnimationState = 1;
//                    state.SetAnimation(0, "Walk1", true);
//                }
//                state.Update(5f * (float)WorldVariables.FrameTime * (Math.Abs(Velocity.X) / MaxVelocity));
//            }
//            else
//            {
//                if (AnimationState != 0)
//                {
//                    AnimationState = 0;
//                    state.SetAnimation(0, "Idle1", true);
//                }
//                state.Update(2f * (float)WorldVariables.FrameTime);
//            }            
//            state.Apply(skeleton);

//            if (Aim != -1f)
//            {
//                if (skeleton.FlipX)
//                    Aim *= -1;

//                skeleton.FindBone("LeftArmUp").Rotation = Aim;
//                skeleton.FindBone("RightArmUp").Rotation = Aim;
//                skeleton.FindBone("LeftArmDown").Rotation = 0f;
//                skeleton.FindBone("RightArmDown").Rotation = 0f;
//                skeleton.FindBone("RightHand").Rotation = 0f;
//                skeleton.FindBone("LeftHand").Rotation = 0f;
//                skeleton.FindBone("LeftHandAttachment").Rotation = 0f;
//                skeleton.FindBone("RightHandAttachment").Rotation = 0f;
//            }
//            else
//            {
//                skeleton.FindBone("LeftHandAttachment").Rotation = 260f;
//                skeleton.FindBone("RightHandAttachment").Rotation = 100f;
//            }


//            skeleton.UpdateWorldTransform();
//        }

//        public void DrawBack(SpriteBatch spriteBatch)
//        {
//            inv.DrawBack(spriteBatch);
//        }

//        public void Draw(SpriteBatch spriteBatch, byte cam)
//        {
//            SpineLoader.RenderDraw(skeleton);
//            CameraManager.Cams[cam].SetRotation(-WorldAngle);
//        }

//        public void DrawFront(SpriteBatch spriteBatch)
//        {
//            inv.DrawFront(spriteBatch);
//        }

//        public void DrawUI(SpriteBatch spriteBatch)
//        {
//            Texture2D m = StaticTests.Marker;
//            Vector2 pos = new Vector2(0, 40);
//            Rectangle rect = new Rectangle(0, 0, (int)(m.Width * health), (int)(m.Height));
//            Rectangle rect2 = new Rectangle(0, 0, (int)(m.Width * Power), (int)(m.Height));
            
//            switch (CS)
//            {
//                case 0:
//                    pos.X = WorldVariables.WindowWidth * 0.1f;
//                    break;
//                case 1:
//                    pos.X = WorldVariables.WindowWidth * 0.3f;
//                    break;
//                case 2:
//                    pos.X = WorldVariables.WindowWidth * 0.6f;
//                    break;
//                case 3:
//                    pos.X = WorldVariables.WindowWidth * 0.9f;
//                    break;
//            }

//            spriteBatch.Draw(StaticTests.Marker, pos + new Vector2(0, 30), null, ColorManager.Triad(Color0, false, 150)[1], 0f, new Vector2(m.Width / 2, m.Height / 2), new Vector2(5.5f, 2.75f), SpriteEffects.None, 1f);
//            spriteBatch.Draw(StaticTests.Marker, pos + new Vector2(0, 30), null, Color.Black, 0f, new Vector2(m.Width / 2, m.Height / 2), new Vector2(5.5f * 0.95f, 2.75f * 0.95f), SpriteEffects.None, 1f);
//            spriteBatch.Draw(StaticTests.Marker, pos, rect, Color.Red, 0f, new Vector2(m.Width / 2, m.Height / 2), new Vector2(5f, 0.5f), SpriteEffects.None, 1f);
//            spriteBatch.Draw(StaticTests.Marker, pos + new Vector2(0, 16), rect2, Color.Blue, 0f, new Vector2(m.Width / 2, m.Height / 2), new Vector2(5f, 0.5f), SpriteEffects.None, 1f);
//            spriteBatch.DrawString(StaticTests.font, "Kills: " + Kills, pos + new Vector2(-74, 26), Color.White);
//            spriteBatch.DrawString(StaticTests.font, "Deaths: " + Deaths, pos + new Vector2(-74, 46), Color.White);
//        }
//    }
//}
