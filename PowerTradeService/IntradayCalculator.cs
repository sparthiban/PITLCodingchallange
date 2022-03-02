using log4net;
using PowerTradeService.Interfaces;
using Services;
using PowerTradeService.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PowerTradeService
{
    public class IntradayCalculator : IIntradayCalculator
    {

        private readonly ILog _log;
        public IntradayCalculator(ILog log)
        {            
            _log = log;
        }


        public List<Position> CalculateIntraday(List<PowerTrade> powerTrades)
        {   

            _log.Info($"calculate power trades");

            var positions = powerTrades.SelectMany(x => x.Periods).GroupBy(x => x.Period).Select(x => new Position { LocalTime = x.Key.ToLocalTime(), Volume = x.Sum(y => y.Volume) });

            return positions.ToList();

        }

        

    }
}
