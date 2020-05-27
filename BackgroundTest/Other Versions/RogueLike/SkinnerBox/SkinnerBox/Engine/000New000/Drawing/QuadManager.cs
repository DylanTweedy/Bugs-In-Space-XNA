using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class QuadManager
    {
        static private GraphicsDevice graphicsDevice;
        static private SpriteBatch spriteBatch;
        static private BasicEffect basicEffect;
        static private VertexBuffer vertexBuffer;
        static private IndexBuffer indexBuffer;
        
        static private List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
        static private List<int> indices = new List<int>();
        
        /// <summary>
        /// List of the indices used for a quad.
        /// </summary>
        static private byte[] BaseIndices = new byte[6];

        /// <summary>
        /// A list of all the quads.
        /// </summary>
        static private List<SkeletonQuad> Quads = new List<SkeletonQuad>();

        /// <summary>
        /// Initializes the class.
        /// </summary>
        static public void Initialize()
        {
            graphicsDevice = GlobalVariables.graphicsDevice;
            spriteBatch = GlobalVariables.spriteBatch;
            basicEffect = new BasicEffect(graphicsDevice);

            BaseIndices[0] = 0;
            BaseIndices[1] = 1;
            BaseIndices[2] = 3;
            BaseIndices[3] = 1;
            BaseIndices[4] = 3;
            BaseIndices[5] = 2;
        }

        /// <summary>
        /// Add a quad to the collection.
        /// </summary>
        /// <param name="quad">The quad to add.</param>
        static public void AddQuad(SkeletonQuad quad)
        {
            Quads.Add(quad);
        }

        /// <summary>
        /// Loads the quads infortion into the indices and vertex lists.
        /// </summary>
        static private void LoadQuads()
        {
            for (int i = 0; i < Quads.Count; i++)
                for (int o = 0; o < BaseIndices.Length; o++)
                    indices.Add(BaseIndices[o] + (4 * i));
            
            for (int i = 0; i < Quads.Count; i++)
                vertices.AddRange(Quads[i].MainQuad.vertices);
        }
        
        /// <summary>
        /// Refreshes the vertex and index buffers.
        /// </summary>
        static private void RefreshBuffers()
        {
            if (vertices.Count > 0)
            {
                vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), vertices.Count, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColorTexture>(vertices.ToArray());
                indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Count, BufferUsage.WriteOnly);
                indexBuffer.SetData(indices.ToArray());
            }
            else
            {
                vertexBuffer = null;
                indexBuffer = null;
            }
        }

        /// <summary>
        /// Prepares the device for rendering.
        /// </summary>
        /// <param name="fillMode">Whether to render the quads solid or in wireframe.</param>
        /// <param name="camera">The camera to render them to.</param>
        static public void PreRender(FillMode fillMode, Camera camera)
        {
            RefreshBuffers();

            //Set up matrix.
            basicEffect.World = Matrix.CreateTranslation(0f, 0f, 0f);
            basicEffect.View = camera.Transform;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0f, camera.FinalRender.Width, camera.FinalRender.Height, 0f, 1f, 0f);

            //Enable vertex tinting.
            basicEffect.VertexColorEnabled = true;

            //Set the buffers.
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            //Set the rasterizerState.
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            //Set the fillmode.
            if (fillMode == FillMode.Solid)
            {
                basicEffect.TextureEnabled = true;
                rasterizerState.FillMode = FillMode.Solid;
            }
            else
            {
                basicEffect.TextureEnabled = false;
                rasterizerState.FillMode = FillMode.WireFrame;
            }

            graphicsDevice.RasterizerState = rasterizerState;

        }

        /// <summary>
        /// Draw all quads.
        /// </summary>
        /// <param name="fillMode">Whether to render the quads solid or in wireframe.</param>
        /// <param name="camera">The camera to render them to.</param>
        static public void Draw(FillMode fillMode, Camera camera)
        {
            //If there are no quads, skip everything.
            if (Quads.Count != 0)
            {
                LoadQuads();
                PreRender(fillMode, camera);

                for (int i = 0; i < Quads.Count; i++)
                {
                    //If quad has SoloCameras, only render to them. 
                    if (Quads[i].SoloCamera == null || Quads[i].SoloCamera.Contains(camera.CameraName))
                    {
                        //Set the texture.
                        basicEffect.Texture = Quads[i].Texture.Texture;
                                          
                        //Render quad.
                        foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, i * 4, 0, 4, 0, 2);
                        }
                    }
                }

                //Clear the lists ready for next frame.
                Quads.Clear();
                vertices.Clear();
                indices.Clear();
            }
        }
    }
}
