namespace Fundamentals.Aggregate
{
    public class Aggregate
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        private static void InternalLesson1()
        {
            var numList2 = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            Console.WriteLine("Sum {0}",
                numList2.Aggregate((a, b) => a + b));

            Console.WriteLine("Avg {0}",
                numList2.AsQueryable().Average());

            Console.WriteLine("All > 3? : {0}",
                numList2.All(x => x > 3));

            Console.WriteLine("Any > 3? : {0}",
                numList2.Any(x => x > 3));

            numList2.Add(3); // adds duplicate num.

            Console.WriteLine("Distinct : {0}",
                string.Join(", ", numList2.Distinct()));

            var numList3 = new List<int>() { 3 };

            Console.WriteLine("Except : {0}",
                string.Join(", ", numList2.Except(numList3)));

            Console.WriteLine("Intersect: {0}",
                string.Join(", ", numList2.Intersect(numList3)));
        }
    }
}
