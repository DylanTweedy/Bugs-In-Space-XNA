using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevStomp
{
    class ChunkPosition
    {
        public int CX;
        public int CY;
        public int QX;
        public int QY;

        public ChunkPosition(int cx, int cy, int qx, int qy)
        {
            CX = cx;
            CY = cy;
            QX = qx;
            QY = qy;
        }
    }
}
