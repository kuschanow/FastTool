using Microsoft.EntityFrameworkCore;

namespace FastTool.Tests;

public class DataBaseTests
{
    [Fact]
    public void WhenDataBaseMigrate_ThenDataBaseCreateIfNotExist()
    {
        // Arrange
        using var sut = new DBContext();

        // Act
        sut.Database.Migrate();

        // Assert
        File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\FastTool\\fasttool.db").Should().Be(true);
    }
}
