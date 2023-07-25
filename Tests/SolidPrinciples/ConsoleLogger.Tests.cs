using Fundamentals.SolidPrinciples.MainLesson;
using Fundamentals.SolidPrinciples.MainLesson.AllRaters;
using Fundamentals.SolidPrinciples.MainLesson.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.SolidPrinciples
{
    public class FakeLogger : ILogger
    {
        public List<string> LoggedMessages { get; } = new List<string>();
        public void Log(string message)
        {
            LoggedMessages.Add(message);
        }
    }

    public class AutoPolicyRaterRate
    {
        [Fact]
        public void LogsMakeRequiredMessageGivenPolicyWithoutMake()
        {
            var logger = new FakeLogger();
            var policy = new Policy() { Type = PolicyType.Auto };
            var rater = new AutoPolicyRater(null);
            rater.Logger = logger;
            
            rater.Rate(policy);

            Assert.Equal("Auto policy must specify Make", logger.LoggedMessages.Last());
        }
    }
}
