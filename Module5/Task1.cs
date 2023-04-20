namespace Module5
{
    public static class Task1
    {
        public static void Run()
        {
            int[] items = { 1, 4, 2, 7, 2, 8, 11, 9, 2 };
            Console.WriteLine(string.Join(", ", items));
            BubbleSort.Sort(items, (a, b) => a - b);
            Console.WriteLine(string.Join(", ", items));
        }
    }
}
