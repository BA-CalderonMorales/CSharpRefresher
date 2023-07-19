using System.Text;

namespace Fundamentals.SolidPrinciples.RatingEngineFiles
{
    public class RaterFactory
    {
        public static Rater Create(Policy policy, RatingEngine engine, bool useReflection = false)
        {
            if (useReflection)
            {
                // demonstrating how reflection can be used here instead of switch
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("Fundamentals.");
                    builder.Append("SolidPrinciples.");
                    builder.Append("RatingEngineFiles.");
                    builder.Append($"{policy.Type}PolicyRater");

                    return (Rater)Activator.CreateInstance(
                                Type.GetType(builder.ToString()),
                                new object[]
                                {
                                    engine,
                                    engine.Logger
                                });
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                switch (policy.Type)
                {
                    case PolicyType.Auto:
                        return new AutoPolicyRater(engine, engine.Logger);
                    case PolicyType.Land:
                        return new LandPolicyRater(engine, engine.Logger);
                    case PolicyType.Life:
                        return new LifePolicyRater(engine, engine.Logger);
                    case PolicyType.Flood:
                        return new FloodPolicyRater(engine, engine.Logger);
                    default:
                        return new Rater(engine, engine.Logger);
                }
            }
        }
    }
}
