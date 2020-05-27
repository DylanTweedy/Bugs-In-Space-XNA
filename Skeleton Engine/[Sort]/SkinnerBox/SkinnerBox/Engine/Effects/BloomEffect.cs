using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SkeletonEngine;

namespace SkeletonEngine
{
    static class BloomEffect
    {
        static BloomComponent bloom;

        static public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            bloom = new BloomComponent();
            bloom.LoadContent(graphicsDevice, content);
        }

        static public void BeginDraw(GraphicsDevice graphicsDevice, RenderTarget2D bloomSceneTarget)
        {
            bloom.Settings = BloomSettings.PresetSettings[5];
            //bloom.Settings.BloomIntensity = 1f;
            bloom.BeginDraw(graphicsDevice, bloomSceneTarget);
        }

        static public void EndDraw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, RenderTarget2D renderTarget, RenderTarget2D bloomTarget1, RenderTarget2D bloomTarget2)
        {
            bloom.Draw(spriteBatch, graphicsDevice, renderTarget, bloomTarget1, bloomTarget2);
        }
    }
}
