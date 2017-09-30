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

        public testMan(Rectangle _rect, Texture2D _texture, Tile[,] map)
        {
            rect = _rect;
            texture = _texture;
            current = getCurrentTile(map);
            inMovement = false;
        }

        public void Update(Tile[,] map)
        {
            current = getCurrentTile(map);

            if(inMovement)
            {
                if (rect.Location == pathFinder.route.Peek().rect.Location)
                {
                    if (pathFinder.route.Count() == 1)
                    {
                        inMovement = false;
                    }
                    else
                    {
                        pathFinder.route.Pop();
                        if(!pathFinder.route.Peek().walkable)
                        {
                            inMovement = false;
                        }
                    }
                }
                else
                {
                    rect.Location = moveTowardsTarget();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        private Tile getCurrentTile(Tile[,] map)
        {
            return map[rect.X / 32, rect.Y / 32];
        }

        private Point moveTowardsTarget()
        {
            Rectangle targetRect = pathFinder.route.Peek().rect;
            int newX = 0;
            int newY = 0;
            int xDif = Math.Abs(rect.X - targetRect.X);
            int yDif = Math.Abs(rect.Y - targetRect.Y);
            if (rect.X > pathFinder.route.Peek().rect.X)
            {
                if(xDif % current.speed == 0)
                {
                    newX = -(current.speed);
                }
                else
                {
                    newX = -(xDif % current.speed);
                }   
            }
            else if (rect.X < pathFinder.route.Peek().rect.X)
            {
                if (xDif % current.speed == 0)
                {
                    newX = current.speed;
                }
                else
                {
                    newX = xDif % current.speed;
                }
            }
            if (rect.Y > pathFinder.route.Peek().rect.Y)
            {
                if (yDif % current.speed == 0)
                {
                    newY = -(current.speed);
                }
                else
                {
                    newY = -(yDif % current.speed);
                }
            }
            else if (rect.Y < pathFinder.route.Peek().rect.Y)
            {
                if (yDif % current.speed == 0)
                {
                    newY = current.speed;
                }
                else
                {
                    newY = yDif % current.speed;
                }
            }

            return new Point(rect.X + newX, rect.Y + newY);
        }

        public void setTarget(Tile _target, Tile[,] _map)
        {
            target = _target;
            inMovement = true;
            pathFinder = new PathFinder(current, target, _map);
        }
    }
}
