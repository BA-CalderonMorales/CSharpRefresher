namespace Fundamentals.Interfaces
{
    public class Interfaces
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
        }

        public static void InternalLesson1()
        {
            Vehicle buick = new Vehicle("Buick", 4, 160);

            if (buick is IDrivable)
            {
                buick.Move();
                buick.Stop();
            } else
            {
                Console.WriteLine("The {0} can't be driven.",
                    buick.Brand);
            }
        }
        public static void InternalLesson2()
        {
            IElectronicDevice TV = RemoteControl.GetDevice();

            PowerButton powerButton = new PowerButton(TV);

            powerButton.Execute();
            powerButton.Undo();

            powerButton.Execute();
            powerButton.Undo();
        }
    }
}
