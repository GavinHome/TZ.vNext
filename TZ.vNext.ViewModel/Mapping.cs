//-----------------------------------------------------------------------------------
// <copyright file="Mapping.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using AutoMapper;
using TZ.vNext.Model;

namespace TZ.vNext.ViewModel
{
    public static class Mapping
    {
        private static IMapper _mapper;

        static Mapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Code, CodeInfo>();
                cfg.CreateMap<Attachment, AttachmentInfo>().ForMember(x => x.BytesContent, opt => opt.Ignore());
                cfg.CreateMap<AttachmentInfo, Attachment>().ConvertUsing<AttachmentConverter>();
                cfg.CreateMap<Salary, SalaryInfo>();
                cfg.CreateMap<SalaryInfo, Salary>();

                cfg.CreateMap<Product, ProductInfo>();
                cfg.CreateMap<ProductInfo, Product>().ForMember(p => p.ContentData, opt => opt.Ignore());

                cfg.CreateMap<Employee, EmployeeInfo>();
                ////cfg.CreateMap<EmployeeInfo, Employee>();
            });

            _mapper = config.CreateMapper();
        }

        public static TDestination Default<TSource, TDestination>(TSource obj)
        {
            return _mapper.Map<TSource, TDestination>(obj);
        }
    }
}
