using System.Text.RegularExpressions;

namespace Module4
{
    public static class Task4
    {
        private static readonly string HTLMTagPattern = "<.*?>";

        public static void Run() 
        {
            var text = "<b>Это</b> текст <i>с</i> <font color=\"red\">HTML</font> кодами";
            Console.WriteLine(RemoveHTMLTags(text));
        }

        private static string RemoveHTMLTags(string text) 
        {
            return Regex.Replace(text, HTLMTagPattern, String.Empty);
        }
    }
}
