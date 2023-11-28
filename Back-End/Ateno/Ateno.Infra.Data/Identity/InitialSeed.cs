using Ateno.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;

namespace Ateno.Infra.Data.Identity
{
    public class InitialSeed : IInitialSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public InitialSeed(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "User";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
