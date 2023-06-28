namespace Fundamentals.Functions
{
    public class Functions
    {
        // -------------- FUNCTIONS -------------------

        // <Access Specifier> <Return Type> <Method Name>(Parameters)
        // { <Body> }

        // Access Specifier: determines whether the function can be called from another class
        // e.g.
        // public : Can be accessed from another class
        // private : Can't be accessed from another class
        // protected : Can't be accessed by class and derived classes

        // Return type: specific type returned
        // e.g.
        // int, bool, object, double, etc.

        // Method Name: name of the function
        // e.g.
        // MyFunction, ConvertToMySpecialString, etc.

        // Parameters: the parameters used by the function to execute logic within the <Body>

        // -------------- END OF FUNCTIONS --------------
        private static void SayHello()
        {
            string name = "";
            Console.Write("What is your name : ");
            name = Console.ReadLine();
            Console.WriteLine("Hello {0}", name);
        }

        private static double GetSum(double x = 1, double y = 1)
        {
            double temp = x; // change the value of x to temp
            x = y; // change the value of y to x
            y = temp; // change the value of temp to y
            Console.WriteLine("What is x during the function GetSum call? x : {0}, because of reassigned variables in local function scope", x);
            Console.WriteLine("What is y after the function GetSum call? y : {0}, because of reassigned variables in local function scope", y);
            return x + y; // x is now y (and vice versa).
        }

        private static void DoubleIt(int x, out int solution)
        {
            solution = x * 2;
        }

        private static void Swap(ref int num3, ref int num4)
        {
            int temp = num3;
            num3 = num4;
            num4 = temp;
        }

        private static double GetSumMore(params double[] nums)
        {
            double sum = 0;
            foreach (int num in nums)
            {
                sum += num;
            }
            return sum;
        }

        private static void GetSumMoreUsingOut(out double solution, params double[] nums)
        {
            solution = 0;
            foreach (int i in nums)
            {
                solution += i;
            }
        }

        private static void PrintInfo(string name, int zipCode)
        {
            Console.WriteLine("{0} lives in the zipcode {1}", name, zipCode);
        }

        private static double GetSum2(double x = 1, double y = 1)
        {
            return x + y;
        }
        private static double GetSum2(string x = "1", string y = "1")
        {
            double dblX = Convert.ToDouble(x);
            double dblY = Convert.ToDouble(y);
            return dblX + dblY;
        }

        public static void Lesson()
        {
            SayHello();
            // GetSum(); // allowed to call without parameters passed in because defaults are in the function...
            double x = 5;
            double y = 4;
            Console.WriteLine("What is the sum of x : {0} and y : {1}", x, y);
            Console.WriteLine("5 + 4 = {0}", GetSum(x, y));
            // Whatever happens in the function, stays in the function because of scope...
            Console.WriteLine("What is x after the function GetSum is called? x : {0}", x);
            Console.WriteLine("What is y after the function GetSum is called? y : {0}", y);

            int solution;
            DoubleIt(15, out solution); // using the keyword out here allows us to have access to the solution inside of the void function
            Console.WriteLine("15 * 2 is equal to {0}", solution);

            int num3 = 10;
            int num4 = 20;
            Console.WriteLine("Before Swap num3 : {0} num4 : {1}",
                num3, num4);
            Swap(ref num3, ref num4); // will swap the reference because we use the keyword ref
            Console.WriteLine("After Swap num3 : {0} num4 : {1}",
                num3, num4);

            GetSumMoreUsingOut(out double answer, 1, 2, 3);
            Console.WriteLine("Using Out pattern: 1 + 2 + 3 is equal to {0}", answer);
            Console.WriteLine("Using Regular return pattern: 1 + 2 + 3 is equal to {0}", GetSumMore(1, 2, 3));

            Console.WriteLine("---------------------------Named Parameters-----------------------------");
            PrintInfo(zipCode: 123457, name: "Yo this is my addy.");

            Console.WriteLine("---------------------------Method Overriding-----------------------------");
            Console.WriteLine("5.0 + 4.0 = {0}", GetSum2(5.0, 4.0));
            Console.WriteLine("5.0 + 4.0 = {0}, but a string literal", GetSum2("5.0", "4.0"));
        }
    }
}
