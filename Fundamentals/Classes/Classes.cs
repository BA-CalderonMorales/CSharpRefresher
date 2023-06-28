namespace Fundamentals.Classes
{
    public class Classes
    {

        struct Rectangle
        {
            public double length;
            public double width;

            public Rectangle(double l = 1, double w = 1)
            {
                this.length = l;
                this.width = w;
            }
            public double Area()
            {
                return length * width;
            }
        }

        public static void InternalLesson1()
        {
            Rectangle rect1;
            rect1.length = 200;
            rect1.width = 50;
            Console.WriteLine("Area of rect1 : {0}",
                rect1.Area());

            Rectangle rect2 = new Rectangle(100, 40);
            rect2 = rect1;
            rect1.length = 33;
            Console.WriteLine("Rect2.length : {0}",
                rect2.length);
        }

        public static void InternalLesson2()
        {
            Animal fox = new Animal("Red", "Raaw!");
            Console.WriteLine("# of Animals {0}",
                Animal.numOfAnimals);
        }

        public static void InternalLesson3()
        {
            Console.WriteLine("Area of Rectangle : {0}",
                ShapeMath.GetArea("Rectangle", 5, 6));
        }

        public static void InternalLesson4()
        {
            // nullable type
            // int randNum = null <- int is a non-nullable value type, so
            int? randNum = null;
            if (randNum == null)
            {
                Console.WriteLine("randNum is null");
            }
            if (!randNum.HasValue)
            {
                Console.WriteLine("randNum does not have value (!jfdkjk.HasValue)");
            }
        }

        public static void InternalLesson5()
        {
            Animal cat = new Animal();
            cat.SetName("Whiskers");
            cat.Sound = "Meow";
            Console.WriteLine("The cat's name is {0} and says {1}",
                cat.GetName(), cat.Sound);
            cat.Owner = "Brandon";
            Console.WriteLine("{0} owner is {1}",
                cat.GetName(), cat.Owner);
            Console.WriteLine("{0} shelter id is {1}",
                cat.GetName(), cat.idNum);
            Console.WriteLine("# of Animals : {0}",
                Animal.NumOfAnimals);
        }

        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
            InternalLesson3();
            InternalLesson4();
            InternalLesson5();
        }
    }
}
