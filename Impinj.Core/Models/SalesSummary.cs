﻿namespace Impinj.Core.Models;

public class SalesSummary
{
    public decimal MedianUnitCost { get; set; }
    public string MostCommonRegion { get; set; }
    public DateTime FirstOrderDate { get; set; }
    public DateTime LastOrderDate { get; set; }
    public int DaysBetween { get; set; }
    public decimal TotalRevenue { get; set; }
}
