using Fundamentals.SolidPrinciples.MainLesson.AllRaters;
using Fundamentals.SolidPrinciples.MainLesson.Interfaces;
using Fundamentals.SolidPrinciples.MainLesson.Logger;
using System.Text;

namespace Fundamentals.SolidPrinciples.MainLesson.AllRaters
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
                    builder.Append("MainLesson.");
                    builder.Append("AllRaters.");
                    builder.Append($"{policy.Type}PolicyRater");

                    return (Rater)Activator.CreateInstance(Type.GetType(builder.ToString()),
                                new object[]
                                {
                                    new RaterUpdate(engine)
                                });
                }
                catch
                {
                    // return null; returning null violates LSP!
                    return new UnknownPolicyRater(new RaterUpdate(engine));
                }
            }
            else
            {
                switch (policy.Type)
                {
                    case PolicyType.Auto:
                        return new AutoPolicyRater(new RaterUpdate(engine));
                    case PolicyType.Land:
                        return new LandPolicyRater(new RaterUpdate(engine));
                    case PolicyType.Life:
                        return new LifePolicyRater(new RaterUpdate(engine));
                    case PolicyType.Flood:
                        return new FloodPolicyRater(new RaterUpdate(engine));
                    default:
                        return new UnknownPolicyRater(new RaterUpdate(engine));
                }
            }
        }
    }
}
