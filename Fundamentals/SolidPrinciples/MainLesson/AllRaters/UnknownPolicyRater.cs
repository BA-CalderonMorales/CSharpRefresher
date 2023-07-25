using Fundamentals.SolidPrinciples.MainLesson.Interfaces;

namespace Fundamentals.SolidPrinciples.MainLesson.AllRaters
{
    public class UnknownPolicyRater : Rater
    {
        public UnknownPolicyRater(IRatingUpdate ratingUpdate)
            : base(ratingUpdate) { }

        public override void Rate(Policy policy)
        {
            Logger.Log("Uknown policy type");
        }
    }
}
