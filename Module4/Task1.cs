using System.Security.Cryptography.X509Certificates;

namespace Module4
{
    public static class Task1
    {
        private static readonly char[] delimeters = { ' ', ',', ';', ':', '.', '?', '!' };

        public static void Run()
        {
            string input = ",ddd, aaaa! Hello, olo, oh";
            Console.WriteLine("Mean word length: {0}", MeanWordLength(input).ToString());
        }

        private static double MeanWordLength(string input)
        {
            string[] words = input.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            return (double)WordLengthSumm(words) / words.Length;
        }

        private static int WordLengthSumm(string[] words)
        {
            int wordLenghtSumm = 0;
            foreach (string word in words)
            {
                wordLenghtSumm += word.Length;
            }
            return wordLenghtSumm;
        }
    }
}
