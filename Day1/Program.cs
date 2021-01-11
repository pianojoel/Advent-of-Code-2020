using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace AoC1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(@"input.txt");
            var data = input.Split('\n');
            List<int> myList = new List<int>();
            foreach (var line in data)
            {

                myList.Add(int.Parse(line));
            }

            for (int i = 0; i < myList.Count(); i++)
            {
                for (int j = i + 1; j < myList.Count(); j++)
                {
                    if (myList[i] + myList[j] == 2020) Console.WriteLine("Answer part 1: " + myList[i] * myList[j]);
                    for (int k = j + 1; k < myList.Count(); k++)
                    {
                        if (myList[i] + myList[j] + myList[k] == 2020) Console.WriteLine("Answer part 2: " + myList[i] * myList[j] * myList[k]);
                    }
                }
            }
        }
    }
}
          

          