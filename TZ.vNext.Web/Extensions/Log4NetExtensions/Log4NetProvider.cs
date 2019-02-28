//-----------------------------------------------------------------------------------
// <copyright file="Log4netProvider.cs" company="��ְ������ѯ�ɷ����޹�˾��Ȩ����">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.IO;
using Microsoft.Extensions.Logging;

namespace TZ.vNext.Web.Log4NetExtensions
{
    public class Log4NetProvider : ILoggerProvider
    {
        private readonly FileInfo _fileInfo;

        public Log4NetProvider(string log4netConfigFile, bool enableScopes = false)
        {
            _fileInfo = new FileInfo(log4netConfigFile);
            EnableScopes = enableScopes;
        }

        public Log4NetProvider(bool enableScopes = false)
        {
            _fileInfo = new FileInfo("log4net.config");
            EnableScopes = enableScopes;
        }

        public bool EnableScopes { get; set; }
        
        public ILogger CreateLogger(string categoryName)
        {
            return new Log4NetLogger(categoryName, _fileInfo, EnableScopes);
        }

        public void Dispose()
        {            
        }
    }
}