using System.Text.RegularExpressions;

namespace Module4
{
    public static class Task6
    {
        private const string numberPattern = @"^-?[0-9]+(?:\.[0-9]+)?$";
        private const string scientificNumberPattern = @"^-?[1-9](?:\.[0-9]+)e-?[0-9]+$";

        public static void Run()
        {
            var text = "-2.01e2";
            ShowInputInfo(text);
        }

        private static void ShowInputInfo(string text) 
        {
            if (Regex.IsMatch(text, numberPattern))
            {
                Console.WriteLine("Число записано в обычной нотации.");
                return;
            }
            if (Regex.IsMatch(text, scientificNumberPattern))
            {
                Console.WriteLine("Число записано в научной нотации.");
                return;
            }
            Console.WriteLine("Это не число.");
        }
    }
}
