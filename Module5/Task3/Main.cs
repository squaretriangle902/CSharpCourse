namespace Module5.Task3
{
    public class Main
    {
        private static readonly int[] array = { 1, 12, 3, 4, 15, 6, 17 };

        public static void Run()
        {
            var compare = (int a, int b) => a - b;
            var sorter1 = CreateParallelSorter(compare, "thread 1");
            var sorter2 = CreateParallelSorter(compare, "thread 2");
            sorter1.SortStartThread();
            sorter2.SortStartThread();

            CreateTask(compare);
        }

        private static void CreateTask(Func<int, int, int> compare)
        {
            int[] arrayCopy = new int[array.Length];
            array.CopyTo(arrayCopy, 0);
            var task = Task.Run(() => BubbleSort.Sort(array, compare, TaskAction));
        }

        private static void TaskAction()
        {
            Console.WriteLine("Task");
        }

        private static ParallelSorter<int> CreateParallelSorter(Func<int, int, int> compare, string name)
        {
            int[] arrayCopy = new int[array.Length];
            array.CopyTo(arrayCopy, 0);
            return new ParallelSorter<int>(arrayCopy, compare, name);
        }
    }
}
