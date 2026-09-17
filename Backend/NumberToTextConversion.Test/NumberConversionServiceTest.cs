using NumberConversionApi.Services;

namespace NumberToTextConversion.Test;

public class NumberConversionServiceTest
{
    [Theory]
    [InlineData("0", "ZERO DOLLAR")]
    [InlineData("1", "ONE DOLLAR")]
    [InlineData("3", "THREE DOLLARS")]
    [InlineData("10", "TEN DOLLARS")]
    [InlineData("21", "TWENTY-ONE DOLLARS")]
    [InlineData("105", "ONE HUNDRED AND FIVE DOLLARS")]
    [InlineData("569", "FIVE HUNDRED AND SIXTY-NINE DOLLARS")]
    [InlineData("1000", "ONE THOUSAND DOLLARS")]
    [InlineData("1234", "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS")]
    [InlineData("1234.35", "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS AND THIRTY-FIVE CENTS")]
    [InlineData("1234567890.35", "ONE BILLION TWO HUNDRED THIRTY-FOUR MILLION FIVE HUNDRED SIXTY-SEVEN THOUSAND EIGHT HUNDRED AND NINETY DOLLARS AND THIRTY-FIVE CENTS")]
    public void ConvertTest(string numberAsString, string expectedText)
    {
        var service = new NumberConversionService();
        var result = service.Convert(numberAsString);
        Assert.Equal(expectedText, result);
    }
}
