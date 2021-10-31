using AspNetCoreProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreProj.Interfaces.Services.Identity
{
    interface IUsersClient : 
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserPhoneNumberStore<User>,
        IUserEmailStore<User>,
        IUserTwoFactorStore<User>,
        IUserLoginStore<User>,
        IUserClaimStore<User>
    {
    }
}
