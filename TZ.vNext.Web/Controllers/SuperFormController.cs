//-----------------------------------------------------------------------------------
// <copyright file="SuperFormController.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/22 09:46:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using TZ.vNext.Services.Contracts;

namespace TZIWB.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SuperFormController : Controller
    {
        private readonly IFormDataService _formDataService;

        public SuperFormController(IFormDataService formDataService)
        {
            _formDataService = formDataService;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        [HttpPost("[action]"), HttpGet("[action]")]
        public IActionResult GridQueryDataSourceMeta()
        {
            return Json(_formDataService.GridQueryDataSourceMeta());
        }

        /// <summary>
        /// 获取数据源元信息（通用）
        /// </summary>
        /// <param name="data">参数：key</param>
        /// <returns>数据源元信息</returns>
        [HttpPost("[action]")]
        public IActionResult GridQuerySchema([FromBody]dynamic data)
        {
            string key = data.key == null ? string.Empty : data.key.ToString();
            return Json(_formDataService.GridQuerySchema(key));
        }

        /// <summary>
        /// 获取枚举数据（通用）
        /// </summary>
        /// <param name="data">参数：key</param>
        /// <returns>枚举数据</returns>
        [HttpPost("[action]")]
        public IActionResult GridQueryEnumType([FromBody]dynamic data)
        {
            string key = data.key == null ? string.Empty : data.key.ToString();
            return Json(new { Data = _formDataService.GridQueryEnumType(key) });
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Save([FromBody]dynamic info)
        {
            return Json(true);
        }
    }
}
