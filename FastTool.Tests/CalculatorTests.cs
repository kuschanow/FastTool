using FastTool;
using FluentAssertions;

namespace FastTool.Tests;

public class CalculatorTests
{
    [Fact]
    public void WhenSimpleExpression_ThenCorrectAnswer()
    {
        // Arrange
        string sut1 = "2 + 2";
        string sut2 = "2 - 2";
        string sut3 = "3 * 3";
        string sut4 = "2 / 2";
        string sut5 = "2 ^ 3";
        string sut6 = "3 % 2";
        string sut7 = "-2 * 2";

        // Act
        var answer1 = Calculator.Calculate(sut1);
        var answer2 = Calculator.Calculate(sut2);
        var answer3 = Calculator.Calculate(sut3);
        var answer4 = Calculator.Calculate(sut4);
        var answer5 = Calculator.Calculate(sut5);
        var answer6 = Calculator.Calculate(sut6);
        var answer7 = Calculator.Calculate(sut7);

        // Assert
        answer1.Should().Be(4);
        answer2.Should().Be(0);
        answer3.Should().Be(9);
        answer4.Should().Be(1);
        answer5.Should().Be(8);
        answer6.Should().Be(1);
        answer7.Should().Be(-4);
    }
}