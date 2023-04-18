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

        public static void BubleSort<T>(T[] items, Func<T, T, int> compare)
        {
            for (int i = items.Length - 2; i >= 2; i--)
            {
                BubbleSortIteration<T>(items, i, compare);
            }
        }

        private static void BubbleSortIteration<T>(T[] items, int lastIndex, Func<T, T, int> compare)
        {
            for (int i = 0; i <= lastIndex; i++)
            {
                SwapWithNextIfGreater<T>(items, i, compare);
            }
        }

        private static void SwapWithNextIfGreater<T>(T[] items, int index, Func<T, T, int> compare)
        {
            if (compare is not null && compare(items[index], items[index + 1]) > 0)
            {
                (items[index], items[index + 1]) = (items[index + 1], items[index]);
            }
        }
    }
}
