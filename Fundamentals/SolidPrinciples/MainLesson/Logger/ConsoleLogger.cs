using Fundamentals.SolidPrinciples.MainLesson.Interfaces;

namespace Fundamentals.SolidPrinciples.MainLesson.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}