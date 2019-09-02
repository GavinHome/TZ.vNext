//-----------------------------------------------------------------------------------
// <copyright file="MongoDbContextOptions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/09/02 15:04:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

namespace TZ.vNext.Core.Mongo
{
    public class MongoDbContextOptions
    {
        public string Database { get; set; }
        public string ConnectString { get; set; }
    }
}