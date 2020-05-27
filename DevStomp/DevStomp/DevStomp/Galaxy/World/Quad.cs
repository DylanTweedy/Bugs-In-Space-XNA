using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevStomp
{
    class Quad
    {
        public VertexPositionColorTexture[] vertices;
        public short[] indices;

        public void Initialize(Vector2 Position, int QuadSize, byte Shape, float[] Interp, byte[] ID)
        {
            //QuadSize *= (int)(WorldVariables.RandomNumber.NextDouble() * 2);

            //Color color = ColorManager.GetShades(Color.Green, 0.85f)[WorldVariables.RandomNumber.Next(0, 5)];

            Color[] colors = new Color[ID.Length];

            for (int i = 0; i < ID.Length; i++)
            {
                switch (ID[i])
                {
                    case 1:
                        colors[i] = Color.Gray;
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
                    vertices[1] = new VertexPositionColorTexture(new Vector3(Position.X, Position.Y + QuadSize, 0), colors[3], new Vector2(0, 1));
                    vertices[3] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y, 0), colors[1], new Vector2(1, 0));
                    vertices[2] = new VertexPositionColorTexture(new Vector3(Position.X + QuadSize, Position.Y + QuadSize, 0), colors[2], new Vector2(1, 1));

                    indices = new short[6];

                    indices[0] = 0; indices[1] = 1; indices[2] = 3;
                    indices[3] = 1; indices[4] = 3; indices[5] = 2;
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

        public void Clear()
        {
            vertices = null;
            indices = null;
        }
    }
}
