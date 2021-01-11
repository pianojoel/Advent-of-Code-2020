using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AoC5
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt");
            var lines = data.Split("\n");
           
            var result = lines
                .Select(a => getSeatNumber(a))
                .OrderBy(s => s)
                .ToList();
            
            Console.WriteLine("Highest seat Id: " + result.Max());
            
            var newResult = result
                .OrderBy(s => s)
                .Select((s, i) => i == 0 ? new { s, V = true } : new { s, V = result[i - 1] + 1 == s })
                .Where(a => a.V == false)
                .Select(v => v.s -1)
                .First();

            Console.WriteLine("Seat Number: " + newResult);
           
        }
        static int getSeatNumber(string seatCode)
        {
            var rows = Enumerable.Range(0, 128).ToList();
            var cols = Enumerable.Range(0, 8).ToList();

            foreach (var c in seatCode)
            {
                switch (c)
                {
                    case 'F':
                        rows = rows.GetRange(0, rows.Count() / 2);
                        break;
                    case 'B':
                        rows = rows.GetRange(rows.Count() / 2, rows.Count() / 2);
                        break;
                    case 'R':
                        cols = cols.GetRange(cols.Count() / 2, cols.Count() / 2);
                        break;
                    case 'L':
                        cols = cols.GetRange(0, cols.Count() / 2);
                        break;
                }
                
            }

            return rows.First() * 8 + cols.First();
        }
        
    }
}
