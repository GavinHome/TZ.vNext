//-----------------------------------------------------------------------------------
// <copyright file="ExportExcelTitleInfo.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>wanghong</author>
// <date>2019/1/9 15:42:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;

namespace TZ.vNext.Core.Schema
{
    public class ExportExcelTitleInfo
    {
        public string PropertyTitle { get; set; }
        public int ExclColunmSize { get; set; }
        public Type PropertyType { get; set; }
    }
}
