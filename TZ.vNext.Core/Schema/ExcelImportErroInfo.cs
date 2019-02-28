//-----------------------------------------------------------------------------------
// <copyright file="ExcelImportErroInfo.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>gaoxiaoyu</author>
// <date>2019/1/11 15:42:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;

namespace TZ.vNext.Core.Schema
{
    public class ExcelImportErroInfo
    {
        public int Row { get; set; }
        public string Coloumn { get; set; }
        public string Message { get; set; }
    }
}
