using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrators = "Admins";
        public const string Users = "Users";

    }
}
