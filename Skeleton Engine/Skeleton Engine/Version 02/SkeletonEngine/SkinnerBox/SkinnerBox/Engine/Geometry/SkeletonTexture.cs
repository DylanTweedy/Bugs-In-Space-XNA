using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public class SkeletonTexture
    {
        public string TextureLocation;
        public string TextureName;

        public Texture2D Texture
        {
            get { return SheetManager.GetRenderTexture(TextureLocation, TextureName); } 
        }

        public SkeletonTexture(string textureLocation, string textureName)
        {
            TextureLocation = textureLocation;
            TextureName = textureName;
        }

        public bool TextureMissing
        {
            get
            {
                if (SheetManager.TextureMissing(TextureLocation, TextureName))
                    return true;

                return false;
            }
        }

        #region Variants

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Color Tint, float Rotation, float Scale, SpriteEffects Flip, float Layer)
        {
            Draw(spriteBatch, Position, Vector2.One, Tint, Rotation, Vector2.One * Scale, Flip, Layer);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Vector2 DrawRectangle, Color Tint, float Rotation, float Scale, SpriteEffects Flip, float Layer)
        {
            Draw(spriteBatch, Position, DrawRectangle, Tint, Rotation, Vector2.One * Scale, Flip, Layer);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Color Tint, float Rotation, Vector2 Scale, SpriteEffects Flip, float Layer)
        {
            Draw(spriteBatch, Position, Vector2.One, Tint, Rotation, Scale, Flip, Layer);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Color Tint, float Rotation, float Scale, SpriteEffects Flip)
        {
            Draw(spriteBatch, Position, Vector2.One, Tint, Rotation, Vector2.One * Scale, Flip, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Vector2 DrawRectangle, Color Tint, float Rotation, float Scale, SpriteEffects Flip)
        {
            Draw(spriteBatch, Position, DrawRectangle, Tint, Rotation, Vector2.One * Scale, Flip, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Color Tint, float Rotation, Vector2 Scale, SpriteEffects Flip)
        {
            Draw(spriteBatch, Position, Vector2.One, Tint, Rotation, Scale, Flip, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Vector2 DrawRectangle, Color Tint, float Rotation, Vector2 Scale, SpriteEffects Flip)
        {
            Draw(spriteBatch, Position, DrawRectangle, Tint, Rotation, Scale, Flip, 0f);
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, Vector2 DrawRectangle, Color Tint, float Rotation, Vector2 Scale, SpriteEffects Flip, float Layer)
        {
            //Scale *= 5f;



            Texture2D Tex = SheetManager.GetRenderTexture(TextureLocation, TextureName);

            //if (!DebugOptions.DrawNormals || Tex != SheetManager.GetRenderTexture("QWERTY", "QWERTY"))
            //if (Tint != Color.White)
            {
                if (DebugOptions.DrawNormals)
                {
                    Tint = Color.White;

                    if (Tex == SheetManager.GetRenderTexture("Core", "MissingTexture"))
                        Tint = new Color(0, 0, 0, 0);
                }


                Vector2 TexSize = new Vector2(Tex.Width, Tex.Height);
                Vector2 Origin = TexSize / 2f;
                Rectangle Rect = new Rectangle(0, 0, (int)(TexSize.X * DrawRectangle.X), (int)(TexSize.Y * DrawRectangle.Y));

                float Seconds = (float)SheetManager.GetTextureTime(TextureLocation, TextureName).TotalSeconds;

                if (Seconds < 1f)
                {
                    Texture2D Tex2 = SheetManager.GetPreviousTexture(TextureLocation, TextureName);
                    Vector2 TexSize2 = new Vector2(Tex2.Width, Tex2.Height);
                    Vector2 Origin2 = TexSize2 / 2f;
                    Rectangle Rect2 = new Rectangle(0, 0, (int)(TexSize2.X * DrawRectangle.X), (int)(TexSize2.Y * DrawRectangle.Y));


                    spriteBatch.Draw(Tex2, Position, Rect2, Tint * (1 - Seconds), Rotation, Origin2, Scale / TexSize2, Flip, Layer);
                    spriteBatch.Draw(Tex, Position, Rect, Tint * Seconds, Rotation, Origin, Scale / TexSize, Flip, Layer);
                }
                else
                    spriteBatch.Draw(Tex, Position, Rect, Tint, Rotation, Origin, Scale / TexSize, Flip, Layer);
            }
            
        }
    }
}
