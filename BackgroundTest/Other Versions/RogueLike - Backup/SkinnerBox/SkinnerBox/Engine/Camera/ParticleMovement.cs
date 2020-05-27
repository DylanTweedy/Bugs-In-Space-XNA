using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    [Serializable()]
    public enum WaveTypeEnum
    {
        Sine,
        Square,
        Sawtooth,
        Triangle,
        Noise,
        Pulse,
    }

    [Serializable()]
    public abstract class ParticleMovement
    {
        public abstract void Update(Particle particle);
    }

    [Serializable()]
    static class PM
    {
        [Serializable()]
        public class RandomMovement : ParticleMovement
        {
            Random rand;
            public float Intensity;
            bool Relative;
            Vector2 Direction;

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

        [Serializable()]
        public class ApplyAcceleration : ParticleMovement
        {
            public Vector2 Acceleration;

            public ApplyAcceleration(Vector2 acceleration)
            {
                Acceleration = acceleration;
            }

            public override void Update(Particle particle)
            {
                particle.Acceleration += Acceleration;
            }
        }

        [Serializable()]
        public class SetMaxSpeed : ParticleMovement
        {
            public float MaxSpeed;
            public float OriginalMaxSpeed;

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

        [Serializable()]
        public class SetMinSpeed : ParticleMovement
        {
            public float MinSpeed;
            public float OriginalMaxSpeed;

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

        [Serializable()]
        public class RightAngle : ParticleMovement
        {
            float Timer;
            float MinTime;
            float Maxtime;
            float CurrentTime;
            float Angle;
            Random Rand;

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
                    if (Rand.Next(0, 2) == 0)
                        particle.Velocity = Vector2.Transform(particle.Velocity, Matrix.CreateRotationZ(Angle));
                    else
                        particle.Velocity = Vector2.Transform(particle.Velocity, Matrix.CreateRotationZ(-Angle));

                    Timer = 0f;
                    CurrentTime = UsefulMethods.FindBetween((float)Rand.NextDouble(), 1f, 0f, Maxtime, MinTime, false);
                }

                Timer += GlobalVariables.WorldTime;
            }

        }

        [Serializable()]
        public class Wave : ParticleMovement
        {
            float Amplitude;
            float Frequency;
            WaveTypeEnum WaveType;
            Random rand;
            float DutyCycle;
            float Angle;

            public Wave(Random Rand, float amplitude, float frequency, float dutyCycle, WaveTypeEnum waveType, float angle)
            {
                Amplitude = amplitude;
                Frequency = frequency;
                WaveType = waveType;
                rand = Rand;
                DutyCycle = dutyCycle;
                Angle = angle;
            }

            public override void Update(Particle particle)
            {
                float num;
                //= (float)Math.Sin(Frequency * particle.TimeAlive * 2f * MathHelper.Pi) * Amplitude;

                float frequency = Frequency * particle.Velocity.Length();
                float amplitude = Amplitude * particle.Velocity.Length();

                switch (WaveType)
                {
                    default /* WaveType.Sine */ :
                        num = (float)Math.Sin(frequency * particle.TimeAlive * 2f * MathHelper.Pi) * amplitude;
                        break;
                    case WaveTypeEnum.Square:
                        num = (float)Math.Sin(frequency * particle.TimeAlive * 2f * MathHelper.Pi) >= 0 ? amplitude : -amplitude;
                        break;
                    case WaveTypeEnum.Sawtooth:
                        num = 2f * (particle.TimeAlive * frequency - (float)Math.Floor(particle.TimeAlive * frequency + 0.5f)) * amplitude;
                        break;
                    case WaveTypeEnum.Triangle:
                        num = Math.Abs(2f * (particle.TimeAlive * frequency - (float)Math.Floor(particle.TimeAlive * frequency + 0.5f))) * amplitude;
                        break;
                    case WaveTypeEnum.Pulse:

                        float period = 1f / frequency;
                        float timeModulusPeriod = particle.TimeAlive - (float)Math.Floor(particle.TimeAlive / period) * period;
                        float phase = timeModulusPeriod / period;
                        if (phase <= DutyCycle)
                            num = amplitude;
                        else
                            num = -amplitude;
                        break;

                    case WaveTypeEnum.Noise:
                        num = (float)(rand.NextDouble() - rand.NextDouble()) * amplitude;
                        break;
                }



                Vector2 pos = Vector2.Transform(particle.Velocity, Matrix.CreateRotationZ(Angle));

                if (pos != Vector2.Zero)
                {
                    pos.Normalize();
                    particle.PositionModifier += pos * num * GlobalVariables.WorldTime;
                }
                else
                {
                    particle.PositionModifier += new Vector2(0, 1) * num * GlobalVariables.WorldTime;
                }
            }
        }

        [Serializable()]
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
                {
                    particle.Position -= (particle.Position - VelocityStorage) * (1f - Speed);
                    VelocityStorage = particle.Position;
                }
                
                if (Timer > CurrentTime)
                {
                    SpeedActive = !SpeedActive;

                    if (SpeedActive)
                        VelocityStorage = particle.Position;
                    else
                    {
                        particle.Position -= (particle.Position - VelocityStorage) * (1f - Speed);
                        VelocityStorage = particle.Position;
                    }

                    Timer = 0f;
                    CurrentTime = UsefulMethods.FindBetween((float)Rand.NextDouble(), 1f, 0f, Maxtime, MinTime, false);
                }

                Timer += GlobalVariables.WorldTime;
            }
        }
    }
}
