using log4net;
using PowerTradeService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeService
{
    partial class ReportingService : ServiceBase
    {
        private readonly IIntradayReportService _intradayReportService;        
        private readonly IConfig _config;       
        private readonly ILog _log;


        public ReportingService(IIntradayReportService intradayReportService,IConfig config, ILog log)
        {
            _config = config;
            _log = log;
            _intradayReportService = intradayReportService;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = _config.IntradayIntervalInMinutes;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
                timer.Start();
            }
            catch (Exception ex)
            {
                _log.Error("Failed to start Intraday Service");
                _log.Error(ex);
                throw;
            }
        }

        private void  StartReportGenerator()
        {

            try
            {
                _log.Info("Intraday Report Generation started");

                _intradayReportService.CreateIntradayReport(DateTime.Now);
                _log.Info("Intraday Report Generation completed");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {   
            StartReportGenerator();
        }

        public void  RunAsConsoleApp()
        {
            StartReportGenerator();
        }

        protected override void OnStop()
        {
            _log.Info("Intraday Service on stop");
        }
    }
}
