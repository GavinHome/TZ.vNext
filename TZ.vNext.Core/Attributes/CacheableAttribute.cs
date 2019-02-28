//-----------------------------------------------------------------------
// <copyright file="CacheableAttribute.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>GouXiangyang</author>
// <date>2017/3/22 12:52:35</date>
//-----------------------------------------------------------------------
using System;

namespace TZ.vNext.Core.Attributes
{
    /// <summary>
    /// 对一个实体启用缓存
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CacheableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the CacheableAttribute class.
        /// </summary>
        public CacheableAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CacheableAttribute class. 设置一个实体的关联关系, 即当前实体在什么情况下需要更新缓存
        /// </summary>
        /// <param name="relationships">实体的关联关系</param>
        public CacheableAttribute(params string[] relationships)
        {
            Relationships = relationships;
        }

        /// <summary>
        /// 关联关系实体名称
        /// </summary>
        public string[] Relationships { get; set; }

        /// <summary>
        /// 是否可以缓存到客户端
        /// </summary>
        public bool LocalCache { get; set; }
    }
}
