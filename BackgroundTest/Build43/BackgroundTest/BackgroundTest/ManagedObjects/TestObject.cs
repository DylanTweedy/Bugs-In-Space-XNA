using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BackgroundTest
{
    class TestObject
    {
        List<Sprite> Objects;
        Texture2D tex;
        int objectNumber;

        public Texture2D Texture
        {
            get { return tex; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X - (tex.Width / 2), (int)Position.Y - (tex.Height / 2), tex.Width, tex.Height); }
        }

        public Vector2 Position
        {
            get { return Objects[0].Position; }
            set
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    Objects[i].Position = value;
                }
            }
        }

        public string CurrentAnimation
        {
            get { return Objects[0].CurrentAnimation; }
        }

        public float Scale
        {
            get { return Objects[0].Scale; }
            set
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    Objects[i].Scale = value;
                }
            }
        }

        public float Rotation
        {
            get { return Objects[0].Rotation; }
            set
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    Objects[i].Rotation = value;
                }
            }
        }

        public bool AutoRotate
        {
            get { return Objects[0].AutoRotate; }
            set
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    Objects[i].AutoRotate = value;
                }
            }
        }

        public Vector2 CenterOffset
        {
            get { return Objects[0].originalOffset; }
            set
            {
                for (int i = 0; i < Objects.Count; i++)
                {
                    Objects[i].originalOffset = value;
                }
            }
        }

        public TestObject(Texture2D texture)
        {
            Objects = new List<Sprite>();
            tex = texture;
        }

        public void AddAnimation(int ObjectNumber, string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, string NextAnimation)
        {
            Objects[ObjectNumber].AddAnimation(Name, X, Y, Width, Height, Frames, FrameLength, NextAnimation);
        }

        public void AddIdleAnimation(int ObjectNumber, string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, float MinTime, float MaxTime)
        {
            Objects[ObjectNumber].AddIdleAnimation(Name, X, Y, Width, Height, Frames, FrameLength, MinTime, MaxTime);
        }

        public void AddMAnimation(int ObjectNumber, string Name, Vector2[] FrameVectors, float FrameLength, string NextAnimation)
        {
            Objects[ObjectNumber].AddMAnimation(Name, FrameLength, NextAnimation);            
        }

        public void AddMAnimation(int ObjectNumber, string Name, List<Vector2> VectorList, float FrameLength, string NextAnimation)
        {
            Objects[ObjectNumber].AddMAnimation(Name, VectorList, FrameLength, NextAnimation);
        }

        public void AddMAnimationVector(int ObjectNumber, string Name, int x, int y)
        {
            Objects[ObjectNumber].AddMAninationVector(Name, x, y);
        }

        public List<Vector2> ReturnMAnimattionList(int ObjectNumber, string Name)
        {
            return Objects[ObjectNumber].movementAnimations[Name].frameVector;
        }

        public void ObjectColour(int ObjectNumber, Color ObjectColour)
        {
            Objects[ObjectNumber].Tint = ObjectColour;
        }



        public void AddAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, string NextAnimation)
        {
            Objects[objectNumber].AddAnimation(Name, X, Y, Width, Height, Frames, FrameLength, NextAnimation);
        }

        public void AddIdleAnimation(string Name, int X, int Y, int Width, int Height, int Frames, float FrameLength, float MinTime, float MaxTime)
        {
            Objects[objectNumber].AddIdleAnimation(Name, X, Y, Width, Height, Frames, FrameLength, MinTime, MaxTime);
        }

        public void AddMAnimation(string Name, float FrameLength, string NextAnimation)
        {
            Objects[objectNumber].AddMAnimation(Name, FrameLength, NextAnimation);
        }

        public void AddMAnimation(string Name, List<Vector2> VectorList, float FrameLength, string NextAnimation)
        {
            Objects[objectNumber].AddMAnimation(Name, VectorList, FrameLength, NextAnimation);
        }

        public void AddMAnimationVector(string Name, int x, int y)
        {
            Objects[objectNumber].AddMAninationVector(Name, x, y);
        }

        public List<Vector2> ReturnMAnimattionList(string Name)
        {
            return Objects[objectNumber].movementAnimations[Name].frameVector;
        }


        public void ObjectColour(Color ObjectColour)
        {
            Objects[objectNumber].Tint = ObjectColour;
        }


        public void AddTexturedObject(Texture2D texture, float OffsetX, float OffsetY)
        {
            Objects.Add(new Sprite(texture));
            Objects[Objects.Count - 1].originalOffset = new Vector2(OffsetX, OffsetY);
        }

        public void AddObject(float OffsetX, float OffsetY)
        {
            Objects.Add(new Sprite(tex));
            Objects[Objects.Count - 1].originalOffset = new Vector2(OffsetX, OffsetY);
            objectNumber = Objects.Count - 1;
        }

        public void ChangeAnimation(int ObjectNumber, string AnimationName)
        {
            Objects[ObjectNumber].CurrentAnimation = AnimationName;
        }

        public void ChangeMovementAnimation(int ObjectNumber, string AnimationName)
        {
            Objects[ObjectNumber].MovementAnimation = AnimationName;
        }

        public void ObjectEffect(SpriteEffects State)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].SpriteEffect = State;
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Animate(gameTime);
            }
        }

        public void AnimationSpeed(float SpeedModifier)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].AnimationSpeed(SpeedModifier);
            }
        }

        public void MoveBy(float x, float y)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].MoveBy(x, y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Draw(spriteBatch);
            }
        }




    }
}
