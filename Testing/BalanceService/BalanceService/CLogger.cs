using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Reflection;

namespace Utility
{
    namespace LoggingService
    {
        public static class CLogger
        {
            #region Members
            
            private static readonly ILog logger = LogManager.GetLogger(typeof(CLogger));
            
            #endregion

            #region Constructors

            static CLogger()
            {
                XmlConfigurator.Configure();
            }

            #endregion

            #region Methods
            
            public static void WriteLog(ELogLevel logLevel, String log)
            {
                if (logLevel.Equals(ELogLevel.DEBUG))
                {
                    logger.Debug(log);
                }

                else if (logLevel.Equals(ELogLevel.ERROR))
                {
                    logger.Error(log);
                }

                else if (logLevel.Equals(ELogLevel.FATAL))
                {
                    logger.Fatal(log);
                }

                else if (logLevel.Equals(ELogLevel.INFO))
                {
                    logger.Info(log);
                }

                else if (logLevel.Equals(ELogLevel.WARN))
                {
                    logger.Warn(log);
                }

            }

            #endregion

        }

    }

}