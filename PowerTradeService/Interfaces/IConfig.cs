using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeService.Interfaces
{
    public interface IConfig
    {
        int IntradayIntervalInMinutes { get; }
        string IntradayFileLocation { get; }      
    }
}
