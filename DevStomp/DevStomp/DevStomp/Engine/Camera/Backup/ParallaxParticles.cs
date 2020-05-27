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

        List<ParallaxParticle> Particles = new List<ParallaxParticle>();
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

        public byte MoveType;

        List<Color> Palette = new List<Color>();
        Camera Cam;

        float Intensity;

        public ParallaxParticles()
        {
            rand = new Random();
            
            MoveType = 0;

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
        public void SetState(float smallestScale, float largestScale, float particleMultiplier, string particleType, List<Color> palette, float drawScale, float drawAlpha, bool glow, bool rotateWithMovement, byte moveType)
        {
            if (smallestScale == 0f)
                smallestScale = 0.0001f;

            MoveType = moveType;
            RotateWithMovement = rotateWithMovement;
            SmallestScale = smallestScale;
            LargestScale = largestScale;
            DrawScale = drawScale;
            DrawAlpha = drawAlpha;
            Glow = glow;

            ParticleMultiplier = particleMultiplier;

            ParticleType = particleType;
            Palette = palette;

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
                            Particles.Add(new ParallaxParticle());

                            //Set particle size.
                            Particles[i].Scale = (rand.Next((int)(SmallestScale * 100000), (int)(LargestScale * 100000)) / 100000f) * CameraZoom;
                            Particles[i].SmallestScale = SmallestScale;
                            Particles[i].LargestScale = LargestScale;
                            Particles[i].DrawScale = DrawScale + (DrawScale * (((float)rand.NextDouble() * 0.2f) - 0.1f));
                            Particles[i].Glow = Glow;
                            Particles[i].RotateWithMovement = RotateWithMovement;
                            Particles[i].MoveType = MoveType;

                            //Set particle texture.
                            //Needs work to make more flexible.
                            //////////////////////////
                            Particles[i].Tex = SheetTest.GetRandomTexture(EngineSettings.ParticlesLocation + ParticleType, rand);
                            //////////////////////////

                            //Set initial size calculations.
                            float Size = Particles[i].Scale * CameraZoom;
                            float SizeMult = Size * zoomMult;

                            //Set the viewport radius based on camera zoom.
                            float diff = Size / SmallestScale;
                            float move = ViewportRadius * diff;

                            

                            //Set particle position.
                            float y = ((float)rand.NextDouble() * (float)rand.NextDouble());
                            Particles[i].Position = new Vector2(0, (1 - y) * move);
                            Particles[i].Position = Vector2.Transform(Particles[i].Position, Matrix.CreateRotationZ((float)(rand.NextDouble() * (Math.PI * 2))));

                            //Set the particles texture scale.
                            Particles[i].TextureScale = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1.1f, 0.9f, false);

                            //Set particle rotation.
                            Particles[i].Angle = (float)(rand.NextDouble() * MathHelper.TwoPi);

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
            ViewportSize = new Vector2(Cam.viewport.Width, Cam.viewport.Height);
            CameraVelocity = Cam.PreviousPosition - Cam.Position;
            CameraZoom = Cam.Zoom;

            //If viewport changes 
            if (ViewportSize != PreviousViewport)
                SetState(SmallestScale, LargestScale, ParticleMultiplier, ParticleType, Palette, DrawScale, DrawAlpha, Glow, RotateWithMovement, MoveType);

            //Set desired particles for viewport size.
            float RangeX = (float)((ViewportSize.X / CameraZoom));
            float RangeY = (float)((ViewportSize.Y / CameraZoom));
            ViewportRadius = (float)Math.Sqrt((RangeX * RangeX) + (RangeY * RangeY)) / 2f;
            DesiredParticles = (int)(Math.Sqrt(ViewportSize.X * ViewportSize.Y) * ParticleMultiplier * EngineSettings.ParticleCount);

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
                float zoomMult = 1f / Particles[i].SmallestScale;
                float Size = Particles[i].Scale * CameraZoom;
                float SizeMult = Size * zoomMult;

                //Move particles.
                Particles[i].Position += CameraVelocity * Size;
                MoveParticles(i, Size);

                //Rotate particles if necessary.
                if (Particles[i].RotateWithMovement)
                {
                    float diff = Particles[i].Angle - UsefulMethods.VectorToAngle(-(Particles[i].Velocity + Particles[i].EnvironmentVelocity));
                    Particles[i].Angle -= diff * GlobalVariables.WorldTime * 10f;
                }

                Size = Particles[i].Scale * CameraZoom;
                SizeMult = Size * zoomMult;

                //Set particles alpha value based on Z position.
                float alpha = Math.Abs(UsefulMethods.FindBetween(Size, Particles[i].LargestScale, Particles[i].SmallestScale, 1f, -1f, false));
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
                        Particles[i].Alpha += tintDifference * ((float)GlobalVariables.FrameTime * 1f);
                }
                else
                    Particles[i].Alpha = finalAlpha;

                //Set particles speed based on distance travelled.
                Particles[i].Speed = Vector2.Distance(Particles[i].Position, Particles[i].PreviousPosition) * CameraZoom;


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
                if (Size > Particles[i].LargestScale)
                {
                    float diff = Particles[i].LargestScale / Particles[i].SmallestScale;

                    Particles[i].Scale /= diff;

                    Particles[i].Position = Normal * (Distance / diff);
                }
                else if (Size < Particles[i].SmallestScale)
                {
                    float diff = Particles[i].LargestScale / Particles[i].SmallestScale;

                    Particles[i].Scale *= diff;

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
            
            switch (Particles[i].MoveType)
            {
                case 0:
                    //Particles[i].Position += new Vector2(0.01f, 0.01f) * Size;
                    break;

                //Snow
                case 1:
                    Particles[i].Acceleration.X = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
                    Particles[i].Acceleration.Y = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
                    //Particles[i].Acceleration.Y += Cam.Gravity;

                    max = 250f * Intensity;

                    if (Particles[i].Velocity.X > max)
                        Particles[i].Velocity.X = max;
                    if (Particles[i].Velocity.X < -max)
                        Particles[i].Velocity.X = -max;

                    if (Particles[i].Velocity.Y > max)
                        Particles[i].Velocity.Y = max;
                    if (Particles[i].Velocity.Y < -max)
                        Particles[i].Velocity.Y = -max;

                    Particles[i].EnvironmentVelocity = Cam.Wind * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 1.5f, 0.5f, false);
                    Particles[i].EnvironmentVelocity.Y += Cam.Gravity * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 1.5f, 0.5f, false);
                    break;

                case 2:
                    Particles[i].Acceleration.X = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
                    
                    max = 25f * Intensity;

                    if (Particles[i].Velocity.X > max)
                        Particles[i].Velocity.X = max;
                    if (Particles[i].Velocity.X < -max)
                        Particles[i].Velocity.X = -max;


                    Particles[i].EnvironmentVelocity = Cam.Wind * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 1.5f, 0.5f, false);
                    Particles[i].EnvironmentVelocity.Y += Cam.Gravity * Size * UsefulMethods.FindBetween(Intensity, 2f, 0.1f, 3f, 2f, false);
                    break;

                case 3:
                    
                    Particles[i].Acceleration.X = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;
                    Particles[i].Acceleration.Y = 1000 * UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1f, -1f, false) * Intensity;

                    max = 250f * Intensity;

                    if (Particles[i].Velocity.X > max)
                        Particles[i].Velocity.X = max;
                    if (Particles[i].Velocity.X < -max)
                        Particles[i].Velocity.X = -max;

                    if (Particles[i].Velocity.Y > max)
                        Particles[i].Velocity.Y = max;
                    if (Particles[i].Velocity.Y < -max)
                        Particles[i].Velocity.Y = -max;
                    break;

                case 4:
                    Particles[i].Position += new Vector2(0.3f, 0.3f) * Size;
                    //Particles[i].Position += new Vector2((float)new Random(i + Particles.Count).NextDouble() * 10000f, (float)new Random(i + Particles.Count + Particles.Count).NextDouble() * 10000f) * Size;


                    float zoomMult = 1f / Particles[i].SmallestScale;
                    Size = Particles[i].Scale * CameraZoom;
                    float SizeMult = Size * zoomMult;

                    Vector2 NormalTest = Particles[i].Position;
                    float DistanceTest = NormalTest.Length();
                    NormalTest.Normalize();

                    float diffTest = ((float)new Random(i).NextDouble() * 0.01f) + 0.995f;
                    diffTest = 0.9995f;

                    Particles[i].Scale /= diffTest;
                    Size = Particles[i].Scale * CameraZoom;
                    SizeMult = Size * zoomMult;

                    Particles[i].Position = NormalTest * (DistanceTest / diffTest);

                    Size = Particles[i].Scale * CameraZoom;
                    SizeMult = Size * zoomMult;
                    break;
            }

            Particles[i].Velocity += Particles[i].Acceleration * Size * GlobalVariables.WorldTime;
            Particles[i].Position += Particles[i].Velocity * GlobalVariables.WorldTime;
            Particles[i].Position += Particles[i].EnvironmentVelocity * GlobalVariables.WorldTime;

            Particles[i].Acceleration = Vector2.Zero;
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam, bool front)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                float size = Particles[i].Scale * CameraZoom;

                if ((front && size > 1f) || (!front && size <= 1f))
                {
                    float angle = Particles[i].Angle;

                    Vector2 stretch = Vector2.One;

                    if (Particles[i].MoveType == 2)
                    {
                        float speed = (Particles[i].Velocity.Length() + Particles[i].EnvironmentVelocity.Length()) * CameraZoom;
                        stretch.Y = UsefulMethods.FindBetween(speed, 1000f, 250, 3f, 1f, false);
                    }

                    float scale = 0f;
                    if (size < 1f)
                        scale = Particles[i].Scale;

                    spriteBatch.Draw(Particles[i].Tex, Particles[i].Position + cam.Position,
                        null, Particles[i].Tint * Particles[i].Alpha * DrawAlpha,
                        angle, new Vector2(Particles[i].Tex.Width / 2, Particles[i].Tex.Height / 2), stretch * Particles[i].Scale * Particles[i].DrawScale,
                        SpriteEffects.None, scale);

                    if (Particles[i].Glow)
                        spriteBatch.Draw(Particles[i].Tex, Particles[i].Position + cam.Position,
                            null, Color.White * Particles[i].Alpha * DrawAlpha,
                            angle, new Vector2(Particles[i].Tex.Width / 2, Particles[i].Tex.Height / 2), stretch * Particles[i].Scale * (Particles[i].DrawScale * 0.5f),
                            SpriteEffects.None, scale);
                }
            }
        }

        public void DrawFront(SpriteBatch spriteBatch)
        {
        }
    }
}
