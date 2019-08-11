//-----------------------------------------------------------------------------------
// <copyright file="EntityFrameworkQuerybleExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace TZ.vNext.Core.Extensions
{
    public static class EntityFrameworkQuerybleExtensions
    {
        ////public static IIncludableQueryable<TEntity, TProperty> Include<TEntity, TProperty>(
        //// this IQueryable<TEntity> source,
        ////  Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        //// where TEntity : class
        ////{
        ////    return default(IIncludableQueryable<TEntity, TProperty>);
        ////}

        public static IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableQueryable<TEntity, ICollection<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return default(IIncludableQueryable<TEntity, TProperty>);
        }

        ////public static IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
        ////    this IIncludableQueryable<TEntity, TPreviousProperty> source,
        ////    Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        ////{
        ////    return default(IIncludableQueryable<TEntity, TProperty>);
        ////}
    }
}
