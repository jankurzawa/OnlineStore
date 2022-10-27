namespace OnlineStore.View
{
    public interface IInputManager
    {
        public string FetchStringValue(string propmpt);
        public int FetchIntValue(string prompt);
        public double FetchDoubleValue(string prompt);
        public uint FetchUintValue(string prompt);
    }
}
