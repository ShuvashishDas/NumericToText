using NumberConversionApi.Interfaces;
using NumberConversionApi.Constants;

namespace NumberConversionApi.Services;

public class NumberConversionService : INumberConversionService
{
    public string Convert(string value)
    {
        ValidateNumber(value);

        ExtractParts(value.Trim(), out List<int> numberPart, out int decimalPart);

        var wholeNumberPart = NumberToText(numberPart);
        var totalAmountInText = $"{wholeNumberPart} dollar{(wholeNumberPart == "one" || wholeNumberPart == "zero" ? "" : "s")}";

        if (decimalPart > 0)
            totalAmountInText += $" and {HundredToText(decimalPart)} cents";

        return totalAmountInText.ToUpper();
    }

    private bool ValidateNumber(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("No number entered.");

        if (!decimal.TryParse(input, out decimal value))
            throw new ArgumentException("Invalid number entered.");

        if (value < 0)
            throw new ArgumentException("Currency can not be negative.");

        var currencyParts = input.Split('.');
        var wholeNumberPart = currencyParts.Length > 0 ? currencyParts[0].Trim() : "";

        if (wholeNumberPart.Length > 0 && wholeNumberPart.Length > 18)
            throw new ArgumentException("Sorry, I can only process currency amounts up to quadrillions.");

        var decimalPart = currencyParts.Length > 1 ? currencyParts[1].Trim() : "";
        if (decimalPart.Length > 2)
            throw new ArgumentException("Cents can not have more than two decimal places.");

        return true;
    }

    private void ExtractParts(string number, out List<int> numberPart, out int decimalPart)
    {
        numberPart = new List<int>();

        var parts = number.Split('.');
        var integerPart = (Int64)Math.Floor(Math.Abs(decimal.Parse(number)));

        while (integerPart > 0)
        {
            var lastThreeDigits = (integerPart % 1000);
            numberPart.Add((int)lastThreeDigits);
            integerPart /= 1000;
        }

        decimalPart = parts.Length > 1 ? int.Parse(parts[1].Trim()) : 0;
    }

    private string NumberToText(List<int> numbers)
    {
        if (numbers.Count == 0) return "zero";

        var result = string.Empty;
        for (int index = Data.allThousands.Length - 1; index >= 0; index--)
        {
            var amount = numbers.Count > index + 1 ? numbers[index + 1] : 0;
            if (amount > 0)
                result += $"{HundredToText(amount)} {Data.allThousands[index]} ";
        }

        var hundred = numbers.Count > 0 ? numbers[0] : 0;
        if (hundred > 0)
            result += HundredToText(hundred, true);

        return result.Trim();
    }

    private string HundredToText(int number, bool hasThousands = false)
    {
        if (number == 0) return "zero";

        var result = string.Empty;
        var showHyphen = false;

        if (number / 100 >= 1)
        {
            result += $"{Data.beforeTen[(int)(number / 100)]} hundred ";
            number %= 100;
        }

        if (hasThousands && result.Length > 0)
            result += "and ";

        if (number / 10 >= 2)
        {
            result += $"{Data.allTens[number / 10]} ";
            number %= 10;
            showHyphen = number > 0;
        }
        else if (number / 10 == 1)
        {
            result += $"{Data.betweenTenAndNineteens[(int)(number % 10)]} ";
            number = 0;
        }

        if (number > 0)
        {
            if (showHyphen) result = $"{result.TrimEnd()}-";
            result += $"{Data.beforeTen[(int)number]} ";
        }

        return result.Trim();
    }
}
