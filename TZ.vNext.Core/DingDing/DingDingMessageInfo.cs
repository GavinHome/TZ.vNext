//-----------------------------------------------------------------------
// <copyright file="DingDingMessageInfo.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>Liyuhang</author>
// <date>2016-12-09 10:06:46</date>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace TZ.vNext.Core
{
    public class DingDingMessageInfo
    {
        public IList<string> DingDingCode { get; set; }

        public string Content { get; set; }

        public string AgentId { get; set; }
    }
}