namespace Fundamentals.FormattingOutput
{
    public class FormattingOutput
    {
        public static void Lesson()
        {
            Console.WriteLine("Currency : {0:c}", 23.455); // automatically format with currency
            Console.WriteLine("Pad with 0s : {0:d4}", 23);
            Console.WriteLine("3 Decimals : {0:f3}", 23.455);
            Console.WriteLine("Commas : {0:n3}", 2300);
        }
    }
}
