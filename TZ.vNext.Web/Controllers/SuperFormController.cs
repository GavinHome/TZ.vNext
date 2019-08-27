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
        /// 数据源
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult GridQueryDataSourceMeta()
        {
            return Json(_formDataService.GridQueryDataSourceMeta());
        }

        /// <summary>
        /// 数据源元信息（通用）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult GridQuerySchema([FromBody]dynamic data)
        {
            string key = data.key == null ? string.Empty : data.key.ToString();
            return Json(_formDataService.GridQuerySchema(key));
        }

        /// <summary>
        /// 获取枚举数据（通用）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
