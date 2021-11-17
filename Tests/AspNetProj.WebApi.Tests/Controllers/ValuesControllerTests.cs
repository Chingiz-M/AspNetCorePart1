using AspNetCoreProj.Interfaces;
using AspNetCoreProj.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspNetProj.WebApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTests
    {
        private readonly WebApplicationFactory<Startup> _Host = new();

        [TestMethod]
        public async Task GetValues_IntegrityTest()
        {
            var client = _Host.CreateClient();

            var response = await client.GetAsync(WebApiAddresses.Values); 

            response.EnsureSuccessStatusCode();

            var values = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }
    }
}
