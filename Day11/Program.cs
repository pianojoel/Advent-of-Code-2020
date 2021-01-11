using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoC11
{
    
    class Program
    {
        static List<List<char>> grid = new List<List<char>>();
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split("\n").Select(l => l.Trim(' ', '\r'));
           
            foreach (var line in data)
            {
                var row = new List<char>();
                foreach (var c in line)
                {
                    row.Add(c);
                }
                grid.Add(row);
            }

            foreach (var row in grid)
            {
                foreach (var c in row)
                {
                    if (c == '.')
                    {
                        Console.Write(" ");
                    }
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
            //while (true)
            //{
            //    Console.WriteLine(UpdateGrid(grid) + " changes");
            //    Console.ReadKey();
            //}
            int a = 1;
            while (a > 0)
            {
               a = UpdateGrid2();
                //Console.WriteLine(a);
                // Console.ReadKey();
                foreach (var row in grid)
                {
                    foreach (var c in row)
                    {
                        Console.Write(c);
                    }
                    Console.WriteLine();
                }
            }

        }

        static int UpdateGrid2()
        {
            List<List<char>> tmpGrid = new List<List<char>>();
            int changes = 0;
            var adj = new List<char>();
            
            for (int i = 0; i < grid.Count(); i++)
            {
                tmpGrid.Add(new List<char>());
                for (int j = 0; j < grid[i].Count(); j++)
                {
                    adj.Clear();
                    
                    int iDir = -1;
                    int jDir = -1;
                    while (i + iDir >= 0 && j + jDir >= 0)
                    {
                        if (grid[i+iDir][j+jDir] == 'L' || grid[i + iDir][j + jDir] == '#') 
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                        
                        iDir--;
                        jDir--;
                    }
                   

                    iDir = -1;
                    jDir = 0;
                    while (i + iDir >= 0 && j + jDir >= 0)
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                        
                        iDir--;
                    }
                   

                    iDir = -1;
                    jDir = 1;
                    while (j + jDir < grid[i].Count() && i + iDir >= 0)
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                        
                        jDir++;
                        iDir--;
                    }
                   

                    iDir = 0;
                    jDir = -1;
                    while (i + iDir >= 0 && j + jDir >= 0)
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                        
                        jDir--;
                    }
                   

                    iDir = 0;
                    jDir = 1;
                    while (i + iDir >= 0 && j + jDir >= 0 && j + jDir < grid[i].Count())
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                       
                        jDir++;
                    }
                   

                    iDir = 1;
                    jDir = -1;
                    while (i + iDir >= 0 && j + jDir >= 0 && i + iDir < grid.Count())
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                        
                        iDir++;
                        jDir--;
                    }
                    

                    iDir = 1;
                    jDir = 0;
                    while (i + iDir >= 0 && j + jDir >= 0 && i + iDir < grid.Count())
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                        
                        iDir++;
                    }
                    
                    iDir = 1;
                    jDir = 1;
                    while (i + iDir < grid.Count() && j + jDir < grid[i].Count())
                    {
                        if (grid[i + iDir][j + jDir] == 'L' || grid[i + iDir][j + jDir] == '#')
                        {
                            adj.Add(grid[i + iDir][j + jDir]);
                            break;
                        }
                       
                        iDir++;
                        jDir++;
                    }
                   

                    switch (grid[i][j])
                    {
                        case 'L':
                            if (adj.Where(c => c == '#').Count() == 0)
                            {
                                tmpGrid[i].Add('#');
                                changes++;
                            }
                            else
                            {
                                tmpGrid[i].Add('L');
                            }
                            break;
                        case '#':
                            if (adj.Where(c => c == '#').Count() >=5)
                            {
                                tmpGrid[i].Add('L');
                                changes++;
                            }
                            else
                            {
                                tmpGrid[i].Add('#');
                            }
                            break;
                        case '.':
                            tmpGrid[i].Add('.');
                            break;
                    }
                   
                }

                
            }

            int occupied = 0;
            for (int i = 0; i < tmpGrid.Count(); i++)
            {
                for (int j = 0; j < tmpGrid[i].Count(); j++)
                {
                    grid[i][j] = tmpGrid[i][j];
                    if (grid[i][j] == '#')
                    {
                        occupied++;
                    }
                }

            }
            Console.WriteLine(occupied + " seats occupied");
            Console.WriteLine(changes + " made");


            return changes;

        }
        static int UpdateGrid(List<List<char>> grid)
        {
            List<List<char>> tmpGrid = new List<List<char>>();
            int changes = 0;
            for (int i = 0; i < grid.Count(); i++)
            {
                tmpGrid.Add(new List<char>());
                List<int> row = new List<int>();
                for (int j = 0; j < grid[i].Count(); j++)
                {
                    int adj = 0;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int l = -1; l < 2; l++)
                        {
                            if (i + k >= 0 && j  + l >= 0 && j + l < grid[i].Count() && i + k < grid.Count() && !(k == 0 && l == 0))
                            {
                                if (grid[i + k][j + l] == '#') adj++;
                               
                            }
                        }
                    }
                    switch (grid[i][j])
                    {
                        case 'L':
                            if (adj == 0)
                            {
                                tmpGrid[i].Add('#');
                                changes++;
                            }
                            else
                            {
                                tmpGrid[i].Add('L');
                            }
                            break;
                        case '#':
                            if (adj >= 4)
                            {
                                tmpGrid[i].Add('L');
                                changes++;
                            }
                            else
                            {
                                tmpGrid[i].Add('#');
                            }
                            break;
                        case '.':
                            tmpGrid[i].Add('.');
                            break;
                    }
                   
                }
            }
            int occupied = 0;
            for (int i = 0; i < tmpGrid.Count(); i++)
            {
                for (int j = 0; j < tmpGrid[i].Count(); j++)
                {
                    grid[i][j] = tmpGrid[i][j];
                    if (grid[i][j] == '#')
                    {
                        occupied++;
                    }
                }

            }
            Console.WriteLine(occupied + " seats occupied");
            return changes;
        }
    }
}
