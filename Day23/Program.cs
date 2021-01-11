using System;
using System.Collections.Generic;
using System.Linq;
namespace AoC23
{
    class Program
    {
        static int cupLastIndex;
        static void Main(string[] args)
        {
            Console.WriteLine(250343L * 651247L);
            Console.ReadKey();
            //250343
            //651247
            string data = "215694783";
            //string data = "389125467"; //testdata
            
            List<int> bigList = Enumerable.Range(10, 999991).ToList();
            List<int> smallList = Enumerable.Range(10, 91).ToList();

            List<int> cups = new List<int>();
            foreach (var c in data)
            {
                cups.Add((int)Char.GetNumericValue(c));
            }

            cups.AddRange(bigList);

            cupLastIndex = cups.Count();
           

            int pos = 0;

            //This takes three hours to complete
            for (int i = 0; i < 10000000; i++)
            {
                
                var current = cups[pos];
                var three = Get3(cups, pos);
                var dest = GetDest(cups[pos], three);
                foreach (var item in three)
                {
                    cups.Remove(item);
                }
                //var removed = Rem3(cups, pos);
                
                cups.InsertRange(cups.IndexOf(dest) + 1, three);
                int count = 0;
                while (cups[pos] != current)
                {
                    cups.Add(cups[0]);
                    cups.RemoveAt(0);
                    count++;
                }
                if (count > 3) Console.WriteLine(count);
                pos++;
                if (pos == cups.Count()) pos = 0;

            }
            
            var front = cups.GetRange(0, cups.IndexOf(1));
            cups.RemoveRange(0, cups.IndexOf(1));
            cups.AddRange(front);

            foreach (var n in cups)
            {
                Console.Write(n);

            }
            Console.WriteLine();
            foreach (var item in cups.GetRange(cups.IndexOf(1), 3))
            {
                Console.WriteLine(item);
            }
        }
        static int GetDest(int value, List<int> three)
        {

            int destValue = value - 1 == 0 ? cupLastIndex : value - 1;
            
            while (three.Contains(destValue)) 
            {
                destValue--; 
                if (destValue == 0) destValue = cupLastIndex;

            }
            return destValue;
        }
        static List<int> Get3(List<int>cups, int pos)
        {
            List<int> three = new List<int>();
            for (int i = pos + 1; i < pos + 4; i++)
            {
                int j = i > cups.Count() -1 ? i - cups.Count() : i;
                three.Add(cups[j]);

            }
            return three;
        }
        static List<int> Rem3(List<int> cups, int pos)
        {
            List<int> newCups = new List<int>(cups);
            for (int i = 0; i < 3; i++)
            {
                int j = pos + 1 > newCups.Count()-1 ? 0 : pos + 1;
                newCups.RemoveAt(j);

            }
            return newCups;
        }


    }
}

