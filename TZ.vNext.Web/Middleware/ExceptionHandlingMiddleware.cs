//-----------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Web.Middleware
{
    /// <summary>
    /// 自定义异常处理中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            GuardUtils.NotNull(context, nameof(context));
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                log4net.LogManager.GetLogger(typeof(HttpContext)).Error(statusCode.ToString());
                log4net.LogManager.GetLogger(typeof(HttpContext)).Error(ex.ToString());
                await _next(context);
            }
        }
    }
}
