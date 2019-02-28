//-----------------------------------------------------------------------
// <copyright file="ColumnNameAttribute.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>wanghong</author>
// <date>2018.12.5</date>
//-----------------------------------------------------------------------
using System;

namespace TZ.vNext.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName { get; }
    }
}
