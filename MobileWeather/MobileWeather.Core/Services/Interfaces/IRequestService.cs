using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services.Interfaces
{
    public interface IRequestService
    {
        Task<TResult> GetAsync<TResult>(Uri uri, Dictionary<string, string> header = null);
        Task<TResult> PostAsync<TRequest, TResult>(Uri uri, TRequest data, Dictionary<string, string> header = null);
        Task<TResult> PutAsync<TRequest, TResult>(Uri uri, TRequest data, Dictionary<string, string> header = null);
        Task<TResult> DeleteAsync<TRequest, TResult>(Uri uri, TRequest data, Dictionary<string, string> header = null);
    }
}
