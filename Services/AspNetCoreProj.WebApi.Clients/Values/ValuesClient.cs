using AspNetCoreProj.Interfaces.TestApi;
using AspNetCoreProj.WebApi.Clients.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Clients.Values
{
    class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(HttpClient client) : base(client, "api/values")
        {
        }

        public void Add(string value)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Edit(int id, string value)
        {
            throw new NotImplementedException();
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
