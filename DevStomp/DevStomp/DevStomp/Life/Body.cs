using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;

namespace DevStomp
{
    class Body
    {
        Skeleton skeleton;
        AnimationState state;


        Random rand;
        int IndexNumber;
        public Colors colors;

        public struct Colors
        {
            public Color Skin;
        }

        public void Initialize()
        {
            LoadSkeleton();
            rand = new Random();            
        }

        private void LoadSkeleton()
        {
            skeleton = SpineLoader.LoadSkeleton("BaseSkeleton");
            state = SpineLoader.GetState(skeleton);
                        
            //if (name == "spineboy")
            //{
            //    stateData.SetMix("walk", "jump", 0.2f);
            //    stateData.SetMix("jump", "walk", 0.4f);
            //}
            //stateData.SetMix("Walk1", "Jump1", 0.5f);
            //stateData.SetMix("Jump1", "Falling1", 0.6f);
            //stateData.SetMix("Falling1", "Walk1", 0.7f);
            //stateData.SetMix("Walk1", "Idle1", 1f);
            //stateData.SetMix("Falling1", "Land1", 1f);
            //stateData.SetMix("Land1", "Idle1", 0.3f);
        }

        private void SetupScale()
        {
            //float s = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 0.25f, 0.15f, false);
            //s = 0.2f;
            //skeleton.RootBone.ScaleX = s;
            //skeleton.RootBone.ScaleY = s;
            //skeleton.FindBone("Body").ScaleX = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 0.75f, 2f, false);
            //skeleton.FindBone("Body").ScaleY = UsefulMethods.FindBetween((float)rand.NextDouble(), 1f, 0f, 0.75f, 2f, false);
            //skeleton.FindBone("Body").ScaleX = 1;
            //skeleton.FindBone("Body").ScaleY = 1;
            //skeleton.FindBone("Pelvis").ScaleX = skeleton.FindBone("Body").ScaleX;
            //skeleton.FindBone("Pelvis").ScaleY = skeleton.FindBone("Body").ScaleY;
        }

        private void SetupColors()
        {
            colors = new Colors();
            colors.Skin = Color.PeachPuff;
            UpdateColors();
        }
        
        public void SetSkinColor(Color color)
        {
            colors.Skin = color;
            UpdateColors();
        }

        private void UpdateColors()
        {
            for (int i = 0; i < skeleton.Slots.Count; i++)
            {
                skeleton.Slots[i].R = colors.Skin.R / 255f;
                skeleton.Slots[i].G = colors.Skin.G / 255f;
                skeleton.Slots[i].B = colors.Skin.B / 255f;
            }         
        }

        public void Update(Vector2 Position, float FloorAngle, Vector2 Velocity, string Animation, float AnimationSpeed)
        {
            SetAnimation(Animation, AnimationSpeed);


            if (Velocity.X > 0)
            {
                skeleton.RootBone.X = -Position.X;
                skeleton.RootBone.Rotation = FloorAngle * 57.2957795f;
                skeleton.FlipX = true;
            }
            else if (Velocity.X < 0)
            {
                skeleton.RootBone.X = Position.X;
                skeleton.RootBone.Rotation = -FloorAngle * 57.2957795f;
                skeleton.FlipX = false;
            }
            
            skeleton.RootBone.Y = -Position.Y;
            
            //skeleton.Bones[3].ScaleX = 1f;
            //skeleton.Bones[4].ScaleX = 1f;
            //skeleton.Bones[7].ScaleX = 1f;
            //skeleton.Bones[9].ScaleX = 1f;
            //skeleton.Bones[1].Y -= 50f;


            skeleton.UpdateWorldTransform();

        }

        private void SetAnimation(string Animation, float AnimationSpeed)
        {

            state.TimeScale = 1f;
            if (state.ToString() != Animation + "1")
            {
                switch (Animation)
                {
                    case "Jump":
                        state.SetAnimation(0, Animation + "1", false);
                        break;

                    default:
                        state.SetAnimation(0, Animation + "1", true);
                        break;
                }
            }

            switch (Animation)
            {
                case "Jump":
                    state.TimeScale = AnimationSpeed;
                    state.Update((float)GlobalVariables.FrameTime);

                    if (state.ToString() != Animation + "1")
                    {
                        state.TimeScale = 1f;
                        state.SetAnimation(0, Animation + "1", false);
                        state.Update(0.99f);
                    }
                    break;

                default:
                    state.Update((float)GlobalVariables.FrameTime * AnimationSpeed);
                    break;
            }

            state.Apply(skeleton);
        }

        public void Draw()
        {
            SpineLoader.RenderDraw(skeleton);
        }
    }
}
