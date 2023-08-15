﻿using SimulatorService.Data.DTOs;

namespace SimulatorService.SyncDataServices.Http
{
    public interface IStockDataClient
    {
        Task PostOriginalOrderToStock(OriginalOrderCreateDto originalOrderCreateDto);
        Task<int> GetInformationFromStockUnit(string userId, string stockId);
        Task<double> GetInformationFromStock(string stockId);
    }
}
