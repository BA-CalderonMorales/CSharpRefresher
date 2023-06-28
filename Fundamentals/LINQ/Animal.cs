namespace Fundamentals.LINQ
{
    public class Animal
    {
        private string _name { get; set; }
        private double _weight { get; set; }
        private double _height { get; set; }
        private int _animalID { get; set; }

        public Animal(string name = "No Name",
            double weight = 0,
            double height = 0)
        {
            Name = name;
            Weight = weight;
            Height = height;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int AnimalID
        {
            get { return _animalID; }
            set { _animalID = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} weighs {1}lbs and is {2} inches tall",
                _name, _weight, _height);
        }
    }
}
