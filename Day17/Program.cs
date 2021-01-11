using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace AoC17
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split('\n').Select(l => l.Trim(' ', '\r'));
            List<List<List<char>>> grid = new List<List<List<char>>>();
            List<List<char>> grid2d = new List<List<char>>();
            foreach (var line in data)
            {
                List<char> x = new List<char>();
                foreach (var c in line)
                {
                    x.Add(c);
                }
                grid2d.Add(x);
            }
            grid.Add(grid2d);


            PrintGrid(grid);
            
            
            for (int i = 0; i < 6; i++)
            {
                AddPadding(grid);
                UpdateGrid(grid);
            }

            PrintGrid(grid);
           



        }
        static void AddPadding(List<List<List<char>>>grid)
        {
            for (int z = 0; z < grid.Count(); z++)
            {
                for (int y = 0; y < grid[z].Count(); y++)
                {
                    grid[z][y].Add('.');
                    grid[z][y].Insert(0, '.');
                }

                grid[z].Add(new List<char>());
                grid[z].Insert(0, new List<char>());
                foreach (var x in grid[z][1])
                {
                    grid[z][0].Add('.');
                    grid[z].Last().Add('.');
                }
            }

            grid.Add(new List<List<char>>());
            grid.Insert(0, new List<List<char>>());
            for (int y = 0; y < grid[1].Count(); y++)
            {
                List<char> tmpList = new List<char>();
                for (int x = 0; x < grid[1][1].Count(); x++)
                {
                    tmpList.Add('.');
                }
                grid[0].Add(tmpList);
                List<char> tmpList2 = new List<char>();
                for (int x = 0; x < grid[1][1].Count(); x++)
                {
                    tmpList2.Add('.');
                }
                grid.Last().Add(tmpList2);
            }

        }
        static void PrintGrid(List<List<List<char>>> grid)
        {
            int count = 0;
            for (int z = 0; z < grid.Count(); z++)
            {
                for (int y = 0; y < grid[z].Count(); y++)
                {
                    for (int x = 0; x < grid[z][y].Count(); x++)
                    {
                        if (grid[z][y][x] == '#') count++;
                        Console.Write(grid[z][y][x]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Active: " + count);
        }
        static void PrintTmpGrid(List<List<List<int>>> grid)
        {
            for (int z = 0; z < grid.Count(); z++)
            {
                for (int y = 0; y < grid[z].Count(); y++)
                {
                    for (int x = 0; x < grid[z][y].Count(); x++)
                    {
                        Console.Write(grid[z][y][x]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void UpdateGrid(List<List<List<char>>> grid)
        {
            List<List<List<int>>> tmpGrid = new List<List<List<int>>>();
            for (int z = 0; z < grid.Count(); z++)
            {
                tmpGrid.Add(new List<List<int>>());
                for (int y = 0; y < grid[z].Count(); y++)
                {
                    tmpGrid[z].Add(new List<int>());
                    for (int x = 0; x < grid[z][y].Count(); x++)
                    {
                        int count = 0;
                        //Check for neighbours here
                        for(int zz= -1; zz < 2; zz++)
                        {
                            if (z + zz < 0 || z + zz >= grid.Count()) continue;
                            for (int yy = -1; yy < 2; yy++)
                            {
                                if (y + yy < 0 || y + yy >= grid[z].Count()) continue;
                                for (int xx = -1; xx < 2; xx++)
                                {
                                    if (x + xx < 0 || x + xx >= grid[z][y].Count()) continue;
                                    if (xx == 0 && yy == 0 && zz == 0) continue;
                                    if (grid[z + zz][y + yy][x + xx] == '#') count++;
                                                                                                           
                                }
                            }
                        }
                        
                        tmpGrid[z][y].Add(count);
                    }
                    
                }
                
            }
            //PrintTmpGrid(tmpGrid);

            for (int z = 0; z < grid.Count(); z++)
            {
                for (int y = 0; y < grid[z].Count(); y++)
                {
                    for (int x = 0; x < grid[z][y].Count(); x++)
                    {
                       if (grid[z][y][x] == '#')
                        {
                            if (tmpGrid[z][y][x] < 2 || tmpGrid[z][y][x] > 3)
                            {
                                grid[z][y][x] = '.';
                            }
                           
                        }
                        else if(grid[z][y][x] == '.')
                        {
                            if(tmpGrid[z][y][x] == 3)
                            {
                                grid[z][y][x] = '#';
                            }
                        }
                    }
                    
                }
                
            }

        }

    }
}
