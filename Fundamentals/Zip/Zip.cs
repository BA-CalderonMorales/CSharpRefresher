namespace Fundamentals.Zip
{
    public class Zip
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        private static void InternalLesson1()
        {
            var listOne = new List<int>(new int[] { 1, 3, 4 });
            var listTwo = new List<int>(new int[] { 4, 6, 8 });
            var sumList = listOne.Zip(listTwo, (x, y) => x + y).ToList();
            foreach (var item in sumList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
