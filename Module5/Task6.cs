using System.Diagnostics;

namespace Module5
{
    public static class Task6
    {
        public static void Run()
        {

            int[] array = { 1, -1, 2, -2, 3, -3, 4, -4 };
            int executionEvaluationCount = 10;
            ExecutionTimesMedian(array, executionEvaluationCount);
            ExecutionTimesMedian(array, SelectCondition, executionEvaluationCount);
            ExecutionTimesMedian(array, delegate (int number) { return number > 0; }, executionEvaluationCount);
            ExecutionTimesMedian(array, (int number) => number > 0, executionEvaluationCount);
            ExecutionTimesMedian(array.Where, (int number) => number > 0, executionEvaluationCount);
        }

        //task6_1
        private static TimeSpan ExecutionTimesMedian(int[] array, int executionEvaluationCount)
        {
            return Median(ExecutionsTimes(array, executionEvaluationCount));
        }

        private static TimeSpan[] ExecutionsTimes(int[] array, int executionEvaluationCount)
        {
            TimeSpan[] timeSpans = new TimeSpan[executionEvaluationCount];
            for (int i = 0; i < executionEvaluationCount; i++)
            {
                timeSpans[i] = EvaluateExecutionTime(PositiveInts, array);
            }
            return timeSpans;
        }

        private static TimeSpan EvaluateExecutionTime(Func<int[], int[]> selectMethod, int[] array)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (selectMethod is not null)
            {
                selectMethod(array);
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private static int[] PositiveInts(int[] array)
        {
            var positiveInts = new List<int>(array.Length);
            foreach (var item in array)
            {
                if (SelectCondition(item))
                {
                    positiveInts.Add(item);
                }
            }
            return positiveInts.ToArray();
        }

        //task6_2-6_4
        private static TimeSpan ExecutionTimesMedian(int[] array, Predicate<int> selectCondition, int executionEvaluationCount)
        {
            return Median(ExecutionsTimes(array, selectCondition, executionEvaluationCount));
        }

        private static TimeSpan[] ExecutionsTimes(int[] array, Predicate<int> selectCondition, int executionEvaluationCount)
        {
            TimeSpan[] timeSpans = new TimeSpan[executionEvaluationCount];
            for (int i = 0; i < executionEvaluationCount; i++)
            {
                timeSpans[i] = ExecutionTime(PositiveInts, array, selectCondition);
            }
            return timeSpans;
        }

        private static TimeSpan ExecutionTime(Func<int[], Predicate<int>, int[]> selectMethod, int[] array,
            Predicate<int> selectCondition)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (selectMethod is not null)
            {
                selectMethod(array, selectCondition);
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private static int[] PositiveInts(int[] array, Predicate<int> selectCondition)
        {
            var positiveInts = new List<int>(array.Length);
            foreach (var item in array)
            {
                if (selectCondition is not null && selectCondition(item))
                {
                    positiveInts.Add(item);
                }
            }
            return positiveInts.ToArray();
        }

        //task6_5
        private static TimeSpan ExecutionTimesMedian(Func<Func<int, bool>, IEnumerable<int>> linqRequest,
            Func<int, bool> predicate, int executionEvaluationCount)
        {
            return Median(ExecutionTimes(linqRequest, predicate, executionEvaluationCount));
        }

        private static TimeSpan[] ExecutionTimes(Func<Func<int, bool>, IEnumerable<int>> linqRequest,
            Func<int, bool> predicate, int executionEvaluationCount)
        {
            TimeSpan[] timeSpans = new TimeSpan[executionEvaluationCount];
            for (int i = 0; i < executionEvaluationCount; i++)
            {
                timeSpans[i] = EvaluateExecutionTime(linqRequest, predicate);
            }
            return timeSpans;
        }

        private static TimeSpan EvaluateExecutionTime(Func<Func<int, bool>, IEnumerable<int>> linqRequest,
            Func<int, bool> predicate)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (linqRequest is not null)
            {
                linqRequest(predicate);
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
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

        private static bool SelectCondition(int number)
        {
            return number > 0;
        }

    }
}
