namespace Fundamentals.ExceptionHandling
{
    public class ExceptionHandling
    {
        public static double DoDivision(double num1, double num2)
        {
            if (num1 == 0)
            {
                throw new System.DivideByZeroException();
            }
            return num1 / num2;
        }

        public static void Lesson()
        {
            DivideByZero();
            DivideByNegative();
        }
        public static void DivideByNegative()
        {

            double num1 = 0;
            double num2 = 1;
            try
            {
                Console.WriteLine("0 / 1 = {0}",
                    DoDivision(num1, num2));
            }
            
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("You can't divide by zero!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType().Name);
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType().Name);
            }

            finally
            {
                // Cleanup can occur after this, for example a database connection
                Console.WriteLine("Cleaning up.");
            }
        }

        public static void DivideByZero()
        {

            double num1 = 5;
            double num2 = 0;
            try
            {
                Console.WriteLine("5 / 0 = {0}",
                    DoDivision(num1, num2));
            }
            
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("You can't divide by zero!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType().Name);
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType().Name);
            }

            finally
            {
                // Cleanup can occur after this, for example a database connection
                Console.WriteLine("Cleaning up.");
            }
        }

    }
}
