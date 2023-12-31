﻿using Fundamentals.SolidPrinciples.MainLesson.Interfaces;

namespace Fundamentals.SolidPrinciples.MainLesson.AllRaters
{
    public class FloodPolicyRater : Rater
    {
        public FloodPolicyRater(IRatingUpdate ratingUpdate)
            : base(ratingUpdate)
        {
        }

        public override void Rate(Policy policy)
        {
            Logger.Log("Rating FLOOD policy...");
            Logger.Log("Validating policy");
            if (policy.BondAmount == 0 || policy.Valuation == 0)
            {
                Logger.Log("Flood policy must specify Bond Amount and Valuation");
                return;
            }
            if (policy.ElevationAboveSeaLevel <= 0)
            {
                Logger.Log("Flood policy is not available for elevations at or below sea level.");
                return;
            }
            if (policy.BondAmount < 0.8m * policy.Valuation)
            {
                Logger.Log("Insufficient bond amount");
                return;
            }
            decimal multiple = 1.0m;
            if (policy.ElevationAboveSeaLevel < 100)
            {
                multiple = 2.0m;
            }
            else if (policy.ElevationAboveSeaLevel < 500)
            {
                multiple = 1.5m;
            }
            else if (policy.ElevationAboveSeaLevel < 1000)
            {
                multiple = 1.1m;
            }
            RatingUpdate.UpdateRating(policy.BondAmount * 0.05m * multiple);
        }
    }
}
