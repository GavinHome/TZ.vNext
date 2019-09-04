//-----------------------------------------------------------------------------------
// <copyright file="FormDataService.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/27 15:49:47</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TZ.vNext.Core.Attributes;
using TZ.vNext.Model;
using TZ.vNext.Services.Contracts;

namespace TZ.vNext.Services.Implement
{
    public class FormDataService : IFormDataService
    {
        /// <summary>
        /// 获取数据源（通用）
        /// </summary>
        /// <returns>数据源</returns>
        public IEnumerable GridQueryDataSourceMeta()
        {
            var type = GetDataSourceAssembly().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(DataSourceAttribute))).ToList();
            var result = type.Select(x => new
            {
                key = x.Name,
                value = GetDataSourceAttribute(x).Name,
                url = GetDataSourceAttribute(x).Url,
                metaUrl = GetDataSourceAttribute(x).MetaUrl
            }).ToList();

            return result;
        }

        /// <summary>
        /// 获取数据源元信息（通用）
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>数据源元信息</returns>
        public IEnumerable GridQuerySchema(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var type = GetDataSourceAssembly().Where(x => x.FullName.EndsWith(key)).FirstOrDefault();

                if (type.IsClass)
                {
                    var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(DescriptionAttribute)) && (IsNumber(x) || IsString(x))).ToList();
                    var result = properties.Select(x => new
                    {
                        field = x.Name,
                        title = GetDescription(x),
                        type = GetPropertyType(x),
                    }).ToList();

                    return result;
                }
                else if (type.IsEnum)
                {
                    var result = new List<object>();

                    result.Add(new
                    {
                        field = "Key",
                        title = "唯一标识",
                        type = "string",
                    });

                    result.Add(new
                    {
                        field = "Desc",
                        title = "描述",
                        type = "string",
                    });

                    result.Add(new
                    {
                        field = "Value",
                        title = "值",
                        type = "string",
                    });

                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取枚举数据（通用）
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>枚举数据</returns>
        public IEnumerable GridQueryEnumType(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var type = GetDataSourceAssembly().Where(x => x.FullName.EndsWith(key)).FirstOrDefault();
                if (type != null)
                {
                    var result = type.GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => new
                    {
                        Key = x.Name,
                        Desc = GetDescription(x),
                        Value = x.GetValue(type)
                    }).ToList();

                    return result;
                }
            }

            return null;
        }

        private bool IsNumber(PropertyInfo property)
        {
            return property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(float) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(long);
        }

        private bool IsString(PropertyInfo property)
        {
            return property.PropertyType == typeof(Guid) || property.PropertyType == typeof(string);
        }

        private object GetPropertyType(PropertyInfo property)
        {
            if (IsNumber(property))
            {
                return "number";
            }
            else if (IsString(property))
            {
                return "string";
            }
            else
            {
                throw new ArgumentException("参数错误");
            }
        }

        private Type[] GetDataSourceAssembly()
        {
            return Assembly.GetAssembly(typeof(Code)).GetTypes();
        }

        private DataSourceAttribute GetDataSourceAttribute(MemberInfo x)
        {
            return (DataSourceAttribute)Attribute.GetCustomAttribute(x, typeof(DataSourceAttribute));
        }

        private string GetDescription(MemberInfo x)
        {
            return ((DescriptionAttribute)Attribute.GetCustomAttribute(x, typeof(DescriptionAttribute))).Description;
        }
    }
}
