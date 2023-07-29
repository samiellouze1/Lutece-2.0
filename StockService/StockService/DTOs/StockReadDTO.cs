using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.DTOs
{
    public class StockReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AveragePrice { get; set; }
        public int Quantity { get; set; }
        public virtual List<StockUnit> StockUnits { get; set; } = new List<StockUnit>();
        public virtual List<OriginalOrder> OriginalOrders { get; set; } = new List<OriginalOrder>();
    }
}
