using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoC9
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt");
            var lines = data.Split("\n");
            List<long> numList = new List<long>();
            foreach (var line in lines)
            {
                numList.Add(Int64.Parse(line));

            }
            //foreach (var num in numList)
            //{
            //    Console.WriteLine(num);
            //}
            int valid = 0;
            long invalid = 0;
            for (int i = 0; i < numList.Count() - 26; i++)
            {
                var range = numList.GetRange(i, 25);
                long num = numList[i + 25];
                if (range.Any(n => range.Where(n2 => n2 != n).Any(n2 => n + n2 == num)))
                {
                    //Console.WriteLine("index: " + (i + 25) + " number: " + num + " is valid");
                    valid++;
                }
                else
                {
                    Console.WriteLine("index: " + (i + 25) + " number: " + num + " is NOT valid");
                    invalid = num;
                }



            }
            
            for (int low = 0; low < numList.Count(); low++)
            {
                long sum = numList[low];
                int high = 1;
                while (sum < invalid)
                {
                    sum += numList[low + high];
                    high++;


                }
                if (sum == invalid)
                {
                    var r = numList.GetRange(low, high);
                    Console.WriteLine(r.Min() + r.Max());
                    //Console.WriteLine(sum);
                    //Console.WriteLine(invalid);
                    //Console.WriteLine($"Sum equals invalid. Answer is {numList[low] + numList[high]}");
                }
            }

        }
    }
}
