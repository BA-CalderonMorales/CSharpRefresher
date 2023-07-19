namespace Fundamentals.SolidPrinciples.RatingEngineFiles
{
    public class FilePolicySource
    {
        public string GetPolicyFromSource(string source)
        {
            return File.ReadAllText(source);
        }
    }
}
