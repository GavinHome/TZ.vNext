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
using Kendo.Mvc.UI;
using TZ.vNext.Web.Extensions.KendoUiExtensions;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> GridQueryEmployees([FromBody]DataSourceRequest request)
        {
            var query = await _employeeService.GetAllValidUserQuery();
            return Json(await query.ToDataSourcePageResultAsync(
                    request,
                    x => new EmployeeInfo
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    }));
        }
    }
}