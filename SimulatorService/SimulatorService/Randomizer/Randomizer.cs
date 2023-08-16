using SimulatorService.Data.DTOs;
using SimulatorService.Data.Enums;
using SimulatorService.SyncDataServices.Http;

namespace SimulatorService.Randomizer
{
    public class Randomizer : IRandomizer
    {
        private readonly IHttpStockDataClient _stockDataClient;
        public Randomizer(IHttpStockDataClient stockDataClient)
        {
            _stockDataClient = stockDataClient;
        }
        public async Task RandomizeOriginalOrderSell()
        {
            var ordertype = OriginalOrderTypeEnum.Sell;
            #region randomizing stock and user
            Random stockrandom = new Random();
            var stockId = stockrandom.Next(0, 4).ToString();
            Random userrandom = new Random();
            var userId = userrandom.Next(0, 4).ToString();
            #endregion
            var stockunitcount = await _stockDataClient.GetInformationFromStockUnit(stockId, userId);
            #region randomizing stock price
            var probabilities = new List<List<double>>()
            {
                new List<double>()
                {
                    -1,3
                },
                new List<double>()
                {
                    1,5
                },
                new List<double>()
                {
                    -3,2
                },
                new List<double>()
                {
                    -4,1
                }
            };
            var stockaverageprice = await _stockDataClient.GetInformationFromStock(stockId);
            var pricerandom = new Random();
            double minValue = probabilities[int.Parse(stockId) - 1][0];
            double maxValue = probabilities[int.Parse(stockId) - 1][1];
            double price = stockaverageprice * (1 - minValue / 100) + pricerandom.NextDouble() * (maxValue - minValue) / 100;
            #endregion
            #region randomizing quantity
            Random originalquantityrandom = new Random();
            var originalquantity = originalquantityrandom.Next(0, stockunitcount);
            #endregion
            var originalOrderCreateDto = new OriginalOrderCreateDto()
            {
                OrderType = ordertype,
                Price = price,
                OriginalQuantity = originalquantity,
                StockId = stockId
            };
            await _stockDataClient.PostOriginalOrderToStock(originalOrderCreateDto);
        }
    }
}
