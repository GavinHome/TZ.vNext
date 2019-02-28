//-----------------------------------------------------------------------------------
// <copyright file="SystemVariableConst.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
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
        public static string MongoDB_Path { get; set; }
        public static string MongoDB_Name { get; set; }
        public static bool Redis_Switch { get; set; }
        public static string Redis_ConnectionString { get; set; }
        public static string Redis_DefaultKey { get; set; }
        public static string Bps_BaseUrl { get; set; }
        public static string Bps_GraphUrl { get; set; }
        public static string Bps_wfadmin { get; set; }
        public static string Bps_HistoryDbConnectionString { get; set; }
        public static bool CA_Switch { get; set; }
        public static string CA_BaseUrl { get; set; }
        public static string Dingding_AgentId { get; set; }
        public static string Dingding_CorpId { get; set; }
        public static string Dingding_CorpSecret { get; set; }
        public static string Dingding_AccessTokenExpireTime { get; set; }
        public static string Dingding_JsTicketExpireTime { get; set; }
        public static string TemplateFilesPath { get; set; }
        public static IDictionary<string, string> StaticTemplateMap { get; set; }
        public static string JobScheduler_Url { get; set; }
        public static string SalaryCalculateUrl { get; set; }
        public static bool JobScheduler_Enable { get; set; }
        public static string NoParticipantMsgJobScheduler_Url { get; set; }
        public static string BaseSalarySheetName { get; set; }
        public static string SalaryCalculateSheetName { get; set; }
        public static string SalaryDataSourceName { get; set; }
        public static dynamic SalaryTemplateFileName { get; set; }
        public static string FileProtectPassword { get; set; }
    }
}
