namespace Fundamentals.DoWhile
{
    public class DoWhile
    {
        public static void Lesson()
        {
            int numberGuessed = 0;
            DoWhile1(numberGuessed);
        }

        public static void DoWhile1(int numberGuessed)
        {
            Random rnd = new Random();
            int secretNumber = rnd.Next(1, 11); // goes up to 11, but doesn't include 11
            Console.WriteLine("Number guessed : {0}", numberGuessed);
            do
            {
                Console.WriteLine("Enter a number between 1 & 10 :");
                numberGuessed = Convert.ToInt32(Console.ReadLine());
            } while (secretNumber != numberGuessed);

            Console.WriteLine("You guessed it! It was {0}", secretNumber);
        }
    }
}
