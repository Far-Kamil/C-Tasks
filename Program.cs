using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
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
            if (convertingString.Count()>0)
            {
                Console.WriteLine($"Invalid characters: {string.Join(", ", convertingString)}");
                return;
            }
            Console.WriteLine("Final string: " + ConvertingString(input));
        }
        private static HashSet<char> GetLowerEnglish(string input) 
        {
            var lowerEnglish = "abcdefghijklmnopqrstuvwxyz";
            var nonLowerEnglish = new HashSet<char>();    
            foreach (var ch in input) 
            {
                if(!lowerEnglish.Contains(ch))
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
