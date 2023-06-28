namespace Fundamentals.Abstraction
{
    public class Abstraction
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
        }

        public static void InternalLesson1()
        {
            Shape[] shapes =
            {
                new Circle(5),
                new Rectangle(4, 5)
            };
            foreach (Shape shape in shapes)
            {
                shape.GetInfo();

                Console.WriteLine("{0} Area : {1:f2}",
                    shape.Name, shape.Area());

                Circle testCirc = shape as Circle; // can use "as" or "is" to check for data types
                if (testCirc == null)
                {
                    Console.WriteLine("This isn't a Circle");
                }
                if (shape is Circle)
                {
                    Console.WriteLine("This isn't a Rectangle");
                }
            }
        }

        public static void InternalLesson2()
        {
            object circ1 = new Circle(4);

            Circle circ2 = (Circle)circ1;
            Console.WriteLine("The {0} Area is {1:f2}",
                circ2.Name, circ2.Area());
        }
    }
}
