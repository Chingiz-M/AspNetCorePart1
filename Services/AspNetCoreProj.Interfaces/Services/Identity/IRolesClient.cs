using AspNetCoreProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreProj.Interfaces.Services.Identity
{
    interface IRolesClient : IRoleStore<Role>
    {
    }
}
