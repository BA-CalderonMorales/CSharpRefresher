namespace Fundamentals.StringFunctions
{
    public class StringFunctions
    {
        public static void Lesson()
        {
            string randString = "This is a string.";

            Console.WriteLine($"We're manipulating this string: {randString}");

            Console.WriteLine("String Length : {0}",
                randString.Length);

            Console.WriteLine("String Contains is : {0}",
                randString.Contains("is"));

            Console.WriteLine("Index of is : {0}",
                randString.IndexOf("is"));

            Console.WriteLine("Remove String : {0}",
                randString.Remove(10, 6));

            Console.WriteLine("Insert String : {0}",
                randString.Insert(10, "short "));

            Console.WriteLine("Replace String : {0}",
                randString.Replace("string", "sentence"));

            Console.WriteLine("Compare A to B : {0}",
                String.Compare("A", "B",
                StringComparison.OrdinalIgnoreCase));
            // Compare strings and ignore case
            // < 0 : str1 preceeds str2
            // = : Zero
            // > 0 : str2 preceeds str1
            Console.WriteLine("A = a : {0}",
                String.Equals("A", "a",
                StringComparison.OrdinalIgnoreCase));

        }
    }
}
