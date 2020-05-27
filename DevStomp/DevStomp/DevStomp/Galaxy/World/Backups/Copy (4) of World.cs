using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DevStomp
{
    class World5
    {
        class Chunk
        {
            public class Quad
            {
                public VertexPositionColorTexture[] vertices;
                public short[] indices;

                public void Initialize(Vector2 Position, int QuadSize, byte Shape, GraphicsDevice graphics, float[] Interp, byte[] ID)
                {
                    //QuadSize *= (int)(WorldVariables.RandomNumber.NextDouble() * 2);

                    //Color color = ColorManager.GetShades(Color.Green, 0.85f)[WorldVariables.RandomNumber.Next(0, 5)];

                    Color[] colors = new Color[ID.Length];

                    for (int i = 0; i < ID.Length; i++)
                    {
                        switch (ID[i])
                        {
                            case 1:
                                colors[i] = Color.White;
                                break;
                            case 2:
                                colors[i] = Color.Brown;
                                break;
                            case 3:
                                colors[i] = Color.Green;
                                break;
                        }
                    }
                    
                    Color color = Color.White;

                    #region Shape
                    switch (Shape)
                    {
                        case 0:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 2; indices[2] = 1;
                            indices[3] = 2; indices[4] = 1; indices[5] = 3;
                            break;

                        case 1:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), colors[0], new Vector2(0, Interp[0]));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), colors[2], new Vector2((1 - Interp[2]), 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 4;
                            indices[3] = 1; indices[4] = 2; indices[5] = 4;
                            indices[6] = 2; indices[7] = 3; indices[8] = 4;
                            break;

                        case 2:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), colors[3], new Vector2(Interp[3], 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), colors[1], new Vector2(1, Interp[1]));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            indices[6] = 0; indices[7] = 3; indices[8] = 4;
                            break;

                        case 3:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), colors[0], new Vector2(0, Interp[0]));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), colors[1], new Vector2(1, Interp[1]));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            break;

                        case 4:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), colors[2], new Vector2(1, (1 - Interp[2])));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), colors[0], new Vector2(Interp[0], 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 4;
                            indices[3] = 1; indices[4] = 3; indices[5] = 4;
                            indices[6] = 1; indices[7] = 2; indices[8] = 3;
                            break;

                        case 5:
                            vertices = new VertexPositionColorTexture[6];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), colors[0], new Vector2(0, Interp[0]));
                            vertices[5] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), colors[0], new Vector2(Interp[0], 0));

                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), colors[2], new Vector2((1 - Interp[2]), 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), colors[2], new Vector2(1, (1 - Interp[2])));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 5;
                            indices[3] = 2; indices[4] = 3; indices[5] = 4;
                            break;

                        case 6:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), colors[3], new Vector2(Interp[3], 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), colors[0], new Vector2(Interp[0], 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            break;

                        case 7:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), colors[0], new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), colors[0], new Vector2(0, Interp[0]));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), colors[0], new Vector2(Interp[0], 0));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 8:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), colors[3], new Vector2(0, (1 - Interp[3])));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), colors[1], new Vector2((1 - Interp[1]), 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 2; indices[4] = 0; indices[5] = 4;
                            indices[6] = 2; indices[7] = 3; indices[8] = 4;
                            break;

                        case 9:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), colors[1], new Vector2((1 - Interp[1]), 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), colors[2], new Vector2((1 - Interp[2]), 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            break;

                        case 10:
                            vertices = new VertexPositionColorTexture[6];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), colors[3], new Vector2(0, (1 - Interp[3])));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), colors[3], new Vector2(Interp[3], 1));

                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), colors[1], new Vector2(1, Interp[1]));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));
                            vertices[5] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), colors[1], new Vector2((1 - Interp[1]), 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 3; indices[4] = 4; indices[5] = 5;
                            break;

                        case 11:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), colors[1], new Vector2(1, Interp[1]));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), colors[1], new Vector2((1 - Interp[1]), 0));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 12:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), colors[3], new Vector2(0, (1 - Interp[3])));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), colors[2], new Vector2(1, (1 - Interp[2])));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 3; indices[2] = 1;
                            indices[3] = 3; indices[4] = 1; indices[5] = 2;
                            break;

                        case 13:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), colors[2], new Vector2((1 - Interp[2]), 1));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), colors[2], new Vector2(1, (1 - Interp[2])));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 14:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), colors[3], new Vector2(0, (1 - Interp[3])));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), colors[3], new Vector2(Interp[3], 1));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 15:
                            break;
                    }
                    #endregion

                    #region Shape
                    //switch (Shape)
                    //{
                    //    case 0:
                    //        vertices = new VertexPositionColorTexture[4];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 1; indices[4] = 2; indices[5] = 3;
                    //        break;

                    //    case 1:
                    //        vertices = new VertexPositionColorTexture[5];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[9];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 4;
                    //        indices[3] = 1; indices[4] = 2; indices[5] = 4;
                    //        indices[6] = 2; indices[7] = 3; indices[8] = 4;
                    //        break;

                    //    case 2:
                    //        vertices = new VertexPositionColorTexture[5];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[9];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 0; indices[4] = 2; indices[5] = 3;
                    //        indices[6] = 0; indices[7] = 3; indices[8] = 4;
                    //        break;

                    //    case 3:
                    //        vertices = new VertexPositionColorTexture[4];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 0; indices[4] = 2; indices[5] = 3;
                    //        break;

                    //    case 4:
                    //        vertices = new VertexPositionColorTexture[5];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[9];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 4;
                    //        indices[3] = 1; indices[4] = 3; indices[5] = 4;
                    //        indices[6] = 1; indices[7] = 2; indices[8] = 3;
                    //        break;

                    //    case 5:
                    //        vertices = new VertexPositionColorTexture[6];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[5] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 5;
                    //        indices[3] = 2; indices[4] = 3; indices[5] = 4;
                    //        break;

                    //    case 6:
                    //        vertices = new VertexPositionColorTexture[4];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 0; indices[4] = 2; indices[5] = 3;
                    //        break;

                    //    case 7:
                    //        vertices = new VertexPositionColorTexture[3];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[3];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        break;

                    //    case 8:
                    //        vertices = new VertexPositionColorTexture[5];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[9];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 2; indices[4] = 0; indices[5] = 4;
                    //        indices[6] = 2; indices[7] = 3; indices[8] = 4;
                    //        break;

                    //    case 9:
                    //        vertices = new VertexPositionColorTexture[4];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 0; indices[4] = 2; indices[5] = 3;
                    //        break;

                    //    case 10:
                    //        vertices = new VertexPositionColorTexture[6];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[5] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 3; indices[4] = 4; indices[5] = 5;
                    //        break;

                    //    case 11:
                    //        vertices = new VertexPositionColorTexture[3];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[3];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        break;

                    //    case 12:
                    //        vertices = new VertexPositionColorTexture[4];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[6];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        indices[3] = 1; indices[4] = 2; indices[5] = 3;
                    //        break;

                    //    case 13:
                    //        vertices = new VertexPositionColorTexture[3];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[3];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        break;

                    //    case 14:
                    //        vertices = new VertexPositionColorTexture[3];

                    //        vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize / 2), 0), Color.Red, new Vector2(0, 0));
                    //        vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));
                    //        vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize / 2), Position.Y + QuadSize, 0), Color.Red, new Vector2(0, 0));

                    //        indices = new short[3];

                    //        indices[0] = 0; indices[1] = 1; indices[2] = 2;
                    //        break;

                    //    case 15:
                    //        break;
                    //}
                    #endregion
                }
            }

            public Quad[][] ChunkQuads;
            public byte[][] ID;
            public byte[][] N;
            public byte[][] Interpolation;
            
            public BasicEffect basicEffect;
            public VertexBuffer vertexBuffer;
            public IndexBuffer indexBuffer;
            public VertexPositionColorTexture[] vertices;
            public short[] indices;

            public void Initialize(GraphicsDevice graphics, int ChunkSize)
            {
                ChunkQuads = new Quad[ChunkSize][];
                ID = new byte[ChunkSize + 1][];
                Interpolation = new byte[ChunkSize + 1][];

                for (int x = 0; x < ID.Length; x++)
                {
                    ID[x] = new byte[ChunkSize + 1];
                    for (int y = 0; y < ID[x].Length; y++)
                    {
                        ID[x][y] = (byte)GlobalVariables.RandomNumber.Next(0, 4);


                        //if (ID[x][y] > 5)
                        //    ID[x][y] = 0;
                        //else
                        //    ID[x][y] = 1;
                    }
                }

                bool Interpolate;
                for (int x = 0; x < ID.Length; x++)
                {
                    Interpolation[x] = new byte[ChunkSize + 1];
                    for (int y = 0; y < ID[x].Length; y++)
                    {
                            Interpolate = false;

                            if (ID[x][y] != 0)
                            {
                                if (x - 1 >= 0)
                                    if (ID[x - 1][y] != 0)
                                        Interpolate = true;
                                    else if (y - 1 >= 0)
                                        if (ID[x][y - 1] != 0)
                                            Interpolate = true;

                                        else if (x + 1 < ID.Length)
                                            if (ID[x + 1][y] != 0)
                                                Interpolate = true;
                                            else if (y + 1 < ID[x].Length)
                                                if (ID[x][y + 1] != 0)
                                                    Interpolate = true;
                                                else
                                                    Interpolation[x][y] = 255;


                            }
                            else
                                Interpolation[x][y] = 0;


                        if (Interpolate)
                            Interpolation[x][y] = (byte)(GlobalVariables.RandomNumber.NextDouble() * 255);

                    }
                }

                basicEffect = new BasicEffect(graphics);
            }

            public void CalculateChunk(GraphicsDevice graphics, Vector2 Position, int QuadSize, int ChunkSize)
            {
                byte[] IDs = new byte[4];
                int VerticiesLength = 0;
                int IndicesLength = 0;
                N = new byte[ChunkSize][];

                for (int x = 0; x < ChunkQuads.Length; x++)
                {
                    ChunkQuads[x] = new Quad[ChunkSize];
                    N[x] = new byte[ChunkSize];

                    for (int y = 0; y < ChunkQuads[x].Length; y++)
                    {
                        N[x][y] = 0;

                        if (Interpolation[x][y] == 0)
                            N[x][y] += 8;
                        if (Interpolation[x + 1][y] == 0)
                            N[x][y] += 4;
                        if (Interpolation[x + 1][y + 1] == 0)
                            N[x][y] += 2;
                        if (Interpolation[x][y + 1] == 0)
                            N[x][y] += 1;

                        IDs[0] = ID[x][y];
                        IDs[1] = ID[x + 1][y];
                        IDs[2] = ID[x + 1][y + 1];
                        IDs[3] = ID[x][y + 1];

                        float[] IntX = new float[4];
                        if (N[x][y] != 15 && N[x][y] != 0)
                        {
                            //IntX[0] = (float)Interpolation[x][y] / 255f;
                            //IntX[1] = (float)Interpolation[x + 1][y] / 255f;
                            //IntX[2] = (float)Interpolation[x + 1][y + 1] / 255f;
                            //IntX[3] = (float)Interpolation[x][y + 1] / 255f;

                            float max = 0.75f;
                            float min = 0.25f;

                            IntX[0] = UsefulMethods.FindBetween(Interpolation[x][y], 255f, 0f, max, min, false);
                            IntX[1] = UsefulMethods.FindBetween(Interpolation[x + 1][y], 255f, 0f, max, min, false);
                            IntX[2] = UsefulMethods.FindBetween(Interpolation[x + 1][y + 1], 255f, 0f, max, min, false);
                            IntX[3] = UsefulMethods.FindBetween(Interpolation[x][y + 1], 255f, 0f, max, min, false);
                        }

                        if (N[x][y] != 15)
                        {
                            ChunkQuads[x][y] = new Quad();
                            ChunkQuads[x][y].Initialize(Position + new Vector2(QuadSize * x, QuadSize * y), QuadSize, N[x][y], graphics, IntX, IDs);
                            VerticiesLength += ChunkQuads[x][y].vertices.Length;
                            IndicesLength += ChunkQuads[x][y].indices.Length;
                        }
                    }
                }


                vertices = new VertexPositionColorTexture[VerticiesLength];
                indices = new short[IndicesLength];

                int V = 0;
                int I = 0;

                for (int x = 0; x < ChunkQuads.Length; x++)
                    for (int y = 0; y < ChunkQuads[x].Length; y++)
                    {
                        if (ChunkQuads[x][y] != null)
                        {
                            for (int i = 0; i < ChunkQuads[x][y].indices.Length; i++)
                            {
                                indices[I] = (short)(ChunkQuads[x][y].indices[i] + V);
                                I++;
                            }

                            for (int i = 0; i < ChunkQuads[x][y].vertices.Length; i++)
                            {
                                vertices[V] = ChunkQuads[x][y].vertices[i];
                                V++;
                            }
                        }
                    }
                
                RefreshBuffers(graphics);
            }

            public void RefreshBuffers(GraphicsDevice graphics)
            {
                vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColorTexture), vertices.Length, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColorTexture>(vertices);
                indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
                indexBuffer.SetData(indices);
            }

            public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Vector2 position, short quadSize)
            {
                basicEffect.World = Matrix.CreateTranslation(0, 0, 0);
                basicEffect.View = CameraManager.Cams[0].Transform;
                basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, (float)GlobalVariables.WindowWidth, (float)GlobalVariables.WindowHeight, 0, 1, 0);

                basicEffect.VertexColorEnabled = true;
                basicEffect.Texture = StaticTests.Stone;
                basicEffect.TextureEnabled = false;
                if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                    basicEffect.TextureEnabled = true;

                graphics.SetVertexBuffer(vertexBuffer);
                graphics.Indices = indexBuffer;

                RasterizerState rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                rasterizerState.FillMode = FillMode.WireFrame;
                if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                    rasterizerState.FillMode = FillMode.Solid;
                graphics.RasterizerState = rasterizerState;

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexBuffer.VertexCount, 0, indexBuffer.IndexCount / 3);
                }


                if (InputManager.KB.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftAlt))
                {
                    for (int x = 0; x < ChunkQuads.Length + 1; x++)
                        for (int y = 0; y < ChunkQuads.Length + 1; y++)
                        {
                            spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * x, quadSize * y) + position, null, Color.LightGreen, 0f,
                                new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(2f, 0.1f), SpriteEffects.None, 1f);

                            spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * x, quadSize * y) + position, null, Color.LightGreen, 0f,
                                new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(0.1f, 2f), SpriteEffects.None, 1f);

                            //InfoBox.AddItem("" + Interpolation[x][y]);
                            //InfoBox.AddItem("" + ID[x][y]);
                            //InfoBox.Draw(spriteBatch, new Vector2(quadSize * x, quadSize * y) + position, 1.5f, 1f);
                            //InfoBox.ClearList();
                        }

                    spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * ChunkQuads.Length, quadSize * ChunkQuads.Length) + position, null, Color.Yellow * 0.5f, 0f,
                        new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(1f, (ChunkQuads.Length * quadSize) / (StaticTests.Marker.Height / 2)), SpriteEffects.None, 1f);

                    spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * ChunkQuads.Length, quadSize * ChunkQuads.Length) + position, null, Color.Yellow * 0.5f, 0f,
                        new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2((ChunkQuads.Length * quadSize) / (StaticTests.Marker.Width / 2), 1f), SpriteEffects.None, 1f);
                }
            }
        }

        Chunk[][] TestChunk;
        short QuadSize;
        short ChunkSize;
        int WorldX;
        int WorldY;
        List<Vector4> EditedPoints;
        Vector2 CenterOfMass;

        List<Vector2> CollisionChunks;

        public void Initialize(Vector2 Position, GraphicsDevice graphics)
        {
            #region OLD
            //basicEffect = new BasicEffect(graphics);

            //VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[12];

            //vertices[0] = new VertexPositionColorTexture(new Vector3(0, 0, 0), Color.Red, Vector2.Zero);
            //vertices[1] = new VertexPositionColorTexture(new Vector3(0, -50, 0), Color.Orange, Vector2.Zero);
            //vertices[2] = new VertexPositionColorTexture(new Vector3(50, 0, 0), Color.Yellow, Vector2.Zero);
            //vertices[3] = new VertexPositionColorTexture(new Vector3(50, -50, 0), Color.Green, Vector2.Zero);
            //vertices[4] = new VertexPositionColorTexture(new Vector3(100, 0, 0), Color.Blue, Vector2.Zero);
            //vertices[5] = new VertexPositionColorTexture(new Vector3(100, -50, 0), Color.Indigo, Vector2.Zero);
            //vertices[6] = new VertexPositionColorTexture(new Vector3(150, 0, 0), Color.Purple, Vector2.Zero);
            //vertices[7] = new VertexPositionColorTexture(new Vector3(150, -50, 0), Color.White, Vector2.Zero);
            //vertices[8] = new VertexPositionColorTexture(new Vector3(200, 0, 0), Color.Cyan, Vector2.Zero);
            //vertices[9] = new VertexPositionColorTexture(new Vector3(200, -50, 0), Color.Black, Vector2.Zero);
            //vertices[10] = new VertexPositionColorTexture(new Vector3(250, 0, 0), Color.DodgerBlue, Vector2.Zero);
            //vertices[11] = new VertexPositionColorTexture(new Vector3(250, -50, 0), Color.Crimson, Vector2.Zero);

            //vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColorTexture), 12, BufferUsage.WriteOnly);
            //vertexBuffer.SetData<VertexPositionColorTexture>(vertices);


            //short[] indices = new short[60];
            //indices[0] = 0; indices[1] = 1; indices[2] = 2;
            //indices[3] = 3; indices[4] = 4; indices[5] = 5;
            //indices[6] = 6; indices[7] = 7; indices[8] = 8;
            //indices[9] = 9; indices[10] = 10; indices[11] = 11;

            ////indices[3] = 1; indices[4] = 2; indices[5] = 3;
            ////indices[6] = 2; indices[7] = 3; indices[8] = 4;
            ////indices[9] = 3; indices[10] = 4; indices[11] = 5;
            ////indices[12] = 4; indices[13] = 5; indices[14] = 6;
            ////indices[15] = 5; indices[16] = 6; indices[17] = 7;
            ////indices[18] = 6; indices[19] = 7; indices[20] = 8;
            ////indices[21] = 7; indices[22] = 8; indices[23] = 9;
            ////indices[24] = 8; indices[25] = 9; indices[26] = 10;
            ////indices[27] = 9; indices[28] = 10; indices[29] = 11;

            ////indices[30] = 10; indices[31] = 11; indices[32] = 10;
            ////indices[33] = 11; indices[34] = 11; indices[35] = 11;

            //indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
            //indexBuffer.SetData(indices);
            #endregion
            //test = new Chunk();
            //test.Initialize(pos, graphics);

            WorldX = 3;
            WorldY = 3;

            QuadSize = 256;
            ChunkSize = 32;

            EditedPoints = new List<Vector4>();

            TestChunk = new Chunk[WorldX][];

            ////////////////////////////////////////////////////
            CollisionChunks = new List<Vector2>();
            ////////////////////////////////////////////////////


            for (int x = 0; x < TestChunk.Length; x++)
            {
                TestChunk[x] = new Chunk[WorldY];
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                        TestChunk[x][y] = new Chunk();
                        TestChunk[x][y].Initialize(graphics, ChunkSize);                    
                }
            }

            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    SetEdges(x, y);
                }

            CenterOfMass = new Vector2((TestChunk.Length * QuadSize * ChunkSize) / 2, (TestChunk[0].Length * QuadSize * ChunkSize) / 2); 

            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                        TestChunk[x][y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y) - CenterOfMass, QuadSize, ChunkSize);                    
                }
        }
        
        public Vector2 SetEdges(int X, int Y)
        {
            Vector2 Edges = Vector2.Zero;

            int XTarget;
            int YTarget;

            XTarget = X - 1;
            YTarget = Y;
            if (XTarget >= 0 && XTarget < WorldX)
            {
                for (int y = 0; y <= ChunkSize; y++)
                {
                    TestChunk[XTarget][YTarget].ID[ChunkSize][y] = TestChunk[X][Y].ID[0][y];
                    TestChunk[XTarget][YTarget].Interpolation[ChunkSize][y] = TestChunk[X][Y].Interpolation[0][y];
                }

                Edges.X = -1;
            }

            XTarget = X + 1;
            YTarget = Y;
            if (XTarget >= 0 && XTarget < WorldX)
            {
                for (int y = 0; y <= ChunkSize; y++)
                {
                    TestChunk[XTarget][YTarget].ID[0][y] = TestChunk[X][Y].ID[ChunkSize][y];
                    TestChunk[XTarget][YTarget].Interpolation[0][y] = TestChunk[X][Y].Interpolation[ChunkSize][y];
                }

                Edges.X = 1;
            }


            XTarget = X;
            YTarget = Y - 1;
            if (YTarget >= 0 && YTarget < WorldY)
            {
                for (int x = 0; x <= ChunkSize; x++)
                {
                    TestChunk[XTarget][YTarget].ID[x][ChunkSize] = TestChunk[X][Y].ID[x][0];
                    TestChunk[XTarget][YTarget].Interpolation[x][ChunkSize] = TestChunk[X][Y].Interpolation[x][0];
                }

                Edges.Y = -1;
            }

            XTarget = X;
            YTarget = Y + 1;
            if (YTarget >= 0 && YTarget < WorldY)
            {
                for (int x = 0; x <= ChunkSize; x++)
                {
                    TestChunk[XTarget][YTarget].ID[x][0] = TestChunk[X][Y].ID[x][ChunkSize];
                    TestChunk[XTarget][YTarget].Interpolation[x][0] = TestChunk[X][Y].Interpolation[x][ChunkSize];
                }

                Edges.Y = 1;
            }


            if (Edges.X != 0 && Edges.Y != 0)
            {
                XTarget = X + (int)Edges.X;
                YTarget = Y + (int)Edges.Y;

                TestChunk[XTarget][YTarget].ID[0][0] = TestChunk[X][Y].ID[ChunkSize][ChunkSize];
                TestChunk[XTarget][YTarget].Interpolation[0][0] = TestChunk[X][Y].Interpolation[ChunkSize][ChunkSize];

                TestChunk[XTarget][YTarget].ID[ChunkSize][ChunkSize] = TestChunk[X][Y].ID[0][0];
                TestChunk[XTarget][YTarget].Interpolation[ChunkSize][ChunkSize] = TestChunk[X][Y].Interpolation[0][0];

                TestChunk[XTarget][YTarget].ID[0][ChunkSize] = TestChunk[X][Y].ID[ChunkSize][0];
                TestChunk[XTarget][YTarget].Interpolation[0][ChunkSize] = TestChunk[X][Y].Interpolation[ChunkSize][0];

                TestChunk[XTarget][YTarget].ID[ChunkSize][0] = TestChunk[X][Y].ID[0][ChunkSize];
                TestChunk[XTarget][YTarget].Interpolation[ChunkSize][0] = TestChunk[X][Y].Interpolation[0][ChunkSize];
            }

            return Edges;
        }

        public void EditPoint(int CX, int CY, int X, int Y, float Addition)
        {      
            while (X > ChunkSize)
            {
                X -= ChunkSize;
                CX++;
            }
            while (X < 0)
            {
                X += ChunkSize;
                CX--;
            }

            while (Y > ChunkSize)
            {
                Y -= ChunkSize;
                CY++;
            }
            while (Y < 0)
            {
                Y += ChunkSize;
                CY--;
            }

            if (CX < 0 || CY < 0 || CX >= TestChunk.Length || CY >= TestChunk.Length)
            {
            }
            else
            {
                float Interp = TestChunk[CX][CY].Interpolation[X][Y] / 255f;
                Interp += Addition;

                if (Interp < 0f)
                    Interp = 0f;
                else if (Interp > 1f)
                    Interp = 1f;

                TestChunk[CX][CY].Interpolation[X][Y] = (byte)(Interp * 255f);

                if (Interp == 0)
                    TestChunk[CX][CY].ID[X][Y] = 0;
                else
                    TestChunk[CX][CY].ID[X][Y] = 1;

                EditedPoints.Add(new Vector4(CX, CY, X, Y));
            }
        }

        public void EditTile(Vector2 Pos, float Addition)
        {
            Pos += CenterOfMass;

            Vector2 SelectedChunk = Pos / (QuadSize * ChunkSize);
            SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
            SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);

            Vector2 ChunkPosition = (Pos / QuadSize) - (SelectedChunk * ChunkSize);
            ChunkPosition.X = (float)Math.Floor(ChunkPosition.X);
            ChunkPosition.Y = (float)Math.Floor(ChunkPosition.Y);

            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, Addition);
            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, Addition);
            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, Addition);
            EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, Addition);
        }

        public bool IsPointInPolygon(Vector2 p, Vector2[] polygon)
        {
            double minX = polygon[0].X;
            double maxX = polygon[0].X;
            double minY = polygon[0].Y;
            double maxY = polygon[0].Y;
            for (int i = 1; i < polygon.Length; i++)
            {
                Vector2 q = polygon[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minY = Math.Min(q.Y, minY);
                maxY = Math.Max(q.Y, maxY);
            }

            if (p.X < minX || p.X > maxX || p.Y < minY || p.Y > maxY)
            {
                return false;
            }

            // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                     p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public Vector2 CheckTile(Vector2 Pos, Vector2 PreviousPos)
        {
            Pos += CenterOfMass;

            Vector2 SelectedChunk = Pos / (QuadSize * ChunkSize);
            SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
            SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);
            
            Vector2 ChunkPosition = (Pos / QuadSize) - (SelectedChunk * ChunkSize);
            ChunkPosition.X = (float)Math.Floor(ChunkPosition.X);
            ChunkPosition.Y = (float)Math.Floor(ChunkPosition.Y);
                        
            if (SelectedChunk.X >= 0 && SelectedChunk.X < WorldX && SelectedChunk.Y >= 0 && SelectedChunk.Y < WorldY)
            {
                if (TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y] != null)
                {
                    #region Find Closest Vertex
                    //Pos -= CenterOfMass;
                    //float Distance = 0f;
                    //float NewDistance;

                    //Vector2 FinalPoint = Vector2.Zero;

                    //for (int i = 0; i < TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices.Length; i++)
                    //{
                    //    Vector2 Vertex = new Vector2(
                    //        TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[i].Position.X,
                    //        TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[i].Position.Y);
                        
                    //    NewDistance = Vector2.Distance(Pos, Vertex);

                    //    if (NewDistance < Distance || i == 0)
                    //    {
                    //        FinalPoint = Vertex;
                    //        Distance = NewDistance;
                    //    }
                    //}

                    //return FinalPoint;
                    #endregion

                    #region Enhanced Edge Detection
                    //Pos -= CenterOfMass;
                    //List<Vector2> IntersectionPoints = new List<Vector2>();

                    //for (int i = 0; i < TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].indices.Length / 3; i++)
                    //{
                    //    short v1 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].indices[0 + (3 * i)];
                    //    short v2 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].indices[1 + (3 * i)];
                    //    short v3 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].indices[2 + (3 * i)];
                    //    Vector3 Vertex1 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[v1].Position;
                    //    Vector3 Vertex2 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[v2].Position;
                    //    Vector3 Vertex3 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[v3].Position;
                    //    Vector2 InterPoint;

                    //    if (CheckCollision(Pos, PreviousPos, new Vector2(Vertex1.X, Vertex1.Y), new Vector2(Vertex2.X, Vertex2.Y)))
                    //    {
                    //        InterPoint = IntersectionPoint(Pos, PreviousPos, new Vector2(Vertex1.X, Vertex1.Y), new Vector2(Vertex2.X, Vertex2.Y));
                    //        if (InterPoint != Vector2.Zero)
                    //            IntersectionPoints.Add(InterPoint);
                    //    }

                    //    if (CheckCollision(Pos, PreviousPos, new Vector2(Vertex2.X, Vertex2.Y), new Vector2(Vertex3.X, Vertex3.Y)))
                    //    {
                    //        InterPoint = IntersectionPoint(Pos, PreviousPos, new Vector2(Vertex2.X, Vertex2.Y), new Vector2(Vertex3.X, Vertex3.Y));
                    //        if (InterPoint != Vector2.Zero)
                    //            IntersectionPoints.Add(InterPoint);
                    //    }

                    //    if (CheckCollision(Pos, PreviousPos, new Vector2(Vertex3.X, Vertex3.Y), new Vector2(Vertex1.X, Vertex1.Y)))
                    //    {
                    //        InterPoint = IntersectionPoint(Pos, PreviousPos, new Vector2(Vertex3.X, Vertex3.Y), new Vector2(Vertex1.X, Vertex1.Y));
                    //        if (InterPoint != Vector2.Zero)
                    //            IntersectionPoints.Add(InterPoint);
                    //    }
                    //}

                    //Vector2 MinVector = Vector2.Zero;

                    //for (int i = 0; i < IntersectionPoints.Count; i++)
                    //{
                    //    if (i == 0)
                    //        MinVector = IntersectionPoints[i];
                    //    else if (Vector2.Distance(PreviousPos, MinVector) > Vector2.Distance(PreviousPos, IntersectionPoints[i]))
                    //        MinVector = IntersectionPoints[i];
                    //}

                    //return MinVector;
                    #endregion

                    Pos -= CenterOfMass;


                    short[] Indices = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].indices;
                    VertexPositionColorTexture[] Vertices = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices;
                    
                    int vertices = Vertices.Length;
                    int indices = Indices.Length;
                    List<Vector2[]> Points = new List<Vector2[]>();

                    for (int i = 0; i < indices / 3; i++)
                    {
                        Points.Add(new Vector2[3]);
                        Vector3 p1 = Vertices[Indices[0 + (3 * i)]].Position;
                        Vector3 p2 = Vertices[Indices[1 + (3 * i)]].Position;
                        Vector3 p3 = Vertices[Indices[2 + (3 * i)]].Position;

                        Points[i][0] = new Vector2(p1.X, p1.Y);
                        Points[i][1] = new Vector2(p2.X, p2.Y);
                        Points[i][2] = new Vector2(p3.X, p3.Y);
                    }
                    Vertices = null;


                    for (int p = 0; p < Points.Count; p++)
                        if (IsPointInPolygon(Pos, Points[p]))
                        {
                            CollisionChunks.Add(new Vector2((QuadSize * ChunkSize * SelectedChunk.X) + (QuadSize * ChunkPosition.X) + (QuadSize / 2), (QuadSize * ChunkSize * SelectedChunk.Y) + (QuadSize * ChunkPosition.Y) + (QuadSize / 2)));

                            float[] Distances = new float[3];
                            bool PointFound = true;

                            Distances[0] = DistanceToLine(Points[p][0], Points[p][1], Pos);
                            Distances[1] = DistanceToLine(Points[p][1], Points[p][2], Pos);
                            Distances[2] = DistanceToLine(Points[p][2], Points[p][0], Pos);

                            float smallest = Math.Min(Distances[0], Math.Min(Distances[1], Distances[2]));
                            Vector2 ClosestPoint = Vector2.Zero;

                            if (smallest == Distances[0])
                                ClosestPoint = GetClosetPoint(Points[p][0], Points[p][1], Pos, true);
                            else if (smallest == Distances[1])
                                ClosestPoint = GetClosetPoint(Points[p][1], Points[p][2], Pos, true);
                            else if (smallest == Distances[2])
                                ClosestPoint = GetClosetPoint(Points[p][2], Points[p][0], Pos, true);
                            ClosestPoint = ClosestPoint - ((Pos - ClosestPoint) * 0.01f);

                                return ClosestPoint;














                            //Vector2 PosDifference = PreviousPos - ((Pos - PreviousPos) * 10f);
                            //Vector2 ReturnPosition = Vector2.Zero;

                            //if (CheckCollision(Pos, PosDifference, Points[p][0], Points[p][1]))
                            //{
                            //    ReturnPosition = IntersectionPoint(Pos, PreviousPos, Points[p][0], Points[p][1]);
                            //    //if (!IsPointInPolygon(ReturnPosition, Points[p]))
                            //        return ReturnPosition;
                            //}

                            //if (CheckCollision(Pos, PosDifference, Points[p][1], Points[p][2]))
                            //{
                            //    ReturnPosition = IntersectionPoint(Pos, PreviousPos, Points[p][1], Points[p][2]);
                            //    //if (!IsPointInPolygon(ReturnPosition, Points[p]))
                            //        return ReturnPosition;
                            //}

                            //if (CheckCollision(Pos, PosDifference, Points[p][2], Points[p][0]))
                            //{
                            //    ReturnPosition = IntersectionPoint(Pos, PreviousPos, Points[p][2], Points[p][0]);
                            //    //if (!IsPointInPolygon(ReturnPosition, Points[p]))
                            //        return ReturnPosition;
                            //}



                            //int VertexCount = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices.Length;

                            //for (int i = 0; i < VertexCount; i++)
                            //{
                            //    Vector3 Vertex1 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[i].Position;
                            //    Vector3 Vertex2;

                            //    if (i == 0)
                            //        Vertex2 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[VertexCount - 1].Position;
                            //    else
                            //        Vertex2 = TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].ChunkQuads[(int)ChunkPosition.X][(int)ChunkPosition.Y].vertices[i - 1].Position;

                            //    if (CheckCollision(Pos, PreviousPos, new Vector2(Vertex1.X, Vertex1.Y), new Vector2(Vertex2.X, Vertex2.Y)))
                            //        return Vector2.One;
                            //}
                        }
                }
            }

            return Vector2.Zero;
        }

        public Vector2 GetClosetPoint(Vector2 A, Vector2 B, Vector2 P, bool segmentClamp)
        {
            Vector2 AP = P - A;
            Vector2 AB = B - A;
            float ab2 = AB.X * AB.X + AB.Y * AB.Y;
            float ap_ab = AP.X * AB.X + AP.Y * AB.Y;
            float t = ap_ab / ab2;
            if (segmentClamp)
            {
                if (t < 0.0f) t = 0.0f;
                else if (t > 1.0f) t = 1.0f;
            }
            Vector2 Closest = A + AB * t;
            return Closest;
        }

        float DistanceToLine(Vector2 v, Vector2 w, Vector2 p)
        {
            // Return minimum distance between line segment vw and point p
            float l2 = Vector2.DistanceSquared(v, w);  // i.e. |w-v|^2 -  avoid a sqrt
            if (l2 == 0.0) return Vector2.Distance(p, v);   // v == w case
            // Consider the line extending the segment, parameterized as v + t (w - v).
            // We find projection of point p onto the line. 
            // It falls where t = [(p-v) . (w-v)] / |w-v|^2
            float t = Vector2.Dot(p - v, w - v) / l2;
            
            if (t < 0.0) return Vector2.Distance(p, v);       // Beyond the 'v' end of the segment
            else if (t > 1.0) return Vector2.Distance(p, w);  // Beyond the 'w' end of the segment

            Vector2 projection = v + t * (w - v);  // Projection falls on the segment
            return Vector2.Distance(p, projection);
        }

        public bool CheckCollision(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            float denominator = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
            float numerator1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
            float numerator2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));

            // Detect coincident lines (has a problem, read below)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

        public Vector2 IntersectionPoint(Vector2 Position, Vector2 PreviousPosition, Vector2 Point1, Vector2 Point2)
        {
            // Get A,B,C of first line - points : ps1 to pe1
            float A1 = Position.Y - PreviousPosition.Y;
            float B1 = PreviousPosition.X - Position.X;
            float C1 = A1 * PreviousPosition.X + B1 * PreviousPosition.Y;

            // Get A,B,C of second line - points : ps2 to pe2
            float A2 = Point1.Y - Point2.Y;
            float B2 = Point2.X - Point1.X;
            float C2 = A2 * Point2.X + B2 * Point2.Y;

            // Get delta and check if the lines are parallel
            float delta = A1 * B2 - A2 * B1;
            if (delta == 0)            
                return Vector2.Zero;            
            else return
            new Vector2(
            (B2 * C1 - B1 * C2) / delta,
            (A1 * C2 - A2 * C1) / delta);
        }

        public void Update(GraphicsDevice graphics)
        {
            Vector2 MousePosition = new Vector2(InputManager.M.X - (GlobalVariables.WindowWidth / 2), InputManager.M.Y - (GlobalVariables.WindowHeight / 2));
            Vector2 CameraPosition = CameraManager.Cams[0].P + (MousePosition / CameraManager.Cams[0].Z) + CenterOfMass;
            
            Vector2 SelectedChunk = CameraPosition / (QuadSize * ChunkSize);
            SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
            SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);


            Vector2 ChunkPosition = (CameraPosition / QuadSize) - (SelectedChunk * ChunkSize);
            ChunkPosition.X = (float)Math.Round(ChunkPosition.X);
            ChunkPosition.Y = (float)Math.Round(ChunkPosition.Y);

            Vector2 Edge = Vector2.Zero;

            if (SelectedChunk.X >= 0 && SelectedChunk.X < WorldX && SelectedChunk.Y >= 0 && SelectedChunk.Y < WorldY)
            {
                if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                {
                    EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, -0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, -0.1f);

                    Edge = SetEdges((int)SelectedChunk.X, (int)SelectedChunk.Y);
                }
                else if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                {
                    EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, 0.1f);
                    //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, 0.1f);

                    Edge = SetEdges((int)SelectedChunk.X, (int)SelectedChunk.Y);
                }
            }

            if (EditedPoints.Count > 0)
            {
                EditedPoints = EditedPoints.Distinct().ToList();
                List<Vector2> EditChunks = new List<Vector2>();
                for (int i = 0; i < EditedPoints.Count; i++)
                {
                    EditChunks.Add(new Vector2(EditedPoints[i].X, EditedPoints[i].Y));
                    if (EditedPoints[i].Z == 0)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X - 1, EditedPoints[i].Y));
                    }

                    if (EditedPoints[i].W == 0)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X, EditedPoints[i].Y - 1));
                    }

                    if (EditedPoints[i].Z == ChunkSize)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X + 1, EditedPoints[i].Y));
                    }

                    if (EditedPoints[i].W == ChunkSize)
                    {
                        EditChunks.Add(new Vector2(EditedPoints[i].X, EditedPoints[i].Y + 1));
                    }
                }
                EditChunks = EditChunks.Distinct().ToList();
                for (int i = 0; i < EditChunks.Count; i++)                
                    if (EditChunks[i].X < 0 || EditChunks[i].Y < 0 || EditChunks[i].X >= TestChunk.Length || EditChunks[i].Y >= TestChunk.Length)
                    {
                        EditChunks.RemoveAt(i);
                        i--;
                    }

                for (int i = 0; i < EditChunks.Count; i++)
                {
                    TestChunk[(int)EditChunks[i].X][(int)EditChunks[i].Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * EditChunks[i].X, QuadSize * ChunkSize * EditChunks[i].Y) - CenterOfMass, QuadSize, ChunkSize);
                }


                //if (Edge.X != 0 && Edge.Y != 0)
                //{
                //    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y) - CenterOfMass, QuadSize, ChunkSize);
                //    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)) - CenterOfMass, QuadSize, ChunkSize);
                //    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)) - CenterOfMass, QuadSize, ChunkSize);
                //    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * SelectedChunk.Y) - CenterOfMass, QuadSize, ChunkSize);
                //}
                //else if (Edge.X != 0 || Edge.Y != 0)
                //{
                //    if (Edge.X != 0)
                //        TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);

                //    if (Edge.Y != 0)
                //        TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);

                //    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);
                //}

                EditedPoints.Clear();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    if (TestChunk[x][y] != null)
                        TestChunk[x][y].Draw(spriteBatch, graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y) - CenterOfMass, QuadSize);
                }

            spriteBatch.Draw(StaticTests.Marker, Vector2.Zero, null, Color.Yellow * 0.5f, 0f,
                new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(10f, 10f), SpriteEffects.None, 1f);

            for (int i = 0; i < CollisionChunks.Count; i++)
            {
                spriteBatch.Draw(StaticTests.Marker, CollisionChunks[i] - CenterOfMass - new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), Color.Green);
            }

            CollisionChunks.Clear();
        }
    }
}
