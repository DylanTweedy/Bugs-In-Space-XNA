using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class LightManager
    {
        static private RenderTarget2D MainRenderTarget;

        static public RenderTarget2D ColorMap;
        static public RenderTarget2D NormalMap;
        static public RenderTarget2D ShadowMap;
         
        static public List<Light> Lights = new List<Light>();
        static public float AmbientBrightness = 0.1f;
        static public Color AmbientColor = Color.White;
        static public float SpecularStrength = .5f;
                  
        static public VertexPositionColorTexture[] Vertices;
        static public VertexBuffer VertexBuffer;


        static public void Initialize(GraphicsDevice graphicsDevice)
        {
            Vertices = new VertexPositionColorTexture[4];
            Vertices[0] = new VertexPositionColorTexture(new Vector3(-1, 1, 0), Color.White, new Vector2(0, 0));
            Vertices[1] = new VertexPositionColorTexture(new Vector3(1, 1, 0), Color.White, new Vector2(1, 0));
            Vertices[2] = new VertexPositionColorTexture(new Vector3(-1, -1, 0), Color.White, new Vector2(0, 1));
            Vertices[3] = new VertexPositionColorTexture(new Vector3(1, -1, 0), Color.White, new Vector2(1, 1));
            VertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), Vertices.Length, BufferUsage.None);
            VertexBuffer.SetData(Vertices);

            UpdateRenderTargets(graphicsDevice, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);            
        }

        static public void UpdateRenderTargets(GraphicsDevice graphicsDevice, int Width, int Height)
        {
            if (ColorMap == null || ColorMap.Width != Width || ColorMap.Height != Height)
            {
                ColorMap = new RenderTarget2D(graphicsDevice, Width, Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                NormalMap = new RenderTarget2D(graphicsDevice, Width, Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
                ShadowMap = new RenderTarget2D(graphicsDevice, Width, Height, false, graphicsDevice.PresentationParameters.BackBufferFormat,
                    graphicsDevice.PresentationParameters.DepthStencilFormat, graphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
            }

        }

        static public void StartColorMap(GraphicsDevice graphicsDevice, RenderTarget2D renderTarget)
        {
            MainRenderTarget = renderTarget;

            if (MainRenderTarget != null)
                UpdateRenderTargets(graphicsDevice, MainRenderTarget.Width, MainRenderTarget.Height);
            else
                UpdateRenderTargets(graphicsDevice, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);        

            graphicsDevice.SetRenderTarget(ColorMap);
            graphicsDevice.Clear(Color.Transparent);
        }

        static public void StartNormalMap(GraphicsDevice graphicsDevice)
        {
            DebugOptions.DrawNormals = true;
            graphicsDevice.SetRenderTarget(NormalMap);
            graphicsDevice.Clear(Color.Transparent);
        }

        static public void DrawFinal(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, List<Light> lights, Camera cam)
        {
            DebugOptions.DrawNormals = false;

            Lights = lights;



            if (Lights != null)
            {
                if (Lights.Count > 250)
                    Lights.RemoveRange(100, Lights.Count - 250);

                for (int i = 0; i < Lights.Count; i++)
                {
                    Lights[i].Position = Vector3.Transform(Lights[i].Position, cam.Transform);
                    Lights[i].LightDecay = (int)(Lights[i].LightDecay * cam.Zoom);
                }
            }

            GenerateShadowMap(graphicsDevice);
            graphicsDevice.SetRenderTarget(MainRenderTarget);
            graphicsDevice.Clear(Color.Transparent);
            DrawCombinedMaps(spriteBatch);
        }

        /// <summary>
        /// Generates the light map from the ColorMap and NormalMap textures combined with all the active lights.
        /// </summary>
        /// <returns></returns>
        static private Texture2D GenerateShadowMap(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(ShadowMap);
            graphicsDevice.Clear(Color.Transparent);

            foreach (var light in Lights)
            {
                if (!light.IsEnabled) continue;

                graphicsDevice.SetVertexBuffer(VertexBuffer);

                // Draw all the light sources
                EffectsManager.LightEffect.Parameters["lightStrength"].SetValue(light.ActualPower);
                EffectsManager.LightEffect.Parameters["lightPosition"].SetValue(light.Position);
                EffectsManager.LightEffect.Parameters["lightColor"].SetValue(light.Color);
                EffectsManager.LightEffect.Parameters["lightDecay"].SetValue(light.LightDecay); // Value between 0.00 and 2.00
                EffectsManager.LightEffect.Parameters["specularStrength"].SetValue(SpecularStrength);

                if (light.LightType == LightType.Point)
                {
                    EffectsManager.LightEffect.CurrentTechnique = EffectsManager.LightEffect.Techniques["DeferredPointLight"];
                }
                else
                {
                    EffectsManager.LightEffect.CurrentTechnique = EffectsManager.LightEffect.Techniques["DeferredSpotLight"];
                    EffectsManager.LightEffect.Parameters["coneAngle"].SetValue(((SpotLight)light).SpotAngle);
                    EffectsManager.LightEffect.Parameters["coneDecay"].SetValue(((SpotLight)light).SpotDecayExponent);
                    EffectsManager.LightEffect.Parameters["coneDirection"].SetValue(((SpotLight)light).Direction);
                }

                if (MainRenderTarget == null)
                {
                    EffectsManager.LightEffect.Parameters["screenWidth"].SetValue(graphicsDevice.Viewport.Width);
                    EffectsManager.LightEffect.Parameters["screenHeight"].SetValue(graphicsDevice.Viewport.Height);
                }
                else
                {
                    EffectsManager.LightEffect.Parameters["screenWidth"].SetValue(MainRenderTarget.Width);
                    EffectsManager.LightEffect.Parameters["screenHeight"].SetValue(MainRenderTarget.Height);
                }

                EffectsManager.LightEffect.Parameters["ambientColor"].SetValue(AmbientColor.ToVector4());
                EffectsManager.LightEffect.Parameters["NormalMap"].SetValue(NormalMap);
                EffectsManager.LightEffect.Parameters["ColorMap"].SetValue(ColorMap);
                EffectsManager.LightEffect.CurrentTechnique.Passes[0].Apply();

                // Add Blending (Black background)
                graphicsDevice.BlendState = BlendBlack;

                // Draw some magic
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertices, 0, 2);
            }


            return ShadowMap;
        }

        /// <summary>
        /// Draws the lightmap combined with the colormap ontot he screen. This is the final step.
        /// </summary>
        static private void DrawCombinedMaps(SpriteBatch spriteBatch)
        {
            EffectsManager.LightCombinedEffect.CurrentTechnique = EffectsManager.LightCombinedEffect.Techniques["DeferredCombined2"];
            EffectsManager.LightCombinedEffect.Parameters["ambient"].SetValue(AmbientBrightness);
            EffectsManager.LightCombinedEffect.Parameters["lightAmbient"].SetValue(4);
            EffectsManager.LightCombinedEffect.Parameters["ambientColor"].SetValue(AmbientColor.ToVector4());
            EffectsManager.LightCombinedEffect.Parameters["ColorMap"].SetValue(ColorMap);
            EffectsManager.LightCombinedEffect.Parameters["ShadingMap"].SetValue(ShadowMap);
            EffectsManager.LightCombinedEffect.Parameters["NormalMap"].SetValue(NormalMap);

            EffectsManager.LightCombinedEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, EffectsManager.LightCombinedEffect);
            spriteBatch.Draw(ColorMap, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        static public BlendState BlendBlack = new BlendState()
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,

            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.SourceAlpha,
            AlphaDestinationBlend = Blend.One
        };
    }
}
