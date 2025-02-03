using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string input=Console.ReadLine();
            string convertingString = ConvertingString(input);
            Console.WriteLine("Final string: " + convertingString);
            
        }
        static string ConvertingString(string input) 
        {
            string convertingString;
            if (input.Length % 2 ==0)
            {
                int halfLength = input.Length / 2;
                string firststring=input.Substring(0, halfLength);
                string secondstring=input.Substring(halfLength);
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
        static string ReverseString(string input) 
        {
            char [] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
