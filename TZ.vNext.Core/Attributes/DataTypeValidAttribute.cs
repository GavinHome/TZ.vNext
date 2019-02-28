//  -----------------------------------------------------------------------
//  <copyright file="DataTypeValidAttribute.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
//   </copyright>
//  <author>gaoxiaoyu</author>
//  <date>2019-1-11</date>
//  -----------------------------------------------------------------------

namespace TZ.vNext.Core.Attributes
{
    public class DataTypeValidAttribute : System.Attribute
    {
       public DataTypeValidAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName { get; set; }
    }
}
