using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Logging;

namespace TZ.vNext.Web.CustomTokenProvider
{
    public class Log4netLogger : ILogger
    {
        private readonly ILog _log;

        public Log4netLogger(string name, FileInfo fileInfo)
        {
            var repository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repository, fileInfo);
            _log = LogManager.GetLogger(repository.Name, name);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical: return _log.IsFatalEnabled;
                case LogLevel.Debug:
                case LogLevel.Trace: return _log.IsDebugEnabled;
                case LogLevel.Error: return _log.IsErrorEnabled;
                case LogLevel.Information: return _log.IsInfoEnabled;
                case LogLevel.Warning: return _log.IsWarnEnabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            string message = null;
            if (null != formatter)
            {
                message = formatter(state, exception);
            }
            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical: _log.Fatal(message); break;
                    case LogLevel.Debug:
                    case LogLevel.Trace: _log.Debug(message); break;
                    case LogLevel.Error: _log.Error(message); break;
                    case LogLevel.Information: _log.Info(message); break;
                    case LogLevel.Warning: _log.Warn(message); break;
                    default:
                        _log.Warn($"Unknown log level {logLevel}.\r\n{message}");
                        break;
                }
            }
        }
    }
}