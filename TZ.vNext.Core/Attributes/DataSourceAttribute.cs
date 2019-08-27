//-----------------------------------------------------------------------
// <copyright file="DataSourceAttribute.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>杨晓民</author>
// <date>2019.08.27</date>
//-----------------------------------------------------------------------

using System;
namespace TZ.vNext.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
    public class DataSourceAttribute : System.Attribute
    {
        private string metaUrl = "/api/SuperForm/GridQuerySchema";
        public DataSourceAttribute(string name)
        {
            Name = name;
            MetaUrl = metaUrl;
        }

        public DataSourceAttribute(string name, string url)
        {
            Name = name;
            Url = url;
            MetaUrl = metaUrl;
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string MetaUrl { get; set; }
    }
}
