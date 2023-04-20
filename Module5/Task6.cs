using System;
using System.Diagnostics;

namespace Module5
{
    public static class Task6
    {
        public static void Run()
        {
            Random random = new Random();
            int[] array = Enumerable.Repeat(0, 1000).Select(i => random.Next(-10, 10)).ToArray();
            int executionEvaluationCount = 100;
            Console.WriteLine(ExecutionTimesMedian(() => PositiveInts(array), executionEvaluationCount));
            Console.WriteLine(ExecutionTimesMedian(() => PositiveInts(array, FilterCondition), executionEvaluationCount));
            Console.WriteLine(ExecutionTimesMedian(() => PositiveInts(array, delegate (int number) { return number > 0; }),
                                                   executionEvaluationCount));
            Console.WriteLine(ExecutionTimesMedian(() => PositiveInts(array, (int number) => number > 0), 
                                                   executionEvaluationCount));
            Console.WriteLine(ExecutionTimesMedian(() => array.Where((int number) => number > 0).ToArray(), 
                                                   executionEvaluationCount));

        }

        private static TimeSpan ExecutionTimesMedian(Action action, int executionEvaluationCount)
        {
            return Median(ExecutionsTimes(action, executionEvaluationCount));
        }

        private static TimeSpan[] ExecutionsTimes(Action action, int executionEvaluationCount)
        {
            TimeSpan[] timeSpans = new TimeSpan[executionEvaluationCount];
            for (int i = 0; i < executionEvaluationCount; i++)
            {
                timeSpans[i] = ExecutionTime(action);
            }
            return timeSpans;
        }

        private static TimeSpan ExecutionTime(Action action)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (action is not null)
            {
                action();
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private static int[] PositiveInts(int[] array)
        {
            var positiveInts = new List<int>(array.Length);
            foreach (var item in array)
            {
                if (FilterCondition(item))
                {
                    positiveInts.Add(item);
                }
            }
            return positiveInts.ToArray();
        }

        private static int[] PositiveInts(int[] array, Predicate<int> filterCondition)
        {
            var positiveInts = new List<int>(array.Length);
            foreach (var item in array)
            {
                if (filterCondition(item))
                {
                    positiveInts.Add(item);
                }
            }
            return positiveInts.ToArray();
        }

        private static TimeSpan Median(TimeSpan[] timeSpans)
        {
            Array.Sort(timeSpans);
            if (timeSpans.Length % 2 == 1)
            {
                return timeSpans[timeSpans.Length / 2];
            }
            return (timeSpans[timeSpans.Length / 2 - 1] + timeSpans[timeSpans.Length / 2]) / 2;
        }

        private static bool FilterCondition(int number)
        {
            return number > 0;
        }

    }
}
