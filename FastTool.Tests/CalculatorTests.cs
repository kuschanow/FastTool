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

    [Fact]
    public void WhenSqrtOrCbrt_ThenCorrectAnswer()
    {
        // Arrange
        string sut1 = "4 - sqrt(3) + 3";
        string sut2 = "4 - sqrt(3 + 4) + 3";
        string sut3 = "4 - cbrt(3 + 4) + 3";

        // Act
        var answer1 = Calculator.Calculate(sut1, Calculator.Mode.Deg, 3);
        var answer2 = Calculator.Calculate(sut2, Calculator.Mode.Deg, 3);
        var answer3 = Calculator.Calculate(sut3, Calculator.Mode.Deg, 3);

        // Assert
        answer1.Should().Be(5.268);
        answer2.Should().Be(4.354);
        answer3.Should().Be(5.087);
    }

    [Fact]
    public void WhenTrigonometryFunc_ThenCorrectAnswer()
    {
        // Arrange
        string sut1 = "sin(90)";
        string sut2 = "cos(90)";
        string sut3 = "tg(35)";
        string sut4 = "ctg(35)";
        string sut5 = "arcsin(1)";
        string sut6 = "acos(1)";
        string sut7 = "atg(1)";
        string sut8 = "arccot(1)";
        string sut9 = "arccot(1) + (4(5-3))";
        string sut10 = "(4 * cos(90) - 8) + 4";

        // Act
        var answer1 = Calculator.Calculate(sut1, Calculator.Mode.Deg, 4);
        var answer2 = Calculator.Calculate(sut2, Calculator.Mode.Deg, 4);
        var answer3 = Calculator.Calculate(sut3, Calculator.Mode.Rad, 4);
        var answer4 = Calculator.Calculate(sut4, Calculator.Mode.Rad, 4);
        var answer5 = Calculator.Calculate(sut5, Calculator.Mode.Rad, 4);
        var answer6 = Calculator.Calculate(sut6, Calculator.Mode.Deg, 4);
        var answer7 = Calculator.Calculate(sut7, Calculator.Mode.Deg, 4);
        var answer8 = Calculator.Calculate(sut8, Calculator.Mode.Deg, 4);
        var answer9 = Calculator.Calculate(sut9, Calculator.Mode.Deg, 4);
        var answer10 = Calculator.Calculate(sut10, Calculator.Mode.Deg, 4);

        // Assert
        answer1.Should().Be(1);
        answer2.Should().Be(0);
        answer3.Should().Be(0.4738);
        answer4.Should().Be(2.1105);
        answer5.Should().Be(1.5708);
        answer6.Should().Be(0);
        answer7.Should().Be(45);
        answer8.Should().Be(45);
        answer9.Should().Be(53);
        answer10.Should().Be(-4);
    }

    [Fact]
    public void WhenMinusBehindFunc_ThenCorrectAnswer()
    {
        // Arrange
        string sut = "-(50 + -cos(60)) + 4(-|3| + 4)";

        // Act
        var answer = Calculator.Calculate(sut, Calculator.Mode.Deg, 4);

        // Assert
        answer.Should().Be(-45.5);
    }

    [Fact]
    public void WhenLogFunc_ThenCorrectAnswer()
    {
        // Arrange
        string sut1 = "log2(4)";
        string sut2 = "lg(1000)";
        string sut3 = "ln(1)";
        string sut4 = "log2(4) + lg(1000) - ln(1)^2";

        // Act
        var answer1 = Calculator.Calculate(sut1, Calculator.Mode.Deg, 4);
        var answer2 = Calculator.Calculate(sut2, Calculator.Mode.Deg, 4);
        var answer3 = Calculator.Calculate(sut3, Calculator.Mode.Deg, 4);
        var answer4 = Calculator.Calculate(sut4, Calculator.Mode.Deg, 4);

        // Assert
        answer1.Should().Be(2);
        answer2.Should().Be(3);
        answer3.Should().Be(0);
        answer4.Should().Be(5);
    }
}