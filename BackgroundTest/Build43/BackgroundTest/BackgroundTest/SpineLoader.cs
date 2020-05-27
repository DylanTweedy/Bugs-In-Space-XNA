using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;


namespace BackgroundTest
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
            Atlases.Add("skeleton", new Atlas("data/" + "skeleton" + ".atlas", new XnaTextureLoader(graphics)));
            Atlases.Add("skeleton2", new Atlas("data/" + "skeleton2" + ".atlas", new XnaTextureLoader(graphics)));        
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

        static public void RenderStart(int cameraNumber)
        {
            skeletonRenderer.Begin(CameraManager.CamerasRead[cameraNumber].Transform);
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
