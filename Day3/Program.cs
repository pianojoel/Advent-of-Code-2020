using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace AoC3
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Answer part 1: " + TreeCollision(1,3));
           
            Console.WriteLine("Answer part 2: " + TreeCollision(1, 1) * TreeCollision(1, 3) * TreeCollision(1, 5) * TreeCollision(1, 7) * TreeCollision(2, 1));



        }
        static long TreeCollision(int down, int right)
        {
            string data = File.ReadAllText(@"input.txt");
            string[] lines = data.Split("\n");
            int x = 0;



            int treesHit = 0;

            for (int i = 0; i < lines.Length; i += down)
            {
                var line = lines[i].Trim();

                char xch = line[x];

                if (xch == '#')
                {
                    treesHit++;

                }
                x += right;
                if (x > 30)
                {
                    x -= 31;

                }

            }

            return treesHit;
        }
    }
}