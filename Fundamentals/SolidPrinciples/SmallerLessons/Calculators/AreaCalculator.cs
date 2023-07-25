using System.Drawing;

namespace Fundamentals.SolidPrinciples.SmallerLessons.Calculators
{
    public class AreaCalculator
    {
        public static int CalculateArea(Rectangle rectangle)
        {
            return rectangle.Height * rectangle.Width;
        }
    }
}
