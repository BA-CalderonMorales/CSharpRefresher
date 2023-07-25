using Fundamentals.SolidPrinciples.MainLesson;

namespace Fundamentals.Tests;

public class Utils
{
    public Utils() { }

    public static bool AssertPoliciesEqual(Policy test, Policy expected)
    {
        if (test.Equals(expected))
        {
            return true;
        }
        return false;
    }
}
