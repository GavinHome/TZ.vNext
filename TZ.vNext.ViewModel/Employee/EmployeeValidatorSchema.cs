//-----------------------------------------------------------------------------------
// <copyright file="EmployeeValidatorSchema.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>gaoxioayu</author>
// <date>2018/11/30 12:45:57</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;

namespace TZ.vNext.ViewModel.Schema
{
    /// <summary>
    /// 人员校验参数
    /// </summary>
    public class EmployeeValidatorSchema
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
    }
}