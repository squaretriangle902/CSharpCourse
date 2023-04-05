using System.Diagnostics;
using System.Text;

namespace Module4
{
    public static class Task8
    {
        private const string StringBuilderInLoopAppendTimeElapsedMessage = "String builder append in loop time elapsed: ";
        private const string StringInLoopAppendTimeElapsedMessage = "String append in loop time elapsed: ";

        public static void Run()
        {
            int n = 100000;
            ConsoleWriteLineTimeSpan(StringBuilderAppendInLoopTimeElapsed(n), StringBuilderInLoopAppendTimeElapsedMessage);
            ConsoleWriteLineTimeSpan(StringAppendInLoopTimeElapsed(n), StringInLoopAppendTimeElapsedMessage);
        }

        private static TimeSpan StringBuilderAppendInLoopTimeElapsed(int iterationCount)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            StringBuilderAppendInLoop(iterationCount);
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private static TimeSpan StringAppendInLoopTimeElapsed(int iterationCount)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            StringAppendInLoop(iterationCount);
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private static void StringAppendInLoop(int iterationCount)
        {
            string str = "";
            for (int i = 0; i < iterationCount; i++)
            {
                str += '*';
            }
        }

        private static void StringBuilderAppendInLoop(int iterationCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < iterationCount; i++)
            {
                stringBuilder.Append('*');
            }
        }

        private static void ConsoleWriteLineTimeSpan(TimeSpan timeSpan, string message)
        {
            Console.Write(message);
            Console.WriteLine("{0:00}:{1:00}:{2:00}.{3}", 
                timeSpan.Hours.ToString(), timeSpan.Minutes.ToString(), 
                timeSpan.Seconds.ToString(), timeSpan.Milliseconds.ToString());
        }

    }
}
