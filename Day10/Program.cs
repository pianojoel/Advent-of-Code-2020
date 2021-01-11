using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AoC10
{
    class Program
    {
        static void Main(string[] args)
        {
            var numList = File.ReadAllText("input.txt")
                .Split("\n")
                .Select(l=> l.Trim())
                .Select(l => int.Parse(l))
                
                .OrderBy(l => l)
                .ToList();
            //foreach (var num in numList)

            //{
            //    Console.WriteLine(num);
            //}
            //Console.WriteLine();          
            //var a = numList.Select((n, i) => i ==  0 ? n - 0 : n - numList[i - 1]).Where(n => n == 1).Count();
            //var b = numList.Select((n, i) => i ==  0 ? n - 0 : n - numList[i - 1]).Where(n => n == 3).Count();
            //Console.WriteLine(a * (b + 1));
            numList.Insert(0, 0);
            numList.Add(numList.Max() + 3);
            //var revNumList = numList.OrderByDescending(l => l).ToList();
            //foreach (var item in revNumList)
            //{
            //    Console.WriteLine(item);
            //}

            List<long> accs = new List<long>();

            for (int i = 0;i < numList.Count(); i++)
            {
                int j = 1;
                int pathCount = 0;
                while (j < 4 && i + j < numList.Count())
                {
                    if (numList[i + j] - numList[i] <= 3) pathCount++;
                    j++;
                }
                if (pathCount == 0) pathCount = 1;
                accs.Add(pathCount);
            }
            
            var paths = new List<long>();
            for (int i = numList.Count() -2; i >= 0; i--)
            {
               
                int j = 1;
                while (j < 4 && i + j < numList.Count())
                {
                    if (numList[i + j] - numList[i] <= 3) paths.Add(accs[i + j]);
                    j++;
                }
                long newValue = 0;
                foreach (var item in paths)
                {
                    newValue += item;
                }
                accs[i] = newValue;
                paths.Clear();

            }

            Console.WriteLine(accs[0]);



        }
        static long permutations(List<int> remaining)
        {
            //Console.WriteLine(remaining.Count());
            if (remaining.Count() == 0)
            {
                return 0;
            }

            List<List<int>> remainingLists = new List<List<int>>();
            int m;
            if (remaining.Count() < 4)
            {
                m = remaining.Count();
            }
            else
            {
                m = 4;
            }
            for (int i = 1; i < m; i++)
            {
                if (remaining[0] - remaining[i] <= 3)
                {
                    remainingLists.Add(remaining.GetRange(i, remaining.Count() - i));
                }
            }
            long result = 0;
            if (remainingLists.Count() > 1)
            {
                result = remainingLists.Count() - 1;
            }
            
            foreach (var list in remainingLists)
            {
                result += permutations(list);

            }

            return result;

        }
    }
}
