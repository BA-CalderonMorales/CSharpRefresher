using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Fundamentals.SolidPrinciples.MainLesson.Logger;

namespace Fundamentals.SolidPrinciples.MainLesson
{

    public class FilePolicyDeserializer
    {
        public ConsoleLogger Logger { get; set; } = new ConsoleLogger();

        public Policy GetPolicyFromJsonString(string json)
        {
            var result = JsonConvert.DeserializeObject<Policy>(json, new StringEnumConverter());
            Policy policy = new Policy();

            if (result != null)
            {
                policy = result;

            }
            return policy;
        }
    }
}
