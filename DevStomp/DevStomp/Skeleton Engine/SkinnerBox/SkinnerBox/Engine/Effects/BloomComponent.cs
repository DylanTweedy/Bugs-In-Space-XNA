#region File Description
//-----------------------------------------------------------------------------
// BloomComponent.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SkeletonEngine
{
    public class BloomComponent
    {
        #region Fields
        
        Effect bloomExtractEffect;
        Effect bloomCombineEffect;
        Effect gaussianBlurEffect;

        RenderTarget2D sceneRenderTarget;
        RenderTarget2D renderTarget1;
        RenderTarget2D renderTarget2;


        // Choose what display settings the bloom should use.
        public BloomSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        BloomSettings settings = BloomSettings.PresetSettings[0];


        // Optionally displays one of the intermediate buffers used
        // by the bloom postprocess, so you can see exactly what is
        // being drawn into each rendertarget.
        public enum IntermediateBuffer
        {
            PreBloom,
            BlurredHorizontally,
            BlurredBothWays,
            FinalResult,
        }

        public IntermediateBuffer ShowBuffer
        {
            get { return showBuffer; }
            set { showBuffer = value; }
        }

        IntermediateBuffer showBuffer = IntermediateBuffer.FinalResult;


        #endregion

        #region Initialization


        public BloomComponent()
        {
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager Content)
        {
            bloomExtractEffect = Content.Load<Effect>("BloomExtract");
            bloomCombineEffect = Content.Load<Effect>("BloomCombine");
            gaussianBlurEffect = Content.Load<Effect>("GaussianBlur");

            // Look up the resolution and format of our main backbuffer.

            PresentationParameters pp = graphicsDevice.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;

            //CreateRenderTargets(graphicsDevice, width, height);
        }

        //private void CreateRenderTargets(GraphicsDevice graphicsDevice, int width, int height)
        //{
        //    PresentationParameters pp = graphicsDevice.PresentationParameters;
        //    SurfaceFormat format = pp.BackBufferFormat;

        //    sceneRenderTarget = new RenderTarget2D(graphicsDevice, width, height, false,
        //                                           format, pp.DepthStencilFormat, pp.MultiSampleCount,
        //                                           RenderTargetUsage.PreserveContents);

        //    width /= 2;
        //    height /= 2;

        //    renderTarget1 = new RenderTarget2D(graphicsDevice, width, height, false, format, DepthFormat.None);
        //    renderTarget2 = new RenderTarget2D(graphicsDevice, width, height, false, format, DepthFormat.None);
        //}


        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        public void UnloadContent()
        {
            sceneRenderTarget.Dispose();
            renderTarget1.Dispose();
            renderTarget2.Dispose();
        }


        #endregion

        #region Draw


        /// <summary>
        /// This should be called at the very start of the scene rendering. The bloom
        /// component uses it to redirect drawing into its custom rendertarget, so it
        /// can capture the scene image in preparation for applying the bloom filter.
        /// </summary>
        public void BeginDraw(GraphicsDevice graphics, RenderTarget2D bloomSceneTarget)
        {
            //if (Visible)
            {
                sceneRenderTarget = bloomSceneTarget;
                graphics.SetRenderTarget(sceneRenderTarget);
                graphics.Clear(Color.Transparent);
            }
        }


        /// <summary>
        /// This is where it all happens. Grabs a scene that has already been rendered,
        /// and uses postprocess magic to add a glowing bloom effect over the top of it.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, RenderTarget2D renderTarget, RenderTarget2D bloomTarget1, RenderTarget2D bloomTarget2)
        {
            renderTarget1 = bloomTarget1;
            renderTarget2 = bloomTarget2;

            //if (sceneRenderTarget == null || (sceneRenderTarget.Width != renderTarget.Width || sceneRenderTarget.Height != renderTarget.Height))
            //    CreateRenderTargets(graphics, renderTarget.Width, renderTarget.Height);

            //graphics.SamplerStates[1] = SamplerState.LinearClamp;

            // Pass 1: draw the scene into rendertarget 1, using a
            // shader that extracts only the brightest parts of the image.
            bloomExtractEffect.Parameters["BloomThreshold"].SetValue(
                Settings.BloomThreshold);

            DrawFullscreenQuad(spriteBatch, graphics, sceneRenderTarget, renderTarget1,
                               bloomExtractEffect,
                               IntermediateBuffer.PreBloom);

            // Pass 2: draw from rendertarget 1 into rendertarget 2,
            // using a shader to apply a horizontal gaussian blur filter.
            SetBlurEffectParameters(1.0f / (float)renderTarget1.Width, 0);

            DrawFullscreenQuad(spriteBatch, graphics, renderTarget1, renderTarget2,
                               gaussianBlurEffect,
                               IntermediateBuffer.BlurredHorizontally);

            // Pass 3: draw from rendertarget 2 back into rendertarget 1,
            // using a shader to apply a vertical gaussian blur filter.
            SetBlurEffectParameters(0, 1.0f / (float)renderTarget1.Height);

            DrawFullscreenQuad(spriteBatch, graphics, renderTarget2, renderTarget1,
                               gaussianBlurEffect,
                               IntermediateBuffer.BlurredBothWays);

            // Pass 4: draw both rendertarget 1 and the original scene
            // image back into the main backbuffer, using a shader that
            // combines them to produce the final bloomed result.
            graphics.SetRenderTarget(renderTarget);


            EffectParameterCollection parameters = bloomCombineEffect.Parameters;

            parameters["BloomIntensity"].SetValue(Settings.BloomIntensity);
            parameters["BaseIntensity"].SetValue(Settings.BaseIntensity);
            parameters["BloomSaturation"].SetValue(Settings.BloomSaturation);
            parameters["BaseSaturation"].SetValue(Settings.BaseSaturation + 1f);
            parameters["Base"].SetValue(sceneRenderTarget);

            Viewport viewport = graphics.Viewport;

            //graphics.Textures[1] = sceneRenderTarget;

            DrawFullscreenQuad(spriteBatch, renderTarget1,
                               viewport.Width, viewport.Height,
                               bloomCombineEffect,
                               IntermediateBuffer.FinalResult);

            //graphics.Textures[1] = null;

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null);
            //spriteBatch.Draw(sceneRenderTarget, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White * 1f);
            //spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, bloomCombineEffect);
            //spriteBatch.Draw(renderTarget1, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White * 1f);
            //spriteBatch.End();


        }


        /// <summary>
        /// Helper for drawing a texture into a rendertarget, using
        /// a custom shader to apply postprocessing effects.
        /// </summary>
        void DrawFullscreenQuad(SpriteBatch spriteBatch, GraphicsDevice graphics, Texture2D texture, RenderTarget2D renderTarget,
                                Effect effect, IntermediateBuffer currentBuffer)
        {
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Transparent);

            DrawFullscreenQuad(spriteBatch, texture,
                               renderTarget.Width, renderTarget.Height,
                               effect, currentBuffer);
        }


        /// <summary>
        /// Helper for drawing a texture into the current rendertarget,
        /// using a custom shader to apply postprocessing effects.
        /// </summary>
        void DrawFullscreenQuad(SpriteBatch spriteBatch, Texture2D texture, int width, int height,
                                Effect effect, IntermediateBuffer currentBuffer)
        {
            // If the user has selected one of the show intermediate buffer options,
            // we still draw the quad to make sure the image will end up on the screen,
            // but might need to skip applying the custom pixel shader.
            if (showBuffer < currentBuffer)
            {
                effect = null;
            }
            
            spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, effect);
            spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();
        }


        /// <summary>
        /// Computes sample weightings and texture coordinate offsets
        /// for one pass of a separable gaussian blur filter.
        /// </summary>
        void SetBlurEffectParameters(float dx, float dy)
        {
            // Look up the sample weight and offset effect parameters.
            EffectParameter weightsParameter, offsetsParameter;

            weightsParameter = gaussianBlurEffect.Parameters["SampleWeights"];
            offsetsParameter = gaussianBlurEffect.Parameters["SampleOffsets"];

            // Look up how many samples our gaussian blur effect supports.
            int sampleCount = weightsParameter.Elements.Count;

            // Create temporary arrays for computing our filter settings.
            float[] sampleWeights = new float[sampleCount];
            Vector2[] sampleOffsets = new Vector2[sampleCount];

            // The first sample always has a zero offset.
            sampleWeights[0] = ComputeGaussian(0);
            sampleOffsets[0] = new Vector2(0);

            // Maintain a sum of all the weighting values.
            float totalWeights = sampleWeights[0];

            // Add pairs of additional sample taps, positioned
            // along a line in both directions from the center.
            for (int i = 0; i < sampleCount / 2; i++)
            {
                // Store weights for the positive and negative taps.
                float weight = ComputeGaussian(i + 1);

                sampleWeights[i * 2 + 1] = weight;
                sampleWeights[i * 2 + 2] = weight;

                totalWeights += weight * 2;

                // To get the maximum amount of blurring from a limited number of
                // pixel shader samples, we take advantage of the bilinear filtering
                // hardware inside the texture fetch unit. If we position our texture
                // coordinates exactly halfway between two texels, the filtering unit
                // will average them for us, giving two samples for the price of one.
                // This allows us to step in units of two texels per sample, rather
                // than just one at a time. The 1.5 offset kicks things off by
                // positioning us nicely in between two texels.
                float sampleOffset = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * sampleOffset;

                // Store texture coordinate offsets for the positive and negative taps.
                sampleOffsets[i * 2 + 1] = delta;
                sampleOffsets[i * 2 + 2] = -delta;
            }

            // Normalize the list of sample weightings, so they will always sum to one.
            for (int i = 0; i < sampleWeights.Length; i++)
            {
                sampleWeights[i] /= totalWeights;
            }

            // Tell the effect about our new filter settings.
            weightsParameter.SetValue(sampleWeights);
            offsetsParameter.SetValue(sampleOffsets);
        }


        /// <summary>
        /// Evaluates a single point on the gaussian falloff curve.
        /// Used for setting up the blur filter weightings.
        /// </summary>
        float ComputeGaussian(float n)
        {
            float theta = Settings.BlurAmount;

            return (float)((1.0 / Math.Sqrt(2 * Math.PI * theta)) *
                           Math.Exp(-(n * n) / (2 * theta * theta)));
        }


        #endregion
    }
}
