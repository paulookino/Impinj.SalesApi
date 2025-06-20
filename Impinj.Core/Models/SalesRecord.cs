namespace Impinj.Core.Models
{
    public class SalesRecord
    {
        public DateTime OrderDate { get; set; }
        public string Region { get; set; }
        public decimal UnitCost { get; set; }
        public int UnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
