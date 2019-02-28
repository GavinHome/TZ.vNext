//-----------------------------------------------------------------------------------
// <copyright file="ApiParamInfo.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/12/24 10:48:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;

namespace TZ.vNext.ViewModel.Params
{
    public class ApiParamInfo
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 签名参数
        /// </summary>
        public IDictionary<string, string> Params { get; set; }
    }
}
