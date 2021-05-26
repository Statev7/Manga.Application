namespace CreateManga.Application.Data.Seed.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CreateManga.Application.Data.Seed.Seeders.Interfaces;
    using CreateManga.Application.Constants.RolesConstants;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = new IdentityUser
            {
                UserName = UsersConstants.USER_USERNAME,
                Email = UsersConstants.USER_EMAIL,
            };
            IdentityUser editor = new IdentityUser
            {
                UserName = UsersConstants.EDITOR_USERNAME,
                Email = UsersConstants.EDITOR_EMAIL,
            };
            IdentityUser author = new IdentityUser
            {
                UserName = UsersConstants.AUTHOR_USERNAME,
                Email = UsersConstants.AUTHOR_EMAIL,
            };
            IdentityUser admin = new IdentityUser
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
