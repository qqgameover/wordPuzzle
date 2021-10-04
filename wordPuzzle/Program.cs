using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                if (word[1] == prevWord) continue;
                if (word[1].Length is < 7 or > 10) continue;
                if (word[1].Contains("-")) continue;
                list.Add(word[1]);
                prevWord = word[1];
            }

            var endArray = ReturnArray(list);
            var dist = endArray.Distinct().ToArray();
            foreach (var s in dist)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(dist.Length);
        }

        private static string[] ReturnArray(List<string> list)
        {
            string[] returnArray = list.ToArray();
            Random rng = new Random();
            var superList = new List<string>();
            int target = 200;
            while (target > superList.Count)
            {
                var item = GetRandomWords(rng, returnArray);
                var fixedItem = string.Join(" ", item);
                superList.Add(fixedItem);
            }

            string[] ourList = superList.ToArray();
            return ourList;
        }

        private static string[] GetRandomWords(Random rng, string[] returnArray)
        {
            int index = rng.Next(returnArray.Length);
            string randomWord = GetNewRandomWord(returnArray);
            string substringRandomWord = randomWord.Substring(randomWord.Length - 3).ToLower();
            if (!IsValidWord(returnArray, substringRandomWord)) GetNewRandomWord(returnArray);
            var list = new List<string>();
            foreach (var word in returnArray)
            {
                string substringAllWords = word.Substring(0, 3);
                if (substringRandomWord == substringAllWords)
                {
                    list.Add(randomWord);
                    list.Add(word);
                    break;
                }
            }

            string[] returnValue = list.ToArray();
            return returnValue;
        }

        private static bool IsValidWord(string[] wordArray, string substring)
        {
            return wordArray.Any(w => w.ToLower().Contains(substring));
        }

        private static string GetNewRandomWord(string[] wordArray)
        {
            Random rng = new Random();
            int index = rng.Next(wordArray.Length);
            string randomWord = wordArray[index];
            string substringRandomWord = randomWord.Substring(randomWord.Length - 3).ToLower();
            while (!IsValidWord(wordArray, substringRandomWord))
            {
                index = rng.Next(wordArray.Length);
                randomWord = wordArray[index];
                substringRandomWord = randomWord.Substring(randomWord.Length - 3).ToLower();
            }

            return randomWord;
        }
    }
}
