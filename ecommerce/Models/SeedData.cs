using Microsoft.AspNetCore.Identity;
using System;

namespace ecommerce.Models
{
    public class SeedData
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                bool roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
        
    

