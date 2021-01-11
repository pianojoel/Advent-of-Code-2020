using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AoC12
{
    class Program
    {
        enum Direction
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270
        }
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split("\n").Select(l => l.Trim(' ', '\r'));
            List<char> instructions = new List<char>();
            List<int> value = new List<int>();

            foreach (var line in data)
            {
                instructions.Add(line[0]);
                value.Add(int.Parse(line.Substring(1)));
            }
            int EW = 0;
            int NS = 0;
            int WPEW = 10;
            int WPNS = 1;
            var dir = Direction.East;





            //for (int i = 0; i < value.Count; i++)
            //{

            //    switch (instructions[i])
            //    {
            //        case 'N':
            //            NS += value[i];
            //            break;
            //        case 'W':
            //            EW -= value[i];
            //            break;
            //        case 'S':
            //            NS -= value[i];
            //            break;
            //        case 'E':
            //            EW += value[i];
            //            break;
            //        case 'R':
            //            var dr = (int)dir + value[i];
            //            if (dr > 359) dr -= 360;
            //            dir = (Direction)dr;
            //            break;
            //        case 'L':
            //            var dl = (int)dir - value[i];
            //            if (dl < 0) dl += 360;
            //            dir = (Direction)dl;
            //            break;
            //        case 'F':
            //            switch (dir)
            //            {
            //                case Direction.North:
            //                    NS += value[i];
            //                    break;
            //                case Direction.East:
            //                    EW += value[i];
            //                    break;
            //                case Direction.South:
            //                    NS -= value[i];
            //                    break;
            //                case Direction.West:
            //                    EW -= value[i];
            //                    break;

            //            }
            //            break;
            //    }
            //    //Console.WriteLine(instructions[i] + " " + value[i]);
            //}
            //Console.WriteLine(EW);
            //Console.WriteLine(NS);
            //Console.WriteLine(Math.Abs(EW) + Math.Abs(NS));




            for (int i = 0; i < value.Count; i++)
            {

                switch (instructions[i])
                {
                    case 'N':
                        WPNS += value[i];
                        break;
                    case 'W':
                        WPEW -= value[i];
                        break;
                    case 'S':
                        WPNS -= value[i];
                        break;
                    case 'E':
                        WPEW += value[i];
                        break;
                    case 'F':
                        for (int j = 0; j < value[i]; j++)
                        {
                            EW += WPEW;
                            NS += WPNS;
                        }
                        break;
                    case 'R':
                        switch (value[i])
                        {
                            case 90:
                                int tmp = WPEW;
                                WPEW = WPNS;
                                WPNS = tmp * -1;
                                break;
                            case 180:
                                WPEW *= -1;
                                WPNS *= -1;
                                break;
                            case 270:
                                tmp = WPEW;
                                WPEW = WPNS * -1;
                                WPNS = tmp;
                                break;


                        }
                        
                        break;
                    case 'L':
                        switch (value[i])
                        {
                            case 270:
                                int tmp = WPEW;
                                WPEW = WPNS;
                                WPNS = tmp * -1;
                                break;
                            case 180:
                                WPEW *= -1;
                                WPNS *= -1;
                                break;
                            case 90:
                                tmp = WPEW;
                                WPEW = WPNS * -1;
                                WPNS = tmp;
                                break;
                        }
                                break;
                }
                //Console.WriteLine(instructions[i] + " " + value[i]);
            }
            Console.WriteLine(EW);
            Console.WriteLine(NS);
            Console.WriteLine(Math.Abs(EW) + Math.Abs(NS));
        }
    }
}
