//-----------------------------------------------------------------------------------
// <copyright file="Log4netLogger.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;

namespace TZ.vNext.Web.Log4NetExtensions
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string name, FileInfo fileInfo, bool enableScopes)
        {
            var repository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repository, fileInfo);

            _log = LogManager.GetLogger(repository.Name, name);
            Name = name;
            EnableScopes = enableScopes;
        }

        public Log4NetLogger(ILoggerRepository repository, string name, bool enableScopes)
        {
            _log = LogManager.GetLogger(repository.Name, name);
            Name = name;
            EnableScopes = enableScopes;
        }

        public string Name { get; set; }

        public bool EnableScopes { get; set; }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            return Log4NetScope.Push(Name, state);
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
                case LogLevel.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);
            if (EnableScopes)
            {
                AppendScopeInformation(new StringBuilder(message));
            }

            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical:
                        _log.Fatal(message);
                        break;
                    case LogLevel.Debug:
                    case LogLevel.Trace:
                        _log.Debug(message);
                        break;
                    case LogLevel.Error:
                        _log.Error(message);
                        break;
                    case LogLevel.Information:
                        _log.Info(message);
                        break;
                    case LogLevel.Warning:
                        _log.Warn(message);
                        break;
                    case LogLevel.None:
                        break;
                    default:
                        _log.Warn($"Unknown log level {logLevel}.\r\n{message}");
                        break;
                }
            }
        }

        private void AppendScopeInformation(StringBuilder messageBuilder)
        {
            var current = Log4NetScope.Current;

            if (current != null)
            {
                messageBuilder.Append($" => {current}");
            }
        }
    }
}