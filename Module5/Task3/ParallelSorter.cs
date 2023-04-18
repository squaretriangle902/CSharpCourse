namespace Module5.Task3
{
    public class ParallelSorter<T>
    {
        private string threadName;
        private Thread thread;
        public event EventHandler<EventArgs> ArrayIsSorted;

        public ParallelSorter(T[] items, Func<T, T, int> compare, string threadName = "")
        {
            this.threadName = threadName;
            thread = new Thread(() => Sort(items, compare));
            ArrayIsSorted = delegate { };
        }

        public void SortStartThread()
        {
            thread.Start();
        }

        protected virtual void OnSortingIsCompleted()
        {
            if (ArrayIsSorted is not null)
            {
                ArrayIsSorted(this, EventArgs.Empty);
            }
        }

        private void Sort(T[] items, Func<T, T, int> compare)
        {
            BubbleSort.Sort(items, compare, ThreadAction);
            OnSortingIsCompleted();
        }

        private void ThreadAction()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Console.WriteLine(threadName);
        }
    }
}
