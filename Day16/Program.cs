using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AoC16
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] myTicket = { 139, 67, 71, 59, 149, 89, 101, 83, 107, 103, 79, 157, 151, 113, 61, 109, 73, 97, 137, 53 };

            var data = File.ReadAllText("input.txt").Split("\n").Select(l => l.Trim('\r', ' '));

            var ranges = File.ReadAllText("fields.txt").Split("\n").Select(l => l.Trim('\r', ' '));

            List<Field> fields = new List<Field>();
            foreach(var line in ranges)
            {
                var info = line.Split(':');
                var rangez = info[1].Split("or");
                var range1 = rangez[0].Split('-');
                var range2 = rangez[1].Split('-');

                fields.Add(new Field(info[0], int.Parse(range1[0]),int.Parse(range1[1]), int.Parse(range2[0]), int.Parse(range2[1])));


            }
            
            List<List<int>> tickets = new List<List<int>>();

            foreach (var line in data)
            {
                var lineData = line.Split(',');
                List<int> numbers = new List<int>();
                foreach (var item in lineData)
                {
                    numbers.Add(int.Parse(item));
                }
                tickets.Add(numbers);

            }
            int tser = 0;
            List<List<int>> toRemove = new List<List<int>>();
            foreach (var ticket in tickets)
            {
                foreach (var item in ticket)
                {
                    bool passed = false;
                    foreach (var field in fields)
                    {
                        if ((item >= field.Low1 && item <= field.High1) || (item >= field.Low2 && item <= field.High2))
                        {
                            passed = true;

                        }
                    }
                    if (!passed)
                    {
                        tser += item;
                        toRemove.Add(ticket);
                    }

                }
            }
           
            foreach (var ticket in toRemove)
            {
                tickets.Remove(ticket);
            }

            
            Dictionary<string, List<int>> fieldindex = new Dictionary<string, List<int>>();
            foreach (var field in fields)
            {
                for (int col = 0; col < tickets[0].Count(); col++)
                {
                   if (tickets.All(t => (t[col] >= field.Low1 && t[col] <= field.High1) || (t[col] >= field.Low2 && t[col] <= field.High2)))
                   {
                       
                        try
                        {
                            fieldindex[field.Name].Add(col);
                        }
                        catch
                        {
                            fieldindex[field.Name] = new List<int>();
                            fieldindex[field.Name].Add(col);
                        }
                        
                   }
                }
            }

            while (fieldindex.Any(f => f.Value.Count() > 1))
            {
                foreach (var f in fieldindex.Where(f => f.Value.Count() == 1))
                {
                    foreach(var rest in fieldindex.Where(r => r.Value.Count() > 1))
                    {
                        rest.Value.RemoveAll(r => r == f.Value.FirstOrDefault());
                    }
                }
            }

            
           
            var answer = fieldindex
                .Where(f => f.Key.Contains("departure"))
                .Select(m => (long)myTicket[m.Value[0]])
                .Aggregate(1, (long total, long next) => total * next);
            
            
            
            Console.WriteLine("Answer: " + answer);
           



        }
    }
    class Field
    {
        public string Name { get; set; }
        public int Low1 { get; set; }
        public int High1 { get; set; }
        public int Low2 { get; set; }
        public int High2 { get; set; }

        public Field(string name, int low1, int high1, int low2, int high2)
        {
            Name = name;
            Low1 = low1;
            High1 = high1;
            Low2 = low2;
            High2 = high2;

        }
        public override string ToString()
        {
            return $"{Name} {Low1} - {High1} {Low2} - {High2}";
        }
    }
}
