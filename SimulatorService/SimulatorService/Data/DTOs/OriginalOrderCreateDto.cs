﻿using SimulatorService.Data.Enums;

namespace SimulatorService.Data.DTOs
{
    public class OriginalOrderCreateDto
    {
        public OrderTypeEnum OrderType { get; set; }
        public double Price { get; set; }
        public int OriginalQuantity { get; set; }
        public string StockId { get; set; }
    }
}
