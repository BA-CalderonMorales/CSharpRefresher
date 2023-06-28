namespace Fundamentals.Interfaces
{
    // interfaces commonly have names that are adjectives
    // commonly have names that start with the letter I.
    interface IDrivable
    {
        int Wheels { get; set; }
        double Speed { get; set; }

        void Move();
        void Stop();
    }
}
