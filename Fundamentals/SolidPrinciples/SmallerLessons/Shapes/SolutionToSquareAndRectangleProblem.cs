using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.SmallerLessons.Shapes
{
    public class RectangeSolutionOne
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public bool IsSquare => Height == Width; // Removes need for Square class.
    }

    public class RectangleSolutionTwo
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class SquareSolutionTwo
    {
        public int Side { get; set; } // No need to store separate variables, will require another Calculator
    }

    public class SolutionToSquareAndRectangleProblem
    {

        public static void Solution1()
        {
            // Try here
        }

        public static void Solution2()
        {
            // Try here
        }
    }
}
