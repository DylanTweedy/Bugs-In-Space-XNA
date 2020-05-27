using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public class CameraSettings
    {
        [Serializable()]
        public class SpaceSettings
        {
            public bool Trails = true;
            public int TrailLength = 500;
            public float TrailResolution = 500000f;

            public bool ShowInfo = true;
            public Color TextColor = Color.White;
            public List<Color> BoxColours = ColorManager.Compliment(Color.DarkOrange);
            public bool Type = true;
            public bool Acceleration = true;
            public bool Velocity = true;
            public bool Mass = true;
            public bool ID = true;

            public bool EnlargeObjects;

            public bool DrawVelocity = true;
            public bool DrawAcceleration = true;
        }

        public SpaceSettings SpaceView = new SpaceSettings();

        public void Update()
        {
            if (InputManager.KBButtonPressed(true, new KeyboardInput(Microsoft.Xna.Framework.Input.Keys.I)))
            {
                if (SpaceView.ShowInfo)
                    SpaceView.ShowInfo = false;
                else
                    SpaceView.ShowInfo = true;
            }

            SpaceView.DrawVelocity = false;
            SpaceView.DrawAcceleration = false;
                        
        }
    }
}
