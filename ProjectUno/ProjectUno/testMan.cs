using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUno
{
    class testMan
    {
        Rectangle rect;
        Texture2D texture;

        Tile current, target;
        PathFinder pathFinder;
        bool inMovement;

        public testMan(Rectangle _rect, Texture2D _texture, Tile[,] map, Tile _target)
        {
            rect = _rect;
            texture = _texture;

            current = getCurrentTile(map);
            target = _target;
            pathFinder = new PathFinder(current, target, map);
            inMovement = false;
        }

        public void Update(Tile[,] map)
        {
            current = getCurrentTile(map);
            if (!inMovement && (current.rect.Location != pathFinder.route.Peek().rect.Location))
            {
                inMovement = true;
                pathFinder = new PathFinder(current, target, map);
            }

            if(inMovement)
            {
                rect.Location = moveTowardsTarget();
                if(current.rect.Location == pathFinder.route.Peek().rect.Location)
                {
                    if(pathFinder.route.Count == 0)
                    {
                        inMovement = false;
                    }
                    else
                    {
                        pathFinder.route.Pop();
                    }                    
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        private Tile getCurrentTile(Tile[,] map)
        {
            Tile _tile = map[0,0];
            foreach(Tile t in map)
            {
                if(t.rect.Contains(rect.X, rect.Y))
                {
                    _tile = t;
                }
            }
            return _tile;
        }

        private Point moveTowardsTarget()
        {
            int newX = rect.X;
            int newY = rect.Y;
            if(pathFinder.route.Peek().indexX == current.indexX)
            {
                if (pathFinder.route.Peek().indexY > current.indexY)
                {
                    newY += 1;
                }
                else
                {
                    newY -= 1;
                }
            }
            else if (pathFinder.route.Peek().indexY == current.indexY)
            {
                if (pathFinder.route.Peek().indexX > current.indexX)
                {
                    newX += 1;
                }
                else
                {
                    newX -= 1;
                }
            }
            return new Point(newX, newY);        }
    }
}
