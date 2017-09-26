using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUno
{
    class PathFinder
    {
        Tile start, destination;
        public Stack<Tile> route;

        public PathFinder(Tile _start, Tile _destination, Tile[,] map)
        {
            start = _start;
            destination = _destination;
            route = findRoute(map);
        }

        private Stack<Tile> findRoute(Tile[,] map)
        {
            Queue<tileCoords> queueA = new Queue<tileCoords>();
            Queue<tileCoords> queueB = new Queue<tileCoords>();
            List<Tile> listLow = new List<Tile>();
            List<Tile> listChecked = new List<Tile>();
            Stack<Tile> stackFinal = new Stack<Tile>();

            Tile tileA = start;
            Tile tileB = destination;

            //START AND TARGET
            map[tileA.indexX, tileA.indexY].costA = 0;
            map[tileA.indexX, tileA.indexY].isStart = true;
            queueA.Enqueue(new tileCoords(tileA.indexX, tileA.indexY));

            map[tileB.indexX, tileB.indexY].costB = 0;
            map[tileB.indexX, tileB.indexY].isTarget = true;
            queueB.Enqueue(new tileCoords(tileB.indexX, tileB.indexY));

            //COSTS A

            tileCoords currentCoords = queueA.Peek();
            while (!map[currentCoords.indexX, currentCoords.indexY].isTarget)
            {
                currentCoords = queueA.Peek();
                queueA.Dequeue();
                if (positionExists(currentCoords.indexX, currentCoords.indexY - 1))//NORTH EXISTS
                {
                    if (map[currentCoords.indexX, currentCoords.indexY - 1].costA == 0 && !map[currentCoords.indexX, currentCoords.indexY - 1].isStart && map[currentCoords.indexX, currentCoords.indexY - 1].walkable)
                    {
                        map[currentCoords.indexX, currentCoords.indexY - 1].costA = map[currentCoords.indexX, currentCoords.indexY].costA + 10;
                        queueA.Enqueue(new tileCoords(currentCoords.indexX, currentCoords.indexY - 1));
                    }
                }

                if (positionExists(currentCoords.indexX + 1, currentCoords.indexY))//EAST EXISTS
                {
                    if (map[currentCoords.indexX + 1, currentCoords.indexY].costA == 0 && !map[currentCoords.indexX + 1, currentCoords.indexY].isStart && map[currentCoords.indexX + 1, currentCoords.indexY].walkable)
                    {
                        map[currentCoords.indexX + 1, currentCoords.indexY].costA = map[currentCoords.indexX, currentCoords.indexY].costA + 10;
                        queueA.Enqueue(new tileCoords(currentCoords.indexX + 1, currentCoords.indexY));
                    }

                }

                if (positionExists(currentCoords.indexX, currentCoords.indexY + 1))//SOUTH EXISTS
                {
                    if (map[currentCoords.indexX, currentCoords.indexY + 1].costA == 0 && !map[currentCoords.indexX, currentCoords.indexY + 1].isStart && map[currentCoords.indexX, currentCoords.indexY + 1].walkable)
                    {
                        map[currentCoords.indexX, currentCoords.indexY + 1].costA = map[currentCoords.indexX, currentCoords.indexY].costA + 10;
                        queueA.Enqueue(new tileCoords(currentCoords.indexX, currentCoords.indexY + 1));
                    }
                }

                if (positionExists(currentCoords.indexX - 1, currentCoords.indexY))//WEST EXISTS
                {
                    if (map[currentCoords.indexX - 1, currentCoords.indexY].costA == 0 && !map[currentCoords.indexX - 1, currentCoords.indexY].isStart && map[currentCoords.indexX - 1, currentCoords.indexY].walkable)
                    {
                        map[currentCoords.indexX - 1, currentCoords.indexY].costA = map[currentCoords.indexX, currentCoords.indexY].costA + 10;
                        queueA.Enqueue(new tileCoords(currentCoords.indexX - 1, currentCoords.indexY));
                    }
                }
            }



            //COSTS B

            currentCoords = queueB.Peek();
            while (!map[currentCoords.indexX, currentCoords.indexY].isStart)
            {
                currentCoords = queueB.Peek();
                queueB.Dequeue();
                if (positionExists(currentCoords.indexX, currentCoords.indexY - 1))//NORTH EXISTS
                {
                    if (map[currentCoords.indexX, currentCoords.indexY - 1].costB == 0 && !map[currentCoords.indexX, currentCoords.indexY - 1].isTarget && map[currentCoords.indexX, currentCoords.indexY - 1].walkable)
                    {
                        map[currentCoords.indexX, currentCoords.indexY - 1].costB = map[currentCoords.indexX, currentCoords.indexY].costB + 10;
                        queueB.Enqueue(new tileCoords(currentCoords.indexX, currentCoords.indexY - 1));
                    }
                }

                if (positionExists(currentCoords.indexX + 1, currentCoords.indexY))//EAST EXISTS
                {
                    if (map[currentCoords.indexX + 1, currentCoords.indexY].costB == 0 && !map[currentCoords.indexX + 1, currentCoords.indexY].isTarget && map[currentCoords.indexX + 1, currentCoords.indexY].walkable)
                    {
                        map[currentCoords.indexX + 1, currentCoords.indexY].costB = map[currentCoords.indexX, currentCoords.indexY].costB + 10;
                        queueB.Enqueue(new tileCoords(currentCoords.indexX + 1, currentCoords.indexY));
                    }

                }

                if (positionExists(currentCoords.indexX, currentCoords.indexY + 1))//SOUTH EXISTS
                {
                    if (map[currentCoords.indexX, currentCoords.indexY + 1].costB == 0 && !map[currentCoords.indexX, currentCoords.indexY + 1].isTarget && map[currentCoords.indexX, currentCoords.indexY + 1].walkable)
                    {
                        map[currentCoords.indexX, currentCoords.indexY + 1].costB = map[currentCoords.indexX, currentCoords.indexY].costB + 10;
                        queueB.Enqueue(new tileCoords(currentCoords.indexX, currentCoords.indexY + 1));
                    }
                }

                if (positionExists(currentCoords.indexX - 1, currentCoords.indexY))//WEST EXISTS
                {
                    if (map[currentCoords.indexX - 1, currentCoords.indexY].costB == 0 && !map[currentCoords.indexX - 1, currentCoords.indexY].isTarget && map[currentCoords.indexX - 1, currentCoords.indexY].walkable)
                    {
                        map[currentCoords.indexX - 1, currentCoords.indexY].costB = map[currentCoords.indexX, currentCoords.indexY].costB + 10;
                        queueB.Enqueue(new tileCoords(currentCoords.indexX - 1, currentCoords.indexY));
                    }
                }
            }

            //COSTS TOTAL

            foreach (Tile t in map)
            {
                t.costT = t.costA + t.costB;
            }

            listLow.Add(map[tileA.indexX, tileA.indexY]);
            Tile currentTile = getLowestCoords(listLow, map);
            while (!currentTile.isTarget)
            {
                currentTile = getLowestCoords(listLow, map);
                listLow.Remove(currentTile);
                listChecked.Add(currentTile);

                if (positionExists(currentTile.indexX, currentTile.indexY - 1))//NORTH EXISTS
                {
                    if (!listChecked.Contains(map[currentTile.indexX, currentTile.indexY - 1]) && !listLow.Contains(map[currentTile.indexX, currentTile.indexY - 1]) && !map[currentTile.indexX, currentTile.indexY - 1].isStart && map[currentTile.indexX, currentTile.indexY - 1].walkable)
                    {
                        listLow.Add(map[currentTile.indexX, currentTile.indexY - 1]);
                        map[currentTile.indexX, currentTile.indexY - 1].rootTile = currentTile;
                    }
                }

                if (positionExists(currentTile.indexX + 1, currentTile.indexY))//EAST EXISTS
                {
                    if (!listChecked.Contains(map[currentTile.indexX + 1, currentTile.indexY]) && !listLow.Contains(map[currentTile.indexX + 1, currentTile.indexY]) && !map[currentTile.indexX + 1, currentTile.indexY].isStart && map[currentTile.indexX + 1, currentTile.indexY].walkable)
                    {
                        listLow.Add(map[currentTile.indexX + 1, currentTile.indexY]);
                        map[currentTile.indexX + 1, currentTile.indexY].rootTile = currentTile;
                    }

                }

                if (positionExists(currentTile.indexX, currentTile.indexY + 1))//SOUTH EXISTS
                {
                    if (!listChecked.Contains(map[currentTile.indexX, currentTile.indexY + 1]) && !listLow.Contains(map[currentTile.indexX, currentTile.indexY + 1]) && !map[currentTile.indexX, currentTile.indexY + 1].isStart && map[currentTile.indexX, currentTile.indexY + 1].walkable)
                    {
                        listLow.Add(map[currentTile.indexX, currentTile.indexY + 1]);
                        map[currentTile.indexX, currentTile.indexY + 1].rootTile = currentTile;
                    }
                }

                if (positionExists(currentTile.indexX - 1, currentTile.indexY))//WEST EXISTS
                {
                    if (!listChecked.Contains(map[currentTile.indexX - 1, currentTile.indexY]) && !listLow.Contains(map[currentTile.indexX - 1, currentTile.indexY]) && !map[currentTile.indexX - 1, currentTile.indexY].isStart && map[currentTile.indexX - 1, currentTile.indexY].walkable)
                    {
                        listLow.Add(map[currentTile.indexX - 1, currentTile.indexY]);
                        map[currentTile.indexX - 1, currentTile.indexY].rootTile = currentTile;
                    }
                }
            }

            currentTile = map[tileB.indexX, tileB.indexY];
            do
            {
                stackFinal.Push(currentTile);
                currentTile = map[currentTile.indexX, currentTile.indexY].rootTile;
            }
            while (!map[currentTile.indexX, currentTile.indexY].isStart);
            return stackFinal;
        }

        static bool positionExists(int x, int y)
        {
            bool exists = false;
            if (x < 16 && x >= 0 && y < 9 && y >= 0)
            {
                exists = true;
            }
            return exists;
        }

        static Tile getLowestCoords(List<Tile> listLow, Tile[,] array)
        {
            Tile tile = listLow.First();

            foreach (Tile t in listLow)
            {
                if (t.costT < tile.costT)
                {
                    tile = t;
                }
            }
            return tile;
        }

    }
}
