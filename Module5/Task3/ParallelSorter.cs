namespace Module5.Task3
{
    public class ParallelSorter<T>
    {
        private string threadMessage;
        private Thread thread;
        public event EventHandler<EventArgs> ArrayIsSorted;

        public ParallelSorter(T[] items, Func<T, T, int> compare, string threadMessage = "")
        {
            this.threadMessage = threadMessage;
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
            BubbleSort.Sort(items, compare, IterationAction);
            OnSortingIsCompleted();
        }

        private void IterationAction()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Console.WriteLine(threadMessage);
        }
    }
}
