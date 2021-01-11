using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace AoC4
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine(ValidPassports());


        }

        static int ValidPassports()
        {
            var data = File.ReadAllText(@"input.txt");

            string[] passports = data.Split("\r\n\r\n");
            List<Passport> pList = new List<Passport>();
            foreach (var passport in passports)
            {
                
                var lines = passport.Split("\n");
                Passport p = new Passport();
                foreach (var line in lines)
                {
                    
                    var posts = line.Split(" ");
                    foreach (var post in posts)
                    {
                        var keyvalue = post.Split(":");
                        switch (keyvalue[0])
                        {
                            case "byr":
                                p.byr = keyvalue[1].Trim();
                                break;
                            case "iyr":
                                p.iyr = keyvalue[1].Trim();
                                break;
                            case "eyr":
                                p.eyr = keyvalue[1].Trim();
                                break;
                            case "hgt":
                                p.hgt = keyvalue[1].Trim();
                                break;
                            case "hcl":
                                p.hcl = keyvalue[1].Trim();
                                break;
                            case "ecl":
                                p.ecl = keyvalue[1].Trim();
                                break;
                            case "pid":
                                p.pid = keyvalue[1].Trim();
                                break;
                            case "cid":
                                p.cid = keyvalue[1].Trim();
                                break;

                        }
                        
                    }
                }
               
                pList.Add(p);
               
            }


            //For part 1:
            //return pList.Where(p => p.byr != null && p.iyr != null && p.eyr != null && p.hgt != null && p.hcl != null && p.ecl != null && p.pid != null).Count();
            //For part 2:
            return pList.Where(p => p.IsValid()).Count();
        }

        class Passport
        {
            public string byr { get; set; }
            public string iyr { get; set; }
            public string eyr { get; set; }
            public string hgt { get; set; }
            public string hcl { get; set; }
            public string ecl { get; set; }
            public string pid { get; set; }
            public string cid { get; set; }

            public bool IsValid()
            {
                if (new[] { byr, iyr, eyr, hgt, hcl, ecl, pid }.Any(a => a is null)) return false;

                bool byrB;
                try
                {
                    int n = int.Parse(byr);
                    if (n >= 1920 && n <= 2002)
                    {
                        byrB = true;
                    }
                    else
                    {
                        byrB = false;
                    }
                }
                catch
                {
                    byrB = false;
                }
               

                bool iyrB;
                try
                {
                    int n = int.Parse(iyr);
                    if (n >= 2010 && n <= 2020)
                    {
                        iyrB = true;
                    }
                    else
                    {
                        iyrB = false;
                    }
                }
                catch
                {
                    iyrB = false;
                }
               
                bool eyrB;
                try
                {
                    int n = int.Parse(eyr);
                    if (n >= 2020 && n <= 2030)
                    {
                        eyrB = true;
                    }
                    else
                    {
                        eyrB = false;
                    }
                }
                catch
                {
                    eyrB = false;
                }
               

                bool hgtB = false;
                try
                {
                    string u = hgt?.Substring(hgt.Length - 2);
                    if (u == "cm")
                    {
                        int h = int.Parse(hgt.Substring(0, 3));
                        if (h >= 150 && h <= 193)
                        {
                            hgtB = true;
                        }
                    }
                    else if (u == "in")
                    {
                        int h = int.Parse(hgt.Substring(0, 2));
                        if (h >= 59 && h <= 76)
                        {
                            hgtB = true;
                        }
                    }

                }
                catch
                {
                }
               

                bool hclB = false;
                if (hcl.Length == 7 && hcl[0] == '#')
                {
                    if (hcl.Substring(1).All(c => char.IsDigit(c) || "abcdef".Contains(c)))
                    {
                        hclB = true;
                    }
                }
                
                bool eclB = false;
                if (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(ecl))
                {
                    eclB = true;
                }
                

                bool pidB = false;
                if (pid.Length == 9 && pid.All(c => char.IsDigit(c)))
                {
                    pidB = true;
                }
                



                return byrB && iyrB && eyrB && hgtB && hclB && eclB && pidB;


            }

            public override string ToString()
            {
                return $"byr: {byr} iyr: {iyr} eyr: {eyr} hgt: {hgt} hcl: {hcl} ecl: {ecl} pid: {pid} cid: {cid}";
            }

        }
    }
}
