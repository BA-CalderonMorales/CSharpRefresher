namespace Fundamentals.Abstraction
{
    public abstract class Shape // keyword abstract - meaning it will never be instantiated...
    {
        // you can define non-abstract methods inside of an abstract class
        public string Name { get; set; }

        public virtual void GetInfo()
        {
            Console.WriteLine($"This is a {Name}");
        }

        public abstract double Area(); // abstract methods can only be made inside of abstract classes.
    }
}
