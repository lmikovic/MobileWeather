﻿using MobileWeather.Core.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly RestClient _restClient;

        public RequestService()
        {
            _restClient = new RestClient();

            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(Uri uri, Dictionary<string,string> header = null)
        {
            return await ExecuteHttpMethodAsync<TResult, TResult>(uri, Method.GET, header);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(Uri uri, TRequest data, Dictionary<string, string> header = null)
        {
            return await ExecuteHttpMethodAsync<TRequest, TResult>(uri, Method.POST, header, data);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(Uri uri, TRequest data, Dictionary<string, string> header = null)
        {
            return await ExecuteHttpMethodAsync<TRequest, TResult>(uri, Method.PUT, header, data);
        }

        public async Task<TResult> DeleteAsync<TRequest, TResult>(Uri uri, TRequest data, Dictionary<string, string> header = null)
        {
            return await ExecuteHttpMethodAsync<TRequest, TResult>(uri, Method.DELETE, header, data);
        }

        private async Task<TResult> ExecuteHttpMethodAsync<TRequest, TResult>(Uri uri, Method httpMethod, Dictionary<string, string> header = null, TRequest data = default(TRequest))
        {
            _restClient.BaseUrl = uri;

            RestRequest restRequest = CreateRestRequest(header);
            restRequest.Method = httpMethod;

            if ((httpMethod == Method.POST || httpMethod == Method.PUT) && data != null)
            {
                string serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
                restRequest.AddParameter("application/json; charset=utf-8", serialized, ParameterType.RequestBody);
            }

            IRestResponse response = await _restClient.ExecuteTaskAsync(restRequest);
            HandleResponse(response);
            var content = response.Content;

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(content, _serializerSettings));
        }

        private RestRequest CreateRestRequest(Dictionary<string, string> header = null)
        {
            var request = new RestRequest();

            request.AddHeader("Accept", "application/json");

            foreach(var headerItem in header ?? new Dictionary<string, string>())
            {
                request.AddHeader(headerItem.Key, headerItem.Value);
            }

            return request;
        }

        private void HandleResponse(IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                var content = response.Content;

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}
