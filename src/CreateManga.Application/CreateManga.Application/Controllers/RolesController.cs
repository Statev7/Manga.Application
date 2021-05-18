namespace CreateManga.Application.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Constants.RolesConstants;

    [Authorize(Roles = RolesConstants.ADMIN_ROLE_NAME)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Create()
        {
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

            await this.roleManager.CreateAsync(userRole);
            await this.roleManager.CreateAsync(editorRole);
            await this.roleManager.CreateAsync(authorRole);
            await this.roleManager.CreateAsync(adminRole);

            return this.Redirect("/");
        }
    }
}
