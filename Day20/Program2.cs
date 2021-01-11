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


                foreach (var line in splitItem.Skip(1))
                {
                    tile.Data.Add(line);
                    
                }

                tile.updateBorders();
                tiles.Add(tile);

            }
            var corners = getCorners(tiles);

            List<List<Tile>> arrangedTiles = new List<List<Tile>>();
            Tile activeTile = corners.First();
            activeTile.updateBorders();
           
            while (true) //Find rotation of first tile
            {
                activeTile.Print();
                if (tiles.Where(t => t.Number != activeTile.Number).Any(t => t.Borders.Contains(activeTile.Right) || t.Borders.Contains(String.Join("", activeTile.Right.Reverse()))) && tiles.Where(t => t.Number != activeTile.Number).Any(t => t.Borders.Contains(activeTile.Bottom) || t.Borders.Contains(String.Join("", activeTile.Bottom.Reverse()))))
                {
                    Console.WriteLine("correct rotation and flip found");
                    break;
                }
                activeTile = Flip(activeTile);
                
                if (tiles.Where(t => t.Number != activeTile.Number).Any(t => t.Borders.Contains(activeTile.Right) || t.Borders.Contains(String.Join("", activeTile.Right.Reverse()))) && tiles.Where(t => t.Number != activeTile.Number).Any(t => t.Borders.Contains(activeTile.Bottom) || t.Borders.Contains(String.Join("", activeTile.Bottom.Reverse()))))
                {
                    Console.WriteLine("correct rotation and flip found");
                    break;
                }
                

                activeTile = Flip(activeTile);
                activeTile = rotateRight(activeTile);
            }
            var topRow = new List<Tile>();
            topRow.Add(activeTile);

            
            while (true) //Find tiles for top row
            {
                 var currentTile = tiles.Where(t => (t.Borders.Contains(activeTile.Right) || t.Borders.Contains(String.Join("", activeTile.Right.Reverse()))) && !topRow.Any(tr => tr.Number == t.Number)).First();
                while (true)
                {
                    if (currentTile.Left == activeTile.Right)
                    {
                        topRow.Add(currentTile);
                        activeTile = currentTile;
                        break;
                    }
                    currentTile = Flip(currentTile);
                    if (currentTile.Left == activeTile.Right)
                    {
                        topRow.Add(currentTile);
                        activeTile = currentTile;
                        break;
                    }
                    currentTile = Flip(currentTile);
                    currentTile = rotateRight(currentTile);

                }
                if (corners.Any(c => c.Number == currentTile.Number))
                {
                    break;
                }

            }

            foreach (var item in topRow)
            {
                List<Tile> myList = new List<Tile>();
                myList.Add(item);
                arrangedTiles.Add(myList);
                
            }

           
            
            //Arrange remaining tiles
            while (arrangedTiles.Select(l => l.Count()).Sum() < tiles.Count()) 
            {
                for (int i = 0; i < arrangedTiles.Count(); i++)
                {
                    activeTile = arrangedTiles[i].Last();
                    activeTile.updateBorders();
                    
                    var currentTile = tiles.Where(t => t.Number != activeTile.Number && (t.Borders.Contains(activeTile.Bottom) || t.Borders.Contains(String.Join("", activeTile.Bottom.Reverse())))).First();

                    while (true)
                    {
                        if (currentTile.Top == activeTile.Bottom)
                        {
                            arrangedTiles[i].Add(currentTile);
                            
                            break;
                        }
                        currentTile = Flip(currentTile);
                        if (currentTile.Top == activeTile.Bottom)
                        {
                            arrangedTiles[i].Add(currentTile);
                           
                            break;
                        }
                        currentTile = Flip(currentTile);
                        currentTile = rotateRight(currentTile);


                    }
                }
            }




            
            //Convert from tile object to string list, remove borders
            List<string> image = new List<string>();

            for (int i = 0; i < arrangedTiles[0].Count(); i++)
            {
                for (int j = 1; j < arrangedTiles[0][0].Data.Count()-1; j++)
                {
                    string row = "";
                    for (int k = 0; k < arrangedTiles.Count(); k++)
                    {
                        row += arrangedTiles[k][i].Data[j].Substring(1,8);

                    }
                    image.Add(row);
                }
            }

            //count monsters
            int monsters = 0;
            while (true)
            {
                monsters = CheckMonsters(image);
                if (monsters > 0) break;
                image = FlipImage(image);
                if (monsters > 0) break;
                image = FlipImage(image);
                image = RotateImage(image);

            }
            Console.WriteLine(CheckMonsters(image) + " monsters in image");

            int monsterhashes = monsters * 15;

            int answer = image.Select(l => l.Count(c => c == '#')).Sum() - monsterhashes;

            Console.WriteLine("answer is: " + answer);

           



        }
        static int CheckMonsters(List<string> image)
        {
            int monsters = 0;
            string tmpImg = "";
            for (int i = 0; i < image.Count() - 2; i++)
            {
                for (int j = 18; j < image[i].Length -1; j++)
                {
                    tmpImg = $"{image[i][j]}";
                    tmpImg += $"{image[i + 1][j - 18]}{image[i + 1][j - 13]}{image[i + 1][j - 13]}{image[i + 1][j - 13]}{image[i + 1][j - 12]}{image[i + 1][j - 7]}{image[i + 1][j - 6]}{image[i + 1][j - 1]}{image[i + 1][j]}{image[i + 1][j +1]}";
                    tmpImg += $"{image[i + 2][j - 17]}{image[i + 2][j - 14]}{image[i + 2][j - 11]}{image[i + 2][j - 8]}{image[i + 2][j - 5]}{image[i + 2][j - 2]}";
                    if (tmpImg.All(c => c == '#'))
                    {
                        monsters++;
                    }
                }
            }
            return monsters;
        }
        static void printImage(List<string> image)
        {
            foreach (var row in image)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }
        static List<string> RotateImage(List<string> image)
        {
            List<string> tmpImg = new List<string>();
            
            for (int i = 0; i < image.Count(); i++)
            {
                tmpImg.Add("");
                for (int j = image[i].Length -1 ; j >= 0; j--)
                {
                    tmpImg[i] += image[j][i];

                }
            }
            
            return tmpImg;

        }
        static List<string> FlipImage(List<string> image)
        {

            List<string> tmpImg = new List<string>();
           
            for (int i = 0; i < image.Count(); i++)
            {
                tmpImg.Add(string.Join("", image[i].Reverse()));

            }
            
            return tmpImg;
        }

        static Tile rotateRight(Tile tile)
        {
            Tile tmpTile = new Tile();
            tmpTile.Number = tile.Number;
            for (int i = 0; i < 10; i++)
            {
                tmpTile.Data.Add("");
                for (int j = 9; j >= 0; j--)
                {
                    tmpTile.Data[i] += tile.Data[j][i];

                }
            }
            tmpTile.updateBorders();
            return tmpTile;
        }
        static Tile Flip(Tile tile)
        {
            Tile tmpTile = new Tile();
            tmpTile.Number = tile.Number;
            for (int i = 0; i < 10; i++)
            {
                tmpTile.Data.Add(string.Join("", tile.Data[i].Reverse()));
                
            }
            tmpTile.updateBorders();
            return tmpTile;

        }
        static List<Tile> getCorners(List<Tile> tiles)
        {
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
            return corners;
        }
    }
    class Tile
    {
        public int Number { get; set; }
        public List<string> Data { get; set; }
        public List<string> Borders { get; set; }
        public string Top { get; set; }
        public string Right { get; set; }
        public string Left { get; set; }
        public string Bottom { get; set; }

        public Tile()
        {
            Data = new List<string>();
            Borders = new List<string>();
        }
        public void updateBorders()
        {
            Top = Data[0];
            Right = String.Join("", Data.Select(l => l.Last()));
            Left = String.Join("", Data.Select(l => l.First()));
            Bottom = Data[9];
            Borders.Clear();
            Borders.Add(Top);
            Borders.Add(Right);
            Borders.Add(Left);
            Borders.Add(Bottom);

        }
        public void Print()
        {
            foreach (var line in Data)
            {
                foreach(var c in line)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
 