using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerTradeService;
using PowerTradeService.Extensions;
using PowerTradeService.Interfaces;
using System;
using System.Collections.Generic;
using Services;
using FluentAssertions;
namespace PowerTradeServiceTests
{
    [TestClass]
    public class PowerReportGenerationTests
    {

        IIntradayDataService mockIntradayDataService;
        IIntradayCalculator mockIntradayCalculator;
        IFileManager mockFileManager;
        ILog mockLog;
        IConfig mockConfig;

        [TestInitialize()]
        public void Initialise()
        {
             mockIntradayDataService = new Mock<IIntradayDataService>().Object;
             mockIntradayCalculator = new Mock<IIntradayCalculator>().Object;
             mockFileManager = new Mock<IFileManager>().Object;
             mockLog = new Mock<ILog>().Object;
             mockConfig = new Mock<IConfig>().Object;           
        }

        [TestMethod]
        public void ValidateLocalTimeConversation()
        {
            int period1 = 1;
            int period2 = 2;
            int period3 = 3;
            int period4 = 4;


            Assert.AreEqual("23:00", period1.ToLocalTime());
            Assert.AreEqual("00:00", period2.ToLocalTime());
            Assert.AreEqual("01:00", period3.ToLocalTime());
            Assert.AreEqual("02:00", period4.ToLocalTime());

        }

        [TestMethod]
        public void ValidateFilename()
        {
            var intradayReportService = new IntradayReportService(mockIntradayDataService, mockIntradayCalculator, mockFileManager, mockLog, mockConfig);

            Assert.AreEqual("PowerPosition_20220301_2340.csv", intradayReportService.GetFileName(new DateTime(2022,03,01,23,40,20)));
        
        }

        [TestMethod]
        public void CalculateIntradayPosition()
        {
            var intradayCalculator = new IntradayCalculator(mockLog);
            var powerTrades = new List<PowerTrade>();

            PowerTrade powerTrade1 = PowerTrade.Create(DateTime.Parse("01 MAR 2022"), 3);
            
            powerTrade1.Periods[0].Period = 1;
            powerTrade1.Periods[0].Volume = 10;
            
            powerTrade1.Periods[1].Period = 2;
            powerTrade1.Periods[1].Volume = 25;

            powerTrade1.Periods[2].Period = 3;
            powerTrade1.Periods[2].Volume = 35;

            PowerTrade powerTrade2 = PowerTrade.Create(DateTime.Parse("01 MAR 2022"), 4);
            powerTrade2.Periods[0].Period = 1;
            powerTrade2.Periods[0].Volume = 100;

            powerTrade2.Periods[1].Period = 2;
            powerTrade2.Periods[1].Volume = 200;

            powerTrade2.Periods[2].Period = 3;
            powerTrade2.Periods[2].Volume = 300;

            powerTrade2.Periods[3].Period = 4;
            powerTrade2.Periods[3].Volume = 400;

            powerTrades.Add(powerTrade1);
            powerTrades.Add(powerTrade2);

            var expectedPositions = new List<Position>();
            expectedPositions.Add(new Position() { LocalTime = "23:00", Volume = 110 });
            expectedPositions.Add(new Position() { LocalTime = "00:00", Volume = 225 });
            expectedPositions.Add(new Position() { LocalTime = "01:00", Volume = 335 });
            expectedPositions.Add(new Position() { LocalTime = "02:00", Volume = 400 });
            
            var actualPositions =intradayCalculator.CalculateIntraday(powerTrades);

            actualPositions.Should().BeEquivalentTo(expectedPositions);

        }
    }
}
