//-----------------------------------------------------------------------
// <copyright file="DingDingMessageHelper.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>Liyuhang</author>
// <date>2016.12.9</date>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace TZ.vNext.Core
{
    public static class DingDingMessageHelper
    {
        /// <summary>
        /// 发送企业会话text消息
        /// </summary>
        /// <param name="messageInfo">会话内容</param>
        public static void SendEnterpriseText(IList<DingDingMessageInfo> messageInfo)
        {
            if (messageInfo != null && messageInfo.Any())
            {
                foreach (var info in messageInfo)
                {
                    var msgUrl = string.Format("https://oapi.dingtalk.com/message/send?access_token={0}", DingDingConfiguration.AccessToken);

                    var postData = new
                    {
                        touser = string.Join("|", info.DingDingCode),
                        agentid = string.IsNullOrEmpty(info.AgentId) ? DingDingConfiguration.AgentId : info.AgentId,
                        msgtype = "text",
                        text = new
                        {
                            content = info.Content + ", " + DateTime.Now.ToString("F")
                        }
                    };

                    DingDingConfiguration.MakePostRequest<Dictionary<string, string>>(msgUrl, Newtonsoft.Json.JsonConvert.SerializeObject(postData));
                }
            }
        }
    }
}