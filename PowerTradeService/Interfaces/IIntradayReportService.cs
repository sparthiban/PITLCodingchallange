using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeService.Interfaces
{
    public interface IIntradayReportService
    {
        void CreateIntradayReport(DateTime reportDate);
        string GetFileName(DateTime reportDate);
    }
}
