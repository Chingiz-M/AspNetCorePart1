﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient client;
        protected string address;

        protected BaseClient(HttpClient client, string Address)
        {
            this.client = client;
            address = Address;
        }
        protected T Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T> GetAsync<T>(string url)
        {
            var response = await client.GetAsync(url).ConfigureAwait(false);
            return await response.
                EnsureSuccessStatusCode().
                Content.ReadFromJsonAsync<T>().
                ConfigureAwait(false);
        }
        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await client.PostAsJsonAsync(url, item).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await client.PutAsJsonAsync(url, item).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var response = await client.DeleteAsync(url).ConfigureAwait(false);
            return response;
        }
    }
}
