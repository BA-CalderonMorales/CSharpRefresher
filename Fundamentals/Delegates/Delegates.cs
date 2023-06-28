namespace Fundamentals.Delegates
{
    public class Delegates
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
        }

        private static void InternalLesson1()
        {
            Arithmetic add, sub, addSub;
            add = new Arithmetic(Add);
            sub = new Arithmetic(Subtract);
            addSub = add + sub;

            Console.WriteLine($"Add {6} and {10}");
            add(6, 10);
            Console.WriteLine($"Add & Subtract {10} and {4}");
            addSub(10, 4);
        }

        public static void InternalLesson2()
        {
            doubleIt dblIt = x => x * 2;
            Console.WriteLine($"5 * 2 = {dblIt(5)}");
        }

        public delegate double doubleIt(double val);

        public delegate void Arithmetic(double num1, double num2);

        public static void Add(double num1, double num2)
        {
            Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
        }

        public static void Subtract(double num1, double num2)
        {
            Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
        }
    }
}
