//-----------------------------------------------------------------------------------
// <copyright file="AttachmentInfo.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.IO;

namespace TZ.vNext.ViewModel
{
    /// <summary>
    /// 附件表
    /// </summary>
    [Serializable]
    public class AttachmentInfo : ICloneable
    {
        private byte[] _content = null;
       
        #region 数据库字段
        public string Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        #endregion

        public string AddControls { get; set; }

        /// <summary>
        /// 上传后文件全路径名 (路径+文件名)
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件二进制
        /// </summary>
        public byte[] BytesContent
        {
            get
            {
                if (_content != null)
                {
                    return _content;
                }

                if (string.IsNullOrEmpty(this.Id))
                {
                    return System.IO.File.ReadAllBytes(this.FilePath);
                }

                return Array.Empty<byte>();
            }

            set
            {
                _content = value;

                if (value != null)
                {
                    this.Size = value.Length;
                }
                else
                {
                    this.Size = 0;
                }
            }
        }

        #region Override function
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var t = obj as AttachmentInfo;

            if (t == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(t.Id))
            {
                return false;
            }

            if (Id == t.Id)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                return base.GetHashCode();
            }

            return Id.GetHashCode();
        }
        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}