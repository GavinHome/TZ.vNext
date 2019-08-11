//-----------------------------------------------------------------------------------
// <copyright file="ExcelWorkBookInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/14 15:42:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace TZ.vNext.Core.Schema
{
    public class ExcelWorkBookInfo
    {
        public ExcelWorkBookInfo()
        {
            Data = new List<DataTable>();
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<DataTable> Data { get; set; }

        public string[] SheetName { get; set; }

        public string[] ChoiseStr { get; set; }

        public bool? IsShowSequenced { get; set; }
    }
}
