//-----------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 09:34:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using TZ.vNext.Services.Contracts;

namespace TZ.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : AuthorizeController
    {
        private readonly IProductService _productService;

        public UserController(IProductService productService)
        {
            _productService = productService;
        }
    }
}