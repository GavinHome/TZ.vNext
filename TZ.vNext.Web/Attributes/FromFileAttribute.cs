//-----------------------------------------------------------------------------------
// <copyright file="FromFileAttribute.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/12/15 10:48:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TZ.vNext.Web.Attributes
{
    public class FromFileAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.FormFile;
    }
}