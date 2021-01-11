using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AoC19
{
    class Program
    {
        static void Main(string[] args)
        {
             var data = File.ReadAllText("input.txt").Split("\r\n\r\n");
            var r42 = File.ReadAllText("42.txt").Split("\n").Select(l => l.Trim(' ', '\r'));
            var r31 = File.ReadAllText("31.txt").Split("\n").Select(l => l.Trim(' ', '\r'));

            //var myList = new List<string>();

            //foreach (var i in r42)
            //{
            //    foreach (var j in r42)
            //    {
            //        foreach (var k in r31)
            //        {
            //            string toAdd = $"{i}{j}{k}";
            //            myList.Add(toAdd);
            //            //Console.WriteLine(toAdd);
            //        }
            //    }
            //}
            //var myResult = myList.Select(s => s.Replace(" ", "")).ToList();
            ////foreach (var result in myResult)
            //// {
            ////     Console.WriteLine(result);
            //// }
            //Console.WriteLine("Count: " + myList.Count());
            //Console.WriteLine("Distinct Count: " + myList.Distinct().Count());

            //int count = 0;
            //var lines = data[1].Split('\n').Select(l => l.Trim(' ', '\r'));

            //count = lines
            //    .Where(l => myResult.Any(r => r == l))
            //    .Count();
            //Console.WriteLine("Count: " + count);
            //Console.ReadKey();

            var messages = data[1].Split("\n").Select(l => l.Trim(' ', '\r'));
            Console.WriteLine(messages.Count());
            bool isMatch;
            int matchCount = 0;
            bool check42 = true;
            bool check31 = false;
            int count31 = 0;
            int count42 = 0;
            foreach (var message in messages)
            {
                isMatch = true;
                check42 = true;
                check31 = false;
                count31 = 0;
                count42 = 0;
                for (int i = 0; i < message.Length; i+= 8)
                {
                    var chunk = message.Substring(i, 8);
                    
                    if (check42)
                    {
                        if (r42.Contains(chunk)) //
                        {
                            Console.Write("[42]");
                            count42++;
                        }
                        else if (i >= 16 && r31.Contains(chunk))
                        {
                            check42 = false;
                            check31 = true;
                        }
                        else
                        {
                            isMatch = false;
                        }
                    }
                    if (check31)
                    {
                        if (r31.Contains(chunk)) //
                        {
                            Console.Write("[31]");
                            count31++;
                        }
                        else
                        {
                            isMatch = false;
                        }

                    }
                    
                   


                }
                if (count31 >= count42) isMatch = false;
                if (count31 < 1) isMatch = false;
                if (isMatch)
                {
                    matchCount++;
                    Console.Write(" Match");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Matchcount: " + matchCount);
            Console.ReadKey();
            Dictionary<string, string> rules = new Dictionary<string, string>();

            foreach (var line in data[0].Split("\n").Select(l => l.Trim(' ', '\r')))
            {
                var splitLine = line.Split(":").Select(l => l.Trim(' ', '\r', '\"')).ToList();
                rules.Add(splitLine[0], splitLine[1]);
            }








            int longest = data[1].Split("\n").Select(l => l.Length).Max();
            

            List<string> newStrings = new List<string>();
            List<string> tmpStrings = new List<string>();

            newStrings.Add("31");
            while (newStrings.Any(s => s.Any(sc => "1234567890".Contains(sc))))
            {
                for (int i = 0; i < newStrings.Count(); i++)
                {

                    if (newStrings[i].Length > longest)
                    {
                        Console.WriteLine("longest");
                        continue;
                    }
                    var splitString = newStrings[i].Split(" ").Select(l => l.Trim(' ', '\r')).ToArray();
                    List<string> history = new List<string>();
                    for (int j = 0; j < splitString.Length; j++)
                    {
                        history.Add(splitString[j]);
                        if (Char.IsLetter(splitString[j][0])) continue;

                        var newRule = rules[splitString[j]];
                        if (newRule.Contains('|'))
                        {
                            string newString = String.Join(" ", history.Take(j).ToArray()) + (j == 0 ? "" : " ") + newRule.Split(" | ")[0] + (j == splitString.Length - 1 ? "" : " ") + String.Join(" ", splitString.TakeLast(splitString.Length - j - 1).ToArray());
                            string newString2 = String.Join(" ", history.Take(j).ToArray()) + (j == 0 ? "" : " ") + newRule.Split(" | ")[1] + (j == splitString.Length - 1 ? "" : " ") + String.Join(" ", splitString.TakeLast(splitString.Length - j - 1).ToArray());

                            tmpStrings.Add(newString);
                            tmpStrings.Add(newString2);

                            break;
                        }
                        else
                        {
                            string newString = String.Join(" ", history.Take(j).ToArray()) + (j == 0 ? "" : " ") + newRule + (j == splitString.Length - 1 ? "" : " ") + String.Join(" ", splitString.TakeLast(splitString.Length - j - 1).ToArray());
                            tmpStrings.Add(newString);
                           
                            break;
                        }

                    }

                    if (!newStrings[i].Any(c => Char.IsDigit(c)))
                    {
                        tmpStrings.Add(newStrings[i]);
                    }
                    history.Clear();
                }
                //foreach (var s in tmpStrings)
                //{
                //    Console.WriteLine(s);
                //}
                //Console.WriteLine("-------------------");  2097152
                //Console.ReadKey();
                newStrings.Clear();
                    newStrings.AddRange(tmpStrings);
                    tmpStrings.Clear();
                Console.WriteLine(newStrings.Select(s => s.Split(" ").Length).Max());  
            }

            

           // var myResult = newStrings.Select(s => s.Replace(" ", "")).ToList();
           ////foreach (var result in myResult)
           //// {
           ////     Console.WriteLine(result);
           //// }
           // Console.WriteLine("Count: " + newStrings.Count());
           // Console.WriteLine("Distinct Count: " + newStrings.Distinct().Count());

           // int count = 0;
           // var lines = data[1].Split('\n').Select(l => l.Trim(' ', '\r'));

           // count = lines                
           //     .Where(l => myResult.Any(r => r == l))
           //     .Count();
           // Console.WriteLine("Count: " + count);




            

           
        }
    }
}
       
