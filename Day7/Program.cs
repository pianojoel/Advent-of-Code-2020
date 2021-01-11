using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;




namespace Aoc7
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt");
            var lines = data.Split("\n");
            List<Bag> bags = new List<Bag>();
            foreach (var line in lines)
            {
                Bag bag = new Bag();

                var splitline = line.Split("bags contain").Select(sl => sl.Trim(' ','\r','.')).ToArray();
                bag.Color = splitline[0];
                var splitcontains = splitline[1].Split(",").Select(s => s.Trim()).ToArray();

                if (!(splitcontains[0] == "no other bags"))
                {
                    foreach (var contains in splitcontains)
                    {
                        
                        var amount = contains.Substring(0, 1);
                        bag.Amount.Add(int.Parse(amount));
                        var containsTrim = contains.Trim(' ', '.').Substring(2);
                        var containsNoBag = containsTrim.Substring(0, containsTrim.IndexOf("bag"));

                        bag.Contains.Add(containsNoBag.Trim());
                       
                    }
                }
                bags.Add(bag);
               
            }

            
            List<Bag> parents = new List<Bag>();
            string myBag = "shiny gold";
            List<Bag> theList = new List<Bag>();
            theList.Add(bags.Where(b => b.Color == myBag).First());
           
            do
            {
                parents.Clear();
                foreach (var bag in theList)
                {
                    parents.AddRange(bags.Where(b => b.Contains.Contains(bag.Color) && !theList.Contains(b)).Distinct());
                }
                
                foreach (var parent in parents)
                {
                    if (!theList.Contains(parent))
                    {
                    theList.Add(parent);

                    }
                }
               
            }
            while (parents.Count() > 0);

           
            Console.WriteLine("Answer part 1: " + (theList.Count() -1));



            List<Bag> current = new List<Bag>();
            List<Bag> tmp = new List<Bag>();
            current.Add(bags.Where(b => b.Color == myBag).First());
            int totalBags = 0;
            
            do
            {
                
                foreach (var bag in current)
                {
                    
                    for (int i = 0; i < bag.Contains.Count(); i++)
                    {
                        for (int j = 0; j < bag.Amount[i]; j++)
                        {
                            totalBags++;
                            tmp.Add(bags.Where(b => b.Color == bag.Contains[i]).First());
                        }
                       
                    }
                }
                current.Clear();
                foreach (var bag in tmp)
                {
                    current.Add(bag);
                }
                tmp.Clear();
                
            }
            while (current.Count() > 0);

            Console.WriteLine("Answer part 2: " + totalBags);
            




        }
    }
    class Bag
    {
        public string Color { get; set; }
        public List<string> Contains { get; set; }
        public List<int> Amount { get; set; }
        public Bag()
        {
            Contains = new List<string>();
            Amount = new List<int>();
        }
        public override string ToString()
        {
            var containsString = "";
            for (int i = 0; i < Contains.Count(); i++)
            {
                containsString += $" {Amount[i]} {Contains[i]}";

            }
            {

            }
            return $"Color: {Color} Contains: {(Contains.Count() > 0 ? containsString : "No other bags")}";
        }
    }
}
