using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace AoC2
{
    class Program
    {
        static void Main(string[] args)
        {


            string[] data = File.ReadAllText("input.txt").Split("\n");

           
            int correct1 = 0;
            int correct2 = 0;
            foreach (var line in data)
            {
                //Console.WriteLine(line);

                string[] lineArr = line.Split(" ");
                var limits = lineArr[0];
                var c = lineArr[1][0];
                var pw = lineArr[2];
                int occurances = pw.Count(p => p == c);
               
                if (occurances >= int.Parse(limits.Split("-")[0]) && occurances <= int.Parse(limits.Split("-")[1]))
                {
                    correct1++;
                }

                if ((pw[int.Parse(limits.Split("-")[0]) - 1] == c && pw[int.Parse(limits.Split("-")[1]) - 1] != c) || (pw[int.Parse(limits.Split("-")[0]) - 1] != c && pw[int.Parse(limits.Split("-")[1]) - 1] == c))
                {
                    correct2++;

                }
            }
            Console.WriteLine("Answer part 1: " + correct1);
            Console.WriteLine("Answer part 2: " + correct2);





        }
    }
}