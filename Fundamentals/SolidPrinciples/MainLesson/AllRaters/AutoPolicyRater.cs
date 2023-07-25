using Fundamentals.SolidPrinciples.MainLesson.Interfaces;

namespace Fundamentals.SolidPrinciples.MainLesson.AllRaters
{
    public class AutoPolicyRater : Rater
    {
        public AutoPolicyRater(IRatingUpdate ratingUpdate)
            : base(ratingUpdate)
        {
        }

        public override void Rate(Policy policy)
        {
            Logger.Log("Rating AUTO policy...");
            Logger.Log("Validating policy");
            if (string.IsNullOrEmpty(policy.Make))
            {
                Logger.Log("Auto policy must specify Make");
                return;
            }
            if (policy.Make == "BMW")
            {
                if (policy.Deductible < 500)
                {
                    RatingUpdate.UpdateRating(1000m);
                    return;
                }
                RatingUpdate.UpdateRating(900m);
            }
        }
    }
}
