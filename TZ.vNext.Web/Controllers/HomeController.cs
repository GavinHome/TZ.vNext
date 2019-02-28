//-----------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TZ.vNext.Core.Const;
using TZ.vNext.Core.util;

namespace TZ.vNext.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult GetGraph(string id)
        {
            var url = SystemVariableConst.Bps_GraphUrl;
            var result = RestUtil.Get(new Uri(url + "?id=" + id), null);
            return Content(result, "text/html");
        }
    }
}
