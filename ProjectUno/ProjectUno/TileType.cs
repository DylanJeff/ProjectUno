using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUno
{
    class TileType
    {
        public Texture2D texture;
        public bool walkable;
        public int discount;
        public int speed;

        /// <summary>
        /// TileType tooltip
        /// </summary>
        /// <param name="_texture">Texture of the tile</param>
        /// <param name="_walkable">If the tile can be walked on or not</param>
        /// <param name="_discount">The % by which the total path cost gets reduced</param>
        /// <param name="_speed">The amount of pixels an object will move every second whilst on this tile</param>
        public TileType(Texture2D _texture, bool _walkable, int _discount, int _speed)
        {
            texture = _texture;
            walkable = _walkable;
            discount = _discount;
            speed = _speed;
        }
    }
}
