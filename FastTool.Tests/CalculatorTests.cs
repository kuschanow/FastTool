using FluentAssertions;

namespace FastTool.Tests;

public class CalculatorTests
{
    [Fact]
    public void WhenStringIsExpression_ThenExpressionCorrect()
    {
        // Arrange
        string sut = "-log(3.5)(5) + -cos4 + ((4+3)3)^3 + -4 * Sin(4 + (4-2cos3)) + |-3| + |3(|-4|)|";

        // Act
        var answer = new Expression(sut);

        // Assert
        answer.Should().Be(answer);
    }

    [Fact]
    public void WhenStringIsExpression_ThenAnswerCorrect()
    {
        // Arrange
        string sut1 = "2 + 3";
        string sut2 = "-log(3.5)(5) + ((4+3)3)^3 + 4 + |-3| + |3(|-4|)|";
        string sut3 = "3,353E-2 + 3";
        Calculator calc = new(Mode.Deg, 4);

        // Act
        var exp = new Expression(sut1);
        var answer1 = calc.Calculate(exp);
        exp = new Expression(sut2);
        var answer2 = calc.Calculate(exp);
        exp = new Expression(sut3);
        var answer3 = calc.Calculate(exp);

        // Assert
        answer1.Should().Be(5);
        answer2.Should().Be(9278.7153d);
        answer3.Should().Be(3.35E-2 + 3);
    }

    [Fact]
    public void WhenPi_ThenAnswerCorrect()
    {
        // Arrange
        string sut1 = "acot1";
        string sut2 = "(pi / 2) - atg1";
        Calculator calc = new(Mode.Rad);

        // Act
        var exp = new Expression(sut1);
        var answer1 = calc.Calculate(exp);
        exp = new Expression(sut2);
        var answer2 = calc.Calculate(exp);

        // Assert
        answer1.Should().Be(answer2);
    }
}
