using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUno
{
    class Tile
    {
        public Rectangle rect;
        Texture2D texture;


        //PATHFINDING
        public int indexX, indexY;
        public int costA, costB, costT, costDiscount, speed;
        public bool isStart, isTarget, walkable;
        public Tile rootTile;

        public Tile(Rectangle _rect, Texture2D _texture, int _indexX, int _indexY)
        {
            rect = _rect;
            texture = _texture;
            indexX = _indexX;
            indexY = _indexY;
            costA = 0;
            costB = 0;
            costT = 0;
            costDiscount = 0;
            speed = 1;
            isStart = false;
            isTarget = false;
            walkable = true;
            rootTile = this;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        public void setType(TileType type)
        {
            texture = type.texture;
            walkable = type.walkable;
            costDiscount = type.discount;
            speed = type.speed;
        }

        public bool checkClicked(MouseState msNow, MouseState msPrev, Viewport view1)
        {
            if(rect.Contains((msNow.X-view1.X), (msNow.Y-view1.Y)) && msPrev.LeftButton == ButtonState.Pressed && msNow.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void resetPathFindingVariables()
        {
            costA = 0;
            costB = 0;
            costT = 0;
            isStart = false;
            isTarget = false;
            rootTile = this;
        }
    }
}
