namespace OnlineStore.View
{
    public class ConsoleInputSystem : IInputManager
    {
        public string FetchStringValue(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().Trim();
        }

        public int FetchIntValue(string prompt)
        {
            int valueResult = 0;
            bool isResultString = true;

            while (isResultString)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine().Trim(), out valueResult) && valueResult > 0) isResultString = false;
                else Console.WriteLine("Invalid input");
            }
            return valueResult;
        }

        public double FetchDoubleValue(string prompt)
        {
            Console.WriteLine(prompt);
            double valueResult = 0;
            bool isResultString = true;

            while (isResultString)
            {
                if (double.TryParse(Console.ReadLine().Trim(), out valueResult) && valueResult > 0) isResultString = false;
                else Console.WriteLine("Invalid input");
            }
            return valueResult;
        }

        public uint FetchUintValue(string prompt)
        {
            Console.WriteLine(prompt);
            uint valueResult;
            while (true)
            {
                if (uint.TryParse(Console.ReadLine().Trim(), out valueResult)) break;
                else Console.WriteLine("Invalid input");
            }
            return valueResult;
        }
    }
}
