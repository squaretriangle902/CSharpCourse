namespace Module5.Task4
{
    public static class IntArrayExtension
    {
        public static int MySum(this int[] array)
        {
            int sum = 0;
            foreach (var item in array) 
            {
                sum += item;
            }
            return sum;
        }
    }
}
