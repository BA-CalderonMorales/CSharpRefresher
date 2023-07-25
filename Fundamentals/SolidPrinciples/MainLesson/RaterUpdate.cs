using Fundamentals.SolidPrinciples.MainLesson.Interfaces;
using Fundamentals.SolidPrinciples.MainLesson.Logger;

namespace Fundamentals.SolidPrinciples.MainLesson
{
    public class RaterUpdate : IRatingUpdate
    {
        public RatingEngine Engine { get; set; }
        public ILogger Logger { get; set; } = new ConsoleLogger();

        public RaterUpdate(RatingEngine engine)
        {
            Engine = engine;
        }
        public void UpdateRating(decimal rating)
        {
            Engine.Rating = rating;
            Logger.Log($"Final rating: {rating}");
            Logger.Log("Rating completed.");
        }
    }
}
