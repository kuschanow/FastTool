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
}
