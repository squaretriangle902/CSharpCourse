using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Module5
{
    public static class BubbleSort
    {
        public static void Sort<T>(T[] items, Func<T, T, int> compare)
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

        public static void Sort<T>(T[] items, Func<T, T, int> compare, Action iterationAction)
        {
            for (int i = items.Length - 2; i >= 2; i--)
            {
                BubbleSortIteration<T>(items, i, compare, iterationAction);
            }
        }

        private static void BubbleSortIteration<T>(T[] items, int lastIndex, Func<T, T, int> compare, Action iterationAction)
        {
            for (int i = 0; i <= lastIndex; i++)
            {
                SwapWithNextIfGreater<T>(items, i, compare, iterationAction);
            }
        }

        private static void SwapWithNextIfGreater<T>(T[] items, int index, Func<T, T, int> compare, Action iterationAction)
        {
            if (compare is not null && compare(items[index], items[index + 1]) > 0)
            {
                (items[index], items[index + 1]) = (items[index + 1], items[index]);
                if (iterationAction is not null)
                {
                    iterationAction();
                }
            }
        }
    }
}
