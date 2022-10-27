namespace OnlineStore.Controler.Factories
{
    public class AddressFactory
    {
        private readonly IInputManager _inputManager;
        private Validation validation;

        public AddressFactory(IInputManager inputManager)
        {
            _inputManager = inputManager;
            validation = new();
        }
        public Address GetAddress()
        {
            string country;
            string city;
            string street;
            int houseNumber;
            int doorNumber;
            int postalCode;

            do
            {
                Regex countryRegex = new Regex(@"^[A-Z]{2,20}\s?\-?[A-Z]{0,10}\s?\-?[A-Z]{0,10}?$");
                country = validation.checkInputWithRegexString(countryRegex, "Country");

                Regex cityRegex = new Regex(@"^[A-Z]{2,20}\s?\-?[0-9]{0,3}[A-Z]{0,10}$");
                city = validation.checkInputWithRegexString(cityRegex, "City");

                Regex streetRegex = new Regex(@"^[A-Z0-9]+\s?\-?[0-9]{0,3}[A-Z]{0,10}$");
                street = validation.checkInputWithRegexString(streetRegex, "Street");

                houseNumber = validation.checkInputInt("House number", 0);
                doorNumber = validation.checkInputInt("Door number", 0);
                postalCode = validation.checkInputInt("Postal code", 1000);
            } while (_inputManager.FetchStringValue("[y] - agree") != "y");

            return new Address(country, city, street, houseNumber, doorNumber, postalCode);
        }
    }
}
