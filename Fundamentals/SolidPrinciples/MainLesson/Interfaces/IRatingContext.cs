using Fundamentals.SolidPrinciples.MainLesson.AllRaters;
using Fundamentals.SolidPrinciples.MainLesson.Logger;

namespace Fundamentals.SolidPrinciples.MainLesson.Interfaces
{
    public interface IRatingContext : ILogger, IRatingUpdate
    {
        string LoadPolicyFromFile();
        string LoadPolicyFromURI(string uri);
        Policy GetPolicyFromJsonString(string policyJson);
        Policy GetPolicyFromXmlString(string policyXml);
        Rater CreateRaterForPolicy(Policy policy, IRatingContext context);
        RatingEngine Engine { get; set; }
    }
}
