using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace SkinnerBox
{
    [Serializable()]
    public class Paddle
    {
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
        public float Rotation;

        public Profile Owner;
        public bool Child;

        public SkillSelector MainSelector;
        public SkillSelector PlayerSelector;

        public Paddle(InputType player, LocationEnum side, Profile owner, bool child)
        {
            Owner = owner;
            Child = child;

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

            MainSelector = new SkillSelector(Player, Owner, null);
            PlayerSelector = new SkillSelector(Player, Owner, Owner.Resource);

            if (player != InputType.None && player != InputType.AI)
                InitializeSkillTree();
        }

        private void InitializeSkillTree()
        {
            SkillTree = new SkillCloud();

            SkillTree.AddSkill(null, "Pong", null, true);

            //SkillTree.UpdateCamera(new Rectangle(0, 0, 100, 100));
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


    [Serializable()]
    public class Ball
    {
        public Particle P;
        public Dictionary<string, Particle> Modifiers = new Dictionary<string, Particle>();
        public BigDecimal Value;
        public Profile Owner;
        public List<string> Types = new List<string>();

        public float BombTime;
        public bool Exploded;
        public float EatTimer;
        public List<Ball> CollectedBalls = new List<Ball>();
        public float ExplosionTime;

        public float ValueAlpha;

        //public List<ParticleEmitter> particleEmitters = new List<ParticleEmitter>();

        public Ball(BigDecimal value)
        {
            P = new Particle();
            P.Alpha = 1f;
            Value = value;
        }
    }

    [Serializable()]
    public class PongObject
    {
        public Particle P;
        public Dictionary<string, Particle> Modifiers = new Dictionary<string, Particle>();
        public List<string> Types = new List<string>();
        public Profile Owner;
        public float Timer;
        public float Duration;
        public float Frequency;
        public float FullFrequency;
        public string MainType;
        public bool Active;

        public List<Ball> CollectedBalls = new List<Ball>();
        public Dictionary<string, float> Floats = new Dictionary<string,float>();

        //public float BlackHoleMultiplier;
        //public float PortalFrequency;
        //public float PortalTimer;
        //public float PortalMultiplier;

        public PongObject(SkeletonTexture tex, Vector2 position, string mainType)
        {
            P = new Particle();
            P.Tex = tex;
            P.Position = position;

            MainType = mainType;
        }
    }
}
