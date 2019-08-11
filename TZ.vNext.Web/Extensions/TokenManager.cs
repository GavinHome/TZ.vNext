//-----------------------------------------------------------------------------------
// <copyright file="TokenManager.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/12/24 10:48:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;

namespace TZ.vNext.Web
{
    public class TokenManager
    {
        private static object _syncLock = new object();
        private static TokenManager _manager;
        private readonly int TOKEN_MAXCOUNT = 100000;
        private readonly ICollection<string> _tokenEnumerable = null;

        public TokenManager()
        {
            _tokenEnumerable = new List<string>();
        }

        public static TokenManager GetInstance
        {
            get
            {
                if (_manager == null)
                {
                    ////单实例对象
                    lock (_syncLock)
                    {
                        if (_manager == null)
                        {
                            _manager = new TokenManager();
                        }
                    }
                }

                return _manager;
            }
        }

        /// <summary>
        /// 添加token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>是否添加成功，成功返回true，失败返回false</returns>
        public bool Add(string token)
        {
            ////防止误判:请求唯一性
            lock (_syncLock)
            {
                if (_tokenEnumerable.Count >= TOKEN_MAXCOUNT)
                {
                    _tokenEnumerable.Clear();
                }

                if (_tokenEnumerable.Contains(token))
                {
                    return false;
                }

                _tokenEnumerable.Add(token);
                return true;
            }
        }
    }
}