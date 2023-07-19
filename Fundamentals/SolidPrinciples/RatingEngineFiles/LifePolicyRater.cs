﻿using Fundamentals.SolidPrinciples.SingleResponsibilityPrinciple;

namespace Fundamentals.SolidPrinciples.RatingEngineFiles
{
    public class LifePolicyRater : Rater
    {
        public LifePolicyRater(RatingEngine engine, ConsoleLogger logger) : base(engine, logger)
        {
        }

        public override void Rate(Policy policy)
        {
            Logger.Log("Rating LIFE policy...");
            Logger.Log("Validating policy.");
            if (policy.DateOfBirth == DateTime.MinValue)
            {
                Logger.Log("Life policy must include DOB.");
                return;
            }
            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                Logger.Log("Centenarians are not eligible for coverate.");
                return;
            }
            if (policy.Amount == 0)
            {
                Logger.Log("Life policy must include an Amount.");
                return;
            }
            int age = DateTime.Today.Year - policy.DateOfBirth.Year;
            if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < policy.DateOfBirth.Day ||
                DateTime.Today.Month < policy.DateOfBirth.Month)
            {
                age--;
            }
            decimal baseRate = policy.Amount * age / 200;
            if (policy.IsSmoker)
            {
                Engine.Rating = baseRate * 2;
                return;
            }
            Engine.Rating = baseRate;
        }
    }
}
