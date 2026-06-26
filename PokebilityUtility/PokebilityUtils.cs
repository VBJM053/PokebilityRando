
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokebilityUtility
{
    public static class PokebilityUtils
    {
        public static List<string> FileParser(string filePath)
        {
            List<string> lines = new List<string>();
            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            return lines;
        }

        public static string RandomizeString(List<string> lines)
        {
            string randomLine;
            try
            {
                Random rand = new Random();
                randomLine = lines[rand.Next(lines.Count)];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error randomizing string: " + ex.Message);
                return null;
            }
            return randomLine;
        }

        public static bool IsPresentInList(string item, List<string> list)
        {
            item = item.Trim().ToLower();
            return list.Any(line => line.Trim().ToLower() == item);
        }

        public static int FindIndex(string item, List<string> list)
        {
            item = item.Trim().ToLower();
            foreach (var line in list)
            {
                if (line.Trim().ToLower() == item)
                {
                    return list.IndexOf(line);
                }
            }
            return -1;
        }

        public static ConsoleColor RandomColor(List<ConsoleColor> color_list)
        {
            Random random = new Random();
            return color_list[random.Next(color_list.Count)];
        }
    }
}
