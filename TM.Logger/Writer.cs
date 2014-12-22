using System;

namespace TM.Logger
{
    public class Writer
    {
        private readonly log4net.ILog _writer;

        public Writer()
        {
            log4net.Config.XmlConfigurator.Configure();
            _writer = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // Uncomment this if you are having config problems
            //            if (!log4net.LogManager.GetRepository().Configured)
            //            {
            //                // log4net not configured
            //                System.Diagnostics.Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            //                
            //                foreach (var message in log4net.LogManager.GetRepository().ConfigurationMessages.Cast<log4net.Util.LogLog>())
            //                {
            //                    System.Diagnostics.Debug.WriteLine(message);
            //                }
            //
            //                System.Diagnostics.Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            //            }
        }

        public void Debug(string message, Exception exception = null)
        {
            _writer.Debug(message, exception);
        }

        public void Error(string message, Exception exception = null)
        {
            _writer.Error(message, exception);
        }

        public void Fatal(string message, Exception exception = null)
        {
            _writer.Fatal(message, exception);
        }

        public void Info(string message, Exception exception = null)
        {
            _writer.Info(message, exception);
        }

        public void Warning(string message, Exception exception = null)
        {
            _writer.Warn(message, exception);
        }
    }
}
