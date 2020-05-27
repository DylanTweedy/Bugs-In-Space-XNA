using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SkeletonEngine
{
    static class EffectsManager
    {
        static public Effect LightEffect;
        static public Effect LightCombinedEffect;

        static public void LoadContent(ContentManager Content)
        {
            LightEffect = Content.Load<Effect>("Effects//MultiTarget");
            LightCombinedEffect = Content.Load<Effect>("Effects//DeferredCombined");
        }
    }
}
