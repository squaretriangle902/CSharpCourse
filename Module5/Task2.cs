namespace Module5
{
    public static class Task2
    {
        public static void Run()
        {
            var sentence = "All you had to do was the following damn train CJ";
            var items = sentence.Split(' ');
            Console.WriteLine(string.Join(" ", items));
            BubbleSort.Sort(items, StringCompare);
            Console.WriteLine(string.Join(" ", items));
        }

        public static int StringCompare(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return a.Length - b.Length;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return a[i] - b[i];
                }
            }
            return 0;
        }

    }
}
