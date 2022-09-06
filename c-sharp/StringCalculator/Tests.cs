using FluentAssertions;

namespace CSharp.StringCalculator;

public class Tests
{
    [Fact]
    public void EmptyString_ReturnsZero()
    {
        var result = Calculator.Calculate("");
        result.Should().Be(0);
    }
    
    [Theory]
    [InlineData("1", 1)]
    [InlineData("456", 456)]
    [InlineData("-2", -2)]
    public void SingleNumber_ReturnsNumber(string input, int expected)
    {
        var result = Calculator.Calculate(input);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("1+1", 2)]
    [InlineData("57+100", 157)]
    [InlineData("1000+0", 1000)]
    public void Addition_ReturnsSum(string expression, int expected)
    {
        var result = Calculator.Calculate(expression);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("-2", -2)]
    [InlineData("4-2", 2)]
    [InlineData("40-2", 38)]
    [InlineData("-2+2", 0)]
    [InlineData("-4-10", -14)]
    public void Subtraction_ReturnsDifference(string expression, int expected)
    {
        var result = Calculator.Calculate(expression);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(" 1", 1)]
    [InlineData("1 ", 1)]
    [InlineData(" 1 ", 1)]
    [InlineData(" 1 + 1 ", 2)]
    [InlineData(" 45 - 60 ", -15)]
    public void Ignores_Whitespace(string expression, int expected)
    {
        var result = Calculator.Calculate(expression);
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("1+1+1", 3)]
    [InlineData("1+1+1+1", 4)]
    [InlineData("2-3+5", 4)]
    public void MultipleOperations_ReturnsCorrectResult(string expression, int expected)
    {
        var result = Calculator.Calculate(expression);
        result.Should().Be(expected);
    }
}