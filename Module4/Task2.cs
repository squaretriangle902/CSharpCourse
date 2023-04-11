using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Module4
{
    public static class Task2
    {
        public static void Run()
        {
            var sentence = "написать программу, которая";
            var occuranceString = "описание";
            Console.WriteLine(DoubleAllOccurances(sentence, occuranceString));
        }

        private static string DoubleAllOccurances(string sentence, string occuranceString)
        {
            var occuranceSet = occuranceString.ToHashSet();
            var stringBuilder = new StringBuilder();
            foreach (var symbol in sentence)
            {
                stringBuilder.Append(symbol);
                if (occuranceSet.Contains(symbol))
                {
                    stringBuilder.Append(symbol);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
