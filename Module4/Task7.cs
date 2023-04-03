using System.Text.RegularExpressions;

namespace Module4
{
    public static class Task7
    {
        private static readonly string timePattern = "(?: [0-9]|1[0-9]|2[0-3]):(?:[0-5][0-9])";

        public static void Run()
        {
            var text = "В 7:55 я встал, позавтракал и к 10:77 пошёл на работу.";
            Console.WriteLine("Время встретилось раз: {0} ", Regex.Matches(text, timePattern).Count);
        }
    }
}
