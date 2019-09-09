//-----------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 09:34:46</date>
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
    public class ProductController : AuthorizeController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_LIST)]
        public async Task<IActionResult> GridQueryProducts([FromBody]DataSourceRequest request)
        {
            return Json(await
                _productService.Get().ToDataSourcePageResultAsync(
                    request,
                    x => x.ToViewModel<ProductInfo>().SetMenus(t =>
                    {
                        IList<MenuTypeEnum> menuIds = new List<MenuTypeEnum>();
                        menuIds.Add(MenuTypeEnum.Detail);
                        if (t.DataStatus == DataStatusEnum.Valid && t.CreateBy == this.HttpContext.User.GetCode())
                        {
                            menuIds.Add(MenuTypeEnum.Edit);
                            menuIds.Add(MenuTypeEnum.Delete);
                        }

                        return menuIds.ToList();
                    })));
        }

        [HttpPost]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_LIST)]
        public async Task<IActionResult> FindById([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            return Json(await _productService.FindById(id));
        }

        [HttpPost]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_CREATE)]
        public async Task<IActionResult> Save([FromBody]ProductInfo info)
        {
            GuardUtils.NotNull(info, nameof(info));
            var model = await _productService.Save(info);
            return Json(model);
        }

        [HttpPost]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_EDIT)]
        public async Task<IActionResult> Enable([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            var result = await _productService.Enable(id);
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_EDIT)]
        public async Task<IActionResult> Disable([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            var result = await _productService.Disable(id);
            return Json(result);
        }

        [HttpPost]
        [Route("[action]")]
        ////[AuthorizePermission(FunctionsConst.SALARY_BASIC_SALARY_EDIT)]
        public async Task<IActionResult> Delete([FromBody]dynamic data)
        {
            System.Guid.TryParse(data.id.ToString(), out Guid id);
            var result = await _productService.Delete(id);
            return Json(result);
        }
    }
}