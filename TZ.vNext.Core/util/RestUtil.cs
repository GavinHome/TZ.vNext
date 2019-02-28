//-----------------------------------------------------------------------
// <copyright file="RestUtil.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>Liyuhang</author>
// <date>2018-05-17 11:04:41</date>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using RestSharp;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Core.util
{
    public static class RestUtil
    {
        #region 请求封装

        public static string Get(Uri url, dynamic requestData)
        {
            var response = SendRequest(url, Method.GET, requestData);
            return response.Content;
        }

        public static string Post(Uri url, dynamic requestData)
        {
            var response = SendRequest(url, Method.POST, requestData);
            return response.Content;
        }

        public static string Put(Uri url, dynamic requestData)
        {
            var response = SendRequest(url, Method.PUT, requestData);
            return response.Content;
        }

        public static string Patch(Uri url, dynamic requestData)
        {
            var response = SendRequest(url, Method.PATCH, requestData);
            return response.Content;
        }

        public static string Delete(Uri url, dynamic requestData)
        {
            var response = SendRequest(url, Method.DELETE, requestData);
            return response.Content;
        }

        public static T Get<T>(Uri url, dynamic requestData) where T : new()
        {
            return SendRequest<T>(url, Method.GET, requestData);
        }

        public static T Post<T>(Uri url, dynamic requestData) where T : new()
        {
            return SendRequest<T>(url, Method.POST, requestData, RequestBodyContentType.Json);
        }

        public static T Put<T>(Uri url, dynamic requestData) where T : new()
        {
            return SendRequest<T>(url, Method.PUT, requestData, RequestBodyContentType.Json);
        }

        public static T Patch<T>(Uri url, dynamic requestData) where T : new()
        {
            return SendRequest<T>(url, Method.PATCH, requestData, RequestBodyContentType.Json);
        }

        public static T Delete<T>(Uri url, dynamic requestData) where T : new()
        {
            return SendRequest<T>(url, Method.DELETE, requestData, RequestBodyContentType.Json);
        }

        #endregion

        public static IRestResponse SendRequest(Uri requestUrl, Method method, dynamic requestData = null, RequestBodyContentType contentType = RequestBodyContentType.Json)
        {
            var client = new RestClient(requestUrl);
            var request = GetRequest(method, requestData, contentType);
            var result = client.Execute(request);

            if (!result.IsSuccessful)
            {
                ////throw new Exception(result.ErrorMessage);
                throw new InvalidOperationException(result.ErrorMessage);
            }
            else
            {
                return result;
            }
        }

        public static T SendRequest<T>(Uri requestUrl, Method method, dynamic requestData = null, RequestBodyContentType contentType = RequestBodyContentType.Json) where T : new()
        {
            var client = new RestClient(requestUrl);
            var request = GetRequest(method, requestData, contentType);
            var result = client.Execute<T>(request);

            if (!result.IsSuccessful)
            {
                ////throw new Exception(result.ErrorMessage);
                throw new InvalidOperationException(result.ErrorMessage);
            }
            else
            {
                return result.Data;
            }
        }

        private static RestRequest GetRequest(Method method, dynamic requestData = null, RequestBodyContentType contentType = RequestBodyContentType.Flatten)
        {
            var request = new RestRequest(method);
            request.DateFormat = "yyyy-MM-dd HH:mm:ss";

            if (contentType == RequestBodyContentType.None || method == Method.GET)
            {
                var parameters = ToDic(requestData);
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        request.AddUrlSegment(p.Key, p.Value);
                    }
                }
            }
            else if (contentType == RequestBodyContentType.Json)
            {
                if (requestData != null)
                {
                    request.AddJsonBody(requestData);
                }
            }
            else if (contentType == RequestBodyContentType.Flatten)
            {
                if (requestData != null)
                {
                    request.AddObject(requestData);
                }

                ////request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            }

            return request;
        }

        private static IDictionary<string, object> ToDic(dynamic postData)
        {
            if (postData == null)
            {
                return new Dictionary<string, object>();
            }
            else
            {
                if (!(postData is ExpandoObject))
                {
                    postData = ((object)postData).ToDynamic();
                }

                IDictionary<string, object> data = postData as IDictionary<string, object>;

                return data;
            }
        }

        private static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                expando.Add(property.Name, property.GetValue(value));
            }

            return (ExpandoObject)expando;
        }
    }
}