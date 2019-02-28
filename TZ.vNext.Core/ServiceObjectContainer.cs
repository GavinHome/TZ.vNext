//-----------------------------------------------------------------------------------
// <copyright file="ServiceObjectContainer.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/12/28 15:58:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace TZ.vNext.Core
{
    public static class ServiceObjectContainer
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
