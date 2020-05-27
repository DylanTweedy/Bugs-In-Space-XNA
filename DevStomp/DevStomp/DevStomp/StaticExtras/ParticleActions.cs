using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DevStomp
{
    class ParticleActions
    {
        public class Actions
        {
            public byte Type;
            public Dictionary<string, ValueType> Values = new Dictionary<string, ValueType>();
            public Random rand;

            public void Type00(float acceleration)
            {
                Values.Add("Acceleration", acceleration);
                Type = 2;
            }

            public void Type01(float angle, float acceleration)
            {
                Values.Add("Acceleration", new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * acceleration);
                Type = 0;
            }

            public void Type02(float acceleration, byte WeaponType, int Seed)
            {
                rand = GlobalVariables.RandomNumber;

                if (WeaponType == 1)
                    rand = new Random(Seed);

                float angle = (float)(MathHelper.TwoPi * rand.NextDouble());
                
                Values.Add("Acceleration", new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * acceleration);
                Type = 0;
            }

            public void Type03(float angle, byte WeaponType, int Seed)
            {
                rand = GlobalVariables.RandomNumber;

                if (WeaponType == 1)
                    rand = new Random(Seed);

                float acceleration = (float)rand.NextDouble();
                
                Values.Add("Acceleration", new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * acceleration);

                Type = 0;
            }

            public void Type04(byte WeaponType, int Seed)
            {
                rand = GlobalVariables.RandomNumber;

                if (WeaponType == 1)
                    rand = new Random(Seed);

                float acceleration = (float)rand.NextDouble();
                float angle = (float)(MathHelper.TwoPi * rand.NextDouble());
                
                Values.Add("Acceleration", new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * acceleration);

                Type = 0;
            }

            public void Type05(Vector2 velocity, Vector2 position, float orbitRadius, float radiusIncrease, float orbitSpeed)
            {
                float OrbitSpeed;
                float OrbitRadius;
                float OrbitRadian;
                float RadiusIncrease;

                OrbitRadian = -(float)Math.Atan2(velocity.X, velocity.Y) + (float)(Math.PI / 2f);
                OrbitRadius = orbitRadius;
                OrbitSpeed = orbitSpeed;
                RadiusIncrease = radiusIncrease;

                OrbitRadian += (OrbitSpeed * (float)GlobalVariables.FrameTime) * 0.9f;

                Values.Add("OrbitSpeed", OrbitSpeed);
                Values.Add("OrbitRadius", OrbitRadius);
                Values.Add("OrbitRadian", OrbitRadian);
                Values.Add("RadiusIncrease", RadiusIncrease);
                Values.Add("OrbitPosition", position);
                Values.Add("StartingPosition", position);
                Values.Add("Starting", true);

                Type = 1;
            }

            public void Type06(Vector2 velocity, Vector2 position, float orbitRadius, float radiusIncrease, float orbitSpeed)
            {
                float OrbitSpeed;
                float OrbitRadius;
                float OrbitRadian;
                float RadiusIncrease;
                Vector2 OrbitPosition;

                OrbitRadian = (float)Math.Atan2(velocity.X, velocity.Y) + (float)(Math.PI / 2f);
                OrbitRadius = orbitRadius;
                OrbitSpeed = orbitSpeed;
                RadiusIncrease = radiusIncrease;

                OrbitPosition = new Vector2(OrbitRadius * (float)Math.Cos(OrbitRadian), OrbitRadius * (float)Math.Sin(OrbitRadian));
                OrbitRadian += (OrbitSpeed * (float)GlobalVariables.FrameTime) * 0.9f;

                Values.Add("OrbitSpeed", OrbitSpeed);
                Values.Add("OrbitRadius", OrbitRadius);
                Values.Add("OrbitRadian", OrbitRadian);
                Values.Add("RadiusIncrease", RadiusIncrease);
                Values.Add("OrbitPosition", OrbitPosition);

                Type = 6;
            }

            public void Type07(int Seed, int DistanceToTravel, Vector2 position)
            {
                Values.Add("DistanceTravelled", 0f);
                Values.Add("DistanceToTravel", DistanceToTravel);
                Values.Add("First", true);
                Values.Add("PreviousPosition", position);

                rand = new Random(Seed);

                Type = 7;
            }

            public void Type08(byte Axis, float ScaleReduction, float MaxScale)
            {
                Values.Add("Axis", Axis);
                Values.Add("ScaleReduction", ScaleReduction);
                Values.Add("MaxScale", MaxScale);
                Type = 8;
            }

            public void Type09(float ScaleAddition, byte Axis, float ScaleReduction, float MaxScale)
            {
                Values.Add("ScaleAddition", ScaleAddition);
                Values.Add("Axis", Axis);
                Values.Add("ScaleReduction", ScaleReduction);
                Values.Add("MaxScale", MaxScale);
                Type = 9;
            }

            public void Type10(float RotationSpeed, float RotationalAcceleration)
            {
                Values.Add("RotationSpeed", RotationSpeed);
                Values.Add("RotationalAcceleration", RotationalAcceleration);
                Type = 10;
            }

            public void Type11(byte type)
            {
                Values.Add("type", type);
                Values.Add("Starting", true);
                Type = 11;
            }

            public void Type12(int Speed)
            {
                Values.Add("CUP", true);
                Values.Add("Speed", Speed);
                Type = 12;
            }

            public void Type13(int Speed)
            {
                Values.Add("Speed", Speed);
                Values.Add("Current", 0);
                Type = 13;
            }

            public void Type14(int Speed)
            {
                Values.Add("Speed", Speed);
                Values.Add("Current", 0);
                Type = 14;
            }

            public void Type15(float DistanceToTravel, Vector2 position)
            {
                Values.Add("DistanceTravelled", 0f);
                Values.Add("DistanceToTravel", DistanceToTravel);
                Values.Add("PreviousPosition", position);
                Type = 15;
            }

            public void Type16(float DistanceToTravel, Vector2 position)
            {
                Values.Add("DistanceTravelled", 0f);
                Values.Add("DistanceToTravel", DistanceToTravel);
                Values.Add("PreviousPosition", position);
                Type = 16;
            }

            public void Type17(int Seed, float DistanceToTravel, Vector2 position)
            {
                rand = new Random(Seed);
                Values.Add("DistanceTravelled", 0f);
                Values.Add("DistanceToTravel", DistanceToTravel);
                Values.Add("PreviousPosition", position);
                Type = 17;
            }

            public void Type18(float DistanceToTravel, Vector2 position, byte WeaponType, int Seed)
            {
                rand = GlobalVariables.RandomNumber;
                Values.Add("DistanceTravelled", 0f);
                Values.Add("DistanceToTravel", DistanceToTravel);
                Values.Add("PreviousPosition", position);
                Values.Add("WeaponType", WeaponType);
                if (WeaponType == 1)                
                    rand = new Random(Seed);
                
                Type = 18;
            }

            public void Type19(float WaveLength, float Amplitude, float type,  bool Up)
            {
                Values.Add("WaveLength", WaveLength);
                Values.Add("Amplitude", Amplitude);
                Values.Add("DistanceTravelled", 0f);
                Values.Add("type", type);
                Values.Add("Up", Up);
                Type = 19;
            }

            public Vector2 Acceleration(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                float angle;

                switch (Type)
                {
                    case 0:
                        return acceleration + (Vector2)Values["Acceleration"];

                    case 2:
                        angle = (float)Math.Atan2(velocity.X, -velocity.Y);
                        Vector2 acc = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
                        return acceleration + (acc * (float)Values["Acceleration"]);
                }

                return acceleration;
            }

            public Vector2 Velocity(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                switch (Type)
                {
                    #region 1
                    case 1:
                        if (!(bool)Values["Starting"])
                            return Vector2.Zero;
                        break;
                    #endregion

                    #region 6
                    case 6:
                        Vector2 OrbitPosition = (Vector2)Values["OrbitPosition"];
                        Vector2 PreviousPosition = OrbitPosition;

                        float OrbitRadius = (float)Values["OrbitRadius"];
                        float OrbitRadian = (float)Values["OrbitRadian"];
                        float OrbitSpeed = (float)Values["OrbitSpeed"];
                        float RadiusIncrease = (float)Values["RadiusIncrease"];

                        Vector2 Diff = Vector2.Zero;
                        OrbitPosition = Vector2.Zero;
                        OrbitPosition += new Vector2(OrbitRadius * (float)Math.Cos(OrbitRadian), OrbitRadius * (float)Math.Sin(OrbitRadian));

                        OrbitRadian += (OrbitSpeed / OrbitRadius) * 10f * (float)GlobalVariables.FrameTime;
                        OrbitRadius += RadiusIncrease * (float)GlobalVariables.FrameTime;

                        Values["OrbitPosition"] = OrbitPosition;
                        Values["OrbitRadius"] = OrbitRadius;
                        Values["OrbitRadian"] = OrbitRadian;

                        Diff = OrbitPosition - PreviousPosition;
                        return velocity + Diff;

                    #endregion

                    #region 7
                    case 7:

                        if ((float)Values["DistanceTravelled"] > (int)Values["DistanceToTravel"])
                        {
                            if ((bool)Values["First"])
                            {
                                Values["DistanceToTravel"] = (int)Values["DistanceToTravel"] / 2;
                                Values["First"] = false;
                            }


                            if (rand.Next(0, 3) == 0)
                            {
                                float y = velocity.Y;
                                float x = velocity.X;

                                if (rand.Next(0, 2) == 0)
                                    velocity.Y = x;
                                else
                                    velocity.Y = -x;


                                if (rand.Next(0, 2) == 0)
                                    velocity.X = y;
                                else
                                    velocity.X = -y;
                            }
                        }

                        Values["DistanceTravelled"] = (float)Values["DistanceTravelled"] + Vector2.Distance(position, (Vector2)Values["PreviousPosition"]);
                        Values["PreviousPosition"] = position;

                        return velocity;

                    #endregion

                    #region 11
                    case 11:
                        if ((bool)Values["Starting"])
                        {
                            float x = velocity.X;
                            float y = velocity.Y;

                            switch ((byte)Values["type"])
                            {
                                case 0:
                                    velocity.X *= -1f;
                                    break;
                                case 1:
                                    velocity.Y *= -1f;
                                    break;
                                case 2:
                                    velocity *= -1f;
                                    break;
                                case 3:
                                    velocity.X = y;
                                    velocity.Y = x;
                                    break;
                                case 4:
                                    velocity.X = -y;
                                    velocity.Y = -x;
                                    break;

                            }
                            Values["Starting"] = false;
                        }
                        return velocity;

                    #endregion
                }

                return velocity;
            }

            public Vector2 Position(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                Vector2 move;
                float dis;
                float angle;

                switch (Type)
                {
                    #region 1
                    case 1:
                        Vector2 StartingPosition = (Vector2)Values["StartingPosition"];
                        Vector2 OrbitPosition = (Vector2)Values["OrbitPosition"];
                        Vector2 PreviousPosition = OrbitPosition;
                        float OrbitRadius = (float)Values["OrbitRadius"];
                        float OrbitRadian = (float)Values["OrbitRadian"];
                        float OrbitSpeed = (float)Values["OrbitSpeed"];
                        float RadiusIncrease = (float)Values["RadiusIncrease"];
                        bool Starting = (bool)Values["Starting"];

                        dis = Vector2.Distance(StartingPosition, position);
                        if (dis <= OrbitRadius && Starting)
                        {
                            Vector2 diff = position - StartingPosition;
                            Values["OrbitPosition"] = position;
                            Values["OrbitRadian"] = -(float)Math.Atan2(diff.X, diff.Y) + (float)(Math.PI / 2f);
                            return position;
                        }
                        else
                        {
                            Vector2 Diff = Vector2.Zero;
                            Values["Starting"] = false;
                            OrbitPosition = StartingPosition;
                            OrbitPosition += new Vector2(OrbitRadius * (float)Math.Cos(OrbitRadian), OrbitRadius * (float)Math.Sin(OrbitRadian));

                            OrbitRadian += (OrbitSpeed / OrbitRadius) * 10f * (float)GlobalVariables.FrameTime;
                            OrbitRadius += RadiusIncrease * (float)GlobalVariables.FrameTime;

                            Values["OrbitPosition"] = OrbitPosition;
                            Values["OrbitRadius"] = OrbitRadius;
                            Values["OrbitRadian"] = OrbitRadian;

                            Diff = OrbitPosition - PreviousPosition;
                            return position + Diff;
                        }
                    #endregion

                    #region 15
                    case 15:
                        move = Vector2.Zero;

                        if ((float)Values["DistanceTravelled"] > (float)Values["DistanceToTravel"])
                        {
                            angle = (float)Math.Atan2(velocity.X, velocity.Y);
                            move = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * ((float)Values["DistanceToTravel"] * 0.75f);
                            Values["DistanceTravelled"] = 0f;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                            position -= move;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                        }

                        float dist = (float)Values["DistanceTravelled"] + Vector2.Distance(position, (Vector2)Values["PreviousPosition"]);

                        Values["DistanceTravelled"] = dist;
                        return position;


                    #endregion

                    #region 16
                    case 16:
                        move = Vector2.Zero;

                        if ((float)Values["DistanceTravelled"] > (float)Values["DistanceToTravel"])
                        {
                            angle = (float)Math.Atan2(velocity.X, velocity.Y);
                            move = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * ((float)Values["DistanceToTravel"] * 0.75f);
                            Values["DistanceTravelled"] = 0f;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                            position -= move;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                        }

                        dis = (float)Values["DistanceTravelled"] + Vector2.Distance(position, (Vector2)Values["PreviousPosition"]);

                        Values["DistanceTravelled"] = dis;
                        return position;


                    #endregion

                    #region 17
                    case 17:
                        move = Vector2.Zero;

                        if ((float)Values["DistanceTravelled"] > (float)Values["DistanceToTravel"])
                        {
                            move = new Vector2((((float)rand.NextDouble() * 2) - 1f), (((float)rand.NextDouble() * 2) - 1f)) * ((float)Values["DistanceToTravel"] * 2f);
                            Values["DistanceTravelled"] = 0f;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                            position -= move;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                        }

                        dis = (float)Values["DistanceTravelled"] + Vector2.Distance(position, (Vector2)Values["PreviousPosition"]);

                        Values["DistanceTravelled"] = dis;
                        return position;


                    #endregion

                    #region 18
                    case 18:
                        move = Vector2.Zero;

                        if ((float)Values["DistanceTravelled"] > (float)Values["DistanceToTravel"])
                        {
                            move = new Vector2((((float)rand.NextDouble() * 2) - 1f), (((float)rand.NextDouble() * 2) - 1f)) * ((float)Values["DistanceToTravel"] * 1.75f);
                            Values["DistanceTravelled"] = 0f;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                            position -= move;
                            Particles.AddParticles(position, 1, 4, 0.05f);
                        }

                        dis = (float)Values["DistanceTravelled"] + Vector2.Distance(position, (Vector2)Values["PreviousPosition"]);

                        Values["DistanceTravelled"] = dis;                        
                        return position;


                    #endregion

                    #region 19
                    case 19:
                        angle = (float)Math.Atan2(velocity.X, -velocity.Y);
                        angle += (float)Math.PI * 0.5f;

                        float movement = UsefulMethods.FindBetween(Math.Abs((float)Values["DistanceTravelled"]), (float)Values["Amplitude"], 0f, 1f, (float)Values["type"], true);
                        
                        Vector2 Direction = new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)) * (float)Values["WaveLength"] * movement;
                        
                        float Distance = 1f * movement;
                        


                        if ((float)Values["DistanceTravelled"] > (float)Values["Amplitude"])
                            Values["Up"] = false;
                        else if ((float)Values["DistanceTravelled"] < -(float)Values["Amplitude"])
                            Values["Up"] = true;

                        if ((bool)Values["Up"])
                        {
                            Values["DistanceTravelled"] = (float)Values["DistanceTravelled"] + Distance;
                            return position + Direction;
                        }
                        else
                        {
                            Values["DistanceTravelled"] = (float)Values["DistanceTravelled"] - Distance;
                            return position - Direction;
                        }
                    #endregion
                }

                return position;
            }

            public Vector2 Scale(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                float MaxScale;
                float MinScale;
                switch (Type)
                {
                    #region 8
                    case 8:
                        switch ((byte)Values["Axis"])
                        {
                            case 0:
                                    scale.X += -(velocity.X * (float)GlobalVariables.FrameTime);
                                    scale.Y += -(velocity.Y * (float)GlobalVariables.FrameTime);                                
                                break;
                            case 1:
                                scale.X += -(velocity.X * (float)GlobalVariables.FrameTime);
                                break;
                            case 2:
                                scale.Y += -(velocity.Y * (float)GlobalVariables.FrameTime);
                                break;
                        }

                        MaxScale = (float)Values["MaxScale"] * 16f;
                        MinScale = (float)Values["MaxScale"] - 0.25f;

                        if (scale.X > MaxScale)
                            scale.X = ((1f - (float)Values["ScaleReduction"]) * MaxScale) + 1f;
                        else if (scale.X < MinScale)
                            scale.X = ((float)Values["ScaleReduction"] / MinScale) + 1f;

                        if (scale.Y > MaxScale)
                            scale.Y = ((1f - (float)Values["ScaleReduction"]) * MaxScale) + MinScale;
                        else if (scale.Y < MinScale)
                            scale.Y = ((float)Values["ScaleReduction"] / MinScale) + MinScale;
                        return scale;
                    #endregion

                    #region 9
                    case 9:
                        switch ((byte)Values["Axis"])
                        {
                            case 0:
                                if ((float)Values["ScaleAddition"] > 0)
                                {
                                    scale.X += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime * 10f);
                                    scale.Y += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime * 10f);
                                }
                                else
                                {
                                    scale.X += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime / 10f);
                                    scale.Y += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime / 10f);
                                }
                                break;
                            case 1:
                                if ((float)Values["ScaleAddition"] > 0)
                                    scale.X += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime * 10f);
                                else
                                    scale.X += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime / 10f);
                                break;
                            case 2:
                                if ((float)Values["ScaleAddition"] > 0)
                                    scale.Y += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime * 10f);
                                else
                                    scale.Y += ((float)Values["ScaleAddition"] * (float)GlobalVariables.FrameTime / 10f);
                                break;
                        }
                        
                        MaxScale = (float)Values["MaxScale"] * 16f;
                        MinScale = (float)Values["MaxScale"] - 0.25f;

                        if (scale.X > MaxScale)
                            scale.X = ((1f - (float)Values["ScaleReduction"]) * MaxScale) + 1f;
                        else if (scale.X < MinScale)
                            scale.X = ((float)Values["ScaleReduction"] / MinScale) + 1f;

                        if (scale.Y > MaxScale)
                            scale.Y = ((1f - (float)Values["ScaleReduction"]) * MaxScale) + MinScale;
                        else if (scale.Y < MinScale)
                            scale.Y = ((float)Values["ScaleReduction"] / MinScale) + MinScale;
                        return scale;
                    #endregion
                }

                return scale;
            }

            public float Rotation(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                switch (Type)
                {
                    case 10:
                        float rotationSpeed = (float)Values["RotationSpeed"];
                        float rotationAcceleration = (float)Values["RotationalAcceleration"];

                        rotation += rotationSpeed * (float)GlobalVariables.FrameTime;
                        rotationSpeed += rotationAcceleration * (float)GlobalVariables.FrameTime;

                        if (rotationSpeed > Math.PI * 10f)
                            rotationSpeed = (float)Math.PI * 10f;
                        else if (rotationSpeed < -Math.PI * 10f)
                            rotationSpeed = -(float)Math.PI * 10f;

                        if (rotation > Math.PI * 100f)
                            rotation -= (float)Math.PI * 100f;
                        else if (rotation < -Math.PI * 100f)
                            rotation += (float)Math.PI * 100f;

                        Values["RotationSpeed"] = rotationSpeed;

                        return rotation;
                }

                return rotation;
            }

            public Color Tint(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                switch (Type)
                {
                    case 12:
                        for (int i = 0; i < (int)Values["Speed"]; i++)
                        {
                            if ((bool)Values["CUP"])
                            {
                                tint.R += 1;
                                tint.G += 1;
                                tint.B += 1;
                            }
                            else
                            {
                                tint.R -= 1;
                                tint.G -= 1;
                                tint.B -= 1;
                            }

                            if (tint.R == 255 || tint.G == 255 || tint.B == 255)
                                Values["CUP"] = false;
                            else if (tint.R == 0 || tint.G == 0 || tint.B == 0)
                                Values["CUP"] = true;
                        }

                        return tint;
                        
                    case 13:
                        if ((int)Values["Current"] >= (int)Values["Speed"])
                        {
                            tint = ColorManager.Compliment(tint)[1];
                            Values["Current"] = 0;
                        }

                        Values["Current"] = (int)Values["Current"] + 1;
                        return tint;

                    case 14:
                        if ((int)Values["Current"] >= (int)Values["Speed"])
                        {
                            tint = ColorManager.RandomFullColor();
                            Values["Current"] = 0;
                        }

                        Values["Current"] = (int)Values["Current"] + 1;
                        return tint;
                }

                return tint;
            }

            public void FinalUpdate(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint)
            {
                switch (Type)
                {
                    case 15:
                        Values["PreviousPosition"] = position;
                        break;
                    case 16:
                        Values["PreviousPosition"] = position;
                        break;
                    case 17:
                        Values["PreviousPosition"] = position;
                        break;
                    case 18:
                        Values["PreviousPosition"] = position;
                        break;
                }
            }
        }
        
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 Position;
        public Vector2 PreviousPosition;
        public Vector2 Scale;
        public float Rotation;
        public Color Tint;
        public byte WeaponType;
        public bool Collide;
        public bool Active;

        List<Actions> ActionList;
        
        public void Initialize(Vector2 acceleration, Vector2 velocity, Vector2 position, Vector2 scale, float rotation, Color tint, byte weaponType)
        {
            Acceleration = acceleration;
            Velocity = velocity;
            Position = position;
            Scale = scale;
            Rotation = rotation;
            Tint = tint;
            WeaponType = weaponType;
            Collide = false;
            Active = true;
        }

        public void Initialize(List<List<ValueType>> A)
        {
            ActionList = new List<Actions>();

            for (int i = 0; i < A.Count; i++)
            {
                ActionList.Add(new Actions());
                switch ((int)A[i][0])
                {
                    case 0:
                        ActionList[ActionList.Count - 1].Type00((float)A[i][1]);
                        break;
                    case 1:
                        ActionList[ActionList.Count - 1].Type01((float)A[i][1], (float)A[i][2]);
                        break;
                    case 2:
                        ActionList[ActionList.Count - 1].Type02((float)A[i][1], WeaponType, (int)A[i][2]);
                        break;
                    case 3:
                        ActionList[ActionList.Count - 1].Type03((float)A[i][1], WeaponType, (int)A[i][2]);
                        break;
                    case 4:
                        ActionList[ActionList.Count - 1].Type04(WeaponType, (int)A[i][1]);
                        break;
                    case 5:
                        ActionList[ActionList.Count - 1].Type05(Velocity, Position, (float)A[i][1], (float)A[i][2], (float)A[i][3]);
                        break;
                    case 6:
                        ActionList[ActionList.Count - 1].Type06(Velocity, Position, (float)A[i][1], (float)A[i][2], (float)A[i][3]);
                        break;
                    case 7:
                        ActionList[ActionList.Count - 1].Type07((int)A[i][1], (int)A[i][2], Position);
                        break;
                    case 8:
                        ActionList[ActionList.Count - 1].Type08((byte)A[i][1], (float)A[i][2], (float)A[i][3]);
                        break;
                    case 9:
                        ActionList[ActionList.Count - 1].Type09((float)A[i][1], (byte)A[i][2], (float)A[i][3], (float)A[i][4]);
                        break;
                    case 10:
                        ActionList[ActionList.Count - 1].Type10((float)A[i][1], (float)A[i][2]);
                        break;
                    case 11:
                        ActionList[ActionList.Count - 1].Type11((byte)A[i][1]);
                        break;
                    case 12:
                        ActionList[ActionList.Count - 1].Type12((int)A[i][1]);
                        break;
                    case 13:
                        ActionList[ActionList.Count - 1].Type13((int)A[i][1]);
                        break;
                    case 14:
                        ActionList[ActionList.Count - 1].Type14((int)A[i][1]);
                        break;
                    case 15:
                        ActionList[ActionList.Count - 1].Type15((int)A[i][1], Position);
                        break;
                    case 16:
                        ActionList[ActionList.Count - 1].Type16((int)A[i][1], Position);
                        break;
                    case 17:
                        ActionList[ActionList.Count - 1].Type17((int)A[i][1], (int)A[i][2], Position);
                        break;
                    case 18:
                        ActionList[ActionList.Count - 1].Type18((int)A[i][1], Position, WeaponType, (int)A[i][2]);
                        break;
                    case 19:
                        ActionList[ActionList.Count - 1].Type19((float)A[i][1], (float)A[i][2], (float)A[i][3], (bool)A[i][4]);
                        break;
                }
            }
        }


        public void Update()
        {
            PreviousPosition = Position;

            if (ActionList != null)
                for (int i = 0; i < ActionList.Count; i++)
                {
                    Position = ActionList[i].Position(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                    Velocity = ActionList[i].Velocity(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                    Acceleration = ActionList[i].Acceleration(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                    Scale = ActionList[i].Scale(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                    Rotation = ActionList[i].Rotation(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                    Tint = ActionList[i].Tint(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                }

            if (ActionList != null)
                for (int i = 0; i < ActionList.Count; i++)
                {
                    ActionList[i].FinalUpdate(Acceleration, Velocity, Position, Scale, Rotation, Tint);
                }
            
            Velocity += Acceleration;
            Position += Velocity;
            Acceleration = Vector2.Zero;
        }
    }
}
