namespace Fundamentals.Abstraction
{
    public class Rectangle : Shape
    {
        public double Length { get; set; }

        public double Width { get; set; }


        public Rectangle(double length, double width)
        {
            Name = "Rectangle";
            Length = length;
            Width = width;
        }

        public override double Area()
        {
            return Length * Width;
        }

        public override void GetInfo()
        {
            base.GetInfo(); // first will execute base class of GetInfo.
            Console.WriteLine($"It has a Length of {Length} and Width of {Width}"); // specific behavior related to the Rectangle class.
        }
    }
}
