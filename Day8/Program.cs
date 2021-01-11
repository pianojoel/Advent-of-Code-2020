using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Aoc8
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt");
            var lines = data.Split("\n");
            List<Operation> oList = new List<Operation>();


            foreach (var line in lines)
            {
                Operation operation = new Operation();
                var cols = line.Split(" ");
                operation.Op = cols[0];
                operation.Value = int.Parse(cols[1]);
                operation.Executed = false;
                operation.Tried = false;
                oList.Add(operation);

            }

            //foreach (var item in oList)
            //{
            //    Console.WriteLine(item.Op + " " + item.Value + " " + item.Executed);
            //}
            //Console.WriteLine(oList.Count());
            //int index = 0;
            //int value = 0;
            //while (!oList[index].Executed)
            //{
            //    oList[index].Executed = true;
            //    switch (oList[index].Op)
            //    {
            //        case "nop":
            //            index++;
            //            break;
            //        case "acc":
            //            value += oList[index].Value;
            //            index++;
            //            break;
            //        case "jmp":
            //            index += oList[index].Value;
            //            break;
            //    }

            //}
            //Console.WriteLine(value);

            int index = 0;
            int value = 0;
            bool tried = false;

            foreach (var item in oList)
            {
                item.OriginalOp = item.Op;
            }
            while (index < 637)
            {
                if (oList[index].Executed)
                {
                    Console.WriteLine("index: " + index);

                    index = 0;
                    value = 0;
                    foreach (var item in oList)
                    {
                        item.Executed = false;
                        if (!(item.OriginalOp == item.Op))
                        {
                            item.Op = item.OriginalOp;
                        }
                    }
                    tried = false;
                }
                if (!oList[index].Tried && !tried)
                {
                    if (oList[index].Op == "nop")
                    {
                        oList[index].Op = "jmp";
                        oList[index].OriginalOp = "nop";

                        oList[index].Tried = true;
                        tried = true;
                    }
                    else if (oList[index].Op == "jmp")
                    {
                        oList[index].Op = "nop";
                        oList[index].OriginalOp = "jmp";
                        oList[index].Tried = true;
                        tried = true;
                    }
                }
                oList[index].Executed = true;
                switch (oList[index].Op)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        value += oList[index].Value;
                        index++;
                        break;
                    case "jmp":
                        index += oList[index].Value;
                        break;
                }

            }
            Console.WriteLine(value);

        }
    }
    class Operation
    {
        public string Op { get; set; }
        public string OriginalOp { get; set; }
        public int Value { get; set; }
        public bool Executed { get; set; }
        public bool Tried { get; set; }
        public bool Changed { get; set; }

    }
}
