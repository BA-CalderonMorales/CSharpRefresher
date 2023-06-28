namespace Fundamentals.Enums
{
    public class Enums
    {
        
        // function that works with enum type
        private static void PaintCar(CarColor cc)
        {
            Console.WriteLine("The car was painted {0} with the code {1}",
                cc, (int)cc);
        }
        
        enum CarColor : byte
        {
            Orange = 1,
            Blue,
            Green,
            Red,
            Yellow
        };

        public static void Lesson()
        {
            InternalLesson1();
        }

        public static void InternalLesson1()
        {
            CarColor car1 = CarColor.Blue;
            PaintCar(car1);
        }
    }
}
