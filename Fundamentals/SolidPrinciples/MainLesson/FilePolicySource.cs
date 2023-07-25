namespace Fundamentals.SolidPrinciples.MainLesson
{
    public class FilePolicySource
    {
        public string GetPolicyFromSource(string source)
        {
            return File.ReadAllText(source);
        }
    }
}
