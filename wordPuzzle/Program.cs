using System;
using System.Collections.Generic;
using System.IO;

namespace wordPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("ordliste.txt");
            var list = new List<string>();
            string prevWord = "";
            foreach (var line in lines)
            {
                string[] word = line.Split('\t');
                if(word[1] == prevWord) continue;
                if (word[1].Length is < 7 or > 10) continue;
                if(word[1].Contains("-")) continue;
                list.Add(word[1]);
                prevWord = word[1];
            }

            string[] returnArray = list.ToArray();

            foreach (var line in returnArray)
            {
                Console.WriteLine(line);   
            }
        }
    }
}
