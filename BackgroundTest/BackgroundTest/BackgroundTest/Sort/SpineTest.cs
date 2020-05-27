using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BackgroundTest
{
    class SpineTest
    {


        Slot headSlot;
        Slot eyeSlot;
        Skeleton skeleton;
        AnimationState state;
        SkeletonBounds bounds = new SkeletonBounds();

        public void Initialize()
        {

        }

        public void LoadContent(GraphicsDevice graphics)
        {


            String name = "spineboy"; // "goblins"; 

            skeleton = SpineLoader.Skeletons[name];
            //skeleton.SetSkin("goblin");
            //skeleton.SetSlotsToSetupPose();

            // Define mixing between animations.
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            if (name == "spineboy")
            {
                stateData.SetMix("walk", "jump", 0.2f);
                stateData.SetMix("jump", "walk", 0.4f);
            }

            state = new AnimationState(stateData);

            //if (true)
            //{
            //    // Event handling for all animations.
            //    state.Start += Start;
            //    state.End += End;
            //    state.Complete += Complete;
            //    state.Event += Event;

            //    state.SetAnimation(0, "drawOrder", true);
            //}
            //else
            {
                state.SetAnimation(0, "walk", false);
                //TrackEntry entry = state.AddAnimation(0, "jump", false, 0);
                //entry.End += new EventHandler<StartEndArgs>(End); // Event handling for queued animations.
                state.AddAnimation(0, "walk", true, 0);
            }
                        
            skeleton.UpdateWorldTransform();


            headSlot = skeleton.FindSlot("head");
            eyeSlot = skeleton.FindSlot("eyes");
        }

        int i = 0;
        public void Update(GameTime gameTime)
        {
            skeleton.RootBone.ScaleX = 0.3f;
            skeleton.RootBone.ScaleY = 0.3f;


            state.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);

            state.Apply(skeleton);
            skeleton.UpdateWorldTransform();



            //headSlot.R = 0f;
            //headSlot.B = 1f;
            //headSlot.G = 0f;


            //i++;


            //if (i > 60)
            //{
            //    if (eyeSlot.Attachment.Name == "eyes")
            //        skeleton.SetAttachment("eyes", "eyes-closed");
            //    else
            //        skeleton.SetAttachment("eyes", "eyes");

            //    i = 0;
            //}

            ////Console.WriteLine(eyeSlot.Attachment.Name);

            bounds.Update(skeleton, true);
            MouseState mouse = Mouse.GetState();

            if (bounds.AabbContainsPoint(mouse.X, mouse.Y))
            {
                BoundingBoxAttachment hit = bounds.ContainsPoint(mouse.X, mouse.Y);
                if (hit != null)
                {
                    headSlot.G = 100;
                    headSlot.B = 0;
                }
            }
        }

        public Vector2 pos
        {
            get { return new Vector2(skeleton.X, skeleton.Y); }
            set { skeleton.X = value.X; skeleton.Y = value.Y + 32; }
        }

        public float rot
        {
            get { return skeleton.RootBone.Rotation; }
            set { skeleton.RootBone.Rotation = -value * 57.2957795f; }
        }

        public Skeleton skele
        {
            get { return skeleton; }
        }

        public void Draw(int cameraNumber)
        {
            //SpineLoader.RenderDraw(skeleton);

        }

        public void Start(object sender, StartEndArgs e)
        {
            //Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": start");
        }

        public void End(object sender, StartEndArgs e)
        {
            //Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": end");
        }

        public void Complete(object sender, CompleteArgs e)
        {
            //Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": complete " + e.LoopCount);
        }

        public void Event(object sender, EventTriggeredArgs e)
        {
            //Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": event " + e.Event);
        }
    }
}
