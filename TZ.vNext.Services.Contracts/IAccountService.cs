//-----------------------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using TZ.vNext.Model;

namespace TZ.vNext.Services.Contracts
{
    /// <summary>
    /// 用户认证
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>用户信息</returns>
        Task<UserAuth> GetUserAuthInfo(string userName);

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <param name="isNoNeed">isNoNeed</param>
        /// <param name="debugKey">debugKey</param>
        /// <returns>是否成功</returns>
        bool Auth(string userName, string password, bool isNoNeed, string debugKey);
    }
}