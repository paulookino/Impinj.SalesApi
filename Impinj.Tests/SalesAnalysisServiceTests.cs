using FluentAssertions;
using Impinj.Core.Models;
using Impinj.Services;

namespace Impinj.Tests;

public class SalesAnalysisServiceTests
{
    private List<SalesRecord> GetSampleData() =>
        new List<SalesRecord>
        {
            new() { OrderDate = new DateTime(2023, 1, 1), Region = "West", UnitCost = 25.5m, UnitsSold = 10, TotalRevenue = 255 },
            new() { OrderDate = new DateTime(2023, 1, 5), Region = "East", UnitCost = 30m, UnitsSold = 5, TotalRevenue = 150 },
            new() { OrderDate = new DateTime(2023, 2, 1), Region = "West", UnitCost = 25.5m, UnitsSold = 8, TotalRevenue = 204 },
        };

    [Fact]
    public void Analyze_Should_Calculate_Correct_Median_And_Mode()
    {
        // Arrange
        var fakeService = new TestableSalesAnalysisService(GetSampleData());

        // Act
        var result = fakeService.Analyze();

        // Assert
        result.MedianUnitCost.Should().Be(25.5m);
        result.MostCommonRegion.Should().Be("West");
        result.FirstOrderDate.Should().Be(new DateTime(2023, 1, 1));
        result.LastOrderDate.Should().Be(new DateTime(2023, 2, 1));
        result.DaysBetween.Should().Be(31);
        result.TotalRevenue.Should().Be(609);
    }

    private class TestableSalesAnalysisService : SalesAnalysisService
    {
        private readonly IEnumerable<SalesRecord> _testData;

        public TestableSalesAnalysisService(IEnumerable<SalesRecord> testData)
        {
            _testData = testData;
        }

        protected override IEnumerable<SalesRecord> LoadRecords() => _testData;
    }
}
