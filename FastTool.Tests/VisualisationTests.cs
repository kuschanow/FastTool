using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool.Tests
{
    public class VisualisationTests
    {
        [Fact]
        public void WhenExpressionContainsOnlyStandartMathAction_ThenVisualisationCorrect()
        {
            //Arrange
            string expStr = "1+2+3";
            Expression exp = new Expression(expStr);

            //Act
            Visualisation sut = new Visualisation(exp);

            //Assert
            sut.Vis.Count.Should().Be(2);

        }

        [Fact]
        public void WhenExpressionContainsStandartMathActionAndBraket_ThenVisualisationCorrect()
        {
            //Arrange
            string expStr = "1+2+(3-4-3)*(4+3)";
            Expression exp = new Expression(expStr);

            //Act
            Visualisation sut = new Visualisation(exp);

            //Assert
            sut.Vis.Count.Should().Be(5);

        }

        [Fact]
        public void WhenExpressionContainsFunction_ThenVisualisationCorrect()
        {
            //Arrange
            string expStr = "log(7+2+1)(50+24+26)";
            Expression exp = new Expression(expStr);

            //Act
            Visualisation sut = new Visualisation(exp);

            //Assert
            sut.Vis.Count.Should().Be(1);

        }

        [Fact]
        public void WhenExpressionContainsFunctionAndStandartMathActionAndBraket_ThenVisualisationCorrect()
        {
            //Arrange
            string expStr = "1+2+(3-4-3)*(4+3)+log(7+2+1)(50+24+26)+3";
            Expression exp = new Expression(expStr);

            //Act
            Visualisation sut = new Visualisation(exp);

            //Assert
            sut.Vis.Count.Should().Be(8);

        }

        [Fact]
        public void WhenExpressionContainsLikeTerms_ThenVisualisationCorrect()
        {
            //Arrange
            string expStr = "1+2-3+5+1-3-3";
            Expression exp = new Expression(expStr);

            //Act
            Visualisation sut = new Visualisation(exp);

            //Assert
            sut.Vis.Count.Should().Be(6);
        }

        [Fact]
        public void WhenExpressionContainsZero_ThenVisualisationCorrect()
        {
            //Arrange
            string expStr = "0-10-4+0-3";
            Expression exp = new Expression(expStr);

            //Act
            Visualisation sut = new Visualisation(exp);

            //Assert
            sut.Vis.Count.Should().Be(6);
        }

    }
}
