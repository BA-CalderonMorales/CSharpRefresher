namespace Fundamentals.Enumerables
{
    public class Animal
    {
        private string _name { get; set; }
        private string _sound { get; set; }

        protected AnimalIDInfo animalIDInfo = new AnimalIDInfo();

        public Animal() : this("No Name", "No Sound") { } // default animal if nothing is set

        public Animal(string name) : this(name, "No Sound") { } // if sound is not provided

        public Animal(string name, string sound)
        {
            Name = name;
            Sound = sound;
        }

        public string Owner { get; set; } = "No Owner";

        public string Name
        {
            get { return this._name; }
            set
            {
                if (!value.Any(char.IsDigit))
                {
                    if (!value.Any(char.IsDigit))
                    {
                        this._name = value;
                    }
                    else
                    {
                        this._name = "No Name";
                        Console.WriteLine("Name can't contain numbers");
                    }
                }
            }
        }

        public string Sound
        {
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
        public void SetAnimalIDInfo(int idNum, string owner)
        {
            animalIDInfo.IDNum = idNum;
            animalIDInfo.Owner = owner;
        }

        public void GetAnimalIDInfo()
        {
            Console.WriteLine($"{Name} has the ID of {animalIDInfo.IDNum} and is owned by {animalIDInfo.Owner}");
        }

        public virtual void MakeSound()
        {
            Console.WriteLine("{0} says {1}", this._name, this._sound);
        }

        public class AnimalHealth
        {
            public bool HealthyWeight(double height, double weight)
            {
                double calc = height / weight;
                if ((calc >= .18) && (calc <= .27))
                {
                    return true; // healthy weight...
                }
                return false;
            }
        }
    }
}
