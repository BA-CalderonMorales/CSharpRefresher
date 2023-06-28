namespace Fundamentals.Interfaces
{
    class RemoteControl
    {
        public static IElectronicDevice GetDevice()
        {
            return new Television();
        }
    }
}
