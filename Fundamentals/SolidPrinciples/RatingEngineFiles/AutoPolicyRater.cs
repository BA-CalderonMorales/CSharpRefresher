using Fundamentals.SolidPrinciples.SingleResponsibilityPrinciple;

namespace Fundamentals.SolidPrinciples.RatingEngineFiles
{
    public class AutoPolicyRater : Rater
    {
        public AutoPolicyRater(RatingEngine engine, ConsoleLogger logger) : base(engine, logger)
        {
        }

        public override void Rate(Policy policy)
        {
            Logger.Log("Rating AUTO policy...");
            Logger.Log("Validating policy");
            if (String.IsNullOrEmpty(policy.Make))
            {
                Logger.Log("Auto policy must specify Make");
                return;
            }
            if (policy.Make == "BMW")
            {
                if (policy.Deductible < 500)
                {
                    Engine.Rating = 1000m;
                }
                Engine.Rating = 900m;
            }
        }
    }
}
