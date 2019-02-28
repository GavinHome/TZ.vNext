//  -----------------------------------------------------------------------
//  <copyright file="ExportExcelAttribute.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
//   </copyright>
//  <author>tzxx</author>
//  <date>2018-12-25</date>
//  -----------------------------------------------------------------------

namespace TZ.vNext.Core.Attributes
{
    public class ExportExcelAttribute : System.Attribute
    {
        public ExportExcelAttribute()
        {
        }

        public ExportExcelAttribute(string colunmName, int colunmSize)
        {
            ColunmName = colunmName;
            ColunmSize = colunmSize;
        }

        public string ColunmName { get; set; }
        public int ColunmSize { get; set; }
    }
}
