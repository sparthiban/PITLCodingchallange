using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeService.Extensions
{
    public static class IntExtension
    {
        public static string ToLocalTime(this int value)
        {
            var localTime = new TimeSpan(22 + value, 0, 0).ToString(@"hh\:mm");

            return localTime;
        }
    }
}
