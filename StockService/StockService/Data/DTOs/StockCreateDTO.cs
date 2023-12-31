﻿using StockService.Data.Enums;
using StockService.Models;
using System.ComponentModel.DataAnnotations;

namespace StockService.Data.DTOs
{
    public class StockCreateDTO
    {
        public string Id{ get; set; }
        public string Name { get; set; }
        public double AveragePrice { get; set; }
        public int Quantity { get; set; }
    }
}
