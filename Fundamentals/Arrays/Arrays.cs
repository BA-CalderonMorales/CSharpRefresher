namespace Fundamentals.Arrays
{
    public class Arrays
    {
        static void PrintArray(int[] intArray, string mess) // parameters are passed to a function
        {
            foreach (int k in intArray)
            {
                Console.WriteLine("{0} : {1}",
                    mess, k);
            }
        }
        public static void Lesson()
        {
            int[] favNums = new int[3];
            favNums[0] = 23;
            Console.WriteLine("favNum 0 : {0}", favNums[0]);
            string[] customers = { "Bob", "Sally", "Sue" };
            var employees = new[] { "Mike", "Paul", "Rick" };
            object[] randomArray = { "Paul", 45, 1.234 };
            Console.WriteLine("randomArray 0 : {0}",
                randomArray[0].GetType());
            Console.WriteLine("Array Size : {0}",
                randomArray.Length);

            Console.WriteLine("--------------------FOR LOOPS------------------------");

            for (int j = 0; j < randomArray.Length; j++)
            {
                Console.WriteLine("Array : {0} : Value : {1}",
                    j, randomArray[j]);
            }

            Console.WriteLine("--------------------Multi-Dimensional Array------------------------");
            string[,] custNames = new string[2, 2]
            {
                // 0    ,   1
                {"Billy", "Bob"}, // 0
                {"Sally", "Smith"} // 1
            };
            Console.WriteLine("MD Value : {0}",
                custNames.GetValue(1, 1));

            for (int j = 0; j < custNames.GetLength(0); j++)
            {
                for (int k = 0; k < custNames.GetLength(1); k++)
                {
                    Console.WriteLine("{0} ",
                        custNames[j, k]);
                }
                Console.WriteLine(); // Empty space
            }

            Console.WriteLine("--------------------Arrays and Functions------------------------");
            int[] randInts = { 1, 4, 9, 12 };
            PrintArray(randInts, "foreach");
            Array.Sort(randInts);
            Array.Reverse(randInts);
            Console.WriteLine("1 at index : {0}",
                Array.IndexOf(randInts, 1));
            // Change the value in an array
            randInts.SetValue(0, 1);

            int[] srcArray = { 1, 2, 3 };
            int[] destArray = new int[2];
            int startInd = 0;
            int length = 2;

            Array.Copy(srcArray, startInd, destArray, 0, length);
            PrintArray(destArray, "Copy");

            // int[] numArray = { 1, 11, 22 };
            // Console.WriteLine("Values that are > 10 : {0}", Array.Find(numArray, lambda function goes here...));
            Array anotherArray = Array.CreateInstance(typeof(int), 10);
            srcArray.CopyTo(anotherArray, 5);

            foreach (int m in anotherArray)
            {
                Console.WriteLine("CopyTo : {0} ", m);
            }

        }
    }
}
