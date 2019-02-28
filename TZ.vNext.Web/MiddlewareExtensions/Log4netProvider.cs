using System.IO;
using Microsoft.Extensions.Logging;

namespace TZ.vNext.Web.CustomTokenProvider
{
    public class Log4netProvider : ILoggerProvider
    {
        private readonly FileInfo _fileInfo;

        public Log4netProvider(string log4netConfigFile)
        {
            _fileInfo = new FileInfo(log4netConfigFile);
        }

        public ILogger CreateLogger(string name)
        {
            return new Log4netLogger(name, _fileInfo);
        }

        public void Dispose()
        {
        }
    }
}