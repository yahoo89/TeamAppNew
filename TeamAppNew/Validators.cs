namespace TeamAppNew_OLD;

internal partial class Program
{
    public static bool isValidContractType(string message)
    {
        bool isFulltime = false;
        bool isValidAnswer = false;

        while (!isValidAnswer)
        {
            Console.Write(message);
            var contractType = Console.ReadLine()?.Trim().ToLower();

            switch (contractType)
            {
                case "yes":
                    isFulltime = true;
                    isValidAnswer = true;
                    break;
                case "no":
                    isFulltime = false;
                    isValidAnswer = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter a valid answer ('YES' or 'NO').");
                    break;
            }
        }

        return isFulltime;
    }
    public static int isValidInt(string message)
    {
        int value;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Please enter a valid number from 1 to infinity.");
        }
    }
    public static int isValidInt(string message, int max)
    {
        int value;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out value) && value > 0 && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Please enter a valid number between 1 and {max}.");
        }
    }


    static string isValidString(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && !int.TryParse(input, out _) && IsValidCharacters(input))
            {
                return input;
            }

            Console.WriteLine("Invalid string. Please try again.");
        }
    }

    static bool IsValidCharacters(string input)
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
