namespace OnlineStore.View.Interfaces
{
    public interface IMenuDisplay
    {
        void ClearScreen();
        void PrintMessage(string message);
        void PrintOptions(List<string> options);
    }
}
