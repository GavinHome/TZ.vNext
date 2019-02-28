//-----------------------------------------------------------------------------------
// <copyright file="DisableFormValueModelBindingAttribute.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TZ.vNext.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context != null)
            {
                var factories = context.ValueProviderFactories;
                factories.RemoveType<FormValueProviderFactory>();
                factories.RemoveType<JQueryFormValueProviderFactory>();
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            ////throw new NotImplementedException();
        }
    }
}
