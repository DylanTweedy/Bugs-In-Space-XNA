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
    }

    static class PM
    {
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


        public class Wave : ParticleMovement
        {
            float Amplitude;
            float Frequency;
            WaveTypeEnum WaveType;
            Random rand;
            float DutyCycle;

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
    }
}
