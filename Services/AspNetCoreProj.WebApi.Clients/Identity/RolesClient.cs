﻿using AspNetCoreProj.Interfaces;
using AspNetCoreProj.WebApi.Clients.Base;
using System.Net.Http;

namespace AspNetCoreProj.WebApi.Clients.Identity
{
    public class RolesClient : BaseClient
    {
        public RolesClient(HttpClient client) : base(client, WebApiAddresses.Identity.Roles)
        {
        }
    }
}
