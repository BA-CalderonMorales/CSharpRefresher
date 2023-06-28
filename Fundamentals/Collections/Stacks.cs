using System.Collections;

namespace Fundamentals.Collections
{
    public class Stacks
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        public static void InternalLesson1()
        {
            // Last In First Out - pulling a plate from a stack of plates
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Console.WriteLine("Peek 1 : {0}",
                stack.Peek());
            Console.WriteLine("Pop 1 : {0}",
                stack.Pop());
            Console.WriteLine("Contain 1 : {0}",
                stack.Contains(1));

            object[] numArray2 = stack.ToArray();
            Console.WriteLine(String.Join(",", numArray2));

            foreach (object o in stack)
            {
                Console.WriteLine($"Stack : {o}");
            }

        }
    }
}
