using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    public class SkillLevel
    {
        Dictionary<ResourceName, Resource> Cost;
    }

    public class SkillRequirement
    {
        public bool RequirementMet = false;
        public Skill RequiredSkill;
        public int RequiredSkillLevel;
        
        public SkillRequirement(Skill skill, int level)
        {
            RequiredSkill = skill;
            RequiredSkillLevel = level;
        }
        
        public SkillRequirement(Skill skill, bool max)
        {
            RequiredSkill = skill;

            if (max)
                RequiredSkillLevel = RequiredSkill.Cost.Count;
            else if (RequiredSkill.Cost.Count > 0)
                RequiredSkillLevel = 1;
            else
                RequiredSkillLevel = 0;
        }

        public SkillRequirement()
        {
        }

        public bool IsRequirementMet()
        {
            if (RequiredSkill != null)
            {
                if (RequiredSkill.Level >= RequiredSkillLevel)
                    return true;
            }
            
            return RequirementMet;
        }
    }

    public class Skill
    {
        //public List<Skill> Requirements = new List<Skill>();
        public Skill Parent;
        public Dictionary<string, Skill> Children = new Dictionary<string, Skill>();
        public Dictionary<string, SkillRequirement> Requirements;



        public int Level;
        public List<Dictionary<ResourceName, Resource>> Cost = new List<Dictionary<ResourceName, Resource>>();



        public bool Hidden;

        public int AllChildren;
        public string Name;
        public bool Resting;
        public float Angle;

        public Vector2 Acceleration;
        public Vector2 Velocity;
        
        public Vector2 Destination;
        public Vector2 Position;

        public float LineAddition;
        public Texture2D Tex;
        public float Scale;


        public Skill(List<Dictionary<ResourceName, Resource>> cost, string name, Skill parent, Dictionary<string, SkillRequirement> requirements)
        {
            Requirements = requirements;

            if (parent != null)
            {
                Parent = parent;
                Parent.Children.Add(name, this);
                SetChildren();
            }
            
            Level = 0;

            if (cost != null)
                Cost = cost;
            
            Hidden = false;
            Destination = Vector2.Zero;
            Position = Destination;
            Name = name;
            Resting = false;
            //Angle = (float)GlobalVariables.RandomNumber.NextDouble() * MathHelper.TwoPi;
            Angle = 0f;
            LineAddition = 0f;
            Tex = StaticTests.Marker;
            Scale = 16f;
        }

        public bool AreRequirementsMet()
        {
            foreach (SkillRequirement Req in Requirements.Values)
                if (!Req.IsRequirementMet())
                    return false;

            return true;
        }

        private void SetChildren()
        {
            if (Parent != null)
            {
                Parent.AllChildren++;
                Parent.SetChildren();
            }
        }

        public float GetAngle()
        {
            float angle = Angle;
            
            if (Parent != null)
                angle += Parent.GetAngle();
            
            return angle;
        }

        //public void AddRequirement(Skill requirement)
        //{
        //    if (requirement != null)
        //    {
        //        if (Requirements.Count == 0)
        //        {
        //            Destination = requirement.Destination;
        //            Position = Destination;
        //        }
                
        //        requirement.ChildrenCount++;
        //        Requirements.Add(requirement);

        //        if (Requirements.Count == 1)
        //            SetChildren();
        //    }
        //}

        public void AddSkillLevel(Dictionary<ResourceName, Resource> cost)
        {
            Cost.Add(cost);
        }

        public void AddSkillLevel(int amountToAdd, float multiplier)
        {
            //if (Cost.Count != 0)
            //    for (int i = 0; i < amountToAdd; i++)
            //    {
            //        Dictionary<ResourceName, Resource> resource = new Dictionary<ResourceName, Resource>();

            //        for (int o = 0; o < Cost[0].Count; o++)                    
            //            resource.Add(new Resource(Cost[0][o].Name, Cost[0][o].Amount));

            //        for (int o = 0; o < resource.Count; o++)
            //            resource[o].Amount = (int)(resource[o].Amount * Math.Pow(multiplier, Cost.Count));

            //        Cost.Add(resource);
            //    }
        }
    }



    public class SkillSelector
    {
        public string SelectedSkill = "";
        public int SelectedCount = 0;
        public InputType Player = InputType.None;
        public Profile PlayerProfile;
        public float MoveTimer = 0f;
        public Camera cam = new Camera();
        public Light light = new PointLight()
            {
                IsEnabled = true,
                Power = 0.5f,
                LightDecay = 300
            };

        public Dictionary<ResourceName, Resource> AvailableResource = new Dictionary<ResourceName,Resource>();

        public SkillSelector(InputType player, Profile profile, Dictionary<ResourceName, Resource> availableResource)
        {
            Player = player;
            PlayerProfile = profile;
            AvailableResource = availableResource;
        }
    }

    public class SkillCloud
    {
        public Dictionary<string, Skill> Skills = new Dictionary<string, Skill>();
        bool ShowAll = true;
        List<SkillSelector> Selectors = new List<SkillSelector>();

        public void AddSkill(List<Dictionary<ResourceName, Resource>> cost, string name, string parent, Dictionary<string, SkillRequirement> requirements)
        {
            if (parent != null)
                Skills.Add(name, new Skill(cost, name, Skills[parent], requirements));
            else
                Skills.Add(name, new Skill(cost, name, null, requirements));
        }

        public void AddSkill(List<Dictionary<ResourceName, Resource>> cost, string name, string parent, bool maxRequirement)
        {
            Dictionary<string, SkillRequirement> requirements = new Dictionary<string, SkillRequirement>();

            if (parent != null)
            {
                requirements.Add(Skills[parent].Name, new SkillRequirement(Skills[parent], maxRequirement));
                Skills.Add(name, new Skill(cost, name, Skills[parent], requirements));
            }
            else
                Skills.Add(name, new Skill(cost, name, null, requirements));
        }

        public void AddSkill(List<Dictionary<ResourceName, Resource>> cost, string name, string parent, int level)
        {
            Dictionary<string, SkillRequirement> requirements = new Dictionary<string, SkillRequirement>();

            if (parent != null)
            {
                requirements.Add(Skills[parent].Name, new SkillRequirement(Skills[parent], level));
                Skills.Add(name, new Skill(cost, name, Skills[parent], requirements));
            }
            else
                Skills.Add(name, new Skill(cost, name, null, requirements));
        }

        public void MainUpdate(Dictionary<ResourceName, Resource> availableResource)
        {
            //if (availableResource != null)            
            //    foreach (SkillSelector S in Selectors)
            //        S.AvailableResource = availableResource;


            FindPosition();
            UpdateSkillsPosition();
            CheckCollisions();

            Selectors.Clear();
        }
        
        public void PlayerUpdate(SkillSelector Selector)
        {
            //if (Selectors.Count != Players.Count)
            //    Selectors = Players;
            //else
            //{
            //    for (int o = 0; o < Selectors.Count; o++)
            //    {
            //        Selectors[o].SelectionColor = Players[o].SelectionColor;
            //        Selectors[o].Player = Players[o].Player;
            //    }
            //}

            UpdateControls(Selector);

            Selectors.Add(Selector);

            BuySkill(Selector);
            SellSkill(Selector);
        }

        private void BuySkill(SkillSelector Selector)
        {
            if (Selector.SelectedCount > 0)
            {
                Skill S = Skills[Selector.SelectedSkill];

                while (Selector.SelectedCount > 0)
                {
                    bool Available = true;

                    if (S.Cost.Count != S.Level)
                    {
                        foreach (KeyValuePair<ResourceName, Resource> R1 in Selector.AvailableResource)
                            if (S.Cost[S.Level][R1.Key].Amount > R1.Value.Amount)
                                Available = false;
                        
                        if (ShowAll)
                            Available = true;
                    }
                    else
                        Available = false;

                    if (Available)
                    {
                        foreach (KeyValuePair<ResourceName, Resource> R1 in Selector.AvailableResource)
                            R1.Value.Amount -= S.Cost[S.Level][R1.Key].Amount;

                        S.Level++;
                    }
                    else
                    {
                        Selector.SelectedCount = 0;
                        break;
                    }

                    Selector.SelectedCount--;
                }
            }
        }

        private void SellSkill(SkillSelector Selector)
        {
            if (Selector.SelectedCount < 0)
            {
                Skill S = Skills[Selector.SelectedSkill];

                while (Selector.SelectedCount < 0)
                {
                    if (S.Level != 0)
                    {
                        foreach (KeyValuePair<ResourceName, Resource> R1 in Selector.AvailableResource)
                            R1.Value.Amount += S.Cost[S.Level - 1][R1.Key].Amount * 0.5f;

                        S.Level--;
                    }
                    else
                    {
                        Selector.SelectedCount = 0;
                        break;
                    }

                    Selector.SelectedCount++;
                }
            }
        }
        
        #region Control

        private void UpdateControls(SkillSelector Selector)
        {
            
                Vector2 Direction = Vector2.Zero;

                switch (Selector.Player)
                {
                    case InputType.Keyboard:

                        if (Direction == Vector2.Zero)
                        {
                            if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Left) ||
                                InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.A))
                                Direction.X -= 1f;
                            if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Right) ||
                                InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.D))
                                Direction.X += 1f;
                            if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Up) ||
                                InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.W))
                                Direction.Y += 1f;
                            if (InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.Down) ||
                                InputManager.KBButtonPressed(false, Microsoft.Xna.Framework.Input.Keys.S))
                                Direction.Y -= 1f;
                        }

                        if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Enter))
                        {
                            Selector.SelectedCount = 1;
                        }

                        break;

                    case InputType.GamepadOne:
                    case InputType.GamepadTwo:
                    case InputType.GamepadThree:
                    case InputType.GamepadFour:

                        int P = 0;
                        switch (Selector.Player)
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

                        Direction = InputManager.GP[P].ThumbSticks.Left;

                        if (Direction == Vector2.Zero)
                        {
                            if (InputManager.GP[P].DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                Direction.X -= 1f;
                            if (InputManager.GP[P].DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                Direction.X += 1f;
                            if (InputManager.GP[P].DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                Direction.Y += 1f;
                            if (InputManager.GP[P].DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                                Direction.Y -= 1f;
                        }

                        if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.A) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.A))
                        {
                            Selector.SelectedCount = 1;
                        }

                        if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.B) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.B))
                        {
                            Selector.SelectedCount = -1;
                        }

                        if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.Y))
                        {
                            Selector.SelectedCount = Skills[Selector.SelectedSkill].Cost.Count - Skills[Selector.SelectedSkill].Level;
                        }

                        if (InputManager.GP[P].IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X) && InputManager.pGP[P].IsButtonUp(Microsoft.Xna.Framework.Input.Buttons.X))
                        {
                            Selector.SelectedCount = -Skills[Selector.SelectedSkill].Level;
                        }

                        break;
                }


                //move to skill based on input direction.
                if ((Direction != Vector2.Zero && Selector.MoveTimer > 0.25f) || (Selector.SelectedSkill == ""))
                {
                    Direction.X *= -1f;
                    Direction.Normalize();

                    float LowestDistance = float.MaxValue;
                    string Selected = "";


                    bool Searching = true;
                    float amount = 4f;

                    while (Searching)
                    {
                        if (Skills.Count <= 1)
                            Searching = false;

                        foreach (KeyValuePair<string, Skill> S in Skills)
                        {
                            if (Selector.SelectedSkill != "")
                            {
                                if (!S.Value.Hidden || ShowAll)
                                    if (Skills[Selector.SelectedSkill] != S.Value)
                                    {
                                        float cone = (float)Math.Cos(MathHelper.Pi / amount);
                                        Vector2 heading = (Skills[Selector.SelectedSkill].Position - S.Value.Position);

                                        if (heading != Vector2.Zero)
                                            heading.Normalize();

                                        if (Vector2.Dot(Direction, heading) > cone) // Target is within the cone.
                                        {
                                            float dis = Vector2.Distance(Skills[Selector.SelectedSkill].Position, S.Value.Position);
                                            if (dis < LowestDistance)
                                            {
                                                Selected = S.Key;
                                                LowestDistance = dis;
                                                Searching = false;
                                            }
                                        }
                                    }
                            }
                            else if (S.Value.Parent == null)
                            {
                                Selected = S.Key;
                                Searching = false;
                            }

                        }

                        amount /= 2f;
                    }

                    if (Selected != "")
                        Selector.SelectedSkill = Selected;

                    Selector.MoveTimer = 0f;
                }
                else if (Direction == Vector2.Zero)
                    Selector.MoveTimer = 0.5f;

                Selector.MoveTimer += GlobalVariables.FrameTime;
            
        }

        #endregion

        #region Tree Building

        private void FindPosition()
        {
            foreach (Skill S in Skills.Values)
            {
                if (S.Parent == null)
                    S.Resting = true;
                else
                //for (int o = 0; o < Skills[i].Requirements.Count && o < 1; o++)
                {
                    //float MidPoint = (circleSize * 2f * (Skills[i].Children + 1f)) + (((circleSize * (Skills[i].Requirements[o].Children + 1f))) / 2f) + Skills[i].Requirements[o].LineAddition;
                    //float MidPoint = (circleSize * 2f *
                    //    (Skills[i].AllChildren + 1f)) +
                    //    (((circleSize * (Skills[i].Requirements[o].Children + 1f)))) + 
                    //    Skills[i].Requirements[o].LineAddition;

                    float MidPoint = ((S.Scale / 2f) *
                        (S.AllChildren + 1f)) +

                        //(((circleSize * 2f))) +

                        (((S.Scale * (S.Parent.Children.Count + 1f))) / 2f) +
                        S.Parent.LineAddition;

                    MidPoint *= 2f;
                    float Angle = (MathHelper.TwoPi / S.Parent.Children.Count);
                    int num = 0;

                    bool Search = true;

                    foreach (Skill S2 in Skills.Values)
                        if (Search)
                        //for (int p = 0; p < Skills[u].Requirements.Count; p++)
                        {
                            if (S2.Parent == S.Parent)
                                num++;

                            if (S2 == S)
                                Search = false;
                        }

                    Angle *= num;
                    Angle += S.GetAngle();

                    Vector2 Dest = S.Parent.Destination + (UsefulMethods.AngleToVector(Angle) * MidPoint);

                    //if (Dest != Dest)
                    //    Dest = Vector2.Zero;
                    //if (Skills[i].Position != Skills[i].Position)
                    //    Skills[i].Position = Vector2.Zero;
                    //if (Skills[i].Velocity != Skills[i].Velocity)
                    //    Skills[i].Velocity = Vector2.Zero;


                    S.Destination = Dest;

                    Vector2 Normal = S.Position - Dest;
                    float Distance = Normal.Length();

                    if (Normal != Vector2.Zero)
                        Normal.Normalize();

                    S.Acceleration -= Normal * (Distance * 25f);

                    //if (Distance > 5f)
                    //    Skills[i].Resting = false;
                    //else
                    S.Resting = true;
                }

                //ShowAll = true;

                if (S.Parent != null)
                    if (S.Parent.Level > 0)
                        S.Hidden = false;
                    else if (S.Parent.Parent != null)
                    {
                        if (S.Parent.Parent.Level == 0 && !ShowAll)
                        {
                            S.Hidden = true;
                            S.Position = S.Parent.Position;
                        }
                        else
                        {
                            S.Hidden = false;
                            //S.Position = S.Parent.Position;
                        }
                    }
            }
        }

        private void UpdateSkillsPosition()
        {
            foreach (Skill S in Skills.Values)
            {
                if (S.Parent != null)
                {
                    S.Velocity += S.Acceleration * GlobalVariables.WorldTime;
                    S.Position += S.Velocity * GlobalVariables.WorldTime;
                    S.Velocity *= 0.9f;
                    S.Acceleration = Vector2.Zero;
                }
            }
        }

        private void CheckCollisions()
        {

            foreach (Skill S1 in Skills.Values)
                foreach (Skill S2 in Skills.Values)
                    if (S1 != S2)
                {
                    if (S2.Name == "Protostar" && S1.Name == "Corpus")
                    {
                    }

                    bool Parent = false;

                    //if (S1.Requirements.Count != 0)
                    //    if (S1.Requirements[0] == Skills[o])
                    //        Parent = true;

                    if (S1.Resting && S2.Resting && !Parent)
                    {
                        bool intersect = false;

                        Vector2 point1 = S1.Destination;
                        Vector2 point2 = Vector2.Zero;
                        Vector2 point3 = S2.Destination;
                        Vector2 point4 = Vector2.Zero;

                        if (S1.Parent != null)
                            point2 = S1.Parent.Destination;
                        if (S2.Parent != null)
                            point4 = S2.Parent.Destination;



                        Vector2 normal1 = point1 - point2;

                        if (normal1 != Vector2.Zero)
                            normal1.Normalize();
                        point1 -= normal1 * 2f;
                        point2 += normal1 * 2f;

                        Vector2 normal2 = point3 - point4;
                        if (normal2 != Vector2.Zero)
                            normal2.Normalize();
                        point3 -= normal2 * 2f;
                        point4 += normal2 * 2f;


                        bool circleIntersect = false;


                        //bool circleIntersect = FindLineCircleIntersections(point1, 1f, point3, point4);





                        if (point1 != point2 && point3 != point4)
                        {
                            intersect = ProcessIntersection(point1, point2, point3, point4);
                            //circleIntersect = IntersectionPoint(point1, circleSize * 2f, point3, point4);
                            circleIntersect = IntersectionPoint(point3, S2.Scale * 2f, point1, point2);
                        }


                        if (circleIntersect)
                        {
                        }
                        circleIntersect = false;

                        if (Vector2.Distance(S1.Destination, S2.Destination) < (S1.Scale + S2.Scale) * 2f || intersect || circleIntersect)
                        {
                            //S1.Angle = (float)GlobalVariables.RandomNumber.NextDouble() * MathHelper.TwoPi;
                            //Skills[o].Angle = (float)GlobalVariables.RandomNumber.NextDouble() * MathHelper.TwoPi;
                            //S1.Resting = false;
                            //Skills[o].Resting = false;

                            //if (S1.Requirements.Count != 0)
                            //{
                            //    S1.Requirements[0].Angle = (float)GlobalVariables.RandomNumber.NextDouble() * MathHelper.TwoPi;
                            //    S1.Requirements[0].Resting = false;
                            //}

                            //if (Skills[o].Requirements.Count != 0)
                            //{
                            //    Skills[o].Requirements[0].Angle = (float)GlobalVariables.RandomNumber.NextDouble() * MathHelper.TwoPi;
                            //    Skills[o].Requirements[0].Resting = false;
                            //}

                            //float angle = MathHelper.TwoPi * GlobalVariables.WorldTime * 5f;
                            float angle = MathHelper.TwoPi / 100f;


                            //S1.Angle += angle;
                            S2.Angle -= angle;
                            //S1.Resting = false;
                            S2.Resting = false;

                            //if (S1.Requirements.Count != 0)
                            //{
                            //    S1.Requirements[0].Angle += angle;
                            //    S1.Requirements[0].Resting = false;
                            //}

                            if (S2.Parent != null)
                            {
                                S2.Parent.Angle -= angle;
                                S2.Parent.Resting = false;
                            }


                            //List<Skill> SkillList = new List<Skill>();

                            //if (S1.Requirements.Count != 0)
                            //    SkillList.Add(S1.Requirements[0]);
                            //else
                            //    SkillList.Add(S1);

                            //if (Skills[o].Requirements.Count != 0)
                            //    SkillList.Add(Skills[o].Requirements[0]);
                            //else
                            //    SkillList.Add(Skills[o]);

                            //SkillList = ExtractChildren(SkillList);
                            //SkillList.Shuffle();

                            //Skills.AddRange(SkillList);
                        }
                    }
                }

        }

        #endregion

        #region Line Checks

        private bool ProcessIntersection(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
        {
            bool intersection;

            float ua = (point4.X - point3.X) * (point1.Y - point3.Y) - (point4.Y - point3.Y) * (point1.X - point3.X);
            float ub = (point2.X - point1.X) * (point1.Y - point3.Y) - (point2.Y - point1.Y) * (point1.X - point3.X);
            float denominator = (point4.Y - point3.Y) * (point2.X - point1.X) - (point4.X - point3.X) * (point2.Y - point1.Y);

            intersection = false;

            if (Math.Abs(denominator) <= 0.00001f)
            {
                intersection = true;
                if (Math.Abs(ua) <= 0.00001f && Math.Abs(ub) <= 0.00001f)
                {
                    intersection = true;
                }
            }
            else
            {
                ua /= denominator;
                ub /= denominator;

                if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
                {
                    intersection = true;
                }
            }

            return intersection;
        }

        private bool IntersectionPoint(Vector2 sc, double r, Vector2 p1, Vector2 p2)
        {
            float a, b, c;
            float bb4ac;
            float mu1, mu2;
            Vector2 dp;

            dp = p2 - p1;

            a = dp.X * dp.X + dp.Y * dp.Y;
            b = 2 * (dp.X * (p1.X - sc.X) + dp.Y * (p1.Y - sc.Y));
            c = sc.X * sc.X + sc.Y * sc.Y;
            c += p1.X * p1.X + p1.Y * p1.Y;
            c -= 2 * (sc.X * p1.X + sc.Y * p1.Y);
            c -= (float)(r * r);
            bb4ac = b * b - 4 * a * c;

            if (Math.Abs(a) < Double.Epsilon || bb4ac < 0)
            {
                return false;
            }

            mu1 = (-b + (float)Math.Sqrt(bb4ac)) / (2 * a);
            mu2 = (-b - (float)Math.Sqrt(bb4ac)) / (2 * a);

            // no intersection
            if ((mu1 < 0 || mu1 > 1) && (mu2 < 0 || mu2 > 1))
            {
                return false;
            }
            // one point on mu1
            else if (mu1 > 0 && mu1 < 1 && (mu2 < 0 || mu2 > 1))
            {
                return true;
            }
            // one point on mu2
            else if (mu2 > 0 && mu2 < 1 && (mu1 < 0 || mu1 > 1))
            {
                return false;
            }
            //  one or two points
            else if (mu1 > 0 && mu1 < 1 && mu2 > 0 && mu2 < 1)
            {
                //  tangential
                if (mu1 == mu2)
                {
                    return false;
                }
                //  two points
                else
                {
                    return true;
                }
            }

            return false;
        }

        //private void ProcessIntersection()
        //{
        //    float ua = (point4.X - point3.X) * (point1.Y - point3.Y) - (point4.Y - point3.Y) * (point1.X - point3.X);
        //    float ub = (point2.X - point1.X) * (point1.Y - point3.Y) - (point2.Y - point1.Y) * (point1.X - point3.X);
        //    float denominator = (point4.Y - point3.Y) * (point2.X - point1.X) - (point4.X - point3.X) * (point2.Y - point1.Y);

        //    intersection = coincident = false;

        //    if (Math.Abs(denominator) <= 0.00001f)
        //    {
        //        if (Math.Abs(ua) <= 0.00001f && Math.Abs(ub) <= 0.00001f)
        //        {
        //            intersection = coincident = true;
        //            intersectionPoint = (point1 + point2) / 2;
        //        }
        //    }
        //    else
        //    {
        //        ua /= denominator;
        //        ub /= denominator;

        //        if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
        //        {
        //            intersection = true;
        //            intersectionPoint.X = point1.X + ua * (point2.X - point1.X);
        //            intersectionPoint.Y = point1.Y + ua * (point2.Y - point1.Y);
        //        }
        //    }
        //}

        #endregion

        public void UpdateCamera(Rectangle viewport, Camera cam, SkillSelector Selector)
        {
            Vector2 Position = Skills[Selector.SelectedSkill].Position;
            cam.ZoomDestination = viewport.Height / 500f;

            cam.PositionDestination = Position;

            //cam.viewport = viewport;
            cam.Update();
        }

        public List<Light> GetLights(SkillSelector Selector)
        {
            List<Light> Lights = new List<Light>();

            Vector2 Pos = Skills[Selector.SelectedSkill].Position;

            foreach (KeyValuePair<string, Skill> S1 in Skills)
            {
                Color color = Color.White;

                for (int o = 0; o < Selectors.Count; o++)
                    if (S1.Key == Selectors[o].SelectedSkill)
                    {
                        if (color == Color.SandyBrown || color == Color.Gold)
                            color = Selectors[o].PlayerProfile.PrimaryColor;
                        else
                            color = Color.Lerp(color, Selectors[o].PlayerProfile.PrimaryColor, 0.5f);

                        Selectors[o].light.Color = color.ToVector4();
                        Selectors[o].light.Position = new Vector3(S1.Value.Position.X, S1.Value.Position.Y, 100f);
                        Selectors[o].light.LightDecay = 300;

                        Lights.Add(Selectors[o].light);
                    }
            }

            //Lights.Add(new PointLight()
            //{
            //    IsEnabled = true,
            //    Color = Selector.PlayerProfile.PrimaryColor.ToVector4() * 0.2f,
            //    Power = 0.5f,
            //    LightDecay = 300,




            //    Position = new Vector3(Pos.X, Pos.Y, 50)
            //});

            //foreach (Skill S1 in Skills.Values)
            //    Lights.Add(new PointLight()
            //    {
            //        IsEnabled = true,
            //        Color = Color.White.ToVector4() * 0.2f,
            //        Power = 2f,
            //        LightDecay = 100,
            //        Position = new Vector3(S1.Position.X, S1.Position.Y, 50)
            //    });

            return Lights;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SkillSelector Selector, Rectangle viewport, RenderTarget2D RenderTarget)
        {
            UpdateCamera(viewport, Selector.cam, Selector);
            //graphicsDevice.SetRenderTarget(Selector.cam.RenderTarget);

            //graphicsDevice.Clear(new Color(40, 40, 40));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            spriteBatch.Draw(SheetManager.GetRenderTexture("Core", "Marker"), new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Selector.cam.Transform);


            SkeletonTexture tex1 = new SkeletonTexture("SkillTree", "SkillBoxBack");
            SkeletonTexture tex2 = new SkeletonTexture("SkillTree", "SkillBoxBase");
            SkeletonTexture tex3 = new SkeletonTexture("SkillTree", "SkillBoxFrame");

            foreach (Skill S1 in Skills.Values)
                if (!S1.Hidden || ShowAll)
                    for (int o = 0; o < Selectors.Count; o++)
                        if (S1.Name == Selectors[o].SelectedSkill)
                            foreach (Skill S2 in Skills.Values)
                                if (!S2.Hidden || ShowAll)
                                    if (S2.Parent != null)
                                        if (S1 == S2.Parent)
                                            Line.DrawLine(spriteBatch, S2.Position, S2.Parent.Position, Selectors[o].PlayerProfile.PrimaryColor, 5f);


            foreach (KeyValuePair<string, Skill> S1 in Skills)
                if (!S1.Value.Hidden || ShowAll)
                    if (S1.Value.Parent != null)
                    {
                        //LERP Colors.
                        for (int o = 0; o < Selectors.Count; o++)
                            if (S1.Key == Selectors[o].SelectedSkill)
                                Line.DrawLine(spriteBatch, S1.Value.Position, S1.Value.Parent.Position, Selectors[o].PlayerProfile.PrimaryColor, 5f);

                        Line.DrawLine(spriteBatch, S1.Value.Position, S1.Value.Parent.Position, Color.Black, 2.5f);
                    }

            //else
            //    Line.DrawLine(spriteBatch, Skills[i].Position, Skills[i].Requirements[o].Position, Color.Yellow, 2.5f);



            foreach (KeyValuePair<string, Skill> S1 in Skills)
                if (!S1.Value.Hidden || ShowAll)
                {
                    Color color = Color.SandyBrown;
                    float scale = 48f;
                    Color BackgroundColor = Color.LightCyan;
                    float Percent = 1f - ((float)S1.Value.Level / (float)S1.Value.Cost.Count);
                    float Alpha = 1f;

                    if (!ShowAll)
                        foreach (SkillRequirement R1 in S1.Value.Requirements.Values)
                            if (!R1.IsRequirementMet())
                            {
                                Alpha = 0.25f;
                                break;
                            }


                    string level = "" + S1.Value.Level;
                    if (S1.Value.Level == S1.Value.Cost.Count)
                    {
                        level = "Max";
                        color = Color.Gold;
                        BackgroundColor = Color.Gold;
                        scale = 60f;
                    }


                    for (int o = 0; o < Selectors.Count; o++)
                        if (S1.Key == Selectors[o].SelectedSkill)
                        {
                            if (color == Color.SandyBrown || color == Color.Gold)
                                color = Selectors[o].PlayerProfile.PrimaryColor;
                            else
                                color = Color.Lerp(color, Selectors[o].PlayerProfile.PrimaryColor, 0.5f);
                            scale = 72f;
                        }

                    //spriteBatch.Draw(tex, Skills[i].Position, null, Color.White, 0f, new Vector2(tex.Width / 2f, tex.Height / 2f), Skills[i].AllChildren + 1, SpriteEffects.None, 0f);

                    tex1.Draw(spriteBatch, S1.Value.Position, Color.Black * Alpha, 0f, scale, SpriteEffects.None);


                    tex2.Draw(spriteBatch, S1.Value.Position, BackgroundColor * Alpha, 0f, scale, SpriteEffects.None);


                    if (level != "Max")
                        tex2.Draw(spriteBatch, S1.Value.Position, new Vector2(1 - Percent, 1), Color.Red, 0f, scale, SpriteEffects.None);

                    tex3.Draw(spriteBatch, S1.Value.Position, color * Alpha, 0f, scale, SpriteEffects.None);
                }

            foreach (KeyValuePair<string, Skill> S1 in Skills)
                if (S1.Key == Selector.SelectedSkill)
                {
                    string level = "" + S1.Value.Level;
                    if (S1.Value.Level == S1.Value.Cost.Count)
                        level = "Max";

                    StaticInfoBox.ClearList();
                    StaticInfoBox.Add(StringManager.BoldString(S1.Value.Name));
                    StaticInfoBox.Add("Level: " + level);

                    if (level != "Max")
                    {
                        StaticInfoBox.Add("");
                        StaticInfoBox.Add(StringManager.ItalicString("Cost"));

                        foreach (KeyValuePair<ResourceName, Resource> R in S1.Value.Cost[S1.Value.Level])
                        {
                            Color color = Color.White;
                            
                            if (Selector.AvailableResource[R.Key].Amount < R.Value.Amount)
                                color = Color.Red;






                            StaticInfoBox.Add(StringManager.ColorString(StringManager.BoldString(WriteNumber.Write(R.Value.Amount, SeparatorType.Standard, true, ShortenType.Short_Scale)) + " " + R.Key, color));

                        }
                    }

                    foreach (SkillRequirement R in S1.Value.Requirements.Values)
                        if (!R.IsRequirementMet())
                        {
                            StaticInfoBox.Add(StringManager.ColorString("Requires " + StringManager.BoldString("level " + R.RequiredSkillLevel) + " " + R.RequiredSkill.Name, Color.Red));
                        }


                    StaticInfoBox.Draw(spriteBatch, S1.Value.Position, 32f, 0.65f, 0f, "", Vector2.Zero, Vector2.Zero, new Color(15, 15, 15), new Vector2(5f), new BorderStyle("Default", Selector.PlayerProfile.PrimaryColor, new Location(8f)));
                }


            spriteBatch.End();


        }
    }
}
