//-----------------------------------------------------------------------------------
// <copyright file="AuthorizeController.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/01/09 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TZ.vNext.Web.Controllers
{
    [Authorize]
    public class AuthorizeController : Controller
    {
    }
}
