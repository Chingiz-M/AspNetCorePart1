using System.Net.Http;

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
    }
}
