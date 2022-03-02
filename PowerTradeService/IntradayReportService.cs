using log4net;
using PowerTradeService.Interfaces;
using System;

namespace PowerTradeService
{
    public class IntradayReportService : IIntradayReportService
    {

        private readonly IIntradayDataService _intradayDataService;
        private readonly ILog _log;
        private readonly IIntradayCalculator _intradayCalculator;
        private readonly IFileManager _fileManager;
        private readonly IConfig _config;


        public IntradayReportService(IIntradayDataService intradayDataService, IIntradayCalculator intradayCalculator, IFileManager fileManager, ILog log, IConfig config)
        {
            _intradayDataService = intradayDataService;
            _intradayCalculator = intradayCalculator;
            _fileManager = fileManager;
            _log = log;
            _config = config;
        }        

        public void CreateIntradayReport(DateTime reportDate)
        {

            _log.Info($"Starting Report Generation {reportDate}");

            var powerTrades= _intradayDataService.GetTrades(reportDate);

            var positions =  _intradayCalculator.CalculateIntraday(powerTrades);

            _fileManager.WriteToCSVAsync(positions, _config.IntradayFileLocation, GetFileName(reportDate));

            _log.Info($"Report Generation completed {reportDate}");

        }

        public string GetFileName(DateTime reportDate)
        {
            return $"PowerPosition_{reportDate:yyyyMMdd_HHmm}.csv";
        }

    }
}
