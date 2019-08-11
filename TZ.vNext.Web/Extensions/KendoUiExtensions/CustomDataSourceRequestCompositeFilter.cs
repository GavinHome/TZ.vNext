//-----------------------------------------------------------------------------------
// <copyright file="CustomDataSourceRequestCompositeFilter.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;

namespace TZ.vNext.Web.Extensions.KendoUiExtensions
{
    public class CustomDataSourceRequestCompositeFilter
    {
        public string Logic { get; set; }
        public IList<CustomDataSourceRequestFilter> Filters { get; set; }
    }
}
