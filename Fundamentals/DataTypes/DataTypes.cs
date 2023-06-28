namespace Fundamentals.DataTypes
{
    public class DataTypes
    {
        public static void Lesson()
        {
            bool canIVote = true;
            Console.WriteLine($"Can you vote? {canIVote}");

            // with integers, there is no decimal points (same with long)...
            Console.WriteLine("Biggest Integer : {0} ", int.MaxValue);
            Console.WriteLine("Smallest Integer : {0} ", int.MinValue);

            Console.WriteLine("Biggest Long : {0} ", long.MaxValue);
            Console.WriteLine("Smallest Long : {0} ", long.MinValue);

            Console.WriteLine("Biggest Decimal : {0} ", decimal.MaxValue);
            Console.WriteLine("Smallest Decimal : {0} ", decimal.MinValue);

            // With decimal data type, you must append the M at the end of the last number.
            decimal decValuePrecision = (decimal)Math.PI + 12.222232323232M;
            Console.WriteLine($"Here is a precise decimal: {decValuePrecision}");

            // 64-bit double 
            Console.WriteLine("Biggest Double : {0} ", double.MaxValue);
            Console.WriteLine("Smallest Double : {0} ", double.MinValue);

            double dblPiVal = Math.PI;
            double dblPiValTwo = Math.PI + 2323.1212;
            Console.WriteLine($"Here is a precise double: {dblPiVal + dblPiValTwo}");

            // 32-bit float
            Console.WriteLine("Biggest Float : {0} ", float.MaxValue);
            Console.WriteLine("Smallest Float : {0} ", float.MinValue);
            // Not as accurate as double
            float fltPiVal = (float) Math.PI;
            float fltPiValTwo = (float) Math.PI + 2323.1212F;
            Console.WriteLine($"Here is a precise double: {fltPiVal + fltPiValTwo}");

            // Other DataTypes
            // byte : 8-bit unsigned int 0 to 255
            // char : 16-bit unicode character
            // sbyte : 8-bit signed int 128 to 127
            // short : 16-bit signed int -32,768 to 32,767
            // uint : 32-bit unsigned int 0 to 4,294,967,295
            // ulong : 64-bit unsigned int 0 to 18,446,744,073,709,551,615
            // ushort : 16-bit unsigned int 0 to 65,535

            bool boolFromStr = bool.Parse("true");
            int intFromStr = int.Parse("100");
            double dblFromStr = double.Parse("1.234");


            string strVal = dblFromStr.ToString();
            Console.WriteLine($"Data type : {strVal.GetType()}");

            // Cast a double into an integer (explicit conversion - may lose some data e.g. decimal values):
            double dblNum = 12.345;
            Console.WriteLine($"Integer : {(int)dblNum}");
            // Implicit conversion - when the smaller size type is being converted to a larger type, no data lost.
            int intNum = 10;
            long longNum = intNum;
            Console.WriteLine($"Long : {longNum}");
        }
    }
}
