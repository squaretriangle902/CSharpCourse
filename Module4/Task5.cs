using System.Text.RegularExpressions;

namespace Module4
{
    public static class Task5
    {
        private const string eMailPattern = @"[a-zA-Z0-9_.]+@(?:[a-zA-Z0-9_]+\.)+(?:[a-zA-Z0-9_]){2,6}";

        public static void Run()
        {
            var text = "Иван: ivan@mail.ru, Петр: p_ivanov@mail.rol.ru";
            var matches = Regex.Matches(text, eMailPattern);
            foreach (Match match in matches) 
            {
                Console.WriteLine(match.Value);
            }
        }
    }
}
