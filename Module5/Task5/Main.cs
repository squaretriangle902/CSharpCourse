namespace Module5.Task5
{
    public static class Main
    {
        public static void Run()
        {
            Console.WriteLine("123".IsInt());
            Console.WriteLine("-123".IsInt());
            Console.WriteLine(" -123".IsInt());
            Console.WriteLine("0".IsInt());
        }
    }
}
