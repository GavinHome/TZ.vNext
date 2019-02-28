//-----------------------------------------------------------------------------------
// <copyright file="AsyncActionFilter.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Web.Filters
{
    public class AsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            GuardUtils.NotNull(context, nameof(context));
            if (context.HttpContext.User != null && context.HttpContext.Request.Method.ToLower() == HttpMethod.Post.ToString().ToLower())
            {
                foreach (var param in context.ActionArguments.Values)
                {
                    PropertyInfo[] fields = param.GetType().GetProperties().Where(x => x.PropertyType == typeof(ClaimsPrincipal)).ToArray();
                    foreach (PropertyInfo field in fields)
                    {
                        field.SetValue(param, context.HttpContext.User, null);
                    }
                }
            }

            await next();
        }
    }
}