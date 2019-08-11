//-----------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="TZ.vNext">
//     Copyright TZEPM. All rights reserved.
// </copyright>
// <author>XU DA WEI</author>
// <date>Sep 20,2013</date>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TZ.vNext.Core
{
    public class PagedResult<T> : IEnumerable<T>, IQueryProvider
    {
        public int Total { get; set; }

        public IEnumerable<T> PageResults { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return PageResults.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count()
        {
            return Total;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
