using AspNetCoreProj.Interfaces;
using AspNetCoreProject.DAL.Context;
using AspNetCoreProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Controllers.Identity
{
    [Route(WebApiAddresses.Identity.Roles)]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;
        public RolesApiController(WebStoreDB dB)
        {
            _RoleStore = new RoleStore<Role>(dB);
        }
        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAll() => await _RoleStore.Roles.ToArrayAsync();
    }
}
