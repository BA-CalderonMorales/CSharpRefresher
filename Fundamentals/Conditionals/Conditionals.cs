namespace Fundamentals.Conditionals
{
    public class Conditionals
    {
        public static void Lesson()
        {
            // Relational Operators : > < >= <= == !=
            // Logical Operators : && || !
            int age = 17;
            ChooseSchool(age);
            Ternary(age);
            Switch(age);
            Compare("Brandon", "Joe");
            Compare("Tom", "tom");
            Compare("Bob", "Bob");
        }

        public static void Compare(string name1, string name2)
        {
            Console.WriteLine("Checking if {0} and {1} are equal", name1, name2);
            if (name2.Equals(name1, StringComparison.Ordinal))
            {
                Console.WriteLine("Names are equal!");
            }
            else
            {
                Console.WriteLine("Names are not equal!");
            }
        }

        public static void Switch(int age)
        {
            switch (age)
            {
                case 1:
                case 2:
                    Console.WriteLine("Go to Day Care dude...");
                    break;
                case 3:
                case 4:
                    Console.WriteLine("Go to Preschool kid...");
                    break;
                case 5:
                    Console.WriteLine("Go to Kindergarten!");
                    break;
                default:
                    Console.WriteLine("Go to another school!");
                    goto OtherSchool; // bad practice, but possible...
            }

        OtherSchool:
            Console.WriteLine("Elementary, Middle, High School...");
        }

        public static void Ternary(int age)
        {
            bool canDrive = age >= 16 ? true : false;
            Console.WriteLine("Cna this person drive? {0}", canDrive);
        }

        public static void ChooseSchool(int age)
        {
            if ((age >= 5) && (age <= 7))
            {
                Console.WriteLine("Go to elementary school");
            }
            if ((age > 7) && (age < 13))
            {
                Console.WriteLine("Go to middle school");
            }
            if ((age > 13) && (age < 19))
            {
                Console.WriteLine("Go to high school");
            }
            if ((age > 19) && (age <= 67))
            {
                Console.WriteLine("Go to college or work, please.");
            }

            if ((age < 14) || (age > 67))
            {
                Console.WriteLine("Laws will not (should not) allow you to work. Retire, please.");
            }
            Console.WriteLine("! true = " + (!true));

        }
    }
}
