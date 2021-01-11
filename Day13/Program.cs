using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;  

namespace AoC13
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split('\n');
            int timeStamp = int.Parse(data[0]);
            List<long> busses = new List<long>();
            foreach (var item in data[1].Split(','))
            {
                if (item != "x")
                {
                    busses.Add(long.Parse(item));
                }
            }
            //foreach (var bus in busses)
            //{
            //    Console.WriteLine(bus);
            //}
            //var q = busses.Select(b => new { bus = b, wait = b - (timeStamp % b) }).OrderBy(b => b.wait).First(); ;
            ////foreach (var item in q)
            ////{
            ////    Console.WriteLine(item);
            ////}
            //Console.WriteLine(q.bus * q.wait);
            //Console.WriteLine(busses[0] * )
            //long i = 100000000000000L;
            Console.WriteLine(2070900611L * 787);
            //Console.ReadKey();
            long incr = 1;
            
               // Console.WriteLine(i);
            long i = 0;

            List<bool> visited = new List<bool>();
            for (int k = 0; k < 9; k++)
            {
                visited.Add(false);
            }
            for (int k = 0; k < busses.Count(); k++)
            {
                incr = 1;
                for (int j = 0; j < k; j++)
                {
                    incr = busses[j] * incr;
                    Console.Write(incr + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(busses);
            while (true)
            {
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && (i + 35) % busses[3] == 0 && (i + 36) % busses[4] == 0 && (i + 40) % busses[5] == 0 && (i + 48) % busses[6] == 0 && (i + 54) % busses[7] == 0 && (i + 77) % busses[8] == 0)
                {
                    Console.WriteLine(i);
                    break;
                }

                if (i % busses[0] == 0 && !visited[0])
                {
                    incr = busses[0];
                    visited[0] = true;
                    Console.WriteLine(busses[0]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && !visited[1])
                {
                    incr = busses[0] * busses[1];
                    visited[1] = true;
                    Console.WriteLine(busses[1]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && !visited[2])
                {
                    incr = busses[0] * busses[1] * busses[2];
                    visited[2] = true;
                    Console.WriteLine(busses[2]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && (i + 35) % busses[3] == 0 && !visited[3])
                {
                    incr = busses[0] * busses[1] * busses[2] * busses[3];
                    visited[3] = true;
                    Console.WriteLine(busses[3]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && (i + 35) % busses[3] == 0 && (i + 36) % busses[4] == 0 && !visited[4])
                {
                    incr = busses[0] * busses[1] * busses[2] * busses[3] * busses[4];
                    visited[4] = true;
                    Console.WriteLine(busses[4]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && (i + 35) % busses[3] == 0 && (i + 36) % busses[4] == 0 && (i + 40) % busses[5] == 0 && !visited[5])
                {
                    incr = busses[0] * busses[1] * busses[2] * busses[3] * busses[4] * busses[5];
                    visited[5] = true;
                    Console.WriteLine(busses[5]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && (i + 35) % busses[3] == 0 && (i + 36) % busses[4] == 0 && (i + 40) % busses[5] == 0 && (i + 48) % busses[6] == 0 && !visited[6])
                {
                    incr = busses[0] * busses[1] * busses[2] * busses[3] * busses[4] * busses[5] * busses[6];
                    visited[6] = true;
                    Console.WriteLine(busses[6]);
                }
                if (i % busses[0] == 0 && (i + 7) % busses[1] == 0 && (i + 17) % busses[2] == 0 && (i + 35) % busses[3] == 0 && (i + 36) % busses[4] == 0 && (i + 40) % busses[5] == 0 && (i + 48) % busses[6] == 0 && (i + 54) % busses[7] == 0 && !visited[7])
                {
                    incr = busses[0] * busses[1] * busses[2] * busses[3] * busses[4] * busses[5] * busses[6] * busses[7];
                    visited[7] = true;
                    Console.WriteLine(busses[7]);

                }



                Console.WriteLine(i + " " + incr);
                i += incr;
            }
        }
    }
}
