//-----------------------------------------------------------------------------------
// <copyright file="LogController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/01/7 17:47:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TZ.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Index([FromBody]dynamic data)
        {
            _logger.LogInformation("------------Front-End Log Report Start------------");
            _logger.LogError($"{Newtonsoft.Json.JsonConvert.SerializeObject(data)}");
            _logger.LogInformation("------------Front-End Log Report End------------");
            return Json(true);
        }
    }
}
