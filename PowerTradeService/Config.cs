using PowerTradeService.Interfaces;
using System.Configuration;

namespace PowerTradeService
{
    public class Config : IConfig
    {
        private readonly int _intradayIntervalInMinutes;
        private readonly string _IntradayFileLocation;


        public Config()
        {
            _intradayIntervalInMinutes = int.Parse(ConfigurationManager.AppSettings["IntradayIntervalInMinutes"]);
            _IntradayFileLocation = ConfigurationManager.AppSettings["IntradayFileLocation"];

        }
        public int IntradayIntervalInMinutes => _intradayIntervalInMinutes;

        public string IntradayFileLocation => _IntradayFileLocation;
    }
}
