using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    public enum WaveTypeEnum
    {
        Sine,
        Square,
        Sawtooth,
        Triangle,
        Noise,
        Pulse,
    }

    public abstract class ParticleMovement
    {
        public abstract void Update(Particle particle);
        public abstract ParticleMovement Clone(ParticleMovement move);
    }

    static class PM
    {
        public class RandomMovement : ParticleMovement
        {
            Random rand;
            public float Intensity;
            bool Relative;
            Vector2 Direction;

            public RandomMovement();

            public override ParticleMovement Clone(RandomMovement movement)
            {
                RandomMovement pm = new RandomMovement();

                pm.rand = movement.rand;
                pm.Intensity = movement.Intensity;
                pm.Relative = movement.Relative;
                pm.Direction = movement.Direction;

                return pm;
            }

            public RandomMovement(Random Rand, float intensity, bool relative, Vector2 direction)
            {
                rand = Rand;
                Intensity = intensity;
                Relative = relative;
                Direction = direction;
                Direction.Normalize();
            }

            public override void Update(Particle particle)
            {
                float Acceleration = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;

                if (!Relative)
                    particle.Acceleration += Acceleration * Direction;
                else
                {
                    Vector2 pos = particle.Position - particle.PreviousPosition;
                    float angle = UsefulMethods.VectorToAngle(pos);
                    float angle2 = UsefulMethods.VectorToAngle(Direction);

                    pos = UsefulMethods.AngleToVector(angle + angle2);

                    particle.Acceleration += Acceleration * pos;
                }
            }
        }
        
        public class ApplyAcceleration : ParticleMovement
        {
            public Vector2 Acceleration;

            public ApplyAcceleration();

            public override ParticleMovement Clone(ApplyAcceleration movement)
            {
                ApplyAcceleration pm = new ApplyAcceleration();

                pm.Acceleration = movement.Acceleration;

                return pm;
            }

            public ApplyAcceleration(Vector2 acceleration)
            {
                Acceleration = acceleration;
            }

            public override void Update(Particle particle)
            {
                particle.Acceleration += Acceleration;
            }
        }

        public class SetMaxSpeed : ParticleMovement
        {
            public float MaxSpeed;
            public float OriginalMaxSpeed;

            public SetMaxSpeed();

            public override ParticleMovement Clone(SetMaxSpeed movement)
            {
                SetMaxSpeed pm = new SetMaxSpeed();

                pm.MaxSpeed = movement.MaxSpeed;
                pm.OriginalMaxSpeed = movement.OriginalMaxSpeed;

                return pm;
            }

            public SetMaxSpeed(float maxSpeed)
            {
                MaxSpeed = maxSpeed;
                OriginalMaxSpeed = maxSpeed;
            }

            public override void Update(Particle particle)
            {
                Vector2 speed = particle.Velocity;
                speed.Normalize();

                float diff = particle.Velocity.Length() - (MaxSpeed);

                if (diff > 0f)
                    particle.Velocity -= diff * speed;
            }
        }

        public class SetMinSpeed : ParticleMovement
        {
            public float MinSpeed;
            public float OriginalMaxSpeed;

            public SetMinSpeed();

            public override ParticleMovement Clone(SetMinSpeed movement)
            {
                SetMinSpeed pm = new SetMinSpeed();

                pm.MinSpeed = movement.MinSpeed;
                pm.OriginalMaxSpeed = movement.OriginalMaxSpeed;

                return pm;
            }

            public SetMinSpeed(float minSpeed)
            {
                MinSpeed = minSpeed;
                OriginalMaxSpeed = minSpeed;
            }

            public override void Update(Particle particle)
            {
                Vector2 speed = particle.Velocity;
                speed.Normalize();

                float diff = particle.Velocity.Length() - (MinSpeed);

                if (diff < 0f)
                    particle.Velocity -= diff * speed;
            }
        }

        public class RightAngle : ParticleMovement
        {
            float Timer;
            float MinTime;
            float Maxtime;
            float CurrentTime;
            float Angle;
            Random Rand;

            public RightAngle();

            public override ParticleMovement Clone(RightAngle movement)
            {
                RightAngle pm = new RightAngle();

                pm.Timer = movement.Timer;
                pm.MinTime = movement.MinTime;
                pm.Maxtime = movement.Maxtime;
                pm.CurrentTime = movement.CurrentTime;
                pm.Angle = movement.Angle;
                pm.Rand = movement.Rand;

                return pm;
            }

            public RightAngle(float minTime, float maxTime, float angle, Random rand)
            {
                Timer = 0f;
                CurrentTime = 0f;
                MinTime = minTime;
                Maxtime = maxTime;
                Rand = rand;
                Angle = angle;
            }

            public override void Update(Particle particle)
            {
                if (CurrentTime == 0f)
                    CurrentTime = UsefulMethods.FindBetween((float)Rand.NextDouble(), 1f, 0f, Maxtime, MinTime, false);

                if (Timer > CurrentTime)
                {
                    float angle = UsefulMethods.VectorToAngle(particle.Velocity);

                    if (Rand.Next(0, 2) == 0)
                        angle += Angle;
                    else
                        angle -= Angle;

                    particle.Velocity = UsefulMethods.AngleToVector(angle) * particle.Velocity.Length();

                    Timer = 0f;
                    CurrentTime = UsefulMethods.FindBetween((float)Rand.NextDouble(), 1f, 0f, Maxtime, MinTime, false);
                }

                Timer += GlobalVariables.WorldTime;
            }

        }

        public class Wave : ParticleMovement
        {
            float Amplitude;
            float Frequency;
            WaveTypeEnum WaveType;
            Random rand;
            float DutyCycle;

            public Wave();

            public override ParticleMovement Clone(Wave movement)
            {
                Wave pm = new Wave();

                pm.Amplitude = movement.Amplitude;
                pm.Frequency = movement.Frequency;
                pm.WaveType = movement.WaveType;
                pm.rand = movement.rand;
                pm.DutyCycle = movement.DutyCycle;

                return pm;
            }

            public Wave(Random Rand, float amplitude, float frequency, float dutyCycle, WaveTypeEnum waveType)
            {
                Amplitude = amplitude;
                Frequency = frequency;
                WaveType = waveType;
                rand = Rand;
                DutyCycle = dutyCycle;
            }

            public override void Update(Particle particle)
            {
                float num;
                //= (float)Math.Sin(Frequency * particle.TimeAlive * 2f * MathHelper.Pi) * Amplitude;

                switch (WaveType)
                {
                    default /* WaveType.Sine */ :
                        num = (float)Math.Sin(Frequency * particle.TimeAlive * 2f * MathHelper.Pi) * Amplitude;
                        break;
                    case WaveTypeEnum.Square:
                        num = (float)Math.Sin(Frequency * particle.TimeAlive * 2f * MathHelper.Pi) >= 0 ? Amplitude : -Amplitude;
                        break;
                    case WaveTypeEnum.Sawtooth:
                        num = 2f * (particle.TimeAlive * Frequency - (float)Math.Floor(particle.TimeAlive * Frequency + 0.5f)) * Amplitude;
                        break;
                    case WaveTypeEnum.Triangle:
                        num = Math.Abs(2f * (particle.TimeAlive * Frequency - (float)Math.Floor(particle.TimeAlive * Frequency + 0.5f))) * Amplitude;
                        break;
                    case WaveTypeEnum.Pulse:
                        
                            float period = 1f / Frequency;
                            float timeModulusPeriod = particle.TimeAlive - (float)Math.Floor(particle.TimeAlive / period) * period;
                            float phase = timeModulusPeriod / period;
                            if (phase <= DutyCycle)
                                num = Amplitude;
                            else
                                num = -Amplitude;
                            break;
                        
                    case WaveTypeEnum.Noise:
                        num = (float)(rand.NextDouble() - rand.NextDouble()) * Amplitude;
                        break;
                }



                Vector2 pos = Vector2.Transform(particle.Velocity, Matrix.CreateRotationZ(MathHelper.Pi / 2f));

                if (pos != Vector2.Zero)
                {
                    pos.Normalize();
                    particle.PositionModifier += pos * num;
                }
                else
                {
                    particle.PositionModifier += new Vector2(0, 1) * num;
                }
            }
        }

        public class Freeze : ParticleMovement
        {
            float Timer;
            float MinTime;
            float Maxtime;
            float CurrentTime;
            float Speed;
            Vector2 VelocityStorage;
            Random Rand;
            bool SpeedActive;

            public Freeze()
            {
            }

            public override ParticleMovement Clone(Freeze movement)
            {
                Freeze pm = new Freeze();

                pm.Timer = movement.Timer;
                pm.MinTime = movement.MinTime;
                pm.Maxtime = movement.Maxtime;
                pm.CurrentTime = movement.CurrentTime;
                pm.Speed = movement.Speed;
                pm.VelocityStorage = movement.VelocityStorage;
                pm.Rand = movement.Rand;
                pm.SpeedActive = movement.SpeedActive;

                return pm;
            }

            public Freeze(float minTime, float maxTime, float speed, Random rand)
            {
                Timer = 0f;
                CurrentTime = 0f;
                MinTime = minTime;
                Maxtime = maxTime;
                Rand = rand;
                Speed = speed;
            }

            public override void Update(Particle particle)
            {
                if (CurrentTime == 0f)
                    CurrentTime = UsefulMethods.FindBetween((float)Rand.NextDouble(), 1f, 0f, Maxtime, MinTime, false);

                if (SpeedActive)
                    particle.Position = VelocityStorage;

                
                if (Timer > CurrentTime)
                {
                    SpeedActive = !SpeedActive;

                    if (SpeedActive)
                        VelocityStorage = particle.Position;
                    else
                        particle.Position = VelocityStorage;
                    
                    Timer = 0f;
                    CurrentTime = UsefulMethods.FindBetween((float)Rand.NextDouble(), 1f, 0f, Maxtime, MinTime, false);
                }

                Timer += GlobalVariables.WorldTime;
            }
        }
    }
}
