using Services;
using System.Collections.Generic;

namespace PowerTradeService.Interfaces
{
    public interface IIntradayCalculator
    {
        List<Position> CalculateIntraday(List<PowerTrade> powerTrades);        
    }
}
