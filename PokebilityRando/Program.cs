using PokebilityUtility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokebilityRando
{
    internal class Program
    {
        static readonly string BAN_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bannedAbilities.txt");
        static readonly string LIST_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "allAbilities.txt");
        static readonly string DOUBLES_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "doublesAbilities.txt");
        static void Main(string[] args)
        {
            Console.WriteLine("Generating random ability...");
            List<string> ban_list = PokebilityUtils.FileParser(BAN_PATH);
            List<string> unformated_ability_list = PokebilityUtils.FileParser(LIST_PATH);
            List<string> doubles_ability_list = PokebilityUtils.FileParser(DOUBLES_PATH);
            List<string> ability_list = new List<string>();
            List<string> description_list = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder descriptionBuilder = new StringBuilder();
            bool description_flag = false;
            foreach (var item in unformated_ability_list)
            {
                description_flag = false;
                foreach (var letter in item)
                {
                    if (!(char.IsDigit(letter)) && !description_flag)
                    {
                        stringBuilder.Append(letter);
                        description_flag = false;
                    }
                    else 
                    {
                        description_flag = true;
                        descriptionBuilder.Append(!char.IsDigit(letter) ? letter : ' ');
                    }

                }
                ability_list.Add(stringBuilder.ToString().Trim());
                description_list.Add(descriptionBuilder.ToString().Trim());
                stringBuilder.Clear();
                descriptionBuilder.Clear();
            }
            string random_ability;
            ConsoleKeyInfo key;
            List<string> generated_abilities = new List<string>();
            bool remove_doubles_flag = RemoveDoubles();
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                }
                else
                {
                    do
                    {
                        random_ability = PokebilityUtils.RandomizeString(ability_list);
                    } while (PokebilityUtils.IsPresentInList(random_ability, ban_list)
                        || PokebilityUtils.IsPresentInList(random_ability, generated_abilities)
                        || (PokebilityUtils.IsPresentInList(random_ability, doubles_ability_list) && remove_doubles_flag)
                    );
                    int index = PokebilityUtils.FindIndex(random_ability, ability_list);
                    Console.ForegroundColor = PokebilityUtils.RandomColor(new List<ConsoleColor> {
                        ConsoleColor.Red,
                        ConsoleColor.Green, 
                        ConsoleColor.Magenta, 
                        ConsoleColor.Blue,
                        ConsoleColor.DarkCyan,
                        ConsoleColor.Black,
                        ConsoleColor.DarkRed,
                    });
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(random_ability);
                    Console.ResetColor();
                    Console.Write("\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(description_list[index]);
                    Console.ResetColor();
                    Console.Write("\n");
                    Console.WriteLine("...");
                    generated_abilities.Add(random_ability);
                }
            } while (key.Key != ConsoleKey.Escape); 
        }

        private static bool RemoveDoubles()
        {
            ConsoleKeyInfo key;
            Console.WriteLine("Are you removing doubles abilities (y/n)?");
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Doubles abilities will be removed.");
                return true;
            }
            else if (key.Key == ConsoleKey.N)
            {
                Console.WriteLine("Doubles abilities will not be removed.");
                return false;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input, please try again.");
                return RemoveDoubles();
            }
        }
    }
}
