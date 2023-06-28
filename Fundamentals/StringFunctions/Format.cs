using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.StringFunctions
{
    public class Format
    {
        public static void Lesson()
        {
            string randString = "This is a string.";

            Console.WriteLine("String being manipulated : {0}",
                randString);

            Console.WriteLine("Pad Left : {0}",
                randString.PadLeft(20, '.'));

            Console.WriteLine("Pad Right : {0}",
                randString.PadRight(20, '.'));

            Console.WriteLine("Trim : {0}",
                randString.Trim());

            Console.WriteLine("UpperCase : {0}",
                randString.ToUpper());

            Console.WriteLine("LowerCase : {0}",
                randString.ToLower());

            string newString = String.Format("{0} saw a {1} {2} in the {3}",
                "Paul", "rabbit", "eating", "field");
            Console.Write(newString + "\n");

            // Escape characters: \" \' \\ \t \a
            Console.WriteLine("\"This is a string\"");

            Console.WriteLine(@"Exactly what I typed \n");

        }
    }
}
