using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Seeding
{
    public static class RoleSeeding
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> RoleManager)
        {
            string[] roles = { "User", "Admin" };
            foreach(var role in roles){
                if(!await RoleManager.RoleExistsAsync(role))
                {
                    await RoleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
    }
}
