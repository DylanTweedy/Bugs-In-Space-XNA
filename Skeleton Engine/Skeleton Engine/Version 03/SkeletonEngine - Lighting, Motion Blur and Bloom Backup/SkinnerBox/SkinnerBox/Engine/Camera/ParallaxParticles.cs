using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public class ParallaxParticles
    {

        List<Particle> Particles = new List<Particle>();
        List<ParallaxParticle> ParticlesPara = new List<ParallaxParticle>();
        Random rand;

        float SmallestScale;
        float LargestScale;
        float DrawScale;
        public float DrawAlpha;
        bool Glow;
        bool RotateWithMovement;

        Vector2 ViewportSize;
        Vector2 PreviousViewport;
        float ViewportRadius;
        Vector2 CameraVelocity;
        float CameraZoom;

        int DesiredParticles;
        float ParticleMultiplier;
        float PreviousParticlesMultiplier;

        int RemoveParticles;

        string ParticleType;

        //public byte MoveType;
        List<ParticleMovement> Movement = new List<ParticleMovement>();

        List<Color> Palette = new List<Color>();
        Camera Cam;

        float Intensity;

        public ParallaxParticles()
        {
            rand = new Random();
            
            ParticleType = "Stars";
        }
        
        /// <summary>
        /// Sets the state of particles.
        /// </summary>
        /// <param name="smallestScale">Smallest a particle can be. Less that 1 for the background, more than 1 for the foreground.</param>
        /// <param name="largestScale">Largest a particle can be. Less that 1 for the background, more than 1 for the foreground.</param>
        /// <param name="particleMultiplier">Amount of particles on screen.</param>
        /// <param name="particleType">The folder to get particles from.</param>
        /// <param name="palette">The color palette.</param>
        /// <param name="drawScale">Scale to draw images.</param>
        /// <param name="drawAlpha">The Alpha value of all particles</param>
        /// <param name="glow">Whether or not the particle glows.</param>
        /// <param name="rotateWithMovement">If the particle rotates to face the direction it is moving.</param>
        /// <param name="moveType">The way the particle moves.</param>
        public void SetState(float smallestScale, float largestScale, float particleMultiplier, string particleType, List<Color> palette, float drawScale, float drawAlpha, bool glow, bool rotateWithMovement, List<ParticleMovement> movement)
        {
            if (smallestScale == 0f)
                smallestScale = 0.0001f;

            RotateWithMovement = rotateWithMovement;
            SmallestScale = smallestScale;
            LargestScale = largestScale;
            DrawScale = drawScale;
            DrawAlpha = drawAlpha;
            Glow = glow;

            ParticleMultiplier = particleMultiplier;

            ParticleType = particleType;
            Palette = palette;

            Movement = movement;

            for (int i = 0; i < Particles.Count; i++)
                if (!Particles[i].Remove)
                {
                    RemoveParticles++;
                    Particles[i].Remove = true;
                }            
        }

        private void GetPalette()
        {
                    Palette.Clear();
                    //Palette = ColorManager.Triad(ColorManager.RandomFullColor(), false, 150);
                    //Palette = ColorManager.Compliment(Color.Green);
                    //Palette = ColorManager.GetShades(Palette);

                    Palette = ColorManager.Spectrum(ColorManager.RandomFullColor());
                    //Palette.Add(new Color(175, 175, 175));
                    //Palette.AddRange(ColorManager.GetShades(new Color(0, 0, 0), 0.6f));


                    //Palette = ColorManager.Compliment(ColorManager.RandomFullColor());
                    //Palette = ColorManager.GetShades(Palette, 0.6f);

        }

        private Color GetColour()
        {
            Color C = Color.White;

                    C = Palette[rand.Next(0, Palette.Count)];

            return C;
        }

        private void SetupParticles()
        {
            //Add particles.
            if (Particles.Count - RemoveParticles < DesiredParticles)
            {
                int particles = (int)(DesiredParticles - Particles.Count + RemoveParticles);
                int particles2 = (int)(DesiredParticles * GlobalVariables.WorldTime);


                if (particles2 < particles && particles2 != 0)
                    particles = particles2;
                else if (particles != 0)
                    particles = 1;

                if (particles != 0)
                {
                    float zoomMult = 1f / SmallestScale;

                    for (int o = 0; o < particles; o++)
                        if (Particles.Count - RemoveParticles < DesiredParticles)
                        {
                            //Add new particle.
                            int i = Particles.Count;
                            Particles.Add(new Particle());
                            ParticlesPara.Add(new ParallaxParticle());
                                                       

                            //Set particle size.
                            ParticlesPara[i].Depth = (rand.Next((int)(SmallestScale * 100000), (int)(LargestScale * 100000)) / 100000f) * CameraZoom;
                            ParticlesPara[i].MinDepth = SmallestScale;
                            ParticlesPara[i].MaxDepth = LargestScale;
                            Particles[i].Scale = Vector2.One * (DrawScale + (DrawScale * (((float)rand.NextDouble() * 0.2f) - 0.1f)));
                            Particles[i].Glow = Glow;
                            Particles[i].RotateWithMovement = RotateWithMovement;
                            //Particles[i].MoveType = MoveType;

                            Particles[i].Movement.AddRange(Movement);

                            //Set particle texture.
                            //Needs work to make more flexible.
                            //////////////////////////
                            Particles[i].Tex = SheetManager.GetRandomTexture(EngineSettings.ParticlesLocation + ParticleType, rand);
                            //////////////////////////

                            //Set initial size calculations.
                            float Size = ParticlesPara[i].Depth * CameraZoom;
                            float SizeMult = Size * zoomMult;

                            //Set the viewport radius based on camera zoom.
                            float diff = Size / SmallestScale;
                            float move = ViewportRadius * diff;

                            

                            //Set particle position.
                            float y = ((float)rand.NextDouble() * (float)rand.NextDouble());
                            Particles[i].Position = new Vector2(0, (1 - y) * move);
                            Particles[i].Position = Vector2.Transform(Particles[i].Position, Matrix.CreateRotationZ((float)(rand.NextDouble() * (Math.PI * 2))));
                            
                            //Set particle rotation.
                            Particles[i].Rotation = (float)(rand.NextDouble() * MathHelper.TwoPi);

                            //Set particle color and alpha.
                            Particles[i].Alpha = 0f;
                            Particles[i].Tint = GetColour();
                        }
                }
            }
            //Flag particles for removal.
            else if (Particles.Count - RemoveParticles > DesiredParticles)
            {
                int particles = (int)(Particles.Count - DesiredParticles);
                int particles2 = (int)(particles * GlobalVariables.WorldTime * 0.5f);
                if (particles2 < particles && particles2 >= 1)
                    particles = particles2;

                for (int o = 0; o < particles; o++)
                    if (Particles.Count - RemoveParticles > DesiredParticles)
                    {
                        Particles[Particles.Count - 1 - RemoveParticles].Remove = true;
                        RemoveParticles++;
                    }
            }
        }
        
        public void Update(Camera cam, float intensity)
        {
            //Set initial data.
            Cam = cam;
            Intensity = intensity;
            //ViewportSize = new Vector2(Cam.viewport.Width, Cam.viewport.Height);
            CameraVelocity = Cam.PreviousPosition - Cam.Position;
            CameraZoom = Cam.Zoom;

            //If viewport changes 
            if (ViewportSize != PreviousViewport)
                SetState(SmallestScale, LargestScale, ParticleMultiplier, ParticleType, Palette, DrawScale, DrawAlpha, Glow, RotateWithMovement, Movement);
                    //MoveType);

            //Set desired particles for viewport size.
            float RangeX = (float)((ViewportSize.X / CameraZoom));
            float RangeY = (float)((ViewportSize.Y / CameraZoom));
            ViewportRadius = (float)Math.Sqrt((RangeX * RangeX) + (RangeY * RangeY)) / 2f;
            DesiredParticles = (int)(Math.Sqrt(ViewportSize.X * ViewportSize.Y) * ParticleMultiplier * EngineSettings.ParticleCount);

            DesiredParticles = 100;
            //If the number of particles does not meet the desired particle count add or remove particles.
            if (Particles.Count != DesiredParticles)
                SetupParticles();

            //Update the particles movement.
            //if (CameraVelocity != Vector2.Zero || MoveType != 0)
            UpdateParticles();

            //Set data for next loop.
            PreviousViewport = ViewportSize;
            PreviousParticlesMultiplier = ParticleMultiplier;
        }

        /// <summary>
        /// Updates the particles movement.
        /// </summary>
        private void UpdateParticles()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                //Set initial size calculations.
                float zoomMult = 1f / ParticlesPara[i].MinDepth;
                float Size = ParticlesPara[i].Depth * CameraZoom;
                float SizeMult = Size * zoomMult;

                //Move particles.
                Particles[i].Position += CameraVelocity * Size;
                MoveParticles(i, Size);

                //Rotate particles if necessary.
                if (Particles[i].RotateWithMovement)
                {
                    float diff = Particles[i].Rotation - UsefulMethods.VectorToAngle(-(Particles[i].Velocity + Particles[i].PositionModifier));
                    Particles[i].Rotation -= diff * GlobalVariables.WorldTime * 10f;
                }

                Size = ParticlesPara[i].Depth * CameraZoom;
                SizeMult = Size * zoomMult;

                //Set particles alpha value based on Z position.
                float alpha = Math.Abs(UsefulMethods.FindBetween(Size, ParticlesPara[i].MaxDepth, ParticlesPara[i].MinDepth, 1f, -1f, false));
                float finalAlpha = UsefulMethods.FindBetween(alpha, 1f, 0.65f, 1f, 0f, true);

                if (Particles[i].Remove)
                {
                    if (Particles[i].Alpha < 0.01f)
                        Particles[i].Alpha = 0f;
                    else
                        Particles[i].Alpha -= Particles[i].Alpha * (float)GlobalVariables.WorldTime;

                    if (Particles[i].Alpha == 0f)
                    {
                        RemoveParticles--;

                        Particles.RemoveAt(i);
                        ParticlesPara.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                else if (finalAlpha > Particles[i].Alpha)
                {
                    float tintDifference = finalAlpha - Particles[i].Alpha;

                    if (tintDifference < 0.01f)
                        Particles[i].Alpha = finalAlpha;
                    else
                        Particles[i].Alpha += tintDifference * ((float)GlobalVariables.FrameTime * 10f);
                }
                else
                    Particles[i].Alpha = finalAlpha;
                
                //Set initial border variables.
                Vector2 Normal = Particles[i].Position;
                float Distance = Normal.Length();
                Normal.Normalize();
                float Radius = ViewportRadius * SizeMult;

                //If a particle goes outside the border move it to the opposite side.
                if (Distance > Radius)
                {
                    Distance -= (Distance - Radius) * 2;
                    Particles[i].Position = -Distance * Normal;
                }

                //If a particle goes too far back bring it to the front, and vice versa.
                if (Size > ParticlesPara[i].MaxDepth)
                {
                    float diff = ParticlesPara[i].MaxDepth / ParticlesPara[i].MinDepth;

                    ParticlesPara[i].Depth /= diff;

                    Particles[i].Position = Normal * (Distance / diff);
                }
                else if (Size < ParticlesPara[i].MinDepth)
                {
                    float diff = ParticlesPara[i].MaxDepth / ParticlesPara[i].MinDepth;

                    ParticlesPara[i].Depth *= diff;

                    Particles[i].Position = Normal * (Distance * diff);
                }

                //Set the particles previous position.
                Particles[i].PreviousPosition = Particles[i].Position;
            }
        }

        private void MoveParticles(int i, float Size)
        {
            #region movetype

            float max;

            #region old

            //switch (Particles[i].MoveType)
            //{
            //    case 0:
            //        //Particles[i].Position += new Vector2(0.01f, 0.01f) * Size;
            //        break;

            //    //Snow
            //    case 1:
            //        Particles[i].Acceleration.X = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
            //        Particles[i].Acceleration.Y = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
            //        //Particles[i].Acceleration.Y += Cam.Gravity;

            //        max = 250f * Intensity;

            //        if (Particles[i].Velocity.X > max)
            //            Particles[i].Velocity.X = max;
            //        if (Particles[i].Velocity.X < -max)
            //            Particles[i].Velocity.X = -max;

            //        if (Particles[i].Velocity.Y > max)
            //            Particles[i].Velocity.Y = max;
            //        if (Particles[i].Velocity.Y < -max)
            //            Particles[i].Velocity.Y = -max;

            //        ParticlesPara[i].EnvironmentVelocity = Cam.Wind * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 1.5f, 0.5f, false);
            //        ParticlesPara[i].EnvironmentVelocity.Y += Cam.Gravity * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 1.5f, 0.5f, false);
            //        break;

            //    case 2:
            //        Particles[i].Acceleration.X = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
                    
            //        max = 25f * Intensity;

            //        if (Particles[i].Velocity.X > max)
            //            Particles[i].Velocity.X = max;
            //        if (Particles[i].Velocity.X < -max)
            //            Particles[i].Velocity.X = -max;


            //        ParticlesPara[i].EnvironmentVelocity = Cam.Wind * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 1.5f, 0.5f, false);
            //        ParticlesPara[i].EnvironmentVelocity.Y += Cam.Gravity * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 3f, 2f, false);
            //        break;

            //    case 3:
                    
            //        Particles[i].Acceleration.X = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
            //        Particles[i].Acceleration.Y = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;

            //        max = 250f * Intensity;

            //        if (Particles[i].Velocity.X > max)
            //            Particles[i].Velocity.X = max;
            //        if (Particles[i].Velocity.X < -max)
            //            Particles[i].Velocity.X = -max;

            //        if (Particles[i].Velocity.Y > max)
            //            Particles[i].Velocity.Y = max;
            //        if (Particles[i].Velocity.Y < -max)
            //            Particles[i].Velocity.Y = -max;
            //        break;

            //    case 4:
            //        Particles[i].Position += new Vector2(0.3f, 0.3f) * Size;
            //        //Particles[i].Position += new Vector2((float)new Random(i + Particles.Count).NextDouble() * 10000f, (float)new Random(i + Particles.Count + Particles.Count).NextDouble() * 10000f) * Size;


            //        float zoomMult = 1f / ParticlesPara[i].MinDepth;
            //        Size = ParticlesPara[i].Depth * CameraZoom;
            //        float SizeMult = Size * zoomMult;

            //        Vector2 NormalTest = Particles[i].Position;
            //        float DistanceTest = NormalTest.Length();
            //        NormalTest.Normalize();

            //        float diffTest = ((float)new Random(i).NextDouble() * 0.01f) + 0.995f;
            //        diffTest = 0.9995f;

            //        ParticlesPara[i].Depth /= diffTest;
            //        Size = ParticlesPara[i].Depth * CameraZoom;
            //        SizeMult = Size * zoomMult;

            //        Particles[i].Position = NormalTest * (DistanceTest / diffTest);

            //        Size = ParticlesPara[i].Depth * CameraZoom;
            //        SizeMult = Size * zoomMult;
            //        break;
            //}

            #endregion

            Particles[i].PositionModifier = Vector2.Zero;


            

            //Movement.Clear();

            ////Movement.Add(new ParticleControl.ApplyGravity(Particles[i], new Vector2(0, Cam.Gravity)));
            ////Movement.Add(new ParticleControl.ApplyWind(Particles[i], Cam.Wind));
            ////Movement.Add(new ParticleControl.RandomMovement(rand, Particles[i], Intensity, true, new Vector2(1, 0)));
            ////Movement.Add(new ParticleControl.RandomMovement(rand, Particles[i], Intensity, true, new Vector2(0, 0.5f)));

            //Movement.Add(new ParticleControl.Wave(rand, Particles[i], 50f, 0.75f, 0.75f, WaveTypeEnum.Sine));



            //Movement.Add(new ParticleControl.SetMaxSpeed(Particles[i], (250f * Size)));


            for (int o = 0; o < Particles[i].Movement.Count; o++)
            {
                string test = Particles[i].Movement[o].GetType().Name;

                //switch (test)
                //{
                //    case "SetMaxSpeed":
                //        ((PM.SetMaxSpeed)Particles[i].Movement[o]).MaxSpeed = ((PM.SetMaxSpeed)Particles[i].Movement[o]).OriginalMaxSpeed * Size;
                //        break;

                //    case "ApplyAcceleration":
                //        ((PM.ApplyAcceleration)Particles[i].Movement[o]).Acceleration = Cam.Wind + new Vector2(0, Cam.Gravity);
                //        break;

                //    case "RandomMovement":
                //        ((PM.RandomMovement)Particles[i].Movement[o]).Intensity = Intensity;
                //        break;
                //}


                //if ()
                //{
                //}



                Particles[i].Movement[o].Update(Particles[i]);
            }

            Particles[i].Acceleration *= Size;
            Particles[i].Update();


            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam, bool front)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                float size = ParticlesPara[i].Depth * CameraZoom;

                if ((front && size > 1f) || (!front && size <= 1f))
                {
                    float angle = Particles[i].Rotation;
                    
                    //if (Particles[i].MoveType == 2)
                    //{
                    //    float speed = (Particles[i].Velocity.Length() + ParticlesPara[i].EnvironmentVelocity.Length()) * CameraZoom;
                    //    stretch.Y = UsefulMethods.FindBetween(speed, 1000f, 250, 3f, 1f, false);
                    //}

                    float scale = 0f;
                    if (size < 1f)
                        scale = ParticlesPara[i].Depth;

                    Particles[i].Tex.Draw(spriteBatch, Particles[i].Position + cam.Position + Particles[i].PositionModifier,
                        Particles[i].Tint * Particles[i].Alpha * DrawAlpha,
                        angle, ParticlesPara[i].Depth * Particles[i].Scale,
                        SpriteEffects.None, scale);

                    if (Particles[i].Glow)
                        Particles[i].Tex.Draw(spriteBatch, Particles[i].Position + cam.Position + Particles[i].PositionModifier,
                            Color.White * Particles[i].Alpha * DrawAlpha,
                            angle, ParticlesPara[i].Depth * (Particles[i].Scale * 0.5f),
                            SpriteEffects.None, scale);
                }
            }
        }

        public void DrawFront(SpriteBatch spriteBatch)
        {
        }
    }
}
