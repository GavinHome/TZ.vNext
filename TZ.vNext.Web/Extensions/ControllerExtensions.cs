//-----------------------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/14 15:42:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Text;
using Microsoft.AspNetCore.Mvc;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Web
{
    public static class ControllerExtensions
    {
        public static IActionResult ConvertFile(this ControllerBase c, string filePath, string fileName)
        {
            GuardUtils.NotNull(c, nameof(c));
            if (filePath == null || !System.IO.File.Exists(filePath))
            {
                return c.NotFound();
            }

            byte[] convertedContent = System.IO.File.ReadAllBytes(filePath);

            return c.ConvertFile(convertedContent, string.Empty);
        }

        public static IActionResult ConvertFile(this ControllerBase c, byte[] bytes, string fileName)
        {
            GuardUtils.NotNull(c, nameof(c));
            c.Response.Headers.Add("Content-Disposition", string.Format("attachment; filename=\"{0}\"", System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8)));
            c.Response.Headers.Add("Content-Encoding", "utf8");

            return c.File(bytes, MimeKit.MimeTypes.GetMimeType(fileName));
        }
    }
}
