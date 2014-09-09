using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{    
    class Program
    {   
        static void printWorldOpenList(bool[,] open)
        {
            Console.WriteLine("\nTiles in the open list: ");
            for (int i = 0; i < TheWorld.SIZEY; i++)
            {
                for (int j = 0; j < TheWorld.SIZEX; j++)
                {
                    int change = Convert.ToInt32(open[j, i]);
                    Console.Write(change + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            TheWorld theWorld = new TheWorld();
            Console.WriteLine("World size: " + TheWorld.SIZEX + " X " + TheWorld.SIZEY + "\n");
            Console.Write("Enter the starting position's x value: ");
            theWorld.startX = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the starting position's y value: ");
            theWorld.startY = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the end position's x value: ");
            theWorld.endX = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the end position's y value: ");
            theWorld.endY = Convert.ToInt32(Console.ReadLine());
            //theWorld.unpathableGen();            
            //theWorld.checkNeighbors(theWorld.startX, theWorld.startY);
            theWorld.FindPath();                        
            //printWorldOpenList(theWorld.isOnOpenList);

            //Console.WriteLine("\nThe open list:");
            //for (int openL = 0; openL < theWorld.openList.Count; openL++)
            //{                
            //    Console.WriteLine(theWorld.openList[openL]);
            //}

            //Console.WriteLine(theWorld.startX);
            //Console.WriteLine(theWorld.startY);
            //Console.WriteLine(theWorld.endX);
            //Console.WriteLine(theWorld.endY);

            //********** Test the pathdecider randomizer *************
            //for (int blah = 0; blah < 10; blah++)
            //{
            //    Console.WriteLine(theWorld.pathDecider());
            //}
            //for (int blah = 0; blah < theWorld.path.Count; blah++)
            //{
            //    Console.WriteLine(theWorld.path[blah]);
            //}
            //Console.WriteLine(theWorld.path[1]);
            //Console.WriteLine(theWorld.path[1].Item1);
            //Console.WriteLine(theWorld.path[1].Item2);

            Console.ReadLine();
        }
    }
}
