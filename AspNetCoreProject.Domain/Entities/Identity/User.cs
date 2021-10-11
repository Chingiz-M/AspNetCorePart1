﻿using Microsoft.AspNetCore.Identity;

namespace AspNetCoreProject.Domain.Entities.Identity
{
    class User : IdentityUser
    {
        public const string Administrator = "Admin";
        public const string DefaultAdminPass = "Password";
    }
}
