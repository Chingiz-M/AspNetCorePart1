using AspNetCoreProj.Interfaces;
using AspNetCoreProj.Interfaces.TestApi;
using AspNetCoreProj.WebApi.Clients.Base;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace AspNetCoreProj.WebApi.Clients.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(HttpClient client) : base(client, WebApiAddresses.Values)
        {
        }

        public void Add(string value)
        {
            var response = client.PostAsJsonAsync(address, value).Result;
            response.EnsureSuccessStatusCode();
        }

        public int Count()
        {
            var response = client.GetAsync($"{address}/count").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<int>().Result;

            return -1;
        }

        public bool Delete(int id)
        {
            var response = client.DeleteAsync($"{address}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public void Edit(int id, string value)
        {
            var response = client.PutAsJsonAsync($"{address}/{id}", value).Result;
            response.EnsureSuccessStatusCode();
        }

        public IEnumerable<string> GetAll()
        {
            var response = client.GetAsync(address).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string GetById(int id)
        {
            var response = client.GetAsync($"{address}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<string>().Result;

            return null;
        }
    }
}
