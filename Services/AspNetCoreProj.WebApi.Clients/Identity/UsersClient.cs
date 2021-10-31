using AspNetCoreProj.Interfaces;
using AspNetCoreProj.WebApi.Clients.Base;
using System.Net.Http;

namespace AspNetCoreProj.WebApi.Clients.Identity
{
    public class UsersClient : BaseClient
    {
        public UsersClient(HttpClient client) : base(client, WebApiAddresses.Identity.Users)
        {
        }
    }
}
