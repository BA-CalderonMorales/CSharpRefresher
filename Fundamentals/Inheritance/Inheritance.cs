namespace Fundamentals.Inheritance
{
    public class Inheritance
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
        }

        public static void InternalLesson1()
        {
            Animal whiskers = new Animal()
            {
                Name = "Whiskers",
                Sound = "Meow"
            };

            Dog grover = new Dog()
            {
                Name = "Grover",
                Sound = "Woof",
                Sound2 = "Grrrrr"
            };

            grover.Sound = "Wooooof";
            whiskers.MakeSound();
            grover.MakeSound();

            whiskers.SetAnimalIDInfo(12345, "Sally Smith");

            grover.SetAnimalIDInfo(57574, "Paul Brown");

            whiskers.GetAnimalIDInfo();
        }

        public static void InternalLesson2() {
            // Class within the same file - nice
            Animal.AnimalHealth getHealth = new Animal.AnimalHealth();
            Console.WriteLine("Is my animal healthy? {0}",
                getHealth.HealthyWeight(11, 46));

            Animal monkey = new Animal()
            {
                Name = "Happy",
                Sound = "Eeee"
            };

            Animal spot = new Dog()
            {
                Name = "Spot",
                Sound = "Wooooof",
                Sound2 = "Geerr"
            };

            spot.MakeSound();
            monkey.MakeSound();
        }
    }
}
