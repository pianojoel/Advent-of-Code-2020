using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Aoc20
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split("\r\n\r\n").Select(i => i.Trim(' ', '\n', '\r'));
            
            List<Tile> tiles = new List<Tile>();

            foreach (var item in data)
            {
                var splitItem = item.Split("\r\n").Select(s => s.Trim(':', ' ', '\n', '\r')).ToArray();
                Tile tile = new Tile();
                tile.Number = int.Parse(splitItem[0].Split(" ")[1]);
                var top = splitItem[1];
                var bottom = splitItem.Last();
                var left = string.Join("", splitItem.Skip(1).Select(l => l.First()).ToArray());
                var right = string.Join("", splitItem.Skip(1).Select(l => l.Last()).ToArray());

                tile.Borders.Add(top);
                tile.Borders.Add(bottom);
                tile.Borders.Add(left);
                tile.Borders.Add(right);

                tiles.Add(tile);

            }
            bool isMatch;
            List<Tile> corners = new List<Tile>();
            foreach (var tile in tiles)
            {
                
                int counter = 0;
                foreach (var side in tile.Borders)
                {
                    isMatch = false;
                    
                    int count = 0;


                    foreach (var t in tiles)
                    {
                        if (t.Borders.Contains(side) && tile != t)
                        {
                            
                            isMatch = true;
                        }
                       
                        if (t.Borders.Contains(string.Join("", side.Reverse())) && tile != t)
                        {
                            
                            isMatch = true;
                        }
                    }
                    
                    if (isMatch) counter++;
                   
                }
               
                
                if (counter == 2) corners.Add(tile);
            }
            long answer = 1;
            foreach (var corner in corners)
            {
                answer *= corner.Number;
            }
            Console.WriteLine("Answer: " + answer);

           
        }
    }
    class Tile
    {
        public int Number { get; set; }
        public List<List<char>> Data { get; set; }
        public List<string> Borders { get; set; }
        public Tile()
        {
            Data = new List<List<char>>();
            Borders = new List<string>();
        }
    }
}
