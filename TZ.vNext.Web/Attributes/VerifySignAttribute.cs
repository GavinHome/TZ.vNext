//-----------------------------------------------------------------------------------
// <copyright file="VerifySignAttribute.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/12/24 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TZ.vNext.Core;
using TZ.vNext.ViewModel.Params;

namespace TZ.vNext.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VerifySignAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var apiParam = context.ActionArguments.Values.SingleOrDefault(x => x is ApiParamInfo);

            if (apiParam == null)
            {
                context.Result = new NotFoundObjectResult("参数错误，请求失败");
                return;
            }

            var data = apiParam as ApiParamInfo;
  
            if (data.Params == null || string.IsNullOrEmpty(data.Sign) )
            {
                context.Result = new BadRequestObjectResult("签名为空，请求无效");
                return;
            }

            if (string.IsNullOrEmpty(data.TimeStamp) || !long.TryParse(data.TimeStamp, out long timeStamp))
            {
                context.Result = new BadRequestObjectResult("时间戳错误，请求无效");
                return;
            }

            //验签
            SortedDictionary<string, string> paras = new SortedDictionary<string, string>(data.Params);
            if (!SecuritySign.VerifyWithTimeStamp(paras, data.Sign, timeStamp))
            {
                context.Result = new BadRequestObjectResult("验签失败，请求无效");
                return;
            }

            var token = data.RequestId;

            //请求唯一性校验
            if (!TokenManager.GetInstance.Add(token))
            {
                token = string.Empty;
            }

            if (!string.IsNullOrEmpty(token))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new BadRequestObjectResult("请求参数无效：" + token);
                return;
            }
        }
    }
}
