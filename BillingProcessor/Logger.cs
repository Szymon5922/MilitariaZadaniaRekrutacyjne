using Azure.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor
{
    public static class Logger
    {
        public static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/myLog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }
    }
}
