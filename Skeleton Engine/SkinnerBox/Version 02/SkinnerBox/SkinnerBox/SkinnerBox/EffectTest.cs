using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace SkinnerBox
{
    static class EffectTest
    {
        static BloomComponent bloom;
        static public Effect test;
        static public Effect normalTest;

        static public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            bloom = new BloomComponent();
            bloom.LoadContent(graphicsDevice, content);
            test = content.Load<Effect>("Effect1");
            normalTest = content.Load<Effect>("normalmap");
        }

        static public void BeginDraw(GraphicsDevice graphicsDevice)
        {
            bloom.Settings = BloomSettings.PresetSettings[3];
            //bloom.Settings.BloomIntensity = 1f;
            bloom.BeginDraw(graphicsDevice);
        }

        static public void EndDraw(GraphicsDevice graphicsDevice, RenderTarget2D renderTarget)
        {
            bloom.Draw(graphicsDevice, renderTarget);
        }
    }
}
