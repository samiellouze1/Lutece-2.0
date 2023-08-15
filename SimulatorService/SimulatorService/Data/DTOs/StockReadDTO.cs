namespace SimulatorService.Data.DTOs
{
    public class StockReadDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double AveragePrice { get; set; }
        public int Quantity { get; set; }
    }
}
