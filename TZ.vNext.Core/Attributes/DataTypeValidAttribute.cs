//  -----------------------------------------------------------------------
//  <copyright file="DataTypeValidAttribute.cs" company="TZ.vNext">
//      Copyright  TZ.vNext. All rights reserved.
//   </copyright>
//  <author>??</author>
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
