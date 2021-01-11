using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc6
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt");

            var groups = data.Split("\r\n\r\n");

            var count = groups
                .Select(g => Regex.Replace(g, @"\t|\n|\r", "")
                .Distinct()
                .Count())
                .Sum();

            Console.WriteLine("Answer part 1: " + count);

            List<List<string>> groupList = new List<List<string>>();
            foreach (var group in groups)
            {
                var g = group.Split("\n").Select(l => l.Trim()).ToList();
                groupList.Add(g);
            }
            int totalYes = 0;
            string current = "";
            List<char> toRemove = new List<char>();
            foreach (var group in groupList)
            {
                current = group[0];
                foreach (var line in group)

                {
                    
                    
                    foreach (var c in current)
                    {
                        if (!line.Contains(c))
                        {
                            toRemove.Add(c);
                        }
                    }
                    foreach (var l in toRemove)
                    {
                        current = current.Remove(current.IndexOf(l),1);
                    }
                    toRemove.Clear();
                   
                }
               
                totalYes += current.Count();
                
            }
            Console.WriteLine("Answer part 2: " + totalYes);
           

        }
    }
}
