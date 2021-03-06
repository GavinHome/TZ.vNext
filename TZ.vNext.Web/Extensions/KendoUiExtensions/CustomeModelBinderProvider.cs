﻿//-----------------------------------------------------------------------------------
// <copyright file="CustomeModelBinderProvider.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Kendo.Mvc.UI;
using TZ.vNext.Web.Infrastructure.KendoUiExtensions;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Web.KendoUiExtensions
{
    public class CustomeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            GuardUtils.NotNull(context, nameof(context));
            if (context.Metadata.ModelType == typeof(DataSourceRequest))
            {
                return new CustomDataSourceRequestModelBinder();
            }

            return null;
        }
    }
}
