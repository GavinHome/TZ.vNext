//  -----------------------------------------------------------------------
//  <copyright file="RequiredAttribute.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
//   </copyright>
//  <author>tzxx</author>
//  <date>2018-12-25</date>
//  -----------------------------------------------------------------------

using System;

namespace TZ.vNext.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : Attribute
    {
        public RequiredAttribute()
            : this(true)
        {
        }

        public RequiredAttribute(bool isreqired)
        {
            IsRequired = isreqired;
        }

        public bool IsRequired { get; set; }
    }
}
