using NumberConversionApi.Services;

namespace NumberToTextConversion.Test;

public class NumberConversionServiceTest
{
    [Theory]
    [InlineData("0", "zero dollar")]
    [InlineData("1", "one dollar")]
    [InlineData("3", "three dollars")]
    [InlineData("10", "ten dollars")]
    [InlineData("21", "twenty-one dollars")]
    [InlineData("105", "one hundred and five dollars")]
    [InlineData("569", "five hundred and sixty-nine dollars")]
    [InlineData("1000", "one thousand dollars")]
    [InlineData("1234", "one thousand two hundred and thirty-four dollars")]
    public void ConvertTest(string numberAsString, string expectedText)
    {
        var service = new NumberConversionService();
        var result = service.Convert(numberAsString);
        Assert.Equal(expectedText, result);
    }
}
