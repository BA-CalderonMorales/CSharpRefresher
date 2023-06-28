namespace Fundamentals.DT
{
    public class DT
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
        }

        public static void InternalLesson1()
        {
            DateTime awesomeDate = new DateTime(1974, 12, 21);
            Console.WriteLine("Day of the Week : {0}",
                awesomeDate.DayOfWeek);
            awesomeDate = awesomeDate.AddDays(4);
            awesomeDate = awesomeDate.AddMonths(1);
            awesomeDate = awesomeDate.AddYears(1);
            Console.WriteLine("New Date: {0}",
                awesomeDate.Date);
        }

        public static void InternalLesson2()
        {
            TimeSpan lunchTime = new TimeSpan(12, 30, 0);
            lunchTime = lunchTime.Subtract(new TimeSpan(0, 15, 0));
            Console.WriteLine("New Time: {0}",
                lunchTime.ToString());
        }
    }
}
