using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SkeletonEngine
{
    static class TestingClass
    {
        static Dungeon testDungeon;

        static public void Initialize()
        {
            testDungeon = new Dungeon();
        }
        
        static public void Update()
        {
            testDungeon.Update();

            DebugOptions.DebugTextLines.Add(new TextLine("" + GraphicsManager.ScreenScale));
            DebugOptions.DebugTextLines.Add(new TextLine("" + GraphicsManager.ViewportOffset));

        }

        static public void PreDraw()
        {
        }

        static public void Draw(Camera camera)
        {

            //if (InputManager.KBPressed(false, Microsoft.Xna.Framework.Input.Keys.LeftControl))
            if (DebugOptions.DebugActive)
                DrawSpaceChunks(camera);


            GeometryVisualizer test = new GeometryVisualizer();
            test.AddData(new SpacePosition(), 0f, 4, new Vector2(50f), Color.Blue);

            test.Draw(camera, FillMode.Solid);

            testDungeon.Draw(camera);

        }

        static public void DrawSpaceChunks(Camera camera)
        {
            Vector2 res = new Vector2(camera.FinalRender.Width, camera.FinalRender.Height);
            float zoom = camera.Zoom;

            float chunksX = 0f;
            float chunksY = 0f;

            if (zoom != 0f)
            {
                chunksX = (res.X / GlobalVariables.CameraChunkSize) / zoom;
                chunksY = (res.Y / GlobalVariables.CameraChunkSize) / zoom;
                
                float zoomLevel = 1f;

                while (chunksX > 10f)
                {
                    zoomLevel *= 5f;

                    chunksX /= 5f;
                    chunksY /= 5f;
                }



                Vector2 centerChunk = new Vector2((float)camera.Position.ChunkX, (float)camera.Position.ChunkY);

                TextBox test = new TextBox();



                DrawSprites.Begin(camera);

                for (int x = 0; x < chunksX + 2; x++)
                    for (int y = 0; y < chunksY + 2; y++)
                    {
                        SpacePosition pos = new SpacePosition();
                        string stringX = "";



                        pos.Position += new Vector2((GlobalVariables.CameraChunkSize / 2f) * (zoomLevel - 1f));

                        BigInteger posX = camera.Position.ChunkX;
                        if (posX < 0)
                            posX -= (BigInteger)zoomLevel;

                        pos.ChunkX += (posX / (BigInteger)zoomLevel) * (BigInteger)zoomLevel;
                        pos.ChunkX += (BigInteger)(Math.Round(x - ((chunksX + 1) / 2f)) * zoomLevel);
                       
                        BigInteger posY = camera.Position.ChunkY;
                        if (posY < 0)
                            posY -= (BigInteger)zoomLevel;

                        pos.ChunkY += (posY / (BigInteger)zoomLevel) * (BigInteger)zoomLevel;
                        pos.ChunkY += (BigInteger)(Math.Round(y - ((chunksY + 1) / 2f)) * zoomLevel);
                        

                        DebugOptions.Geometry.AddData(pos,
                            MathHelper.Pi, 4, new Vector2(GlobalVariables.CameraChunkSize * zoomLevel), Color.White);



                        test.Position = pos;
                        test.TextBoxSize = new Vector2(GlobalVariables.CameraChunkSize * zoomLevel);
                        test.Border = null;
                        test.Rotation = 0f;
                        test.Scale = 15000f * zoomLevel;
                        test.BackgroundColor = Color.Transparent;

                        test.Update(new List<TextLine>() { new TextLine( 
                        StringManager.FontSizeString(
                        StringManager.ColorString("X: " + pos.ChunkX + " Y: " + pos.ChunkY, Color.White), 10))
                        { Alignment = 0f }               
            
                        });
                        test.TextBoxSize = new Vector2(GlobalVariables.CameraChunkSize * zoomLevel * 0.975f);

                        test.Draw(camera);

                    }

                DrawSprites.End();

                DrawSprites.Begin(camera);
                DebugOptions.Geometry.Draw(camera, FillMode.WireFrame);
                DrawSprites.End();
            }
        }
    }
}
