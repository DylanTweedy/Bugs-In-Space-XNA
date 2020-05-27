using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeletonEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkinnerBox
{

    [Serializable()]
    public class Pong
    {
        [Serializable()]
        public class ScoreCounter
        {
            public Particle P;
            public BigDecimal Amount = 0;
            public BigDecimal DesiredAmount;
            public Profile Owner;
            public float Time;

            public ScoreCounter(Vector2 Position, BigDecimal amount, Profile owner, float time)
            {
                P = new Particle();
                P.Position = Position;
                Owner = owner;

                if (owner != null)
                    P.Tint = owner.PrimaryColor;

                DesiredAmount = amount;
                Time = time;
            }
        }

        List<Ball> Balls = new List<Ball>();
        List<Paddle> Paddles = new List<Paddle>();
        List<PongObject> Objects = new List<PongObject>();
        Location BorderSize;
        Location PlayersLocation;
        
        float PaddleOffset = 8f;
        Random rand;
        int HumanPlayers;
        Dictionary<ResourceName, Resource> TotalScore = new Dictionary<ResourceName,Resource>();

        SkillCloud SkillTree;

        float BallTimer;
        BigDecimal PixelsPerSecond = 0;

        Paddle WinningPlayer;
        GlobalParticleEmitter Emitter = new GlobalParticleEmitter();

        List<Ball> BallBank = new List<Ball>();
        int TotalBalls;
        int MaxBalls = 1000;

        List<ScoreCounter> ScoreCounters = new List<ScoreCounter>();
        






        



        public void Initialize()
        {
            BorderSize = new Location(8, 8, 8, 8);
            PlayersLocation = new Location(0, 0, 0, 0);
            rand = new Random();
            TotalScore.Add(ResourceName.Pixel, new Resource(0));

            InitializeTree();
        }

        public void InitializeTree()
        {
            SkillTree = new SkillCloud();

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 1000, 1.05f), "Bonus Ball", null, false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 1000, 1.05f), "Ball Count", "Bonus Ball", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 1000, 1.05f), "Ball Value", "Bonus Ball", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 1000, 1.05f), "Base Pixels Per Second", "Bonus Ball", false);


            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 100, 18, 1.15f), "Vortex", "Bonus Ball", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vortex Efficiency", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vortex Base Resource", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Vortex Duration", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 20, 1.15f), "Vortex Frequency", "Vortex", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 1", "Vortex", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 100, 9, 1.15f), "Black Hole", "Special Ball 1", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Black Hole Efficiency", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Black Hole Base Resource", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 99, 1.15f), "Black Hole Value Increase", "Black Hole", false);

            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Special Ball 2", "Black Hole", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 100, 3, 1.15f), "Portal", "Special Ball 2", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Efficiency", "Portal", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Base Resource", "Portal", false);
            SkillTree.AddSkill(ResourceCreation.CreateSingle(ResourceName.Pixel, 10, 10, 1.15f), "Portal Extra Ball", "Portal", false);
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

            //SkillTree.UpdateCamera(new Rectangle(0, 0, 100, 100));
        }

        public void Update()
        {
            pulseTimer += GlobalVariables.WorldTime;

            UpdatePixelsPerSecond();

            BorderSize = new Location(0, 8, 0, 8);
            BorderSize = new Location(0, 0, 0, 0);

            UpdatePlayers();
            UpdateBalls();
            UpdateObjects();
            UpdatePlayerTree();
            UpdateMainTree();
            UpdateMainTreeRequirements();

            Emitter.Update();

            TotalScore[ResourceName.Pixel].Amount += PixelsPerSecond * GlobalVariables.WorldTime;
            CountBalls();
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

                if (WinningPlayer == null)
                    WinningPlayer = Paddles[i];

                if (WinningPlayer.Owner.Resource[ResourceName.Pixel].Amount < Paddles[i].Owner.Resource[ResourceName.Pixel].Amount)
                    WinningPlayer = Paddles[i];
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
                                    float dist2 = Vector2.Distance(Paddles[i].Position, Balls[o].P.TruePosition);

                                    if (dist1 > dist2)
                                    {
                                        dist1 = dist2;
                                        selected = o;
                                    }
                                }
                            }

                            if (selected != -1)
                            {
                                Vector2 pos = Balls[selected].P.TruePosition;

                                switch (Paddles[i].Side)
                                {
                                    case LocationEnum.Left:
                                    case LocationEnum.Right:
                                        if (Balls[selected].P.TruePosition.Y > maxY - (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.TruePosition - new Vector2(0, Balls[selected].P.TruePosition.Y - (maxY - (Paddles[i].Size / 2f)));
                                        else if (Balls[selected].P.TruePosition.Y < minY + (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.TruePosition - new Vector2(0, Balls[selected].P.TruePosition.Y - (minY + (Paddles[i].Size / 2f)));
                                        break;

                                    case LocationEnum.Top:
                                    case LocationEnum.Bottom:
                                        if (Balls[selected].P.TruePosition.X > maxX - (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.TruePosition - new Vector2(Balls[selected].P.TruePosition.X - (maxX - (Paddles[i].Size / 2f)), 0);
                                        else if (Balls[selected].P.TruePosition.X < minX + (Paddles[i].Size / 2f))
                                            pos = Balls[selected].P.TruePosition - new Vector2(Balls[selected].P.TruePosition.X - (minX + (Paddles[i].Size / 2f)), 0);
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
               //if (Paddles[i].Player == InputType.AI)
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
            SkillTree.MainUpdate(TotalScore);

            for (int i = 0; i < Paddles.Count; i++)
            {
                Paddles[i].MainSelector.AvailableResource = TotalScore;

                if (Paddles[i].MainTree)
                    SkillTree.PlayerUpdate(Paddles[i].MainSelector);
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
                }

        }

        #endregion

        private void CountBalls()
        {
            TotalBalls = Balls.Count;


            for (int i = 0; i < Objects.Count; i++)            
                TotalBalls += Objects[i].CollectedBalls.Count;

            for (int i = 0; i < Balls.Count; i++)            
                TotalBalls += Balls[i].CollectedBalls.Count;

            DebugOptions.DebugDisplay.Add("Ball Count: " + TotalBalls);
            MaxBalls = 1000;
        }

        private void UpdatePixelsPerSecond()
        {
            PixelsPerSecond = 0;

            foreach (Skill S1 in SkillTree.Skills.Values)
                PixelsPerSecond += S1.Level * 0.5f;


            for (int i = 0; i < Paddles.Count; i++)
            {
                Paddles[i].Owner.Resource[ResourceName.Pixel].Amount += PixelsPerSecond * GlobalVariables.WorldTime;
                //Paddles[i].Score.Truncate();
            }
            
            for (int i = 0; i < ScoreCounters.Count; i++)
            {
                Vector2 Center = GraphicsManager.GameResolution / 2f;

                Vector2 Direction = Center - ScoreCounters[i].P.TruePosition;
                if (Direction != Vector2.Zero)
                    Direction.Normalize();

                ScoreCounters[i].P.Velocity = Direction * 256f;
                ScoreCounters[i].P.Update();


                if (ScoreCounters[i].Owner != null)
                {
                    for (int p = 0; p < Paddles.Count; p++)
                        if (ScoreCounters[i].Owner == Paddles[p].Owner && !Paddles[p].Child)
                            if (ScoreCounters[i].P.TimeAlive < ScoreCounters[i].Time)
                            {
                                ScoreCounters[i].P.Position = Paddles[p].Position;
                                ScoreCounters[i].P.Rotation = 0f;

                                float PaddleRotation = 0f;

                                switch (Paddles[p].Side)
                                {
                                    case LocationEnum.Top:
                                        PaddleRotation = MathHelper.Pi;
                                        break;

                                    case LocationEnum.Left:
                                        PaddleRotation = MathHelper.PiOver2;
                                        ScoreCounters[i].P.Rotation = PaddleRotation;
                                        break;

                                    case LocationEnum.Right:
                                        PaddleRotation = MathHelper.Pi * 1.5f;
                                        ScoreCounters[i].P.Rotation = PaddleRotation;
                                        break;
                                }

                                ScoreCounters[i].P.Position += Vector2.Transform(new Vector2(0f, -48f), Matrix.CreateRotationZ(PaddleRotation));
                            }

                    float delta = 0f;

                    if (ScoreCounters[i].P.TimeAlive > ScoreCounters[i].Time)
                    {
                        ScoreCounters[i].P.Alpha -= 1.5f * GlobalVariables.WorldTime;
                        ScoreCounters[i].P.Scale.Y *= 0.99f;
                    }
                    //else if (ScoreCounters[i].P.TimeAlive > ScoreCounters[i].Time * 0.25f)
                    //{
                    //    delta = UsefulMethods.FindBetween(ScoreCounters[i].P.TimeAlive, ScoreCounters[i].P.TimeAlive * 0.25f, 0f, 1f, 0f, false);
                    //    ScoreCounters[i].P.Alpha = delta;
                    //    ScoreCounters[i].P.Scale.Y = 25f * delta;
                    //}
                    else
                        ScoreCounters[i].P.Scale.Y = 22f;

                    delta = UsefulMethods.FindBetween(ScoreCounters[i].P.TimeAlive, ScoreCounters[i].Time * 0.25f, 0f, 1f, 0f, false);

                    //ScoreCounters[i].Amount = ScoreCounters[i].DesiredAmount * delta;
                    ScoreCounters[i].Amount += (ScoreCounters[i].DesiredAmount - ScoreCounters[i].Amount) * delta;
                    //ScoreCounters[i].Amount = ScoreCounters[i].DesiredAmount;
                }
                else
                {
                    if (ScoreCounters[i].P.TimeAlive > ScoreCounters[i].Time)
                    {
                        if (ScoreCounters[i].P.TimeAlive > ScoreCounters[i].Time * 2.5f)
                            ScoreCounters[i].P.Alpha -= 2.5f * GlobalVariables.WorldTime;
                    }
                    else
                        ScoreCounters[i].P.Scale.Y += 200f * GlobalVariables.WorldTime;

                    float delta = UsefulMethods.FindBetween(ScoreCounters[i].P.TimeAlive, ScoreCounters[i].Time * 3f, 0f, 1f, 0f, false);

                    ScoreCounters[i].Amount += (ScoreCounters[i].DesiredAmount - ScoreCounters[i].Amount) * delta;
                }

                if (ScoreCounters[i].P.Alpha <= 0f)
                {
                    ScoreCounters.RemoveAt(i);
                    i--;
                }
            }

            
            for (int i = ScoreCounters.Count - 1; i >= 0; i--)
                for (int o = ScoreCounters.Count - 1; o >= 0; o--)
                    if (i != o)
                    {
                        if (ScoreCounters[i].P.Tint == ScoreCounters[o].P.Tint)
                            if (ScoreCounters[i].Owner == null && Vector2.Distance(ScoreCounters[i].P.TruePosition, ScoreCounters[o].P.TruePosition) < 64f)
                            {
                                ScoreCounters[i].DesiredAmount += ScoreCounters[o].DesiredAmount;
                                ScoreCounters[i].P.TimeAlive = 0f;
                                ScoreCounters[i].P.Scale.Y = 0f;
                                ScoreCounters[i].P.Alpha = 1f;
                                ScoreCounters.RemoveAt(o);

                                if (o < i)
                                {
                                    i--;
                                    if (i < 0)
                                        break;
                                }
                            }
                            //else if (ScoreCounters[i].Owner == ScoreCounters[o].Owner && ScoreCounters[i].P.TimeAlive > time2)
                            //{
                            //    ScoreCounters[i].DesiredAmount += ScoreCounters[o].DesiredAmount;
                            //    ScoreCounters[i].P.TimeAlive = 0f;
                            //    ScoreCounters.RemoveAt(o);

                            //    if (o < i)
                            //    {
                            //        i--;
                            //        if (i < 0)
                            //            break;
                            //    }
                            //}
                    }
        }
        
        #region Balls

        private void UpdateBalls()
        {
            for (int i = 0; i < Balls.Count; i++)
            {
                PM.SetMaxSpeed maxSpeed = new PM.SetMaxSpeed(500f);
                PM.SetMinSpeed minSpeed = new PM.SetMinSpeed(100f);
                maxSpeed.Update(Balls[i].P);
                minSpeed.Update(Balls[i].P);

                Balls[i].P.Update();
                Vector2 normal;
                float distance;
                PM.ApplyAcceleration acc;

                if (Balls[i].ValueAlpha > 0f)
                {
                    Balls[i].ValueAlpha -= 2f * GlobalVariables.WorldTime;

                    if (Balls[i].ValueAlpha < 0f)
                        Balls[i].ValueAlpha = 0f;
                }

                bool BreakLoop = false;

                for (int o = 0; o < Objects.Count; o++)
                {
                    Interations.BallObjectInteraction(Balls[i], Objects[o]);

                    if (Balls[i].P.Remove)
                    {
                        Balls[i].P.Remove = false;
                        Balls.RemoveAt(i);
                        i--;
                        BreakLoop = true;
                        break;
                    }

                    if (Objects[o].P.Remove)
                    {
                        Objects[o].P.Remove = false;
                        ResetObject(o);
                    }
                }

                if (BreakLoop)                
                    continue;

                maxSpeed.Update(Balls[i].P);
                minSpeed.Update(Balls[i].P);
                
                //for (int p = 0; p < Balls[i].particleEmitters.Count; p++)
                //    Balls[i].particleEmitters[p].Update(Balls[i].P.TruePosition, Vector2.Zero);

                for (int m = 0; m < Balls[i].P.Movement.Count; m++)
                {
                    Balls[i].P.Movement[m].Update(Balls[i].P);
                }


                maxSpeed.Update(Balls[i].P);
                minSpeed.Update(Balls[i].P);


                for (int b = 0; b < Balls[i].Types.Count; b++)
                {
                    switch (Balls[i].Types[b])
                    {
                        case "Explosion":
                            Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002")}, 
                                new List<Color>() { Color.Red, Color.Yellow, Color.Orange, Color.OrangeRed },
                                1.5f, Balls[i].P.TruePosition, rand, 0.15f, 150f, -Balls[i].P.Velocity, MathHelper.Pi / 6f, Vector2.One * 8f, Vector2.One * 16f, 0.25f);
                            
                            if (Balls[i].BombTime < Balls[i].P.TimeAlive && !Balls[i].Exploded)
                            {
                                for (int o = 0; o < Balls.Count; o++)
                                    if (i != o)
                                    {
                                        normal = Balls[i].P.TruePosition - Balls[o].P.TruePosition;
                                        distance = normal.Length();
                                        if (normal != Vector2.Zero)
                                            normal.Normalize();

                                        distance = 10000000f / (distance * distance);
                                        if (distance < 0f)
                                            distance = 0f;

                                        Balls[o].P.Velocity += -normal * distance;
                                    }

                                Balls[i].P.RotateWithMovement = false;
                                Balls[i].Exploded = true;
                            }
                            break;

                        case "Implosion":
                            Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                new List<Color>() { Color.White, Color.Blue, Color.Cyan, Color.LightBlue },
                                1.5f, Balls[i].P.TruePosition, rand, 0.15f, 150f, -Balls[i].P.Velocity, MathHelper.Pi / 6f, Vector2.One * 8f, Vector2.One * 16f, 0.25f);

                            if (Balls[i].BombTime < Balls[i].P.TimeAlive && !Balls[i].Exploded)
                            {
                                for (int o = 0; o < Balls.Count; o++)
                                    if (i != o)
                                    {
                                        normal = Balls[i].P.TruePosition - Balls[o].P.TruePosition;
                                        distance = normal.Length();
                                        if (normal != Vector2.Zero)
                                            normal.Normalize();

                                        distance = 1000000f / (distance * distance);
                                        if (distance < 0f)
                                            distance = 0f;

                                        Balls[o].P.Velocity += normal * distance;
                                    }

                                Balls[i].P.RotateWithMovement = false;
                                Balls[i].Exploded = true;
                            }

                            break;

                        case "Mine":
                            if (Balls[i].BombTime < Balls[i].P.TimeAlive && !Balls[i].Exploded)
                                Balls[i].Exploded = true;
                            break;

                        case "Combining":
                            if (Balls[i].EatTimer > 0.5f)
                            for (int o = 0; o < Balls.Count; o++)
                                if (i != o)
                                    if (Balls[i].P.Area >= Balls[o].P.Area)
                                {
                                    float Scale1 = Balls[i].P.Scale.X;
                                    if (Balls[i].P.Scale.Y > Balls[i].P.Scale.X)
                                        Scale1 = Balls[i].P.Scale.Y;

                                    float Scale2 = Balls[o].P.Scale.X;
                                    if (Balls[o].P.Scale.Y > Balls[o].P.Scale.X)
                                        Scale2 = Balls[o].P.Scale.Y;

                                    if (Vector2.Distance(Balls[i].P.TruePosition, Balls[o].P.TruePosition) < (Scale1 / 4f) + (Scale2 / 2f))
                                    {
                                        float FinalScale = (float)Math.Sqrt((Scale1 * Scale1) + (Scale2 * Scale2));

                                        Balls[i].P.Scale.X = FinalScale;
                                        Balls[i].P.Scale.Y = FinalScale;
                                        
                                        if (Balls[i].EatTimer > 1f)
                                            Balls[i].EatTimer = 0f;
                                        
                                        if (Balls[i].P.Scale.X > 64 || Balls[i].P.Scale.Y > 64)                                        
                                            Balls[i].Exploded = true;
                                        
                                        Balls[i].CollectedBalls.Add(Balls[o]);
                                        Balls.RemoveAt(o);

                                        o--;

                                        if (i > o)
                                            i--;

                                        break;
                                    }
                                }

                            Balls[i].EatTimer += GlobalVariables.WorldTime;
                            break;

                        case "Gravity":
                            for (int o = 0; o < Balls.Count; o++)
                            {
                                if (i != o && !Balls[o].Types.Contains("Gravity"))
                                {
                                    normal = Balls[i].P.TruePosition - Balls[o].P.TruePosition;
                                    distance = normal.Length();

                                    if (normal != Vector2.Zero && distance < 200f)
                                    {
                                        normal.Normalize();

                                        if (distance < Balls[i].P.Scale.Length() * 3f)
                                            distance = Balls[i].P.Scale.Length() * 3f;

                                        acc = new PM.ApplyAcceleration(normal * (((2.5e+5f * Balls[i].P.Scale)) / (distance * distance)));
                                        acc.Update(Balls[o].P);
                                    }
                                }
                            }
                            break;
                    }
                }



                maxSpeed.Update(Balls[i].P);
                minSpeed.Update(Balls[i].P);


                //PM.Wave wave = new PM.Wave(rand, 0.75f, 2f, 0.8f, WaveTypeEnum.Sine);
                //wave.Update(Balls[i].P);



                //Vector2 dis = Balls[i].P.Position - InputManager.MousePosition;
                //dis.Normalize();

                //PM.ApplyAcceleration acc = new PM.ApplyAcceleration(-dis * 500f);
                //acc.Update(Balls[i].P);

                //PM.ApplyAcceleration acc = new PM.ApplyAcceleration(new Vector2(0, 500f));
                //acc.Update(Balls[i].P);



                if (Balls[i].P.Position != Balls[i].P.Position)
                {
                    Balls.RemoveAt(i);
                    i--;
                    continue;
                }
                
                if (Balls[i].Exploded)
                {
                    if (Balls[i].CollectedBalls.Count > 0)
                    {
                        float Angle = MathHelper.TwoPi / Balls[i].CollectedBalls.Count;
                        float MainAngle = UsefulMethods.VectorToAngle(Balls[i].P.Velocity);

                        for (int c = 0; c < Balls[i].CollectedBalls.Count; c++)
                        {
                            Balls[i].CollectedBalls[c].P.Position = Balls[i].P.TruePosition;
                            Balls[i].CollectedBalls[c].P.Velocity = Vector2.Transform(Balls[i].P.Velocity, Matrix.CreateRotationZ(MainAngle));
                            MainAngle += Angle;

                            Balls[i].CollectedBalls[c].Owner = Balls[i].Owner;
                            Balls[i].CollectedBalls[c].P.Tint = Balls[i].P.Tint;
                            Balls[i].CollectedBalls[c].EatTimer = 0f;

                            //if (TotalBalls >= MaxBalls)
                            //    BallBank.Add(Balls[i].CollectedBalls[c]);
                            //else
                                Balls.Add(Balls[i].CollectedBalls[c]);
                        }

                        Balls[i].CollectedBalls.Clear();
                    }

                    bool ExplosionDone = false;

                    if (Balls[i].Types.Contains("Implosion"))
                    {
                        Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Pong", "Explosion") },
                                    new List<Color>() { Color.Cyan * 0.25f, Color.LightBlue * 0.25f, Color.DeepSkyBlue * 0.25f, Color.CornflowerBlue * 0.25f },
                                    5, Balls[i].P.TruePosition, rand, 0.25f, 50f, Balls[i].P.Scale * 3f, Vector2.Zero, 0.5f);

                        Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                    new List<Color>() { Color.Cyan, Color.LightBlue, Color.DeepSkyBlue, Color.CornflowerBlue }, 
                                    50, Balls[i].P.TruePosition, rand, 0.5f, Balls[i].P.Scale.X * 3f, Vector2.One * 16f, Vector2.One * 1f, 0.75f);

                        ExplosionDone = true;
                    }

                    if (Balls[i].Types.Contains("Combining"))
                    {
                        Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Pong", "Explosion") },
                                    new List<Color>() { Color.Green * 0.25f, Color.LimeGreen * 0.25f, Color.Lime * 0.25f, Color.ForestGreen * 0.25f },
                                    5, Balls[i].P.TruePosition, rand, 0.5f, 50f, Balls[i].P.Scale, Balls[i].P.Scale * 3f, 0.5f);

                        Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                    new List<Color>() { Color.Green * 0.75f, Color.LimeGreen * 0.75f, Color.Lime * 0.75f, Color.ForestGreen * 0.75f },
                                    50, Balls[i].P.TruePosition, rand, 0.5f, Balls[i].P.Scale.X * 3f, Vector2.One * 8f, Vector2.One * 16f, 0.75f);

                        ExplosionDone = true;
                    }

                    if (!ExplosionDone)
                    {
                        Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Pong", "Explosion") },
                                    new List<Color>() { Color.Red * 0.25f, Color.Orange * 0.25f, Color.White * 0.25f, Color.LightGoldenrodYellow * 0.25f },
                                    5, Balls[i].P.TruePosition, rand, 0.5f, 50f, Balls[i].P.Scale, Balls[i].P.Scale * 3f, 0.5f);

                        Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                    new List<Color>() { Color.Red * 0.75f, Color.Orange * 0.75f, Color.White * 0.75f, Color.LightGoldenrodYellow * 0.75f },
                                    50, Balls[i].P.TruePosition, rand, 0.5f, Balls[i].P.Scale.X * 3f, Vector2.One * 8f, Vector2.One * 16f, 0.75f);
                    }
                    //if (Balls[i].ExplosionTime == 0f)
                    //    Balls[i].ExplosionTime = Balls[i].P.TimeAlive;

                    //float mult = (Balls[i].P.TimeAlive - Balls[i].ExplosionTime);
                    //Balls[i].P.Scale = Vector2.One * (18f + (18f * UsefulMethods.FindBetween(mult * 4f, 1f, 0f, mult, 0f, false, 0.5f) * 50f));
                    //Balls[i].P.Velocity = Vector2.Zero;

                    //Balls[i].P.Tint = Color.White;
                    //Balls[i].P.Alpha = UsefulMethods.FindBetween(mult * 4f, 1f, 0f, 1f, 0f, true);
                    //Balls[i].P.Tex = new SkeletonTexture("Pong", "Explosion");

                    //if (mult > 0.25f)
                        Balls.RemoveAt(i);
                }
            }

            CheckCollisions();


            //TotalScore = 0f;

            int maxCount = (MaxBalls / 2) - Balls.Count;
            int count = 1;

            foreach (Skill S1 in SkillTree.Skills.Values)
                count += S1.Level;

            count = count - Balls.Count;

            for (int c = 0; c < count; c++)
            {
                Vector2 Velocity = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
                Velocity.Normalize();
                Velocity *= 250f;

                AddBalls(new Vector2(GraphicsManager.GameResolution.X / 2f, GraphicsManager.GameResolution.Y / 2f), Velocity, 1);
            }

            for (int i = 0; i < Balls.Count; i++)
                foreach (KeyValuePair<string, Particle> P in Balls[i].Modifiers)
                {
                    P.Value.Position = Balls[i].P.Position;
                    P.Value.PositionModifier = Balls[i].P.PositionModifier;
                    P.Value.Alpha = Balls[i].P.Alpha;
                    P.Value.Tint = Balls[i].P.Tint;
                    P.Value.Scale = Balls[i].P.Scale;
                    Vector2 normal;

                    switch (P.Key)
                    {
                        case "Sine1":
                            P.Value.Scale *= 1.5f;
                            P.Value.Rotation -= Balls[i].P.Velocity.Length() * GlobalVariables.WorldTime * 0.01f;
                            P.Value.Tint = Color.White;
                            P.Value.Alpha = Balls[i].P.Alpha * 0.5f;
                            break;

                        case "Sine2":
                            P.Value.Scale *= 1.25f;
                            P.Value.Rotation += Balls[i].P.Velocity.Length() * GlobalVariables.WorldTime * 0.01f;
                            P.Value.Tint = Color.Gray;
                            P.Value.Alpha = Balls[i].P.Alpha * 0.5f;
                            break;

                        case "Explosion":
                            P.Value.Rotation = Balls[i].P.Rotation;
                            break;

                        case "Implosion":
                            P.Value.Rotation = Balls[i].P.Rotation;
                            P.Value.Tint = Color.White;
                            break;

                        case "Indecisive":
                            P.Value.Rotation = Balls[i].P.Rotation;
                            break;
                            
                        case "Worm":
                            normal = -UsefulMethods.AngleToVector(Balls[i].P.Rotation);

                            P.Value.Position += normal * (Balls[i].P.Scale.Y / 2f);
                            P.Value.Rotation = Balls[i].P.Rotation;
                            P.Value.Scale *= 1.5f;
                            P.Value.Tint = Color.White;
                            break;

                        case "Combining":
                            if (Balls[i].EatTimer > 0.5f)
                                P.Value.Tex = new SkeletonTexture("Pong", "CombiningBallOpen");
                            else
                                P.Value.Tex = new SkeletonTexture("Pong", "CombiningBallClosed");

                            P.Value.Rotation = Balls[i].P.Rotation;
                            P.Value.Tint = Color.White;
                            break;

                        case "Gravity":
                            normal = -UsefulMethods.AngleToVector(P.Value.Rotation);

                            P.Value.Position += normal * (Balls[i].P.Scale.Y);
                            P.Value.Rotation += 4f * GlobalVariables.WorldTime;;
                            P.Value.Scale = Balls[i].P.Scale * 0.5f;
                            break;

                    }
                }
        }

        private void AddBalls(Vector2 Location, Vector2 Velocity, int Count)
        {
            BallBank.Clear();
            for (int o = 0; o < Count; o++)
            {
                //if (BallBank.Count > 0)
                //{
                //    int i = Balls.Count;

                //    Balls.Add(BallBank[0]);
                //    BallBank.RemoveAt(0);
                //    Balls[i].P.Velocity = Velocity;
                //    Balls[i].P.Position = Location;
                //    Balls[i].P.PositionModifier = Vector2.Zero;
                //}
                //else
                {
                    int i = Balls.Count;

                    Balls.Add(new Ball(1 + PixelsPerSecond));



                    Balls[i].P.Scale = Vector2.One * 18f;
                    Balls[i].P.Tex = new SkeletonTexture("Pong", "Ball");
                    Balls[i].P.RotateWithMovement = true;
                    Balls[i].P.Velocity = Velocity;
                    Balls[i].P.Tint = Color.White;
                    Balls[i].P.Position = Location;

                    BallTimer = 0f;

                    #region Bonus Ball
                    float Chance = UsefulMethods.FindBetween(SkillTree.Skills["Bonus Ball"].Level, SkillTree.Skills["Bonus Ball"].Cost.Count, 0, 0.5f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].P.Glow = true;
                        Balls[i].Types.Add("Bonus");
                    }
                    #endregion

                    #region Indecisive Ball
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 2"].Level, SkillTree.Skills["Special Ball 2"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 2"].Level * 2f;
                        Balls[i].Types.Add("Right Angle");
                        Balls[i].Modifiers.Add("Indecisive", new Particle());
                        Balls[i].Modifiers["Indecisive"].Tex = new SkeletonTexture("Pong", "IndecisiveBall");
                        Balls[i].P.Movement.Add(new PM.RightAngle(0.1f, 3f, MathHelper.Pi / 2f, rand));
                        Balls[i].P.Movement.Add(new PM.Freeze(0.1f, 2f, 0.33f, rand));
                    }
                    #endregion

                    #region ImplosionBall
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 4"].Level, SkillTree.Skills["Special Ball 4"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 4"].Level * 2f;
                        Balls[i].Types.Add("Implosion");
                        Balls[i].Modifiers.Add("Implosion", new Particle());
                        Balls[i].Modifiers["Implosion"].Tex = new SkeletonTexture("Pong", "ImplosionBall");
                        Balls[i].BombTime = UsefulMethods.RandomBetween(10f, 0.1f);

                        //Balls[i].particleEmitters.Add(new ParticleEmitter(rand, 0.25f, 50f, 75));
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "000"), Color.LightBlue);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "001"), Color.LightBlue);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "002"), Color.LightBlue);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "003"), Color.LightBlue);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "000"), Color.Cyan);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "001"), Color.Cyan);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "002"), Color.Cyan);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "003"), Color.Cyan);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "000"), Color.White);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "001"), Color.White);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "002"), Color.White);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "003"), Color.White);
                    }
                    #endregion

                    #region Explosion Ball
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 3"].Level, SkillTree.Skills["Special Ball 3"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 3"].Level * 2f;
                        Balls[i].Types.Add("Explosion");
                        Balls[i].Modifiers.Add("Explosion", new Particle());
                        Balls[i].Modifiers["Explosion"].Tex = new SkeletonTexture("Pong", "ExplosionBall");
                        Balls[i].BombTime = UsefulMethods.RandomBetween(10f, 0.1f);

                        //Balls[i].particleEmitters.Add(new ParticleEmitter(rand, 0.25f, 50f, 75));
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "000"), Color.OrangeRed);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "001"), Color.OrangeRed);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "002"), Color.OrangeRed);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "003"), Color.OrangeRed);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "000"), Color.Orange);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "001"), Color.Orange);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "002"), Color.Orange);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "003"), Color.Orange);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "000"), Color.Gold);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "001"), Color.Gold);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "002"), Color.Gold);
                        //Balls[i].particleEmitters[Balls[i].particleEmitters.Count - 1].AddTextures(new SkeletonTexture("Particles-Dust", "003"), Color.Gold);
                    }
                    #endregion

                    #region Worm Ball
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 5"].Level, SkillTree.Skills["Special Ball 5"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 1"].Level * 2f;
                        Balls[i].Types.Add("Worm");

                        Balls[i].Modifiers.Add("Worm", new Particle());
                        Balls[i].Modifiers["Worm"].Tex = new SkeletonTexture("Pong", "WormBall");
                        Balls[i].P.Movement.Add(new PM.Wave(rand, -1f, 0.0025f, 0.8f, WaveTypeEnum.Square, 0f));
                    }
                    #endregion

                    #region Combining Ball
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 6"].Level, SkillTree.Skills["Special Ball 6"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 6"].Level * 2f;
                        Balls[i].Types.Add("Combining");

                        Balls[i].Modifiers.Add("Combining", new Particle());
                        Balls[i].Modifiers["Combining"].Tex = new SkeletonTexture("Pong", "CombiningBallOpen");
                    }
                    #endregion

                    #region Sine Ball
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 1"].Level, SkillTree.Skills["Special Ball 1"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 1"].Level * 2f;
                        Balls[i].Types.Add("Sine");

                        Balls[i].Modifiers.Add("Sine1", new Particle());
                        Balls[i].Modifiers["Sine1"].Tex = new SkeletonTexture("Pong", "SineBall");
                        Balls[i].Modifiers.Add("Sine2", new Particle());
                        Balls[i].Modifiers["Sine2"].Tex = new SkeletonTexture("Pong", "SineBall");
                        Balls[i].P.Movement.Add(new PM.Wave(rand, 0.75f, 0.0025f, 0.8f, WaveTypeEnum.Sine, MathHelper.Pi / 2f));
                    }
                    #endregion

                    #region Gravity Ball
                    Chance = UsefulMethods.FindBetween(SkillTree.Skills["Special Ball 7"].Level, SkillTree.Skills["Special Ball 7"].Cost.Count, 0, 0.25f, 0f, false, 0.5f);
                    if (rand.NextDouble() < Chance)
                    {
                        Balls[i].Value *= SkillTree.Skills["Special Ball 7"].Level * 2f;
                        Balls[i].Types.Add("Gravity");

                        Balls[i].Modifiers.Add("Gravity", new Particle());
                        Balls[i].Modifiers["Gravity"].Tex = new SkeletonTexture("Pong", "Ball");
                    }
                    #endregion
                }
            }
        }
        
        private BigDecimal GetBallValue(int i)
        {
            float maxTime = 600f;
            float maxMult = 10f;

            float J = UsefulMethods.FindBetween(Balls[i].P.TimeAlive, maxTime, 0f, maxMult, 1f, false, 0.5f);
            BigDecimal Value = Balls[i].Value * J;

            for (int t = 0; t < Balls[i].Types.Count; t++)
            {
                switch (Balls[i].Types[t])
                {
                    case "Bonus":
                        float M = UsefulMethods.FindBetween(SkillTree.Skills["Bonus Ball"].Level, SkillTree.Skills["Bonus Ball"].Cost.Count, 0f, 100f, 1f, false);

                        Value *= M;
                        break;
                }
            }

            return Value;
        }

        #endregion

        #region Objects
        
        private void UpdateObjects()
        {
            Vector2 RandomPosMin = new Vector2(GraphicsManager.GameResolution.X * 0.05f, GraphicsManager.GameResolution.Y * 0.05f);
            Vector2 RandomPosMax = GraphicsManager.GameResolution - RandomPosMin;
            
            LoadObjects();

            for (int i = 0; i < Objects.Count; i++)
            {
                UpdateObject(i);

                Objects[i].Timer += GlobalVariables.WorldTime;

                if (!Objects[i].Active && Objects[i].Timer > Objects[i].Frequency)
                    InitializeObject(i, UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax));                
                else if (Objects[i].Active && Objects[i].Timer > Objects[i].Duration)                
                    ResetObject(i);
                                
                if (Objects[i].Active)
                    ObjectAlphaControl(i);
            }
        }
        
        private void LoadObjects()
        {
            #region Count Objects

            int TotalVortex = 0;
            int TotalTurret = 0;
            int TotalWall = 0;
            int TotalBumper = 0;
            int TotalPaddleUp = 0;
            int TotalPaddleArea = 0;
            int TotalBallUp = 0;
            int TotalBallArea = 0;

            for (int i = 0; i < Objects.Count; i++)
            {
                switch (Objects[i].MainType)
                {
                    case "Vortex":
                        TotalVortex++;
                        break;

                    case "Turret":
                        TotalTurret++;
                        break;

                    case "Wall":
                        TotalWall++;
                        break;

                    case "Bumper":
                        TotalBumper++;
                        break;

                    case "PaddleUp":
                        TotalPaddleUp++;
                        break;

                    case "PaddleArea":
                        TotalPaddleArea++;
                        break;

                    case "BallUp":
                        TotalBallUp++;
                        break;

                    case "BallArea":
                        TotalBallArea++;
                        break;
                }
            }

            #endregion

            int MaxObjects = 1;
            Vector2 RandomPosMin = new Vector2(GraphicsManager.GameResolution.X * 0.05f, GraphicsManager.GameResolution.Y * 0.05f);
            Vector2 RandomPosMax = GraphicsManager.GameResolution - RandomPosMin;

            #region Vortex

            if (SkillTree.Skills["Vortex"].Level > 0)
            {
                while (TotalVortex < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "Vortex"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "Vortex"));
                    TotalVortex++;
                }
            }
            else if (TotalVortex > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "Vortex" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion

            #region Turret

            if (SkillTree.Skills["Turret"].Level > 0)
            {
                while (TotalTurret < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "Turret"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "Turret"));
                    TotalTurret++;
                }

                if (TotalTurret > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "Turret" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalTurret > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "Turret" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion
            
            #region Wall

            if (SkillTree.Skills["Wall"].Level > 0)
            {
                while (TotalWall < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "Wall"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "Wall"));
                    TotalWall++;
                }

                if (TotalWall > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "Wall" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalWall > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "Wall" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion

            #region Bumper

            if (SkillTree.Skills["Bumper"].Level > 0)
            {
                while (TotalBumper < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "Bumper"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "Bumper"));
                    TotalBumper++;
                }

                if (TotalBumper > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "Bumper" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalBumper > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "Bumper" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion

            #region Paddle-Ups

            if (SkillTree.Skills["Paddle-Ups"].Level > 0)
            {
                while (TotalPaddleUp < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "PaddleUp"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "PaddleUp"));
                    TotalPaddleUp++;
                }

                if (TotalPaddleUp > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "PaddleUp" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalPaddleUp > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "PaddleUp" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion

            #region Paddle-Ups Area

            if (SkillTree.Skills["Paddle-Ups Area"].Level > 0)
            {
                while (TotalPaddleArea < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "PaddleArea"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "PaddleArea"));
                    TotalPaddleArea++;
                }

                if (TotalPaddleArea > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "PaddleArea" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalPaddleArea > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "PaddleArea" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion

            #region Ball-Ups

            if (SkillTree.Skills["Ball-Ups"].Level > 0)
            {
                while (TotalBallUp < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "BallUp"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "BallUp"));
                    TotalBallUp++;
                }

                if (TotalBallUp > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "BallUp" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalBallUp > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "BallUp" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion

            #region Ball-Ups Area

            if (SkillTree.Skills["Ball-Ups Area"].Level > 0)
            {
                while (TotalBallArea < MaxObjects)
                {
                    Objects.Add(new PongObject(new SkeletonTexture("Pong", "BallArea"), UsefulMethods.RandomPosRectangle(RandomPosMin, RandomPosMax), "BallArea"));
                    TotalBallArea++;
                }

                if (TotalBallArea > MaxObjects)
                    for (int i = 0; i < Objects.Count; i++)
                        if (Objects[i].MainType == "BallArea" && !Objects[i].Active)
                        {
                            Objects.RemoveAt(i);
                            i--;
                        }
            }
            else if (TotalBallArea > 0)
                for (int i = 0; i < Objects.Count; i++)
                    if (Objects[i].MainType == "BallArea" && !Objects[i].Active)
                    {
                        Objects.RemoveAt(i);
                        i--;
                    }

            #endregion
        }

        private void InitializeObject(int i, Vector2 RandomPosition)
        {
            Objects[i].Timer = 0f;
            Objects[i].Active = true;
            Objects[i].P.Position = RandomPosition;
            Objects[i].P.Alpha = 0f;

            switch (Objects[i].MainType)
            {
                #region Vortex
                case "Vortex":
                    if (SkillTree.Skills["Portal"].Level > 0 && rand.NextDouble() > 0.5f)
                    {
                        Objects[i].Types.Add("Portal");

                        Objects[i].Modifiers.Add("Portal", new Particle());
                        Objects[i].Modifiers["Portal"].Tex = new SkeletonTexture("Pong", "Portal");
                        Objects[i].Modifiers["Portal"].RotationalVelocity = ((float)rand.NextDouble() * 1f) + 0.5f;
                        if (rand.Next(0, 2) == 0)
                            Objects[i].Modifiers["Portal"].RotationalVelocity *= -1f;
                        Objects[i].Modifiers["Portal"].Tint = ColorManager.RandomColor();

                        Objects[i].Floats.Add("PortalMultiplier", (SkillTree.Skills["Portal Extra Ball"].Level / 2) + 2);
                        Objects[i].Floats.Add("PortalTimer", 0f);

                        if (SkillTree.Skills["Portal Delay"].Level == 10)
                            Objects[i].Floats.Add("PortalFrequency", 0.05f);
                        else
                            Objects[i].Floats.Add("PortalFrequency", 1f - SkillTree.Skills["Portal Delay"].Level * 0.1f);
                    }


                    if (SkillTree.Skills["Vortex"].Level > 0 && rand.NextDouble() > 0.5f)
                    {
                        Objects[i].Types.Add("Vortex");

                        Objects[i].Modifiers.Add("Vortex", new Particle());
                        Objects[i].Modifiers["Vortex"].Tex = new SkeletonTexture("Pong", "Vortex");
                        Objects[i].Modifiers["Vortex"].RotationalVelocity = ((float)rand.NextDouble() * 1f) + 0.5f;
                        if (rand.Next(0, 2) == 0)
                            Objects[i].Modifiers["Vortex"].RotationalVelocity *= -1f;
                        Objects[i].Modifiers["Vortex"].Tint = ColorManager.RandomColor();
                    }

                    if (SkillTree.Skills["Black Hole"].Level > 0 && rand.NextDouble() > 0.5f)
                    {
                        Objects[i].Types.Add("Black Hole");

                        Objects[i].Modifiers.Add("Black Hole", new Particle());
                        Objects[i].Modifiers["Black Hole"].Tex = new SkeletonTexture("Pong", "Black Hole");
                        Objects[i].Modifiers["Black Hole"].RotationalVelocity = ((float)rand.NextDouble() * 1f) + 0.5f;
                        if (rand.Next(0, 2) == 0)
                            Objects[i].Modifiers["Black Hole"].RotationalVelocity *= -1f;
                        Objects[i].Modifiers["Black Hole"].Tint = ColorManager.RandomColor();

                        Objects[i].Floats.Add("BlackHoleMultiplier", SkillTree.Skills["Black Hole Value Increase"].Level + 1);
                    }

                    Objects[i].P.Tex = new SkeletonTexture("Pong", "VortexBack");
                    Objects[i].P.Tint = ColorManager.RandomColor();
                    Objects[i].P.RotationalVelocity = ((float)rand.NextDouble() * 1f) + 0.5f;
                    if (rand.Next(0, 2) == 0)
                        Objects[i].P.RotationalVelocity *= -1f;


                    break;
                #endregion

                #region Turret
                case "Turret":
                    Objects[i].Types.Add("TurretMain");
                    Objects[i].P.Tex = new SkeletonTexture("Pong", "TurretBack");
                    Objects[i].P.Tint = Color.White;
                    Objects[i].P.RotationalVelocity = (float)rand.NextDouble() + 0.5f;
                    if (rand.Next(0, 2) == 0)
                        Objects[i].P.RotationalVelocity *= -1f;

                    if (rand.Next(0, 2) == 0)
                    {
                        Objects[i].Types.Add("Turret");

                        Objects[i].Floats.Add("TurretFireRate", 0.1f);

                        Objects[i].Floats.Add("TurretTimer", 0f);
                        Objects[i].Modifiers.Add("Turret", new Particle());
                        Objects[i].Modifiers["Turret"].Tex = new SkeletonTexture("Pong", "Turret");
                        Objects[i].Modifiers["Turret"].Tint = Color.White;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Objects[i].Types.Add("TurretUltra");

                        Objects[i].Floats.Add("TurretUltraFireRate", 0.1f);
                        Objects[i].Floats.Add("TurretUltraCount", 5f);

                        Objects[i].Floats.Add("TurretUltraTimer", 0f);
                        Objects[i].Modifiers.Add("TurretUltra", new Particle());
                        Objects[i].Modifiers["TurretUltra"].Tex = new SkeletonTexture("Pong", "TurretUltra");
                        Objects[i].Modifiers["TurretUltra"].Tint = Color.White;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Objects[i].Types.Add("TurretGold");

                        Objects[i].Floats.Add("TurretGoldValue", 10f);

                        Objects[i].Modifiers.Add("TurretGold", new Particle());
                        Objects[i].Modifiers["TurretGold"].Tex = new SkeletonTexture("Pong", "TurretGold");
                        Objects[i].Modifiers["TurretGold"].Tint = Color.Gold;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Objects[i].Types.Add("TurretVacuum");

                        Objects[i].Floats.Add("TurretVacuumClone", 10f);

                        Objects[i].Modifiers.Add("TurretVacuum", new Particle());
                        Objects[i].Modifiers["TurretVacuum"].Tex = new SkeletonTexture("Pong", "TurretVacuum");
                        Objects[i].Modifiers["TurretVacuum"].Tint = Color.White;
                    }

                    break;
                #endregion

                #region Wall
                case "Wall":
                    Objects[i].P.Tex = new SkeletonTexture("Core", "Marker");
                    Objects[i].P.Tint = Color.White;
                    Objects[i].P.RotationalVelocity = 0f;
                    Objects[i].P.Rotation = (float)rand.NextDouble() * MathHelper.TwoPi;
                    break;
                #endregion

                #region Bumper
                case "Bumper":
                    Objects[i].P.Tex = new SkeletonTexture("Pong", "Bumper");
                    Objects[i].P.Tint = ColorManager.RandomColor();
                    Objects[i].P.RotationalVelocity = 0f;
                    Objects[i].P.Rotation = (float)rand.NextDouble() * MathHelper.TwoPi;
                    break;
                #endregion

                #region PaddleUp
                case "PaddleUp":
                    Objects[i].P.Tex = new SkeletonTexture("Pong", "PaddleUp");
                    Objects[i].P.Tint = ColorManager.RandomColor();
                    Objects[i].P.RotationalVelocity = 0f;
                    break;
                #endregion

                #region PaddleArea
                case "PaddleArea":
                    Objects[i].P.Tex = new SkeletonTexture("Pong", "PaddleArea");
                    Objects[i].P.Tint = ColorManager.RandomColor();
                    Objects[i].P.RotationalVelocity = 0f;
                    break;
                #endregion

                #region BallUp
                case "BallUp":
                    Objects[i].P.Tex = new SkeletonTexture("Pong", "BallUp");
                    Objects[i].P.Tint = ColorManager.RandomColor();
                    Objects[i].P.RotationalVelocity = 0f;
                    break;
                #endregion

                #region BallArea
                case "BallArea":
                    Objects[i].P.Tex = new SkeletonTexture("Pong", "BallArea");
                    Objects[i].P.Tint = ColorManager.RandomColor();
                    Objects[i].P.RotationalVelocity = 0f;
                    break;
                #endregion

            }
        }

        private void ResetObject(int i)
        {
            switch (Objects[i].MainType)
            {
                case "Vortex":
                    for (int t = 0; t < Objects[i].Types.Count; t++)
                        switch (Objects[i].Types[t])
                        {
                            case "Black Hole":
                                if (Objects[i].CollectedBalls.Count > 0)
                                {
                                    for (int o = 0; o < Objects[i].CollectedBalls.Count; o++)
                                    {
                                        Vector2 normal = Vector2.Transform(new Vector2(0f, -1f), Matrix.CreateRotationZ((float)rand.NextDouble() * MathHelper.TwoPi));

                                        Objects[i].CollectedBalls[o].P.TruePosition = Objects[i].P.Position;
                                        Objects[i].CollectedBalls[o].P.Velocity = normal * (256f + ((float)rand.NextDouble() * 512f));
                                    }

                                    Balls.AddRange(Objects[i].CollectedBalls);
                                    Objects[i].CollectedBalls.Clear();

                                    //GlobalVariables.ActivateSpeedChange(5f, 0.05f, 0.05f, 0.9f);
                                }
                                break;
                        }
                    break;


                case "Turret":
                    break;

                case "Wall":
                    break;

                case "Bumper":
                    break;

                case "PaddleUp":
                    break;

                case "PaddleArea":
                    break;

                case "BallUp":
                    break;

                case "BallArea":
                    break;
            }

            Objects[i].Floats.Clear();
            Objects[i].Modifiers.Clear();
            Objects[i].Types.Clear();
            Objects[i].Timer = 0f;
            Objects[i].Active = false;
            Objects[i].Frequency = Objects[i].FullFrequency - ((float)rand.NextDouble() * (Objects[i].FullFrequency / 2f));
            Objects[i].P.Rotation = 0f;
            Objects[i].P.Alpha = 0f;

            if (Objects[i].CollectedBalls.Count > 0)
            {
                BallBank.AddRange(Objects[i].CollectedBalls);
                Objects[i].CollectedBalls.Clear();
            }
        }


        private void UpdateObject(int i)
        {
            if (Objects[i].Active)
            {
                UpdateObjectDuration(i);

                #region Types
                for (int t = 0; t < Objects[i].Types.Count; t++)
                    switch (Objects[i].Types[t])
                    {
                        case "Black Hole":
                            float TimeLeft = UsefulMethods.FindBetween(Objects[i].Timer, Objects[i].Duration, 0f, 2f, 0.1f, false);
                            Objects[i].Modifiers["Black Hole"].Rotation += Objects[i].Modifiers["Black Hole"].RotationalVelocity * 5f * TimeLeft * TimeLeft * GlobalVariables.WorldTime;

                            Objects[i].P.Scale = Vector2.One * UsefulMethods.FindBetween(TimeLeft * TimeLeft, 4f, 0f, 175f, 0.1f, false);

                            Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                        new List<Color>() { Color.White * Objects[i].P.Alpha, Color.Gray * Objects[i].P.Alpha, Color.LightGray * Objects[i].P.Alpha },
                                        6f, Objects[i].P.TruePosition, rand, 0.6f, Objects[i].P.Scale.Length() * 0.6f, Vector2.One * 8f, Vector2.One * 16f, 0.75f);
                            break;

                        case "Portal":
                            Objects[i].Floats["PortalTimer"] += GlobalVariables.WorldTime;

                            Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                        new List<Color>() { Color.BlueViolet * Objects[i].P.Alpha, Color.Cyan * Objects[i].P.Alpha, Color.Violet * Objects[i].P.Alpha, Color.LightBlue * Objects[i].P.Alpha },
                                        10f, Objects[i].P.TruePosition, rand, 0.6f, Objects[i].P.Scale.Length() * 0.6f, Vector2.One * 8f, Vector2.One * 16f, 0.75f);

                            if (Objects[i].Floats["PortalTimer"] > Objects[i].Floats["PortalFrequency"] && Objects[i].P.Alpha > 0.33f)
                            {
                                Objects[i].Floats["PortalTimer"] = 0f;
                                PortalBall(i);
                            }
                            break;

                        case "Turret":
                            if (Objects[i].Floats["TurretTimer"] > Objects[i].Floats["TurretFireRate"])
                            {
                                AddBalls(Objects[i].P.TruePosition, Vector2.Transform(new Vector2(0, -1f), Matrix.CreateRotationZ(Objects[i].P.Rotation)), 1);
                                Balls[Balls.Count - 1].P.Velocity.Normalize();
                                Balls[Balls.Count - 1].P.Position += Balls[Balls.Count - 1].P.Velocity * 32f;

                                Balls[Balls.Count - 1].P.Velocity *= 256f;
                                Balls[Balls.Count - 1].Owner = Objects[i].Owner;
                                Balls[Balls.Count - 1].P.Tint = Objects[i].P.Tint;

                                if (Objects[i].Types.Contains("TurretGold"))
                                {
                                    Balls[Balls.Count - 1].P.Glow = true;
                                    Balls[Balls.Count - 1].Types.Add("Bonus");
                                }

                                Objects[i].Floats["TurretTimer"] = 0f;
                            }

                            Objects[i].Floats["TurretTimer"] += GlobalVariables.WorldTime;
                            break;

                        case "TurretUltra":
                            if (Objects[i].Floats["TurretUltraTimer"] > Objects[i].Floats["TurretUltraFireRate"])
                            {
                                AddBalls(Objects[i].P.TruePosition, Vector2.Transform(new Vector2(0, 1f), Matrix.CreateRotationZ(Objects[i].P.Rotation)), 1);
                                Balls[Balls.Count - 1].P.Velocity.Normalize();
                                Balls[Balls.Count - 1].P.Position += Balls[Balls.Count - 1].P.Velocity * 32f;

                                Balls[Balls.Count - 1].P.Velocity *= 512f;
                                Balls[Balls.Count - 1].Owner = Objects[i].Owner;
                                Balls[Balls.Count - 1].P.Tint = Objects[i].P.Tint;

                                if (Objects[i].Types.Contains("TurretGold"))
                                {
                                    Balls[Balls.Count - 1].P.Glow = true;
                                    Balls[Balls.Count - 1].Types.Add("Bonus");
                                }

                                Objects[i].Floats["TurretUltraTimer"] = 0f;

                                for (int num = 0; num < (int)Objects[i].Floats["TurretUltraCount"]; num++)
                                {
                                    Balls[Balls.Count - 1].CollectedBalls.Add(Balls[Balls.Count - 1].Clone());
                                    Balls[Balls.Count - 1].CollectedBalls[Balls[Balls.Count - 1].CollectedBalls.Count - 1].Owner = Objects[i].Owner;
                                }

                                Balls[Balls.Count - 1].P.Scale *= 1.5f;
                                Balls[Balls.Count - 1].Types.Add("Mine");
                                Balls[Balls.Count - 1].BombTime = 0.75f;
                            }

                            Objects[i].Floats["TurretUltraTimer"] += GlobalVariables.WorldTime;
                            break;

                        case "TurretVacuum":
                            if (Objects[i].CollectedBalls.Count > 0)
                            {
                                float coneSize = MathHelper.Pi / 8f;
                                Vector2 Direction = UsefulMethods.AngleToVector(Objects[i].P.Rotation + MathHelper.PiOver2);
                                Direction = Vector2.Transform(Direction, Matrix.CreateRotationZ(-coneSize / 2f));
                                float coneIncrease = coneSize / (Objects[i].Floats["TurretVacuumClone"] * Objects[i].CollectedBalls.Count);
                                
                                Vector2 newDirection = Direction;

                                //for (int b = 0; b < Objects[i].CollectedBalls.Count; b++)
                                if (Objects[i].CollectedBalls.Count > 0)
                                {
                                    int b = 0;

                                    for (int num = 0; num < (int)Objects[i].Floats["TurretVacuumClone"]; num++)
                                    {
                                        Balls.Add(Objects[i].CollectedBalls[b].Clone());
                                        Balls[Balls.Count - 1].Owner = Objects[i].Owner;

                                        newDirection = Vector2.Transform(Direction, Matrix.CreateRotationZ(coneIncrease * num));

                                        Balls[Balls.Count - 1].P.Position = Objects[i].P.TruePosition + (newDirection * 32f);
                                        Balls[Balls.Count - 1].P.Velocity = newDirection * 256f;
                                        Balls[Balls.Count - 1].Owner = Objects[i].Owner;

                                        if (Objects[i].Owner != null)
                                            Balls[Balls.Count - 1].P.Tint = Objects[i].Owner.PrimaryColor;
                                    }
                                    Objects[i].CollectedBalls.RemoveAt(b);
                                }
                            }

                            break;
                    }
                #endregion

                Objects[i].P.Update();

                foreach (KeyValuePair<string, Particle> P in Objects[i].Modifiers)
                {
                    switch (P.Key)
                    {
                        case "Turret":
                            P.Value.Rotation = Objects[i].P.Rotation;
                            P.Value.Tint = Objects[i].P.Tint;
                            break;

                        case "TurretVacuum":
                            P.Value.Rotation = Objects[i].P.Rotation;
                            break;

                        case "TurretGold":
                            P.Value.Rotation = -Objects[i].P.Rotation;
                            break;

                        case "TurretUltra":
                            P.Value.Rotation = Objects[i].P.Rotation;
                            P.Value.Tint = Objects[i].P.Tint;
                            break;
                    }


                    P.Value.Position = Objects[i].P.Position;
                    P.Value.Update();
                }
            }
        }

        private void UpdateObjectDuration(int i)
        {
            switch (Objects[i].MainType)
            {
                case "Vortex":
                    Objects[i].FullFrequency = 310f - (SkillTree.Skills["Vortex Frequency"].Level * 15f);
                    Objects[i].Duration = (5f + SkillTree.Skills["Vortex Duration"].Level);

                    Emitter.EmitParticles(new List<SkeletonTexture>() { new SkeletonTexture("Particles-Dust", "000"), new SkeletonTexture("Particles-Dust", "001"), new SkeletonTexture("Particles-Dust", "002") },
                                new List<Color>() { Color.Purple * Objects[i].P.Alpha, Color.MediumPurple * Objects[i].P.Alpha, Color.Violet * Objects[i].P.Alpha, Color.MediumVioletRed * Objects[i].P.Alpha },
                                3f, Objects[i].P.TruePosition, rand, 0.4f, Objects[i].P.Scale.Length() * 0.6f, Vector2.One * 8f, Vector2.One * 16f, 0.75f);
                    break;

                case "Turret":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;

                case "Wall":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;

                case "Bumper":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;

                case "PaddleUp":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;

                case "PaddleArea":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;

                case "BallUp":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;

                case "BallArea":
                    Objects[i].FullFrequency = 10f;
                    Objects[i].Duration = 10f;
                    break;
            }
        }

        ///// <summary>
        ///// Returns false if ball is destroyed.
        ///// </summary>
        ///// <param name="i"></param>
        ///// <returns></returns>
        //private bool BallObjectInteraction(int i)
        //{
        //    Vector2 normal;
        //    float distance;
        //    PM.ApplyAcceleration acc;

        //    for (int o = 0; o < Objects.Count; o++)

        //        if (Objects[o].Active)
        //        {
        //            normal = Balls[i].P.TruePosition - Objects[o].P.TruePosition;
        //            distance = normal.Length();
        //            if (normal != Vector2.Zero)
        //                normal.Normalize();

        //            for (int t = 0; t < Objects[o].Types.Count; t++)
        //                switch (Objects[o].Types[t])
        //                {
        //                    case "Vortex":
        //                        if (distance < 128f)
        //                            distance = 128f;

        //                        //acc = new PM.ApplyAcceleration(-normal * ((1e+7f * Objects[o].P.Alpha) / (distance * distance)));

        //                        float TimeLeft = UsefulMethods.FindBetween(Objects[o].Timer, Objects[o].Duration, 0f, 2f, 0.1f, false);
        //                        acc = new PM.ApplyAcceleration(-normal * (((5e+6f * Objects[o].P.Alpha * Objects[o].P.Scale.Length() * TimeLeft * TimeLeft) / 25f) / (distance * distance)));
        //                        acc.Update(Balls[i].P);

        //                        //acc.Update(Balls[i].P);

        //                        break;

        //                    case "Black Hole":
        //                        if (distance < Objects[o].P.Scale.Length() / 8f)
        //                        {
        //                            Balls[i].Value += Objects[o].BlackHoleMultiplier * PixelsPerSecond;
        //                            Balls[i].EatTimer = 0f;

        //                            if (TotalBalls >= MaxBalls)
        //                                BallBank.Add(Balls[i]);
        //                            else
        //                                Objects[o].CollectedBalls.Add(Balls[i]);
        //                            Balls.RemoveAt(i);
        //                            return false;
        //                        }

        //                        if (distance < 256f)
        //                            Balls[i].P.Position += -normal * (512f) * GlobalVariables.WorldTime;
        //                        break;

        //                    case "TurretMain":
        //                        if (distance < (Objects[o].P.Scale.X / 3f) + (Balls[i].P.Scale.X / 2f))
        //                        {
        //                            Vector2 collisionNormal = Objects[o].P.TruePosition - Balls[i].P.TruePosition;
        //                            if (collisionNormal != Vector2.Zero)
        //                                collisionNormal *= 1f / collisionNormal.Length();

        //                            float Intersection = Vector2.Distance(Objects[o].P.TruePosition, Balls[i].P.TruePosition)
        //                            - ((Objects[o].P.Scale.X / 3f) + (Balls[i].P.Scale.X / 2f));

        //                            Balls[i].P.Position += collisionNormal * Intersection;
        //                            Balls[i].P.Velocity = -collisionNormal * Balls[i].P.Velocity.Length();

        //                            Objects[o].Owner = Balls[i].Owner;
        //                            Objects[o].P.Tint = Balls[i].P.Tint;
        //                        }
        //                        break;
        //                }

        //        }


        //    return true;
        //}





        private void ObjectAlphaControl(int i)
        {
            float StartMod = UsefulMethods.FindBetween(Objects[i].Timer, Objects[i].Duration * 0.05f, 0f, 1f, 0f, false);
            float EndMod = UsefulMethods.FindBetween(Objects[i].Timer, Objects[i].Duration, Objects[i].Duration * 0.95f, 1f, 0f, true);

            float FinalMod = StartMod;
            if (StartMod > EndMod)
                FinalMod = EndMod;

            switch (Objects[i].MainType)
            {
                case "Vortex":
                    Objects[i].P.Alpha = FinalMod * 0.9f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 100f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;

                case "Turret":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 64f;

                    foreach (KeyValuePair<string, Particle> P in Objects[i].Modifiers)
                    {
                        P.Value.Alpha = FinalMod * 0.9f;
                        P.Value.Scale = Vector2.One * FinalMod * 64f;
                    }
                    break;

                case "Wall":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = new Vector2(10, 1) * FinalMod * 16f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;

                case "Bumper":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 48f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;

                case "PaddleUp":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 32f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;

                case "PaddleArea":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 100f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;

                case "BallUp":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 32f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;

                case "BallArea":
                    Objects[i].P.Alpha = FinalMod * 1f;
                    Objects[i].P.Scale = Vector2.One * FinalMod * 100f;

                    foreach (Particle P in Objects[i].Modifiers.Values)
                    {
                        P.Alpha = FinalMod * 0.9f;
                        P.Scale = Vector2.One * FinalMod * 100f;
                    }
                    break;
            }
        }

        private void PortalBall(int i)
        {
            int ball = rand.Next(0, Balls.Count);

            Ball CloneBall = Balls[ball].Clone();
            CloneBall.Owner = Balls[ball].Owner;

            for (int n = 0; n < Objects[i].Floats["PortalMultiplier"] + 1; n++)
            {
                //if (TotalBalls >= MaxBalls)
                //{           
                //    //BallBank.Add(CloneBall.Clone());
                //}
                //else
                {
                    Vector2 vel = Vector2.Transform(new Vector2(0, 1), Matrix.CreateRotationZ((float)rand.NextDouble() * MathHelper.TwoPi));

                    CloneBall.P.Position = Objects[i].P.TruePosition + (vel * 48f);
                    CloneBall.P.Velocity = vel * 2000f;

                    Balls.Add(CloneBall.Clone());
                    Balls[Balls.Count - 1].Owner = CloneBall.Owner;
                }
            }

        }


        //private void UpdateObjectPixels()
        //{
        //    BigDecimal VortexBase = 10f * (SkillTree.Skills["Vortex Base Resource"].Level + 1f) * SkillTree.Skills["Vortex"].Level;
        //    BigDecimal VortexEfficiency = 1f;
        //    if (SkillTree.Skills["Vortex Efficiency"].Level > 0)
        //        VortexEfficiency = BigDecimal.Pow(1.4f, SkillTree.Skills["Vortex Efficiency"].Level);

        //    PixelsPerSecond += (VortexBase * VortexEfficiency);
        //}

        #endregion
        
        private void CheckCollisions()
        {
            for (int i = 0; i < Balls.Count; i++)
            {
                float TexSize = Balls[i].P.Scale.X / 2f;
                if (Balls[i].P.Scale.Y / 2f > TexSize)
                    TexSize = Balls[i].P.Scale.Y / 2f;

                

                #region Remove Balls
                
                if (Balls[i].P.TruePosition.X + TexSize < 0 ||
                    Balls[i].P.TruePosition.X - TexSize > GraphicsManager.GameResolution.X ||
                    Balls[i].P.TruePosition.Y + TexSize < 0 ||
                    Balls[i].P.TruePosition.Y - TexSize > GraphicsManager.GameResolution.Y)
                {
                    bool active = false;
                    if (!active)
                    {
                        BigDecimal Value = GetBallValue(i);

                        if (Balls[i].Owner == null)
                        {
                            ScoreCounters.Add(new ScoreCounter(Balls[i].P.Position, Value, Balls[i].Owner, 0.1f));
                            ScoreCounters[ScoreCounters.Count - 1].P.Tint = Color.White * 0.2f;
                        }
                        else
                        {
                            bool ScoreExists = false;

                            for (int s = 0; s < ScoreCounters.Count; s++)
                                if (ScoreCounters[s].Owner == Balls[i].Owner)
                                    if (ScoreCounters[s].Time > ScoreCounters[s].P.TimeAlive)
                                    {
                                        ScoreCounters[s].P.TimeAlive = 0f;
                                        ScoreCounters[s].DesiredAmount += Value;
                                        ScoreExists = true;
                                        break;
                                    }

                            if (!ScoreExists)
                                ScoreCounters.Add(new ScoreCounter(Balls[i].P.Position, Value, Balls[i].Owner, 0.25f));

                            Balls[i].Owner.Resource[ResourceName.Pixel].Amount += Value;
                        }

                        TotalScore[ResourceName.Pixel].Amount += Value;
                        Balls.RemoveAt(i);
                        i--;
                        continue;
                    }
                }

                #endregion

                #region Edge Collision

                if (BorderSize.Left > 0)
                {
                    if (Balls[i].P.TruePosition.X - TexSize < BorderSize.Left)
                    {
                        Balls[i].P.Position.X -= (Balls[i].P.TruePosition.X - TexSize) - BorderSize.Left;
                        Balls[i].P.Velocity.X *= -1;
                    }

                }
                if (BorderSize.Top > 0)
                {
                    if (Balls[i].P.TruePosition.Y - TexSize < BorderSize.Top)
                    {
                        Balls[i].P.Position.Y -= (Balls[i].P.TruePosition.Y - TexSize) - BorderSize.Top;
                        Balls[i].P.Velocity.Y *= -1;
                    }
                }
                if (BorderSize.Right > 0)
                {
                    if (Balls[i].P.TruePosition.X + TexSize > GraphicsManager.GameResolution.X - BorderSize.Right)
                    {
                        Balls[i].P.Position.X -= (Balls[i].P.TruePosition.X + TexSize) - (GraphicsManager.GameResolution.X - BorderSize.Right);
                        Balls[i].P.Velocity.X *= -1;
                    }
                }
                if (BorderSize.Bottom > 0)
                {
                    if (Balls[i].P.TruePosition.Y + TexSize > GraphicsManager.GameResolution.Y - BorderSize.Bottom)
                    {
                        Balls[i].P.Position.Y -= (Balls[i].P.TruePosition.Y + TexSize) - (GraphicsManager.GameResolution.Y - BorderSize.Bottom);
                        Balls[i].P.Velocity.Y *= -1;
                    }
                }

                #endregion

                #region Paddle Collision

                for (int o = 0; o < Paddles.Count; o++)
                {
                    if (Vector2.Distance(Balls[i].P.TruePosition, Paddles[o].Position) < TexSize + Paddles[o].Size)
                    {
                        Rectangle rect1 = Paddles[o].GetRectangle();
                        Rectangle rect2 = new Rectangle((int)(Balls[i].P.TruePosition.X - TexSize), (int)(Balls[i].P.TruePosition.Y - TexSize), (int)(TexSize * 2), (int)(TexSize * 2));

                        if (rect1.Intersects(rect2))
                        {
                            float speed = Balls[i].P.Velocity.Length();
                            Vector2 dis = Balls[i].P.TruePosition - Paddles[o].Position;

                            //Balls[i].P.Position += dis;

                            //dis = Balls[i].P.GetTruePosition() - Paddles[o].Position;
                            dis.Normalize();
                            Balls[i].P.Velocity = dis * speed;
                            Balls[i].P.Velocity += Paddles[o].Velocity * 0.25f;
                            Balls[i].P.Position += Balls[i].P.Velocity * GlobalVariables.WorldTime;

                            Balls[i].Owner = Paddles[o].Owner;
                            Balls[i].P.Tint = Paddles[o].Owner.PrimaryColor;
                            Balls[i].ValueAlpha = 2f;

                            for (int b = 0; b < Balls[i].Types.Count; b++)
                            {
                                Vector2 normal;
                                float distance;

                                switch (Balls[i].Types[b])
                                {
                                    case "Explosion":
                                        if (!Balls[i].Exploded)
                                            Balls[i].P.TimeAlive = Balls[i].BombTime;                                        
                                        break;

                                    case "Implosion":
                                        if (!Balls[i].Exploded)                                        
                                            Balls[i].P.TimeAlive = Balls[i].BombTime;    
                                        break;
                                }
                            }

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

        float pulseTimer;

        public List<Light> GetLights()
        {
            Color color = Color.White;
                //if (WinningPlayer != null)
                //color = WinningPlayer.Owner.PrimaryColor;

            List<Light> Lights = new List<Light>();
            float pulseMax = 0.438f;
            float pulsePower = UsefulMethods.FindBetween(pulseTimer, pulseMax, 0f, 5f, 0, false, 4f) + 0.25f;
            int pulseRadius = (int)((pulsePower / 5f) * 10f) + 100;

            pulsePower = 5f;
            pulseRadius = 100;
            float LightPower = 0.25f;

            Lights.Add(new PointLight()
            {
                IsEnabled = true,
                Color = color.ToVector4(),
                Power = LightPower,
                LightDecay = 1000,
                Position = new Vector3(0, 0, 100)
            });
            Lights.Add(new PointLight()
                {
                    IsEnabled = true,
                    Color = color.ToVector4(),
                    Power = LightPower,
                    LightDecay = 1000,
                    Position = new Vector3(1920, 0, 100)
                });
            Lights.Add(new PointLight()
            {
                IsEnabled = true,
                Color = color.ToVector4(),
                Power = LightPower,
                LightDecay = 1000,
                Position = new Vector3(0, 1080, 100)
            });
            Lights.Add(new PointLight()
            {
                IsEnabled = true,
                Color = color.ToVector4(),
                Power = LightPower,
                LightDecay = 1000,
                Position = new Vector3(1920, 1080, 100)
            });
            Lights.Add(new PointLight()
            {
                IsEnabled = true,
                Color = color.ToVector4(),
                Power = LightPower,
                LightDecay = 1000,
                Position = new Vector3(1920 / 2f, 1080 / 2f, 50)
            });

            for (int i = 0; i < Paddles.Count; i++)
            {
                Lights.Add(new PointLight()
                {
                    IsEnabled = true,
                    Color = Color.White.ToVector4() * 0.5f,
                    Power = 1f,
                    LightDecay = pulseRadius,
                    Position = new Vector3(Paddles[i].Position.X, Paddles[i].Position.Y, 50)
                });
            }



            for (int i = 0; i < Balls.Count; i++)
            {
                if (Balls[i].P.Glow)
                Lights.Add(new PointLight()
                {
                    IsEnabled = true,
                    Color = Balls[i].P.Tint.ToVector4() * 0.2f,
                    Power = pulsePower,
                    LightDecay = pulseRadius,
                    Position = new Vector3(Balls[i].P.TruePosition.X, Balls[i].P.TruePosition.Y, 50)
                });
            }

            if (pulseTimer > pulseMax)
                pulseTimer = 0f;


            return Lights;
        }

        public void DrawEffects(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Camera cam)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Camera cam)
        {
            int humans = 0;
            
            Vector2 GameSize = GraphicsManager.GameResolution;
            SkeletonTexture Tex;
            StaticInfoBox.ClearList();

            cam.SetPosition(GameSize / 2f);
            cam.SetRotation(0f);
            cam.SetZoom(1f);

            //if (Balls.Count > 0)
            //    cam.SetPosition(Balls[0].P.TruePosition);


            new SkeletonTexture("Core", "Marker").Draw(spriteBatch, GameSize / 2f, Color.DarkSlateGray, 0f, GameSize, SpriteEffects.None);

            //spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, (int)GameSize.X, (int)GameSize.Y), Color.Gray);

            if (TotalScore[ResourceName.Pixel].Amount > 1)
                StaticInfoBox.Add(StringManager.ColorString("" + WriteNumber.Write(TotalScore[ResourceName.Pixel].Amount, SeparatorType.Standard, true, ShortenType.Short_Scale), Color.White));

            if (PixelsPerSecond > 0)
                StaticInfoBox.Add(StringManager.ColorString("" + WriteNumber.Write(PixelsPerSecond, SeparatorType.Standard, true, ShortenType.Short_Scale), Color.White) + " Pixels Per Second");

            //StaticInfoBox.Add(StringManager.ColorString("This is a ", Color.Yellow) + StringManager.ItalicString("test") + StringManager.BoldString(" This ") + "IS A TEST");


            //if (TotalScore[ResourceName.Pixel].Amount > 1 || PixelsPerSecond > 0)
            //    StaticInfoBox.Draw(spriteBatch, new Vector2((GraphicsManager.GameResolution.X / 2f), GraphicsManager.GameResolution.Y / 2f), 75f, 0.1f, 0f);
            Color ScoreColor = Color.White;
            if (WinningPlayer != null)
                ScoreColor = WinningPlayer.Owner.PrimaryColor;

            if (TotalScore[ResourceName.Pixel].Amount > 1 || PixelsPerSecond > 0)
                StaticInfoBox.Draw(spriteBatch, new Vector2((GraphicsManager.GameResolution.X / 2f), GraphicsManager.GameResolution.Y / 2f),
                    75f, 0.3f, 0f, new Color(25, 25, 25), new Vector2(16f), new BorderStyle("Default", ScoreColor, new Location(16f)));
            
            //graphicsDevice.Viewport = GraphicsManager.ScaledViewport(new Viewport(cam.viewport));
            //graphicsDevice.PresentationParameters.BackBufferHeight


            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].Active)
                {
                    foreach (KeyValuePair<string, Particle> P in Objects[i].Modifiers)
                    {
                        switch (P.Key)
                        {
                            case "Portal":
                                P.Value.DrawParticle(spriteBatch);
                                break;

                            default:
                                break;
                        }
                    }

                    Objects[i].P.DrawParticle(spriteBatch);

                    foreach (KeyValuePair<string, Particle> P in Objects[i].Modifiers)
                    {
                        switch (P.Key)
                        {
                            case "Portal":
                                break;

                            default:
                                P.Value.DrawParticle(spriteBatch);
                                break;
                        }
                    }
                }
            }

            Emitter.Draw(spriteBatch);

            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, (int)BorderSize.Left, (int)GameSize.Y), Color.White);
            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, (int)GameSize.X, (int)BorderSize.Top), Color.White);
            spriteBatch.Draw(StaticTests.Marker, new Rectangle((int)GameSize.X - (int)BorderSize.Right, 0, (int)BorderSize.Right, (int)GameSize.Y), Color.White);
            spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, (int)GameSize.Y - (int)BorderSize.Bottom, (int)GameSize.X, (int)BorderSize.Bottom), Color.White);








            SkeletonTexture Center = new SkeletonTexture("Pong", "PaddleCenter");
            SkeletonTexture Edge = new SkeletonTexture("Pong", "PaddleEdge");


            for (int i = 0; i < Balls.Count; i++)
            {
                if (Balls[i].P.Glow)
                {
                    Tex = new SkeletonTexture("Pong", "BallGlow");
                    Tex.Draw(spriteBatch, Balls[i].P.TruePosition, Balls[i].P.Tint * Balls[i].P.Alpha * 0.5f, Balls[i].P.Rotation, Balls[i].P.Scale * 1.75f, SpriteEffects.None);
                }

                Balls[i].P.DrawParticle(spriteBatch);

                if (!Balls[i].Exploded)
                    foreach (KeyValuePair<string, Particle> P in Balls[i].Modifiers)
                    {
                                P.Value.DrawParticle(spriteBatch);
                    }
            }

            for (int i = 0; i < ScoreCounters.Count; i++)
            {

                StaticInfoBox.ClearList();
                StaticInfoBox.Add(StringManager.ColorString(WriteNumber.Write(ScoreCounters[i].Amount, SeparatorType.Standard, true, ShortenType.Short_Scale), ScoreCounters[i].P.Tint) + "");

                if (ScoreCounters[i].Owner != null)
                StaticInfoBox.Draw(spriteBatch, ScoreCounters[i].P.TruePosition,
                        ScoreCounters[i].P.Scale.Y, ScoreCounters[i].P.Alpha, ScoreCounters[i].P.Rotation, "Tahoma");
                else
                    StaticInfoBox.Draw(spriteBatch, ScoreCounters[i].P.TruePosition,
                            ScoreCounters[i].P.Scale.Y, ScoreCounters[i].P.Alpha * 3f, 0f, "Tahoma");
            }

            for (int i = 0; i < Paddles.Count; i++)
            {
                //Paddles[i].Owner.Resource[ResourceName.Pixel].Amount = 0;

                Paddles[i].Size = 64;
                float PaddleRotation = 0f;

                switch (Paddles[i].Side)
                {
                    case LocationEnum.Top:
                        PaddleRotation = MathHelper.Pi;
                        break;

                    case LocationEnum.Left:
                        PaddleRotation = MathHelper.PiOver2;
                        break;

                    case LocationEnum.Right:
                        PaddleRotation = MathHelper.Pi * 1.5f;
                        break;
                }


                if (!Paddles[i].Child)
                    Center.Draw(spriteBatch, Paddles[i].Position, Color.White, PaddleRotation, new Vector2(Paddles[i].Size - 16f, Paddles[i].Thickness), SpriteEffects.None);
                else
                    Center.Draw(spriteBatch, Paddles[i].Position, Paddles[i].Tint, PaddleRotation, new Vector2(Paddles[i].Size - 16f, Paddles[i].Thickness), SpriteEffects.None);

                Edge.Draw(spriteBatch, Paddles[i].Position + Vector2.Transform(new Vector2((Paddles[i].Size / 2f) - 8, 0), Matrix.CreateRotationZ(PaddleRotation)), Paddles[i].Tint, PaddleRotation, new Vector2(16f, Paddles[i].Thickness), SpriteEffects.None);
                Edge.Draw(spriteBatch, Paddles[i].Position - Vector2.Transform(new Vector2((Paddles[i].Size / 2f) - 8, 0), Matrix.CreateRotationZ(PaddleRotation)), Paddles[i].Tint, PaddleRotation, new Vector2(16f, Paddles[i].Thickness), SpriteEffects.FlipHorizontally);

                if (!Paddles[i].Child)
                {
                    Edge.Draw(spriteBatch, Paddles[i].Position + Vector2.Transform(new Vector2((Paddles[i].Size / 2f) - 16f, 0), Matrix.CreateRotationZ(PaddleRotation)), Color.White, PaddleRotation, new Vector2(16f, Paddles[i].Thickness), SpriteEffects.None);
                    Edge.Draw(spriteBatch, Paddles[i].Position - Vector2.Transform(new Vector2((Paddles[i].Size / 2f) - 16f, 0), Matrix.CreateRotationZ(PaddleRotation)), Color.White, PaddleRotation, new Vector2(16f, Paddles[i].Thickness), SpriteEffects.FlipHorizontally);
                }

                if (!Paddles[i].Child)
                {
                    StaticInfoBox.ClearList();

                    StaticInfoBox.Add(StringManager.ColorString("" + WriteNumber.Write(Paddles[i].Owner.Resource[ResourceName.Pixel].Amount, SeparatorType.Standard, true, ShortenType.Short_Scale), Paddles[i].Tint));


                    Tex = new SkeletonTexture("Pong", "Crown");

                    //Paddles[i].Owner.Resource[ResourceName.Pixel].Amount.Truncate(3);

                    if (Paddles[i].Owner.Resource[ResourceName.Pixel].Amount > 0)
                        switch (Paddles[i].Side)
                        {
                            case LocationEnum.Left:
                                if (WinningPlayer != null && HumanPlayers > 1)
                                    if (Paddles[i] == WinningPlayer)
                                        Tex.Draw(spriteBatch, Paddles[i].Position + new Vector2(40, 0), Color.Gold, MathHelper.Pi * 0.5f, 24f, SpriteEffects.None);


                                StaticInfoBox.Draw(spriteBatch, Paddles[i].Position + new Vector2(20, 0), 14f, 0.75f, MathHelper.Pi * 0.5f, "Tahoma");
                                break;

                            case LocationEnum.Top:
                                if (WinningPlayer != null && HumanPlayers > 1)
                                    if (Paddles[i] == WinningPlayer)
                                        Tex.Draw(spriteBatch, Paddles[i].Position + new Vector2(0, 40), Color.Gold, MathHelper.Pi, 24f, SpriteEffects.None);

                                StaticInfoBox.Draw(spriteBatch, Paddles[i].Position + new Vector2(0, 20), 14f, 0.75f, 0f, "Tahoma");
                                break;

                            case LocationEnum.Right:
                                if (WinningPlayer != null && HumanPlayers > 1)
                                    if (Paddles[i] == WinningPlayer)
                                        Tex.Draw(spriteBatch, Paddles[i].Position + new Vector2(-40, 0), Color.Gold, MathHelper.Pi * 1.5f, 24f, SpriteEffects.None);

                                StaticInfoBox.Draw(spriteBatch, Paddles[i].Position + new Vector2(-20, 0), 14f, 0.75f, MathHelper.Pi * 1.5f, "Tahoma");
                                break;

                            case LocationEnum.Bottom:
                                if (WinningPlayer != null && HumanPlayers > 1)
                                    if (Paddles[i] == WinningPlayer)
                                        Tex.Draw(spriteBatch, Paddles[i].Position + new Vector2(0, -40), Color.Gold, 0f, 24f, SpriteEffects.None);

                                StaticInfoBox.Draw(spriteBatch, Paddles[i].Position + new Vector2(0, -20), 14f, 0.75f, 0f, "Tahoma");
                                break;
                        }

                    //StaticInfoBox.Draw(spriteBatch, new Vector2((GraphicsManager.GameResolution.X / (HumanPlayers + 1)) * (humans + 1), 50f), 2f, 0.75f, 0f, new Color(0, 0, 0, 0));


                    humans++;
                }
            }

            BorderStyle border = new BorderStyle("Default", Color.White, new Location(64f));
            border.DrawBorders(spriteBatch, GameSize / 2f, GameSize / 2f, 0f, 1f);

        }

        private void DrawBalls(SpriteBatch spriteBatch)
        {

        }

        public void DrawSkillTrees(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Camera cam)
        {
            for (int i = 0; i < Paddles.Count; i++)
            {
                if (Paddles[i].MainTree)
                {
                    Vector2 TreeDisplace = Vector2.Zero;
                    float amount = 30f;
                    Vector2 BoxSize = new Vector2(1280, 720) / 2f;

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




                    Paddles[i].MainSelector.cam.UpdateRenderTarget(rect, graphicsDevice);











                    for (int r = 0; r < 2; r++)
                    {
                        if (r == 0)
                        {
                            DebugOptions.DrawNormals = false;
                            graphicsDevice.SetRenderTarget(Paddles[i].MainSelector.cam.RenderTarget);
                            graphicsDevice.Clear(Color.Transparent);
                        }
                        else
                        {
                            DebugOptions.DrawNormals = true;
                            graphicsDevice.SetRenderTarget(Paddles[i].MainSelector.cam.NormalMap);
                            graphicsDevice.Clear(Color.Transparent);
                        }

                        if (!DebugOptions.DrawNormals)
                            SkillTree.Draw(spriteBatch, graphicsDevice, Paddles[i].MainSelector, rect, cam.RenderTargetUI);
                        else
                            SkillTree.Draw(spriteBatch, graphicsDevice, Paddles[i].MainSelector, rect, cam.NormalMapUI);
                    }

                    List<Light> Lights = SkillTree.GetLights(Paddles[i].MainSelector);
                    LightManager.AmbientBrightness = 0.05f;
                    #region Apply Normals

                    //for (int r = 0; r < 2; r++)
                    //{
                    //    if (r == 0)
                    //        LightManager.StartColorMap(graphicsDevice, Paddles[i].MainSelector.cam.RenderTarget);
                    //    else
                    //        LightManager.StartNormalMap(graphicsDevice);

                    //    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                    //    if (r == 0)
                    //        spriteBatch.Draw(Paddles[i].MainSelector.cam.RenderTarget, Vector2.Zero, Color.White);
                    //    else
                    //        spriteBatch.Draw(Paddles[i].MainSelector.cam.NormalMap, Vector2.Zero, Color.White);

                    //    spriteBatch.End();
                    //}

                    LightManager.DrawFinal(graphicsDevice, spriteBatch, Lights, Paddles[i].MainSelector.cam);

                    #endregion

                    graphicsDevice.SetRenderTarget(cam.NormalMapUI);

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null);
                    spriteBatch.Draw(Paddles[i].MainSelector.cam.NormalMap, rect, Color.White);
                    
                    spriteBatch.End();


                    graphicsDevice.SetRenderTarget(cam.RenderTargetUI);

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null);
                    spriteBatch.Draw(SheetManager.GetRenderTexture("Core", "Marker"), rect, Color.Black);

                    spriteBatch.Draw(Paddles[i].MainSelector.cam.RenderTarget, rect, Color.White);


                    BorderStyle border = new BorderStyle("Default", Color.White, new Location(8f));
                    border.DrawBorders(spriteBatch, new Vector2(rect.X + (rect.Width / 2f), rect.Y + (rect.Height / 2f)), new Vector2(rect.Width / 2f, rect.Height / 2f), 0f, 1f);
                    spriteBatch.End();

                    //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
                    ////spriteBatch.Draw(, rect, Color.White);
                    //spriteBatch.Draw(Paddles[i].MainSelector.cam.RenderTarget, Vector2.Zero, rect, Color.Gold, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    //spriteBatch.End();
                }
            }

            //for (int i = 0; i < Paddles.Count; i++)
            //    if (Paddles[i].SkillTreeOpen && Paddles[i].Player != InputType.None && Paddles[i].Player != InputType.AI)
            //        Paddles[i].SkillTree.Draw(spriteBatch, graphicsDevice);
        }
    }
}
