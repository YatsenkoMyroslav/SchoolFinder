using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public abstract class ServiceClient
    {
        private readonly HttpClient _client;
        private readonly SessionStorage _sessionStorage;

        public ServiceClient(HttpClient client, SessionStorage sessionStorage)
        {
            _client = client;
            _sessionStorage = sessionStorage;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            await ProvideToken();
            HttpResponseMessage response = await _client.DeleteAsync(requestUri);
            
            return response;
        }

        protected async Task<TResult> DeleteAsync<TResult>(string requestUri, Func<HttpResponseMessage, Task<TResult>> responseReader)
        {
            HttpResponseMessage response = await DeleteAsync(requestUri);
            TResult result = await responseReader(response);

            return result;
        }

        protected async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            
            return response;
        }

        protected async Task<TResult> GetAsync<TResult>(string requestUri, Func<HttpResponseMessage, Task<TResult>> responseReader)
        {
            await ProvideToken();
            HttpResponseMessage response = await GetAsync(requestUri);
            TResult result = await responseReader(response);

            return result;
        }

        protected async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string requestUri, TValue value)
        {
            await ProvideToken();
            HttpResponseMessage response = await _client.PostAsJsonAsync(requestUri, value);

            return response;
        }

        protected async Task<TResult> PostAsJsonAsync<TValue, TResult>(string requestUri, TValue value, Func<HttpResponseMessage, Task<TResult>> responseReader)
        {
            await ProvideToken();
            HttpResponseMessage response = await PostAsJsonAsync(requestUri, value);
            TResult result = await responseReader(response);

            return result;
        }

        protected async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            await ProvideToken();
            HttpResponseMessage response = await _client.PostAsync(requestUri, content);
            
            return response;
        }

        protected async Task<TResult> PostAsync<TResult>(string requestUri, HttpContent content, Func<HttpResponseMessage, Task<TResult>> responseReader)
        {
            await ProvideToken();
            HttpResponseMessage response = await PostAsync(requestUri, content);
            TResult result = await responseReader(response);

            return result;
        }

        protected async Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string requestUri, TValue value)
        {
            await ProvideToken();
            HttpResponseMessage response = await _client.PutAsJsonAsync(requestUri, value);
           
            return response;
        }

        protected async Task<TResult> PutAsJsonAsync<TValue, TResult>(string requestUri, TValue value, Func<HttpResponseMessage, Task<TResult>> responseReader)
        {
            await ProvideToken();
            HttpResponseMessage response = await PutAsJsonAsync(requestUri, value);
            TResult result = await responseReader(response);

            return result;
        }

        private async Task ProvideToken()
        {
            AppState state = (await _sessionStorage.Get<AppState>("AppState")) ?? new AppState();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", state.Token);
        }
    }
}
