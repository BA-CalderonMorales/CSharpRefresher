using Fundamentals.SolidPrinciples.MainLesson.Interfaces;

namespace Fundamentals.SolidPrinciples.MainLesson.AllRaters
{
    public class LandPolicyRater : Rater
    {
        public LandPolicyRater(IRatingUpdate ratingUpdate)
            : base(ratingUpdate)
        {
        }

        public override void Rate(Policy policy)
        {
            Logger.Log("Rating LAND policy...");
            Logger.Log("Validating policy.");
            if (policy.BondAmount == 0 || policy.Valuation == 0)
            {
                Logger.Log("Land policy must specify Bond Amount and Validation");
                return;
            }
            if (policy.BondAmount < 0.8m * policy.Valuation)
            {
                Logger.Log("Insufficient bond amount.");
                return;
            }
            RatingUpdate.UpdateRating(policy.BondAmount * 0.05m);
        }
    }
}
