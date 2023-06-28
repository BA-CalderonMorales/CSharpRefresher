
namespace Fundamentals.Generics
{
    class Generics 
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
        }

        public static void InternalLesson1()
        {
            List<Animal> animalList = new List<Animal>();

            List<int> numList = new List<int>();

            numList.Add(12);

            animalList.Add(new Animal() { Name = "Doug" });
            animalList.Add(new Animal() { Name = "Paul" });
            animalList.Add(new Animal() { Name = "Sally" });

            animalList.Insert(1, new Animal() { Name = "Steve" });
            animalList.RemoveAt(1);

            Console.WriteLine("Num of Animals : {0}",
                animalList.Count);

            foreach (Animal animal in animalList)
            {
                Console.WriteLine(animal.Name);
            }

            int x = 5, y = 4;
            Animal.GetSum<int>(ref x, ref y);

            string strX = "5", strY = "4";
            Animal.GetSum<string>(ref strX, ref strY);
        }

        public static void InternalLesson2()
        {
            Rectangle<int> rec1 = new Rectangle<int>(20, 50);
            Console.WriteLine(rec1.GetArea());

            Rectangle<string> rec2 = new Rectangle<string>("20", "50");
            Console.WriteLine(rec2.GetArea());
        }

        public struct Rectangle<T>
        {
            private T width;
            private T length;

            public T Width
            {
                get { return width; }
                set { width = value; }
            }
            public T Length 
            {
                get { return length; }
                set { length = value; }
            }

            public Rectangle(T w, T l)
            {
                width = w;
                length = l;
            }

            public string GetArea()
            {
                double dblWidth = Convert.ToDouble(Width);
                double dblLength = Convert.ToDouble(Length);

                return string.Format($"{Width} * {Length} = {dblWidth * dblLength}");
            }
        }
    }
}
