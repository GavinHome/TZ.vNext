//-----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>11/24/2017 10:56:52 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using TZ.vNext.Core.Attributes;
using TZ.vNext.Core.Const;
using TZ.vNext.Core.Entity;

namespace TZ.vNext.Core.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 是否为可空类型
        /// </summary>
        /// <param name="type">待判断的类型</param>
        /// <returns>判断结果</returns>
        public static bool IsNullable(this Type type)
        {
            return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 获取类型的核心类型, 用于判断可空类型的基本类型
        /// </summary>
        /// <param name="type">原始类型</param>
        /// <returns>核心类型</returns>
        public static Type GetCoreType(this Type type)
        {
            if (type != null && IsNullable(type))
            {
                return !type.IsValueType ? type : Nullable.GetUnderlyingType(type);
            }
            else
            {
                return type;
            }
        }

        /// <summary>
        /// 获取泛型类型的泛型参数类型
        /// </summary>
        /// <param name="type">原始类型</param>
        /// <returns>泛型参数类型</returns>
        public static Type GetGenericType(this Type type)
        {
            if (type.IsGenericType)
            {
                return type.GenericTypeArguments.FirstOrDefault().GetCoreType();
            }

            return type.GetCoreType();
        }

        /// <summary>
        /// get the most inner base type of a type
        /// </summary>
        /// <param name="type">type instance</param>
        /// <returns>the most inner type</returns>
        public static Type GetBaseType(this Type type)
        {
            if (type.BaseType != null)
            {
                return type.BaseType.GetBaseType();
            }

            return type;
        }

        /// <summary>
        /// to judge a type is from type or inherit from type 
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="from">from</param>
        /// <returns>bool</returns>
        public static bool IsFromType(this Type type, Type from)
        {
            if (type == from || from.IsAssignableFrom(type) || type.IsSubclassOf(from))
            {
                return true;
            }

            if (type.BaseType != null)
            {
                return type.BaseType.IsFromType(from);
            }

            return false;
        }

        /// <summary>
        /// whether a type was cached, type must be a entityset
        /// </summary>
        /// <param name="type">typeof EntitySet</param>
        /// <returns>whether a type was cached</returns>
        public static bool IsCached(this Type type)
        {
            return type.GetCustomAttributes(typeof(CacheableAttribute), true).Any();
        }

        /// <summary>
        /// get the cache key from a entityset type
        /// </summary>
        /// <param name="type">typeof EntitySet</param>
        /// <returns>the cache key</returns>
        public static string GetCacheKey(this Type type)
        {
            return CommonConstant.EntitySetCacheProfix + type.Name.ToUpper();
        }

        /// <summary>
        /// 判断成员中是否包含特性
        /// </summary>
        /// <typeparam name="T">类型参数, Attribute 类型</typeparam>
        /// <param name="member">带判断成员</param>
        /// <returns>判断结果</returns>
        public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
        {
            if (member != null)
            {
                return member.GetCustomAttribute<T>() != null;
            }

            return false;
        }

        /// <summary>
        /// 判断成员中是否包含特性
        /// </summary>
        /// <param name="member">带判断成员</param>
        /// <param name="types">包含的特性</param>
        /// <returns>判断结果</returns>
        public static bool HasAttribute(this MemberInfo member, params Type[] types)
        {
            if (member != null && types.IsNotNullOrEmpty())
            {
                for (int i = 0; i < types.Length; i++)
                {
                    if (member.GetCustomAttributes().Any(x => types.Contains(x.GetType())))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取对象上的显示名称(DisplayName特性的值)
        /// </summary>
        /// <param name="member">待获取对象</param>
        /// <returns>显示名称</returns>
        public static string GetDisplayName(this MemberInfo member)
        {
            string result = null;
            if (member != null)
            {
                var display = member.GetCustomAttribute<DisplayAttribute>();
                if (display != null)
                {
                    result = display.Name;
                }
                else
                {
                    var displayName = member.GetCustomAttribute<DisplayNameAttribute>();
                    if (displayName != null)
                    {
                        result = displayName.DisplayName;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取一个对象中对于类型 T 的引用的外键名称集合
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="type">包含T类型引用的对象</param>
        /// <returns>外键名集合</returns>
        public static List<string> GetForeignKeys<T>(this Type type) where T : EntitySet
        {
            if (type != null)
            {
                var properties = type.GetProperties().Where(x => x.PropertyType.IsFromType(typeof(T))).ToList();
                if (properties.IsNotNullOrEmpty())
                {
                    var list = new List<string>();
                    foreach (var prop in properties)
                    {
                        var fk = prop.GetCustomAttribute<ForeignKeyAttribute>();
                        if (fk != null)
                        {
                            list.Add(fk.Name);
                        }
                        else
                        {
                            list.Add(prop.Name + nameof(EntitySet.Id));
                        }
                    }
                }
            }

            return Array.Empty<string>().ToList();
        }

        /// <summary>
        /// 获取一个对象中对于类型 t 的引用的外键名称集合
        /// </summary>
        /// <param name="type">包含类型 t 引用的对象</param>
        /// <param name="t">类型 t</param>
        /// <returns>外键名集合</returns>
        public static List<string> GetForeignKeys(this Type type, Type t)
        {
            if (type != null)
            {
                var properties = type.GetProperties().Where(x => x.PropertyType.IsFromType(t)).ToList();
                if (properties.IsNotNullOrEmpty())
                {
                    var list = new List<string>();
                    foreach (var prop in properties)
                    {
                        var fk = prop.GetCustomAttribute<ForeignKeyAttribute>();
                        if (fk != null)
                        {
                            list.Add(fk.Name);
                        }
                        else
                        {
                            list.Add(prop.Name + nameof(EntitySet.Id));
                        }
                    }
                }
            }

            return Array.Empty<string>().ToList();
        }

        /// <summary>
        /// 获取对象obj中对于类型 T 的第一个引用的外键名称
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="type">包含类型 T 引用的对象</param>
        /// <returns>外键名</returns>
        public static string GetForeignKey<T>(this Type type) where T : IEntitySet
        {
            if (type != null)
            {
                var prop = type.GetProperties().FirstOrDefault(x => x.PropertyType.IsFromType(typeof(T)));
                if (prop != null)
                {
                    var fk = prop.GetCustomAttribute<ForeignKeyAttribute>();
                    if (fk != null)
                    {
                        return fk.Name;
                    }

                    return prop.Name + nameof(EntitySet.Id);
                }
            }

            return null;
        }

        /// <summary>
        /// 获取对象obj中对于类型 t 的第一个引用的外键名称
        /// </summary>
        /// <param name="type">包含类型 t 引用的对象</param>
        /// <param name="t">类型 t</param>
        /// <returns>外键名</returns>
        public static string GetForeignKey(this Type type, Type t)
        {
            if (type != null)
            {
                var prop = type.GetProperties().FirstOrDefault(x => x.PropertyType.IsFromType(t));
                if (prop != null)
                {
                    var fk = prop.GetCustomAttribute<ForeignKeyAttribute>();
                    if (fk != null)
                    {
                        return fk.Name;
                    }

                    return prop.Name + nameof(EntitySet.Id);
                }
            }

            return null;
        }

        /// <summary>
        /// 根据外键属性获取导航属性
        /// 外键对于的导航属性为将外键设置为 ForeignKey 特性的属性或者末尾加上 Id 等于外键列名的属性
        /// </summary>
        /// <param name="property">外键属性</param>
        /// <returns>导航属性</returns>
        public static PropertyInfo GetForeignKeyNavigationProperty(this PropertyInfo property)
        {
            var type = property.DeclaringType;
            var prop = type.GetProperties().FirstOrDefault(x => x.GetCustomAttributes<ForeignKeyAttribute>().Any(a => a.Name == property.Name));
            if (prop == null)
            {
                prop = type.GetProperty(property.Name.Substring(0, property.Name.Length - nameof(EntitySet.Id).Length));
            }

            return prop;
        }

        /// <summary>
        /// 获取NavigableAttribute指定列的值
        /// </summary>
        /// <param name="type">要获取NavigableAttribute类型</param>
        /// <returns>对象的NavigableAttribute指定的列</returns>
        public static PropertyInfo GetNavigationProperty(this Type type)
        {
            return type.GetProperties().FirstOrDefault(x => !x.PropertyType.IsFromType(typeof(EntitySet)) && !x.PropertyType.IsFromType(typeof(ICollection<EntitySet>)) && x.GetCustomAttribute<NavigableAttribute>() != null);
        }

        public static void Each<T>(this IEnumerable<T> instance, Action<T, int> action)
        {
            int num = 0;
            foreach (T t in instance)
            {
                int num1 = num;
                num = num1 + 1;
                action(t, num1);
            }
        }

        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (T t in instance)
            {
                action(t);
            }
        }
    }
}
