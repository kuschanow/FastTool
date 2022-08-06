using FluentAssertions;
using System.Threading;

namespace FastTool.Tests
{
    public class TimerTests
    {
        [Fact]
        public void WhenTimerCreate_ThenTimeEqualsTimeOnCreate()
        {
            // Arrange
            TimeSpan time = TimeSpan.FromMinutes(5);
            var timer = new Timer(time);

            // Act
            var result = timer.Time == time;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void WhenTimerStart_ThenTimeChanged()
        {
            // Arrange
            TimeSpan time = TimeSpan.FromSeconds(5);
            var timer = new Timer(time);

            // Act
            timer.Start();
            Thread.Sleep(2100);
            timer.Pause();

            // Assert
            Math.Round(timer.TimeLeft.TotalSeconds).Should().Be(3);
        }

    }
}
