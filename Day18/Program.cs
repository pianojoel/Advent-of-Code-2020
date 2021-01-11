using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoC18
{enum Op
    {
        Add,
        Sub,
        Mult,
        Div
    }
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split('\n').Select(l => l.Trim(' ', '\r'));

            //Console.WriteLine(Evaluate("(9 * (7 * 4 + 6) * 8 * (7 * 4 + 2 + 3 * 6))*((8 * 6) * 5) * (7 + 5 * 5 * (4 + 7) * 8 * 6) * 8 "));

            //Console.WriteLine(Evaluate(AddPs("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2")));
            
            long answer = 0;

            foreach (var line in data)
            {
                long result = Evaluate(AddPs(line));// *((8 * 6) * 5) * (7 + 5 * 5 * (4 + 7) * 8 * 6) * 8
                Console.WriteLine(result); //484704
                answer += result; //(9 * (7 * 4 + 6) * 8 * (7 * 4 + 2 + 3 * 6))
            }
            Console.WriteLine(answer);
        }
        static long Evaluate(string expression)
        {
            var op = new Op();
            op = Op.Add;
            long value = 0;
            for (int i = 0; i < expression.Length;i++)
            {
                char c = expression[i];
                if (Char.IsDigit(c))
               {
                    value = Calc(value, (int)Char.GetNumericValue(c), op);
               }


               switch (c)
                {
                    case '+':
                        op = Op.Add;
                        break;
                    case '-':
                        op = Op.Sub;
                        break;
                    case '*':
                        op = Op.Mult;
                        break;
                    case '/':
                        op = Op.Div;
                        break;
                }
                   
                if (c == '(')
                {
                    value = Calc(value, Evaluate(expression.Substring(i + 1)), op);
                    int count = 0;
                    int open = 1;
                    while (true)
                    {
                        var v = expression.Substring(i + 1)[count];
                        if (open == 1 && v == ')') break;
                        if (v == '(') open++;
                        if (open > 1 && v == ')') open--;
                        count++;
                    }

                    i += count +1;
                }
                if (c == ')')
                {
                    return value;
                }




            }

            
            return value;

        }
        static long Calc(long value, long toCalc, Op op)
        {
            switch (op)
            {
                case Op.Add:
                    value = value + toCalc;
                    break;
                case Op.Sub:
                    value = value - toCalc;
                    break;
                case Op.Mult:
                    value *= toCalc;
                    break;
                case Op.Div:
                    value /= toCalc;
                    break;

            }
            return value;
        }

        static string AddPs(string expression)
        {
            string newString = expression;
            int i = 0;
            while (i < newString.Length)
            {
                char c = newString[i];
                if (c == '+')
                {
                    if (Char.IsDigit(newString[i - 2]))
                    {
                        newString = newString.Insert(i - 2, "(");
                        i++;
                    }
                    else if (newString[i - 2] == ')')
                    {
                        int count = -3;
                        int open = 1;
                        while (true)
                        {
                            var v = newString[i + count];
                            if (open == 1 && v == '(') break;
                            if (v == ')') open++;
                            if (open > 1 && v == '(') open--;
                            count--;
                        }
                        newString = newString.Insert(i + count, "(");
                        i++;
                    }

                    if (Char.IsDigit(newString[i + 2]))
                    {
                        newString = newString.Insert(i + 3, ")");

                    }
                    else if (newString[i + 2] == '(')
                    {
                        int count = 3;
                        int open = 1;
                        while (true)
                        {
                            var v = newString[i + count];
                            if (open == 1 && v == ')') break;
                            if (v == '(') open++;
                            if (open > 1 && v == ')') open--;
                            count++;
                        }
                        newString = newString.Insert(i + count, ")");
                    }
                }
                i++;
            }
            return newString;
        }
    }
}
