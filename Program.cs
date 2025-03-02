using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();
            if (input == null)
                return;
            var convertingString = GetLowerEnglish(input);
            if (convertingString.Count() > 0)
            {
                Console.WriteLine($"Invalid characters: {string.Join(", ", convertingString)}");
                return;
            }
            var processedString = ConvertingString(input);
            Console.WriteLine(processedString);
            Console.WriteLine(GenerateCharacters(input));
            Console.WriteLine("Long string: "+GetString(input, "aeiouy"));
        }
        private static string GetString(string input, string boundaries) 
        {
            int right = -1;
            int left = -1;
            for(int i = 0; i < input.Length; i++) 
            {
                if (boundaries.Contains(input[i]))
                {
                    left = i;
                    break;
                }
            }
            for(int i = input.Length-1; i>=0; i--) 
            {
                if(boundaries.Contains(input[i]))
                {
                    right = i;
                    break;
                }
            }
            if (right == -1 || left == -1)
                return "";
            return input.Substring(left, right-left+1);
        }
        private static Dictionary<char, int> CountCharacters(string input)
        {
            var characters = new Dictionary<char, int>();
            foreach (var ch in input)
            {
                if (characters.ContainsKey(ch))
                    characters[ch]++;
                else characters.Add(ch, 1);
            }
            return characters;
        }
        private static string GenerateCharacters(string input)
        {
            var message = "Number of characters in string: ";
            var characters = CountCharacters(input);
            foreach (var ch in characters.Keys)
            {
                message += $"\n {ch}: {characters[ch]}";
            }
            return message;
        }
        private static HashSet<char> GetLowerEnglish(string input)
        {
            var lowerEnglish = "abcdefghijklmnopqrstuvwxyz";
            var nonLowerEnglish = new HashSet<char>();
            foreach (var ch in input)
            {
                if (!lowerEnglish.Contains(ch))
                    nonLowerEnglish.Add(ch);
            }
            return nonLowerEnglish;
        }
        private static string ConvertingString(string input)
        {
            string convertingString;
            if (input.Length % 2 == 0)
            {
                int halfLength = input.Length / 2;
                string firststring = input.Substring(0, halfLength);
                string secondstring = input.Substring(halfLength);
                string reversedFirstHalf = ReverseString(firststring);
                string reversedSecondHalf = ReverseString(secondstring);
                convertingString = reversedFirstHalf + reversedSecondHalf;
            }
            else
            {
                string reversedInput = ReverseString(input);
                convertingString = reversedInput + input;
            }
            return convertingString;
        }
        private static string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
