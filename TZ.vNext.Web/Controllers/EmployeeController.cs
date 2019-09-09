//-----------------------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>yangxiaomin</author>
// <date>2019/09/02 17:15:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using TZ.vNext.Services.Contracts;
using TZ.vNext.ViewModel;
using TZ.vNext.Core.Utility;
using TZ.vNext.Web.Extensions.KendoUiExtensions;
using TZ.vNext.Web.PermissionExtensions;
using TZ.vNext.Core.Enum;
using TZ.vNext.ViewModel.Schema;

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
        ////[AuthorizePermission(FunctionsConst.Employee_MANAGEMENT)]
        public async Task<IActionResult> Init()
        {
            var result = await _employeeService.Init();
            return Json(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GridQueryEmployees([FromBody]DataSourceRequest request)
        {
            var query = await _employeeService.GetAllValid();
            return Json(await query.ToDataSourcePageResultAsync(
                    request,
                    x => x.ToViewModel<EmployeeInfo>().SetMenus(t =>
                    {
                        IList<MenuTypeEnum> menuIds = new List<MenuTypeEnum>();
                        menuIds.Add(MenuTypeEnum.Detail);
                        if (x.DataStatus == DataStatusEnum.Valid)
                        {
                            menuIds.Add(MenuTypeEnum.Edit);
                            menuIds.Add(MenuTypeEnum.Disable);
                        }
                        else
                        {
                            menuIds.Add(MenuTypeEnum.Enable);
                        }

                        return menuIds.ToList();
                    })));
        }

        [HttpPost("[action]")]
        [AuthorizePermission(FunctionsConst.Employee_LIST)]
        public async Task<IActionResult> GridQueryEmployeesGrid([FromBody]DataSourceRequest request)
        {
            var query = await _employeeService.Get();
            return Json(await query.ToDataSourcePageResultAsync(
                    request,
                    x => x.ToViewModel<EmployeeInfo>().SetMenus(t =>
                    {
                        IList<MenuTypeEnum> menuIds = new List<MenuTypeEnum>();
                        menuIds.Add(MenuTypeEnum.Detail);
                        if (x.DataStatus == DataStatusEnum.Valid)
                        {
                            menuIds.Add(MenuTypeEnum.Edit);
                            menuIds.Add(MenuTypeEnum.Disable);
                        }
                        else
                        {
                            menuIds.Add(MenuTypeEnum.Enable);
                        }

                        return menuIds.ToList();
                    })));
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.Employee_DETAIL)]
        public async Task<IActionResult> FindById([FromBody]dynamic data)
        {
            string id = data.id != null ? data.id.ToString() : string.Empty;
            return Json(await _employeeService.FindById(id));
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.Employee_CREATE)]
        public async Task<IActionResult> Save([FromBody]EmployeeInfo info)
        {
            GuardUtils.NotNull(info, nameof(info));
            var model = await _employeeService.Save(info);
            return Json(model);
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.Employee_EDIT)]
        public async Task<IActionResult> Enable([FromBody]dynamic data)
        {
            string id = data.id != null ? data.id.ToString() : string.Empty;
            var result = await _employeeService.Enable(id);
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.Employee_EDIT)]
        public async Task<IActionResult> Disable([FromBody]dynamic data)
        {
            string id = data.id != null ? data.id.ToString() : string.Empty;
            var result = await _employeeService.Disable(id);
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.Employee_DETAIL)]
        public async Task<IActionResult> CheckCode([FromQuery]EmployeeValidatorSchema data)
        {
            if (data == null)
            {
                return Json(true);
            }

            var model = await _employeeService.FindByCode(data.Code);
            return Json(string.IsNullOrEmpty(data.Code) || model == null || model.Id == data.Id);
        }
    }
}