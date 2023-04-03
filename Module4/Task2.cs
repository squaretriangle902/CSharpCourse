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
            var pattern = '[' + occuranceString + ']';
            var stringBuilder = new StringBuilder(sentence);
            var matches = Regex.Matches(sentence, pattern);
            var charsAdded = 0;
            foreach (Match match in matches)
            {
                stringBuilder.Insert(match.Index + charsAdded, match.Value);
                charsAdded++;
            }
            return stringBuilder.ToString();
        }

    }
}
