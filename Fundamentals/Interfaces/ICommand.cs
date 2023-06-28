namespace Fundamentals.Interfaces
{
    interface ICommand
    {
        void Execute();
        void Undo();
    }
}
