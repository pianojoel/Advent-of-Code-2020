using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AoC17b
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("input.txt").Split('\n').Select(l => l.Trim(' ', '\r'));
            List<List<List<List<char>>>> grid = new List<List<List<List<char>>>>();
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
            grid.Add(new List<List<List<char>>>());
            grid[0].Add(grid2d);


            PrintGrid(grid);


            for (int i = 0; i < 6; i++)
            {
                AddPadding(grid);
                UpdateGrid(grid);
                //PrintGrid(grid);
                
                //Console.ReadKey();
            }

            PrintGrid(grid);




        }
        static void AddPadding(List<List<List<List<char>>>> grid)
        {
            for (int w = 0; w < grid.Count(); w++)
            {


                for (int z = 0; z < grid[w].Count(); z++)
                {
                    for (int y = 0; y < grid[w][z].Count(); y++)
                    {
                        grid[w][z][y].Add('.');
                        grid[w][z][y].Insert(0, '.');
                    }

                    grid[w][z].Add(new List<char>());
                    grid[w][z].Insert(0, new List<char>());
                    foreach (var x in grid[w][z][1])
                    {
                        grid[w][z][0].Add('.');
                        grid[w][z].Last().Add('.');
                    }
                }
            

            grid[w].Add(new List<List<char>>());
            grid[w].Insert(0, new List<List<char>>());
            for (int y = 0; y < grid[w][1].Count(); y++)
            {
                List<char> tmpList = new List<char>();
                for (int x = 0; x < grid[w][1][1].Count(); x++)
                {
                    tmpList.Add('.');
                }
                grid[w][0].Add(tmpList);
                List<char> tmpList2 = new List<char>();
                for (int x = 0; x < grid[w][1][1].Count(); x++)
                {
                    tmpList2.Add('.');
                }
                grid[w].Last().Add(tmpList2);
            }
            }

            //grid.Add(new List<List<List<char>>>());
            //grid.Insert(0, new List<List<List<char>>>());
            var tmpCube = new List<List<List<char>>>();
            var tmpCube2 = new List<List<List<char>>>();

            for (int z = 0; z < grid[0].Count(); z++)
            {
                tmpCube.Add(new List<List<char>>());
                tmpCube2.Add(new List<List<char>>());

                for (int y = 0; y < grid[0][1].Count(); y++)
                {
                    tmpCube[z].Add(new List<char>());
                    tmpCube2[z].Add(new List<char>());

                    for (int x = 0; x < grid[0][1][1].Count(); x++)
                    {
                        tmpCube[z][y].Add('.');
                        tmpCube2[z][y].Add('.');

                    }
                }
            }
            grid.Add(tmpCube);
            grid.Insert(0, tmpCube2);

        }
        static void PrintGrid(List<List<List<List<char>>>> grid)
        {
            int count = 0;

            for (int w = 0; w < grid.Count(); w++)
            {

            
            for (int z = 0; z < grid[w].Count(); z++)
            {
                for (int y = 0; y < grid[w][z].Count(); y++)
                {
                    for (int x = 0; x < grid[w][z][y].Count(); x++)
                    {
                        if (grid[w][z][y][x] == '#') count++;
                        //Console.Write(grid[w][z][y][x]);
                    }
                    //Console.WriteLine();
                }
                //Console.WriteLine();
            }
            }
            Console.WriteLine("Active: " + count);
        }
        static void PrintTmpGrid(List<List<List<List<int>>>> grid)
        {
            for (int w = 0; w < grid.Count(); w++)
            {


                for (int z = 0; z < grid[w].Count(); z++)
                {
                    for (int y = 0; y < grid[w][z].Count(); y++)
                    {
                        for (int x = 0; x < grid[w][z][y].Count(); x++)
                        {
                            Console.Write(grid[w][z][y][x]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        static void UpdateGrid(List<List<List<List<char>>>> grid)
        {
            List<List<List<List<int>>>> tmpGrid = new List<List<List<List<int>>>>();
            for (int w = 0; w < grid.Count(); w++)
            {
                tmpGrid.Add(new List<List<List<int>>>());

                for (int z = 0; z < grid[w].Count(); z++)
                {
                    tmpGrid[w].Add(new List<List<int>>());
                    for (int y = 0; y < grid[w][z].Count(); y++)
                    {
                        tmpGrid[w][z].Add(new List<int>());
                        for (int x = 0; x < grid[w][z][y].Count(); x++)
                        {
                            int count = 0;
                            //Check for neighbours here

                            for (int ww = -1; ww < 2; ww++)
                            {
                                if (w + ww < 0 || w + ww >= grid.Count()) continue;

                                for (int zz = -1; zz < 2; zz++)
                                {
                                    if (z + zz < 0 || z + zz >= grid[w].Count()) continue;
                                    for (int yy = -1; yy < 2; yy++)
                                    {
                                        if (y + yy < 0 || y + yy >= grid[w][z].Count()) continue;
                                        for (int xx = -1; xx < 2; xx++)
                                        {
                                            if (x + xx < 0 || x + xx >= grid[w][z][y].Count()) continue;
                                            if (xx == 0 && yy == 0 && zz == 0 && ww == 0) continue;
                                            if (grid[w + ww][z + zz][y + yy][x + xx] == '#') count++;

                                        }
                                    }
                                }
                            }
                            tmpGrid[w][z][y].Add(count);
                        }

                    }

                }
            }
            //PrintTmpGrid(tmpGrid);
            for (int w = 0; w < grid.Count(); w++)
            {


                for (int z = 0; z < grid[w].Count(); z++)
                {
                    for (int y = 0; y < grid[w][z].Count(); y++)
                    {
                        for (int x = 0; x < grid[w][z][y].Count(); x++)
                        {
                            if (grid[w][z][y][x] == '#')
                            {
                                if (tmpGrid[w][z][y][x] < 2 || tmpGrid[w][z][y][x] > 3)
                                {
                                    grid[w][z][y][x] = '.';
                                }

                            }
                            else if (grid[w][z][y][x] == '.')
                            {
                                if (tmpGrid[w][z][y][x] == 3)
                                {
                                    grid[w][z][y][x] = '#';
                                }
                            }
                        }

                    }

                }
            }
        }
    }
}
