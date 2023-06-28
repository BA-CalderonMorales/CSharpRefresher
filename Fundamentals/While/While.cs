namespace Fundamentals.While
{
    public class While
    {
        public static void Lesson()
        {
            While1();
            While2();
        }

        public static void While1()
        {
            int i = 1;
            Console.WriteLine(i);
            while (i <= 10)
            {
                if (i == 5)
                {
                    break;
                } else
                {
                    i++;
                    Console.WriteLine(i);
                }
            }
        }

        public static void While2()
        {
            int i = 1;
            while (i <= 10)
            {
                if (i % 2 == 0)
                {
                    i++;
                    continue;
                }
                if (i == 9)
                {
                    break;
                }
                Console.WriteLine(i);
                i++;
            }
        }
    }
}
