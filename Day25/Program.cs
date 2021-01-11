using System;

namespace aoC25
{
    class Program
    {
        static void Main(string[] args)
        {
            long value = 1;
            int counter = 1;
            int pKey1 = 15113849;
            int pKey2 = 4206373;

            int loopSize1;
            int loopSize2;
            while (true)
            {
                value *= 7;
                value = value % 20201227;
                              
                
                if (value == pKey1)
                {
                    Console.WriteLine("loop size 1: " + counter);
                    loopSize1 = counter;
                    break;
                }
               
                counter++;
            }
            counter = 1;
            value = 1;
            while (true)
            {
                value *= 7;
                value = value % 20201227;
                
                
                if (value == pKey2)
                {
                    Console.WriteLine("loop size 2: " + counter);
                    loopSize2 = counter;
                    break;
                }
                counter++;
            }
            value = 1;
            for (int i = 0; i < loopSize2; i++)
            {
                value *= pKey1;
                value = value % 20201227;
            }
            Console.WriteLine(value);

        }
    }
}
