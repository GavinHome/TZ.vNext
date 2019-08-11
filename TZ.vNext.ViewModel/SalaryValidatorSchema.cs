//-----------------------------------------------------------------------------------
// <copyright file="SalaryValidatorSchema.cs" company="TZ.vNext">
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
    /// 薪酬项校验参数
    /// </summary>
    public class SalaryValidatorSchema
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}