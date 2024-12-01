using TeamAppNew.Enums;

namespace TeamAppNew.Helpers;

public class Validator : IValidator
{
    public ContractType ValidateContractType(string message)
    {
        bool isValidAnswer = false;

        while (!isValidAnswer)
        {
            Console.Write(message);
            var contractType = Console.ReadLine()?.Trim().ToLower();
            isValidAnswer = true;

            switch (contractType)
            {
                case "yes":
                    return ContractType.FullTime;
                case "no":
                    return ContractType.PartTime;
                default:
                    isValidAnswer = false;
                    Console.WriteLine("Invalid input. Please enter a valid answer ('YES' or 'NO').");
                    break;
            }
        }

        return ContractType.FullTime; // default option
    }

    public int IsValidTeamSize(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Please enter a valid number from 1 to infinity.");
        }
    }
    public int IsValidAge(string message, int max = 100)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value) && value > 0 && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Please enter a valid number between 1 and {max}.");
        }
    }

    public string IsValidString(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && IsValidCharacters(input))
            {
                return input;
            }

            Console.WriteLine("Invalid string. Please try again.");
        }
    }

    public bool IsValidCharacters(string input)
    {
        foreach (char c in input)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                return false;
            }
        }

        return true;
    }
}