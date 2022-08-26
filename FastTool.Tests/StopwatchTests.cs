using FluentAssertions;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace FastTool.Tests
{
    public class StopwatchTests
    {
        [Fact]
        public void WhenStopwatchStart_ThenTimeGrow()
        {
            // Arrange
            var stopwatch = new Stopwatch(1000);

            // Act
            stopwatch.Start();
            Thread.Sleep(1500);

            // Assert
            stopwatch.Time.TotalSeconds.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenStopwatchStart_ThenTimeCorrect()
        {
            // Arrange
            var stopwatch = new Stopwatch(100);

            // Act
            stopwatch.Start();
            Thread.Sleep(3550);

            // Assert
            Math.Round(stopwatch.Time.TotalSeconds, 1).Should().Be(3.5);
        }
    }
}
