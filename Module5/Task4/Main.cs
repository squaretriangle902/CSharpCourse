namespace Module5.Task4
{
    public static class Main
    {
        public static void Run() 
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7 };
            Console.WriteLine("{0} = {1}", string.Join(" + ", array), array.MySum().ToString());
        }
    }
}
