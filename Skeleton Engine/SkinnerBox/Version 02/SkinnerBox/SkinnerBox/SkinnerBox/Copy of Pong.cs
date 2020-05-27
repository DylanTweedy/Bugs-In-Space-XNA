using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeletonEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkinnerBox
{

    class Pong
    {
        public class Paddle
        {
            public BigDecimal Score;
            public int Speed;
            public int Size;
            public int Thickness;

            public LocationEnum Side;
            public LocationEnum PreviousSide;
            public int SideWeight;

            public Vector2 Position;
            public Vector2 Velocity;
            public InputType Player;
            public float PaddleOffset;
            public Color Tint;
            public SkillCloud SkillTree;
            public bool SkillTreeOpen;
            public bool MainTree;

            public Profile Owner;
            public bool Child;

            public Paddle(InputType player, LocationEnum side, Profile owner, bool child)
            {
                Owner = owner;
                Child = child;

                Score = 0;
                Speed = 500;

                Side = side;
                PreviousSide = LocationEnum.None;


                Player = player;
                Position = Vector2.Zero;
                PaddleOffset = 8;
                Size = 64;
                Thickness = 16;
                Tint = Owner.PrimaryColor;
                SkillTreeOpen = false;

                if (player != InputType.None && player != InputType.AI)
                    InitializeSkillTree();
            }

            private void InitializeSkillTree()
            {
                SkillTree = new SkillCloud();

                SkillTree.AddSkill(null, "Pong", null, true);

                SkillTree.UpdateCamera(new Rectangle(0, 0, 100, 100));
            }

            public Rectangle GetRectangle()
            {
                Rectangle rect1 = Rectangle.Empty;

                switch (Side)
                {
                    case LocationEnum.Left:
                    case LocationEnum.Right:
                        rect1 = new Rectangle((int)(Position.X - (Thickness / 2f)), (int)(Position.Y - (Size / 2f)),
                            Thickness, Size);
                        break;

                    case LocationEnum.Top:
                    case LocationEnum.Bottom:
                        rect1 = new Rectangle((int)(Position.X - (Size / 2f)), (int)(Position.Y - (Thickness / 2f)),
                            Size, Thickness);
                        break;
                }

                return rect1;
            }
        }

        public class Ball
        {
            public Particle P;
            public BigDecimal Value;
            public Profile Owner;
            public List<string> Types = new List<string>();

            public Ball(BigDecimal value)
            {
                P = new Particle();
                Value = value;
            }

            public Ball(Ball clone)
            {
                P = new Particle(clone.P);
                Value = clone.Value;
                Owner = clone.Owner;
                
                for (int i = 0; i < clone.Types.Count; i++)
                    Types.Add(clone.Types[i]);
            }
        }

        public class PongObject
        {
            public Texture2D Tex;
            public Vector2 Position;
            public float Timer;
            public float Duration;
            public float Frequency;
            public float FullFrequency;
            public string ObjectType;
            public string MainType;
            public bool Active;
            public float Alpha;
            public float Rotation;
            public float RotationalVelocity;
            public float Scale;

            public float BaseResource;
            public float Efficiency;


            public List<Ball> CollectedBalls = new List<Ball>();
            public float BlackHoleMultiplier;
            public float PortalFrequency;
            public float PortalTimer;
            public float PortalMultiplier;

            public PongObject(Texture2D tex, Vector2 position, string objectType)
            {
                Tex = tex;
                Position = position;
                ObjectType = objectType;
                MainType = ObjectType;
                Alpha = 0f;
                Rotation = 0f;
                Scale = 1f;
            }
        }

        #region Variables

        #region Location Variables
        public enum LocationEnum
        {
            Left,
            Top,
            Right,
            Bottom,
            None
        }

        public class Location
        {
            public float Left;
            public float Top;
            public float Right;
            public float Bottom;

            public Location(float left, float top, float right, float bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public LocationEnum GetMax()
            {
                LocationEnum Max = LocationEnum.None;
                float MaxNum = float.MinValue;

                if (Left > MaxNum)
                {
                    Max = LocationEnum.Left;
                    MaxNum = Left;
                }
                if (Top > MaxNum)
                {
                    Max = LocationEnum.Top;
                    MaxNum = Top;
                }
                if (Right > MaxNum)
                {
                    Max = LocationEnum.Right;
                    MaxNum = Right;
                }
                if (Bottom > MaxNum)
                {
                    Max = LocationEnum.Bottom;
                    MaxNum = Bottom;
                }

                return Max;
            }

            public LocationEnum GetMin()
            {
                LocationEnum Min = LocationEnum.None;
                float MinNum = float.MaxValue;

                if (Left < MinNum)
                {
                    Min = LocationEnum.Left;
                    MinNum = Left;
                }
                if (Top < MinNum)
                {
                    Min = LocationEnum.Top;
                    MinNum = Top;
                }
                if (Right < MinNum)
                {
                    Min = LocationEnum.Right;
                    MinNum = Right;
                }
                if (Bottom < MinNum)
                {
                    Min = LocationEnum.Bottom;
                    MinNum = Bottom;
                }

                return Min;
            }

            public float GetMaxNum()
            {
                float MaxNum = Left;

                if (Top > MaxNum)
                    MaxNum = Top;

                if (Right > MaxNum)
                    MaxNum = Right;

                if (Bottom > MaxNum)
                    MaxNum = Bottom;

                return MaxNum;
            }
        }
        #endregion

        List<Ball> Balls = new List<Ball>();
        List<Paddle> Paddles = new List<Paddle>();
        List<PongObject> Objects = new List<PongObject>();
        Location BorderSize;
        Location PlayersLocation;

        float PaddleOffset = 8f;
        Random rand;
        int HumanPlayers;
        BigDecimal TotalScore;

        SkillCloud SkillTree;
        bool MainTreeOpen;

        float BallTimer;
        BigDecimal PixelsPerSecond = 0;

        #endregion

        public void Initialize()
        {
            BorderSize = new Location(8, 8, 8, 8);
            PlayersLocation = new Location(0, 0, 0, 0);
            rand = new Random();

            InitializeTree();
        }

        public void InitializeTree()
        {
            SkillTree = new SkillCloud();

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Bonus Ball", null, false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 100, 18, 1.15f), "Vortex", "Bonus Ball", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vortex Efficiency", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vortex Base Resource", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vortex Duration", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 20, 1.15f), "Vortex Frequency", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 1", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 100, 9, 1.15f), "Black Hole", "Special Ball 1", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Black Hole Base Resource", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Black Hole Efficiency", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 99, 1.15f), "Black Hole Value Increase", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Black Hole Duration", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 2", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 100, 3, 1.15f), "Portal", "Special Ball 2", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Base Resource", "Portal", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Efficiency", "Portal", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Extra Ball", "Portal", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Duration", "Portal", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Delay", "Portal", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Turret", "Bonus Ball", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Turret Fire Rate", "Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Turret Frequency", "Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Turret Base Resource", "Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Turret Duration", "Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Turret Efficiency", "Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 3", "Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vacuum Turret", "Special Ball 3", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vacuum Turret Multiplier", "Vacuum Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vacuum Turret Base Resource", "Vacuum Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vacuum Turret Efficiency", "Vacuum Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vacuum Turret Fire Rate", "Vacuum Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 4", "Vacuum Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Gold Turret", "Special Ball 4", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Gold Turret Fire Rate", "Gold Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Gold Turret Base Resource", "Gold Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Gold Turret Ball Value", "Gold Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Gold Turret Efficiency", "Gold Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 5", "Gold Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ultra Turret", "Special Ball 5", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ultra Turret Fire Rate", "Ultra Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ultra Turret Efficiency", "Ultra Turret", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ultra Turret Base Resource", "Ultra Turret", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 6", "Bonus Ball", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups", "Special Ball 6", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Special Effect", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Freeze/Split", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Size Change", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Speed Change", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Damage/Heal", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Frequency", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 7", "Ball-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Area", "Special Ball 7", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Area Radius", "Ball-Ups Area", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Ball-Ups Area Duration", "Ball-Ups Area", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups", "Special Ball 6", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Speed Change", "Paddle-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Size Change", "Paddle-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Sticky", "Paddle-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Gravity", "Paddle-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 8", "Paddle-Ups", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Area", "Special Ball 8", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Area Radius", "Paddle-Ups Area", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Paddle-Ups Area Duration", "Paddle-Ups Area", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 9", "Bonus Ball", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Border", "Special Ball 9", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Border Frequency", "Border", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Border Duration", "Border", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Border Efficiency", "Border", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Border Base Resource", "Border", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Double Border", "Border", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Bumper", "Special Ball 9", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Bumper Base Resource", "Bumper", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Bumper Frequency", "Bumper", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Bumper Efficiency", "Bumper", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Bumper Duration", "Bumper", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Wall", "Special Ball 9", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Wall Frequency", "Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Wall Efficiency", "Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Wall Base Resource", "Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Wall Health", "Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 10", "Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rotating Wall", "Special Ball 10", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rotating Wall Base Resource", "Rotating Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rotating Wall Efficiency", "Rotating Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rotating Wall Multiplier", "Rotating Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 11", "Rotating Wall", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rubik Box", "Special Ball 11", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rubik Box Efficiency", "Rubik Box", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rubik Box Base Resource", "Rubik Box", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Rubik Box Multiplier", "Rubik Box", false);

            SkillTree.UpdateCamera(new Rectangle(0, 0, 100, 100));
        }

        public void Update()
        {
            UpdatePixelsPerSecond();

            BorderSize = new Location(0, 8, 0, 8);
            BorderSize = new Location(0, 0, 0, 0);

            UpdatePlayers();
            UpdateBalls();
            UpdateObjects();
            UpdatePlayerTree();
            UpdateMainTree();
            UpdateMainTreeRequirements();


            TotalScore += PixelsPerSecond * GlobalVariables.WorldTime;
            //TotalScore.Truncate();
        }

        #region Players

        private void UpdatePlayers()
        {
            AddPlayers();
            UpdateControls();
            UpdatePaddleSides();
            
            HumanPlayers = 0;
            for (int i = 0; i < Paddles.Count; i++)
            {
                Paddles[i].Tint = Paddles[i].Owner.PrimaryColor;

                if (!Paddles[i].Child)
                    HumanPlayers++;
            }
        }

        private void UpdatePaddleSides()
        {
            for (int i = 0; i < Paddles.Count; i++)
            {
                if (Paddles[i].PreviousSide != Paddles[i].Side)
                {
                    for (int o = 0; o < Paddles.Count; o++)
                        if (o != i)
                        {
                            if (Paddles[i].PreviousSide == Paddles[o].Side && Paddles[o].SideWeight > Paddles[i].SideWeight)
                            {
                                Paddles[o].SideWeight--;
                            }
                        }

                    Paddles[i].PreviousSide = Paddles[i].Side;

                    Paddles[i].SideWeight = 0;

                    for (int o = 0; o < Paddles.Count; o++)
                        if (o != i)
                        {
                            if (Paddles[i].Side == Paddles[o].Side)
                            {
                                Paddles[o].SideWeight++;
                            }
                        }

                }
            }

            bool redistribute = false;

            for (int i = 0; i < Paddles.Count; i++)
                for (int o = 0; o < Paddles.Count; o++)
                    if (Paddles[i].Side == Paddles[o].Side)
                        if (Paddles[i].SideWeight == Paddles[o].SideWeight)
                        {
                            redistribute = true;
                            break;
                        }


            if (redistribute)
            {
                int left = 0;
                int right = 0;
                int top = 0;
                int bottom = 0;

                for (int i = 0; i < Paddles.Count; i++)
                {

                    switch (Paddles[i].Side)
                    {
                        case LocationEnum.Left:
                            Paddles[i].SideWeight = left;
                            left++;
                            break;
                        case LocationEnum.Top:
                            Paddles[i].SideWeight = top;
                            top++;
                            break;
                        case LocationEnum.Right:
                            Paddles[i].SideWeight = right;
                            right++;
                            break;
                        case LocationEnum.Bottom:
                            Paddles[i].SideWeight = bottom;
                            bottom++;
                            break;
                    }
                }
            }

        }

        private void UpdateControls()
        {
            for (int i = 0; i < Paddles.Count; i++)
            {
                Paddles[i].Velocity = Vector2.Zero;

                if (!Paddles[i].SkillTreeOpen && !Paddles[i].MainTree)
                    switch (Paddles[i].Player)
                    {
                        #region Keyboard

                        case InputType.Keyboard:

                            #region Change Side

                            if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Left))
                            {
                                Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                                Paddles[i].Side = LocationEnum.Left;
                            }
                            if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Up))
                            {
                                Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                                Paddles[i].Side = LocationEnum.Top;
                            }
                            if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Right))
                            {
                                Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                                Paddles[i].Side = LocationEnum.Right;
                            }
                            if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Down))
                            {
                                Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                                Paddles[i].Side = LocationEnum.Bottom;
                            }

                            #endregion

                            #region Move Paddle

                            switch (Paddles[i].Side)
                            {
                                case LocationEnum.Left:
                                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.W))
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.S))
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.A))
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.D))
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    break;

                                case LocationEnum.Top:
                                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.W))
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.S))
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.A))
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.D))
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    break;

                                case LocationEnum.Right:
                                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.W))
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.S))
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.A))
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.D))
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    break;

                                case LocationEnum.Bottom:
                                    if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.W))
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.S))
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.A))
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.D))
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    break;
                            }

                            #endregion

                            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                                Paddles[i].SkillTreeOpen = true;

                            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                                Paddles[i].MainTree = true;

                            break;

                        #endregion

                        #region Gamepad


                        case InputType.GamepadOne:
                        case InputType.GamepadTwo:
                        case InputType.GamepadThree:
                        case InputType.GamepadFour:

                            #region Setup

                            int P = 0;

                            switch (Paddles[i].Player)
                            {
                                case InputType.GamepadOne:
                                    P = 0;
                                    break;
                                case InputType.GamepadTwo:
                                    P = 1;
                                    break;
                                case InputType.GamepadThree:
                                    P = 2;
                                    break;
                                case InputType.GamepadFour:
                                    P = 3;
                                    break;
                            }

                            #endregion

                            #region Change Side

                            if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.X))
                            {
                                Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                                Paddles[i].Side = LocationEnum.Left;
                            }
                            if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.Y))
                            {
                                Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                                Paddles[i].Side = LocationEnum.Top;
                            }
                            if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.B) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.B))
                            {
                                Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                                Paddles[i].Side = LocationEnum.Right;
                            }
                            if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.A) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.A))
                            {
                                Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                                Paddles[i].Side = LocationEnum.Bottom;
                            }

                            #endregion

                            #region Move Paddle

                            switch (Paddles[i].Side)
                            {
                                case LocationEnum.Left:
                                    if (InputManager.GP[P].DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    else
                                    {
                                        Paddles[i].Velocity.Y -= InputManager.GP[P].ThumbSticks.Left.Y * Paddles[i].Speed;
                                        Paddles[i].Velocity.Y += InputManager.GP[P].ThumbSticks.Left.X * Paddles[i].Speed;
                                    }
                                    break;

                                case LocationEnum.Top:
                                    if (InputManager.GP[P].DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    else
                                    {
                                        Paddles[i].Velocity.X -= InputManager.GP[P].ThumbSticks.Left.Y * Paddles[i].Speed;
                                        Paddles[i].Velocity.X += InputManager.GP[P].ThumbSticks.Left.X * Paddles[i].Speed;
                                    }
                                    break;

                                case LocationEnum.Right:
                                    if (InputManager.GP[P].DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.Y += Paddles[i].Speed;
                                    else
                                    {
                                        Paddles[i].Velocity.Y -= InputManager.GP[P].ThumbSticks.Left.Y * Paddles[i].Speed;
                                        Paddles[i].Velocity.Y += InputManager.GP[P].ThumbSticks.Left.X * Paddles[i].Speed;
                                    }
                                    break;

                                case LocationEnum.Bottom:
                                    if (InputManager.GP[P].DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X -= Paddles[i].Speed;
                                    else if (InputManager.GP[P].DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                        Paddles[i].Velocity.X += Paddles[i].Speed;
                                    else
                                    {
                                        Paddles[i].Velocity.X -= InputManager.GP[P].ThumbSticks.Left.Y * Paddles[i].Speed;
                                        Paddles[i].Velocity.X += InputManager.GP[P].ThumbSticks.Left.X * Paddles[i].Speed;
                                    }
                                    break;
                            }

                            #endregion

                            if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.RightShoulder))
                                Paddles[i].SkillTreeOpen = true;
                            if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.LeftShoulder))
                                Paddles[i].MainTree = true;

                            break;

                        #endregion

                        #region AI

                        case InputType.AI:
                            CountPlayers();

                            #region Find Side

                            LocationEnum max = PlayersLocation.GetMax();
                            float average = (PlayersLocation.Left + PlayersLocation.Right + PlayersLocation.Top + PlayersLocation.Bottom) / 4f;
                            float maxNum = PlayersLocation.GetMaxNum();

                            if (!(average - 1 < maxNum && average + 1 > maxNum))
                                if (max == Paddles[i].Side)
                                {
                                    bool choosing = true;

                                    while (choosing)
                                    {
                                        int select = rand.Next(0, 4);

                                        switch (select)
                                        {
                                            case 0:
                                                if (max != LocationEnum.Left)
                                                {
                                                    Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                                                    Paddles[i].Side = LocationEnum.Left;
                                                    choosing = false;
                                                }
                                                break;
                                            case 1:
                                                if (max != LocationEnum.Top)
                                                {
                                                    Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                                                    Paddles[i].Side = LocationEnum.Top;
                                                    choosing = false;
                                                }
                                                break;
                                            case 2:
                                                if (max != LocationEnum.Right)
                                                {
                                                    Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                                                    Paddles[i].Side = LocationEnum.Right;
                                                    choosing = false;
                                                }
                                                break;
                                            case 3:
                                                if (max != LocationEnum.Bottom)
                                                {
                                                    Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                                                    Paddles[i].Side = LocationEnum.Bottom;
                                                    choosing = false;
                                                }
                                                break;
                                        }
                                    }
                                }

                            #endregion

                            #region

                            //bool test = true;
                            //for (int b = 0; b < Balls.Count; b++)
                            //{
                            //    if (Balls[b] == Paddles[i].Tracking)
                            //    {
                            //        test = false;
                            //        break;
                            //    }
                            //}
                            //if (test)
                            //    Paddles[i].Tracking = Balls[rand.Next(0, Balls.Count)];

                            //if (Paddles[i].Tracking == null)
                            //    Paddles[i].Tracking = Balls[rand.Next(0, Balls.Count)];
                            //else if (Paddles[i].Tracking.Remove)
                            //    Paddles[i].Tracking = Balls[rand.Next(0, Balls.Count)];


                            //Location sides = new Location(0, 0, 0, 0);

                            //sides.Left = minimum_distance(Vector2.Zero, new Vector2(0, GraphicsManager.GameResolution.Y), Paddles[i].Tracking.P.Position);
                            //sides.Top = minimum_distance(Vector2.Zero, new Vector2(GraphicsManager.GameResolution.X, 0), Paddles[i].Tracking.P.Position);
                            //sides.Right = minimum_distance(new Vector2(GraphicsManager.GameResolution.X, 0), new Vector2(GraphicsManager.GameResolution.X, GraphicsManager.GameResolution.Y), Paddles[i].Tracking.P.Position);
                            //sides.Bottom = minimum_distance(new Vector2(0, GraphicsManager.GameResolution.Y), new Vector2(GraphicsManager.GameResolution.X, GraphicsManager.GameResolution.Y), Paddles[i].Tracking.P.Position);

                            //LocationEnum side = sides.GetMin();


                            //if (side != Paddles[i].Side)
                            //    switch (side)
                            //    {
                            //        case LocationEnum.Left:
                            //            Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                            //            Paddles[i].Side = LocationEnum.Left;

                            //            break;
                            //        case LocationEnum.Top:
                            //            Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                            //            Paddles[i].Side = LocationEnum.Top;

                            //            break;
                            //        case LocationEnum.Right:
                            //            Paddles[i].Position.Y = GraphicsManager.GameResolution.Y / 2f;
                            //            Paddles[i].Side = LocationEnum.Right;

                            //            break;
                            //        case LocationEnum.Bottom:
                            //            Paddles[i].Position.X = GraphicsManager.GameResolution.X / 2f;
                            //            Paddles[i].Side = LocationEnum.Bottom;
                            //            break;
                            //    }

                            //Vector2 dis = -(Paddles[i].Position - Paddles[i].Tracking.P.Position);
                            //dis.Normalize();

                            //Paddles[i].Velocity = Paddles[i].Speed * dis;

                            #endregion


                            int selected = -1;
                            float dist1 = float.MaxValue;

                            float OmaxX = GraphicsManager.GameResolution.X;
                            float OmaxY = GraphicsManager.GameResolution.Y;

                            float maxX = OmaxX;
                            float maxY = OmaxY;
                            float minX = 0f;
                            float minY = 0f;

                            float SideWeight = (Paddles[i].SideWeight + 1);


                            float maxPaddlesX = (float)Math.Floor(OmaxX / (Paddles[i].Size) - 1f);
                            float maxPaddlesY = (float)Math.Floor(OmaxY / (Paddles[i].Size) - 1f);

                            int layers = 0;
                            int topLayer = 0;


                            //float maxSideX = Paddles[i].Size * SideWeight;
                            //float maxSideY = Paddles[i].Size * SideWeight;

                            //if (SideWeight



                            switch (Paddles[i].Side)
                            {
                                case LocationEnum.Left:
                                    if (PlayersLocation.Left != 0)
                                    {
                                        if (PlayersLocation.Left > maxPaddlesY)
                                        {
                                            layers = (int)Math.Ceiling(PlayersLocation.Left / maxPaddlesY);

                                            for (int l = 0; l < layers; l++)
                                            {
                                                if (SideWeight > maxPaddlesY)
                                                {
                                                    SideWeight -= maxPaddlesY;
                                                    topLayer--;
                                                }
                                            }

                                            if (Paddles[i].SideWeight < PlayersLocation.Left - (maxPaddlesY * (layers - 1)))
                                                OmaxY /= PlayersLocation.Left - (maxPaddlesY * (layers - 1));
                                            else
                                                OmaxY /= maxPaddlesY;
                                        }
                                        else
                                            OmaxY /= PlayersLocation.Left;
                                    }

                                    minY = (OmaxY * SideWeight) - OmaxY;
                                    maxY = minY + OmaxY;
                                    break;

                                case LocationEnum.Top:
                                    if (PlayersLocation.Top != 0)
                                    {
                                        if (PlayersLocation.Top > maxPaddlesX)
                                        {
                                            layers = (int)Math.Ceiling(PlayersLocation.Top / maxPaddlesX);

                                            for (int l = 0; l < layers; l++)
                                            {
                                                if (SideWeight > maxPaddlesX)
                                                {
                                                    SideWeight -= maxPaddlesX;
                                                    topLayer--;
                                                }
                                            }

                                            if (Paddles[i].SideWeight < PlayersLocation.Top - (maxPaddlesX * (layers - 1)))
                                                OmaxX /= PlayersLocation.Top - (maxPaddlesX * (layers - 1));
                                            else
                                                OmaxX /= maxPaddlesX;
                                        }
                                        else
                                            OmaxX /= PlayersLocation.Top;
                                    }

                                    minX = (OmaxX * SideWeight) - OmaxX;
                                    maxX = minX + OmaxX;
                                    break;

                                case LocationEnum.Right:
                                    if (PlayersLocation.Right != 0)
                                    {
                                        if (PlayersLocation.Right > maxPaddlesY)
                                        {
                                            layers = (int)Math.Ceiling(PlayersLocation.Right / maxPaddlesY);

                                            for (int l = 0; l < layers; l++)
                                            {
                                                if (SideWeight > maxPaddlesY)
                                                {
                                                    SideWeight -= maxPaddlesY;
                                                    topLayer--;
                                                }
                                            }

                                            if (Paddles[i].SideWeight < PlayersLocation.Right - (maxPaddlesY * (layers - 1)))
                                                OmaxY /= PlayersLocation.Right - (maxPaddlesY * (layers - 1));
                                            else
                                                OmaxY /= maxPaddlesY;
                                        }
                                        else
                                            OmaxY /= PlayersLocation.Right;
                                    }

                                    minY = (OmaxY * SideWeight) - OmaxY;
                                    maxY = minY + OmaxY;
                                    break;

                                case LocationEnum.Bottom:
                                    if (PlayersLocation.Bottom != 0)
                                    {
                                        if (PlayersLocation.Bottom > maxPaddlesX)
                                        {
                                            layers = (int)Math.Ceiling(PlayersLocation.Bottom / maxPaddlesX);

                                            for (int l = 0; l < layers; l++)
                                            {
                                                if (SideWeight > maxPaddlesX)
                                                {
                                                    SideWeight -= maxPaddlesX;
                                                    topLayer--;
                                                }
                                            }

                                            if (Paddles[i].SideWeight < PlayersLocation.Bottom - (maxPaddlesX * (layers - 1)))
                                                OmaxX /= PlayersLocation.Bottom - (maxPaddlesX * (layers - 1));
                                            else
                                                OmaxX /= maxPaddlesX;
                                        }
                                        else
                                            OmaxX /= PlayersLocation.Bottom;
                                    }

                                    minX = (OmaxX * SideWeight) - OmaxX;
                                    maxX = minX + OmaxX;
                                    break;
                            }





                            for (int o = 0; o < Balls.Count; o++)
                            {
                                {
                                    float dist2 = Vector2.Distance(Paddles[i].Position, Balls[o].P.Position);

                                    if (dist1 > dist2)
                                    {
                                        dist1 = dist2;
                                        selected = o;
                                    }
                                }
                            }

                            if (selected != -1)
                            {
                                Vector2 pos = Balls[selected].P.Position;

                                switch (Paddles[i].Side)
                                {
                                    case LocationEnum.Left:
                                    case LocationEnum.Right:
                                        if (Balls[selected].P.Position.Y > maxY - (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.Position - new Vector2(0, Balls[selected].P.Position.Y - (maxY - (Paddles[i].Size / 2f)));
                                        else if (Balls[selected].P.Position.Y < minY + (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.Position - new Vector2(0, Balls[selected].P.Position.Y - (minY + (Paddles[i].Size / 2f)));
                                        break;

                                    case LocationEnum.Top:
                                    case LocationEnum.Bottom:
                                        if (Balls[selected].P.Position.X > maxX - (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.Position - new Vector2(Balls[selected].P.Position.X - (maxX - (Paddles[i].Size / 2f)), 0);
                                        else if (Balls[selected].P.Position.X < minX + (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.Position - new Vector2(Balls[selected].P.Position.X - (minX + (Paddles[i].Size / 2f)), 0);
                                        break;
                                }

                                Vector2 dis = -(Paddles[i].Position - pos);

                                dis.Normalize();

                                Paddles[i].Velocity = Paddles[i].Speed * dis;
                            }
                            else
                            {
                                Vector2 dis = Vector2.Zero;


                                switch (Paddles[i].Side)
                                {
                                    case LocationEnum.Left:
                                        dis = -(Paddles[i].Position - new Vector2(0, minY + (OmaxY / 2f)));
                                        break;

                                    case LocationEnum.Top:
                                        dis = -(Paddles[i].Position - new Vector2(minX + (OmaxX / 2f), 0));
                                        break;

                                    case LocationEnum.Right:
                                        dis = -(Paddles[i].Position - new Vector2(GraphicsManager.GameResolution.X, minY + (OmaxY / 2f)));
                                        break;

                                    case LocationEnum.Bottom:
                                        dis = -(Paddles[i].Position - new Vector2(minX + (OmaxX / 2f), GraphicsManager.GameResolution.Y));
                                        break;
                                }

                                if (dis != Vector2.Zero)
                                    dis.Normalize();

                                Paddles[i].Velocity = Paddles[i].Speed * dis;
                            }


                            #region
                            //int selected = 0;
                            //float dist1 = float.MaxValue;

                            //for (int o = 0; o < Balls.Count; o++)
                            //{
                            //    float dist2 = Vector2.Distance(Paddles[i].Position, Balls[o].P.Position);

                            //    if (dist1 > dist2)
                            //    {
                            //        dist1 = dist2;
                            //        selected = o;
                            //    }
                            //}

                            //Vector2 dis = -(Paddles[i].Position - Balls[selected].P.Position);
                            //dis.Normalize();

                            //Paddles[i].Velocity = Paddles[i].Speed * dis;

                            #endregion

                            //if (Balls.Count > i)
                            //{
                            //    Vector2 dis = -(Paddles[i].Position - Balls[i].P.GetTruePosition());
                            //    dis.Normalize();

                            //    Paddles[i].Velocity = Paddles[i].Speed * dis;
                            //}
                            //else if (Balls.Count != 0)
                            //{
                            //    int BallNum = i;

                            //    while (BallNum >= Balls.Count)
                            //        BallNum -= Balls.Count;

                            //    Vector2 dis = -(Paddles[i].Position - Balls[BallNum].GetTruePosition());
                            //    dis.Normalize();

                            //    Paddles[i].Velocity = Paddles[i].Speed * dis;
                            //}

                            break;


                        #endregion
                    }
                else if (Paddles[i].SkillTreeOpen)
                {
                    switch (Paddles[i].Player)
                    {
                        case InputType.Keyboard:
                            if (InputManager.KB.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                                Paddles[i].SkillTreeOpen = false;
                            break;


                        case InputType.GamepadOne:
                        case InputType.GamepadTwo:
                        case InputType.GamepadThree:
                        case InputType.GamepadFour:

                            #region Setup

                            int P = 0;

                            switch (Paddles[i].Player)
                            {
                                case InputType.GamepadOne:
                                    P = 0;
                                    break;
                                case InputType.GamepadTwo:
                                    P = 1;
                                    break;
                                case InputType.GamepadThree:
                                    P = 2;
                                    break;
                                case InputType.GamepadFour:
                                    P = 3;
                                    break;
                            }

                            #endregion

                            if (InputManager.GP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.RightShoulder))
                                Paddles[i].SkillTreeOpen = false;
                            break;
                    }
                }
                else if (Paddles[i].MainTree)
                {
                    switch (Paddles[i].Player)
                    {
                        case InputType.Keyboard:
                            if (InputManager.KB.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                                Paddles[i].MainTree = false;
                            break;


                        case InputType.GamepadOne:
                        case InputType.GamepadTwo:
                        case InputType.GamepadThree:
                        case InputType.GamepadFour:

                            #region Setup

                            int P = 0;

                            switch (Paddles[i].Player)
                            {
                                case InputType.GamepadOne:
                                    P = 0;
                                    break;
                                case InputType.GamepadTwo:
                                    P = 1;
                                    break;
                                case InputType.GamepadThree:
                                    P = 2;
                                    break;
                                case InputType.GamepadFour:
                                    P = 3;
                                    break;
                            }

                            #endregion

                            if (InputManager.GP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.LeftShoulder))
                                Paddles[i].MainTree = false;
                            break;
                    }
                }

                Paddles[i].Position += Paddles[i].Velocity * GlobalVariables.WorldTime;

                SetPaddleOffset(i);

                switch (Paddles[i].Side)
                {
                    case LocationEnum.Left:
                    case LocationEnum.Right:
                        if (Paddles[i].Position.Y - (Paddles[i].Size / 2f) - BorderSize.Top < 0)
                            Paddles[i].Position.Y = (Paddles[i].Size / 2f) + BorderSize.Top;
                        else if (Paddles[i].Position.Y + (Paddles[i].Size / 2f) > GraphicsManager.GameResolution.Y - BorderSize.Bottom)
                            Paddles[i].Position.Y = GraphicsManager.GameResolution.Y - (Paddles[i].Size / 2f) - BorderSize.Bottom;
                        break;

                    case LocationEnum.Top:
                    case LocationEnum.Bottom:
                        if (Paddles[i].Position.X - (Paddles[i].Size / 2f) - BorderSize.Left < 0)
                            Paddles[i].Position.X = (Paddles[i].Size / 2f) + BorderSize.Left;
                        else if (Paddles[i].Position.X + (Paddles[i].Size / 2f) > GraphicsManager.GameResolution.X - BorderSize.Right)
                            Paddles[i].Position.X = GraphicsManager.GameResolution.X - (Paddles[i].Size / 2f) - BorderSize.Right;
                        break;

                }
            }
        }



        private void CountPlayers()
        {
            PlayersLocation = new Location(0, 0, 0, 0);
            for (int i = 0; i < Paddles.Count; i++)
            {
                switch (Paddles[i].Side)
                {
                    case LocationEnum.Left:
                        PlayersLocation.Left++;
                        break;
                    case LocationEnum.Top:
                        PlayersLocation.Top++;
                        break;
                    case LocationEnum.Right:
                        PlayersLocation.Right++;
                        break;
                    case LocationEnum.Bottom:
                        PlayersLocation.Bottom++;
                        break;
                }
            }
        }

        private void AddPlayers()
        {
            int existingPlayer = -1;

            if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Back)))
            {
                LocationEnum Highest = PlayersLocation.GetMax();
                string name = "Player: " + (Paddles.Count + 1);
                Profiles.profiles.Add(name, new Profile(name, ColorManager.RandomColor()));
                Paddles.Add(new Paddle(InputType.AI, Highest, Profiles.profiles[name], false));
            }

            if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.Enter)))
            {
                existingPlayer = -1;

                for (int i = 0; i < Paddles.Count; i++)
                {
                    if (Paddles[i].Player == InputType.Keyboard)
                    {
                        existingPlayer = i;
                        break;
                    }
                }


                if (existingPlayer == -1)
                {
                    LocationEnum Highest = PlayersLocation.GetMax();
                    string name = "Player: " + (Paddles.Count + 1);
                    Profiles.profiles.Add(name, new Profile(name, ColorManager.RandomFullColor()));
                    Paddles.Add(new Paddle(InputType.Keyboard, Highest, Profiles.profiles[name], false));
                }
                else
                {
                    LocationEnum Highest = PlayersLocation.GetMax();
                    Paddles.Add(new Paddle(InputType.AI, Highest, Paddles[existingPlayer].Owner, true));
                }
            }

            for (int p = 0; p < InputManager.GP.Count; p++)
                if (InputManager.GP[p].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Start) && InputManager.pGP[p].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.Start))
                {
                    InputType player = InputType.GamepadOne;

                    switch (p)
                    {
                        case 0:
                            player = InputType.GamepadOne;
                            break;
                        case 1:
                            player = InputType.GamepadTwo;
                            break;
                        case 2:
                            player = InputType.GamepadThree;
                            break;
                        case 3:
                            player = InputType.GamepadFour;
                            break;
                    }


                    for (int i = 0; i < Paddles.Count; i++)
                    {
                        if (Paddles[i].Player == player)
                        {
                            existingPlayer = i;
                            break;
                        }
                    }

                    if (existingPlayer == -1)
                    {
                        LocationEnum Highest = PlayersLocation.GetMax();
                        string name = "Player: " + (Paddles.Count + 1);
                        Profiles.profiles.Add(name, new Profile(name, ColorManager.RandomFullColor()));
                        Paddles.Add(new Paddle(player, Highest, Profiles.profiles[name], false));
                    }
                    else
                    {
                        LocationEnum Highest = PlayersLocation.GetMax();
                        Paddles.Add(new Paddle(InputType.AI, Highest, Paddles[existingPlayer].Owner, true));
                    }
                }
        }
        
        #endregion

        #region Main Tree

        private void UpdateMainTree()
        {
            MainTreeOpen = false;

            for (int i = 0; i < Paddles.Count; i++)
                if (Paddles[i].MainTree)
                {
                    MainTreeOpen = true;
                    break;
                }

            if (MainTreeOpen)
            {
                Vector2 BoxSize = new Vector2(1280, 720) / 3f;
                Rectangle rect = new Rectangle(
                    (int)((GraphicsManager.GameResolution.X / 2f) - (BoxSize.X / 2f)),
                    (int)((GraphicsManager.GameResolution.Y / 2f) - (BoxSize.Y / 2f)),
                    (int)BoxSize.X,
                    (int)BoxSize.Y);

                List<SkillSelector> Selectors = new List<SkillSelector>();

                for (int i = 0; i < Paddles.Count; i++)
                    if (Paddles[i].MainTree)
                        Selectors.Add(new SkillSelector(Paddles[i].Player, Paddles[i].Tint));

                SkillTree.Update(rect, Selectors);
            }

            BuyMainTree();
        }

        private void BuyMainTree()
        {
            if (SkillTree.BuySkill != "")
            {
                List<Resource> resource = new List<Resource>();

                resource.Add(new Resource(ResourceName.Pixel, TotalScore));
                resource = SkillTree.BuySelectedSkill(resource);

                TotalScore = resource[0].Amount;
            }
        }

        private void UpdateMainTreeRequirements()
        {
            foreach (Skill S in SkillTree.Skills.Values)
            {
                bool RequirementExists = false;

                switch (S.Name)
                {
                    case "Black Hole":
                        foreach (SkillRequirement R in S.Requirements.Values)
                            if (R.RequiredSkill == SkillTree.Skills["Vortex"])
                            {
                                RequirementExists = true;

                                S.Requirements["Vortex"].RequiredSkillLevel = (3 * (S.Level + 1));

                                if (S.Requirements["Vortex"].RequiredSkillLevel > S.Requirements["Vortex"].RequiredSkill.Cost.Count)
                                    S.Requirements["Vortex"].RequiredSkillLevel = S.Requirements["Vortex"].RequiredSkill.Cost.Count;
                            }

                        if (!RequirementExists)
                            S.Requirements.Add("Vortex", new SkillRequirement(SkillTree.Skills["Vortex"], true));
                        break;

                    case "Portal":
                        foreach (SkillRequirement R in S.Requirements.Values)
                            if (R.RequiredSkill == SkillTree.Skills["Black Hole"])
                            {
                                RequirementExists = true;

                                S.Requirements["Black Hole"].RequiredSkillLevel = (3 * (S.Level + 1));

                                if (S.Requirements["Black Hole"].RequiredSkillLevel > S.Requirements["Black Hole"].RequiredSkill.Cost.Count)
                                    S.Requirements["Black Hole"].RequiredSkillLevel = S.Requirements["Black Hole"].RequiredSkill.Cost.Count;
                            }

                        if (!RequirementExists)
                            S.Requirements.Add("Black Hole", new SkillRequirement(SkillTree.Skills["Black Hole"], true));
                        break;
                }
            }
        }

        #endregion

        #region Player Tree

        private void UpdatePlayerTree()
        {
            for (int i = 0; i < Paddles.Count; i++)
                if (Paddles[i].SkillTreeOpen && Paddles[i].Player != InputType.None && Paddles[i].Player != InputType.AI)
                {
                    Vector2 TreeDisplace = Vector2.Zero;
                    float amount = 30f;
                    Vector2 BoxSize = new Vector2(1280, 720) / 4f;

                    switch (Paddles[i].Side)
                    {
                        case LocationEnum.Left:
                            TreeDisplace.X += amount;
                            TreeDisplace.Y -= (BoxSize.Y / 2f);
                            break;
                        case LocationEnum.Right:
                            TreeDisplace.X -= BoxSize.X + amount;
                            TreeDisplace.Y -= (BoxSize.Y / 2f);
                            break;
                        case LocationEnum.Top:
                            TreeDisplace.Y += amount;
                            TreeDisplace.X -= (BoxSize.X / 2f);
                            break;
                        case LocationEnum.Bottom:
                            TreeDisplace.Y -= BoxSize.Y + amount;
                            TreeDisplace.X -= (BoxSize.X / 2f);
                            break;
                    }

                    Rectangle rect = Rectangle.Empty;

                    rect.X = (int)(Paddles[i].Position.X + TreeDisplace.X);
                    rect.Y = (int)(Paddles[i].Position.Y + TreeDisplace.Y);
                    rect.Width = (int)BoxSize.X;
                    rect.Height = (int)BoxSize.Y;

                    if (rect.X < 0)
                        rect.X = 0;
                    else if (rect.X + rect.Width > GraphicsManager.GameResolution.X)
                        rect.X = (int)GraphicsManager.GameResolution.X - rect.Width;
                    if (rect.Y < 0)
                        rect.Y = 0;
                    else if (rect.Y + rect.Height > GraphicsManager.GameResolution.Y)
                        rect.Y = (int)GraphicsManager.GameResolution.Y - rect.Height;

                    //rect = new Rectangle(0, 0, 1280, 720);

                    Paddles[i].SkillTree.Update(rect, new List<SkillSelector> { new SkillSelector(Paddles[i].Player, Paddles[i].Tint) });
                }

        }

        #endregion

        private void UpdatePixelsPerSecond()
        {
            PixelsPerSecond = 0;

            foreach (Skill S1 in SkillTree.Skills.Values)
                PixelsPerSecond += S1.Level * 0.5f;


            for (int i = 0; i < Paddles.Count; i++)
            {
                Paddles[i].Score += PixelsPerSecond * GlobalVariables.WorldTime;
                //Paddles[i].Score.Truncate();
            }

        }
        
        #region Balls

        private void UpdateBalls()
        {
            for (int i = 0; i < Balls.Count; i++)
            {
                Balls[i].P.Update();
                Vector2 normal;
                float distance;
                PM.ApplyAcceleration acc;

                bool BreakLoop = false;
                for (int o = 0; o < Objects.Count; o++)
                {
                    switch (Objects[o].ObjectType)
                    {
                        case "Vortex":
                            if (Objects[o].Active)
                            {
                                normal = Balls[i].P.GetTruePosition() - Objects[o].Position;
                                distance = normal.Length();
                                normal.Normalize();

                                if (distance < 32f)                                
                                    distance = 32f;

                                acc = new PM.ApplyAcceleration(-normal * ((1.5e+7f * Objects[o].Alpha) / (distance * distance)));

                                acc.Update(Balls[i].P);
                            }
                            break;

                        case "Portal":
                            normal = Balls[i].P.GetTruePosition() - Objects[o].Position;
                            distance = normal.Length();

                            if (distance < 16f)
                            {
                                Objects[o].CollectedBalls.Add(Balls[i]);
                                Balls.RemoveAt(i);
                                i--;
                                BreakLoop = true;
                                break;
                            }
                            break;

                        case "Black Hole":
                            normal = Balls[i].P.GetTruePosition() - Objects[o].Position;
                            distance = normal.Length();
                            normal.Normalize();

                            if (distance < 8f * Objects[o].Scale)
                            {
                                Balls[i].Value += Objects[o].BlackHoleMultiplier * PixelsPerSecond;
                                Objects[o].CollectedBalls.Add(Balls[i]);
                                Balls.RemoveAt(i);
                                i--;
                                BreakLoop = true;
                                break;
                            }
                            else if (distance < 32f * Objects[o].Scale)
                                distance = 32f * Objects[o].Scale;
                            else if (distance > 175f * Objects[o].Scale)
                                distance = 175f * Objects[o].Scale;

                            float TimeLeft = UsefulMethods.FindBetween(Objects[o].Timer, Objects[o].Duration, 0f, 2f, 0.1f, false);
                            acc = new PM.ApplyAcceleration(-normal * ((5e+6f * Objects[o].Alpha * Objects[o].Scale * TimeLeft * TimeLeft * 25f) / (distance * distance)));
                            acc.Update(Balls[i].P);
                            break;
                    }



                    if (BreakLoop)
                        break;
                }

                if (BreakLoop)
                    continue;

                //PM.Wave wave = new PM.Wave(rand, 0.75f, 2f, 0.8f, WaveTypeEnum.Sine);
                //wave.Update(Balls[i].P);



                //Vector2 dis = Balls[i].P.Position - InputManager.MousePosition;
                //dis.Normalize();

                //PM.ApplyAcceleration acc = new PM.ApplyAcceleration(-dis * 500f);
                //acc.Update(Balls[i].P);

                //PM.ApplyAcceleration acc = new PM.ApplyAcceleration(new Vector2(0, 500f));
                //acc.Update(Balls[i].P);

                PM.SetMaxSpeed maxSpeed = new PM.SetMaxSpeed(1000f);
                maxSpeed.Update(Balls[i].P);

                PM.SetMinSpeed minSpeed = new PM.SetMinSpeed(250f);
                minSpeed.Update(Balls[i].P);


                if (Balls[i].P.Position != Balls[i].P.Position)
                    Balls.RemoveAt(i);
            }

            CheckCollisions();


            //TotalScore = 0f;

            AddBalls();
        }

        private void AddBalls()
        {
            //Balls.Clear();

            BallTimer += GlobalVariables.WorldTime;

            float BallTime = 5f;
            int MaxBalls = 1;
            //int ballValue = 1 + Balls.Count;

            foreach (Skill S1 in SkillTree.Skills.Values)
                MaxBalls += S1.Level;


            if (BallTimer > BallTime || Balls.Count < MaxBalls)
            {
                for (int o = 0; o < MaxBalls - Balls.Count; o++)
                {
                    int i = Balls.Count;

                    Balls.Add(new Ball(1 + PixelsPerSecond));
                    Balls[i].P.Velocity = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
                    Balls[i].P.Velocity.Normalize();
                    Balls[i].P.Velocity *= 250f;
                    Balls[i].P.Scale = 0.33f;
                    Balls[i].P.Tex = SheetTest.GetTexture("Pong", 3);
                    Balls[i].P.RotateWithMovement = true;

                    Balls[i].P.Tint = Color.White;
                    Balls[i].P.Position = new Vector2(GraphicsManager.GameResolution.X / 2f, GraphicsManager.GameResolution.Y / 2f);

                    BallTimer = 0f;


                    float bonusChance = UsefulMethods.FindBetween(SkillTree.Skills["Bonus Ball"].Level, SkillTree.Skills["Bonus Ball"].Cost.Count, 0, 0.5f, 0f, false);
                    if (rand.NextDouble() < bonusChance)
                    {
                        Balls[i].P.Glow = true;
                        Balls[i].Types.Add("Bonus");
                    }
                }
            }
        }

        #endregion

        #region Objects

        int VortexCount = 0;
        int PortalCount = 0;
        int BlackHoleCount = 0;
        int TotalVortex = 0;
        int TotalVortexLevel = 0;

        private void UpdateObjects()
        {
            UpdateObjectPixels();
            CountObjects();

            int SizeReductionX = (int)(GraphicsManager.GameResolution.X * 0.05f);
            int SizeReductionY = (int)(GraphicsManager.GameResolution.Y * 0.05f);
            Vector2 RandomPosition = new Vector2(rand.Next(SizeReductionX, (int)GraphicsManager.GameResolution.X - SizeReductionX), rand.Next(SizeReductionY, (int)GraphicsManager.GameResolution.Y - SizeReductionY));

            if (TotalVortexLevel > TotalVortex)
                Objects.Add(new PongObject(SheetTest.GetTexture("Pong", 2), RandomPosition, "Vortex"));
            
            for (int i = 0; i < Objects.Count; i++)
            {
                UpdateObject(i);

                Objects[i].Timer += GlobalVariables.WorldTime;

                if (!Objects[i].Active && Objects[i].Timer > Objects[i].Frequency)
                    InitializeObject(i, RandomPosition);                
                else if (Objects[i].Active && Objects[i].Timer > Objects[i].Duration)                
                    ResetObject(i);
                                
                if (Objects[i].Active)
                    ObjectAlphaControl(i);

                ManageCollectedBalls(i);
            }
        }

        private void CountObjects()
        {
            VortexCount = 0;
            PortalCount = 0;
            BlackHoleCount = 0;
            TotalVortex = 0;

            for (int i = 0; i < Objects.Count; i++)
            {
                switch (Objects[i].ObjectType)
                {
                    case "Vortex":
                        if (Objects[i].Active)
                            VortexCount++;

                        TotalVortex++;
                        break;

                    case "Portal":
                        if (Objects[i].Active)
                            PortalCount++;

                        TotalVortex++;
                        break;

                    case "Black Hole":
                        if (Objects[i].Active)
                            BlackHoleCount++;

                        TotalVortex++;
                        break;
                }
            }

            TotalVortexLevel = SkillTree.Skills["Vortex"].Level + SkillTree.Skills["Portal"].Level + SkillTree.Skills["Black Hole"].Level;
        }

        private void ObjectAlphaControl(int i)
        {
            float StartMod = UsefulMethods.FindBetween(Objects[i].Timer, Objects[i].Duration * 0.5f, 0f, 1f, 0f, false);
            float EndMod = UsefulMethods.FindBetween(Objects[i].Timer, Objects[i].Duration, Objects[i].Duration * 0.95f, 1f, 0f, true);

            float FinalMod = StartMod;
            if (StartMod > EndMod)
                FinalMod = EndMod;

            Objects[i].Alpha = FinalMod;
        }

        private void UpdateObjectPixels()
        {
            BigDecimal VortexBase = 10f * (SkillTree.Skills["Vortex Base Resource"].Level + 1f) * SkillTree.Skills["Vortex"].Level;
            BigDecimal VortexEfficiency = 1f;
            if (SkillTree.Skills["Vortex Efficiency"].Level > 0)
                VortexEfficiency = BigDecimal.Pow(1.4f, SkillTree.Skills["Vortex Efficiency"].Level);

            BigDecimal BlackHoleBase = ((10f * (SkillTree.Skills["Black Hole Base Resource"].Level + 1f)) + VortexBase) * SkillTree.Skills["Black Hole"].Level;
            BigDecimal BlackHoleEfficiency = 1f;
            if (SkillTree.Skills["Black Hole Efficiency"].Level > 0)
                BlackHoleEfficiency = BigDecimal.Pow(1.4f, SkillTree.Skills["Black Hole Efficiency"].Level) + VortexEfficiency;

            BigDecimal PortalBase = ((10f * (SkillTree.Skills["Portal Base Resource"].Level + 1f)) + BlackHoleBase + VortexBase) * SkillTree.Skills["Portal"].Level;
            BigDecimal PortalEfficiency = 1f;
            if (SkillTree.Skills["Portal Efficiency"].Level > 0)
                PortalEfficiency = BigDecimal.Pow(1.4f, SkillTree.Skills["Portal Efficiency"].Level) + BlackHoleEfficiency + VortexEfficiency;








            PixelsPerSecond += (VortexBase * VortexEfficiency) + (BlackHoleBase * BlackHoleEfficiency) + (PortalBase * PortalEfficiency);
        }

        private void UpdateObject(int i)
        {
            switch (Objects[i].ObjectType)
            {
                case "Vortex":
                    Objects[i].Rotation += Objects[i].RotationalVelocity * GlobalVariables.WorldTime;
                    break;

                case "Black Hole":
                    float TimeLeft = UsefulMethods.FindBetween(Objects[i].Timer, Objects[i].Duration, 0f, 2f, 0.1f, false);
                    Objects[i].Rotation += Objects[i].RotationalVelocity * 10f * TimeLeft * TimeLeft * GlobalVariables.WorldTime;

                    Objects[i].Scale = UsefulMethods.FindBetween(TimeLeft * TimeLeft, 4f, 0f, 1.6f, 0.1f, false);

                    break;

                case "Portal":Objects[i].Rotation += Objects[i].RotationalVelocity * GlobalVariables.WorldTime;
                    Objects[i].PortalTimer += GlobalVariables.WorldTime;

                    if (Objects[i].PortalTimer > Objects[i].PortalFrequency && Objects[i].Alpha > 0.33f)
                    {
                        Objects[i].PortalTimer = 0f;
                        bool end = false;

                        for (int o = 0; o < Objects.Count; o++)
                            if (Objects[o].CollectedBalls.Count != 0 && Objects[o].ObjectType == "Portal" && Objects[o] != Objects[i])
                            {
                                PortalBall(o, i);
                                end = true;
                                break;
                            }

                        if (!end)
                            for (int o = 0; o < Objects.Count; o++)
                                if (Objects[o].CollectedBalls.Count != 0 && Objects[o].ObjectType != "Black Hole")
                                {
                                    PortalBall(o, i);
                                    end = true;
                                    break;
                                }
                    }
                    break;
            }
        }

        private void InitializeObject(int i, Vector2 RandomPosition)
        {
            Objects[i].Timer = 0f;
            Objects[i].Active = true;
            Objects[i].Position = RandomPosition;
            Objects[i].Alpha = 0f;

            switch (Objects[i].MainType)
            {
                case "Vortex":
                    //if (SkillTree.Skills["Portal"].Level > PortalCount && rand.Next(0, 27) == 0)
                    //{
                    //    PortalCount++;
                    //    VortexCount--;
                    //    Objects[i].ObjectType = "Portal";
                    //    Objects[i].Tex = SheetTest.GetTexture("Pong", 1);
                    //}
                    //else if (SkillTree.Skills["Black Hole"].Level > BlackHoleCount && rand.Next(0, 9) == 0)
                    //{
                    //    BlackHoleCount++;
                    //    VortexCount--;
                    //    Objects[i].ObjectType = "Black Hole";
                    //    Objects[i].Tex = SheetTest.GetTexture("Pong", 0);
                    //}
                    //else
                    //    Objects[i].Tex = SheetTest.GetTexture("Pong", 2);

                    #region

                    if ((BlackHoleCount + VortexCount > (PortalCount + 1) * 9) && SkillTree.Skills["Portal"].Level > PortalCount && rand.Next(0, 2) == 0)
                    {
                        PortalCount++;
                        VortexCount--;
                        Objects[i].ObjectType = "Portal";
                        Objects[i].Tex = SheetTest.GetTexture("Pong", 1);
                    }
                    else if (VortexCount > ((BlackHoleCount + 1) * 3) && SkillTree.Skills["Black Hole"].Level > BlackHoleCount && rand.Next(0, 2) == 0)
                    {
                        BlackHoleCount++;
                        VortexCount--;
                        Objects[i].ObjectType = "Black Hole";
                        Objects[i].Tex = SheetTest.GetTexture("Pong", 0);
                    }
                    else
                        Objects[i].Tex = SheetTest.GetTexture("Pong", 2);

                    #endregion

                    Objects[i].FullFrequency = 360f - (SkillTree.Skills["Vortex Frequency"].Level * 15f);
                    Objects[i].PortalTimer = 0f;
                    Objects[i].RotationalVelocity = ((float)rand.NextDouble() * 1f) + 0.5f;

                    if (rand.Next(0, 2) == 0)
                        Objects[i].RotationalVelocity *= -1f;
                                        
                    switch (Objects[i].ObjectType)
                    {
                        case "Vortex":
                            Objects[i].Scale = 0.75f;
                            Objects[i].Duration = 5f + SkillTree.Skills["Vortex Duration"].Level;
                            break;

                        case "Black Hole":
                            Objects[i].Scale = 0.01f;
                            Objects[i].Duration = 10f + SkillTree.Skills["Vortex Duration"].Level + SkillTree.Skills["Black Hole Duration"].Level;
                            Objects[i].BlackHoleMultiplier = SkillTree.Skills["Black Hole Value Increase"].Level + 1;
                            break;

                        case "Portal":
                            Objects[i].Scale = 0.75f;
                            Objects[i].Duration = 15f + SkillTree.Skills["Vortex Duration"].Level + SkillTree.Skills["Black Hole Duration"].Level + SkillTree.Skills["Portal Duration"].Level;
                            Objects[i].PortalMultiplier = SkillTree.Skills["Portal Extra Ball"].Level + 2;
                            Objects[i].BlackHoleMultiplier = SkillTree.Skills["Black Hole Value Increase"].Level + 1;

                            if (SkillTree.Skills["Portal Delay"].Level == 10)
                                Objects[i].PortalFrequency = 0.01f;
                            else
                                Objects[i].PortalFrequency = 1f - SkillTree.Skills["Portal Delay"].Level * 0.1f;
                            break;
                    }
                    break;
            }
        }

        private void ResetObject(int i)
        {
            Objects[i].Timer = 0f;
            Objects[i].Active = false;
            Objects[i].Frequency = Objects[i].FullFrequency - ((float)rand.NextDouble() * (Objects[i].FullFrequency / 2f));
            Objects[i].Rotation = 0f;
            Objects[i].Alpha = 0f;

            switch (Objects[i].MainType)
            {
                case "Vortex":
                    switch (Objects[i].ObjectType)
                    {
                        case "Portal":
                            PortalCount--;
                            VortexCount++;

                            for (int o = 0; o < Objects[i].CollectedBalls.Count; o++)
                            {
                                Vector2 normal = Objects[i].CollectedBalls[o].P.Velocity;
                                normal.Normalize();

                                normal *= (float)rand.NextDouble() * 64f;

                                Objects[i].CollectedBalls[o].P.Position = Objects[i].Position + normal;
                            }

                            Balls.AddRange(Objects[i].CollectedBalls);
                            Objects[i].CollectedBalls.Clear();
                            break;

                        case "Black Hole":
                            BlackHoleCount--;
                            VortexCount++;

                            for (int o = 0; o < Objects[i].CollectedBalls.Count; o++)
                            {
                                Vector2 normal = Objects[i].CollectedBalls[o].P.Velocity;
                                normal.Normalize();

                                normal *= (float)rand.NextDouble() * 64f;

                                Objects[i].CollectedBalls[o].P.Position = Objects[i].Position + normal;
                            }

                            Balls.AddRange(Objects[i].CollectedBalls);
                            Objects[i].CollectedBalls.Clear();

                            GlobalVariables.ActivateSpeedChange(5f, 0.05f, 0.05f, 0.9f);
                            break;
                    }

                    Objects[i].ObjectType = "Vortex";
                    Objects[i].FullFrequency = (SkillTree.Skills["Vortex Frequency"].Level * 15f) - 360f;
                    break;
            }
        }

        private void ManageCollectedBalls(int i)
        {
            while (Objects[i].CollectedBalls.Count > 10000)
            {
                List<int> ActiveObjects = new List<int>();

                for (int o = 0; o < Objects.Count; o++)
                    if (o != i)
                        if (Objects[o].Active && Objects[o].Alpha > 0.5f)
                            ActiveObjects.Add(o);

                if (ActiveObjects.Count == 0)
                    ActiveObjects.Add(i);

                Objects[i].CollectedBalls[10000].P.Position = Objects[ActiveObjects[rand.Next(0, ActiveObjects.Count)]].Position;
                Balls.Add(Objects[i].CollectedBalls[10000]);
                Objects[i].CollectedBalls.RemoveAt(10000);
            }
        }




        private void PortalBall(int o, int i)
        {
            int ball = rand.Next(0, Objects[o].CollectedBalls.Count);

            Objects[o].CollectedBalls[ball].Value += Objects[i].BlackHoleMultiplier * PixelsPerSecond;

            Balls.Add(Objects[o].CollectedBalls[ball]);
            Objects[o].CollectedBalls.RemoveAt(ball);

            Vector2 vel = Balls[Balls.Count - 1].P.Velocity;

            if (vel != Vector2.Zero)
                vel.Normalize();

            Balls[Balls.Count - 1].P.Position = Objects[i].Position + (vel * 20f);

            for (int n = 0; n < Objects[o].PortalMultiplier; n++)
            {
                Balls.Add(new Ball(Balls[Balls.Count - 1]));

                vel = Balls[Balls.Count - 1].P.Velocity;
                float dis = vel.Length();

                float angle = UsefulMethods.VectorToAngle(vel);
                angle += MathHelper.Pi * (float)rand.NextDouble();

                Balls[Balls.Count - 1].P.Velocity = UsefulMethods.AngleToVector(angle) * dis;

                if (vel != Vector2.Zero)
                    vel.Normalize();

                Balls[Balls.Count - 1].P.Position = Objects[i].Position + (vel * 20f);
            }
        }

        #endregion
        
        private void CheckCollisions()
        {
            for (int i = 0; i < Balls.Count; i++)
            {
                float TexSize = Balls[i].P.Tex.Width * Balls[i].P.Scale * 0.5f;
                if (Balls[i].P.Tex.Height * Balls[i].P.Scale * 0.5f > TexSize)
                    TexSize = Balls[i].P.Tex.Height * Balls[i].P.Scale * 0.5f;


                #region Remove Balls

                if (Balls[i].P.GetTruePosition().X + TexSize < 0 ||
                    Balls[i].P.GetTruePosition().X - TexSize > GraphicsManager.GameResolution.X ||
                    Balls[i].P.GetTruePosition().Y + TexSize < 0 ||
                    Balls[i].P.GetTruePosition().Y - TexSize > GraphicsManager.GameResolution.Y)
                {
                    float maxTime = 600f;
                    float maxMult = 10f;

                    float J = UsefulMethods.FindBetween(Balls[i].P.TimeAlive, maxTime, 0f, maxMult, 1f, false, 0.5f);
                    Balls[i].Value *= J;

                    for (int t = 0; t < Balls[i].Types.Count; t++)
                    {
                        switch (Balls[i].Types[t])
                        {
                            case "Bonus":
                                Balls[i].Value = BigDecimal.Pow((double)Balls[i].Value, 1.15f);
                                break;
                        }
                    }

                    TotalScore += Balls[i].Value;
                    Balls.RemoveAt(i);
                    i--;
                    continue;
                }

                #endregion

                #region Edge Collision

                if (BorderSize.Left > 0)
                {
                    if (Balls[i].P.GetTruePosition().X - TexSize < BorderSize.Left)
                    {
                        Balls[i].P.Position.X -= (Balls[i].P.GetTruePosition().X - TexSize) - BorderSize.Left;
                        Balls[i].P.Velocity.X *= -1;
                    }

                }
                if (BorderSize.Top > 0)
                {
                    if (Balls[i].P.GetTruePosition().Y - TexSize < BorderSize.Top)
                    {
                        Balls[i].P.Position.Y -= (Balls[i].P.GetTruePosition().Y - TexSize) - BorderSize.Top;
                        Balls[i].P.Velocity.Y *= -1;
                    }
                }
                if (BorderSize.Right > 0)
                {
                    if (Balls[i].P.GetTruePosition().X + TexSize > GraphicsManager.GameResolution.X - BorderSize.Right)
                    {
                        Balls[i].P.Position.X -= (Balls[i].P.GetTruePosition().X + TexSize) - (GraphicsManager.GameResolution.X - BorderSize.Right);
                        Balls[i].P.Velocity.X *= -1;
                    }
                }
                if (BorderSize.Bottom > 0)
                {
                    if (Balls[i].P.GetTruePosition().Y + TexSize > GraphicsManager.GameResolution.Y - BorderSize.Bottom)
                    {
                        Balls[i].P.Position.Y -= (Balls[i].P.GetTruePosition().Y + TexSize) - (GraphicsManager.GameResolution.Y - BorderSize.Bottom);
                        Balls[i].P.Velocity.Y *= -1;
                    }
                }

                #endregion

                #region Paddle Collision

                for (int o = 0; o < Paddles.Count; o++)
                {
                    if (Vector2.Distance(Balls[i].P.GetTruePosition(), Paddles[o].Position) < TexSize + Paddles[o].Size)
                    {
                        Rectangle rect1 = Paddles[o].GetRectangle();
                        Rectangle rect2 = new Rectangle((int)(Balls[i].P.GetTruePosition().X - (TexSize * Balls[i].P.Scale)), (int)(Balls[i].P.GetTruePosition().Y - (TexSize * Balls[i].P.Scale)), (int)((TexSize * Balls[i].P.Scale) * 2), (int)((TexSize * Balls[i].P.Scale) * 2));

                        if (rect1.Intersects(rect2))
                        {
                            float speed = Balls[i].P.Velocity.Length();
                            Vector2 dis = Balls[i].P.GetTruePosition() - Paddles[o].Position;

                            //Balls[i].P.Position += dis;

                            //dis = Balls[i].P.GetTruePosition() - Paddles[o].Position;
                            dis.Normalize();
                            Balls[i].P.Velocity = dis * speed;
                            Balls[i].P.Velocity += Paddles[o].Velocity * 0.25f;
                            Balls[i].P.Position += Balls[i].P.Velocity * GlobalVariables.WorldTime;

                            Balls[i].Owner = Paddles[o].Owner;
                            Balls[i].P.Tint = Paddles[o].Owner.PrimaryColor;
                        }
                    }
                }

                #endregion
            }

            for (int i = 0; i < Paddles.Count; i++)
            {
                Paddles[i].PaddleOffset = PaddleOffset;
                SetPaddleOffset(i);
            }

            #region Paddle Overlap
            
            for (int i = Paddles.Count - 1; i >= 0; i--)
            {
                Rectangle rect1 = Paddles[i].GetRectangle();

                for (int o = Paddles.Count - 1; o >= 0; o--)
                {
                    if (i != o)
                        if (Paddles[i].Side == Paddles[o].Side)
                        {
                            Rectangle rect2 = Paddles[o].GetRectangle();

                            if (rect1.Intersects(rect2))
                            {
                                if (Paddles[i].SideWeight >= Paddles[o].SideWeight)
                                {
                                    Paddles[o].PaddleOffset += PaddleOffset + (Paddles[o].Thickness / 2f);
                                    SetPaddleOffset(o);
                                    rect2 = Paddles[o].GetRectangle();
                                }
                                else
                                {
                                    Paddles[i].PaddleOffset += PaddleOffset + (Paddles[i].Thickness / 2f);
                                    SetPaddleOffset(i);
                                    rect1 = Paddles[i].GetRectangle();
                                }
                            }
                        }
                }
            }

            #endregion
        }

        private void SetPaddleOffset(int i)
        {
            switch (Paddles[i].Side)
            {
                case LocationEnum.Left:
                    Paddles[i].Position.X = Paddles[i].PaddleOffset + BorderSize.Left;
                    break;

                case LocationEnum.Top:
                    Paddles[i].Position.Y = Paddles[i].PaddleOffset + BorderSize.Top;
                    break;

                case LocationEnum.Right:
                    Paddles[i].Position.X = GraphicsManager.GameResolution.X - Paddles[i].PaddleOffset - BorderSize.Right;
                    break;

                case LocationEnum.Bottom:
                    Paddles[i].Position.Y = GraphicsManager.GameResolution.Y - Paddles[i].PaddleOffset - BorderSize.Bottom;
                    break;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            int humans = 0;

            Vector2 GameSize = GraphicsManager.GameResolution;

            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, (int)GameSize.X, (int)GameSize.Y), Color.Black);

            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, (int)BorderSize.Left, (int)GameSize.Y), Color.White);
            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, (int)GameSize.X, (int)BorderSize.Top), Color.White);
            spriteBatch.Draw(StaticTests.Marker, new Rectangle((int)GameSize.X - (int)BorderSize.Right, 0, (int)BorderSize.Right, (int)GameSize.Y), Color.White);
            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, (int)GameSize.Y - (int)BorderSize.Bottom, (int)GameSize.X, (int)BorderSize.Bottom), Color.White);

            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].Active)
                    spriteBatch.Draw(Objects[i].Tex, Objects[i].Position, null, Color.White * Objects[i].Alpha * 0.75f, Objects[i].Rotation, new Vector2(Objects[i].Tex.Width / 2f, Objects[i].Tex.Height / 2f), Objects[i].Scale, SpriteEffects.None, 0f);
            }

            for (int i = 0; i < Paddles.Count; i++)
            {
                switch (Paddles[i].Side)
                {
                    case LocationEnum.Top:
                    case LocationEnum.Bottom:
                        spriteBatch.Draw(StaticTests.Marker, Paddles[i].Position, null, Paddles[i].Tint, 0f,
                            new Vector2(StaticTests.Marker.Width / 2f, StaticTests.Marker.Height / 2f),
                            new Vector2((float)Paddles[i].Size / (float)StaticTests.Marker.Width, (float)Paddles[i].Thickness / (float)StaticTests.Marker.Height),
                            SpriteEffects.None, 0f);
                        break;

                    case LocationEnum.Left:
                    case LocationEnum.Right:
                        spriteBatch.Draw(StaticTests.Marker, Paddles[i].Position, null, Paddles[i].Tint, 0f,
                            new Vector2(StaticTests.Marker.Width / 2f, StaticTests.Marker.Height / 2f),
                            new Vector2((float)Paddles[i].Thickness / (float)StaticTests.Marker.Width, (float)Paddles[i].Size / (float)StaticTests.Marker.Height),
                            SpriteEffects.None, 0f);
                        break;
                }

                if (!Paddles[i].Child)
                {
                    switch (Paddles[i].Side)
                    {
                        case LocationEnum.Top:
                        case LocationEnum.Bottom:
                            spriteBatch.Draw(StaticTests.Marker, Paddles[i].Position, null, Color.White, 0f,
                                new Vector2(StaticTests.Marker.Width / 2f, StaticTests.Marker.Height / 2f),
                                new Vector2((float)(Paddles[i].Size * 0.75f) / (float)StaticTests.Marker.Width, (float)Paddles[i].Thickness / (float)StaticTests.Marker.Height),
                                SpriteEffects.None, 0f);

                            break;

                        case LocationEnum.Left:
                        case LocationEnum.Right:
                            spriteBatch.Draw(StaticTests.Marker, Paddles[i].Position, null, Color.White, 0f,
                                new Vector2(StaticTests.Marker.Width / 2f, StaticTests.Marker.Height / 2f),
                                new Vector2((float)Paddles[i].Thickness / (float)StaticTests.Marker.Width, (float)(Paddles[i].Size * 0.75f) / (float)StaticTests.Marker.Height),
                                SpriteEffects.None, 0f);

                            break;
                    }


                    StaticInfoBox.ClearList();
                    StaticInfoBox.AddItem(StringManager.ColorString("" + WriteNumber.Write(Paddles[i].Score, SeparatorType.Standard, 3, ShortenType.Short_Scale), Paddles[i].Tint));
                    StaticInfoBox.Draw(spriteBatch, new Vector2((GraphicsManager.GameResolution.X / (HumanPlayers + 1)) * (humans + 1), 50f), 2f, 0.75f);
                    humans++;
                }
            }

            StaticInfoBox.ClearList();
            StaticInfoBox.AddItem(StringManager.ColorString("" + WriteNumber.Write(TotalScore, SeparatorType.Standard, 3, ShortenType.Short_Scale), Color.White));
            StaticInfoBox.AddItem(StringManager.ColorString("" + WriteNumber.Write(PixelsPerSecond, SeparatorType.Standard, 3, ShortenType.Short_Scale), Color.White) + " Pixels Per Second");
            StaticInfoBox.Draw(spriteBatch, new Vector2((GraphicsManager.GameResolution.X / 2f), GraphicsManager.GameResolution.Y - 50f), 2f, 0.75f);

            for (int i = 0; i < Balls.Count; i++)
            {
                Balls[i].P.Scale = 0.25f;
                Balls[i].P.Scale = 0.25f + (0.01f * Balls[i].P.TimeAlive);
                
                if (Balls[i].P.Glow)
                {
                    Texture2D Tex = SheetTest.GetTexture("Pong", 4);
                    spriteBatch.Draw(Tex, Balls[i].P.GetTruePosition(), null, Color.Gold, Balls[i].P.Rotation, new Vector2(Tex.Width / 2f, Tex.Height / 2f), Balls[i].P.Scale * 1.75f, SpriteEffects.None, 0f);
                }


                Balls[i].P.DrawParticle(spriteBatch);

                //StaticInfoBox.ClearList();

                //if (Balls[i].Owner != null)
                //    StaticInfoBox.AddItem((Balls[i].Value * Balls[i].Power) + "");
                //else
                //    StaticInfoBox.AddItem(Balls[i].Value + "");


                //StaticInfoBox.Draw(spriteBatch, Balls[i].P.Position - new Vector2(0, 20f), 1.5f, 0.75f);
            }
        }

        public void DrawSkillTrees(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (MainTreeOpen)
                SkillTree.Draw(spriteBatch, graphicsDevice);

            for (int i = 0; i < Paddles.Count; i++)
                if (Paddles[i].SkillTreeOpen && Paddles[i].Player != InputType.None && Paddles[i].Player != InputType.AI)
                    Paddles[i].SkillTree.Draw(spriteBatch, graphicsDevice);
        }
    }
}
