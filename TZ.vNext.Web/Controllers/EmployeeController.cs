//-----------------------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>yangxiaomin</author>
// <date>2019/09/02 17:15:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TZ.vNext.Services.Contracts;
using TZ.vNext.ViewModel;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost, HttpGet]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.)]
        public async Task<IActionResult> Init()
        {
            var result = await _employeeService.Init();
            return Json(result);
        }
    }
}