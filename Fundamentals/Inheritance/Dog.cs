namespace Fundamentals.Inheritance
{
    public class Dog : Animal
    {
        public string Sound2 { get; set; } = "Grr";

        public override void MakeSound()
        {
            Console.WriteLine($"{this.Name} says {this.Sound} and {this.Sound2}");
        }

        public Dog(string name = "No Name",
            string sound = "No Sound",
            string sound2 = "No Sound 2") : base(name, sound)
        {
            Sound2 = sound2;
        }
    }
}
