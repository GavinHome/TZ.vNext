//-----------------------------------------------------------------------------------
// <copyright file="Log4netScope.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading;

namespace TZ.vNext.Web.Log4NetExtensions
{
    public class Log4NetScope
    {
        private static AsyncLocal<Log4NetScope> _value = new AsyncLocal<Log4NetScope>();
        private readonly string _name;
        private readonly object _state;

        public Log4NetScope(string name, object state)
        {
            _name = name;
            _state = state;
        }

        public static Log4NetScope Current
        {
            get { return _value.Value; }
            set { _value.Value = value; }
        }

        public Log4NetScope Parent { get; private set; }

        public static IDisposable Push(string name, object state)
        {
            var temp = Current;
            Current = new Log4NetScope(name, state);
            Current.Parent = temp;

            return new DisposableScope();
        }

        public override string ToString()
        {
            return _state?.ToString();
        }

        private sealed class DisposableScope : IDisposable
        {
            public void Dispose()
            {
                Current = Current.Parent;
            }
        }
    }
}
