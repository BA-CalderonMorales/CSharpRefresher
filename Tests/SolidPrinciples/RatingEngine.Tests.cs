using Fundamentals.SolidPrinciples;
using Fundamentals.SolidPrinciples.MainLesson;
using Fundamentals.SolidPrinciples.SingleResponsibilityPrinciple;
using Fundamentals.Tests;
using Newtonsoft.Json;

namespace Tests.SolidPrinciples
{
    public class RatingEngineTests
    {
        [Fact] // With BadSrp...
        public static void ReturnsRating10000For200000LandPolicyBadSrp()
        {
            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.Equal(10000, result);
        }

        [Fact] // With GoodSrp...
        public static void ReturnsDefaultPolicyFromEmptyJsonStringGoodSrp()
        {
            var deserializer = new FilePolicyDeserializer();
            var inputJson = "{}";
            Policy expected = new Policy();
            Policy test = deserializer.GetPolicyFromJsonString(inputJson);
            Assert.Equal(true, Utils.AssertPoliciesEqual(test, expected));
        }

        [Fact]
        public static void WritingToPolicyJsonFileAndDeserializingSuccessGoodSrp()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land,
                BondAmount = 200000,
                Valuation = 200000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);
            
            FilePolicySource policySource = new FilePolicySource();
            string policyJson = policySource.GetPolicyFromSource("policy.json");

            Assert.NotEmpty(policyJson);
        }
    }
}
