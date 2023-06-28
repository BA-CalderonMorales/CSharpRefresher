namespace Fundamentals.Abstraction
{
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Name = "Circle";
            Radius = radius;
        }

        public override double Area()
        {
            return Math.PI * (Math.Pow(Radius, 2.0));
        }

        public override void GetInfo()
        {
            base.GetInfo(); // first will execute base class of GetInfo.
            Console.WriteLine($"It has a Radius of {Radius}"); // specific behavior related to the Circle class.
        }
    }
}
