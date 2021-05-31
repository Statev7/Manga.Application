namespace CreateManga.Application.Data.Seed.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CreateManga.Application.Data.Seed.Seeders.Interfaces;
    using CreateManga.Application.Constants.RolesConstants;
    using CreateManga.Application.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationUser user = new ApplicationUser
            {
                UserName = UsersConstants.USER_USERNAME,
                Email = UsersConstants.USER_EMAIL,
            };
            ApplicationUser editor = new ApplicationUser
            {
                UserName = UsersConstants.EDITOR_USERNAME,
                Email = UsersConstants.EDITOR_EMAIL,
            };
            ApplicationUser author = new ApplicationUser
            {
                UserName = UsersConstants.AUTHOR_USERNAME,
                Email = UsersConstants.AUTHOR_EMAIL,
            };
            ApplicationUser admin = new ApplicationUser
            {
                UserName = UsersConstants.ADMIN_USERNAME,
                Email = UsersConstants.ADMIN_EMAIL,
            };

            await userManager.CreateAsync(user, UsersConstants.USER_PASSWORD);
            await userManager.CreateAsync(editor, UsersConstants.EDITOR_PASSWORD);
            await userManager.CreateAsync(author, UsersConstants.AUTHOR_PASSWORD);
            await userManager.CreateAsync(admin, UsersConstants.ADMIN_PASSWORD);

            await userManager.AddToRoleAsync(user, RolesConstants.USER_ROLE_NAME);
            await userManager.AddToRoleAsync(editor, RolesConstants.EDITOR_ROLE_NAME);
            await userManager.AddToRoleAsync(author, RolesConstants.AUTHOR_ROLE_NAME);
            await userManager.AddToRoleAsync(admin, RolesConstants.ADMIN_ROLE_NAME);
        }
    }
}
