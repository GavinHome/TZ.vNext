// -----------------------------------------------------------------------
// <copyright file="ClientConst.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>He Cong</author>
// <date>2012-10-16</date>
// -----------------------------------------------------------------------

namespace TZ.vNext.Core.Const
{
    using System;
    using System.Collections.Generic;

    public static class ClientConst
    {
        /// <summary>
        ///  In查询List的最大数量
        /// </summary>
        public const int QueryListMaxCount = 500;

        /// <summary>
        ///  序号
        /// </summary>
        public const string RowNumberDes = "序号";

        /// <summary>
        ///  序号列
        /// </summary>
        public const string RowNumber = "RowNumber";

        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 系统自动发起
        /// </summary>
        public const string SystemAutomaticGenerate = "系统自动发起";

        /// <summary>
        ///  有会签的提交流程提交人特殊状态
        /// </summary>
        public const int SubmitApprovalStatus = -99;

        #region 普元工作流 处理未找到参与者

        /// <summary>
        ///  Bps流程未找到参与者时，特殊系统管理员UserName专门改派代办的管理员）
        /// </summary>
        public const string BpsWfAdminCode = "wfadmin";

        /// <summary>
        ///  Bps流程未找到参与者时，特殊系统管理员名字（专门改派代办的管理员）
        /// </summary>
        public const string BpsWfAdminName = "未找到审核人";

        #endregion
    }
}