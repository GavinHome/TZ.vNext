//-----------------------------------------------------------------------
// <copyright file="IEntitySetOfType.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>7/27/2017 10:14:21 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;

namespace TZ.vNext.ViewModel
{
    public interface IViewModelOfType<TKey> : IViewModel
    {
        new TKey Id { get; set; }
    }
}
