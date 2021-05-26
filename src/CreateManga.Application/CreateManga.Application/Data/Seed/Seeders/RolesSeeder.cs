namespace CreateManga.Application.Data.Seed.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CreateManga.Application.Data.Seed.Seeders.Interfaces;
    using CreateManga.Application.Constants.RolesConstants;

    public class RolesSeeder : ISeeder
    {
        public RolesSeeder()
        {
            
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityRole userRole = new IdentityRole
            {
                Name = RolesConstants.USER_ROLE_NAME,
            };
            IdentityRole editorRole = new IdentityRole
            {
                Name = RolesConstants.EDITOR_ROLE_NAME,
            };
            IdentityRole authorRole = new IdentityRole
            {
                Name = RolesConstants.AUTHOR_ROLE_NAME,
            };
            IdentityRole adminRole = new IdentityRole
            {
                Name = RolesConstants.ADMIN_ROLE_NAME,
            };

            await roleManager.CreateAsync(userRole);
            await roleManager.CreateAsync(editorRole);
            await roleManager.CreateAsync(authorRole);
            await roleManager.CreateAsync(adminRole);
        }
    }
}
