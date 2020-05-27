using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameRebuild
{
    class Parallax
    {
        class Particle
        {
            public Vector2 Pos;
            public Vector2 PPos;
            public float Scale;
            public float Alpha;
            public float Speed;
            public float Angle;
            public bool Remove;
            public int ID;
            public Color Tint;
        }

        List<Particle> ParticlesBack;
        List<Particle> ParticlesFront;
        List<Particle> ParticlesBig;
        Vector2 ImageDimensionsHalf;
        Vector2 ImageDimensions;
        Random rand;

        Vector2 CPos;

        float scaleLow;
        float scaleHigh;
        float scaleFront;
        float scaleMultiplier;

        Vector2 windowDimensions;
        Vector2 D;
        Vector2 P;
        float R;
        float Z;

        float maxSpeed;
        float stretchmin;
        float stretchmax;

        int MaxParticles;
        int BigParticles;
        float particleMultiplier;
        float ratioBack;
        float ratioFront;
        float BigParticleMultiplier;
        
        float RangeX;
        float RangeY;
        float HalfX;
        float HalfY;

        int backRemove;
        int bigRemove;
        int frontRemove;

        float Radius;
        float Diameter;

        public byte MoveType;
        public byte ParticleType;

        List<Color> Palette;

        public Parallax(Vector2 centerPoint)
        {
            rand = new Random();
            ParticlesBack = new List<Particle>();
            ParticlesFront = new List<Particle>();
            ParticlesBig = new List<Particle>();
            Palette = new List<Color>();

            ImageDimensions = new Vector2(32, 32);
            ImageDimensionsHalf = ImageDimensions / 2;

            scaleLow = 0.1f;
            scaleHigh = 0.75f;
            scaleFront = 2.5f;
            scaleMultiplier = scaleHigh / scaleLow;

            particleMultiplier = 20f;
            ratioBack = 0.5f;
            BigParticleMultiplier = 0.1f;

            MoveType = 3;
            ParticleType = 0;

            GetPalette();
        }

        double test;


        public void Update(Vector2 WindowDimensions, Vector2 d, Vector2 p, float r, float z)
        {
            windowDimensions = WindowDimensions;
            D = d;
            P = p;
            R = r;
            Z = z;

            windowDimensions *= 1f;

            RangeX = (int)((windowDimensions.X / Z) + (ImageDimensions.X));
            RangeY = (int)((windowDimensions.Y / Z) + (ImageDimensions.Y));
            HalfX = RangeX / 2f;
            HalfY = RangeY / 2f;
            Diameter = (float)Math.Sqrt((RangeX * RangeX) + (RangeY * RangeY));
            Radius = Diameter / 2f;
                        
            test += WorldVariables.FrameTime;
            if (test > 10)
            {
                ratioBack = (float)rand.NextDouble();
                particleMultiplier = (float)rand.NextDouble() * 10f;
                BigParticleMultiplier = (float)rand.NextDouble() / 10f;
                GetPalette();
                test = 0;
            }
            //ratioBack = 0.5f;
            //particleMultiplier = 10f;
            //BigParticleMultiplier = 0.1f;

            ratioFront = 1 - ratioBack;

            //Add particles 
            MaxParticles = (int)(Math.Sqrt(windowDimensions.X * windowDimensions.Y) * particleMultiplier);
            BigParticles = (int)(Math.Sqrt(windowDimensions.X * windowDimensions.Y) * BigParticleMultiplier);
            if (ParticlesFront.Count != Math.Ceiling(MaxParticles * ratioFront) ||
                ParticlesBack.Count != Math.Floor(MaxParticles * ratioBack) ||
                MaxParticles == 0 || ParticlesBig.Count != BigParticles)
                SetupParticles();
            
            UpdateParticles(scaleLow, scaleHigh, 0);
            UpdateParticles(scaleHigh, scaleFront, 1);
            UpdateParticles(scaleLow, scaleHigh / 2, 2);
        }
        
        private void GetPalette()
        {
            switch (ParticleType)
            {
                case 0:
                    Palette.Clear();
                    //Palette = ColorManager.Triad(ColorManager.RandomFullColor(), false, 150);
                    //Palette = ColorManager.Compliment(ColorManager.RandomFullColor());
                    //Palette = ColorManager.Compliment(Color.Green);
                    //Palette = ColorManager.GetShades(Palette);

                    Palette = ColorManager.Spectrum(ColorManager.RandomFullColor());

                    break;
            }

        }

        private Color GetColour()
        {
            Color C = Color.White;

            switch (ParticleType)
            {
                default:
                    C = Palette[rand.Next(0, Palette.Count)];
                    break;
            }

            return C;
        }

        private void SetupParticles()
        {
            float mult = 0.25f;
            int particles;
            int particles2;
            float Size;
            float SizeMult;
            float zoomMult;
            int low;
            int high;
            int i;

            if (ParticlesBig.Count < BigParticles)
            {
                particles = (int)(BigParticles - ParticlesBig.Count);
                particles2 = (int)(BigParticles * mult * WorldVariables.FrameTime);
                if (particles2 < particles && particles2 != 0)
                    particles = particles2;
                else if (particles != 0)
                    particles = 1;

                if (particles != 0)
                {
                    zoomMult = 1f / scaleLow;
                    low = (int)(scaleLow * 100000);
                    high = (int)(scaleHigh * 100000);

                    for (int o = 0; o < particles; o++)
                        if (ParticlesBig.Count < BigParticles)
                        {
                            i = ParticlesBig.Count;

                            ParticlesBig.Add(new Particle());
                            ParticlesBig[i].Scale = (rand.Next(low, high) / 100000f) * Z;

                            //////////////////////////
                            ParticlesBig[i].ID = rand.Next(0, SM.StarsBig.Elements);
                            //////////////////////////

                            Size = ParticlesBig[i].Scale * Z;
                            SizeMult = Size * zoomMult;
                            
                            ParticlesBig[i].Pos = new Vector2(rand.Next(0, (int)RangeX) - HalfX, rand.Next(0, (int)RangeY) - HalfY) * SizeMult;

                            ParticlesBig[i].Angle = (float)(rand.NextDouble() * MathHelper.TwoPi);
                            ParticlesBig[i].Alpha = 0f;
                            ParticlesBig[i].Tint = GetColour();
                        }
                }
            }
            else if (ParticlesBig.Count > BigParticles)
            {
                particles = (int)(ParticlesBig.Count - BigParticles);
                particles2 = (int)(particles * WorldVariables.FrameTime * 0.5f);
                if (particles2 < particles && particles2 >= 1)
                    particles = particles2;

                for (int o = 0; o < particles; o++)
                    if (ParticlesBig.Count - bigRemove > BigParticles)
                    {
                        ParticlesBig[ParticlesBig.Count - 1 - bigRemove].Remove = true;
                        bigRemove++;
                    }
            }

            if (ParticlesBack.Count < Math.Floor(MaxParticles * ratioBack))
            {
                particles = (int)Math.Floor((MaxParticles * ratioBack) - ParticlesBack.Count);
                particles2 = (int)((MaxParticles * ratioBack) * mult * WorldVariables.FrameTime);
                if (particles2 < particles && particles2 != 0)
                    particles = particles2;
                else if (particles != 0)
                    particles = 1;

                if (particles != 0)
                {
                    zoomMult = 1f / scaleLow;
                    low = (int)(scaleLow * 100000);
                    high = (int)(scaleHigh * 100000);

                    for (int o = 0; o < particles; o++)
                        if (ParticlesBack.Count < (int)Math.Floor(MaxParticles * ratioBack))
                        {
                            i = ParticlesBack.Count;

                            ParticlesBack.Add(new Particle());
                            ParticlesBack[i].Scale = (rand.Next(low, high) / 100000f) * Z;
                            
                            //////////////////////////
                            ParticlesBack[i].ID = rand.Next(0, SM.StarsBack.Elements);
                            //////////////////////////

                            Size = ParticlesBack[i].Scale * Z;
                            SizeMult = Size * zoomMult;

                            //float x = (float)rand.NextDouble() * Diameter * SizeMult;
                            //float y = 0;
                            //float r = (float)(MathHelper.TwoPi * rand.NextDouble());
                            //float cos = (float)Math.Cos(r);
                            //float sin = (float)Math.Sin(r);

                            //ParticlesBack[i].Pos.X = (x * cos) - (-y * sin);
                            //ParticlesBack[i].Pos.Y = (y * cos) - (x * sin);

                            ParticlesBack[i].Pos = new Vector2(rand.Next(0, (int)RangeX) - HalfX, rand.Next(0, (int)RangeY) - HalfY) * SizeMult;

                            ParticlesBack[i].Angle = (float)(rand.NextDouble() * MathHelper.TwoPi);
                            ParticlesBack[i].Alpha = 0f;
                            ParticlesBack[i].Tint = GetColour();
                        }
                }
            }
            else if (ParticlesBack.Count > Math.Floor(MaxParticles * ratioBack))
            {
                particles = (int)(ParticlesBack.Count - Math.Floor(MaxParticles * ratioBack));
                particles2 = (int)(particles * WorldVariables.FrameTime * 0.5f);
                if (particles2 < particles && particles2 >= 1)
                    particles = particles2;
                
                for (int o = 0; o < particles; o++)
                    if (ParticlesBack.Count - backRemove > (int)Math.Floor(MaxParticles * ratioBack))
                    {
                        ParticlesBack[ParticlesBack.Count - 1 - backRemove].Remove = true;
                        backRemove++;
                    }
            }


            if (ParticlesFront.Count < Math.Ceiling(MaxParticles * ratioFront))
            {
                particles = (int)Math.Ceiling((MaxParticles * ratioFront) - ParticlesFront.Count);
                particles2 = (int)((MaxParticles * ratioFront) * mult * WorldVariables.FrameTime);
                if (particles2 < particles && particles2 != 0)
                    particles = particles2;
                else if (particles != 0)
                    particles = 1;

                if (particles != 0)
                {
                    zoomMult = 1f / scaleLow;
                    low = (int)(scaleHigh * 100000);
                    high = (int)(scaleFront * 100000);

                    for (int o = 0; o < particles; o++)
                        if (ParticlesFront.Count < (int)Math.Ceiling(MaxParticles * ratioFront))
                        {
                            i = ParticlesFront.Count;

                            ParticlesFront.Add(new Particle());
                            ParticlesFront[i].Scale = (rand.Next(low, high) / 100000f) * Z;

                            //////////////////////////
                            ParticlesFront[i].ID = rand.Next(0, SM.StarsFront.Elements);
                            //////////////////////////

                            Size = ParticlesFront[i].Scale * Z;
                            SizeMult = Size * zoomMult;
                            ParticlesFront[i].Pos = new Vector2(rand.Next(0, (int)RangeX) - HalfX, rand.Next(0, (int)RangeY) - HalfY) * SizeMult;

                            ParticlesFront[i].Angle = (float)(rand.NextDouble() * MathHelper.TwoPi);
                            ParticlesFront[i].Alpha = 0f;
                            ParticlesFront[i].Tint = GetColour();
                        }
                }
            }
            else if (ParticlesFront.Count > Math.Ceiling(MaxParticles * ratioFront))
            {
                particles = (int)(ParticlesFront.Count - Math.Ceiling(MaxParticles * ratioFront));
                particles2 = (int)(particles * WorldVariables.FrameTime * 0.5f);
                if (particles2 < particles && particles2 >= 1)
                    particles = particles2;

                for (int o = 0; o < particles; o++)
                    if (ParticlesFront.Count - frontRemove > (int)Math.Ceiling(MaxParticles * ratioFront))
                    {
                        ParticlesFront[ParticlesFront.Count - 1 - frontRemove].Remove = true;
                        frontRemove++;
                    }
            }

        }

        private void UpdateParticles(float low, float high, byte type)
        {
            scaleMultiplier = high / low;
            float zoomMult = 1f / scaleLow;        
            CPos = P;

            List<Particle> group = new List<Particle>();

            switch (type)
            {
                case 0:
                    group = ParticlesBack;
                    break;

                case 1:
                    group = ParticlesFront;
                    break;

                case 2:
                    group = ParticlesBig;
                    break;
            }
            
            for (int i = 0; i < group.Count; i++)
            {
                float Size = group[i].Scale * Z;
                float SizeMult = Size * zoomMult;
                
                group[i].PPos = group[i].Pos;

                switch (MoveType)
                {
                    case 0:
                        break;
                        
                    case 1:
                        group[i].Pos += new Vector2(0f, 15f) * UsefulMethods.FindBetween(Size, scaleFront, scaleLow, 1, 0.1f, false);
                        break;

                    case 2:
                        group[i].Pos *= UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 1.01f, 0.99f, false);
                        break;

                    case 3:
                        group[i].Pos += new Vector2(10.25f, 10.25f) * Size;
                        break;
                }
                
                group[i].Pos += D * Size;
                
                float tint = Math.Abs(UsefulMethods.FindBetween(Size, high, low, 1f, -1f, false));
                float fTint = UsefulMethods.FindBetween(tint, 1f, 0.75f, 1f, 0f, true);

                if (group[i].Remove)
                {
                    if (group[i].Alpha < 0.01f)
                        group[i].Alpha = 0f;
                    else
                        group[i].Alpha -= group[i].Alpha * (float)WorldVariables.FrameTime;

                    if (group[i].Alpha == 0f)
                    {
                        switch (type)
                        {
                            case 0:
                                backRemove--;
                                break;

                            case 1:
                                frontRemove--;
                                break;

                            case 2:
                                bigRemove--;
                                break;
                        }

                        group.RemoveAt(i);
                        continue;
                    }
                }
                else if (fTint > group[i].Alpha)
                {
                    float diff = fTint - group[i].Alpha;

                    if (diff < 0.01f)
                        group[i].Alpha = fTint;
                    else
                        group[i].Alpha += diff * ((float)WorldVariables.FrameTime * 5f);
                }
                else
                    group[i].Alpha = fTint;


                group[i].Speed = Vector2.Distance(group[i].Pos, group[i].PPos) * Z;

                if (Size > high)
                {
                    group[i].Scale /= scaleMultiplier;
                    Size = group[i].Scale * Z;
                    SizeMult = Size * zoomMult;
                    group[i].Pos = new Vector2(rand.Next(0, (int)RangeX) - HalfX, rand.Next(0, (int)RangeY) - HalfY) * SizeMult;
                }
                else if (Size < low)
                {
                    group[i].Scale *= scaleMultiplier;
                    Size = group[i].Scale * Z;
                    SizeMult = Size * zoomMult;
                    group[i].Pos = new Vector2(rand.Next(0, (int)RangeX) - HalfX, rand.Next(0, (int)RangeY) - HalfY) * SizeMult;
                }
                
                if (Vector2.Distance(group[i].Pos, Vector2.Zero) > Radius * SizeMult)
                {
                    group[i].Pos.X *= -0.99f;
                    group[i].Pos.Y *= -0.99f;
                    group[i].Tint = GetColour();
                }
            }

            switch (type)
            {
                case 0:
                    ParticlesBack = group;
                    break;

                case 1:
                    ParticlesFront = group;
                    break;

                case 2:
                    ParticlesBig = group;
                    break;
            }
        }
        
        public void DrawBack(SpriteBatch spriteBatch)
        {
            maxSpeed = 45f;
            stretchmax = 30f;
            stretchmin = 15f;

            for (int i = 0; i < ParticlesBack.Count; i++)
            {
                if (ParticlesBack[i].Speed < maxSpeed)
                {
                    Vector2 s = Vector2.Zero;
                    float stretch = UsefulMethods.FindBetween(ParticlesBack[i].Speed, stretchmax, stretchmin, 10f, 1f, false);
                    float tint = 1f;
                    float angle = ParticlesBack[i].Angle;

                    if (ParticlesBack[i].Speed > 15f)
                        angle = -(float)(Math.PI + Math.Atan2(ParticlesBack[i].PPos.X - ParticlesBack[i].Pos.X, ParticlesBack[i].PPos.Y - ParticlesBack[i].Pos.Y));

                    if (ParticlesBack[i].Speed > stretchmax)
                        tint = UsefulMethods.FindBetween(ParticlesBack[i].Speed, maxSpeed, stretchmax, 1f, 0f, true);

                    s.Y = stretch;
                    s.X = 1;

                    if (s.Y < 1)
                        s.Y = 1;
                    if (s.Y > 10)
                        s.Y = 10;

                    Texture2D tex = SM.StarsBack.S(ParticlesBack[i].ID);
                    Vector2 pos = ParticlesBack[i].Pos + CPos;
                    Rectangle rec = SM.StarsBack.Rectangles[ParticlesBack[i].ID];
                    Vector2 origin = new Vector2(SM.StarsBack.Size / 2, SM.StarsBack.Size / 2);
                    Vector2 scale = ParticlesBack[i].Scale * s;
                    tint = ParticlesBack[i].Alpha * tint;

                    spriteBatch.Draw(tex, pos, rec, ParticlesBack[i].Tint * tint, angle, origin, scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(tex, pos, rec, Color.White * tint, angle, origin, scale * 0.6f, SpriteEffects.None, 0f);
                
                }
            }


            for (int i = 0; i < ParticlesBig.Count; i++)
            {
                if (ParticlesBig[i].Speed < maxSpeed)
                {
                    float tint = 0.25f;
                    float angle = ParticlesBig[i].Angle;

                    if (ParticlesBig[i].Speed > stretchmax)
                        tint = UsefulMethods.FindBetween(ParticlesBig[i].Speed, maxSpeed, stretchmax, 0.25f, 0f, true);
                    
                    spriteBatch.Draw(SM.StarsBig.S(ParticlesBig[i].ID), ParticlesBig[i].Pos + CPos,
                        SM.StarsBig.Rectangles[ParticlesBig[i].ID], ParticlesBig[i].Tint * ParticlesBig[i].Alpha * tint,
                        angle, new Vector2(SM.StarsBig.Size / 2, SM.StarsBig.Size / 2), ParticlesBig[i].Scale * 7.5f,
                        SpriteEffects.None, 0f);

                }
            }
        }

        public void DrawFront(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ParticlesFront.Count; i++)
            {
                if (ParticlesFront[i].Speed < maxSpeed)
                {
                    //if (Particles[i].Scale <= 0.75f)
                    Vector2 s = Vector2.Zero;
                    //s.X = Math.Abs(Particles[i].Pos.X - Particles[i].PPos.X);
                    //s.Y = Math.Abs(Particles[i].Pos.Y - Particles[i].PPos.Y);

                    float stretch = UsefulMethods.FindBetween(ParticlesFront[i].Speed, stretchmax, stretchmin, 10f, 1f, false);
                    float tint = 1f;
                    float angle = ParticlesFront[i].Angle;

                    if (ParticlesFront[i].Speed > 15f)
                        angle = -(float)(Math.PI + Math.Atan2(ParticlesFront[i].PPos.X - ParticlesFront[i].Pos.X, ParticlesFront[i].PPos.Y - ParticlesFront[i].Pos.Y));

                    if (ParticlesFront[i].Speed > stretchmax)
                        tint = UsefulMethods.FindBetween(ParticlesFront[i].Speed, maxSpeed, stretchmax, 1f, 0f, true);

                    s.Y = stretch;
                    s.X = 1;

                    if (s.Y < 1)
                        s.Y = 1;
                    if (s.Y > 10)
                        s.Y = 10;

                    Texture2D tex = SM.StarsFront.S(ParticlesFront[i].ID);
                    Vector2 pos = ParticlesFront[i].Pos + CPos;
                    Rectangle rec = SM.StarsFront.Rectangles[ParticlesFront[i].ID];
                    Vector2 origin = new Vector2(SM.StarsFront.Size / 2, SM.StarsFront.Size / 2);
                    Vector2 scale = ParticlesFront[i].Scale * s;
                    tint = ParticlesFront[i].Alpha * tint;

                    spriteBatch.Draw(tex, pos, rec, ParticlesFront[i].Tint * tint, angle, origin, scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(tex, pos, rec, Color.White * tint, angle, origin, scale * 0.6f, SpriteEffects.None, 0f);
                }
            }

            int w = StaticTests.Marker.Width;

            for (int i = 0; i < Palette.Count; i++)
            {
                    spriteBatch.Draw(StaticTests.Marker, new Vector2(w * i - ((w / 2) * Palette.Count), 0), Palette[i]);
            }
        }
    }
}
