using Autofac;
using log4net;
using PowerTradeService.Interfaces;
using Services;
using System;
using System.ServiceProcess;
using System.Threading;

namespace PowerTradeService
{
    static class Program
    {
        private static IContainer Container { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            RegisterContainer();

            var reportingService = new ReportingService(Container.Resolve<IIntradayReportService>(), Container.Resolve<IConfig>(), Container.Resolve<ILog>());
            
            
            if (!Environment.UserInteractive)    
            {
                
                var ServicesToRun = new ServiceBase[]
                {
                    reportingService
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                reportingService.RunAsConsoleApp();                
            }
        }

        private static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileManager>().As<IFileManager>();
            builder.RegisterType<PowerService>().As<IPowerService>().SingleInstance();
            builder.RegisterInstance(LogManager.GetLogger("Logger")).As<ILog>();
            builder.RegisterType<IntradayDataService>().As<IIntradayDataService>();
            builder.RegisterType<Config>().As<IConfig>();
            builder.RegisterType<IntradayCalculator>().As<IIntradayCalculator>();
            builder.RegisterType<IntradayReportService>().As<IIntradayReportService>();            
            builder.RegisterType<ReportingService>();

            Container = builder.Build();
        }
    }
}
