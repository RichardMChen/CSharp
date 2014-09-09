using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class TheWorld
    {
        static Random random = new Random();
        public const int SIZEX = 5;  // The number of columns in the grid world
        public const int SIZEY = 5;  // The number of rows in the grid world
        public int startX = 0, startY = 0, endX = 0, endY = 0;  // The x and y positions of the start and end
        int currentX = 0, currentY = 0;  // Holds the current X value and the current Y value
        bool[,] pathable = new bool[SIZEX, SIZEY];  // A 2-D array to know where the unpathable areas are
        bool[,] isOnOpenList = new bool[SIZEX, SIZEY];  // A 2-D array to know which tiles are on the open list
        bool[,] isOnPathList = new bool[SIZEX, SIZEY];  // A 2-D array to know which tiles are on the agent's path
        List<Tuple<int, int>> path = new List<Tuple<int,int>>();  // A list to hold the order the agent took the path
        List<Tuple<int, int>> openList = new List<Tuple<int, int>>();  // A list to hold the all the tiles not traversed yet
        int stuckCounter = 0;  // A counter to check if the agent can not go elsewhere

        /*
         * A struct for the parent's x and y value of the current node
         */
        public struct Parent
        {
            public int x;
            public int y;
        }
        public Parent[,] parentNode = new Parent[SIZEX,SIZEY];  // A 2-D array to hold the parent x and y values of a tile

        public TheWorld()
        {
            for (int i = 0; i < TheWorld.SIZEY; i++)
            {
                for (int j = 0; j < TheWorld.SIZEX; j++)
                {
                    pathable[j, i] = true;
                    isOnOpenList[j, i] = false;
                    isOnPathList[j, i] = false;
                }
            }
        }

        /*
         * A random number generator
         */
        public int randomGen()
        {
            int randNum = random.Next(0, 99);
            return randNum;
        }

        /*
         * A random number generator to determine which direction to take
         */
        public int pathDecider()
        {
            int randChoice = random.Next(0, 4);
            return randChoice;
        }

        /****************************************************
         * Generates unpathable tiles on the grid world
         ****************************************************/
        void unpathableGen()
        {
            //path.Add(Tuple.Create(0, 0));
            //path.Add(Tuple.Create(1, 3));
            int unpathable = 0;
            for (int i = 0; i < SIZEY; i++)
            {
                for (int j = 0; j < SIZEX; j++)
                {
                    unpathable = randomGen();
                    if (unpathable < 25)
                    {
                        pathable[j, i] = false;
                    }
                    if (startX == j && startY == i)
                    {
                        pathable[j, i] = true;
                    }
                    if (endX == j && endY == i)
                    {
                        pathable[j, i] = true;
                    }
                }
            }
        }

        /*
         * Checks the neighboring tiles of the current tile
         */
        void checkNeighbors(int posX, int posY)
        {
            // Check up
            if (posY - 1 >= 0 && pathable[posX, posY - 1] == true && isOnOpenList[posX, posY - 1] == false && isOnPathList[posX, posY - 1] == false)
            {
                openList.Add(Tuple.Create(posX, posY - 1));
                isOnOpenList[posX, posY - 1] = true;
            }

            // Check left
            if (posX - 1 >= 0 && pathable[posX - 1, posY] == true && isOnOpenList[posX - 1, posY] == false && isOnPathList[posX - 1, posY] == false)
            {
                openList.Add(Tuple.Create(posX - 1, posY));
                isOnOpenList[posX - 1, posY] = true;
            }

            // Check down
            if (posY + 1 < SIZEY && pathable[posX, posY + 1] == true && isOnOpenList[posX, posY + 1] == false && isOnPathList[posX, posY + 1] == false)
            {
                openList.Add(Tuple.Create(posX, posY + 1));
                isOnOpenList[posX, posY + 1] = true;
            }

            // Check right
            if (posX + 1 < SIZEX && pathable[posX + 1, posY] == true && isOnOpenList[posX + 1, posY] == false && isOnPathList[posX + 1, posY] == false)
            {
                openList.Add(Tuple.Create(posX + 1, posY));
                isOnOpenList[posX + 1, posY] = true;
            }
        }
                
        /*
         * Removes a tile location from the open list
         */
        void removeFromOpenList(int pointX, int pointY)
        {
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].Item1 == pointX && openList[i].Item2 == pointY)
                {
                    openList.RemoveAt(i);
                }
            }
        }

        /*
         * Based on the pathDecider's generated number, it will move to a direction depending on the number
         */
        bool nextMove(int pX, int pY)
        {
            int next = pathDecider();
            bool canMove = false;
            if (next == 0) // Moving up
            {
                if (pY - 1 >= 0 && pathable[pX, pY - 1] == true && isOnPathList[pX, pY - 1] == false)
                {
                    parentNode[pX, pY - 1].x = pX;
                    parentNode[pX, pY - 1].y = pY;
                    path.Add(Tuple.Create(pX, pY - 1));
                    isOnPathList[pX, pY - 1] = true;
                    removeFromOpenList(pX, pY - 1);
                    checkNeighbors(pX, pY - 1);
                    currentY = currentY - 1;
                    canMove = true;
                }
            }
            if (next == 1) // Moving left
            {
                if (pX - 1 >= 0 && pathable[pX - 1, pY] == true && isOnPathList[pX - 1, pY] == false)
                {
                    parentNode[pX - 1, pY].x = pX;
                    parentNode[pX - 1, pY].y = pY;
                    path.Add(Tuple.Create(pX - 1, pY));
                    isOnPathList[pX - 1, pY] = true;
                    removeFromOpenList(pX - 1, pY);
                    checkNeighbors(pX - 1, pY);
                    currentX = currentX - 1;
                    canMove = true;
                }
            }

            if (next == 2) // Moving down
            {
                if (pY + 1 < SIZEY && pathable[pX, pY + 1] == true && isOnPathList[pX, pY + 1] == false)
                {
                    parentNode[pX, pY + 1].x = pX;
                    parentNode[pX, pY + 1].y = pY;
                    path.Add(Tuple.Create(pX, pY + 1));
                    isOnPathList[pX, pY + 1] = true;
                    removeFromOpenList(pX, pY + 1);
                    checkNeighbors(pX, pY + 1);
                    currentY = currentY + 1;
                    canMove = true;
                }
            }

            if (next == 3) // Moving right
            {
                if (pX + 1 < SIZEX && pathable[pX + 1, pY] == true && isOnPathList[pX + 1, pY] == false)
                {
                    parentNode[pX + 1, pY].x = pX;
                    parentNode[pX + 1, pY].y = pY;
                    path.Add(Tuple.Create(pX + 1, pY));
                    isOnPathList[pX + 1, pY] = true;
                    removeFromOpenList(pX + 1, pY);
                    checkNeighbors(pX + 1, pY);
                    currentX = currentX + 1;
                    canMove = true;
                }
            }
            return canMove;
        }

        /*
         * Used to determine whether the agent is stuck. Agent is stuck if cardinal direction tiles
         * are unpathable and/or were already used as part of the path
         */
        bool isStuck(int posX, int posY)
        {
            stuckCounter = 0;
            //Check up
            if (posY - 1 < 0)
            {
                stuckCounter++;
            }
            if (posY - 1 >= 0 && (pathable[posX, posY - 1] == false || isOnPathList[posX, posY - 1] == true))
            {
                stuckCounter++;
            }

            // Check left
            if (posX - 1 < 0)
            {
                stuckCounter++;
            }
            if (posX - 1 >= 0 && (pathable[posX - 1, posY] == false || isOnPathList[posX - 1, posY] == true))
            {
                stuckCounter++;
            }

            // Check down
            if (posY + 1 >= SIZEY)
            {
                stuckCounter++;
            }
            if (posY + 1 < SIZEY && (pathable[posX, posY + 1] == false || isOnPathList[posX, posY + 1] == true))
            {
                stuckCounter++;
            }

            // Check right
            if (posX + 1 >= SIZEX)
            {
                stuckCounter++;
            }
            if (posX + 1 < SIZEX && (pathable[posX + 1, posY] == false || isOnPathList[posX + 1, posY] == true))
            {
                stuckCounter++;
            }

            // If all cardinal directions are either unpathable or are already on your current path then you are stuck
            if (stuckCounter >= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Prints the grid world with the unpathable tiles, start, and end labeled
         */
        static void printPathable(bool[,] w, int sX, int sY, int eX, int eY)
        {
            //Console.WriteLine("World in True/False:");
            //for (int i = 0; i < TheWorld.SIZEY; i++)
            //{
            //    for (int j = 0; j < TheWorld.SIZEX; j++)
            //    {
            //        Console.Write(w[j, i] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //TheWorld world = new TheWorld();
            Console.WriteLine("\nThe World \n0 -> NOT Pathable   1 -> Pathable   S -> Start   E -> End");
            for (int i = 0; i < TheWorld.SIZEY; i++)
            {
                for (int j = 0; j < TheWorld.SIZEX; j++)
                {
                    if (j == sX && i == sY)
                    {
                        Console.Write("S");
                    }
                    else if (j == eX && i == eY)
                    {
                        Console.Write("E");
                    }
                    else
                    {
                        int change = Convert.ToInt32(w[j, i]);
                        Console.Write(change);
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        /*
         * Finds the path and prints the path
         */
        public void FindPath()
        {
            bool endFound = false;
            int numRuns = 1;
            for (int i = 0; i < SIZEY; i++)
            {
                for (int j = 0; j < SIZEX; j++)
                {
                    pathable[j, i] = true;
                    isOnPathList[j, i] = false;
                    isOnOpenList[j, i] = false;
                }
            }
            Console.Write("Number of runs: ");
            numRuns = Convert.ToInt32(Console.ReadLine());
            //currentX = startX;
            //currentY = startY;            
            //isOnPathList[currentX, currentY] = true;
            //parentNode[currentX, currentY].x = currentX;
            //parentNode[currentX, currentY].y = currentY;
            //path.Add(Tuple.Create(currentX, currentY));
            unpathableGen();            
            //checkNeighbors(currentX, currentY);

            for (int r = 0; r < numRuns; r++)
            {
                for (int i = 0; i < SIZEY; i++)
                {
                    for (int j = 0; j < SIZEX; j++)
                    {
                        isOnPathList[j, i] = false;
                        isOnOpenList[j, i] = false;
                    }
                }
                openList.Clear();
                path.Clear();
                currentX = startX;
                currentY = startY;
                isOnPathList[currentX, currentY] = true;
                parentNode[currentX, currentY].x = currentX;
                parentNode[currentX, currentY].y = currentY;
                path.Add(Tuple.Create(currentX, currentY));
                checkNeighbors(currentX, currentY);
                Console.WriteLine("\nRun #: " + (r + 1));
                printPathable(pathable, startX, startY, endX, endY);
                //for (int i = 0; i < 50000000; i++)
                while (openList.Count > 0 || endFound == true)
                {
                    checkNeighbors(currentX, currentY);
                    if (isStuck(currentX, currentY) == true)
                    {
                        if (openList.Count > 0)
                        {
                            int randOpen = 0;
                            randOpen = random.Next(0, openList.Count);
                            //Console.WriteLine("randopen: " + randOpen);
                            currentX = openList[randOpen].Item1;
                            currentY = openList[randOpen].Item2;
                            path.Add(Tuple.Create(currentX, currentY));
                            isOnPathList[currentX, currentY] = true;
                            removeFromOpenList(currentX, currentY);
                            checkNeighbors(currentX, currentY);
                            //currentX = parentNode[currentX, currentY].x;
                            //currentY = parentNode[currentX, currentY].y;
                        }
                    }
                    else
                    {
                        //while (nextMove(currentX, currentY) == false)
                        //{
                        nextMove(currentX, currentY);
                        //}
                    }
                    if (currentX == endX && currentY == endY)
                    {
                        endFound = true;
                        break;
                    }
                }

                if (endFound == true)
                {
                    Console.WriteLine("\nThe end has been reached!");
                    Console.WriteLine("Length to get there: " + path.Count);
                }
                else
                {
                    Console.WriteLine("There is no path to the end! :(\n");
                }

                //Console.WriteLine("Stuck counter: " + stuckCounter);
                Console.WriteLine("\nPath List:");
                for (int p = 0; p < path.Count; p++)
                {
                    Console.WriteLine(p + " - path: " + path[p].Item1 + ", " + path[p].Item2);
                    /*Console.WriteLine(" parent node: " + parentNode[path[p].Item1, path[p].Item2].x + ", "
                        + parentNode[path[p].Item1, path[p].Item2].y);*/
                }
                #region Open List and what is on the Path List Debug
                //Console.WriteLine("\nThe open list:");
                //for (int openL = 0; openL < openList.Count; openL++)
                //{
                //    Console.WriteLine(openList[openL]);
                //}
                //Console.WriteLine("\nWhat is part of path: ");
                //for (int i = 0; i < SIZEY; i++)
                //{
                //    for (int j = 0; j < SIZEX; j++)
                //    {
                //        int tile = Convert.ToInt32(isOnPathList[j, i]);
                //        Console.Write(tile + " ");                   
                //    }
                //    Console.WriteLine();
                //}
                #endregion
            }
        }
    }
}
