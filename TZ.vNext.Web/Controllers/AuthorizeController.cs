//-----------------------------------------------------------------------------------
// <copyright file="AuthorizeController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
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
