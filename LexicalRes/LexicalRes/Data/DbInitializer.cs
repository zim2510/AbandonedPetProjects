using LexicalRes.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexicalRes.Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _appDbcontext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public DbInitializer(AppDbContext appDbContext, UserManager<AppUser> userManager,
                             RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _appDbcontext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public void Seed()
        {
            string[] roles = new string[] { "Learner", "Admin" };

            if (_userManager.FindByEmailAsync("wordadmin@overthecoffee.com").Result != null)
            {
                return;
            }

            //Create User
            var user = new AppUser
            {
                UserName = _configuration.GetValue<string>("SuperAdmin:UserName"),
                Email = _configuration.GetValue<string>("SuperAdmin:Email"),
                FullName = _configuration.GetValue<string>("SuperAdmin:FullName")
            };

            //Set password
            var userResult = _userManager.CreateAsync(user, _configuration.GetValue<string>("SuperAdmin:Password")).Result;
            if (!userResult.Succeeded)
            {
                throw new Exception("Error in User seeding!");
            }

            //Create Roles
            foreach (string role in roles)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = role
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult roleResult = _roleManager.CreateAsync(identityRole).Result;

                if (!roleResult.Succeeded)
                {
                    throw new Exception("Error in Role seeding!");
                }
            }

            var result = _userManager.AddToRoleAsync(user, "Admin").Result;
        }
    }
}
