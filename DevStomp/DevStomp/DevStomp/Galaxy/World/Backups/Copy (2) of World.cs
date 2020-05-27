using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DevStomp
{
    class World3
    {
        class Chunk
        {
            public class Quad
            {
                public VertexPositionColorTexture[] vertices;
                public short[] indices;

                public void Initialize(Vector2 Position, int QuadSize, byte Shape, GraphicsDevice graphics, float[] Interp)
                {
                    //QuadSize *= (int)(WorldVariables.RandomNumber.NextDouble() * 2);

                    //Color color = ColorManager.GetShades(Color.Green, 0.85f)[WorldVariables.RandomNumber.Next(0, 5)];
                    Color color = Color.Red;

                    #region Shape
                    switch (Shape)
                    {
                        case 0:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 1; indices[4] = 2; indices[5] = 3;
                            break;

                        case 1:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 4;
                            indices[3] = 1; indices[4] = 2; indices[5] = 4;
                            indices[6] = 2; indices[7] = 3; indices[8] = 4;
                            break;

                        case 2:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), color, new Vector2(0, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            indices[6] = 0; indices[7] = 3; indices[8] = 4;
                            break;

                        case 3:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            break;

                        case 4:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), color, new Vector2(0, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 4;
                            indices[3] = 1; indices[4] = 3; indices[5] = 4;
                            indices[6] = 1; indices[7] = 2; indices[8] = 3;
                            break;

                        case 5:
                            vertices = new VertexPositionColorTexture[6];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), color, new Vector2(0, 0));
                            vertices[5] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), color, new Vector2(0, 0));

                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 5;
                            indices[3] = 2; indices[4] = 3; indices[5] = 4;
                            break;

                        case 6:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            break;

                        case 7:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * Interp[0]), 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[0]), Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 8:
                            vertices = new VertexPositionColorTexture[5];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[9];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 2; indices[4] = 0; indices[5] = 4;
                            indices[6] = 2; indices[7] = 3; indices[8] = 4;
                            break;

                        case 9:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 0; indices[4] = 2; indices[5] = 3;
                            break;

                        case 10:
                            vertices = new VertexPositionColorTexture[6];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), color, new Vector2(0, 0));

                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), color, new Vector2(0, 0));
                            vertices[4] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[5] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[1])), Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 3; indices[4] = 4; indices[5] = 5;
                            break;

                        case 11:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * Interp[1]), 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize  * (1 - Interp[1])), Position.Y, 0), color, new Vector2(0, 0));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 12:
                            vertices = new VertexPositionColorTexture[4];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize * (1 - Interp[2])), 0), color, new Vector2(0, 0));
                            vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));

                            indices = new short[6];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            indices[3] = 1; indices[4] = 2; indices[5] = 3;
                            break;

                        case 13:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * (1 - Interp[2])), Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + (QuadSize  * (1 - Interp[2])), 0), color, new Vector2(0, 0));

                            indices = new short[3];

                            indices[0] = 0; indices[1] = 1; indices[2] = 2;
                            break;

                        case 14:
                            vertices = new VertexPositionColorTexture[3];

                            vertices[0] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + (QuadSize * (1 - Interp[3])), 0), color, new Vector2(0, 0));
                            vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), color, new Vector2(0, 0));
                            vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + (QuadSize * Interp[3]), Position.Y + QuadSize, 0), color, new Vector2(0, 0));

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
                        ID[x][y] = (byte)GlobalVariables.RandomNumber.Next(0, 10);


                        if (ID[x][y] > 5)
                            ID[x][y] = 0;
                        else
                            ID[x][y] = 1;
                    }
                }


                for (int x = 0; x < ID.Length; x++)
                {
                    Interpolation[x] = new byte[ChunkSize + 1];
                    for (int y = 0; y < ID[x].Length; y++)
                    {
                            bool Interpolate = false;

                            if (ID[x][y] != 0)
                            {
                                if (x - 1 >= 0)
                                    if (ID[x - 1][y] == 0)
                                        Interpolate = true;
                                    else if (y - 1 >= 0)
                                        if (ID[x][y - 1] == 0)
                                            Interpolate = true;

                                        else if (x + 1 < ID.Length)
                                            if (ID[x + 1][y] == 0)
                                                Interpolate = true;
                                            else if (y + 1 < ID[x].Length)
                                                if (ID[x][y + 1] == 0)
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
                byte[][] N = new byte[ChunkSize][];
                int VerticiesLength = 0;
                int IndicesLength = 0;

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

                        float[] IntX = new float[4];
                        if (N[x][y] != 15 && N[x][y] != 0)
                        {
                            IntX[0] = (float)Interpolation[x][y] / 255f;
                            IntX[1] = (float)Interpolation[x + 1][y] / 255f;
                            IntX[2] = (float)Interpolation[x + 1][y + 1] / 255f;
                            IntX[3] = (float)Interpolation[x][y + 1] / 255f;
                        }

                        if (N[x][y] != 15)
                        {
                            ChunkQuads[x][y] = new Quad();
                            ChunkQuads[x][y].Initialize(Position + new Vector2(QuadSize * x, QuadSize * y), QuadSize, N[x][y], graphics, IntX);
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
                        if (N[x][y] != 15)
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
                //for (int i = 0; i < 100; i++)
                {
                    //vertexBuffer = new VertexBuffer(graphics, typeof(VertexPositionColorTexture), vertices.Length, BufferUsage.WriteOnly);
                    //vertexBuffer.SetData<VertexPositionColorTexture>(vertices);
                    //indexBuffer = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
                    //indexBuffer.SetData(indices);
                    
                    basicEffect.World = Matrix.CreateTranslation(0, 0, 0);
                    basicEffect.View = CameraManager.Cams[0].Transform;
                    basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, (float)GlobalVariables.WindowWidth, (float)GlobalVariables.WindowHeight, 0, 1, 0);

                    basicEffect.VertexColorEnabled = true;

                    graphics.SetVertexBuffer(vertexBuffer);
                    graphics.Indices = indexBuffer;

                    RasterizerState rasterizerState = new RasterizerState();
                    rasterizerState.CullMode = CullMode.None;
                    rasterizerState.FillMode = FillMode.Solid;
                    graphics.RasterizerState = rasterizerState;

                    foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexBuffer.VertexCount, 0, indexBuffer.IndexCount / 3);
                    }
                }

                //for (int x = 0; x < ChunkQuads.Length + 1; x++)
                //    for (int y = 0; y < ChunkQuads.Length + 1; y++)
                //    {
                //        spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * x, quadSize * y) + position, null, Color.LightGreen, 0f,
                //            new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(2f, 0.1f), SpriteEffects.None, 1f);

                //        spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * x, quadSize * y) + position, null, Color.LightGreen, 0f,
                //            new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(0.1f, 2f), SpriteEffects.None, 1f);

                //        InfoBox.AddItem("" + Interpolation[x][y]);
                //        InfoBox.AddItem("" + ID[x][y]);
                //        InfoBox.Draw(spriteBatch, new Vector2(quadSize * x, quadSize * y) + position, 1.5f, 1f);
                //        InfoBox.ClearList();
                //    }


                spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * ChunkQuads.Length, quadSize * ChunkQuads.Length) + position, null, Color.Yellow * 0.5f, 0f,
                    new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(1f, (ChunkQuads.Length * quadSize) / (StaticTests.Marker.Height / 2)), SpriteEffects.None, 1f);

                spriteBatch.Draw(StaticTests.Marker, new Vector2(quadSize * ChunkQuads.Length, quadSize * ChunkQuads.Length) + position, null, Color.Yellow * 0.5f, 0f,
                    new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2((ChunkQuads.Length * quadSize) / (StaticTests.Marker.Width / 2), 1f), SpriteEffects.None, 1f);
            
            
            
            }            
        }

        Chunk[][] TestChunk;
        short QuadSize;
        short ChunkSize;
        int WorldX;
        int WorldY;

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

            WorldX = 2;
            WorldY = 2;

            QuadSize = 16;
            ChunkSize = 64;


            TestChunk = new Chunk[WorldX][];

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
                

            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                        TestChunk[x][y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y), QuadSize, ChunkSize);                    
                }
        }

        public Vector2 SetEdges(int X, int Y)
        {
            Vector2 Edges = Vector2.Zero;
            
            int XTarget;
            int YTarget;

            XTarget = X - 1;
            YTarget = Y;
            if (XTarget != -1)
            {
                for (int y = 0; y <= ChunkSize; y++)
                {
                    TestChunk[XTarget][YTarget].ID[ChunkSize][y] = TestChunk[X][Y].ID[0][y];
                    TestChunk[XTarget][YTarget].Interpolation[ChunkSize][y] = TestChunk[X][Y].Interpolation[0][y];
                }

                Edges.X = -1;
            }
            else
            {
                XTarget = X + 1;
                YTarget = Y;
                if (XTarget != WorldY)
                {
                    for (int y = 0; y <= ChunkSize; y++)
                    {
                        TestChunk[XTarget][YTarget].ID[0][y] = TestChunk[X][Y].ID[ChunkSize][y];
                        TestChunk[XTarget][YTarget].Interpolation[0][y] = TestChunk[X][Y].Interpolation[ChunkSize][y];
                    }

                    Edges.X = 1;
                }
            }
            
            XTarget = X;
            YTarget = Y - 1;
            if (YTarget != -1)
            {
                for (int x = 0; x <= ChunkSize; x++)
                {
                    TestChunk[XTarget][YTarget].ID[x][ChunkSize] = TestChunk[X][Y].ID[x][0];
                    TestChunk[XTarget][YTarget].Interpolation[x][ChunkSize] = TestChunk[X][Y].Interpolation[x][0];
                }

                Edges.Y = -1;
            }
            else
            {
                XTarget = X;
                YTarget = Y + 1;
                if (YTarget != WorldY)
                {
                    for (int x = 0; x <= ChunkSize; x++)
                    {
                        TestChunk[XTarget][YTarget].ID[x][0] = TestChunk[X][Y].ID[x][ChunkSize];
                        TestChunk[XTarget][YTarget].Interpolation[x][0] = TestChunk[X][Y].Interpolation[x][ChunkSize];
                    }

                    Edges.Y = 1;
                }
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

            float Interp = TestChunk[CX][CY].Interpolation[X][Y] / 255f;
            Interp += Addition;

            if (Interp < 0f)
                Interp = 0f;
            else if (Interp > 1f)
                Interp = 1f;
            
            TestChunk[CX][CY].Interpolation[X][Y] = (byte)(Interp * 255f);      
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            #region OLD
            //view = Matrix.Identity *
            //Matrix.CreateTranslation(-CameraManager.Cams[0].P.X, CameraManager.Cams[0].P.Y, 0) *
            //Matrix.CreateRotationZ(CameraManager.Cams[0].R) *
            //Matrix.CreateTranslation(CameraManager.Cams[0].Origin.X, CameraManager.Cams[0].Origin.Y, 0) *
            //Matrix.CreateScale(CameraManager.Cams[0].Z);
            //view = CameraManager.Cams[0].Transform;

            //basicEffect.World = Matrix.Identity;
            //basicEffect.View = view;
            //basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, (float)WorldVariables.WindowWidth, (float)WorldVariables.WindowHeight, 0, 1, 0);

            //basicEffect.VertexColorEnabled = true;

            //graphicsDevice.SetVertexBuffer(vertexBuffer);
            //graphicsDevice.Indices = indexBuffer;

            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //graphicsDevice.RasterizerState = rasterizerState;

            //foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, 12, 0, 10);
            //}
            #endregion
            //test.Draw(spriteBatch, graphicsDevice);

            Vector2 CameraPosition = CameraManager.Cams[0].P;
            Vector2 MousePosition = new Vector2(InputManager.M.X, InputManager.M.Y);


            Vector2 SelectedChunk = CameraPosition / (QuadSize * ChunkSize);
            SelectedChunk.X = (float)Math.Floor(SelectedChunk.X);
            SelectedChunk.Y = (float)Math.Floor(SelectedChunk.Y);


            Vector2 ChunkPosition = (CameraPosition / QuadSize) - (SelectedChunk * ChunkSize);
            ChunkPosition.X = (float)Math.Round(ChunkPosition.X);
            ChunkPosition.Y = (float)Math.Round(ChunkPosition.Y);
            
            Vector2 Chunk = Vector2.Zero;

            spriteBatch.Draw(StaticTests.Marker, CameraPosition, null, Color.LightBlue, 0f,
                new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(2f, 0.1f), SpriteEffects.None, 1f);
            spriteBatch.Draw(StaticTests.Marker, CameraPosition, null, Color.LightBlue, 0f,
                new Vector2(StaticTests.Marker.Width / 2, StaticTests.Marker.Height / 2), new Vector2(0.1f, 2f), SpriteEffects.None, 1f);

            if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, -1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, -0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, -0.1f);

                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 2, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 2, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 3, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 3, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 4, (int)ChunkPosition.Y, -0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 4, (int)ChunkPosition.Y, -0.1f);

                Vector2 Edge = SetEdges((int)SelectedChunk.X, (int)SelectedChunk.Y);

                if (Edge.X != 0 && Edge.Y != 0)
                {
                    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);
                    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);
                    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);
                }
                else
                {
                    if (Edge.X != 0)
                        TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);

                    if (Edge.Y != 0)
                        TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);
                }

                TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);


            }
            else if (InputManager.KB.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, 1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y + 1, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y - 1, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y + 1, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y - 1, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y + 1, 0.1f);
                //EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y - 1, 0.1f);

                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 1, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 1, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 2, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 2, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 3, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 3, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X + 4, (int)ChunkPosition.Y, 0.1f);
                EditPoint((int)SelectedChunk.X, (int)SelectedChunk.Y, (int)ChunkPosition.X - 4, (int)ChunkPosition.Y, 0.1f);

                Vector2 Edge = SetEdges((int)SelectedChunk.X, (int)SelectedChunk.Y);

                if (Edge.X != 0 && Edge.Y != 0)
                {
                    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);
                    TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);
                    TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);
                }
                else
                {
                    if (Edge.X != 0)
                        TestChunk[(int)SelectedChunk.X + (int)Edge.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * (SelectedChunk.X + (int)Edge.X), QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);

                    if (Edge.Y != 0)
                        TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y + (int)Edge.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * (SelectedChunk.Y + (int)Edge.Y)), QuadSize, ChunkSize);
                }

                TestChunk[(int)SelectedChunk.X][(int)SelectedChunk.Y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * SelectedChunk.X, QuadSize * ChunkSize * SelectedChunk.Y), QuadSize, ChunkSize);

            }
            
            for (int x = 0; x < TestChunk.Length; x++)
                for (int y = 0; y < TestChunk[x].Length; y++)
                {
                    //if (x == 0 && y == 0)
                    //    TestChunk[x][y].CalculateChunk(graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y), QuadSize, ChunkSize);

                    if (TestChunk[x][y] != null)
                    TestChunk[x][y].Draw(spriteBatch, graphics, new Vector2(QuadSize * ChunkSize * x, QuadSize * ChunkSize * y), QuadSize);
                }

            
            Console.WriteLine(MousePosition);
            Console.WriteLine(ChunkPosition);
            Console.WriteLine(SelectedChunk);
            Console.WriteLine(sizeof(bool));
        }
    }
}
