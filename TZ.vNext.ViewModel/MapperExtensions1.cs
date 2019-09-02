//-----------------------------------------------------------------------------------
// <copyright file="MapperExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TZ.vNext.Core.Cache;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Core.Utility;
using TZ.vNext.ViewModel.Extensions;

namespace TZ.vNext.ViewModel
{
    public static class MapperExtensions
    {
        public static IList<string> GetMenus<T>(this T model, Func<T, IList<MenuTypeEnum>> meduIdListGenerator) where T : IEntitySet
        {
            return meduIdListGenerator(model).Select(x => x.ToString()).ToList();
        }

        public static IList<string> GetViewMenus<TViewModel>(this TViewModel model, Func<TViewModel, IList<MenuTypeEnum>> meduIdListGenerator) where TViewModel : IViewModel
        {
            return meduIdListGenerator(model).Select(x => x.ToString()).ToList();
        }

        public static List<AttachmentInfo> GetUploadFile(this ICache cache, string controlId)
        {
            if (cache.TryGetValue(Core.Const.CommonConstant.AttachmentCacheName, out object cacheFiles))
            {
                var list = cacheFiles as List<AttachmentInfo> ?? new List<AttachmentInfo>();

                if (!string.IsNullOrEmpty(controlId))
                {
                    list = list.Where(x => x.AddControls == controlId).ToList();
                }

                return list;
            }

            return new List<AttachmentInfo>();
        }

        public static void SetUploadFile(this ICache cache, string controlId, IList<AttachmentInfo> files)
        {
            cache.DelUploadFile(controlId);

            cache.TryGetValue(Core.Const.CommonConstant.AttachmentCacheName, out object cacheFiles);
            var list = cacheFiles as List<AttachmentInfo> ?? new List<AttachmentInfo>();

            if (!string.IsNullOrEmpty(controlId) && files.Count > 0)
            {
                foreach (var item in files)
                {
                    item.AddControls = controlId;
                }

                list.AddRange(files);

                cache.Set(Core.Const.CommonConstant.AttachmentCacheName, list);
            }
        }

        public static void DelUploadFile(this ICache cache, string controlId)
        {
            if (cache.TryGetValue(Core.Const.CommonConstant.AttachmentCacheName, out object cacheFiles))
            {
                var list = cacheFiles as List<AttachmentInfo> ?? new List<AttachmentInfo>();

                if (!string.IsNullOrEmpty(controlId))
                {
                    list = list.Where(x => x.AddControls != controlId).ToList();
                }

                cache.Set(Core.Const.CommonConstant.AttachmentCacheName, list);
            }
        }

        public static TViewModel SetClaimsPrincipal<TViewModel>(this TViewModel model, ClaimsPrincipal user) where TViewModel : IViewModel
        {
            //GuardUtils.NotNull(model, nameof(model));
            if (model.IsNotNull())
            {
                model.User = user;
            }
            return model;
        }

        public static TViewModel SetMenus<TViewModel>(this TViewModel model, IList<string> menus) where TViewModel : IViewModel
        {
            if (model.IsNotNull())
            {
                model.Menus = menus;
            }

            return model;
        }

        public static TViewModel SetMenus<TViewModel>(this TViewModel model, Func<TViewModel, IList<MenuTypeEnum>> meduIdListGenerator) where TViewModel : IViewModel
        {
            if (model.IsNotNull())
            {
                model.Menus = model.GetViewMenus(meduIdListGenerator);
            }

            return model;
        }

        public static TModel ToModel<TModel>(this BaseInfo model) where TModel : class, IEntitySet
        {
            TModel t = Mapping.Default<BaseInfo, TModel>(model);
            if (model != null && t != null)
            {
                t.SetEntityPrincipal(model.User);
            }

            return t;
        }

        public static TModel ToModel<TModel>(this MongoBaseInfo model) where TModel : class, IEntitySet
        {
            TModel t = Mapping.Default<MongoBaseInfo, TModel>(model);
            if (model != null && t != null)
            {
                t.SetEntityPrincipal(model.User);
            }

            return t;
        }

        public static TViewModel ToViewModel<TViewModel>(this IEntitySet entity) where TViewModel : class
        {
            TViewModel t = Mapping.Default<IEntitySet, TViewModel>(entity);
            return t;
        }

        public static IList<TViewModel> ToViewModels<TModel, TViewModel>(this IList<TModel> models) where TModel : IEntitySet
        {
            IList<TViewModel> t = Mapping.Default<IList<TModel>, IList<TViewModel>>(models);
            return t;
        }

        public static IList<TModel> ToModels<TViewModel, TModel>(this IList<TViewModel> viewModels) where TViewModel : IViewModel
        {
            IList<TModel> t = Mapping.Default<IList<TViewModel>, IList<TModel>>(viewModels);
            foreach (var item in t)
            {
                if (t.IsNotNullOrEmpty())
                {
                    t.SetEntityPrincipal(viewModels.First().User);
                }
            }

            return t;
        }
    }
}