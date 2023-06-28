using System.Globalization;
using System.Text;

namespace Fundamentals.SB
{
    public class SB
    {
        public static void Lesson()
        {
            // StringBuilder has a default size of 16 characters.
            StringBuilder sb = new StringBuilder("Random Text");
            StringBuilder sb2 = new StringBuilder("More Stuff that is very important");

            Console.WriteLine("Capacity : {0}", sb2.Capacity);
            Console.WriteLine("Length : {0}", sb2.Length);

            sb2.AppendLine("\nMore in important text");
            Console.WriteLine(sb2.ToString());

            CultureInfo enUs = CultureInfo.CreateSpecificCulture("en-US");
            string bestCust = "Bob Smith";
            sb2.AppendFormat(enUs, "Best Customer : {0}", bestCust);
            Console.WriteLine(sb2.ToString());

            sb2.Replace("text", "characters");
            Console.WriteLine(sb2.ToString());

            sb2.Clear();
            Console.WriteLine(sb2.ToString());

            sb2.Append("Random Text Appended via the .Append method.");
            Console.WriteLine(sb2.ToString());

            Console.WriteLine("Does sb equal sb2 ? {0}", sb.Equals(sb2));

            sb2.Insert(11, " that's great!");
            Console.WriteLine(sb2.ToString());

            sb2.Remove(11, 7);
            Console.WriteLine(sb2.ToString());
        }
    }
}
