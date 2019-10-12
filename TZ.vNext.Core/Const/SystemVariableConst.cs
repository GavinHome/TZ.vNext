//-----------------------------------------------------------------------------------
// <copyright file="SystemVariableConst.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/14 9:06:59</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TZ.vNext.Core.Const
{
    public static class SystemVariableConst
    {
        public static string Redis_ConnectionString { get; set; }
        public static string Redis_DefaultKey { get; set; }

        public static string Dingding_AgentId { get; set; }
        public static string Dingding_CorpId { get; set; }
        public static string Dingding_CorpSecret { get; set; }
        public static string Dingding_AccessTokenExpireTime { get; set; }
        public static string Dingding_JsTicketExpireTime { get; set; }

        public static string TemplateFilesPath { get; set; }
        public static IDictionary<string, string> StaticTemplateMap { get; set; }
    }
}
