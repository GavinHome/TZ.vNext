//-----------------------------------------------------------------------------------
// <copyright file="CodesController.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using TZ.vNext.Core;
using TZ.vNext.Core.Enum;
using TZ.vNext.Model;
using TZ.vNext.Model.Context;

namespace TZ.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CodesController : BaseController
    {
        public CodesController(AppDbContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<Code> options)
        {
            ////return Ok(await GetPageResultAsync<Customer>(options));
            return await GetToActionResultAsync<Code>(options);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TreeQueryCodes(int codeType, Guid? parentId)
        {
            var query = (await GetAsync<Code>()).Where(x => x.CodeTypeId == codeType && x.ParentId == parentId && x.DataState == (int)DataStatusEnum.Valid).OrderBy(x => x.OrderIndex);
            var results = query.Select(x => new { Value = x.Id, Label = x.Name, IsLeaf = Get<Code>().Where(c => c.ParentId == x.Id && c.DataState == (int)DataStatusEnum.Valid).Count() == 0 });
            return Json(results);
        }
    }
}
