using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC15
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers =  new List<int> { 6, 13, 1, 15, 2, 0 };



            //for (int turn = 7; turn <= 30000000; turn++)
            //{
            //    if (numbers.SkipLast(1).Contains(numbers[turn - 2]))
            //    {
            //        numbers.Add((turn -2) - numbers.SkipLast(1).ToList().LastIndexOf(numbers[turn -2]));

            //    }
            //    else
            //    {
            //        numbers.Add(0);
            //    }
            //}
            //Console.WriteLine(numbers.Last()) ;


            Dictionary<int, int> numberIndex = new Dictionary<int, int>();
            for(int i = 0; i < numbers.Count(); i++)
            {
                numberIndex.Add(numbers[i], i + 1);
            }
            int lastNumber = numbers[5];
            int toAdd;
            for (int i = 7; i <= 30000000; i++)
            {
                if (numberIndex.ContainsKey(lastNumber))
                {
                    int diff = i - 1 - numberIndex[lastNumber];
                    numberIndex[lastNumber] = i - 1;
                    lastNumber = diff;
                }
                else
                {
                    numberIndex[lastNumber] = i - 1;
                    lastNumber = 0;
                }
                
            }

            Console.WriteLine(lastNumber);


        }
    }
}
