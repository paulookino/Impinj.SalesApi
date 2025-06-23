using CsvHelper;
using CsvHelper.Configuration;
using Impinj.Core.Models;
using System.Globalization;

namespace Impinj.Services
{
    public class SalesAnalysisService
    {
        private readonly string _csvPath = Path.Combine("Input", "SalesRecords.csv");

        protected virtual IEnumerable<SalesRecord> LoadRecords()
        {
            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            });

            return csv.GetRecords<SalesRecord>().ToList();
        }

        public SalesSummary Analyze()
        {
            var records = LoadRecords();

            var unitCosts = records.Select(r => r.UnitCost).OrderBy(c => c).ToList();
            var median = unitCosts.Count % 2 == 1
                ? unitCosts[unitCosts.Count / 2]
                : (unitCosts[unitCosts.Count / 2 - 1] + unitCosts[unitCosts.Count / 2]) / 2;

            var mostCommonRegion = records
                .GroupBy(r => r.Region)
                .OrderByDescending(g => g.Count())
                .First().Key;

            var firstDate = records.Min(r => r.OrderDate);
            var lastDate = records.Max(r => r.OrderDate);
            var daysBetween = (lastDate - firstDate).Days;

            var totalRevenue = records.Sum(r => r.TotalRevenue);

            return new SalesSummary
            {
                MedianUnitCost = median,
                MostCommonRegion = mostCommonRegion,
                FirstOrderDate = firstDate,
                LastOrderDate = lastDate,
                DaysBetween = daysBetween,
                TotalRevenue = totalRevenue
            };
        }
    }

}
