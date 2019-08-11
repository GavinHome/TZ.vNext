//-----------------------------------------------------------------------------------
// <copyright file="CustomDataSourceRequestModelBinder.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Web.Extensions.KendoUiExtensions;

namespace TZ.vNext.Web.Infrastructure.KendoUiExtensions
{
    public class CustomDataSourceRequestModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var body = await bindingContext.ActionContext.HttpContext.ReadAsStringAsync();
            DataSourceRequest request = JsonConvert.DeserializeObject<DataSourceRequest>(body);

            request = request ?? new DataSourceRequest();

            request.Filters = ParseFilters(body);

            request.Sorts = ParseSorts(body);

            bindingContext.Result = ModelBindingResult.Success(request);
        }

        private static IList<IFilterDescriptor> ParseFilters(string body)
        {
            IList<IFilterDescriptor> filters = null;
            dynamic json = JsonConvert.DeserializeObject<dynamic>(body);
            if (json != null && json.filter != null)
            {
                dynamic filter = json.filter;
                if (filter.filters != null)
                {
                    if (filter.filters.ToString().Contains("logic"))
                    {
                        filters = ParseCompositeFilters(filter.ToString());
                    }
                    else
                    {
                        filters = ParseSimpleFilters(filter.ToString());
                    }
                }
            }

            return filters ?? FilterDescriptorFactory.Create(string.Empty);
        }

        private static IList<IFilterDescriptor> ParseSimpleFilters(string filterInput)
        {
            CustomDataSourceRequestFilter filters = JsonConvert.DeserializeObject<CustomDataSourceRequestFilter>(filterInput);
            return FilterDescriptorFactory.Create(FilterFormat(filters));
        }

        private static IList<IFilterDescriptor> ParseCompositeFilters(string filterInput)
        {
            var compositefilterResult = new StringBuilder();
            CustomDataSourceRequestCompositeFilter filters = JsonConvert.DeserializeObject<CustomDataSourceRequestCompositeFilter>(filterInput);
            if (filters != null && filters.Filters != null)
            {
                var balance = filters.Filters.Count;
                foreach (var filter in filters.Filters)
                {
                    balance--;
                    string filterResult = FilterFormat(filter);                    
                    if (!string.IsNullOrEmpty(filterResult))
                    {
                        compositefilterResult.Append($"({filterResult})");                       
                        if (balance > 0)
                        {
                            compositefilterResult.Append($"~{filters.Logic}~");
                        }
                    }
                }
            }

            return FilterDescriptorFactory.Create(compositefilterResult.ToString());
        }

        private static string FilterFormat(CustomDataSourceRequestFilter filtersParser)
        {
            var filterResult = new StringBuilder();
            if (filtersParser != null && filtersParser.Filters != null)
            {
                var balance = filtersParser.Filters.Count;
                foreach (var item in filtersParser.Filters)
                {
                    filterResult.Append($"{item.Field}~{item.Operator}~'{item.Value}'");

                    balance--;
                    if (balance > 0)
                    {
                        filterResult.Append($"~{filtersParser.Logic}~");
                    }
                }
            }

            return filterResult.ToString();
        }

        private static IList<SortDescriptor> ParseSorts(string body)
        {
            dynamic json = JsonConvert.DeserializeObject<dynamic>(body);
            IList<SortDescriptor> sortDescriptors = new List<SortDescriptor>();
            if (json != null && json.sort != null)
            {
                IList<CustomDataSourceRequestSort> sortsParser = JsonConvert.DeserializeObject<IList<CustomDataSourceRequestSort>>(json.sort.ToString());

                ////if (sortsParser != null && sortsParser.Count > 0)
                ////{
                ////    foreach (var item in sortsParser)
                ////    {
                ////        sortDescriptors.Add(new SortDescriptor(item.Field, item.Dir == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending));
                ////    }
                ////}

                var sortResult = new StringBuilder();
                if (sortsParser != null && sortsParser.Count > 0)
                {
                    var balance = sortsParser.Count;
                    foreach (var item in sortsParser)
                    {
                        sortResult.Append($"{item.Field}-{item.Dir}");

                        balance--;
                        if (balance > 0)
                        {
                            sortResult.Append("~");
                        }
                    }
                }

                sortDescriptors = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(sortResult.ToString());
            }

            return sortDescriptors;
        }
    }
}