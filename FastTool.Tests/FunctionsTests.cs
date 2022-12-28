using FastTool.CalculationTool.Functions;
using FastTool.CalculationTool.Interfaces;

namespace FastTool.Tests
{
    public class FunctionsTests
    {
        [Fact]
        public void FactorialTest()
        {
            // Arrange
            var sut = new Factor(new ICalculateble[] { new GetComplex(new ICalculateble[] {new Number(-4), new Number(-0.5)}) });

            //Act
            var answer = sut.Calculate(Mode.Deg);

            //Assert
            answer.Magnitude.Should().Be(0.23571275348885148);
        }
    }
}
