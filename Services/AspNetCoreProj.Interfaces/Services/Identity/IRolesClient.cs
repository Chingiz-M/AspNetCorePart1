using AspNetCoreProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreProj.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<Role>
    {
    }
}
