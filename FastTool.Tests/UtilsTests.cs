using System.Globalization;

namespace FastTool.Tests
{
    public class UtilsTests
    {
        [Fact]
        public void FormatTest()
        {
            //Arrange
            var str = 234.74.ToString("#", new CultureInfo("en-US"));

            //Act


            //Assert


        }
    }
}
