namespace Fundamentals.Enumerables
{
    public class Enumerables
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
            InternalLesson3();
        }

        private static void InternalLesson1()
        {
            AnimalFarm myAnimals = new AnimalFarm();
            myAnimals[0] = new Animal("Wilbur");
            myAnimals[1] = new Animal("Templeton");
            myAnimals[2] = new Animal("Gander");
            myAnimals[3] = new Animal("Charlotte");
            foreach (Animal animal in myAnimals)
            {
                Console.WriteLine(animal.Name);
            }
        }

        private static void InternalLesson2()
        {
            Box box1 = new Box(2, 3, 4);
            Box box2 = new Box(5, 6, 7);
            Box box3 = box1 + box2;

            Console.WriteLine($"Box 3 : {box3}");
            Console.WriteLine($"Box Int: {(int)box3}");
            Box box4 = (Box)4;

        }

        private static void InternalLesson3()
        {
            var shopkins = new { Name = "Shopkins", Price = 4.99 };
            Console.WriteLine("{0} cost ${1}",
                shopkins.Name, shopkins.Price);

            var toyArray = new[]
            {
                new { Name = "Yo-Kai Pack", Price = 4.99 },
                new { Name = "Legos", Price = 9.99 }
            };

            foreach (var toy in toyArray)
            {
                Console.WriteLine("{0} cost ${1}",
                    toy.Name, toy.Price);
            }
        }
    }
}
