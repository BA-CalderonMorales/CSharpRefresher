using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.Collections
{
    class Queues
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        public static void InternalLesson1()
        {
            Queue queue = new Queue();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Console.WriteLine("1 in Queue : {0}",
                queue.Contains(1));
            
            Console.WriteLine("Remove : {0}",
                queue.Dequeue());

            Console.WriteLine("Peek 1 : {0}",
                queue.Peek());

            object[] numArray = queue.ToArray();

            Console.WriteLine(String.Join(",", numArray));
            foreach (object o in queue)
            {
                Console.WriteLine($"Queue : {o}");
            }

            queue.Clear();
        }
    }
}
