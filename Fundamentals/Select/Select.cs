namespace Fundamentals.Select
{
    public class Select
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        private static void InternalLesson1()
        {
            var oneTo10 = new List<int>();
            oneTo10.AddRange(Enumerable.Range(1, 10));
            var squares = oneTo10.Select(x => x * x);
            foreach (var square in squares)
            {
                Console.WriteLine(square);
            }
        }
    }
}
