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
        string sut8 = "-2";

        // Act
        var answer1 = Calculator.Calculate(sut1);
        var answer2 = Calculator.Calculate(sut2);
        var answer3 = Calculator.Calculate(sut3);
        var answer4 = Calculator.Calculate(sut4);
        var answer5 = Calculator.Calculate(sut5);
        var answer6 = Calculator.Calculate(sut6);
        var answer7 = Calculator.Calculate(sut7);
        var answer8 = Calculator.Calculate(sut8);

        // Assert
        answer1.Should().Be(4);
        answer2.Should().Be(0);
        answer3.Should().Be(9);
        answer4.Should().Be(1);
        answer5.Should().Be(8);
        answer6.Should().Be(1);
        answer7.Should().Be(-4);
        answer8.Should().Be(-2);
    }

    [Fact]
    public void WhenDifficultExpression_ThenCorrectAnswer()
    {
        // Arrange
        string sut1 = "2 + 2^3";
        string sut2 = "(2 - 2)";
        string sut3 = "(3 - 1) - 3 * 3";
        string sut4 = "((3 - 1) - 3) ^ 3";
        string sut5 = "16/2^3";
        string sut6 = "3*5+3/(5+2+1)*5";
        string sut7 = "3*5+3/(5*(2+1))*5";

        // Act
        var answer1 = Calculator.Calculate(sut1);
        var answer2 = Calculator.Calculate(sut2);
        var answer3 = Calculator.Calculate(sut3);
        var answer4 = Calculator.Calculate(sut4);
        var answer5 = Calculator.Calculate(sut5);
        var answer6 = Calculator.Calculate(sut6);
        var answer7 = Calculator.Calculate(sut7);

        // Assert
        answer1.Should().Be(10);
        answer2.Should().Be(0);
        answer3.Should().Be(-7);
        answer4.Should().Be(-1);
        answer5.Should().Be(2);
        answer6.Should().Be(16.875);
        answer7.Should().Be(16);
    }

    [Fact]
    public void WhenExpression_ThenCorrectAnswer()
    {
        // Arrange
        string sut1 = "2 + 2^3";
        string sut2 = "(2 - 2)";
        string sut3 = "(3 - 1) - 3 * 3";
        string sut4 = "((3 - 1) - 3) ^ 3";
        string sut5 = "16/2^3";
        string sut6 = "3*5+3/(5+2+1)*5";
        string sut7 = "3*5+3/(5*(2+1))*5";
        string sut8 = "2 + 2";
        string sut9 = "2 - 2";
        string sut10 = "3 * 3";
        string sut11 = "2 / 2";
        string sut12 = "2 ^ 3";
        string sut13 = "3 % 2";
        string sut14 = "-2 * 2";

        // Act
        var answer1 = Calculator.Calculate(sut1);
        var answer2 = Calculator.Calculate(sut2);
        var answer3 = Calculator.Calculate(sut3);
        var answer4 = Calculator.Calculate(sut4);
        var answer5 = Calculator.Calculate(sut5);
        var answer6 = Calculator.Calculate(sut6);
        var answer7 = Calculator.Calculate(sut7);
        var answer8 = Calculator.Calculate(sut8);
        var answer9 = Calculator.Calculate(sut9);
        var answer10 = Calculator.Calculate(sut10);
        var answer11 = Calculator.Calculate(sut11);
        var answer12 = Calculator.Calculate(sut12);
        var answer13 = Calculator.Calculate(sut13);
        var answer14 = Calculator.Calculate(sut14);

        // Assert
        answer1.Should().Be(10);
        answer2.Should().Be(0);
        answer3.Should().Be(-7);
        answer4.Should().Be(-1);
        answer5.Should().Be(2);
        answer6.Should().Be(16.875);
        answer7.Should().Be(16);
        answer8.Should().Be(4);
        answer9.Should().Be(0);
        answer10.Should().Be(9);
        answer11.Should().Be(1);
        answer12.Should().Be(8);
        answer13.Should().Be(1);
        answer14.Should().Be(-4);
    }

    [Fact]
    public void WhenMultiplicationIsUnsigned_ThenCorrectAnswer()
    {
        // Arrange
        string sut = "3 - 3(4-1+4(3-4)) + 5 + 3(3 - 2) + 4";

        // Act
        var answer = Calculator.Calculate(sut);

        // Assert
        answer.Should().Be(18);
    }
}