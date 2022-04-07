namespace UILayer;

public static class InputValidation
{
    /// <summary>
    /// Method to check if user input is valid. An invalid string is null or whitespace;
    /// </summary>
    /// <returns>string</returns>
    public static string ValidString()
    {
    EnterString:
        string valid = Console.ReadLine() ?? "";

        if (String.IsNullOrWhiteSpace(valid))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid input!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto EnterString;
        }
        return valid;
    }

    public static int ValidInteger()
    {
    EnterInteger:
        int index;

        if (Int32.TryParse(Console.ReadLine(), out index) && (index >= 0))
        {
            return index;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid input!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto EnterInteger;
        }
    }

    public static decimal ValidDecimal()
    {
    EnterDecimal:
        decimal price;

        if (Decimal.TryParse(Console.ReadLine(), out price) && (price >= 0))
        {
            return price;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid input!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto EnterDecimal;
        }
    }
}