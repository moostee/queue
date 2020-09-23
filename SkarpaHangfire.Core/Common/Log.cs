using NLog;
using System;

namespace skarpa.core.Common
{
    public static class Log
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void Error(Exception ex)
        {
            logger.Error(ex, ex.Message);
        }

        public static void Info(string txt)
        {
            logger.Info(txt);
        }
    }
}
