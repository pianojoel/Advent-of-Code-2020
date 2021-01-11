using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoC24
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split("\r\n");

            List<Tile> tiles = new List<Tile>();

            foreach (var line in data)
            {
                int pos = 0;
                string dir = "";
                //Console.WriteLine(line);
                var current = new Tile(0, 0);
                while (pos < line.Length)
                {
                    dir = line.Length - pos >= 2 ? line.Substring(pos, 2) : line.Substring(pos, 1);

                    if (!new string[] { "nw", "ne", "se", "sw" }.Contains(dir))
                    {
                        dir = line.Substring(pos, 1);
                        pos++;
                    }
                    else
                    {
                        pos += 2;
                    }

                    switch (dir)
                    {
                        case "nw":
                            //Console.WriteLine("North West");
                            current = new Tile(current.X - 1, current.Y - 1);
                            break;

                        case "ne":
                            
                            current = new Tile(current.X + 1, current.Y - 1);
                            //Console.WriteLine("North East");
                            break;
                            
                        case "se":
                            
                            current = new Tile(current.X + 1, current.Y + 1);
                            //Console.WriteLine("South East");
                            break;

                        case "sw":
                            
                                current = new Tile(current.X - 1, current.Y + 1);
                           
                            //Console.WriteLine("South West");
                            break;
                        case "e":
                           
                                current = new Tile(current.X + 2, current.Y);
                           
                            //Console.WriteLine("East");
                            break;
                        case "w":
                            
                                current = new Tile(current.X - 2, current.Y);
                           
                            //Console.WriteLine("West");
                            break;

                    }

                    //switch (dir)
                    //{
                    //    case "nw":
                    //        Console.WriteLine("North West");
                    //        if (tiles.Any(t => t.X == current.X - 1 && t.Y == current.Y - 1))
                    //        {
                    //            current = tiles.Where(t => t.X == current.X - 1 && t.Y == current.Y - 1).First();
                    //            current.Flip();
                    //        }
                    //        else
                    //        {
                    //            current = new Tile(current.X - 1, current.Y - 1);
                    //            tiles.Add(current);
                    //        }
                    //        break;
                    //    case "ne":
                    //        if (tiles.Any(t => t.X == current.X + 1 && t.Y == current.Y - 1))
                    //        {
                    //            current = tiles.Where(t => t.X == current.X + 1 && t.Y == current.Y - 1).First();
                    //            current.Flip();
                    //        }
                    //        else
                    //        {
                    //            current = new Tile(current.X + 1, current.Y - 1);
                    //            tiles.Add(current);
                    //        }
                    //        Console.WriteLine("North East");
                    //        break;
                    //    case "se":
                    //        if (tiles.Any(t => t.X == current.X + 1 && t.Y == current.Y + 1))
                    //        {
                    //            current = tiles.Where(t => t.X == current.X + 1 && t.Y == current.Y + 1).First();
                    //            current.Flip();
                    //        }
                    //        else
                    //        {
                    //            current = new Tile(current.X + 1, current.Y + 1);
                    //            tiles.Add(current);
                    //        }
                    //        Console.WriteLine("South East");
                    //        break;
                    //    case "sw":
                    //        if (tiles.Any(t => t.X == current.X - 1 && t.Y == current.Y + 1))
                    //        {
                    //            current = tiles.Where(t => t.X == current.X - 1 && t.Y == current.Y + 1).First();
                    //            current.Flip();
                    //        }
                    //        else
                    //        {
                    //            current = new Tile(current.X - 1, current.Y + 1);
                    //            tiles.Add(current);
                    //        }
                    //        Console.WriteLine("South West");
                    //        break;
                    //    case "e":
                    //        if (tiles.Any(t => t.X == current.X + 2 && t.Y == current.Y))
                    //        {
                    //            current = tiles.Where(t => t.X == current.X + 2 && t.Y == current.Y).First();
                    //            current.Flip();
                    //        }
                    //        else
                    //        {
                    //            current = new Tile(current.X +2, current.Y);
                    //            tiles.Add(current);
                    //        }
                    //        Console.WriteLine("East");
                    //        break;
                    //    case "w":
                    //        if (tiles.Any(t => t.X == current.X - 2 && t.Y == current.Y))
                    //        {
                    //            current = tiles.Where(t => t.X == current.X - 2 && t.Y == current.Y).First();
                    //            current.Flip();
                    //        }
                    //        else
                    //        {
                    //            current = new Tile(current.X - 2, current.Y);
                    //            tiles.Add(current);
                    //        }
                    //        Console.WriteLine("West");
                    //        break;

                    //}
                }
                if (tiles.Any(t => t.X == current.X && t.Y == current.Y))
                {
                    current = tiles.Where(t => t.X == current.X && t.Y == current.Y).First();
                    current.Flip();
                }
                else
                {
                    
                    tiles.Add(current);
                }


                //Console.ReadKey();
            }
            //Answer Part 1:
            Console.WriteLine("Answer part 1: " + tiles.Where(t => t.Color == "black").Count());
            Console.WriteLine();

            tiles.RemoveAll(t => t.Color == "white");


            for (int i = 0; i < 100; i++)
            {

                Console.WriteLine(i);
                List<Tile> tmpList = new List<Tile>();
                foreach (var tile in tiles) //Pad with white tiles
                {

                    if (!tiles.Concat(tmpList)
                        .Any(t => t.X == tile.X - 1 && t.Y == tile.Y - 1))//Northwest
                    {
                        Tile tmpTile = new Tile(tile.X - 1, tile.Y - 1);
                        tmpTile.Color = "white";
                        tmpList.Add(tmpTile);
                    }
                    if (!tiles.Concat(tmpList).Any(t => t.X == tile.X + 1 && t.Y == tile.Y - 1))//Northeast
                    {
                        Tile tmpTile = new Tile(tile.X + 1, tile.Y - 1);
                        tmpTile.Color = "white";
                        tmpList.Add(tmpTile);
                    }
                    if (!tiles.Concat(tmpList).Any(t => t.X == tile.X - 1 && t.Y == tile.Y + 1))//Southwest
                    {
                        Tile tmpTile = new Tile(tile.X - 1, tile.Y + 1);
                        tmpTile.Color = "white";
                        tmpList.Add(tmpTile);
                    }
                    if (!tiles.Concat(tmpList).Any(t => t.X == tile.X + 1 && t.Y == tile.Y + 1))//Southeast
                    {
                        Tile tmpTile = new Tile(tile.X + 1, tile.Y + 1);
                        tmpTile.Color = "white";
                        tmpList.Add(tmpTile);
                    }
                    if (!tiles.Concat(tmpList).Any(t => t.X == tile.X - 2 && t.Y == tile.Y))//west
                    {
                        Tile tmpTile = new Tile(tile.X - 2, tile.Y);
                        tmpTile.Color = "white";
                        tmpList.Add(tmpTile);
                    }
                    if (!tiles.Concat(tmpList).Any(t => t.X == tile.X + 2 && t.Y == tile.Y))//east
                    {
                        Tile tmpTile = new Tile(tile.X + 2, tile.Y);
                        tmpTile.Color = "white";
                        tmpList.Add(tmpTile);
                    }


                }


                tiles.AddRange(tmpList);

                foreach (var tile in tiles)
                {
                    var adjList = tiles.Where(t => ((t.X == tile.X - 1 || t.X == tile.X + 1) && (t.Y == tile.Y - 1 || t.Y == tile.Y + 1)) || ((t.X == tile.X - 2 || t.X == tile.X + 2) && t.Y == tile.Y)).ToList();
                    int blackAdj = adjList.Where(t => t.Color == "black").Count();
                    

                    if (tile.Color == "black")
                    {
                        if (blackAdj == 0 || blackAdj > 2)
                        {
                            tile.ToFlip = true;
                        }
                    }
                    if (tile.Color == "white")
                    {
                        if (blackAdj == 2)
                        {
                            tile.ToFlip = true;
                        }
                    }
                }
                foreach (var tile in tiles)
                {
                    if (tile.ToFlip)
                    {
                        tile.Flip();
                        tile.ToFlip = false;
                    }
                }
                tiles.RemoveAll(t => t.Color == "white");
                //tiles.ForEach(t => t.ToFlip = false);

                Console.WriteLine(tiles.Count());
                //Console.ReadKey();
            }
        }
    }
    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }
        public bool ToFlip { get; set; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
            Color = "black";
            ToFlip = false;

        }
        public void Flip()
        {
            if (Color == "black")
            {
                Color = "white";
            }
            else
            {
                Color = "black";
            }
        }
    }
}
