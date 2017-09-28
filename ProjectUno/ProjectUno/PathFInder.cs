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
            Stack<Tile> routeStack = new Stack<Tile>();

            routeStack.Push(map[3, 4]);
            routeStack.Push(map[2, 4]);
            routeStack.Push(map[1, 4]);
            routeStack.Push(map[1, 3]);
            routeStack.Push(map[0, 3]);
            routeStack.Push(map[0, 2]);
            routeStack.Push(map[0, 1]);
            
            return routeStack;
        }
    }
}


