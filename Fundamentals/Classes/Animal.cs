namespace Fundamentals.Classes
{
    // Classes are blueprints for creating objects
    public class Animal
    {
        // Attributes on a class are called fields
        private string _name { get; set; }

        private string _sound { get; set; }

        public static int numOfAnimals = 0;

        public const string SHELTER = "Brandon's Home for Animals";

        // readonly fields that are set at runtime in constructors can not be changed
        public readonly int idNum;

        // Constructor(s)
        public Animal() : this("No Name", "No Sound") { } // default animal if nothing is set

        public Animal(string name) : this(name, "No Sound") { } // if sound is not provided

        public Animal(string name, string sound)
        {
            SetName(name);
            Sound = sound; // uses value (where value = incoming sound from constructor).

            NumOfAnimals = 1;
            Random rnd = new Random();
            idNum = rnd.Next(1, 2147483640);
        }

        // Method(s) : model the capabilities of an object
        public void MakeSound()
        {
            Console.WriteLine("{0} says {1}", this._name, this._sound);
        }

        public static int GetNumAnimals()
        {
            return numOfAnimals; 
        }

        // Wrong way
        public void SetName(string name)
        {
            if (!name.Any(char.IsDigit))
            {
                if (!name.Any(char.IsDigit))
                {
                    this._name = name;
                }
                else
                {
                    this._name = "No Name";
                    Console.WriteLine("Name can't contain numbers");
                }
            }
        }

        // Wrong way
        public string GetName()
        {
            return this._name;
        }

        // Right way - getters and settings in one scope
        public string Sound
        {
            // value is implicit...
            get { return this._sound; }
            set
            {
                if (value.Length > 10)
                {
                    this._sound = "No Sound";
                    Console.WriteLine("Sound is to long");
                }
                else
                {
                    this._sound = value;
                }
            }
        }

        public string Owner { get; set; } = "No Owner";

        public static int NumOfAnimals
        {
            get { return numOfAnimals; }
            set { numOfAnimals += value; }
        }
    }
}
