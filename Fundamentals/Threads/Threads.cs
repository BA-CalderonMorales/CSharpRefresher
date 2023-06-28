namespace Fundamentals.Threads
{
    public class Threads
    {
        public static void Lesson()
        {
            InternalLesson1(); // Thread
            // InternalLesson2(); // Thread.Sleep
            // InternalLesson3(); // Thread.Block
            // InternalLesson4(); // Pass arguments to a Thread
        }

        static void PrintZero()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(0);
            }
        }

        static void PrintOne()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(1);
            }
        }

        static void PrintTwo()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(2);
            }
        }

        private static void InternalLesson1()
        {
            Thread t = new Thread(PrintOne); // Can't guarantee when your thread is going to execute.
            Thread t2 = new Thread(PrintZero);

            Thread.CurrentThread.Name = "main";

            t.Start();
            PrintTwo(); // within the main thread, executes when it wants to
            t2.Start();
        }

        private static void InternalLesson2()
        {
            int num = 1;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(num);
                Thread.Sleep(1000);
                num++;
            }
            Console.WriteLine("Thread Ends.");
        }
        private static void InternalLesson3()
        {
            BankAccount acct = new BankAccount(10);
            Thread[] threads = new Thread[15];

            Thread.CurrentThread.Name = "main";

            for (int i = 0; i < 15; i++)
            {
                Thread t = new Thread(new ThreadStart(acct.IssueWithdraw));
                t.Name = i.ToString();
                threads[i] = t;
            }

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Thread {0} Alive : {1}",
                    threads[i].Name, threads[i].IsAlive);

                threads[i].Start();

                Console.WriteLine("Thread {0} Alive : {1}",
                    threads[i].Name, threads[i].IsAlive);
            }
            Console.WriteLine("Current Priority : {0}",
                Thread.CurrentThread.Priority);
            Console.WriteLine("Thread {0} Ending",
                Thread.CurrentThread.Name);
        }

        static void CountTo(int maxNum)
        {
            for (int i = 0; i <= maxNum; i++)
            {
                Console.WriteLine(i);
            }
        }

        private static void InternalLesson4()
        {
            Thread t = new Thread(() => CountTo(10));
            t.Start();

            new Thread(() =>
            {
                CountTo(5);
                CountTo(6);
            }).Start();
        }
    }
}
