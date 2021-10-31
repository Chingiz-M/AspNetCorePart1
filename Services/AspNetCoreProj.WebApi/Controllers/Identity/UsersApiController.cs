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
    [Route(WebApiAddresses.Identity.Users)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _UserStrore;
        public UsersApiController(WebStoreDB dB)
        {
            _UserStrore = new UserStore<User, Role, WebStoreDB>(dB);
        }
        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAll() => await _UserStrore.Users.ToArrayAsync();
    }
}
