namespace FastTool.Tests;

public class ExpressionParserTests
{
    [Fact]
    public void ExpressionParserParseTest()
    {
        // Arrange
        var sut = new ExpressionParser();
        var exp = "\\left|3 - (5log(6, 20)) / \\left|sin6 + tgpi\\right|\\right|";

        // Act
        var answer = sut.Parse(exp).Calculate(Mode.Deg);

        // Assert
        answer.Should().Be(49.44029933728401);
    }
}
