namespace Module5.Task3
{
    public class Main
    {
        private static readonly int[] array = { 1, 12, 3, 4, 15, 6, 17 };

        public static async void Run()
        {
            var compare = (int a, int b) => a - b;
            var sorter1 = CreateParallelSorter(compare, "thread 1");
            var sorter2 = CreateParallelSorter(compare, "thread 2");

            sorter1.SortStartThread();
            sorter2.SortStartThread();

            await SortAsync(compare);
            SortAsync(compare).GetAwaiter().GetResult();
        }

        private static async Task SortAsync(Func<int, int, int> compare)
        {
            int[] arrayCopy = CreateArrayCopy();
            await Task.Run(() => BubbleSort.Sort(array, compare, () => 
            {
                Console.WriteLine("Async sort iteration completed");
                Task.Delay(TimeSpan.FromMilliseconds(100));
            }));
            Console.WriteLine("Async sorted completed");
        }

        private static ParallelSorter<int> CreateParallelSorter(Func<int, int, int> compare, string name)
        {
            int[] arrayCopy = CreateArrayCopy();
            return new ParallelSorter<int>(arrayCopy, compare, name);
        }

        private static int[] CreateArrayCopy()
        {
            int[] arrayCopy = new int[array.Length];
            array.CopyTo(arrayCopy, 0);
            return arrayCopy;
        }
    }
}
