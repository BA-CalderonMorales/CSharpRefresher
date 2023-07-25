using Fundamentals.SolidPrinciples.MainLesson;
using Fundamentals.SolidPrinciples.MainLesson.AllRaters;
using Fundamentals.SolidPrinciples.MainLesson.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SolidPrinciples
{
    public class FakeRaterUpdate : IRatingUpdate
    {
        public decimal? NewRating { get; private set; }
        public void UpdateRating(decimal rating)
        {
            NewRating = rating;
        }
    }

    public class AutoPolicyRaterRaterTests
    {
        [Fact]
        public void SetRatingTo1000ForBMWWith250Deductible()
        {
            var policy = new Policy()
            {
                Type = PolicyType.Auto,
                Make = "BMW",
                Deductible = 250m
            };
            var ratingUpdater = new FakeRaterUpdate();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(1000m, ratingUpdater.NewRating.Value);
        }
    }
}
