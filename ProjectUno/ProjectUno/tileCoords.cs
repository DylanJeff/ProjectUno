using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUno
{
    class tileCoords
    {
        public bool isStart, isTarget, beenUsed;
        public int indexX, indexY;
        public tileCoords(int _x, int _y)
        {
            indexX = _x;
            indexY = _y;
            isStart = false;
            isTarget = false;
            beenUsed = false;
        }
    }
}
