using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected HttpClient client;
        protected string Address;

        protected BaseClient(HttpClient client, string Address)
        {
            this.client = client;
            this.Address = Address;
        }
        protected T Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T> GetAsync<T>(string url, CancellationToken cancel = default)
        {
            var response = await client.GetAsync(url, cancel).ConfigureAwait(false);
            return await response.
                EnsureSuccessStatusCode().
                Content.ReadFromJsonAsync<T>().
                ConfigureAwait(false);
        }
        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancel = default)
        {
            var response = await client.PostAsJsonAsync(url, item, cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancel = default)
        {
            var response = await client.PutAsJsonAsync(url, item, cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancel = default)
        {
            var response = await client.DeleteAsync(url, cancel).ConfigureAwait(false);
            return response;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        private bool _Dispose;
        public void Dispose(bool disposing)
        {
            if (_Dispose) return;
            _Dispose = true;

            if (disposing)
            {

            }
        }
    }
}
