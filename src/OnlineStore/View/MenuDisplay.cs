namespace OnlineStore.View
{
    public class MenuDisplay : IMenuDisplay
    {
        public void ClearScreen()
            => Console.Clear();
        public void PrintMessage(string message)
            => Console.WriteLine(message);
        public void PrintOptions(List<string> options)
        {
            Console.WriteLine("~~~~~~ Options available ~~~~~~");
            foreach (string option in options) Console.WriteLine(option);
        }
    }
}
