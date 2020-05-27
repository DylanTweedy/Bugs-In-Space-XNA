using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SkeletonEngine
{
    [Serializable()]
    public enum ListStyle
    {
        Number, Bullet, Check, Multi, Radio, None
    }

    [Serializable()]
    class ListData
    {
        ListStyle Style;


    }


}
