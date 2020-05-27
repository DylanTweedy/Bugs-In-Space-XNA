//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace BugsInSpace
//{
//    class MobileSprite
//    {
//        //Variables

//        SpriteAnimation sprite;
//        Queue<Vector2> queuePath = new Queue<Vector2>();
//        Vector2 target;
//        float speed = 1f;
//        int collisionBufferX = 0;
//        int collisionBufferY = 0;
//        bool active = true;
//        bool bMovingTowardsTarget = true;
//        bool bPathing = true;
//        bool bLoopPath = true;
//        bool bCollidable = true;
//        bool bVisible = true;
//        bool bDeactivateAtEndOfPath = false;
//        bool bHideAtEndOfPath = false;
//        string sEndPathAnimation = null;
//        public bool locked;
//        public int health;

//        //Properties

//        public SpriteAnimation Sprite
//        {
//            get { return sprite; }
//        }

//        public Vector2 Position
//        {
//            get { return sprite.Position; }
//            set { sprite.Position = value; }
//        }

//        public Vector2 Target
//        {
//            get { return target; }
//            set { target = value; }
//        }

//        public int HorizontalCollisionBuffer
//        {
//            get { return collisionBufferX; }
//            set { collisionBufferX = value; }
//        }

//        public int VerticalCollisionBuffer
//        {
//            get { return collisionBufferY; }
//            set { collisionBufferY = value; }
//        }

//        public int Health
//        {
//            get { return health; }
//            set { health = value; }
//        }

//        public bool IsPathing
//        {
//            get { return bPathing; }
//            set { bPathing = value; }
//        }

//        public bool DeactivateAfterPathing
//        {
//            get { return bDeactivateAtEndOfPath; }
//            set { bDeactivateAtEndOfPath = value; }
//        }

//        public bool LoopPath
//        {
//            get { return bLoopPath; }
//            set { bLoopPath = value; }
//        }

//        public string EndPathAnimation
//        {
//            get { return sEndPathAnimation; }
//            set { sEndPathAnimation = value; }
//        }

//        public bool HideAtEndOfPath
//        {
//            get { return bHideAtEndOfPath; }
//            set { bHideAtEndOfPath = value; }
//        }

//        public bool IsVisible
//        {
//            get { return bVisible; }
//            set { bVisible = value; }
//        }

//        public float Speed
//        {
//            get { return speed; }
//            set { speed = value; }
//        }

//        public bool IsActive
//        {
//            get { return active; }
//            set { active = value; }
//        }

//        public bool IsMoving
//        {
//            get { return bMovingTowardsTarget; }
//            set { bMovingTowardsTarget = value; }
//        }

//        public bool IsCollidable
//        {
//            get { return bCollidable; }
//            set { bCollidable = value; }
//        }

//        public bool IsLocked
//        {
//            get { return locked; }
//            set { locked = value; }
//        }

//        public Rectangle BoundingBox
//        {
//            get { return sprite.BoundingBox; }
//        }

//        public Rectangle CollisionBox
//        {
//            get
//            {
//                return new Rectangle(sprite.BoundingBox.X + collisionBufferX, sprite.BoundingBox.Y + collisionBufferY, sprite.Width - (2 * collisionBufferX), sprite.Height - (2 * collisionBufferY));
//            }
//        }

//        //Constructors

//        public MobileSprite(Texture2D texture)
//        {
//            sprite = new SpriteAnimation(texture);
//        }

//        //Methods

//        public void AddPathNode(Vector2 node)
//        {
//            queuePath.Enqueue(node);
//        }

//        public void AddPathNode(int X, int Y)
//        {
//            queuePath.Enqueue(new Vector2(X, Y));
//        }

//        public void ClearPathNodes()
//        {
//            queuePath.Clear();
//        }

//        //Update

//        public void Update(GameTime gameTime)
//        {
//            if (active && bMovingTowardsTarget)
//            {
//                if (!(target == null))
//                {
//                    Vector2 Delta = new Vector2(target.X - sprite.X, target.Y - sprite.Y);

//                    if (Delta.Length() > Speed)
//                    {
//                        Delta.Normalize();
//                        Delta *= Speed;
//                        Position += Delta;
//                    }
//                    else
//                    {
//                        if (target == sprite.Position)
//                        {
//                            if (bPathing)
//                            {
//                                if (queuePath.Count > 0)
//                                {
//                                    target = queuePath.Dequeue();
//                                    if (bLoopPath)
//                                    {
//                                        queuePath.Enqueue(target);
//                                    }
//                                }
//                                else
//                                {
//                                    if (!(sEndPathAnimation == null))
//                                    {
//                                        if (!(Sprite.CurrentAnimation == sEndPathAnimation))
//                                        {
//                                            Sprite.CurrentAnimation = sEndPathAnimation;
//                                        }
//                                    }

//                                    if (bDeactivateAtEndOfPath)
//                                    {
//                                        IsActive = false;
//                                    }

//                                    if (bHideAtEndOfPath)
//                                    {
//                                        IsVisible = false;
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                            sprite.Position = target;
//                        }
//                    }
//                }
//            }

//            if (active)
//                sprite.Update(gameTime);
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            if (bVisible)
//            {
//                sprite.Draw(spriteBatch, 0, 0);
//            }
//        }
//    }
//}
