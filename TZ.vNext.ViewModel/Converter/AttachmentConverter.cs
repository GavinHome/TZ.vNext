//-----------------------------------------------------------------------------------
// <copyright file="AttachmentConverter.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using AutoMapper;
using TZ.vNext.Model;

namespace TZ.vNext.ViewModel
{
    public class AttachmentConverter : ITypeConverter<AttachmentInfo, Attachment>
    {
        private static string _baseFilePath = TZ.vNext.Core.Const.CommonConstant.DefaultAttachementStorageDirectory;

        public Attachment Convert(AttachmentInfo source, Attachment destination, ResolutionContext context)
        {
            if (destination != null && source != null && source.Id == destination.Id.ToString())
            {
                return destination;
            }

            if (source == null)
            {
                return null;
            }

            Attachment converted = new Attachment();
            converted.Ext = source.Ext;
            converted.Name = source.Name;
            converted.Size = source.Size;
            converted.CreateAt = DateTime.Now;

            if (!string.IsNullOrEmpty(source.Url))
            {
                converted.Id = Guid.Parse(source.Id);
                converted.Url = source.Url;
            }
            else
            {
                if (System.IO.File.Exists(source.FilePath))
                {
                    source.BytesContent = System.IO.File.ReadAllBytes(source.FilePath);
                    source.Url = Save(source.BytesContent);

                    string file = System.IO.Path.Combine(_baseFilePath, source.Url);
                    converted.Size = (new System.IO.FileInfo(file)).Length;
                    converted.Url = source.Url;
                    ////converted.Id = Guid.Parse(source.Id);
                }
                else
                {
                    return null;
                    //// throw new Exception("文件不存在：" + source.FilePath);
                }
            }

            return converted;
        }

        private string Save(byte[] content)
        {
            string monthFolder = DateTime.Now.ToString("yyyyMM");

            //按月存放
            string fileFolder = System.IO.Path.Combine(_baseFilePath, monthFolder);

            if (!System.IO.Directory.Exists(fileFolder))
            {
                System.IO.Directory.CreateDirectory(fileFolder);
            }

            string filePath = Core.WebConfig.NewGuid().ToString() + ".data";
            string fileFullPath = System.IO.Path.Combine(fileFolder, filePath);

            using (var fileStream = new System.IO.FileStream(fileFullPath, System.IO.FileMode.Create))
            {
                fileStream.Write(content, 0, content.Length);
            }

            return string.Format("{0}\\{1}", monthFolder, filePath);
        }
    }
}