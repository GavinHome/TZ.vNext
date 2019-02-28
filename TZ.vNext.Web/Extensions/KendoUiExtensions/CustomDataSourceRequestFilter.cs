//-----------------------------------------------------------------------------------
// <copyright file="CustomDataSourceRequestFilter.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;

namespace TZ.vNext.Web.Extensions.KendoUiExtensions
{
    public class CustomDataSourceRequestFilter
    {
        public string Logic { get; set; }
        public IList<CustomDataSourceRequestFilterDescriptor> Filters { get; set; }
    }
}
