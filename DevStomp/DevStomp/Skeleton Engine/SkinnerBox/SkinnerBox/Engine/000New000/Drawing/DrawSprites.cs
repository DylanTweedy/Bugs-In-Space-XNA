using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    static class DrawSprites
    {
        static SpriteBatch spriteBatch;

        static public void Initialize()
        {
            spriteBatch = GlobalVariables.spriteBatch;
        }

        static public void Begin(Camera camera)
        {
            if (camera != null)
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);
            else
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GraphicsManager.ScreenMatrix);
        }

        static public void DrawTranslated(Camera camera, Texture2D tex, SpacePosition position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects)
        {
            GlobalVariables.TempSP = SpacePosition.CameraTransform(camera.Position, position);
            GlobalVariables.spriteBatch.Draw(tex, GlobalVariables.TempSP.RoughPosition, null, color, rotation, origin, scale, spriteEffects, 0f);
        }

        static public void End()
        {
            spriteBatch.End();
        }
    }
}
