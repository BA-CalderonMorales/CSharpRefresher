using Fundamentals.SolidPrinciples.SingleResponsibilityPrinciple;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fundamentals.SolidPrinciples.RatingEngineFiles
{
    public class RatingEngine
    {
        public ConsoleLogger Logger { get; set; } = new ConsoleLogger();
        public FilePolicySource PolicySource { get; set; } = new FilePolicySource();
        public FilePolicyDeserializer PolicyDeserializer { get; set; } = new FilePolicyDeserializer();
        public decimal Rating { get; set; }

        public void Rate(bool srp = false)
        {
            if (srp)
            {
                GoodSOLID();
            }
            else
            {
                NoSOLID();
            }
            DetermineRating();
        }

        private void GoodSOLID()
        {
            #region Responsibilities and Testability
            /**
             * Responsibilities have a direct relationship with testability.
             * 
             * - When classes have many responsibilities, it becomes more difficult to test them.
             * - Tests become longer, complex, brittle, and coupled to implmentation.
             */
            #endregion
            Logger.Log("Starting rate.");
            Logger.Log("Loading policy.");
            string policyJson = PolicySource.GetPolicyFromSource("policy.json"); // load policy - open file policy.json
            var policy = PolicyDeserializer.GetPolicyFromJsonString(policyJson);
            var rater = RaterFactory.Create(policy, this, true);
            rater?.Rate(policy);
            Logger.Log("Rating completed.");
        }

        private void NoSOLID()
        {
            // Logging - Console.WriteLine("Starting rate.");
            // Persistence - string policy = File.ReadAllText("policy.json");
            // Encoding Format - var policy = JsonConvert.DeserializeObject<Policy>...
            // Business Rule - case Policy.Auto: ...
            // Validation - if (String.IsNullOrEmpty(policy.Make))
            // Age Calculation - ...specific formulas used
            Console.WriteLine("Starting rate.");
            Console.WriteLine("Loading policy.");

            // load policy - open file policy.json
            string policyJson = File.ReadAllText("policy.json");

            var policy = JsonConvert.DeserializeObject<Policy>(policyJson,
                new StringEnumConverter());

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    Console.WriteLine("Rating AUTO policy...");
                    Console.WriteLine("Validating policy.");
                    if (string.IsNullOrEmpty(policy.Make))
                    {
                        Console.WriteLine("Auto policy must specify Make");
                        return;
                    }
                    if (policy.Make == "BMW")
                    {
                        if (policy.Deductible < 500)
                        {
                            Rating = 1000m;
                        }
                        Rating = 900m;
                    }
                    break;

                case PolicyType.Land:
                    Console.WriteLine("Rating LAND policy...");
                    Console.WriteLine("Validating policy.");
                    if (policy.BondAmount == 0 || policy.Valuation == 0)
                    {
                        Console.WriteLine("Land policy must specify Bond Amount and Validation");
                        return;
                    }
                    if (policy.BondAmount < 0.8m * policy.Valuation)
                    {
                        Console.WriteLine("Insufficient bond amount.");
                        return;
                    }
                    Rating = policy.BondAmount * 0.05m;
                    break;

                case PolicyType.Life:
                    Console.WriteLine("Rating LIFE policy...");
                    Console.WriteLine("Validating policy.");
                    if (policy.DateOfBirth == DateTime.MinValue)
                    {
                        Console.WriteLine("Life policy must include DOB.");
                        return;
                    }
                    if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
                    {
                        Console.WriteLine("Centenarians are not eligible for coverate.");
                        return;
                    }
                    if (policy.Amount == 0)
                    {
                        Console.WriteLine("Life policy must include an Amount.");
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
                        Rating = baseRate * 2;
                        break;
                    }
                    Rating = baseRate;
                    break;

                default:
                    Console.WriteLine("Uknown policy type.");
                    break;
            }

            Console.WriteLine("Rating completed.");
        }

        private void DetermineRating()
        {
            if (Rating > 0)
            {
                Console.WriteLine($"Rating: {Rating}");
            }
            else
            {
                Console.WriteLine("No rating produced.");
            }
        }
    }
}
