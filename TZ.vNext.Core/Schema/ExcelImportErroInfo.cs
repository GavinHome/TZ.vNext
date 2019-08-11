//-----------------------------------------------------------------------------------
// <copyright file="ExcelImportErroInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
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
