//-----------------------------------------------------------------------------------
// <copyright file="SalaryController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/17 21:43:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TZ.vNext.Core.Enum;
using TZ.vNext.Model.Enum;
using TZ.vNext.Services.Contracts;
using TZ.vNext.ViewModel;
using TZ.vNext.Core.Extensions;
using TZ.vNext.ViewModel.Schema;
using TZ.vNext.Web.PermissionExtensions;
using TZ.vNext.Core.Utility;
using TZ.vNext.Web.Extensions.KendoUiExtensions;

namespace TZ.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SalaryController : AuthorizeController
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_LIST)]
        public async Task<IActionResult> GridQuerySalaries([FromBody]DataSourceRequest request)
        {
            return Json(await
                _salaryService.Get().ToDataSourcePageResultAsync(
                    request,
                    x => x.ToViewModel<SalaryInfo>().SetMenus(t =>
                    {
                        IList<MenuTypeEnum> menuIds = new List<MenuTypeEnum>();
                        menuIds.Add(MenuTypeEnum.Detail);
                        if (t.FormType != FormType.Fixed)
                        {
                            if (t.DataStatus == DataStatusEnum.Valid)
                            {
                                menuIds.Add(MenuTypeEnum.Edit);
                                menuIds.Add(MenuTypeEnum.Disable);
                            }
                            else
                            {
                                menuIds.Add(MenuTypeEnum.Enable);
                            }
                        }

                        return menuIds.ToList();
                    })));
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_LIST)]
        public async Task<IActionResult> FindById([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            return Json(await _salaryService.FindById(id));
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_CREATE)]
        public async Task<IActionResult> Save([FromBody]SalaryInfo info)
        {
            GuardUtils.NotNull(info, nameof(info));
            info.Key = string.IsNullOrEmpty(info.Key) ? info.Name : info.Key;
            info.FormType = FormType.Import;
            info.FormName = FormType.Import.GetDescription();
            info.SalaryType = SalaryType.Import;
            var model = await _salaryService.Save(info);
            return Json(model);
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_EDIT)]
        public async Task<IActionResult> Enable([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            var result = await _salaryService.Enable(id);
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_EDIT)]
        public async Task<IActionResult> Disable([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            var result = await _salaryService.Disable(id);
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_LIST)]
        public async Task<IActionResult> CheckName([FromQuery]SalaryValidatorSchema data)
        {
            if (data == null)
            {
                return Json(true);
            }

            var model = await _salaryService.FindByName(data.Name);
            return Json(string.IsNullOrEmpty(data.Name) || model == null || model.Id == data.Id);
        }
    }
}