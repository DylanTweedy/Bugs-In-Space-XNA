using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using SkeletonEngine;

namespace DevStomp
{
    static class SpineLoader
    {
        static GraphicsDevice graphics;
        static public Dictionary<string, Skeleton> Skeletons;
        static Dictionary<string, Atlas> Atlases;

        static SkeletonRenderer skeletonRenderer;

        static public void LoadContent(GraphicsDevice graphicsDevice)
        {
            graphics = graphicsDevice;

            skeletonRenderer = new SkeletonRenderer(graphicsDevice);
            skeletonRenderer.PremultipliedAlpha = true;

            Atlases = new Dictionary<string, Atlas>();
            //Atlases.Add("skeleton", new Atlas("data/" + "skeleton" + ".atlas", new XnaTextureLoader(graphics)));
            //Atlases.Add("skeleton2", new Atlas("data/" + "skeleton2" + ".atlas", new XnaTextureLoader(graphics)));

            Atlases.Add("BaseSkeleton", new Atlas("data/" + "BaseSkeleton" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Leg", new Atlas("data/" + "Leg" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Arm", new Atlas("data/" + "Arm" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Chest", new Atlas("data/" + "Chest" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Foot", new Atlas("data/" + "Foot" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Hand", new Atlas("data/" + "Hand" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Head", new Atlas("data/" + "Head" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Neck", new Atlas("data/" + "Neck" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("Pelvis", new Atlas("data/" + "Pelvis" + ".atlas", new XnaTextureLoader(graphics)));

            BuildSkeletonDictionary();
        }

        static private void BuildSkeletonDictionary()
        {
            //Skeletons = new Dictionary<string, Skeleton>();
            //SkeletonJson json;
            //string name;

            //name = "skeleton";
            //json = new SkeletonJson(Atlases[name]);
            //Skeletons.Add(name, new Skeleton(json.ReadSkeletonData("data/" + name + ".json")));

            //name = "Leg";
            //json = new SkeletonJson(Atlases[name]);
            //Skeletons.Add(name, new Skeleton(json.ReadSkeletonData("data/" + name + ".json")));
        }

        static public Skeleton LoadSkeleton(string AtlasName)
        {
            //Change This, don't want it loading the skeleton file a bunch of times.
            Skeleton skeleton;

            SkeletonJson json = new SkeletonJson(Atlases[AtlasName]);
            skeleton = new Skeleton(json.ReadSkeletonData("data/" + AtlasName + ".json"));

            skeleton.SetSlotsToSetupPose();

            return skeleton;
        }

        static public AnimationState GetState(Skeleton skeleton)
        {
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);

            return new AnimationState(stateData);
        }

        static public void RenderStart(int cameraNumber)
        {
            skeletonRenderer.Begin(CameraManager.Cams[cameraNumber].Transform);
        }

        static public void RenderStart()
        {
            skeletonRenderer.Begin();
        }

        static public void RenderDraw(Skeleton skeleton)
        {
            skeletonRenderer.Draw(skeleton);
        }

        static public void RenderEnd()
        {
            skeletonRenderer.End();
        }


















    }
}
