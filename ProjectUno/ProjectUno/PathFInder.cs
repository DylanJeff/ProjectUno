using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUno
{
    class PathFinder
    {
        int startX, startY, targetX, targetY, mapXLength, mapYLength;
        public Stack<Tile> route;

        public PathFinder(Tile _start, Tile _target, Tile[,] map)
        {
            startX = _start.indexX;
            startY = _start.indexY;
            targetX = _target.indexX;
            targetY = _target.indexY;
            mapXLength = map.GetLength(0);
            mapYLength = map.GetLength(1);
            route = findRoute(map);
        }

        private Stack<Tile> findRoute(Tile[,] map)
        {
            Tile currentTile;
            Stack<Tile> routeStack = new Stack<Tile>();
            Queue<Tile> costQueue = new Queue<Tile>();
            List<Tile> rootingList = new List<Tile>();
            List<Tile> checkedList = new List<Tile>();


            //ASSIGN START VALUES
            map[startX, startY].costA = 0;
            map[startX, startY].isStart = true;

            //ASSIGN TARGET VALUES
            map[targetX, targetY].costA = 0;
            map[targetX, targetY].isTarget = true;

            //ASSIGN COST A VALUES TO THE TILES
            costQueue.Enqueue(map[startX, startY]);
            currentTile = costQueue.Peek();
            while(!currentTile.isTarget)
            {
                currentTile = costQueue.Peek();
                costQueue.Dequeue();
                neswCostA(ref map, ref costQueue, currentTile, 0, -1);//NORTH
                neswCostA(ref map, ref costQueue, currentTile, 1, 0);//EAST
                neswCostA(ref map, ref costQueue, currentTile, 0, 1);//SOUTH
                neswCostA(ref map, ref costQueue, currentTile, -1, 0);//WEST

                neswCostA(ref map, ref costQueue, currentTile, 1, -1);//NORTH-EAST
                neswCostA(ref map, ref costQueue, currentTile, 1, 1);//SOUTH-EAST
                neswCostA(ref map, ref costQueue, currentTile, -1, 1);//SOUTH-WEST
                neswCostA(ref map, ref costQueue, currentTile, -1, -1);//NORTH-WEST
            }

            //ASSIGN COST B VALUES TO THE TILES
            costQueue = new Queue<Tile>();
            costQueue.Enqueue(map[targetX, targetY]);
            currentTile = costQueue.Peek();
            while (!currentTile.isStart)
            {
                currentTile = costQueue.Peek();
                costQueue.Dequeue();
                neswCostB(ref map, ref costQueue, currentTile, 0, -1);
                neswCostB(ref map, ref costQueue, currentTile, 1, 0);
                neswCostB(ref map, ref costQueue, currentTile, 0, 1);
                neswCostB(ref map, ref costQueue, currentTile, -1, 0);

                neswCostB(ref map, ref costQueue, currentTile, 1, -1);//NORTH-EAST
                neswCostB(ref map, ref costQueue, currentTile, 1, 1);//SOUTH-EAST
                neswCostB(ref map, ref costQueue, currentTile, -1, 1);//SOUTH-WEST
                neswCostB(ref map, ref costQueue, currentTile, -1, -1);//NORTH-WEST
            }

            //ASSIGN COST T VALUES TO THE TILES
            foreach(Tile t in map)
            {
                t.costT = t.costA + t.costB;
            }

            //ASSIGN ROOTS TO THE TILES
            rootingList.Add(map[startX, startY]);
            currentTile = tileLowest(rootingList);
            while (!currentTile.isTarget)
            {
                currentTile = tileLowest(rootingList);
                rootingList.Remove(currentTile);
                checkedList.Add(currentTile);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, 0, -1);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, 1, 0);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, 0, 1);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, -1, 0);

                neswRoot(ref map, ref rootingList, checkedList, currentTile, 1, -1);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, 1, 1);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, -1, 1);
                neswRoot(ref map, ref rootingList, checkedList, currentTile, -1, -1);
            }

            //PUT TOGETHER THE ROUTE
            currentTile = map[targetX, targetY];
            do
            {
                routeStack.Push(currentTile);
                currentTile = map[currentTile.indexX, currentTile.indexY].rootTile;
            }
            while (!map[currentTile.indexX, currentTile.indexY].isStart);

            //RESET TILE PATH FINDING VARIABLES
            foreach (Tile t in map)
            {
                t.resetPathFindingVariables();
            }

            //SEND THE ROUTE
            return routeStack;
        }

        private bool tileExists(int x, int y)
        {
            bool exists = false;
            if (x < mapXLength && x >= 0 && y < mapYLength && y >= 0)
            {
                exists = true;
            }
            return exists;
        }

        private Tile tileLowest(List<Tile> rootingList)
        {
            Tile tile = rootingList.First();
            foreach(Tile t in rootingList)
            {
                if (t.costT < tile.costT)
                {
                    tile = t;
                }
            }
            return tile;
        }

        private bool validationCostA(Tile tile)
        {
            if(tile.costA == 0 && !tile.isStart && tile.walkable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validationCostB(Tile tile)
        {
            if (tile.costB == 0 && !tile.isTarget && tile.walkable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validationRoot(Tile tile, List<Tile> checkedList, List<Tile> rootingList)
        {
            if (!checkedList.Contains(tile) && !rootingList.Contains(tile) && !tile.isStart && tile.walkable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void neswCostA(ref Tile[,] map, ref Queue<Tile> costQueue, Tile currentTile, int xChange, int yChange)
        {
            int addition = 0;
            if(xChange + yChange == -1 || xChange + yChange == 1)
            {
                addition = 10;
            }
            else
            {
                addition = 14;
            }
            if (tileExists(currentTile.indexX + xChange, currentTile.indexY + yChange))//IS THERE A TILE IN THE GIVEN DIRECTION
            {
                if (validationCostA(map[currentTile.indexX + xChange, currentTile.indexY + yChange]))//CAN THAT TILE BE USED
                {
                    map[currentTile.indexX + xChange, currentTile.indexY + yChange].costA = currentTile.costA + addition;
                    costQueue.Enqueue(map[currentTile.indexX + xChange, currentTile.indexY + yChange]);
                }
            }
        }

        private void neswCostB(ref Tile[,] map, ref Queue<Tile> costQueue, Tile currentTile, int xChange, int yChange)
        {
            int addition = 0;
            if (xChange + yChange == -1 || xChange + yChange == 1)
            {
                addition = 10;
            }
            else
            {
                addition = 14;
            }
            if (tileExists(currentTile.indexX + xChange, currentTile.indexY + yChange))//IS THERE A TILE IN THE GIVEN DIRECTION
            {
                if (validationCostB(map[currentTile.indexX + xChange, currentTile.indexY + yChange]))//CAN THAT TILE BE USED
                {
                    map[currentTile.indexX + xChange, currentTile.indexY + yChange].costB = currentTile.costB + addition;
                    costQueue.Enqueue(map[currentTile.indexX + xChange, currentTile.indexY + yChange]);
                }
            }
        }

        private void neswRoot(ref Tile[,] map, ref List<Tile> rootingList, List<Tile> checkedList, Tile currentTile, int xChange, int yChange)
        {
            if (tileExists(currentTile.indexX + xChange, currentTile.indexY + yChange))//IS THERE A TILE IN THE GIVEN DIRECTION
            {
                if (validationRoot(map[currentTile.indexX + xChange, currentTile.indexY + yChange], checkedList, rootingList))
                {
                    rootingList.Add(map[currentTile.indexX + xChange, currentTile.indexY + yChange]);
                    map[currentTile.indexX + xChange, currentTile.indexY + yChange].rootTile = currentTile;
                }
            }
        }
    }
}


