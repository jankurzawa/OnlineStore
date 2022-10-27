namespace OnlineStore.Controler.Factories
{
    public class Validation
    {
        private IInputManager inputManager;

        public Validation()
            => inputManager = new ConsoleInputSystem();

        public string checkInputWithRegexString(Regex regex, string field)
        {
            string inputValue;
            bool isCorrect;
            do
            {
                if (field == "Mail")
                    inputValue = inputManager.FetchStringValue($"{field}:").ToLower();
                else if (field == "Login" || field == "Password")
                    inputValue = inputManager.FetchStringValue($"{field}:");
                else
                    inputValue = inputManager.FetchStringValue($"{field}:").ToUpper();
                isCorrect = regex.IsMatch(inputValue);
                if (isCorrect == false)  
                    Console.WriteLine($"Wrong {field.ToLower()} format!"); 
            }
            while (isCorrect != true);

            return inputValue;
        }

        public int checkInputInt(string field, int minValue)
        {
            int outputValue;
            bool isCorrect;
            do
            {
                Int32.TryParse(inputManager.FetchStringValue($"{field}:"), out outputValue);
                isCorrect = (outputValue > minValue);
                if (isCorrect == false) { Console.WriteLine($"Wrong {field.ToLower()} format! Only numbers."); }
            }
            while (isCorrect != true);

            return outputValue;
        }
    }
}
