using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoC14
{
    class Program
    {
        static void Main(string[] args)
        {

           

            var data = File.ReadAllText("input.txt").Split('\n').Select(l => l.Trim('\r', ' '));

            string mask = "";
            Dictionary<string, long> keyvalues = new Dictionary<string, long>();
            
            foreach (var line in data)
            {
                var info = line.Split('=').Select(i => i.Trim()).ToArray();
                if (info[0] == "mask")
                {
                    mask = info[1];
                }
                else
                {
                    //part 2:
                    var address = int.Parse(info[0].Substring(info[0].IndexOf('[') +1, info[0].IndexOf(']') - info[0].IndexOf('[') -1));

                    var addresses = MaskOverwrite2(mask, toBinary(address));
                    foreach (var a in addresses)
                    {
                        keyvalues[$"mem[{toDecimal(a)}]"] = int.Parse(info[1]);
                    }
                    
                    //part 1:
                    //keyvalues[info[0]] = toDecimal(MaskOverwrite(mask, toBinary(int.Parse(info[1]))));
                   
                }
            }

            foreach (var item in keyvalues)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            long sum = keyvalues.Select(kv => kv.Value).Sum();
            Console.WriteLine(sum);
        }
        static string toBinary(int value)
        {
            string binary = "";

            while (value > 0)
            {
                var r = (value % 2).ToString();
               
                binary = binary.Insert(0, r.ToString());
                
                value /= 2;                
            }                        
            return binary.PadLeft(36,'0');
        }
        static long toDecimal (string binary)
        {
            long value = 0;
            long j = 1;
            for (int i = 1; i <= binary.Length; i++)              
            {
                if (binary[binary.Length -i] == '1')
                {
                    value += j;
                }
                j *= 2;
            }
            return value;
        }
        static string MaskOverwrite(string mask, string binary)
        {
            string newBinary = "";
            for (int i = 0; i < binary.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    newBinary += binary[i];
                }
                else
                {
                    newBinary += mask[i];
                }
            }
            return newBinary;
        }

        static List<string> MaskOverwrite2(string mask, string binary)
        {
            
            List<string> newBinaries = new List<string>();
            string newBinary = "";
            for (int i = 0; i < binary.Length; i++)
            {
                
                if (mask[i] == '0')
                {
                    newBinary += binary[i];
                }
                else if (mask[i] == '1')
                {
                    newBinary += '1';
                }
                else if(mask[i] == 'X')
                {
                    newBinary += 'X';
                }
                
            }
            newBinaries.Add(newBinary);
            while (newBinaries.Any(nb => nb.Contains('X')))
            {
                List<string> tmpBin = new List<string>();
                foreach (var current in newBinaries)
                {
                    string child0 = current.Substring(0, current.IndexOf('X')) + "0" + current.Substring(current.IndexOf('X') + 1);
                    string child1 = current.Substring(0, current.IndexOf('X')) + "1" + current.Substring(current.IndexOf('X') + 1);
                        
                        
                    tmpBin.Add(child0);
                    tmpBin.Add(child1);
                }
                newBinaries.Clear();
                newBinaries.AddRange(tmpBin);
                tmpBin.Clear();

            }
            
            return newBinaries;
            


        }
    }
}
