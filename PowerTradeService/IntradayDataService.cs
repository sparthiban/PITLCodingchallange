using log4net;
using PowerTradeService.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTradeService
{
    public class IntradayDataService : IIntradayDataService
    {
        private readonly IPowerService _powerService;
        private readonly ILog _log;

        public IntradayDataService(IPowerService powerService, ILog log)
        {
            _powerService = powerService;
            _log = log;
        }

        public List<PowerTrade> GetTrades(DateTime date)
        {
            try
            {
                _log.Info($"Begin GetTradesAsync {date}");
                var powerTrades = _powerService.GetTrades(DateTime.Now);
                _log.Info($"completed GetTradesAsync {date}");
                return powerTrades.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
