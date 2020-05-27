using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    public enum LocationEnum
    {
        Left,
        Top,
        Right,
        Bottom,
        None
    }

    public class Location
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;

        public Location(float Size)
        {
            Left = Size;
            Top = Size;
            Right = Size;
            Bottom = Size;
        }

        public Location(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public LocationEnum GetMax()
        {
            LocationEnum Max = LocationEnum.None;
            float MaxNum = float.MinValue;

            if (Left > MaxNum)
            {
                Max = LocationEnum.Left;
                MaxNum = Left;
            }
            if (Top > MaxNum)
            {
                Max = LocationEnum.Top;
                MaxNum = Top;
            }
            if (Right > MaxNum)
            {
                Max = LocationEnum.Right;
                MaxNum = Right;
            }
            if (Bottom > MaxNum)
            {
                Max = LocationEnum.Bottom;
                MaxNum = Bottom;
            }

            return Max;
        }

        public LocationEnum GetMin()
        {
            LocationEnum Min = LocationEnum.None;
            float MinNum = float.MaxValue;

            if (Left < MinNum)
            {
                Min = LocationEnum.Left;
                MinNum = Left;
            }
            if (Top < MinNum)
            {
                Min = LocationEnum.Top;
                MinNum = Top;
            }
            if (Right < MinNum)
            {
                Min = LocationEnum.Right;
                MinNum = Right;
            }
            if (Bottom < MinNum)
            {
                Min = LocationEnum.Bottom;
                MinNum = Bottom;
            }

            return Min;
        }

        public float GetMaxNum()
        {
            float MaxNum = Left;

            if (Top > MaxNum)
                MaxNum = Top;

            if (Right > MaxNum)
                MaxNum = Right;

            if (Bottom > MaxNum)
                MaxNum = Bottom;

            return MaxNum;
        }
    }

    class BorderStyle
    {
        public string Name;
        public Color Tint;
        public Location Thickness;

        public BorderStyle(string name, Color tint, Location thickness)
        {
            Name = name;
            Tint = tint;
            Thickness = thickness;
        }

        public void DrawBorders(SpriteBatch spriteBatch, Vector2 Position, Vector2 Scale, float Rotation, float Alpha)
        {
            SkeletonTexture Side = new SkeletonTexture("Core-BorderStyles", Name);
            SkeletonTexture Corner = new SkeletonTexture("Core-BorderStyles", Name + "Corner");

            if (Side.TextureMissing)
                Side = new SkeletonTexture("Core-BorderStyles", "Default");
            if (Corner.TextureMissing)
                Corner = new SkeletonTexture("Core-BorderStyles", "DefaultCorner");

            Tint *= Alpha;

            if (Thickness.Top > 0)
                Side.Draw(spriteBatch, Position - Vector2.Transform(new Vector2(0, Scale.Y + (Thickness.Top / 2f)), Matrix.CreateRotationZ(Rotation)), Tint, Rotation, new Vector2(Scale.X * 2f, Thickness.Top), SpriteEffects.None);
            if (Thickness.Bottom > 0)
                Side.Draw(spriteBatch, Position + Vector2.Transform(new Vector2(0, Scale.Y + (Thickness.Bottom / 2f)), Matrix.CreateRotationZ(Rotation)), Tint, Rotation, new Vector2(Scale.X * 2f, Thickness.Bottom), SpriteEffects.FlipVertically);
            if (Thickness.Left > 0)
                Side.Draw(spriteBatch, Position - Vector2.Transform(new Vector2(Scale.X + (Thickness.Left / 2f), 0), Matrix.CreateRotationZ(Rotation)), Tint, Rotation + MathHelper.PiOver2, new Vector2(Scale.Y * 2f, Thickness.Left), SpriteEffects.FlipVertically);
            if (Thickness.Right > 0)
                Side.Draw(spriteBatch, Position + Vector2.Transform(new Vector2(Scale.X + (Thickness.Right / 2f), 0), Matrix.CreateRotationZ(Rotation)), Tint, Rotation + MathHelper.PiOver2, new Vector2(Scale.Y * 2f, Thickness.Right), SpriteEffects.None);


            if (Thickness.Left > 0 && Thickness.Top > 0)
                Corner.Draw(spriteBatch, Position - Vector2.Transform(new Vector2(Scale.X + (Thickness.Left / 2f), Scale.Y + (Thickness.Top / 2f)), Matrix.CreateRotationZ(Rotation)), Tint, Rotation, new Vector2(Thickness.Left, Thickness.Top), SpriteEffects.FlipHorizontally);
            if (Thickness.Right > 0 && Thickness.Top > 0)
                Corner.Draw(spriteBatch, Position + Vector2.Transform(new Vector2(Scale.X + (Thickness.Right / 2f), -Scale.Y - (Thickness.Top / 2f)), Matrix.CreateRotationZ(Rotation)), Tint, Rotation, new Vector2(Thickness.Right, Thickness.Top), SpriteEffects.None);
            if (Thickness.Right > 0 && Thickness.Bottom > 0)
                Corner.Draw(spriteBatch, Position - Vector2.Transform(new Vector2(-Scale.X - (Thickness.Right / 2f), -Scale.Y - (Thickness.Bottom / 2f)), Matrix.CreateRotationZ(Rotation)), Tint, Rotation + MathHelper.PiOver2, new Vector2(Thickness.Bottom, Thickness.Right), SpriteEffects.None);
            if (Thickness.Left > 0 && Thickness.Bottom > 0)
                Corner.Draw(spriteBatch, Position + Vector2.Transform(new Vector2(-Scale.X - (Thickness.Left / 2f), Scale.Y + (Thickness.Bottom / 2f)), Matrix.CreateRotationZ(Rotation)), Tint, Rotation + MathHelper.PiOver2, new Vector2(Thickness.Bottom, Thickness.Left), SpriteEffects.FlipVertically);
        }
    }
}
